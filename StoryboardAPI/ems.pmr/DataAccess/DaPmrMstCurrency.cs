﻿
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
using System.Text;


namespace ems.pmr.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class DaPmrMstCurrency
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetPmrCurrencySummary(MdlPmrMstCurrency values)
        {
            try
            {

                msSQL = " select  currencyexchange_gid,currency_code,exchange_rate,country as country_name , CONCAT(b.user_firstname,' ',b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                    " from crm_trn_tcurrencyexchange a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_date desc";
                // %%%% Exchange rate integration query - DONT DELETE %%%%

//                msSQL = " SELECT e.currencyexchange_gid,e.currency_code,e.exchange_rate,e.country AS country_name,CONCAT(b.user_firstname, ' ', b.user_lastname) AS created_by, " +
//" DATE_FORMAT(e.created_date, '%d-%m-%Y') AS created_date FROM crm_trn_tcurrencyexchange e JOIN (SELECT currency_code, MAX(created_date) AS max_created_date " +
//" FROM crm_trn_tcurrencyexchange GROUP BY currency_code) m ON e.currency_code = m.currency_code AND e.created_date = m.max_created_date " +
//" LEFT JOIN adm_mst_tuser b ON b.user_gid = e.created_by GROUP BY e.currency_code order by e.currency_code asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<currency_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new currency_list
                        {
                            currencyexchange_gid = dt["currencyexchange_gid"].ToString(),
                            currency_code = dt["currency_code"].ToString(),
                            exchange_rate = dt["exchange_rate"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            country_name = dt["country_name"].ToString(),
                        });
                        values.currency_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting the currency summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }

        public void DaGetPmrCountryDtl(MdlPmrMstCurrency values)
        {
            try
            {

                msSQL = " select  country_gid, country_code, country_name " +
                        " from adm_mst_tcountry ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getcountrydropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getcountrydropdown
                        {
                            country_gid = dt["country_gid"].ToString(),
                            country_name = dt["country_name"].ToString(),
                        });
                        values.GetPmrCountryDtl = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting the country details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }


        public void DaPostPmrCurrency(string user_gid, currency_list values)
        {
            try
            {

                msSQL = " select * from crm_trn_tcurrencyexchange where country_gid= '" + values.country_name + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    //values.status = true;
                    values.message = "Country Name Already Exist";
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CUR");
                    msSQL = " Select country_name from adm_mst_tcountry where country_gid= '" + values.country_name + "'";
                    string lscountry_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " insert into crm_trn_tcurrencyexchange(" +
                            " currencyexchange_gid," +
                            " currency_code," +
                            " country_gid," +
                            " exchange_rate," +
                            " country," +
                            " created_by, " +
                            " created_date)" +
                            " values(" +
                            " '" + msGetGid + "'," +
                            " '" + values.currency_code.Replace("'", "\\'") + "'," +
                            " '" + values.country_name + "'," +
                            " '" + values.exchange_rate.Replace("'", "\\'") + "',";
                    if (lscountry_name == null || lscountry_name == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + lscountry_name.Replace("'", "") + "',";
                    }
                    msSQL += "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Currency Added Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Currency";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Adding Currency!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }


        }

        public void DaPmrCurrencyUpdate(string user_gid, currency_list values)
        {
            try
            {

                msSQL = " Select country_gid from adm_mst_tcountry where country_name= '" + values.country_nameedit + "'";
                string lscountry_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " insert into crm_trn_tcurrencyexchangehistory (" +
                       " currency_code," +
                       " exchange_rate," +
                       " country," +
                       " updated_by, " +
                       " updated_date)" +
                       " values(" +
                       " '" + values.currency_codeedit.Replace("'", "\\'") + "'," +
                       "'" + values.exchange_rateedit.Replace("'", "\\'") + "',";
                if (values.country_nameedit == null || values.country_nameedit == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.country_nameedit.Replace("'", "") + "',";
                }
                msSQL += "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)

                {
                    msSQL = " update  crm_trn_tcurrencyexchange set " +
                 " currency_code = '" + values.currency_codeedit + "'," +
                 " exchange_rate = '" + values.exchange_rateedit + "'," +
                 " country = '" + values.country_nameedit + "'," +
                 " country_gid = '" + lscountry_gid + "'," +
                 " updated_by = '" + user_gid + "'," +
                 " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where currencyexchange_gid='" + values.currencyexchange_gid + "'  ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Currency Updated Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Currency";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Currency";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Currency!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }




        }

        public void DaPmrCurrencySummaryDelete(string currencyexchange_gid, currency_list values)
        {
            try
            {

                msSQL = "  delete from crm_trn_tcurrencyexchange where currencyexchange_gid='" + currencyexchange_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Currency Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deleting Currency";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Deleting Currency!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }


        public void DaGetBreadCrumb(string user_gid, string module_gid, MdlPmrMstCurrency values)
        {
            try
            {

                msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<breadcrumb_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new breadcrumb_list
                        {


                            module_name1 = dt["module_name1"].ToString(),
                            sref1 = dt["sref1"].ToString(),
                            module_name2 = dt["module_name2"].ToString(),
                            sref2 = dt["sref2"].ToString(),
                            module_name3 = dt["module_name3"].ToString(),
                            sref3 = dt["sref3"].ToString(),

                        });
                        values.breadcrumb_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting BreadCrumb!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                 $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
             ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
             msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
             DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }

        }

    }
}