﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Serialization.Json;
using System.Web.SessionState;
using System.Runtime.Serialization;

using System.IO;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

namespace Aphysoft.Share
{
    public class Provider
    {
        #region Server

        internal static Dictionary<string, StreamClientInstance> clientInstances;

        public static Dictionary<string, StreamClientInstance> ClientInstances
        {
            get { return clientInstances; }
        }

        internal static Dictionary<string, StreamSessionInstance> sessionInstances;

        public static Dictionary<string, StreamSessionInstance> SessionInstances
        {
            get { return sessionInstances; }
        }

        internal static Dictionary<string, OnReceivedCallback> registerCallbacks;

        private static object instancesWaitSync = new object();

        internal static void ServerInit()
        {
            clientInstances = new Dictionary<string, StreamClientInstance>();
            sessionInstances = new Dictionary<string, StreamSessionInstance>();
            registerCallbacks = new Dictionary<string, OnReceivedCallback>();

            Share.Database.Execute("update session set SS_ClientsCount = 0 where SS_ClientsCount > 0");

            Service.Register(typeof(BaseStreamServiceMessage), BaseStreamServiceMessageHandler);
        }

        private static void BaseStreamServiceMessageHandler(MessageEventArgs e)
        {
            if (sessionInstances == null || clientInstances == null || registerCallbacks == null) return;

            BaseServiceMessage msg = e.Message;
            Connection connection = e.Connection;
            Type type = msg.GetType();

            BaseStreamServiceMessage streamMessage = (BaseStreamServiceMessage)msg;
            string sessionID = streamMessage.SessionID;

            if (sessionID == null)
            {
                
                connection.Reply(streamMessage);
            }

            if (type == typeof(StreamServiceMessage))
            {
                #region Stream

                StreamServiceMessage message = (StreamServiceMessage)streamMessage;

                string clientID = message.ClientID;
       
                StreamClientInstance clientInstance;

                bool newClientInstance = false;

                lock (instancesWaitSync)
                {
                    if (clientInstances.ContainsKey(clientID))
                        clientInstance = clientInstances[clientID];
                    else
                    {
                        clientInstance = new StreamClientInstance(clientID);
                        clientInstances.Add(clientID, clientInstance);
                        newClientInstance = true;

                        // add to sessionInstance, if not found, create new
                        StreamSessionInstance sessionInstance;

                        if (sessionInstances.ContainsKey(sessionID))
                        {
                            sessionInstance = sessionInstances[sessionID];

                            // existing session,
                            // the session has another client instance
                            clientInstance.SessionIndex = sessionInstance.GetAvailableIndex();
                        }
                        else
                        {
                            sessionInstance = new StreamSessionInstance(sessionID);
                            sessionInstances.Add(sessionID, sessionInstance);

                            // yes, this is new session,
                            // it means new client
                            clientInstance.SessionIndex = 0;
                        }

                        clientInstance.SessionInstance = sessionInstance;
                        sessionInstance.ClientInstances.Add(clientID, clientInstance);

                        Share.Database.Execute("update session set SS_ClientsCount = {0} where SS_SID = {1}", sessionInstance.ClientInstances.Count, sessionID);
                    }
                }

                bool updateDomain = false;

                if (newClientInstance)
                {
                    if (message.HostSessionIndex != clientInstance.SessionIndex)
                    {
                        message.MessageContinue = false;
                        message.MessageType = "updatestreamdomain";
                        message.MessageData = clientInstance.SessionIndex;

                        connection.Reply(message);

                        updateDomain = true;
                    }
                }

                if (updateDomain == false)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    do
                    {
                        do
                        {
                            if (clientInstance.DataQueue.Count > 0) break;
                            else if (stopwatch.Elapsed.TotalSeconds >= 50) break;

                            clientInstance.resetEvent.WaitOne(1000);
                        }
                        while (true);

                        if (clientInstance.DataQueue.Count > 0)
                        {
                            lock (clientInstance.dataQueueWaitSync)
                            {
                                if (clientInstance.DataQueue.Count > 0)
                                {
                                    StreamInstanceData data = clientInstance.DataQueue.Dequeue();

                                    message.MessageContinue = false;
                                    message.MessageType = data.Type;
                                    message.MessageData = data.Data;

                                    connection.Reply(message);

                                    if (clientInstance.DataQueue.Count == 0)
                                        clientInstance.resetEvent.Reset();
                                }
                            }
                        }
                        else break;

                    }
                    while (true);
                }

                message.MessageContinue = true;
                message.MessageType = null;
                message.MessageData = null;

                connection.Reply(message);

                #endregion
            }
            else if (type == typeof(ClientEndServiceMessage))
            {
                #region Client End

                ClientEndServiceMessage message = (ClientEndServiceMessage)streamMessage;

                string clientID = message.ClientID;

                lock (instancesWaitSync)
                {                    
                    if (clientInstances.ContainsKey(clientID))
                    {
                        StreamClientInstance clientInstance = clientInstances[clientID];

                        clientInstances.Remove(clientID);

                        StreamSessionInstance sessionInstance = clientInstance.SessionInstance;

                        sessionInstance.ClientInstances.Remove(clientID);

                        Share.Database.Execute("update session set SS_ClientsCount = {0} where SS_SID = {1}", sessionInstance.ClientInstances.Count, sessionID);

                        if (sessionInstance.ClientInstances.Count == 0)
                        {
                            // dont have client instances anymore, remove this
                            if (sessionInstances.ContainsKey(sessionInstance.SessionID))
                            {
                                sessionInstances.Remove(sessionInstance.SessionID);
                            }
                        }
                    }
                }

                #endregion
            }
            else if (type == typeof(RegisterServiceMessage))
            {
                #region Register

                RegisterServiceMessage message = (RegisterServiceMessage)streamMessage;

                string clientID = message.ClientID;
                string[] registers = message.Registers;
                bool force = message.Force;
                
                lock (instancesWaitSync)
                {
                    if (clientInstances.ContainsKey(clientID))
                    {
                        StreamClientInstance instance = clientInstances[clientID];

                        if (force)
                        {
                            instance.RemoveAllRegisters();
                        }
                        foreach (string register in registers)
                        {
                            if (register.StartsWith("-"))
                            {
                                string registerName = register.Substring(1);
                                if (registerName.Length > 0) instance.RemoveRegister(registerName);
                            }
                            else
                            {
                                //Service.Debug("Client " + clientID + " register " + register);

                                instance.Register(register);
                                if (registerCallbacks.ContainsKey(register))
                                {
                                    registerCallbacks[register](e);
                                }
                            }
                        }

                    }
                }

                #endregion
            }
        }

