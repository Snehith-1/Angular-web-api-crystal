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
using System.Web;
//using OfficeOpenXml;
using System.Configuration;
using System.IO;
//using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
using static ems.pmr.Models.pmrtax_list;


namespace ems.pmr.DataAccess
{
    public class DaPmrMstTax
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetTaxSummary(MdlPmrMstTax values)
        {
            try
            {
                 
                msSQL = " select  tax_gid, tax_name, percentage, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                    " from acp_mst_ttax a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<pmrtax_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new pmrtax_list
                        {
                            tax_gid = dt["tax_gid"].ToString(),
                            tax_name = dt["tax_name"].ToString(),
                            percentage = dt["percentage"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                        });
                        values.pmrtax_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {

                values.message = "Exception occured while getting Tax summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                  $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
              DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaPostTax(string user_gid, pmrtax_list values)
        {
            try
            {
                
                msGetGid = objcmnfunctions.GetMasterGID("STXM");
                msSQL = " Select tax_name from adm_mst_ttax where tax_gid = '" + values.tax_name + "'";
                string tax_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " insert into acp_mst_ttax(" +
                        " tax_gid," +
                        " tax_name," +
                        " percentage," +
                        " created_by, " +
                        " created_date)" +
                        " values(" +
                        " '" + msGetGid + "',";
                if (values.tax_name == null || values.tax_name == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.tax_name.Replace("'", "\\'") + "',";
                }
                msSQL += "'" + values.percentage + "'," +
                         "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Tax Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Adding Tax";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while  Adding Tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                  $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
              DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
        public void DaUpdatedTax(string user_gid, pmrtax_list values)
        {
            try
            {
                 


                msSQL = " update  acp_mst_ttax set " +
          " tax_name    = '" + values.taxedit_name + "'," +
          " percentage  = '" + values.editpercentage + "'," +
          " updated_by = '" + user_gid + "'," +
          " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where tax_gid='" + values.tax_gid + "'  ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    values.status = true;
                    values.message = "Tax Updated Successfully";

                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Tax";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                  $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
              DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             
        }

        public void DadeleteTaxSummary(string tax_gid, pmrtax_list values)
        {
            try
            {
                 
                msSQL = "  delete from acp_mst_ttax where tax_gid='" + tax_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Tax Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deleting Tax";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while deleting Tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                  $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
              DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             
        }


    }
}