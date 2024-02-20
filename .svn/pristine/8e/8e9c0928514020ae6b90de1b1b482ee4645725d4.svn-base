using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.hrm.Models;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.hrm.DataAccess
{
    public class DaApplyLeave
    {           
        dbconn objdbconn = new dbconn();
        hrClass fnopeningbalance = new hrClass();
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        DataTable dt_datatable1;
        DataTable table;
        DataTable objTblRQ;
        string msSQL;
        int mnResult;
        string lsfile_name, Filepath, hierary_level;
        string lsdocname, lsdocpath;
        DateTime leave_fromdate;
        DateTime leave_todate;
        string lsbeyond_eligible;
        string lsleave_eligible;
        Double leave_eligible;
        string msGetGID2, msGetGid;
        string lsweekoff_applicable, lsholiday_applicable, msGetleavedtlGID;
        public static List<DateTime> lstholiday = new List<DateTime>();
        public static List<string> lstweekoff = new List<string>();
        string lsWeekOff_flag, lsday, lsholiday;
        Double lscount;
        Double lsdaycount = 0;
        string half_day, lslop, lspath;
        string employee, lsdate, lsleavetype, lsemployee, lshalfday, lshalfsession, lstype, msGetGID, msGetGID1, lsflag;
        string lsleavetype_name, mssql1;
        string lsleavetype_gid, lsleavetype_code;
        string leave_gid, lsapply_leave, lsdocument_flag;
        HttpPostedFile httpPostedFile;
        string lsleave_session;
        DateTime lsstart_date, lsend_date;
        cmnfunctions objcmnfunctions = new cmnfunctions();
        public bool DaGetLeaveType(string employee_gid, string user_gid, leavecountdetails objleavecountdetails)
        {
            try
            {
                
                fnopeningbalance.openingbalance(employee_gid);
                var getdata = new List<leavetype_list>();
                msSQL = " select b.leavetype_name,a.total_leavecount,a.leave_taken,a.available_leavecount,b.leavetype_gid from hrm_mst_tleavecreditsdtl a " +
                        " left join hrm_mst_tleavetype b on a.leavetype_gid = b.leavetype_gid " +
                        " where a.employee_gid='" + employee_gid + "' and a.month = '" + DateTime.Now.ToString("MM") + "' and a.year = '" + DateTime.Now.ToString("yyyy") + "'" +
                        " and active_flag='Y'";
                dt_datatable = objdbconn.GetDataTable(msSQL); 
                if (dt_datatable.Rows.Count != 0)
                {

                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (Convert.ToDouble(dr_datarow["available_leavecount"]) <= 0)
                        {
                            lsapply_leave = "Y";
                        }
                        else
                        {
                            lsapply_leave = "N";
                        }
                        getdata.Add(new leavetype_list

                        {
                            leavetype_gid = dr_datarow["leavetype_gid"].ToString(),
                            leavetype_name = dr_datarow["leavetype_name"].ToString(),
                            count_leavetaken = Convert.ToDouble(dr_datarow["leave_taken"]),
                            count_leaveavailable = Convert.ToDouble(dr_datarow["available_leavecount"]),
                            lsapply_leave = lsapply_leave
                        });

                    }

                    objleavecountdetails.leavetype_list = getdata;
                }
                dt_datatable.Dispose();

                return true;
            }
            
            catch(Exception ex)
            {
                objleavecountdetails.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +ex.Message.ToString() +
                 "***********" + objleavecountdetails.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/ HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
                return false;
            }

        }
        public void DaGetLeaveTypeName (Mdlapplyleave values)
        {
            try
            {
                
                msSQL = " Select leavetype_name, leavetype_gid " +
                        " from hrm_mst_tleavetype ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<leave_type_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new leave_type_list
                        {
                            leavetype_name = dt["leavetype_name"].ToString(),
                            leavetype_gid = dt["leavetype_gid"].ToString(),
                        });
                        values.leave_type_list = getModuleList;
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
        public bool DaPostApplyLeave(string employee_gid, string user_gid, applyleavedetails values)
        {
            try
            {
                
                msSQL = " select leavetype_name from hrm_mst_tleavetype where leavetype_gid = '" + values.leavetype_gid + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lsleavetype_name = objMySqlDataReader["leavetype_name"].ToString();
                }
                
                leave_fromdate = values.leave_from;
                leave_todate = values.leave_to;
                if (values.leave_session == "NA")
                {
                    values.noofdays_leave = (leave_todate - leave_fromdate).TotalDays + 1;
                }
                else
                {

                    values.noofdays_leave = 0.5;
                }
                //Leave applied after days Not applicable
                string lsleave_days;
                DateTime from_date;
                DateTime lsleaveapplied_days;
                msSQL = " select leave_days from hrm_mst_tleavetype where leavetype_gid= '" + values.leavetype_gid + "'";
                lsleave_days = objdbconn.GetExecuteScalar(msSQL);
                if (lsleave_days != "0")
                {
                    from_date = Convert.ToDateTime(leave_fromdate.ToString("yyyy-MM-dd"));
                    lsleaveapplied_days = from_date.AddDays(Convert.ToDouble(lsleave_days));
                    if (Convert.ToDateTime(lsleaveapplied_days.ToString("yyyy-MM-dd")) < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        values.message = " Leave Cannot be applied after " + lsleave_days + " days from the day availed!!!";
                        return false;
                    }
                }
                //Leave applied after days Not applicable

                //After Payrun Leave cannot be applied
                msSQL = " select * from pay_trn_tsalary where payrun_flag='Y' AND year='" + leave_fromdate.ToString("yyyy") + "'" +
                        " and month='" + leave_fromdate.ToString("MMMM") + "'  and employee_gid='" + employee_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                
                if (objMySqlDataReader.HasRows != true)
                {
                    //No Negative Leave
                    
                    msSQL = " Select beyond_eligible from hrm_mst_tleavetype where leavetype_gid = '" + values.leavetype_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    
                    if (objMySqlDataReader.HasRows == true)
                    {
                        lsbeyond_eligible = objMySqlDataReader["beyond_eligible"].ToString();
                        
                        if (lsbeyond_eligible == "N")

                            msSQL = " select available_leavecount,leavetype_gid from hrm_mst_tleavecreditsdtl where leavetype_gid='" + values.leavetype_gid + "'" +
                                    " and employee_gid='" + employee_gid + "' and month='" + DateTime.Now.ToString("MM") + "' and year ='" + DateTime.Now.ToString("yyyy") + "'" +
                                    " and active_flag='Y'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            lsleave_eligible = objMySqlDataReader["available_leavecount"].ToString();
                            leave_eligible = Convert.ToDouble(lsleave_eligible);
                        }
                        
                        if (values.leavetype_gid != "LOP")
                        {
                            msSQL = " select weekoff_applicable,holiday_applicable from hrm_mst_tleavetype where leavetype_gid='" + values.leavetype_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                lsweekoff_applicable = objMySqlDataReader["weekoff_applicable"].ToString();
                                lsholiday_applicable = objMySqlDataReader["holiday_applicable"].ToString();
                            }

                            
                            if (lsweekoff_applicable == "N" && lsholiday_applicable == "Y")
                            {
                                DateTime leavefromdate;

                                Double lsNoOFDays = 0;
                                string lsleave;
                                leavefromdate = values.leave_from;
                                for (int d = 0; d < values.noofdays_leave; d++)
                                {
                                    msSQL = " select " + leavefromdate.DayOfWeek.ToString() + " as lsday  from hrm_mst_tweekoffemployee " +
                                            " where employee_gid='" + employee_gid + "' ";
                                    dt_datatable = objdbconn.GetDataTable(msSQL);
                                    if (dt_datatable.Rows.Count != 0)
                                    {
                                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                                        {
                                            lsday = dr_datarow["lsday"].ToString();
                                            if (lsday == "Non-working Day")
                                            {
                                                lsWeekOff_flag = "Y";
                                                lstweekoff.Add(lsday);
                                            }
                                            else
                                            {
                                                lsdaycount = lsdaycount + 1;
                                                lsWeekOff_flag = "N";
                                            }
                                        }
                                    }
                                    leavefromdate = leavefromdate.AddDays(1);
                                }

                                if (values.leave_session == "NA")
                                {
                                    lsNoOFDays = lsdaycount;
                                }
                                else
                                {
                                    if (lsdaycount != 0)
                                    {
                                        lsNoOFDays = 0.5;
                                        lsdaycount = 0.5;
                                    }
                                }

                                if ((leave_eligible < lsNoOFDays) == true)
                                {
                                    values.message = "No Available Leave Balance";
                                    return false;
                                }
                                else if (lsdaycount == 0)
                                {
                                    values.message = "Leave cannot be applied on weekoff!!! ";
                                    return false;
                                }
                            }
                            else if (lsweekoff_applicable == "Y" && lsholiday_applicable == "N")
                            {
                                DateTime leavefromdate;
                                Double lsNoOFDays = 0;
                                string lsleave;
                                leavefromdate = values.leave_from;

                                for (int d = 0; d < values.noofdays_leave; d++)
                                {
                                    msSQL = " select date(a.holiday_date) as holiday from hrm_mst_tholiday a " +
                                            " inner join hrm_mst_tholiday2employee b on a.holiday_gid=b.holiday_gid " +
                                            " where b.employee_gid='" + employee_gid + "' and a.holiday_date='" + leavefromdate.ToString("yyyy-MM-dd") + "' ";
                                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                    if (objMySqlDataReader.HasRows == true)
                                    {
                                        lsholiday = "Y";
                                    }
                                    else
                                    {
                                        lsdaycount = lsdaycount + 1;
                                        lsholiday = "N";
                                    }

                                    
                                    leavefromdate = leavefromdate.AddDays(1);
                                }

                                if (values.leave_session == "NA")
                                {
                                    lsNoOFDays = lsdaycount;
                                }
                                else
                                {
                                    if (lsdaycount != 0)
                                    {
                                        lsNoOFDays = 0.5;
                                        lsdaycount = 0.5;
                                    }
                                }

                                if ((leave_eligible < lsNoOFDays) == true)
                                {
                                    values.message = "No Available Leave Balance";
                                    return false;
                                }
                                else if (lsdaycount == 0)
                                {
                                    values.message = "Leave cannot be applied on Holiday!!! ";
                                    return false;
                                }
                            }
                            else if (lsweekoff_applicable == "N" && lsholiday_applicable == "N")
                            {
                                DateTime leavefromdate;
                                Double lsNoOFDays = 0;
                                string lsleave;
                                leavefromdate = values.leave_from;
                                for (int d = 0; d < values.noofdays_leave; d++)
                                {
                                    msSQL = " select " + leavefromdate.DayOfWeek.ToString() + " as lsday  from hrm_mst_tweekoffemployee " +
                                            " where employee_gid='" + employee_gid + "' ";
                                    dt_datatable = objdbconn.GetDataTable(msSQL);
                                    if (dt_datatable.Rows.Count != 0)
                                    {
                                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                                        {
                                            lsday = dr_datarow["lsday"].ToString();
                                            if (lsday == "Non-working Day")
                                            {
                                                lsWeekOff_flag = "Y";
                                                lstweekoff.Add(lsday);
                                            }
                                            else
                                            {
                                                msSQL = " select date(a.holiday_date) as holiday from hrm_mst_tholiday a " +
                                                        " inner join hrm_mst_tholiday2employee b on a.holiday_gid=b.holiday_gid " +
                                                        " where b.employee_gid='" + employee_gid + "' and a.holiday_date='" + leavefromdate.ToString("yyyy-MM-dd") + "' ";
                                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                                if (objMySqlDataReader.HasRows == true)
                                                {
                                                    lsholiday = "Y";
                                                }
                                                else
                                                {
                                                    lsdaycount = lsdaycount + 1;
                                                    lsholiday = "N";
                                                }

                                                
                                                lsWeekOff_flag = "N";
                                            }
                                        }
                                    }
                                    leavefromdate = leavefromdate.AddDays(1);
                                }

                                if (values.leave_session == "NA")
                                {
                                    lsNoOFDays = lsdaycount;
                                }
                                else
                                {
                                    if (lsdaycount != 0)
                                    {
                                        lsNoOFDays = 0.5;
                                        lsdaycount = 0.5;
                                    }
                                }

                                if ((leave_eligible < lsNoOFDays) == true)
                                {
                                    values.message = "No Available Leave Balance";
                                    return false;
                                }
                                else if (lsdaycount == 0)
                                {
                                    values.message = "Leave cannot be applied on weekoff or Holiday!!! ";
                                    return false;
                                }
                            }
                            else
                            {
                                lsdaycount = values.noofdays_leave;
                                if ((leave_eligible < values.noofdays_leave) == true)

                                {
                                    values.message = "No Available Leave Balance";
                                    return false;
                                }
                            }
                        }
                    }
                    //Already Leave Applied on Same Date
                    msSQL = " select leavedtl_gid,date_format(leavedtl_date,'%d/%m/%y') as leave_date from hrm_trn_tleavedtl " +
                            " where leavedtl_date >= '" + leave_fromdate.ToString("yyyy-MM-dd") + "' and " +
                            " leavedtl_date<='" + leave_todate.ToString("yyyy-MM-dd") + "' and created_by='" + user_gid + "' and leave_status<>'Cancelled'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        values.message = " Already Leave Apply For This Date  " + values.leave_from;
                        return false;
                    }
                    else if (values.leave_from > values.leave_to)
                    {
                        values.message = "To Date Should Be Greater Than From Date";
                        return false;
                    }
                    
                    //Fin Leave
                    msSQL = " select attendance_startdate,attendance_enddate " +
                            " from adm_mst_tcompany ";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        lsstart_date = Convert.ToDateTime(objMySqlDataReader["attendance_startdate"].ToString());
                        lsend_date = Convert.ToDateTime(objMySqlDataReader["attendance_enddate"].ToString());

                        msSQL = " select attendance_startdate,attendance_enddate from adm_mst_tcompany a " +
                                " where  (cast('" + leave_fromdate.ToString("yyyy-MM-dd") + "' as date)   " +
                                " between attendance_startdate  and attendance_enddate ) ";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == false)
                        {
                            values.message = "Employee is Allowed to Apply Leave from date " + lsstart_date.ToString("dd-MM-yyyy") + " to  " + lsend_date.ToString("dd-MM-yyyy") + "";
                            return false;
                        }
                    }
                    //Pending Leave
                    msSQL = " select attendance_startdate,attendance_enddate " +
                            " from adm_mst_tcompany ";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        lsstart_date = Convert.ToDateTime(objMySqlDataReader["attendance_startdate"].ToString());
                        lsend_date = Convert.ToDateTime(objMySqlDataReader["attendance_enddate"].ToString());

                        msSQL = " select * from hrm_trn_tleave " +
                                " where employee_gid='" + employee_gid + "' and leave_status='Pending'" +
                                " and leavetype_gid='" + values.leavetype_gid + "'" +
                                " and leave_todate>= '" + lsstart_date.ToString("yyyy-MM-dd") + "' and " +
                                " leave_todate<='" + lsend_date.ToString("yyyy-MM-dd") + "'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            values.message = "Your Previous Leave from date " + leave_fromdate.ToString("dd-MM-yyyy") + " to  " + leave_todate.ToString("dd-MM-yyyy") + " is Pending ,you Cannot apply  !!! Kindly Close your Previous Leave ";
                            return false;
                        }
                    }
                    //Insert Leave
                    msGetGID2 = objcmnfunctions.GetMasterGID("HLVP");
                    if (msGetGID2 == "E")
                    {
                        return false;
                    }

                    msSQL = " insert into hrm_trn_tleave " +
                            " ( leave_gid,  " +
                            " employee_gid , " +
                            " leavetype_gid , " +
                            " leave_applydate , " +
                            " leave_fromdate, " +
                            " leave_todate , " +
                            " leave_noofdays , " +
                            " leave_reason, " +
                            " leave_status ," +
                            " created_by, " +
                            " created_date) " +
                            " values ( " +
                            " '" + msGetGID2 + "', " +
                            " '" + employee_gid + "', " +
                            " '" + values.leavetype_gid + "', " +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                            " '" + leave_fromdate.ToString("yyyy-MM-dd") + "'," +
                            " '" + leave_todate.ToString("yyyy-MM-dd") + "', " +
                            " '" + lsdaycount + "'," +
                            " '" + values.leave_reason.Replace("'", "") + "'," +
                            " 'Pending'," +
                            " '" + user_gid + "'," +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select document_name,document_path from hrm_tmp_tleavedocument where user_gid='" + user_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows)
                    {
                        lsdocname = objMySqlDataReader["document_name"].ToString();
                        lsdocpath = objMySqlDataReader["document_path"].ToString();
                    }
                    

                    msSQL = " update hrm_trn_tleave set document_name='" + lsdocname + "', document_path='" + lsdocpath + "' " +
                            " where leave_gid='" + msGetGID2 + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = "delete from hrm_tmp_tleavedocument where  user_gid='" + user_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        return false;
                    }

                    leave_gid = msGetGID2;
                    msSQL = " select weekoff_applicable,holiday_applicable from hrm_mst_tleavetype where leavetype_gid='" + values.leavetype_gid + "' ";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    
                    if (objMySqlDataReader.HasRows == true)
                    {
                        lsweekoff_applicable = objMySqlDataReader["weekoff_applicable"].ToString();
                        lsholiday_applicable = objMySqlDataReader["holiday_applicable"].ToString();
                    }
                    

                    for (int d = 0; d < values.noofdays_leave; d++)
                    {
                        msSQL = "select " + leave_fromdate.DayOfWeek.ToString() + " as lsday  from hrm_mst_tweekoffemployee where employee_gid='" + employee_gid + "' ";
                        lsday = objdbconn.GetExecuteScalar(msSQL);
                        if (lsday == "Non-working Day")
                        {
                            lsWeekOff_flag = "Y";
                            lstweekoff.Add(lsday);
                        }
                        else
                        {
                            lsWeekOff_flag = "N";
                        }

                        msSQL = " select date(a.holiday_date) as holiday from hrm_mst_tholiday a " +
                                " inner join hrm_mst_tholiday2employee b on a.holiday_gid=b.holiday_gid " +
                                " where b.employee_gid='" + employee_gid + "' and a.holiday_date='" + leave_fromdate.ToString("yyyy-MM-dd") + "' ";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        
                        if (objMySqlDataReader.HasRows == true)
                        {
                            lsholiday = "Y";
                        }
                        else
                        {
                            lsholiday = "N";
                        }
                        
                        if (((lsday == "Non-working Day") && (lsweekoff_applicable == "Y")) || ((lsholiday == "Y") && (lsholiday_applicable == "Y")) == true)
                        {
                            if (values.leave_session == "NA")
                            {
                                half_day = "N";
                                lscount = 1;
                            }
                            else
                            {
                                half_day = "Y";
                                lscount = 0.5;
                            }
                            msGetleavedtlGID = objcmnfunctions.GetMasterGID("HLVC");
                            msSQL =  " Insert into hrm_trn_tleavedtl " +
                                     " (leavedtl_gid," +
                                     " leave_gid ," +
                                     " leavetype_gid," +
                                     " leavedtl_date," +
                                     " created_date," +
                                     " created_by," +
                                     " weekoff_flag," +
                                     " holiday, " +
                                     " leave_count," +
                                     " half_day," +
                                     " half_session," +
                                     " lop) Values( " +
                                     " '" + msGetleavedtlGID + "'," +
                                     " '" + leave_gid + "'," +
                                     " '" + values.leavetype_gid + "'," +
                                     " '" + leave_fromdate.ToString("yyyy-MM-dd") + "'," +
                                     " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                     " '" + user_gid + "'," +
                                     " '" + lsWeekOff_flag + "'," +
                                     " '" + lsholiday + "'," +
                                     " '" + lscount + "'," +
                                     " '" + half_day + "'," +
                                     " '" + values.leave_session + "'";
                            if (lslop != "")
                            {
                                msSQL += ",'Y')";
                            }
                            else
                            {
                                msSQL += ",'N')";
                            }
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else if (lsday != "Non-working Day" && lsholiday == "N")
                        {
                            if (values.leave_session == "NA")
                            {
                                half_day = "N";
                                lsleave_session = "NA";
                                lscount = 1;
                            }
                            else if (values.leave_session == "AN")
                            {
                                half_day = "Y";
                                lsleave_session = "AL";
                                lscount = 0.5;
                            }
                            else if (values.leave_session == "FN")
                            {
                                half_day = "Y";
                                lsleave_session = "FL";
                                lscount = 0.5;
                            }

                            msGetleavedtlGID = objcmnfunctions.GetMasterGID("HLVC");
                            msSQL =  " Insert into hrm_trn_tleavedtl" +
                                     " (leavedtl_gid," +
                                     " leave_gid ," +
                                     " leavetype_gid," +
                                     " leavedtl_date," +
                                     " created_date," +
                                     " created_by," +
                                     " weekoff_flag," +
                                     " holiday, " +
                                     " leave_count," +
                                     " half_day," +
                                     " half_session," +
                                     " lop) Values( " +
                                     " '" + msGetleavedtlGID + "'," +
                                     " '" + leave_gid + "'," +
                                     " '" + values.leavetype_gid + "'," +
                                     " '" + leave_fromdate.ToString("yyyy-MM-dd") + "'," +
                                     " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                     " '" + user_gid + "'," +
                                     " '" + lsWeekOff_flag + "'," +
                                     " '" + lsholiday + "'," +
                                     " '" + lscount + "'," +
                                     " '" + half_day + "'," +
                                     " '" + lsleave_session + "'";
                            if (lslop != "")
                            {
                                msSQL += ",'Y')";
                            }
                            else
                            {
                                msSQL += ",'N')";
                            }
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        leave_fromdate = leave_fromdate.AddDays(1);
                    }

                    objcmnfunctions.PopSummary(employee_gid, user_gid, lscount);
                    objTblRQ = objcmnfunctions.foundRow(table);
                    lscount = objcmnfunctions.foundcount(lscount);

                    if (lscount > 0)
                    {
                        foreach (DataRow objRow1 in objTblRQ.Rows)
                        {
                            employee = objRow1["employee_gid"].ToString();
                            hierary_level = objRow1["hierarchy_level"].ToString();

                            if ((lscount == 1.0) && (employee == employee_gid))
                            {
                                msSQL = " update hrm_trn_tleave set " +
                                        " leave_status='Approved' " +
                                        " where leave_gid = '" + leave_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult != 0)
                                {
                                    msSQL = " update hrm_trn_tleavedtl set " +
                                            " leave_status='Approved' " +
                                            " where leave_gid = '" + leave_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                msSQL = " Select date_format(x.leavedtl_date,'%Y-%m-%d') as leavedate,y.employee_gid from hrm_trn_tleavedtl x " +
                                        " left join hrm_trn_tleave y on x.leave_gid=y.leave_gid " +
                                        " where x.leave_gid = '" + leave_gid + "'";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                if (dt_datatable.Rows.Count != 0)
                                {
                                    foreach (DataRow objtblemploteedatarow in dt_datatable.Rows)
                                    {
                                        lsdate = objtblemploteedatarow["leavedate"].ToString();
                                        lsemployee = objtblemploteedatarow["employee_gid"].ToString();

                                        msSQL = " Select y.employee_gid,z.leavetype_code,x.half_day,x.half_session from hrm_trn_tleavedtl x " +
                                                " left join hrm_trn_tleave y on x.leave_gid=y.leave_gid " +
                                                " left join hrm_mst_tleavetype z on y.leavetype_gid=z.leavetype_gid " +
                                                " where x.leave_gid = '" + leave_gid + "' and x.leavedtl_date='" + lsdate + "' ";
                                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                        
                                        if (objMySqlDataReader.HasRows == true)
                                        {
                                            lsleavetype = objMySqlDataReader["leavetype_code"].ToString();
                                            lshalfday = objMySqlDataReader["half_day"].ToString();
                                            lshalfsession = objMySqlDataReader["half_session"].ToString();
                                            lstype = lsleavetype;

                                        }
                                        
                                        msSQL = "Select employee_gid from hrm_trn_tattendance " +
                                                "where attendance_date='" + lsdate + "' and employee_gid='" + lsemployee + "'";
                                        dt_datatable = objdbconn.GetDataTable(msSQL);
                                        if (dt_datatable.Rows.Count != 0)
                                        {
                                            msSQL = "update hrm_trn_tattendance set " +
                                                    "employee_attendance='Leave', " +
                                                    "attendance_type='" + lstype + "', " +
                                                    " day_session='" + lshalfsession + "', " +
                                                    "update_flag='N'" +
                                                    "where attendance_date='" + lsdate + "' and employee_gid='" + lsemployee + "'";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                        else
                                        {
                                            msGetGID = objcmnfunctions.GetMasterGID("HATP");
                                            msSQL = "Insert Into hrm_trn_tattendance" +
                                                    "(attendance_gid," +
                                                    " employee_gid," +
                                                    " attendance_date," +
                                                    " shift_date," +
                                                    " employee_attendance," +
                                                    " day_session, " +
                                                    " attendance_type)" +
                                                    " VALUES ( " +
                                                    "'" + msGetGID + "', " +
                                                    "'" + lsemployee + "'," +
                                                    "'" + lsdate + "'," +
                                                    "'" + lsdate + "'," +
                                                    "'Leave'," +
                                                    "'" + lshalfsession + "', " +
                                                    "'" + lstype + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (employee != employee_gid)
                                {
                                    msGetGID1 = objcmnfunctions.GetMasterGID("PODC");
                                    msSQL = "insert into hrm_trn_tapproval ( " +
                                            " approval_gid, " +
                                            " approved_by, " +
                                            " approved_date, " +
                                            " seqhierarchy_view, " +
                                            " hierary_level, " +
                                            " submodule_gid, " +
                                            " leaveapproval_gid, " +
                                            " leave_gid" +
                                            " ) values ( " +
                                            " '" + msGetGID1 + "', " +
                                            " '" + employee + "' , " +
                                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                                            " 'N', " +
                                            " '" + hierary_level + "' , " +
                                            " 'HRMLEVARL', " +
                                            " '" + leave_gid + "', " +
                                            " '" + leave_gid + "') ";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                if (mnResult == 0)
                                {
                                    values.message = "Error Occured in Approval";

                                }
                                msSQL = "select approved_by from hrm_trn_tapproval where leaveapproval_gid='" + leave_gid + "' and approved_by='" + employee + "'";
                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                
                                if (objMySqlDataReader.HasRows == true)
                                {
                                    lsflag = objMySqlDataReader["approved_by"].ToString();
                                }
                                

                                if (lsflag == employee_gid)
                                {

                                    msSQL = "update hrm_trn_tapproval set " +
                                            "approval_flag='Y' " +
                                            "where approved_by='" + lsflag + "' and leaveapproval_gid = '" + leave_gid + "'";

                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " SELECT employeereporting_to FROM adm_mst_tmodule2employee " +
                                            " where employee_gid   = '" + employee_gid + "' and module_gid ='HRM' and employeereporting_to='EM1006040001' ";
                                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                    
                                    if (objMySqlDataReader.HasRows == true)
                                    {
                                        
                                        msSQL = "update hrm_trn_tleave set " +
                                                    "leave_status='Approved' " +
                                                    "where leave_gid = '" + leave_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        if (mnResult == 1)
                                        {
                                            msSQL = "update hrm_trn_tleavedtl set " +
                                                           "leave_status='Approved' " +
                                                           "where leave_gid = '" + leave_gid + "'";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }
                                }
                            }
                        }

                        msSQL =   "update hrm_trn_tapproval set " +
                                  "seqhierarchy_view='Y' " +
                                  "where approval_flag='N'and approved_by <> '" + employee_gid + "' " +
                                  "and leaveapproval_gid = '" + leave_gid + "'" +
                                  "order by hierary_level desc limit 1";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = "update hrm_trn_tleave set " +
                                "leave_status='Approved' " +
                                "where leave_gid = '" + leave_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = "update hrm_trn_tleavedtl set " +
                                    "leave_status='Approved' " +
                                    "where leave_gid = '" + leave_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    if (mnResult == 1)
                    {
                        //Mailfunction starts here
                        string lsapprovedby;
                        string message;
                        string employee_mailid = null;
                        string employeename = null;
                        string applied_by = null;
                        string supportmail = null;
                        string pwd = null;
                        int MailFlag;
                        string reason = null;
                        string days = null;
                        string fromhours = null;
                        string tohours = null;
                        string emailpassword = null;
                        string trace_comment;
                        string permission_date = null;
                        string todate = null;
                        string fromdate = null;
                        msSQL = "Select approved_by from hrm_trn_tapproval where leaveapproval_gid= '" + leave_gid + "' and approval_flag<>'Y'";
                        objTblRQ = objdbconn.GetDataTable(msSQL);
                       
                            foreach (DataRow objTblRow in objTblRQ.Rows)
                            {
                                lsapprovedby = objTblRow["approved_by"].ToString();
                                msSQL = "select pop_username,pop_password from adm_mst_tcompany";
                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                if (objMySqlDataReader.HasRows == true)
                                {
                                    
                                    supportmail = objMySqlDataReader["pop_username"].ToString();
                                    emailpassword = objMySqlDataReader["pop_password"].ToString();
                                }
                                
                                if (supportmail != "")
                                {
                                    msSQL =    " select b.employee_emailid,(date_format(a.leave_fromdate,'%d/%m/%y')) as fromdate, " +
                                               "(date_format(a.leave_todate,'%d/%m/%y')) as todate," +
                                               " Concat(c.user_firstname,' ',c.user_lastname) as username,a.leave_noofdays,a.leave_reason " +
                                               " from hrm_trn_tleave a " +
                                               "left join hrm_trn_tapproval d on a.leave_gid=d.leaveapproval_gid " +
                                               " left join hrm_mst_temployee b on d.approved_by=b.employee_gid " +
                                               " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                               " where a.leave_gid='" + leave_gid + "' and b.employee_gid='" + lsapprovedby + "'";
                                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                    if (objMySqlDataReader.HasRows == true)
                                    {
                                        
                                        employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                                        employeename = objMySqlDataReader["username"].ToString();
                                        reason = objMySqlDataReader["leave_reason"].ToString();
                                        days = objMySqlDataReader["leave_noofdays"].ToString();
                                        fromdate = objMySqlDataReader["fromdate"].ToString();
                                        todate = objMySqlDataReader["todate"].ToString();

                                    }
                                    
                                    if (employee_mailid != "")
                                    {
                                        msSQL = " select a.created_by," +
                                                " Concat(c.user_firstname,' ',c.user_lastname) as username " +
                                                " from hrm_trn_tleave a " +
                                                " left join adm_mst_tuser c on a.created_by=c.user_gid " +
                                                " where a.leave_gid='" + leave_gid + "'";
                                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                        if (objMySqlDataReader.HasRows == true)
                                        {
                                            
                                            applied_by = objMySqlDataReader["username"].ToString();
                                        }
                                        

                                        message = "Hi Sir/Madam,  <br />";
                                        message = message + "<br />";
                                        message = message + "I have applied for " + values.leavetype_gid + " <br />";
                                        message = message + "<br />";
                                        message = message + "<b>Reason :</b> " + reason + "<br />";
                                        message = message + "<br />";
                                        message = message + "<b>From Date :</b> " + fromdate + " &nbsp; &nbsp; <b>To Date :</b> " + todate + "<br />";
                                        message = message + "<br />";
                                        message = message + "<b>Total No of Days :</b> " + days + " <br />";
                                        message = message + "<br />";
                                        message = message + " Thanks and Regards  <br />";
                                        message = message + " " + applied_by + " <br />";
                                        try
                                        {
                                            MailFlag = objcmnfunctions.SendSMTP2(supportmail, emailpassword, employee_mailid, "" + applied_by + " applied for Leave", message, "", "", "");

                                        }
                                        catch
                                        {

                                        }
                                    }
                                }

                            }
                    }
                }                
                fnopeningbalance.openingbalance(employee_gid);
                values.status = true;
                return true;
            }
            catch(Exception ex)
            {
                values.status = false;
                    
                    objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                    "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
                
                return false;

            }

        }
        public bool Daleavevalidate(mdlleavevalidate values, string user_gid, string employee_gid)
        {
            try
            {
                
                msSQL = " Select beyond_eligible from hrm_mst_tleavetype where leavetype_gid = '" + values.leave_gid + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                
                if (objMySqlDataReader.HasRows == true)
                {
                    lsbeyond_eligible = objMySqlDataReader["beyond_eligible"].ToString();
                    
                    if (lsbeyond_eligible == "N")

                        msSQL = " select available_leavecount,leavetype_gid from hrm_mst_tleavecreditsdtl where leavetype_gid='" + values.leave_gid + "'" +
                                " and employee_gid='" + employee_gid + "' and month='" + DateTime.Now.ToString("MM") + "' and year ='" + DateTime.Now.ToString("yyyy") + "'" +
                                " and active_flag='Y'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        lsleave_eligible = objMySqlDataReader["available_leavecount"].ToString();
                        leave_eligible = Convert.ToDouble(lsleave_eligible);
                    }
                    
                    if (values.leave_gid != "LOP")
                    {
                        msSQL = " select weekoff_applicable,holiday_applicable from hrm_mst_tleavetype where leavetype_gid='" + values.leave_gid + "'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            lsweekoff_applicable = objMySqlDataReader["weekoff_applicable"].ToString();
                            lsholiday_applicable = objMySqlDataReader["holiday_applicable"].ToString();
                        }

                        
                        if (lsweekoff_applicable == "N" && lsholiday_applicable == "Y")
                        {
                            DateTime leavefromdate;

                            Double lsNoOFDays = 0;
                            string lsleave;
                            leavefromdate = values.leave_from;
                            for (int d = 0; d < values.leave_days; d++)
                            {

                                msSQL = " select " + leavefromdate.DayOfWeek.ToString() + " as lsday  from hrm_mst_tweekoffemployee " +
                                        " where employee_gid='" + employee_gid + "' ";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                if (dt_datatable.Rows.Count != 0)
                                {

                                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                                    {
                                        lsday = dr_datarow["lsday"].ToString();
                                        if (lsday == "Non-working Day")
                                        {
                                            lsWeekOff_flag = "Y";
                                            lstweekoff.Add(lsday);
                                        }

                                        else
                                        {
                                            lsdaycount = lsdaycount + 1;
                                            lsWeekOff_flag = "N";
                                        }
                                    }
                                }
                                leavefromdate = leavefromdate.AddDays(1);
                            }

                            if (values.leave_session == "NA")
                            {
                                lsNoOFDays = lsdaycount;
                            }
                            else
                            {
                                if (lsdaycount != 0)
                                {
                                    lsNoOFDays = 0.5;
                                }
                            }

                            if ((leave_eligible < lsNoOFDays) == true)
                            {
                                values.leave_days = 0;
                                values.message = "No Available Leave Balance";
                                return true;
                            }
                            else if (lsdaycount == 0)
                            {
                                values.leave_days = 0;
                                values.message = "Leave cannot be applied on weekoff!!! ";
                                return true;
                            }
                            values.leave_days = lsdaycount;
                            return true;
                        }
                        else if (lsweekoff_applicable == "Y" && lsholiday_applicable == "N")
                        {
                            DateTime leavefromdate;

                            Double lsNoOFDays = 0;
                            string lsleave;
                            leavefromdate = values.leave_from;
                            for (int d = 0; d < values.leave_days; d++)
                            {

                                msSQL = " select date(a.holiday_date) as holiday from hrm_mst_tholiday a " +
                                        " inner join hrm_mst_tholiday2employee b on a.holiday_gid=b.holiday_gid " +
                                        " where b.employee_gid='" + employee_gid + "' and a.holiday_date='" + leavefromdate.ToString("yyyy-MM-dd") + "' ";
                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                if (objMySqlDataReader.HasRows == true)
                                {
                                    lsholiday = "Y";
                                }
                                else
                                {
                                    lsdaycount = lsdaycount + 1;
                                    lsholiday = "N";
                                }

                                
                                leavefromdate = leavefromdate.AddDays(1);
                            }

                            if (values.leave_session == "NA")
                            {
                                lsNoOFDays = lsdaycount;
                            }
                            else
                            {
                                if (lsdaycount != 0)
                                {
                                    lsNoOFDays = 0.5;
                                }
                            }

                            if ((leave_eligible < lsNoOFDays) == true)
                            {
                                values.leave_days = 0;
                                values.message = "No Available Leave Balance";
                                return true;
                            }
                            else if (lsdaycount == 0)
                            {
                                values.leave_days = 0;
                                values.message = "Leave cannot be applied on Holiday!!! ";
                                return true;
                            }
                            values.leave_days = lsdaycount;
                            return true;
                        }
                        else if (lsweekoff_applicable == "N" && lsholiday_applicable == "N")
                        {
                            DateTime leavefromdate;

                            Double lsNoOFDays = 0;
                            string lsleave;
                            leavefromdate = values.leave_from;
                            for (int d = 0; d < values.leave_days; d++)
                            {

                                msSQL = " select " + leavefromdate.DayOfWeek.ToString() + " as lsday  from hrm_mst_tweekoffemployee " +
                                        " where employee_gid='" + employee_gid + "' ";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                if (dt_datatable.Rows.Count != 0)
                                {

                                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                                    {
                                        lsday = dr_datarow["lsday"].ToString();
                                        if (lsday == "Non-working Day")
                                        {
                                            lsWeekOff_flag = "Y";
                                            lstweekoff.Add(lsday);
                                        }

                                        else
                                        {
                                            msSQL = " select date(a.holiday_date) as holiday from hrm_mst_tholiday a " +
                                       " inner join hrm_mst_tholiday2employee b on a.holiday_gid=b.holiday_gid " +
                                       " where b.employee_gid='" + employee_gid + "' and a.holiday_date='" + leavefromdate.ToString("yyyy-MM-dd") + "' ";
                                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                            if (objMySqlDataReader.HasRows == true)
                                            {
                                                lsholiday = "Y";
                                            }
                                            else
                                            {
                                                lsdaycount = lsdaycount + 1;
                                                lsholiday = "N";
                                            }

                                            
                                            lsWeekOff_flag = "N";
                                        }
                                    }
                                }
                                leavefromdate = leavefromdate.AddDays(1);
                            }

                            if (values.leave_session == "NA")
                            {
                                lsNoOFDays = lsdaycount;
                            }
                            else
                            {
                                if (lsdaycount != 0)
                                {
                                    lsNoOFDays = 0.5;
                                }
                            }

                            if ((leave_eligible < lsNoOFDays) == true)
                            {
                                values.leave_days = 0;
                                values.message = "No Available Leave Balance";
                                return true;
                            }
                            else if (lsdaycount == 0)
                            {
                                values.leave_days = 0;
                                values.message = "Leave cannot be applied on weekoff or Holiday!!! ";
                                return true;
                            }
                            values.leave_days = lsdaycount;
                            return true;
                        }
                        else
                        {

                            if ((leave_eligible < values.leave_days) == true)

                            {
                                values.message = "No Available Leave Balance";
                                return false;
                            }

                            return true;
                        }
                    }
                }
                return true;
            }

            catch (Exception ex)            
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

                return false;
            }       
        }
        public bool DaGetApplyLeaveSummary(applyleavegetleavedetails values, string employee_gid)
        {
            try
            {
                
                msSQL =   " select a.leave_gid,a.leavetype_gid,a.document_name,date_format(a.leave_applydate,'%d-%m-%Y') as leave_applydate,date_format(a.leave_fromdate,'%d-%m-%Y') as leave_fromdate,date_format(a.leave_todate,'%d-%m-%Y') as leave_todate,a.leave_noofdays,b.leavetype_name, " +
                          " a.leave_reason,a.leave_status,concat(d.user_firstname,' ',d.user_lastname) as leave_approvedby " +
                          " from hrm_trn_tleave a " +
                          " left join hrm_mst_tleavetype b on a.leavetype_gid=b.leavetype_gid " +
                          " left join hrm_mst_temployee c on a.leave_approvedby=c.employee_gid " +
                          " left join adm_mst_tuser d on d.user_gid=c.user_gid " +
                          " where a.employee_gid='" + employee_gid + "' order by a.leave_applydate desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<leave_list1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (dr_datarow["document_name"].ToString() != "")
                        {
                            lsdocument_flag = "Y";
                        }
                        else
                        {
                            lsdocument_flag = "N";
                        }
                        getleave.Add(new leave_list1
                        {
                            leavetype_gid = (dr_datarow["leave_gid"].ToString()),
                            leavetype_name = (dr_datarow["leavetype_name"].ToString()),
                            leave_from = (dr_datarow["leave_fromdate"].ToString()),
                            leave_to = (dr_datarow["leave_todate"].ToString()),
                            noofdays_leave = (dr_datarow["leave_noofdays"].ToString()),
                            leave_reason = (dr_datarow["leave_reason"].ToString()),
                            approval_status = (dr_datarow["leave_status"].ToString()),
                            approved_by = (dr_datarow["leave_approvedby"].ToString()),
                            leave_applydate = (dr_datarow["leave_applydate"].ToString()),
                            document_name = lsdocument_flag
                        });
                    }
                    values.leave_list= getleave;
                }
                dt_datatable.Dispose();
                fnopeningbalance.openingbalance(employee_gid);
                return true;
            }
            
            catch(Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

                return false;
            }
            
        }
        public bool DaGetApproveLeaveSummary(applyleavegetleavedetails values, string employee_gid)
        {
            try
            {
                
                msSQL = " select a.leave_gid,a.leavetype_gid,date_format(a.leave_fromdate,'%d-%m-%Y') as leave_fromdate,date_format(a.leave_todate,'%d-%m-%Y') as leave_todate,a.leave_noofdays,b.leavetype_name, " +
                        " a.leave_reason,a.leave_status,concat(d.user_firstname,' ',d.user_lastname) as leave_appliedby " +
                        " from hrm_trn_tleave a " +
                        " left join hrm_mst_tleavetype b on a.leavetype_gid=b.leavetype_gid " +
                        " left join hrm_mst_temployee c on a.employee_gid=c.employee_gid " +
                        " left join adm_mst_tuser d on d.user_gid=c.user_gid " +
                        " where a.leave_approvedby='" + employee_gid + "' order by a.leave_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<leave_list1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleave.Add(new leave_list1
                        {
                            leave_gid = (dr_datarow["leave_gid"].ToString()),
                            leavetype_gid = (dr_datarow["leavetype_gid"].ToString()),
                            leavetype_name = (dr_datarow["leavetype_name"].ToString()),
                            leave_from = (dr_datarow["leave_fromdate"].ToString()),
                            leave_to = (dr_datarow["leave_todate"].ToString()),
                            noofdays_leave = (dr_datarow["leave_noofdays"].ToString()),
                            leave_reason = (dr_datarow["leave_reason"].ToString()),
                            approval_status = (dr_datarow["leave_status"].ToString()),
                            applied_by = (dr_datarow["leave_appliedby"].ToString()),
                        });
                    }
                    values.leave_list = getleave;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }

            catch(Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + 
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }

            
        }
        public bool DaPostLeavePendingDelete(string leavetype_gid, applyleavegetleavedetails values)
        {
            try
            {
                
                msSQL = " Delete from hrm_trn_tleavedtl where leave_gid='" + leavetype_gid + "' and leave_status <> 'Approved'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " Delete from hrm_trn_tleave where leave_gid='" + leavetype_gid + "' and leave_status <> 'Approved'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {
                    values.status = true;
                    return true;
                }
                else
                {
                    values.status = false;
                    return false;
                }
            }
            
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }

            
        }
        // Month Wise Leave Report......//
        public bool getleavereport_da(string employee_gid, monthwise_leavereport values)
        {
            try
            {
                
                string query, suquery;
                query = "select cast(concat(date_format(e.attendance_date,'%b'),' - ',year(e.attendance_date)) as char) as Duration ";

                mssql1 = " select a.leavetype_gid,a.leavetype_name,a.leavetype_code " +
                         " from hrm_mst_tleavetype a" +
                         " left join hrm_mst_tleavegradedtl b on a.leavetype_gid=b.leavetype_gid" +
                         " left join hrm_trn_tleavegrade2employee c on c.leavegrade_gid=b.leavegrade_gid" +
                         " where b.active_flag='Y' and c.employee_gid='" + employee_gid + "' ";

                mssql1 += "group by a.leavetype_gid order by a.leavetype_gid asc ";
                dt_datatable = objdbconn.GetDataTable(mssql1);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        suquery = "";
                        lsleavetype_gid = dt["leavetype_gid"].ToString();
                        lsleavetype_name = dt["leavetype_name"].ToString();
                        string lsnew = lsleavetype_name.Replace(" ", "");
                        lsleavetype_code = dt["leavetype_code"].ToString();
                        suquery = " ,(select ifnull(SUM(if(a.day_session='NA','1','0.5')),0) as count " +
                              " from hrm_trn_tattendance a where a.employee_gid='" + employee_gid + "' and a.employee_attendance='Leave' and a.attendance_type='" + lsleavetype_code + "' " +
                              " and a.employee_gid='" + employee_gid + "' and month(a.attendance_date)=month(e.attendance_date) and year(a.attendance_date)=year(e.attendance_date)) as '" + lsnew + "' ";
                        query += suquery;
                    }
                }

                query += ",(select count(employee_attendance) from hrm_trn_tattendance  x " +
                   " where x.employee_gid='" + employee_gid + "' and employee_attendance='Absent' and " +
                   " month(x.attendance_date)=month(e.attendance_date) and year(x.attendance_date)=year(e.attendance_date)) as LOP, " +
                   " (select ifnull(SUM(if(x.attendance_type='OD','1','0.5')),0) from hrm_trn_tattendance  x " +
                   " where x.employee_gid='" + employee_gid + "' and employee_attendance='Onduty' and " +
                   " month(x.attendance_date)=month(e.attendance_date) and year(x.attendance_date)=year(e.attendance_date)) as OD," +
                   " (select ifnull(sum(permission_totalhours),0) as total_hours from hrm_trn_tpermission h where h.permission_status='Approved'" +
                   " and employee_gid='" + employee_gid + "' and month(h.permission_date)=month(e.attendance_date) and year(h.permission_date)=year(e.attendance_date)) as Permission," +
                   " (select ifnull(sum(compoff_noofdays),0) as compoff from hrm_trn_tcompensatoryoff i where i.compensatoryoff_status='Approved'" +
                   " and employee_gid='" + employee_gid + "' and month(i.compensatoryoff_applydate)=month(e.attendance_date) and year(i.compensatoryoff_applydate)=year(e.attendance_date)) as CompOff ";

                query += " From hrm_trn_tattendance e " +
                         " where employee_gid='" + employee_gid + "' and attendance_date <= date(now()) and " +
                         " attendance_date >=date('" + DateTime.Now.AddMonths(-5).ToString("yyyy-MM-dd") + "')";

                query += " group by monthname(e.attendance_date) order by year(e.attendance_date) desc, month(e.attendance_date) desc ";

                dt_datatable1 = objdbconn.GetDataTable(query);
                if (dt_datatable1.Rows.Count != 0)
                {

                    //string JSONresult;
                    //JSONresult = JsonConvert.SerializeObject(dt_datatable1);
                    //values.response = JSONresult;
                    //Rootobject objCustomer = JsonConvert.DeserializeObject<Rootobject>(JSONresult);
                }

                dt_datatable.Dispose();
                dt_datatable1.Dispose();

                values.status = true;
                return true;
            }

            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPostUploadDocument(HttpRequest httpRequest, applyleaveuploaddocument objfilename, string employee_gid, string user_gid)
        {
            try
            {
                

                applyleaveuploaddocument objdocumentmodel = new applyleaveuploaddocument();
                HttpFileCollection httpFileCollection;

                string lsfilepath = string.Empty;
                string lsdocument_gid = string.Empty;
                MemoryStream ms = new MemoryStream();
                MemoryStream ms_stream = new MemoryStream();
                string document_gid = string.Empty;
                string lscompany_code = string.Empty;
                string pdfFilName = string.Empty;
                Stream ls_readStream;
                string leave_gid = HttpContext.Current.Request.Params["leave_gid"];
                string lsdocumenttype_gid = string.Empty;
                String path = lspath;

                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a where 1=1";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "HRMS/ApplyLeave/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                {
                    if ((!System.IO.Directory.Exists(path)))
                        System.IO.Directory.CreateDirectory(path);
                }

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");

                try
                {
                    if (httpRequest.Files.Count > 0)
                    {
                        string lsfirstdocument_filepath = string.Empty;

                        httpFileCollection = httpRequest.Files;
                        for (int i = 0; i < httpFileCollection.Count; i++)
                        {
                            httpPostedFile = httpFileCollection[i];
                            string FileExtension = httpPostedFile.FileName;
                            //string lsfile_gid = msdocument_gid + FileExtension;
                            string lsfile_gid = msdocument_gid;
                            FileExtension = Path.GetExtension(FileExtension).ToLower();
                            lsfile_gid = lsfile_gid + FileExtension;
                            ls_readStream = httpPostedFile.InputStream;
                            ls_readStream.CopyTo(ms);
                            lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "HRMS/ApplyLeave/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                            objcmnfunctions.uploadFile(lspath, lsfile_gid);

                            msSQL = " select * from hrm_tmp_tleavedocument where created_by = '" + user_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            if (objMySqlDataReader.HasRows == false)
                            {

                                msGetGid = objcmnfunctions.GetMasterGID("TDOC");
                                msSQL =      " insert into hrm_tmp_tleavedocument( " +
                                             " tmpdocument_gid," +
                                             " user_gid ," +
                                             " document_name," +
                                             " document_path," +
                                             " created_by," +
                                             " created_date" +
                                             " )values(" +
                                             "'" + msGetGid + "'," +
                                             "'" + user_gid + "'," +
                                             "'" + httpPostedFile.FileName + "'," +
                                             "'" + lspath + lsfile_gid + "'," +
                                             "'" + user_gid + "'," +
                                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult != 0)
                                {
                                    objfilename.status = true;
                                    objfilename.message = "Document Uploaded Successfully";
                                }
                                else
                                {
                                    objfilename.status = false;
                                    objfilename.message = "Error Occured";
                                }
                            }
                            else
                            {
                                objfilename.status = false;
                                objfilename.message = "Document already uploaded for this leave";
                            }
                        }
                        msSQL = "select document_name,tmpdocument_gid from hrm_tmp_tleavedocument where created_by='" + user_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var get_filename = new List<applyleaveuploaddocumentlist>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dr_datarow in dt_datatable.Rows)
                            {
                                get_filename.Add(new applyleaveuploaddocumentlist
                                {
                                    documentname = (dr_datarow["document_name"].ToString()),
                                    tmpdocument_gid = (dr_datarow["tmpdocument_gid"].ToString())
                                });
                            }
                            objfilename.filename_list = get_filename;
                        }
                        dt_datatable.Dispose();
                    }

                    if (mnResult != 0)
                    {
                        objfilename.status = true;
                        objfilename.message = "Success";
                        return true;
                    }
                    else
                    {
                        objfilename.status = false;

                        objfilename.message = "failure";

                        return false;
                    }
                }
                catch (Exception ex)
                {
                    objfilename.status = false;

                    objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                    "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

                    objfilename.message = "failure";
                    return false;
                }
            }
            catch(Exception ex)
            {
                objfilename.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaGetDeleteDocument(string tmpdocument_gid, applyleaveuploaddocument values, string user_gid)
        {
            try
            {
                

                msSQL = " delete from hrm_tmp_tleavedocument where tmpdocument_gid='" + tmpdocument_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = "select document_name,tmpdocument_gid from hrm_tmp_tleavedocument where created_by='" + user_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var get_filename = new List<applyleaveuploaddocumentlist>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            get_filename.Add(new applyleaveuploaddocumentlist
                            {
                                documentname = (dr_datarow["document_name"].ToString()),
                                tmpdocument_gid = (dr_datarow["tmpdocument_gid"].ToString())
                            });
                        }
                        values.filename_list = get_filename;
                    }
                    dt_datatable.Dispose();


                    values.status = true;
                    values.message = "success";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "failure";
                    return false;
                }
            }
            
            catch(Exception ex) 
            {
                values.status = false;

                values.message = "failure";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

                return false;
            }
        }        
    }
}


    