        internal static StreamSessionInstance GetSessionInstance(string sessionID)
        {
            if (sessionID != null && sessionInstances.ContainsKey(sessionID))
                return sessionInstances[sessionID];
            else
                return null;
        }

        public static string[] GetClientsByRegister(string register)
        {
            if (Service.IsServer)
            {               
                List<string> clients = new List<string>();

                foreach (KeyValuePair<string, StreamClientInstance> pair in clientInstances)
                {
                    if (pair.Value.IsRegistered(register))
                    {
                        clients.Add(pair.Key);
                    }
                }

                return clients.ToArray();
            }
            else return null;
        }

        public static KeyValuePair<string, string[]>[] GetClientsByRegisterMatch(string registerMatch)
        {
            if (Service.IsServer)
            {
                List<KeyValuePair<string, string[]>> clients = new List<KeyValuePair<string, string[]>>();

                foreach (KeyValuePair<string, StreamClientInstance> pair in clientInstances)
                {
                    string[] matchTo;
                    if (pair.Value.IsRegisteredMatch(registerMatch, out matchTo))
                    {
                        KeyValuePair<string, string[]> kvp = new KeyValuePair<string, string[]>(pair.Key, matchTo);
                        clients.Add(kvp);
                    }
                }

                return clients.ToArray();
            }
            else return null;
        }

        public static int SetActionByRegister(string register, string type, object data)
        {
            string[] clientIDs = GetClientsByRegister(register);

            foreach (string clientID in clientIDs)
            {
                StreamClientInstance clientInstance = clientInstances[clientID];
                clientInstance.SetAction(type, data);
            }

            return clientIDs.Length;
        }

        public static void SetActionByRegister(string register, object data)
        {
            SetActionByRegister(register, register, data);
        }

        public static int SetActionByRegisterMatch(string registerMatch, string type, object data)
        {
            KeyValuePair<string, string[]>[] clientIDs = GetClientsByRegisterMatch(registerMatch);

            foreach (KeyValuePair<string, string[]> pair in clientIDs)
            {
                string clientID = pair.Key;
                string[] matches = pair.Value;
                
                StreamClientInstance clientInstance = clientInstances[clientID];
                clientInstance.SetAction(type, data);
            }

            return clientIDs.Length;
        }

