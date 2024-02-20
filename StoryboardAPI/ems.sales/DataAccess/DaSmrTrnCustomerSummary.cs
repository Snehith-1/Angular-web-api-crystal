﻿using ems.sales.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.NetworkInformation;
using System.Linq.Expressions;
using System.Configuration;
using System.IO;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Data.Common;
using System.ComponentModel;
using OfficeOpenXml.Style;
using System.Drawing;


namespace ems.sales.DataAccess
{
    public class DaSmrTrnCustomerSummary
    {
        HttpPostedFile httpPostedFile;

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL, msSQL1 = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string exclcustomer_code, lsregiongid, lscountrygid, lscurrencyexchangegid;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lspricesegment2product_gid,lspricesegment_gid, lscustomer_name,lspricesegment_name, lscustomercode, lsCode, msGetGid, msGetGid1, msGetGid2, msGetGid3, msGetPrivilege_gid, msGetModule2employee_gid, status;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetSmrTrnCustomerSummary(MdlSmrTrnCustomerSummary values)
        {
            try {
                
                msSQL = " Select distinct UCASE(a.customer_id) as customer_id,a.*,c.*, concat(c.customercontact_name,' / ',c.mobile,' / ',c.email) as contact_details, " +
                "  a.customer_region, a.customer_type, " +
                " (SELECT DATE_FORMAT(MAX(salesorder_date), '%d %b %Y') FROM smr_trn_tsalesorder WHERE customer_gid = a.customer_gid) AS last_order_date, " +
                " (SELECT DATE_FORMAT(Min(salesorder_date), '%d %b %Y') FROM smr_trn_tsalesorder WHERE customer_gid = a.customer_gid) AS first_order_date," +
                " CONCAT((SELECT DATE_FORMAT(MIN(salesorder_date), '%d %b %Y') FROM smr_trn_tsalesorder WHERE customer_gid = a.customer_gid),'  ('," +
                " (SELECT CONCAT(DATEDIFF(CURDATE(), MIN(salesorder_date)), ' days',')') FROM smr_trn_tsalesorder WHERE customer_gid = a.customer_gid)) as customer_since," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" + 
                " from crm_mst_tcustomer a" +                
                " left join crm_mst_tcustomercontact c on a.customer_gid=c.customer_gid " +
                " left join crm_mst_tregion d on a.customer_region=d.region_name " +
                " where c.customerbranch_name='H.Q'" +
                " order by a.created_date DESC ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<smrcustomer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new smrcustomer_list
                    {
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_id = dt["customer_id"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_type = dt["customer_type"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        region_name = dt["customer_region"].ToString(),
                        customer_state = dt["customer_state"].ToString(),
                        last_order_date = dt["last_order_date"].ToString(),
                        first_order_date = dt["first_order_date"].ToString(),
                        customer_since = dt["customer_since"].ToString(),
                        status = dt["status"].ToString(),
                    });

                    values.smrcustomer_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Loading Customer Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
        public void DaGetcountry(MdlSmrTrnCustomerSummary values)
        {
            try {
               
                msSQL = " Select country_name,country_gid  " +
                    " from adm_mst_tcountry ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getcountry>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getcountry
                    {
                        country_name = dt["country_name"].ToString(),
                        country_gid = dt["country_gid"].ToString(),
                    });
                    values.Getcountry = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Country Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaGetcurrency(MdlSmrTrnCustomerSummary values)
        {
            try {
               
                msSQL = " Select currency_code ,currencyexchange_gid  " +
                    " from crm_trn_tcurrencyexchange ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getcurency>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getcurency
                    {
                        currency_code = dt["currency_code"].ToString(),
                        currencyexchange_gid = dt["currencyexchange_gid"].ToString(),
                    });
                    values.Getcurency = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Currency!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
        public void GetOnChangeCountry(string country_gid, MdlSmrTrnCustomerSummary values)
        {
            try {
                
                msSQL = " select a.country_gid,a.country_name,b.currency_code ,b.exchange_rate from adm_mst_tcountry a  " +
                    " left join crm_trn_tcurrencyexchange b on a.country_gid = b.country_gid " +
                    " where a.country_gid='" + country_gid + "'";
           

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetOnchangecuontry>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetOnchangecuontry
                    {

                        country_name = dt["country_name"].ToString(),
                        currency_code = dt["currency_code"].ToString(),
                    });
                    values.GetOnchangecuontry = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Currency Code !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaGettax(MdlSmrTrnCustomerSummary values)
        {
            try {
              
                msSQL = " Select tax_gid ,tax_name " +
                    " from acp_mst_ttax  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Gettax>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Gettax
                    {
                        tax_name = dt["tax_name"].ToString(),
                        tax_gid = dt["tax_gid"].ToString(),
                    });
                    values.Gettax = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }
        public void DaGetregion(MdlSmrTrnCustomerSummary values)
        {
            try {
              
                msSQL = " SELECT region_gid, region_code, region_name FROM crm_mst_tregion Order by region_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getregion>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getregion
                    {
                        region_name = dt["region_name"].ToString(),
                        region_gid = dt["region_gid"].ToString(),
                    });
                    values.Getregion = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Region !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }
        public void DaGetcustomercity(MdlSmrTrnCustomerSummary values)
        {
            try {
              
                msSQL = " SELECT  customer_gid, customer_code, customer_city FROM crm_mst_tcustomer ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getcustomercity>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getcustomercity
                    {
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_city = dt["customer_city"].ToString(),
                    });
                    values.Getcustomercity = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Customer City!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
          
        }

        public void DaGetCustomerCode(MdlSmrTrnCustomerSummary values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CC");

            values.customer_id = msGetGid;
        }
        public void DaPostCustomer(string user_gid, postcustomer_list values)

        {
            try {
               
                msGetGid = objcmnfunctions.GetMasterGID("CC");
            msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='CC' order by finyear asc limit 0,1 ";
            string lsCode = objdbconn.GetExecuteScalar(msSQL);

            string lscustomer_code = "CC-" + "00" + lsCode;
            string lscustomercode = "H.Q";
            string lscustomer_branch = "H.Q";

            msSQL = " Select country_name from adm_mst_tcountry where country_gid='" + values.countryname + "'";
            string lscountryname = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select region_name from crm_mst_tregion where region_gid='" + values.region_name + "'";
            string lsregionname = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " Select customer_type from crm_mst_tcustomertype where customertype_gid='" + values.customer_type + "'";
                string lscustomer_type = objdbconn.GetExecuteScalar(msSQL);

                {
                msGetGid = objcmnfunctions.GetMasterGID("BCRM");
                msGetGid1 = objcmnfunctions.GetMasterGID("BCCM");
                msGetGid2 = objcmnfunctions.GetMasterGID("BLBP");
                msGetGid3 = objcmnfunctions.GetMasterGID("BLCC");

                msSQL = " insert into crm_mst_tcustomer (" +
                   " customer_gid," +
                   " customer_id, " +
                   " customer_name, " +
                   " company_website, " +
                   " customer_address, " +
                   " customer_address2," +
                   " customer_city," +
                   " currency_gid," +
                   " customer_country," +
                   " customer_region," +
                   " customer_state," +
                   " gst_number ," +
                   " customer_pin ," +
                   " customer_type ," +
                   " status ," +
                  " created_by," +
                   "created_date" +
                    ") values (" +
                   "'" + msGetGid + "', " +
                   "'" + lscustomer_code + "'," +
                   "'" + values.customer_name + "'," +
                   "'" + values.company_website + "'," +
                   "'" + values.address1 + "'," +
                   "'" + values.address2 + "'," +
                   "'" + values.customer_city + "'," +
                   "'" + values.currencyexchange_gid + "'," +
                   "'" + lscountryname + "'," +
                   "'" + lsregionname + "'," +
                   "'" + values.customer_state + "'," +
                   "'" + values.gst_number + "'," +
                   "'" + values.postal_code + "'," +
                    "'" + lscustomer_type + "'," +
                     "'Active'," +
                    "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " insert into crm_mst_tcustomercontact  (" +
                    " customercontact_gid," +
                    " customer_gid," +
                    " customercontact_name, " +
                    " customerbranch_name, " +
                    " email, " +
                    " mobile, " +
                     " main_contact, " +
                    " designation," +
                    " address1," +
                    " address2," +
                    " state," +
                    " city," +
                    " country_gid," +
                    " region," +
                    " fax, " +
                    " zip_code, " +
                    " fax_area_code, " +
                    " fax_country_code," +
                    " gst_number, " +
                     " created_by," +
                   "created_date" +

                    ") values (" +
                    "'" + msGetGid1 + "', " +
                    "'" + msGetGid + "', " +
                    "'" + values.customercontact_name + "'," +
                    "'" + lscustomer_branch + "'," +
                    "'" + values.email + "'," +
                    "'" + values.mobiles.e164Number + "'," +
                    "'Y'," +
                    "'" + values.designation + "'," +
                    "'" + values.address1 + "'," +
                    "'" + values.address2 + "'," +
                    "'" + values.customer_state + "'," +
                    "'" + values.customer_city + "'," +
                     "'" + values.countryname + "'," +
                    "'" + lsregionname + "'," +
                     "'" + values.fax + "'," +
                     "'" + values.postal_code + "'," +
                     "'" + values.area_code + "'," +
                     "'" + values.country_code + "'," +
                   "'" + values.gst_number + "'," +
                   "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {

                    msSQL = " INSERT INTO crm_trn_tleadbank " +
                           " (leadbank_gid, " +
                           " customer_gid, " +
                           " leadbank_name," +
                           " leadbank_address1, " +
                           " leadbank_address2, " +
                           " leadbank_city, " +
                           " leadbank_code, " +
                           " leadbank_state, " +
                           " leadbank_pin, " +
                           " leadbank_country, " +
                           " leadbank_region, " +
                           " approval_flag, " +
                           " leadbank_id, " +
                           " status, " +
                           " main_branch," +
                           " main_contact," +
                           " customer_type," +
                           " created_by, " +
                           " created_date)" +
                           " values ( " +
                           "'" + msGetGid2 + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.customer_name + "'," +
                           "'" + values.address1 + "'," +
                           "'" + values.address2 + "'," +
                           "'" + values.customer_city + "'," +
                           "'" + lscustomercode + "'," +
                           "'" + values.customer_state + "'," +
                           "'" + values.postal_code + "'," +
                           "'" + lscountryname + "'," +
                           "'" + lsregionname + "'," +
                           "'Approved'," +
                           "'" + lscustomer_code + "'," +
                           "'Y'," +
                           "'Y'," +
                           "'Y'," +
                            "'" + values.customer_type + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    msSQL = " INSERT into crm_trn_tleadbankcontact (" +
                        " leadbankcontact_gid, " +
                        " leadbank_gid," +
                        " leadbankbranch_name, " +
                        " leadbankcontact_name," +
                        " email," +
                        " mobile," +
                        " designation," +
                        " did_number," +
                        " created_date," +
                        " created_by," +
                        " address1," +
                        " address2, " +
                        " state, " +
                        " country_gid, " +
                        " city, " +
                        " pincode, " +
                        " region_name, " +
                        " main_contact," +
                        " phone1," +
                        " area_code1," +
                        " country_code1," +
                        " fax," +
                        " fax_area_code," +
                        " fax_country_code)" +
                        " values (" +
                        " '" + msGetGid3 + "'," +
                        " '" + msGetGid2 + "'," +
                        "'" + lscustomercode + "'," +
                        "'" + values.customercontact_name + "'," +
                        "'" + values.email + "'," +
                        "'" + values.mobile + "'," +
                        "'" + values.designation + "'," +
                        "'0'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                        "'" + user_gid + "'," +
                        "'" + values.address1 + "'," +
                        "'" + values.address2 + "'," +
                        "'" + values.customer_state + "'," +
                        "'" + values.countryname + "'," +
                        "'" + values.customer_city + "'," +
                        "'" + values.postal_code + "'," +
                        "'" + lsregionname + "'," +
                        "'Y'," +
                        "'" + values.phone1 + "'," +
                        "'" + values.area_code + "'," +
                        "'" + values.country_code + "'," +
                        "'" + values.fax + "'," +
                         "'" + values.fax_area_code + "'," +
                        "'" + values.fax_country_code + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Customer Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Adding Customer";
                }

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Submitting Customer !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaGetSmrTrnDistributorSummary(MdlSmrTrnCustomerSummary values)
        {
            try {
              
                msSQL = " Select distinct UCASE(a.customer_id) as customer_id,a.*,c.*, concat(c.customercontact_name,' / ',c.mobile,' / ',c.email) as contact_details, " +
                " case when d.region_name is null then concat(a.customer_city,' / ',a.customer_state)" +
                " when d.region_name is not null " +
                " then Concat(d.region_name,' / ',a.customer_city,' / ',a.customer_state) end as region_name " +
                " from crm_mst_tcustomer a" +
                " left join crm_mst_tregion d on a.customer_region =d.region_gid " +
                " left join crm_mst_tcustomercontact c on a.customer_gid=c.customer_gid " +
                " left join crm_mst_tcustomer b on a.customer_gid = b.customer_gid  where  c.customerbranch_name='H.Q' and  a.customer_type = 'Distributor' " +
                " group  by a.customer_gid asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<smrcustomer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new smrcustomer_list
                    {
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_id = dt["customer_id"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        customer_state = dt["customer_state"].ToString(),
                    });
                    values.smrcustomer_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Loading Distributor !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }
        public void DaGetSmrTrnRetailerSummary(MdlSmrTrnCustomerSummary values)
        {
            try {
               
                msSQL = " Select distinct UCASE(a.customer_id) as customer_id,a.*,c.*, concat(c.customercontact_name,' / ',c.mobile,' / ',c.email) as contact_details, " +
                " case when d.region_name is null then concat(a.customer_city,' / ',a.customer_state)" +
                " when d.region_name is not null " +
                " then Concat(d.region_name,' / ',a.customer_city,' / ',a.customer_state) end as region_name " +
                " from crm_mst_tcustomer a" +
                " left join crm_mst_tregion d on a.customer_region =d.region_gid " +
                " left join crm_mst_tcustomercontact c on a.customer_gid=c.customer_gid " +
                " left join crm_mst_tcustomer b on a.customer_gid = b.customer_gid  where  c.customerbranch_name='H.Q' and a.customer_type = 'Retailer' " +
                 " group  by a.customer_gid asc "; 

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<smrcustomer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new smrcustomer_list
                    {
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_id = dt["customer_id"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        customer_state = dt["customer_state"].ToString(),
                    });
                    values.smrcustomer_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Loading Retailer !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
        public void DaGetSmrTrnCorporateSummary(MdlSmrTrnCustomerSummary values)
        {
            try {
               
                msSQL = " Select distinct UCASE(a.customer_id) as customer_id,a.*,c.*, concat(c.customercontact_name,' / ',c.mobile,' / ',c.email) as contact_details, " +
                " case when d.region_name is null then concat(a.customer_city,' / ',a.customer_state)" +
                " when d.region_name is not null " +
                " then Concat(d.region_name,' / ',a.customer_city,' / ',a.customer_state) end as region_name " +
                " from crm_mst_tcustomer a" +
                " left join crm_mst_tregion d on a.customer_region =d.region_gid " +
                " left join crm_mst_tcustomercontact c on a.customer_gid=c.customer_gid " +
                " left join crm_mst_tcustomer b on a.customer_gid = b.customer_gid  where c.customerbranch_name='H.Q' and a.customer_type = 'Corporate' " +
                 " group  by a.customer_gid asc "; 

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<smrcustomer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new smrcustomer_list
                    {
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_id = dt["customer_id"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        customer_state = dt["customer_state"].ToString(),
                    });
                    values.smrcustomer_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured whileLoading Corporate!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaGetSmrTrnCustomerCount(string employee_gid, string user_gid, MdlSmrTrnCustomerSummary values)
        {
            try {
               
                msSQL = " select (select count(customer_gid) from crm_mst_tcustomer where customer_type='Distributor') as distributor_count, " +
                    " (select count(customer_gid) from crm_mst_tcustomer where customer_type='Retailer') as retailer_counts," +
                    " (select count(customer_gid) from crm_mst_tcustomer where customer_type='Corporate') as corporate_count, " +
                    " (select count(customer_gid) from crm_mst_tcustomer) as total_count ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var customercount_list = new List<customercount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    customercount_list.Add(new customercount_list
                    {
                        distributor_count = (dt["distributor_count"].ToString()),
                        retailer_counts = (dt["retailer_counts"].ToString()),
                        corporate_count = (dt["corporate_count"].ToString()),
                        total_count = (dt["total_count"].ToString()),
                    });
                    values.customercount_list = customercount_list;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Customer Count !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaGetViewcustomerSummary(string customer_gid, MdlSmrTrnCustomerSummary values)
        
        {
            try {
               

                msSQL = " SELECT a.customer_gid, a.customer_name, a.company_website, UCASE(a.customer_id) as customer_id,a.gst_number, " +
                " a.customer_code,b.currency_code, a.customer_address,c.phone,c.area_code,c.country_code,c.fax,c.fax_area_code, " +
                " a.customer_city, a.customer_state,  a.customer_country as customer_country, a.customer_pin,c.fax_country_code, " +
                " a.customer_region, a.main_branch, a.customer_address2,c.customercontact_gid, c.customer_gid, c.customerbranch_name ," +
                " c.customercontact_name, c.email, " +
                " c.mobile, c.designation, c.main_contact,d.region_name,a.customer_country FROM crm_mst_tcustomer a " +
                " left join crm_mst_tcustomercontact c on a.customer_gid = c.customer_gid " +
                " left join crm_mst_tregion d on a.customer_region = d.region_gid " +
                " left join adm_mst_tcountry e on  e.country_gid  = a.customer_country " +
                " left join crm_trn_tcurrencyexchange b on a.currency_gid=b.currencyexchange_gid " +
                " Where a.customer_gid = '" + customer_gid + "'" +
                "group  by a.customer_gid asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<postcustomer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new postcustomer_list
                    {


                        customer_id = dt["customer_id"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        company_website = dt["company_website"].ToString(),
                        customercontact_name = dt["customercontact_name"].ToString(),
                        designation = dt["designation"].ToString(),
                        mobile = dt["mobile"].ToString(),
                        email = dt["email"].ToString(),
                        customer_address = dt["customer_address"].ToString(),
                        customer_address2 = dt["customer_address2"].ToString(),
                        customer_city = dt["customer_city"].ToString(),
                        customer_state = dt["customer_state"].ToString(),
                        countryname = dt["customer_country"].ToString(),
                        customer_pin = dt["customer_pin"].ToString(),
                        region_name = dt["customer_region"].ToString(),
                        customer_code = dt["customer_code"].ToString(),
                        phone = dt["phone"].ToString(),
                        area_code = dt["area_code"].ToString(),
                        country_code = dt["country_code"].ToString(),
                        fax = dt["fax"].ToString(),
                        fax_area_code = dt["fax_area_code"].ToString(),
                        fax_country_code = dt["fax_country_code"].ToString(),
                        gst_number = dt["gst_number"].ToString(),
                        customerbranch_name = dt["customerbranch_name"].ToString(),


                    });
                    values.postcustomer_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Cutomer View !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaGetcustomerInactive(string Customer_gid, MdlSmrTrnCustomerSummary values)
        {
            try {
              
                msSQL = " update crm_mst_tcustomer set" +
                        " status='Inactive'" +
                        " where customer_gid = '" + Customer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Customer Inactivated Successfully";
            }
            else
            {
                {
                    values.status = false;
                    values.message = "Error While Customer Inactivated";
                }
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while  Updating Customer Inactivated !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }

        public void DaGetcustomerActive(string Customer_gid, MdlSmrTrnCustomerSummary values)
        {
            try {
               
                msSQL = " update crm_mst_tcustomer set" +
                        " status='Active'" +
                        " where customer_gid = '" + Customer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Customer Activated Successfully";
            }
            else
            {
                {
                    values.status = false;
                    values.message = "Error While Customer Activated ";
                }
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Customer Activated !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }


       
        //custmerpricesegment summary
        public void DaGetProductAssignSummary(string customer_gid, MdlSmrTrnCustomerSummary values)
        {
            try {
               
                msSQL = "select * from smr_trn_tpricesegment2customer where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count == 0)
            {
                msSQL = " SELECT d.producttype_name,concat(b.productgroup_code,   ' | '   ,b.productgroup_name) as productgroup_name, a.product_gid, a.cost_price, a.product_code, CONCAT_WS('|',a.product_name,a.size, a.width, a.length) as product_name, " +
                    " CONCAT(f.user_firstname,' ',f.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date, " +
                    " (select customer_gid from crm_mst_tcustomer where customer_gid='" + customer_gid + "') as customer_gid ," +
                    " (select customer_name from crm_mst_tcustomer where customer_gid='" + customer_gid + "') as customer_name ," +
                    " c.productuomclass_code,e.productuom_code,c.productuomclass_name,(case when a.stockable ='Y' then 'Yes' else 'No ' end) as stockable," +
                    " e.productuom_name,e.productuom_gid,d.producttype_name as product_type,(case when a.status ='1' then 'Active' else 'Inactive' end) as Status," +
                    " (case when a.serial_flag ='Y' then 'Yes' else 'No' end)as serial_flag,i.product_price,a.mrp_price," +
                    " (case when a.avg_lead_time is null then '0 days' else concat(a.avg_lead_time,'  ', 'days') end)as lead_time  from pmr_mst_tproduct a " +
                    " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid " +
                    " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid " +
                    " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid " +
                    " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid " +
                    " left join smr_trn_tpricelistdtl i on i.product_gid = a.product_gid " +
                    " left join adm_mst_tuser f on f.user_gid=a.created_by group by a.product_name order by a.created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getproductlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getproductlist
                        {
                            customer_gid = dt["customer_gid"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            cost_price = dt["cost_price"].ToString(),
                            selling_price = dt["mrp_price"].ToString(),
                            product_price = dt["mrp_price"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            productuom_gid = dt["productuom_gid"].ToString(),
                        });
                        values.product_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();

            }
            else {
                msSQL = " SELECT d.producttype_name,concat(b.productgroup_code,   ' | '   ,b.productgroup_name) as productgroup_name, a.product_gid, a.cost_price, a.product_code, CONCAT_WS('|',a.product_name,a.size, a.width, a.length) as product_name,"+
                        " CONCAT(f.user_firstname,' ',f.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date,j.stock_gid, g.pricesegment2product_gid," +
                        " (select customer_gid from crm_mst_tcustomer where customer_gid='" + customer_gid + "') as customer_gid , " +
                        " (select customer_name from crm_mst_tcustomer where customer_gid='" + customer_gid + "') as customer_name , " +
                        " c.productuomclass_code,e.productuom_code,c.productuomclass_name,(case when a.stockable ='Y' then 'Yes' else 'No ' end) as stockable, " +
                        " e.productuom_name,e.productuom_gid,d.producttype_name as product_type,(case when a.status ='1' then 'Active' else 'Inactive' end) as Status," +
                        " (case when a.serial_flag ='Y' then 'Yes' else 'No' end)as serial_flag,g.product_price,a.mrp_price, " +
                        " (case when a.avg_lead_time is null then '0 days' else concat(a.avg_lead_time,'  ', 'days') end)as lead_time  from pmr_mst_tproduct a  " +
                        " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid  " +
                        " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid  " +
                        " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid  " +
                        " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid  " +
                        " left join smr_trn_tpricelistdtl i on i.product_gid = a.product_gid  " +
                        " left join adm_mst_tuser f on f.user_gid=a.created_by " +
                        " left join smr_trn_tpricesegment2product g on g.product_gid=a.product_gid " +
                        " left join smr_trn_tpricesegment2customer h on h.pricesegment_gid=g.pricesegment_gid  " +
                        " left join ims_trn_tstock j on j.product_gid=g.product_gid " +
                        " where h.customer_gid='" + customer_gid + "'" +
                        " group by a.product_name order by a.created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getproductlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getproductlist
                        {
                            customer_gid = dt["customer_gid"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            cost_price = dt["cost_price"].ToString(),
                            selling_price = dt["product_price"].ToString(),
                            product_price = dt["mrp_price"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            productuom_gid = dt["productuom_gid"].ToString(),
                            stock_gid = dt["stock_gid"].ToString(),
                            pricesegment2product_gid = dt["pricesegment2product_gid"].ToString(),
                        });
                        values.product_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Assigning Product !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }

        // Customer edit summary
        public void DaGetEditCustomer(string customer_gid, MdlSmrTrnCustomerSummary values)
        {
            try {
               
                msSQL = " SELECT a.customer_gid,a.customer_name, a.company_website,a.customer_type,a.gst_number, a.customer_id ,c.phone,c.area_code,c.country_code,c.fax,c.fax_area_code," +
                    " a.customer_code,a.customer_address,c.city,a.customer_country,c.state,c.fax_country_code,c.country_gid , " +
                    " c.zip_code,a.customer_region,c.customercontact_gid,d.region_name,c.customercontact_name,b.currency_code,c.email,c.mobile,c.designation,a.customer_address2 " +
                    " from crm_mst_tcustomer a" +
                    " left join crm_mst_tcustomercontact c on a.customer_gid=c.customer_gid " +
                    " left join crm_mst_tregion d on a.customer_region = d.region_gid " +
                    " left join adm_mst_tcountry e on a.customer_country = e.country_gid " +
                    " left join crm_trn_tcurrencyexchange b on a.currency_gid=b.currencyexchange_gid " +
                    " where a.customer_gid ='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetCustomerlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetCustomerlist
                    {
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_id = dt["customer_id"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customercontact_name = dt["customercontact_name"].ToString(),
                        customer_type = dt["customer_type"].ToString(),
                        mobile_number= dt["mobile"].ToString(),
                        address1 = dt["customer_address"].ToString(),
                        address2 = dt["customer_address2"].ToString(),
                        email = dt["email"].ToString(),
                        city = dt["city"].ToString(),
                        postal_code = dt["zip_code"].ToString(),
                        country_name = dt["customer_country"].ToString(),
                        currencyname = dt["currency_code"].ToString(),
                        region_name = dt["customer_region"].ToString(),
                        gst_number = dt["gst_number"].ToString(),
                        company_website = dt["company_website"].ToString(),
                        country_code = dt["fax_country_code"].ToString(),
                        area_code = dt["fax_area_code"].ToString(),
                        fax_number = dt["fax"].ToString(),
                        designation = dt["designation"].ToString(),
                        customer_state = dt["state"].ToString(),

                    });
                    values.GetCustomerList = getModuleList;
                }

            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting  Customer !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
          
        }

        public void DaGetcustomername(string customer_gid, MdlSmrTrnCustomerSummary values)
        {
            try {
              

                msSQL = "  select customer_name,customer_type from crm_mst_tcustomer where customer_gid='" + customer_gid + "'  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetCustomerlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetCustomerlist
                    {
                        customer_name = dt["customer_name"].ToString(),
                        customer_type = dt["customer_type"].ToString(),
                    });
                    values.GetCustomerList = getModuleList;
                }

            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Customer Type !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           


        }
        public void DaUpdateCostomer(GetCustomerlist values)
        {
            try {
               
                msSQL = "select region_name from crm_mst_tregion " +
               " where region_gid = '" + values.region_name + "'";
            string lsregionname = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "Update crm_mst_tcustomer set" +
                    " customer_name = '" + values.customer_name + "'," +
                    " customer_type='" + values.customer_type + "'," +                
                    " company_website = '" + values.company_website + "'," +
                    " customer_id = '" + values.customer_id + "'," +
                    " customer_address = '" + values.address1 + "'," +
                    " customer_address2 = '" + values.address2 + "'," +                                
                    " customer_country = '" + values.country_name + "'," +
                    " customer_region = '" + values.region_name + "'," +
                    " gst_number = '" + values.gst_number + "'" +
                    " where customer_gid = '" + values.customer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                

                msSQL = "Update crm_mst_tcustomercontact set" +
                       " customercontact_name = '" + values.customercontact_name + "'," +
                       " address1='" + values.address1 + "'," +
                       " address2='" + values.address2 + "'," +
                       " state='" + values.customer_state + "'," +
                       " city='" + values.city + "'," +
                       " region='" + lsregionname + "'," +
                       " zip_code='" + values.postal_code + "'," +
                       " mobile = '" + values.mobiles + "'," +
                       " email = '" + values.email + "'," +
                       " designation = '" + values.designation + "'," +
                       " fax_area_code = '" + values.area_code + "'," +
                       " fax_country_code = '" + values.country_code + "'," +
                       " fax = '" + values.fax_number + "'," +
                       " gst_number='" + values.gst_number + "'" +
                       " where customer_gid = '" + values.customer_gid+ "'";
                mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult1 == 1)
            {
                msSQL = "update crm_trn_tleadbank set" +
                        " leadbank_name = '" + values.customer_name + "'" +
                        " where customer_gid = '" + values.customer_gid + "'";
                mnResult2 = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = "select leadbank_gid from crm_trn_tleadbank " +
                " where customer_gid = '" + values.customer_gid + "'";
            string lsleadbankname = objdbconn.GetExecuteScalar(msSQL);


            if (mnResult2 == 1)
            {
                msSQL = " update crm_trn_tleadbank set leadbank_country='" + values.countryname + "', " +
                        " leadbank_pin='" + values.postal_code + "', " +
                        " leadbank_address1='" + values.address1 + "', " +
                        " leadbank_address2='" + values.address2 + "', " +
                        " leadbank_city='" + values.city + "', " +
                        " leadbank_state='" + values.customer_state + "', " +
                        " leadbank_region='" + values.region_name + "', " +
                        " company_website='" + values.company_website + "' " +
                        " where leadbank_gid='" + lsleadbankname + "' ";
                mnResult3 = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
                if(mnResult3 == 1)
                { 

                msSQL = " update crm_trn_tleadbankcontact set" +
                        " leadbankcontact_name = '" + values.customercontact_name + "'," +
                        " address1='" + values.address1 + "'," +
                        " address2='" + values.address2 + "', " +
                        " state='" + values.customer_state + "', " +
                        " city='" + values.city + "', " +
                        " pincode='" + values.postal_code + "', " +
                        " region_name='" + lsregionname + "', " +
                        " email = '" + values.email + "'," +
                        " mobile = '" + values.mobiles + "'," +
                        " designation = '" + values.designation + "'," +
                        " area_code1 = '" + values.area_code + "'," +
                        " country_code1 = '" + values.country_code + "'," +
                        " fax = '" + values.fax_number + "'," +
                        " fax_area_code = '" + values.area_code + "'," +
                        " fax_country_code = '" + values.country_code + "'" +
                        " where leadbank_gid='" + lsleadbankname + "'";
                mnResult3 = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult == 1 && mnResult1 == 1 && mnResult2 == 1 && mnResult3 == 1)
            {

                values.status = true;
                values.message = "Successfully updated";
            }
            else
            {
                values.status = false;
                values.message = "Error while updating";
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Customer  !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            

        }
        //customer Segment Add and edit
        public void DaCustomerprice(string user_gid, Getproductlist values)
        {
            try {
               
                msSQL = "select * from smr_trn_tpricesegment2customer where customer_gid ='" + values.customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count == 0)
            {
                msSQL = "  select customer_name from crm_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
                       lscustomer_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select * from smr_trn_tpricesegment where pricesegment_name ='" + lscustomer_name + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count == 0)
                {
                    string msGetGID = objcmnfunctions.GetMasterGID("SPRS");
                    msSQL = " insert into smr_trn_tpricesegment ( " +
                            " pricesegment_gid," +
                            " pricesegment_code, " +
                            " pricesegment_name " +
                            " ) values( " +
                            " '" + msGetGID + "', " +
                            " '" + values.customer_gid + "'," +
                            " '" + lscustomer_name + "' )";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                msSQL = "select pricesegment_gid,pricesegment_name from smr_trn_tpricesegment where pricesegment_name ='" + lscustomer_name + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        lspricesegment_gid = dt["pricesegment_gid"].ToString();
                        lspricesegment_name = dt["pricesegment_name"].ToString();
                    }
                }
                string msGetGID1 = objcmnfunctions.GetMasterGID("VPDC");
                msSQL = " insert into smr_trn_tpricesegment2customer(" +
                        " pricesegment2customer_gid, " +
                        " pricesegment_gid, " +
                        " pricesegment_name," +
                        " customer_gid, " +
                        " customer_name, " +
                        " created_by, " +
                        " created_date" +
                        " )values( " +
                        "'" + msGetGID1 + "', " +
                        "'" + lspricesegment_gid + "'," +
                        "'" + lscustomer_name + "'," +
                        "'" + values.customer_gid + "', " +
                        "'" + lscustomer_name + "', " +
                        "'" + user_gid + "', " +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                foreach (var data in values.salesproduct_list)
                {
                    string lsproductgroup_code = data.productgroup_name.Split('|').First().Trim();
                    string lsproductgroup_name = data.productgroup_name.Split('|').Last().Trim();
                    string mspricesegment2productgetgid = objcmnfunctions.GetMasterGID("SRCT");
                    msSQL = " insert into smr_trn_tpricesegment2product( " +
                            " pricesegment2product_gid, " +
                            " product_code, " +
                            " product_name," +
                            " product_gid," +
                            " productuom_gid," +
                            " productuom_name," +
                            " productgroup_code," +
                            " productgroup , " +
                            " pricesegment_gid , " +
                            " pricesegment_name , " +
                            " product_price, " +
                            " cost_price, " +
                            " customerproduct_code, " +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            " '" + mspricesegment2productgetgid + "', " +
                            " '" + data.product_code + "'," +
                            " '" + data.product_name + "', " +
                            " '" + data.product_gid + "', " +
                            " '" + data.productuom_gid + "'," +
                            " '" + data.productuom_name + "'," +
                            " '" + lsproductgroup_code + "'," +
                            " '" + lsproductgroup_name + "'," +
                            " '" + lspricesegment_gid + "'," +
                            " '" + lspricesegment_name + "'," +
                            " '" + data.selling_price + "'," +
                            " '" + data.cost_price + "'," +
                            " '" + lsproductgroup_code + "', " +
                            " '" + user_gid + "'," +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        string msSTOCKGetGID = objcmnfunctions.GetMasterGID("ISKP");
                        msSQL = " insert into ims_trn_tstock " +
                                " (stock_gid, " +
                                " branch_gid, " +
                                " product_gid, " +
                                " uom_gid, " +
                                " stock_qty, " +
                                " grn_qty, " +
                                " unit_price, " +
                                " remarks, " +
                                " stocktype_gid, " +
                                " reference_gid, " +
                                " stock_flag, " +
                                " created_by, " +
                                " created_date)" +
                                " values( " +
                                " '" + msSTOCKGetGID + "'," +
                                " '" + data.branch_gid + "'," +
                                " '" + data.product_gid + "', " +
                                " '" + data.productuom_gid + "'," +
                                " '0'," +
                                " '0'," +
                                " '" + data.selling_price + "'," +
                                " 'From Sales'," +
                                " 'SY0905270003'," +
                                " '" + mspricesegment2productgetgid + "'," +
                                " 'Y'," +
                                " '" + user_gid + "'," +
                                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = " INSERT INTO smr_trn_tstockpricehistory( " +
                                " product_gid, " +
                                " pricesegment_gid, " +
                                " customerproduct_code," +
                                " old_price, " +
                                " stock_gid, " +
                                " updated_price, " +
                                " updated_by ," +
                                " updated_date " +
                                " ) VALUES ( " +
                                " '" + data.product_gid + "', " +
                                " '" + lspricesegment_gid + "', " +
                                " '" + lsproductgroup_code + "'," +
                                " '" + data.product_price + "', " +
                                " '" + msSTOCKGetGID + "', " +
                                " '" + data.selling_price + "', " +
                                " '" + user_gid + "', " +
                                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ) ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = " Pricesegment Added Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured While Adding Pricesegment";
                    }

                }
            }
            else
            {
                //msSQL= " select a.pricesegment2product_gid from smr_trn_tpricesegment2product a " +
                //       " left join smr_trn_tpricesegment2customer b on a.pricesegment_gid=b.pricesegment_gid "+
                //       " where customer_gid='" + values.customer_gid + "'";
                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //foreach (DataRow row in dt_datatable.Rows)
                //{
                    foreach (var data in values.salesproduct_list)
                    {
                        msSQL = "  select customer_name from crm_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
                        lscustomer_name = objdbconn.GetExecuteScalar(msSQL);
                        string lsproductgroup_code = data.productgroup_name.Split('|').First().Trim();
                        msSQL = "select pricesegment_gid from smr_trn_tpricesegment where pricesegment_name ='" + lscustomer_name + "'";
                        lspricesegment_gid = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " update smr_trn_tpricesegment2product set" +
                                " product_price = '" + data.selling_price + "'," +
                                " cost_price='" + data.cost_price + "'" +
                                " where pricesegment_gid='" + lspricesegment_gid + "'" +
                                " and pricesegment2product_gid='" + data.pricesegment2product_gid +"'";
                        mnResult3 = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult3 != 0)
                        {
                            msSQL = " INSERT INTO smr_trn_tstockpricehistory( " +
                                    " product_gid, " +
                                    " pricesegment_gid, " +
                                    " customerproduct_code," +
                                    " old_price, " +
                                    " stock_gid, " +
                                    " updated_price, " +
                                    " updated_by ," +
                                    " updated_date " +
                                    " ) VALUES ( " +
                                    " '" + data.product_gid + "', " +
                                    " '" + lspricesegment_gid + "', " +
                                    " '" + lsproductgroup_code + "'," +
                                    " '" + data.product_price + "', " +
                                    " '" + data.stock_gid + "', " +
                                    " '" + data.selling_price + "', " +
                                    " '" + user_gid + "', " +
                                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ) ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                values.status = true;
                                values.message = " Pricesegment Updated Successfully";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error Occured While Updated Pricesegment";
                            }

                        }
                    }
                //}

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured whileUpdated Pricesegment !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }

        public void DaCustomerpriceupdate(string user_gid, Getproductlist values)
        {
            try {
               
                foreach (var data in values.salesproduct_list)
            {
                msSQL = "  select customer_name from crm_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
                lscustomer_name = objdbconn.GetExecuteScalar(msSQL);
                string lsproductgroup_code = data.productgroup_name.Split('|').First().Trim();
                msSQL = "select pricesegment_gid from smr_trn_tpricesegment where pricesegment_name ='" + lscustomer_name + "'";
                lspricesegment_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " update smr_trn_tpricesegment2product set" +
                        " product_price = '" + data.selling_price + "'," +
                        " cost_price='" + data.cost_price + "'" +
                        " where pricesegment_gid='" + lspricesegment_gid + "'" +
                        " and pricesegment2product_gid='" + data.pricesegment2product_gid + "'";
                mnResult3 = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult3 != 0)
                {
                    msSQL = " INSERT INTO smr_trn_tstockpricehistory( " +
                            " product_gid, " +
                            " pricesegment_gid, " +
                            " customerproduct_code," +
                            " old_price, " +
                            " stock_gid, " +
                            " updated_price, " +
                            " updated_by ," +
                            " updated_date " +
                            " ) VALUES ( " +
                            " '" + data.product_gid + "', " +
                            " '" + lspricesegment_gid + "', " +
                            " '" + lsproductgroup_code + "'," +
                            " '" + data.product_price + "', " +
                            " '" + data.stock_gid + "', " +
                            " '" + data.selling_price + "', " +
                            " '" + user_gid + "', " +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ) ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = " Pricesegment Updated Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured While Updated Pricesegment";
                    }

                }
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updated Pricesegment !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           

        }


        public void DaCustomerImport(HttpRequest httpRequest, string user_gid, result objResult, postcustomer_list values)
        {
            try {
              
                string lscompany_code;
            string excelRange, endRange;
            int rowCount, columnCount;
          

            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = ConfigurationManager.AppSettings["importexcelfile1"];

                if (!Directory.Exists(lsfilePath))
                    Directory.CreateDirectory(lsfilePath);

                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = lsfile_gid + FileExtension;
                FileInfo fileinfo = new FileInfo(lsfilePath);
                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                try
                {
                   // ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (ExcelPackage xlPackage = new ExcelPackage(ms))
                    {
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets["Customer"];
                        rowCount = worksheet.Dimension.End.Row;
                        columnCount = worksheet.Dimension.End.Column;
                        endRange = worksheet.Dimension.End.Address;
                    }
                    string status;
                    status = objcmnfunctions.uploadFile(lsfilePath, FileExtension);
                    file.Close();
                    ms.Close();

                    objcmnfunctions.uploadFile(lspath, lsfile_gid);
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.Message.ToString();
                    return;
                }

                //Excel To DataTable
                try
                {
                    DataTable dataTable = new DataTable();
                    int totalSheet = 1;
                    string connectionString = string.Empty;
                    string fileExtension = Path.GetExtension(lspath);

                    //lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                    //string correctedPath = Regex.Replace(lsfilePath, @"\\+", @"\");
                    //excelRange = "A1:" + endRange + rowCount.ToString();
                    //dt = objcmnfunctions.ExcelToDataTable(correctedPath, excelRange);
                    //dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                    lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                    string correctedPath = Regex.Replace(lsfilePath, @"\\+", @"\");
                    string sheetName;
                    try
                    {
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + correctedPath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0';";
                    }
                    catch (Exception ex)
                    {

                    }

                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();
                        DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                        if (schemaTable != null)
                        {
                            var tempDataTable = (from dataRow in schemaTable.AsEnumerable()
                                                 where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                                 select dataRow).CopyToDataTable();

                            schemaTable = tempDataTable;
                            totalSheet = schemaTable.Rows.Count;
                            using (OleDbCommand command = new OleDbCommand())
                            {
                                sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString().Replace("'", "").Trim();
                                command.Connection = connection;
                                command.CommandText = "SELECT * FROM [" + sheetName + "]";

                                using (OleDbDataReader reader = command.ExecuteReader())
                                {
                                    dataTable.Load(reader);
                                }

                                // Insert data into the database
                                //InsertDataIntoDatabase(dt);

                                foreach (DataRow row in dataTable.Rows)
                                {

                                    string excustomercode = row["Customer Code"].ToString();
                                    string excustomername = row["Customer Name"].ToString();
                                    string excustomertype = row["Customer Type"].ToString();
                                    string exccontactpersonname = row["Contact Person"].ToString();
                                    string excustomermobile = row["Moblie Number"].ToString();
                                    string exaddress1 = row["Address 1"].ToString();
                                    string exaddress2 = row["Address 2"].ToString();
                                    string excity = row["City"].ToString();
                                    string expostalcode = row["Postal Code"].ToString();
                                    string excountry = row["Country"].ToString();
                                    string excurrency = row["Currency "].ToString();
                                    string exemail = row["Email"].ToString();
                                    string exstate = row["State"].ToString();
                                    string exgstnumber = row["GST Number"].ToString();
                                    string exfaxnumber = row["Fax Number"].ToString();
                                    string exdesignation = row["Desgination"].ToString();
                                    string excompanywebsite = row["Company Website"].ToString();
                                    string exregion = row["Region"].ToString();

                                    if (excustomercode == "")
                                    {
                                        msGetGid = objcmnfunctions.GetMasterGID("CC");
                                        msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='CC' order by finyear asc limit 0,1 ";
                                        string exCode = objdbconn.GetExecuteScalar(msSQL);
                                        exclcustomer_code = "CC-" + "00" + exCode;

                                    }
                                    else
                                    {
                                        exclcustomer_code = excustomercode;

                                    }

                                    string exclcustomercode = "H.Q";
                                    string exclcustomer_branch = "H.Q";


                                    msSQL = "select currencyexchange_gid from crm_trn_tcurrencyexchange where currency_code = '" + excurrency + "'";
                                    string lscurrencyexchange_gid = objdbconn.GetExecuteScalar(msSQL);
                                    if (lscurrencyexchange_gid == "")
                                    {
                                        values.message = "";

                                    }
                                    else
                                    {
                                        lscurrencyexchangegid = lscurrencyexchange_gid;
                                    }
                                    msSQL = "select country_gid from adm_mst_tcountry where country_name = '" + excountry + "'";
                                    string lscountry_gid = objdbconn.GetExecuteScalar(msSQL);
                                    if (lscountry_gid == "")
                                    {
                                        values.message = "";
                                    }
                                    else
                                    {

                                        lscountrygid = lscountry_gid;
                                    }
                                    msSQL1 = "select region_gid from crm_mst_tregion where region_name = '" + exregion + "'";
                                    string lsregion_gid = objdbconn.GetExecuteScalar(msSQL1);
                                    if (lsregion_gid == "")
                                    {
                                        values.message = "";
                                    }
                                    else
                                    {
                                         lsregiongid = lsregion_gid;
                                    }
                                    {

                                        msGetGid = objcmnfunctions.GetMasterGID("BCRM");
                                        msGetGid1 = objcmnfunctions.GetMasterGID("BCCM");
                                        msGetGid2 = objcmnfunctions.GetMasterGID("BLBP");
                                        msGetGid3 = objcmnfunctions.GetMasterGID("BLCC");

                                        msSQL = " insert into crm_mst_tcustomer (" +
                                           " customer_gid," +
                                           " customer_id, " +
                                           " customer_name, " +
                                           " company_website, " +
                                           " customer_address, " +
                                           " customer_address2," +
                                           " customer_city," +
                                           " currency_gid," +
                                           " customer_country," +
                                           " customer_region," +
                                           " customer_state," +
                                           " gst_number ," +
                                           " customer_pin ," +
                                           " customer_type ," +
                                          " created_by," +
                                           "created_date" +
                                            ") values (" +
                                           "'" + msGetGid + "', " +
                                           "'" + exclcustomer_code + "'," +
                                           "'" + excustomername + "'," +
                                           "'" + excompanywebsite + "'," +
                                           "'" + exaddress1 + "'," +
                                           "'" + exaddress2 + "'," +
                                           "'" + excity + "'," +
                                           "'" + lscurrencyexchangegid + "'," +
                                           "'" + lscountrygid + "'," +
                                           "'" + lsregiongid + "'," +
                                           "'" + exstate + "'," +
                                           "'" + exgstnumber + "'," +
                                           "'" + expostalcode + "'," +
                                            "'" + excustomertype + "'," +
                                            "'" + user_gid + "'," +
                                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        if (mnResult != 0)
                                        {
                                            msSQL = " insert into crm_mst_tcustomercontact  (" +
                                            " customercontact_gid," +
                                            " customer_gid," +
                                            " customercontact_name, " +
                                            " customerbranch_name, " +
                                            " email, " +
                                            " mobile, " +
                                             " main_contact, " +
                                            " designation," +
                                            " address1," +
                                            " address2," +
                                            " state," +
                                            " city," +
                                            " country_gid," +
                                            " region," +
                                            " fax, " +
                                            " zip_code, " +
                                            " fax_area_code, " +
                                            " fax_country_code," +
                                            " gst_number, " +
                                             " created_by," +
                                           "created_date" +

                                            ") values (" +
                                            "'" + msGetGid1 + "', " +
                                            "'" + msGetGid + "', " +
                                            "'" + excustomername + "'," +
                                            "'" + exclcustomer_branch + "'," +
                                            "'" + exemail + "'," +
                                            "'" + excustomermobile + "'," +
                                            "'Y'," +
                                            "'" + exdesignation + "'," +
                                            "'" + exaddress1 + "'," +
                                            "'" + exaddress2 + "'," +
                                            "'" + exstate + "'," +
                                            "'" + excity + "'," +
                                             "'" + lscountrygid + "'," +
                                            "'" + lsregiongid + "'," +
                                             "'" + exfaxnumber + "'," +
                                             "'" + expostalcode + "'," +
                                             "'" + values.fax_area_code + "'," +
                                             "'" + values.fax_country_code + "'," +
                                           "'" + values.gst_number + "'," +
                                           "'" + user_gid + "'," +
                                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }

                                        if (mnResult != 0)
                                        {

                                            msSQL = " INSERT INTO crm_trn_tleadbank " +
                                                   " (leadbank_gid, " +
                                                   " customer_gid, " +
                                                   " leadbank_name," +
                                                   " leadbank_address1, " +
                                                   " leadbank_address2, " +
                                                   " leadbank_city, " +
                                                   " leadbank_code, " +
                                                   " leadbank_state, " +
                                                   " leadbank_pin, " +
                                                   " leadbank_country, " +
                                                   " leadbank_region, " +
                                                   " approval_flag, " +
                                                   " leadbank_id, " +
                                                   " status, " +
                                                   " main_branch," +
                                                   " main_contact," +
                                                   " customer_type," +
                                                   " created_by, " +
                                                   " created_date)" +
                                                   " values ( " +
                                                   "'" + msGetGid2 + "'," +
                                                   "'" + msGetGid + "'," +
                                                   "'" + excustomername + "'," +
                                                   "'" + exaddress1 + "'," +
                                                   "'" + exaddress2 + "'," +
                                                   "'" + excity + "'," +
                                                   "'" + lscustomercode + "'," +
                                                   "'" + exstate + "'," +
                                                   "'" + expostalcode + "'," +
                                                   "'" + lscountrygid + "'," +
                                                   "'" + lsregiongid + "'," +
                                                   "'Approved'," +
                                                   "'" + exclcustomer_code + "'," +
                                                   "'Y'," +
                                                   "'Y'," +
                                                   "'Y'," +
                                                    "'" + excustomertype + "'," +
                                                   "'" + user_gid + "'," +
                                                   "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }

                                        if (mnResult != 0)
                                        {
                                            msSQL = " INSERT into crm_trn_tleadbankcontact (" +
                                                " leadbankcontact_gid, " +
                                                " leadbank_gid," +
                                                " leadbankbranch_name, " +
                                                " leadbankcontact_name," +
                                                " email," +
                                                " mobile," +
                                                " designation," +
                                                " did_number," +
                                                " created_date," +
                                                " created_by," +
                                                " address1," +
                                                " address2, " +
                                                " state, " +
                                                " country_gid, " +
                                                " city, " +
                                                " pincode, " +
                                                " region_name, " +
                                                " main_contact," +
                                                " phone1," +
                                                " area_code1," +
                                                " country_code1," +
                                                " fax," +
                                                " fax_area_code," +
                                                " fax_country_code)" +
                                                " values (" +
                                                " '" + msGetGid3 + "'," +
                                                " '" + msGetGid2 + "'," +
                                                "'" + lscustomercode + "'," +
                                                "'" + exccontactpersonname + "'," +
                                                "'" + exemail + "'," +
                                                "'" + excustomermobile + "'," +
                                                "'" + exdesignation + "'," +
                                                "'0'," +
                                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                                "'" + user_gid + "'," +
                                                "'" + exaddress1 + "'," +
                                                "'" + exaddress2 + "'," +
                                                "'" + exstate + "'," +
                                                "'" + lscountrygid + "'," +
                                                "'" + excity + "'," +
                                                "'" + expostalcode + "'," +
                                                "'" + lsregiongid + "'," +
                                                "'Y'," +
                                                "'" + values.phone1 + "'," +
                                                "'" + values.area_code + "'," +
                                                "'" + values.country_code + "'," +
                                                "'" + values.fax + "'," +
                                                 "'" + values.fax_area_code + "'," +
                                                "'" + values.fax_country_code + "')";

                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }

                                        if (mnResult != 0)
                                        {
                                            values.status = true;
                                            values.message = "Customer Added Successfully";
                                        }
                                        else
                                        {
                                            values.status = false;
                                            values.message = "Error While Adding Customer";
                                        }
                                    }




                                }
                            }
                        }
                    }
                }


                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.Message.ToString();
                    return;
                }
                //  Nullable<DateTime> ldcodecreation_date;


            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.Message.ToString();
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Adding Customer!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            

        }
        //For Customer Branch Add
        public void DaPostCustomerbranch( customerbranch_list values)
        {
            try {
              
                msGetGid1 = objcmnfunctions.GetMasterGID("BCCM");
            msSQL = " insert into crm_mst_tcustomercontact( " +
                    " customercontact_gid, " +
                    " customer_gid, " +
                    " customerbranch_name, " +
                    " address1, " +
                    " address2, " +
                    " city, " +
                    " state, " +
                    " region, " +
                    " designation, " +
                    " zip_code, " +
                    " country_gid, " +
                    " mobile, " +
                    " customercontact_name, " +
                    " email " +
                    ") VALUES (" +

                       "'" + msGetGid1 + "'," +
                     "'" + values.customer_gid + "'," +
                     "'" + values.customerbranch_name+ "'," +
                     "'" + values.address1 + "'," +
                     "'" + values.address2 + "'," +
                     "'" + values.customer_city + "'," +
                     "'" + values.customer_state + "'," +
                     "'" + values.region_name + "'," +
                     "'" + values.designation + "'," +
                     "'" + values.customer_pin + "'," +
                     "'" + values.country_name + "'," +
                     "'" + values.mobiles.e164Number + "'," +
                     "'" + values.customercontact_name + "'," +
                     "'" + values.email + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = " Customer Branch Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updated Customer Branch";
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while  While Updated Customer Branch !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
        public void DaGetSmrTrnCustomerBranch(string customer_gid, MdlSmrTrnCustomerSummary values)
        {
            try {
               
                msSQL = " SELECT distinct a.customercontact_gid as customer_gid, a.customerbranch_name, concat(a.address1,'|',a.address2) as customer_address, " +
                  " a.city as customer_city, a.state as customer_state, a.zip_code as customer_pin,b.country_name,a.customercontact_name,a.mobile,a.designation " +
                  " from crm_mst_tcustomercontact a left join adm_mst_tcountry b on a.country_gid = b.country_gid " +
                  " where customer_gid = '" + customer_gid + "' ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<smrcustomerbranch_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new smrcustomerbranch_list
                    {
                        customer_gid = dt["customer_gid"].ToString(),
                        customerbranch_name = dt["customerbranch_name"].ToString(),
                        customer_city = dt["customer_city"].ToString(),
                        customer_state = dt["customer_state"].ToString(),
                        customer_address = dt["customer_address"].ToString(),
                        customer_pin = dt["customer_pin"].ToString(),
                        country_name = dt["country_name"].ToString(),
                        customercontact_name = dt["customercontact_name"].ToString(),
                        designation = dt["designation"].ToString(),
                        mobile = dt["mobile"].ToString(),
                    });

                    values.smrcustomerbranch_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured whileLoading Customer Branch !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaGetCustomerReportExport(MdlSmrTrnCustomerSummary values)
        {
            try {
                
                msSQL = " Select distinct UCASE(a.customer_id) as CustomerId,a.customer_type as CustomerType,a.customer_name as CustomerName,a.customer_state as State,concat(c.customercontact_name,' / ',c.mobile,' / ',c.email) as ContactDetails, " +
                    " case when d.region_name is null then concat(a.customer_city,' / ',a.customer_state)" +
                    " when d.region_name is not null " +
                    " then Concat(d.region_name,' / ',a.customer_city,' / ',a.customer_state) end as RegionName , a.customer_type as CustomerType, " +
                    " (SELECT DATE_FORMAT(MAX(salesorder_date), '%d %b %Y') FROM smr_trn_tsalesorder WHERE customer_gid = a.customer_gid) AS LastOrderDate," +
                    " (SELECT DATE_FORMAT(Min(salesorder_date), '%d %b %Y') FROM smr_trn_tsalesorder WHERE customer_gid = a.customer_gid) AS FirstOrderDate, " +
                    " CONCAT((SELECT DATE_FORMAT(MIN(salesorder_date), '%d %b %Y') FROM smr_trn_tsalesorder WHERE customer_gid = a.customer_gid),'  (', " +
                    " (SELECT CONCAT(DATEDIFF(CURDATE(), MIN(salesorder_date)), ' days', ')') FROM smr_trn_tsalesorder WHERE customer_gid = a.customer_gid)) as CustomerSince " +
                    " from crm_mst_tcustomer a" +
                    " left join crm_mst_tregion d on a.customer_region =d.region_gid " +
                    " left join crm_mst_tcustomercontact c on a.customer_gid=c.customer_gid " +
                    " order by a.customer_id asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Customer Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                string lspath = ConfigurationManager.AppSettings["exportexcelfile"] + "/customer/export" + "/" + lscompany_code + "/" + "Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/TestReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(lspath)))
                        System.IO.Directory.CreateDirectory(lspath);
                }

                string lsname2 = "Customer_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                string lspath1 = ConfigurationManager.AppSettings["exportexcelfile"] + "/customer/export" + "/" + lscompany_code + "/" + "Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsname2;

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(lspath1);
                using (var range = workSheet.Cells[1, 1, 1, 8])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(file);

                var getModuleList = new List<customerexport_list>();
                if (dt_datatable.Rows.Count != 0)
                {

                    getModuleList.Add(new customerexport_list
                    {
                        lsname2 = lsname2,
                        lspath1 = lspath1,
                    });
                    values.customerexport_list = getModuleList;

                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Export Customer !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }



        public void DaGetbranch( string customer_gid,MdlSmrTrnCustomerSummary values)
        {
            try {
              
                msSQL = " Select customercontact_gid,customer_gid,customerbranch_name " +
                " from crm_mst_tcustomercontact" +
            " where customer_gid = '" + customer_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<branch_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new branch_list
                    {
                        customerbranch_name = dt["customerbranch_name"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customercontact_gid = dt["customercontact_gid"].ToString(),
                    });
                    values.branch_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Customer Branch Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }


        public void DaPostCustomercontact(string user_gid,customercontact_list values)
        {
            try {
               
                msGetGid1 = objcmnfunctions.GetMasterGID("BCCM");
            msSQL = " INSERT INTO crm_mst_tcustomercontact" +
                " (customercontact_gid," +
                " customer_gid," +
                " customerbranch_name," +
                " customercontact_name," +
                " email," +
                " mobile," +
                " designation," +
                " address1," +
                " address2," +
                " city," +
                " state," +
                " country_gid," +
                " zip_code,"+
                " created_by," +
                " created_date " +
                 ") VALUES (" +

                       "'" + msGetGid1 + "'," +
                     "'" + values.customer_gid + "'," +
                     "'" + values.customerbranch_name + "'," +
                     "'" + values.customercontact_name + "'," +
                     "'" + values.email + "'," +
                     "'" + values.mobiles.e164Number + "'," +
                     "'" + values.designation + "'," +
                     "'" + values.address1 + "'," +
                     "'" + values.address2 + "'," +
                     "'" + values.city + "'," +
                     "'" + values.state + "'," +
                     "'" + values.country_name + "'," +
                     "'" + values.zip_code + "'," +
                      "'" + user_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";


            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = " Customer Contact Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Customer Contact";
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Adding Customer Contact !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           

        }
        public void DaGetCustomerTypeSummary(MdlSmrTrnCustomerSummary values)
        {
            msSQL = "select customertype_gid,customer_type from crm_mst_tcustomertype ORDER BY customertype_gid ASC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmodulelist = new List<customertype_list1>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getmodulelist.Add(new customertype_list1
                    {
                        customertype_gid1 = dt["customertype_gid"].ToString(),
                        customer_type1 = dt["customer_type"].ToString(),
                    });
                    values.customertype_list1 = getmodulelist;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetSmrTrnCustomerContact(string customer_gid, MdlSmrTrnCustomerSummary values)
        {
            try {
              
                msSQL = "  select a.customercontact_gid,a.customer_gid,a.customerbranch_name," +
                " a.customercontact_name, a.email, a.mobile, a.designation, a.did_number, " +
                "concat(a.address1,'|',a.address2)as address1,a.city,a.state,a.country_gid," +
                "c.country_name,a.zip_code,b.customer_code " +
                " from crm_mst_tcustomercontact a" +
                " left join crm_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                " left join adm_mst_tcountry c on a.country_gid = c.country_gid" +
                " where a.customer_gid = '" + customer_gid + "'" +
                " union " +
                " select a.customercontact_gid,a.customer_gid,a.customerbranch_name," +
                " a.customercontact_name, a.email, a.mobile, a.designation, " +
                " a.did_number,concat(a.address1,'|',a.address2)as address1,a.city,a.state," +
                " a.country_gid,c.country_name,a.zip_code, b.customer_code " +
                " from crm_mst_tcustomercontact a" +
                " left join crm_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                " left join adm_mst_tcountry c on a.country_gid = c.country_gid" +
                " where b.customergroup_gid = '" + customer_gid + "'" +
                " group by customercontact_gid order by customercontact_name asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<customercontact_list1>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new customercontact_list1
                    {
                        customer_gid = dt["customer_gid"].ToString(),
                        customerbranch_name = dt["customerbranch_name"].ToString(),
                        city = dt["city"].ToString(),
                        state = dt["state"].ToString(),
                        customercontact_name = dt["customercontact_name"].ToString(),
                        designation = dt["designation"].ToString(),
                        zip_code = dt["zip_code"].ToString(),
                        country_name = dt["country_name"].ToString(),
                        mobiles = dt["mobile"].ToString(),
                        address1 = dt["address1"].ToString(),
                        address2 = dt["address1"].ToString(),
                    });

                    values.customercontact_list1 = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Customer Contact!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
    }
}