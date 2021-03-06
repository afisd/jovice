﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aphysoft.Share;

using System.Text;
using System.Net;
using Jovice;

namespace Center.Providers
{
    public class ServiceSearchMatch : SearchMatch
    {
        #region Constructors

        public ServiceSearchMatch() : base()
        {
            Root("services");
            Root("service");

            Language("oid", "OID");
            Language("order id", "OID");
            Language("sid", "SID");
            Language("service id", "SID");
            Language("services id", "SID");

            Language("customer", "CUSTOMER");
            Language("customers", "CUSTOMER");
            Language("name", "NAME");
            Language("names", "NAME");
            Language("vcid", "VCID");
            Language("vpnip", "VPNIP");
            Language("vpn ip", "VPNIP");
            Language("ipvpn", "VPNIP");
            Language("ip vpn", "VPNIP");
            Language("transaccess", "TRANSACC");
            Language("transacc", "TRANSACC");
            Language("transac", "TRANSACC");
            Language("transaces", "TRANSACC");
            Language("transacess", "TRANSACC");
            Language("astinet", "ASTINET");
            Language("astinetbb", "ASTINETBB");
            Language("astinet bedabandwidth", "ASTINETBB");
            Language("astinet bedabw", "ASTINETBB");
            Language("astinet beda bandwidth", "ASTINETBB");
            Language("astinet different bandwidth", "ASTINETBB");
            Language("astinet multi bandwidth", "ASTINETBB");
            Language("metro-e", "METRO");
            Language("metro-ethernet", "METRO");
            Language("metro ethernet", "METRO");
            Language("metro e", "METRO");
            Language("metro", "METRO");
        }

        #endregion

        #region Methods

