using System;
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
using System.Web.UI.WebControls;
using System.Web.Http.Results;
using static OfficeOpenXml.ExcelErrorValue;




namespace ems.crm.DataAccess
{
    public class DaInstagram
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;


        public void DaGetInstagram(instagramlist values)
        {
            try
            {


                result objresult = new result();
                instagramconfiguration getinstagramcredentials = instagramcredentials();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string requestAddressURL = "https://graph.instagram.com/me?fields=id,username&access_token=" + getinstagramcredentials.access_token + "";
                var clientAddress = new RestClient(requestAddressURL);
                var requestAddress = new RestRequest(Method.GET);
                IRestResponse responseAddress = clientAddress.Execute(requestAddress);
                string address_erpid = responseAddress.Content;
                string errornetsuiteJSON = responseAddress.Content;
                instagramlist objMdlInstagramMessageResponse = new instagramlist();
                objMdlInstagramMessageResponse = JsonConvert.DeserializeObject<instagramlist>(errornetsuiteJSON);
                if (responseAddress.StatusCode == HttpStatusCode.OK)
                {


                    msSQL = "truncate crm_smm_tinstagramdetails";
                    mnResult5 = objdbconn.ExecuteNonQuerySQL(msSQL);



                    if (mnResult5 == 1)
                    {
                        msSQL = "insert into crm_smm_tinstagramdetails(" +
                                             "id," +
                                             "user_name," +
                                            "instagram_type," +
                                             "created_date)" +
                                             "values(" +
                                             "'" + objMdlInstagramMessageResponse.id + "'," +
                                             "'" + objMdlInstagramMessageResponse.username + "'," +
                                            " 'Page', " +
                                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

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

                msSQL = " select id,instagram_type,user_name from crm_smm_tinstagramdetails";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows)
                {
                    values.id = objOdbcDataReader ["id"].ToString();
                    values.instagram_type = objOdbcDataReader ["instagram_type"].ToString();
                    values.username = objOdbcDataReader ["user_name"].ToString();
                }

                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Error While Fetching Instagram Summary";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Instagram/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }



        }
        public void DaGetInstagramProfile(MdlInstagram values)
        {
            try
            {
                result objresult = new result();
                instagramconfiguration getinstagramcredentials = instagramcredentials();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string requestAddressURL = "https://graph.instagram.com/me/media?fields=id,caption,media_type,media_url,timestamp&access_token=" + getinstagramcredentials.access_token + "";
                var clientAddress = new RestClient(requestAddressURL);
                var requestAddress = new RestRequest(Method.GET);
                IRestResponse responseAddress = clientAddress.Execute(requestAddress);
                string address_erpid = responseAddress.Content;
                string errornetsuiteJSON = responseAddress.Content;
                instagramprofile1_list objinstagramprofile1_list = new instagramprofile1_list();
                instagramprofile_list objMdlInstagramMessageResponse = new instagramprofile_list();
                objMdlInstagramMessageResponse = JsonConvert.DeserializeObject<instagramprofile_list>(errornetsuiteJSON);
                //var get_picturelist = new List<pictureList>();
                //var get_videoList = new List<videoList>();
                //foreach (var item in objMdlInstagramMessageResponse.data)
                //{
                //    if (item.media_type == "IMAGE")
                //    {
                //        get_picturelist.Add(new pictureList
                //        {
                //            media_url = item.media_url
                //        });
                //    }
                //    else
                //    {
                //        get_videoList.Add(new videoList
                //        {
                //            media_url = item.media_url
                //        });
                //    }
                //}
                //objinstagramprofile1_list.data = get_picturelist;
                //objinstagramprofile1_list.videoData = get_videoList;
                if (responseAddress.StatusCode == HttpStatusCode.OK)
                {
                    msSQL = "truncate crm_smm_tinstagramdtl";
                    mnResult5 = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult5 == 1)
                    {

                        var instagramlist = objMdlInstagramMessageResponse.data;

                        // Convert emoji to HTML entity code

                        foreach (var item in instagramlist)

                        {
                            string htmlEntityCode = System.Net.WebUtility.HtmlEncode(item.caption);

                            Console.WriteLine("Original Emoji: " + item.caption);
                            Console.WriteLine("HTML Entity Code: " + htmlEntityCode);

                            msSQL = "insert into crm_smm_tinstagramdtl(" +
                                                 "post_id," +
                                                 "post_type," +
                                                "post_url," +
                                                "caption," +
                                                 "postcreated_time)" +
                                                 "values(" +
                                                 "'" + item.id + "'," +
                                                 "'" + item.media_type + "'," +
                                                 "'" + item.media_url + "'," +
                                                  "'" + htmlEntityCode + "'," +
                                                 "'" + item.timestamp.ToString("yyyy-MM-dd HH:mm:ss") + "')";

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

                msSQL = "select (select ifnull(count(post_type),0) from crm_smm_tinstagramdtl where post_type='IMAGE') as image_count,(select ifnull(count(post_type),0) " +
                     "from crm_smm_tinstagramdtl where post_type='VIDEO') as video_count,(select ifnull(count(post_type),0) from crm_smm_tinstagramdtl ) as total_count ;";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows)
                {
                    values.image_count = objOdbcDataReader ["image_count"].ToString();
                    values.video_count = objOdbcDataReader ["video_count"].ToString();
                    values.total_count = objOdbcDataReader ["total_count"].ToString();

                }



                msSQL = "select *from crm_smm_tinstagramdtl";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<instagram_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new instagram_list
                        {
                            instagram_gid = dt["instagram_gid"].ToString(),
                            post_id = dt["post_id"].ToString(),
                            post_type = dt["post_type"].ToString(),
                            post_url = dt["post_url"].ToString(),
                            caption = dt["caption"].ToString(),
                            postcreated_time = dt["postcreated_time"].ToString()

                        });
                        values.instagram_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Error While Fetching Instagram Profile";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Instagram/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
        }
        public instagramconfiguration instagramcredentials()
        {
            instagramconfiguration getinstagramcredentials = new instagramconfiguration();
            try
            { 
            msSQL = " select access_token from crm_smm_tinstagramservice";
            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
            if (objOdbcDataReader .HasRows == true)
            {
                 
                getinstagramcredentials.access_token = objOdbcDataReader ["access_token"].ToString();
                 
            }
            else
            {

            }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********"  + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Instagram/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            return getinstagramcredentials;
        }
    }
}
