﻿using ems.crm.Models;
using ems.system.Models;
using ems.utilities.Functions;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Http.Results;
using static ems.crm.Models.leadbank_list;
using static OfficeOpenXml.ExcelErrorValue;




namespace ems.crm.DataAccess
{
    public class DaSmsCampaign
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;

        string msSQL = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        string lssource_gid;
        string lssource_name, param1, lsleadbank_name, lsid, lscategoryindustry_name, lscountry_name, lscampagin_title, lscampaign_flag, lsleadbank_gid, lscountry_gid, mscusconGetGID, lscountrygid, mscustomerGetGID, msGETcustomercode,
            lsregion_name, lsbankcontact, msGetGid, msGetGid1, msGetGid2, msGetGid3, msGetGid4,
            msGetGid5, msGetGid6, msGetGid7, msGetGid8, msGetGid9, msGetGid10, msGetGid11, lscurrencyexchange_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5,
            mnResult6, mnResult7, mnResult8, mnResult9, mnResult10, mnResult11,
            mnResult12, mnResult13, mnResult14, mnResult15, mnResult16, mnResult17, mnResult18, mnResult19;
        char lsstatus, lsaddtocustomer;

        /// code  by snehith

        public void DaGetSmsCampaign(MdlSmsCampaign values)
        {
            msSQL = "   select   id,campagin_title,campagin_message,date_format(a.created_date,'%d-%m-%Y')as created_date,concat(b.user_firstname,(' '), b.user_lastname) as created_by from crm_smm_tsmscampaign a  " +
                    "  left join adm_mst_tuser b on b.user_gid =a.created_by  order by id desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<smscampaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new smscampaign_list
                    {
                        id = dt["id"].ToString(),
                        campagin_title = dt["campagin_title"].ToString(),
                        campagin_message = dt["campagin_message"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),


                    });
                    values.smscampaign_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetSmsCampaignCount(MdlSmsCampaign values)
        {
            msSQL = "  select count(id) as campaign_count from crm_smm_tsmscampaign  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<smscampaigncount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new smscampaigncount_list
                    {
                        campaign_count = dt["campaign_count"].ToString(),
                        


                    });
                    values.smscampaigncount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostSmsCampaign(string user_gid, smspostcampaign_list values)

        {
            msSQL = " select campagin_title from crm_smm_tsmscampaign where campagin_title = '" + values.campaign_title + "' limit 1";
            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

            if (objOdbcDataReader .HasRows == true)
            {
                values.status = false;
                values.message = "Campaign Title Already Exist !!";
            }
           
            else
            {

                    msSQL = " insert into crm_smm_tsmscampaign(" +
                            " campagin_title," +
                            " campagin_message," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + values.campaign_title + "'," +
                            " '" + values.campaign_message + "',";
                    msSQL += "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Campaign Added Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Campaign!!";
                    }
                
            }
             
        }
        public void DaUpdateSmsCampaign(string user_gid, smspostcampaign_list values)
        {
            msSQL = " select id,campagin_title  from crm_smm_tsmscampaign where campagin_title = '" + values.campaign_titleedit + "' limit 1";
            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
            if (objOdbcDataReader .HasRows)
            {
                lsid = objOdbcDataReader ["id"].ToString();
                lscampagin_title = objOdbcDataReader ["campagin_title"].ToString();
            }
            if (lsid == values.id)
            {
                msSQL = " update  crm_smm_tsmscampaign set " +

                " campagin_title = '" + values.campaign_titleedit + "'," +

                " campagin_message = '" + values.campaign_messageedit + "'," +

                " updated_by = '" + user_gid + "'," +

                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id='" + values.id + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Campaign Updated Successfully !!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Campaign !!";
                }
            }
            else if (lsid == null || lsid == "")
            {
                msSQL = " update  crm_smm_tsmscampaign set " +

                " campagin_title = '" + values.campaign_titleedit + "'," +

                " campagin_message = '" + values.campaign_messageedit + "'," +

                " updated_by = '" + user_gid + "'," +

                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id='" + values.id + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Campaign Updated Successfully !!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Campaign !!";
                }
            }
            else
            {
                    values.status = false;
                    values.message = "Campaign with the same name already exists !!";
            }
             
        }
        public void DaDeleteSmsCampaign(string id, smspostcampaign_list values)
        {
            msSQL = " select (case when campaign_flag !='' || campaign_flag is not null  then campaign_flag  when campaign_flag is null || campaign_flag =''  then 'N' end) as campaign_flag  from crm_smm_tsmscampaign where id = '" + id + "' limit 1";
            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

            if (objOdbcDataReader .HasRows)
            {
                lscampaign_flag = objOdbcDataReader ["campaign_flag"].ToString();
               
            }
            if( lscampaign_flag == "Y")
            {
                values.status = false;
                values.message = "Campaign already used hence can't be deleted!!";
            }
            else
            {
                msSQL = "  delete from crm_smm_tsmscampaign where id = '" + id + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Campaign Deleted Successfully !!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deleting Campaign !!";
                }

            }
             
        }
        public void DaSmsLeadCustomerDetails(MdlSmsCampaign values)

        {
            msSQL = "select b.address1,b.address2, b.city,b.state,a.customer_type,b.leadbankcontact_name,a.leadbank_gid,b.email,b.mobile," +
                "b.created_date from crm_trn_tleadbank a left join crm_trn_tleadbankcontact b on a.leadbank_gid=b.leadbank_gid;";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmodulelist = new List<smsleadcustomerdetails_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getmodulelist.Add(new smsleadcustomerdetails_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        names = dt["leadbankcontact_name"].ToString(),
                        customer_type = dt["customer_type"].ToString(),
                        default_phone = dt["mobile"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        email = dt["email"].ToString(),
                        address1 = dt["address1"].ToString(),
                        address2 = dt["address2"].ToString(),
                        city = dt["city"].ToString(),
                        state = dt["state"].ToString(),



                    });
                    values.smsleadcustomerdetails_list = getmodulelist;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaSmssendtolead(string user_gid, smssendtolead values)
        {
        }
        }
}