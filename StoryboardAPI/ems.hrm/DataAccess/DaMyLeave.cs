using ems.hrm.Models;
using ems.utilities.Functions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

using System.Linq;
using System.Web;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.hrm.DataAccess
{
    public class DaMyLeave
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objMySqlDataReader;

        DataTable dt_datatable, dt_table;
        string msSQL, query, mssql1, suquery;
        public bool DaGetMyLeave(string employee_gid, mdlmyLeave objmyLeave)
        {
            try
            {
                
                List<leavetype_details> getdata = null;


                query = "select cast(concat(date_format(e.attendance_date,'%b'),' - ',year(e.attendance_date)) as char) as Duration ";
                mssql1 = " select a.leavetype_gid,a.leavetype_name,a.leavetype_code " +
                        " from hrm_mst_tleavetype a" +
                        " left join hrm_mst_tleavegradedtl b on a.leavetype_gid=b.leavetype_gid" +
                        " left join hrm_trn_tleavegrade2employee c on c.leavegrade_gid=b.leavegrade_gid" +
                        " where b.active_flag='Y' and c.employee_gid='" + employee_gid + "'";
                mssql1 += "group by a.leavetype_gid order by a.leavetype_gid asc ";
                dt_table = objdbconn.GetDataTable(mssql1);
                if (dt_table.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_table.Rows)
                    {
                        suquery = "";

                        suquery = " ,(select ifnull(SUM(if(a.day_session='NA','1','0.5')),0) as count " +
                                  " from hrm_trn_tattendance a where a.employee_gid='" + employee_gid + "' and a.employee_attendance='Leave' and a.attendance_type='" + dr_datarow["leavetype_code"].ToString() + "' " +
                                  " and a.employee_gid='" + employee_gid + "' and month(a.attendance_date)=month(e.attendance_date) and year(a.attendance_date)=year(e.attendance_date)) as '" + dr_datarow["leavetype_name"].ToString() + "' ";
                        query += suquery;
                    }
                }

                query += " From hrm_trn_tattendance e " +
                     " where attendance_date <= date(now()) and attendance_date >=date('" + DateTime.Now.AddMonths(-2).ToString("yyyy-MM-dd") + "')";

                query += " group by monthname(e.attendance_date) order by year(e.attendance_date) desc, month(e.attendance_date) desc ";

                msSQL = query;

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getloginsummary = new List<leavetype_details>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginsummary.Add(new leavetype_details
                        {
                            duration = dr_datarow["Duration"].ToString(),
                            count_sl = dr_datarow["Sick Leave"].ToString(),
                            count_cl = dr_datarow["Casual Leave"].ToString(),
                            count_compoff = dr_datarow["Compensatory off"].ToString(),
                        });
                    }
                    objmyLeave.leavetype_details = getloginsummary;
                }
                dt_datatable.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                objmyLeave.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

                return false;
            }

        }
    }
}