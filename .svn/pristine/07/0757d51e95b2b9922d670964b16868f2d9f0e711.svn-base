using System;
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
using StoryboardAPI.Models;
using System.Web.Http.Results;


namespace ems.pmr.DataAccess
{
    public class DaPmrTrnOpeningInvoice
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string  lsinvoice_refno;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid, maGetGID, lsvendor_code, msUserGid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetOpeningInvoiceSummary(MdlPmrTrnOpeningInvoice values)
        {
            try
            {
                msSQL = " select distinct a.invoice_gid, a.invoice_refno, a.invoice_status, a.invoice_flag, " +
                    " CASE when a.payment_flag <> 'PY Pending' then a.payment_flag " +
                    " else a.invoice_flag end as 'overall_status', format(a.initialinvoice_amount,2) as initialinvoice_amount, " +
                    " a.vendor_gid, format(a.invoice_amount,2) as invoice_amount, a.vendor_gid, " +
                    " a.invoice_date, a.payment_flag, " +
                    " c.vendor_code, c.vendor_companyname,a.invoice_from " +
                    " from acp_trn_tinvoice a " +
                    " left join acp_trn_tinvoicedtl b on a.invoice_gid = b.invoice_gid " +
                    " left join acp_mst_tvendor c on a.vendor_gid = c.vendor_gid " +
                    " left join adm_mst_tuser d on d.user_gid = a.user_gid " +
                    " left join hrm_mst_temployee e on e.user_gid = d.user_gid " +
                    " left join adm_mst_tmodule2employee  f on f.employee_gid = e.employee_gid " +
                    " where (a.user_gid and a.invoice_type)='Opening Invoice' " +
                    " order by a.invoice_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<openinginvoice_list>();

                if (dt_datatable.Rows.Count != 0)
                {


                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new openinginvoice_list
                        {

                            invoice_refno = dt["invoice_refno"].ToString(),
                            invoice_date = dt["invoice_date"].ToString(),
                            vendor_companyname = dt["vendor_companyname"].ToString(),
                            invoice_amount = dt["invoice_amount"].ToString(),
                            overall_status = dt["overall_status"].ToString(),


                        });
                        values.openinginvoice_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }


        }

        public void DaGetvendor(MdlPmrTrnOpeningInvoice values)
        {
            try
            {
                msSQL = msSQL = " select vendor_gid,vendor_companyname from acp_mst_tvendor ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getvendor>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getvendor
                        {
                            vendor_gid = dt["vendor_gid"].ToString(),
                            vendor_companyname = dt["vendor_companyname"].ToString(),
                        });
                        values.Getvendor = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

        public void DaGetOnChangevonder(string vendor_gid, MdlPmrTrnOpeningInvoice values)
        {
            try
            {
                msSQL = "select vendor_code,contact_telephonenumber,contactperson_name,address_gid from acp_mst_tvendor where vendor_gid='" + vendor_gid + "'; ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getvendor>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getvendor
                        {
                            vendor_code = dt["vendor_code"].ToString(),
                            contact_telephonenumber = dt["contact_telephonenumber"].ToString(),
                            contactperson_name = dt["contactperson_name"].ToString(),




                        });
                        values.Getvendor = getModuleList;
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

        public void DaGetOnChangevonderAddress(string vendor_gid, MdlPmrTrnOpeningInvoice values)
        {
            try
            {
                msSQL = " select concat(b.address1,',',b.address2) as address,b.fax from acp_mst_tvendor a " +
                    " left join adm_mst_taddress b on a.address_gid=b.address_gid " +
                    " where a.vendor_gid='" + vendor_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetvendorAddress>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetvendorAddress
                        {
                            address = dt["address"].ToString(),
                            fax = dt["fax"].ToString(),

                        });
                        values.GetvendorAddress = getModuleList;
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

        public void DaGetbranch(MdlPmrTrnOpeningInvoice values)
        {
            try
            {
                msSQL = " SELECT branch_gid, branch_name,branch_code " +
                                " FROM hrm_mst_tbranch ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getbranch>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getbranch
                        {

                            branch_gid = dt["branch_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                        });
                        values.Getbranch = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

        public void DaGetcurrency(MdlPmrTrnOpeningInvoice values)
        {
            try
            {
                msSQL = " select currencyexchange_gid,updated_date,currency_code,exchange_rate from crm_trn_tcurrencyexchange ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getcurrency>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getcurrency
                        {

                            currencyexchange_gid = dt["currencyexchange_gid"].ToString(),
                            currency_code = dt["currency_code"].ToString(),

                        });
                        values.Getcurrency = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

        public void DaGetOnChangeCurrency(string currencyexchange_gid, MdlPmrTrnOpeningInvoice values)
        {
            try
            {
                msSQL = " select currencyexchange_gid,updated_date,currency_code,exchange_rate from crm_trn_tcurrencyexchange " +
                    " where currencyexchange_gid='" + currencyexchange_gid + "'";

                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var getModuleList = new List<GetOnChangeCurrency>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dt in dt_datatable.Rows)
                //    {
                //        getModuleList.Add(new GetOnChangeCurrency
                //        {

                //            exchange_rate = dt["exchange_rate"].ToString(),
                //            currency_code = dt["currency_code"].ToString(),


                //        });
                //        values.GetOnChangeCurrency = getModuleList;
                //    }
                //}
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);



                if (objOdbcDataReader.HasRows == true)
                {
                    values.exchange_rate = objOdbcDataReader["exchange_rate"].ToString();
                    values.currency_code = objOdbcDataReader["currency_code"].ToString();

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }


        }

        public void DaPostOpeningIvoicedtl(OpeningIvoicedtl values)
        {
            try
            {
                msSQL = " select invoice_refno from acp_trn_tinvoice where invoice_refno = '" + values.invoice_refno.Replace("'", "'") + "' ";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows)
                {
                    lsinvoice_refno = objOdbcDataReader["invoice_refno"].ToString();
                }
                if (lsinvoice_refno != values.invoice_refno)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("OINV");

                    msSQL = " insert into acp_trn_tinvoice(" +
                             " invoice_gid," +
                             " invoice_refno," +
                             " vendor_contact_person," +
                             " vendor_address," +
                             " invoice_date," +
                             " invoice_remarks," +
                             " currency_code," +
                             " exchange_rate," +
                             " Order_Total," +
                             " received_amount," +
                             "invoice_status," +
                             "invoice_flag," +
                             "invoice_from," +
                             " received_year)" +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'" + values.invoice_refno.Replace("'", "'") + "'," +
                             "'" + values.Vendor_Contact_Person.Replace("'", "'") + "'," +
                             "'" + values.address.Replace("'", "'") + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                             "'" + values.invoice_remarks.Replace("'", "'") + "'," +
                             "'" + values.currency_code.Replace("'", "'") + "'," +
                             "'" + values.exchange_rate.Replace("'", "'") + "'," +
                             "'" + values.Order_Total.Replace("'", "'") + "'," +
                             "'" + values.received_amount.Replace("'", "'") + "'," +
                             "'IV Completed'," +
                             "'Payment Pending'," +
                             "'Opening Invoice'," +
                             "'" + values.received_year.Replace("'", "'") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Opening Invoice Added Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Opening Ivoice !!";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Invoice Ref No Already Exist !!";

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }



     }
}
