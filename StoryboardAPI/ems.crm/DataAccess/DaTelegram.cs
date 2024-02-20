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
using System.Web.Services.Description;
using static OfficeOpenXml.ExcelErrorValue;
using System.Web.Http.Results;




namespace ems.crm.DataAccess
{
    public class DaTelegram
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, lsupload_path, lsupload_type, msGetModule2employee_gid, final_path;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        int mnResult6;

        public telegramlist DaGetTelegram()
        {

            result objresult = new result();
            telegramconfiguration gettelegramcredentials = telegramcredentials();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string requestAddressURL = "https://api.telegram.org/"+ gettelegramcredentials.bot_id + "/getChat?chat_id="+ gettelegramcredentials.chat_id + "";
            var clientAddress = new RestClient(requestAddressURL);
            var requestAddress = new RestRequest(Method.GET);
            IRestResponse responseAddress = clientAddress.Execute(requestAddress);
            string address_erpid = responseAddress.Content;
            string errornetsuiteJSON = responseAddress.Content;
            telegramlist objMdlTelegramMessageResponse = new telegramlist();
            objMdlTelegramMessageResponse = JsonConvert.DeserializeObject<telegramlist>(errornetsuiteJSON);
            if (responseAddress.StatusCode == HttpStatusCode.OK)
            {
                msSQL = "truncate crm_smm_telegramdetails" ;
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = "insert into crm_smm_telegramdetails(" +
                                         "id," +
                                         "telegram_type," +
                                         "page_name," +
                                         "page_link," +
                                         "bot_name," +
                                         "created_date)" +
                                         "values(" +
                                         "'" + objMdlTelegramMessageResponse.result.id + "'," +
                                       " 'Channel', " +
                                         "'" + objMdlTelegramMessageResponse.result.title + "'," +
                                        "'" + objMdlTelegramMessageResponse.result.invite_link + "'," +
                                         "'" + objMdlTelegramMessageResponse.result.username + "'," +
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
            return objMdlTelegramMessageResponse;



        }
        public void DaGetTelegramdetails(telegramlist values)
        {
            try
            {
                 
                msSQL = " select telegram_gid,id,page_name,page_link,bot_name from crm_smm_telegramdetails";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows)
                {
                    values.telegram_gid = objOdbcDataReader ["telegram_gid"].ToString();
                    values.id = objOdbcDataReader ["id"].ToString();
                    values.title = objOdbcDataReader ["page_name"].ToString();
                    values.invite_link = objOdbcDataReader ["page_link"].ToString();
                    values.bot_name = objOdbcDataReader ["bot_name"].ToString();
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Telegram Detail!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            } 
        }
        public void DaTelegramUpload(HttpRequest httpRequest, string user_gid, result objResult)
        {

            //uploadimageproductcategorycreationSummary objdocumentmodel = new uploadimageproductcategorycreationSummary();
            HttpFileCollection httpFileCollection;
            telegramconfiguration gettelegramcredentials = telegramcredentials();

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            HttpPostedFile httpPostedFile;

            string lspath, lspath1;
            string msGetGid;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            string telegram_caption = httpRequest.Form[0];
            //path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            MemoryStream ms = new MemoryStream();

            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        if (FileExtension == ".jpg" | FileExtension == ".png" | FileExtension == ".jpeg" | FileExtension == ".gif" | FileExtension == ".jfif" | FileExtension == ".JPG" | FileExtension == ".JPEG" | FileExtension == ".JIFF" | FileExtension == ".TIFF" | FileExtension == ".PNG" | FileExtension == ".GIF" | FileExtension == ".JFIF" | FileExtension == ".webp" | FileExtension == ".WEBP")
                        {

                            bool status1;
                            status1 = objcmnfunctions.UploadStream(ConfigurationManager.AppSettings["blob_containername"],
                                 "Telegram/" + msdocument_gid + FileExtension,
                                 FileExtension, ms);
                            ms.Close();
                            final_path = ConfigurationManager.AppSettings["blob_containername"] + "/Telegram/";
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                            var client = new RestClient("https://api.telegram.org");
                            var request = new RestRequest("/" + gettelegramcredentials.bot_id + "/sendPhoto?chat_id=" + gettelegramcredentials.chat_id + "&caption=" + telegram_caption + "", Method.POST);
                            string filePath = Path.Combine(ConfigurationManager.AppSettings["blob_imagepath1"],
                                                            final_path, msdocument_gid + FileExtension +
                                                            ConfigurationManager.AppSettings["blob_imagepath2"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath3"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath4"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath5"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath6"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath7"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath8"]);

                            request.AddParameter("photo", filePath);
                            //request.AddFile("photo", final_path);
                            request.AlwaysMultipartFormData = true;
                            IRestResponse response = client.Execute(request);

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                msSQL = " insert into crm_smm_ttelegram(" +
                          " message_content," +
                         " upload_path," +
                          " upload_type," +
                          " created_by," +
                          " created_date)" +
                                " values(" +
                                " '" + telegram_caption + "'," +
                                " '" + filePath + "'," +
                                "'Image',";
                                msSQL += "'" + user_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult != 0)
                                {
                                    objResult.status = true;
                                    objResult.message = "Posted in Telegram Successfully !!";
                                }
                                else
                                {
                                    objResult.status = false;
                                    objResult.message = "Error While Storage in Local Path !!";
                                }
                            }
                            else
                            {
                                objResult.status = false;
                                objResult.message = "Error While Posting in Telegram !!";
                            }
                        }