        public static void SetActionByRegisterMatch(string registerMatch, object data)
        {
            KeyValuePair<string, string[]>[] clientIDs = GetClientsByRegisterMatch(registerMatch);

            foreach (KeyValuePair<string, string[]> pair in clientIDs)
            {
                string clientID = pair.Key;
                string[] matches = pair.Value;
                               
                StreamClientInstance clientInstance = clientInstances[clientID];

                foreach (string match in matches)
                {
                    clientInstance.SetAction(match, data);
                }
            }
        }

        public static void RegisterCallback(string register, OnReceivedCallback method)
        {
            if (!registerCallbacks.ContainsKey(register))
                registerCallbacks.Add(register, method);
        }

        #endregion

        #region Server+Client

        #endregion

        #region Client

        internal static void Init()
        {
            Resource.Register("xhr_stream", ResourceTypes.Text, Provider.StreamBeginProcessRequest, Provider.StreamEndProcessRequest)
                .NoBufferOutput().AllowOrigin("http" + (Settings.SSLAvailable ? "s" : "") + "://" + Settings.PageDomain).AllowCredentials();
            Resource.Register("xhr_provider", ResourceTypes.JSON, Provider.ProviderBeginProcessRequest, Provider.ProviderEndProcessRequest);

            if (Settings.EnableUI)
            {
                Resource.Register("xhr_content_provider", ResourceTypes.JSON, Content.Begin, Content.End);
            }
        }

        public static string ClientID()
        {
            return Params.GetValue("c");
        }

        private static Dictionary<int, ProviderRegister> resourceRegisters = new Dictionary<int, ProviderRegister>();
        private static Dictionary<string, ProviderRegister> serviceRegisters = new Dictionary<string, ProviderRegister>();

        public static event ProviderClientDisconnectedEventHandler ClientDisconnected;
        
