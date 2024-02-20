using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.crm.Models;
using System.Runtime.InteropServices;



namespace ems.crm.DataAccess
{
    public class DaCampaignService
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        int mnResult;
        string msGetGid, msGetGid1, lscustomertype_gid, lsmodule_gid, lsshopify_flag, lsupdated_date, lscreated_date, lsupdated_by, lscreated_by, lscustomer_type;

        public void DaGetWhatsappSummary(MdlCampaignService values)
        {
            try
            {
                msSQL = " select s_no,workspace_id,channel_id,access_token,channelgroup_id,mobile_number, whatsapp_status," +
                        " channel_name,created_by,created_date from crm_smm_whatsapp_service limit 1";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<campaignservice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new campaignservice_list
                        {
                            s_no = dt["s_no"].ToString(),
                            workspace_id = dt["workspace_id"].ToString(),
                            channel_id = dt["channel_id"].ToString(),
                            access_token = dt["access_token"].ToString(),
                            channelgroup_id = dt["channelgroup_id"].ToString(),
                            mobile_number = dt["mobile_number"].ToString(),
                            channel_name = dt["channel_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            whatsapp_status = dt["whatsapp_status"].ToString(),
                        });
                        values.campaignservice_list = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting whatsapp summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaUpdateWhatsappService(string user_gid, campaignservice_list values)

        {
            try
            {
                if (values.whatsapp_status == "Y")
                {
                    if (values.whatsapp_id == null || values.whatsapp_id == "")
                    {
                        msSQL = " insert into crm_smm_whatsapp_service(" +
                        " workspace_id," +
                        " channel_id," +
                        " access_token," +
                        " mobile_number," +
                        " channel_name," +
                        " channelgroup_id," +
                        " created_by," +
                        " whatsapp_status," +
                        " created_date)" +
                        " values(" +
                        "'" + values.workspace_id + "'," +
                        "'" + values.channel_id + "'," +
                        "'AccessKey " + values.whatsapp_accesstoken + "'," +
                        "'" + values.mobile_number + "'," +
                        "'" + values.channel_name + "'," +
                        "'" + values.channelgroup_id + "'," +
                        "'" + user_gid + "'," +
                        "'Y'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        if (mnResult != 0)
                        {
                            msSQL = " update adm_mst_tmodule set shopify_flag = 'Y' where module_name = 'Whatsapp'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 0)
                            {
                                msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Whatsapp'";
                                lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                                lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                                if (lsshopify_flag == "N")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    if (mnResult != 0)
                                    {
                                        values.status = true;
                                        values.message = "Whatsapp Credentials Updated Successfully !!";
                                    }
                                }
                                else if (lsshopify_flag == "")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    values.status = true;
                                    values.message = "Shopify Credentials Updated Successfully!!";
                                }
                                else
                                {
                                    values.status = false;
                                    values.message = "Error While Updating Whatsapp Credentials!!";
                                }

                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Whatsapp Credentials !!";
                            }
                        }
                    }
                    else
                    {
                        msSQL = " update  crm_smm_whatsapp_service set " +

                                " workspace_id = '" + values.workspace_id + "'," +
                                " channel_id = '" + values.channel_id + "'," +
                                " access_token = '" + values.whatsapp_accesstoken + "'," +
                                " mobile_number = '" + values.mobile_number + "'," +
                                " channel_name = '" + values.channel_name + "'," +
                                " channelgroup_id =  '" + values.channelgroup_id + "'," +
                                " updated_by = '" + user_gid + "'," +
                                "whatsapp_status='Y'," +
                                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where s_no='" + values.whatsapp_id + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msSQL = " update adm_mst_tmodule set shopify_flag = 'Y' where module_name = 'Whatsapp'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Whatsapp'";
                                lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                                lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                                if (lsshopify_flag == "N")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    if (mnResult != 0)
                                    {
                                        values.status = true;
                                        values.message = "Whatsapp Credentials Updated Successfully !!";
                                    }
                                }
                                else if (lsshopify_flag == "")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    values.status = true;
                                    values.message = "Whatsapp Credentials Updated Successfully!!";
                                }
                                else
                                {
                                    values.status = false;
                                    values.message = "Error While Updating Whatsapp Credentials!!";
                                }
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Whatsapp Credentials !!";
                            }
                        }
                    }
                }
                else
                {
                    msSQL = " update crm_smm_whatsapp_service set whatsapp_status = 'N'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update adm_mst_tmodule set shopify_flag = 'N' where module_name = 'Whatsapp'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Disabled Successfully!!";
                }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while updating whatsapp service!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaGetShopifySummary(MdlCampaignService values)
        {
            try
            {
                msSQL = " select s_no,access_token,shopify_store_name,store_month_year,created_by,created_date,shopify_status" +
                        " from crm_smm_shopify_service limit 1 ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<shopifycampaignservice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new shopifycampaignservice_list
                        {
                            s_no = dt["s_no"].ToString(),
                            shopify_access_token = dt["access_token"].ToString(),
                            shopify_store_name = dt["shopify_store_name"].ToString(),
                            store_month_year = dt["store_month_year"].ToString(),
                            shopify_created_by = dt["created_by"].ToString(),
                            shopify_created_date = dt["created_date"].ToString(),
                            shopify_status = dt["shopify_status"].ToString(),
                        });
                        values.shopifycampaignservice_list = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting shopify summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaGetMailSummary(MdlCampaignService values)
        {
            try
            {
                msSQL = " select access_token,base_url,created_by,s_no,created_date,receiving_domain,email_status," +
                        " sending_domain,email_username from crm_smm_mail_service limit 1 ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<mailcampaignservice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new mailcampaignservice_list
                        {
                            mail_access_token = dt["access_token"].ToString(),
                            mail_base_url = dt["base_url"].ToString(),
                            mail_created_by = dt["created_by"].ToString(),
                            s_no = dt["s_no"].ToString(),
                            mail_created_date = dt["created_date"].ToString(),
                            receiving_domain = dt["receiving_domain"].ToString(),
                            sending_domain = dt["sending_domain"].ToString(),
                            email_username = dt["email_username"].ToString(),
                            email_status = dt["email_status"].ToString(),
                        });
                        values.mailcampaignservice_list = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting mail summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaUpdateShopifyService(string user_gid, shopifyservcie_list values)
        {
            try
            {
                if (values.shopify_status == "Y")
                {
                    if (values.shopify_id == null || values.shopify_id == "")
                    {


                        msSQL = " insert into crm_smm_shopify_service(" +
                                " access_token," +
                                " shopify_store_name," +
                                " store_month_year," +
                                " created_by," +
                                "shopify_status," +
                                " created_date)" +
                                " values(" +
                                "'" + values.shopify_accesstoken + "'," +
                                 " '" + values.shopify_store_name + "'," +
                                " '" + values.store_month_year + "',";
                        msSQL += "'" + user_gid + "'," +
                            "'Y'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = " update adm_mst_tmodule set shopify_flag = 'Y' where module_name = 'Shopify'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Shopify'";
                                lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                                lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                                if (lsshopify_flag == "N")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    if (mnResult != 0)
                                    {
                                        values.status = true;
                                        values.message = "Shopify Credentials Updated Successfully !!";
                                    }
                                }
                                else if (lsshopify_flag == "")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    values.status = true;
                                    values.message = "Shopify Credentials Updated Successfully!!";
                                }
                                else
                                {
                                    values.status = false;
                                    values.message = "Error While Updating Shopify Credentials!!";
                                }
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Shopify Credentials!!";
                            }

                        }
                    }
                    else
                    {
                        msSQL = " update  crm_smm_shopify_service set " +

                        " access_token = '" + values.shopify_accesstoken + "'," +

                        " shopify_store_name = '" + values.shopify_store_name + "'," +
                        " store_month_year = '" + values.store_month_year + "'," +

                        " updated_by = '" + user_gid + "'," +
                        "shopify_status='Y'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where s_no='" + values.shopify_id + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = " update adm_mst_tmodule set shopify_flag = 'Y' where module_name = 'Shopify'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Shopify'";
                                lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                                lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                                if (lsshopify_flag == "N")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    if (mnResult != 0)
                                    {
                                        values.status = true;
                                        values.message = "Shopify Credentials Updated Successfully !!";
                                    }
                                }
                                else if (lsshopify_flag == "")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    values.status = true;
                                    values.message = "Shopify Credentials Updated Successfully!!";
                                }
                                else
                                {
                                    values.status = false;
                                    values.message = "Error While Updating Shopify Credentials !!";
                                }
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Shopify Credentials !!";
                            }
                        }
                    }
                }

                else
                {
                    msSQL = "update crm_smm_shopify_service set shopify_status = 'N'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update adm_mst_tmodule set shopify_flag = 'N' where module_name = 'Shopify'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //}
                    //else
                    //{
                    values.status = true;
                    values.message = "Disabled Successfully!!";
                }
                    


                
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while updating shopify service!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaUpdateEmailService(string user_gid, emailservice_list values)
        {
            try
            {
                if (values.email_status == "Y")
                {
                    if (values.email_id == null || values.email_id == "")
                    {


                        msSQL = " insert into crm_smm_mail_service(" +
                                " access_token," +
                                " base_url," +
                                " receiving_domain," +
                                " sending_domain," +
                                " email_username," +
                                " created_by," +
                                " email_status," +
                                " created_date)" +
                                " values(" +
                                "'" + values.mail_access_token + "'," +
                                " '" + values.mail_base_url + "'," +
                                " '" + values.receiving_domain + "'," +
                                " '" + values.sending_domain + "'," +
                                " '" + values.email_username + "',";
                        msSQL += "'" + user_gid + "'," +
                             "'Y'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = " update adm_mst_tmodule set shopify_flag = 'Y' where module_name = 'Email'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Email'";
                                lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                                lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                                if (lsshopify_flag == "N")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    if (mnResult != 0)
                                    {
                                        values.status = true;
                                        values.message = "Email Credentials Updated Successfully !!";
                                    }
                                }
                                else if (lsshopify_flag == "")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    values.status = true;
                                    values.message = "Shopify Credentials Updated Successfully!!";
                                }
                                else
                                {
                                    values.status = false;
                                    values.message = "Error While Updating Email Credentials!!";
                                }
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Email Credentials !!";
                            }

                        }
                    }
                    else
                    {
                        msSQL = " update  crm_smm_mail_service set " +

                        " access_token = '" + values.mail_access_token + "'," +
                        " base_url = '" + values.mail_base_url + "'," +
                        " updated_by = '" + user_gid + "'," +
                        " receiving_domain= '" + values.receiving_domain + "'," +
                        " sending_domain= '" + values.sending_domain + "'," +
                        " email_status= 'Y'," +
                        " email_username= '" + values.email_username + "'," +

                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where s_no='" + values.email_id + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = " update adm_mst_tmodule set shopify_flag = 'Y' where module_name = 'Email'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Email'";
                                lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                                lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                                if (lsshopify_flag == "N")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    if (mnResult != 0)
                                    {
                                        values.status = true;
                                        values.message = "Email Credentials Updated Successfully !!";
                                    }
                                }
                                else if (lsshopify_flag == "")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    values.status = true;
                                    values.message = "Shopify Credentials Updated Successfully!!";
                                }
                                else
                                {
                                    values.status = false;
                                    values.message = "Error While Updating Email Credentials!!";
                                }
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Email Credentials !!";
                            }
                        }
                    }
                    
                }
                else
                {
                    msSQL = "update crm_smm_mail_service set email_status = 'N'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update adm_mst_tmodule set shopify_flag = 'N' where module_name = 'Email '";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //}
                    //else
                    //{
                    values.status = true;
                    values.message = "Disabled Successfully!!";
                }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while updating email service!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }

        public void DaGetFacebookServiceSummary(MdlCampaignService values)
        {
            try
            {
                msSQL = " select access_token,page_id,s_no,facebook_status from crm_smm_tfacebookservice limit 1 ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<facebookcampaignservice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new facebookcampaignservice_list
                        {
                            facebook_access_token = dt["access_token"].ToString(),
                            facebook_page_id = dt["page_id"].ToString(),
                            facebook_id = dt["s_no"].ToString(),
                            facebook_status = dt["facebook_status"].ToString(),
                        });
                        values.facebookcampaignservice_list = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting face book service summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaUpdateFacebookService(string user_gid, facebookcampaignservice_list values)
        {
            try
            {
                if (values.facebook_status == "Y")
                {
                    if (values.facebook_id == null || values.facebook_id == "")
                    {


                        msSQL = " insert into crm_smm_tfacebookservice(" +
                                " page_id," +
                                " access_token," +
                                " created_by," +
                                " facebook_status," +
                                " created_date)" +
                                " values(" +
                                "'" + values.facebook_page_id + "'," +
                                "'" + values.facebook_access_token + "',";
                        msSQL += "'" + user_gid + "'," +
                                 "'Y'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Facebook'";
                            lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                            lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                            if (lsshopify_flag == "N")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                if (mnResult != 0)
                                {
                                    values.status = true;
                                    values.message = "Facebook Credentials Updated Successfully !!";
                                }
                            }
                            else if (lsshopify_flag == "")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                values.status = true;
                                values.message = "Shopify Credentials Updated Successfully!!";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Facebook Credentials!!";
                            }
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Facebook Credentials !!";
                        }

                    }
                    else
                    {
                        msSQL = " update  crm_smm_tfacebookservice set " +

                                " access_token = '" + values.facebook_access_token + "'," +
                                " page_id = '" + values.facebook_page_id + "'," +
                                " updated_by = '" + user_gid + "'," +
                                " facebook_status = 'Y'," +
                                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where s_no='" + values.facebook_id + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Facebook'";
                            lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                            lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                            if (lsshopify_flag == "N")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                if (mnResult != 0)
                                {
                                    values.status = true;
                                    values.message = "Facebook Credentials Updated Successfully !!";
                                }
                            }
                            else if (lsshopify_flag == "")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                values.status = true;
                                values.message = "Shopify Credentials Updated Successfully!!";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Facebook Credentials!!";
                            }
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Facebook Credentials !!";
                        }
                    }
                }

                else
                {
                    msSQL = " update crm_smm_tfacebookservice set facebook_status = 'N'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update adm_mst_tmodule set shopify_flag = 'N' where module_name = 'Facebook'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Disabled Successfully!!";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while updating facebook service!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }

        public void DaGetLinkedinServiceSummary(MdlCampaignService values)
        {
            try
            {
                msSQL = " select access_token,s_no,linkedin_status from crm_smm_tlinkedinservice limit 1 ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<linkedincampaignservice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new linkedincampaignservice_list
                        {
                            linkedin_access_token = dt["access_token"].ToString(),
                            linkedin_id = dt["s_no"].ToString(),
                            linkedin_status = dt["linkedin_status"].ToString(),
                        });
                        values.linkedincampaignservice_list = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting linkedin service!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaUpdateLinkedinService(string user_gid, linkedincampaignservice_list values)
        {
            try
            {
                if (values.linkedin_status == "Y")
                {
                    if (values.linkedin_id == null || values.linkedin_id == "")
                    {


                        msSQL = " insert into crm_smm_tlinkedinservice(" +
                           " access_token," +
                           " created_by," +
                           " created_date)" +
                            " linkedin_status," +
                           " values(" +
                           "'" + values.linkedin_access_token + "',";
                        msSQL += "'" + user_gid + "'," +
                               "'Y'" +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Linkedin'";
                            lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                            lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                            if (lsshopify_flag == "N")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                if (mnResult != 0)
                                {
                                    values.status = true;
                                    values.message = "Linkedin Credentials Updated Successfully !!";
                                }
                            }
                            else if (lsshopify_flag == "")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                values.status = true;
                                values.message = "Shopify Credentials Updated Successfully!!";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Linkedin Credentials!!";
                            }
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Linedin Credentials !!";
                        }

                    }
                    else
                    {
                        msSQL = " update  crm_smm_tlinkedinservice set " +

                                " access_token = '" + values.linkedin_access_token + "'," +
                                " updated_by = '" + user_gid + "'," +
                                " linkedin_status = 'Y'," +
                                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where s_no='" + values.linkedin_id + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Linkedin'";
                            lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                            lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                            if (lsshopify_flag == "N")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                if (mnResult != 0)
                                {
                                    values.status = true;
                                    values.message = "Linkedin Credentials Updated Successfully !!";
                                }
                            }
                            else if (lsshopify_flag == "")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                values.status = true;
                                values.message = "Shopify Credentials Updated Successfully!!";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Linkedin Credentials!!";
                            }
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Linkedin Credentials !!";
                        }
                    }
                }

                else
                {
                    msSQL = " update crm_smm_tlinkedinservice set linkedin_status = 'N'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update adm_mst_tmodule set shopify_flag = 'N' where module_name = 'Linkedin'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Disabled Successfully!!";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while updatsing linked services!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaGetTelegramServiceSummary(MdlCampaignService values)
        {
            try
            {
                msSQL = " select bot_id,chat_id,s_no,telegram_status from crm_smm_ttelegramservice limit 1 ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<telegramcampaignservice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new telegramcampaignservice_list
                        {
                            bot_id = dt["bot_id"].ToString(),
                            chat_id = dt["chat_id"].ToString(),
                            telegram_id = dt["s_no"].ToString(),
                            telegram_status = dt["telegram_status"].ToString(),
                        });
                        values.telegramcampaignservice_list = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting telegram service summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaUpdateTelegramService(string user_gid, telegramcampaignservice_list values)
        {
            try
            {
                if (values.telegram_status == "Y")
                {
                    if (values.telegram_id == null || values.telegram_id == "")
                    {


                        msSQL = " insert into crm_smm_ttelegramservice(" +
                                " bot_id," +
                                " chat_id," +
                                " created_by," +
                                " telegram_status," +
                                " created_date)" +
                                " values(" +
                                "'" + values.bot_id + "'," +
                                   "'" + values.chat_id + "',";
                        msSQL += "'" + user_gid + "'," +
                                    "'Y'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = " update adm_mst_tmodule set shopify_flag = 'Y' where module_name = 'Telegram'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Telegram'";
                                lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                                lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                                if (lsshopify_flag == "N")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    if (mnResult != 0)
                                    {
                                        values.status = true;
                                        values.message = "Telegram Credentials Updated Successfully !!";
                                    }
                                }
                                else if (lsshopify_flag == "")
                                {
                                    msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    values.status = true;
                                    values.message = "Shopify Credentials Updated Successfully!!";
                                }
                                else
                                {
                                    values.status = false;
                                    values.message = "Error While Updating Telegram Credentials!!";
                                }
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Telegram Credentials !!";
                            }

                        }
                    }
                    else
                    {
                        msSQL = " update  crm_smm_ttelegramservice set " +
                        " bot_id = '" + values.bot_id + "'," +
                        " chat_id = '" + values.chat_id + "'," +
                        " updated_by = '" + user_gid + "'," +
                        " telegram_status='Y'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where s_no='" + values.telegram_id + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Telegram'";
                            lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                            lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                            if (lsshopify_flag == "N")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                if (mnResult != 0)
                                {
                                    values.status = true;
                                    values.message = "Telegram Credentials Updated Successfully !!";
                                }
                            }
                            else if (lsshopify_flag == "")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                values.status = true;
                                values.message = "Shopify Credentials Updated Successfully!!";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Telegram Credentials!!";
                            }
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Telegram Credentials !!";
                        }
                    }
                }

                else
                {
                    msSQL = " update crm_smm_ttelegramservice set telegram_status = 'N'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update adm_mst_tmodule set shopify_flag = 'N' where module_name = 'Telegram'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Disabled Successfully!!";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while updating telegram service!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaGetInstagramServiceSummary(MdlCampaignService values)
        {
            try
            {
                msSQL = " select access_token,s_no,instagram_status from crm_smm_tinstagramservice limit 1 ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<instagramcampaignservice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new instagramcampaignservice_list
                        {
                            instagram_access_token = dt["access_token"].ToString(),
                            instagram_id = dt["s_no"].ToString(),
                            instagram_status = dt["instagram_status"].ToString(),
                        });
                        values.instagramcampaignservice_list = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting instagram service!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaUpdateInstagramService(string user_gid, instagramcampaignservice_list values)
        {
            try
            {
                if (values.instagram_status == "Y")
                {
                    if (values.instagram_id == null || values.instagram_id == "")
                    {


                        msSQL = " insert into crm_smm_tinstagramservice(" +
                          " access_token," +
                          " created_by," +
                          " instagram_status," +
                          " created_date)" +
                          " values(" +
                          "'" + values.instagram_access_token + "',";
                        msSQL += "'" + user_gid + "'," +
                              "'Y'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Instagram'";
                            lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                            lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                            if (lsshopify_flag == "N")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                if (mnResult != 0)
                                {
                                    values.status = true;
                                    values.message = "Instagram Credentials Updated Successfully !!";
                                }
                            }
                            else if (lsshopify_flag == "")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                values.status = true;
                                values.message = "Shopify Credentials Updated Successfully!!";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Instagram Credentials!!";
                            }
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Instagram Credentials !!";
                        }

                    }
                    else
                    {
                        msSQL = " update  crm_smm_tinstagramservice set " +

                        " access_token = '" + values.instagram_access_token + "'," +
                        " updated_by = '" + user_gid + "'," +
                        "instagram_status = 'Y'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where s_no='" + values.instagram_id + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = "select module_gid_parent from adm_mst_tmodule  where module_name ='Instagram'";
                            lsmodule_gid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select shopify_flag from adm_mst_tmodule  where module_gid = '" + lsmodule_gid + " '";
                            lsshopify_flag = objdbconn.GetExecuteScalar(msSQL);
                            if (lsshopify_flag == "N")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='Y' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                if (mnResult != 0)
                                {
                                    values.status = true;
                                    values.message = "Instagram Credentials Updated Successfully !!";
                                }
                            }
                            else if (lsshopify_flag == "")
                            {
                                msSQL = "update adm_mst_tmodule set shopify_flag='' where module_gid ='" + lsmodule_gid + " '";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                values.status = true;
                                values.message = "Shopify Credentials Updated Successfully!!";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Instagram Credentials!!";
                            }
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Instagram Credentials !!";
                        }
                    }
                }

                else
                {
                    msSQL = " update crm_smm_tinstagramservice set instagram_status = 'N'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update adm_mst_tmodule set shopify_flag = 'N' where module_name = 'Instagram'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Disabled Successfully!!";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while updating instagram service!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaGetCustomerTypeSummary(MdlCampaignService values)
        {
            try
            {
                msSQL = "select customertype_gid,customer_type from crm_mst_tcustomertype ORDER BY customertype_gid ASC";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<customertype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new customertype_list
                        {
                            customertype_gid = dt["customertype_gid"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                        });
                        values.customertype_list = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting customer type!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaUpdateCustomerType(string user_gid, customertype_list values)
        {
            try
            {
                if (values.corporate_gid != null)
                {
                    msSQL = "select customer_type from crm_mst_tcustomertype where customertype_gid = 'BCRT240331000'";
                    string lscorporate_type = objdbconn.GetExecuteScalar(msSQL);

                    if (lscorporate_type != values.corporate_name)
                    {
                        msSQL = " select customertype_gid,customer_type,created_by,updated_by,created_date,updated_date" +
                          " from crm_mst_tcustomertype where " +
                          " customertype_gid='BCRT240331000'";
                        objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader.HasRows)
                        {
                            lscustomertype_gid = objOdbcDataReader["customertype_gid"].ToString();
                            lscustomer_type = objOdbcDataReader["customer_type"].ToString();
                            lscreated_by = objOdbcDataReader["created_by"].ToString();
                            lsupdated_by = objOdbcDataReader["updated_by"].ToString();
                            lscreated_date = objOdbcDataReader["created_date"].ToString();
                            lsupdated_date = objOdbcDataReader["updated_date"].ToString();
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("BCTL");
                        msSQL = " insert into crm_trn_tcustomertypelog(" +
                                " customertypelog_gid," +
                                " pre_customertype_gid," +
                                " pre_customertype," +
                                " curr_customertype," +
                                " updated_by, " +
                                " updated_date)" +
                                " values(" +
                                " '" + msGetGid + "'," +
                                " '" + lscustomertype_gid + "'," +
                                "'" + lscustomer_type + "'," +
                                "'" + values.corporate_name + "'," +
                                "'" + lscreated_by + "'," +
                                "STR_TO_DATE('" + lscreated_date + "', '%m/%d/%Y %h:%i:%s %p'))";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msSQL = " update  crm_mst_tcustomertype set " +
                                    " customer_type = '" + values.corporate_name + "'," +
                                    " updated_by = '" + user_gid + "'," +
                                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                    " where customertype_gid='" + values.corporate_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                msSQL = "update crm_trn_tleadbank set customer_type ='" + values.corporate_name + "' where customertype_gid='" + lscustomertype_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            if (mnResult == 1)
                            {
                                values.status = true;
                                values.message = "Customer Type Updated Successfully !!";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Customer Type !!";
                            }
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Adding Customer Type !!";
                        }
                    }


                }
                if (values.retailer_gid != null)
                {
                    msSQL = "select customer_type from crm_mst_tcustomertype where customertype_gid = 'BCRT240331001'";
                    string lscorporate_type = objdbconn.GetExecuteScalar(msSQL);

                    if (lscorporate_type != values.retailer_name)
                    {
                        msSQL = " select customertype_gid,customer_type,created_by,updated_by,created_date,updated_date" +
                          " from crm_mst_tcustomertype where " +
                          " customertype_gid='BCRT240331001'";
                        objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader.HasRows)
                        {
                            lscustomertype_gid = objOdbcDataReader["customertype_gid"].ToString();
                            lscustomer_type = objOdbcDataReader["customer_type"].ToString();
                            lscreated_by = objOdbcDataReader["created_by"].ToString();
                            lsupdated_by = objOdbcDataReader["updated_by"].ToString();
                            lscreated_date = objOdbcDataReader["created_date"].ToString();
                            lsupdated_date = objOdbcDataReader["updated_date"].ToString();
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("BCTL");
                        msSQL = " insert into crm_trn_tcustomertypelog(" +
                                " customertypelog_gid," +
                                " pre_customertype_gid," +
                                " pre_customertype," +
                                " curr_customertype," +
                                " updated_by, " +
                                " updated_date)" +
                                " values(" +
                                " '" + msGetGid + "'," +
                                " '" + lscustomertype_gid + "'," +
                                "'" + lscustomer_type + "'," +
                                "'" + values.retailer_name + "'," +
                                "'" + lscreated_by + "'," +
                                "STR_TO_DATE('" + lscreated_date + "', '%m/%d/%Y %h:%i:%s %p'))";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msSQL = " update  crm_mst_tcustomertype set " +
                                    " customer_type = '" + values.retailer_name + "'," +
                                    " updated_by = '" + user_gid + "'," +
                                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                    " where customertype_gid='" + values.retailer_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                msSQL = "update crm_trn_tleadbank set customer_type ='" + values.retailer_name + "' where customertype_gid='" + lscustomertype_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            if (mnResult == 1)
                            {
                                values.status = true;
                                values.message = "Customer Type Updated Successfully !!";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Customer Type !!";
                            }
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Adding Customer Type !!";
                        }
                    }

                }
                if (values.distributor_gid != null)
                {
                    msSQL = "select customer_type from crm_mst_tcustomertype where customertype_gid = 'BCRT240331002'";
                    string lscorporate_type = objdbconn.GetExecuteScalar(msSQL);

                    if (lscorporate_type != values.distributor_name)
                    {
                        msSQL = " select customertype_gid,customer_type,created_by,updated_by,created_date,updated_date" +
                          " from crm_mst_tcustomertype where " +
                          " customertype_gid='BCRT240331002'";
                        objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader.HasRows)
                        {
                            lscustomertype_gid = objOdbcDataReader["customertype_gid"].ToString();
                            lscustomer_type = objOdbcDataReader["customer_type"].ToString();
                            lscreated_by = objOdbcDataReader["created_by"].ToString();
                            lsupdated_by = objOdbcDataReader["updated_by"].ToString();
                            lscreated_date = objOdbcDataReader["created_date"].ToString();
                            lsupdated_date = objOdbcDataReader["updated_date"].ToString();
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("BCTL");
                        msSQL = " insert into crm_trn_tcustomertypelog(" +
                                " customertypelog_gid," +
                                " pre_customertype_gid," +
                                " pre_customertype," +
                                " curr_customertype," +
                                " updated_by, " +
                                " updated_date)" +
                                " values(" +
                                " '" + msGetGid + "'," +
                                " '" + lscustomertype_gid + "'," +
                                "'" + lscustomer_type + "'," +
                                "'" + values.distributor_name + "'," +
                                "'" + lscreated_by + "'," +
                                "STR_TO_DATE('" + lscreated_date + "', '%m/%d/%Y %h:%i:%s %p'))";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msSQL = " update  crm_mst_tcustomertype set " +
                                    " customer_type = '" + values.distributor_name + "'," +
                                    " updated_by = '" + user_gid + "'," +
                                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                    " where customertype_gid='" + values.distributor_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                msSQL = "update crm_trn_tleadbank set customer_type ='" + values.distributor_name + "' where customertype_gid='" + lscustomertype_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            if (mnResult == 1)
                            {
                                values.status = true;
                                values.message = "Customer Type Updated Successfully !!";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Updating Customer Type !!";
                            }
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Adding Customer Type !!";
                        }
                    }

                }

                //msSQL = " select customer_type from crm_mst_tcustomertype where customer_type = '" + values.customer_type + "'";
                //objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                //if (objOdbcDataReader.HasRows == false)
                //{ }

                objOdbcDataReader.Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Customer Type Details";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }
        public void DaUpdateLivechatService(string user_gid, livechatservice_list values)
        {

            if (values.livechat_id == null || values.livechat_id == "")
            {


                msSQL = " insert into crm_smm_tinlinechatservice(" +
                        " id," +
                        " access_token," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.id + "'," +
                           "'" + values.livechataccess_token + "',";
                msSQL += "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Livecha Credentials Updated Successfully !!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Livecha Credentials !!";
                }

            }
            else
            {
                msSQL = " update  crm_smm_tinlinechatservice set " +
                " id = '" + values.id + "'," +
                " access_token = '" + values.livechataccess_token + "'," +
                " updated_by = '" + user_gid + "'," +
                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where s_no='" + values.livechat_id + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Livecha Credentials Updated Successfully !!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Livecha Credentials !!";
                }
            }

        }
        public void DaGetLivechatServiceSummary(MdlCampaignService values)
        {
            msSQL = " select * from crm_smm_tinlinechatservice limit 1 ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmodulelist = new List<livechatservice_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getmodulelist.Add(new livechatservice_list
                    {
                        id = dt["id"].ToString(),
                        livechataccess_token = dt["access_token"].ToString(),
                        livechat_id = dt["s_no"].ToString(),
                    });
                    values.livechatservice_list = getmodulelist;
                }
            }
            dt_datatable.Dispose();
        }
    }
}
