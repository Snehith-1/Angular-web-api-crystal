using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.sales.Models;
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
using static ems.sales.Models.smrtax_list;



namespace ems.sales.DataAccess
{
    public class DaSmrMstTaxSummary
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        string lstax_name;
        string lspercentage;
        public void DaGetTaxSummary(MdlSmrMstTaxSummary values)
        {
            try
            {
                
                msSQL = " select  tax_gid, tax_name, percentage, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                    " from acp_mst_ttax a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<smrtax_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new smrtax_list
                    {
                        tax_gid = dt["tax_gid"].ToString(),
                        tax_name = dt["tax_name"].ToString(),
                        percentage = dt["percentage"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.smrtax_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Tax Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaPostTax(string user_gid, smrtax_list values)
        {
            try
            {
               

                msGetGid = objcmnfunctions.GetMasterGID("STXM");
            msSQL = " Select tax_name,percentage  from acp_mst_ttax where tax_name = '" + values.tax_name + "' and percentage='" + values.percentage + "'";
         
            objOdbcDataReader = objdbconn.GetDataReader(msSQL);
            if (objOdbcDataReader.HasRows == true)
            {
                lstax_name = objOdbcDataReader["tax_name"].ToString();
                lspercentage = objOdbcDataReader["percentage"].ToString();
            }
            if (lstax_name == values.tax_name && lspercentage == values.percentage)
            {
                values.message = "Tax Name and Tax Percentage Should be Unique! ";
            }
            else
            {

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
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Adding Tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }
        public void DaUpdatedTax(string user_gid, smrtax_list values)
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
                values.message = "Exception occured while Updating Tax !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }

        public void DadeleteTaxSummary(string tax_gid, smrtax_list values)
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
                values.message = "Exception occured while deleting Tax !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }


    }
}

