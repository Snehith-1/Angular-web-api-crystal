using ems.system.Models;
using ems.utilities.Functions;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;
using static OfficeOpenXml.ExcelErrorValue;


using static System.Collections.Specialized.BitVector32;

namespace ems.system.DataAccess
{

    public class DaSystemRptEmployeeReport
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        int mnResult;
        string msGetGid, msGetGid1, lsuser_name;

        // Module Master Summary
        public void DaGetEmployeeReportSummary(string branch_gid, MdlSystemRptEmployeeReport values)
        {

            msSQL = " Select distinct a.user_gid,c.employee_gid,ifnull(c.passport_no,'') as passport_no,ifnull(c.workpermit_no,'') as workpermit_no, " +
                    " ifnull(c.passport_expiredate,'') as passport_expiredate, ifnull(c.workpermit_expiredate,'') as workpermit_expiredate,  " +
                    " concat(a.user_firstname,' ',a.user_lastname) as user_name ,a.user_code," +
                    " e.branch_name, g.department_name,d.designation_name,c.employee_gender,c.employee_joiningdate, " +
                    " concat(j.address1,',',j.address2,',', j.city,',', j.state,',',k.country_name,',', j.postal_code) as employee_address, " +
                    " c.useraccess, a.user_status, cast(concat(y.perhour_rate, ' / ' ,x.daysalary_rate) as char) as salary,y.permonth_rate,c.fin_no,y.permonth_rate" +
                    " FROM adm_mst_tuser a " +
                    " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                    " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                    " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                    " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                    " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                    " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                    " left join pay_trn_temployee2wage x on c.employee_gid=x.employee_gid" +
                    " left join pay_mst_tdaysalarymaster y on x.daysalary_gid=y.daysalary_gid" +
                    " left join hrm_trn_temployeetypedtl h on c.employee_gid=h.employee_gid ";
                    if ( branch_gid != "null")
                      {
                           msSQL += " where ";
                           if (branch_gid != "null")
                           {
                              msSQL += " e.branch_gid = '" + branch_gid + "' ";
                           }
                      }

                     dt_datatable = objdbconn.GetDataTable(msSQL);
                     var getModuleList = new List<employeereport_list>();
                     if (dt_datatable.Rows.Count != 0)
                    {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new employeereport_list
                    {
                        user_gid = dt["user_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        user_code = dt["user_code"].ToString(),
                        user_name = dt["user_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        designation_name = dt["designation_name"].ToString(),
                        employee_gender = dt["employee_gender"].ToString(),
                        employee_joiningdate = dt["employee_joiningdate"].ToString(),
                        workpermit_no = dt["workpermit_no"].ToString(),
                        workpermit_expiredate = dt["workpermit_expiredate"].ToString(),
                        passport_no = dt["passport_no"].ToString(),
                        passport_expiredate = dt["passport_expiredate"].ToString(),
                        fin_no = dt["fin_no"].ToString(),
                        salary = dt["salary"].ToString(),
                        permonth_rate = dt["permonth_rate"].ToString(),
                   
                    });
                    values.employeereport_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetBranchDtl(MdlSystemRptEmployeeReport values)
        {
            msSQL = " Select branch_gid,branch_name  " +
                    " from  hrm_mst_tbranch order by branch_gid asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetBranchnamedropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetBranchnamedropdown
                    {
                        branch_gid = dt["branch_gid"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                    });
                    values.GetBranchDtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetReportExportExcel(MdlSystemRptEmployeeReport values)
        {
            msSQL = " Select distinct a.user_gid,c.employee_gid,ifnull(c.passport_no,'') as passport_no,ifnull(c.workpermit_no,'') as workpermit_no, " +
                    " ifnull(c.passport_expiredate,'') as passport_expiredate, ifnull(c.workpermit_expiredate,'') as workpermit_expiredate,  " +
                    " concat(a.user_firstname,' ',a.user_lastname) as user_name ,a.user_code," +
                    " e.branch_name, g.department_name,d.designation_name,c.employee_gender,c.employee_joiningdate, " +
                    " concat(j.address1,',',j.address2,',', j.city,',', j.state,',',k.country_name,',', j.postal_code) as employee_address, " +
                    " c.useraccess, a.user_status, cast(concat(y.perhour_rate, ' / ' ,x.daysalary_rate) as char) as salary,y.permonth_rate,c.fin_no,y.permonth_rate" +
                    " FROM adm_mst_tuser a " +
                    " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                    " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                    " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                    " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                    " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                    " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                    " left join pay_trn_temployee2wage x on c.employee_gid=x.employee_gid" +
                    " left join pay_mst_tdaysalarymaster y on x.daysalary_gid=y.daysalary_gid" +
                    " left join hrm_trn_temployeetypedtl h on c.employee_gid=h.employee_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Employee Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                string lspath = ConfigurationManager.AppSettings["exportexcelfile"] + "/Employee/export" + "/" + lscompany_code + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/TestReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(lspath)))
                        System.IO.Directory.CreateDirectory(lspath);
                }

                string lsname = "employee_report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                string lspath1 = ConfigurationManager.AppSettings["exportexcelfile"] + "/Employee/export" + "/" + lscompany_code + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsname;

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

                var getModuleList = new List<GetreportExportExcel>();
                if (dt_datatable.Rows.Count != 0)
                {

                    getModuleList.Add(new GetreportExportExcel
                    {

                        lsname = lsname,
                        lspath1 = lspath1,



                    });
                    values.getreport_exportexcel = getModuleList;

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



    }

    }

