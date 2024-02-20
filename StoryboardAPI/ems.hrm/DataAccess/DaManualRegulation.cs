using ems.hrm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using MySqlX.XDevAPI;

namespace ems.hrm.DataAccess
{
    public class DaManualRegulation
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;

        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        int mnResult;
        string msGetGid, msGetGid1, lsempoyeegid, msgetshift;

        // Module Master Summary
        public void DaManualRegulationsummary(string fromdate,string todate,MdlManualRegulation values)
        {

            try
            {
                // Parse fromdate and todate strings into DateTime objects
                DateTime fromDate = DateTime.ParseExact(fromdate, "yyyy-MM-dd", null);
                DateTime toDate = DateTime.ParseExact(todate, "yyyy-MM-dd", null);

                // Initialize count variable
                int count = 1;
                List<string> dynamicDayNames = new List<string>();
                var getdaysList = new List<daylist>();
                msSQL = "select /*+ MAX_EXECUTION_TIME(900000) */ distinct a.employee_gid," +
                    "c.user_code,concat(c.user_firstname,' ' ,c.user_lastname) as user_name," +
                    "br.branch_gid,";

                for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
                {
                    getdaysList.Add(new daylist
                    {
                        days = "day"+ count.ToString(),
                    });
                    dynamicDayNames.Add("day"+ count.ToString());
                    msSQL += "(SELECT attendance FROM hrm_trn_manualregulation_V x " +
                        " WHERE a.employee_gid = x.employee_gid AND x.shift_date = '"+ date.ToString("yyyy-MM-dd") + "') AS day"+count+", ";    
                    
                    count++;
                     }

                
                msSQL +=  " br.branch_name from hrm_mst_temployee a  " +
                          " inner join adm_mst_tuser c on a.user_gid=c.user_gid  " +
                          " inner join hrm_mst_tbranch br on a.branch_gid=br.branch_gid  " +
                          " left join hrm_trn_temployeetypedtl h on a.employee_gid=h.employee_gid  " +
                          " left join hrm_mst_tsectionassign2employee m on m.employee_gid=a.employee_gid" +
                          " left join hrm_mst_tsection i on i.section_gid=m.section_gid " +
                          " left join hrm_mst_tblock z on z.block_gid=m.block_gid " +
                          " left join hrm_mst_tunit n on n.unit_gid=m.unit_gid   " +
                          " where c.user_status='Y' and a.attendance_flag='Y' "+
                          " order by length(c.user_code),c.user_code asc";



                dt_datatable = objdbconn.GetDataTable(msSQL);
                             
                var getModuleList = new List<manuallist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        var getModuleList1 = new List<daydatalist>();
                        for (int i = 1; i <= dynamicDayNames.Count; i++)
                        {

                            string dynamicDayName = "day" + i.ToString();
                            string attendanceValue = dt[dynamicDayName].ToString();


                            
                            getModuleList1.Add(new daydatalist
                            {
                                dayi = dynamicDayName,
                                attendance = attendanceValue
                            });
                        }

                        
                        getModuleList.Add(new manuallist
                        {
                            employee_gid = dt["employee_gid"].ToString(),
                            user_code = dt["user_code"].ToString(),
                            user_name = dt["user_name"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            daydatalist= getModuleList1

                        });
                        values.dayslist = getdaysList;
                        values.manuallist = getModuleList;
                        values.daydatalist = getModuleList1;
                    }
                }
                dt_datatable.Dispose();
            }

            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }
    }
}