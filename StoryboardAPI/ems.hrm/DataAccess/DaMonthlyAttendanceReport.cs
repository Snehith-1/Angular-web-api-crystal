
using ems.hrm.Models;
using ems.utilities.Functions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Data.Odbc;

using System.Linq;
using System.Web;

namespace ems.hrm.DataAccess
{
    public class DaMonthlyAttendanceReport
    {
        dbconn objdbconn = new dbconn();
        OdbcDataReader objMySqlDataReader;
        cmnfunctions objcmnfunctions;
        string msSQL = string.Empty;
        DataTable dt_table;
        public void DaGetMonthlyReportBranch(MdlMonthlyAttendanceReport values)
        {
            try
            {
                
                msSQL = "select branch_name, branch_gid from hrm_mst_tbranch";
                dt_table = objdbconn.GetDataTable(msSQL);
                var GetBranch_list = new List<GetMonthlyreportbranch_list>();
                if (dt_table.Rows.Count > 0)
                {
                    foreach (DataRow dt_branch in dt_table.Rows)
                    {
                        GetBranch_list.Add(new GetMonthlyreportbranch_list
                        {
                            branch_name = dt_branch["branch_name"].ToString(),
                            branch_gid = dt_branch["branch_gid"].ToString(),
                        });
                    }
                    values.GetMonthlyreportbranch_list = GetBranch_list;
                }
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }
        public void DaGetMonthlyReportSummaryDetails( string year, string month,MdlMonthlyAttendanceReport values)
        {
            try
            {
                
                string lsday = null;
                string lsmonth;

                if (month.Length > 1)
                {
                    lsmonth = month;
                }
                else
                {
                    lsmonth = "0" + month;
                }

                // "0" + month;
                lsday = "01";
                string lsdaystr = lsday + "/" + lsmonth + "/" + year;
                DateTime lsdate = DateTime.ParseExact(lsdaystr.Replace("/", ""), "ddMMyyyy", null);
                //string lsdates = lsdate.ToString("yyyyMMdd");

                msSQL = " select /*+ MAX_EXECUTION_TIME(900000) */ distinct a.employee_gid,c.user_code as user_code,concat(c.user_firstname,' ' ,c.user_lastname) as user_name,br.branch_gid," +
                        " cast((select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.ToString("yyyy-MM-dd") + "' limit 0,1 ) as char) as date1, " +
                        " cast((select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(1).ToString("yyyy-MM-dd") + "' limit 0,1 ) as char) as date2, " +
                        " cast((select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(2).ToString("yyyy-MM-dd") + "' limit 0,1 ) as char) as date3, " +
                        " cast((select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(3).ToString("yyyy-MM-dd") + "' limit 0,1 ) as char) as date4, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(4).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date5, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(5).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date6, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(6).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date7, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(7).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date8, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(8).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date9, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(9).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date10, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(10).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date11, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(11).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date12, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(12).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date13, " +
                        " (select distinct " +
                        "case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(13).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date14, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(14).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date15, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(15).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date16, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                        " from hrm_trn_tattendance x " +
                        " where(a.employee_gid = x.employee_gid) " +
                        " and x.attendance_date='" + lsdate.AddDays(16).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date17, " +
                        " (select distinct " +
                        " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                        " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                        " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                        " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                        " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                        " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                        " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                        " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                        " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                        " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(17).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date18, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(18).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date19, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(19).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date20, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(20).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date21, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(21).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date22, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(22).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date23, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(23).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date24, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(24).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date25, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(25).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date26, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(26).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date27, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(27).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date28, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(28).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date29, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(29).ToString("yyyy-MM-dd") + "' limit 0,1 ) as date30, " +
                      " (select distinct " +
                      " case when x.attendance_type='P' and (logout_time is null or time(logout_time)='00:00:00') then 'XA' " +
                      " when x.attendance_type='P' and (login_time is null or time(login_time)='00:00:00') then 'AX' " +
                      " when x.attendance_type='P' then 'XX' when x.attendance_type='A' then 'AA' " +
                      " when x.attendance_type='CL' then 'CL'  when x.attendance_type='WH' then 'WH' " +
                      " when x.attendance_type='CL001' then 'CL'  when x.attendance_type='SL001' then 'SL' " +
                      " when x.attendance_type='Compoff' then 'CO'  when x.attendance_type='EL001' then 'EL' " +
                      " when x.attendance_type='AL' then 'AL'  when x.attendance_type='FH' then 'FH' " +
                      " when x.attendance_type='OD' then 'DD' when x.attendance_type='NH' then 'NH'  " +
                      " when x.attendance_type='XD' then 'XD' when x.attendance_type='DX' then 'DX' " +
                      " when x.attendance_type='Maternity' then 'Maternity' end  as attendance " +
                      " from hrm_trn_tattendance x " +
                      " where(a.employee_gid = x.employee_gid) " +
                      " and x.attendance_date='" + lsdate.AddDays(30).ToString("yyyy-MM-dd") + "' limit 0,1  ) as date31 " +
                      " from hrm_mst_temployee a " +
                      " inner join adm_mst_tuser c on a.user_gid=c.user_gid " +
                      " inner join hrm_mst_tbranch br on a.branch_gid=br.branch_gid " +
                      " left join hrm_mst_tsectionassign2employee q on q.employee_gid=a.employee_gid" +
                      " left join hrm_mst_tsection i on i.section_gid=q.section_gid" +
                      " left join hrm_mst_tblock z on z.block_gid=q.block_gid" +
                      " left join hrm_mst_tunit n on n.unit_gid=q.unit_gid" +
                      " left join hrm_trn_temployeetypedtl h on a.employee_gid=h.employee_gid " +
                      " where c.user_status='Y' and a.attendance_flag='Y' ";


                msSQL += " order by length(user_code),user_code ";
                dt_table = objdbconn.GetDataTable(msSQL);
                var Getmonthlydetailreport = new List<GetMonthlyDetailsReport_list>();
                if (dt_table.Rows.Count > 0)
                {
                    foreach (DataRow dt_detailsReport in dt_table.Rows)
                    {
                        Getmonthlydetailreport.Add(new GetMonthlyDetailsReport_list
                        {
                            user_code = dt_detailsReport["user_code"].ToString(),
                            user_name = dt_detailsReport["user_name"].ToString(),
                            branch_gid = dt_detailsReport["branch_gid"].ToString(),
                            date1 = dt_detailsReport["date1"].ToString(),
                            date2 = dt_detailsReport["date2"].ToString(),
                            date3 = dt_detailsReport["date3"].ToString(),
                            date4 = dt_detailsReport["date4"].ToString(),
                            date5 = dt_detailsReport["date5"].ToString(),
                            date6 = dt_detailsReport["date6"].ToString(),
                            date7 = dt_detailsReport["date7"].ToString(),
                            date8 = dt_detailsReport["date8"].ToString(),
                            date9 = dt_detailsReport["date9"].ToString(),
                            date10 = dt_detailsReport["date10"].ToString(),
                            date11 = dt_detailsReport["date11"].ToString(),
                            date12 = dt_detailsReport["date12"].ToString(),
                            date13 = dt_detailsReport["date13"].ToString(),
                            date14 = dt_detailsReport["date14"].ToString(),
                            date15 = dt_detailsReport["date15"].ToString(),
                            date16 = dt_detailsReport["date16"].ToString(),
                            date17 = dt_detailsReport["date17"].ToString(),
                            date18 = dt_detailsReport["date18"].ToString(),
                            date19 = dt_detailsReport["date19"].ToString(),
                            date20 = dt_detailsReport["date20"].ToString(),
                            date21 = dt_detailsReport["date21"].ToString(),
                            date22 = dt_detailsReport["date23"].ToString(),
                            date23 = dt_detailsReport["date23"].ToString(),
                            date24 = dt_detailsReport["date24"].ToString(),
                            date25 = dt_detailsReport["date25"].ToString(),
                            date26 = dt_detailsReport["date26"].ToString(),
                            date27 = dt_detailsReport["date27"].ToString(),
                            date28 = dt_detailsReport["date28"].ToString(),
                            date29 = dt_detailsReport["date29"].ToString(),
                            date30 = dt_detailsReport["date30"].ToString(),
                            date31 = dt_detailsReport["date31"].ToString(),
                        });
                        values.GetMonthlyDetailsReport_list = Getmonthlydetailreport;
                    }
                }
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