        public override void Process(SearchMatchResult matchResult, SearchMatchQuery matchQuery)
        {
            matchResult.Type = "jovice_service";

            Where whereVCID = SearchDescriptor.Build(matchQuery.Descriptors, delegate(SearchDescriptor descriptor)
            {
                SearchConstraints c = descriptor.Constraint;

                return descriptor.Build(delegate(int index, string value)
                {
                    string v = jovice.Escape(value);

                    if (descriptor.Descriptor == "VCID")
                    {
                        if (v.Length >= 4)
                        {
                            if (c == SearchConstraints.StartsWith) return "MC_VCID like '" + v + "%'";
                            else if (c == SearchConstraints.EndsWith) return "MC_VCID like '%" + v + "'";
                            else if (c == SearchConstraints.Like) return "MC_VCID like '%" + v + "%'";
                            else if (c == SearchConstraints.Equal) return "MC_VCID like '" + v + "'";
                        }
                        else return null;
                    }
                    return null;
                });
            });
            Where whereService = SearchDescriptor.Build(matchQuery.Descriptors, delegate (SearchDescriptor descriptor)
            {
                SearchConstraints c = descriptor.Constraint;
                string cv = Search.JoinValues(descriptor.Values);

                return descriptor.Build(delegate (int index, string value)
                {
                    string v = jovice.Escape(value);

                    if (descriptor.Descriptor == "SID")
                    {
                        if (c == SearchConstraints.StartsWith) return $"SI_VID like '{v}%'";
                        else if (c == SearchConstraints.EndsWith) return $"SI_VID like '%{v}'";
                        else if (c == SearchConstraints.Like) return $"SI_VID like '%{v}%'";
                        else if (c == SearchConstraints.Equal) return $"SI_VID like '{v}'";
                    }
                    else if (descriptor.Descriptor == "OID")
                    {
                        if (c == SearchConstraints.StartsWith) return $"SO_OID like '{v}%'";
                        else if (c == SearchConstraints.EndsWith) return $"SO_OID like '%{v}'";
                        else if (c == SearchConstraints.Like) return $"SO_OID like '%{v}%'";
                        else if (c == SearchConstraints.Equal) return $"SO_OID like '{v}'";
                    }
                    else if ((descriptor.SuperDescriptor == "CUSTOMER" && descriptor.Descriptor == "NAME") || descriptor.Descriptor == "CUSTOMER")
                    {
                        if (c == SearchConstraints.StartsWith) return "SC_Name like '" + v + "%'";
                        else if (c == SearchConstraints.EndsWith) return "SC_Name like '%" + v + "'";
                        else if (c == SearchConstraints.Like) return "SC_Name like '%" + v + "%'";
                        else if (c == SearchConstraints.Equal) return "SC_Name like '" + v + "'";
                    }
                    else if (descriptor.Descriptor == "ASTINET")
                    {
                        string res = "SI_Type = 'AS'";
                        if (v.Length > 0) res += " AND SC_Name like '" + v + "'";
                        return res;
                    }
                    else if (descriptor.Descriptor == "ASTINETBB")
                    {
                        string res = "SI_Type = 'AB'";
                        if (v.Length > 0) res += " AND SC_Name like '" + v + "'";
                        return res;
                    }
                    else if (descriptor.Descriptor == "VPNIP")
                    {
                        string res = "SI_Type = 'VP'";
                        if (v.Length > 0) res += " AND SC_Name like '" + v + "'";
                        return res;
                    }
                    else if (descriptor.Descriptor == "TRANSACC")
                    {
                        string res = "SI_Type = 'TA'";
                        if (v.Length > 0) res += " AND SC_Name like '" + v + "'";
                        return res;
                    }
                    else if (descriptor.Descriptor == "METRO")
                    {
                        string res = "SI_Type is null";
                        if (v.Length > 0) res += " AND SC_Name like '" + v + "'";
                        return res;
                    }

                    return null;
                });
            });

            if (whereVCID.Value != null)
            {
                #region VCID exists
                matchResult.QueryCount = @"
select distinct SO_ID
from (select SO_ID
from (
select MC_SI as SO_ID from 
MECircuit" + whereVCID.Format(" where ") + @"
union all
select MI_SI as SO_ID from
MEInterface, MECircuit
where MI_MC = MC_ID" + whereVCID.Format(" and ") + @"
union all
select PI_SI as SO_ID from
PEInterface, MEInterface, MECircuit
where PI_ID = MI_TO_PI and MI_MC = MC_ID" + whereVCID.Format(" and ") + @"
) source where SO_ID is not null
) source, Service
left join ServiceCustomer on SC_ID = SE_O_SC
where SI_ID = SO_ID" + whereService.Format(" and ");

                matchResult.Query = @"
select distinct 

SE_ID, SE_VID, SE_Type, SE_SubType, SE_O_SID, SE_O_OID, SE_O_N_Detail, SE_O_N_AM, SC_Name, SC_AlternateName, SC_N_AccountNumber


from (
select SO_ID, ROW_NUMBER() OVER (order by SO_Rate desc, SO_Bandwidth desc
) AS SO_RN from (
select MC_SE as SO_ID, null as SO_Rate, null as SO_Bandwidth from 
MECircuit" + whereVCID.Format(" where ") + @"
union all
select MI_SE as SO_ID, MI_Rate_Output as SO_Rate, MQ_Bandwidth as SO_Bandwidth from
MECircuit, MEInterface left join MEQOS on MI_MQ_Output = MQ_ID
where MI_MC = MC_ID" + whereVCID.Format(" and ") + @"
union all
select PI_SE as SO_ID, 0 as SO_Rate, 0 as SO_Bandwidth from
PEInterface, MEInterface, MECircuit
where PI_ID = MI_TO_PI and MI_MC = MC_ID" + whereVCID.Format(" and ") + @"
) source where SO_ID is not null
) source, Service
left join ServiceCustomer on SC_ID = SE_SC
where SE_ID = SO_ID" + whereService.Format(" and ");
                #endregion
            }
            else
            {
                #region Service exists
                matchResult.QueryCount = $@"
select distinct SI_ID 
from ServiceImmediate
left join Service on SE_ID = SI_SE
left join ServiceOrder on SO_SE = SE_ID
left join ServiceCustomer on SC_ID = SE_SC{whereService.Format(" where ")}";

                matchResult.Query = $@"
select distinct SI_ID, SI_VID, SE_SID, SE_Detail, SC_Name, SC_AlternateName, SC_AccountNumber, 
case when SI_Type is NULL then SP_Type else SI_Type end as SI_Type, 
SI_SE_Check, SE_ID, SE_LastCheck, SP_Product
from ServiceImmediate
left join Service on SE_ID = SI_SE
left join ServiceOrder on SO_SE = SE_ID
left join ServiceCustomer on SC_ID = SE_SC
left join ServiceProduct on SP_ID = SE_SP{whereService.Format(" where ")}";
                #endregion
            }

            matchResult.RowID = "SI_ID";

            matchResult.Hide("SI_ID");
            matchResult.Hide("SE_ID");

            Topology.Prepare(matchResult);

            matchResult.Sort("SE_SID", "SID");
            matchResult.Sort("SC_Name", "Customer");

            matchResult.AddColumn("StreamServiceID"); // 33
        }

        public override void RowProcess(SearchMatchResult matchResult, List<object> objects)
        {
            Topology.Discovery(objects, TopologyDiscoveryTypes.ServiceID);   

            objects.Add(Base64.Encode((string)objects[2])); // 34
        }

        private static string[] CommonString(string left, string right)
        {
            List<string> result = new List<string>();
            string[] rightArray = right.Split();
            string[] leftArray = left.Split();

            result.AddRange(rightArray.Where(r => leftArray.Any(l => l.StartsWith(r))));

            // must check other way in case left array contains smaller words than right array
            result.AddRange(leftArray.Where(l => rightArray.Any(r => r.StartsWith(l))));

            return result.Distinct().ToArray();
        }

        #endregion
    }
}