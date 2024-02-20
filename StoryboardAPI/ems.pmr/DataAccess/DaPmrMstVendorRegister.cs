﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.pmr.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
//using System.Web;
//using OfficeOpenXml;
using System.Configuration;
using System.IO;
//using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
using System.Web;
using System.Net.NetworkInformation;
using System.Web.Http.Results;
using System.ComponentModel;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using System.Xml.Linq;
using OfficeOpenXml.Style;

using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.pmr.DataAccess
{
    public class DaPmrMstVendorRegister
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lscountry, lsCountryGID, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetGid2, msGetPrivilege_gid,
            final_path, msGetModule2employee_gid, maGetGID, lsvendor_code, msUserGid, maGetGID1;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        private object msGetGID;
        private string mrGetGID;
        private string exclproducttypecode;
        private string mcGetGID;

        public void DaGetVendorRegisterSummary(MdlPmrMstVendorRegister values)
        {
            try
            {
                 
                msSQL = " SELECT vendorregister_gid, vendor_code, " +
                " vendor_companyname, contactperson_name, " +
                " contact_telephonenumber,vendor_status, " + "case when active_flag = 'Y' then 'Active' else 'In-Active' end as active_flag" +
                " from acp_mst_tvendorregister " +
                " Order by  vendorregister_gid desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getvendor_lists>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getvendor_lists
                        {

                            vendorregister_gid = dt["vendorregister_gid"].ToString(),
                            vendor_code = dt["vendor_code"].ToString(),
                            vendor_companyname = dt["vendor_companyname"].ToString(),
                            contactperson_name = dt["contactperson_name"].ToString(),
                            contact_telephonenumber = dt["contact_telephonenumber"].ToString(),
                            vendor_status = dt["vendor_status"].ToString(),
                            active_flag = dt["active_flag"].ToString()
                        });
                        values.Getvendor_lists = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting vendor register summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetcountry(MdlPmrMstVendorRegister values)
        {
            try
            {
               
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
                values.message = "Exception occured while getting country";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaGetcurrency(MdlPmrMstVendorRegister values)
        {
            try
            {
                
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
                values.message = "Exception occured while getting currency!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaGettax(MdlPmrMstVendorRegister values)
        {
            try
            {
                
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
                values.message = "Exception occured while getting tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaPostVendorRegister(string user_gid, vendor_list values)

        {
            try
            {
               
                msSQL = " select * from acp_mst_tvendorregister where vendor_code= '" + values.vendor_code + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    //values.status = true;
                    values.message = "Vendor code Already Exist";
                }
                else
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("PVRR");
                    msGetGid2 = objcmnfunctions.GetMasterGID("PVRM");
                    msGetGid = objcmnfunctions.GetMasterGID("SADM");
                    msSQL = " SELECT currency_code FROM crm_trn_tcurrencyexchange WHERE currencyexchange_gid='" + values.currencyname + "' ";
                    string lscurrency_code = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " insert into acp_mst_tvendorregister (" +
                        " vendorregister_gid, " +
                        " vendor_code, " +
                        " vendor_companyname, " +
                        " contactperson_name, " +
                        " contact_telephonenumber, " +
                        " email_id," +
                        " tin_number," +
                        " excise_details," +
                        " pan_number," +
                        " servicetax_number," +
                        " cst_number," +
                        " bank_details," +
                        " ifsc_code," +
                        " rtgs_code, " +
                        " vendor_status, " +
                        " address_gid," +
                        " tax_gid," +
                        " currencyexchange_gid) " +
                        " values (" +
                        "'" + msGetGid1 + "', " +
                        "'" + values.vendor_code + "', " +
                        "'" + values.vendor_companyname + "'," +
                        "'" + values.contactperson_name + "'," +
                        "'" + values.mobile.e164Number + "'," +
                        "'" + values.email_address + "'," +
                        "'" + values.tin_number + "'," +
                        "'" + values.excise_details + "'," +
                        "'" + values.pan_number + "'," +
                        "'" + values.servicetax_number + "'," +
                        "'" + values.cst_number + "'," +
                        "'" + values.bank_details + "'," +
                        "'" + values.ifsc_code + "'," +
                        "'" + values.rtgs_code + "'," +
                        "'Vendor Approved'," +
                        "'" + msGetGid + "', " +
                        "'" + values.taxname + "'," +
                        "'" + values.currencyname + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = " insert into acp_mst_tvendor (" +
                        " vendor_gid, " +
                        " vendor_code, " +
                        " vendor_companyname, " +
                        " contactperson_name, " +
                        " contact_telephonenumber, " +
                        " email_id," +
                        " tin_number," +
                        " excise_details," +
                        " pan_number," +
                        " servicetax_number," +
                        " cst_number," +
                        " bank_details," +
                        " ifsc_code," +
                        " rtgs_code, " +
                        " autopo, " +
                        " address_gid," +
                        " tax_gid," +
                        " currencyexchange_gid) " +
                        " values (" +
                        "'" + msGetGid2 + "', " +
                        "'" + values.vendor_code + "', " +
                        "'" + values.vendor_companyname + "'," +
                        "'" + values.contactperson_name + "'," +
                        "'" + values.mobile.e164Number + "'," +
                        "'" + values.email_address + "'," +
                        "'" + values.tin_number + "'," +
                        "'" + values.excise_details + "'," +
                        "'" + values.pan_number + "'," +
                        "'" + values.servicetax_number + "'," +
                        "'" + values.cst_number + "'," +
                        "'" + values.bank_details + "'," +
                        "'" + values.ifsc_code + "'," +
                        "'" + values.rtgs_code + "'," +
                        "'N'," +
                        "'" + msGetGid + "', " +
                        "'" + values.taxname + "'," +
                        "'" + values.currencyname + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (mnResult != 0)
                    {

                        msSQL = " insert into adm_mst_taddress " +
                        " (address_gid, " +
                        " address1, " +
                        " address2, " +
                        " city, " +
                        " state, " +
                        " postal_code, " +
                        " country_gid, " +
                        " fax ) " +
                        " values (" +
                        "'" + msGetGid + "', " +
                        "'" + values.address + "'," +
                        "'" + values.address2 + "'," +
                        "'" + values.city + "'," +
                        "'" + values.state_name + "'," +
                        "'" + values.postal_code + "'," +
                        "'" + values.countryname + "'," +
                        "'" + values.fax_name + "')";

                        mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);


                    }
                    if (mnResult1 != 0)
                    {
                        values.status = true;
                        values.message = "Vendor Added Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Vendor";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding vendor register!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }


        public void DaGetDocumentType(MdlPmrMstVendorRegister values)
        {
            try
            {
               
                msSQL = "select documenttype_gid,documenttype_name " +
     " from  acp_mst_tvendordocumenttype  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetDocumentType>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetDocumentType
                        {
                            documenttype_gid = dt["documenttype_gid"].ToString(),
                            documenttype_name = dt["documenttype_name"].ToString(),
                        });
                        values.GetDocumentType = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting document type!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        public void DaUpdateVendorStatus(string user_gid, ActiveStatus_list values)
        {
            try
            {
                
                msSQL = " update acp_mst_tvendorregister set active_flag='" + values.active_flag + "',approved_remarks='" + values.product_desc + "' where vendorregister_gid='" + values.vendorregister_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Vendor Status Updated Successfully";

                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Vendor Status ";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while updating vendor status!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }


        public void DaGetVendorRegisterDetail(string vendorregister_gid, MdlPmrMstVendorRegister values)
        {
            try
            {
                
                msSQL = " SELECT a.vendorregister_gid, a.currencyexchange_gid, a.vendor_code,f.tax_name,d.currency_code," +
" a.vendor_companyname, a.contactperson_name," +
" a.contact_telephonenumber, a.email_id, b.address1," +
" b.address2, b.city,a.tin_number,a.excise_details,a.pan_number," +
" a.servicetax_number,a.cst_number, a.bank_details, a.ifsc_code, a.rtgs_code," +
" a.blacklist_remarks, a.blacklist_flag, a.blacklist_date, a.blacklist_by, " +
" b.state, b.postal_code," +
" b.country_gid, b.fax," +
" c.country_name" +
" FROM acp_mst_tvendorregister a " +
" left join adm_mst_taddress b on  a.address_gid = b.address_gid " +
" left join adm_mst_tcountry c on  b.country_gid = c.country_gid " +
" left join crm_trn_tcurrencyexchange d on a.currencyexchange_gid = d.currencyexchange_gid " +
" left join acp_mst_ttax f on a.tax_gid = f.tax_gid " +
" where a.vendorregister_gid = '" + vendorregister_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<editvendorregistersummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new editvendorregistersummary_list
                        {

                            vendorregister_gid = dt["vendorregister_gid"].ToString(),

                            vendor_code = dt["vendor_code"].ToString(),
                            tax_name = dt["tax_name"].ToString(),
                            currency_code = dt["currency_code"].ToString(),
                            vendor_companyname = dt["vendor_companyname"].ToString(),
                            contactperson_name = dt["contactperson_name"].ToString(),
                            contact_telephonenumber = dt["contact_telephonenumber"].ToString(),
                            email_id = dt["email_id"].ToString(),
                            address1 = dt["address1"].ToString(),
                            address2 = dt["address2"].ToString(),
                            city = dt["city"].ToString(),
                            tin_number = dt["tin_number"].ToString(),
                            excise_details = dt["excise_details"].ToString(),
                            pan_number = dt["pan_number"].ToString(),
                            servicetax_number = dt["servicetax_number"].ToString(),
                            cst_number = dt["cst_number"].ToString(),
                            bank_details = dt["bank_details"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            rtgs_code = dt["rtgs_code"].ToString(),
                            blacklist_remarks = dt["blacklist_remarks"].ToString(),
                            blacklist_flag = dt["blacklist_flag"].ToString(),
                            blacklist_date = dt["blacklist_date"].ToString(),
                            blacklist_by = dt["blacklist_by"].ToString(),
                            state = dt["state"].ToString(),
                            postal_code = dt["postal_code"].ToString(),
                            fax = dt["fax"].ToString(),
                            country_name = dt["country_name"].ToString(),

                        });
                        values.editvendorregistersummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting vendor register details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }


        public void DaPostVendorRegisterUpdate(string user_gid, vendor_listaddinfo values)


        {
            try
            {
                

                msSQL = " SELECT country_gid FROM  adm_mst_tcountry WHERE country_name='" + values.country_name + "' ";
                string country_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT currencyexchange_gid FROM crm_trn_tcurrencyexchange WHERE currency_code='" + values.currency_code + "' ";
                string currencyexchange_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT tax_gid FROM acp_mst_ttax WHERE tax_name='" + values.tax_name + "' ";
                string tax_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " UPDATE acp_mst_tvendorregister a,adm_mst_taddress b " +
                    " SET a.vendor_companyname = '" + values.vendor_companyname + "'," +
                    " a.contactperson_name = '" + values.contactperson_name + "'," +
                    " a.contact_telephonenumber = '" + values.contact_telephonenumber + "'," +
                    " a.email_id = '" + values.email_id + "'," +
                    " a.tin_number = '" + values.tin_number + "'," +
                    " a.excise_details = '" + values.excise_details + "'," +
                    " a.pan_number = '" + values.pan_number + "'," +
                    " a.servicetax_number = '" + values.servicetax_number + "'," +
                    " a.cst_number = '" + values.cst_number + "'," +
                    " a.bank_details = '" + values.bank_details + "'," +
                    " a.ifsc_code = '" + values.ifsc_code + "'," +
                    " a.rtgs_code = '" + values.rtgs_code + "'," +
                    " b.address1 = '" + values.address1 + "'," +
                    " b.address2 = '" + values.address2 + "'," +
                    " b.city = '" + values.city + "'," +
                    " b.state = '" + values.state + "'," +
                    " b.postal_code = '" + values.postal_code + "'," +
                    " b.country_gid = '" + country_gid + "'," +
                    " b.fax = '" + values.fax + "'," +
                    " a.tax_gid = '" + tax_gid + "'," +
                    " a.currencyexchange_gid = '" + currencyexchange_gid + "'" +
                    " WHERE a.vendor_code = '" + values.vendor_code + "'";

                mnResult2 = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult2 != 0)
                {
                    msSQL = " UPDATE acp_mst_tvendor a,adm_mst_taddress b " +
                    " SET a.vendor_companyname = '" + values.vendor_companyname + "'," +
                    " a.contactperson_name = '" + values.contactperson_name + "'," +
                    " a.contact_telephonenumber = '" + values.contact_telephonenumber + "'," +
                    " a.email_id = '" + values.email_id + "'," +
                    " a.tin_number = '" + values.tin_number + "'," +
                    " a.excise_details = '" + values.excise_details + "'," +
                    " a.pan_number = '" + values.pan_number + "'," +
                    " a.servicetax_number = '" + values.servicetax_number + "'," +
                    " a.cst_number = '" + values.cst_number + "'," +
                    " a.bank_details = '" + values.bank_details + "'," +
                    " a.ifsc_code = '" + values.ifsc_code + "'," +
                    " a.rtgs_code = '" + values.rtgs_code + "'," +
                    " b.address1 = '" + values.address1 + "'," +
                    " b.address2 = '" + values.address2 + "'," +
                    " b.city = '" + values.city + "'," +
                    " b.state = '" + values.state + "'," +
                    " b.postal_code = '" + values.postal_code + "'," +
                    " b.country_gid = '" + country_gid + "'," +
                    " b.fax = '" + values.fax + "'," +
                    " a.tax_gid = '" + tax_gid + "'," +
                    " a.currencyexchange_gid = '" + currencyexchange_gid + "'" +
                    " WHERE a.vendor_code = '" + values.vendor_code + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)

                {

                    values.status = true;
                    values.message = "Vendor Updated Successfully";

                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Vendor ";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while updating vendor register!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }


        public void DaVendorRegisterSummaryDelete(string vendorregister_gid, vendor_listaddinfo values)
        {
            try
            {
                
                msSQL = " SELECT vendor_code FROM  acp_mst_tvendorregister WHERE vendorregister_gid='" + vendorregister_gid + "' ";
                string vendor_code = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "  Delete from acp_mst_tvendor where vendor_code='" + vendor_code + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "  Delete from acp_mst_tvendorregister where vendorregister_gid='" + vendorregister_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Vendor Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deleting Vendor";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while deleting vendor!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaPostVendorRegisterAdditionalInformation(string user_gid, vendor_listaddinfo values)

        {
            try
            {
               
                if (values.bank_details == null && values.cst_number == null && values.excise_details == null
&& values.ifsc_code == null && values.pan_number == null &&
values.rtgs_code == null && values.servicetax_number == null && values.tin_number == null)
                { values.message = "Please Enter The Values"; }
                else
                {
                    msSQL = " update acp_mst_tvendorregister set" +
                            " bank_details='" + values.bank_details + "', " +
                            " cst_number='" + values.cst_number + "'," +
                            " excise_details='" + values.excise_details + "'," +
                            " ifsc_code='" + values.ifsc_code + "'," +
                            " pan_number='" + values.pan_number + "'," +
                            " rtgs_code='" + values.rtgs_code + "'," +
                            " servicetax_number='" + values.servicetax_number + "'," +
                            " tin_number='" + values.tin_number + "'" +
                            " WHERE vendorregister_gid = '" + values.vendorregister_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = " SELECT vendor_code FROM  acp_mst_tvendorregister WHERE vendorregister_gid='" + values.vendorregister_gid + "' ";
                        string vendor_code = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "  Select vendor_gid from acp_mst_tvendor where vendor_code='" + vendor_code + "'  ";
                        string vendor_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " update acp_mst_tvendor set" +
                            " bank_details='" + values.bank_details + "', " +
                            " cst_number='" + values.cst_number + "'," +
                            " excise_details='" + values.excise_details + "'," +
                            " ifsc_code='" + values.ifsc_code + "'," +
                            " pan_number='" + values.pan_number + "'," +
                            " rtgs_code='" + values.rtgs_code + "'," +
                            " servicetax_number='" + values.servicetax_number + "'," +
                            " tin_number='" + values.tin_number + "'" +
                            " WHERE vendor_gid = '" + vendor_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Vendor Additional Information Updated Successfully";

                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Vendor ";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding additional information in vendor!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaVendorImportExcel (HttpRequest httpRequest, string user_gid, result objResult, product_list values)
        {
            try
            {
                 
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
                    bool status1;


                    status1 = objcmnfunctions.UploadStream(ConfigurationManager.AppSettings["blob_containername"], lscompany_code + "/" + "Lead Import/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, FileExtension, ms);
                    ms.Close();

                    // Connect to the storage account
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"].ToString());
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(ConfigurationManager.AppSettings["blob_containername"].ToString());

                    // Get a reference to the blob
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(lscompany_code + "/" + "Lead Import/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension);
                    string path_url = lscompany_code + "/" + "Lead Import/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;


                    // Download the blob's contents and read Excel file
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // await blockBlob.DownloadToStreamAsync(memoryStream);

                        blockBlob.DownloadToStream(memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.Position = 0;
                        // Load Excel package from the memory stream
                        //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (ExcelPackage package = new ExcelPackage(memoryStream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"]; // worksheet name
                                                                                              // Remove the first row
                            worksheet.DeleteRow(1);

                            // Convert Excel data to array list format
                            List<List<string>> excelData = new List<List<string>>();

                            for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
                            {
                                List<string> rowData = new List<string>();
                                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                                {
                                    var cellValue = worksheet.Cells[row, col].Value?.ToString();
                                    rowData.Add(cellValue);
                                }

                                string vendor_code = rowData[0];
                                string vendor_companyname = rowData[1];
                                string contactperson_name = rowData[2];
                                string contact_telephonenumber = rowData[3];
                                string email_id = rowData[4];
                                string fax = rowData[5];
                                string address1 = rowData[6];
                                string address2 = rowData[7];
                                string city = rowData[8];
                                string state = rowData[9];
                                string postal_code = rowData[10];
                                string country_name = rowData[11];
                                string tax = rowData[12];


                                msSQL = " Select vendor_gid " +
                                        " from acp_mst_tvendor where " +
                                        " vendor_code = '" + vendor_code + "'";
                                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                                if (objOdbcDataReader.HasRows == true)
                                {

                                    objOdbcDataReader.Close();
                                }
                                if (country_name == "India")
                                {
                                    lsCountryGID = "CN06070099";
                                }
                                else
                                {
                                    msSQL = " select country_gid from adm_mst_tcountry where country_name= " +
                                      "where country_name = '" + country_name + "'";
                                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                                    if (objOdbcDataReader.HasRows == true)
                                    {
                                        objOdbcDataReader.Read();
                                        lsCountryGID = objOdbcDataReader["country_gid"].ToString();
                                        objOdbcDataReader.Close();
                                    }
                                }
                                //insertion in table

                                mcGetGID = objcmnfunctions.GetMasterGID("PVRM");
                                maGetGID = objcmnfunctions.GetMasterGID("SADM");
                                msSQL = " insert into acp_mst_tvendorregister (" +
                                        " vendorregister_gid, " +
                                        " vendor_code, " +
                                        " vendor_companyname, " +
                                        " contactperson_name, " +
                                        " contact_telephonenumber, " +
                                        " email_id," +
                                        " vendor_status, " +
                                        " created_date, " +
                                        " created_by, " +
                                        " address_gid ) " +
                                        " values (" +
                                        "'" + mcGetGID + "', " +
                                        "'" + vendor_code + "'," +
                                        "'" + vendor_companyname + "'," +
                                        "'" + contactperson_name + "'," +
                                        "'" + contact_telephonenumber + "'," +
                                        "'" + email_id + "'," +
                                        "'Vendor Approved'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'" + user_gid + "'," +
                                        "'" + maGetGID + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                if (mnResult != 0)
                                {
                                    msSQL = " insert into adm_mst_taddress " +
                                    " (address_gid, " +
                                    " address1, " +
                                    " address2, " +
                                    " city, " +
                                    " state, " +
                                    " postal_code, " +
                                    " country_gid, " +
                                    " fax ) " +
                                    " values (" +
                                    "'" + maGetGID + "', " +
                                    "'" + address1 + "'," +
                                    "'" + address2 + "'," +
                                    "'" + city + "'," +
                                    "'" + state + "'," +
                                    "'" + postal_code + "'," +
                                    "'" + lsCountryGID + "'," +
                                    "'" + fax + "')";
                                    mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }

                                if (mnResult1 != 0)
                                {
                                    values.status = true;
                                    values.message = "Excel Uploaded Successfully";

                                }
                                else
                                {
                                    values.status = false;
                                    values.message = "Error While Uploading Excel";
                                }

                            }
                        }

                    }
                }

                catch (Exception ex)
                {
                   
                    objResult.message = ex.Message.ToString();
                }
                if (mnResult1 != 0)
                {
                    values.status = true;
                    values.message = "Excel Uploaded Successfully";

                }
            }

            catch (Exception ex)
            {
                values.message = "Exception occured while adding vendor template";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }



        public void DaGetVendorReportExport(MdlPmrMstVendorRegister values)
        {
            try
            {
                
                msSQL = " SELECT vendor_code as VendorCode, " +
" vendor_companyname as VendorCompany, contactperson_name as ContactPerson, " +
" contact_telephonenumber as ContactNumber,vendor_status as Status " +
" from acp_mst_tvendorregister " +
" Order by  vendorregister_gid desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                string lscompany_code = string.Empty;
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("vendor Report");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";

                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    string lspath = ConfigurationManager.AppSettings["exportexcelfile"] + "/vendor/export" + "/" + lscompany_code + "/" + "Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                    //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/TestReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    {
                        if ((!System.IO.Directory.Exists(lspath)))
                            System.IO.Directory.CreateDirectory(lspath);
                    }

                    string lsname2 = "vendor_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                    string lspath1 = ConfigurationManager.AppSettings["exportexcelfile"] + "/vendor/export" + "/" + lscompany_code + "/" + "Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsname2;

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

                    var getModuleList = new List<vendorexport_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {

                        getModuleList.Add(new vendorexport_list
                        {
                            lsname2 = lsname2,
                            lspath1 = lspath1,
                        });
                        values.vendorexport_list = getModuleList;

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
                values.message = "Exception occured while exporting vendor!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }


        public void DaPostdocumentImage(HttpRequest httpRequest, result objResult, string user_gid)
        {
            try
            {
                 

                HttpFileCollection httpFileCollection;
                string lsfilepath = string.Empty;
                string lsdocument_gid = string.Empty;
                MemoryStream ms_stream = new MemoryStream();
                string documenttype_gid = string.Empty;
                string lscompany_code = string.Empty;
                HttpPostedFile httpPostedFile;

                string lspath;
                string msGetGid;

                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                string vendorregister_gid = httpRequest.Form[1];
                string documenttype_name = httpRequest.Form[0];

                msSQL = "select documenttype_name  from  acp_mst_tvendordocumenttype  where documenttype_gid ='" + documenttype_name + "'";
                documenttype_gid = objdbconn.GetExecuteScalar(msSQL);

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

                            bool status1;
                            //status1 = objcmnfunctions.UploadStream(ConfigurationManager.AppSettings["blob_containername"], lscompany_code + "/" + "CRM/Product/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, FileExtension, ms);
                            //ms.Close();

                            //final_path = ConfigurationManager.AppSettings["blob_containername"] + "/" + lscompany_code + "/" + "CRM/Product/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                            //msSQL = "update pmr_mst_tproduct set " +
                            //" product_image='" + ConfigurationManager.AppSettings["blob_imagepath1"] + final_path + msdocument_gid + FileExtension + ConfigurationManager.AppSettings["blob_imagepath2"] +'&' + ConfigurationManager.AppSettings["blob_imagepath3"] + '&' + ConfigurationManager.AppSettings["blob_imagepath4"] + '&' + ConfigurationManager.AppSettings["blob_imagepath5"] +'&' + ConfigurationManager.AppSettings["blob_imagepath6"] + '&' + ConfigurationManager.AppSettings["blob_imagepath7"] + '&' + ConfigurationManager.AppSettings["blob_imagepath8"] + "'" +
                            // " where product_gid='" + product_gid + "'";


                            status1 = objcmnfunctions.UploadStream(ConfigurationManager.AppSettings["blob_containername"], lscompany_code + "/" + "PURCHASE/Product/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, FileExtension, ms);
                            ms.Close();

                            final_path = ConfigurationManager.AppSettings["blob_containername"] + "/" + lscompany_code + "/" + "PURCHASE/Product/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                            string httpsUrl = ConfigurationManager.AppSettings["blob_imagepath1"] + final_path + msdocument_gid + FileExtension + ConfigurationManager.AppSettings["blob_imagepath2"] +
                                                       '&' + ConfigurationManager.AppSettings["blob_imagepath3"] + '&' + ConfigurationManager.AppSettings["blob_imagepath4"] + '&' + ConfigurationManager.AppSettings["blob_imagepath5"] +
                                                       '&' + ConfigurationManager.AppSettings["blob_imagepath6"] + '&' + ConfigurationManager.AppSettings["blob_imagepath7"] + '&' + ConfigurationManager.AppSettings["blob_imagepath8"];


                            maGetGID1 = objcmnfunctions.GetMasterGID("SVDM");
                            msSQL = " insert into acp_mst_tvendorregisterdocument (" +
                                " document_gid, " +
                                " vendorregister_gid, " +
                                " document_type, " +
                                " document_name, " +
                                " created_date, " +
                                " file_path, " +
                                " created_by ) " +
                                " values (" +
                                "'" + maGetGID1 + "', " +
                                "'" + vendorregister_gid + "'," +
                                "'" + documenttype_name + "'," +
                                 "'" + documenttype_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + httpsUrl + "'," +
                                "'" + user_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                    }


                    if (mnResult != 0)
                    {
                        objResult.status = true;
                        objResult.message = "Document Added Successfully !!";
                    }
                    else
                    {
                        objResult.status = false;
                        objResult.message = "Error While Adding Document !!";
                    }

                }

                catch (Exception ex)
                {
                    objResult.message = ex.Message.ToString();
                }
                //return true;
            }
            catch (Exception ex)
            {
                objResult.message = "Exception occured while adding document image!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
            ex.Message.ToString() + "***********" + objResult.message.ToString() + "*****Query****" +
            msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
            DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }

        public void DaGetDocumentdtl(string vendorregister_gid, MdlPmrMstVendorRegister values)
        {
            try
            {
                
                msSQL = " select a.document_gid,a.document_type,a.document_name,date_format(a.created_date,'%d-%m-%Y')as created_date,a.file_path," +
                        " a.created_by,b.user_firstname from" +
                        " acp_mst_tvendorregisterdocument a" +
                        " left join adm_mst_tuser b on b.user_gid=a.created_by" +
                        " where vendorregister_gid='" + vendorregister_gid + "' " +
                        " group by a.document_gid order by DATE(a.created_date) asc, created_date desc  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetDocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetDocument_list
                        {

                          
                            created_date = dt["created_date"].ToString(),
                            document_gid = dt["document_gid"].ToString(),
                            document_type = dt["document_type"].ToString(),
                            document_name = dt["document_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            user_firstname = dt["user_firstname"].ToString(),
                            file_path = dt["file_path"].ToString(),


                        });
                        values.GetDocument_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting document details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
               $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
           ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
           msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
           DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             
        }

    }

}


