﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.crm.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
using System.Web;
using OfficeOpenXml;
using System.Configuration;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using RestSharp;
using Newtonsoft.Json;
using System.Web.Http.Results;
using static OfficeOpenXml.ExcelErrorValue;



namespace ems.crm.DataAccess
{
    public class DaLinkedin
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        int mnResult, mnResult1;
        string lsprofile_picture, lsuser_name;


        public string DaGetLinkedinProfile(string user_gid)
        {

            result objresult = new result();
            linkedinconfiguration getlinkedincredentials = linkedincredentials();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string requestAddressURL = "https://api.linkedin.com";
            var clientAddress = new RestClient(requestAddressURL);
            var requestAddress = new RestRequest("/v2/me?projection=(id%2CprofilePicture(displayImage~digitalmediaAsset%3AplayableStreams))", Method.GET);
            requestAddress.AddHeader("Authorization", "" + getlinkedincredentials.access_token + "");
            requestAddress.AddHeader("Cookie", "lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4052:u=107:x=1:i=1690447109:t=1690514997:v=2:sig=AQHdvTTL2KKWVCBy0Wncp8_j0L_rhNGZ\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"; lidc=\"b=OB71:s=O:r=O:a=O:p=O:g=2953:u=1:x=1:i=1690446031:t=1690532431:v=2:sig=AQHM4bLW4V2UCpCly3mhe6XtJ27eHCS-\"; lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4054:u=112:x=1:i=1690606086:t=1690614903:v=2:sig=AQEGtL9D6zi1XMv_X3rNOLayasKBbOud\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"");
            IRestResponse responseAddress = clientAddress.Execute(requestAddress);
            string address_erpid = responseAddress.Content;
            string errornetsuiteJSON = responseAddress.Content;
            linkedinprofile_list objMdlLinkedinprofileMessageResponse = new linkedinprofile_list();
            objMdlLinkedinprofileMessageResponse = JsonConvert.DeserializeObject<linkedinprofile_list>(errornetsuiteJSON);
            //string profile = objMdlLinkedinprofileMessageResponse.profilePicture.displayImageObj.elements[0].identifiers[0].identifier;

            try
            {
                 
                if (responseAddress.StatusCode == HttpStatusCode.OK)
                {
                    msSQL = "delete from   crm_smm_tlinkedinusersetails where type='Profile Picture'";
                    mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult1 == 1)
                    {
                        msSQL = "insert into crm_smm_tlinkedinusersetails(" +
                                         "type," +
                                         "upload_path," +
                                         "created_by," +
                                         "created_date)" +
                                         "values(" +
                                          "'Profile Picture'," +
                                       " '" + objMdlLinkedinprofileMessageResponse.profilePicture.displayImageObj.elements[0].identifiers[0].identifier + "',";
                        msSQL += "'" + user_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {
                            objresult.status = true;
                            objresult.message = "Record Inserted  !!";
                        }

                    }


                    msSQL = "select upload_path from crm_smm_tlinkedinusersetails where type='Profile Picture' ";
                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader .HasRows)
                    {
                        lsprofile_picture = objOdbcDataReader ["upload_path"].ToString();

                    }

                     

                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
              objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Getting LinkedIn Profile!! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Whatsapp/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
            return lsprofile_picture;


        }

        public void DaPostlinkedin(string user_gid, post_list values, result objResult)
        {

            try
            {
                string body_content = values.body_content;
                linkedinconfiguration getlinkedincredentials = linkedincredentials();

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var client = new RestClient("https://api.linkedin.com");
                var request = new RestRequest("/v2/ugcPosts", Method.POST);
                request.AddHeader("Content-Type", "text/plain");
                request.AddHeader("Authorization", "" + getlinkedincredentials.access_token + "");
                request.AddHeader("Cookie", "lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4052:u=107:x=1:i=1690447763:t=1690514997:v=2:sig=AQF53ungx7Xv2SchfUH-1JuVmM1gXMBE\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"; lidc=\"b=OB71:s=O:r=O:a=O:p=O:g=2953:u=1:x=1:i=1690446031:t=1690532431:v=2:sig=AQHM4bLW4V2UCpCly3mhe6XtJ27eHCS-\"; lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4055:u=112:x=1:i=1690792969:t=1690855311:v=2:sig=AQH031-Gy41KdS1Cd5hwNo4243VbJC-_\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"");
                //             var body = @"{" + "\n" +
                //  @"    ""author"": ""urn:li:person:_dZQknI6ZQ""," + "\n" +
                //  @"    ""lifecycleState"": ""PUBLISHED""," + "\n" +
                //  @"    ""specificContent"": {" + "\n" +
                //  @"        ""com.linkedin.ugc.sharecontent"": {" + "\n" +
                //  @"            ""shareCommentary"": {" + "\n" +
                // //@"                ""text"": ""Welcome to Linkedin API!""" + "\n" +
                //// @"                ""text"": "" '" + "" + "\n" +
                // @"""text"": """ + body_content +  "\n" +
                //  //@"                ""text"": ""Welcome to Linkedin API!""" + "\n" +
                //  //@"                ""text"": ""'" + values.body_content + "'" + "\n" +                    
                //  @"            }," + "\n" +
                //  @"            ""shareMediaCategory"": ""NONE""" + "\n" +
                //  @"        }" + "\n" +
                //  @"    }," + "\n" +
                //  @"    ""visibility"": {" + "\n" +
                //  @"        ""com.linkedin.ugc.MemberNetworkVisibility"": ""PUBLIC""" + "\n" +
                //  @"    }" + "\n" +
                //  @"}";

                var body = "{\"author\":\"urn:li:person:_dZQknI6ZQ\",\"lifecycleState\":\"PUBLISHED\",\"specificContent\":{\"com.linkedin.ugc.ShareContent\":{\"shareCommentary\":{\"text\":" + "\"" + body_content + "\"" + "},\"shareMediaCategory\":\"NONE\"}},\"visibility\":{\"com.linkedin.ugc.MemberNetworkVisibility\":\"PUBLIC\"}}";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    msSQL = " insert into crm_smm_tlinkedindtl(" +
                            " upload_type," +
                           " message_content," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           " 'Text', " +
                           " '" + values.body_content + "',";
                    msSQL += "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objResult.status = true;
                    objResult.message = "Posted in Linkedin Successfully !!";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error While Posting in Linkedin !!";
                }
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }
        public string DaGetLinkedinUser()
        {

            result objresult = new result();
            linkedinconfiguration getlinkedincredentials = linkedincredentials();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string requestAddressURL = "https://api.linkedin.com";
            var clientAddress = new RestClient(requestAddressURL);
            var requestAddress = new RestRequest("/v2/me", Method.GET);
            requestAddress.AddHeader("Authorization", "" + getlinkedincredentials.access_token + "");
            requestAddress.AddHeader("Cookie", "lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4052:u=107:x=1:i=1690447109:t=1690514997:v=2:sig=AQHdvTTL2KKWVCBy0Wncp8_j0L_rhNGZ\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"; lidc=\"b=OB71:s=O:r=O:a=O:p=O:g=2953:u=1:x=1:i=1690446031:t=1690532431:v=2:sig=AQHM4bLW4V2UCpCly3mhe6XtJ27eHCS-\"; lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4054:u=112:x=1:i=1690606086:t=1690614903:v=2:sig=AQEGtL9D6zi1XMv_X3rNOLayasKBbOud\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"");
            IRestResponse responseAddress = clientAddress.Execute(requestAddress);
            string address_erpid = responseAddress.Content;
            string errornetsuiteJSON = responseAddress.Content;
            linkedinuser_list objMdlLinkedinMessageResponse = new linkedinuser_list();
            objMdlLinkedinMessageResponse = JsonConvert.DeserializeObject<linkedinuser_list>(errornetsuiteJSON);

            try
            {
                 
                if (responseAddress.StatusCode == HttpStatusCode.OK)
                {
                    msSQL = "delete from   crm_smm_tlinkedinusersetails where type='User Details'";
                    mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult1 == 1)
                    {
                        msSQL = "insert into crm_smm_tlinkedinusersetails(" +
                                         "type," +
                                         "first_name," +
                                         "last_name)" +
                                         "values(" +
                                          "'User Details'," +
                                      "'" + objMdlLinkedinMessageResponse.localizedFirstName + "'," +
                                      "'" + objMdlLinkedinMessageResponse.localizedLastName + "')";


                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {
                            objresult.status = true;
                            objresult.message = "Record Inserted!!";
                        }
                    }
                    msSQL = "select concat(ifnull(a.first_name,''),' ',ifnull(a.last_name,''))as user_name from crm_smm_tlinkedinusersetails a where type='User Details'";
                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader .HasRows)
                    {
                        lsuser_name = objOdbcDataReader ["user_name"].ToString();

                    }

                     

                }
                objOdbcDataReader .Close();

            }
            catch (Exception ex)
            {
            objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Getting LinkedIn User!! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Whatsapp/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

           
                return lsuser_name;

            }
        public void DaGetlinkedin(MdlLinkedin values)
        {
            try
            {
                 
                msSQL = " select linkedin_gid,message_content,upload_type,created_date FROM crm_smm_tlinkedindtl  order by linkedin_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<linkedin_summary>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new linkedin_summary
                        {
                            linkedin_gid = dt["linkedin_gid"].ToString(),
                            message_content = dt["message_content"].ToString(),
                            upload_type = dt["upload_type"].ToString(),
                            created_date = dt["created_date"].ToString(),


                        });
                        values.linkedin_summary = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting LinkedIn!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            

        }

        public linkedinconfiguration linkedincredentials()
        {
            linkedinconfiguration getlinkedincredentials = new linkedinconfiguration();

            try
            {
                 
                msSQL = " select access_token from crm_smm_tlinkedinservice";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows == true)
                {
                     
                    getlinkedincredentials.access_token = objOdbcDataReader ["access_token"].ToString();

                     
                }
                else
                {

                }
                objOdbcDataReader .Close();

            }
            catch (Exception ex)
            {
             objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Getting LinkedIn Credentials!! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Whatsapp/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
            return getlinkedincredentials;
        }
    }
}