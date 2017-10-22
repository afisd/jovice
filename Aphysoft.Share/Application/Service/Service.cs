﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;


namespace Aphysoft.Share
{
    public sealed class Service : BaseService
    {
        #region Fields

        private static Database share = Share.Database;

        #endregion

        #region Instancing

        private static Service instance = null;

        private static Service Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Service();

                    ((BaseService)instance).Connected += delegate (Connection c)
                    {
                        Connected?.Invoke(c);
                    };
                    ((BaseService)instance).Disconnected += delegate (Connection c)
                    {
                        Disconnected?.Invoke(c);
                    };
                    ((BaseService)instance).Traced += delegate (string message)
                    {
                        Traced?.Invoke(message);
                    };
                }
                return instance;
            }
        }

        public static new bool IsServer
        {
            get
            {
                BaseService instance = Instance;

                return instance.IsServer;
            }
        }

        public static new bool IsClient
        {
            get
            {
                BaseService instance = Instance;

                return instance.IsClient;
            }
        }

        public static new bool IsConnected
        {
            get
            {
                BaseService instance = Instance;

                return instance.IsConnected;
            }
        }

        internal static new string LastExceptionMessage
        {
            get
            {
                BaseService instance = Instance;

                return instance.LastExceptionMessage;
            }
        }

        public static new void Register(Type messageType, OnReceivedCallback method)
        {
            BaseService instance = Service.Instance;

            instance.Register(messageType, method);
        }

        public static new void Debug(string message)
        {
            BaseService instance = Service.Instance;

            instance.Trace(message);
        }

        public static void Debug(object message)
        {
            Debug(message.ToString());
        }

        public static void Debug(object[] message)
        {
            StringBuilder sb = new StringBuilder();
            bool next = false;
            foreach (object o in message)
            {
                if (next) sb.Append(", ");
                else next = true;
                sb.Append(o.ToString());
            }
            Debug(sb.ToString());
        }

        public static new bool Send(BaseServiceMessage message)
        {
            BaseService instance = Service.Instance;

            return instance.Send(message);
        }

        public static new T Wait<T>(T originalMessage) where T : BaseServiceMessage, new()
        {
            BaseService instance = Service.Instance;

            if (HttpContext.Current != null)
                return instance.Wait<T>(originalMessage, new MessageWaitCallback(ClientConnectedCallback), HttpContext.Current);
            else
                return instance.Wait<T>(originalMessage);
        }

        private static bool ClientConnectedCallback(object obj)
        {
            HttpContext context = (HttpContext)obj;
            HttpResponse response = context.Response;

            if (response.IsClientConnected)
                return false;
            else
                return true;
        }

        #endregion

        #region Constructors

        private Service() : base()
        {
        }

        #endregion

        #region Static

        public static new void FileReceive(int[] files, StartFileReceiveEventHandler handler)
        {
            BaseService instance = Instance;
            instance.FileReceive(files, handler);
        }

        public static void Server()
        {
            Server(ServiceTraceLevels.None);
        }

        public static void Server(ServiceTraceLevels type)
        {
            if (!IsServer && !IsClient)
            {
                if (share.Test(delegate(string message)
                {
                }))
                {
                    if (type < ServiceTraceLevels.None)
                    {
                        Console.WriteLine("ServiceTraceLevels: " + type);
                        Traced += delegate (string message)
                        {
                            Console.WriteLine(message);
                        };
                    }
                    
                    Instance.SetTraceType(type);
                    Instance.Server(IPAddress.Any, 2347);

                    Provider.ServerInit();

                    Register(typeof(SessionClientServiceMessage), SessionClientServiceMessageHandler);
                }
            }
        }

        public static void Client()
        {
            if (!IsServer && !IsClient)
            {
                Client(new IPEndPoint(IPAddress.Loopback, 2347), ServiceTraceLevels.None);
            }
        }

        public static void Client(IPAddress server)
        {
            if (!IsServer && !IsClient)
            {
                Client(new IPEndPoint(server, 23474), ServiceTraceLevels.None);
            }
        }

        public static new void Client(IPEndPoint server)
        {
            if (!IsServer && !IsClient)
            {
                Client(server, ServiceTraceLevels.None);
            }
        }

        public static void Client(IPEndPoint server, ServiceTraceLevels type)
        {
            if (!IsServer && !IsClient)
            {
                if (type < ServiceTraceLevels.None)
                {
                    Console.WriteLine("ServiceTraceLevels: " + type);
                    Traced += delegate (string message)
                    {
                        Console.WriteLine(message);
                    };
                }

                Instance.SetTraceType(type);
                ((BaseService)Instance).Client(server);
            }
        }

        public static void End()
        {
            if (IsServer || IsClient)
            {
                Instance.Disconnect();
            }
        }

        private static void SessionClientServiceMessageHandler(MessageEventArgs e)
        {
            SessionClientServiceMessage m = (SessionClientServiceMessage)e.Message;

            if (IsServer)
            {
                string sessionID = m.SessionID;
                StreamSessionInstance sessionInstance = Provider.GetSessionInstance(sessionID);

                if (sessionInstance == null) m.StreamSubDomainIndex = 0;
                else
                {
                    // we have this session before, maybe this a new window (client),
                    int index = sessionInstance.GetAvailableIndex();
                    m.StreamSubDomainIndex = index % m.StreamSubDomainLength;               
                }

                e.Connection.Reply(m);
            }

        }

        public static new event ConnectedEventHandler Connected;

        public static new event DisconnectedEventHandler Disconnected;

        public static new event TracedEventHandler Traced;

        #endregion
    }



}
