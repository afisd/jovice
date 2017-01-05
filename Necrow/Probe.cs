﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

using System.Text.RegularExpressions;

using Aphysoft.Share;


namespace Center
{
    #region To Database

    internal abstract class ToDatabase
    {
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

    }

    internal abstract class StatusToDatabase : ToDatabase
    {
        private bool status;

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        private bool protocol;

        public bool Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }

        private bool enable;

        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        private bool updateStatus = false;

        public bool UpdateStatus
        {
            get { return updateStatus; }
            set { updateStatus = value; }
        }

        private bool updateProtocol = false;

        public bool UpdateProtocol
        {
            get { return updateProtocol; }
            set { updateProtocol = value; }
        }

        private bool updateEnable = false;

        public bool UpdateEnable
        {
            get { return updateEnable; }
            set { updateEnable = value; }
        }
    }

    internal abstract class ServiceBaseToDatabase : StatusToDatabase
    {
        private string serviceID;

        public string ServiceID
        {
            get { return serviceID; }
            set { serviceID = value; }
        }
    }

    internal abstract class InterfaceToDatabase : ServiceBaseToDatabase
    {
        #region Basic

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private bool updateName = false;

        public bool UpdateName
        {
            get { return updateName; }
            set { updateName = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private bool updateDescription = false;

        public bool UpdateDescription
        {
            get { return updateDescription; }
            set { updateDescription = value; }
        }
        
        private string interfaceType = null;

        public string InterfaceType
        {
            get { return interfaceType; }
            set { interfaceType = value; }
        }

        private bool updateInterfaceType = false;

        public bool UpdateInterfaceType
        {
            get { return updateInterfaceType; }
            set { updateInterfaceType = value; }
        }

        private int dot1q = -1;

        public int Dot1Q
        {
            get { return dot1q; }
            set { dot1q = value; }
        }

        private bool updateDot1Q = false;

        public bool UpdateDot1Q
        {
            get { return updateDot1Q; }
            set { updateDot1Q = value; }
        }

        private DateTime? lastDown = null;

        public DateTime? LastDown
        {
            get { return lastDown; }
            set { lastDown = value; }
        }

        private bool updateLastDown = false;

        public bool UpdateLastDown
        {
            get { return updateLastDown; }
            set { updateLastDown = value; }
        }

        #endregion

        #region QOS

        private int rateInput = -1;

        public int RateInput
        {
            get { return rateInput; }
            set { rateInput = value; }
        }

        private int rateOutput = -1;

        public int RateOutput
        {
            get { return rateOutput; }
            set { rateOutput = value; }
        }

        private bool updateRateInput = false;

        public bool UpdateRateInput
        {
            get { return updateRateInput; }
            set { updateRateInput = value; }
        }

        private bool updateRateOutput = false;

        public bool UpdateRateOutput
        {
            get { return updateRateOutput; }
            set { updateRateOutput = value; }
        }

        #endregion

        #region Topology

        private int aggr = -1;

        public int Aggr
        {
            get { return aggr; }
            set { aggr = value; }
        }

        private bool updateAggr = false;

        public bool UpdateAggr
        {
            get { return updateAggr; }
            set { updateAggr = value; }
        }

        private string parentID = null;

        public string ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }

        private bool updateParentID = false;

        public bool UpdateParentID
        {
            get { return updateParentID; }
            set { updateParentID = value; }
        }

        private string topologyMEInterfaceID = null;

        public string TopologyMEInterfaceID
        {
            get { return topologyMEInterfaceID; }
            set { topologyMEInterfaceID = value; }
        }

        private bool updateTopologyMEInterfaceID = false;

        public bool UpdateTopologyMEInterfaceID
        {
            get { return updateTopologyMEInterfaceID; }
            set { updateTopologyMEInterfaceID = value; }
        }

        private string topologyNeighborInterfaceID = null;

        public string TopologyNeighborInterfaceID
        {
            get { return topologyNeighborInterfaceID; }
            set { topologyNeighborInterfaceID = value; }
        }

        private bool updateTopologyNeighborInterfaceID = false;

        public bool UpdateTopologyNeighborInterfaceID
        {
            get { return updateTopologyNeighborInterfaceID; }
            set { updateTopologyNeighborInterfaceID = value; }
        }

        private bool physicalNeighborChecked = false;

        public bool PhysicalNeighborChecked
        {
            get { return physicalNeighborChecked; }
            set { physicalNeighborChecked = value; }
        }

        private string aggrNeighborParentID = null;

        public string AggrNeighborParentID
        {
            get { return aggrNeighborParentID; }
            set { aggrNeighborParentID = value; }
        }

        private Dictionary<int, Tuple<string, string, string>> childrenNeighbor = null;

        public Dictionary<int, Tuple<string, string, string>> ChildrenNeighbor
        {
            get { return childrenNeighbor; }
            set { childrenNeighbor = value; }
        }

        #endregion

        #region Summary

        private long cirTotalInput = -1;

        public long CirTotalInput
        {
            get { return cirTotalInput; }
            set { cirTotalInput = value; }
        }

        private bool updateCirTotalInput = false;

        public bool UpdateCirTotalInput
        {
            get { return updateCirTotalInput; }
            set { updateCirTotalInput = value; }
        }

        private long cirTotalOutput = -1;

        public long CirTotalOutput
        {
            get { return cirTotalOutput; }
            set { cirTotalOutput = value; }
        }

        private bool updateCirTotalOutput = false;

        public bool UpdateCirTotalOutput
        {
            get { return updateCirTotalOutput; }
            set { updateCirTotalOutput = value; }
        }

        private int cirConfigTotalInput = -1;

        public int CirConfigTotalInput
        {
            get { return cirConfigTotalInput; }
            set { cirConfigTotalInput = value; }
        }

        private bool updateCirConfigTotalInput = false;

        public bool UpdateCirConfigTotalInput
        {
            get { return updateCirConfigTotalInput; }
            set { updateCirConfigTotalInput = value; }
        }

        private int cirConfigTotalOutput = -1;

        public int CirConfigTotalOutput
        {
            get { return cirConfigTotalOutput; }
            set { cirConfigTotalOutput = value; }
        }

        private bool updateCirConfigTotalOutput = false;

        public bool UpdateCirConfigTotalOutput
        {
            get { return updateCirConfigTotalOutput; }
            set { updateCirConfigTotalOutput = value; }
        }

        private int subInterfaceCount = -1;

        public int SubInterfaceCount
        {
            get { return subInterfaceCount; }
            set { subInterfaceCount = value; }
        }

        private bool updateSubInterfaceCount = false;

        public bool UpdateSubInterfaceCount
        {
            get { return updateSubInterfaceCount; }
            set { updateSubInterfaceCount = value; }
        }

        #endregion
    }

    internal class CustomerToDatabase : ToDatabase
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string cid;

        public string CID
        {
            get { return cid; }
            set { cid = value; }
        }
    }

    internal class ServiceToDatabase : ToDatabase
    {
        private string sid;

        public string SID
        {
            get { return sid; }
            set { sid = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string subType;

        public string SubType
        {
            get { return subType; }
            set { subType = value; }
        }

        private string rawDesc;

        public string RawDesc
        {
            get { return rawDesc; }
            set { rawDesc = value; }
        }

        private string customerID;

        public string CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
    }

    #endregion

    internal static class ProbeExtensions
    {
        public static string NullableInfo(this object value, string format)
        {
            string strtype = value.ToString();
            return strtype == "-1" ? null : (format != null && strtype != null) ? string.Format(format, strtype) : strtype;
        }

        public static string NullableInfo(this object value)
        {
            return value.NullableInfo(null);
        }
    }

    internal sealed partial class Probe : SshConnection
    {
        #region Enums

        private enum UpdateTypes
        {
            NecrowVersion,
            TimeStamp,
            TimeOffset,
            Remark,
            IP,
            Name,
            Active,
            Terminal,
            ConnectType,
            Model,
            Version,
            SubVersion,
            VersionTime,
            LastConfiguration
        }

        private enum EventActions { Add, Remove, Delete, Update }
        private enum EventElements
        {
            ALUCustomer, QOS, SDP, Circuit, Interface, Peer, CircuitReference,
            VRFReference, VRF, VRFRouteTarget, InterfaceIP, Service, Customer, NodeReference, InterfaceReference,
            NodeAlias, NodeSummary, POPInterfaceReference, Routing, NeighborInterface,
            PrefixList, PrefixEntry
        }

        #endregion

        #region Fields

        private bool stopping = false;

        private ProbeProperties properties;

        internal ProbeProperties Properties { get { return properties; } }

        private Thread mainLoop = null;
        private Thread idleThread = null;

        private Queue<string> outputs = new Queue<string>();
        private Dictionary<string, object> updates;
        private Dictionary<string, string> summaries;
        private string defaultOutputIdentifier = null;
        private string outputIdentifier = null;

        private Database j;

        private string nodeID;
        private string nodeName;
        private string nodeManufacture;
        private string nodeModel;
        private string nodeVersion;
        private string nodeSubVersion;
        private string nodeIP;
        private string nodeTerminal;
        private string nodeConnectType;
        private string nodeAreaID;
        private string nodeType;
        private int nodeNVER;
        private TimeSpan nodeTimeOffset;
        private int probeProgressID = -1;
        private DateTime nodeProbeStartTime = DateTime.MinValue;
        private bool nodeConnected = false;

        private bool noMore = false;

        private readonly string alu = "ALCATEL-LUCENT";
        private readonly string hwe = "HUAWEI";
        private readonly string cso = "CISCO";
        private readonly string jun = "JUNIPER";

        private readonly string xr = "XR";

        private char[] newline = new char[] { (char)13, (char)10 };

        private Dictionary<string, Row> reservedInterfaces;
        private Dictionary<string, Row> popInterfaces;

        public new bool IsConnected
        {
            get
            {
                return mainLoop != null;
            }
        }

        private bool started = false;

        public bool IsStarted
        {
            get { return started; }
        }

        #endregion

        #region Constructors

        public Probe(ProbeProperties access)
        {
            this.properties = access;

            j = Jovice.Database;

            Connected += OnConnected;
            Disconnected += OnDisconnected;
            ConnectionFailed += OnConnectionFailed;
            Received += OnReceived;
        }

        #endregion

        #region Database

        public Batch Batch()
        {
            return j.Batch();
        }

        public Insert Insert(string table)
        {
            return j.Insert(table);
        }

        public Update Update(string table)
        {
            return j.Update(table);
        }

        public Dictionary<string, Row> QueryDictionary(string sql, string key, params object[] args)
        {
            return j.QueryDictionary(sql, key, args);
        }

        public Dictionary<string, Row> QueryDictionary(string sql, string key, QueryDictionaryDuplicateCallback duplicate, params object[] args)
        {
            return j.QueryDictionary(sql, key, duplicate, args);
        }

        public Dictionary<string, Row> QueryDictionary(string sql, QueryDictionaryKeyCallback callback, params object[] args)
        {
            return j.QueryDictionary(sql, callback, args);
        }

        public Dictionary<string, Row> QueryDictionary(string sql, QueryDictionaryKeyCallback callback, QueryDictionaryDuplicateCallback duplicate, params object[] args)
        {
            return j.QueryDictionary(sql, callback, duplicate, args);
        }

        public List<string> QueryList(string sql, string key, params object[] args)
        {
            return j.QueryList(sql, key, args);
        }

        public string Format(string sql, params object[] args)
        {
            return j.Format(sql, args);
        }

        public Result Query(string sql, params object[] args)
        {
            return j.Query(sql, args);
        }

        public Column Scalar(string sql, params object[] args)
        {
            return j.Scalar(sql, args);
        }

        public Result Execute(string sql, params object[] args)
        {
            return j.Execute(sql, args);
        }

        public Result ExecuteIdentity(string sql, params object[] args)
        {
            return j.ExecuteIdentity(sql, args);
        }

        #endregion

        #region Static

        public static Probe Create(ProbeProperties access, string identifier)
        {
            Probe probe = new Probe(access);
            probe.defaultOutputIdentifier = identifier;

            return probe;
        }

        #endregion

        private void Event(string message)
        {
            string oi;

            if (outputIdentifier == null)
                oi = defaultOutputIdentifier;
            else
                oi = outputIdentifier;

            Necrow.Event(message, oi);
        }

        private void Event(Result result, EventActions action, EventElements element, bool reportzero)
        {
            int row = result.AffectedRows;
            if (row > 0 || (row >= 0 && reportzero))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(row);
                sb.Append(' ');
                if (row == 1)
                {
                    switch (element)
                    {
                        case EventElements.ALUCustomer: sb.Append("alu-customer"); break;
                        case EventElements.Circuit: sb.Append("circuit"); break;
                        case EventElements.Interface: sb.Append("interface"); break;
                        case EventElements.QOS: sb.Append("QOS"); break;
                        case EventElements.SDP: sb.Append("SDP"); break;
                        case EventElements.Peer: sb.Append("peer"); break;
                        case EventElements.CircuitReference: sb.Append("circuit reference"); break;
                        case EventElements.InterfaceIP: sb.Append("interface IP"); break;
                        case EventElements.VRF: sb.Append("VRF"); break;
                        case EventElements.VRFReference: sb.Append("VRF reference"); break;
                        case EventElements.VRFRouteTarget: sb.Append("VRF route target"); break;
                        case EventElements.Customer: sb.Append("customer"); break;
                        case EventElements.Service: sb.Append("service"); break;
                        case EventElements.NodeReference: sb.Append("node reference"); break;
                        case EventElements.InterfaceReference: sb.Append("interface reference"); break;
                        case EventElements.NodeAlias: sb.Append("node alias"); break;
                        case EventElements.NodeSummary: sb.Append("node summary"); break;
                        case EventElements.POPInterfaceReference: sb.Append("POP interface reference"); break;
                        case EventElements.Routing: sb.Append("routing"); break;
                        case EventElements.NeighborInterface: sb.Append("neighbor interface"); break;
                        case EventElements.PrefixList: sb.Append("prefix-list"); break;
                        case EventElements.PrefixEntry: sb.Append("prefix-list entry"); break;
                    }
                }
                else
                {
                    switch (element)
                    {
                        case EventElements.ALUCustomer: sb.Append("alu-customers"); break;
                        case EventElements.Circuit: sb.Append("circuits"); break;
                        case EventElements.Interface: sb.Append("interfaces"); break;
                        case EventElements.QOS: sb.Append("QOSes"); break;
                        case EventElements.SDP: sb.Append("SDPs"); break;
                        case EventElements.Peer: sb.Append("peers"); break;
                        case EventElements.CircuitReference: sb.Append("circuit references"); break;
                        case EventElements.InterfaceIP: sb.Append("interface IPs"); break;
                        case EventElements.VRF: sb.Append("VRFs"); break;
                        case EventElements.VRFReference: sb.Append("VRF references"); break;
                        case EventElements.VRFRouteTarget: sb.Append("VRF route targets"); break;
                        case EventElements.Customer: sb.Append("customers"); break;
                        case EventElements.Service: sb.Append("services"); break;
                        case EventElements.NodeReference: sb.Append("node references"); break;
                        case EventElements.InterfaceReference: sb.Append("interface references"); break;
                        case EventElements.NodeAlias: sb.Append("node aliases"); break;
                        case EventElements.NodeSummary: sb.Append("node summaries"); break;
                        case EventElements.POPInterfaceReference: sb.Append("POP interface references"); break;
                        case EventElements.Routing: sb.Append("routings"); break;
                        case EventElements.NeighborInterface: sb.Append("neighbor interfaces"); break;
                        case EventElements.PrefixList: sb.Append("prefix-lists"); break;
                        case EventElements.PrefixEntry: sb.Append("prefix-list entries"); break;
                    }
                }
                if (row > 1) sb.Append(" have been ");
                else sb.Append(" has been ");
                if (action == EventActions.Add) sb.Append("added (");
                else if (action == EventActions.Delete) sb.Append("deleted (");
                else if (action == EventActions.Remove) sb.Append("removed (");
                else if (action == EventActions.Update) sb.Append("updated (");
                else sb.Append("affected (");
                sb.Append(string.Format("{0:0.###}", result.ExecutionTime.TotalSeconds));
                sb.Append("s)");

                Event(sb.ToString());
            }
        }

        internal void Start()
        {
            if (!IsStarted)
            {
                started = true;

                Event("Connecting... (" + properties.SSHUser + "@" + properties.SSHServerAddress + " [" + properties.TacacUser + "])");
                new Thread(new ThreadStart(delegate ()
                {
                    Start(properties.SSHServerAddress, properties.SSHUser, properties.SSHPassword);
                })).Start();
            }           
        }

        internal void QueueStop()
        {
            if (stopping == false)
            {
                Event("Stop initiated");
                stopping = true;
            }
        }

        internal void BeginRestart()
        {
            stopping = false;

            if (IsConnected)
            {
                Event("Restart initiated");
                Stop();
            }
        }

        private void Failure()
        {
            outputIdentifier = null;
            Event("Connection failure has occured");
        }

        private void OnConnected(object sender)
        {
            Event("Connected!");

            if (mainLoop != null)
            {
                mainLoop.Abort();
                mainLoop = null;
            }

            mainLoop = new Thread(new ThreadStart(MainLoop));
            mainLoop.Start();
        }
        
        private void OnDisconnected(object sender)
        {
            outputIdentifier = null;
            Event("Disconnected");

            if (mainLoop != null)
            {
                mainLoop.Abort();
                mainLoop = null;
                Event("Main thread loop aborted");
            }
            if (idleThread != null)
            {
                idleThread.Abort();
                mainLoop = null;
                Event("Idle thread aborted");
            }

            int c = j.Cancel();
            if (c > 0) Event("Canceled " + c + " database transactions");

            started = false;

            if (stopping)
            {
                stopping = false;
                Event("Probe session has ended");
            }
        }
        
        private void OnConnectionFailed(object sender, Exception exception)
        {
            string message = exception.Message;

            if (message.IndexOf("Auth fail") > -1) Event("Connection failed: Authentication failed");
            else if (message.IndexOf("unreachable") > -1) Event("Connection failed: Server unreachable");
            else Event("Connection failed");
                      

            if (!Necrow.InTime(properties)) stopping = true;

            if (stopping)
            {
                stopping = false;
                Event("Connection attempt aborted");
                started = false;
            }
            else
            {
                Event("Reconnecting in 20 seconds...");
                Thread.Sleep(20000);
                started = false;
            }
        }
        
        private void OnReceived(object sender, string output)
        {
            if (output != null && output != "")
            {
                lock (outputs)
                {
                    outputs.Enqueue(output);
                }
            }
        }

        private void MainLoop()
        {
            Culture.Default();
            
            while (true)
            {
                int xpID = -1;
                Row node = null;
                bool prioritizeProcess = false;

                string prioritizeNode = Necrow.NextPrioritize();

                if (prioritizeNode != null)
                {
                    if (prioritizeNode.EndsWith("*"))
                    {
                        prioritizeNode = prioritizeNode.TrimEnd(new char[] { '*' });
                        prioritizeProcess = true;
                    }

                    Event("Prioritizing Probe: " + prioritizeNode);
                    Result rnode = Query("select * from Node where upper(NO_Name) = {0}", prioritizeNode);

                    if (rnode.Count == 1)
                    {
                        node = rnode[0];
                    }
                    else
                    {
                        Event("Failed, not exists in the database.");
                        continue;
                    }
                }
                else
                {
                    Tuple<int, string> noded = Necrow.NextNode();

                    xpID = noded.Item1;
                    Result rnode = Query("select * from Node where NO_ID = {0}", noded.Item2);
                    node = rnode[0];
                }

                if (node != null)
                {
                    bool continueProcess = false;

#if !DEBUG
                    try
                    {
#endif

                    Enter(node, xpID, out continueProcess, prioritizeProcess);
#if !DEBUG
                    }
                    catch (Exception ex)
                    {
                        string info = null;
                        
                        if (ex.Message.IndexOf("was being aborted") == -1)
                        {
                            info = ex.Message;
                            Necrow.Log(nodeName, ex.Message, ex.StackTrace);
                            Update(UpdateTypes.Remark, "PROBEFAILED");
                        }

                        continueProcess = false;

                        Save();

                        if (nodeConnected)
                        {
                            if (info != null)
                                Event("Caught error: " + info + ", exiting current node...");
                            Exit();
                        }
                        else if (info != null)
                            Event("Caught error: " + info);
                    }
#endif
                    if (continueProcess)
                    {
                        idleThread = new Thread(new ThreadStart(delegate ()
                        {
                            while (true)
                            {
                                Thread.Sleep(30000);
                                if (nodeManufacture == alu || nodeManufacture == cso || nodeManufacture == hwe) SendCharacter((char)27);

                            }
                        }));
                        idleThread.Start();

                        Batch batch = Batch();
                        Result result;

                        batch.Begin();

                        // RESERVED INTERFACES
                        reservedInterfaces = QueryDictionary("select * from ReservedInterface where RI_NO = {0}", "RI_Name", delegate (Row row)
                        {
                            // delete duplicated RI_Name per RI_NO
                            batch.Execute("delete from ReservedInterface where RI_ID = {0}", row["RI_ID"].ToString());
                        }, nodeID);

                        // POP
                        if (nodeType == "P")
                        {
                            popInterfaces = new Dictionary<string, Row>();
                            result = Query("select * from POP where UPPER(OO_NO_Name) = {0}", nodeName);
                            foreach (Row row in result)
                            {
                                string storedID = row["OO_ID"].ToString();
                                string interfaceName = row["OO_PI_Name"].ToString();
                                bool ooPINULL = row["OO_PI"].IsNull;

                                if (!popInterfaces.ContainsKey(interfaceName))
                                {
                                    popInterfaces.Add(interfaceName, row);

                                    if (row["OO_NO_Name"].ToString() != nodeName)
                                    {
                                        // fix incorrect name in POP
                                        batch.Execute("update POP set OO_NO_Name = {0} where OO_ID = {1}", nodeName, storedID);
                                    }
                                }
                                else
                                {
                                    // delete duplicated OO_PI_Name per OO_NO_Name
                                    batch.Execute("delete from POP where OO_ID = {0}", storedID);
                                }
                            }
                        }
                        else popInterfaces = null;

                        batch.Commit();

#if !DEBUG
                        try
                        {
#endif
                        if (nodeType == "P")
                        {
                            PEProcess();
                        }
                        else if (nodeType == "M")
                        {
                            MEProcess();
                        }

                        Necrow.AcknowledgeNodeVersion(nodeManufacture, nodeVersion, nodeSubVersion);
#if !DEBUG
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.IndexOf("was being aborted") == -1)
                            {
                                Necrow.Log(nodeName, ex.Message, ex.StackTrace);
                                Event("Caught error: " + ex.Message + ", exiting current node...");
                                Update(UpdateTypes.Remark, "PROBEFAILED");
                            }
                        }
#endif

                        Update(UpdateTypes.Active, 1);
                        SaveExit();

                        if (idleThread != null)
                        {
                            idleThread.Abort();
                            idleThread = null;
                        }
                    }

                    if (!Necrow.InTime(properties)) stopping = true;

                    if (stopping)
                    {
                        Stop();
                        break;
                    }
                    else
                    {
                        // delay after probing finished
                        Event("Next node in 5 seconds...");
                        Thread.Sleep(5000);
                    }
                }
            }
        }

        private static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = RandomHelper.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void SendLine(string command)
        {
            SendLine(command, false);
        }

        private void SendLine(string command, bool saveOnFailure)
        {
            if (IsConnected)
            {
                Thread.Sleep(250);
                bool res = WriteLine(command);
                if (res == false)
                {
                    if (saveOnFailure) Save();
                    Failure();
                }
            }
        }

        private void Send(string line)
        {
            if (IsConnected)
            {
                Thread.Sleep(250);
                bool res = Write(line);
                if (res == false) Failure();
            }
        }

        private void SendCharacter(char character)
        {
            Send(character.ToString());
        }

        private void SendSpace()
        {
            SendCharacter((char)32);
        }

        private void SendControlRightBracket()
        {
            SendCharacter((char)29);
        }

        private void SendControlC()
        {
            SendCharacter((char)3);
        }

        private void SendControlZ()
        {
            SendCharacter((char)26);
        }

        private void WaitUntilMCETerminalReady(string waitMessage)
        {
            int loop = 0;

            while (true)
            {
                // check output, break when terminal is ready
                int wait = 0;
                bool continueWait = true;
                while (wait < 100)
                {
                    while (outputs.Count > 0) lock (outputs) outputs.Dequeue();
                    if (LastOutput != null && LastOutput.TrimEnd().EndsWith(properties.SSHTerminal))
                    {
                        continueWait = false;
                        break;
                    }
                    //Event(LastOutput);
                    Thread.Sleep(100);
                    wait++;
                }
                if (continueWait == false) break;

                // else continue wait...
                loop++;
                if (loop == 3) Failure(); // loop 3 times, its a failure

                // print message where we are waiting (or why)
                Event(waitMessage + "... (" + loop + ")");

                //Event("Last Reading Output: ");
                //int lp = LastOutput.Length - 200;
                //if (lp < 0) lp = 0;
                //string lop = LastOutput.Substring(lp);
                //lop = lop.Replace("\r", "<CR>");
                //lop = lop.Replace("\n", "<NL>");
                //Event(lop);

                // try sending exit characters
                SendCharacter((char)13);
                SendControlRightBracket();
                SendControlC();

                Thread.Sleep(1000);
            }

        }

        private List<string> MCESendLine(string command, out bool timeout)
        {
            timeout = false;

            SendLine(command);
            SendLine("echo end\\ request");

            StringBuilder lineBuilder = new StringBuilder();
            List<string> lines = new List<string>();

            StringBuilder lastOutputSB = new StringBuilder();

            int wait = 0;
            while (true)
            {
                if (outputs.Count > 0)
                {
                    lock (outputs)
                    {
                        wait = 0;
                        string output = outputs.Dequeue();
                        if (output == null) continue;

                        lastOutputSB.Append(output);

                        for (int i = 0; i < output.Length; i++)
                        {
                            byte b = (byte)output[i];
                            if (b == 10)
                            {
                                string line = lineBuilder.ToString().Trim();
                                lines.Add(line);
                                lineBuilder.Clear();
                            }
                            else if (b == 9) lineBuilder.Append(' ');
                            else if (b >= 32) lineBuilder.Append((char)b);
                        }
                        if (lastOutputSB.ToString().IndexOf("end request") > -1) break;
                    }
                }
                else
                {
                    wait++;
                    Thread.Sleep(100);
                    if (wait == 200)
                    {
                        SendControlC();
                        Thread.Sleep(1000);
                        timeout = true;
                        break;
                    }
                }
            }
            if (lineBuilder.Length > 0) lines.Add(lineBuilder.ToString().Trim());

            if (timeout == true) return null;
            else return lines;
        }

        private int MCEExpect(params string[] args)
        {
            if (args.Length == 0) return -1;

            int wait = 0;
            int expectReturn = -1;

            Queue<string> lastOutputs = new Queue<string>();

            while (true)
            {
                if (outputs.Count > 0)
                {
                    lock (outputs)
                    {
                        wait = 0;

                        string output = outputs.Dequeue();
                        if (output != null)
                        {
                            lastOutputs.Enqueue(output);
                            if (lastOutputs.Count > 100) lastOutputs.Dequeue();

                            StringBuilder lastOutputSB = new StringBuilder();
                            foreach (string s in lastOutputs)
                                lastOutputSB.Append(s);

                            string lastOutput = lastOutputSB.ToString();

                            bool found = false;
                            for (int i = 0; i < args.Length; i++)
                            {
                                string expect = args[i];
                                if (lastOutput.IndexOf(expect) > -1)
                                {
                                    expectReturn = i;
                                    found = true;
                                    break;
                                }
                            }
                            if (found) break;
                        }
                    }
                }
                else
                {
                    Thread.Sleep(50);
                    wait += 1;
                    if (wait == 400) break;
                }

            }

            return expectReturn;
        }

        private string MCECheckNodeIP(string hostname)
        {
            return MCECheckNodeIP(hostname, false);
        }

        private string MCECheckNodeIP(string hostname, bool reverse)
        {
            string cpeip = null;

            bool timeout;
            List<string> lines = MCESendLine("cat /etc/hosts | grep -i " + hostname, out timeout);
            if (timeout) Failure();

            Dictionary<string, string> greppair = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                if (line.Length > 0 && char.IsDigit(line[0]))
                {
                    string[] linex = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (linex.Length == 2)
                    {
                        IPAddress address;
                        if (IPAddress.TryParse(linex[0], out address))
                        {
                            if (!reverse && !greppair.ContainsKey(linex[1].ToLower()))
                            {
                                greppair.Add(linex[1].ToLower(), linex[0]);
                            }
                            else if (!greppair.ContainsKey(linex[0]))
                            {
                                greppair.Add(linex[0], linex[1]);
                            }
                        }
                    }
                }
            }
            if (greppair.ContainsKey(hostname.ToLower())) cpeip = greppair[hostname.ToLower()].ToUpper();
            return cpeip;
        }

        private void Update(UpdateTypes type, object value)
        {
            string key = null;

            switch (type)
            {
                case UpdateTypes.NecrowVersion: key = "NO_NVER"; break;
                case UpdateTypes.TimeStamp: key = "NO_TimeStamp"; break;
                case UpdateTypes.TimeOffset: key = "NO_TimeOffset"; break;
                case UpdateTypes.Remark: key = "NO_Remark"; break;
                case UpdateTypes.IP: key = "NO_IP"; break;
                case UpdateTypes.Name: key = "NO_Name"; break;
                case UpdateTypes.Active: key = "NO_Active"; break;
                case UpdateTypes.Terminal: key = "NO_Terminal"; break;
                case UpdateTypes.ConnectType: key = "NO_ConnectType"; break;
                case UpdateTypes.Model: key = "NO_Model"; break;
                case UpdateTypes.Version: key = "NO_Version"; break;
                case UpdateTypes.SubVersion: key = "NO_SubVersion"; break;
                case UpdateTypes.VersionTime: key = "NO_VersionTime"; break;
                case UpdateTypes.LastConfiguration: key = "NO_LastConfiguration"; break;
            }

            if (key != null)
            {
                if (updates.ContainsKey(key)) updates[key] = value;
                else updates.Add(key, value);
            }
        }

        private void Status(string status)
        {
            if (probeProgressID != -1) Execute("update ProbeProgress set XP_Status = {0} where XP_ID = {1}", status, probeProgressID);
        }

        private void Summary(string key, int value)
        {
            Summary(key, value.ToString());
        }

        private void Summary(string key, float value)
        {
            Summary(key, value.ToString());
        }

        private void Summary(string key, bool value)
        {
            Summary(key, value ? 1 : 0);
        }

        private void Summary(string key, string value)
        {
            if (key != null && value != null)
            {
                if (summaries.ContainsKey(key)) summaries[key] = value;
                else summaries.Add(key, value);
            }
        }

        private void Exit()
        {
            Event("Exiting...");
            if (outputIdentifier != null)
            {
                Exit(nodeManufacture);
            }
            outputIdentifier = null;
            Event("Exit!");

            nodeConnected = false;
        }

        private void Exit(string manufacture)
        {
            if (idleThread != null)
            {
                idleThread.Abort();
                idleThread = null;
            }

            Thread.Sleep(200);
            SendLine("");
            Thread.Sleep(200);
            if (manufacture == alu) SendLine("logout");
            else if (manufacture == hwe) SendLine("quit");
            else if (manufacture == cso) SendLine("exit");
            else if (manufacture == jun) SendLine("exit");

            WaitUntilMCETerminalReady("MCE Waiting on Exit");
        }

        private void SaveExit()
        {
            Save();
            Exit();
        }

        private bool Request(string command, out string[] lines)
        {
            Event("Request [" + command + "]...");

            Thread.Sleep(100);

            bool requestLoop = true;
            bool timeout = false;
            lines = null;

            while (requestLoop)
            {
                outputs.Clear();
                SendLine(command);

                Stopwatch stopwatch = new Stopwatch();
                StringBuilder lineBuilder = new StringBuilder();
                StringBuilder lastOutputSB = new StringBuilder();
                List<string> listlines = new List<string>();

                stopwatch.Start();

                int wait = 0;
                bool ending = false;

                while (true)
                {
                    if (outputs.Count > 0)
                    {
                        lock (outputs)
                        {
                            wait = 0;
                            string output = outputs.Dequeue();
                            lastOutputSB.Append(output);

                            for (int i = 0; i < output.Length; i++)
                            {
                                byte b = (byte)output[i];
                                if (b == 10)
                                {
                                    string line = lineBuilder.ToString();

                                    if (nodeManufacture == hwe && nodeVersion == "5.160" && line.Length > 80)
                                    {
                                        int looptimes = (int)Math.Ceiling((float)line.Length / 80);

                                        for (int loop = 0; loop < looptimes; loop++)
                                        {
                                            int sisa = 80;
                                            if (loop == looptimes - 1) sisa = line.Length - (loop * 80);
                                            string curline = line.Substring(loop * 80, sisa);
                                            listlines.Add(curline);
                                        }
                                    }
                                    else
                                        listlines.Add(line);

                                    lineBuilder.Clear();
                                }
                                else if (b >= 32) lineBuilder.Append((char)b);
                            }

                            string losb = lastOutputSB.ToString();

                            if (noMore == false)
                            {
                                if (nodeManufacture == alu)
                                {
                                    string aluMORE = "Press any key to continue (Q to quit)";
                                    if (losb.Contains(aluMORE))
                                    {
                                        List<string> newlines = new List<string>();

                                        foreach (string line in listlines)
                                        {
                                            string newline;
                                            if (line.IndexOf(aluMORE) > -1)
                                                newline = line.Replace(aluMORE + "                                      ", "");
                                            else newline = line;

                                            newlines.Add(newline);
                                        }

                                        listlines = newlines;

                                        SendSpace();
                                    }
                                }
                            }

                            if (losb.TrimEnd().EndsWith(nodeTerminal.Trim())) ending = true;
                        }
                    }
                    else
                    {
                        if (ending) break;

                        wait++;

                        if (wait % 200 == 0 && wait < 1600)
                        {
                            Event("Waiting...");
                            SendLine("");
                        }

                        Thread.Sleep(100);
                        if (wait == 1600)
                        {
                            timeout = true;
                            Event("Reading timeout, cancel the reading...");
                        }
                        if (wait >= 1600 && wait % 50 == 0)
                        {
                            SendControlC();
                        }
                        if (wait == 2000)
                        {
                            Failure();
                        }
                    }
                }
                if (lineBuilder.Length > 0) listlines.Add(lineBuilder.ToString().Trim());
                stopwatch.Stop();

                if (!timeout)
                {
                    lines = listlines.ToArray();

                    bool improperCommand = false;

                    if (lines.Length < 10)
                    {
                        improperCommand = true;
                        foreach (string line in lines)
                        {
                            if (line.Contains(command.Length <= 20 ? command : command.Substring(0, 20)))
                            {
                                improperCommand = false;
                                break;
                            }
                        }
                    }

                    if (improperCommand)
                    {
                        Event("Improper command, send request again...");
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Event("Request completed (" + lines.Length + " lines in " + string.Format("{0:0.###}", stopwatch.Elapsed.TotalSeconds) + "s)");
                        lines = listlines.ToArray();
                        requestLoop = false;
                    }
                }
                else requestLoop = false;
            }

            if (!timeout) return false;
            else
            {
                SaveExit();
                return true;
            }
        }

        private bool ConnectByTelnet(string host, string manufacture)
        {
            int expect = -1;
            bool connectSuccess = false;

            string user = properties.TacacUser;
            string pass = properties.TacacPassword;

            Event("Connecting with Telnet... (" + user + "@" + host + ")");
            SendLine("telnet " + host);

            if (manufacture == alu)
            {
                #region alu
                expect = MCEExpect("ogin:");
                if (expect == 0)
                {
                    Event("Authenticating: User");
                    SendLine(user);

                    expect = MCEExpect("assword:");
                    if (expect == 0)
                    {
                        Event("Authenticating: Password");
                        SendLine(pass);

                        expect = MCEExpect("#", "ogin:", "closed by foreign");
                        if (expect == 0) connectSuccess = true;
                        else SendControlZ();
                    }
                    else
                    {
                        Event("Cannot find password console prefix");
                        SendControlZ();
                    }
                }
                else
                {
                    Event("Cannot find login console prefix");
                    SendControlC();
                }
                #endregion
            }
            else if (manufacture == hwe)
            {
                #region hwe
                expect = MCEExpect("sername:", "closed by foreign");
                if (expect == 0)
                {
                    Event("Authenticating: User");
                    SendLine(user);

                    expect = MCEExpect("assword:");
                    if (expect == 0)
                    {
                        Event("Authenticating: Password");
                        SendLine(pass);

                        expect = MCEExpect(">", "sername:", "Tacacs server reject");
                        if (expect == 0) connectSuccess = true;
                        else
                        {
                            SendControlRightBracket();
                            SendControlC();
                        }
                    }
                    else
                    {
                        Event("Cannot find password console prefix");
                        SendControlRightBracket();
                        SendControlC();
                    }
                }
                else
                {
                    Event("Cannot find username console prefix");
                    SendControlC();
                }
                #endregion
            }
            else if (manufacture == cso)
            {
                #region cso
                expect = MCEExpect("sername:");
                if (expect == 0)
                {
                    Event("Authenticating: User");
                    SendLine(user);

                    expect = MCEExpect("assword:");
                    if (expect == 0)
                    {
                        Event("Authenticating: Password");
                        SendLine(pass);

                        expect = MCEExpect("#", "sername:", "closed by foreign", "cation failed");
                        if (expect == 0) connectSuccess = true;
                        else
                        {
                            SendControlRightBracket();
                            SendControlC();
                        }
                    }
                    else
                    {
                        Event("Cannot find password console prefix");
                        SendControlRightBracket();
                        SendControlC();
                    }
                }
                else
                {
                    Event("Cannot find username console prefix");
                    SendControlC();
                }
                #endregion
            }
            else SendControlC();

            return connectSuccess;
        }

        private bool ConnectBySSH(string host, string manufacture)
        {
            int expect = -1;
            bool connectSuccess = false;

            string user = properties.TacacUser;
            string pass = properties.TacacPassword;

            Event("Connecting with SSH... (" + user + "@" + host + ")");
            SendLine("ssh -o StrictHostKeyChecking=no " + user + "@" + host);

            if (manufacture == hwe)
            {
                #region hwe

                expect = MCEExpect("assword:", "Connection refused");
                if (expect == 0)
                {
                    Event("Authenticating: Password");
                    SendLine(pass);

                    expect = MCEExpect(">", "assword:");
                    if (expect == 0) connectSuccess = true;
                    else SendControlC();
                }
                else SendControlC();

                #endregion
            }
            else if (manufacture == cso)
            {
                #region cso

                expect = MCEExpect("assword:", "Connection refused");
                if (expect == 0)
                {
                    Event("Authenticating: Password");
                    SendLine(pass);

                    expect = MCEExpect("#", "assword:");
                    if (expect == 0) connectSuccess = true;
                    else SendControlC();
                }
                else SendControlC();

                #endregion
            }
            else if (manufacture == jun)
            {
                #region jun

                expect = MCEExpect("password:");
                if (expect == 0)
                {
                    Event("Authenticating: Password");
                    SendLine(pass);

                    expect = MCEExpect(">", "assword:");
                    if (expect == 0) connectSuccess = true;
                    else SendControlC();
                }
                else SendControlC();

                #endregion
            }
            else SendControlC();

            return connectSuccess;
        }

        private void Save()
        {
            Insert insert;
            Update update;
            Result result;
            Batch batch = Batch();
            
            update = Update("Node");
            foreach (KeyValuePair<string, object> pair in updates) update.Set(pair.Key, pair.Value);
            update.Where("NO_ID", nodeID);
            update.Execute();
            
            // end node
            result = ExecuteIdentity("insert into ProbeHistory(XH_NO, XH_StartTime, XH_EndTime) values({0}, {1}, {2})", nodeID, nodeProbeStartTime, DateTime.UtcNow);
            long probeHistoryID = result.Identity;
            
            // nodesummary
            result = Query("select * from NodeSummary where NS_NO = {0}", nodeID);
            Dictionary<string, Tuple<string, string>> dbsummaries = new Dictionary<string, Tuple<string, string>>();

            batch.Begin();
            foreach (Row row in result)
            {
                string key = row["NS_Key"].ToString();
                string id = row["NS_ID"].ToString();
                string value = row["NS_Value"].ToString();

                if (dbsummaries.ContainsKey(key)) batch.Execute("delete from NodeSummary where NS_ID = {0}", id); // Duplicated summary key, remove this
                else dbsummaries.Add(key, new Tuple<string, string>(id, value));
            }
            batch.Commit();

            // old nodesummaryarchive (for migration purpose build 21)
            List<string> availablearchives = QueryList("select NS_Key from NodeSummaryArchive, NodeSummary where NSX_NS = NS_ID and NS_NO = {0}", "NS_Key", nodeID);

            batch.Begin();
            foreach (KeyValuePair<string, string> pair in summaries)
            {
                if (pair.Value == null) continue; // were not accepting null summary

                Tuple<string, string> db = null;
                if (dbsummaries.ContainsKey(pair.Key)) db = dbsummaries[pair.Key];

                if (db == null)
                {
                    string id = Database.ID();

                    insert = Insert("NodeSummary");
                    insert.Value("NS_ID", id);
                    insert.Value("NS_NO", nodeID);
                    insert.Value("NS_Key", pair.Key);
                    insert.Value("NS_Value", pair.Value);
                    batch.Execute(insert);

                    insert = Insert("NodeSummaryArchive");
                    insert.Value("NSX_XH", probeHistoryID);
                    insert.Value("NSX_NS", id);
                    insert.Value("NSX_Value", pair.Value);
                    batch.Execute(insert);

                    Event("Summary " + pair.Key + " NEW: " + pair.Value);
                }
                else
                {
                    string id = db.Item1;
                    string value = db.Item2;

                    if (pair.Value != value)
                    {
                        // summary has changed
                        // insert archive
                        insert = Insert("NodeSummaryArchive");
                        insert.Value("NSX_XH", probeHistoryID);
                        insert.Value("NSX_NS", id);
                        insert.Value("NSX_Value", pair.Value);
                        batch.Execute(insert);
                        // update nodesummary
                        update = Update("NodeSummary");
                        update.Set("NS_Value", pair.Value);
                        update.Where("NS_ID", id);
                        batch.Execute(update);

                        Event("Summary " + pair.Key + " CHANGED: " + value + " -> " + pair.Value);
                    }
                    else
                    {
                        // no change
                        // check if we dont have archive before, make one
                        if (availablearchives.IndexOf(pair.Key) == -1)
                        {
                            insert = Insert("NodeSummaryArchive");
                            insert.Value("NSX_XH", probeHistoryID); // current probehistory
                            insert.Value("NSX_NS", id);
                            insert.Value("NSX_Value", value); // current value
                            batch.Execute(insert);
                        }
                    }
                }
            }
            batch.Commit();

            if (probeProgressID != -1)
            {
                Execute("delete from ProbeProgress where XP_ID = {0}", probeProgressID);
                probeProgressID = -1;
            }
        }

        private void Enter(Row row, int probeProgressID, out bool continueProcess, bool prioritizeProcess)
        {
            string[] lines = null;

            continueProcess = false;

            WaitUntilMCETerminalReady("MCE Waiting I");

            updates = new Dictionary<string, object>();
            summaries = new Dictionary<string, string>();

            nodeID = row["NO_ID"].ToString();
            nodeName = row["NO_Name"].ToString();
            nodeManufacture = row["NO_Manufacture"].ToString();
            nodeModel = row["NO_Model"].ToString();
            nodeVersion = row["NO_Version"].ToString();
            nodeSubVersion = row["NO_SubVersion"].ToString();
            nodeIP = row["NO_IP"].ToString();
            nodeTerminal = row["NO_Terminal"].ToString();
            nodeConnectType = row["NO_ConnectType"].ToString();
            nodeAreaID = row["NO_AR"].ToString();
            nodeType = row["NO_Type"].ToString();
            nodeNVER = row["NO_NVER"].ToInt(0);
            this.probeProgressID = probeProgressID;

            string previousRemark = row["NO_Remark"].ToString();

            nodeProbeStartTime = DateTime.UtcNow;
            Execute("update ProbeProgress set XP_StartTime = {0} where XP_ID = {1}", DateTime.UtcNow, this.probeProgressID);

            Event("Begin probing into " + nodeName);
            Event("Manufacture: " + nodeManufacture + "");
            if (nodeModel != null) Event("Model: " + nodeModel);

            Update(UpdateTypes.Remark, null);
            Update(UpdateTypes.TimeStamp, DateTime.UtcNow);

            // check node manufacture
            if (nodeManufacture == alu || nodeManufacture == cso || nodeManufacture == hwe || nodeManufacture == jun) ;
            else
            {
                Event("Unsupported node manufacture");
                Save();
                return;
            }

            #region CHECK IP

            if (nodeIP != null) Event("Host IP: " + nodeIP);

            Event("Checking host IP");
            string resolvedIP = MCECheckNodeIP(nodeName);

            if (nodeIP == null)
            {
                if (resolvedIP == null)
                {
                    Event("Hostname is unresolved");

                    if (previousRemark == "UNRESOLVED")
                    {
                        Event("Mark this node as inactive");
                        Update(UpdateTypes.Active, 0);
                    }
                    else
                        Update(UpdateTypes.Remark, "UNRESOLVED");

                    Save();
                    return;
                }
                else
                {
                    Event("Resolved Host IP: " + resolvedIP);
                    nodeIP = resolvedIP;
                    Update(UpdateTypes.IP, nodeIP);
                }
            }
            else
            {
                if (resolvedIP == null)
                {
                    Event("Hostname is unresolved");

                    // reverse ip?
                    Event("Resolving by reverse host name");
                    string hostName = MCECheckNodeIP(nodeIP, true);

                    if (hostName != null)
                    {
                        Result result = Query("select * from Node where NO_Name = {0}", hostName);

                        if (result.Count == 0)
                        {
                            Event("Node " + nodeName + " has changed to " + hostName);
                            if (!NecrowVirtualization.AliasExists(nodeName))
                            {
                                Execute("insert into NodeAlias(NA_ID, NA_NO, NA_Name) values({0}, {1}, {2})", Database.ID(), nodeID, nodeName);
                                NecrowVirtualization.AliasLoad();
                            }

                            Update(UpdateTypes.Name, hostName);

                            // Update interface virtualizations
                            if (nodeType == "P")
                            {
                                Tuple<string, List<Tuple<string, string, string, string, string, string>>> changeThis = null;
                                List<Tuple<string, string, string, string, string, string>> interfaces = null;
                                lock (NecrowVirtualization.PESync)
                                {
                                    foreach (Tuple<string, List<Tuple<string, string, string, string, string, string>>> entry in NecrowVirtualization.PEPhysicalInterfaces)
                                    {
                                        if (entry.Item1 == nodeName)
                                        {
                                            changeThis = entry;
                                            break;
                                        }
                                    }
                                    if (changeThis != null)
                                    {
                                        NecrowVirtualization.PEPhysicalInterfaces.Remove(changeThis);
                                        interfaces = changeThis.Item2;
                                    }
                                    else interfaces = new List<Tuple<string, string, string, string, string, string>>();

                                    NecrowVirtualization.PEPhysicalInterfaces.Add(new Tuple<string, List<Tuple<string, string, string, string, string, string>>>(hostName, interfaces));
                                    NecrowVirtualization.PEPhysicalInterfacesSort();
                                }
                            }
                            else if (nodeType == "M")
                            {
                                Tuple<string, List<Tuple<string, string, string, string, string, string, string>>> changeThis = null;
                                List<Tuple<string, string, string, string, string, string, string>> interfaces = null;
                                lock (NecrowVirtualization.MESync)
                                {
                                    foreach (Tuple<string, List<Tuple<string, string, string, string, string, string, string>>> entry in NecrowVirtualization.MEPhysicalInterfaces)
                                    {
                                        if (entry.Item1 == nodeName)
                                        {
                                            changeThis = entry;
                                            break;
                                        }
                                    }
                                    if (changeThis != null)
                                    {
                                        NecrowVirtualization.MEPhysicalInterfaces.Remove(changeThis);
                                        interfaces = changeThis.Item2;
                                    }
                                    else interfaces = new List<Tuple<string, string, string, string, string, string, string>>();

                                    NecrowVirtualization.MEPhysicalInterfaces.Add(new Tuple<string, List<Tuple<string, string, string, string, string, string, string>>>(hostName, interfaces));
                                    NecrowVirtualization.MEPhysicalInterfacesSort();
                                }
                            }
                            
                            nodeName = hostName;
                        }
                        else
                        {
                            Event("Node " + nodeName + " has new name " + hostName + ". " + hostName + " is already exists. TODO: resolve the conflicts");
                            Event("Mark this node as inactive");

                            Update(UpdateTypes.Active, 0);

                            Save();
                            return;
                        }                        
                    }
                    else
                    {
                        Event("Hostname has become unresolved");

                        if (previousRemark == "UNRESOLVED")
                        {
                            Event("Mark this node as inactive");
                            Update(UpdateTypes.Active, 0);
                        }
                        else
                        {
                            Event("Will check again in next iteration");
                            Update(UpdateTypes.Remark, "UNRESOLVED");
                        }

                        Save();
                        return;
                    }
                }
                else if (nodeIP != resolvedIP)
                {
                    Event("Host IP has changed to: " + resolvedIP + ", this node is now marked as inactive");
                    Update(UpdateTypes.Remark, "IPHASCHANGED");
                    Update(UpdateTypes.Active, 0);

                    Save();
                    return;
                }
            }

            Event("Host identified");

            outputIdentifier = nodeName;

            #endregion

            #region CONNECT

            string connectType = nodeConnectType;
            int connectSequence = 0;
            string connectBy = null;

            if (connectType == null)
            {
                if (nodeManufacture == alu || nodeManufacture == cso) connectType = "T";
                else if (nodeManufacture == hwe || nodeManufacture == jun) connectType = "S";
            }

            bool connectSuccess = false;

            while (true)
            {
                WaitUntilMCETerminalReady("MCE Waiting II");

                string currentConnectType = null;

                if (connectSequence == 0)
                {
                    if (connectType == "T") currentConnectType = "T";
                    else if (connectType == "S") currentConnectType = "S";
                }
                else if (connectSequence == 1)
                {
                    if (connectType == "T") currentConnectType = "S";
                    else if (connectType == "S") currentConnectType = "T";
                }

                if (currentConnectType == "T")
                {
                    connectSuccess = ConnectByTelnet(nodeName, nodeManufacture);
                    if (connectSuccess) connectBy = "T";
                    else Event("Telnet failed");
                }
                else if (currentConnectType == "S")
                {
                    connectSuccess = ConnectBySSH(nodeName, nodeManufacture);
                    if (connectSuccess) connectBy = "S";
                    else Event("SSH failed");
                }

                if (connectSuccess || connectSequence == 1) break;
                else connectSequence++;
            }

            if (connectSuccess == false)
            {
                Event("Connection failed");

                bool tacacError = false;
                int loop = 0;
                while (true)
                {
                    WaitUntilMCETerminalReady("MCE Waiting III");

                    string testOtherNode;

                    if (nodeName == "PE2-D2-CKA-VPN") testOtherNode = "PE-D2-CKA-VPN";
                    else testOtherNode = "PE2-D2-CKA-VPN";

                    Event("Trying to connect to other node...(" + testOtherNode + ")");

                    bool testConnected = ConnectByTelnet(testOtherNode, cso);

                    if (testConnected)
                    {
                        Exit(cso);
                        outputIdentifier = null;
                        Event("Connected! TACAC server is OK.");
                        break;
                    }
                    else
                    {
                        tacacError = true;
                        outputIdentifier = null;
                        Event("Connection failed, TACAC server is possible overloaded/error/not responding.");

                        int time = 1;
                        #region time
                        if (loop == 0)
                        {
                            Event("Try again in 1 minute");
                        }
                        else if (loop == 1)
                        {
                            Event("Try again in 5 minutes");
                            time = 5;
                        }
                        else if (loop == 2)
                        {
                            Event("Try again in 15 minutes");
                            time = 15;
                        }
                        else if (loop == 3)
                        {
                            Event("Try again in 30 minutes");
                            time = 30;
                        }
                        else if (loop >= 4)
                        {
                            Event("Try again in 1 hour");
                            time = 60;
                        }
                        #endregion

                        Thread.Sleep(60000 * time);
                        loop++;
                    }
                }

                if (tacacError)
                {
                    // this node is innocent
                    // TODO: try again?
                }
                else
                {
                    if (previousRemark == "CONNECTFAIL")
                        Update(UpdateTypes.Active, 0);
                    else
                        Update(UpdateTypes.Remark, "CONNECTFAIL");

                }

                Save();
                return;
            }

            if (nodeConnectType == null || connectBy != connectType)
                Update(UpdateTypes.ConnectType, connectBy);

            Event("Connected!");

            nodeConnected = true;

            string terminal = null;

            if (nodeManufacture == alu)
            {
                lines = LastOutput.Split('\n');
                string lastLine = lines[lines.Length - 1];

                int titik2 = lastLine.LastIndexOf(':');
                terminal = lastLine.Substring(titik2 + 1);
            }
            else if (nodeManufacture == hwe)
            {
                lines = LastOutput.Split('\n');
                string lastLine = lines[lines.Length - 1];
                terminal = lastLine;
            }
            else if (nodeManufacture == cso)
            {
                lines = LastOutput.Split('\n');
                string lastLine = lines[lines.Length - 1];
                terminal = lastLine;

                if (terminal.EndsWith(">"))
                {
                    Event("Error: Not In Privileged EXEC mode");
                    SaveExit();
                    return;
                }
            }
            else if (nodeManufacture == jun)
            {
                lines = LastOutput.Split('\n');
                string lastLine = lines[lines.Length - 1];

                string[] linex = lastLine.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                terminal = linex[1];
            }

            Event("Terminal: " + terminal + "");

            if (terminal != nodeTerminal) Update(UpdateTypes.Terminal, terminal);
            nodeTerminal = terminal;

            #endregion

            #region TIME

            Event("Checking Time");

            bool nodeTimeRetrieved = false;
            DateTime utcTime = DateTime.UtcNow;
            DateTime nodeTime = DateTime.MinValue;

            string[] junShowSystemUptimeLines = null;

            if (nodeManufacture == alu)
            {
                #region alu

                if (Request("show system time", out lines)) return;

                foreach (string line in lines)
                {
                    if (line.StartsWith("Current Date"))
                    {
                        //Current Date & Time : 2016/05/06 23:38:39
                        //01234567890123456789012345678901234567890
                        string ps = line.Substring(22, 19);
                        if (DateTime.TryParseExact(ps, "yyyy/MM/dd HH:mm:ss", null, DateTimeStyles.None, out nodeTime)) { nodeTimeRetrieved = true; }
                        break;
                    }
                }

                #endregion
            }
            else if (nodeManufacture == cso)
            {
                #region cso
                if (Request("show clock", out lines)) return;

                foreach (string line in lines)
                {
                    if (line.Length > 0 && char.IsDigit(line[0]))
                    {
                        string[] ps = line.Split('.');
                        //00:26:20.139 WIB Sat May 7 2016
                        string[] pt = ps[1].Split(' ');
                        if (DateTime.TryParseExact(string.Format("{0} {1} {2} {3}", ps[0], pt[3], pt[4], pt[5]), "HH:mm:ss MMM d yyyy", null, DateTimeStyles.None, out nodeTime)) { nodeTimeRetrieved = true; }
                        break;
                    }
                }

                #endregion
            }
            else if (nodeManufacture == hwe)
            {
                #region hwe
                if (Request("display clock", out lines)) return;

                foreach (string line in lines)
                {
                    if (line.Length > 0 && line[0] == '2')
                    {
                        //2016-05-06 16:52:51+08:00
                        //0123456789012345678901234
                        string[] ps = line.Split('+');
                        if (DateTime.TryParseExact(ps[0], "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out nodeTime)) { nodeTimeRetrieved = true; }
                        break;
                    }
                }

                #endregion
            }
            else if (nodeManufacture == jun)
            {
                #region jun
                if (Request("show system uptime", out junShowSystemUptimeLines)) return;

                foreach (string line in junShowSystemUptimeLines)
                {
                    if (line.StartsWith("Current time: "))
                    {
                        //Current time: 2016-05-07 00:02:48
                        //0123456789012345678901234567890123456789
                        string ps = line.Substring(14, 19);
                        if (DateTime.TryParseExact(ps, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out nodeTime)) { nodeTimeRetrieved = true; }
                        break;
                    }
                }

                #endregion
            }

            if (!nodeTimeRetrieved)
            {
                Event("Failure on node time retrieval");
                SaveExit();
                return;
            }

            utcTime = new DateTime(
                utcTime.Ticks - (utcTime.Ticks % TimeSpan.TicksPerSecond),
                utcTime.Kind
                ); // cut millisecond section

            nodeTimeOffset = nodeTime - utcTime;
            Event("Local time: " + nodeTime.ToString("yyyy/MM/dd HH:mm:ss"));
            Event("UTC offset: " + nodeTimeOffset.TotalHours + "h");
            Update(UpdateTypes.TimeOffset, nodeTimeOffset.TotalHours);

            #endregion

            #region TERMINAL SETUP

            Event("Setup terminal");

            noMore = true; // by default, we can no more

            if (nodeManufacture == alu)
            {
                if (Request("environment no saved-ind-prompt", out lines)) return;
                if (Request("environment no more", out lines)) return;

                string oline = string.Join(" ", lines);
                if (oline.IndexOf("CLI Command not allowed for this user.") > -1)
                    noMore = false;
                else
                    noMore = true;
            }
            else if (nodeManufacture == hwe)
            {
                if (Request("screen-length 0 temporary", out lines)) return;
            }
            else if (nodeManufacture == cso)
            {
                if (Request("terminal length 0", out lines)) return;
            }
            else if (nodeManufacture == jun)
            {
                if (Request("set cli screen-length 0", out lines)) return;
            }

            #endregion

            #region VERSION

            bool checkVersion = false;

            if (nodeVersion == null) checkVersion = true;
            else
            {
                DateTime versionTime = row["NO_VersionTime"].ToDateTime();
                TimeSpan span = utcTime - versionTime;
                if (span.TotalDays >= 7) checkVersion = true;
            }

            if (checkVersion)
            {
                Event("Checking Version");

                string version = null;
                string subVersion = null;
                string model = null;

                if (nodeManufacture == alu)
                {
                    #region alu
                    if (Request("show version | match TiMOS", out lines)) return;

                    foreach (string line in lines)
                    {
                        //TiMOS-C-11.0.R6 cp
                        //0123456789012345
                        if (line.StartsWith("TiMOS"))
                        {
                            version = line.Substring(0, line.IndexOf(' ')).Trim();
                            break;
                        }
                    }
                    #endregion
                }
                else if (nodeManufacture == hwe)
                {
                    #region hwe
                    if (Request("display version", out lines)) return;

                    foreach (string line in lines)
                    {
                        if (line.StartsWith("VRP (R) software"))
                        {
                            version = line.Substring(26, line.IndexOf(' ', 26) - 26).Trim();
                            break;
                        }
                    }
                    // additional setup for huawei >5.90 for screen-width tweak (help problem with 5.160 auto text-wrap)
                    //if (version == "5.160")
                    //{
                    //    if (Send("screen-width 80" + (char)13 + "Y")) { NodeStop(); return true; }
                    //    NodeRead(out timeout);
                    //    if (timeout) { NodeStop(); return true; }
                    //}

                    #endregion
                }
                else if (nodeManufacture == cso)
                {
                    #region cso
                    if (Request("show version | in IOS", out lines)) return;

                    string sl = string.Join("", lines.ToArray());

                    if (sl.IndexOf("Cisco IOS XR Software") > -1)
                    {
                        // ASR
                        model = "ASR";
                        version = "XR";
                        int iv = sl.IndexOf("Version");
                        if (iv > -1)
                        {
                            string ivonw = sl.Substring(iv + 7).Trim();
                            if (ivonw.Length > 0)
                            {
                                iv = ivonw.IndexOf('[');
                                if (iv == -1) iv = ivonw.IndexOf(',');
                                if (iv == -1) iv = ivonw.IndexOf(' ');
                                if (iv > -1)
                                {
                                    subVersion = ivonw.Substring(0, iv);
                                }
                            }
                        }
                    }
                    else
                    {
                        // IOS
                        version = "IOS";

                        // software
                        int iver = sl.IndexOf("Version ");
                        if (iver > -1)
                        {
                            string imod = sl.Substring(iver + 8, sl.IndexOf(',', iver + 8) - (iver + 8));
                            subVersion = imod;
                        }

                        // model
                        if (Request("show version | in bytes of memory", out lines)) return;

                        sl = string.Join("", lines.ToArray());
                        string slo = sl.ToLower();

                        int cisc = slo.IndexOf("cisco ");
                        if (cisc > -1)
                        {
                            string imod = sl.Substring(cisc + 6, sl.IndexOf(' ', cisc + 6) - (cisc + 6));

                            int iod = 0;
                            foreach (char imodc in imod)
                            {
                                if (char.IsDigit(imodc)) { break; }
                                iod++;
                            }
                            model = imod.Substring(iod);
                        }
                    }
                    #endregion
                }
                else if (nodeManufacture == jun)
                {
                    #region jun
                    if (Request("show version | match \"JUNOS Base OS boot\"", out lines)) return;

                    foreach (string line in lines)
                    {
                        if (line.StartsWith("JUNOS Base OS boot"))
                        {
                            string[] linex = line.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                            if (linex.Length >= 2) version = linex[1];
                            break;
                        }
                    }

                    #endregion
                }

                if (model != nodeModel)
                {
                    nodeModel = model;
                    Update(UpdateTypes.Model, model);
                    Event("Model discovered: " + model);
                }
                if (version != nodeVersion)
                {
                    nodeVersion = version;
                    Update(UpdateTypes.Version, version);
                    Event("Version updated: " + version);
                }
                if (subVersion != nodeSubVersion)
                {
                    nodeSubVersion = subVersion;
                    Update(UpdateTypes.SubVersion, subVersion);
                    Event("SubVersion updated: " + subVersion);
                }

                Update(UpdateTypes.VersionTime, DateTime.UtcNow);
            }

            if (nodeVersion == null)
            {
                Event("Cant determined node version.");
                SaveExit();
                return;
            }

            Event("Version: " + nodeVersion + ((nodeSubVersion != null) ? ":" + nodeSubVersion : ""));

            #endregion

            #region LAST CONFIGURATION

            Event("Checking Last Configuration");

            bool configurationHasChanged = false;
            bool lastConfLiveRetrieved = false;
            DateTime lastConfLive = DateTime.MinValue;

            if (nodeManufacture == alu)
            {
                #region alu
                if (Request("show system information | match \"Time Last Modified\"", out lines)) return;

                bool lastModified = false;

                foreach (string line in lines)
                {
                    if (line.StartsWith("Time Last Modified"))
                    {
                        lastModified = true;
                        //Time Last Modified     : 2
                        //Time Last Modified        : 2016/11/07 17:35:36
                        //01234567890123456789012345
                        string datetime = line.Substring(25).Trim(new char[] { ' ', ':' });
                        lastConfLive = DateTime.Parse(datetime);
                        lastConfLive = new DateTime(
                            lastConfLive.Ticks - (lastConfLive.Ticks % TimeSpan.TicksPerSecond),
                            lastConfLive.Kind
                            );
                        lastConfLiveRetrieved = true;
                        break;
                    }
                }

                if (lastModified == false)
                {
                    if (Request("show system information | match \"Time Last Saved\"", out lines)) return;

                    foreach (string line in lines)
                    {
                        if (line.StartsWith("Time Last Saved"))
                        {
                            lastModified = true;
                            //Time Last Saved        : 2015/01/13 01:13:56
                            //01234567890123456789012345
                            string datetime = line.Substring(25).Trim();
                            if (DateTime.TryParse(datetime, out lastConfLive))
                            {
                                lastConfLive = new DateTime(
                                    lastConfLive.Ticks - (lastConfLive.Ticks % TimeSpan.TicksPerSecond),
                                    lastConfLive.Kind
                                    );
                                lastConfLiveRetrieved = true;
                                break;
                            }
                        }
                    }
                }
                #endregion
            }
            else if (nodeManufacture == hwe)
            {
                #region hwe

                if (nodeVersion.StartsWith("8"))
                {
                    if (Request("display configuration commit list 1", out lines)) return;

                    foreach (string line in lines)
                    {
                        if (line.Length > 0 && char.IsDigit(line[0]))
                        {
                            //1    1000000583    -                    850106          2016-05-04 16:32:38+07:00
                            //0123456789012345678901234567890123456789012345678901234567890123456789
                            //          1         2         3         4         5         6
                            string ps = line.Substring(56, 19);
                            if (DateTime.TryParseExact(ps, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out lastConfLive)) lastConfLiveRetrieved = true;
                            break;
                        }
                    }
                }
                else
                {
                    if (Request("display changed-configuration time", out lines)) return;

                    StringBuilder datesection = null;
                    foreach (string line in lines)
                    {
                        //The time when system configuration has been changed lastly is:May 06 2016 03:28:39
                        if (line.StartsWith("The time"))
                        {
                            int colon = line.IndexOf(':');
                            if (colon < line.Length - 1)
                            {
                                datesection = new StringBuilder();
                                datesection.Append(line.Substring(colon + 1));
                            }
                        }
                        else if (datesection != null)
                        {
                            if (!line.StartsWith(terminal))
                                datesection.Append(line);
                        }
                    }

                    if (datesection != null)
                    {
                        if (DateTime.TryParseExact(datesection.ToString(), "MMM dd yyyy HH:mm:ss", null, DateTimeStyles.None, out lastConfLive)) lastConfLiveRetrieved = true;
                    }
                }
                #endregion
            }
            else if (nodeManufacture == cso)
            {
                #region cso
                if (nodeVersion == xr)
                {
                    #region xr
                    if (Request("show configuration history commit last 1 | in commit", out lines)) return;

                    foreach (string line in lines)
                    {
                        if (line.Length > 0 && line[0] == '1')
                        {
                            //1     commit     id 1000000807                  Fri
                            //01234567890123456789012345678901234567890123456789
                            string dateparts = line.Substring(48).Trim();

                            if (dateparts != null)
                            {
                                //Fri Jan 16 11:18:46 2015
                                string[] dates = dateparts.Split(StringSplitTypes.Space, StringSplitOptions.RemoveEmptyEntries);

                                if (DateTime.TryParse(dates[1] + " " + dates[2] + " " + dates[4] + " " + dates[3], out lastConfLive))
                                {
                                    lastConfLive = new DateTime(
                                        lastConfLive.Ticks - (lastConfLive.Ticks % TimeSpan.TicksPerSecond),
                                        lastConfLive.Kind
                                        );
                                    lastConfLiveRetrieved = true;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region ios

                    // because so many differences between IOS version, we might try every possible command
                    bool passed = false;

                    // most of ios version will work this way
                    if (Request("show configuration id detail", out lines)) return;

                    foreach (string line in lines)
                    {
                        if (line.StartsWith("Last change time"))
                        {
                            //Last change time             : 2015-01-17T02:33:01.553Z
                            //01234567890123456789012345678901234567890123456789
                            //          1         2         3
                            string dateparts = line.Substring(31).Trim();
                            if (dateparts != null)
                            {
                                if (DateTime.TryParse(dateparts, out lastConfLive))
                                {
                                    Event("Using configuration id");
                                    passed = true;
                                    lastConfLive = new DateTime(
                                        lastConfLive.Ticks - (lastConfLive.Ticks % TimeSpan.TicksPerSecond),
                                        lastConfLive.Kind
                                        );
                                    lastConfLiveRetrieved = true;
                                    break;
                                }
                            }
                            break;
                        }
                    }

                    if (passed == false)
                    {
                        // using xr-like command history
                        //show configuration history
                        if (Request("show configuration history", out lines)) return;

                        string lastline = null;
                        foreach (string line in lines)
                        {
                            if (line.IndexOf("Invalid input detected") > -1) break;
                            else if (line == "") break;
                            else if (char.IsDigit(line[0])) lastline = line;
                        }

                        if (lastline != null)
                        {
                            string[] linex = lastline.Split(StringSplitTypes.Space, StringSplitOptions.RemoveEmptyEntries);
                            //5    23:33:21 WIB Tue Mar 1 2016User : ffm-myarham
                            string datetime = linex[1];
                            string datemonth = linex[4].ToUpper();
                            string datedate = linex[5];
                            string dateyear = linex[6];
                            if (dateyear.Length > 4) dateyear = dateyear.Substring(0, 4);

                            string datestr = datemonth + " " + datedate + " " + dateyear + " " + datetime;

                            DateTime parsedDT = DateTime.MinValue;
                            if (DateTime.TryParse(datestr, out parsedDT))
                            {
                                Event("Using configuration history");
                                passed = true;
                                lastConfLive = parsedDT;
                                lastConfLiveRetrieved = true;
                            }
                        }
                    }

                    if (passed == false)
                    {
                        // and here we are, using forbidden command ever
                        if (Request("show log | in CONFIG_I", out lines)) return;

                        string lastline = null;
                        foreach (string line in lines)
                        {
                            if (line.StartsWith("*")) lastline = line;
                        }

                        if (lastline != null)
                        {
                            string[] lastlines = lastline.Split(StringSplitTypes.Space, StringSplitOptions.RemoveEmptyEntries);

                            string datemonth = lastlines[0].TrimStart(new char[] { '*' }).ToUpper();
                            string datedate = lastlines[1];
                            string datetime = lastlines[2];

                            string datestr = datemonth + " " + datedate + " " + DateTime.Now.Year + " " + datetime;

                            DateTime parsedDT = DateTime.MinValue;
                            if (DateTime.TryParse(datestr, out parsedDT))
                            {
                                Event("Using log config");
                                passed = true;

                                if (parsedDT > DateTime.Now)
                                {
                                    // probably year is wrong, so use last year
                                    string datestrrev = datemonth + " " + datedate + " " + (DateTime.Now.Year - 1) + " " + datetime;
                                    parsedDT = DateTime.Parse(datestrrev);
                                }

                                lastConfLive = parsedDT;
                                lastConfLiveRetrieved = true;
                            }
                        }
                    }

                    if (passed == false)
                    {
                        // and... if everything fail, we will use this slowlest command ever
                        if (Request("show run | in Last config", out lines)) return;

                        foreach (string line in lines)
                        {
                            //! Last configuration change at 1
                            //01234567890123456789012345678901
                            if (line.StartsWith("! Last config"))
                            {
                                string eline = line.Substring(31);
                                string[] linex = eline.Split(StringSplitTypes.Space, StringSplitOptions.RemoveEmptyEntries);
                                //17:27:19 WIB Tue Mar 1 2016 
                                //0        1   2   3   4 5
                                string datetime = linex[0];
                                string datemonth = linex[3].ToUpper();
                                string datedate = linex[4];
                                string dateyear = linex[5];
                                string datestr = datemonth + " " + datedate + " " + dateyear + " " + datetime;

                                DateTime parsedDT = DateTime.MinValue;
                                if (DateTime.TryParse(datestr, out parsedDT))
                                {
                                    Event("Using running configuration");
                                    passed = true;
                                    lastConfLive = parsedDT;
                                    lastConfLiveRetrieved = true;
                                }
                                break;
                            }
                        }
                    }

                    #endregion
                }
                #endregion
            }
            else if (nodeManufacture == jun)
            {
                #region jun
                foreach (string line in junShowSystemUptimeLines)
                {
                    if (line.StartsWith("Last configured: "))
                    {
                        //Last configured: 2015-01-20 09:53:54
                        //0123456789012345678901234567890123456789
                        string ps = line.Substring(17, 19);
                        if (DateTime.TryParseExact(ps, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out lastConfLive)) { lastConfLiveRetrieved = true; }
                        break;
                    }
                }
                #endregion
            }

            DateTime lastConfDB = row["NO_LastConfiguration"].ToDateTime();

            if (!lastConfLiveRetrieved)
            {
                Event("Failure on last configuration retrieval");
                SaveExit();
                return;
            }

            lastConfLive = lastConfLive - nodeTimeOffset;
            TimeSpan difference = lastConfLive - lastConfDB;

            if (Math.Abs(difference.TotalSeconds) > 1)
            {
                Event("Saved: " + lastConfDB.ToString("yyyy/MM/dd HH:mm:ss") + " UTC");
                Event("Actual: " + lastConfLive.ToString("yyyy/MM/dd HH:mm:ss") + " UTC");
                Event("Configuration has changed!");
                Update(UpdateTypes.LastConfiguration, lastConfLive);
                configurationHasChanged = true;
            }
            else
            {
                Event("Saved: " + lastConfLive.ToString("yyyy/MM/dd HH:mm:ss") + " UTC");
                Event("Actual: " + lastConfLive.ToString("yyyy/MM/dd HH:mm:ss") + " UTC");
                Event("Configuration is up to date");
            }

            #endregion

            #region CPU / MEMORY

            Event("Checking CPU / Memory"); //mengecek memory dan CPU 

            int cpu = -1;
            int mtotal = -1;
            int mused = -1;

            #region Live

            if (nodeManufacture == cso)
            {
                #region cso
                if (Request("show processes cpu | in CPU", out lines)) return;

                foreach (string line in lines)
                {
                    if (line.StartsWith("CPU "))
                    {
                        int oid = line.Trim().LastIndexOf(' ');
                        if (oid > -1)
                        {
                            string okx = line.Substring(oid + 1).Trim();
                            string perc = okx.Substring(0, okx.IndexOf('%'));
                            if (!int.TryParse(perc, out cpu)) cpu = -1;
                        }
                    }
                }

                if (nodeVersion == xr)
                {
                    //show memory summary | in Physical Memory
                    if (Request("show memory summary | in Physical Memory", out lines)) return;

                    foreach (string line in lines)
                    {
                        string lint = line.Trim();
                        if (lint.StartsWith("Physical Memory: "))
                        {
                            string[] linex = lint.Split(StringSplitTypes.Space, StringSplitOptions.RemoveEmptyEntries);

                            string ltot = linex[2];
                            string lfree = linex[4];

                            ltot = ltot.Substring(0, ltot.Length - 1);
                            lfree = lfree.Substring(1, lfree.Length - 2);

                            int ltots;
                            if (int.TryParse(ltot, out ltots))
                                mtotal = ltots * 1000;
                            else
                                mtotal = -1;

                            if (mtotal > -1)
                            {
                                int lfrees;
                                if (int.TryParse(lfree, out lfrees))
                                    mused = mtotal - (lfrees * 1000);
                                else
                                    mused = -1;
                            }

                        }

                    }
                }
                else
                {
                    //show process memory  | in Processor Pool
                    if (Request("show process memory | in Total:", out lines)) return;

                    foreach (string line in lines)
                    {
                        string lint = line.Trim();
                        if (lint.StartsWith("Processor Pool"))
                        {
                            string[] linex = lint.Split(StringSplitTypes.Space, StringSplitOptions.RemoveEmptyEntries);

                            string ltot = linex[3];
                            string luse = linex[5];

                            double ltots;
                            if (!double.TryParse(ltot, out ltots)) ltots = -1;

                            double luses;
                            if (!double.TryParse(luse, out luses)) luses = -1;

                            if (ltots >= 0) mtotal = (int)Math.Round(ltots / 1000);
                            if (luses >= 0) mused = (int)Math.Round(luses / 1000);
                            break;
                        }
                        else if (lint.StartsWith("Total:"))
                        {
                            string[] linex = lint.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                            string ltot = linex[0].Trim().Split(StringSplitTypes.Space)[1];
                            string luse = linex[1].Trim().Split(StringSplitTypes.Space)[1];

                            double ltots;
                            if (!double.TryParse(ltot, out ltots)) ltots = -1;

                            double luses;
                            if (!double.TryParse(luse, out luses)) luses = -1;

                            if (ltots >= 0) mtotal = (int)Math.Round(ltots / 1000);
                            if (luses >= 0) mused = (int)Math.Round(luses / 1000);
                            break;
                        }
                    }
                }

                #endregion
            }
            else if (nodeManufacture == alu)
            {
                #region alu
                if (Request("show system cpu | match \"Busiest Core\"", out lines)) return;

                foreach (string line in lines)
                {
                    string lint = line.Trim();

                    if (lint.StartsWith("Busiest Core "))
                    {
                        int oid = lint.LastIndexOf(' ');
                        if (oid > -1)
                        {
                            string okx = lint.Substring(oid + 1).Trim();
                            string perc = okx.Substring(0, okx.IndexOf('%'));

                            float cpuf;
                            if (!float.TryParse(perc, out cpuf)) cpuf = -1;

                            if (cpuf == -1) cpu = -1;
                            else cpu = (int)Math.Round(cpuf);
                        }
                    }
                }

                //show system memory-pools | match bytes
                if (Request("show system memory-pools | match bytes", out lines)) return;

                foreach (string line in lines)
                {
                    string lint = line.Trim();

                    if (lint.StartsWith("Total In Use") || lint.StartsWith("Available Memory"))
                    {
                        string[] linex = lint.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (linex.Length >= 2)
                        {
                            string ibytes = linex[1].Trim();
                            ibytes = ibytes.Substring(0, ibytes.IndexOf(' '));
                            string[] ibytesx = ibytes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            string cbytes = string.Join("", ibytesx);

                            double dbytes;
                            if (!double.TryParse(cbytes, out dbytes)) dbytes = -1;

                            if (lint.StartsWith("Total In Use") && dbytes > -1)
                            {
                                mused = (int)Math.Round(dbytes / 1000);
                            }
                            else if (mused > -1 && lint.StartsWith("Available Memory") && dbytes > -1)
                            {
                                mtotal = mused + (int)Math.Round(dbytes / 1000);
                            }
                        }
                    }
                }

                #endregion
            }
            else if (nodeManufacture == hwe)
            {
                #region hwe
                if (Request("display cpu-usage", out lines)) return;

                foreach (string line in lines)
                {
                    string lint = line.Trim();

                    if (nodeVersion.StartsWith("8"))
                    {
                        //System cpu use rate is : 10%
                        //012345678901234567890123456789
                        if (lint.StartsWith("System cpu use rate is"))
                        {
                            string okx = line.Substring(25).Trim();
                            string perc = okx.Substring(0, okx.IndexOf('%'));
                            if (!int.TryParse(perc, out cpu)) cpu = -1;
                            break;
                        }
                    }
                    else
                    {
                        if (lint.StartsWith("CPU utilization for"))
                        {
                            int oid = lint.LastIndexOf(' ');
                            if (oid > -1)
                            {
                                string okx = line.Substring(oid + 1).Trim();
                                string perc = okx.Substring(0, okx.IndexOf('%'));
                                if (!int.TryParse(perc, out cpu)) cpu = -1;
                            }
                            break;
                        }
                    }
                }

                if (Request("display memory-usage", out lines)) return;

                foreach (string line in lines)
                {
                    string lint = line.Trim();

                    if (lint.StartsWith("System Total") || lint.StartsWith("Total Memory"))
                    {
                        string[] linex = lint.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                        if (linex.Length >= 2)
                        {
                            string odata = linex[1].Trim();
                            odata = odata.Substring(0, odata.IndexOf(' '));

                            double dbytes;
                            if (!double.TryParse(odata, out dbytes)) dbytes = -1;

                            if (lint.StartsWith("System Total"))
                            {
                                if (nodeVersion == "8.80") // already in Kbytes
                                    mtotal = (int)Math.Round(dbytes);
                                else
                                    mtotal = (int)Math.Round(dbytes / 1000);
                            }
                            else if (lint.StartsWith("Total Memory"))
                            {
                                if (nodeVersion == "8.80") // already in Kbytes
                                    mused = (int)Math.Round(dbytes);
                                else
                                    mused = (int)Math.Round(dbytes / 1000);
                            }
                        }
                    }
                }
                #endregion
            }
            else if (nodeManufacture == jun)
            {
                #region jun

                //show chassis routing-engine | match Idle
                if (Request("show chassis routing-engine | match Idle", out lines)) return;

                foreach (string line in lines)
                {
                    string lint = line.Trim();

                    if (lint.StartsWith("Idle"))
                    {
                        string[] linex = lint.Split(StringSplitTypes.Space, StringSplitOptions.RemoveEmptyEntries);
                        if (linex.Length == 3)
                        {
                            string perc = linex[1];
                            int idlecpu;
                            if (int.TryParse(perc, out idlecpu))
                            {
                                cpu = 100 - idlecpu;
                            }
                            else
                            {
                                cpu = -1;
                            }
                        }
                        break;
                    }
                }

                //show task memory
                if (Request("show task memory", out lines)) return;

                foreach (string line in lines)
                {
                    string lint = line.Trim();

                    if (lint.StartsWith("Currently In Use:") || lint.StartsWith("Available:"))
                    {
                        string[] linex = lint.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                        if (linex.Length >= 2)
                        {
                            string rightside = linex[1].Trim();

                            linex = rightside.Split(StringSplitTypes.Space, StringSplitOptions.RemoveEmptyEntries);

                            if (linex.Length >= 1)
                            {
                                string odata = linex[0].Trim();
                                int dbytes;
                                if (!int.TryParse(odata, out dbytes)) dbytes = -1;

                                if (lint.StartsWith("Currently In Use:"))
                                {
                                    mused = dbytes;
                                }
                                else if (mused > -1 && lint.StartsWith("Available:"))
                                {
                                    mtotal = mused + dbytes;
                                }
                            }
                        }
                    }
                }

                #endregion
            }

            #endregion

            #region Check and Update

            if (cpu > -1 && cpu < 100)
            {
                Event("CPU = " + cpu + "%");
                Summary("CPU", cpu);
            }
            else
            {
                Event("CPU load is unavailable (" + cpu + ")");
                Summary("CPU", null);
            }
            if (mtotal > -1)
            {
                Event("Memory total = " + mtotal + "KB");
                Summary("MEMORY_TOTAL", mtotal);
            }
            else
            {
                Event("Memory total is unavailable");
                Summary("MEMORY_TOTAL", null);
            }
            if (mused > -1)
            {
                Event("Memory used = " + mused + "KB");
                Summary("MEMORY_USED", mused);
            }
            else
            {
                Event("Memory used is unavailable");
                Summary("MEMORY_USED", null);
            }

            #endregion

            #endregion

            if (configurationHasChanged || nodeNVER < Necrow.Version)
            {
                continueProcess = true;
                if (nodeNVER < Necrow.Version)
                {
                    Event("Updated to newer Necrow version");
                    Update(UpdateTypes.NecrowVersion, Necrow.Version);
                }
            }
            else if (prioritizeProcess)
            {
                Event("Prioritized node, continuing process");
                continueProcess = true;
            }
            else
            {
                SaveExit();
            }
        }

        private void UpdateInfo(StringBuilder updateInfo, string title, string info)
        {
            if (updateInfo.Length > 0) updateInfo.Append(", ");
            if (info != null)
            {
                if (info.Length > 0) info = " " + info;
            }
            else info = "";
            updateInfo.Append(title + info);
        }

        private void UpdateInfo(StringBuilder updateInfo, string title, string from, string to)
        {
            if (updateInfo.Length > 0) updateInfo.Append(", ");
            if (from != null)
            {
                if (from.Length > 10) from = from.Substring(0, 10) + "...";
                else if (from.Length == 0) from = "<empty>";
            }
            else from = "NULL";
            if (to != null)
            {
                if (to.Length > 10) to = to.Substring(0, 10) + "...";
                else if (to.Length == 0) to = "<empty>";
            }
            else to = "NULL";
            updateInfo.Append(title + " " + from + " -> " + to);
        }

        private void UpdateInfo(StringBuilder updateInfo, string title, string from, string to, bool changeInfo)
        {
            if (changeInfo)
            {
                if (updateInfo.Length > 0) updateInfo.Append(", ");
                if (from == null)
                {
                    updateInfo.Append(title + " assigned");
                }
                else if (to == null)
                {
                    updateInfo.Append(title + " removed");
                }
                else
                {
                    updateInfo.Append(title + " changed");
                }
            }
            else
                UpdateInfo(updateInfo, title, from, to);
        }

        private void ServiceExecute(ServiceReference reference)
        {
            Batch batch = Batch();
            Result result;

            List<Tuple<ServiceBaseToDatabase, ServiceMapping>> mappings = reference.Mappings;
            List<string> customerid = new List<string>();
            List<string> serviceid = new List<string>();
            List<string> servicebycustomerid = new List<string>();
            Dictionary<string, Row> customerdb = new Dictionary<string, Row>();
            Dictionary<string, Row> servicedb = new Dictionary<string, Row>();
            Dictionary<string, List<Row>> servicebycustomerdb = new Dictionary<string, List<Row>>();
            List<CustomerToDatabase> customerinsert = new List<CustomerToDatabase>();
            List<CustomerToDatabase> customerupdate = new List<CustomerToDatabase>();
            List<ServiceToDatabase> serviceinsert = new List<ServiceToDatabase>();
            List<ServiceToDatabase> serviceupdate = new List<ServiceToDatabase>();

            foreach (Tuple<ServiceBaseToDatabase, ServiceMapping> tuple in mappings)
            {
                string sid = tuple.Item2.SID;
                string cid = tuple.Item2.CID;
                if (serviceid.IndexOf(sid) == -1) serviceid.Add(sid);
                if (cid != null && customerid.IndexOf(cid) == -1) customerid.Add(cid);
            }

            Result customerresult = Query("select * from ServiceCustomer where SC_CID in {0}", customerid);
            foreach (Row row in customerresult)
            {
                string c_id = row["SC_ID"].ToString();

                customerdb.Add(row["SC_CID"].ToString(), row);
                if (!row["SC_Name_Set"].ToBool(false)) servicebycustomerid.Add(row["SC_ID"].ToString());

                if (!servicebycustomerdb.ContainsKey(c_id)) servicebycustomerdb.Add(c_id, new List<Row>());
            }
            Result serviceresult = Query("select * from Service where SE_SID in {0} or SE_SC in {1}", serviceid, servicebycustomerid);
            foreach (Row row in serviceresult)
            {
                servicedb.Add(row["SE_SID"].ToString(), row);
                string c_id = row["SE_SC"].ToString();
                if (c_id != null)
                {
                    if (servicebycustomerdb.ContainsKey(c_id)) servicebycustomerdb[c_id].Add(row);
                    else
                    {
                        List<Row> lrow = new List<Row>();
                        lrow.Add(row);
                        servicebycustomerdb.Add(c_id, lrow);
                    }
                }
            }
            foreach (Tuple<ServiceBaseToDatabase, ServiceMapping> tuple in mappings)
            {
                string sid = tuple.Item2.SID;
                string cid = tuple.Item2.CID;
                string stype = tuple.Item2.ServiceType;
                string ssubtype = tuple.Item2.ServiceSubType;
                string cdesc = tuple.Item2.CleanDescription;

                string s_type = null, s_subtype = null;
                if (stype == "VPNIP")
                {
                    s_type = "VP";
                    if (ssubtype == "TRANS") s_subtype = "TA";
                }
                else if (stype == "ASTINET") s_type = "AS";
                else if (stype == "ASTINETBB") s_type = "AB";
                else if (stype == "VPNINSTAN") s_type = "VI";

                string c_id = null, c_name = null, s_id = null;

                #region sc
                if (cid != null)
                {
                    if (customerdb.ContainsKey(cid))
                    {
                        c_id = customerdb[cid]["SC_ID"].ToString();
                        c_name = customerdb[cid]["SC_Name"].ToString();
                    }
                    else
                    {
                        c_id = Database.ID();
                        c_name = cdesc;

                        Row ncdb = new Row();
                        ncdb.Add("SC_ID", new Column("SC_ID", c_id, false));
                        ncdb.Add("SC_CID", new Column("SC_CID", cid, false));
                        ncdb.Add("SC_Name", new Column("SC_Name", c_name, false));
                        ncdb.Add("SC_Name_Set", new Column("SC_Name_Set", null, true));
                        customerdb.Add(cid, ncdb);

                        Event("Customer ADD: " + c_name + " (" + cid + ")");
                        CustomerToDatabase c = new CustomerToDatabase();
                        c.ID = c_id;
                        c.CID = cid;
                        c.Name = c_name;
                        customerinsert.Add(c);

                        servicebycustomerdb.Add(c_id, new List<Row>());
                    }
                }
                #endregion

                #region se
                if (servicedb.ContainsKey(sid))
                {
                    s_id = servicedb[sid]["SE_ID"].ToString();

                    if (servicedb[sid]["SE_Type"].ToString() == null && s_type != null)
                    {
                        Event("Service UPDATE: " + sid + " " + s_type + " " + s_subtype);
                        ServiceToDatabase c = new ServiceToDatabase();
                        c.ID = s_id;
                        c.Type = s_type;
                        c.SubType = s_subtype;
                        serviceupdate.Add(c);
                    }

                    tuple.Item1.ServiceID = s_id;
                }
                else
                {
                    s_id = Database.ID();

                    Row ndb = new Row();
                    ndb.Add("SE_ID", new Column("SE_ID", s_id, false));
                    ndb.Add("SE_SID", new Column("SE_SID", sid, false));
                    ndb.Add("SE_SC", new Column("SE_SC", c_id, false));
                    ndb.Add("SE_Type", new Column("SE_Type", s_type, s_type == null ? true : false));
                    ndb.Add("SE_SubType", new Column("SE_SubType", s_subtype, s_subtype == null ? true : false));
                    ndb.Add("SE_Raw_Desc", new Column("SE_Raw_Desc", cdesc, cdesc == null ? true : false));
                    servicedb.Add(sid, ndb);

                    Event("Service ADD: " + sid + " (" + cid + ")");
                    ServiceToDatabase c = new ServiceToDatabase();
                    c.ID = s_id;
                    c.SID = sid;
                    c.CustomerID = c_id;
                    c.Type = s_type;
                    c.SubType = s_subtype;
                    c.RawDesc = cdesc;
                    serviceinsert.Add(c);

                    //set interface to this service
                    tuple.Item1.ServiceID = s_id;

                    if (c_id != null)
                        servicebycustomerdb[c_id].Add(ndb);
                }
                #endregion

                #region Name Processing

                if (c_id != null)
                {
                    List<Row> rownems = servicebycustomerdb[c_id];

                    if (rownems.Count > 1)
                    {
                        List<string> nems = new List<string>();

                        foreach (Row rownem in rownems)
                        {
                            string[] rds = rownem["SE_Raw_Desc"].ToString()
                                .Split(
                                new string[] { ",", "U/", "JL.", "JL ", "(", "[", "]", ")", "LT.", " LT ", " LT",
                                            "GD.", " KM", " KOMP.", " BLOK ",
                                            " SID ", " SID:", " SID-", " SID=",
                                            " CID ", " CID:", " CID-", " CID=", " CID.", "EXCID", " JL", " EX ",
                                            " FAA:", " FAI:", " FAA-", " FAI-", " CINTA ",
                                            " EX-",
                                            " KK ", "TBK", "BANDWIDTH",  },
                                StringSplitOptions.RemoveEmptyEntries);

                            if (rds.Length > 0)
                                nems.Add(rds[0].Trim());
                        }

                        Dictionary<string, int> lexicals = new Dictionary<string, int>();

                        int totaln = 0;
                        foreach (string nem in nems)
                        {
                            string[] nemp = nem.Split(StringSplitTypes.Space, StringSplitOptions.RemoveEmptyEntries);
                            for (int ni = 0; ni < nemp.Length; ni++)
                            {
                                for (int nj = 1; nj <= nemp.Length - ni; nj++)
                                {
                                    string[] subn = new string[nj];
                                    Array.Copy(nemp, ni, subn, 0, nj);
                                    string sub = string.Join(" ", subn);
                                    if (lexicals.ContainsKey(sub))
                                    {
                                        lexicals[sub] += 1;
                                        totaln++;
                                    }
                                    else
                                        lexicals.Add(sub, 1);
                                }
                            }
                        }

                        List<KeyValuePair<string, int>> lexicalList = lexicals.ToList();

                        if (lexicalList.Count > 0)
                        {
                            lexicalList.Sort((a, b) =>
                            {
                                if (a.Value > b.Value) return -1;
                                else if (a.Value < b.Value) return 1;
                                else
                                {
                                    if (a.Key.Length > b.Key.Length) return -1;
                                    else if (a.Key.Length < b.Key.Length) return 1;
                                    else return 0;
                                }
                            });

                            string cname = lexicalList[0].Key;

                            if (lexicalList[0].Value > 1)
                            {
                                for (int li = 0; li < (lexicalList.Count > 10 ? 10 : lexicalList.Count); li++)
                                {
                                    KeyValuePair<string, int> lip = lexicalList[li];

                                    string likey = lip.Key;
                                    int lival = lip.Value;
                                    int likeylen = StringHelper.CountWord(likey);
                                    bool lolos = true;
                                    for (int ly = li + 1; ly < (lexicalList.Count > 10 ? 10 : lexicalList.Count); ly++)
                                    {
                                        KeyValuePair<string, int> lyp = lexicalList[ly];

                                        string lykey = lyp.Key;
                                        int lyval = lyp.Value;
                                        int lykeylen = StringHelper.CountWord(lykey);

                                        if (lykey.Length > likey.Length)
                                        {
                                            if (likeylen > 1)
                                            {
                                                if (lykey.IndexOf(likey) > -1)
                                                {
                                                    int distance = lival - lyval;

                                                    double dtotaln = (double)totaln;
                                                    double minx = Math.Pow((1 / Math.Log(0.08 * dtotaln + 3)), 1.75);
                                                    if (((double)distance / dtotaln) > minx) { }
                                                    else lolos = false;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (lykey.IndexOf(likey) > -1)
                                                {
                                                    if ((ly - li) < 4)
                                                    {
                                                        int distance = lival - lyval;

                                                        double dtotaln = (double)totaln;
                                                        double minx = Math.Pow((1 / Math.Log(0.08 * dtotaln + 3)), 1.75);
                                                        if (((double)distance / dtotaln) > minx) { }
                                                        else lolos = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        lolos = false;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (lykeylen == 1) { }
                                            else
                                            {
                                                if (lykeylen < likeylen) { if (likey.IndexOf(lykey) > -1) break; }
                                                else break;
                                            }
                                        }
                                    }

                                    if (lolos)
                                    {
                                        cname = likey;
                                        break;
                                    }
                                }
                            }

                            if (cname != null)
                            {
                                cname = cname.Trim(new char[] { ' ', '\"', '\'', '&', '(', ')', '-' });
                                cname = cname.Replace("PT.", "PT");
                                cname = cname.Replace(" PT", "");
                                cname = cname.Replace("PT ", "");

                                if (cname != c_name)
                                {
                                    Event("Customer UPDATE: " + cname + " (" + cid + ")");

                                    customerdb.Remove(cid);

                                    Row ncdb = new Row();
                                    ncdb.Add("SC_ID", new Column("SC_ID", c_id, false));
                                    ncdb.Add("SC_CID", new Column("SC_CID", cid, false));
                                    ncdb.Add("SC_Name", new Column("SC_Name", cname, false));
                                    ncdb.Add("SC_Name_Set", new Column("SC_Name_Set", null, true));

                                    customerdb.Add(cid, ncdb);

                                    CustomerToDatabase c = new CustomerToDatabase();
                                    c.ID = c_id;
                                    c.Name = cname;
                                    customerupdate.Add(c);
                                }
                            }

                        }
                    }
                }
                #endregion
            }

            // CUSTOMER ADD
            batch.Begin();
            foreach (CustomerToDatabase s in customerinsert)
            {
                Insert insert = Insert("ServiceCustomer");
                insert.Value("SC_ID", s.ID);
                insert.Value("SC_CID", s.CID);
                insert.Value("SC_Name", s.Name);
                batch.Execute(insert);
            }
            result = batch.Commit();
            Event(result, EventActions.Add, EventElements.Customer, false);

            // CUSTOMER UPDATE
            batch.Begin();
            foreach (CustomerToDatabase s in customerupdate)
            {
                batch.Execute("update ServiceCustomer set SC_Name = {0} where SC_ID = {1}", s.Name, s.ID);
            }
            result = batch.Commit();
            Event(result, EventActions.Update, EventElements.Customer, false);

            // SERVICE ADD
            batch.Begin();
            foreach (ServiceToDatabase s in serviceinsert)
            {
                Insert insert = Insert("Service");
                insert.Value("SE_ID", s.ID);
                insert.Value("SE_SID", s.SID);
                insert.Value("SE_SC", s.CustomerID);
                insert.Value("SE_Type", s.Type);
                insert.Value("SE_SubType", s.SubType);
                insert.Value("SE_Raw_Desc", s.RawDesc);
                batch.Execute(insert);
            }
            result = batch.Commit();
            Event(result, EventActions.Add, EventElements.Service, false);

            // SERVICE UPDATE
            batch.Begin();
            foreach (ServiceToDatabase s in serviceupdate)
            {
                Update update = Update("Service");
                update.Set("SE_Type", s.Type);
                update.Set("SE_SubType", s.SubType);
                update.Where("SE_ID", s.ID);
                batch.Execute(update);
            }
            result = batch.Commit();
            Event(result, EventActions.Update, EventElements.Service, false);
        }

        public string FindNeighborPart(string description, string name)
        {
            int find = description.IndexOf(name);
            int findLength = name.Length;

            if (find == -1)
            {
                if (name.StartsWith("ME-"))
                {
                    // coba pake ME1-
                    string nameAlternate = name.Replace("ME-", "ME1-");
                    find = description.IndexOf(nameAlternate);
                    if (find > -1) findLength = nameAlternate.Length;
                }
                else if (name.StartsWith("PE-"))
                {
                    // coba pake PE1-
                    string nameAlternate = name.Replace("PE-", "PE1-");
                    find = description.IndexOf(nameAlternate);
                    if (find > -1) findLength = nameAlternate.Length;
                }
            }
            if (find == -1)
            {
                Match m = captureNodeTypeNumberLocation.Match(name);

                if (m.Success && m.Groups[0].Value == name && m.Groups.Count == 4)
                {
                    string peme = m.Groups[1].Value;
                    string number = m.Groups[2].Value;
                    string loc = m.Groups[3].Value;

                    // ME9-CKA
                    string pemeNumberStripLoc = " " + peme + number + "-" + loc + " ";
                    find = description.IndexOf(pemeNumberStripLoc);
                    if (find > -1) findLength = pemeNumberStripLoc.Length;

                    // ME-CKA9
                    if (find == -1)
                    {
                        string pemeStripLocNumber = " " + peme + "-" + loc + number + " ";
                        find = description.IndexOf(pemeStripLocNumber);
                        if (find > -1) findLength = pemeStripLocNumber.Length;
                    }

                    // CKA-9
                    if (find == -1)
                    {
                        string locStripNumber = " " + loc + "-" + number + " ";
                        find = description.IndexOf(locStripNumber);
                        if (find > -1) findLength = locStripNumber.Length;
                    }

                    // CKA9
                    if (find == -1)
                    {
                        string locNumber = " " + loc + number + " ";
                        find = description.IndexOf(locNumber);
                        if (find > -1) findLength = locNumber.Length;
                    }

                }
            }

            if (find > -1) return description.Substring(find + findLength);
            else return null;
        }

        private string FindNeighborPartUsingAlias(string description, string name)
        {
            int find = -1;
            int findLength = name.Length;

            if (NecrowVirtualization.Aliases.ContainsKey(name))
            {
                // search with alias
                foreach (string alias in NecrowVirtualization.Aliases[name])
                {
                    find = description.IndexOf(alias);
                    if (find > -1)
                    {
                        findLength = alias.Length;
                        break;
                    }
                }
            }

            if (find > -1) return description.Substring(find + findLength);
            else return null;
        }

        private string[] GenerateTestInterface(string type, string port)
        {
            List<string> testInterfaces = new List<string>();

            if (type != null && Necrow.InterfaceTestPrefixes.ContainsKey(type))
            {
                foreach (string prefix in Necrow.InterfaceTestPrefixes[type])
                {
                    testInterfaces.Add(port);
                    testInterfaces.Add(prefix + port);
                    testInterfaces.Add(prefix + "-" + port);
                    testInterfaces.Add(prefix + " " + port);
                    testInterfaces.Add(prefix + "/" + port);
                }
            }
            else testInterfaces.Add(port);

            testInterfaces = List.Sort(testInterfaces, SortMethods.LengthDescending);

            return testInterfaces.ToArray();
        }

        private void FindPhysicalNeighbor(InterfaceToDatabase li)
        {
            if (li.PhysicalNeighborChecked) return;
            li.PhysicalNeighborChecked = true;

            string description = CleanDescription(li.Description, nodeName);
            if (description == null) return;

            string interfaceName = li.Name;
            string interfaceType = li.InterfaceType;
            string interfacePort = interfaceName.Substring(2);

            bool done = false;

            Result result;
            Batch batch = Batch();
            Insert insert;

            #region TO_PI

            if (li.GetType() == typeof(MEInterfaceToDatabase))
            {
                MEInterfaceToDatabase mi = (MEInterfaceToDatabase)li;

                string neighborPEName = null;
                string neighborPEPart = null;
                List<Tuple<string, string, string, string, string, string>> currentNeighborPEInterfaces = null;

                lock (NecrowVirtualization.PESync)
                {
                    foreach (Tuple<string, List<Tuple<string, string, string, string, string, string>>> pe in NecrowVirtualization.PEPhysicalInterfaces)
                    {
                        neighborPEName = pe.Item1;
                        currentNeighborPEInterfaces = pe.Item2;
                        neighborPEPart = FindNeighborPart(description, neighborPEName);
                        if (neighborPEPart != null) break;
                    }
                    if (neighborPEPart == null)
                    {
                        foreach (Tuple<string, List<Tuple<string, string, string, string, string, string>>> pe in NecrowVirtualization.PEPhysicalInterfaces)
                        {
                            neighborPEName = pe.Item1;
                            currentNeighborPEInterfaces = pe.Item2;
                            neighborPEPart = FindNeighborPartUsingAlias(description, neighborPEName);
                            if (neighborPEPart != null) break;
                        }
                    }
                }
                if (neighborPEPart != null)
                {
                    Tuple<string, string, string, string, string, string> matchedInterface = null;

                    #region Find Interface

                    int leftMostFinding = neighborPEPart.Length;

                    foreach (Tuple<string, string, string, string, string, string> currentNeighborInterface in currentNeighborPEInterfaces)
                    {
                        string neighborInterfaceName = currentNeighborInterface.Item1;
                        string neighborInterfaceType = currentNeighborInterface.Item4;
                        string neighborInterfacePort = neighborInterfaceName.Substring(2);

                        foreach (string test in GenerateTestInterface(neighborInterfaceType, neighborInterfacePort))
                        {
                            int pos = neighborPEPart.IndexOf(test);

                            if (pos > -1 && pos < leftMostFinding)
                            {
                                leftMostFinding = pos;
                                matchedInterface = currentNeighborInterface;
                            }
                        }
                    }

                    #endregion

                    if (matchedInterface != null)
                    {
                        #region Crosscheck with matched interface description

                        string matchedDescription = CleanDescription(matchedInterface.Item2, neighborPEName);

                        if (matchedDescription != null)
                        {
                            string matchedNeighborPart = FindNeighborPart(matchedDescription, nodeName);

                            if (matchedNeighborPart != null) // at least we can find me name or the alias in pi description
                            {
                                foreach (string test in GenerateTestInterface(interfaceType, interfacePort))
                                {
                                    if (matchedNeighborPart.IndexOf(test) > -1)
                                    {
                                        mi.TopologyPEInterfaceID = matchedInterface.Item3;
                                        mi.NeighborCheckPITOMI = matchedInterface.Item6;

                                        // anak agregator ga mgkn punya anak sendiri
                                        // daftar parentnya juga untuk ditangkap di aggr pencari anak
                                        if (li.Aggr != -1) li.AggrNeighborParentID = matchedInterface.Item5;
                                        else
                                        {
                                            // find pi child
                                            li.ChildrenNeighbor = new Dictionary<int, Tuple<string, string, string>>();
                                            result = Query("select PI_ID, PI_DOT1Q, PI_TO_MI from PEInterface where PI_PI = {0}", mi.TopologyPEInterfaceID);
                                            foreach (Row row in result)
                                            {
                                                if (!row["PI_DOT1Q"].IsNull)
                                                {
                                                    int dot1q = row["PI_DOT1Q"].ToIntShort();
                                                    if (!li.ChildrenNeighbor.ContainsKey(dot1q)) li.ChildrenNeighbor.Add(dot1q, 
                                                        new Tuple<string, string, string>(row["PI_ID"].ToString(), row["PI_TO_MI"].ToString(), null));
                                                }
                                            }
                                        }

                                        break;
                                    }
                                }
                            }
                        }

                        #endregion
                    }

                    done = true;
                }

                if (done) return;
            }

            #endregion

            #region TO_MI

            string neighborMEName = null;
            string neighborMEPart = null;
            List<Tuple<string, string, string, string, string, string, string>> currentNeighborMEInterfaces = null;

            lock (NecrowVirtualization.MESync)
            {
                foreach (Tuple<string, List<Tuple<string, string, string, string, string, string, string>>> me in NecrowVirtualization.MEPhysicalInterfaces)
                {
                    neighborMEName = me.Item1;
                    currentNeighborMEInterfaces = me.Item2;
                    neighborMEPart = FindNeighborPart(description, neighborMEName);
                    if (neighborMEPart != null) break;
                }
                if (neighborMEPart == null)
                {
                    foreach (Tuple<string, List<Tuple<string, string, string, string, string, string, string>>> me in NecrowVirtualization.MEPhysicalInterfaces)
                    {
                        neighborMEName = me.Item1;
                        currentNeighborMEInterfaces = me.Item2;
                        neighborMEPart = FindNeighborPartUsingAlias(description, neighborMEName);
                        if (neighborMEPart != null) break;
                    }
                }
            }
            if (neighborMEPart != null)
            {
                Tuple<string, string, string, string, string, string, string> matchedInterface = null;

                #region Find Interface

                int leftMostFinding = neighborMEPart.Length;

                foreach (Tuple<string, string, string, string, string, string, string> currentNeighborInterface in currentNeighborMEInterfaces)
                {
                    string neighborInterfaceName = currentNeighborInterface.Item1;
                    string neighborInterfaceType = currentNeighborInterface.Item4;
                    string neighborInterfacePort = neighborInterfaceName.Substring(2);

                    foreach (string test in GenerateTestInterface(neighborInterfaceType, neighborInterfacePort))
                    {
                        int pos = neighborMEPart.IndexOf(test);

                        if (pos > -1 && pos < leftMostFinding)
                        {
                            leftMostFinding = pos;
                            matchedInterface = currentNeighborInterface;
                        }
                    }
                }

                #endregion

                if (matchedInterface != null)
                {
                    #region Crosscheck with matched interface description

                    string matchedDescription = CleanDescription(matchedInterface.Item2, neighborMEName);

                    if (matchedDescription != null)
                    {
                        string matchedNeighborPart = FindNeighborPart(matchedDescription, nodeName);

                        if (matchedNeighborPart != null)
                        {
                            foreach (string test in GenerateTestInterface(interfaceType, interfacePort))
                            {
                                if (matchedNeighborPart.IndexOf(test) > -1)
                                {
                                    li.TopologyMEInterfaceID = matchedInterface.Item3;
                                    if (li.GetType() == typeof(PEInterfaceToDatabase))
                                        ((PEInterfaceToDatabase)li).NeighborCheckMITOPI = matchedInterface.Item7;
                                    else
                                        ((MEInterfaceToDatabase)li).NeighborCheckMITOMI = matchedInterface.Item6;

                                    // anak agregator ga mgkn punya anak sendiri
                                    // daftar parentnya juga untuk ditangkap di aggr pencari anak
                                    if (li.Aggr != -1) li.AggrNeighborParentID = matchedInterface.Item5;
                                    else
                                    {
                                        // find mi child
                                        li.ChildrenNeighbor = new Dictionary<int, Tuple<string, string, string>>();
                                        result = Query("select MI_ID, MI_DOT1Q, MI_TO_MI, MI_TO_PI from MEInterface where MI_MI = {0}", li.TopologyMEInterfaceID);
                                        foreach (Row row in result)
                                        {
                                            if (!row["MI_DOT1Q"].IsNull)
                                            {
                                                int dot1q = row["MI_DOT1Q"].ToIntShort();
                                                if (!li.ChildrenNeighbor.ContainsKey(dot1q)) li.ChildrenNeighbor.Add(dot1q, 
                                                    new Tuple<string, string, string>(row["MI_ID"].ToString(), row["MI_TO_MI"].ToString(), row["MI_TO_PI"].ToString()));
                                            }
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }

                    #endregion
                }

                done = true;
            }

            if (done) return;

            #endregion

            #region TO_NI

            string findNeighborNode = null;
            string findNeighborPart = null;

            lock (NecrowVirtualization.NNSync)
            {
                foreach (Tuple<string, List<Tuple<string, string>>> nn in NecrowVirtualization.NNPhysicalInterfaces)
                {
                    string neighborName = nn.Item1;
                    List<Tuple<string, string>> currentNeighborInterfaces = nn.Item2;

                    string neighborPart = FindNeighborPart(description, neighborName);

                    if (neighborPart != null)
                    {
                        findNeighborNode = neighborName;
                        findNeighborPart = neighborPart;

                        Tuple<string, string> matchedInterface = null;

                        #region Find Interface

                        int leftMostFinding = neighborPart.Length;

                        foreach (Tuple<string, string> currentNeighborInterface in currentNeighborInterfaces)
                        {
                            string neighborInterfaceName = currentNeighborInterface.Item1;

                            foreach (string test in GenerateTestInterface(null, neighborInterfaceName))
                            {
                                int pos = neighborPart.IndexOf(test);

                                if (pos > -1 && pos < leftMostFinding)
                                {
                                    leftMostFinding = pos;
                                    matchedInterface = currentNeighborInterface;
                                }
                            }
                        }

                        #endregion

                        if (matchedInterface != null)
                        {
                            li.TopologyNeighborInterfaceID = matchedInterface.Item2;
                            done = true;
                        }

                        break;
                    }
                }
            }

            if (!done)
            {
                if (findNeighborNode == null)
                {
                    // find neighbor node in description
                    findNeighborNode = FindNode(description, out findNeighborPart);
                }

                if (findNeighborNode != null)
                {
                    string neighborNodeID = null;

                    if (NecrowVirtualization.NodeNeighbors.ContainsKey(findNeighborNode))
                    {
                        neighborNodeID = NecrowVirtualization.NodeNeighbors[findNeighborNode];
                    }
                    else
                    {
                        batch.Begin();

                        // insert neighbor node
                        neighborNodeID = Database.ID();
                        insert = Insert("NodeNeighbor");
                        insert.Value("NN_ID", neighborNodeID);
                        insert.Value("NN_Name", findNeighborNode);
                        batch.Execute(insert);

                        // insert neighbor interface unspecified
                        string unspecifiedID = Database.ID();
                        insert = Insert("NeighborInterface");
                        insert.Value("NI_ID", unspecifiedID);
                        insert.Value("NI_NN", neighborNodeID);
                        insert.Value("NI_Name", "UNSPECIFIED");
                        batch.Execute(insert);

                        batch.Commit();

                        // tambah ke collection neighbors
                        lock (NecrowVirtualization.NNSync)
                        {
                            NecrowVirtualization.NodeNeighbors.Add(findNeighborNode, neighborNodeID);
                            NecrowVirtualization.NNPhysicalInterfaces.Add(new Tuple<string, List<Tuple<string, string>>>(findNeighborNode, new List<Tuple<string, string>>()));
                            NecrowVirtualization.NNUnspecifiedInterfaces.Add(findNeighborNode, unspecifiedID);

                            NecrowVirtualization.NNPhysicalInterfaces.Sort((a, b) => b.Item1.Length.CompareTo(a.Item1.Length));
                        }
                    }

                    // find interface
                    string neighborInterface = FindInterface(findNeighborPart);

                    if (neighborInterface == null)
                    {
                        // pake unspecified
                        li.TopologyNeighborInterfaceID = NecrowVirtualization.NNUnspecifiedInterfaces[findNeighborNode];
                    }
                    else
                    {
                        List<Tuple<string, string>> interfaces = null;

                        lock (NecrowVirtualization.NNSync)
                        {
                            foreach (Tuple<string, List<Tuple<string, string>>> tuple in NecrowVirtualization.NNPhysicalInterfaces)
                            {
                                if (tuple.Item1 == findNeighborNode)
                                {
                                    interfaces = tuple.Item2;
                                    break;
                                }
                            }
                        }

                        bool exists = false;
                        foreach (Tuple<string, string> ni in interfaces)
                        {
                            if (ni.Item1 == neighborInterface)
                            {
                                exists = true;
                                li.TopologyNeighborInterfaceID = ni.Item2;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            // new interface under neighborNode
                            string interfaceID = Database.ID();
                            insert = Insert("NeighborInterface");
                            insert.Value("NI_ID", interfaceID);
                            insert.Value("NI_NN", neighborNodeID);
                            insert.Value("NI_Name", neighborInterface);
                            insert.Execute();

                            li.TopologyNeighborInterfaceID = interfaceID;
                            interfaces.Add(new Tuple<string, string>(neighborInterface, interfaceID));
                            
                            interfaces.Sort((a, b) => b.Item1.Length.CompareTo(a.Item1.Length));
                        }
                    }
                }
            }

            #endregion
        }

        private readonly char[] cleanDescriptionAllowedCharacterForTrimStart = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private readonly char[] cleanDescriptionAllowedCharacterForTrimEnd = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        private readonly string[] cleanDescriptionStartsWith = { "EKS ", "EKS.", "EXS ", "EXS. ", "EX ", "EX-", "BEKAS ", "MIGRASI", "MIGRATED", "PINDAH", "GANTI" };
        private readonly string[] cleanDescriptionUnnecessaryInfo = { "(EX", "(EKS", "[EX", "[EKS", " EX-", " EKS-", " EXS.", " EKS.", " BEKAS ", " PINDAHAN ", " MOVE " };

        private readonly Regex findNodeRegex = new Regex(@"^(?:(?:T)\d{0,2}-D[1-7]|(?:GPON|MSAN|DSL(?:AM)?|ME|PE|SW(?:C)?|BRAS|DCN|SBC|HRB|WAC|WAG)\d{0,2})(?:-(?:\d[A-Z\d]+|[A-Z][A-Z\d]*)){1,4}$");
        private readonly Regex findInterfaceRegex = new Regex(@"^(?:(?:\/)*(?:(?:F(?:A(?:ST)?)?|(?:(?:TE(?:NGIG(?:ABIT)?)?|HU(?:NDRED)?){0,1}(?:G(?:I(?:GABIT)?)?)?)){0,1}(?:E(?:T(?:HERNET)?)?)?|XE)?(?:\/|-)*(?:[0-9]{1,2})(?:\/[0-9]{1,2}){1,3}|PKT[0-9])$");
        private readonly Regex captureNodeTypeNumberLocation = new Regex(@"^(ME|PE)(\d)-D[1-7]-([A-Z]{3})$");

        private string CleanDescription(string description, string originNodeName)
        {
            // ends if null or empty
            if (string.IsNullOrEmpty(description)) return null;

            // make all description upper case, remove leading and trailing characters except allowed characters, and replace underscores with space
            description = description.ToUpper().TrimStartExcept(cleanDescriptionAllowedCharacterForTrimStart).TrimEndExcept(cleanDescriptionAllowedCharacterForTrimEnd).Replace('_', ' ');

            // ends if migrated or moved somewhere elese
            if (description.StartsWith(cleanDescriptionStartsWith)) return null;

            // removes unnecessary info after specified point
            int unid = description.IndexOf(cleanDescriptionUnnecessaryInfo);
            if (unid > -1) description = description.Remove(unid);

            // removes current node name
            description = description.Replace(originNodeName, "");

            return description;
        }

        private string FindNode(string description, out string nodePart)
        {
            string[] splits = description.Split(new string[] {
                " ", "(", ")", "_", "[", "]", ";", ".", "=", ":", "@", "/", "\\",
                " L2-", " TO-", "-SID-", "-SOID-", "-TENOSS-", "-SID4", "-SOID4", "-TO-", "-IP-",
                "-PORT",
                "-GE-", "-GI-", "-TE-", "-XE-", "-FA-",
                "-GI0", "-GI1", "-GI2", "-GI3", "-GI4", "-GI5", "-GI6", "-GI7", "-GI8", "-GI9",
                "-GE0", "-GE1", "-GE2", "-GE3", "-GE4", "-GE5", "-GE6", "-GE7", "-GE8", "-GE9",
                "-TE0", "-TE1", "-TE2", "-TE3", "-TE4", "-TE5", "-TE6", "-TE7", "-TE8", "-TE9",
                "-XE0", "-XE1", "-XE2", "-XE3", "-XE4", "-XE5", "-XE6", "-XE7", "-XE8", "-XE9",
                "-FA0", "-FA1", "-FA2", "-FA3", "-FA4", "-FA5", "-FA6", "-FA7", "-FA8", "-FA9",
            }, StringSplitOptions.RemoveEmptyEntries);

            nodePart = null;

            foreach (string split in splits)
            {
                if (findNodeRegex.IsMatch(split))
                {
                    int isp = description.IndexOf(split);
                    nodePart = description.Substring(isp + split.Length, description.Length - isp - split.Length);
                    return split;
                }
                else
                {
                    string[] splitstrip = split.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    
                    StringBuilder nsplit = new StringBuilder();

                    bool first = true, anumber = false;
                    foreach (string split2s in splitstrip)
                    {
                        if (!first && !anumber)
                        {
                            nsplit.Append("-");
                            anumber = false;
                        }

                        int spliti;
                        if (int.TryParse(split2s, out spliti)) anumber = true;

                        nsplit.Append(split2s);

                        first = false;
                    }
                    string split2 = nsplit.ToString();

                    if (findNodeRegex.IsMatch(split2))
                    {
                        int isp = description.IndexOf(split);
                        nodePart = description.Substring(isp + split.Length, description.Length - isp - split.Length);
                        return split2;
                    }
                    else
                    {
                        if (anumber && split2.IndexOf('-') > -1) // so its ended with number
                        {
                            // remove last strip and join with the previous token
                            string split3 = split2.Substring(0, split2.LastIndexOf('-')) + split2.Substring(split2.LastIndexOf('-') + 1);

                            if (findNodeRegex.IsMatch(split3))
                            {
                                int isp = description.IndexOf(split);
                                nodePart = description.Substring(isp + split.Length, description.Length - isp - split.Length);
                                return split3;
                            }
                        }
                    }
                }
            }

            return null;
        }

        private string FindInterface(string description)
        {
            string[] splits = description.Split(new string[] {
                " ", "(", ")", "_", "[", "]", ";", "=", ":", "@", "\\"
            }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string split in splits)
            {
                if (findInterfaceRegex.IsMatch(split))
                {
                    string iclean = split.TrimStart(new char[] { '/' }).Replace("-", "").Replace("XE", "TE");

                    int ic = 0;
                    foreach (char c in iclean)
                    {
                        if (char.IsDigit(c)) break;
                        ic++;
                    }

                    if (ic == 3 & iclean.StartsWith("PKT")) return iclean;
                    
                    string port = iclean.Substring(ic);
                    string utype = iclean.Substring(0, 1);

                    string gtype;
                    if (utype == "H") gtype = "Hu";
                    else if (utype == "T") gtype = "Te";
                    else if (utype == "G") gtype = "Gi";
                    else if (utype == "F") gtype = "Fa";
                    else if (utype == "E") gtype = "Et";
                    else gtype = "Ex";

                    return gtype + port;
                }
            }

            return null;
        }
    }

    internal class ServiceReference
    {
        #region Fields

        private List<Tuple<ServiceBaseToDatabase, ServiceMapping>> mappings;

        public List<Tuple<ServiceBaseToDatabase, ServiceMapping>> Mappings
        {
            get { return mappings; }
        }

        #endregion

        #region Constructors

        public ServiceReference()
        {
            mappings = new List<Tuple<ServiceBaseToDatabase, ServiceMapping>>();
        }

        #endregion

        #region Methods

        public void Add(ServiceBaseToDatabase reference, string description)
        {
            ServiceMapping servmap = ServiceMapping.Parse(description);
            if (servmap.SID != null)
                mappings.Add(new Tuple<ServiceBaseToDatabase, ServiceMapping>(reference, servmap));
        }

        #endregion
    }

    internal class ServiceMapping
    {
        #region Constants

        private static string[] monthsEnglish = new string[] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };
        private static string[] monthsBahasa = new string[] { "JANUARI", "FEBRUARI", "MARET", "APRIL", "MEI", "JUNI", "JULI", "AGUSTUS", "SEPTEMBER", "OKTOBER", "NOVEMBER", "DESEMBER" };

        #endregion

        #region Properties

        private string sid;

        public string SID
        {
            get { return sid; }
            private set { sid = value; }
        }

        private string cid;

        public string CID
        {
            get { return cid; }
            set { cid = value; }
        }

        private string serviceType;

        public string ServiceType
        {
            get { return serviceType; }
            private set { serviceType = value; }
        }

        private string serviceSubType;

        public string ServiceSubType
        {
            get { return serviceSubType; }
            set { serviceSubType = value; }
        }

        private string cleanDescription;

        public string CleanDescription
        {
            get { return cleanDescription; }
            private set { cleanDescription = value; }
        }

        private string rawDescription;

        public string RawDescription
        {
            get { return rawDescription; }
            private set { rawDescription = value; }
        }

        #endregion

        #region Methods

        public static ServiceMapping Parse(string desc)
        {
            ServiceMapping de = new ServiceMapping();
            de.RawDescription = desc;

            string[] s = desc.Split(new char[] { '_', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string d = string.Join(" ", s).ToUpper();

            int rmv = -1;
            int rle = -1;

            //                         12345678901234567890
            if ((rmv = d.IndexOf("MM IPVPN INSTAN ")) > -1) { de.ServiceType = "VPNINSTAN"; rle = 15; }
            else if ((rmv = d.IndexOf("MM VPNIP INSTAN ")) > -1) { de.ServiceType = "VPNINSTAN"; rle = 15; }
            else if ((rmv = d.IndexOf("VPNIP INSTANT ")) > -1) { de.ServiceType = "VPNINSTAN"; rle = 13; }
            else if ((rmv = d.IndexOf("IPVPN INSTANT ")) > -1) { de.ServiceType = "VPNINSTAN"; rle = 13; }
            else if ((rmv = d.IndexOf("VPNIP INSTAN ")) > -1) { de.ServiceType = "VPNINSTAN"; rle = 12; }
            else if ((rmv = d.IndexOf("IPVPN INSTAN ")) > -1) { de.ServiceType = "VPNINSTAN"; rle = 12; }
            else if ((rmv = d.IndexOf("VPNIP VPN IP ")) > -1) { de.ServiceType = "VPNIP"; rle = 12; }
            else if ((rmv = d.IndexOf("VPNIP VPNIP ")) > -1) { de.ServiceType = "VPNIP"; rle = 11; }
            else if ((rmv = d.IndexOf("VPN INSTAN ")) > -1) { de.ServiceType = "VPNINSTAN"; rle = 10; }
            else if ((rmv = d.IndexOf("MM IPVPN ")) > -1) { de.ServiceType = "VPNIP"; rle = 8; }
            else if ((rmv = d.IndexOf("MM VPNIP ")) > -1) { de.ServiceType = "VPNIP"; rle = 8; }
            else if ((rmv = d.IndexOf("MM VPNIP ")) > -1) { de.ServiceType = "VPNIP"; rle = 8; }
            else if ((rmv = d.IndexOf("MM VPNIP ")) > -1) { de.ServiceType = "VPNIP"; rle = 8; }
            else if ((rmv = d.IndexOf("VPN IP ")) > -1) { de.ServiceType = "VPNIP"; rle = 6; }
            else if ((rmv = d.IndexOf("IP VPN ")) > -1) { de.ServiceType = "VPNIP"; rle = 6; }
            else if ((rmv = d.IndexOf("VPNIP ")) > -1) { de.ServiceType = "VPNIP"; rle = 5; }
            else if ((rmv = d.IndexOf("IPVPN ")) > -1) { de.ServiceType = "VPNIP"; rle = 5; }
            //                         12345678901234567890
            else if ((rmv = d.IndexOf("MM ASTINET BEDA BW ")) > -1) { de.ServiceType = "ASTINETBB"; rle = 18; }
            else if ((rmv = d.IndexOf("MM ASTINET BEDABW ")) > -1) { de.ServiceType = "ASTINETBB"; rle = 17; }
            else if ((rmv = d.IndexOf("ASTINET BEDA BW ")) > -1) { de.ServiceType = "ASTINETBB"; rle = 15; }
            else if ((rmv = d.IndexOf("ASTINET BEDABW ")) > -1) { de.ServiceType = "ASTINETBB"; rle = 14; }
            else if ((rmv = d.IndexOf("MM ASTINET BB ")) > -1) { de.ServiceType = "ASTINETBB"; rle = 13; }
            else if ((rmv = d.IndexOf("ASTINET BB ")) > -1) { de.ServiceType = "ASTINETBB"; rle = 10; }
            else if ((rmv = d.IndexOf("ASTINETBB ")) > -1) { de.ServiceType = "ASTINETBB"; rle = 9; }
            else if ((rmv = d.IndexOf("AST BEDA BW ")) > -1) { de.ServiceType = "ASTINETBB"; rle = 11; }
            else if ((rmv = d.IndexOf("AST BEDABW ")) > -1) { de.ServiceType = "ASTINETBB"; rle = 10; }
            else if ((rmv = d.IndexOf("AST BB ")) > -1) { de.ServiceType = "ASTINETBB"; rle = 7; }
            //                         12345678901234567890
            else if ((rmv = d.IndexOf("MM ASTINET ")) > -1) { de.ServiceType = "ASTINET"; rle = 10; }
            else if ((rmv = d.IndexOf("ASTINET ")) > -1) { de.ServiceType = "ASTINET"; rle = 7; }
            //                         12345678901234567890
            else rmv = -1;

            if (rmv > -1) d = d.Remove(rmv, rle);
            rmv = -1;
            rle = -1;

            if (de.ServiceType == null || de.ServiceType == "VPNIP")
            {
                //                         12345678901234567890
                if ((rmv = d.IndexOf("VPNIP TRANS ACCESS ")) > -1) { de.ServiceType = "VPNIP"; rle = 18; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRANSACTIONAL ACCESS ")) > -1) { de.ServiceType = "VPNIP"; rle = 20; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("MM TRANS ACCESS ")) > -1) { de.ServiceType = "VPNIP"; rle = 15; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRANSS ACCESS ")) > -1) { de.ServiceType = "VPNIP"; rle = 13; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRANS ACCESS ")) > -1) { de.ServiceType = "VPNIP"; rle = 12; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRANSACCESS ")) > -1) { de.ServiceType = "VPNIP"; rle = 11; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRANS ACCES ")) > -1) { de.ServiceType = "VPNIP"; rle = 11; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRANSACC ")) > -1) { de.ServiceType = "VPNIP"; rle = 8; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRAN ACC ")) > -1) { de.ServiceType = "VPNIP"; rle = 8; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRANS AC ")) > -1) { de.ServiceType = "VPNIP"; rle = 8; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRANSAC ")) > -1) { de.ServiceType = "VPNIP"; rle = 7; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRANSC ")) > -1) { de.ServiceType = "VPNIP"; rle = 6; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRANSS ")) > -1) { de.ServiceType = "VPNIP"; rle = 6; de.ServiceSubType = "TRANS"; }
                else if ((rmv = d.IndexOf("TRANS ")) > -1 && de.ServiceType == null) { de.ServiceType = "VPNIP"; rle = 5; de.ServiceSubType = "TRANS"; }
                else rmv = -1;

                if (rmv > -1) d = d.Remove(rmv, rle);
                rmv = -1;
                rle = -1;
            }

            rmv = -1;
            rle = -1;

            d = d.Trim();

            //                         12345678901234567890
            if ((rmv = d.IndexOf("(EX SID FEAS")) > -1) { rle = 12; }
            else if ((rmv = d.IndexOf("[EX SID FEAS")) > -1) { rle = 12; }
            else if ((rmv = d.IndexOf("EX SID FEAS")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("EX SID FEAS")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("SID (FEAS)")) > -1) { rle = 10; }
            else if ((rmv = d.IndexOf("SID [FEAS]")) > -1) { rle = 10; }
            else if ((rmv = d.IndexOf("SID <FEAS>")) > -1) { rle = 10; }
            else if ((rmv = d.IndexOf("(SID FEAS")) > -1) { rle = 9; }
            else if ((rmv = d.IndexOf("[SID FEAS")) > -1) { rle = 9; }
            else if ((rmv = d.IndexOf("SID FEAS")) > -1) { rle = 8; }
            else if ((rmv = d.IndexOf("(EX FEAS")) > -1) { rle = 8; }
            else if ((rmv = d.IndexOf("[EX FEAS")) > -1) { rle = 8; }
            else if ((rmv = d.IndexOf("(EX SID")) > -1) { rle = 7; }
            else if ((rmv = d.IndexOf("[EX SID")) > -1) { rle = 7; }
            else if ((rmv = d.IndexOf("EX FEAS")) > -1) { rle = 7; }
            else if ((rmv = d.IndexOf("EX-SID")) > -1) { rle = 6; }
            else if ((rmv = d.IndexOf("EX SID")) > -1) { rle = 6; }
            else if ((rmv = d.IndexOf("X-SID")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("X SID")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("EXSID")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("XSID3")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf("XSID4")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf("XSID ")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf("FEAS ")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf("VLAN ")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf(" EX3")) > -1) { rle = 3; }
            else if ((rmv = d.IndexOf(" EX4")) > -1) { rle = 3; }
            else if ((rmv = d.IndexOf("(EX3")) > -1) { rle = 3; }
            else if ((rmv = d.IndexOf("(EX4")) > -1) { rle = 3; }
            else if ((rmv = d.IndexOf("[EX3")) > -1) { rle = 3; }
            else if ((rmv = d.IndexOf("[EX4")) > -1) { rle = 3; }
            else if ((rmv = d.IndexOf("<EX3")) > -1) { rle = 3; }
            else if ((rmv = d.IndexOf("<EX4")) > -1) { rle = 3; }

            if (rmv > -1)
            {
                int rmvn = rmv + rle;
                if (rmvn < d.Length)
                {
                    if (d[rmvn] == ' ') rmvn += 1;
                    else if (d[rmvn] == ':' || d[rmvn] == '-' || d[rmvn] == '=')
                    {
                        rmvn += 1;
                        if (rmvn < d.Length && d[rmvn] == ' ') rmvn += 1;
                    }
                }
                if (rmvn < d.Length)
                {
                    int end = d.IndexOfAny(new char[] { ' ', ')', '(', ']', '[', '.', '<', '>' }, rmvn);
                    if (end > -1) d = d.Remove(rmv, end - rmv);
                    else d = d.Remove(rmv);
                }
            }
            rmv = -1;
            rle = -1;

            //                         12345678901234567890
            if ((rmv = d.IndexOf("SID-TENOSS-")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("SID-TENOSS:")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("SID-TENOSS=")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("SID-TENOSS ")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("SID TENOSS:")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("SID TENOSS=")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("SID TENOSS ")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("TENOSS-SID-")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("TENOSS-SID:")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("TENOSS-SID=")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("TENOSS-SID ")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("TENOSS SID:")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("TENOSS SID=")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("TENOSS SID ")) > -1) { rle = 11; }
            else if ((rmv = d.IndexOf("SID SID ")) > -1) { rle = 7; }
            else if ((rmv = d.IndexOf("-SOID-")) > -1) { rle = 6; }
            else if ((rmv = d.IndexOf("(SID-")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("(SID:")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("(SID=")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("(SID%")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("(SID ")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("<SID-")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("<SID:")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("<SID=")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("<SID%")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("<SID ")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("[SID-")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("[SID:")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("[SID=")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("[SID%")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf("[SID ")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf(" SID-")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf(" SID:")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf(" SID=")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf(" SID%")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf(" SID ")) > -1) { rle = 5; }
            else if ((rmv = d.IndexOf(" SIDT")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf(" SID0")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf(" SID1")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf(" SID2")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf(" SID3")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf(" SID4")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf(" SID5")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf(" SID6")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf(" SID7")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf(" SID8")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf(" SID9")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf("SID-")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf("SID:")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf("SID=")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf("SID%")) > -1) { rle = 4; }
            else if ((rmv = d.IndexOf("SID ")) > -1) { rle = 4; }
            else rmv = -1;

            if (rmv > -1)
            {
                int rmvn = rmv + rle;
                if (rmvn < d.Length)
                {
                    if (d[rmvn] == ' ') rmvn += 1;
                    else if (d[rmvn] == ':' || d[rmvn] == '-' || d[rmvn] == '=' || d[rmvn] == '(' || d[rmvn] == '[')
                    {
                        rmvn += 1;
                        if (rmvn < d.Length && d[rmvn] == ' ') rmvn += 1;
                    }
                }
                if (rmvn < d.Length)
                {
                    int end = -1;
                    int nextend = rmvn;

                    while (true)
                    {

                        end = d.IndexOfAny(new char[] { ' ', ')', '(', ']', '[', '.', '<', '>' }, nextend);
                        if (end > -1 && end < d.Length && d[end] == ' ' && end - rmvn <= 8) nextend = end + 1;
                        else break;
                    }

                    if (end > -1)
                    {
                        int len = end - rmv - rle;
                        if (len + rmvn > d.Length) de.SID = d.Substring(rmvn).Trim();
                        else de.SID = d.Substring(rmvn, len).Trim();
                        d = d.Remove(rmv, end - rmv);
                    }
                    else
                    {
                        string imx = d.Substring(rmvn).Trim();
                        imx = imx.Replace(' ', '_');

                        if (imx.Length > 13)
                        {
                            StringBuilder nimx = new StringBuilder();
                            nimx.Append(imx.Substring(0, 13));
                            for (int imxi = 13; imxi < imx.Length; imxi++)
                            {
                                if (char.IsDigit(imx[imxi])) nimx.Append(imx[imxi]);
                                else break;
                            }

                            imx = nimx.ToString();
                        }

                        de.SID = imx;
                        d = d.Remove(rmv);
                    }
                }

                if (de.SID != null)
                {
                    int weirdc = de.SID.IndexOfAny(new char[] { ' ' });

                    if (weirdc > -1) de.SID = null;

                }
            }

            if (de.SID == null)
            {
                string[] ss = d.Split(new char[] { ' ', ':', '=' });

                List<string> sidc = new List<string>();
                foreach (string si in ss)
                {
                    int dig = 0;

                    string fsi = si.Trim(new char[] { '-', ')', '(', '[', ']', '>', '<' });


                    // count digit in si
                    foreach (char ci in fsi)
                        if (char.IsDigit(ci))
                            dig++;

                    double oc = (double)dig / (double)fsi.Length;

                    if (oc > 0.3 && fsi.Length > 8 &&
                        !fsi.StartsWith("FAA-") &&
                        !fsi.StartsWith("FAI-") &&
                        !fsi.StartsWith("FAD-") &&
                        !fsi.StartsWith("GI") &&
                        !fsi.StartsWith("TE") &&
                        !fsi.StartsWith("FA") &&
                        fsi.IndexOf("GBPS") == -1 &&
                        fsi.IndexOf("KBPS") == -1 &&
                        fsi.IndexOf("MBPS") == -1 &&
                        fsi.IndexOf("BPS") == -1 &&
                        fsi.IndexOfAny(new char[] { '/', '.', ';', '\'', '\"', '>', '<', '/' }) == -1)
                    {
                        int imin = fsi.LastIndexOf('-');

                        if (imin > -1)
                        {
                            string lastport = fsi.Substring(imin + 1);

                            if (lastport.Length < 5) fsi = null;
                            else
                            {
                                bool adadigit = false;
                                foreach (char lastportc in lastport)
                                {
                                    if (char.IsDigit(lastportc))
                                    {
                                        adadigit = true;
                                        break;
                                    }
                                }

                                if (adadigit == false)
                                    fsi = null;
                            }
                        }

                        if (fsi != null)
                        {
                            if (fsi.Length >= 6 && fsi.Length <= 16)
                            {
                                bool isDate = true;

                                string[] fsip = fsi.Split(new char[] { '-' });
                                if (fsip.Length == 3)
                                {
                                    string first = fsip[0];
                                    if (char.IsDigit(first[0]))
                                    {
                                        if (first.Length == 1 || first.Length == 2 && char.IsDigit(first[1])) { }
                                        else isDate = false;
                                    }
                                    if (isDate && !char.IsDigit(first[0]))
                                    {
                                        if (first.Length >= 3 && (
                                            List.StartsWith(monthsEnglish, first) > -1 ||
                                            List.StartsWith(monthsBahasa, first) > -1
                                            ))
                                        { }
                                        else isDate = false;
                                    }
                                    string second = fsip[1];
                                    if (isDate && char.IsDigit(second[0]))
                                    {
                                        if (second.Length == 1 || second.Length == 2 && char.IsDigit(second[1])) { }
                                        else isDate = false;
                                    }
                                    if (isDate && !char.IsDigit(second[0]))
                                    {
                                        if (second.Length >= 3 && (
                                            List.StartsWith(monthsEnglish, second) > -1 ||
                                            List.StartsWith(monthsBahasa, second) > -1
                                            ))
                                        { }
                                        else isDate = false;
                                    }
                                    string third = fsip[2];
                                    if (isDate && char.IsDigit(second[0]))
                                    {
                                        if (third.Length == 2 && char.IsDigit(third[1])) { }
                                        else if (third.Length == 4 && char.IsDigit(third[1]) && char.IsDigit(third[2]) && char.IsDigit(third[3])) { }
                                        else isDate = false;
                                    }
                                    else isDate = false;
                                }
                                else if (fsip.Length == 1)
                                {
                                    // 04APR2014
                                    // APR42014
                                    // 4APR2014
                                    // 04042014

                                    if (char.IsDigit(fsi[0]))
                                    {

                                    }
                                    else
                                    {
                                        int tlen = 1;
                                        for (int fi = 1; fi < fsi.Length; fi++)
                                        {
                                            if (char.IsDigit(fsi[fi])) break;
                                            else tlen++;
                                        }

                                        string t = fsi.Substring(0, tlen);

                                        if (List.StartsWith(monthsEnglish, t) > -1 ||
                                            List.StartsWith(monthsBahasa, t) > -1)
                                        { }
                                        else isDate = false;

                                        if (isDate && fsi.Length > tlen)
                                        {
                                            int remaining = fsi.Length - tlen;
                                            if (remaining >= 3 && remaining <= 6)
                                            {
                                                for (int ooi = 0; ooi < remaining; ooi++)
                                                {
                                                    char cc = fsi[tlen + ooi];
                                                    if (!char.IsDigit(cc))
                                                    {
                                                        isDate = false;
                                                        break;
                                                    }
                                                }
                                            }
                                            else isDate = false;
                                        }
                                    }
                                }
                                else isDate = false;

                                if (isDate) fsi = null;
                            }
                        }

                        if (fsi != null)
                            sidc.Add(fsi);
                    }
                }

                if (sidc.Count > 0)
                {
                    sidc.Sort((a, b) => b.Length.CompareTo(a.Length));

                    de.SID = sidc[0];
                    d = d.Remove(d.IndexOf(de.SID), de.SID.Length);
                }
            }

            if (de.SID != null)
            {
                if (de.SID.Length <= 8)
                    de.SID = null;
                else
                {
                    string fixsid = de.SID.Trim(new char[] { '-', ')', '(', '[', ']', '>', '<', '\'', '\"' });
                    fixsid = fixsid.Replace("--", "-");

                    string[] sids = fixsid.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (sids.Length > 0)
                        fixsid = sids[0];

                    de.SID = fixsid;

                    if (StringHelper.Count(de.SID, '-') > 3)
                    {
                        de.SID = null;
                    }

                    if (de.SID != null)
                    {
                        int lmin = de.SID.LastIndexOf('-');
                        if (lmin > -1)
                            de.CID = de.SID.Substring(0, lmin);

                        if (de.CID == null && lmin == -1)
                        {
                            if (de.SID.Length == 12 && de.SID[0] == '4')
                            {
                                bool alldigit = true;
                                foreach (char c in de.SID) { if (!char.IsDigit(c)) { alldigit = false; break; } }
                                if (alldigit)
                                {
                                    de.CID = de.SID.Substring(0, 7);
                                }
                            }
                            if (de.SID.Length == 17 && (de.SID[0] == '4' || de.SID[0] == '3'))
                            {
                                bool alldigit = true;
                                foreach (char c in de.SID) { if (!char.IsDigit(c)) { alldigit = false; break; } }
                                if (alldigit)
                                {
                                    de.CID = de.SID.Substring(0, 7);
                                }
                            }
                        }
                    }
                }
            }

            d = d.Trim();

            // if double, singlekan
            if (d.Length >= 2 && d.Length % 2 == 0)
            {
                int halflen = d.Length / 2;
                string leftside = d.Substring(0, halflen);
                string rightside = d.Substring(halflen, halflen);

                if (leftside == rightside)
                    d = leftside;
            }

            d = d.Replace("()", "");
            d = d.Replace("[]", "");
            d = string.Join(" ", d.Split(StringSplitTypes.Space, StringSplitOptions.RemoveEmptyEntries));

            de.CleanDescription = d;

            return de;
        }

        #endregion
    }
}