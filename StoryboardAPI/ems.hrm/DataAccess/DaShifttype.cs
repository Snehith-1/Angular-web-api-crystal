using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.hrm.Models;
using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;


namespace ems.hrm.DataAccess
{
    public class DaShifttype
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;

        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        int mnResult; 
        string msGetGid, msGetGid1, lsempoyeegid, msgetshift;

        // Module Master Summary
        public void DaShiftSummary(MdlShiftType values)
        {

            try
            {

                msSQL = " select a.shifttype_gid,a.shifttype_name," +
                        " group_concat(c.branch_name) as branch_name,status from hrm_mst_tshifttype a " +
                        " left join hrm_mst_tshifttype2branch b on b.shifttype_gid = a.shifttype_gid " +
                        " left join hrm_mst_tbranch c on c.branch_gid = b.branch_gid where 1=1 " +
                        " GROUP BY shifttype_gid ORDER BY shifttype_name ASC";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<shift_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new shift_list
                        {
                            shifttype_gid = dt["shifttype_gid"].ToString(),
                            shifttype_name = dt["shifttype_name"].ToString(),
                            status = dt["status"].ToString(),
                        });
                        values.shift_list = getModuleList;
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
        public void DaGetWeekdaysummary(shifttypeadd_list values)
        {
            try
            {


                msSQL = "select weekday_gid,weekday from hrm_mst_tweekdays";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<weekday_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new weekday_list
                        {
                            weekday_gid = dt["weekday_gid"].ToString(),
                            weekday = dt["weekday"].ToString(),

                        });
                        values.weekday_list = getModuleList;
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
        public void Dashiftweekdaystime(string employee_gid, shifttypeadd_list values)
        {

            try
            {

                msGetGid = objcmnfunctions.GetMasterGID("HMSl");
                msgetshift = objcmnfunctions.GetMasterGID("HSPM");


                msSQL = " Insert into hrm_mst_tmasterscheduler( " +
                    " scheduler_gid," +
                    " shifttype_gid, " +
                    " shift_mode," +
                    " run_time," +
                    " execute_in," +
                    " cutoff_time," +
                    " overnight_flag," +
                    " In_overnightflag," +
                    " Out_overnightflag," +
                    " created_by," +
                    " created_date " +
                    " )values( " +
                    " '" + msGetGid + "'," +
                     "'" + msgetshift + "', " +
                    " 'Login', " +
                    "'" + values.login_scheduler + "'," +
                    "'" + values.login_scheduler + "'," +
                    "'" + values.entrycutoff_time + "'," +
                    "'" + values.overnight_flag + "'," +
                    "'" + values.inovernight_flag + "'," +
                    "'" + values.outovernight_flag + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " Insert into hrm_mst_tmasterscheduler( " +
                    " scheduler_gid," +
                    " shifttype_gid, " +
                    " shift_mode," +
                    " run_time," +
                    " execute_out," +
                    " cutoff_time," +
                    " overnight_flag," +
                    " In_overnightflag," +
                    " Out_overnightflag," +
                    " created_by," +
                    " created_date " +
                    " )values( " +
                    " '" + msGetGid + "'," +
                     "'" + msgetshift + "', " +
                    " 'Logout', " +
                    "'" + values.logout_schedular + "'," +
                    "'" + values.logout_schedular + "'," +
                    "'" + values.entrycutoff_time + "'," +
                    "'" + values.overnight_flag + "'," +
                    "'" + values.inovernight_flag + "'," +
                    "'" + values.outovernight_flag + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " insert into hrm_mst_tshifttype (" +
                    " shifttype_gid, " +
                    " shifttype_name," +
                    " grace_time," +
                    " email_list," +
                    " created_by," +
                    " created_date )" +
                    " values (" +
                    "'" + msgetshift + "', " +
                    "'" + values.shift_name + "'," +
                    "'" + values.grace_time + "'," +
                    "'" + values.email_list + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                foreach (var data in values.weekday_list)
                {
                    string msgetshiftdtl = objcmnfunctions.GetMasterGID("HSDL");
                    msSQL = " insert into hrm_mst_tshifttypedtl (" +
                        " shifttypedtl_gid, " +
                        " shifttypedtl_name, " +
                        " shifttype_gid, " +
                        " shift_fromhours, " +
                        " shift_fromminutes, " +
                        " OTcutoff_hours, " +
                        " OTcutoff_minutes, " +
                        " shift_tohours," +
                        " created_by, " +
                        " created_date," +
                        " shift_tominutes)" +
                        " values (" +
                        "'" + msgetshiftdtl + "', " +
                        "'" + data.weekday + "'," +
                        "'" + msgetshift + "'," +
                        "'" + data.logintime.ToString("HH") + "'," +
                        "'" + data.logintime.ToString("mm") + "'," +
                        "'" + data.Ot_cutoff.ToString("HH") + "'," +
                        "'" + data.Ot_cutoff.ToString("mm") + "'," +
                        "'" + data.logouttime.ToString("HH") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                        "'" + data.logouttime.ToString("mm") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Shift Type Added Sucessfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Adding Shift Type";
                }
            }

            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }

        public void DaGetshiftTimepopup(string shifttype_gid, MdlShiftType values)
        {

            try
            {

                msSQL = " select shifttypedtl_gid, shifttypedtl_name, concat(shift_fromhours,':',shift_fromminutes) as start_time, " +
                " concat(shift_tohours,':',shift_tominutes) as end_time  from hrm_mst_tshifttypedtl where shifttype_gid='" + shifttype_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Time_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Time_list
                        {

                            shifttypedtl_gid = dt["shifttypedtl_gid"].ToString(),
                            shifttypedtl_name = dt["shifttypedtl_name"].ToString(),
                            start_time = dt["start_time"].ToString(),
                            end_time = dt["end_time"].ToString(),
                        });
                        values.Time_list = getModuleList;
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
        public void DaDeleteShift(string shifttype_gid, MdlShiftType values)
        {
            try
            {

                msSQL = "  delete from hrm_mst_tshifttype where shifttype_gid='" + shifttype_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Shift Type Deleted Successfully";
                }
                else
                {
                    {
                        values.status = false;
                        values.message = "Error While Deleting Shift Type";
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
        public void DaGetshiftActive(string shifttype_gid, MdlShiftType values)
        {
            try
            {

                msSQL = " update hrm_mst_tshifttype set" +
                       " status='Y'" +
                       " where shifttype_gid = '" + shifttype_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Shift Type Activated Successfully";
                }
                else
                {
                    {
                        values.status = false;
                        values.message = "Error While Shift Type Activated";
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
        public void DaGetshiftInActive(string shifttype_gid, MdlShiftType values)
        {

            try
            {

                msSQL = " update hrm_mst_tshifttype set" +
                       " status='N'" +
                       " where shifttype_gid = '" + shifttype_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Shift Type Inactivated Successfully";
                }
                else
                {
                    {
                        values.status = false;
                        values.message = "Error While Shift Type Inactivated";
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

        // Assign Summary
        public void DaGetShiftAssignsumary(Assignsubmit_list values)
        {

            try
            {

                msSQL = " select b.employee_gid,a.user_code,a.user_firstname, " +
                        "  d.department_name,j.designation_name " +
                        " from adm_mst_tuser a inner join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        " inner join hrm_mst_tbranch c on b.branch_gid = c.branch_gid " +
                        " inner join hrm_mst_tdepartment d on b.department_gid = d.department_gid " +
                        " inner join hrm_trn_temployeetypedtl e on b.employee_gid=e.employee_gid " +
                        " left join hrm_mst_tsectionassign2employee f on f.employee_gid = b.employee_gid " +
                        " left join hrm_mst_tsection g on f.section_gid=g.section_gid " +
                        " left join hrm_mst_tblock h on f.block_gid=h.block_gid " +
                        " left join hrm_mst_tunit i on i.unit_gid=f.unit_gid " +
                        " inner join adm_mst_tdesignation j on j.designation_gid = b.designation_gid " +
                        " where  b.employee_gid not in " +
                        "(select a.employee_gid from hrm_trn_temployee2shifttype a left Join hrm_mst_temployee b on a.employee_gid = b.employee_gid  )" +
                        " and a.user_status='Y'  and e.employeetype_name='Roll'  order by a.user_firstname asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Assign_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Assign_list
                        {
                            employee_gid = dt["employee_gid"].ToString(),
                            user_code = dt["user_code"].ToString(),
                            user_firstname = dt["user_firstname"].ToString(),
                            department_name = dt["department_name"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                        });
                        values.Assign_list = getModuleList;
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

        public void DaShiftAssignSubmit(string employee_gid, Assignsubmit_list values)
        {
            try
            {

                foreach (var data in values.Assign_list)
                {
                    msSQL = "select * from hrm_trn_temployee2shifttype where employee_gid='" + data.employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    if (dt_datatable.Rows.Count != 0)
                    {
                        msSQL = " update hrm_trn_temployee2shifttype set " +
                            " shift_status='Y'," +
                            " update_flag='N', " +
                            " shifteffective_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                            " updated_by='" + data.employee_gid + "', " +
                            " shifttype_gid='" + values.shifttype_gid + "' " +
                            " where employee_gid='" + data.employee_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = " select * from hrm_trn_temployee2shifttypedtl " +
                                            " where employee_gid='" + data.employee_gid + "' " +
                                            " and shifttype_gid='" + values.shifttype_gid + "' ";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable.Rows.Count != 0)
                            {
                                msSQL = " update hrm_trn_temployee2shifttypedtl set shift_status='Y' where employee_gid='" + data.employee_gid + "' " +
                                       " and shifttype_gid='" + values.shifttype_gid + "' ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                    else
                    {
                        msSQL = " select a.shifttypedtl_gid,a.shifttype_gid,a.shifttypedtl_name,a.shift_fromhours,a.shift_tohours, " +
                               " a.lunchout_hours,a.lunchout_minutes,a.lunchin_hours,a.lunchin_minutes, " +
                               " a.shift_fromminutes, a.shift_tominutes from hrm_mst_tshifttypedtl a " +
                               " inner join hrm_mst_tshifttype b on a.shifttype_gid=b.shifttype_gid " +
                               " where b.shifttype_gid='" + values.shifttype_gid + "' ";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count > 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                msGetGid = objcmnfunctions.GetMasterGID("HEST");
                                msSQL = " insert into hrm_trn_temployee2shifttypedtl (" +
                                " employee2shifttypedtl_gid," +
                                " employee2shifttype_gid," +
                                " shifttype_gid," +
                                " shifttypedtl_gid," +
                                " employee2shifttype_name," +
                                " shift_fromhours," +
                                " shift_fromminutes," +
                                " shift_tohours," +
                                " shift_tominutes," +
                                " lunchout_hours," +
                                " lunchout_minutes," +
                                " lunchin_hours," +
                                " lunchin_minutes," +
                                " employee_gid," +
                                " created_by," +
                                " shift_status, " +
                                " created_date)" +
                                " values (" +
                                " '" + msGetGid + "', " +
                                " '" + msGetGid1 + "', " +
                                " '" + values.shifttype_gid + "', " +
                                " '" + dt["shifttypedtl_gid"].ToString() + "', " +
                                " '" + dt["shifttypedtl_name"].ToString() + "'," +
                                " '" + dt["shift_fromhours"].ToString() + "'," +
                                " '" + dt["shift_fromminutes"].ToString() + "'," +
                                " '" + dt["shift_tohours"].ToString() + "'," +
                                " '" + dt["shift_tominutes"].ToString() + "'," +
                                " '" + dt["lunchout_hours"].ToString() + "'," +
                                " '" + dt["lunchout_minutes"].ToString() + "'," +
                                " '" + dt["lunchin_hours"].ToString() + "'," +
                                " '" + dt["lunchin_minutes"].ToString() + "'," +
                                " '" + employee_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'Y'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            msGetGid1 = objcmnfunctions.GetMasterGID("HESC");
                            msSQL = " insert into hrm_trn_temployee2shifttype( " +
                                    " employee2shifttype_gid, " +
                                    " employee_gid,  " +
                                    " shifttype_gid, " +
                                    " shifteffective_date ," +
                                    " shiftactive_flag ," +
                                    " created_by," +
                                    " created_date) " +
                                    " values( " +
                                    "'" + msGetGid1 + "'," +
                                    "'" + data.employee_gid + "'," +
                                    "'" + values.shifttype_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                    "'Y'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                values.status = true;
                                values.message ="Employee Assign  Successfully";
                            }
                            else
                            {
                                {
                                    values.status = false;
                                    values.message = "Error While Employee Assign";
                                }
                            }
                        }
                    }


                        

                    
                }

                //-------------select count for employee to assign the shift------------------------------
                msSQL = " select a.shifttypedtl_gid,a.shifttype_gid,a.shifttypedtl_name,a.shift_fromhours,a.shift_tohours, " +
                                               " a.lunchout_hours,a.lunchout_minutes,a.lunchin_hours,a.lunchin_minutes, " +
                                               " a.shift_fromminutes, a.shift_tominutes from hrm_mst_tshifttypedtl a " +
                                               " inner join hrm_mst_tshifttype b on a.shifttype_gid=b.shifttype_gid " +
                                               " where b.shifttype_gid='" + values.shifttype_gid + "' ";
                                               dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count > 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("HEST");
                        msSQL = " insert into hrm_trn_temployee2shifttypedtl (" +
                        " employee2shifttypedtl_gid," +
                        " employee2shifttype_gid," +
                        " shifttype_gid," +
                        " shifttypedtl_gid," +
                        " employee2shifttype_name," +
                        " shift_fromhours," +
                        " shift_fromminutes," +
                        " shift_tohours," +
                        " shift_tominutes," +
                        " lunchout_hours," +
                        " lunchout_minutes," +
                        " lunchin_hours," +
                        " lunchin_minutes," +
                        " employee_gid," +
                        " created_by," +
                        " shift_status, " +
                        " created_date)" +
                        " values (" +
                        " '" + msGetGid + "', " +
                        " '" + msGetGid1 + "', " +
                        " '" + values.shifttype_gid + "', " +
                        " '" + dt["shifttypedtl_gid"].ToString() + "', " +
                        " '" + dt["shifttypedtl_name"].ToString() + "'," +
                        " '" + dt["shift_fromhours"].ToString() + "'," +
                        " '" + dt["shift_fromminutes"].ToString() + "'," +
                        " '" + dt["shift_tohours"].ToString() + "'," +
                        " '" + dt["shift_tominutes"].ToString() + "'," +
                        " '" + dt["lunchout_hours"].ToString() + "'," +
                        " '" + dt["lunchout_minutes"].ToString() + "'," +
                        " '" + dt["lunchin_hours"].ToString() + "'," +
                        " '" + dt["lunchin_minutes"].ToString() + "'," +
                        " '" + employee_gid + "'," +
                        "'" + employee_gid + "'," +
                        "'Y'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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

        // Assign Summary
        public void DaGetShiftUnAssignsumary(UnAssignsubmit_list values)
        {

            try
            {

                msSQL = " select b.employee_gid,a.user_code,a.user_firstname, " +
                        "  d.department_name,j.designation_name " +
                        " from adm_mst_tuser a inner join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        " inner join hrm_mst_tbranch c on b.branch_gid = c.branch_gid " +
                        " inner join hrm_mst_tdepartment d on b.department_gid = d.department_gid " +
                        " inner join hrm_trn_temployeetypedtl e on b.employee_gid=e.employee_gid " +
                        " left join hrm_mst_tsectionassign2employee f on f.employee_gid = b.employee_gid " +
                        " left join hrm_mst_tsection g on f.section_gid=g.section_gid " +
                        " left join hrm_mst_tblock h on f.block_gid=h.block_gid " +
                        " left join hrm_mst_tunit i on i.unit_gid=f.unit_gid " +
                        " inner join adm_mst_tdesignation j on j.designation_gid = b.designation_gid " +
                        " where  b.employee_gid  in " +
                        "(select a.employee_gid from hrm_trn_temployee2shifttype a left Join hrm_mst_temployee b on a.employee_gid = b.employee_gid  )" +
                        " and a.user_status='Y'  and e.employeetype_name='Roll'  order by a.user_firstname asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<UnAssign_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new UnAssign_list
                        {
                            employee_gid = dt["employee_gid"].ToString(),
                            user_code = dt["user_code"].ToString(),
                            user_firstname = dt["user_firstname"].ToString(),
                            department_name = dt["department_name"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                        });
                        values.UnAssign_list = getModuleList;
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

        public void DaShiftUnAssignSubmit(string employee_gid, UnAssignsubmit_list values)
        {
            try
            {
                foreach (var data in values.UnAssign_list)
                {
                    msSQL = " Delete from hrm_trn_temployee2shifttypedtl  where  employee_gid='" + data.employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = " Delete from hrm_trn_temployee2shifttype  where  employee_gid='" + data.employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Employee  unassign Successfully";
                    }
                    else
                    {
                        {
                            values.status = false;
                            values.message = "Error While Employee Unassign";
                        }
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

        public void DaShifttypeassign(MdlShiftType values ,string shifttype_gid)
        {

            try
            {

                msSQL = " select a.shifttype_gid,a.shifttype_name," +
                        " group_concat(c.branch_name) as branch_name,status from hrm_mst_tshifttype a " +
                        " left join hrm_mst_tshifttype2branch b on b.shifttype_gid = a.shifttype_gid " +
                        " left join hrm_mst_tbranch c on c.branch_gid = b.branch_gid " +
                        " where a.shifttype_gid='" + shifttype_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Assign_type>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Assign_type
                        {
                            shifttype_gid = dt["shifttype_gid"].ToString(),
                            shifttype_name = dt["shifttype_name"].ToString(),
                        });
                        values.Assign_type = getModuleList;
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
    
