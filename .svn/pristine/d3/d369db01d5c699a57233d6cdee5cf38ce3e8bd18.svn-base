using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.crm.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Configuration;
using System.IO;
using static OfficeOpenXml.ExcelErrorValue;
using static Microsoft.IO.RecyclableMemoryStreamManager;
using Newtonsoft.Json.Linq;
using System.Web.Http.Results;
using System.Web.UI;

namespace ems.crm.DataAccess
{
    public class daWebsite
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objodbcDataReader;
        DataTable dt_datatable;
        string lscustomer_gid, msGetGid, msGetGid1, msGetGid2, customer, lschat_id, final_path;
        int mnResult, mnResult3;

        public void DachatSummary(MdlWebsite values)
        { ///code for get no of customers////
            try
            {
                result objresult = new result();
                inlinechatconfiguration getinlinechatcredentials = inlinechatcredentials();
                MdlWebsite objMdlWebsite = new MdlWebsite();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var client = new RestClient("https://api.livechatinc.com");
                var request = new RestRequest("/v3.4/agent/action/list_customers", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                var body = @"{}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                objMdlWebsite = JsonConvert.DeserializeObject<MdlWebsite>(response.Content);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    var listcustomer = objMdlWebsite.customers;
                    foreach (var item in listcustomer)
                    {
                        DateTime createdDateTime = item.created_at;
                        DateTime openedAtDateTime = item.last_visit.last_pages[0].opened_at;
                        TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime istCreatedDateTime = TimeZoneInfo.ConvertTimeFromUtc(createdDateTime, istTimeZone);
                        DateTime istOpenedAtDateTime = TimeZoneInfo.ConvertTimeFromUtc(openedAtDateTime, istTimeZone);
                        string formattedCreatedDateTime = istCreatedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        string formattedOpenedAtDateTime = istOpenedAtDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                        if (item.type == "customer")
                        {

                            msSQL = "select user_id from  crm_smm_tinlinechat where user_id ='" + item.id + "'";
                            objodbcDataReader = objdbconn.GetDataReader(msSQL);
                            if (objodbcDataReader != null && objodbcDataReader.HasRows != true)
                            {
                                msSQL = "insert into crm_smm_tinlinechat(" +
                                                 "user_id," +
                                                "user_name," +
                                                "user_mail," +
                                                "email_verified," +
                                                 "user_type," +
                                                "created_date," +
                                                "ip_address," +
                                                "country," +
                                                 "country_code," +
                                                "region," +
                                                "city," +
                                                "timezone," +
                                                 "latitude," +
                                                "longitude," +
                                                "page_openedat," +
                                                 "page_url," +
                                                "title," +
                                                 "threads_count," +
                                                "visits_count," +
                                                "user_agent," +
                                                 "page_views_count)" +
                                                 "values(" +
                                                 "'" + item.id + "'," +
                                                 "'" + item.name + "'," +
                                                  "'" + item.email + "'," +
                                                   "'" + item.email_verified + "'," +
                                                 "'" + item.type + "'," +
                                               "'" + formattedCreatedDateTime + "'," +
                                                  "'" + item.last_visit.ip + "'," +
                                                   "'" + item.last_visit.geolocation.country + "'," +
                                                 "'" + item.last_visit.geolocation.country_code + "'," +
                                                 "'" + item.last_visit.geolocation.region + "'," +
                                                  "'" + item.last_visit.geolocation.city + "'," +
                                                   "'" + item.last_visit.geolocation.timezone + "'," +
                                                 "'" + item.last_visit.geolocation.latitude + "'," +
                                                 "'" + item.last_visit.geolocation.longitude + "'," +
                                                  "'" + formattedOpenedAtDateTime + "'," +
                                                   "'" + item.last_visit.last_pages[0].url + "'," +
                                                 "'" + item.last_visit.last_pages[0].title + "'," +
                                                 "'" + item.statistics.threads_count + "'," +
                                                  "'" + item.statistics.visits_count + "'," +
                                                  "'" + item.last_visit.user_agent + "'," +
                                                 "'" + item.statistics.page_views_count + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult == 1)
                                {
                                    objresult.status = true;
                                    objresult.message = "Record Added!";
                                }
                                else
                                {
                                    objresult.message = "Failed!";
                                }

                            }
                        }
                    }
                }

                msSQL = " select b.user_id,b.user_name,b.user_mail,TIME(b.created_date) as chatted_time ,(SELECT CASE WHEN DATE(b.created_date) = CURDATE() " +
                   " THEN DATE_FORMAT(b.created_date, '%d-%m-%y')   WHEN DATE(b.created_date) = CURDATE() - INTERVAL 1 DAY THEN 'Yesterday' " +
                   " ELSE DATE_FORMAT(b.created_date, '%d/%m/%y') END AS formatted_date ) AS chatted_at,SUBSTRING(b.user_name, 1,2) AS first_letter,b.chat_id from crm_smm_tchatmessage b order by b.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<listof_chat>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new listof_chat
                        {
                            user_id = dt["user_id"].ToString(),
                            user_name = dt["user_name"].ToString(),
                            user_mail = dt["user_mail"].ToString(),
                            first_letter = dt["first_letter"].ToString(),
                            chatted_at = dt["chatted_at"].ToString(),
                            chatted_time = dt["chatted_time"].ToString(),
                            chat_id = dt["chat_id"].ToString(),

                        });
                        values.listof_chat = getModuleList;
                    }
                }
                if (dt_datatable != null)
                {
                    dt_datatable.Dispose();
                }

            }
            catch (Exception ex)
            {
                values.message = "Error While Vieing Mail";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
        }
        public void DaGetuserdeatils(MdlWebsite values, string user_id)
        {
            try
            {
                msSQL = " select a.user_id,a.user_name,a.user_mail,SUBSTRING(a.user_name, 1,2) AS first_letter,a.user_type,concat(a.city,', ',ifnull(a.region,''),', ',ifnull(a.country,''))as location," +
                   "a.page_openedat,a.page_url,a.title,a.threads_count,a.visits_count,a.page_views_count,a.created_date,a.ip_address,a.user_agent  " +
                   " from crm_smm_tinlinechat a WHERE  user_id = '" + user_id + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<user_details>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new user_details
                        {
                            user_id = dt["user_id"].ToString(),
                            user_name = dt["user_name"].ToString(),
                            user_mail = dt["user_mail"].ToString(),
                            first_letter = dt["first_letter"].ToString(),
                            user_type = dt["user_type"].ToString(),
                            location = dt["location"].ToString(),
                            page_openedat = dt["page_openedat"].ToString(),
                            page_url = dt["page_url"].ToString(),
                            title = dt["title"].ToString(),
                            threads_count = dt["threads_count"].ToString(),
                            visits_count = dt["visits_count"].ToString(),
                            page_views_count = dt["page_views_count"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            ip_address = dt["ip_address"].ToString(),
                            user_agent = dt["user_agent"].ToString(),
                        });
                        values.user_details = getModuleList;
                    }
                }
                if (dt_datatable != null)
                {
                    dt_datatable.Dispose();
                }
            }
            catch (Exception ex)
            {
                values.message = "Error While Vieing Mail";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
        }
        public void Dalistofchat(MdlWebsite values)
        {
            try
            {

                //Get chat and thread id///
                result objresult = new result();
                inlinechatconfiguration getinlinechatcredentials = inlinechatcredentials();
                MdlWebsite objMdlWebsite = new MdlWebsite();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var client = new RestClient("https://api.livechatinc.com");
                var request = new RestRequest("/v3.4/agent/action/list_chats", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                var body = @"{}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                objMdlWebsite = JsonConvert.DeserializeObject<MdlWebsite>(response.Content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var listchat = objMdlWebsite.chats_summary;

                    foreach (var item in listchat)
                    {

                        if (item.last_event_per_type.filled_form != null)
                        {
                            DateTime createdDateTime = item.last_event_per_type.filled_form.thread_created_at;
                            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                            DateTime istCreatedDateTime = TimeZoneInfo.ConvertTimeFromUtc(createdDateTime, istTimeZone);
                            string formattedCreatedDateTime = istCreatedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                            msSQL = "select chat_id from  crm_smm_tchatmessage where chat_id ='" + item.id + "'";
                            objodbcDataReader = objdbconn.GetDataReader(msSQL);
                            if (objodbcDataReader != null && objodbcDataReader.HasRows != true)
                            {
                                msSQL = "insert into crm_smm_tchatmessage(" +
                                             "chat_id," +
                                            "created_date," +
                                            "user_id," +
                                             "user_name," +
                                            "user_mail," +
                                            "event_id," +
                                             "event_type)" +
                                             "values(" +
                                             "'" + item.id + "'," +
                                               "'" + formattedCreatedDateTime + "'," +
                                              "'" + item.last_event_per_type.filled_form.@event.author_id + "'," +
                                             "'" + item.last_event_per_type.filled_form.@event.fields[0].answer + "'," +
                                              "'" + item.last_event_per_type.filled_form.@event.fields[1].answer + "'," +
                                               "'" + item.last_event_per_type.filled_form.@event.id + "'," +
                                             "'" + item.last_event_per_type.filled_form.@event.type + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult == 1)
                                {
                                    objresult.status = true;
                                    objresult.message = "Record Added!";
                                }
                                else
                                {
                                    objresult.message = "Failed!";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Error While Vieing Mail";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
        }
        public void Dalistofthreads(Rootobject2 values)
        {
            try
            {

                msSQL = "select chat_id from crm_smm_tchatmessage";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<listof_threads>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new listof_threads
                        {
                            chat_id = dt["chat_id"].ToString()
                        });
                        values.listof_threads = getModuleList;
                    }
                }
                if (values.listof_threads != null)
                {
                    for (int i = 0; i < values.listof_threads.ToArray().Length; i++)
                    {
                        result objresult = new result();
                        inlinechatconfiguration getinlinechatcredentials = inlinechatcredentials();
                        Rootobject2 objMdlWebsite = new Rootobject2();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var client = new RestClient("https://api.livechatinc.com");
                        var request = new RestRequest("/v3.4/agent/action/list_threads", Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                        var body = "{\"chat_id\":\"" + values.listof_threads[i].chat_id + "\"}";
                        request.AddParameter("application/json", body, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        objMdlWebsite = JsonConvert.DeserializeObject<Rootobject2>(response.Content);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var listthreads = objMdlWebsite.threads;
                            foreach (var item in listthreads)
                            {
                                msSQL = "select thread_id from  crm_smm_tchatmessagethread where thread_id ='" + item.id + "'";
                                objodbcDataReader = objdbconn.GetDataReader(msSQL);
                                if (objodbcDataReader != null && objodbcDataReader.HasRows != true)
                                {

                                    DateTime createdDateTime = item.created_at;
                                    TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                                    DateTime istCreatedDateTime = TimeZoneInfo.ConvertTimeFromUtc(createdDateTime, istTimeZone);
                                    string formattedCreatedDateTime = istCreatedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                                    msSQL = "insert into crm_smm_tchatmessagethread(" +
                                                     "chat_id," +
                                                    "thread_id," +
                                                    "created_date," +
                                                    "nextthread_id," +
                                                     "previousthread_id," +
                                                     "next_page_id)" +
                                                     "values(" +
                                                      "'" + values.listof_threads[i].chat_id + "'," +
                                                     "'" + item.id + "'," +
                                                     "'" + formattedCreatedDateTime + "'," +
                                                      "'" + item.next_thread_id + "'," +
                                                     "'" + item.previous_thread_id + "'," +
                                                     "'" + objMdlWebsite.next_page_id + "')";

                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    if (mnResult == 1)
                                    {
                                        objresult.status = true;
                                        objresult.message = "Record Added!";
                                    }
                                    else
                                    {
                                        objresult.message = "Failed!";
                                    }
                                }

                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                values.message = "Error While Vieing Mail";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
        }
        public void Daindividualchat(mdlinlinechat values, string chat_id)
        {
            try
            {
                msSQL = "select thread_id from crm_smm_tchatmessagethread where chat_id='" + chat_id + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<listof_threads>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new listof_threads
                        {
                            thread_id = dt["thread_id"].ToString()
                        });
                        values.listof_threads = getModuleList;
                    }
                }
                if (values.listof_threads != null)
                {
                    for (int i = 0; i < values.listof_threads.ToArray().Length; i++)
                    {
                        result objresult = new result();
                        inlinechatconfiguration getinlinechatcredentials = inlinechatcredentials();
                        mdlinlinechat objMdlWebsite = new mdlinlinechat();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var client = new RestClient("https://api.livechatinc.com");
                        var request = new RestRequest("/v3.4/agent/action/get_chat", Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                        var body = "{\"chat_id\":\"" + chat_id + "\",\"thread_id\":\"" + values.listof_threads[i].thread_id + "\"}";
                        request.AddParameter("application/json", body, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        objMdlWebsite = JsonConvert.DeserializeObject<mdlinlinechat>(response.Content);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {

                            for (int j = 0; j < objMdlWebsite.thread.events.ToArray().Length; j++)
                            {

                                msSQL = "select event_id from  crm_smm_tchatmessagedtl where event_id ='" + objMdlWebsite.thread.events[j].id + "'";
                                objodbcDataReader = objdbconn.GetDataReader(msSQL);
                                if (objodbcDataReader != null && objodbcDataReader.HasRows != true)
                                {
                                    DateTime createdDateTime = objMdlWebsite.thread.created_at;
                                    DateTime chatedAtDateTime = objMdlWebsite.thread.events[j].created_at;
                                    TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                                    DateTime istCreatedDateTime = TimeZoneInfo.ConvertTimeFromUtc(createdDateTime, istTimeZone);
                                    DateTime istchatedAtDateTime = TimeZoneInfo.ConvertTimeFromUtc(chatedAtDateTime, istTimeZone);
                                    string formattedCreatedDateTime = istCreatedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                                    string formattedChatedAtDateTime = istchatedAtDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                                    if (objMdlWebsite.thread.events[j].type == "message" || objMdlWebsite.thread.events[j].type == "system_message" || objMdlWebsite.thread.events[j].type == "file")
                                    {
                                        string agentId = objMdlWebsite.users.Length > 1 ? objMdlWebsite.users[1]?.id ?? "null" : "null";
                                        string imageurl = string.IsNullOrEmpty(objMdlWebsite.thread.events[j].url) ? "null" : "" + objMdlWebsite.thread.events[j].url + "";
                                        string imagename = string.IsNullOrEmpty(objMdlWebsite.thread.events[j].name) ? "null" : "" + objMdlWebsite.thread.events[j].name + "";
                                        string alternativename = string.IsNullOrEmpty(objMdlWebsite.thread.events[j].alternative_text) ? "null" : "" + objMdlWebsite.thread.events[j].alternative_text + "";


                                        string commentMessage = System.Net.WebUtility.HtmlEncode(objMdlWebsite.thread.events[j].text);

                                        Console.WriteLine("Original Emoji: " + objMdlWebsite.thread.events[j].text);
                                        Console.WriteLine("HTML Entity Code: " + commentMessage);

                                        string messageWithEmojis = objMdlWebsite.thread.events[j].text;

                                        // Replace single quotes with double single quotes to escape them
                                        //string escapedMessage = messageWithEmojis.Replace("'", "''");
                                        msSQL = "insert into crm_smm_tchatmessagedtl(" +
                                                       "user_id," +
                                                       "chat_id," +
                                                       "thread_id," +
                                                       "created_date," +
                                                       "event_type," +
                                                       "event_id," +
                                                       "chatted_at," +
                                                       "message," +
                                                       "agent_id," +
                                                       "image_url," +
                                                       "image_name," +
                                                       "alternative_name," +
                                                       "author_id)" +
                                                       "values(" +
                                                       "'" + objMdlWebsite.users[0].id + "'," +
                                                       "'" + objMdlWebsite.id + "'," +
                                                       "'" + objMdlWebsite.thread.id + "'," +
                                                       "'" + formattedCreatedDateTime + "'," +
                                                       "'" + objMdlWebsite.thread.events[j].type + "'," +
                                                       "'" + objMdlWebsite.thread.events[j].id + "'," +
                                                       "'" + formattedChatedAtDateTime + "'," +
                                                       "'" + commentMessage + "'," +
                                                       "'" + agentId + "'," +
                                                       "'" + imageurl + "'," +
                                                       "'" + imagename + "'," +
                                                       "'" + alternativename + "'," +
                                                       "'" + objMdlWebsite.thread.events[j].author_id + "')";


                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        if (mnResult == 1)
                                        {
                                            objresult.status = true;
                                            objresult.message = "Record Added!";
                                        }
                                        else
                                        {
                                            objresult.message = "Failed!";
                                        }
                                    }
                                    else if (objMdlWebsite.thread.events[j].type == "filled_form")
                                    {
                                        msSQL = "insert into crm_smm_tchatmessagedtl(" +
                                                           "user_id," +
                                                            "chat_id," +
                                                           "thread_id," +
                                                           "created_date," +
                                                            "event_type," +
                                                           "event_id," +
                                                           "chatted_at," +
                                                           "message," +
                                                            "author_id)" +
                                                            "values(" +
                                                             "'" + objMdlWebsite.users[0].id + "'," +
                                                             "'" + objMdlWebsite.id + "'," +
                                                             "'" + objMdlWebsite.thread.id + "'," +
                                                           "'" + formattedCreatedDateTime + "'," +
                                                             "'" + objMdlWebsite.thread.events[j].type + "'," +
                                                             "'" + objMdlWebsite.thread.events[j].id + "'," +
                                                            "'" + formattedChatedAtDateTime + "'," +
                                                             "'" + objMdlWebsite.thread.events[j].text + "'," +
                                                            "'" + objMdlWebsite.thread.events[j].author_id + "')";

                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        if (mnResult == 1)
                                        {
                                            objresult.status = true;
                                            objresult.message = "Delivered!";
                                        }
                                        else
                                        {
                                            objresult.message = "Failed!";
                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                msSQL = " select a.user_id,a.user_name,SUBSTRING(a.user_name, 1,2) AS first_letter,a.user_mail " +
                          " from crm_smm_tchatmessage a left join crm_smm_tinlinechat b on b.user_id=a.user_id WHERE  chat_id = '" + chat_id + "'";

                objodbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objodbcDataReader != null && objodbcDataReader.HasRows)
                {
                    values.user_id = objodbcDataReader["user_id"].ToString();
                    values.user_name = objodbcDataReader["user_name"].ToString();
                    values.user_mail = objodbcDataReader["user_mail"].ToString();
                    values.first_letter = objodbcDataReader["first_letter"].ToString();
                    objodbcDataReader.Close();

                }
                msSQL = " Select a.user_id,a.chat_id,a.thread_id,a.created_date,a.event_id,a.event_type,a.chatted_at,a.author_id, " +
                            " a.agent_id,a.image_name,CASE WHEN a.event_type = 'filled_form' THEN CONCAT('Form',' - ',IFNULL(b.user_name, ''), ' ', IFNULL(b.user_mail, '')) WHEN a.event_type = 'file' THEN a.image_url ELSE a.message END AS message" +
                            " from crm_smm_tchatmessagedtl a left join crm_smm_tinlinechat b on b.user_id=a.user_id where a.chat_id ='" + chat_id + "' order BY  a.chatted_at desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleLists = new List<GetViewchatsummary>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string FileExtension = null;
                        if (dt["event_type"].ToString() == "file")
                        {
                            FileExtension = Path.GetExtension(dt["message"].ToString());
                            getModuleLists.Add(new GetViewchatsummary
                            {
                                user_id = dt["user_id"].ToString(),
                                chat_id = dt["chat_id"].ToString(),
                                created_date = dt["created_date"].ToString(),
                                chatted_at = dt["chatted_at"].ToString(),
                                message = dt["message"].ToString(),
                                author_id = dt["author_id"].ToString(),
                                agent_id = dt["agent_id"].ToString(),
                                event_type = dt["event_type"].ToString(),
                                image_name = dt["image_name"].ToString(),
                                ext = fileType(FileExtension)
                            });
                        }
                        else
                        {
                            getModuleLists.Add(new GetViewchatsummary
                            {
                                user_id = dt["user_id"].ToString(),
                                chat_id = dt["chat_id"].ToString(),
                                created_date = dt["created_date"].ToString(),
                                chatted_at = dt["chatted_at"].ToString(),
                                message = dt["message"].ToString(),
                                author_id = dt["author_id"].ToString(),
                                agent_id = dt["agent_id"].ToString(),
                                event_type = dt["event_type"].ToString()

                            });
                        }
                        values.GetViewchatsummary = getModuleLists;
                    }
                }
                if (dt_datatable != null)
                {
                    dt_datatable.Dispose();
                }
            }
            catch (Exception ex)
            {
                values.message = "Error While Vieing Mail";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
        }
        public string fileType(string FileExtension)
        {

            string file_type = null;
            if (FileExtension == ".jpg" | FileExtension == ".png" | FileExtension == ".jpeg" | FileExtension == ".gif" | FileExtension == ".JPG" | FileExtension == ".JPEG" | FileExtension == ".JIFF" | FileExtension == ".TIFF" | FileExtension == ".PNG" | FileExtension == ".GIF" | FileExtension == ".JFIF" | FileExtension == ".svg" | FileExtension == ".jfif")
            {
                file_type = "Image";
            }
            else if (FileExtension == ".mp4" | FileExtension == ".MP4" | FileExtension == ".avi" | FileExtension == ".mkv" | FileExtension == ".wmv" | FileExtension == ".mov" | FileExtension == ".WebM" | FileExtension == ".flv" | FileExtension == ".hevc" | FileExtension == ".vpg")
            {
                file_type = "Video";
            }
            else
            {
                file_type = "Document";
            }
            return file_type;
        }
        public result DaMessagesend(messagesend values, string user_gid)
        {

            result objresult = new result();
            try
            {
                inlinechatconfiguration getinlinechatcredentials = inlinechatcredentials();
                msSQL = "select useradd_flag from  crm_smm_tchatmessage where chat_id ='" + values.chat_id + "' and useradd_flag ='Unassigned'";
                objodbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objodbcDataReader.HasRows == true)
                {

                    MdlWebsite objMdlWebsites = new MdlWebsite();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var clients = new RestClient("https://api.livechatinc.com");
                    var requests = new RestRequest("/v3.4/agent/action/add_user_to_chat", Method.POST);
                    requests.AddHeader("Content-Type", "application/json");
                    requests.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                    var bodycontent = @"{" + "\n" +
               @"   ""chat_id"": """ + values.chat_id + @"""," + "\n" +
               @"    ""user_id"": "" " + getinlinechatcredentials.id + @"""," + "\n" +
               @"     ""user_type"": ""agent""," + "\n" +
               @"     ""visibility"":  ""all""," + "\n" +
               @"     ""ignore_requester_presence"": true" + "\n" +
               @"}";
                    requests.AddParameter("application/json", bodycontent, ParameterType.RequestBody);
                    IRestResponse responses = clients.Execute(requests);
                    objMdlWebsites = JsonConvert.DeserializeObject<MdlWebsite>(responses.Content);
                    if (responses.StatusCode == HttpStatusCode.OK)
                    {
                        msSQL = " update  crm_smm_tchatmessage  set " +
                                " useradd_flag = 'Assigned' where chat_id='" + values.chat_id + "'  ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objresult.status = true;
                            objresult.message = "Updated!";
                        }
                        else
                        {
                            objresult.message = "Failed!";
                        }

                    }
                }
                mdlinlinechat objMdlWebsite = new mdlinlinechat();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var client = new RestClient("https://api.livechatinc.com");
                var request = new RestRequest("/v3.4/agent/action/send_event", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                //var body = "{\"chat_id\":\"" + values. + "\",\"thread_id\":\"" + thread_id + "\"}";
                var body = @"{" + "\n" +
                   @"   ""chat_id"": """ + values.chat_id + @"""," + "\n" +
                   @"   ""event"": {" + "\n" +
                   @"     ""type"": ""message""," + "\n" +
                   @"     ""text"": """ + values.sendtext + @"""," + "\n" +
                   @"     ""visibility"": ""all""" + "\n" +
                   @"   }" + "\n" +
                   @"}";


                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                objMdlWebsite = JsonConvert.DeserializeObject<mdlinlinechat>(response.Content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    objresult.status = true;
                    objresult.message = "Message Send Successfully!";
                }
                else
                {
                    string content = response.Content;
                    JObject errorJson;
                    try
                    {
                        errorJson = JObject.Parse(content);
                        if (errorJson["error"]?["type"]?.ToString() == "validation")
                        {
                            objresult.status = true;
                            objresult.status = fnresumechat(values.chat_id, values.sendtext);
                            objresult.message = "Message Send Successfully!";

                        }
                        else
                        {
                            objresult.status = false;
                            objresult.message = "Failed to Send!!!";
                        }

                    }
                    catch (JsonException)
                    {
                        // Handle JSON parsing exception
                        objresult.status = false;
                        objresult.message = "Failed to Send!!!";
                    }

                }

            }
            catch (Exception ex)
            {
                values.message = "Error While Vieing Mail";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
            return objresult;
        }
        public bool fnresumechat(string chat_id, string sendtext)
        {
            result objresults = new result();
            try
            {
                inlinechatconfiguration getinlinechatcredentials = inlinechatcredentials();

                MdlWebsite objMdlWebsites = new MdlWebsite();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var clients = new RestClient("https://api.livechatinc.com");
                var requests = new RestRequest("/v3.4/agent/action/resume_chat", Method.POST);
                requests.AddHeader("Content-Type", "application/json");
                requests.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                var body = @"{""chat"": {""id"": """ + chat_id + @"""} }";
                requests.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                objMdlWebsites = JsonConvert.DeserializeObject<MdlWebsite>(responses.Content);
                if (responses.StatusCode == HttpStatusCode.OK)
                {
                    msSQL = "select thread_id from  crm_smm_tchatmessagethread where thread_id ='" + objMdlWebsites.thread_id + "'";
                    objodbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objodbcDataReader != null && objodbcDataReader.HasRows != true)
                    {
                        msSQL = "insert into crm_smm_tchatmessagethread(" +
                                                       "chat_id," +
                                                       "thread_id," +
                                                       "created_date)" +
                                                       "values(" +
                                                       "'" + chat_id + "'," +
                                                       "'" + objMdlWebsites.thread_id + "'," +
                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objresults.status = true;
                            objresults.message = "Delivered!";
                            result objresult = new result();
                            mdlinlinechat objMdlWebsite = new mdlinlinechat();
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                            var client = new RestClient("https://api.livechatinc.com");
                            var request = new RestRequest("/v3.4/agent/action/send_event", Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            request.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                            //var body = "{\"chat_id\":\"" + values. + "\",\"thread_id\":\"" + thread_id + "\"}";
                            var bodycontent = @"{" + "\n" +
                               @"   ""chat_id"": """ + chat_id + @"""," + "\n" +
                               @"   ""event"": {" + "\n" +
                               @"     ""type"": ""message""," + "\n" +
                               @"     ""text"": """ + sendtext + @"""," + "\n" +
                               @"     ""visibility"": ""all""" + "\n" +
                               @"   }" + "\n" +
                               @"}";
                            request.AddParameter("application/json", bodycontent, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            objMdlWebsite = JsonConvert.DeserializeObject<mdlinlinechat>(response.Content);

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                objresult.status = true;
                                objresult.message = "Message Send Successfully!";
                            }
                        }
                        else
                        {
                            objresults.message = "Failed to Send!!!";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                objresults.message = "Failed to Send!!!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + objresults.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
            return true;
        }
        public inlinechatconfiguration inlinechatcredentials()
        {

            inlinechatconfiguration getinlinechatcredentials = new inlinechatconfiguration();
            try
            {

                msSQL = " select id,access_token from crm_smm_tinlinechatservice";
                objodbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objodbcDataReader != null && objodbcDataReader.HasRows)
                {
                    getinlinechatcredentials.id = objodbcDataReader["id"].ToString();
                    getinlinechatcredentials.access_token = objodbcDataReader["access_token"].ToString();

                }
            }
            catch (Exception ex)
            {

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            return getinlinechatcredentials;
        }
        public void DaGetchatAnalyticsUser(MdlWebsite values)
        {
            try
            {
                msSQL = "select b.city,b.country,count(a.chat_id) AS total_users from crm_smm_tchatmessage a left join crm_smm_tinlinechat b on b.user_id=a.user_id; ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<chat_analytics>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new chat_analytics
                        {
                            city = dt["city"].ToString(),
                            country = dt["country"].ToString(),
                            total_users = dt["total_users"].ToString(),
                        });
                    }
                    values.chat_analytics = getmodulelist;
                }

                if (dt_datatable != null)
                {
                    dt_datatable.Dispose();
                }
            }
            catch (Exception ex)
            {
                values.message = "Error While Vieing Mail";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
        }
        public void DaGetchatAnalyticsSummary(MdlWebsite values)
        {
            try
            {
                result objresult = new result();
                inlinechatconfiguration getinlinechatcredentials = inlinechatcredentials();
                MdlWebsite objMdlWebsite = new MdlWebsite();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var client = new RestClient("https://api.livechatinc.com");
                var request = new RestRequest("/v3.4/agent/action/list_customers", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                var body = @"{}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                objMdlWebsite = JsonConvert.DeserializeObject<MdlWebsite>(response.Content);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    var listcustomer = objMdlWebsite.customers;
                    foreach (var item in listcustomer)
                    {
                        DateTime createdDateTime = item.created_at;
                        DateTime openedAtDateTime = item.last_visit.last_pages[0].opened_at;
                        TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime istCreatedDateTime = TimeZoneInfo.ConvertTimeFromUtc(createdDateTime, istTimeZone);
                        DateTime istOpenedAtDateTime = TimeZoneInfo.ConvertTimeFromUtc(openedAtDateTime, istTimeZone);
                        string formattedCreatedDateTime = istCreatedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        string formattedOpenedAtDateTime = istOpenedAtDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                        if (item.type == "customer")
                        {

                            msSQL = "select user_id from  crm_smm_tinlinechat where user_id ='" + item.id + "'";
                            objodbcDataReader = objdbconn.GetDataReader(msSQL);
                            if (objodbcDataReader != null && objodbcDataReader.HasRows != true)
                            {
                                msSQL = "insert into crm_smm_tinlinechat(" +
                                                 "user_id," +
                                                "user_name," +
                                                "user_mail," +
                                                "email_verified," +
                                                 "user_type," +
                                                "created_date," +
                                                "ip_address," +
                                                "country," +
                                                 "country_code," +
                                                "region," +
                                                "city," +
                                                "timezone," +
                                                 "latitude," +
                                                "longitude," +
                                                "page_openedat," +
                                                 "page_url," +
                                                "title," +
                                                 "threads_count," +
                                                "visits_count," +
                                                "user_agent," +
                                                 "page_views_count)" +
                                                 "values(" +
                                                 "'" + item.id + "'," +
                                                 "'" + item.name + "'," +
                                                  "'" + item.email + "'," +
                                                   "'" + item.email_verified + "'," +
                                                 "'" + item.type + "'," +
                                               "'" + formattedCreatedDateTime + "'," +
                                                  "'" + item.last_visit.ip + "'," +
                                                   "'" + item.last_visit.geolocation.country + "'," +
                                                 "'" + item.last_visit.geolocation.country_code + "'," +
                                                 "'" + item.last_visit.geolocation.region + "'," +
                                                  "'" + item.last_visit.geolocation.city + "'," +
                                                   "'" + item.last_visit.geolocation.timezone + "'," +
                                                 "'" + item.last_visit.geolocation.latitude + "'," +
                                                 "'" + item.last_visit.geolocation.longitude + "'," +
                                                  "'" + formattedOpenedAtDateTime + "'," +
                                                   "'" + item.last_visit.last_pages[0].url + "'," +
                                                 "'" + item.last_visit.last_pages[0].title + "'," +
                                                 "'" + item.statistics.threads_count + "'," +
                                                  "'" + item.statistics.visits_count + "'," +
                                                  "'" + item.last_visit.user_agent + "'," +
                                                 "'" + item.statistics.page_views_count + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult == 1)
                                {
                                    objresult.status = true;
                                    objresult.message = "Record Added!";
                                }
                                else
                                {
                                    objresult.message = "Failed!";
                                }

                            }
                            else
                            {
                                msSQL = " update  crm_smm_tinlinechat set " +
                                " threads_count = '" + item.statistics.threads_count + "'," +
                                " visits_count = '" + item.statistics.visits_count + "'," +
                                " page_views_count = '" + item.statistics.page_views_count + "' where user_id='" + item.id + "' ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult == 1)
                                {
                                    objresult.status = true;
                                    objresult.message = " Updated Successfully !!";
                                }
                                else
                                {
                                    objresult.status = false;
                                    objresult.message = "Error While Updating !!";
                                }
                            }

                        }
                    }
                }

        
                msSQL = "select a.user_name,a.user_mail,DATE_FORMAT(a.created_date, '%Y-%m-%d') AS  created_date,b.country,b.city,b.visits_count,b.page_views_count,b.title from crm_smm_tchatmessage a left join crm_smm_tinlinechat b on b.user_id=a.user_id order by a.created_date desc  ;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<chat_analytics1>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new chat_analytics1
                        {
                            user_name = dt["user_name"].ToString(),
                            user_mail = dt["user_mail"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            country = string.IsNullOrEmpty(dt["country"].ToString()) ? "Not Defined" : dt["country"].ToString(),
                            city = string.IsNullOrEmpty(dt["city"].ToString()) ? "Not Defined" : dt["city"].ToString(),
                            visits_count = dt["visits_count"].ToString(),
                            page_views_count = dt["page_views_count"].ToString(),
                            page_title = dt["title"].ToString(),

                        });
                    }
                }
                values.chat_analytics1 = getmodulelist;
            }
            catch (Exception ex)
            {
                values.message = "Error While Vieing Mail";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
        }
        public void DaGetweekwiselist(MdlWebsite values)
        {
            try
            {

                msSQL = "SELECT CONCAT(LEFT(DATE_FORMAT(created_date, '%W'), 3), '-', DAY(created_date)) AS week_date,created_date,COUNT(*) AS week_users FROM crm_smm_tchatmessage " +
                    "  WHERE created_date >= CURDATE() - INTERVAL 7 DAY GROUP BY week_date ORDER BY created_date;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<weekwise_report>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new weekwise_report
                        {
                            week_date = dt["week_date"].ToString(),
                            week_users = dt["week_users"].ToString(),
                        });
                    }
                    values.weekwise_report = getmodulelist;
                }

                if (dt_datatable != null)
                {
                    dt_datatable.Dispose();
                }
            }
            catch (Exception ex)
            {
                values.message = "Error While Vieing Mail";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
        }
        public result Dauploadsend(messagesend values)
        {
            result objresults = new result();
            try
            {

                inlinechatconfiguration getinlinechatcredentials = inlinechatcredentials();
                msSQL = "select useradd_flag from  crm_smm_tchatmessage where chat_id ='" + values.chat_id + "' and useradd_flag ='Unassigned'";
                objodbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objodbcDataReader != null && objodbcDataReader.HasRows)
                {

                    MdlWebsite objMdlWebsites = new MdlWebsite();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var clients = new RestClient("https://api.livechatinc.com");
                    var requests = new RestRequest("/v3.4/agent/action/add_user_to_chat", Method.POST);
                    requests.AddHeader("Content-Type", "application/json");
                    requests.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                    var bodycontent = @"{" + "\n" +
               @"   ""chat_id"": """ + values.chat_id + @"""," + "\n" +
               @"    ""user_id"": "" " + getinlinechatcredentials.id + @"""," + "\n" +
               @"     ""user_type"": ""agent""," + "\n" +
               @"     ""visibility"":  ""all""," + "\n" +
               @"     ""ignore_requester_presence"": true" + "\n" +
               @"}";
                    requests.AddParameter("application/json", bodycontent, ParameterType.RequestBody);
                    IRestResponse responses = clients.Execute(requests);
                    objMdlWebsites = JsonConvert.DeserializeObject<MdlWebsite>(responses.Content);
                    if (responses.StatusCode == HttpStatusCode.OK)
                    {
                        msSQL = " update  crm_smm_tchatmessage  set " +
                                " useradd_flag = 'Assigned' where chat_id='" + values.chat_id + "'  ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objresults.status = true;
                            objresults.message = "Updated!";
                        }
                        else
                        {
                            objresults.message = "Failed!";
                        }

                    }
                }
                mdlinlinechat objMdlWebsite = new mdlinlinechat();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var client = new RestClient("https://api.livechatinc.com");
                var request = new RestRequest("/v3.4/agent/action/send_event", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                //var body = "{\"chat_id\":\"" + values. + "\",\"thread_id\":\"" + thread_id + "\"}";
                var body = @"{" + "\n" +
                   @"   ""chat_id"": """ + values.chat_id + @"""," + "\n" +
                   @"   ""event"": {" + "\n" +
                   @"     ""type"": ""file""," + "\n" +
                   @"     ""url"": """ + values.image_url + @"""," + "\n" +
                   @"     ""visibility"": ""all""" + "\n" +
                   @"   }" + "\n" +
                   @"}";


                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                objMdlWebsite = JsonConvert.DeserializeObject<mdlinlinechat>(response.Content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    objresults.status = true;
                    objresults.message = "Message Send Successfully!";
                }
                else
                {
                    string content = response.Content;
                    JObject errorJson;
                    try
                    {
                        errorJson = JObject.Parse(content);
                        if (errorJson["error"]?["type"]?.ToString() == "validation")
                        {
                            objresults.status = true;
                            objresults.status = fnresumechatupload(values.chat_id, values.image_url);
                            objresults.message = "Message Send Successfully!";

                        }
                        else
                        {
                            objresults.status = false;
                            objresults.message = "Failed to Send!!!";
                        }

                    }
                    catch (JsonException)
                    {
                        // Handle JSON parsing exception
                        objresults.status = false;
                        objresults.message = "Failed to Send!!!";
                    }

                }
            }
            catch (Exception ex)
            {
                values.message = "Error While Vieing Mail";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
            return objresults;
        }
        public bool fnresumechatupload(string chat_id, string image_url)
        {
            result objresults = new result();
            try
            {
                inlinechatconfiguration getinlinechatcredentials = inlinechatcredentials();
                MdlWebsite objMdlWebsites = new MdlWebsite();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var clients = new RestClient("https://api.livechatinc.com");
                var requests = new RestRequest("/v3.4/agent/action/resume_chat", Method.POST);
                requests.AddHeader("Content-Type", "application/json");
                requests.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                var body = @"{""chat"": {""id"": """ + chat_id + @"""} }";
                requests.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse responses = clients.Execute(requests);
                objMdlWebsites = JsonConvert.DeserializeObject<MdlWebsite>(responses.Content);
                if (responses.StatusCode == HttpStatusCode.OK)
                {
                    msSQL = "select thread_id from  crm_smm_tchatmessagethread where thread_id ='" + objMdlWebsites.thread_id + "'";
                    objodbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objodbcDataReader != null && objodbcDataReader.HasRows != true)
                    {
                        msSQL = "insert into crm_smm_tchatmessagethread(" +
                                                       "chat_id," +
                                                       "thread_id," +
                                                       "created_date)" +
                                                       "values(" +
                                                       "'" + chat_id + "'," +
                                                       "'" + objMdlWebsites.thread_id + "'," +
                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objresults.status = true;
                            objresults.message = "Delivered!";
                            result objresult = new result();
                            mdlinlinechat objMdlWebsite = new mdlinlinechat();
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                            var client = new RestClient("https://api.livechatinc.com");
                            var request = new RestRequest("/v3.4/agent/action/send_event", Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            request.AddHeader("Authorization", "" + getinlinechatcredentials.access_token + "");
                            //var body = "{\"chat_id\":\"" + values. + "\",\"thread_id\":\"" + thread_id + "\"}";
                            var bodycontent = @"{" + "\n" +
                               @"   ""chat_id"": """ + chat_id + @"""," + "\n" +
                               @"   ""event"": {" + "\n" +
                               @"     ""type"": ""file""," + "\n" +
                               @"     ""url"": """ + image_url + @"""," + "\n" +
                               @"     ""visibility"": ""all""" + "\n" +
                               @"   }" + "\n" +
                               @"}";
                            request.AddParameter("application/json", bodycontent, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            objMdlWebsite = JsonConvert.DeserializeObject<mdlinlinechat>(response.Content);

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                objresult.status = true;
                                objresult.message = "Message Send Successfully!";
                            }
                        }
                        else
                        {
                            objresults.message = "Failed to Send!!!";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                objresults.message = "Error While Vieing Mail";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + objresults.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
            return true;
        }
        public void DaGetaccesstoken(inlinechatconfiguration values)
        {
            try
            {
                msSQL = " select id,access_token from crm_smm_tinlinechatservice";
                objodbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objodbcDataReader != null && objodbcDataReader.HasRows)
                {
                    objodbcDataReader.Read();
                    values.id = objodbcDataReader["id"].ToString();
                    values.access_token = objodbcDataReader["access_token"].ToString();
                    objodbcDataReader.Close();

                }
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Livechat/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
        }
    }

}


