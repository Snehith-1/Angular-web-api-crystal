using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.hrm.Models;
using MySql.Data.MySqlClient;

namespace ems.hrm.DataAccess
{
    public class DaLeaveopening
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string strClientIP = "";
        string msSQL = string.Empty;
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        DataTable dt_datatable2;
        string msEmployeeGID, msUserGid, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGETGid, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaGetLeaveopening(MdlOpeningleave values)
        {
            try
            {
                
                {
                    msSQL = " SELECT a.appointmentorder_gid, concat(a.first_name,' ',a.last_name) as user_name , a.gender, a.dob, a.mobile_number, a.email_address, a.qualification,a.employee_gid, " +
                            " a.experience_detail, a.perm_address_gid, a.temp_address_gid, a.template_gid, a.created_by, a.created_date, " +
                            " a.branch_name,a.designation_name,date_format(a.appointment_date, '%d-%m-%Y') as appointment_date " +
                            " FROM hrm_trn_tappointmentorder a " +
                            " left join hrm_trn_temployeetypedtl j on a.employee_gid=j.employee_gid ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getModuleList = new List<leaveopening_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getModuleList.Add(new leaveopening_list
                            {

                                appointmentorder_gid = dt["appointmentorder_gid"].ToString(),
                                user_name = dt["user_name"].ToString(),
                                designation_name = dt["designation_name"].ToString(),
                                branch_name = dt["branch_name"].ToString(),
                                appointment_date = dt["appointment_date"].ToString(),
                                gender = dt["gender"].ToString(),
                                dob = dt["dob"].ToString(),
                                mobile_number = dt["mobile_number"].ToString(),
                                email_address = dt["email_address"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                qualification = dt["qualification"].ToString(),
                            });
                            values.leaveopening_list = getModuleList;
                        }
                    }
                    dt_datatable.Dispose();
                }
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaGetleavegradeopeningdropdown(MdlOpeningleave values)
        {
            try
            {
                
                msSQL = " select leavegrade_gid,leavegrade_name " +
                    "from hrm_mst_tleavegrade ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<leave_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new leave_list
                        {
                            leavegrade_name = dt["leavegrade_name"].ToString(),
                            leavegrade_gid = dt["leavegrade_gid"].ToString(),
                        });
                        values.leave_list = getModuleList;
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

        public void Daeditleaveopening(MdlOpeningleave values, string leavegrade_gid)
        {
            try
            {
                
                msSQL = " select a.leavetype_gid,c.leavetype_name,a.total_leavecount,a.available_leavecount,a.leave_limit,b.leavegrade_gid from hrm_mst_tleavegradedtl a " +
               " left join hrm_mst_tleavegrade b on a.leavegrade_gid=b.leavegrade_gid " +
               " left join hrm_mst_tleavetype c on a.leavetype_gid=c.leavetype_gid " +
               " where a.leavegrade_gid='" + leavegrade_gid + "' and a.active_flag='Y'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<leaveopeningbalance_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new leaveopeningbalance_list
                        {
                            leavetype_name = dt["leavetype_name"].ToString(),
                            total_leavecount = dt["total_leavecount"].ToString(),
                            available_leavecount = dt["available_leavecount"].ToString(),
                            leave_limit = dt["leave_limit"].ToString(),
                            leavetype_gid = dt["leavetype_gid"].ToString(),
                            leavegrade_gid = dt["leavegrade_gid"].ToString(),

                        });
                        values.leaveopeningbalance_list = getModuleList;
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

        public void DaPostleavebalance(leavebalance_list values)
        {
            try
            {
                
                if (values.flag == "1")
                {
                    msSQL = "  Delete from hrm_trn_tleavegrade2employee where employee_gid = '" + values.employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (values.total_leavecount != "" && values.available_leavecount != "")
                {
                    msGETGid = objcmnfunctions.GetMasterGID("LE2G");
                    msSQL = "Insert Into hrm_trn_tleavegrade2employee" +
                          "(leavegrade2employee_gid," +
                          " branch_gid," +
                          " employee_gid," +
                          " employee_name," +
                          " leavegrade_gid," +
                          " leavegrade_code," +
                          " leavegrade_name," +
                          " attendance_startdate," +
                          " attendance_enddate," +
                          " total_leavecount," +
                          " available_leavecount," +
                          " leave_limit)" +

                          " VALUES ( " +
                          "'" + msGETGid + "', " +
                          "'" + values.branch_gid + "'," +
                          "'" + values.employee_gid + "'," +
                          "'" + values.employee_name + "'," +
                          "'" + values.leavegrade_gid + "'," +
                          "'" + values.leavegrade_code + "'," +
                          "'" + values.leavegrade_name + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                          "'" + values.total_leavecount + "'," +
                          "'" + values.available_leavecount + "'," +
                          "'" + values.leave_limit + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;

                        values.message = "Updated Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occur While Updating";
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