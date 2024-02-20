using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using ems.hrm.Models;
using System.Data.Odbc;

using ems.utilities.Functions;
using MySql.Data.MySqlClient;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.hrm.DataAccess
{
    public class DaApproveLeave
    {
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        DataTable table;
        string msSQL, msSAP, msSAD, msSRE, lsstatus;
        string lslop;
        string lsleavestatus;
        string lsdate, lsenddate;
        DateTime attendance_lsdate, attendance_lsenddate;
        string lsleavetype, lsemployee, lshalfday, lshalfsession, lstype, msGetGID;
        string lscarry, lsaccrual, leave_name;
        string lsstartdate, monthNumber, leave_year, lsemployee_gid, branch;
        string lsleavegrade_name, lsleavegrade_gid, lsleavetype_gid;
        int mnResult, MailFlag;
        Double lsavailable_leave, lsleave_available, lstotal_leave, ls_limit, leave_taken, lscarry_count;
        string lswhatsappflag, employee_mobile, approved_by, lsapprovedby;
        string employeename, reason, days, compofffromdate, actualworkingdate, lsmessage;
        string supportmail, pwd, employee_mailid, message, lblUserCode;
        string lblEmployeeGID, msPRSQL, lsPR_lists, mssql1, lssystem_type;
        string lslogin, lsshift_gid, attendance_date, lslogout, strClientIP;
        DateTime lsdate2, lsdate_compoff;
        TimeSpan time1, time2, lslunch, lstotal;
        string lstime, onduty_date, lsdocument_flag, fromdate, todate;
        bool blncommit = true;
        private bool False;
        private DateTime permission_date;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string lsemployeeper_gid;
        DateTime lspermission_date;
        TimeSpan lstotalpermissiontime;
        TimeSpan lspermissiontime;
        public bool DaGetApprovalCount(string employee_gid, string user_gid, approvalcountdetails objleavecountdetails)
        {
            try
            {                
                msSQL = " select a.count_leave,b.count_login,c.count_logout,d.count_onduty,e.count_compoff,f.count_permission, " +
                   " sum(a.count_leave + b.count_login + c.count_logout + d.count_onduty + e.count_compoff + f.count_permission) as total_approved " +
                   " from(select count(leave_status) as count_leave from hrm_trn_tleave where leave_status = 'Approved' and leave_approvedby = '" + employee_gid + "') as a, " +
                   " (select count(approval_gid) as count_login from hrm_trn_tloginapproval where approved_by = '" + employee_gid + "' and approval_flag = 'Y')  as b, " +
                   " (select count(approval_gid) as count_logout from hrm_trn_tlogoutapproval where approved_by = '" + employee_gid + "' and approval_flag = 'Y') as c, " +
                   " (select count(ondutytracker_gid) as count_onduty from hrm_trn_tondutytracker where onduty_approveby = '" + employee_gid + "' " +
                   " and ondutytracker_status = 'Approved')  as d, " +
                   " (select count(compensatoryoff_gid) as count_compoff from hrm_trn_tcompensatoryoff " +
                   " where compensatoryoff_approveby = '" + employee_gid + "' and compensatoryoff_status = 'Approved') as e, " +
                   " (select count(permissiondtl_gid) as count_permission from hrm_trn_tpermissiondtl where permission_approvedby = '" + employee_gid + "' " +
                   " and permission_status = 'Approved' ) as f group by count_leave desc";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objleavecountdetails.count_approval = objMySqlDataReader["total_approved"].ToString();
                    objleavecountdetails.approved_leave = objMySqlDataReader["count_leave"].ToString();
                    objleavecountdetails.approved_login = objMySqlDataReader["count_login"].ToString();
                    objleavecountdetails.approved_logout = objMySqlDataReader["count_logout"].ToString();
                    objleavecountdetails.approved_onduty = objMySqlDataReader["count_onduty"].ToString();
                    objleavecountdetails.approved_compoff = objMySqlDataReader["count_compoff"].ToString();
                    objleavecountdetails.approved_permission = objMySqlDataReader["count_permission"].ToString();
                }
                else
                {
                    objleavecountdetails.count_approval = "0";
                }
                objMySqlDataReader.Close();

                msSQL = " select a.count_leave,b.count_login,c.count_logout,d.count_onduty,e.count_compoff,f.count_permission, " +
                      " sum(a.count_leave + b.count_login + c.count_logout + d.count_onduty + e.count_compoff + f.count_permission) as total_rejected" +
                      " from(select count(leave_status) as count_leave from hrm_trn_tleave where leave_status = 'Rejected' and leave_approvedby = '" + employee_gid + "') as a, " +
                      " (select count(approval_gid) as count_login from hrm_trn_tloginapproval where approved_by = '" + employee_gid + "' and approval_flag = 'R')  as b, " +
                      " (select count(approval_gid) as count_logout from hrm_trn_tlogoutapproval where approved_by = '" + employee_gid + "' and approval_flag = 'R') as c, " +
                      " (select count(ondutytracker_gid) as count_onduty from hrm_trn_tondutytracker where onduty_approveby = '" + employee_gid + "' " +
                      " and ondutytracker_status = 'Rejected')  as d, " +
                      " (select count(compensatoryoff_gid) as count_compoff from hrm_trn_tcompensatoryoff " +
                      " where compensatoryoff_approveby = '" + employee_gid + "' and compensatoryoff_status = 'Rejected') as e, " +
                      " (select count(permissiondtl_gid) as count_permission from hrm_trn_tpermissiondtl where permission_approvedby = '" + employee_gid + "' " +
                      " and permission_status = 'Rejected' ) as f group by count_leave desc";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objleavecountdetails.count_rejected = objMySqlDataReader["total_rejected"].ToString();
                    objleavecountdetails.rejected_leave = objMySqlDataReader["count_leave"].ToString();
                    objleavecountdetails.rejected_login = objMySqlDataReader["count_login"].ToString();
                    objleavecountdetails.rejected_logout = objMySqlDataReader["count_logout"].ToString();
                    objleavecountdetails.rejected_onduty = objMySqlDataReader["count_onduty"].ToString();
                    objleavecountdetails.rejected_compoff = objMySqlDataReader["count_compoff"].ToString();
                    objleavecountdetails.rejected_permission = objMySqlDataReader["count_permission"].ToString();
                }
                else
                {
                    objleavecountdetails.count_rejected = "0";
                }
                objMySqlDataReader.Close();

                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " select a.count_leave,b.count_login,c.count_logout,d.count_onduty,e.count_compoff,f.count_permission, " +
                       " sum(a.count_leave + b.count_login + c.count_logout + d.count_onduty + e.count_compoff + f.count_permission) as total_pending " +
                       " from(select count(leave_status) as count_leave from hrm_trn_tleave " +
                       " where leave_status = 'Pending' and  employee_gid in (" + lblEmployeeGID + ")) as a, " +
                       " (select count(attendancelogintmp_gid) as count_login from hrm_tmp_tattendancelogin where employee_gid in (" + lblEmployeeGID + ") and status = 'Pending')  as b, " +
                       " (select count(attendancetmp_gid) as count_logout from hrm_tmp_tattendance where employee_gid in (" + lblEmployeeGID + ") and status = 'Pending') as c, " +
                       " (select count(ondutytracker_gid) as count_onduty from hrm_trn_tondutytracker where employee_gid in (" + lblEmployeeGID + ") and ondutytracker_status = 'Pending')  as d, " +
                       " (select count(compensatoryoff_gid) as count_compoff from hrm_trn_tcompensatoryoff " +
                       " where employee_gid in (" + lblEmployeeGID + ") and compensatoryoff_status = 'Pending') as e, " +
                       " (select count(permissiondtl_gid) as count_permission from hrm_trn_tpermissiondtl " +
                       " where employee_gid in (" + lblEmployeeGID + ") and permission_status = 'Pending' ) as f group by count_leave desc";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objleavecountdetails.count_approvalpending = objMySqlDataReader["total_pending"].ToString();
                    objleavecountdetails.pending_leave = objMySqlDataReader["count_leave"].ToString();
                    objleavecountdetails.pending_login = objMySqlDataReader["count_login"].ToString();
                    objleavecountdetails.pending_logout = objMySqlDataReader["count_logout"].ToString();
                    objleavecountdetails.pending_onduty = objMySqlDataReader["count_onduty"].ToString();
                    objleavecountdetails.pending_compoff = objMySqlDataReader["count_compoff"].ToString();
                    objleavecountdetails.pending_permission = objMySqlDataReader["count_permission"].ToString();
                }
                else
                {
                    objleavecountdetails.count_approvalpending = "0";
                }
                objMySqlDataReader.Close();

                objleavecountdetails.count_history = Convert.ToString(Convert.ToInt16(objleavecountdetails.count_approval) + Convert.ToInt16(objleavecountdetails.count_rejected));

                return true;
            }
            catch (Exception ex)
            {
                objleavecountdetails.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaGetApproveLeaveDetails(GetapproveLeavedetails values, string leave_gid)
        {
            try
            {                
                msSQL = " select a.leave_gid,a.leavetype_gid,date_format(a.leave_fromdate,'%d-%m-%Y') as leave_fromdate,date_format(a.leave_todate,'%d-%m-%Y') as leave_todate,a.leave_noofdays,b.leavetype_name, " +
                   " a.leave_reason,a.leave_status,concat(d.user_firstname,' ',d.user_lastname) as leave_appliedby " +
                   " from hrm_trn_tleave a " +
                   " left join hrm_mst_tleavetype b on a.leavetype_gid=b.leavetype_gid " +
                   " left join hrm_mst_temployee c on a.employee_gid=c.employee_gid " +
                   " left join adm_mst_tuser d on d.user_gid=c.user_gid " +
                   " where a.leave_gid='" + leave_gid + "'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    values.leave_gid = objMySqlDataReader["leave_gid"].ToString();
                    values.leave_fromdate = objMySqlDataReader["leave_fromdate"].ToString();
                    values.leave_todate = objMySqlDataReader["leave_todate"].ToString();
                    values.leave_status = objMySqlDataReader["leave_status"].ToString();
                    values.leave_noofdays = objMySqlDataReader["leave_noofdays"].ToString();
                    values.leavetype_name = objMySqlDataReader["leavetype_name"].ToString();
                    values.leave_reason = objMySqlDataReader["leave_reason"].ToString();
                    values.applied_by = objMySqlDataReader["leave_appliedby"].ToString();
                }

                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        //......................* 1. LEAVE APPROVAL Details *.................................//
        public bool DaGetLeaveApprovePendingDetails(getleavedetails values, string employee_gid)
        {
            try
            {                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                      " from adm_mst_tsubmodule a " +
                      " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                // Leave Approval Pending Summary...//

                msSQL = " select a.leave_gid,a.document_name,date_format(a.leave_fromdate,'%d-%m-%Y') as leave_fromdate,date_format(a.leave_todate,'%d-%m-%Y') as leave_todate,a.leave_noofdays,b.leavetype_name, " +
                     " a.leave_reason,a.leave_status,concat(d.user_firstname,' ',d.user_lastname) as leave_appliedby " +
                     " from hrm_trn_tleave a " +
                    " left join hrm_mst_tleavetype b on a.leavetype_gid=b.leavetype_gid" +
                  " left join hrm_mst_temployee c on c.employee_gid=a.employee_gid " +
                  " left join adm_mst_tuser d on d.user_gid=c.user_gid" +
                  " left join hrm_mst_tdepartment e on e.department_gid=c.department_gid" +
                  " left join hrm_trn_tattendance s on s.employee_gid=a.employee_gid " +
                  " left join hrm_trn_tleavedtl w on a.leave_gid = w.leave_gid" +
                  " where 1=1 and a.leave_status = 'Pending'";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (c.employee_gid in (" + lblEmployeeGID + ")";
                }

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";

                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (w.leavedtl_gid in (select y.leavedtl_gid from hrm_trn_tapproval x left join hrm_trn_tleavedtl y on x.leave_gid=y.leave_gid " +
                        " where  x.approval_flag = 'N' and x.approved_by = '" + employee_gid + "' )))";
                    }
                    else
                    {
                        objMySqlDataReader.Close();
                        msSQL += " and a.leave_gid in (select leaveapproval_gid from (select approved_by, " +
                             " leaveapproval_gid from hrm_trn_tapproval " +
                             " where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + employee_gid + "' group by  leaveapproval_gid order by" +
                             " approval_gid asc) p where approved_by = '" + employee_gid + "'))";
                        msPRSQL = " select distinct leaveapproval_gid from hrm_trn_tapproval where approval_flag = 'Y'";

                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows)
                        {
                            objMySqlDataReader.Close();
                            msPRSQL = " select distinct leaveapproval_gid from hrm_trn_tapproval where approval_flag = 'Y' " +
                                  " and approved_by in (" + lblEmployeeGID + ")";

                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows)
                            {
                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["leaveapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                            }
                            objMySqlDataReader.Close();

                            if (lsPR_lists != "")
                            {
                                msPRSQL = " select distinct a.leave_gid from hrm_trn_tleave a " +
                             " left join hrm_trn_tapproval b on a.leave_gid = b.leaveapproval_gid " +
                             " where a.leave_status = 'Pending' and a.leave_gid in (" + lsPR_lists + ")" +
                             " and b.approval_flag = 'N' and b.approved_by = '" + "employee_gid" + "'";
                                lsPR_lists = "";
                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                if (objMySqlDataReader.HasRows)
                                {
                                    while (objMySqlDataReader.Read())
                                    {
                                        lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["leave_gid"].ToString() + "',";
                                    }
                                    if (lsPR_lists != "")
                                    {
                                        lsPR_lists = lsPR_lists.TrimEnd(',');
                                        msPRSQL += " or a.leave_gid in (" + lsPR_lists + ")";
                                    }
                                }
                                objMySqlDataReader.Close();
                            }
                            objMySqlDataReader.Close();
                        }
                        objMySqlDataReader.Close();
                    }
                }
                objMySqlDataReader.Close();

                msSQL = msSQL + " and a.employee_gid<>'" + employee_gid + "' group by a.leave_gid order by date(a.created_date) desc,a.created_date asc,a.leave_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<applyleave_list>();
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
                        getleave.Add(new applyleave_list
                        {
                            leave_gid = (dr_datarow["leave_gid"].ToString()),
                            leavetype_name = (dr_datarow["leavetype_name"].ToString()),
                            leave_from = (dr_datarow["leave_fromdate"].ToString()),
                            leave_to = (dr_datarow["leave_todate"].ToString()),
                            noofdays_leave = (dr_datarow["leave_noofdays"].ToString()),
                            leave_reason = (dr_datarow["leave_reason"].ToString()),
                            approval_status = (dr_datarow["leave_status"].ToString()),
                            applied_by = (dr_datarow["leave_appliedby"].ToString()),
                            document_name = lsdocument_flag
                        });
                    }
                    values.applyleave_list = getleave;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        // Approved Summary...//
        public bool DaGetLeaveApproveDetails(getleavedetails values, string employee_gid)
        {
            try
            {                
                msSQL = " select a.leave_gid,a.leavetype_gid,a.document_name,date_format(a.leave_fromdate,'%d-%m-%Y') as leave_fromdate,date_format(a.leave_todate,'%d-%m-%Y') as leave_todate,a.leave_noofdays,b.leavetype_name, " +
                    " a.leave_reason,a.leave_status,concat(d.user_firstname,' ',d.user_lastname) as leave_appliedby " +
                    " from hrm_trn_tleave a " +
                    " left join hrm_mst_tleavetype b on a.leavetype_gid=b.leavetype_gid " +
                    " left join hrm_mst_temployee c on a.employee_gid=c.employee_gid " +
                    " left join adm_mst_tuser d on d.user_gid=c.user_gid " +
                    " where a.leave_approvedby='" + employee_gid + "' and a.leave_status='Approved' order by a.leave_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<applyleave_list>();
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
                        getleave.Add(new applyleave_list
                        {
                            leave_gid = (dr_datarow["leave_gid"].ToString()),
                            leavetype_name = (dr_datarow["leavetype_name"].ToString()),
                            leave_from = (dr_datarow["leave_fromdate"].ToString()),
                            leave_to = (dr_datarow["leave_todate"].ToString()),
                            noofdays_leave = (dr_datarow["leave_noofdays"].ToString()),
                            leave_reason = (dr_datarow["leave_reason"].ToString()),
                            approval_status = (dr_datarow["leave_status"].ToString()),
                            applied_by = (dr_datarow["leave_appliedby"].ToString()),
                            document_name = lsdocument_flag
                        });
                    }
                    values.applyleave_list = getleave;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        // Rejected Summary....//
        public bool DaGetLeaveRejectDetails(getleavedetails values, string employee_gid)
        {
            try
            {                
                msSQL = " select a.leave_gid,a.leavetype_gid,a.document_name,date_format(a.leave_fromdate,'%d-%m-%Y') as leave_fromdate,date_format(a.leave_todate,'%d-%m-%Y') as leave_todate,a.leave_noofdays,b.leavetype_name, " +
                   " a.leave_reason,a.leave_status,concat(d.user_firstname,' ',d.user_lastname) as leave_appliedby " +
                   " from hrm_trn_tleave a " +
                   " left join hrm_mst_tleavetype b on a.leavetype_gid=b.leavetype_gid " +
                   " left join hrm_mst_temployee c on a.employee_gid=c.employee_gid " +
                   " left join adm_mst_tuser d on d.user_gid=c.user_gid " +
                   " where a.leave_approvedby='" + employee_gid + "' and a.leave_status='Rejected' order by a.leave_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<applyleave_list>();
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
                        getleave.Add(new applyleave_list
                        {
                            leave_gid = (dr_datarow["leave_gid"].ToString()),
                            leavetype_name = (dr_datarow["leavetype_name"].ToString()),
                            leave_from = (dr_datarow["leave_fromdate"].ToString()),
                            leave_to = (dr_datarow["leave_todate"].ToString()),
                            noofdays_leave = (dr_datarow["leave_noofdays"].ToString()),
                            leave_reason = (dr_datarow["leave_reason"].ToString()),
                            approval_status = (dr_datarow["leave_status"].ToString()),
                            applied_by = (dr_datarow["leave_appliedby"].ToString()),
                            document_name = lsdocument_flag
                        });
                    }
                    values.applyleave_list = getleave;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        // Approve Leave Click....//
        public bool DaPostApproveLeave(string employee_gid, string user_gid, approveleavedetails values)
        {
            try
            {                
                lsleavestatus = "Approved";

                msSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    if ((objMySqlDataReader["approval_type"].ToString()) == "Parallel")
                    {
                        objMySqlDataReader.Close();
                        msSQL = " update hrm_trn_tapproval set " +
                                " approval_flag = 'N/A', " +
                                " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                " where approved_by <> '" + employee_gid + "'" +
                                " and leaveapproval_gid = '" + values.leave_gid + "' and submodule_gid='HRMLEVARL'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = " update hrm_trn_tapproval set " +
                               " approval_flag = 'Y', " +
                               " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                               " where approved_by = '" + employee_gid + "'" +
                               " and leaveapproval_gid = '" + values.leave_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        objMySqlDataReader.Close();
                        msSQL = " update hrm_trn_tapproval set " +
                            " approval_flag = 'Y', " +
                            " seqhierarchy_view='N', " +
                            " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                            " where approved_by = '" + employee_gid + "'" +
                            " and leaveapproval_gid = '" + values.leave_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update hrm_trn_tapproval set " +
                          " seqhierarchy_view='Y' " +
                          " where approval_flag='N'and approved_by <> '" + employee_gid + "' and leaveapproval_gid = '" + values.leave_gid + "'" +
                          " order by hierary_level desc limit 1 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                //Approve

                msSQL = " select approval_gid from hrm_trn_tapproval where leaveapproval_gid='" + values.leave_gid + "'" +
                        " and approval_flag = 'N' and submodule_gid='HRMLEVARL'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == false)
                {
                    objMySqlDataReader.Close();
                    msSQL = " update hrm_trn_tleavedtl set " +
                            " leave_status = '" + lsleavestatus + "'," +
                            " updated_by = '" + employee_gid + "'," +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                            " where leave_gid = '" + values.leave_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        objMySqlDataReader.Close();
                        msSQL = " update hrm_trn_tleave set " +
                          " leave_status = '" + lsleavestatus + "'," +
                          " leave_approvedby = '" + employee_gid + "'," +
                          " leave_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                          " approval_remarks = '" + values.approval_remarks + "'," +
                          " updated_by = '" + employee_gid + "'," +
                          " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                          " where leave_gid = '" + values.leave_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = " select leave_fromdate, date_format(leave_fromdate, '%Y') as lsyear,employee_gid," +
                                    " date_format(leave_fromdate, '%m') as month  from hrm_trn_tleave where leave_gid='" + values.leave_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            
                            if (objMySqlDataReader.HasRows == true)
                            {
                                lsstartdate = objMySqlDataReader["leave_fromdate"].ToString();
                                lsenddate = objMySqlDataReader["leave_fromdate"].ToString();
                                monthNumber = objMySqlDataReader["month"].ToString();
                                leave_year = objMySqlDataReader["lsyear"].ToString();
                                lsemployee_gid = objMySqlDataReader["employee_gid"].ToString();
                            }
                            objMySqlDataReader.Close();
                            msSQL = " Select branch_gid from hrm_mst_temployee where employee_gid='" + lsemployee_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            
                            if (objMySqlDataReader.HasRows == true)
                            {

                                branch = objMySqlDataReader["branch_gid"].ToString();
                            }
                            objMySqlDataReader.Close();
                            lslop = Getleave(lsemployee_gid, branch, lsstartdate, lsenddate, monthNumber, leave_year);

                        }
                        //-----------------------------half day LOP Calculation----------------------

                        msSQL = " update hrm_trn_tattendance set " +
                                " halfdayabsent_flag='Y'," +
                                " halfdayleave_flag ='Y'" +
                                " where day_session <> 'NA' and login_time is null and logout_time is null and employee_attendance='Leave'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        //-----------------------------end half day LOP Calculation----------------------

                        msSQL = " Select date_format(x.leavedtl_date,'%Y-%m-%d') as leavedate,y.employee_gid from hrm_trn_tleavedtl x " +
                                   " left join hrm_trn_tleave y on x.leave_gid=y.leave_gid " +
                                      " where x.leave_gid = '" + values.leave_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow objemployeedatarow in dt_datatable.Rows)
                            {
                                lsdate = objemployeedatarow["leavedate"].ToString();
                                lsemployee = objemployeedatarow["employee_gid"].ToString();
                                msSQL = " Select y.employee_gid,z.leavetype_code,x.half_day,x.half_session from hrm_trn_tleavedtl x " +
                                " left join hrm_trn_tleave y on x.leave_gid=y.leave_gid " +
                                " left join hrm_mst_tleavetype z on y.leavetype_gid=z.leavetype_gid " +
                                 " where x.leave_gid = '" + values.leave_gid + "' and x.leavedtl_date='" + lsdate + "' ";
                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                
                                if (objMySqlDataReader.HasRows == true)
                                {
                                    lsleavetype = objMySqlDataReader["leavetype_code"].ToString();
                                    lshalfday = objMySqlDataReader["half_day"].ToString();
                                    lshalfsession = objMySqlDataReader["half_session"].ToString();
                                    lstype = lsleavetype;
                                }
                                objMySqlDataReader.Close();
                                msSQL = " Select employee_gid from hrm_trn_tattendance " +
                                    "where attendance_date='" + lsdate + "' and employee_gid='" + lsemployee + "'";
                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                
                                if (objMySqlDataReader.HasRows == true)
                                {
                                    objMySqlDataReader.Close();
                                    msSQL = " update hrm_trn_tattendance set " +
                                            " employee_attendance='Leave', " +
                                            " attendance_type='" + lstype + "', " +
                                            " day_session='" + lshalfsession + "', " +
                                            " update_flag='N'" +
                                            " where attendance_date='" + lsdate + "' and employee_gid='" + lsemployee + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                }
                                else
                                {
                                    objMySqlDataReader.Close();
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
                                objMySqlDataReader.Close();
                            }
                        }
                        msSQL = " Select a.leavetype_gid,b.leavedtl_gid from hrm_trn_tleave a" +
                                " left join hrm_trn_tleavedtl b on a.leave_gid=b.leave_gid" +
                                " where a.leave_gid = '" + values.leave_gid + "'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        
                        if (objMySqlDataReader.HasRows == true)
                        {

                            msSQL = " Select compensatoryoffdtl_gid from hrm_trn_tcompensatoryoffdtl" +
                                   " where leave_status='Leave Applied' and" +
                                   " leavedtl_gid='" + objMySqlDataReader["leavedtl_gid"] + "'";
                            objMySqlDataReader.Close();
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            
                            if (objMySqlDataReader.HasRows == true)
                            {

                                msSQL = " update hrm_trn_tcompensatoryoffdtl set leave_status='Approved'" +
                                            " where compensatoryoffdtl_gid='" + objMySqlDataReader["compensatoryoffdtl_gid"] + "'";
                                objMySqlDataReader.Close();
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                        }
                        objMySqlDataReader.Close();
                    }
                }
                //.............Mail Functions.................//

                msSQL = " Select approved_by from hrm_trn_tapproval " +
                        " where approved_by='" + employee_gid + "' and leaveapproval_gid='" + values.leave_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {

                    lsapprovedby = objMySqlDataReader["approved_by"].ToString();
                    objMySqlDataReader.Close();
                    msSQL = " select pop_username,pop_password from adm_mst_tcompany where company_gid='1'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        supportmail = objMySqlDataReader["pop_username"].ToString();
                        pwd = objMySqlDataReader["pop_password"].ToString();
                    }
                    objMySqlDataReader.Close();
                    if (supportmail != "")
                    {
                        msSQL = "select b.employee_emailid,(date_format(a.leave_fromdate,'%d/%m/%y')) as fromdate, " +
                                " (date_format(a.leave_todate,'%d/%m/%y')) as todate,a.leave_remarks, " +
                                " Concat(c.user_firstname,' ',c.user_lastname) as username, " +
                               " a.leave_noofdays,a.leave_reason  from hrm_trn_tleave a " +
                               " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                            " where a.leave_gid='" + values.leave_gid + "'";
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
                        objMySqlDataReader.Close();
                        msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                                       " from hrm_mst_temployee a " +
                                     " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                       " where a.employee_gid='" + lsapprovedby + "'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            approved_by = objMySqlDataReader["username"].ToString();
                        }
                        objMySqlDataReader.Close();

                        message = "Hi " + employeename + ",  <br />";
                        message = message + "<br />";
                        message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsleavestatus + " your leave<br />";
                        message = message + "<br />";
                        message = message + "<b>Reason :</b> " + reason + "<br />";
                        message = message + "<br />";
                        message = message + "<b>From Date :</b> " + fromdate + " &nbsp; &nbsp; <b>To Date :</b> " + todate + "<br />";
                        message = message + "<br />";
                        message = message + "<b>Total No of Days :</b> " + days + " <br />";
                        message = message + "<br />";
                        try
                        {
                            MailFlag = objcmnfunctions.SendSMTP2(supportmail, pwd, employee_mailid, "Your leave has been " + lsleavestatus + " ", message, "", "", "");

                        }
                        catch
                        {
                            values.message = "Mail Not Send";
                        }
                    }
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "leave Approved Successfully...!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured...!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        // Reject Leave Click....//
        public bool DaPostRejectLeave(applyleavedetails values, string employee_gid, string user_gid)
        {
            try
            {                
                bool MailFlag;
                DataTable objTblemployee;

                lsleavestatus = "Rejected";
                msSQL = " update hrm_trn_tapproval set " +
                        " approval_flag = 'R', " +
                        " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                        " where approved_by = '" + employee_gid + "'" +
                        " and leaveapproval_gid = '" + values.leave_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update hrm_trn_tleave set " +
                           " leave_status = '" + lsleavestatus + "'," +
                           " leave_approvedby = '" + employee_gid + "'," +
                           " leave_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                           " updated_by = '" + employee_gid + "'," +
                           " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                           " where leave_gid = '" + values.leave_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " update hrm_trn_tleavedtl set " +
                                " leave_status = '" + lsleavestatus + "'," +
                                " updated_by = '" + employee_gid + "'," +
                                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                " where leave_gid = '" + values.leave_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


                msSQL = " Select a.leavetype_gid,b.leavedtl_gid from hrm_trn_tleave a" +
                        " left join hrm_trn_tleavedtl b on a.leave_gid=b.leave_gid" +
                        " where a.leavetype_gid='LT1203060069' and a.leave_gid = '" + values.leave_gid + "'";
                objTblemployee = objdbconn.GetDataTable(msSQL);
                if (objTblemployee.Rows.Count != 0)
                {
                    foreach (DataRow dt in objTblemployee.Rows)
                    {
                        msSQL = " Select compensatoryoffdtl_gid from hrm_trn_tcompensatoryoffdtl" +
                                   " where leave_status='Leave Applied' and" +
                                   " leavedtl_gid = '" + dt["leavedtl_gid"] + "'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msSQL = " update hrm_trn_tcompensatoryoffdtl set leave_status='not taken'," +
                                       " leave_date=null,leavedtl_gid=''" +
                                       " where compensatoryoffdtl_gid='" + objMySqlDataReader["compensatoryoffdtl_gid"] + "'";
                            objMySqlDataReader.Close();
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        objMySqlDataReader.Close();
                    }
                }
                objTblemployee.Dispose();

                //---------------------------------------------Whatsapp flag--------------------------------//

                msSQL = "select whatsapp_flag from adm_mst_tcompany";
                lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);
                if (lswhatsappflag == "Y")
                {
                    msSQL = "select b.employee_mobileno,(date_format(a.leave_fromdate,'%d/%m/%y')) as fromdate, " +
                                "(date_format(a.leave_todate,'%d/%m/%y')) as todate,a.leave_remarks, " +
                                "Concat(c.user_firstname,' ',c.user_lastname) as username, " +
                                " a.leave_noofdays,a.leave_reason  from hrm_trn_tleave a " +
                             " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                " where a.leave_gid='" + values.leave_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mobile = objMySqlDataReader["employee_mobileno"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        reason = objMySqlDataReader["leave_reason"].ToString();
                        days = objMySqlDataReader["leave_noofdays"].ToString();
                        fromdate = objMySqlDataReader["fromdate"].ToString();
                        todate = objMySqlDataReader["todate"].ToString();
                    }
                    objMySqlDataReader.Close();

                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username from hrm_mst_temployee a " +
                            " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                            " where a.employee_gid='" + employee_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();
                    try
                    {
                        lsmessage = "Hi " + employeename + ",   " + approved_by + "  had " + lsleavestatus + " your leave  from " + fromdate + "  to " + todate + " for about the " + days + " days ";

                        objcmnfunctions.sendMessage(employee_mobile, lsmessage);
                    }
                    catch (Exception ex)
                    {
                        values.message = "Leave Rejected Successfully,but Message Not Send.";
                    }

                }

                msSQL = "Select approved_by from hrm_trn_tapproval " +
                        "where approved_by='" + employee_gid + "' and leaveapproval_gid='" + values.leave_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lsapprovedby = objMySqlDataReader["approved_by"].ToString();
                    objMySqlDataReader.Close();
                    msSQL = "select b.employee_emailid,(date_format(a.leave_fromdate,'%d/%m/%y')) as fromdate, " +
                                    "(date_format(a.leave_todate,'%d/%m/%y')) as todate,a.leave_remarks, " +
                                    "Concat(c.user_firstname,' ',c.user_lastname) as username, " +
                                    " a.leave_noofdays,a.leave_reason  from hrm_trn_tleave a " +
                                 " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                        " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                    " where a.leave_gid='" + values.leave_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        reason = objMySqlDataReader["leave_remarks"].ToString();
                        days = objMySqlDataReader["leave_noofdays"].ToString();
                        fromdate = objMySqlDataReader["fromdate"].ToString();
                        todate = objMySqlDataReader["todate"].ToString();
                    }
                    objMySqlDataReader.Close();

                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                            " from hrm_mst_temployee a " +
                            " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                            " where a.employee_gid='" + lsapprovedby + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();

                    message = "Hi " + employeename + ",  <br />";
                    message = message + "<br />";
                    message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsleavestatus + " your leave <br />";
                    message = message + "<br />";
                    message = message + "<b>Reason :</b> " + reason + "<br />";
                    message = message + "<br />";
                    message = message + "<b>From Date :</b> " + fromdate + " &nbsp; &nbsp; <b>To Date :</b> " + todate + "<br />";
                    message = message + "<br />";
                    message = message + "<b>No of Days :</b> " + days + " <br />";
                    message = message + "<br />";
                    try
                    {
                        MailFlag = objcmnfunctions.mail(employee_mailid, "Your leave has been " + lsleavestatus + " ", message);
                    }
                    catch (Exception ex)
                    {
                        values.message = "Mail Not Send.";
                    }
                }
                if (mnResult != 0)
                {
                    values.message = "Leave Rejected Successfully !";
                    return true;
                }
                else
                {
                    values.message = "Error Occured !";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        //..............................* 2. LOGIN APPROVAL Details *..............................//
        public bool DaGetLoginApproval(getlogindetails values, string employee_gid)
        {
            try
            {                
                //Login Approval Pending Summary.....//
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                        " from adm_mst_tsubmodule a " +
                        " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                msSQL = " SELECT distinct k.attendancelogintmp_gid,k.employee_gid,k.status,date_format(k.attendance_date, '%d-%m-%Y') as attendence_date, " +
                        " time_format(k.login_time, '%H:%i %p') as login_time, " +
                        " date_format(k.created_date, '%d-%m-%Y') as applydate , k.remarks, concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, " +
                        "  '/', m.department_name) as employeename FROM hrm_tmp_tattendancelogin k " +
                        " left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                        " left join hrm_mst_tdepartment m on m.department_gid=c.department_gid" +
                        " left join adm_mst_tdesignation e on c.designation_gid = e.designation_gid " +
                        " left join adm_mst_tuser n on n.user_gid = c.user_gid   " +
                        " where 1 = 1 ";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (k.employee_gid in (" + lblEmployeeGID + ")";
                }

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNLTA'";
                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    //-------------------parallel flow starts here------------------------------//
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or k.attendancelogintmp_gid in (select loginapproval_gid from hrm_trn_tloginapproval " +
                                 " where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  loginapproval_gid))";

                        //----parallel flow ends here---------------------------------//
                    }
                    else
                    {
                        objMySqlDataReader.Close();
                        msSQL += " and k.attendancelogintmp_gid in (select loginapproval_gid from (select approved_by, loginapproval_gid from hrm_trn_tloginapproval " +
                                 " where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + employee_gid + "' group by  loginapproval_gid order by loginapproval_gid asc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y'";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            objMySqlDataReader.Close();
                            msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y' " +
                                      " and approved_by in (" + lblEmployeeGID + ")";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["loginapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                            }
                            else
                            {
                                lsPR_lists = "";
                            }
                            objMySqlDataReader.Close();
                            if (lsPR_lists != "")
                            {
                                msPRSQL = " select distinct a.attendancelogintmp_gid from hrm_tmp_tattendancelogin a " +
                             " left join hrm_trn_tloginapproval b on a.attendancelogintmp_gid = b.loginapproval_gid " +
                             " where a.status = 'Pending' and a.attendancelogintmp_gid in (" + lsPR_lists + ")" +
                             " and b.approval_flag = 'N' and b.approved_by = '" + employee_gid + "'";
                                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                                lsPR_lists = "";
                                if (objMySqlDataReader.HasRows == true)
                                {
                                    while (objMySqlDataReader.Read())
                                    {
                                        lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["attendancelogintmp_gid"].ToString() + "',";
                                    }
                                    if (lsPR_lists != "")
                                    {
                                        lsPR_lists = lsPR_lists.TrimEnd(',');
                                        msSQL += " or k.attendancelogintmp_gid in (" + lsPR_lists + ")";
                                    }

                                }
                                objMySqlDataReader.Close();
                            }
                        }
                    }
                }
                msSAP = msSQL + " and k.employee_gid <>'" + employee_gid + "' and k.status = 'Pending' group by k.attendancelogintmp_gid " +
                                " order by date(k.created_date)desc,k.created_date asc,attendancelogintmp_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSAP);
                var getloginpending = new List<loginpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginpending.Add(new loginpending_list
                        {
                            attendancelogintmp_gid = (dr_datarow["attendancelogintmp_gid"].ToString()),
                            loginapply_date = (dr_datarow["applydate"].ToString()),
                            employee_name = (dr_datarow["employeename"].ToString()),
                            loginattendence_date = (dr_datarow["attendence_date"].ToString()),
                            login_time = (dr_datarow["login_time"].ToString()),
                            login_reason = (dr_datarow["remarks"].ToString()),
                            login_status = (dr_datarow["status"].ToString()),
                            apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                        });
                    }
                    values.loginpending_list = getloginpending;
                }
                dt_datatable.Dispose();

                msSRE = msSQL + " and k.employee_gid <>'" + employee_gid + "' and k.status = 'Rejected' group by k.attendancelogintmp_gid " +
                    " order by date(k.created_date)desc,k.created_date asc,attendancelogintmp_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSRE);
                var getloginrejected = new List<loginrejected_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginrejected.Add(new loginrejected_list
                        {
                            attendancelogintmp_gid = (dr_datarow["attendancelogintmp_gid"].ToString()),
                            loginapply_date = (dr_datarow["applydate"].ToString()),
                            employee_name = (dr_datarow["employeename"].ToString()),
                            loginattendence_date = (dr_datarow["attendence_date"].ToString()),
                            login_time = (dr_datarow["login_time"].ToString()),
                            login_reason = (dr_datarow["remarks"].ToString()),
                            login_status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.loginrejected_list = getloginrejected;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool getloginleaveapprovedetails(getleavesummarydetails values, string employee_gid)
        {
            try
            {                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                        " from adm_mst_tsubmodule a " +
                        " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRMATNLTA'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                // login Approved Summary...//
                msSQL = "select  k.attendancelogintmp_gid,concat(n.user_firstname,n.user_lastname)as employee_gid ,k.status,K.attendance_date,k.login_time,k.remarks,k.created_date " +
                        "from  hrm_tmp_tattendancelogin k " +
                        "left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                        "left join hrm_trn_tleavedtl w on k.status = w.leave_status " +
                        "left join adm_mst_tuser n on n.user_gid = c.user_gid " +
                        " where  1 = 1 and k.status='Approved'";

                msSQL += " and (c.employee_gid in (" + lblEmployeeGID + ")";

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNLTA'";

                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    // -------------------parallel flow starts here------------------------------
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                        msSQL += " or k.attendancelogintmp_gid in (select loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  loginapproval_gid))";
                    else
                    {
                        msSQL += " or k.attendancelogintmp_gid in (select loginapproval_gid from (select approved_by, loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'N' group by  loginapproval_gid order by loginapproval_gid asc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y'";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y' and approved_by = '" + employee_gid + " '";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {

                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["loginapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                            }
                            objMySqlDataReader.Close();
                            if (lsPR_lists != "")
                            {
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                                msSQL += " or k.attendancelogintmp_gid in (" + lsPR_lists + ")";
                            }
                        }
                    }
                }
                objMySqlDataReader.Close();

                msSQL = msSQL + " and k.employee_gid <>'" + employee_gid + "' and k.status <> 'Pending' group by k.attendancelogintmp_gid order by date(k.created_date)desc,k.created_date asc,attendancelogintmp_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<login_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleave.Add(new login_list
                        {
                            loginapply_date = (dr_datarow["created_date"].ToString()),
                            loginattendence_date = (dr_datarow["attendance_date"].ToString()),
                            login_time = (dr_datarow["login_time"].ToString()),
                            login_reason = (dr_datarow["remarks"].ToString()),
                            login_status = (dr_datarow["status"].ToString()),
                            employee_name = (dr_datarow["employee_gid"].ToString()),

                        });
                    }
                    values.login_list = getleave;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaGetLoginLeaveRejectDetails(getleavesummarydetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                        " from adm_mst_tsubmodule a " +
                        " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRMATNLTA'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                // login Rejected Summary...//
                msSQL = "select  k.attendancelogintmp_gid,concat(n.user_firstname,n.user_lastname)as employee_gid ,k.status,K.attendance_date,k.login_time,k.remarks,k.created_date " +
                        "from  hrm_tmp_tattendancelogin k " +
                        "left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                        "left join hrm_trn_tleavedtl w on k.status = w.leave_status " +
                        "left join adm_mst_tuser n on n.user_gid = c.user_gid " +
                        " where  1 = 1 and k.status='Rejected'";

                msSQL += " and (c.employee_gid in (" + lblEmployeeGID + ")";

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNLTA'";

                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    // -------------------parallel flow starts here------------------------------
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                        msSQL += " or k.attendancelogintmp_gid in (select loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  loginapproval_gid))";
                    else
                    {
                        msSQL += " or k.attendancelogintmp_gid in (select loginapproval_gid from (select approved_by, loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'N' group by  loginapproval_gid order by loginapproval_gid asc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y'";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y' and approved_by = '" + employee_gid + " '";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {

                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["loginapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                            }
                            objMySqlDataReader.Close();
                            if (lsPR_lists != "")
                            {
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                                msSQL += " or k.attendancelogintmp_gid in (" + lsPR_lists + ")";
                            }
                        }
                    }
                }
                objMySqlDataReader.Close();

                msSQL = msSQL + " and k.employee_gid <>'" + employee_gid + "' and k.status = 'Rejected' group by k.attendancelogintmp_gid order by date(k.created_date)desc,k.created_date asc,attendancelogintmp_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<login_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleave.Add(new login_list
                        {
                            loginapply_date = (dr_datarow["created_date"].ToString()),
                            loginattendence_date = (dr_datarow["attendance_date"].ToString()),
                            login_time = (dr_datarow["login_time"].ToString()),
                            login_reason = (dr_datarow["remarks"].ToString()),
                            login_status = (dr_datarow["status"].ToString()),
                            employee_name = (dr_datarow["employee_gid"].ToString()),

                        });
                    }
                    values.login_list = getleave;
                }
                dt_datatable.Dispose();

                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPostApproveLogin(approvelogin values, string employee_gid)
        {
            try
            {
                
                lsleavestatus = "Approved";
                lsdate2 = DateTime.ParseExact(values.loginattendence_date.Replace("-", ""), "ddMMyyyy", null);
                msSQL = "select approval_type from adm_mst_tmodule where module_gid = 'HRMATNLTA'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {

                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                    {
                        objMySqlDataReader.Close();
                        msSQL = " update hrm_trn_tloginapproval set " +
                                      " approval_flag = 'N/A', " +
                                      " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                                      " where approved_by <> '" + employee_gid + "' " +
                                      " and loginapproval_gid = '" + values.attendancelogintmp_gid + "' and  submodule_gid='HRMATNLTA'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update hrm_trn_tloginapproval set " +
                                    " approval_flag = 'Y', " +
                                    " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                                    " where approved_by = '" + employee_gid + "' " +
                                    " and loginapproval_gid = '" + values.attendancelogintmp_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        objMySqlDataReader.Close();
                        msSQL = " update hrm_trn_tloginapproval set " +
                                      " approval_flag = 'Y', " +
                                      " seqhierarchy_view='N', " +
                                      " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                      " where approved_by = '" + employee_gid + "'" +
                                      " and loginapproval_gid = '" + values.attendancelogintmp_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update hrm_trn_tloginapproval set " +
                            " seqhierarchy_view='Y' " +
                            " where approval_flag='N'and approved_by <> '" + employee_gid + "' and loginapproval_gid = '" + values.attendancelogintmp_gid + "'" +
                            " order by hierary_level desc limit 1 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objMySqlDataReader.Close();

                msSQL = " select approval_gid from hrm_trn_tloginapproval where loginapproval_gid='" + values.attendancelogintmp_gid + "' " +
                        " and approval_flag = 'N' and submodule_gid='HRMATNLTA'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == false)
                {
                    objMySqlDataReader.Close();
                    msSQL = " update hrm_tmp_tattendancelogin set " +
                            " status = '" + lsleavestatus + "'," +
                            " approved_by = '" + employee_gid + "'," +
                            " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            " updated_by = '" + employee_gid + "'," +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                            " where attendancelogintmp_gid = '" + values.attendancelogintmp_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {

                        lsdate2 = DateTime.ParseExact(values.loginattendence_date.Replace("-", ""), "ddMMyyyy", null);
                        msSQL = "Select date_format(login_time,'%Y-%m-%d %H:%i:%s') as login_time from hrm_tmp_tattendancelogin " +
                                "where attendance_date='" + lsdate2.ToString("yyyy-MM-dd") + "' and employee_gid='" + values.apply_employeegid + "'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            lslogin = objMySqlDataReader["login_time"].ToString();
                        }
                        objMySqlDataReader.Close();
                        msSQL = " select attendance_gid from hrm_trn_tattendance " +
                                " where employee_gid='" + values.apply_employeegid + "' and attendance_date='" + lsdate2.ToString("yyyy-MM-dd") + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count > 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                msSQL = " update hrm_trn_tattendance set" +
                                    " login_time='" + lslogin + "'," +
                                    " login_time_audit='" + lslogin + "'," +
                                    " attendance_source='Requisition'," +
                                    " approved_by='" + employee_gid + "'," +
                                    " employee_attendance='Present'," +
                                    " attendance_type='P'," +
                                    " update_flag='N' " +
                                    " where employee_gid='" + values.apply_employeegid + "' and" +
                                    " attendance_date='" + lsdate2.ToString("yyyy-MM-dd") + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            dt_datatable.Dispose();
                        }
                        else
                        {

                            msSQL = " select employee2shifttypedtl_gid from hrm_trn_temployee2shifttypedtl where employee_gid='" + lsemployee_gid + "' " +
                                  " and employee2shifttype_name='" + (lsdate2.DayOfWeek.ToString()).ToLower() + "' and shift_status='Y' ";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                lsshift_gid = objMySqlDataReader["employee2shifttypedtl_gid"].ToString();
                            }
                            objMySqlDataReader.Close();
                            msGetGID = objcmnfunctions.GetMasterGID("HATP");
                            msSQL = "Insert Into hrm_trn_tattendance" +
                                    "(attendance_gid," +
                                    " employee_gid," +
                                    " attendance_date," +
                                    " attendance_source," +
                                    " login_time," +
                                    " login_time_audit, " +
                                    " employee_attendance," +
                                    " shifttype_gid," +
                                    " attendance_type)" +
                                    " VALUES ( " +
                                    "'" + msGetGID + "', " +
                                    "'" + lsemployee_gid + "'," +
                                    "'" + lsdate2.ToString("yyyy-MM-dd") + "'," +
                                    "'Requisition'," +
                                    "'" + lslogin + "'," +
                                    "'" + lslogin + "'," +
                                    "'Present'," +
                                    "'" + lsshift_gid + "'," +
                                    "'P')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
                objMySqlDataReader.Close();

                //-------------------------------------------whatsapp----------------------------//
                msSQL = "select whatsapp_flag from adm_mst_tcompany";
                lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);

                if (lswhatsappflag == "Y")
                {
                    msSQL = " select b.employee_mobileno," +
                                 " date_format(a.login_time,'%d-%M-%Y %r ')as login_time," +
                                 " concat(c.user_firstname,' ',c.user_lastname) as username," +
                                 " date_format(a.attendance_date,'%d-%m-%Y') as attendance_date from hrm_tmp_tattendancelogin a" +
                                 " left join hrm_trn_tloginapproval d on a.attendancelogintmp_gid=d.loginapproval_gid" +
                                  " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                             " where a.attendancelogintmp_gid='" + values.attendancelogintmp_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mobile = objMySqlDataReader["employee_mobileno"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        fromdate = objMySqlDataReader["login_time"].ToString();
                        attendance_date = objMySqlDataReader["attendance_date"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                            " from hrm_mst_temployee a " +
                           " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                            " where a.employee_gid='" + employee_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();
                    try
                    {
                        lsmessage = "Hi " + employeename + ",   " + approved_by + "  had " + lsleavestatus + " your Login Time Requisition on   " + attendance_date + " from " + fromdate + "  ";
                        objcmnfunctions.sendMessage(employee_mobile, lsmessage);
                        values.message = "Login Time Approved Successfully";
                    }
                    catch
                    {
                        values.message = "Login Time Approved Successfully,but Message Not Send. ";
                    }
                }
                // Mail Flag.....//

                msSQL = " Select approved_by from hrm_trn_tloginapproval " +
                        " where approved_by='" + employee_gid + "' and loginapproval_gid='" + values.attendancelogintmp_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lsapprovedby = objMySqlDataReader["approved_by"].ToString();

                    objMySqlDataReader.Close();
                    msSQL = " select pop_username,pop_password from adm_mst_tcompany where company_gid='1'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        supportmail = objMySqlDataReader["pop_username"].ToString();
                        pwd = objMySqlDataReader["pop_password"].ToString();
                    }
                    objMySqlDataReader.Close();

                    msSQL = " select b.employee_emailid," +
                             " date_format(a.login_time,'%d-%M-%Y %r ')as login_time," +
                             " concat(c.user_firstname,' ',c.user_lastname) as username," +
                             " date_format(a.attendance_date,'%d-%m-%Y') as attendance_date from hrm_tmp_tattendancelogin a" +
                             " left join hrm_trn_tloginapproval d on a.attendancelogintmp_gid=d.loginapproval_gid" +
                              " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                            " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                         " where a.attendancelogintmp_gid='" + values.attendancelogintmp_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        fromdate = objMySqlDataReader["login_time"].ToString();
                        attendance_date = objMySqlDataReader["attendance_date"].ToString();
                    }

                    objMySqlDataReader.Close();

                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                               " from hrm_mst_temployee a " +
                             " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                               " where a.employee_gid='" + lsapprovedby + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();

                    message = "Hi " + employeename + ",  <br />";
                    message = message + "<br />";
                    message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsleavestatus + " your Login Time Requisition on " + attendance_date + "<br />";
                    message = message + "<br />";
                    message = message + "<b>Login Time Requisition :</b> " + fromdate + "<br />";
                    message = message + "<br />";
                    try
                    {
                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, pwd, employee_mailid, "Your Login Time Requisition has been " + lsleavestatus + " ", message, "", "", "");
                        values.message = "Login Time Approved Successfully";
                    }
                    catch
                    {
                        values.message = "Login Time Approved Successfully,but Mail Not Send. ";
                    }

                    objMySqlDataReader.Close();
                }

                if (mnResult != 0)
                {
                    values.message = "Login Approved Successfully...!";
                    return true;
                }
                else
                {
                    values.message = "Error Occured...!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPostRejectLogin(approvelogin values, string employee_gid)
        {
            try
            {
                
                lsleavestatus = "Rejected";
                msSQL =     " update hrm_tmp_tattendancelogin set " +
                            " status = '" + lsleavestatus + "'," +
                            " approved_by = '" + employee_gid + "'," +
                            " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            " updated_by = '" + employee_gid + "'," +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                            " where attendancelogintmp_gid = '" + values.attendancelogintmp_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update hrm_trn_tloginapproval set " +
                                     " approval_flag = 'R', " +
                                     " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                     " where approved_by = '" + employee_gid + "'" +
                                     " and loginapproval_gid = '" + values.attendancelogintmp_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //-------------------------------------------whatsapp----------------------------//
                msSQL = "select whatsapp_flag from adm_mst_tcompany";
                lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);

                if (lswhatsappflag == "Y")
                {
                    msSQL = " select b.employee_mobileno," +
                                 " date_format(a.login_time,'%d-%M-%Y %r ')as login_time," +
                                 " concat(c.user_firstname,' ',c.user_lastname) as username," +
                                 " date_format(a.attendance_date,'%d-%m-%Y') as attendance_date from hrm_tmp_tattendancelogin a" +
                                 " left join hrm_trn_tloginapproval d on a.attendancelogintmp_gid=d.loginapproval_gid" +
                                  " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                             " where a.attendancelogintmp_gid='" + values.attendancelogintmp_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mobile = objMySqlDataReader["employee_mobileno"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        fromdate = objMySqlDataReader["login_time"].ToString();
                        attendance_date = objMySqlDataReader["attendance_date"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                            " from hrm_mst_temployee a " +
                           " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                            " where a.employee_gid='" + employee_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();
                    try
                    {
                        lsmessage = "Hi " + employeename + ",   " + approved_by + "  had " + lsleavestatus + " your Login Time Requisition on   " + attendance_date + " from " + fromdate + "  ";
                        objcmnfunctions.sendMessage(employee_mobile, lsmessage);
                        values.message = "Login Time Rejected Successfully";
                    }
                    catch
                    {
                        values.message = "Login Time Rejected Successfully,but Message Not Send. ";
                    }
                }
                // Mail Flag.....//
                msSQL = " Select approved_by from hrm_trn_tloginapproval " +
                        " where approved_by='" + employee_gid + "' and loginapproval_gid='" + values.attendancelogintmp_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {

                    lsapprovedby = objMySqlDataReader["approved_by"].ToString();
                    objMySqlDataReader.Close();
                    msSQL = " select pop_username,pop_password from adm_mst_tcompany where company_gid='1'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        supportmail = objMySqlDataReader["pop_username"].ToString();
                        pwd = objMySqlDataReader["pop_password"].ToString();
                    }
                    objMySqlDataReader.Close();

                    msSQL = " select b.employee_emailid," +
                             " date_format(a.login_time,'%d-%M-%Y %r ')as login_time," +
                             " concat(c.user_firstname,' ',c.user_lastname) as username," +
                             " date_format(a.attendance_date,'%d-%m-%Y') as attendance_date from hrm_tmp_tattendancelogin a" +
                             " left join hrm_trn_tloginapproval d on a.attendancelogintmp_gid=d.loginapproval_gid" +
                              " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                            " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                         " where a.attendancelogintmp_gid='" + values.attendancelogintmp_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        fromdate = objMySqlDataReader["login_time"].ToString();
                        attendance_date = objMySqlDataReader["attendance_date"].ToString();
                    }

                    objMySqlDataReader.Close();

                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                               " from hrm_mst_temployee a " +
                             " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                               " where a.employee_gid='" + lsapprovedby + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();

                    message = "Hi " + employeename + ",  <br />";
                    message = message + "<br />";
                    message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsleavestatus + " your Login Time Requisition on " + attendance_date + "<br />";
                    message = message + "<br />";
                    message = message + "<b>Login Time Requisition :</b> " + fromdate + "<br />";
                    message = message + "<br />";
                    try
                    {
                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, pwd, employee_mailid, "Your Login Time Requisition has been " + lsleavestatus + " ", message, "", "", "");
                        values.message = "Login Time Rejected Successfully";
                    }
                    catch
                    {
                        values.message = "Login Time Rejected Successfully,but Mail Not Send. ";
                    }

                    objMySqlDataReader.Close();
                }
                if (mnResult != 0)
                {
                    values.message = "Login Rejected Successfully...!";
                    return true;
                }
                else
                {
                    values.message = "Error Occured...!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }

        //..............................* 3. LOGOUT APPROVAL  Details*.............................//
        public bool DaGetLogoutApproval(getlogoutdetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);
                msSQL = " Select a.hierarchy_level " +
                  " from adm_mst_tsubmodule a " +
                  " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                msSQL = " SELECT distinct k.attendancetmp_gid,k.employee_gid,k.status, date_format(k.created_date,'%d-%m-%Y') as created_date, " +
                    " concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, '/', m.department_name) as employeename, " +
                    " date_format(k.attendance_date, '%d-%m-%Y') as attendence_date, time_format(k.logout_time, '%H:%i %p') as logout_time,k.remarks " +
                 " FROM hrm_tmp_tattendance k " +
                 " left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                     " left join hrm_mst_tdepartment m on m.department_gid=c.department_gid" +
                 " left join adm_mst_tdesignation e on c.designation_gid = e.designation_gid " +
                 " left join adm_mst_tuser n on n.user_gid = c.user_gid   " +
                 " where  1 = 1 ";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (k.employee_gid in (" + lblEmployeeGID + ") ";
                }
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNALT'";
                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    //-------------------parallel flow starts here------------------------------//
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or k.attendancetmp_gid in (select logoutapproval_gid from hrm_trn_tlogoutapproval " +
                                 " where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  logoutapproval_gid))";
                        //-------------------parallel flow END here------------------------------//
                    }
                    else
                    {
                        msSQL += " and k.attendancetmp_gid in (select logoutapproval_gid from (select approved_by, " +
                                " logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + employee_gid + "'" +
                                " group by  logoutapproval_gid order by logoutapproval_gid asc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = " select distinct logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'Y'" +
                                  " and approved_by in (" + lblEmployeeGID + ")";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            while (objMySqlDataReader.Read())
                            {
                                lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["logoutapproval_gid"].ToString() + "',";
                            }
                            lsPR_lists = lsPR_lists.TrimEnd(',');

                        }
                        else
                        {
                            lsPR_lists = "";
                        }
                        objMySqlDataReader.Close();
                        if (lsPR_lists != "")
                        {
                            msPRSQL = " select distinct a.attendancetmp_gid from hrm_tmp_tattendance a " +
                             " left join hrm_trn_tlogoutapproval b on a.attendancetmp_gid = b.logoutapproval_gid " +
                             " where a.status = 'Pending' and a.attendancetmp_gid in (" + lsPR_lists + ")" +
                             " and b.approval_flag = 'N' and b.approved_by = '" + employee_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            lsPR_lists = "";
                            if (objMySqlDataReader.HasRows == true)
                            {
                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["attendancetmp_gid"].ToString() + "',";
                                }
                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                    msSQL += " or k.attendancetmp_gid in (" + lsPR_lists + ")";
                                }
                                objMySqlDataReader.Close();
                            }
                        }
                    }
                }
                objMySqlDataReader.Close();
                msSAP = msSQL + " and k.employee_gid <>'" + employee_gid + "' and k.status = 'Pending' group by k.attendancetmp_gid " +
                                " order by date(k.created_date)desc,k.created_date asc,k.attendancetmp_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSAP);
                var getlogoutpending = new List<logoutpending_list>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlogoutpending.Add(new logoutpending_list
                        {
                            attendancelogouttmp_gid = (dr_datarow["attendancetmp_gid"].ToString()),
                            logoutapply_date = (dr_datarow["created_date"].ToString()),
                            employee_name = (dr_datarow["employeename"].ToString()),
                            logoutattendence_date = (dr_datarow["attendence_date"].ToString()),
                            logout_time = (dr_datarow["logout_time"].ToString()),
                            logout_reason = (dr_datarow["remarks"].ToString()),
                            logout_status = (dr_datarow["status"].ToString()),
                            apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                        });
                    }
                    values.logoutpending_list = getlogoutpending;
                }
                dt_datatable.Dispose();
                msSRE = msSQL + " and k.employee_gid <>'" + employee_gid + "' and k.status = 'Rejected' group by k.attendancetmp_gid " +
                           " order by date(k.created_date)desc,k.created_date asc,attendancetmp_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSRE);
                var getlogoutrejected = new List<logoutrejected_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlogoutrejected.Add(new logoutrejected_list
                        {
                            attendancelogouttmp_gid = (dr_datarow["attendancetmp_gid"].ToString()),
                            logoutapply_date = (dr_datarow["created_date"].ToString()),
                            employee_name = (dr_datarow["employeename"].ToString()),
                            logoutattendence_date = (dr_datarow["attendence_date"].ToString()),
                            logout_time = (dr_datarow["logout_time"].ToString()),
                            logout_reason = (dr_datarow["remarks"].ToString()),
                            logout_status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.logoutrejected_list = getlogoutrejected;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaGetLogoutLeaveApproveDetails(getleavesummarydetails values, string employee_gid)
        {
            try
            {
                

                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                        " from adm_mst_tsubmodule a " +
                        " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRMATNALT'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                // logout Approved Summary...//
                msSQL = "select  k.attendancetmp_gid,concat(n.user_firstname,n.user_lastname)as employee_name ,k.status,K.attendance_date,k.logout_time,k.remarks,k.created_date " +
                        "from  hrm_tmp_tattendance k " +
                        "left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                        "left join hrm_trn_tleavedtl w on k.status = w.leave_status " +
                        "left join adm_mst_tuser n on n.user_gid = c.user_gid " +
                        " where  1 = 1 and k.status='Approved'";
                if (lblUserCode != "-1")
                    msSQL += " and (c.employee_gid in (" + lblEmployeeGID + ")";

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNALT'";

                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    // -------------------parallel flow starts here------------------------------
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                        msSQL += " or k.attendancetmp_gid in (select logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  logoutapproval_gid))";
                    else
                    {
                        msSQL += " or k.attendancetmp_gid in (select logoutapproval_gid from (select approved_by, logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'N' group by  logoutapproval_gid order by logoutapproval_gid asc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = " select distinct logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'Y'";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = " select distinct logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'Y' and approved_by = '" + employee_gid + " '";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {

                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["logoutapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                            }
                            objMySqlDataReader.Close();
                            if (lsPR_lists != "")
                            {
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                                msSQL += " or k.attendancetmp_gid in (" + lsPR_lists + ")";
                            }
                        }
                    }
                }
                objMySqlDataReader.Close();

                msSQL = msSQL + " and k.employee_gid <>'" + employee_gid + "' and k.status <> 'Pending' group by k.attendancetmp_gid order by date(k.created_date)desc,k.created_date asc,attendancetmp_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<logout_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleave.Add(new logout_list
                        {
                            logoutapply_date = (dr_datarow["created_date"].ToString()),
                            logoutattendence_date = (dr_datarow["attendance_date"].ToString()),
                            logout_time = (dr_datarow["logout_time"].ToString()),
                            logout_reason = (dr_datarow["remarks"].ToString()),
                            logout_status = (dr_datarow["status"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),

                        });
                    }
                    values.logout_list = getleave;
                }
                dt_datatable.Dispose();

                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaGetLogoutLeaveRejectDetails(getleavesummarydetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                      " from adm_mst_tsubmodule a " +
                      " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRMATNALT'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                // logout Rejected Summary...//


                msSQL = "select  k.attendancetmp_gid,concat(n.user_firstname,n.user_lastname)as employee_name ,k.status,K.attendance_date,k.logout_time,k.remarks,k.created_date " +
                        "from  hrm_tmp_tattendance k " +
                        "left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                        "left join hrm_trn_tleavedtl w on k.status = w.leave_status " +
                        "left join adm_mst_tuser n on n.user_gid = c.user_gid " +
                        " where  1 = 1 and k.status='Rejected'";
                if (lblUserCode != "-1")
                    msSQL += " and (c.employee_gid in (" + lblEmployeeGID + ")";


                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNALT'";

                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    // -------------------parallel flow starts here------------------------------
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                        msSQL += " or k.attendancetmp_gid in (select logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  logoutapproval_gid))";
                    else
                    {
                        msSQL += " or k.attendancetmp_gid in (select logoutapproval_gid from (select approved_by, logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'N' group by  logoutapproval_gid order by logoutapproval_gid asc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = " select distinct logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'Y'";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = " select distinct logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'Y' and approved_by = '" + employee_gid + " '";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {

                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["logoutapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                            }
                            objMySqlDataReader.Close();
                            if (lsPR_lists != "")
                            {
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                                msSQL += " or k.attendancetmp_gid in (" + lsPR_lists + ")";
                            }
                        }
                    }
                }
                objMySqlDataReader.Close();

                msSQL = msSQL + " and k.employee_gid <>'" + employee_gid + "' and k.status = 'Rejected' group by k.attendancetmp_gid order by date(k.created_date)desc,k.created_date asc,attendancetmp_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<logout_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleave.Add(new logout_list
                        {
                            logoutapply_date = (dr_datarow["created_date"].ToString()),
                            logoutattendence_date = (dr_datarow["attendance_date"].ToString()),
                            logout_time = (dr_datarow["logout_time"].ToString()),
                            logout_reason = (dr_datarow["remarks"].ToString()),
                            logout_status = (dr_datarow["status"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),

                        });
                    }
                    values.logout_list = getleave;
                }
                dt_datatable.Dispose();

                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPostApproveLogout(approvelogout values, string employee_gid)
        {
            try
            {
                
                //  strClientIP = Request.UserHostAddress();
                lsleavestatus = "Approved";


                msSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNALT'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                    {
                        objMySqlDataReader.Close();
                        msSQL = " update hrm_trn_tlogoutapproval set " +
                                      " approval_flag = 'N/A', " +
                                      " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                                      " where approved_by <> '" + employee_gid + "' " +
                                      " and logoutapproval_gid = '" + values.attendancelogouttmp_gid + "' and submodule_gid='HRMATNALT'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update hrm_trn_tlogoutapproval set " +
                                   " approval_flag = 'Y', " +
                                   " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                                   " where approved_by = '" + employee_gid + "' " +
                                   " and logoutapproval_gid = '" + values.attendancelogouttmp_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        objMySqlDataReader.Close();
                        msSQL = " update hrm_trn_tlogoutapproval set " +
                                     " approval_flag = 'Y', " +
                                     " seqhierarchy_view='N', " +
                                     " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                     " where approved_by =  '" + employee_gid + "' " +
                                     " and logoutapproval_gid =  '" + values.attendancelogouttmp_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update hrm_trn_tlogoutapproval set " +
                            " seqhierarchy_view='Y' " +
                            " where approval_flag='N'and approved_by <> '" + employee_gid + "' and logoutapproval_gid = '" + values.attendancelogouttmp_gid + "'" +
                            " order by hierary_level desc limit 1 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }
                objMySqlDataReader.Close();
                msSQL = " select approval_gid from hrm_trn_tlogoutapproval where logoutapproval_gid='" + values.attendancelogouttmp_gid + "' " +
                        " and approval_flag = 'N' and submodule_gid='HRMATNALT'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == false)
                {
                    objMySqlDataReader.Close();
                    msSQL = " update hrm_tmp_tattendance set " +
                            " status = '" + lsleavestatus + "'," +
                            " approvedby = '" + employee_gid + "'," +
                            " approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            " updated_by = '" + employee_gid + "'," +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                            " where attendancetmp_gid =  '" + values.attendancelogouttmp_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        lsdate2 = DateTime.ParseExact(values.logoutattendence_date.Replace("-", ""), "ddMMyyyy", null);

                        msSQL = "Select date_format(logout_time,'%Y-%m-%d %H:%i:%s') as logout_time from hrm_tmp_tattendance " +
                                "where attendance_date='" + lsdate2.ToString("yyyy-MM-dd") + "' and employee_gid='" + values.apply_employeegid + "'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            lslogout = objMySqlDataReader["logout_time"].ToString();
                        }
                        objMySqlDataReader.Close();
                        msSQL = " update hrm_trn_tattendance set " +
                          " logout_time = '" + lslogout + "'," +
                          " logout_time_audit = '" + lslogout + "'," +
                          " logout_ip = '" + strClientIP + "', " +
                          " update_flag='N' " +
                          " where attendance_date='" + lsdate2.ToString("yyyy-MM-dd") + "' and employee_gid='" + values.apply_employeegid + "'" +
                          " and login_time is not null";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "Select (time(login_time)) as logintime,(time(logout_time)) as logouttime,(time(lunch_out_scheduled)) as lunch_out_scheduled from hrm_trn_tattendance " +
                                " where employee_gid = '" + values.apply_employeegid + "' and " +
                                " attendance_date like '" + lsdate2.ToString("yyyy-MM-dd") + "%'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            if (objMySqlDataReader["logintime"].ToString() != "")
                            {
                                time1 = TimeSpan.Parse(objMySqlDataReader["logintime"].ToString());
                            }
                            if (objMySqlDataReader["logouttime"].ToString() != "")
                            {
                                time2 = TimeSpan.Parse(objMySqlDataReader["logouttime"].ToString());
                            }
                            lslunch = TimeSpan.Parse(objMySqlDataReader["lunch_out_scheduled"].ToString());
                            lstime = objMySqlDataReader["logouttime"].ToString();

                            if (TimeSpan.Parse(lstime) < TimeSpan.Parse("14:00:00"))
                            {
                                lstotal = time2 - time1;
                            }
                            else
                            {
                                lstotal = time2 - time1;
                                lstotal = lstotal - lslunch;
                            }
                        }
                        objMySqlDataReader.Close();
                    }
                }
                objMySqlDataReader.Close();

                // ......................Whatspp.......................//

                msSQL = "select whatsapp_flag from adm_mst_tcompany";
                lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);
                if (lswhatsappflag == "Y")
                {
                    msSQL = " select b.employee_mobileno," +
                                     " date_format(a.logout_time,'%d-%M-%Y %r')as logout_time," +
                                     " concat(c.user_firstname,' ',c.user_lastname) as username," +
                                     " a.attendance_date from hrm_tmp_tattendance a" +
                                     " left join hrm_trn_tlogoutapproval d on a.attendancetmp_gid=d.logoutapproval_gid" +
                                      " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                 " where a.attendancetmp_gid='" + values.attendancelogouttmp_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mobile = objMySqlDataReader["employee_mobileno"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        fromdate = objMySqlDataReader["logout_time"].ToString();
                        attendance_date = objMySqlDataReader["attendance_date"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + employee_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();
                    try
                    {
                        lsmessage = "Hi " + employeename + ",   " + approved_by + "  had " + lsleavestatus + " your Logout Time Requisition on   " + attendance_date + " from " + fromdate + "  ";

                        objcmnfunctions.sendMessage(employee_mobile, lsmessage);
                        values.message = "Logout Time Approved Successfully";
                    }
                    catch
                    {
                        values.message = "Logout Time Approved Successfully,but Message Not Send. ";
                    }
                }

                //..........................................................mail functions...........................//

                msSQL = " Select approved_by from hrm_trn_tlogoutapproval " +
                        " where approved_by='" + employee_gid + "' and logoutapproval_gid='" + values.attendancelogouttmp_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {

                    lsapprovedby = objMySqlDataReader["approved_by"].ToString();
                    objMySqlDataReader.Close();
                    msSQL = " select pop_username,pop_password from adm_mst_tcompany where company_gid='1'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        supportmail = objMySqlDataReader["pop_username"].ToString();
                        pwd = objMySqlDataReader["pop_password"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select b.employee_emailid," +
                                 " date_format(a.logout_time,'%d-%M-%Y %r')as logout_time," +
                                 " concat(c.user_firstname,' ',c.user_lastname) as username," +
                                 " a.attendance_date from hrm_tmp_tattendance a" +
                                 " left join hrm_trn_tlogoutapproval d on a.attendancetmp_gid=d.logoutapproval_gid" +
                                  " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                             " where a.attendancetmp_gid='" + values.attendancelogouttmp_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        fromdate = objMySqlDataReader["logout_time"].ToString();
                        attendance_date = objMySqlDataReader["attendance_date"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + lsapprovedby + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();
                    message = "Hi " + employeename + ",  <br />";
                    message = message + "<br />";
                    message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsleavestatus + " your Logout Time Requisition on " + attendance_date + "<br />";
                    message = message + "<br />";
                    message = message + "<b>Logout Time Requisition :</b> " + fromdate + "<br />";
                    message = message + "<br />";

                    try
                    {
                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, pwd, employee_mailid, "Your Logout Time Requisition has been " + lsleavestatus + " ", message, "", "", "");
                    }
                    catch
                    {
                        values.status = false;
                        values.message = "Mail Not Send !";
                    }


                }
                objMySqlDataReader.Close();

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Logout Approved Successfully...!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured...!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPostRejectLogout(approvelogout values, string employee_gid)
        {
            try
            {
                
                lsleavestatus = "Rejected";
                msSQL = " update hrm_tmp_tattendance set " +
                            " status = '" + lsleavestatus + "'," +
                            " approvedby = '" + employee_gid + "'," +
                            " approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            " updated_by = '" + employee_gid + "'," +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                            " where attendancetmp_gid = '" + values.attendancelogouttmp_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update hrm_trn_tlogoutapproval set " +
                                     " approval_flag = 'R', " +
                                     " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                     " where approved_by = '" + employee_gid + "'" +
                                     " and logoutapproval_gid = '" + values.attendancelogouttmp_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                // ......................Whatspp.......................//

                msSQL = "select whatsapp_flag from adm_mst_tcompany";
                lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);
                if (lswhatsappflag == "Y")
                {
                    msSQL = " select b.employee_mobileno," +
                                     " date_format(a.logout_time,'%d-%M-%Y %r')as logout_time," +
                                     " concat(c.user_firstname,' ',c.user_lastname) as username," +
                                     " a.attendance_date from hrm_tmp_tattendance a" +
                                     " left join hrm_trn_tlogoutapproval d on a.attendancetmp_gid=d.logoutapproval_gid" +
                                      " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                 " where a.attendancetmp_gid='" + values.attendancelogouttmp_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mobile = objMySqlDataReader["employee_mobileno"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        fromdate = objMySqlDataReader["logout_time"].ToString();
                        attendance_date = objMySqlDataReader["attendance_date"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + employee_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();
                    try
                    {
                        lsmessage = "Hi " + employeename + ",   " + approved_by + "  had " + lsleavestatus + " your Logout Time Requisition on   " + attendance_date + " from " + fromdate + "  ";

                        objcmnfunctions.sendMessage(employee_mobile, lsmessage);
                        values.message = "Logout Rejected Successfully !";
                    }
                    catch
                    {
                        values.message = "Logout Time Rejected Successfully,but Message Not Send !";
                    }
                }

                //..........................................................mail functions...........................//

                msSQL = " Select approved_by from hrm_trn_tlogoutapproval " +
                        " where approved_by='" + employee_gid + "' and logoutapproval_gid='" + values.attendancelogouttmp_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {

                    lsapprovedby = objMySqlDataReader["approved_by"].ToString();
                    objMySqlDataReader.Close();
                    msSQL = " select pop_username,pop_password from adm_mst_tcompany where company_gid='1'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        supportmail = objMySqlDataReader["pop_username"].ToString();
                        pwd = objMySqlDataReader["pop_password"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select b.employee_emailid," +
                                 " date_format(a.logout_time,'%d-%M-%Y %r')as logout_time," +
                                 " concat(c.user_firstname,' ',c.user_lastname) as username," +
                                 " a.attendance_date from hrm_tmp_tattendance a" +
                                 " left join hrm_trn_tlogoutapproval d on a.attendancetmp_gid=d.logoutapproval_gid" +
                                 " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                 " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                 " where a.attendancetmp_gid='" + values.attendancelogouttmp_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        fromdate = objMySqlDataReader["logout_time"].ToString();
                        attendance_date = objMySqlDataReader["attendance_date"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + lsapprovedby + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();
                    message = "Hi " + employeename + ",  <br />";
                    message = message + "<br />";
                    message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsleavestatus + " your Logout Time Requisition on " + attendance_date + "<br />";
                    message = message + "<br />";
                    message = message + "<b>Logout Time Requisition :</b> " + fromdate + "<br />";
                    message = message + "<br />";

                    try
                    {
                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, pwd, employee_mailid, "Your Logout Time Requisition has been " + lsleavestatus + " ", message, "", "", "");
                    }
                    catch
                    {
                        values.message = "Mail Not Send !";
                    }
                }
                objMySqlDataReader.Close();

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Logout Time Rejected Successfully !";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured !";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        //..............................* 4. OD APPROVAL Details *..................................//
        public bool DaGetODSummaryDetails(getODdetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                     " from adm_mst_tsubmodule a " +
                     " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                lsPR_lists = "";
                msSQL = " select distinct l.employee_gid,k.ondutytracker_gid,k.ondutytracker_status,date_format(k.ondutytracker_date,'%d-%m-%Y') as ondutydate, " +
                        " concat(k.onduty_fromtime,':',k.from_minutes) as onduty_fromtime,concat(k.onduty_totime,':',k.to_minutes) as onduty_totime,k.onduty_duration,date_format(k.created_date, '%d-%m-%Y') as createddate,k.onduty_reason, " +
                        " concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, '/', m.department_name) as employee_name " +
                        "  from hrm_trn_tondutytracker k" +
                        " left join hrm_mst_temployee l on l.employee_gid=k.employee_gid" +
                        " left join hrm_mst_tdepartment m on m.department_gid=l.department_gid" +
                        " left join adm_mst_tuser n on n.user_gid=l.user_gid" +
                        " left join hrm_trn_tattendance t on t.employee_gid=k.employee_gid " +
                        " where k.ondutytracker_status='Pending' ";
                msSQL += " and (l.employee_gid in (" + lblEmployeeGID + ")) ";
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    string lsapprovaltype = (objMySqlDataReader["approval_type"].ToString());
                    objMySqlDataReader.Close();
                    if (lsapprovaltype == "Parallel")
                    {
                        msSQL += " or (k.ondutytracker_gid in (select ondutyapproval_gid from hrm_trn_tapproval where " +
                                 " approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  ondutyapproval_gid) " +
                                 " and k.ondutytracker_status ='Pending') ";
                    }
                    else
                    {
                        msSQL += " and k.ondutytracker_gid in (select ondutyapproval_gid from (select approved_by, " +
                                 " ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + employee_gid + "'" +
                                 " group by  ondutyapproval_gid order by ondutyapproval_gid desc) p where " +
                                 " approved_by = '" + employee_gid + "')";

                        msPRSQL = " select distinct ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N'";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            objMySqlDataReader.Close();
                            msPRSQL = " select distinct ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N'" +
                                      " and approved_by = '" + employee_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {

                                string lsondutyapproval_gid = objMySqlDataReader["ondutyapproval_gid"].ToString();
                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lsondutyapproval_gid + "',";
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                                objMySqlDataReader.Close();

                            }
                            objMySqlDataReader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or k.ondutytracker_gid in (" + lsPR_lists + ")";
                            }
                        }
                        objMySqlDataReader.Close();
                    }
                }
                objMySqlDataReader.Close();

                msSQL = msSQL + "  order by date(k.created_date) desc,k.created_date asc,k.ondutytracker_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getODpending = new List<ODpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getODpending.Add(new ODpending_list
                        {
                            ondutytracker_gid = (dr_datarow["ondutytracker_gid"].ToString()),
                            created_date = (dr_datarow["createddate"].ToString()),
                            onduty_date = (dr_datarow["ondutydate"].ToString()),
                            onduty_from = (dr_datarow["onduty_fromtime"].ToString()),
                            onduty_to = (dr_datarow["onduty_totime"].ToString()),
                            onduty_duration = (dr_datarow["onduty_duration"].ToString()),
                            onduty_reason = (dr_datarow["onduty_reason"].ToString()),
                            ondutytracker_status = (dr_datarow["ondutytracker_status"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                        });
                    }
                    values.ODpending_list = getODpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaGetODLeaveApproveDetails(getleavesummarydetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                        " from adm_mst_tsubmodule a " +
                        " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRMATNALT'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                // od Approved Summary...//
                msSQL = " select distinct k.ondutytracker_gid,k.ondutytracker_status,k.ondutytracker_date, k.onduty_fromtime, " +
                        " k.onduty_duration,k.created_date,k.onduty_reason, k.onduty_totime,concat(n.user_firstname,n.user_lastname)as employee_name " +
                        " from  hrm_trn_tondutytracker k " +
                        " left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                        " left join hrm_trn_tleavedtl w on k.ondutytracker_status = w.leave_status " +
                        " left join adm_mst_tuser n on n.user_gid = c.user_gid " +
                        " where  1 = 1 and k.ondutytracker_status = 'Approved' ";
                msSQL +=" and (c.employee_gid in (" + lblEmployeeGID + ")";

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";

                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    // -------------------parallel flow starts here------------------------------
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                        msSQL += " or (k.ondutytracker_gid in (select ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  ondutyapproval_gid) and k.ondutytracker_status ='Approved')) ";
                    else
                    {
                        msSQL += " or k.ondutytracker_gid in (select ondutyapproval_gid from (select approved_by, ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N' group by  ondutyapproval_gid order by ondutyapproval_gid desc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = " select distinct ondutyapproval_gid from hrm_trn_tapproval";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = "select distinct ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + " '";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["ondutyapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                            }
                            objMySqlDataReader.Close();
                            if (lsPR_lists != "")
                            {
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                                msSQL += " or k.ondutytracker_gid in (" + lsPR_lists + ")";
                            }
                        }
                    }
                }
                objMySqlDataReader.Close();

                msSQL = msSQL + " and k.ondutytracker_status = 'Approved'  order by date(k.created_date) desc,k.created_date asc,k.ondutytracker_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<od_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleave.Add(new od_list
                        {
                            onduty_date = (dr_datarow["ondutytracker_date"].ToString()),
                            onduty_from = (dr_datarow["onduty_fromtime"].ToString()),
                            onduty_to = (dr_datarow["onduty_totime"].ToString()),
                            onduty_duration = (dr_datarow["onduty_duration"].ToString()),
                            onduty_reason = (dr_datarow["onduty_reason"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            ondutytracker_status = (dr_datarow["ondutytracker_status"].ToString()),
                        });
                    }
                    values.od_list = getleave;
                }
                dt_datatable.Dispose();

                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }

        public bool GetODLeaveRejectDetails(getleavesummarydetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                      " from adm_mst_tsubmodule a " +
                      " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRMATNALT'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                // od Rejected Summary...//

                msSQL = "select distinct k.ondutytracker_gid,k.ondutytracker_status,k.ondutytracker_date, k.onduty_fromtime, " +
                        " k.onduty_duration,k.created_date,k.onduty_reason, k.onduty_totime,concat(n.user_firstname,n.user_lastname)as employee_name " +
                        " from  hrm_trn_tondutytracker k " +
                        " left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                        " left join hrm_trn_tleavedtl w on k.ondutytracker_status = w.leave_status " +
                        " left join adm_mst_tuser n on n.user_gid = c.user_gid " +
                        " where  1 = 1 and k.ondutytracker_status = 'Rejected' ";
                msSQL += " and (c.employee_gid in (" + lblEmployeeGID + ")";

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";

                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    // -------------------parallel flow starts here------------------------------
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                        msSQL += " or (k.ondutytracker_gid in (select ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  ondutyapproval_gid) and k.ondutytracker_status ='Rejected')) ";
                    else
                    {
                        msSQL += " or k.ondutytracker_gid in (select ondutyapproval_gid from (select approved_by, ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N' group by  ondutyapproval_gid order by ondutyapproval_gid desc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = " select distinct ondutyapproval_gid from hrm_trn_tapproval";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = "select distinct ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + " '";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {

                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["ondutyapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                            }
                            objMySqlDataReader.Close();
                            if (lsPR_lists != "")
                            {
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                                msSQL += " or k.ondutytracker_gid in (" + lsPR_lists + ")";
                            }

                        }
                    }
                }
                objMySqlDataReader.Close();

                msSQL = msSQL + " order by date(k.created_date) desc,k.created_date asc,k.ondutytracker_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<od_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleave.Add(new od_list
                        {
                            onduty_date = (dr_datarow["ondutytracker_date"].ToString()),
                            onduty_from = (dr_datarow["onduty_fromtime"].ToString()),
                            onduty_to = (dr_datarow["onduty_totime"].ToString()),
                            onduty_duration = (dr_datarow["onduty_duration"].ToString()),
                            onduty_reason = (dr_datarow["onduty_reason"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            ondutytracker_status = (dr_datarow["ondutytracker_status"].ToString()),
                        });
                    }
                    values.od_list = getleave;
                }
                dt_datatable.Dispose();

                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPostApproveOD(approveOD values, string employee_gid)
        {
            try
            {
                
                string lsondutystatus = "Approved";

                msSQL = " update hrm_trn_tapproval set " +
                        " approval_flag = 'Y', " +
                        " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                        " where approved_by = '" + employee_gid + "'" +
                        " and ondutyapproval_gid = '" + values.ondutytracker_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 0)
                {
                    values.message = "Error Occured While Updating Records";
                }
                else
                {
                    msSQL = "select cast(concat(ondutytracker_date,' ',replace(concat(onduty_fromtime,':',from_minutes,':00'),' ','')) as char) as login_time," +
                        " cast(concat(ondutytracker_date,' ',replace(concat(onduty_totime,':',to_minutes,':00') ,' ',''))as char) as logout_time," +
                        " half_day,cast((half_session) as char) as half_session,date_format(ondutytracker_date,'%Y-%m-%d') as ondutytracker_date,employee_gid from hrm_trn_tondutytracker" +
                        " where ondutytracker_gid = '" + values.ondutytracker_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {

                        msSQL = "update hrm_trn_tattendance set " +
                            " attendance_type ='" + objMySqlDataReader["half_session"] + "'," +
                            " employee_attendance='Onduty'," +
                            " login_time='" + objMySqlDataReader["login_time"] + "'," +
                            " logout_time='" + objMySqlDataReader["logout_time"] + "'," +
                            " update_flag='N'" +
                          " where attendance_date='" + objMySqlDataReader["ondutytracker_date"] + "' and employee_gid='" + objMySqlDataReader["employee_gid"] + "'";
                        objMySqlDataReader.Close();
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                msSQL = " select approval_gid from hrm_trn_tapproval where ondutyapproval_gid='" + values.ondutytracker_gid + "' and approval_flag = 'N'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == false)
                {
                    objMySqlDataReader.Close();
                    msSQL = " update hrm_trn_tondutytracker set " +
                               " ondutytracker_status = 'Approved'," +
                               " onduty_approveby = '" + employee_gid + "'," +
                               " onduty_approvedate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                               " where ondutytracker_gid = '" + values.ondutytracker_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    objMySqlDataReader.Close();
                    msSQL = " update hrm_trn_tondutytracker set " +
                              " ondutytracker_status = 'Pending'" +
                              " where ondutytracker_gid = '" + values.ondutytracker_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objMySqlDataReader.Close();

                msSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                    {
                        objMySqlDataReader.Close();
                        msSQL = " update hrm_trn_tondutytracker set " +
                                " ondutytracker_status = 'Approved'," +
                                " onduty_approveby = '" + employee_gid + "'," +
                               " onduty_approvedate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                " where ondutytracker_gid = '" + values.ondutytracker_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        msSQL = " update hrm_trn_tapproval set " +
                                " approval_flag = 'Y', " +
                                " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                " where ondutyapproval_gid = '" + values.ondutytracker_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = " update hrm_trn_tapproval set " +
                                    " approval_flag = 'Y', " +
                                    " seqhierarchy_view='N', " +
                                    " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                    " where approved_by = '" + employee_gid + "'" +
                                    " and ondutyapproval_gid = '" + values.ondutytracker_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update hrm_trn_tapproval set " +
                          " seqhierarchy_view='Y' " +
                          " where approval_flag='N'and approved_by <> '" + employee_gid + "' and ondutyapproval_gid = '" + values.ondutytracker_gid + "'" +
                          " order by hierary_level desc limit 1 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objMySqlDataReader.Close();


                //........................Whatsapp.........................................//

                msSQL = "select whatsapp_flag from adm_mst_tcompany";
                lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);

                if (lswhatsappflag == "Y")
                {
                    msSQL = " select b.employee_mobileno,a.onduty_duration,a.onduty_fromtime, a.onduty_totime, date_format(a.ondutytracker_date,'%d-%m-%Y') as ondutytracker_date," +
                                 " Concat(c.user_firstname,' ',c.user_lastname) as username,a.onduty_reason,a.ondutytracker_date" +
                                 " from hrm_trn_tondutytracker a" +
                                 " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                                 " where a.ondutytracker_gid='" + values.ondutytracker_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mobile = objMySqlDataReader["employee_mobileno"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        reason = objMySqlDataReader["onduty_reason"].ToString();
                        days = objMySqlDataReader["onduty_duration"].ToString();
                        fromdate = objMySqlDataReader["onduty_fromtime"].ToString();
                        todate = objMySqlDataReader["onduty_totime"].ToString();
                        onduty_date = objMySqlDataReader["ondutytracker_date"].ToString();
                    }
                    objMySqlDataReader.Close();

                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                            " from hrm_mst_temployee a " +
                            " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                             " where a.employee_gid='" + employee_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();

                    try
                    {
                        lsmessage = "Hi " + employeename + ",   " + approved_by + "  had " + lsondutystatus + " your Onduty applied on  " + onduty_date + " from " + fromdate + " to " + todate + " for about " + days + " hours ";

                        objcmnfunctions.sendMessage(employee_mobile, lsmessage);
                    }
                    catch
                    {
                        values.message = "OD Approved Successfully,but Message Not Send.";
                    }
                }
                //.............Mail Functions.................//

                msSQL = " Select approved_by from hrm_trn_tapproval " +
                        " where approved_by='" + employee_gid + "' and ondutyapproval_gid='" + values.ondutytracker_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {

                    lsapprovedby = objMySqlDataReader["approved_by"].ToString();
                    objMySqlDataReader.Close();
                    msSQL = " select pop_username,pop_password from adm_mst_tcompany where company_gid='1'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        supportmail = objMySqlDataReader["pop_username"].ToString();
                        pwd = objMySqlDataReader["pop_password"].ToString();
                    }
                    objMySqlDataReader.Close();

                    msSQL = " select b.employee_emailid,a.onduty_duration,a.onduty_fromtime, a.onduty_totime, date_format(a.ondutytracker_date,'%d-%m-%Y') as ondutytracker_date," +
                             " Concat(c.user_firstname,' ',c.user_lastname) as username,a.onduty_reason,a.ondutytracker_date" +
                             " from hrm_trn_tondutytracker a" +
                             " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                             " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                             " where a.ondutytracker_gid='" + values.ondutytracker_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        reason = objMySqlDataReader["onduty_reason"].ToString();
                        days = objMySqlDataReader["onduty_duration"].ToString();
                        fromdate = objMySqlDataReader["onduty_fromtime"].ToString();
                        todate = objMySqlDataReader["onduty_totime"].ToString();
                        onduty_date = objMySqlDataReader["ondutytracker_date"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + lsapprovedby + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();

                    message = "Hi " + employeename + ",  <br />";
                    message = message + "<br />";
                    message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsondutystatus + " your Onduty applied on " + onduty_date + " <br />";
                    message = message + "<br />";
                    message = message + "<b>Reason :</b> " + reason + "<br />";
                    message = message + "<br />";
                    message = message + "<b>From Hours :</b> " + fromdate + " &nbsp; &nbsp; <b>To Hours :</b> " + todate + "<br />";
                    message = message + "<br />";
                    message = message + "<b>Total No of Hours :</b> " + days + " <br />";
                    message = message + "<br />";

                    try
                    {
                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, pwd, employee_mailid, "Your Onduty has been " + lsondutystatus + " ", message, "", "", "");
                    }
                    catch
                    {
                        values.message = "Mail Not Send";
                    }

                }

                if (mnResult != 0)
                {
                    values.message = "OD Approved Successfully...!";
                    return true;
                }
                else
                {
                    values.message = "Error Occured...!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPostRejectOD(approveOD values, string employee_gid)
        {
            try
            {
                
                string lsondutystatus = "Rejected";

                msSQL = " update hrm_trn_tondutytracker set " +
                        " ondutytracker_status = '" + lsondutystatus + "'," +
                        " onduty_approveby = '" + employee_gid + "'," +
                        " onduty_approvedate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                        " where ondutytracker_gid = '" + values.ondutytracker_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //........................Whatsapp.........................................//

                msSQL = "select whatsapp_flag from adm_mst_tcompany";
                lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);

                if (lswhatsappflag == "Y")
                {
                    msSQL = " select b.employee_mobileno,a.onduty_duration,a.onduty_fromtime, a.onduty_totime, date_format(a.ondutytracker_date,'%d-%m-%Y') as ondutytracker_date," +
                                 " Concat(c.user_firstname,' ',c.user_lastname) as username,a.onduty_reason,a.ondutytracker_date" +
                                 " from hrm_trn_tondutytracker a" +
                                 " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                                 " where a.ondutytracker_gid='" + values.ondutytracker_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mobile = objMySqlDataReader["employee_mobileno"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        reason = objMySqlDataReader["onduty_reason"].ToString();
                        days = objMySqlDataReader["onduty_duration"].ToString();
                        fromdate = objMySqlDataReader["onduty_fromtime"].ToString();
                        todate = objMySqlDataReader["onduty_totime"].ToString();
                        onduty_date = objMySqlDataReader["ondutytracker_date"].ToString();
                    }
                    objMySqlDataReader.Close();

                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                            " from hrm_mst_temployee a " +
                            " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                             " where a.employee_gid='" + employee_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();

                    try
                    {
                        lsmessage = "Hi " + employeename + ",   " + approved_by + "  had " + lsondutystatus + " your Onduty applied on  " + onduty_date + " from " + fromdate + " to " + todate + " for about " + days + " hours ";

                        objcmnfunctions.sendMessage(employee_mobile, lsmessage);
                    }
                    catch
                    {
                        values.message = "OD Rejected Successfully,but Message Not Send.";
                    }
                }


                //.............Mail Functions.................//

                msSQL = " Select approved_by from hrm_trn_tapproval " +
                        " where approved_by='" + employee_gid + "' and ondutyapproval_gid='" + values.ondutytracker_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {

                    lsapprovedby = objMySqlDataReader["approved_by"].ToString();
                    objMySqlDataReader.Close();
                    msSQL = " select pop_username,pop_password from adm_mst_tcompany where company_gid='1'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        supportmail = objMySqlDataReader["pop_username"].ToString();
                        pwd = objMySqlDataReader["pop_password"].ToString();
                    }
                    objMySqlDataReader.Close();

                    msSQL = " select b.employee_emailid,a.onduty_duration,a.onduty_fromtime, a.onduty_totime, date_format(a.ondutytracker_date,'%d-%m-%Y') as ondutytracker_date," +
                             " Concat(c.user_firstname,' ',c.user_lastname) as username,a.onduty_reason,a.ondutytracker_date" +
                             " from hrm_trn_tondutytracker a" +
                             " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                             " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                             " where a.ondutytracker_gid='" + values.ondutytracker_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        reason = objMySqlDataReader["onduty_reason"].ToString();
                        days = objMySqlDataReader["onduty_duration"].ToString();
                        fromdate = objMySqlDataReader["onduty_fromtime"].ToString();
                        todate = objMySqlDataReader["onduty_totime"].ToString();
                        onduty_date = objMySqlDataReader["ondutytracker_date"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + lsapprovedby + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();

                    message = "Hi " + employeename + ",  <br />";
                    message = message + "<br />";
                    message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsondutystatus + " your Onduty applied on " + onduty_date + " <br />";
                    message = message + "<br />";
                    message = message + "<b>Reason :</b> " + reason + "<br />";
                    message = message + "<br />";
                    message = message + "<b>From Hours :</b> " + fromdate + " &nbsp; &nbsp; <b>To Hours :</b> " + todate + "<br />";
                    message = message + "<br />";
                    message = message + "<b>Total No of Hours :</b> " + days + " <br />";
                    message = message + "<br />";

                    try
                    {
                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, pwd, employee_mailid, "Your Onduty has been " + lsondutystatus + " ", message, "", "", "");
                        values.message = "On Duty Rejected Successfully !";
                    }
                    catch
                    {
                        values.message = "Mail Not Send";
                    }

                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "OD Rejected Successfully...!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured...!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        //..............................* 5. PERMISSION APPROVAL  Details *..........................//
        public bool GetPermissionSummaryDetails(getpermissiondetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = "select system_type from hrm_mst_thrconfig " +
                       " where actualcompany_code='" + ConfigurationManager.ConnectionStrings["company_code"] + "'";
                lssystem_type = objdbconn.GetExecuteScalar(msSQL);

                lsPR_lists = "";
                msSQL = " select distinct a.permission_gid,a.permissiondtl_gid,a.permission_reason,a.permission_status,concat(a.permission_totalhours, ':', a.total_mins, ':', '00') " +
                        " as permission_total ,concat(a.permission_fromhours, ':', a.permission_frommins) as permission_from," +
                        " concat(a.permission_tohours, ':', a.permission_tomins) as permission_to ,date_format(a.permission_date, '%d-%m-%Y') as permission_date, " +
                        " date_format(a.permission_applydate, '%d-%m-%Y') as createddate, " +
                        " concat(d.user_code, '/', d.user_firstname, ' ', d.user_lastname, '/', e.department_name) as employee_name " +
                        " from hrm_trn_tpermissiondtl a left join hrm_mst_temployee i on a.employee_gid=i.employee_gid" +
                        " left join hrm_mst_tdepartment e on e.department_gid=i.department_gid " +
                        " left join adm_mst_Tuser d on d.user_gid=i.user_gid" +
                        " left join hrm_trn_temployeetypedtl j on i.employee_gid=j.employee_gid " +
                        " where a.permission_status='Pending'";
                if (lssystem_type == "AUDIT")
                {
                    msSQL += " and j.employeetype_name='Roll' ";
                }
                msSQL += " and (i.employee_gid in (" + lblEmployeeGID + ")) ";
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (a.permissiondtl_gid in (select permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  permissionapproval_gid) and a.permission_status ='Approval Pending') ";
                    }
                    else
                    {
                        msSQL += " and a.permissiondtl_gid in (select permissionapproval_gid from (select approved_by, " +
                                 " permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + employee_gid + "'" +
                                 " group by  permissionapproval_gid order by permissionapproval_gid desc) p where approved_by = '" + employee_gid + "')";

                        msPRSQL = " select distinct permissionapproval_gid from hrm_trn_tapproval ";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = " select distinct permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                string lspermissionapproval_gid = objMySqlDataReader["permissionapproval_gid"].ToString();

                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lspermissionapproval_gid + "',";
                                }

                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                            }
                            objMySqlDataReader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or a.permissiondtl_gid in (" + lsPR_lists + ")";
                            }

                        }

                    }
                }
                msSQL = msSQL + " and a.permission_status='Pending' order by date(a.created_date) desc,a.created_date asc,a.permissiondtl_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpermissionpending = new List<permissionpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpermissionpending.Add(new permissionpending_list
                        {
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            permission_gid = (dr_datarow["permission_gid"].ToString()),
                            permissiondtl_gid = (dr_datarow["permissiondtl_gid"].ToString()),
                            permission_date = (dr_datarow["permission_date"].ToString()),
                            permission_from = (dr_datarow["permission_from"].ToString()),
                            permission_to = (dr_datarow["permission_to"].ToString()),
                            permission_duration = (dr_datarow["permission_total"].ToString()),
                            permission_reason = (dr_datarow["permission_reason"].ToString()),
                            permission_status = (dr_datarow["permission_status"].ToString()),
                            permission_createddate = (dr_datarow["createddate"].ToString()),
                        });
                    }
                    values.permissionpending_list = getpermissionpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaGetPermissionLeaveApproveDetails(getleavesummarydetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                        " from adm_mst_tsubmodule a " +
                        " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRMATNALT'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                // Permission Approved Summary...//

                msSQL = "  select distinct a.permission_gid,a.permissiondtl_gid,a.permission_status,a.permission_date,a.permission_fromhours,a.permission_frommins," +
                        "  a.permission_totalhours,a.total_mins,a.permission_tohours, a.permission_applydate,a.permission_reason,a.permission_status,concat(n.user_firstname,n.user_lastname)as employee_name " +
                        "  from hrm_trn_tpermissiondtl a " +
                        "  left join hrm_mst_temployee c on a.employee_gid = c.employee_gid " +
                        "  left join hrm_trn_tleavedtl w on a.permission_status = w.leave_status " +
                        "  left join adm_mst_tuser n on n.user_gid = c.user_gid " +
                        "  where  1 = 1 and a.permission_status = 'Approved' ";

                msSQL += " and (c.employee_gid in (" + lblEmployeeGID + ")";

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";

                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    // -------------------parallel flow starts here------------------------------
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                        msSQL += " or (a.permissiondtl_gid in (select permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  permissionapproval_gid) and a.permission_status ='Approval Pending'))";
                    else
                    {
                        msSQL += "  or a.permissiondtl_gid in (select permissionapproval_gid from (select approved_by, permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' group by  permissionapproval_gid order by permissionapproval_gid desc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = "select distinct permissionapproval_gid from hrm_trn_tapproval";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = " select distinct permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'Y' and approved_by = '" + employee_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {

                                while (objMySqlDataReader.Read())
                                {
                                    {
                                        lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["permissionapproval_gid"].ToString() + "',";
                                    }
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                                objMySqlDataReader.Close();
                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                    msSQL += " or a.permissiondtl_gid in (" + lsPR_lists + ")";
                                }


                            }
                        }
                    }
                }
                objMySqlDataReader.Close();

                msSQL = msSQL + "order by date(a.created_date)desc, a.created_date asc, a.permissiondtl_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<permission_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleave.Add(new permission_list
                        {
                            permission_date = (dr_datarow["permission_date"].ToString()),
                            permission_from = (dr_datarow["permission_fromhours"].ToString()),
                            permission_to = (dr_datarow["permission_tohours"].ToString()),
                            permission_duration = (dr_datarow["permission_totalhours"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            permission_reason = (dr_datarow["permission_reason"].ToString()),
                            permission_status = (dr_datarow["permission_status"].ToString()),
                        });
                    }
                    values.permission_list = getleave;
                }
                dt_datatable.Dispose();

                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool GetPermissionLeaveRejectDetails(getleavesummarydetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                      " from adm_mst_tsubmodule a " +
                      " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRMATNALT'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                // Permission Rejected Summary...//
                msSQL = "  select distinct a.permission_gid,a.permissiondtl_gid,a.permission_status,a.permission_date,a.permission_fromhours,a.permission_frommins," +
                        "  a.permission_totalhours,a.total_mins,a.permission_tohours, a.permission_applydate,a.permission_reason,a.permission_status,concat(n.user_firstname,n.user_lastname)as employee_name " +
                        "  from hrm_trn_tpermissiondtl a " +
                        "  left join hrm_mst_temployee c on a.employee_gid = c.employee_gid " +
                        "  left join hrm_trn_tleavedtl w on a.permission_status = w.leave_status " +
                        "  left join adm_mst_tuser n on n.user_gid = c.user_gid " +
                        "  where  1 = 1 and a.permission_status = 'Rejected' ";
                msSQL += " and (c.employee_gid in (" + lblEmployeeGID + ")";

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";

                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    // -------------------parallel flow starts here------------------------------
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                        msSQL += " or (a.permissiondtl_gid in (select permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  permissionapproval_gid) and a.permission_status ='Rejected'))";
                    else
                    {
                        msSQL += "  or a.permissiondtl_gid in (select permissionapproval_gid from (select approved_by, permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' group by  permissionapproval_gid order by permissionapproval_gid desc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = "select distinct permissionapproval_gid from hrm_trn_tapproval";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = " select distinct permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["permissionapproval_gid"].ToString() + "',";
                                }
                                objMySqlDataReader.Close();
                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                    msSQL += " or a.permissiondtl_gid in (" + lsPR_lists + ")";
                                }
                            }
                        }
                    }
                }

                msSQL = msSQL + "and a.permission_status = 'Rejected'  order by date(a.created_date)desc, a.created_date asc, a.permissiondtl_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<permission_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleave.Add(new permission_list
                        {
                            permission_date = (dr_datarow["permission_date"].ToString()),
                            permission_from = (dr_datarow["permission_fromhours"].ToString()),
                            permission_to = (dr_datarow["permission_tohours"].ToString()),
                            permission_duration = (dr_datarow["permission_totalhours"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            permission_reason = (dr_datarow["permission_reason"].ToString()),
                            permission_status = (dr_datarow["permission_status"].ToString()),
                        });
                    }
                    values.permission_list = getleave;
                }
                dt_datatable.Dispose();

                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DapostApprovePermission(string permissiondtl_gid, approvepermission values, string employee_gid)
        {
            try
            {
                
                lsleavestatus = "Approved";
                msSQL =   " update hrm_trn_tapproval set " +
                          " approval_flag = 'Y', " +
                          " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                          " where approved_by = '" + employee_gid + "'" +
                          " and permissionapproval_gid = '" + permissiondtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 0)
                {
                    blncommit = False;
                    objcmnfunctions.LogForAudit("Error Occured While Updating Records" + msSQL);
                }
                //objMySqlDataReader.Close();
                msSQL = " select approval_gid from hrm_trn_tapproval where permissionapproval_gid='" + permissiondtl_gid + "' and approval_flag = 'N'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == false)
                {
                    objMySqlDataReader.Close();
                    msSQL = " update hrm_trn_tpermissiondtl set " +
                                " permission_status = '" + lsleavestatus + "'," +
                                " permission_approvedby = '" + employee_gid + "'," +
                                " permission_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                " updated_by = '" + employee_gid + "'," +
                                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                " where permissiondtl_gid = '" + permissiondtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                    msSQL = " select permission_gid,employee_gid,permission_date, " +
                        " concat(permission_totalhours,':',total_mins,':00') as totaltime " +
                        " from  hrm_trn_tpermissiondtl where  permissiondtl_gid='" + permissiondtl_gid + "'" +
                        "  and permission_status='Approved'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        lsemployeeper_gid = objMySqlDataReader["employee_gid"].ToString();
                        lspermission_date = Convert.ToDateTime(objMySqlDataReader["permission_date"].ToString());
                    }

                    msSQL = " select permission_gid,employee_gid,permission_totalhours,total_mins, " +
                  " concat(permission_totalhours,':',total_mins,':00') as totaltime " +
                  " from  hrm_trn_tpermissiondtl where  employee_gid='" + lsemployeeper_gid + "'" +
                  " and permission_date='" + lspermission_date.ToString("yyyy-MM-dd") + "' and permission_status='Approved'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);

                    if (objMySqlDataReader.HasRows == true)
                    {
                        while (objMySqlDataReader.Read())
                        {
                            lspermissiontime = TimeSpan.Parse(objMySqlDataReader["totaltime"].ToString());
                            lstotalpermissiontime = lstotalpermissiontime.Add(lspermissiontime);
                        }
                        string totalpermissionhr;
                        string totalpermissionmin;
                        string lstotalpermissionhours = Convert.ToString(lstotalpermissiontime.Hours);
                        Double lshrperlength = lstotalpermissionhours.Length;
                        if (Convert.ToString(lshrperlength) == "2")
                        {
                            totalpermissionhr = Convert.ToString(lstotalpermissiontime.Hours);
                        }
                        else
                        {
                            totalpermissionhr = "0" + lstotalpermissiontime.Hours;
                        }

                        string lstotalpermissionmin = Convert.ToString(lstotalpermissiontime.Minutes);
                        Double lshrperminlength = lstotalpermissionmin.Length;
                        if (Convert.ToString(lshrperminlength) == "2")
                        {
                            totalpermissionmin = Convert.ToString(lstotalpermissiontime.Minutes);
                        }
                        else
                        {
                            totalpermissionmin = "0" + lstotalpermissiontime.Minutes;
                        }

                        msSQL = " Update hrm_trn_tpermission set " +
                        " permission_totalhours= '" + totalpermissionhr + "'," +
                        " total_mins= '" + totalpermissionmin + "'," +
                        " permission_status= 'Approved'" +
                        " where  employee_gid='" + lsemployeeper_gid + "'" +
                        " and permission_date='" + lspermission_date.ToString("yyyy-MM-dd") + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {
                    objMySqlDataReader.Close();
                    msSQL = " update hrm_trn_tpermissiondtl set " +
                              " permission_status = 'Approval Pending'" +
                              " where permission_status = '" + permissiondtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                    {
                        objMySqlDataReader.Close();
                        msSQL = " update hrm_trn_tpermissiondtl set " +
                               " permission_status = '" + lsleavestatus + "'," +
                               " permission_approvedby = '" + employee_gid + "'," +
                               " permission_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                               " updated_by = '" + employee_gid + "'," +
                               " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                               " where permissiondtl_gid = '" + permissiondtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update hrm_trn_tapproval set " +
                                " approval_flag = 'Y', " +
                                " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                " where permissionapproval_gid = '" + permissiondtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " select permission_gid,employee_gid,permission_date, " +
                     " concat(permission_totalhours,':',total_mins,':00') as totaltime " +
                     " from  hrm_trn_tpermissiondtl where  permissiondtl_gid='" + permissiondtl_gid + "'" +
                     "  and permission_status='Approved'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            lsemployeeper_gid = objMySqlDataReader["employee_gid"].ToString();
                            lspermission_date = Convert.ToDateTime(objMySqlDataReader["permission_date"].ToString());
                        }

                        msSQL = " select permission_gid,employee_gid,permission_totalhours,total_mins, " +
                                  " concat(permission_totalhours,':',total_mins,':00') as totaltime " +
                                  " from  hrm_trn_tpermissiondtl where  employee_gid='" + lsemployeeper_gid + "'" +
                                  " and permission_date='" + lspermission_date.ToString("yyyy-MM-dd") + "' and permission_status='Approved'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);

                        if (objMySqlDataReader.HasRows == true)
                        {
                            while (objMySqlDataReader.Read())
                            {
                                lspermissiontime = TimeSpan.Parse(objMySqlDataReader["totaltime"].ToString());
                                lstotalpermissiontime = lstotalpermissiontime.Add(lspermissiontime);
                            }
                            string totalpermissionhr;
                            string totalpermissionmin;
                            string lstotalpermissionhours = Convert.ToString(lstotalpermissiontime.Hours);
                            Double lshrperlength = lstotalpermissionhours.Length;
                            if (Convert.ToString(lshrperlength) == "2")
                            {
                                totalpermissionhr = Convert.ToString(lstotalpermissiontime.Hours);
                            }
                            else
                            {
                                totalpermissionhr = "0" + lstotalpermissiontime.Hours;
                            }

                            string lstotalpermissionmin = Convert.ToString(lstotalpermissiontime.Minutes);
                            Double lshrperminlength = lstotalpermissionmin.Length;
                            if (Convert.ToString(lshrperminlength) == "2")
                            {
                                totalpermissionmin = Convert.ToString(lstotalpermissiontime.Minutes);
                            }
                            else
                            {
                                totalpermissionmin = "0" + lstotalpermissiontime.Minutes;
                            }

                            msSQL = " Update hrm_trn_tpermission set " +
                            " permission_totalhours= '" + totalpermissionhr + "'," +
                            " total_mins= '" + totalpermissionmin + "'," +
                            " permission_status= 'Approved'" +
                            " where  employee_gid='" + lsemployeeper_gid + "'" +
                            " and permission_date='" + lspermission_date.ToString("yyyy-MM-dd") + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                    }
                    else
                    {
                        msSQL = " update hrm_trn_tapproval set " +
                       " approval_flag = 'Y', " +
                       " seqhierarchy_view='N', " +
                       " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                       " where approved_by = '" + employee_gid + "'" +
                       " and permissionapproval_gid = '" + permissiondtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update hrm_trn_tapproval set " +
                          " seqhierarchy_view='Y' " +
                          " where approval_flag='N'and approved_by <> '" + employee_gid + "' and permissionapproval_gid = '" + permissiondtl_gid + "'" +
                          " order by hierary_level desc limit 1 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                //'----------------------------------half day condition--------------------------------------'
                msSQL = " select permission_gid,employee_gid,permission_date, " +
                   " concat(permission_totalhours,':',total_mins,':00') as totaltime " +
                   " from  hrm_trn_tpermissiondtl where  permissiondtl_gid='" + permissiondtl_gid + "'" +
                   "  and permission_status='Approved'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lsemployeeper_gid = objMySqlDataReader["employee_gid"].ToString();
                    lspermission_date = Convert.ToDateTime(objMySqlDataReader["permission_date"].ToString());
                }

                msSQL = " select a.halfdayabsent_flag,b.employee_gid,b.permission_date from hrm_trn_tattendance a " +
                        " left join hrm_trn_tpermission b on a.attendance_date=b.permission_date and a.employee_gid=b.employee_gid " +
                        " where b.permission_status='Approved' and " +
                        " timediff(Time(a.logout_time),Time(a.login_time)) <=  '04:00:00' and b.employee_gid = '" + lsemployeeper_gid + "'" +
                        " and b.permission_date='" + lspermission_date.ToString("yyyy-MM-dd") + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapprovepermission = new List<approvepermission_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapprovepermission.Add(new approvepermission_list
                        {
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            attendance_date = (dr_datarow["permission_date"].ToString()),
                        });
                        msSQL = " update hrm_trn_tattendance set " +
                                " halfdayabsent_flag = 'N'" +
                                " where employee_gid = '" + employee_gid + "' and attendance_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.approvepermission_list = getapprovepermission;
                }
                dt_datatable.Dispose();
                //'----------------------------------end half day condition--------------------------------------'
                //'---------------------------------------whatsapp--------------------------------
                msSQL = "select whatsapp_flag from adm_mst_tcompany";
                lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);
                if (lswhatsappflag == "Y")
                {
                    msSQL =         " select b.employee_mobileno,concat(a.permission_totalhours,':',a.total_mins,':00') as permission_total," +
                                    " concat(a.permission_fromhours,':',a.permission_frommins)as fromhours," +
                                    " concat(a.permission_tohours,':',a.permission_tomins)as tohours," +
                                    " concat(c.user_firstname,' ',c.user_lastname) as username,a.permission_reason ," +
                                    " a.permission_date from hrm_trn_tpermissiondtl a" +
                                    " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                    " where a.permissiondtl_gid='" + permissiondtl_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        
                        employee_mobile = (objMySqlDataReader["employee_mobileno"].ToString());
                        employeename = (objMySqlDataReader["username"].ToString());
                        reason = (objMySqlDataReader["permission_reason"].ToString());
                        days = (objMySqlDataReader["permission_total"].ToString());
                        fromdate = (objMySqlDataReader["fromhours"].ToString());
                        todate = (objMySqlDataReader["tohours"].ToString());
                        permission_date = Convert.ToDateTime(objMySqlDataReader["permission_date"].ToString());
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select " +
                                 "concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + employee_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        
                        approved_by = (objMySqlDataReader["username"].ToString());
                    }
                    objMySqlDataReader.Close();

                    try
                    {
                        lsmessage = "Hi " + employeename + "," + approved_by + "  had " + lsleavestatus + " your Permission dated on  " + permission_date.ToString("dd-MM-yyyy") + " from " + fromdate + " to " + todate + " for about " + days + " hours ";
                        objcmnfunctions.sendMessage(employee_mobile, lsmessage);
                    }

                    catch
                    {

                    }

                    objMySqlDataReader.Close();
                }
                //'----------------------------------mail--------------------------------------'
                msSQL = "Select approved_by from hrm_trn_tapproval " +
                    "where approved_by='" + employee_gid + "' and permissionapproval_gid='" + permissiondtl_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {

                    lsapprovedby = objMySqlDataReader["approved_by"].ToString();
                    objMySqlDataReader.Close();
                    msSQL = " select pop_username,pop_password from adm_mst_tcompany where company_gid='1'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {

                        supportmail = objMySqlDataReader["pop_username"].ToString();
                        pwd = objMySqlDataReader["pop_password"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select b.employee_emailid,concat(a.permission_totalhours,':',a.total_mins,':00') as permission_total," +
                                " concat(a.permission_fromhours,':',a.permission_frommins)as fromhours," +
                                " concat(a.permission_tohours,':',a.permission_tomins)as tohours," +
                                " concat(c.user_firstname,' ',c.user_lastname) as username,a.permission_reason ," +
                                " a.permission_date from hrm_trn_tpermissiondtl a" +
                                " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                            " where a.permissiondtl_gid='" + permissiondtl_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {

                        employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        reason = objMySqlDataReader["permission_reason"].ToString();
                        days = objMySqlDataReader["permission_total"].ToString();
                        fromdate = objMySqlDataReader["fromhours"].ToString();
                        todate = objMySqlDataReader["tohours"].ToString();
                        permission_date = Convert.ToDateTime(objMySqlDataReader["permission_date"].ToString());
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select " +
                                 "concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + lsapprovedby + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {

                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();
                    message = "Hi " + employeename + ",  <br />";
                    message = message + "<br />";
                    message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsleavestatus + " your Permission dated on " + permission_date.ToString("dd-MM-yyyy") + " <br />";
                    message = message + "<br />";
                    message = message + "<b>Reason :</b> " + reason + "<br />";
                    message = message + "<br />";
                    message = message + "<b>From Hour :</b> " + fromdate + " &nbsp; &nbsp; <b>To Hour :</b> " + todate + "<br />";
                    message = message + "<br />";
                    message = message + "<b>Total No. of Hours :</b> " + days + " <br />";
                    message = message + "<br />";
                    try
                    {
                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, pwd, employee_mailid, "Your Permission has been " + lsleavestatus + " ", message, "", "", "");
                    }
                    catch
                    {

                    }
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Permission Approved Successfully...!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DapostRejectPermission(string permissiondtl_gid, rejectpermission values, string employee_gid)
        {
            try
            {
                
                lsleavestatus = "Rejected";
                msSQL = " update hrm_trn_tpermissiondtl set " +
                            " permission_status = '" + lsleavestatus + "'," +
                            " permission_approvedby = '" + employee_gid + "'," +
                            " permission_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            " updated_by = '" + employee_gid + "'," +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                            " where permissiondtl_gid = '" + permissiondtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //'----------------------------------end half day condition--------------------------------------'
                //'---------------------------------------whatsapp--------------------------------
                msSQL = "select whatsapp_flag from adm_mst_tcompany";
                lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);
                if (lswhatsappflag == "Y")
                {
                    msSQL = " select b.employee_mobileno,concat(a.permission_totalhours,':',a.total_mins,':00') as permission_total," +
                                    " concat(a.permission_fromhours,':',a.permission_frommins)as fromhours," +
                                    " concat(a.permission_tohours,':',a.permission_tomins)as tohours," +
                                    " concat(c.user_firstname,' ',c.user_lastname) as username,a.permission_reason ," +
                                    " a.permission_date from hrm_trn_tpermissiondtl a" +
                                    " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                " where a.permissiondtl_gid='" + permissiondtl_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        
                        employee_mobile = (objMySqlDataReader["employee_mobileno"].ToString());
                        employeename = (objMySqlDataReader["username"].ToString());
                        reason = (objMySqlDataReader["permission_reason"].ToString());
                        days = (objMySqlDataReader["permission_total"].ToString());
                        fromdate = (objMySqlDataReader["fromhours"].ToString());
                        todate = (objMySqlDataReader["tohours"].ToString());
                        permission_date = Convert.ToDateTime(objMySqlDataReader["permission_date"].ToString());
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select " +
                                 "concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + employee_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        
                        approved_by = (objMySqlDataReader["username"].ToString());
                    }
                    objMySqlDataReader.Close();

                    try
                    {
                        lsmessage = "Hi " + employeename + "," + approved_by + "  had " + lsleavestatus + " your Permission dated on  " + permission_date.ToString("dd-MM-yyyy") + " from " + fromdate + " to " + todate + " for about " + days + " hours ";
                        objcmnfunctions.sendMessage(employee_mobile, lsmessage);
                    }
                    catch
                    {

                    }

                    objMySqlDataReader.Close();
                }
                //'----------------------------------mail--------------------------------------'
                msSQL = "Select approved_by from hrm_trn_tapproval " +
                    "where approved_by='" + employee_gid + "' and permissionapproval_gid='" + permissiondtl_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    while (objMySqlDataReader.Read())
                    {
                        lsapprovedby = objMySqlDataReader["username"].ToString();
                        objMySqlDataReader.Close();
                        msSQL = " select pop_username,pop_password from adm_mst_tcompany where company_gid='1'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            
                            supportmail = objMySqlDataReader["pop_username"].ToString();
                            pwd = objMySqlDataReader["pop_password"].ToString();
                        }
                        objMySqlDataReader.Close();
                        msSQL =     " select b.employee_emailid,concat(a.permission_totalhours,':',a.total_mins,':00') as permission_total," +
                                    " concat(a.permission_fromhours,':',a.permission_frommins)as fromhours," +
                                    " concat(a.permission_tohours,':',a.permission_tomins)as tohours," +
                                    " concat(c.user_firstname,' ',c.user_lastname) as username,a.permission_reason ," +
                                    " a.permission_date from hrm_trn_tpermissiondtl a" +
                                    " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                    " where a.permissiondtl_gid='" + permissiondtl_gid + "'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            
                            employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                            employeename = objMySqlDataReader["username"].ToString();
                            reason = objMySqlDataReader["permission_reason"].ToString();
                            days = objMySqlDataReader["permission_total"].ToString();
                            fromdate = objMySqlDataReader["fromhours"].ToString();
                            todate = objMySqlDataReader["tohours"].ToString();
                            permission_date = Convert.ToDateTime(objMySqlDataReader["permission_date"].ToString());
                        }
                        objMySqlDataReader.Close();
                        msSQL = " select " +
                                     "concat(c.user_firstname,' ',c.user_lastname) as username " +
                                       " from hrm_mst_temployee a " +
                                     " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                       " where a.employee_gid='" + lsapprovedby + "'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            
                            approved_by = objMySqlDataReader["username"].ToString();
                        }
                        objMySqlDataReader.Close();
                        message = "Hi " + employeename + ",  <br />";
                        message = message + "<br />";
                        message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsleavestatus + " your Permission dated on " + permission_date.ToString("dd-MM-yyyy") + " <br />";
                        message = message + "<br />";
                        message = message + "<b>Reason :</b> " + reason + "<br />";
                        message = message + "<br />";
                        message = message + "<b>From Hour :</b> " + fromdate + " &nbsp; &nbsp; <b>To Hour :</b> " + todate + "<br />";
                        message = message + "<br />";
                        message = message + "<b>Total No. of Hours :</b> " + days + " <br />";
                        message = message + "<br />";
                        try
                        {
                            MailFlag = objcmnfunctions.SendSMTP2(supportmail, pwd, employee_mailid, "Your Permission has been " + lsleavestatus + " ", message, "", "", "");

                        }
                        catch
                        {

                        }
                    }
                    objMySqlDataReader.Close();
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Permission Rejected Successfully...!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        //..............................* 6. COMPOFF APPROVAL Details *..............................//
        public bool DaGetCompoffSummaryDetails(getcompoffdetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                        " from adm_mst_tsubmodule a " +
                        " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                lsPR_lists = "";
                msSQL = " select distinct o.compensatoryoff_gid,o.compensatoryoff_status,o.compensatoryoff_reason,date_format(o.actualworking_todate, '%d-%m-%Y') " +
                        " as Compoff_to  ,date_format(o.actualworking_fromdate, '%d-%m-%Y') as actual_working,o.compoff_noofdays, " +
                        " date_format(o.compensatoryoff_applydate, '%d-%m-%Y') as compensatoryoff_applydate, " +
                        " concat(r.user_code, '/', r.user_firstname, ' ', r.user_lastname, '/', q.department_name) as employee_name " +
                         " from hrm_trn_tcompensatoryoff o " +
                         " left join hrm_mst_temployee p on p.employee_gid=o.employee_gid" +
                         " left join hrm_mst_tdepartment q on q.department_gid=p.department_gid" +
                         " left join adm_mst_tuser r on r.user_gid=p.user_gid" +
                         " where o.compensatoryoff_status='Pending'";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (p.employee_gid in (" + lblEmployeeGID + ") and o.employee_gid<>'" + employee_gid + "' ) ";
                }
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (o.compensatoryoff_gid in (select compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  compensatoryoff_gid) and o.compensatoryoff_status ='Approval Pending') ";
                    }
                    else
                    {
                        msSQL += " and o.compensatoryoff_gid in (select compensatoryoff_gid from (select approved_by, " +
                                 " compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + employee_gid + "'" +
                                 " group by  compensatoryoff_gid order by compensatoryoff_gid desc) p where approved_by = '" + employee_gid + "')";
                        msPRSQL = " select distinct compensatoryoff_gid from hrm_trn_tapproval ";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = " select distinct compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' " +
                                      " and approved_by = '" + employee_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                string lscompensatoryoff_gid = objMySqlDataReader["compensatoryoff_gid"].ToString();

                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lscompensatoryoff_gid + "',";

                                }
                                objMySqlDataReader.Close();
                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                            }
                            objMySqlDataReader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or o.compensatoryoff_gid in (" + lsPR_lists + ")";
                            }
                        }
                    }
                }
                msSQL = msSQL + " group by o.compensatoryoff_gid order by" +
                            "  date(o.created_date) desc,o.created_date asc,o.compensatoryoff_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcompoffpending = new List<compoffpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcompoffpending.Add(new compoffpending_list
                        {
                            compensatoryoff_gid = (dr_datarow["compensatoryoff_gid"].ToString()),
                            Compoff_from = (dr_datarow["compensatoryoff_applydate"].ToString()),
                            Compoff_to = (dr_datarow["actual_working"].ToString()),
                            Compoff_duration = (dr_datarow["compoff_noofdays"].ToString()),
                            Compoff_reason = (dr_datarow["compensatoryoff_reason"].ToString()),
                            Compoff_status = (dr_datarow["compensatoryoff_status"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.compoffpending_list = getcompoffpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaGetCompoffLeaveApproveDetails(getleavesummarydetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                        " from adm_mst_tsubmodule a " +
                        " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRMATNALT'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                // od Approved Summary...//
                msSQL = "select distinct o.compensatoryoff_gid,o.compensatoryoff_status,date_format(o.actualworking_fromdate, '%d-%m-%Y') as actualworking_fromdate,o.actualworking_todate, " +
                        "o.compoff_noofdays, o.compensatoryoff_reason,concat(n.user_firstname,n.user_lastname)as employee_name, " +
                        " date_format(o.compensatoryoff_applydate, '%d-%m-%Y') as compensatoryoff_applydate " +
                        " from  hrm_trn_tcompensatoryoff o " +
                        " left join hrm_mst_temployee c on o.employee_gid = c.employee_gid " +
                        " left join hrm_trn_tleavedtl w on o.compensatoryoff_status = w.leave_status " +
                        " left join adm_mst_tuser n on n.user_gid = c.user_gid " +
                        " where  1 = 1 and o.compensatoryoff_status = 'Approved' ";
                if (lblUserCode != "-1")
                    msSQL += " and (c.employee_gid in (" + lblEmployeeGID + ")";

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";

                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    // -------------------parallel flow starts here------------------------------
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                        msSQL += " or (o.compensatoryoff_gid in (select compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  compensatoryoff_gid) and o.compensatoryoff_status ='Approval Pending'))";
                    else
                    {
                        msSQL += "  or o.compensatoryoff_gid in (select compensatoryoff_gid from (select approved_by, compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' group by  compensatoryoff_gid order by compensatoryoff_gid desc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = "select distinct compensatoryoff_gid from hrm_trn_tapproval ";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = "select distinct compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["compensatoryoff_gid"].ToString() + "',";
                                }
                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                    msSQL += " or o.compensatoryoff_gid in (" + lsPR_lists + ")";
                                }

                                objMySqlDataReader.Close();
                            }
                        }
                    }
                }
                objMySqlDataReader.Close();

                msSQL = msSQL + "  and o.compensatoryoff_status = 'Approved' and o.employee_gid <> '" + employee_gid + "' group by o.compensatoryoff_gid order by" +
                                "  date(o.created_date) desc,o.created_date asc,o.compensatoryoff_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<compoffdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleave.Add(new compoffdtl_list
                        {
                            Compoff_from = (dr_datarow["compensatoryoff_applydate"].ToString()),
                            Compoff_to = (dr_datarow["actualworking_fromdate"].ToString()),
                            Compoff_duration = (dr_datarow["compoff_noofdays"].ToString()),
                            Compoff_reason = (dr_datarow["compensatoryoff_reason"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            Compoff_status = (dr_datarow["compensatoryoff_status"].ToString()),

                        });
                    }
                    values.compoffdtl_list = getleave;
                }
                dt_datatable.Dispose();

                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaGetCompoffLeaveRejectDetails(getleavesummarydetails values, string employee_gid)
        {
            try
            {
                
                lblEmployeeGID = employeereporting(employee_gid);

                msSQL = " Select a.hierarchy_level " +
                      " from adm_mst_tsubmodule a " +
                      " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRMATNALT'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lblUserCode = objMySqlDataReader["hierarchy_level"].ToString();
                }
                objMySqlDataReader.Close();

                // Compoff Rejected Summary...//
                msSQL = "select distinct o.compensatoryoff_gid,o.compensatoryoff_status,date_format(o.actualworking_fromdate, '%d-%m-%Y') as actualworking_fromdate,o.actualworking_todate, " +
                        "o.compoff_noofdays, o.compensatoryoff_reason,concat(n.user_firstname,n.user_lastname)as employee_name, " +
                        " date_format(o.compensatoryoff_applydate, '%d-%m-%Y') as compensatoryoff_applydate " +
                        " from  hrm_trn_tcompensatoryoff o " +
                        " left join hrm_mst_temployee c on o.employee_gid = c.employee_gid " +
                        " left join hrm_trn_tleavedtl w on o.compensatoryoff_status = w.leave_status " +
                        " left join adm_mst_tuser n on n.user_gid = c.user_gid " +
                        " where  1 = 1 and o.compensatoryoff_status = 'Rejected' ";
                if (lblUserCode != "-1")
                    msSQL += " and (c.employee_gid in (" + lblEmployeeGID + ")";

                    msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";

                objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    
                    // -------------------parallel flow starts here------------------------------
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                        msSQL += " or (o.compensatoryoff_gid in (select compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "' group by  compensatoryoff_gid) and o.compensatoryoff_status ='Approval Pending'))";
                    else
                    {
                        msSQL += "  or o.compensatoryoff_gid in (select compensatoryoff_gid from (select approved_by, compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' group by  compensatoryoff_gid order by compensatoryoff_gid desc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = "select distinct compensatoryoff_gid from hrm_trn_tapproval ";
                        objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            msPRSQL = "select distinct compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + employee_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msPRSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                while (objMySqlDataReader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objMySqlDataReader["compensatoryoff_gid"].ToString() + "',";
                                }
                                objMySqlDataReader.Close();
                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                    msSQL += " or o.compensatoryoff_gid in (" + lsPR_lists + ")";
                                }
                            }
                        }
                    }
                }
                msSQL = msSQL + "  and o.compensatoryoff_status = 'Rejected' and o.employee_gid <> '" + employee_gid + "' group by o.compensatoryoff_gid order by" +
                            "  date(o.created_date) desc,o.created_date asc,o.compensatoryoff_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<compoffdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleave.Add(new compoffdtl_list
                        {
                            Compoff_from = (dr_datarow["compensatoryoff_applydate"].ToString()),
                            Compoff_to = (dr_datarow["actualworking_fromdate"].ToString()),
                            Compoff_duration = (dr_datarow["compoff_noofdays"].ToString()),
                            Compoff_reason = (dr_datarow["compensatoryoff_reason"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            Compoff_status = (dr_datarow["compensatoryoff_status"].ToString()),

                        });
                    }
                    values.compoffdtl_list = getleave;
                }
                dt_datatable.Dispose();

                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DapostCompoffApprove(approvecompoff values, string employee_gid)
        {
            try
            {
                
                lsleavestatus = "Approved";

                msSQL = " update hrm_trn_tapproval set " +
                         " approval_flag = 'Y', " +
                         " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                         " where approved_by = '" + employee_gid + "'" +
                         " and compensatoryoff_gid = '" + values.compensatoryoff_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 0)
                {
                    values.message = "Error Occured While Updating Records";
                }

                msSQL = " select approval_gid from hrm_trn_tapproval where compensatoryoff_gid='" + values.compensatoryoff_gid + "' and approval_flag = 'N'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == false)
                {
                    objMySqlDataReader.Close();
                    msSQL = " update hrm_trn_tcompensatoryoff set " +
                                " compensatoryoff_status = 'Approved'," +
                                " compensatoryoff_approveby = '" + employee_gid + "'," +
                                " compensatoryoff_approvedate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                                " updated_by = '" + employee_gid + "',  " +
                                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                               " where compensatoryoff_gid = '" + values.compensatoryoff_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "Update hrm_trn_tcompensatoryoffdtl set " +
                                " status='" + lsleavestatus + "'," +
                                " updated_by='" + employee_gid + "'," +
                                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                " where compensatoryoff_gid = '" + values.compensatoryoff_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " Select compensatoryoff_applydate,employee_gid from hrm_trn_tcompensatoryoff where compensatoryoff_gid = '" + values.compensatoryoff_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        lsdate_compoff = Convert.ToDateTime(objMySqlDataReader["compensatoryoff_applydate"].ToString());
                        lsemployee = objMySqlDataReader["employee_gid"].ToString();
                        objMySqlDataReader.Close();
                        msSQL = "Select employee_gid from hrm_trn_tattendance " +
                                "where attendance_date='" + lsdate_compoff.ToString("yyyy-MM-dd") + "' and employee_gid='" + lsemployee + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count > 0)
                        {
                            msSQL = "update hrm_trn_tattendance set " +
                                        "employee_attendance='Compoff', " +
                                        "attendance_type='Compoff', " +
                                        " update_flag='N'" +
                                         "where attendance_date='" + lsdate_compoff.ToString("yyyy-MM-dd") + "' and employee_gid='" + lsemployee + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            msGetGID = objcmnfunctions.GetMasterGID("HATP");
                            msSQL = "Insert Into hrm_trn_tattendance" +
                                    "(attendance_gid," +
                                    " employee_gid," +
                                    " attendance_date," +
                                    " employee_attendance," +
                                    " attendance_type)" +
                                    " VALUES ( " +
                                    "'" + msGetGID + "', " +
                                    "'" + lsemployee + "'," +
                                    "'" + lsdate_compoff.ToString("yyyy-MM-dd") + "'," +
                                    "'Compoff'," +
                                    "'Compoff')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                }

                else
                {
                    objMySqlDataReader.Close();
                    msSQL = " update hrm_trn_tcompensatoryoff set " +
                              " compensatoryoff_status = 'Pending'" +
                              " where compensatoryoff_gid = '" + values.compensatoryoff_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    if (objMySqlDataReader["approval_type"].ToString() == "Parallel")
                    {
                        objMySqlDataReader.Close();

                        msSQL = " update hrm_trn_tcompensatoryoff set " +
                                " compensatoryoff_status = 'Approved'," +
                                " compensatoryoff_approveby = '" + employee_gid + "'," +
                                " compensatoryoff_approvedate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' , " +
                                " updated_by = '" + employee_gid + "',  " +
                                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                               " where compensatoryoff_gid = '" + values.compensatoryoff_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "Update hrm_trn_tcompensatoryoffdtl set " +
                                " status='" + lsleavestatus + "'," +
                                " updated_by='" + employee_gid + "'," +
                                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                " where compensatoryoff_gid = '" + values.compensatoryoff_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update hrm_trn_tapproval set " +
                                " approval_flag = 'Y', " +
                                " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                " where compensatoryoff_gid = '" + values.compensatoryoff_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " Select compensatoryoff_applydate,employee_gid from hrm_trn_tcompensatoryoff where compensatoryoff_gid = '" + values.compensatoryoff_gid + "'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            lsdate_compoff = Convert.ToDateTime(objMySqlDataReader["compensatoryoff_applydate"].ToString());
                            lsemployee = objMySqlDataReader["employee_gid"].ToString();
                            objMySqlDataReader.Close();
                            msSQL = " Select employee_gid from hrm_trn_tattendance " +
                                    " where attendance_date='" + lsdate_compoff.ToString("yyyy-MM-dd") + "' and employee_gid='" + lsemployee + "'";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable.Rows.Count > 0)
                            {
                                msSQL = "update hrm_trn_tattendance set " +
                                        "employee_attendance='Compoff', " +
                                        "attendance_type='Compoff', " +
                                        " update_flag='N'" +
                                         "where attendance_date='" + lsdate_compoff.ToString("yyyy-MM-dd") + "' and employee_gid='" + lsemployee + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else
                            {
                                msGetGID = objcmnfunctions.GetMasterGID("HATP");
                                msSQL = "Insert Into hrm_trn_tattendance" +
                                         "(attendance_gid," +
                                         " employee_gid," +
                                         " attendance_date," +
                                         " employee_attendance," +
                                         " attendance_type)" +
                                         " VALUES ( " +
                                         "'" + msGetGID + "', " +
                                         "'" + lsemployee + "'," +
                                         "'" + lsdate_compoff.ToString("yyyy-MM-dd") + "'," +
                                         "'Compoff'," +
                                         "'Compoff')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            dt_datatable.Dispose();
                        }
                    }
                    else
                    {

                        msSQL = " update hrm_trn_tapproval set " +
                                " approval_flag = 'Y', " +
                                " seqhierarchy_view='N', " +
                                " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                " where compensatoryoff_gid = '" + values.compensatoryoff_gid + "' and  approved_by = '" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update hrm_trn_tapproval set " +
                            " seqhierarchy_view='Y' " +
                            " where approval_flag='N'and approved_by <> '" + employee_gid + "' and compensatoryoff_gid = '" + values.compensatoryoff_gid + "'" +
                            " order by hierary_level desc limit 1 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    }
                }
                objMySqlDataReader.Close();

                //---------------------------------------whatsapp--------------------------------//

                msSQL = "select whatsapp_flag from adm_mst_tcompany";
                lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);

                if (lswhatsappflag == "Y")
                {
                    msSQL = " select b.employee_mobileno,(date_format(a.actualworking_fromdate,'%d/%m/%Y')) as actualworking_fromdate, " +
                                           "(date_format(a.actualworking_todate,'%d/%m/%Y')) as actualworking_todate, " +
                                           "(date_format(a.compensatoryoff_applydate,'%d/%m/%Y')) as compensatoryoff_applydate, " +
                                           " Concat(c.user_firstname,' ',c.user_lastname) as username,a.compensatoryoff_reason,a.compoff_noofdays " +
                                           " from hrm_trn_tcompensatoryoff a " +
                                           " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                           " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                            " where a.compensatoryoff_gid='" + values.compensatoryoff_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        employee_mobile = objMySqlDataReader["employee_mobileno"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        reason = objMySqlDataReader["compensatoryoff_reason"].ToString();
                        compofffromdate = objMySqlDataReader["compensatoryoff_applydate"].ToString();
                        actualworkingdate = objMySqlDataReader["actualworking_fromdate"].ToString();
                        days = objMySqlDataReader["compoff_noofdays"].ToString();
                    }
                    objMySqlDataReader.Close();

                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + employee_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();
                    try
                    {
                        lsmessage = "Hi " + employeename + ",   " + approved_by + "  had " + lsleavestatus + "  your Compensatory Off  on " + compofffromdate + " since you have worked on  " + actualworkingdate + "  ";

                        objcmnfunctions.sendMessage(employee_mobile, lsmessage);
                    }
                    catch
                    {
                        values.message = "Compensatory off Approved Successfully,but Message Not Send. ";
                    }

                }

                //----------------------------------mail-------------------------------------//

                msSQL = "Select approved_by from hrm_trn_tapproval " +
                    "where approved_by='" + employee_gid + "' and compensatoryoff_gid='" + values.compensatoryoff_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lsapprovedby = objMySqlDataReader["approved_by"].ToString();
                    objMySqlDataReader.Close();
                    msSQL = " select pop_username,pop_password from adm_mst_tcompany where company_gid='1'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        supportmail = objMySqlDataReader["pop_username"].ToString();
                        pwd = objMySqlDataReader["pop_password"].ToString();
                    }
                    objMySqlDataReader.Close();

                    if (supportmail != "")
                    {
                        msSQL = " select b.employee_emailid,(date_format(a.actualworking_fromdate,'%d/%m/%Y')) as actualworking_fromdate, " +
                                                "(date_format(a.actualworking_todate,'%d/%m/%Y')) as actualworking_todate, " +
                                                 "(date_format(a.compensatoryoff_applydate,'%d/%m/%Y')) as compensatoryoff_applydate, " +
                                                " Concat(c.user_firstname,' ',c.user_lastname) as username,a.compensatoryoff_reason,a.compoff_noofdays " +
                                                " from hrm_trn_tcompensatoryoff a " +
                                                " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                                " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                 " where a.compensatoryoff_gid='" + values.compensatoryoff_gid + "'";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {
                            employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                            employeename = objMySqlDataReader["username"].ToString();
                            reason = objMySqlDataReader["compensatoryoff_reason"].ToString();
                            compofffromdate = objMySqlDataReader["compensatoryoff_applydate"].ToString();
                            actualworkingdate = objMySqlDataReader["actualworking_fromdate"].ToString();
                            days = objMySqlDataReader["compoff_noofdays"].ToString();
                        }
                        objMySqlDataReader.Close();
                        if (employee_mailid != "")
                        {
                            msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as username " +
                            " from hrm_mst_temployee a " +
                            " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                            " where a.employee_gid='" + lsapprovedby + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                approved_by = objMySqlDataReader["username"].ToString();
                            }
                            objMySqlDataReader.Close();

                            message = "Hi " + employeename + ",  <br />";
                            message = message + "<br />";
                            message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsleavestatus + " your Compensatory Off applied on " + compofffromdate + " <br />";
                            message = message + "<br />";
                            message = message + "<b>Reason :</b> " + reason + " <br />";
                            message = message + "<br />";
                            message = message + "<b>Actual Working Date :</b> " + actualworkingdate + " <br />";
                            message = message + "<br />";

                            try
                            {
                                MailFlag = objcmnfunctions.SendSMTP2(supportmail, pwd, employee_mailid, "Your Compensatory Off has been " + lsleavestatus + " ", message, "", "", "");
                            }
                            catch
                            {
                                values.message = "Mail Not Send. ";
                            }
                        }
                    }
                }
                objMySqlDataReader.Close();

                if (mnResult != 0)
                {
                    values.message = "Compensatory Off Approved Successfully";
                    return true;
                }
                else
                {
                    values.message = "Error Occured...!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DapostRejectCompoff(string compensatoryoff_gid, rejectcompoff values, string employee_gid)
        {
            try
            {
                
                lsleavestatus = "Rejected";
                msSQL = " update hrm_trn_tcompensatoryoff set " +
                            " compensatoryoff_status = '" + lsleavestatus + "'," +
                            " compensatoryoff_approveby = '" + employee_gid + "'," +
                            " compensatoryoff_approvedate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                            " where compensatoryoff_gid = '" + compensatoryoff_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                //'----------------------------------end half day condition--------------------------------------'
                //'---------------------------------------whatsapp--------------------------------


                msSQL = "select whatsapp_flag from adm_mst_tcompany";
                lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);
                if (lswhatsappflag == "Y")
                {

                    msSQL = " select b.employee_mobileno,(date_format(a.actualworking_fromdate,'%d/%m/%Y')) as actualworking_fromdate, " +
                                           "(date_format(a.actualworking_todate,'%d/%m/%Y')) as actualworking_todate, " +
                                           "(date_format(a.compensatoryoff_applydate,'%d/%m/%Y')) as compensatoryoff_applydate, " +
                                           " Concat(c.user_firstname,' ',c.user_lastname) as username,a.compensatoryoff_reason,a.compoff_noofdays " +
                                           " from hrm_trn_tcompensatoryoff a " +
                                           " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                           " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                           " where a.compensatoryoff_gid='" + compensatoryoff_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        
                        employee_mobile = (objMySqlDataReader["employee_mobileno"].ToString());
                        employeename = (objMySqlDataReader["username"].ToString());
                        reason = (objMySqlDataReader["compensatoryoff_reason"].ToString());
                        days = (objMySqlDataReader["compoff_noofdays"].ToString());
                        compofffromdate = (objMySqlDataReader["compensatoryoff_applydate"].ToString());
                        actualworkingdate = (objMySqlDataReader["actualworking_fromdate"].ToString());
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select " +
                                 "concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + employee_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        
                        approved_by = (objMySqlDataReader["username"].ToString());
                    }
                    objMySqlDataReader.Close();

                    try
                    {
                        lsmessage = "Hi " + employeename + ",   " + approved_by + "  had " + lsleavestatus + "  your Compensatory Off  on " + compofffromdate + " ";
                        objcmnfunctions.sendMessage(employee_mobile, lsmessage);
                    }

                    catch
                    {

                    }

                    objMySqlDataReader.Close();
                }


                //'----------------------------------mail--------------------------------------'
                msSQL = "Select approved_by from hrm_trn_tapproval " +
                    "where approved_by='" + employee_gid + "' and compensatoryoff_gid='" + compensatoryoff_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lsapprovedby = objMySqlDataReader["approved_by"].ToString();
                    objMySqlDataReader.Close();
                    msSQL = " select pop_username,pop_password from adm_mst_tcompany where company_gid='1'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        
                        supportmail = objMySqlDataReader["pop_username"].ToString();
                        pwd = objMySqlDataReader["pop_password"].ToString();
                    }
                    objMySqlDataReader.Close();

                    msSQL = " select b.employee_emailid,(date_format(a.actualworking_fromdate,'%d/%m/%y')) as actualworking_fromdate, " +
                                        " (date_format(a.actualworking_todate,'%d/%m/%Y')) as actualworking_todate, " +
                                        "(date_format(a.compensatoryoff_applydate,'%d/%m/%Y')) as compensatoryoff_applydate, " +
                                        " Concat(c.user_firstname,' ',c.user_lastname) as username,a.compensatoryoff_reason,a.compoff_noofdays " +
                                        " from hrm_trn_tcompensatoryoff a " +
                                        " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " +
                                        " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                        " where a.compensatoryoff_gid='" + compensatoryoff_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        
                        employee_mailid = objMySqlDataReader["employee_emailid"].ToString();
                        employeename = objMySqlDataReader["username"].ToString();
                        reason = objMySqlDataReader["compensatoryoff_reason"].ToString();
                        days = objMySqlDataReader["compoff_noofdays"].ToString();
                        actualworkingdate = objMySqlDataReader["actualworking_fromdate"].ToString();
                        compofffromdate = objMySqlDataReader["actualworking_todate"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " select " +
                                 "concat(c.user_firstname,' ',c.user_lastname) as username " +
                                   " from hrm_mst_temployee a " +
                                 " left join adm_mst_tuser c on a.user_gid=c.user_gid " +
                                   " where a.employee_gid='" + lsapprovedby + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        
                        approved_by = objMySqlDataReader["username"].ToString();
                    }
                    objMySqlDataReader.Close();
                    message = "Hi " + employeename + ",  <br />";
                    message = message + "<br />";
                    message = message + "Mr/Mrs/Ms " + approved_by + " had " + lsleavestatus + " your Compensatory Off applied on " + compofffromdate + " <br />";
                    message = message + "<br />";
                    message = message + "<b>Reason :</b> " + reason + "<br />";
                    message = message + "<br />";
                    message = message + "<b>Actual Working Date :</b> " + actualworkingdate + "<br />";
                    message = message + "<br />";
                    message = message + "<b>No.Of.Days :</b> " + days + " <br />";
                    message = message + "<br />";
                    try
                    {
                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, pwd, employee_mailid, "Your Compensatory Off has been " + lsleavestatus + " ", message, "", "", "");

                    }

                    catch (Exception ex)
                    {
                        values.message = ex.Message;
                    }
                }

                objMySqlDataReader.Close();

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Compoff Rejected Successfully...!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured...!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        // Function Call....//
        public string Getleave(string employee_gid, string branch_gid, string startdate, string enddate, String leave_month, String leave_year)
        {
            try
            {
                
                msSQL = " select attendance_startdate,attendance_enddate " +
                    " from adm_mst_tcompany ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow objtblemploteedatarow in dt_datatable.Rows)
                    {
                        attendance_lsdate = Convert.ToDateTime(objtblemploteedatarow["attendance_startdate"]);
                        attendance_lsenddate = Convert.ToDateTime(objtblemploteedatarow["attendance_enddate"]);
                    }
                }


                if (employee_gid != "")
                {
                    msSQL = " select a.employee_gid,a.leavegrade_gid,a.leavegrade_code,a.leavegrade_name,b.leavetype_gid,c.leavetype_name," +
                        " b.total_leavecount, b.available_leavecount, b.leave_limit,c.carryforward,c.accrud " +
                        " from hrm_trn_tleavegrade2employee a " +
                        " left join hrm_mst_tleavegradedtl b on a.leavegrade_gid = b.leavegrade_gid " +
                        " left join hrm_mst_tleavetype c on c.leavetype_gid = b.leavetype_gid " +
                        " where a.employee_gid='" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow objtblemploteedatarow in dt_datatable.Rows)
                        {
                            lsleave_available = 0.0;
                            lscarry = objtblemploteedatarow["carryforward"].ToString();
                            lsaccrual = objtblemploteedatarow["accrud"].ToString();
                            ls_limit = 0.0;
                            lsleavegrade_name = objtblemploteedatarow["leavegrade_name"].ToString();
                            lsleavegrade_gid = objtblemploteedatarow["leavegrade_gid"].ToString();
                            lsleavetype_gid = objtblemploteedatarow["leavetype_gid"].ToString();
                            leave_name = objtblemploteedatarow["leavetype_name"].ToString();
                            lsavailable_leave = Convert.ToDouble(objtblemploteedatarow["available_leavecount"]);
                            ls_limit = Convert.ToDouble(objtblemploteedatarow["leave_limit"].ToString());
                            lstotal_leave = Convert.ToDouble(objtblemploteedatarow["total_leavecount"].ToString());
                            if (lsaccrual == "N")
                            {
                                msSQL = " SELECT sum(b.leave_count) as totalleave FROM hrm_trn_tleave a " +
                                    " left join hrm_trn_tleavedtl b on a.leave_gid = b.leave_gid " +
                                    " where a.leavetype_gid = '" + lsleavetype_gid + "' " +
                                    " and a.employee_gid = '" + employee_gid + "' " +
                                    " and a.leave_status = 'Approved' " +
                                    " and a.leave_todate <= '" + attendance_lsenddate.ToString("yyyy-MM-dd") + "' and leave_todate >= '" + attendance_lsdate.ToString("yyyy-MM-dd") + "'" +
                                    " group by a.leavetype_gid ";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                if (dt_datatable.Rows.Count != 0)
                                {
                                    foreach (DataRow objTblrow in dt_datatable.Rows)
                                    {
                                        if (objTblrow["totalleave"] == null)
                                        {
                                            leave_taken = 0.0;
                                        }
                                        else
                                        {
                                            leave_taken = Convert.ToDouble(objTblrow["totalleave"]);
                                            lsleave_available = lstotal_leave - leave_taken;
                                        }

                                    }
                                }

                                else
                                {
                                    leave_taken = 0.0;
                                    lsleave_available = lstotal_leave - leave_taken;
                                }

                            }
                            else if (lsaccrual == "Y")
                            {
                                msSQL = " SELECT sum(b.leave_count) as totalleave " +
                                   " FROM hrm_trn_tleave a " +
                                   " left join hrm_trn_tleavedtl b on a.leave_gid = b.leave_gid " +
                                   " where a.leavetype_gid = '" + lsleavetype_gid + "' " +
                                   " and a.employee_gid = '" + employee_gid + "' " +
                                   " and a.leave_status = 'Approved' " +
                                   " and date_format(a.leave_todate,'%Y-%m') <= '" + DateTime.Now.ToString("yyyy-MM") + "' and leave_todate >= '" + attendance_lsdate.ToString("yyyy-MM-dd") + "'" +
                                   " group by a.leavetype_gid";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                if (dt_datatable.Rows.Count != 0)
                                {
                                    foreach (DataRow objTblrow in dt_datatable.Rows)
                                    {
                                        if (objTblrow["totalleave"] == null)
                                        {
                                            leave_taken = 0.0;
                                        }
                                        else
                                        {
                                            leave_taken = Convert.ToDouble(objTblrow["totalleave"]);
                                        }

                                    }
                                }
                                else
                                {
                                    leave_taken = 0.0;
                                }
                            }
                            if (lsaccrual == "Y")
                            {
                                if (lscarry == "N")
                                {
                                    msSQL = " select sum(leavecarry_count) as  leavecarry_count from hrm_mst_tleavecreditsdtl " +
                                            " where employee_gid='" + employee_gid + "' " +
                                            " and leavetype_gid='" + lsleavetype_gid + "' and year='" + leave_year + "'" +
                                            " and month <= '" + leave_month + "'";
                                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                    if (objMySqlDataReader.HasRows == true)
                                    {
                                        if (objMySqlDataReader["leavecarry_count"] == null)
                                        {

                                        }
                                        else
                                        {
                                            lscarry_count = Convert.ToDouble(objMySqlDataReader["leavecarry_count"]);
                                        }

                                    }
                                    objMySqlDataReader.Close();
                                    lsavailable_leave = lscarry_count - leave_taken;

                                    msSQL = " update hrm_mst_tleavecreditsdtl set " +
                                            " leave_taken='" + leave_taken + "', " +
                                            " available_leavecount='" + lsavailable_leave + "' " +
                                            " where employee_gid='" + employee_gid + "' " +
                                            " and leavetype_gid='" + lsleavetype_gid + "'" +
                                            " and month ='" + leave_month + "' and year='" + leave_year + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            if (lsaccrual == "N")
                            {
                                if (lscarry == "N")
                                {
                                    msSQL = " select sum(leavecarry_count) as  leavecarry_count from hrm_mst_tleavecreditsdtl " +
                                            " where employee_gid='" + employee_gid + "' " +
                                            " and leavetype_gid='" + lsleavetype_gid + "' and year='" + leave_year + "'" +
                                            " and month <= '" + leave_month + "'";
                                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                    if (objMySqlDataReader.HasRows == true)
                                    {
                                        lscarry_count = Convert.ToDouble(objMySqlDataReader["leavecarry_count"]);
                                    }
                                    objMySqlDataReader.Close();
                                    lsavailable_leave = lstotal_leave - leave_taken;
                                    msSQL = " update hrm_mst_tleavecreditsdtl set " +
                                            " leave_taken='" + leave_taken + "', " +
                                            " available_leavecount='" + lsavailable_leave + "' " +
                                            " where employee_gid='" + employee_gid + "'" +
                                            " and leavetype_gid='" + lsleavetype_gid + "' " +
                                            " and month ='" + leave_month + "' and year='" + leave_year + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }


                            }
                        }
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return "";
            }
        }

        public string employeereporting(string employee_gid)
        {
            try
            {
                
                var lsemployeeGID = objcmnfunctions.childloop(employee_gid);

                if (lsemployeeGID == "" || lsemployeeGID == null)
                {
                    lblEmployeeGID = "'" + employee_gid + "'";

                }
                else
                {
                    lsemployeeGID = lsemployeeGID.TrimEnd(',');
                    lblEmployeeGID = lsemployeeGID;

                }

                return lblEmployeeGID;
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return "";
            }
        }
        public bool DaGetLeaveDocument(string leave_gid, uploaddocuments values)
        {
            try
            {
                
                msSQL = "select leave_gid,document_name,document_path from hrm_trn_tleave where leave_gid='" + leave_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocument = new List<uploaddocumentlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if ((dr_datarow["document_name"].ToString() != "") || (dr_datarow["document_path"].ToString() != ""))
                        {
                            getdocument.Add(new uploaddocumentlist
                            {
                                documentname = (dr_datarow["document_name"].ToString()),
                                path = (dr_datarow["document_path"].ToString()),
                                tmpdocument_gid = (dr_datarow["leave_gid"].ToString())
                            });
                            values.uploaddocumentlist = getdocument;
                        }
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
    }
}