                        else if (FileExtension == ".mp4" | FileExtension == ".MP4" | FileExtension == ".avi" | FileExtension == ".mkv" | FileExtension == ".wmv" | FileExtension == ".mov" | FileExtension == ".WebM" | FileExtension == ".flv" | FileExtension == ".hevc" | FileExtension == ".vpg")

                        {

                            bool status1;
                            status1 = objcmnfunctions.UploadStream(ConfigurationManager.AppSettings["blob_containername"],
                                 "Telegram/" + msdocument_gid + FileExtension,
                                 FileExtension, ms);
                            ms.Close();
                            final_path = ConfigurationManager.AppSettings["blob_containername"] + "/Telegram/";
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                            var client = new RestClient("https://api.telegram.org");
                            var request = new RestRequest("/" + gettelegramcredentials.bot_id + "/sendVideo?chat_id=" + gettelegramcredentials.chat_id + "&caption=" + telegram_caption + "", Method.POST);
                            string filePath = Path.Combine(ConfigurationManager.AppSettings["blob_imagepath1"],
                                                            final_path, msdocument_gid + FileExtension +
                                                            ConfigurationManager.AppSettings["blob_imagepath2"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath3"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath4"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath5"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath6"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath7"] + '&' +
                                                            ConfigurationManager.AppSettings["blob_imagepath8"]);

                            request.AddParameter("video", filePath);
                            //request.AddFile("photo", final_path);
                            request.AlwaysMultipartFormData = true;
                            IRestResponse response = client.Execute(request);

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                msSQL = " insert into crm_smm_ttelegram(" +
                         " message_content," +
                        " upload_path," +
                         " upload_type," +
                         " created_by," +
                         " created_date)" +
                               " values(" +
                               " '" + telegram_caption + "'," +
                               " '" + filePath + "'," +
                               "'Video',";
                                msSQL += "'" + user_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult != 0)
                                {
                                    objResult.status = true;
                                    objResult.message = "Posted in Telegram Successfully !!";
                                }
                                else
                                {
                                    objResult.status = false;
                                    objResult.message = "Error While Storage in Local Path !!";
                                }
                            }
                            else
                            {
                                objResult.status = false;
                                objResult.message = "Error While Posting in Telegram !!";
                            }

                        }



                    }
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error While Posting in Telegram !!";
                }
            }
            catch (Exception ex)
            {
                objResult.status = false;
                //objResult.message = ex.ToString();
                objResult.message = "Exception occured while Uploading Telegram!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + objResult.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            //return true;

        }
        public void DaTelegrammessage(string user_gid, message_list values, result objResult)
        {
                 
                try
                {
                    telegramconfiguration gettelegramcredentials = telegramcredentials();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var client = new RestClient("https://api.telegram.org");
                    var request = new RestRequest("/" + gettelegramcredentials.bot_id + "/sendMessage?chat_id=" + gettelegramcredentials.chat_id + "&text=" + values.telegram_caption + "", Method.POST);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        msSQL = " insert into crm_smm_ttelegram(" +
                                " upload_type," +
                               " message_content," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               " 'Text', " +
                               " '" + values.telegram_caption + "',";
                        msSQL += "'" + user_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        objResult.status = true;
                        objResult.message = "Posted in Telegram Successfully !!";
                    }
                    else
                    {
                        objResult.status = false;
                        objResult.message = "Error While Posting in Telegram !!";
                    }
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                //objResult.message = ex.ToString();
                values.message = "Exception occured while Getting Telegram Message!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }  
        }
        public void DaGetTelegramImage(MdlTelegram values)
        {
            try
            {
                 
                msSQL = "select (select ifnull(count(upload_type),0) from crm_smm_ttelegram where upload_type='Image') as image_count,(select ifnull(count(upload_type),0) " +
          "from crm_smm_ttelegram where upload_type='Video') as video_count,(select ifnull(count(upload_type),0)from crm_smm_ttelegram ) as total_count,(select ifnull(count(upload_type),0) from crm_smm_ttelegram where upload_type='Text') as text_count ;";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows)
                {
                    values.image_count = objOdbcDataReader ["image_count"].ToString();
                    values.video_count = objOdbcDataReader ["video_count"].ToString();
                    values.text_count = objOdbcDataReader ["text_count"].ToString();
                    values.total_count = objOdbcDataReader ["total_count"].ToString();


                }

                 

                msSQL = "  select id,upload_path,message_content,upload_type, created_date FROM crm_smm_ttelegram  order by id desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<telegramsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new telegramsummary_list
                        {
                            id = dt["id"].ToString(),
                            upload_path = dt["upload_path"].ToString(),
                            telegram_caption = dt["message_content"].ToString(),
                            upload_type = dt["upload_type"].ToString(),
                            created_date = dt["created_date"].ToString(),


                        });
                        values.telegramsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Telegram Image!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaTelegramrepostupload( telegramsummary_list values, string user_gid, result objResult)
        {
            try
            {
                 
                telegramconfiguration gettelegramcredentials = telegramcredentials();
                string lspath, lspath1;

                int count = 0;
                msSQL = " select upload_path,upload_type from crm_smm_ttelegram" +
                   "  where id='" + values.image_id + "' ";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows)
                {
                    lsupload_path = objOdbcDataReader ["upload_path"].ToString();
                    lsupload_type = objOdbcDataReader ["upload_type"].ToString();

                     

                    //lspath = ConfigurationManager.AppSettings["telegram_repost"]  + lsupload_path;

                    if (lsupload_type == "Image")
                    {

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var client = new RestClient("https://api.telegram.org");
                        var request = new RestRequest("/" + gettelegramcredentials.bot_id + "/sendPhoto?chat_id=" + gettelegramcredentials.chat_id + "&caption=" + values.telegram_caption + "", Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("photo", lsupload_path);
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            msSQL = " insert into crm_smm_ttelegram(" +
                      " message_content," +
                     " upload_path," +
                      " upload_type," +
                      " created_by," +
                      " created_date)" +
                            " values(" +
                            " '" + values.telegram_caption + "'," +
                            " '" + lsupload_path + "'," +
                            "'Image',";
                            msSQL += "'" + user_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                objResult.status = true;
                                objResult.message = "Posted in Telegram Successfully !!";
                            }
                            else
                            {
                                objResult.status = false;
                                objResult.message = "Error While Storage in Local Path !!";
                            }
                        }
                        else
                        {
                            objResult.status = false;
                            objResult.message = "Error While Posting in Telegram !!";
                        }
                    }


                    else if (lsupload_type == "Video")

                    {

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var client = new RestClient("https://api.telegram.org");
                        var request = new RestRequest("/" + gettelegramcredentials.bot_id + "/sendVideo?chat_id=" + gettelegramcredentials.chat_id + "&caption=" + values.telegram_caption + "", Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("video", lsupload_path);
                        IRestResponse response = client.Execute(request);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            msSQL = " insert into crm_smm_ttelegram(" +
                     " message_content," +
                    " upload_path," +
                     " upload_type," +
                     " created_by," +
                     " created_date)" +
                           " values(" +
                           " '" + values.telegram_caption + "'," +
                           " '" + lsupload_path + "'," +
                           "'Video',";
                            msSQL += "'" + user_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                objResult.status = true;
                                objResult.message = "Posted in Telegram Successfully !!";
                            }
                            else
                            {
                                objResult.status = false;
                                objResult.message = "Error While Storage in Local Path !!";
                            }
                        }
                        else
                        {
                            objResult.status = false;
                            objResult.message = "Error While Posting in Telegram !!";
                        }

                    }

                    else if (lsupload_type == "Text")

                    {

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var client = new RestClient("https://api.telegram.org");
                        var request = new RestRequest("/" + gettelegramcredentials.bot_id + "/sendMessage?chat_id=" + gettelegramcredentials.chat_id + "&text=" + values.telegram_caption + "", Method.POST);
                        IRestResponse response = client.Execute(request);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            msSQL = " insert into crm_smm_ttelegram(" +
                     " message_content," +
                     " upload_type," +
                     " created_by," +
                     " created_date)" +
                           " values(" +
                           " '" + values.telegram_caption + "'," +
                           "'Text',";
                            msSQL += "'" + user_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                objResult.status = true;
                                objResult.message = "Posted in Telegram Successfully !!";
                            }
                            else
                            {
                                objResult.status = false;
                                objResult.message = "Error While Storage in Local Path !!";
                            }
                        }
                        else
                        {
                            objResult.status = false;
                            objResult.message = "Error While Posting in Telegram !!";
                        }

                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while While Upoading Telegram Repost!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public telegramconfiguration telegramcredentials()
        {
            telegramconfiguration gettelegramcredentials = new telegramconfiguration();

            msSQL = " select bot_id,chat_id from crm_smm_ttelegramservice";
            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
            if (objOdbcDataReader .HasRows == true)
            {
                 
                gettelegramcredentials.bot_id = objOdbcDataReader ["bot_id"].ToString();
                gettelegramcredentials.chat_id = objOdbcDataReader ["chat_id"].ToString();

                 
            }
            else
            {

            }
            objOdbcDataReader .Close();
            return gettelegramcredentials;

        }


    }
}