        internal static void StreamBeginProcessRequest(ResourceAsyncResult result)
        {
            HttpResponse httpResponse = result.Context.Response;
            HttpRequest httpRequest = result.Context.Request;

            try
            {
                httpResponse.Write("for(;;); ");

                StreamHeartbeatData hb = new StreamHeartbeatData();
                hb.serverTime = DateTime.Now;

                string heartbeat = WebUtilities.Serializer.Serialize(hb);

                StringBuilder padding = new StringBuilder();
                int ipadding = 1024 - heartbeat.Length;
                for (int i = 0; i < ipadding; i++) padding.Append(" ");
                httpResponse.Write(heartbeat + padding.ToString() + "\n");
            }
            catch
            {
                result.SetCompleted();
                return;
            }
            
            if (Service.IsConnected)
            {
                string clientID = Params.GetValue("c", result.Context);
                string sessionID = Session.ID;
                bool clientStillConnected = true;

                if (sessionID == null || clientID == null)
                {
                    try
                    {
                        httpResponse.Write("{\"t\":\"unavailable\"}\n");
                    }
                    catch (HttpException hex)
                    {
                    }
                }
                else
                {

                    try
                    {
                        httpResponse.Write("{\"t\":\"available\"}\n");
                    }
                    catch (HttpException hex)
                    {
                        clientStillConnected = false;
                    }

                    if (clientStillConnected)
                    {
                        StreamServiceMessage message = new StreamServiceMessage(sessionID);

                        //message.httpRequest.UserHostAddress;
                        message.ClientID = clientID;

                        string requestHost = httpRequest.Headers["Host"];

                        if (requestHost != null)
                        {
                            string challengeRequestBaseHost = Settings.StreamBaseSubDomain + "." + Settings.StreamDomain;
                            if (Settings.StreamBasePort != 0)
                                challengeRequestBaseHost += ":" + Settings.StreamBasePort;

                            if (requestHost == challengeRequestBaseHost)
                                message.HostSessionIndex = -1;
                            else
                            {
                                for (int ido = 0; ido < Settings.StreamSubDomains.Length; ido++)
                                {
                                    string challengeRequestHost = Settings.StreamSubDomains[ido] + "." + Settings.StreamDomain;
                                    if (Settings.StreamSubPorts[ido] != 0)
                                        challengeRequestHost += ":" + Settings.StreamSubPorts[ido];

                                    if (requestHost == challengeRequestHost)
                                    {
                                        message.HostSessionIndex = ido;
                                        break;
                                    }
                                }
                            }
                        }

                        if (Service.Send(message))
                        {
                            do
                            {
                                StreamServiceMessage response = Service.Wait(message);

                                if (!response.IsAborted)
                                {
                                    if (response.MessageContinue)
                                    {
                                        try
                                        {
                                            httpResponse.Write("{\"t\":\"continue\"}\n");
                                        }
                                        catch (HttpException hex)
                                        {
                                            clientStillConnected = false;
                                        }

                                        break;
                                    }
                                    else
                                    {
                                        if (response.MessageType == "updatestreamdomain")
                                        {
                                            int hostIndex = ((int)response.MessageData) % Settings.StreamSubDomains.Length;
                                            string hostName;

                                            if (hostIndex < Settings.StreamSubDomains.Length)
                                            {
                                                hostName = Settings.StreamSubDomains[hostIndex] + "." + Settings.StreamDomain;
                                                if (Settings.StreamSubPorts[hostIndex] != 0)
                                                    hostName += ":" + Settings.StreamSubPorts[hostIndex];
                                            }
                                            else
                                            {
                                                hostName = Settings.StreamBaseSubDomain + "." + Settings.StreamDomain;
                                                if (Settings.StreamBasePort != 0)
                                                    hostName += ":" + Settings.StreamBasePort;
                                            }

                                            try
                                            {
                                                httpResponse.Write(string.Format("{{\"t\":\"updatestreamdomain\",\"d\":\"{0}\"}}\n", hostName));
                                            }
                                            catch (HttpException hex)
                                            {
                                                clientStillConnected = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (response.MessageData == null)
                                                    httpResponse.Write(string.Format("{{\"t\":\"{0}\"}}\n", response.MessageType));
                                                else
                                                    httpResponse.Write("{\"t\":\"" + response.MessageType + "\",\"d\":" + WebUtilities.Serializer.Serialize(response.MessageData) + "}\n");
                                            }
                                            catch (HttpException hex)
                                            {
                                                clientStillConnected = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // two type                            
                                    if (Service.IsConnected == false) // if server down
                                    {
                                        string hostName = Settings.StreamBaseSubDomain + "." + Settings.StreamDomain;

                                        if (Settings.StreamBasePort != 0)
                                            hostName += ":" + Settings.StreamBasePort;

                                        httpResponse.Write(string.Format("{{\"t\":\"updatestreamdomain\",\"d\":\"{0}\"}}\n", hostName));
                                        httpResponse.Write("{\"t\":\"disconnected\"}\n");
                                    }
                                    else // if page left
                                    {
                                        // tell server that page has been left
                                        Service.Send(new ClientEndServiceMessage(clientID, sessionID));
                                        httpResponse.Write("{\"t\":\"pageend\"}\n");
                                    }

                                    break;
                                }
                            }
                            while (true);
                        }
                    }
                }
            }
            else
            {
                try
                {
                    httpResponse.Write("{\"t\":\"unavailable\"}\n");
                }
                catch (HttpException hex)
                {
                }
            }

            result.SetCompleted();
        }

        internal static void StreamEndProcessRequest(ResourceAsyncResult result)
        {
        }

        internal static void ProviderBeginProcessRequest(ResourceAsyncResult result)
        {
            HttpContext context = result.Context;
            HttpResponse response = context.Response;
            HttpRequest request = context.Request;

            //Identity.Start();

            result.Tag = 0;

            // get requested provider id
            string appids = QueryString.GetValue("i");
            string cid = QueryString.GetValue("c");

            if (cid == null) // or cid not registered
            {
                response.Status = "403 Forbidden";
            }
            else if (appids != null)
            {
                // parse to int
                int appid;
                if (int.TryParse(appids, out appid))
                {
                    // succeeded
                    ProviderPacket packet = null;
                    result.Tag = appid;

                    if (appid >= 0 && appid <= 50) // system appid
                    {
                        #region System APPID
                        if (appid == 1)
                        {
                            #region Debug
                            string message = Params.GetValue("m");

                            packet = new ProviderPacket();
                            #endregion
                        }
                        else if (appid == 10 || appid == 11)
                        {
                            #region Change Register
                            string x = Params.GetValue("x");
                            string c = ClientID();

                            if (x != null && c != null)
                            {
                                RegisterServiceMessage m = new RegisterServiceMessage(c, x.Split(StringSplitTypes.Comma, StringSplitOptions.RemoveEmptyEntries));
                                if (appid == 11) m.Force = true;
                                Service.Send(m);
                            }
                            #endregion
                        }

                        #endregion
                    }
                    else if (resourceRegisters.ContainsKey(appid))
                    {
                        ProviderRegister register = resourceRegisters[appid];
                        ResourceRequest proc = register.ResourceHandler;

                        packet = proc(result, appid);
                    }

                    if (packet == null)
                        packet = ProviderPacket.Null();
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(packet.GetType());
                    serializer.WriteObject(response.OutputStream, packet);
                }
            }
            result.SetCompleted();
        }

        internal static void ProviderEndProcessRequest(ResourceAsyncResult result)
        {
        }

        internal static void OnClientDisconnected(ProviderClientDisconnectedEventArgs e)
        {
            ClientDisconnected?.Invoke(e);
        }
        
        public static void Register(int[] ids, ResourceRequest handler)
        {
            ProviderRegister register = null;

            foreach (int id in ids)
            {
                if (!resourceRegisters.ContainsKey(id))
                {
                    if (register == null)
                        register = new ProviderRegister(handler);

                    lock (resourceRegisters)
                    {
                        resourceRegisters.Add(id, register);
                    }
                }
            }
        }

        public static void Register(int id, ResourceRequest handler)
        {
            if (!resourceRegisters.ContainsKey(id))
            {
                lock (resourceRegisters)
                {
                    resourceRegisters.Add(id, new ProviderRegister(handler));
                }
            }
        }
        
        #endregion
    }
    
    public class ProviderClientDisconnectedEventArgs : EventArgs
    {
        private string sessionID;

        public string SessionID
        {
            get { return sessionID; }
        }

        public ProviderClientDisconnectedEventArgs(string sessionID)
        {
            this.sessionID = sessionID;
        }
    }

    public delegate void ProviderRequestHandler(string data);

    public delegate void ProviderClientDisconnectedEventHandler(ProviderClientDisconnectedEventArgs e);

    public class StreamInstanceData
    {
        #region Fields

        private string type;

        private object data;        

        #endregion

        #region Properties

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public object Data
        {
            get { return data; }
            set { data = value; }
        }

        #endregion

        #region Constructor

        public StreamInstanceData(string type, object data)
        {
            this.type = type;
            this.data = data;
        }

        #endregion
    }

    public class StreamSessionInstance
    {
        #region Fields

        private string sessionID;
        
        public string SessionID
        {
            get { return sessionID; }
        }
        
        private Dictionary<string, StreamClientInstance> clientInstances;

        public Dictionary<string, StreamClientInstance> ClientInstances
        {
            get { return clientInstances; }
        }

        private string ipAddress;

        public string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        #endregion

        #region Properties
        



        #endregion

        #region Constructors

        public StreamSessionInstance(string sessionID)
        {
            clientInstances = new Dictionary<string, StreamClientInstance>();

            this.sessionID = sessionID;
        }

        #endregion

        #region Methods
        
        public int GetAvailableIndex()
        {
            int lind = -1;

            foreach (KeyValuePair<string, StreamClientInstance> pair in clientInstances)
            {
                StreamClientInstance clientInstance = pair.Value;

                int nind = clientInstance.SessionIndex;
                if ((nind - lind) > 1)
                    break;

                lind = nind;
            }

            return (lind + 1);
        }   

        #endregion
    }

    public class StreamClientInstance
    {
        #region Fields

        private string clientID;

        private List<string> registers = new List<string>();

        public object registersWaitSync = new object();

        public ManualResetEvent resetEvent = new ManualResetEvent(false);

        private Queue<StreamInstanceData> dataQueue = new Queue<StreamInstanceData>();

        public object dataQueueWaitSync = new object();

        private StreamSessionInstance sessionInstance = null;

        private int sessionIndex;

        #endregion

        #region Properties
        
        public StreamSessionInstance SessionInstance
        {
            get { return sessionInstance; }
            set { sessionInstance = value; }
        }

        public Queue<StreamInstanceData> DataQueue
        {
            get { return dataQueue; }
        }

        public int SessionIndex
        {
            get { return sessionIndex; }
            set { sessionIndex = value; }
        }

        #endregion

        #region Constructors

        public StreamClientInstance(string clientID)
        {
            this.clientID = clientID;
        }

        #endregion

        #region Methods

        public void SetAction(string type, object data)
        {
            lock (dataQueueWaitSync)
            {
                DataQueue.Enqueue(new StreamInstanceData(type, data));
                resetEvent.Set();
            }
        }

        public void Register(string register)
        {
            lock (registersWaitSync)
            {
                if (!registers.Contains(register))
                    registers.Add(register);
            }
        }

        public void RemoveRegister(string register)
        {
            lock (registersWaitSync)
            {
                if (registers.Contains(register))
                    registers.Remove(register);
            }
        }

        public void RemoveAllRegisters()
        {
            lock (registersWaitSync)
            {
                registers.Clear();
            }
        }

        public bool IsRegistered(string register)
        {
            if (registers.Contains(register)) return true;
            else return false;
        }

        public bool IsRegisteredMatch(string registerPattern, out string[] matchTo)
        {
            bool re = false;
            List<string> matchTos = new List<string>();
            
            foreach (string s in registers)
            {
                if (Regex.IsMatch(s, "^" + registerPattern + "$"))
                {
                    re = true;
                    matchTos.Add(s);
                }
            }

            matchTo = matchTos.ToArray();

            return re;
        }

        #endregion
    }

    internal class StreamHeartbeatData
    {
        #region Fields

        public string t = "heartbeat";
        public string d = "";
        public DateTime serverTime;
        public string v = Share.Version();

        #endregion
    }

    [Serializable]
    internal abstract class BaseStreamServiceMessage : SessionServiceMessage
    {
        #region Constructor
        public BaseStreamServiceMessage(string sessionID) : base(sessionID)
        {
            
        }
        #endregion
    }

    [Serializable]
    internal abstract class BaseClientServiceMessage : BaseStreamServiceMessage
    {
        #region Constructor
        public BaseClientServiceMessage(string clientID, string sessionID) : base(sessionID)
        {
            ClientID = clientID;
        }

        #endregion
    }

    [Serializable]
    internal class StreamServiceMessage : BaseStreamServiceMessage
    {
        #region Fields

        private int hostSessionIndex = -1;

        private bool messageContinue = false;

        private string messageType = null;

        private object messageData = null;

        public int HostSessionIndex
        {
            get { return hostSessionIndex; }
            set { hostSessionIndex = value; }
        }

        public bool MessageContinue
        {
            get { return messageContinue; }
            set { messageContinue = value; }
        }

        public string MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

        public object MessageData
        {
            get { return messageData; }
            set { messageData = value; }
        }

        #endregion

        #region Constructors

        public StreamServiceMessage(string sessionID) : base(sessionID)
        {
                
        }

        public StreamServiceMessage() : base("")
        {

        }

        #endregion
    }

    [Serializable]
    internal class ClientEndServiceMessage : BaseClientServiceMessage
    {
        #region Constructors

        public ClientEndServiceMessage(string clientID, string sessionID) : base(clientID, sessionID)
        {
        }

        public ClientEndServiceMessage() : base(null, null)
        {

        }

        #endregion
    }

    [Serializable]
    internal class RegisterServiceMessage : BaseClientServiceMessage
    {
        #region Fields

        private string[] registers;

        public string[] Registers
        {
            get { return registers; }
            set { registers = value; }
        }

        private bool force = false;

        public bool Force
        {
            get { return force; }
            set { force = value; }
        }

        #endregion

        #region Constructors

        public RegisterServiceMessage(string clientID, string[] registers) : base(clientID, null)
        {
            this.registers = registers;
        }

        #endregion
    }
    
    internal class ProviderRegister
    {
        private ResourceRequest resourceHandler;

        public ResourceRequest ResourceHandler
        {
            get { return resourceHandler; }
        }

        public ProviderRegister(ResourceRequest handler)
        {
            resourceHandler = handler;
        }
    }

    public delegate ProviderPacket ResourceRequest(ResourceAsyncResult result, int id);

    [DataContract]
    public class ProviderPacket
    {
        private string _data = null;

        [DataMember(Name = "data")]
        internal string _Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public ProviderPacket()
        {
        }

        public static ProviderPacket Null()
        {
            return new ProviderPacket();
        }

        public static ProviderPacket Data(string data)
        {
            ProviderPacket p = new ProviderPacket();
            p._Data = data;
            return p;
        }
    }
}