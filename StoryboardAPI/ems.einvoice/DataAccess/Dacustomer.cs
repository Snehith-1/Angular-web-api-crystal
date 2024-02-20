using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.einvoice.Models;
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

using OfficeOpenXml;
//using OfficeOpenXml.Style;
using ems.einvoice.Models;
using System.Net.NetworkInformation;
using System.Web.Http.Results;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.einvoice.DataAccess
{
    public class Dacustomer
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid2, msGetGid, msGetGid3, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaCustomersummary(Mdlcustomer values)
        {
            try
            {

                msSQL = " select UCASE(a.customer_id) as customer_id,c.customercontact_name,a.customer_gid,a.customer_name,concat(c.customercontact_name,' / ',c.mobile,' / ',c.email) as contact_details,case when d.region_name is null then concat(a.customer_city,' / ',a.customer_state) when d.region_name is not null  then Concat(d.region_name,' / ',a.customer_city,' / ',a.customer_state) end as region_name,concat(b.user_firstname,' ',b.user_lastname) as user_name, a.created_date " +
                    " from crm_mst_tcustomer a" +
                    " left join crm_mst_tregion d on a.customer_region =d.region_gid " +
                    " left join crm_mst_tcustomercontact c on a.customer_gid=c.customer_gid " +
                    "  left join adm_mst_tuser b on b.user_gid=a.created_by " +
                    " where (a.status <> 'Deleted' or a.status is null  )" +
                    " and a.main_branch = 'Y' and c.main_contact = 'Y' and a.customer = 'Y' order by a.created_date desc, customer_id desc, customer_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<CustomerSummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new CustomerSummary_list
                        {
                            customer_gid = dt["customer_gid"].ToString(),
                            customer_id = dt["customer_id"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            contact_details = dt["contact_details"].ToString(),
                            region_name = dt["region_name"].ToString(),
                            customercontact_name = dt["customercontact_name"].ToString(),
                            created_by = dt["user_name"].ToString(),
                            created_date = dt["created_date"].ToString(),
                        });
                        values.CustomerSummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Customer Summary!"; 
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
         "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }
        public void DaPostcustomer(string user_gid, Customer_list values)
        {
            try
            {

                msGetGid = objcmnfunctions.GetMasterGID("BCRM");
                msGetGid1 = objcmnfunctions.GetMasterGID("BCCM");
                msGetGid2 = objcmnfunctions.GetMasterGID("BLBP");

                msSQL = " Select country_name from adm_mst_tcountry where country_gid = '" + values.country + "'";
                string lscountry_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " Select region_name from crm_mst_tregion where region_gid = '" + values.Region + "'";
                string lsregion_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select customer_gid from crm_mst_tcustomer where " +
                        " customer_name='" + values.customername.Trim().Replace("'", "") + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == false)
                {
                    msSQL = " INSERT INTO crm_mst_tcustomer" +
                   " (customer_gid," +
                   " customer_id," +
                   " customer_name," +
                   " company_website," +
                   " customer_code," +
                   " customer_address," +
                   " customer_address2," +
                   " customer_state," +
                   " customer_country," +
                   " currency_gid," +
                   " customer_city," +
                   " customer_pin," +
                   " customer_region," +
                   " main_branch," +
                   " status, " +
                   " gst_number," +
                   " created_by," +
                   " created_date)" +
                   " values( " +
                   "'" + msGetGid + "'," +
                   "'" + values.customercode + "'," +
                   "'" + (values.customername).Trim().Replace("'", "") + "'," +
                   "'" + values.CompanyWebsite + "'," +
                   "'H.Q'," +
                   "'" + values.Address1 + "'," +
                   "'" + values.address2 + "'," +
                   "'" + values.state + "'," +
                   "'" + values.country + "'," +
                   "'" + values.currency + "'," +
                   "'" + values.city + "'," +
                   "'" + values.pincode + "'," +
                   "'" + lsregion_name + "','Y','Active'," +
                   "'" + values.gstnumber + "'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " INSERT INTO crm_mst_tcustomercontact" +
                            " (customercontact_gid," +
                            " customer_gid," +
                            " customerbranch_name, " +
                            " customercontact_name," +
                            " email," +
                            " mobile," +
                            " designation," +
                            " created_date," +
                            " created_by," +
                            " address1, " +
                            " address2, " +
                            " state, " +
                            " city, " +
                            " region, " +
                            " zip_code, " +
                            " main_contact," +
                            " fax," +
                            " fax_country_code," +
                            " gst_number)" +
                            " values( " +
                            "'" + msGetGid1 + "'," +
                            "'" + msGetGid + "'," +
                            "'H.Q', " +
                            "'" + values.contactpersonname + "'," +
                            "'" + values.Email_ID + "'," +
                            "'" + values.contacttelephonenumber
                            + "'," +
                            "'" + values.designation + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + user_gid + "', " +
                            "'" + values.Address1 + "'," +
                            "'" + values.address2 + "'," +
                            "'" + values.state + "'," +
                            "'" + values.city + "'," +
                            "'" + lsregion_name + "', " +
                            "'" + values.pincode + "'," +
                            " 'Y'," +
                            "'" + values.Fax + "'," +
                            "'" + values.Fax + "'," +
                            "'" + values.gstnumber + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

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
                           " created_by, " +
                           " created_date)" +
                           " values ( " +
                           "'" + msGetGid2 + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.customername.Trim().Replace("'", "") + "'," +
                           "'" + values.Address1 + "'," +
                           "'" + values.address2 + "'," +
                           "'" + values.city + "'," +
                           "'H.Q', " +
                           "'" + values.state + "'," +
                           "'" + values.pincode + "'," +
                           "'" + values.country + "'," +
                           "'" + lsregion_name + "', " +
                           "'Approved'," +
                           "'" + values.customercode + "'," +
                           "'Y'," +
                           "'Y'," +
                           "'Y'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid3 = objcmnfunctions.GetMasterGID("BLCC");
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
                                " fax_country_code)" +
                                " values (" +
                                " '" + msGetGid3 + "'," +
                                " '" + msGetGid2 + "'," +
                                "'H.Q', " +
                                "'" + values.contactpersonname + "'," +
                                "'" + values.Email_ID + "'," +
                                "'" + values.contacttelephonenumber + "'," +
                                "'" + values.designation + "'," +
                                "'0'," +
                                " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + user_gid + "'," +
                                "'" + values.Address1 + "'," +
                                "'" + values.address2 + "'," +
                                "'" + values.state + "'," +
                                "'" + values.country + "'," +
                                "'" + values.city + "'," +
                                "'" + values.pincode + "'," +
                                "'" + lsregion_name + "', " +
                                "'Y'," +
                                "'" + values.phone1 + "'," +
                                "'" + values.phonearea1 + "'," +
                                "'" + values.phoneCountrycode1 + "'," +
                                "'" + values.Fax + "'," +
                                "'" + values.Fax + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                else
                {
                    values.status = false;
                    values.message = "Customer Name Already Exist";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Inserting Customer!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
         "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaUpdatedCustomer(string user_gid, Customer_list values)
        {
            try
            {

                msSQL = " Update crm_mst_tcustomer set" +
                " customer_name = '" + values.customername.Trim().Replace("'", "") + "'," +
                " company_website = '" + values.CompanyWebsite + "'," +
                " customer_id = '" + values.customercode + "'," +
                " customer_address = '" + values.Address1.Replace("'", "") + "'," +
                " customer_address2 = '" + values.address2.Replace("'", "") + "'," +
                " customer_city = '" + values.city + "'," +
                " customer_region = '" + values.Region + "'," +
                " customer_state = '" + values.state + "'," +
                " customer_country = '" + values.country + "'," +
                " currency_gid ='" + values.currency + "'," +
                " gst_number='" + values.gstnumber + "'," +
                " customer_pin = '" + values.pincode + "'" +
                " where customer_gid = '" + values.Customer_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " Update crm_mst_tcustomercontact set" +
                   " customercontact_name = '" + values.contactpersonname + "'," +
                   " address1='" + values.Address1.Replace("'", "") + "', " +
                   " address2='" + values.address2.Replace("'", "") + "', " +
                   " state='" + values.state + "', " +
                   " country_gid='" + values.country + "', " +
                   " city='" + values.city + "', " +
                   " region='" + values.Region + "', " +
                   " zip_code='" + values.pincode + "'," +
                   " mobile = '" + values.contacttelephonenumber + "'," +
                   " email = '" + values.Email_ID + "'," +
                   " designation = '" + values.designation + "'," +
                   " country_code = '" + values.phoneCountrycode1 + "'," +
                   " fax = '" + values.Fax + "'," +
                   " gst_number='" + values.gstnumber + "'" +
                   " where customer_gid = '" + values.Customer_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " Select  leadbank_gid from crm_trn_tleadbank where customer_gid = '" + values.Customer_gid + "'";
                string lsleadbank_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update crm_trn_tleadbank set leadbank_country='" + values.country + "', " +
                        " leadbank_pin='" + values.pincode + "', " +
                        " leadbank_address1='" + values.Address1.Replace("'", "") + "', " +
                        " leadbank_city='" + values.city + "', " +
                        " leadbank_state='" + values.state + "', " +
                        " leadbank_region='" + values.Region + "', " +
                        " company_website='" + values.CompanyWebsite + "' " +
                        " where leadbank_gid='" + lsleadbank_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update crm_trn_tleadbankcontact set" +
                        " leadbankcontact_name = '" + values.contactpersonname + "'," +
                        " address1='" + values.Address1.Replace("'", "") + "'," +
                        " address2='" + values.address2.Replace("'", "") + "', " +
                        " state='" + values.state + "', " +
                        " country_gid='" + values.country + "', " +
                        " city='" + values.city + "', " +
                        " pincode='" + values.pincode + "', " +
                        " region_name='" + values.region_name + "', " +
                        " email = '" + values.Email_ID + "'," +
                        " mobile = '" + values.contacttelephonenumber + "'," +
                        " designation = '" + values.designation + "'," +
                        " country_code1 = '" + values.phoneCountrycode1 + "'," +
                        " fax = '" + values.Fax + "'" +
                        " where leadbank_gid='" + lsleadbank_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Customer Updated Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating customer";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Customer!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
        "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        } 
        public void DaGetcountrydropdown(MdlProduct values)
        {
            try
            {


                msSQL = "select country_name,country_gid from adm_mst_tcountry  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getcountrydropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getcountrydropdown
                        {
                            country_code = dt["country_name"].ToString(),
                            country_gid = dt["country_gid"].ToString(),

                        });
                        values.Getcountrydropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding Country dropdown!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.ToString() +
        "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          

        }
        public void DaGetcurrencydropdown(MdlProduct values)
        {
            try
            {


                msSQL = " select currency_code,currencyexchange_gid  " +
                    " from crm_trn_tcurrencyexchange ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getcurrencydropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getcurrencydropdown
                        {
                            currency_code = dt["currency_code"].ToString(),
                            currencyexchange_gid = dt["currencyexchange_gid"].ToString(),

                        });
                        values.Getcurrencydropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding currency dropdown!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
        "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaGetRegiondropdown(MdlProduct values)
        {
            try
            {


                msSQL = " select region_gid,region_name " +
                    " from crm_mst_tregion ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getregiondropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getregiondropdown
                        {
                            region_name = dt["region_name"].ToString(),
                            region_gid = dt["region_gid"].ToString(),

                        });
                        values.GetRegiondropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding region dropdown!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.ToString() +
        "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
            public Customer_list DaGetEditcustomer(string customer_gid)
        {
            try
            {

                Customer_list objCustomer_list = new Customer_list();
                msSQL = " SELECT a.customer_gid,a.customer_name,b.region_gid,a.gst_number, a.company_website, a.customer_id,c.phone,c.area_code,c.country_code,c.fax,c.fax_area_code," +
                    " a.customer_code,a.customer_address,a.customer_city,a.customer_gid,customer_country,d.currency_code,a.customer_state,c.country_code,c.fax_country_code," +
                    " a.customer_pin,b.region_name,customercontact_gid,customercontact_name,email,mobile,designation,a.customer_address2, " +
                    " d.currencyexchange_gid from crm_mst_tcustomer a" +
                    " left join crm_mst_tregion b on a.customer_region = b.region_gid " +
                    " left join crm_mst_tcustomercontact c on a.customer_gid=c.customer_gid " +
                    " left join crm_trn_tcurrencyexchange d on a.currency_gid=d.currencyexchange_gid" +
                    " where a.customer_gid ='" + customer_gid + "'and a.main_branch = 'Y' and c.main_contact = 'Y'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    objMySqlDataReader.Read();
                    objCustomer_list.customercode = objMySqlDataReader["customer_id"].ToString();
                    objCustomer_list.Customer_gid = objMySqlDataReader["customer_gid"].ToString();

                    objCustomer_list.customername = objMySqlDataReader["customer_name"].ToString();
                    objCustomer_list.CompanyWebsite = objMySqlDataReader["company_website"].ToString();
                    objCustomer_list.Address1 = objMySqlDataReader["customer_address"].ToString();
                    objCustomer_list.address2 = objMySqlDataReader["customer_address2"].ToString();
                    objCustomer_list.state = objMySqlDataReader["customer_state"].ToString();
                    objCustomer_list.country = objMySqlDataReader["customer_country"].ToString();
                    objCustomer_list.CompanyWebsite = objMySqlDataReader["company_website"].ToString();
                    objCustomer_list.city = objMySqlDataReader["customer_city"].ToString();
                    objCustomer_list.pincode = objMySqlDataReader["customer_pin"].ToString();
                    objCustomer_list.gstnumber = objMySqlDataReader["gst_number"].ToString();
                    objCustomer_list.contactpersonname = objMySqlDataReader["customercontact_name"].ToString();
                    objCustomer_list.Email_ID = objMySqlDataReader["email"].ToString();
                    objCustomer_list.region_name = objMySqlDataReader["region_name"].ToString();
                    objCustomer_list.designation = objMySqlDataReader["designation"].ToString();
                    objCustomer_list.contacttelephonenumber = objMySqlDataReader["mobile"].ToString();
                    objCustomer_list.currency = objMySqlDataReader["currencyexchange_gid"].ToString();
                    objCustomer_list.Region = objMySqlDataReader["region_gid"].ToString();
                    objMySqlDataReader.Close();
                }
                return objCustomer_list;
            }
            catch (Exception ex)
            {

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                       "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return null;
               
            }
          
        }
        public void DaDeletecustomer(string Customer_gid, Customer_list values)
        {
            try
            {

                msSQL = " update crm_mst_tcustomer set" +
                            " status='Deleted'" +
                            " where customer_gid = '" + Customer_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Customer Deleted Successfully";
                }
                else
                {
                    {
                        values.status = false;
                        values.message = "Error While Deleting Customer";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Customer Delete!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
       "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void Daviewcustomer(string customer_gid, Mdlcustomer values)
        {
            try
            {

                msSQL = " SELECT a.customer_gid,a.customer_name,b.region_gid,a.gst_number, a.company_website, a.customer_id,c.phone,c.area_code,c.country_code,c.fax,c.fax_area_code," +
                     " a.customer_code,a.customer_address,a.customer_city,a.customer_gid,customer_country,d.currency_code,a.customer_state,c.country_code,c.fax_country_code," +
                     " a.customer_pin,b.region_name,customercontact_gid,customercontact_name,email,mobile,designation,a.customer_address2, " +
                     " d.currencyexchange_gid from crm_mst_tcustomer a" +
                     " left join crm_mst_tregion b on a.customer_region = b.region_gid " +
                     " left join crm_mst_tcustomercontact c on a.customer_gid=c.customer_gid " +
                     " left join crm_trn_tcurrencyexchange d on a.currency_gid=d.currencyexchange_gid" +
                     " where a.customer_gid ='" + customer_gid + "'and a.main_branch = 'Y' and c.main_contact = 'Y'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Customer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Customer_list
                        {

                            customercode = dt["customer_id"].ToString(),
                            Customer_gid = dt["customer_gid"].ToString(),
                            customername = dt["customer_name"].ToString(),
                            CompanyWebsite = dt["company_website"].ToString(),
                            Address1 = dt["customer_address"].ToString(),
                            address2 = dt["customer_address2"].ToString(),
                            state = dt["customer_state"].ToString(),
                            country = dt["customer_country"].ToString(),
                            pincode = dt["customer_pin"].ToString(),
                            city = dt["customer_city"].ToString(),
                            gstnumber = dt["gst_number"].ToString(),
                            contactpersonname = dt["customercontact_name"].ToString(),
                            Email_ID = dt["email"].ToString(),
                            region_name = dt["region_name"].ToString(),
                            designation = dt["designation"].ToString(),
                            contacttelephonenumber = dt["mobile"].ToString(),
                            currency = dt["currency_code"].ToString(),
                            Region = dt["region_name"].ToString()
                        });
                        values.Customer_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Customer View!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                     "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
    }
}


