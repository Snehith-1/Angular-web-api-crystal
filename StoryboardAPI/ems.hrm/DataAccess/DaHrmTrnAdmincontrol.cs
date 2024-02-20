using System;
using System.Collections.Generic;
using System.Linq;
using ems.hrm.Models;
using ems.utilities.Functions;
using System.Data;
using System.Configuration;
using System.Data.Odbc;

using System.IO;
using System.Web;
using OfficeOpenXml;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Globalization;
using MySql.Data.MySqlClient;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.hrm.DataAccess
{
    public class DaHrmTrnAdmincontrol
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable, objtbl;
        int mnResult, mnResult1,importcount;
        string lsleavegrade_code, lsleavegrade_name, lsattendance_startdate, lsattendance_enddate, lsleavetype_gid, lsleavetype_name, lstotal_leavecount, lsavailable_leavecount, lsleave_limit, lsholidaygrade_gid, lsholiday_gid, lsholiday_date;
        string msUserGid, msEmployeeGID, msBiometricGID, msGetemployeetype, msTemporaryAddressGetGID, msPermanentAddressGetGID, usercode, lsuser_gid, lsemployee_gid, lsuser_code, lscountry_gid2, lscountry_gid, msGetGIDN;
        HttpPostedFile httpPostedFile;
        string lstemcountry_gid, msdocument_gid, lspcountry_gid, lsentity_gid, lsdepartment_gid, lsbranch_gid, uppercasedbvalue, lsdesignation_gid;
        int ErrorCount;
        public void DaGetEmployeedtlSummary(string user_gid,MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , " +
                   " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                   " c.employee_gender,  " +
                   " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                   " d.designation_name,c.designation_gid,c.employee_gid,e.branch_name, " +
                   " CASE " +
                   " WHEN a.user_status = 'Y' THEN 'Active'  " +
                   " WHEN a.user_status = 'N' THEN 'Inactive' " +
                   " END as user_status,c.department_gid,c.branch_gid, e.branch_name, g.department_name " +
                   " FROM adm_mst_tuser a " +
                   " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                   " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                   " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                   " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                   " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                   " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                   " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                   " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid " +
                   " group by c.employee_gid " +
                   " order by c.employee_gid desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<employee_list10>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new employee_list10
                        {

                            user_gid = dt["user_gid"].ToString(),
                            useraccess = dt["useraccess"].ToString(),
                            entity_name = dt["entity_name"].ToString(),
                            user_code = dt["user_code"].ToString(),
                            user_name = dt["user_name"].ToString(),
                            employee_joiningdate = dt["employee_joiningdate"].ToString(),
                            employee_gender = dt["employee_gender"].ToString(),
                            emp_address = dt["emp_address"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                            designation_gid = dt["designation_gid"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            user_status = dt["user_status"].ToString(),
                            department_gid = dt["department_gid"].ToString(),
                            department_name = dt["department_name"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),

                        });
                        values.employee_list = getModuleList;
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
        public void DaGetDocumentlist(string user_gid, MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " select a.uploadexcellog_gid,concat(b.user_firstname, b.user_lastname) as updated_by," +
                   " a.uploaded_date,a.importcount from hrm_trn_temployeeuploadexcellog a " +
                   " left join adm_mst_tuser b on b.user_gid = a.uploaded_by";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<document_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new document_list
                        {

                            document_name = dt["uploadexcellog_gid"].ToString(),
                            updated_by = dt["updated_by"].ToString(),
                            uploaded_date = dt["uploaded_date"].ToString(),
                            importcount = dt["importcount"].ToString(),


                        });
                        values.document_list = getModuleList;
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
        public void DaGetDocumentDtllist(string document_gid, MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " select first_name,last_name,user_code,remarks from hrm_trn_temployeeuploadexcelerrorlog " +
                    " where uploadexcellog_gid = '" + document_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<documentdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new documentdtl_list
                        {

                            user_code = dt["user_code"].ToString(),
                            first_name = dt["first_name"].ToString(),
                            last_name = dt["last_name"].ToString(),
                            remarks = dt["remarks"].ToString(),


                        });
                        values.documentdtl_list = getModuleList;
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
        public void DaPostEmployeedetails(employee_lists values, string user_gid)
        {
            try
            {
                
                msSQL = "select bloodgroup_name from sys_mst_tbloodgroup where bloodgroup_gid='" + values.bloodgroup + "'";
                string lsbloodgroupname = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT user_code FROM adm_mst_tuser where user_code = '" + values.user_code + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);

                if (objMySqlDataReader.HasRows)
                {
                    lsuser_code = objMySqlDataReader["user_code"].ToString();
                    values.status = false;
                    return;
                }
                string joiningdate = values.empjoiningdate;
                DateTime uiDate = DateTime.ParseExact(joiningdate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string mysqjoiningdate = uiDate.ToString("yyyy-MM-dd");

                string uppercaseString = values.user_code.ToUpper();
                if (uppercaseString != lsuser_code)
                {
                    msUserGid = objcmnfunctions.GetMasterGID("SUSM");

                    msSQL = " insert into adm_mst_tuser(" +
                    " user_gid," +
                    " user_code," +
                    " user_firstname," +
                    " user_lastname, " +
                    " user_password, " +
                    " user_status, " +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msUserGid + "'," +
                    " '" + values.user_code + "'," +
                    " '" + values.first_name + "'," +
                    " '" + values.last_name + "'," +
                    " '" + objcmnfunctions.ConvertToAscii(values.password) + "'," +
                    "'" + values.active_flag + "',";
                    msSQL += "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        msEmployeeGID = objcmnfunctions.GetMasterGID("SERM");
                        msBiometricGID = objcmnfunctions.GetBiometricGID();
                        msSQL1 = " Insert into hrm_mst_temployee " +
                            " (employee_gid , " +
                            " user_gid," +
                            " designation_gid," +
                            " employee_mobileno , " +
                            " employee_personalno , " +
                            " employee_dob , " +
                            " employee_emailid , " +
                            " employee_gender , " +
                            " department_gid," +
                            " entity_gid," +
                            " employee_photo," +
                            " employee_qualification," +
                            " age," +
                            " role_gid," +
                            " bloodgroup_name," +
                            " employee_joiningdate," +
                            " useraccess," +
                            " engagement_type," +
                            " attendance_flag, " +
                            " identity_no, " +
                            " branch_gid, " +
                            " biometric_id, " +
                            " created_by, " +
                            " created_date " +
                            " )values( " +
                            "'" + msEmployeeGID + "', " +
                            "'" + msUserGid + "', " +
                            "'" + values.designationname + "'," +
                            "'" + values.mobileno + "'," +
                            "'" + values.mobile + "'," +
                            "'" + values.dob + "'," +
                            "'" + values.email + "'," +
                            "'" + values.gender + "'," +
                            "'" + values.departmentname + "'," +
                            "'" + values.entityname + "'," +
                            "'" + null + "'," +
                            "'" + values.qualification + "'," +
                            "'" + values.age + "'," +
                            "'" + values.role + "'," +
                            "'" + lsbloodgroupname + "'," +
                            "'" + mysqjoiningdate + "'," +
                            "'" + values.active_flag + "'," +
                            "'Direct'," +
                            "'Y'," +
                            " '" + values.aadhar_no + "'," +
                            " '" + values.branchname + "'," +
                            "'" + msBiometricGID + "'," +
                            "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL1);

                        if (mnResult == 1)
                        {
                            msSQL =  " update  hrm_mst_temployee set " +
                                     " employee_photo = '/assets/media/images/Employee_defaultimage.png'" +
                                     "  where employee_gid='" + msEmployeeGID + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            msGetemployeetype = objcmnfunctions.GetMasterGID("SETD");
                            msSQL = " insert into hrm_trn_temployeetypedtl(" +
                          " employeetypedtl_gid," +
                          " employee_gid," +
                          " workertype_gid," +
                          " systemtype_gid, " +
                          " branch_gid, " +
                          " wagestype_gid, " +
                          " department_gid, " +
                          " employeetype_name, " +
                          " designation_gid, " +
                          " created_by, " +
                          " created_date)" +
                          " values(" +
                          " '" + msGetemployeetype + "'," +
                          " '" + msEmployeeGID + "'," +
                          " 'null'," +
                          " 'Audit'," +
                          " '" + values.branchname + "'," +
                          " 'wg001'," +
                          " '" + values.departmentname + "'," +
                          "'Roll'," +
                          " '" + values.designationname + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            //shift assignment starts
                            if (mnResult == 1)
                            {
                                string msGetGID_shift = objcmnfunctions.GetMasterGID("HESC");
                                msSQL = " insert into hrm_trn_temployee2shifttype( " +
                                " employee2shifttype_gid, " +
                                " employee_gid,  " +
                                " shifttype_gid, " +
                                " shifteffective_date ," +
                                " shiftactive_flag ," +
                                " created_by," +
                                " created_date) " +
                                " values( " +
                                "'" + msGetGID_shift + "'," +
                                "'" + msEmployeeGID + "'," +
                                "'" + values.shift + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                "'Y'," +
                                "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult == 1)
                                {
                                    msSQL = " select * from hrm_trn_temployee2shifttypedtl " +
                                    " where employee_gid='" + msEmployeeGID + "' " +
                                    " and shifttype_gid='" + values.shift + "' ";
                                    objtbl = objdbconn.GetDataTable(msSQL);
                                    if (objtbl.Rows.Count > 0)
                                    {
                                        msSQL = " update hrm_trn_temployee2shifttypedtl set shift_status='Y' where employee_gid='" + msEmployeeGID + "' " +
                                               " and shifttype_gid='" + values.shift + "' ";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    else
                                    {
                                        msSQL = " select a.shifttypedtl_gid,a.shifttype_gid,a.shifttypedtl_name,a.shift_fromhours,a.shift_tohours, " +
                                               " a.lunchout_hours,a.lunchout_minutes,a.lunchin_hours,a.lunchin_minutes, " +
                                               " a.shift_fromminutes, a.shift_tominutes from hrm_mst_tshifttypedtl a " +
                                               " inner join hrm_mst_tshifttype b on a.shifttype_gid=b.shifttype_gid " +
                                               " where b.shifttype_gid='" + values.shift + "' ";
                                        objtbl = objdbconn.GetDataTable(msSQL);
                                        if (objtbl.Rows.Count > 0)
                                        {
                                            foreach (DataRow dt in objtbl.Rows)
                                            {
                                                msGetGIDN = objcmnfunctions.GetMasterGID("HEST");
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
                                                " '" + msGetGIDN + "', " +
                                                " '" + msGetGID_shift + "', " +
                                                " '" + values.shift + "', " +
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
                                                " '" + msEmployeeGID + "'," +
                                                "'" + user_gid + "'," +
                                                "'Y'," +
                                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                            }
                                        }
                                    }
                                }
                                //shift assignmentends
                                msSQL = " select  c.leavetype_name,b.leavetype_gid,a.leavegrade_gid ,a.leavegrade_code, a.leavegrade_name ,c.leavetype_name ,a.attendance_startdate,a.attendance_enddate, " +
                                        " format(sum(b.total_leavecount),2)as total_leavecount , format(sum(b.available_leavecount),2)as available_leavecount, " +
                                        " format(sum(b.leave_limit),2)as leave_limit from hrm_mst_tleavegrade a " +
                                        " left join hrm_mst_tleavegradedtl b on a.leavegrade_gid=b.leavegrade_gid  " +
                                        " left join hrm_mst_tleavetype c on b.leavetype_gid=c.leavetype_gid where a.leavegrade_gid='" + values.leavegrade + "' group by a.leavegrade_gid ";

                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                if (objMySqlDataReader.HasRows == true)
                                {
                                    lsleavegrade_code = objMySqlDataReader["leavegrade_code"].ToString();
                                    lsleavegrade_name = objMySqlDataReader["leavegrade_name"].ToString();
                                    lsattendance_startdate = objMySqlDataReader["attendance_startdate"].ToString();
                                    lsattendance_enddate = objMySqlDataReader["attendance_enddate"].ToString();
                                    lsleavetype_gid = objMySqlDataReader["leavetype_gid"].ToString();
                                    lsleavetype_name = objMySqlDataReader["leavetype_name"].ToString();
                                    lstotal_leavecount = objMySqlDataReader["total_leavecount"].ToString();
                                    lsavailable_leavecount = objMySqlDataReader["available_leavecount"].ToString();
                                    lsleave_limit = objMySqlDataReader["leave_limit"].ToString();
                                    string msgetassign2employee_gid = objcmnfunctions.GetMasterGID("LE2G");

                                    msSQL = " insert into hrm_trn_tleavegrade2employee ( " +
                                            " leavegrade2employee_gid," +
                                            " branch_gid ," +
                                            " employee_gid," +
                                            " employee_name," +
                                            " leavegrade_gid," +
                                            " leavegrade_code," +
                                            " leavegrade_name, " +
                                            " attendance_startdate, " +
                                            " attendance_enddate, " +
                                            " total_leavecount, " +
                                            " available_leavecount, " +
                                            " leave_limit " +
                                            " ) Values ( " +
                                            " '" + msgetassign2employee_gid + "', " +
                                            " '" + values.branchname + "', " +
                                            " '" + msEmployeeGID + "', " +
                                            " '" + values.first_name + "', " +
                                            " '" + values.leavegrade + "', " +
                                            " '" + lsleavegrade_code + "'," +
                                            " '" + lsleavegrade_name + "'," +
                                            " '" + Convert.ToDateTime(lsattendance_startdate).ToString("yyyy-MM-dd") + "'," +
                                            " '" + Convert.ToDateTime(lsattendance_enddate).ToString("yyyy-MM-dd") + "'," +
                                            " '" + lstotal_leavecount + "'," +
                                            " '" + lsavailable_leavecount + "'," +
                                            " '" + lsleave_limit + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                msSQL = "select a.holidaygrade_gid,b.holiday_gid,b.holiday_date from hrm_mst_tholidaygrade a " +
                                       " left join hrm_mst_tholiday2grade b on b.holidaygrade_gid=a.holidaygrade_gid " +
                                       " where a.holidaygrade_gid='" + values.holidaygrade + "' ";
                                DataTable objtblgrade = objdbconn.GetDataTable(msSQL);
                                if (objtblgrade.Rows.Count > 0)
                                {
                                    foreach (DataRow dt in objtblgrade.Rows)
                                    {
                                        lsholidaygrade_gid = dt["holidaygrade_gid"].ToString();
                                        lsholiday_gid = dt["holiday_gid"].ToString();
                                        lsholiday_date = dt["holiday_date"].ToString();

                                        string msGetGID = objcmnfunctions.GetMasterGID("HYTE");
                                        msSQL =    " insert into hrm_mst_tholiday2employee ( " +
                                                   " holiday2employee, " +
                                                   " holidaygrade_gid, " +
                                                   " holiday_gid, " +
                                                   " employee_gid, " +
                                                   " holiday_date, " +
                                                   " created_by, " +
                                                   " created_date ) " +
                                                   " values ( " +
                                                   "'" + msGetGID + "', " +
                                                   "'" + lsholidaygrade_gid + "', " +
                                                   " '" + lsholiday_gid + "'," +
                                                   " '" + msEmployeeGID + "', " +
                                                   "'" + Convert.ToDateTime(lsholiday_date).ToString("yyyy-MM-dd") + "', " +
                                                   "'" + user_gid + "', " +
                                                   "'" + DateTime.Now.ToString("yyyy-MM-dd") + "') ";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }
                                msPermanentAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
                                msTemporaryAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
                                msSQL = " insert into adm_mst_taddress(" +
                                        " address_gid," +
                                        " parent_gid," +
                                        " country_gid," +
                                        " address1, " +
                                        " address2, " +
                                        " city, " +
                                        " state, " +
                                        " address_type, " +
                                        " postal_code, " +
                                        " created_by, " +
                                        " created_date)" +
                                        " values(" +
                                        " '" + msPermanentAddressGetGID + "'," +
                                        " '" + msEmployeeGID + "'," +
                                        " '" + values.country + "'," +
                                        " '" + values.permanent_address1 + "'," +
                                        " '" + values.permanent_address2 + "'," +
                                        " '" + values.permanent_city + "'," +
                                        " '" + values.permanent_state + "'," +
                                        " 'Permanent'," +
                                        "'" + values.permanent_postal + "',";
                                msSQL += "'" + user_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult == 1)
                                {
                                    msSQL = " insert into adm_mst_taddress(" +
                                            " address_gid," +
                                            " parent_gid," +
                                            " country_gid," +
                                            " address1, " +
                                            " address2, " +
                                            " city, " +
                                            " state, " +
                                            " address_type, " +
                                            " postal_code, " +
                                            " created_by, " +
                                            " created_date)" +
                                            " values(" +
                                            " '" + msTemporaryAddressGetGID + "'," +
                                            " '" + msEmployeeGID + "'," +
                                            " '" + values.countryname + "'," +
                                            " '" + values.temporary_address1 + "'," +
                                            " '" + values.temporary_address2 + "'," +
                                            " '" + values.temporary_city + "'," +
                                            " '" + values.temporary_state + "'," +
                                            " 'Temporary'," +
                                            "'" + values.temporary_postal + "',";
                                    msSQL += "'" + user_gid + "'," +
                                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                        }
                    }

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Employee Added Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Employee !!";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Employee User Code Already Exist";
                }
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaGetentitydropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " select  entity_gid, entity_name " +
                    " from adm_mst_tentity a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by entity_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getentitydropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getentitydropdown
                        {
                            entity_gid = dt["entity_gid"].ToString(),
                            entity_name = dt["entity_name"].ToString(),

                        });
                        values.Getentitydropdown = getModuleList;
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
        public void DaGetbloodgroupdropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " SELECT bloodgroup_name,bloodgroup_gid FROM sys_mst_tbloodgroup";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getbloodgroupdropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getbloodgroupdropdown
                        {
                            bloodgroup_gid = dt["bloodgroup_gid"].ToString(),
                            bloodgroup_name = dt["bloodgroup_name"].ToString(),

                        });
                        values.Getbloodgroupdropdown = getModuleList;
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
        public void DaGetdesignationdropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " Select designation_name,designation_gid  " +
                    " from adm_mst_tdesignation ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getdesignationdropdown1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getdesignationdropdown1
                        {
                            designation_name = dt["designation_name"].ToString(),
                            designation_gid = dt["designation_gid"].ToString(),
                        });
                        values.Getdesignationdropdown = getModuleList;
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
        public void DaGetcountrydropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " Select country_name,country_gid  " +
                    " from adm_mst_tcountry ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getcountrydropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getcountrydropdown
                        {
                            country_name = dt["country_name"].ToString(),
                            country_gid = dt["country_gid"].ToString(),
                        });
                        values.Getcountrydropdown = getModuleList;
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
        public void DaGetcountry2dropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " Select country_name as country_names,country_gid as country_gids  " +
                        " from adm_mst_tcountry ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getcountry2dropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getcountry2dropdown
                        {
                            country_names = dt["country_names"].ToString(),
                            country_gids = dt["country_gids"].ToString(),
                        });
                        values.Getcountry2dropdown = getModuleList;
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
        public void DaGetbranchdropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " Select branch_name,branch_gid  " +
                    " from hrm_mst_tbranch ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getbranchdropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getbranchdropdown
                        {
                            branch_name = dt["branch_name"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),
                        });
                        values.Getbranchdropdown = getModuleList;
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
        public void DaGetworkertypedropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " select workertype_gid,workertype_name  " +
                    " from hrm_mst_tworkertype ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getworkertypedropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getworkertypedropdown
                        {
                            workertype_name = dt["workertype_name"].ToString(),
                            workertype_gid = dt["workertype_gid"].ToString(),
                        });
                        values.Getworkertypedropdown = getModuleList;
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
        public void DaGetroledropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " select role_gid,role_name  " +
                    " from hrm_mst_trole ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getroledropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getroledropdown
                        {
                            role_name = dt["role_name"].ToString(),
                            role_gid = dt["role_gid"].ToString(),
                        });
                        values.Getroledropdown = getModuleList;
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
        public void DaGetholidaygradedropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " select holidaygrade_gid,holidaygrade_name  " +
                    " from hrm_mst_tholidaygrade ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getholidaygradedropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getholidaygradedropdown
                        {
                            holidaygrade_name = dt["holidaygrade_name"].ToString(),
                            holidaygrade_gid = dt["holidaygrade_gid"].ToString(),
                        });
                        values.Getholidaygradedropdown = getModuleList;
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
        public void DaGetjobtypedropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " select jobtype_gid,jobtype_name  " +
                    " from hrm_mst_tjobtype ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getjobtypenamedropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getjobtypenamedropdown
                        {
                            jobtype_name = dt["jobtype_name"].ToString(),
                            jobtype_gid = dt["jobtype_gid"].ToString(),
                        });
                        values.Getjobtypenamedropdown = getModuleList;
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
        public void DaGetshifttypedropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " select shifttype_name,shifttype_gid  " +
                    " from hrm_mst_tshifttype ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getshifttypenamedropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getshifttypenamedropdown
                        {
                            shifttype_name = dt["shifttype_name"].ToString(),
                            shifttype_gid = dt["shifttype_gid"].ToString(),
                        });
                        values.Getshifttypenamedropdown = getModuleList;
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
        public void DaGetleavegradedropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " select leavegrade_name,leavegrade_gid  " +
                    " from hrm_mst_tleavegrade ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getleavegradenamedropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getleavegradenamedropdown
                        {
                            leavegrade_name = dt["leavegrade_name"].ToString(),
                            leavegrade_gid = dt["leavegrade_gid"].ToString(),
                        });
                        values.Getleavegradenamedropdown = getModuleList;
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
        public void DaGetdepartmentdropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " Select department_name,department_gid  " +
                    " from hrm_mst_tdepartment ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getdepartmentdropdown1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getdepartmentdropdown1
                        {
                            department_name = dt["department_name"].ToString(),
                            department_gid = dt["department_gid"].ToString(),
                        });
                        values.Getdepartmentdropdown = getModuleList;
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
        public void DaGetreportingtodropdown(MdlHrmTrnAdmincontrol values)
        {
            try
            {
                
                msSQL = " select distinct  a.employee_gid, concat(b.user_firstname,' ',b.user_lastname) as employee_name  from hrm_mst_temployee a" +
                   " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                   " left join adm_mst_tmodule2employee c on c.employee_gid= a.employee_gid where c.hierarchy_level>0";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getreportingtodropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getreportingtodropdown
                        {
                            employee_name = dt["employee_name"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                        });
                        values.Getreportingtodropdown = getModuleList;
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
        public void DaEmployeeProfileUpload(HttpRequest httpRequest, result objResult, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            HttpPostedFile httpPostedFile;

            string lspath;
            string msGetGid;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            string entity = httpRequest.Form[0];
            string branch = httpRequest.Form[1];
            string department = httpRequest.Form[2];
            string designation = httpRequest.Form[3];
            string active_flag = httpRequest.Form[4];
            string user_code = httpRequest.Form[5];
            string password = httpRequest.Form[6];
            string first_name = httpRequest.Form[8];
            string last_name = httpRequest.Form[9];
            string gender = httpRequest.Form[10];
            string email = httpRequest.Form[11];
            string mobile = httpRequest.Form[12];
            string permanent_address1 = httpRequest.Form[14];
            string permanent_address2 = httpRequest.Form[14];
            string country = httpRequest.Form[15];
            string permanent_city = httpRequest.Form[16];
            string permanent_state = httpRequest.Form[17];
            string permanent_postal = httpRequest.Form[18];
            string temporary_address1 = httpRequest.Form[19];
            string temporary_address2 = httpRequest.Form[20];
            string countryname = httpRequest.Form[21];
            string temporary_city = httpRequest.Form[22];
            string temporary_state = httpRequest.Form[23];
            string temporary_postal = httpRequest.Form[24];

            MemoryStream ms = new MemoryStream();
            lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Employee/Profile/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Employee/Profile/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        string status;
                        status = objcmnfunctions.uploadFile(lspath + msdocument_gid, FileExtension);
                        //string local_path = "E:/Angular15/AngularUI/src";
                        ms.Close();
                        lspath = "/assets/media/images/erpdocument" + "/" + lscompany_code + "/" + "Employee/Profile/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        string final_path = lspath + msdocument_gid + FileExtension;
                        msSQL = " SELECT user_code FROM adm_mst_tuser where user_code = '" + user_code + "' ";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows)
                        {
                            lsuser_code = objMySqlDataReader["user_code"].ToString();
                        }
                        if (lsuser_code != null && lsuser_code != "")
                        {
                            lsuser_code = lsuser_code.ToUpper();
                        }
                        else
                        {
                            lsuser_code = null;

                        }
                        //string usercode =lsuser_code.ToUpper();
                        string uppercaseString = user_code.ToUpper();
                        if (uppercaseString != lsuser_code)
                        {
                            msUserGid = objcmnfunctions.GetMasterGID("SUSM");

                            msSQL = " insert into adm_mst_tuser(" +
                            " user_gid," +
                            " user_code," +
                            " user_firstname," +
                            " user_lastname, " +
                            " user_password, " +
                            " user_status, " +
                            " created_by, " +
                            " created_date)" +
                            " values(" +
                            " '" + msUserGid + "'," +
                            " '" + user_code + "'," +
                            " '" + first_name + "'," +
                            " '" + last_name + "'," +
                            " '" + objcmnfunctions.ConvertToAscii(password) + "'," +
                            "'" + active_flag + "',";
                            msSQL += "'" + user_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult == 1)
                            {
                                msEmployeeGID = objcmnfunctions.GetMasterGID("SERM");
                               
                                msBiometricGID = objcmnfunctions.GetBiometricGID();
                                msSQL1 = " Insert into hrm_mst_temployee " +
                                    " (employee_gid , " +
                                    " user_gid," +
                                    " designation_gid," +
                                    " employee_mobileno , " +
                                    " employee_emailid , " +
                                    " employee_gender , " +
                                    " department_gid," +
                                    " entity_gid," +
                                    " employee_photo," +
                                    " useraccess," +
                                    " engagement_type," +
                                    " attendance_flag, " +
                                    " branch_gid, " +
                                    " biometric_id, " +
                                    " created_by, " +
                                    " created_date " +
                                    " )values( " +
                                    "'" + msEmployeeGID + "', " +
                                    "'" + msUserGid + "', " +
                                    "'" + designation + "'," +
                                    "'" + mobile + "'," +
                                    "'" + email + "'," +
                                    "'" + gender + "'," +
                                    "'" + department + "'," +
                                    "'" + entity + "'," +
                                    "'" + final_path + "'," +
                                    "'" + active_flag + "'," +
                                    "'Direct'," +
                                    "'Y'," +
                                    " '" + branch + "'," +
                                    "'" + msBiometricGID + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL1);
                                if (mnResult == 1)
                                {

                                    msGetemployeetype = objcmnfunctions.GetMasterGID("SETD");
                                    msSQL = " insert into hrm_trn_temployeetypedtl(" +
                                  " employeetypedtl_gid," +
                                  " employee_gid," +
                                  " workertype_gid," +
                                  " systemtype_gid, " +
                                  " branch_gid, " +
                                  " wagestype_gid, " +
                                  " department_gid, " +
                                  " employeetype_name, " +
                                  " designation_gid, " +
                                  " created_by, " +
                                  " created_date)" +
                                  " values(" +
                                  " '" + msGetemployeetype + "'," +
                                  " '" + msEmployeeGID + "'," +
                                  " 'null'," +
                                  " 'Audit'," +
                                  " '" + branch + "'," +
                                  " 'wg001'," +
                                  " '" + department + "'," +
                                    "'Roll'," +
                                  " '" + designation + "'," +
                                   "'" + user_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    if (mnResult == 1)
                                    {
                                        
                                        msPermanentAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
                                        msTemporaryAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
                                        msSQL = " insert into adm_mst_taddress(" +
                                    " address_gid," +
                                    " parent_gid," +
                                    " country_gid," +
                                    " address1, " +
                                    " address2, " +
                                    " city, " +
                                    " state, " +
                                    " address_type, " +
                                    " postal_code, " +
                                    " created_by, " +
                                    " created_date)" +
                                    " values(" +
                                    " '" + msPermanentAddressGetGID + "'," +
                                    " '" + msEmployeeGID + "'," +
                                    " '" + country + "'," +
                                    " '" + permanent_address1 + "'," +
                                    " '" + permanent_address2 + "'," +
                                    " '" + permanent_city + "'," +
                                    " '" + permanent_state + "'," +
                                    "'Permanent'," +
                                    "'" + permanent_postal + "',";
                                        msSQL += "'" + user_gid + "'," +
                                                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        if (mnResult == 1)
                                        {
                                           
                                            msSQL = " insert into adm_mst_taddress(" +
                                    " address_gid," +
                                    " parent_gid," +
                                    " country_gid," +
                                    " address1, " +
                                    " address2, " +
                                    " city, " +
                                    " state, " +
                                    " address_type, " +
                                    " postal_code, " +
                                    " created_by, " +
                                    " created_date)" +
                                    " values(" +
                                    " '" + msTemporaryAddressGetGID + "'," +
                                    " '" + msEmployeeGID + "'," +
                                    " '" + countryname + "'," +
                                    " '" + temporary_address1 + "'," +
                                    " '" + temporary_address2 + "'," +
                                    " '" + temporary_city + "'," +
                                    " '" + temporary_state + "'," +
                                    "'Temporary'," +
                                    "'" + temporary_postal + "',";
                                            msSQL += "'" + user_gid + "'," +
                                                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }

                                    }


                                }
                            }

                            if (mnResult != 0)
                            {
                                objResult.status = true;
                                objResult.message = "Employee Added Successfully !!";
                            }
                            else
                            {
                                objResult.status = false;
                                objResult.message = "Error While Adding Employee !!";
                            }
                        }
                        else
                        {
                            objResult.status = false;
                            objResult.message = "Employee User Code Already Exist !!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objResult.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaGetEditEmployeeSummary(string employee_gid, MdlHrmTrnAdmincontrol values)
        {
            try
            {

                msSQL = " select a.employee_gid,a.employee_gender,a.biometric_id,a.employee_diffabled,a.age,a.role_gid,a.employee_personalno,z.entity_name," +
                   " a.employeereporting_to, a.employeereporting_to,a.jobtype_gid,j.workertype_gid, a.identity_no,date_format(a.employee_dob,'%d-%m-%Y') as employee_dob,a.employee_sign,a.bloodgroup_name,t.employeepreviouscompany_name, " +
                   " a.passport_no,q.role_name,b.user_password,a.fin_no,a.workpermit_no,date_format(a.passport_expiredate,'%d-%m-%Y') as passport_expiredate, a.branch_gid,a.father_name, " +
                   " date_format(a.workpermit_expiredate,'%d-%m-%Y') as workpermit_expiredate,a.employeepreviouscompany_gid,l.perhour_rate,l.permonth_rate," +
                   " date_format(a.finno_expiredate,'%d-%m-%Y')as finno_expiredate, a.department_gid, m.daysalary_rate as perdayrate," +
                   " concat(b.user_firstname, ' ', b.user_lastname)as employee_name,a.employee_photo,b.user_gid,date_format(a.employee_joiningdate,'%d-%m-%Y') as employee_joiningdate,b.regional_username, " +
                   " h.employeetype_name,i.wagetype_name,j.workertype_name,k.roll_name,a.employee_tagid,h.wagestype_gid,h.employeetype_name,  " +
                   " case when a.employee_hideattendance='Y' then 'Yes' else 'No' end as emp_hideattendance, " +
                   " a.employee_emailid,a.employee_companyemailid,a.employee_mobileno,a.employee_personalno,a.employee_qualification,a.employee_documents,a.remarks, " +
                   " a.employee_experience,a.employee_experiencedtl, a.employeereporting_to , a.employment_type ," +
                   " b.user_code,b.user_firstname,b.user_lastname,y.leavegrade_name, h.workertype_gid, a.designation_gid, b.user_status,b.usergroup_gid,c.usergroup_code, " +
                   " d.branch_name,  e.department_name,f.designation_name, g.jobtype_name,s.section_name,a.section_gid, " +
                   " (select address2 from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Permanent') as permanent_address2, " +
                   " (select city from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Permanent') as permanent_city, " +
                   " (select state from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Permanent') as permanent_state, " +
                   " (select postal_code from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Permanent') as permanent_postalcode, " +
                   " (select address_gid from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Permanent') as permanent_addressgid, " +
                   " (select n.country_name from adm_mst_taddress m LEFT JOIN adm_mst_tcountry n ON m.country_gid = n.country_gid   where parent_gid = '" + employee_gid + "' and address_type = 'Permanent') as permanent_country, " +
                   " (select r.country_gid from adm_mst_taddress q LEFT JOIN adm_mst_tcountry r ON q.country_gid = r.country_gid   where parent_gid = '" + employee_gid + "' and address_type = 'Permanent') as permanent_countrygid, " +
                   " (select address1 from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Temporary') as temporary_address1, " +
                   " (select address2 from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Temporary') as temporary_address2, " +
                   " (select city from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Temporary') as temporary_city, " +
                   " (select state from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Temporary') as temporary_state, " +
                   " (select postal_code from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Temporary') as temporary_postalcode, " +
                   " (select address_gid from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Temporary') as temporary_addressgid, " +
                   " (select p.country_name from adm_mst_taddress o LEFT JOIN adm_mst_tcountry p ON o.country_gid = p.country_gid   where parent_gid = '" + employee_gid + "' and address_type = 'Temporary') as temporary_country, " +
                   " (select t.country_gid from adm_mst_taddress s LEFT JOIN adm_mst_tcountry t ON s.country_gid = t.country_gid   where parent_gid = '" + employee_gid + "' and address_type = 'Temporary') as temporary_countrygid, " +
                   " (select address1 from adm_mst_taddress where parent_gid = '" + employee_gid + "' and address_type = 'Permanent') as permanent_address1, " +
                   " (select i.user_firstname from adm_mst_tuser i ,  hrm_mst_temployee j where i.user_gid = j.user_gid " +
                   " and a.employeereporting_to = j.employee_gid)  as approveby_name,a.rolltype_gid, " +
                   " a.nationality,a.nric_no,a.skill_set " +
                   " FROM hrm_mst_temployee a  LEFT JOIN adm_mst_tuser b ON a.user_gid = b.user_gid " +
                   " left join pay_trn_temployee2wage m on a.employee_gid = m.employee_gid " +
                   " LEFT JOIN pay_mst_tdaysalarymaster l on m.daysalary_gid = l.daysalary_gid" +
                   " LEFT JOIN adm_mst_tusergroup c ON b.usergroup_gid = c.usergroup_gid " +
                   " LEFT JOIN hrm_mst_tbranch d ON a.branch_gid = d.branch_gid " +
                   " LEFT JOIN hrm_mst_tdepartment e ON a.department_gid = e.department_gid  " +
                   " LEFT JOIN adm_mst_tdesignation f ON a.designation_gid = f.designation_gid " +
                   " LEFT JOIN hrm_mst_tjobtype g ON a.jobtype_gid = g.jobtype_gid " +
                   " LEFT JOIN hrm_trn_temployeetypedtl h ON a.employee_gid = h.employee_gid" +
                   " LEFT JOIN hrm_mst_twagestype i ON h.wagestype_gid = i.wagestype_gid" +
                   " LEFT JOIN hrm_mst_tworkertype j ON h.workertype_gid = j.workertype_gid" +
                   " LEFT JOIN hrm_mst_temployeerolltype k ON h.systemtype_gid = k.systemtype_name" +
                   " LEFT JOIN hrm_mst_tsection s ON a.section_gid= s.section_gid " +
                   " left join adm_mst_tentity z on z.entity_gid=a.entity_gid " +
                   " left join hrm_mst_trole q on q.role_gid = a.role_gid " +
                   " left join hrm_trn_tleavegrade2employee y on y.employee_gid = a.employee_gid " +
                   " left join hrm_mst_temployeepreviouscompany t on a.employeepreviouscompany_gid = t.employeepreviouscompany_gid" +
                   " WHERE a.employee_gid = '" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetEditEmployeeSummary>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetEditEmployeeSummary
                        {
                            employee_gid = dt["employee_gid"].ToString(),
                            employee_gender = dt["employee_gender"].ToString(),
                            entity_name = dt["entity_name"].ToString(),
                            identity_no = dt["identity_no"].ToString(),
                            employee_dob = dt["employee_dob"].ToString(),
                            employee_sign = dt["employee_sign"].ToString(),
                            bloodgroup_name = dt["bloodgroup_name"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),
                            jobtype_gid = dt["jobtype_gid"].ToString(),
                            department_gid = dt["department_gid"].ToString(),
                            designation_gid = dt["designation_gid"].ToString(),
                            employee_photo = dt["employee_photo"].ToString(),
                            employee_emailid = dt["employee_emailid"].ToString(),
                            employee_companyemailid = dt["employee_companyemailid"].ToString(),
                            employee_mobileno = dt["employee_mobileno"].ToString(),
                            employee_personalno = dt["employee_personalno"].ToString(),
                            employee_documents = dt["employee_documents"].ToString(),
                            employee_experience = dt["employee_experience"].ToString(),
                            employee_experiencedtl = dt["employee_experiencedtl"].ToString(),
                            employeereporting_to = dt["employeereporting_to"].ToString(),
                            employment_type = dt["employment_type"].ToString(),
                            user_code = dt["user_code"].ToString(),
                            user_firstname = dt["user_firstname"].ToString(),
                            user_lastname = dt["user_lastname"].ToString(),
                            user_status = dt["user_status"].ToString(),
                            usergroup_gid = dt["usergroup_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            department_name = dt["department_name"].ToString(),
                            approveby_name = dt["approveby_name"].ToString(),
                            employee_joiningdate = dt["employee_joiningdate"].ToString(),
                            nationality = dt["nationality"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                            role_name = dt["role_name"].ToString(),
                            workertype_gid = dt["workertype_gid"].ToString(),
                            jobtype_name = dt["jobtype_name"].ToString(),
                            workertype_name = dt["workertype_name"].ToString(),
                            biometric_id = dt["biometric_id"].ToString(),
                            user_password = dt["user_password"].ToString(),
                            employee_name = dt["employee_name"].ToString(),
                            leavegrade_name = dt["leavegrade_name"].ToString(),
                            father_name = dt["father_name"].ToString(),
                            employee_qualification = dt["employee_qualification"].ToString(),
                            employee_diffabled = dt["employee_diffabled"].ToString(),
                            age = dt["age"].ToString(),
                            permanent_countrygid = dt["permanent_countrygid"].ToString(),
                            temporary_countrygid = dt["temporary_countrygid"].ToString(),
                            nric_no = dt["nric_no"].ToString(),
                            permanent_address1 = dt["permanent_address1"].ToString(),
                            permanent_address2 = dt["permanent_address2"].ToString(),
                            permanent_city = dt["permanent_city"].ToString(),
                            permanent_state = dt["permanent_state"].ToString(),
                            permanent_postalcode = dt["permanent_postalcode"].ToString(),
                            permanent_country = dt["permanent_country"].ToString(),
                            permanent_addressgid = dt["permanent_addressgid"].ToString(),
                            temporary_address1 = dt["temporary_address1"].ToString(),
                            temporary_address2 = dt["temporary_address2"].ToString(),
                            temporary_city = dt["temporary_city"].ToString(),
                            temporary_postalcode = dt["temporary_postalcode"].ToString(),
                            temporary_country = dt["temporary_country"].ToString(),
                            temporary_state = dt["temporary_state"].ToString(),
                            temporary_addressgid = dt["temporary_addressgid"].ToString(),

                        });
                        values.GetEditEmployeeSummary = getModuleList;
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
        public void DaUpdateEmployeeProfileUpload(HttpRequest httpRequest, result objResult, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            HttpPostedFile httpPostedFile;

            string lspath;
            string msGetGid;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            string entity = httpRequest.Form[0];
            string branch = httpRequest.Form[1];
            string department = httpRequest.Form[2];
            string designation = httpRequest.Form[3];
            string active_flag = httpRequest.Form[4];
            string user_code = httpRequest.Form[5];
            string first_name = httpRequest.Form[6];
            string last_name = httpRequest.Form[7];
            string gender = httpRequest.Form[8];
            string email = httpRequest.Form[9];
            string mobile = httpRequest.Form[10];
            string permanent_address1 = httpRequest.Form[11];
            string permanent_address2 = httpRequest.Form[12];
            string country = httpRequest.Form[13];
            string permanent_city = httpRequest.Form[14];
            string permanent_state = httpRequest.Form[15];
            string permanent_postal = httpRequest.Form[16];
            string temporary_address1 = httpRequest.Form[17];
            string temporary_address2 = httpRequest.Form[18];
            string countryname = httpRequest.Form[19];
            string temporary_city = httpRequest.Form[20];
            string temporary_state = httpRequest.Form[21];
            string temporary_postal = httpRequest.Form[22];
            string permanent_addressgid = httpRequest.Form[23];
            string temporary_addressgid = httpRequest.Form[24];
            string employee_gid = httpRequest.Form[25];
            MemoryStream ms = new MemoryStream();
            lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Employee/Profile/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Employee/Profile/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        string status;
                        status = objcmnfunctions.uploadFile(lspath + msdocument_gid, FileExtension);
                        //string local_path = "E:/Angular15/AngularUI/src";
                        ms.Close();
                        lspath = "assets/media/images/erpdocument" + "/" + lscompany_code + "/" + "Employee/Profile/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        string final_path = lspath + msdocument_gid + FileExtension;

                        msSQL = " SELECT user_gid FROM hrm_mst_temployee where employee_gid = '" + employee_gid + "' ";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows)
                        {
                            lsuser_gid = objMySqlDataReader["user_gid"].ToString();
                        }
                        msSQL =    " update  adm_mst_tuser set " +
                                   " user_firstname = '" + first_name + "'," +
                                   " user_lastname = '" + last_name + "'," +
                                   " user_status = '" + active_flag + "'," +
                                   " updated_by = '" + user_gid + "'," +
                                   " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where user_gid='" + lsuser_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = " update  hrm_mst_temployee set " +
                                    " designation_gid = '" + designation + "'," +
                                    " employee_mobileno = '" + mobile + "'," +
                                    " employee_emailid = '" + email + "'," +
                                    " employee_gender = '" + gender + "'," +
                                    " department_gid = '" + department + "'," +
                                    " employee_photo = '" + final_path + "'," +
                                    " entity_gid = '" + entity + "'," +
                                    " useraccess = '" + active_flag + "'," +
                                    " branch_gid = '" + branch + "'," +
                                    " updated_by = '" + user_gid + "'," +
                                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where employee_gid='" + employee_gid + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult == 1)
                            {
                                msSQL = " update hrm_trn_temployeetypedtl set " +
                                " wagestype_gid='wg001', " +
                                " systemtype_gid='Audit', " +
                                " branch_gid='" + branch + "', " +
                                " employeetype_name='Roll', " +
                                " department_gid='" + department + "', " +
                                " designation_gid='" + designation + "', " +
                                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " updated_by='" + user_gid + "'" +
                                " where employee_gid = '" + employee_gid + "' ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult == 1)
                                {                                   
                                    msSQL = " update adm_mst_taddress SET " +
                                            " country_gid = '" + country + "', " +
                                            " address1 =  '" + permanent_address1 + "', " +
                                            " address2 = '" + permanent_address2 + "'," +
                                            " city = '" + permanent_city + "', " +
                                            " state = '" + permanent_state + "', " +
                                            " postal_code = '" + permanent_postal + "'" +
                                            " where address_gid = '" + permanent_addressgid + "' and " +
                                            " parent_gid = '" + employee_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    if (mnResult == 1)
                                    {
                                        msSQL = " update adm_mst_taddress SET " +
                                                " country_gid = '" + countryname + "', " +
                                                " address1 =  '" + temporary_address1 + "', " +
                                                " address2 = '" + temporary_address2 + "'," +
                                                " city = '" + temporary_city + "', " +
                                                " state = '" + temporary_state + "', " +
                                                " postal_code = '" + temporary_postal + "'" +
                                                " where address_gid = '" + temporary_addressgid + "' and " +
                                                " parent_gid = '" + employee_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }
                            }
                        }
                        if (mnResult != 0)
                        {
                            objResult.status = true;
                            objResult.message = "Employee Updated Successfully !!";
                        }
                        else
                        {
                            objResult.status = false;
                            objResult.message = "Error While Updating  Employee !!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objResult.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        //update employee details from edit page
        public void DaUpdateEmployeedetails(employee_lists values, string user_gid)
        {
            try
            {
                msSQL = " SELECT user_gid FROM hrm_mst_temployee where employee_gid = '" + values.employee_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lsuser_gid = objMySqlDataReader["user_gid"].ToString();
                }

                msSQL = " update  adm_mst_tuser set " +
                                   " user_firstname = '" + values.first_name + "'," +
                                   " user_code = '" + values.user_code + "'," +
                                   " user_lastname = '" + values.last_name + "'," +
                                   " user_status = '" + values.active_flag + "'," +
                                   " updated_by = '" + user_gid + "'," +
                                   " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where user_gid='" + lsuser_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = " update  hrm_mst_temployee set " +
                                    " designation_gid = '" + values.designationname + "'," +
                                    " jobtype_gid = '" + values.jobtype + "'," +
                                    " employee_joiningdate = '" + values.empjoiningdate + "'," +
                                    " bloodgroup_name = '" + values.bloodgroup + "'," +
                                    " employee_mobileno = '" + values.mobile + "'," +
                                    " employee_emailid = '" + values.email + "'," +
                                    " employee_gender = '" + values.gender + "'," +
                                    " department_gid = '" + values.departmentname + "'," +
                                    " entity_gid = '" + values.entityname + "'," +
                                    " father_name = '" + values.father_spouse + "'," +
                                    " useraccess = '" + values.active_flag + "'," +
                                    " branch_gid = '" + values.branchname + "'," +
                                    " updated_by = '" + user_gid + "'," +
                                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where employee_gid='" + values.employee_gid + "'  ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msSQL = " update hrm_trn_temployeetypedtl set " +
                                         " wagestype_gid='wg001', " +
                                         " systemtype_gid='Audit', " +
                                         " branch_gid='" + values.branchname + "', " +
                                         " workertype_gid='" + values.workertype + "', " +
                                         " employeetype_name='Roll', " +
                                         " department_gid='" + values.departmentname + "', " +
                                         " designation_gid='" + values.designationname + "', " +
                                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                         " updated_by='" + user_gid + "'" +
                                         " where employee_gid = '" + values.employee_gid + "' ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {
                            msSQL = "select parent_gid from adm_mst_taddress where parent_gid='" + values.employee_gid + "' and address_type='Permanent' ";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            if (objMySqlDataReader.HasRows)
                            {
                                msSQL = " update adm_mst_taddress SET " +
                                              " country_gid = '" + values.country + "', " +
                                              " address1 =  '" + values.permanent_address1 + "', " +
                                              " address2 = '" + values.permanent_address2 + "'," +
                                              " city = '" + values.permanent_city + "', " +
                                              " state = '" + values.permanent_state + "', " +
                                              " postal_code = '" + values.permanent_postal + "'" +
                                              " where address_gid = '" + values.permanent_addressgid + "' and " +
                                              " parent_gid = '" + values.employee_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                            else
                            {
                                msPermanentAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
                                msSQL = " insert into adm_mst_taddress(" +
                            " address_gid," +
                            " parent_gid," +
                            " country_gid," +
                            " address1, " +
                            " address2, " +
                            " city, " +
                            " state, " +
                            " address_type, " +
                            " postal_code, " +
                            " created_by, " +
                            " created_date)" +
                            " values(" +
                            " '" + msPermanentAddressGetGID + "'," +
                            " '" + values.employee_gid + "'," +
                            " '" + values.country + "'," +
                            " '" + values.permanent_address1 + "'," +
                            " '" + values.permanent_address2 + "'," +
                            " '" + values.permanent_city + "'," +
                            " '" + values.permanent_state + "'," +
                            " 'Permanent'," +
                            "'" + values.permanent_postal + "',";
                                msSQL += "'" + user_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }

                            if (mnResult == 1)
                            {
                                msSQL = "select parent_gid from adm_mst_taddress where parent_gid='" + values.employee_gid + "' and address_type='Temporary' ";
                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                if (objMySqlDataReader.HasRows)
                                {
                                    msSQL = " update adm_mst_taddress SET " +
                                                 " country_gid = '" + values.countryname + "', " +
                                                 " address1 =  '" + values.temporary_address1 + "', " +
                                                 " address2 = '" + values.temporary_address2 + "'," +
                                                 " city = '" + values.temporary_city + "', " +
                                                 " state = '" + values.temporary_state + "', " +
                                                 " postal_code = '" + values.temporary_postal + "'" +
                                                 " where address_gid = '" + values.temporary_addressgid + "' and " +
                                                 " parent_gid = '" + values.employee_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                }
                                else
                                {

                                    msTemporaryAddressGetGID = objcmnfunctions.GetMasterGID("SADM");


                                    msSQL = " insert into adm_mst_taddress(" +
                                    " address_gid," +
                                    " parent_gid," +
                                    " country_gid," +
                                    " address1, " +
                                    " address2, " +
                                    " city, " +
                                    " state, " +
                                    " address_type, " +
                                    " postal_code, " +
                                    " created_by, " +
                                    " created_date)" +
                                    " values(" +
                                    " '" + msTemporaryAddressGetGID + "'," +
                                    " '" + values.employee_gid + "'," +
                                    " '" + values.countryname + "'," +
                                    " '" + values.temporary_address1 + "'," +
                                    " '" + values.temporary_address2 + "'," +
                                    " '" + values.temporary_city + "'," +
                                    " '" + values.temporary_state + "'," +
                                    " 'Temporary'," +
                                    "'" + values.temporary_postal + "',";
                                    msSQL += "'" + user_gid + "'," +
                                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                }

                            }

                        }
                    }

                    msSQL = " delete from hrm_trn_temployee2shifttype " +
                        " where employee_gid='" + values.employee_gid + "' " +
                        " and shifttype_gid='" + values.shift + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    string msGetGID_shift = objcmnfunctions.GetMasterGID("HESC");

                    msSQL = " insert into hrm_trn_temployee2shifttype( " +
                    " employee2shifttype_gid, " +
                    " employee_gid,  " +
                    " shifttype_gid, " +
                    " shifteffective_date ," +
                    " shiftactive_flag ," +
                    " created_by," +
                    " created_date) " +
                    " values( " +
                    "'" + msGetGID_shift + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.shift + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                    "'Y'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)

                    {
                        msSQL = " select * from hrm_trn_temployee2shifttypedtl " +
                        " where employee_gid='" + values.employee_gid + "' " +
                        " and shifttype_gid='" + values.shift + "' ";
                        objtbl = objdbconn.GetDataTable(msSQL);
                        if (objtbl.Rows.Count > 0)
                        {
                            msSQL = " update hrm_trn_temployee2shifttypedtl set shift_status='Y' where employee_gid='" + values.employee_gid + "' " +
                                   " and shifttype_gid='" + values.shift + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            msSQL = " delete from hrm_trn_temployee2shifttypedtl " +
                           " where employee_gid='" + values.employee_gid + "' " +
                           " and shifttype_gid='" + values.shift + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " select a.shifttypedtl_gid,a.shifttype_gid,a.shifttypedtl_name,a.shift_fromhours,a.shift_tohours, " +
                                   " a.lunchout_hours,a.lunchout_minutes,a.lunchin_hours,a.lunchin_minutes, " +
                                   " a.shift_fromminutes, a.shift_tominutes from hrm_mst_tshifttypedtl a " +
                                   " inner join hrm_mst_tshifttype b on a.shifttype_gid=b.shifttype_gid " +
                                   " where b.shifttype_gid='" + values.shift + "' ";
                            objtbl = objdbconn.GetDataTable(msSQL);
                            if (objtbl.Rows.Count > 0)
                            {
                                foreach (DataRow dt in objtbl.Rows)
                                {
                                    msGetGIDN = objcmnfunctions.GetMasterGID("HEST");
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
                                    " '" + msGetGIDN + "', " +
                                    " '" + msGetGID_shift + "', " +
                                    " '" + values.shift + "', " +
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
                                    " '" + values.employee_gid + "'," +
                                    "'" + user_gid + "'," +
                                    "'Y'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                }
                            }
                        }

                    }

                    msSQL = " select  c.leavetype_name,b.leavetype_gid,a.leavegrade_gid ,a.leavegrade_code, a.leavegrade_name ,c.leavetype_name ,a.attendance_startdate,a.attendance_enddate, " +
                                       " format(sum(b.total_leavecount),2)as total_leavecount , format(sum(b.available_leavecount),2)as available_leavecount, " +
                                      " format(sum(b.leave_limit),2)as leave_limit from hrm_mst_tleavegrade a " +
                                     " left join hrm_mst_tleavegradedtl b on a.leavegrade_gid=b.leavegrade_gid  " +
                                     " left join hrm_mst_tleavetype c on b.leavetype_gid=c.leavetype_gid where a.leavegrade_gid='" + values.leavegrade + "' group by a.leavegrade_gid ";

                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        lsleavegrade_code = objMySqlDataReader["leavegrade_code"].ToString();
                        lsleavegrade_name = objMySqlDataReader["leavegrade_name"].ToString();
                        lsattendance_startdate = objMySqlDataReader["attendance_startdate"].ToString();
                        lsattendance_enddate = objMySqlDataReader["attendance_enddate"].ToString();
                        lsleavetype_gid = objMySqlDataReader["leavetype_gid"].ToString();
                        lsleavetype_name = objMySqlDataReader["leavetype_name"].ToString();
                        lstotal_leavecount = objMySqlDataReader["total_leavecount"].ToString();
                        lsavailable_leavecount = objMySqlDataReader["available_leavecount"].ToString();
                        lsleave_limit = objMySqlDataReader["leave_limit"].ToString();

                        string msgetassign2employee_gid = objcmnfunctions.GetMasterGID("LE2G");

                        msSQL = " delete from hrm_trn_tleavegrade2employee " +
                        " where employee_gid='" + msEmployeeGID + "' " +
                        " and leavegrade_gid='" + values.leavegrade + "' ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " insert into hrm_trn_tleavegrade2employee ( " +
                        " leavegrade2employee_gid," +
                        " branch_gid ," +
                        " employee_gid," +
                        " employee_name," +
                        " leavegrade_gid," +
                        " leavegrade_code," +
                        " leavegrade_name, " +
                        " attendance_startdate, " +
                        " attendance_enddate, " +
                        " total_leavecount, " +
                        " available_leavecount, " +
                        " leave_limit " +
                        " ) Values ( " +
                        " '" + msgetassign2employee_gid + "', " +
                        " '" + values.branchname + "', " +
                        " '" + values.employee_gid + "', " +
                        " '" + values.first_name + "', " +
                        " '" + values.leavegrade + "', " +
                        " '" + lsleavegrade_code + "'," +
                        " '" + lsleavegrade_name + "'," +
                        " '" + Convert.ToDateTime(lsattendance_startdate).ToString("yyyy-MM-dd") + "'," +
                        " '" + Convert.ToDateTime(lsattendance_enddate).ToString("yyyy-MM-dd") + "'," +
                        " '" + lstotal_leavecount + "'," +
                        " '" + lsavailable_leavecount + "'," +
                        " '" + lsleave_limit + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }

                    msSQL = "select a.holidaygrade_gid,b.holiday_gid,b.holiday_date from hrm_mst_tholidaygrade a " +
                                      " left join hrm_mst_tholiday2grade b on b.holidaygrade_gid=a.holidaygrade_gid " +
                                      " where a.holidaygrade_gid='" + values.holidaygrade + "' ";
                    DataTable objtblgrade = objdbconn.GetDataTable(msSQL);
                    if (objtblgrade.Rows.Count > 0)
                    {
                        foreach (DataRow dt in objtblgrade.Rows)
                        {
                            lsholidaygrade_gid = dt["holidaygrade_gid"].ToString();
                            lsholiday_gid = dt["holiday_gid"].ToString();
                            lsholiday_date = dt["holiday_date"].ToString();

                            string msGetGID = objcmnfunctions.GetMasterGID("HYTE");

                            msSQL = " delete from hrm_mst_tholiday2employee " +
                                    " where employee_gid='" + values.employee_gid + "' " +
                                    " and holidaygrade_gid='" + values.holidaygrade + "' ";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " insert into hrm_mst_tholiday2employee ( " +
                           " holiday2employee, " +
                           " holidaygrade_gid, " +
                           " holiday_gid, " +
                           " employee_gid, " +
                           " holiday_date, " +
                           " created_by, " +
                           " created_date ) " +
                           " values ( " +
                           "'" + msGetGID + "', " +
                           "'" + lsholidaygrade_gid + "', " +
                           "'" + lsholiday_gid + "'," +
                           " '" + values.employee_gid + "', " +
                           "'" + Convert.ToDateTime(lsholiday_date).ToString("yyyy-MM-dd") + "', " +
                           "'" + user_gid + "', " +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd") + "') ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }

                    }


                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Employee Added Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Employee !!";
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
        public void DaGetresetpassword(string user_gid, employee_lists values)
        {
            try
            {
                
                msSQL = " select user_gid from hrm_mst_temployee where employee_gid = '" + values.employee_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lsuser_gid = objMySqlDataReader["user_gid"].ToString();
                }

                msSQL = " update  adm_mst_tuser set " +
                        " user_password = '" + objcmnfunctions.ConvertToAscii(values.password) + "'," +
                        " updated_by = '" + user_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where user_gid='" + lsuser_gid + "'  ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Password Reset Successfully !!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Reset Password !!";
                }
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaGetupdateusercode(string user_gid, employee_lists values)
        {
            try
            {
                
                msSQL = " select user_gid from hrm_mst_temployee where employee_gid = '" + values.employee_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lsuser_gid = objMySqlDataReader["user_gid"].ToString();
                }
                msSQL = " SELECT user_code FROM adm_mst_tuser where user_code = '" + values.user_code + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lsuser_code = objMySqlDataReader["user_code"].ToString();
                    if (lsuser_code != null && lsuser_code != "")
                    {
                        usercode = lsuser_code.ToUpper();
                    }
                    else
                    {
                        usercode = null;
                    }
                }
                string uppercaseString = values.user_code.ToUpper();

                if (uppercaseString != usercode)
                {
                    msSQL = " update  adm_mst_tuser set " +
                             " user_code = '" + values.user_code + "'," +
                             " updated_by = '" + user_gid + "'," +
                             " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where user_gid='" + lsuser_gid + "' ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "User Code Updated Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While User Code Updated !!";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "User Code Already Exist !!";
                }
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaGetupdateuserdeactivate(string user_gid, employee_lists values)
        {
            try
            {
                
                msSQL = " select user_gid from hrm_mst_temployee where employee_gid = '" + values.employee_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lsuser_gid = objMySqlDataReader["user_gid"].ToString();
                }
                msSQL = "update adm_mst_tuser set user_status='N' where user_gid='" + lsuser_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = " update  hrm_mst_temployee set " +
                        " exit_date = '" + values.deactive_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " remarks  = '" + values.remarks + "'," +
                        " updated_by = '" + user_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where user_gid='" + lsuser_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "User Deactivated Successfully !!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deactivating User !!";
                }
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaEmployeeImport(HttpRequest httpRequest, string user_gid, result objResult, employee_lists values)
        {
            string lscompany_code;
            string excelRange, endRange;
            int rowCount, columnCount;

            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = ConfigurationManager.AppSettings["importexcelfile1"] + lscompany_code + "/" + " Import_Excel/Hrm_Module/EmployeeExcels/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

                if (!Directory.Exists(lsfilePath))
                    Directory.CreateDirectory(lsfilePath);

                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = lsfile_gid + FileExtension;
                FileInfo fileinfo = new FileInfo(lsfilePath);
                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                   
                    string status;
                    status = objcmnfunctions.uploadFile(lsfilePath, FileExtension);
                    file.Close();
                    ms.Close();

                    msSQL = " insert into hrm_trn_temployeeuploadexcellog(" +
                            " uploadexcellog_gid," +
                            " fileextenssion," +
                            " uploaded_by, " +
                            " uploaded_date)" +
                            " values(" +
                            " '" + msdocument_gid + "'," +
                            " '" + FileExtension + "'," +
                            " '" + user_gid + "'," +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.ToString();
                    return;
                }
                //Excel To DataTable
                try
                {
                    DataTable dataTable = new DataTable();
                    int totalSheet = 1;
                    string connectionString = string.Empty;
                    string fileExtension = Path.GetExtension(lspath);

                    lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                    string correctedPath = Regex.Replace(lsfilePath, @"\\+", @"\");

                    try
                    {
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + correctedPath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0';";
                    }
                    catch (Exception ex)
                    {

                    }
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();
                        DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                        if (schemaTable != null)
                        {
                            var tempDataTable = (from dataRow in schemaTable.AsEnumerable()
                                                 where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                                 select dataRow).CopyToDataTable();

                            schemaTable = tempDataTable;
                            totalSheet = schemaTable.Rows.Count;
                            using (OleDbCommand command = new OleDbCommand())
                            {
                                command.Connection = connection;
                                command.CommandText = "select * from [Sheet1$]";

                                using (OleDbDataReader reader = command.ExecuteReader())
                                {
                                    dataTable.Load(reader);
                                }

                                // Upload document
                                importcount = 0;

                                foreach (DataRow row in dataTable.Rows)
                                {
                                    string firstname = row["First Name"].ToString();
                                    string lastname = row["Last Name"].ToString();
                                    string entity = row["Entity"].ToString();
                                    string branch = row["Branch"].ToString();
                                    string department = row["Department"].ToString();
                                    string designation = row["Designation"].ToString();
                                    string employeeacess = row["Employee Access(Y or N)"].ToString();
                                    string usercode = row["User Code"].ToString();
                                    string password = row["User Password"].ToString();
                                    string gender = row["Gender(Male or Female)"].ToString();
                                    string email = row["Personal Email Address"].ToString();
                                    string phno = row["Personal Phone Number"].ToString();

                                    string percity = row["Permanent_City"].ToString();
                                    string perstate = row["Permanent_State"].ToString();
                                    string percountry = row["Permanent_Country"].ToString();  
                                    string perpincode = row["Permanent_Postal Code"].ToString();
                                    string peraddress = row["Permanent_Address"].ToString();
                                    string tepcity = row["Temporary_City"].ToString(); 
                                    string tepstate = row["Temporary_State"].ToString();
                                    string tepcountry = row["Temporary_Country"].ToString();
                                    string teopincode = row["Temporary_Postal Code"].ToString();
                                    string tepaddress = row["Temporary_Address"].ToString();
                                    ErrorCount = 0;
                                    // getting country_gids
                                    msSQL = "select country_gid from adm_mst_tcountry where country_name = '" + percountry.Replace("'", "\'").Trim() + "'";
                                     lspcountry_gid = objdbconn.GetExecuteScalar(msSQL);
                                    if (lspcountry_gid == "")
                                    {
                                        string MstGid = objcmnfunctions.GetMasterGID("UPEE");
                                        msSQL = " insert into hrm_trn_temployeeuploadexcelerrorlog (" +
                                                " uploaderrorlog_gid ," +
                                                " uploadexcellog_gid, " +
                                                " first_name, " +
                                                " last_name, " +
                                                " remarks, " +
                                                " user_code)" +
                                                " values(" +
                                                " '" + MstGid + "'," +
                                                " '" + msdocument_gid + "'," +
                                                " '" + firstname + "'," +
                                                " '" + lastname + "'," +
                                                " 'Permanent country not found' ," +
                                                "'" + usercode + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        ErrorCount++;
                                    }
                                    else { lspcountry_gid = lspcountry_gid; }

                                    msSQL = "select country_gid from adm_mst_tcountry where country_name = '" + tepcountry.Replace("'", "\'").Trim() + "'";
                                     lstemcountry_gid = objdbconn.GetExecuteScalar(msSQL);
                                    if (lstemcountry_gid == "")
                                    {
                                        string MstGid = objcmnfunctions.GetMasterGID("UPEE");
                                        msSQL = " insert into hrm_trn_temployeeuploadexcelerrorlog (" +
                                                " uploaderrorlog_gid ," +
                                                " uploadexcellog_gid, " +
                                                " first_name, " +
                                                " last_name, " +
                                                " remarks, " +
                                                " user_code)" +
                                                " values(" +
                                                " '" + MstGid + "'," +
                                                " '" + msdocument_gid + "'," +
                                                " '" + firstname + "'," +
                                                " '" + lastname + "'," +
                                                " 'Temporary country not found' ," +
                                                "'" + usercode + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        ErrorCount++;
                                    }
                                    else {  lstemcountry_gid = lstemcountry_gid; }

                                    //getting Entity Gid
                                    msSQL = "select entity_gid from adm_mst_tentity where entity_name = '" + entity.Replace("'", "\'").Trim() + "'";
                                    lsentity_gid = objdbconn.GetExecuteScalar(msSQL);
                                    if (lsentity_gid == "")
                                    {
                                        string MstGid = objcmnfunctions.GetMasterGID("UPEE");
                                        msSQL = " insert into hrm_trn_temployeeuploadexcelerrorlog (" +
                                                " uploaderrorlog_gid ," +
                                                " uploadexcellog_gid, " +
                                                " first_name, " +
                                                " last_name, " +
                                                " remarks, " +
                                                " user_code)" +
                                                " values(" +
                                                " '" + MstGid + "'," +
                                                " '" + msdocument_gid + "'," +
                                                " '" + firstname + "'," +
                                                " '" + lastname + "'," +
                                                " 'Entity not found' ," +
                                                "'" + usercode + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        ErrorCount++;
                                    }
                                    else { lsentity_gid = lsentity_gid; }

                                    //getting branch_gid
                                    msSQL = "select branch_gid from hrm_mst_tbranch where branch_name = '" + branch.Replace("'", "\'").Trim() + "'";
                                    lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
                                    if (lsbranch_gid == "") 
                                    {
                                        string MstGid = objcmnfunctions.GetMasterGID("UPEE");
                                        msSQL = " insert into hrm_trn_temployeeuploadexcelerrorlog (" +
                                                " uploaderrorlog_gid ," +
                                                " uploadexcellog_gid, " +
                                                " first_name, " +
                                                " last_name, " +
                                                " remarks, " +
                                                " user_code)" +
                                                " values(" +
                                                " '" + MstGid + "'," +
                                                " '" + msdocument_gid + "'," +
                                                " '" + firstname + "'," +
                                                " '" + lastname + "'," +
                                                " 'Branch not found' ," +
                                                "'" + usercode + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        ErrorCount++;
                                    }
                                    else { lsbranch_gid = lsbranch_gid; }

                                    //getting department_gid
                                    msSQL = "select department_gid from hrm_mst_tdepartment where department_name = '" + department.Replace("'", "\'").Trim() + "'";
                                    lsdepartment_gid = objdbconn.GetExecuteScalar(msSQL);
                                    if (lsdepartment_gid == "")
                                    {
                                        string MstGid = objcmnfunctions.GetMasterGID("UPEE");
                                        msSQL = " insert into hrm_trn_temployeeuploadexcelerrorlog (" +
                                                " uploaderrorlog_gid ," +
                                                " uploadexcellog_gid, " +
                                                " first_name, " +
                                                " last_name, " +
                                                " remarks, " +
                                                " user_code)" +
                                                " values(" +
                                                " '" + MstGid + "'," +
                                                " '" + msdocument_gid + "'," +
                                                " '" + firstname + "'," +
                                                " '" + lastname + "'," +
                                                " 'Department not found' ," +
                                                "'" + usercode + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        ErrorCount++;
                                    }
                                    else { lsdepartment_gid = lsdepartment_gid; }

                                    //getting designation_gid
                                    msSQL = "select designation_gid from adm_mst_tdesignation where designation_name = '" + designation.Replace("'", "\'").Trim() + "'";
                                    lsdesignation_gid = objdbconn.GetExecuteScalar(msSQL);
                                    if (lsdesignation_gid == "")
                                    {
                                        string MstGid = objcmnfunctions.GetMasterGID("UPEE");
                                        msSQL = " insert into hrm_trn_temployeeuploadexcelerrorlog (" +
                                                " uploaderrorlog_gid ," +
                                                " uploadexcellog_gid, " +
                                                " first_name, " +
                                                " last_name, " +
                                                " remarks, " +
                                                " user_code)" +
                                                " values(" +
                                                " '" + MstGid + "'," +
                                                " '" + msdocument_gid + "'," +
                                                " '" + firstname + "'," +
                                                " '" + lastname + "'," +
                                                " 'Designation not found' ," +
                                                "'" + usercode + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        ErrorCount++;
                                    }
                                    else { lsdesignation_gid = lsdesignation_gid; }

                                    msSQL = " SELECT user_code FROM adm_mst_tuser where user_code = '" + usercode.Replace("'", "\'").Trim() + "' ";
                                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                    if (objMySqlDataReader.HasRows)
                                    {
                                        lsuser_code = objMySqlDataReader["user_code"].ToString();
                                    }
                                    
                                    if (lsuser_code != null) 
                                    { 
                                     uppercasedbvalue = lsuser_code.ToUpper();
                                    }

                                    string uppercaseString = usercode.ToUpper();
                                    if (uppercaseString != uppercasedbvalue && ErrorCount==0)
                                    {
                                        msUserGid = objcmnfunctions.GetMasterGID("SUSM");
                                        msSQL = " insert into adm_mst_tuser(" +
                                                " user_gid," +
                                                " user_code," +
                                                " user_firstname," +
                                                " user_lastname, " +
                                                " user_password, " +
                                                " user_status, " +
                                                " created_by, " +
                                                " created_date)" +
                                                " values(" +
                                                " '" + msUserGid + "'," +
                                                " '" + uppercaseString + "'," +
                                                " '" + firstname.Replace("'","\'").Trim() + "'," +
                                                " '" + lastname.Replace("'", "\'").Trim() + "'," +
                                                " '" + objcmnfunctions.ConvertToAscii(password) + "'," +
                                                "'" + employeeacess.Replace("'", "\'").Trim() + "',";
                                       msSQL += "'" + user_gid + "'," +
                                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        importcount++;
                                        if (mnResult == 1)
                                        {
                                            msEmployeeGID = objcmnfunctions.GetMasterGID("SERM");
                                            msBiometricGID = objcmnfunctions.GetBiometricGID();
                                            msSQL1 =    " Insert into hrm_mst_temployee " +
                                                        " (employee_gid , " +
                                                        " user_gid," +
                                                        " designation_gid," +
                                                        " employee_mobileno , " +
                                                        " employee_emailid , " +
                                                        " employee_gender , " +
                                                        " department_gid," +
                                                        " entity_gid," +
                                                        " employee_photo," +
                                                        " useraccess," +
                                                        " engagement_type," +
                                                        " attendance_flag, " +
                                                        " branch_gid, " +
                                                        " biometric_id, " +
                                                        " created_by, " +
                                                        " created_date " +
                                                        " )values( " +
                                                        "'" + msEmployeeGID + "', " +
                                                        "'" + msUserGid + "', " +
                                                        "'" + lsdesignation_gid + "'," +
                                                        "'" + phno.Replace("'", "\'").Trim() + "'," +
                                                        "'" + email + "'," +
                                                        "'" + gender.Replace("'", "\'").Trim() + "'," +
                                                        "'" + lsdepartment_gid + "'," +
                                                        "'" + lsentity_gid + "'," +
                                                        "'" + null + "'," +
                                                        "'" + employeeacess.Replace("'", "\'").Trim() + "'," +
                                                        "'Direct'," +
                                                        "'Y'," +
                                                        " '" + lsbranch_gid + "'," +
                                                        "'" + msBiometricGID + "'," +
                                                        "'" + user_gid + "'," +
                                                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL1);
                                            if (mnResult == 1)
                                            {                                               
                                                msGetemployeetype = objcmnfunctions.GetMasterGID("SETD");
                                                msSQL =   " insert into hrm_trn_temployeetypedtl(" +
                                                          " employeetypedtl_gid," +
                                                          " employee_gid," +
                                                          " workertype_gid," +
                                                          " systemtype_gid, " +
                                                          " branch_gid, " +
                                                          " wagestype_gid, " +
                                                          " department_gid, " +
                                                          " employeetype_name, " +
                                                          " designation_gid, " +
                                                          " created_by, " +
                                                          " created_date)" +
                                                          " values(" +
                                                          " '" + msGetemployeetype + "'," +
                                                          " '" + msEmployeeGID + "'," +
                                                          " 'null'," +
                                                          " 'Audit'," +
                                                          " '" + lsbranch_gid + "'," +
                                                          " 'wg001'," +
                                                          " '" + lsdepartment_gid + "'," +
                                                          "'Roll'," +
                                                          " '" + lsdesignation_gid + "'," +
                                                          "'" + user_gid + "'," +
                                                          "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                if (mnResult == 1)
                                                {
                                                    msPermanentAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
                                                    msTemporaryAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
                                                    msSQL = " insert into adm_mst_taddress(" +
                                                            " address_gid," +
                                                            " parent_gid," +
                                                            " country_gid," +
                                                            " address1, " +
                                                            " city, " +
                                                            " state, " +
                                                            " address_type, " +
                                                            " postal_code, " +
                                                            " created_by, " +
                                                            " created_date)" +
                                                            " values(" +
                                                            " '" + msPermanentAddressGetGID + "'," +
                                                            " '" + msEmployeeGID + "'," +
                                                            " '" + lspcountry_gid + "'," +
                                                            " '" + peraddress.Replace("'", "\'").Trim() + "'," +
                                                            " '" + percity.Replace("'", "\'").Trim() + "'," +
                                                            " '" + perstate.Replace("'", "\'").Trim() + "'," +
                                                            " 'Permanent'," +
                                                            "'" + perpincode.Replace("'", "\'").Trim() + "',";
                                                   msSQL += "'" + user_gid + "'," +
                                                            "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                    if (mnResult == 1)
                                                    {
                                                        msSQL = " insert into adm_mst_taddress(" +
                                                                " address_gid," +
                                                                " parent_gid," +
                                                                " country_gid," +
                                                                " address1, " +
                                                                " city, " +
                                                                " state, " +
                                                                " address_type, " +
                                                                " postal_code, " +
                                                                " created_by, " +
                                                                " created_date)" +
                                                                " values(" +
                                                                " '" + msTemporaryAddressGetGID + "'," +
                                                                " '" + msEmployeeGID + "'," +
                                                                " '" + lstemcountry_gid + "'," +
                                                                " '" + tepaddress.Replace("'", "\'").Trim() + "'," +
                                                                " '" + tepcity.Replace("'", "\'").Trim() + "'," +
                                                                " '" + tepstate.Replace("'", "\'").Trim() + "'," +
                                                                " 'Temporary'," +
                                                                "'" + teopincode.Replace("'", "\'").Trim() + "',";
                                                       msSQL += "'" + user_gid + "'," +
                                                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                                       mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                    }
                                                }
                                            }
                                        }                                       
                                    }
                                    else
                                    {
                                        if (ErrorCount==0)
                                        {
                                            string MstGid = objcmnfunctions.GetMasterGID("UPEE");
                                            msSQL = " insert into hrm_trn_temployeeuploadexcelerrorlog (" +
                                                    " uploaderrorlog_gid ," +
                                                    " uploadexcellog_gid, " +
                                                    " first_name, " +
                                                    " last_name, " +
                                                    " remarks, " +
                                                    " user_code)" +
                                                    " values(" +
                                                    " '" + MstGid + "'," +
                                                    " '" + msdocument_gid + "'," +
                                                    " '" + firstname + "'," +
                                                    " '" + lastname + "'," +
                                                    " 'user_code already exist' ," +
                                                    "'" + usercode + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.ToString();
                    return;
                }
            }

            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

            msSQL = " update  hrm_trn_temployeeuploadexcellog set " +
                    " importcount = " + importcount + " "+
                    " where uploadexcellog_gid='" + msdocument_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (importcount == 0)
            {
                objResult.status = false;
                objResult.message = "No employee data has been imported so Please check the error log.";
            }
            else
            {
                objResult.status = true;
                objResult.message = importcount + "  employee data has been imported";
            }
        }
    }
}