using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Odbc;

using ems.hrm.Models;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.hrm.DataAccess
{
    public class DaEmployeeOnboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        //  FnAgroHBAPIConn objFnSamAgroHBAPIConn = new FnAgroHBAPIConn();

        string msSQL = string.Empty;
        OdbcDataReader objMySqlDataReader, objMySqlDataReader1;
        DataTable dt_datatable;
        string msEmployeeGID, msGetGid, msGetPrivilege_gid, msGetModule2employee_gid, msGetAPICode;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        int mnResult6;
        string lsentity_flag = string.Empty;
        int k, ls_port, hierarchy_level;
        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, employee_reporting_to, erp_id;
        string lsemployee_name, lsemployee_gid, lscreated_by, lsemployee_reportingto, lsmodule2employee_gid;
        string sToken = string.Empty;
        Random rand = new Random();
        string lsto_mail, ls_baselocation, lstask_gid, lshierarchy_level, lscc_mail, strBCC, lsbcc_mail;
        string lstask_name, lstask_remarks, lscreated_date, lsbase_location, lsrptngto_erpid, lstaskassignedby, lsnewemployee_name, lsBccmail_id, lsreporting_to, lsreporting_toname;
        public string[] lsBCCReceipients;
        public string[] lsCCReceipients;
        string lsentity_name, lsemployee_emailid, lsemployee_mobileno, lsuser_firstname, lsuser_lastname, lsuser_code, lsdepartment_gid;
        string loglspath = "", logFileName = "";
        string lspath, lsentity_gid, lsemployee_externalid;
        string lsmodulereportingto_gid = "";
        public bool DaEmployeeSummary(MdlEmployeeOnboard objemployee)
        {
            try
            {
                
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , a.api_codes, " +
                    " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                    " c.employee_gender,  " +
                    " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                    " d.designation_name,c.designation_gid,c.employee_gid,c.employee_emailid,e.branch_name,s.baselocation_name,c.baselocation_gid,concat(v.user_firstname,' ',v.user_lastname) as employeereporting_to, " +
                    " CASE " +
                    " WHEN a.user_status = 'Y' THEN 'Active'  " +
                    " WHEN a.user_status = 'N' THEN 'Inactive' " +
                    " END as user_status,c.department_gid,c.branch_gid, e.branch_name, g.department_name " +
                    " FROM adm_mst_tuser a " +
                    " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee p on c.employeereporting_to = p.employee_gid " +
                    " left join adm_mst_tuser v on p.user_gid = v.user_gid " +
                    " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                    " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                    " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                    " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                    " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                    " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                    " left join sys_mst_tbaselocation s on s.baselocation_gid=c.baselocation_gid" +
                    " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid " +
                    " group by c.employee_gid " +
                    " order by c.employee_gid desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employeelist = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employeelist.Add(new employee
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            user_gid = dr_datarow["user_gid"].ToString(),
                            branch_name = dr_datarow["branch_name"].ToString(),
                            designation_name = dr_datarow["designation_name"].ToString(),
                            department_name = dr_datarow["department_name"].ToString(),
                            user_code = dr_datarow["user_code"].ToString(),
                            employee_name = dr_datarow["user_name"].ToString(),
                            user_status = dr_datarow["user_status"].ToString(),
                            company_name = dr_datarow["entity_name"].ToString(),
                            baselocation_gid = dr_datarow["baselocation_gid"].ToString(),
                            baselocation_name = dr_datarow["baselocation_name"].ToString(),
                            employeereporting_to = dr_datarow["employeereporting_to"].ToString(),
                            employee_emailid = dr_datarow["employee_emailid"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            api_code = dr_datarow["api_codes"].ToString()

                        });
                    }
                    objemployee.employee = get_employeelist;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaEmployeeActiveSummary(MdlEmployeeOnboard objemployee)
        {
            try
            {
                
                msSQL = " Select distinct a.user_gid,c.erp_id,c.useraccess,a.api_codes,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , " +
                    " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                    " c.employee_gender,  " +
                    " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                    " d.designation_name,c.designation_gid,c.employee_gid,c.employee_emailid,e.branch_name,s.baselocation_name,c.baselocation_gid,concat(v.user_firstname,' ',v.user_lastname) as employeereporting_to, " +
                    " concat(r.user_firstname,' ',r.user_lastname) as created_by, date_format(c.created_date, '%d-%m-%Y') as created_date, c.employee_externalid, c.employee_entitychange_flag," +
                    " CASE " +
                    " WHEN a.user_status = 'Y' THEN 'Active'  " +
                    " WHEN a.user_status = 'N' THEN 'Inactive' " +
                    " END as user_status,c.department_gid,c.branch_gid, e.branch_name, g.department_name" +
                    " FROM adm_mst_tuser a " +
                    " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee p on c.employeereporting_to = p.employee_gid " +
                    " left join hrm_mst_temployee q on c.created_by = q.employee_gid " +
                    " left join adm_mst_tuser r on q.user_gid = r.user_gid " +
                    " left join adm_mst_tuser v on p.user_gid = v.user_gid " +
                    " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                    " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                    " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                    " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                    " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                    " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                    " left join sys_mst_tbaselocation s on s.baselocation_gid=c.baselocation_gid" +
                    " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid " +
                    " left join sys_mst_temployeelog n on n.employee_gid = c.employee_gid " +
                    " where a.user_status = 'Y' and (c.employee_status = 'A' || c.employee_status = null)" +
                    " group by c.employee_gid " +
                    " order by c.created_date desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employeelist = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employeelist.Add(new employee
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            user_gid = dr_datarow["user_gid"].ToString(),
                            branch_name = dr_datarow["branch_name"].ToString(),
                            designation_name = dr_datarow["designation_name"].ToString(),
                            department_name = dr_datarow["department_name"].ToString(),
                            user_code = dr_datarow["user_code"].ToString(),
                            employee_name = dr_datarow["user_name"].ToString(),
                            user_status = dr_datarow["user_status"].ToString(),
                            company_name = dr_datarow["entity_name"].ToString(),
                            baselocation_gid = dr_datarow["baselocation_gid"].ToString(),
                            baselocation_name = dr_datarow["baselocation_name"].ToString(),
                            employeereporting_to = dr_datarow["employeereporting_to"].ToString(),
                            employee_emailid = dr_datarow["employee_emailid"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            erp_id = dr_datarow["erp_id"].ToString(),
                            employee_externalid = dr_datarow["employee_externalid"].ToString(),
                            employee_entitychange_flag = dr_datarow["employee_entitychange_flag"].ToString(),
                            api_code = dr_datarow["api_codes"].ToString()
                        });
                    }
                    objemployee.employee = get_employeelist;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaEmployeeRelievedSummary(MdlEmployeeOnboard objemployee)
        {
            try
            {
                
                msSQL = "select b.employee_gid,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name, " +
                    " a.user_code ,concat(a.user_firstname,' ',a.user_lastname) as user_name , b.remarks, date_format(b.created_date, '%d-%m-%Y') as created_date, " +
                    " concat(e.user_firstname,' ',e.user_lastname) as created_by,date_format( b.exit_date, '%d-%m-%Y') as exit_date,concat(g.user_firstname, ' ', g.user_lastname) as employeereporting_to," +
                    " s.baselocation_name,c.baselocation_gid,c.department_gid,h.department_name, " +
                    " CASE " +
                   " WHEN a.user_status = 'Y' THEN 'Active'  " +
                   " WHEN a.user_status = 'N' THEN 'Inactive' " +
                   " END as user_access " +
                    " from hrm_trn_texitemployee b" +
                    " left join hrm_mst_temployee c on c.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser a on c.user_gid = a.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = b.created_by " +
                    " left join adm_mst_tuser e on d.user_gid = e.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = c.employeereporting_to " +
                    " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                    " left join sys_mst_tbaselocation s on s.baselocation_gid = c.baselocation_gid " +
                    " left join hrm_mst_tdepartment h on h.department_gid = c.department_gid" +
                    " left join adm_mst_tentity z on z.entity_gid = c.entity_gid " +
                    " left join sys_mst_temployeelog i on c.employee_gid = i.employee_gid " +
                   " where a.user_status = 'Y' and  c.employee_status = 'R'" +
                    " group by c.employee_gid " +
                    " order by b.exit_date desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employeelist = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employeelist.Add(new employee
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            department_name = dr_datarow["department_name"].ToString(),
                            user_code = dr_datarow["user_code"].ToString(),
                            employee_name = dr_datarow["user_name"].ToString(),
                            entity_name = dr_datarow["entity_name"].ToString(),
                            baselocation_gid = dr_datarow["baselocation_gid"].ToString(),
                            baselocation_name = dr_datarow["baselocation_name"].ToString(),
                            employeereporting_to = dr_datarow["employeereporting_to"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            remarks = dr_datarow["remarks"].ToString(),
                            relive_date = dr_datarow["exit_date"].ToString(),
                            user_access = dr_datarow["user_access"].ToString()
                        });
                    }
                    objemployee.employee = get_employeelist;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaEmployeePendingSummary(MdlEmployeeOnboard objemployee)
        {
            try
            {
                
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , " +
                    " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,a.api_codes," +
                    " c.employee_gender,  " +
                    " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                    " d.designation_name,c.designation_gid,c.employee_gid,c.employee_emailid,e.branch_name,s.baselocation_name," +
                    " c.baselocation_gid,concat(v.user_firstname,' ',v.user_lastname) as employeereporting_to, " +
                    " CASE " +
                    " WHEN a.user_status = 'Y' THEN 'Active'  " +
                    " WHEN a.user_status = 'N' THEN 'Inactive' " +
                    " END as user_status,c.department_gid,c.branch_gid, date_format(c.employee_joiningdate,'%d-%m-%Y') as joining_date," +
                    " e.branch_name, g.department_name, date_format(c.created_date,'%d-%m-%Y') as created_date,concat(r.user_firstname,' ',r.user_lastname) as created_by, b.employee_status" +
                    " FROM adm_mst_tuser a " +
                    " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee p on c.employeereporting_to = p.employee_gid " +
                    " left join hrm_mst_temployee q on q.employee_gid = c.created_by " +
                    " left join adm_mst_tuser r on q.user_gid = r.user_gid " +
                    " left join adm_mst_tuser v on p.user_gid = v.user_gid " +
                    " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                    " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                    " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                    " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                    " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                    " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                    " left join sys_mst_tbaselocation s on s.baselocation_gid=c.baselocation_gid" +
                    " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid " +
                    " left join sys_mst_temployeelog b on b.employee_gid = c.employee_gid "+                   
                    " order by c.employee_gid desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employeelist = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employeelist.Add(new employee
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            user_gid = dr_datarow["user_gid"].ToString(),
                            branch_name = dr_datarow["branch_name"].ToString(),
                            designation_name = dr_datarow["designation_name"].ToString(),
                            department_name = dr_datarow["department_name"].ToString(),
                            user_code = dr_datarow["user_code"].ToString(),
                            employee_name = dr_datarow["user_name"].ToString(),
                            user_status = dr_datarow["user_status"].ToString(),
                            entity_name = dr_datarow["entity_name"].ToString(),
                            baselocation_gid = dr_datarow["baselocation_gid"].ToString(),
                            baselocation_name = dr_datarow["baselocation_name"].ToString(),
                            employeereporting_to = dr_datarow["employeereporting_to"].ToString(),
                            employee_emailid = dr_datarow["employee_emailid"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            joining_date = dr_datarow["joining_date"].ToString(),
                            employee_status = dr_datarow["employee_status"].ToString(),
                            api_code = dr_datarow["api_codes"].ToString()
                        });
                    }
                    objemployee.employee = get_employeelist;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaEmployeeInactiveSummary(MdlEmployeeOnboard objemployee)
        {
            try
            {
                
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name ," +
                        " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,concat(l.user_firstname,' ',l.user_lastname) as employeereporting_to, " +
                        " date_format(c.employee_joiningdate, '%d-%m-%Y') as joining_date,c.employee_gid, date_format(c.updated_date, '%d-%m-%Y') as updated_date, " +
                        " concat(e.user_firstname,' ',e.user_lastname) as updated_by,c.remarks  " +
                        " FROM adm_mst_tuser a left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                        " left join hrm_mst_temployee d on d.employee_gid = c.updated_by " +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                        " left join adm_mst_tentity z on z.entity_gid=c.entity_gid " +
                        " left join sys_mst_temployeelog n on n.employee_gid=c.employee_gid " +
                        " left join hrm_mst_temployee m on m.employee_gid=c.employeereporting_to" +
                        " left join adm_mst_tuser l on m.user_gid=l.user_gid " +
                        " where a.user_status = 'N' and c.employee_status='I'" +
                        " group by employee_gid " +
                        " order by updated_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employeelist = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employeelist.Add(new employee
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            user_gid = dr_datarow["user_gid"].ToString(),
                            user_code = dr_datarow["user_code"].ToString(),
                            employee_name = dr_datarow["user_name"].ToString(),
                            employeereporting_to = dr_datarow["employeereporting_to"].ToString(),
                            remarks = dr_datarow["remarks"].ToString(),
                            entity_name = dr_datarow["entity_name"].ToString(),
                            updated_date = dr_datarow["updated_date"].ToString(),
                            updated_by = dr_datarow["updated_by"].ToString(),
                            joining_date = dr_datarow["joining_date"].ToString(),
                        });
                    }
                    objemployee.employee = get_employeelist;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPopEntity(entity_list objentity_list)
        {
            try
            {
                
                msSQL = "select entity_gid,entity_name from adm_mst_tentity where 1=1 ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_entity = new List<entity>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_entity.Add(new entity
                        {
                            entity_gid = dr_row["entity_gid"].ToString(),
                            entity_name = dr_row["entity_name"].ToString()
                        });
                    }
                    objentity_list.entity = get_entity;
                    objentity_list.status = true;
                    return true;
                }
                else
                {
                    objentity_list.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                objentity_list.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPopBranch(MdlEmployeeOnboard objemployee_list)
        {
            try
            {
                
                msSQL = "select branch_gid,branch_name from hrm_mst_tbranch order by branch_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_branch_list = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_branch_list.Add(new employee
                        {
                            branch_gid = dr_row["branch_gid"].ToString(),
                            branch_name = dr_row["branch_name"].ToString()
                        });
                    }
                    objemployee_list.employee = get_branch_list;
                    objemployee_list.status = true;
                    return true;
                }
                else
                {
                    objemployee_list.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                objemployee_list.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPopDepartment(MdlEmployeeOnboard objemployee_list)
        {
            try
            {
                
                msSQL = "select department_gid,department_name from hrm_mst_tdepartment order by department_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_department_list = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {   
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_department_list.Add(new employee
                        {
                            department_gid = dr_row["department_gid"].ToString(),
                            department_name = dr_row["department_name"].ToString()
                        });
                    }
                    objemployee_list.employee = get_department_list;
                    objemployee_list.status = true;
                    return true;
                }
                else
                {
                    objemployee_list.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                objemployee_list.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPopSubfunction(MdlEmployeeOnboard objemployee_list)
        {
            try
            {
                
                msSQL = "select subfunction_gid,subfunction_name from sys_mst_tsubfunction where status='Y' and delete_flag='N'; ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_subfunction_list = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_subfunction_list.Add(new employee
                        {
                            subfunction_gid = dr_row["subfunction_gid"].ToString(),
                            subfunction_name = dr_row["subfunction_name"].ToString()
                        });
                    }
                    objemployee_list.employee = get_subfunction_list;
                    objemployee_list.status = true;
                    return true;
                }
                else
                {
                    objemployee_list.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                objemployee_list.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPopDesignation(MdlEmployeeOnboard objemployee_list)
        {
            try
            {
                
                msSQL = "select designation_gid,designation_name from adm_mst_tdesignation order by designation_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_designation_list = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_designation_list.Add(new employee
                        {
                            designation_gid = dr_row["designation_gid"].ToString(),
                            designation_name = dr_row["designation_name"].ToString()
                        });
                    }
                    objemployee_list.employee = get_designation_list;
                    objemployee_list.status = true;
                    return true;
                }
                else
                {
                    objemployee_list.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                objemployee_list.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPopCountry(country_list objcountry_list)
        {
            try
            {
                
                msSQL = " Select country_gid,country_code,country_name From adm_mst_tcountry ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_country = new List<country>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_country.Add(new country
                        {
                            country_gid = dr_row["country_gid"].ToString(),
                            country_name = dr_row["country_name"].ToString()
                        });
                    }
                    objcountry_list.country = get_country;
                    objcountry_list.status = true;
                    return true;
                }
                else
                {
                    objcountry_list.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                objcountry_list.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPopRole(role_list objrolemaster)
        {
            try
            {
                
                msSQL = "select role_gid,role_name from hrm_mst_trole order by role_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_role = new List<rolemaster>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_role.Add(new rolemaster
                        {
                            role_gid = dr_datarow["role_gid"].ToString(),
                            role_name = dr_datarow["role_name"].ToString()

                        });
                    }
                    objrolemaster.rolemaster = get_role;
                    objrolemaster.status = true;
                    return true;
                }
                else
                {
                    objrolemaster.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                objrolemaster.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaPopReportingTo(reportingto_list objreportingto)
        {
            try
            {
                
                msSQL = " select distinct  a.employee_gid, concat(b.user_firstname,' ',b.user_lastname) as employee_name  from hrm_mst_temployee a"+
                        " left join adm_mst_tuser b on a.user_gid=b.user_gid"+
                        " left join adm_mst_tmodule2employee c on c.employee_gid= a.employee_gid where c.hierarchy_level>0  and b.user_status='Y'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_reportingto = new List<reportingto>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_reportingto.Add(new reportingto
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            employee_name = dr_datarow["employee_name"].ToString()

                        });
                    }
                    objreportingto.reportingto = get_reportingto;
                    objreportingto.status = true;
                    return true;
                }
                else
                {
                    objreportingto.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                objreportingto.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaEmployeeEditView(employee objemployee, string employee_gid)
        {
            try
            {
                
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , user_firstname,user_lastname," +
                   " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                   " c.employee_gender,c.role_gid,employeereporting_to, x.subfunction_name,c.subfunction_gid, " +
                   " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                   " d.designation_name,c.designation_gid,c.employee_gid,e.branch_name, employee_emailid,employee_mobileno,baselocation_name,s.baselocation_gid,c.entity_gid," +
                   " CASE " +
                   " WHEN a.user_status = 'Y' THEN 'Active'  " +
                   " WHEN a.user_status = 'N' THEN 'Inactive' " +
                   " END as user_access,a.user_status,c.department_gid,c.branch_gid,n.role_name, e.branch_name, g.department_name,c.marital_status,c.marital_status_gid,date_format(c.employee_joiningdate, '%d-%m-%Y') as joiningdate, " +
                   " c.employee_personalno as personal_phone_no,c.personal_emailid,c.bloodgroup as bloodgroup_name,c.bloodgroup_gid " +
                   " FROM hrm_mst_temployee c " +
                   " left join adm_mst_tuser a on a.user_gid = c.user_gid " +
                   " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                   " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                   " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                   " left join sys_mst_tsubfunction x on x.subfunction_gid=c.subfunction_gid " +
                   " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                   " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                   " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                   " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid" +
                   " left join hrm_mst_trole n on n.role_gid=c.role_gid " +
                   " left join sys_mst_tbaselocation s on s.baselocation_gid = c.baselocation_gid " +
                   " where c.employee_gid='" + employee_gid + "'" +                   
                   " order by c.employee_gid desc  ";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objemployee.company_name = objMySqlDataReader["entity_name"].ToString();
                    objemployee.entity_gid = objMySqlDataReader["entity_gid"].ToString();
                    objemployee.branch_gid = objMySqlDataReader["branch_gid"].ToString();
                    objemployee.branch_name = objMySqlDataReader["branch_name"].ToString();
                    objemployee.department_gid = objMySqlDataReader["department_gid"].ToString();
                    objemployee.department_name = objMySqlDataReader["department_name"].ToString();
                    objemployee.subfunction_gid = objMySqlDataReader["subfunction_gid"].ToString();
                    objemployee.subfunction_name = objMySqlDataReader["subfunction_name"].ToString();
                    objemployee.designation_gid = objMySqlDataReader["designation_gid"].ToString();
                    objemployee.designation_name = objMySqlDataReader["designation_name"].ToString();
                    objemployee.useraccess = objMySqlDataReader["useraccess"].ToString();
                    objemployee.user_access = objMySqlDataReader["user_access"].ToString();
                    objemployee.user_status = objMySqlDataReader["user_status"].ToString();
                    objemployee.user_firstname = objMySqlDataReader["user_firstname"].ToString();
                    objemployee.user_lastname = objMySqlDataReader["user_lastname"].ToString();
                    objemployee.gender = objMySqlDataReader["employee_gender"].ToString();
                    objemployee.employee_emailid = objMySqlDataReader["employee_emailid"].ToString();
                    objemployee.employee_mobileno = objMySqlDataReader["employee_mobileno"].ToString();
                    objemployee.user_code = objMySqlDataReader["user_code"].ToString();
                    objemployee.role_gid = objMySqlDataReader["role_gid"].ToString();
                    objemployee.role_name = objMySqlDataReader["role_name"].ToString();
                    objemployee.employee_reportingto = objMySqlDataReader["employeereporting_to"].ToString();
                    objemployee.baselocation_gid = objMySqlDataReader["baselocation_gid"].ToString();
                    objemployee.baselocation_name = objMySqlDataReader["baselocation_name"].ToString();
                    objemployee.marital_status = objMySqlDataReader["marital_status"].ToString();
                    objemployee.marital_status_gid = objMySqlDataReader["marital_status_gid"].ToString();
                    objemployee.joining_date = objMySqlDataReader["joiningdate"].ToString();
                    objemployee.personal_phone_no = objMySqlDataReader["personal_phone_no"].ToString();
                    objemployee.personal_emailid = objMySqlDataReader["personal_emailid"].ToString();
                    objemployee.bloodgroup_name = objMySqlDataReader["bloodgroup_name"].ToString();
                    objemployee.bloodgroup_gid = objMySqlDataReader["bloodgroup_gid"].ToString();
                    if (objMySqlDataReader["employee_joiningdate"].ToString() != "")
                    {
                        objemployee.joiningdate = Convert.ToDateTime(objMySqlDataReader["employee_joiningdate"].ToString());
                    }
                }
                msSQL = "select concat(user_code, ' || ', user_firstname,' ',user_lastname) as employee_reportingto_name" +
                        " from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid = b.user_gid" +
                        " where employee_gid = '" + objemployee.employee_reportingto + "'";
                objemployee.employee_reportingto_name = objdbconn.GetExecuteScalar(msSQL);

                string lspermanentaddressGID = string.Empty;
                string lstemporaryaddressGID = string.Empty;

                msSQL = " Select permanentaddress_gid,temporaryaddress_gid from " +
                " hrm_trn_temployeedtl where " +
                " employee_gid = '" + employee_gid + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lspermanentaddressGID = objMySqlDataReader["permanentaddress_gid"].ToString();
                    lstemporaryaddressGID = objMySqlDataReader["temporaryaddress_gid"].ToString();
                }
                msSQL = " Select a.address1,a.address2,a.city, " +
                " a.postal_code,a.state,b.country_name,b.country_gid" +
                " from adm_mst_taddress a,adm_mst_tcountry b " +
                " where  address_gid = '" + lspermanentaddressGID + "' and " +
                " a.country_gid = b.country_gid and " +
                " a.parent_gid = '" + employee_gid + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objemployee.per_address1 = objMySqlDataReader["address1"].ToString();
                    objemployee.per_address2 = objMySqlDataReader["address2"].ToString();
                    objemployee.per_country_gid = objMySqlDataReader["country_gid"].ToString();
                    objemployee.per_country_name = objMySqlDataReader["country_name"].ToString();
                    objemployee.per_state = objMySqlDataReader["state"].ToString();
                    objemployee.per_city = objMySqlDataReader["city"].ToString();
                    objemployee.per_postal_code = objMySqlDataReader["postal_code"].ToString();
                }

                msSQL = " Select a.address1,a.address2,a.city, " +
                " a.postal_code,a.state,b.country_name,b.country_gid" +
                " from adm_mst_taddress a,adm_mst_tcountry b " +
                " where  address_gid = '" + lstemporaryaddressGID + "' and " +
                " a.country_gid = b.country_gid and " +
                " a.parent_gid = '" + employee_gid + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objemployee.temp_address1 = objMySqlDataReader["address1"].ToString();
                    objemployee.temp_address2 = objMySqlDataReader["address2"].ToString();
                    objemployee.temp_country_gid = objMySqlDataReader["country_gid"].ToString();
                    objemployee.temp_country_name = objMySqlDataReader["country_name"].ToString();
                    objemployee.temp_state = objMySqlDataReader["state"].ToString();
                    objemployee.temp_city = objMySqlDataReader["city"].ToString();
                    objemployee.temp_postal_code = objMySqlDataReader["postal_code"].ToString();
                }
                objMySqlDataReader.Close();
                objemployee.status = true;
                return true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaEmployeePendingEditView(employee objemployee, string employee_gid)
        {
            try
            {
                
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , user_firstname,user_lastname," +
                   " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ," +
                   " c.employee_gender,c.role_gid,employeereporting_to,  " +
                   " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                   " d.designation_name,c.designation_gid,c.employee_gid,e.branch_name, employee_emailid,employee_mobileno,baselocation_name,s.baselocation_gid,c.entity_gid," +
                   "c.subfunction_gid,x.subfunction_name," +
                   " CASE " +
                   " WHEN a.user_status = 'Y' THEN 'Active'  " +
                   " WHEN a.user_status = 'N' THEN 'Inactive' " +
                   " END as user_access,a.user_status,c.department_gid,c.branch_gid,n.role_name, e.branch_name, g.department_name,c.marital_status, " +
                  " c.marital_status_gid, date_format(c.employee_joiningdate,'%d-%m-%Y') as joiningdate,c.employee_joiningdate as joining_date,c.employee_personalno as personal_phone_no,c.personal_emailid,c.bloodgroup as bloodgroup_name,c.bloodgroup_gid " +
                   " FROM hrm_mst_temployee c " +
                   " left join adm_mst_tuser a on a.user_gid = c.user_gid " +
                   " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                   " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                   " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                   "left join sys_mst_tsubfunction x on x.subfunction_gid=c.subfunction_gid " +
                   " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                   " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                   " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                   " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid" +
                   " left join hrm_mst_trole n on n.role_gid=c.role_gid " +
                   " left join sys_mst_tbaselocation s on s.baselocation_gid = c.baselocation_gid " +
                   " where c.employee_gid='" + employee_gid + "'" +
                   " group by c.employee_gid " +
                   " order by c.employee_gid desc  ";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objemployee.company_name = objMySqlDataReader["entity_name"].ToString();
                    objemployee.entity_gid = objMySqlDataReader["entity_gid"].ToString();
                    objemployee.branch_gid = objMySqlDataReader["branch_gid"].ToString();
                    objemployee.branch_name = objMySqlDataReader["branch_name"].ToString();
                    objemployee.department_gid = objMySqlDataReader["department_gid"].ToString();
                    objemployee.department_name = objMySqlDataReader["department_name"].ToString();
                    objemployee.subfunction_gid = objMySqlDataReader["subfunction_gid"].ToString();
                    objemployee.subfunction_name = objMySqlDataReader["subfunction_name"].ToString();
                    objemployee.designation_gid = objMySqlDataReader["designation_gid"].ToString();
                    objemployee.designation_name = objMySqlDataReader["designation_name"].ToString();
                    objemployee.useraccess = objMySqlDataReader["useraccess"].ToString();
                    objemployee.user_access = objMySqlDataReader["user_access"].ToString();
                    objemployee.user_status = objMySqlDataReader["user_status"].ToString();
                    objemployee.user_firstname = objMySqlDataReader["user_firstname"].ToString();
                    objemployee.user_lastname = objMySqlDataReader["user_lastname"].ToString();
                    objemployee.gender = objMySqlDataReader["employee_gender"].ToString();
                    objemployee.employee_emailid = objMySqlDataReader["employee_emailid"].ToString();
                    objemployee.employee_mobileno = objMySqlDataReader["employee_mobileno"].ToString();
                    objemployee.user_code = objMySqlDataReader["user_code"].ToString();
                    objemployee.role_gid = objMySqlDataReader["role_gid"].ToString();
                    objemployee.role_name = objMySqlDataReader["role_name"].ToString();
                    objemployee.employee_reportingto = objMySqlDataReader["employeereporting_to"].ToString();
                    objemployee.baselocation_gid = objMySqlDataReader["baselocation_gid"].ToString();
                    objemployee.baselocation_name = objMySqlDataReader["baselocation_name"].ToString();
                    objemployee.marital_status = objMySqlDataReader["marital_status"].ToString();
                    objemployee.marital_status_gid = objMySqlDataReader["marital_status_gid"].ToString();
                    objemployee.joining_date = objMySqlDataReader["joiningdate"].ToString();
                    if (objMySqlDataReader["joining_date"].ToString() != "")
                    {
                        objemployee.joiningdate = Convert.ToDateTime(objMySqlDataReader["joining_date"].ToString());
                    }
                    objemployee.personal_phone_no = objMySqlDataReader["personal_phone_no"].ToString();
                    objemployee.personal_emailid = objMySqlDataReader["personal_emailid"].ToString();
                    objemployee.bloodgroup_name = objMySqlDataReader["bloodgroup_name"].ToString();
                    objemployee.bloodgroup_gid = objMySqlDataReader["bloodgroup_gid"].ToString();
                }
                msSQL = "select concat(user_code, ' || ', user_firstname,' ',user_lastname) as employee_reportingto_name" +
                        " from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid = b.user_gid" +
                        " where employee_gid = '" + objemployee.employee_reportingto + "'";
                objemployee.employee_reportingto_name = objdbconn.GetExecuteScalar(msSQL);

                string lspermanentaddressGID = string.Empty;
                string lstemporaryaddressGID = string.Empty;

                msSQL = " Select permanentaddress_gid,temporaryaddress_gid from " +
                " hrm_trn_temployeedtl where " +
                " employee_gid = '" + employee_gid + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lspermanentaddressGID = objMySqlDataReader["permanentaddress_gid"].ToString();
                    lstemporaryaddressGID = objMySqlDataReader["temporaryaddress_gid"].ToString();
                }
                msSQL = "Select a.address1,a.address2,a.city,  a.postal_code,a.state,b.country_gid,b.country_name  from adm_mst_taddress a " +
                        "left join adm_mst_tcountry b on a.country_gid=b.country_gid " +
                        "where  a.address_gid = '" + lspermanentaddressGID + "'  and  a.parent_gid = '" + employee_gid + "' group by a.address_gid";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objemployee.per_address1 = objMySqlDataReader["address1"].ToString();
                    objemployee.per_address2 = objMySqlDataReader["address2"].ToString();
                    objemployee.per_country_gid = objMySqlDataReader["country_gid"].ToString();
                    objemployee.per_country_name = objMySqlDataReader["country_name"].ToString();
                    objemployee.per_state = objMySqlDataReader["state"].ToString();
                    objemployee.per_city = objMySqlDataReader["city"].ToString();
                    objemployee.per_postal_code = objMySqlDataReader["postal_code"].ToString();
                }

                msSQL = "Select a.address1,a.address2,a.city,  a.postal_code,a.state,b.country_gid,b.country_name  from adm_mst_taddress a " +
                        "left join adm_mst_tcountry b on a.country_gid=b.country_gid " +
                        "where  a.address_gid = '" + lstemporaryaddressGID + "'  and  a.parent_gid = '" + employee_gid + "' group by a.address_gid";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objemployee.temp_address1 = objMySqlDataReader["address1"].ToString();
                    objemployee.temp_address2 = objMySqlDataReader["address2"].ToString();
                    objemployee.temp_country_gid = objMySqlDataReader["country_gid"].ToString();
                    objemployee.temp_country_name = objMySqlDataReader["country_name"].ToString();
                    objemployee.temp_state = objMySqlDataReader["state"].ToString();
                    objemployee.temp_city = objMySqlDataReader["city"].ToString();
                    objemployee.temp_postal_code = objMySqlDataReader["postal_code"].ToString();

                }
                objMySqlDataReader.Close();
                objemployee.status = true;
                return true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaEmployeeProfileView(employee objemployee, string employee_gid)
        {
            try
            {
                
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , user_firstname,user_lastname," +
                       " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                       " c.employee_gender,c.role_gid,employeereporting_to,  " +
                       " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                       " d.designation_name,c.designation_gid,c.employee_gid,e.branch_name, employee_emailid,employee_mobileno,baselocation_name,s.baselocation_gid,c.entity_gid," +
                       " CASE " +
                       " WHEN a.user_status = 'Y' THEN 'Active'  " +
                       " WHEN a.user_status = 'N' THEN 'Inactive' " +
                       " END as user_access,a.user_status,c.department_gid,c.branch_gid,n.role_name, e.branch_name, g.department_name,c.marital_status,c.marital_status_gid,date_format(c.employee_joiningdate, '%d-%m-%Y') as joiningdate, " +
                       " c.employee_personalno as personal_phone_no,c.personal_emailid,c.bloodgroup as bloodgroup_name,c.bloodgroup_gid " +
                       " FROM hrm_mst_temployee c " +
                       " left join adm_mst_tuser a on a.user_gid = c.user_gid " +
                       " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                       " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                       " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                       " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                       " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                       " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                       " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid" +
                       " left join hrm_mst_trole n on n.role_gid=c.role_gid " +
                       " left join sys_mst_tbaselocation s on s.baselocation_gid = c.baselocation_gid " +
                       " where c.employee_gid='" + employee_gid + "'" +
                       " group by c.employee_gid " +
                       " order by c.employee_gid desc ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objemployee.company_name = objMySqlDataReader["entity_name"].ToString();
                    objemployee.entity_gid = objMySqlDataReader["entity_gid"].ToString();
                    objemployee.branch_gid = objMySqlDataReader["branch_gid"].ToString();
                    objemployee.branch_name = objMySqlDataReader["branch_name"].ToString();
                    objemployee.department_gid = objMySqlDataReader["department_gid"].ToString();
                    objemployee.department_name = objMySqlDataReader["department_name"].ToString();
                    objemployee.designation_gid = objMySqlDataReader["designation_gid"].ToString();
                    objemployee.designation_name = objMySqlDataReader["designation_name"].ToString();
                    objemployee.useraccess = objMySqlDataReader["useraccess"].ToString();
                    objemployee.user_access = objMySqlDataReader["user_access"].ToString();
                    objemployee.user_status = objMySqlDataReader["user_status"].ToString();
                    objemployee.user_firstname = objMySqlDataReader["user_firstname"].ToString();
                    objemployee.user_lastname = objMySqlDataReader["user_lastname"].ToString();
                    objemployee.gender = objMySqlDataReader["employee_gender"].ToString();
                    objemployee.employee_emailid = objMySqlDataReader["employee_emailid"].ToString();
                    objemployee.employee_mobileno = objMySqlDataReader["employee_mobileno"].ToString();
                    objemployee.user_code = objMySqlDataReader["user_code"].ToString();
                    objemployee.role_gid = objMySqlDataReader["role_gid"].ToString();
                    objemployee.role_name = objMySqlDataReader["role_name"].ToString();
                    objemployee.employee_reportingto = objMySqlDataReader["employeereporting_to"].ToString();
                    objemployee.baselocation_gid = objMySqlDataReader["baselocation_gid"].ToString();
                    objemployee.baselocation_name = objMySqlDataReader["baselocation_name"].ToString();
                    objemployee.marital_status = objMySqlDataReader["marital_status"].ToString();
                    objemployee.marital_status_gid = objMySqlDataReader["marital_status_gid"].ToString();
                    objemployee.joining_date = objMySqlDataReader["joining_date"].ToString();
                    objemployee.personal_phone_no = objMySqlDataReader["personal_phone_no"].ToString();
                    objemployee.personal_emailid = objMySqlDataReader["personal_emailid"].ToString();
                    objemployee.bloodgroup_name = objMySqlDataReader["bloodgroup_name"].ToString();
                    objemployee.bloodgroup_gid = objMySqlDataReader["bloodgroup_gid"].ToString();
                    if (objMySqlDataReader["employee_joiningdate"].ToString() != "")
                    {
                        objemployee.joiningdate = Convert.ToDateTime(objMySqlDataReader["employee_joiningdate"].ToString());
                    }
                }
                msSQL = "select concat(user_code, ' || ', user_firstname,' ',user_lastname) as employee_reportingto_name" +
                        " from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid = b.user_gid" +
                        " where employee_gid = '" + objemployee.employee_reportingto + "'";
                objemployee.employee_reportingto_name = objdbconn.GetExecuteScalar(msSQL);

                string lspermanentaddressGID = string.Empty;
                string lstemporaryaddressGID = string.Empty;

                msSQL = " Select permanentaddress_gid,temporaryaddress_gid from " +
                        " hrm_trn_temployeedtl where " +
                        " employee_gid = '" + employee_gid + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lspermanentaddressGID = objMySqlDataReader["permanentaddress_gid"].ToString();
                    lstemporaryaddressGID = objMySqlDataReader["temporaryaddress_gid"].ToString();
                }
                msSQL = " Select a.address1,a.address2,a.city, " +
                        " a.postal_code,a.state,b.country_name,b.country_gid" +
                        " from adm_mst_taddress a,adm_mst_tcountry b " +
                        " where  address_gid = '" + lspermanentaddressGID + "' and " +
                        " a.country_gid = b.country_gid and " +
                        " a.parent_gid = '" + employee_gid + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objemployee.per_address1 = objMySqlDataReader["address1"].ToString();
                    objemployee.per_address2 = objMySqlDataReader["address2"].ToString();
                    objemployee.per_country_gid = objMySqlDataReader["country_gid"].ToString();
                    objemployee.per_country_name = objMySqlDataReader["country_name"].ToString();
                    objemployee.per_state = objMySqlDataReader["state"].ToString();
                    objemployee.per_city = objMySqlDataReader["city"].ToString();
                    objemployee.per_postal_code = objMySqlDataReader["postal_code"].ToString();
                }

                msSQL = " Select a.address1,a.address2,a.city, " +
                        " a.postal_code,a.state,b.country_name,b.country_gid" +
                        " from adm_mst_taddress a,adm_mst_tcountry b " +
                        " where  address_gid = '" + lstemporaryaddressGID + "' and " +
                        " a.country_gid = b.country_gid and " +
                        " a.parent_gid = '" + employee_gid + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objemployee.temp_address1 = objMySqlDataReader["address1"].ToString();
                    objemployee.temp_address2 = objMySqlDataReader["address2"].ToString();
                    objemployee.temp_country_gid = objMySqlDataReader["country_gid"].ToString();
                    objemployee.temp_country_name = objMySqlDataReader["country_name"].ToString();
                    objemployee.temp_state = objMySqlDataReader["state"].ToString();
                    objemployee.temp_city = objMySqlDataReader["city"].ToString();
                    objemployee.temp_postal_code = objMySqlDataReader["postal_code"].ToString();
                }
                objMySqlDataReader.Close();
                objemployee.status = true;
                return true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaGetHRDocProfilelist(string employee_gid, hrdoc_list objemployeedoc_list)
        {
            try
            {
                
                msSQL = " select hrdoc_id,hrdocument_gid,hrdocument_name,hrdoc_name, hrdoc_path, " +
                        " documentsentforsign_flag,esignexpiry_flag,documentsigned_flag, " +
                        " concat(c.user_firstname,' ', c.user_lastname,'/' ,c.user_code) as created_by, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,migration_flag " +
                        " from sys_mst_temployeehrdocument a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.employee_gid = '" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_hrdoc_list = new List<hrdoc>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_hrdoc_list.Add(new hrdoc_list
                        {
                            hrdoc_id = dr_datarow["hrdoc_id"].ToString(),
                            hrdocument_gid = dr_datarow["hrdocument_gid"].ToString(),
                            hrdocument_name = dr_datarow["hrdocument_name"].ToString(),
                            hrdoc_name = dr_datarow["hrdoc_name"].ToString(),
                            hrdoc_path = objcmnstorage.EncryptData((dr_datarow["hrdoc_path"].ToString())),
                            documentsentforsign_flag = dr_datarow["documentsentforsign_flag"].ToString(),
                            esignexpiry_flag = dr_datarow["esignexpiry_flag"].ToString(),
                            documentsigned_flag = dr_datarow["documentsigned_flag"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            migration_flag = dr_datarow["migration_flag"].ToString(),
                        });
                    }
                    objemployeedoc_list.hrdoc = get_hrdoc_list;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                objemployeedoc_list.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaEmployeePendingUpdate(employee objemployee, string employee_gid)
        {
            string user_gid = string.Empty;
            try
            {
                
                msSQL = "select user_gid  from hrm_mst_temployee where employee_gid='" + objemployee.employee_gid + "'";
                user_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " update adm_mst_tuser set " +
                " user_code = '" + objemployee.user_code + "', " +
                " user_firstname = '" + objemployee.user_firstname + "', " +
                " user_lastname = '" + objemployee.user_lastname + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                " where user_gid = '" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 0)
                {
                    objemployee.message = "Error Occured While Updating Records";
                    objemployee.status = false;
                    return false;
                }
                else
                {
                    msSQL = " Update hrm_mst_temployee set  " +
                     " branch_gid = '" + objemployee.branch_gid + "'," +
                     " baselocation_gid = '" + objemployee.baselocation_gid + "'," +
                     " designation_gid = '" + objemployee.designation_gid + "'," +
                     " subfunction_gid ='" + objemployee.subfunction_gid + "'," +
                     " employee_mobileno ='" + objemployee.employee_mobileno + "'," +
                     " employeereporting_to ='" + objemployee.employee_reportingto + "'," +
                     " role_gid ='" + objemployee.role_gid + "'," +
                     " employee_emailid = '" + objemployee.employee_emailid + "'," +
                     " employee_gender = '" + objemployee.gender + "'," +
                     " department_gid = '" + objemployee.department_gid + "' ," +
                     " marital_status = '" + objemployee.marital_status + "'," +
                     " bloodgroup = '" + objemployee.bloodgroup_name + "',";
                    if (objemployee.joining_date != null || objemployee.joining_date != "")
                        msSQL += " employee_joiningdate = '" + Convert.ToDateTime(objemployee.joining_date).ToString("yyyy-MM-dd") + "',";
                    msSQL += " employee_personalno = '" + objemployee.personal_phone_no.Replace("'", "") + "'," +
                     " personal_emailid = '" + objemployee.personal_emailid.Replace("'", "") + "'," +
                     " marital_status_gid = '" + objemployee.marital_status_gid + "'," +
                     " bloodgroup_gid = '" + objemployee.bloodgroup_gid + "'," +
                     " updated_by = '" + employee_gid + "'," +
                     " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                     " update_flag='N' " +
                     " where employee_gid = '" + objemployee.employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        msSQL = " update hrm_trn_temployeetypedtl set " +
                           " wagestype_gid='wg001', " +
                           " systemtype_gid='Actual', " +
                           " branch_gid = '" + objemployee.branch_gid + "'," +
                           " department_gid='" + objemployee.department_gid + "', " +
                           " designation_gid='" + objemployee.designation_gid + "', " +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                           " updated_by='" + employee_gid + "'" +
                           " where employee_gid = '" + objemployee.employee_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            string lsPerAddress_gid = string.Empty;
                            string lsTempAddress_gid = string.Empty;
                            msSQL = " Select permanentaddress_gid,temporaryaddress_gid from " +
                            " hrm_trn_temployeedtl where " +
                            " employee_gid = '" + objemployee.employee_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                lsPerAddress_gid = objMySqlDataReader["permanentaddress_gid"].ToString();
                                lsTempAddress_gid = objMySqlDataReader["temporaryaddress_gid"].ToString();
                            }
                            msSQL = " update adm_mst_taddress SET " +
                            " country_gid = '" + objemployee.per_country_gid + "', " +
                            " address1 =  '" + objemployee.per_address1 + "', " +
                            " address2 = '" + objemployee.per_address2 + "'," +
                            " city = '" + objemployee.per_city + "', " +
                            " postal_code = '" + objemployee.per_postal_code + "'," +
                            " state = '" + objemployee.per_state + "' " +
                            " where address_gid = '" + lsPerAddress_gid + "' and " +
                            " parent_gid = '" + objemployee.employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update adm_mst_taddress SET " +
                           " country_gid = '" + objemployee.temp_country_gid + "', " +
                           " address1 =  '" + objemployee.temp_address1 + "', " +
                           " address2 = '" + objemployee.temp_address2 + "'," +
                           " city = '" + objemployee.temp_city + "', " +
                           " postal_code = '" + objemployee.temp_postal_code + "'," +
                           " state = '" + objemployee.temp_state + "' " +
                           " where address_gid = '" + lsTempAddress_gid + "' and " +
                           " parent_gid = '" + objemployee.employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany)";
                            lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select hierarchy_level from adm_mst_tmodule2employee a where " +
                           " a.module_gid='" + lsmodulereportingto_gid + "' and a.employee_gid='" + objemployee.employee_reportingto + "'";
                            lshierarchy_level = objdbconn.GetExecuteScalar(msSQL);
                            hierarchy_level = Convert.ToInt16(lshierarchy_level);
                            hierarchy_level += 1;
                            lshierarchy_level = Convert.ToString(hierarchy_level);
                            msSQL = "update adm_mst_tmodule2employee set " +
                                    " employeereporting_to='" + objemployee.employee_reportingto + "'," +
                                    " hierarchy_level = '" + lshierarchy_level + "' " +
                                    " where employee_gid='" + objemployee.employee_gid + "' and module_gid='" + lsmodulereportingto_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                        else
                        {
                            objemployee.message = "Error while updating Employee Details";
                            objemployee.status = false;
                            return false;
                        }
                    }
                    else
                    {
                        objemployee.message = "Error while updating Employee Details";
                        objemployee.status = false;
                        return false;
                    }
                    objemployee.message = "Employee Details has been Updated Successfullly";
                    objemployee.status = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public bool DaEmployeeUpdate(employee objemployee, string employee_gid)
        {
            string type = "Employee";
            string user_gid = string.Empty;
            try
            {
                
                msSQL = "select user_gid  from hrm_mst_temployee where employee_gid='" + objemployee.employee_gid + "'";
                user_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " update adm_mst_tuser set " +
                " user_code = '" + objemployee.user_code + "', " +
                " user_firstname = '" + objemployee.user_firstname + "', " +
                " user_lastname = '" + objemployee.user_lastname + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                " where user_gid = '" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 0)
                {
                    objemployee.message = "Error Occured While Updating Records";
                    objemployee.status = false;
                    return false;
                }
                else
                {
                    msSQL = " select a.employee_emailid,d.baselocation_name,a.employee_mobileno,b.user_firstname,b.user_lastname,b.user_code, employeereporting_to, case when a.entity_gid is null then c.entity_name else c.entity_name end as entity_name, a.department_gid " +
                            " from hrm_mst_temployee a" +
                            " left join adm_mst_tuser b on a.user_gid = b.user_gid" +
                            " left join adm_mst_tentity c on c.entity_gid=a.entity_gid" +
                            " left join sys_mst_tbaselocation d on d.baselocation_gid = a.baselocation_gid " +
                            " where a.employee_gid='" + objemployee.employee_gid + "'";

                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {

                        lsentity_name = objMySqlDataReader["entity_name"].ToString();
                        lsemployee_emailid = objMySqlDataReader["employee_emailid"].ToString();
                        lsemployee_mobileno = objMySqlDataReader["employee_mobileno"].ToString();
                        lsuser_firstname = objMySqlDataReader["user_firstname"].ToString();
                        lsuser_lastname = objMySqlDataReader["user_lastname"].ToString();
                        lsuser_code = objMySqlDataReader["user_code"].ToString();
                        lsreporting_to = objMySqlDataReader["employeereporting_to"].ToString();
                        lsbase_location = objMySqlDataReader["baselocation_name"].ToString();
                        lsdepartment_gid = objMySqlDataReader["department_gid"].ToString();

                    }
                    objMySqlDataReader.Close();
                    msSQL = "select concat(user_firstname,' ', user_lastname) as employeereporting_to" +
                            " from hrm_mst_temployee a" +
                            " left join adm_mst_tuser b on a.user_gid = b.user_gid" +
                            " where a.employee_gid='" + lsreporting_to + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        lsreporting_toname = objMySqlDataReader["employeereporting_to"].ToString();
                    }
                    objMySqlDataReader.Close();
                    msSQL = " Update hrm_mst_temployee set  " +
                     " branch_gid = '" + objemployee.branch_gid + "'," +
                     " baselocation_gid = '" + objemployee.baselocation_gid + "'," +
                     " designation_gid = '" + objemployee.designation_gid + "'," +
                     " subfunction_gid ='" + objemployee.subfunction_gid + "'," +
                     " employee_mobileno ='" + objemployee.employee_mobileno + "'," +
                     " employeereporting_to ='" + objemployee.employee_reportingto + "'," +
                     " role_gid ='" + objemployee.role_gid + "'," +
                     " employee_emailid = '" + objemployee.employee_emailid + "'," +
                     " employee_gender = '" + objemployee.gender + "'," +
                     " department_gid = '" + objemployee.department_gid + "' ," +
                     " marital_status = '" + objemployee.marital_status + "'," +
                     " bloodgroup = '" + objemployee.bloodgroup_name + "'," +
                     " employee_joiningdate = '" + Convert.ToDateTime(objemployee.joining_date).ToString("yyyy-MM-dd") + "'," +
                     " employee_personalno = '" + objemployee.personal_phone_no.Replace("'", "") + "'," +
                     " personal_emailid = '" + objemployee.personal_emailid.Replace("'", "") + "'," +
                     " marital_status_gid = '" + objemployee.marital_status_gid + "'," +
                     " bloodgroup_gid = '" + objemployee.bloodgroup_gid + "'," +
                     " updated_by = '" + employee_gid + "'," +
                     " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                     " update_flag='N' " +
                     " where employee_gid = '" + objemployee.employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        msSQL = " update hrm_trn_temployeetypedtl set " +
                           " wagestype_gid='wg001', " +
                           " systemtype_gid='Actual', " +
                           " branch_gid = '" + objemployee.branch_gid + "'," +
                           " department_gid='" + objemployee.department_gid + "', " +
                           " designation_gid='" + objemployee.designation_gid + "', " +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                           " updated_by='" + employee_gid + "'" +
                           " where employee_gid = '" + objemployee.employee_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            string lsPerAddress_gid = string.Empty;
                            string lsTempAddress_gid = string.Empty;
                            msSQL = " Select permanentaddress_gid,temporaryaddress_gid from " +
                            " hrm_trn_temployeedtl where " +
                            " employee_gid = '" + objemployee.employee_gid + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {
                                lsPerAddress_gid = objMySqlDataReader["permanentaddress_gid"].ToString();
                                lsTempAddress_gid = objMySqlDataReader["temporaryaddress_gid"].ToString();
                            }
                            msSQL = " update adm_mst_taddress SET " +
                            " country_gid = '" + objemployee.per_country_gid + "', " +
                            " address1 =  '" + objemployee.per_address1 + "', " +
                            " address2 = '" + objemployee.per_address2 + "'," +
                            " city = '" + objemployee.per_city + "', " +
                            " postal_code = '" + objemployee.per_postal_code + "'," +
                            " state = '" + objemployee.per_state + "' " +
                            " where address_gid = '" + lsPerAddress_gid + "' and " +
                            " parent_gid = '" + objemployee.employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update adm_mst_taddress SET " +
                           " country_gid = '" + objemployee.temp_country_gid + "', " +
                           " address1 =  '" + objemployee.temp_address1 + "', " +
                           " address2 = '" + objemployee.temp_address2 + "'," +
                           " city = '" + objemployee.temp_city + "', " +
                           " postal_code = '" + objemployee.temp_postal_code + "'," +
                           " state = '" + objemployee.temp_state + "' " +
                           " where address_gid = '" + lsTempAddress_gid + "' and " +
                           " parent_gid = '" + objemployee.employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            objemployee.message = "Error while updating Employee Details - 1";
                            objemployee.status = false;
                            return false;
                        }
                    }
                    else
                    {
                        objemployee.message = "Error while updating Employee Details - 2";
                        objemployee.status = false;
                        return false;
                    }

                    msSQL = "select baselocation_name from sys_mst_tbaselocation where baselocation_gid = '" + objemployee.baselocation_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        ls_baselocation = objMySqlDataReader["baselocation_name"].ToString();
                    }
                    objMySqlDataReader.Close();

                    objemployee.message = "Employee Details has been Updated Successfullly";
                    objemployee.status = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public void DaGetTaskList(MdlTaskList values)
        {
            try
            {
                
                msSQL = " SELECT task_gid,task_name FROM sys_mst_ttask a" +
                    " where status='Y' and delete_flag='N' order by a.task_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettask_list = new List<tasklists>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettask_list.Add(new tasklists
                        {
                            task_gid = (dr_datarow["task_gid"].ToString()),
                            task_name = (dr_datarow["task_name"].ToString()),
                        });
                    }
                    values.tasklists = gettask_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public bool DaPostTask(string employee_gid, string user_gid, List<MdlEmployeetasklist> values)
        {
            bool status = true;
            string GetGroupingteamGid = string.Join(",", values.Select(item => $"'{item.team_gid}'"));
            List<Teamdetail> objTeamGid = new List<Teamdetail>();
            List<TeamMemberdetail> objTeamMemberdetail = new List<TeamMemberdetail>();
            msSQL = "select teammanager_gid,teammanager_name,team_gid from sys_mst_tteam where team_gid in (" + GetGroupingteamGid + ")";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
                objTeamGid = cmnfunctions.ConvertDataTable<Teamdetail>(dt_datatable);
            dt_datatable.Dispose();

            msSQL = " select team2member_gid,team_gid,member_gid, member_name from sys_mst_tteam2member " +
                    " where team_gid in (" + GetGroupingteamGid + ")";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
                objTeamMemberdetail = cmnfunctions.ConvertDataTable<TeamMemberdetail>(dt_datatable);
            dt_datatable.Dispose();

            foreach (var i in values)
            {
                if (objTeamGid != null)
                {
                    var getManagerInfo = objTeamGid.Where(a => a.team_gid == i.team_gid).FirstOrDefault();
                    i.teammanager_gid = getManagerInfo.teammanager_gid;
                    i.teammanager_name = getManagerInfo.teammanager_name;
                }

                msGetGid = objcmnfunctions.GetMasterGID("TKIN");
                msSQL = " insert into sys_mst_ttaskinitiate(" +
                        " taskinitiate_gid," +
                        " employee_gid," +
                        " task_gid," +
                        " task_name," +
                        " team_gid, " +
                        " team_name, " +
                        " teammanager_gid, " +
                        " teammanager_name, " +
                        " task_remarks," +
                        " created_by, " +
                        " created_date)" +
                        " values(" +
                        " '" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + i.task_gid + "'," +
                        "'" + i.task_name.Replace("'", "") + "'," +
                        "'" + i.team_gid + "'," +
                        "'" + i.team_name.Replace("'", "") + "'," +
                        "'" + i.teammanager_gid + "'," +
                        "'" + i.teammanager_name + "',";
                if (i.task_remarks == null || i.task_remarks == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + i.task_remarks.Replace("'", "") + "',";
                }
                msSQL += "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    var objTeamMemberinfo = objTeamMemberdetail.Where(a => a.team_gid == i.team_gid).ToList();
                    foreach (var j in objTeamMemberinfo)
                    {
                        string msGetTeamGid = objcmnfunctions.GetMasterGID("TATM");
                        msSQL = " insert into sys_trn_tteam2member(" +
                              " trnteam2member_gid," +
                              " taskinitiate_gid," +
                              " team2member_gid," +
                              " team_gid," +
                              " member_gid, " +
                              " member_name, " +
                              " created_by, " +
                              " created_date)" +
                              " values(" +
                              "'" + msGetTeamGid + "'," +
                              "'" + msGetGid + "'," +
                              "'" + j.team2member_gid + "'," +
                              "'" + j.team_gid + "'," +
                              "'" + j.member_gid + "'," +
                              "'" + j.member_name + "'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }

            if (mnResult == 0)
                status = false;
            return status;
        }

        public void DaGetTeamList(MdlTeamList values)
        {
            try
            {
                
                msSQL = " SELECT team_gid,team_name FROM sys_mst_tteam a" +
                    " where status='Y' order by a.team_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getteam_list = new List<teamlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getteam_list.Add(new teamlist
                        {
                            team_gid = (dr_datarow["team_gid"].ToString()),
                            team_name = (dr_datarow["team_name"].ToString()),
                        });
                    }
                    values.teamlist = getteam_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetTaskList(string employee_gid, string user_gid, MdlTaskList values)
        {
            try
            {
                
                msSQL = " SELECT task_gid,task_name FROM sys_mst_ttask a" +
                    " where status='Y' and delete_flag='N' and task_gid not in " +
                    " (select task_gid from sys_mst_ttaskinitiate where employee_gid ='" + user_gid + "' " +
                    " or employee_gid ='" + employee_gid + "') order by a.task_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettask_list = new List<tasklists>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettask_list.Add(new tasklists
                        {
                            task_gid = (dr_datarow["task_gid"].ToString()),
                            task_name = (dr_datarow["task_name"].ToString()),
                        });
                    }
                    values.tasklists = gettask_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaPostHRDocumentUpload(HttpRequest httpRequest, HRuploaddocument objfilename, string employee_gid)
        {
            // upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;

            string lsdocumenttype_gid = string.Empty;
            string hrdocument_name = httpRequest.Form["document_name"];
            string hrdocument_gid = httpRequest.Form["document_gid"];
            string employee = httpRequest.Form["employee_gid"];
            string hrdocumentList = httpRequest.Form["hrdocumentList"];
            List<HrDocument> hrDocuments = JsonConvert.DeserializeObject<List<HrDocument>>(hrdocumentList);

            String path = lspath;
            HttpPostedFile httpPostedFile;



            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/HRDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        var getdocumentdtl = hrDocuments.Where(a => a.AutoID_Key == httpFileCollection.AllKeys[i]).FirstOrDefault();
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        string project_flag = httpRequest.Form["project_flag"].ToString();
                        FileExtension = Path.GetExtension(FileExtension).ToLower();

                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "System/HRDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "System/HRDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("HRDU");
                        msSQL = " insert into sys_mst_temployeehrdocument(" +
                                    " hrdoc_id," +
                                    " employee_gid ," +
                                    " hrdocument_gid," +
                                    " hrdocument_name," +
                                    " hrdoc_name ," +
                                    " hrdoc_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee + "'," +
                                    "'" + getdocumentdtl.hrdocument_gid + "'," +
                                    "'" + getdocumentdtl.hrdocument_name.Replace("'", @"\'") + "'," +
                                    "'" + getdocumentdtl.file_name + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
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
                            objfilename.message = "Error Occured While Uploading the document";

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }
        public bool DaEmployeeAdd(employee objemployee, string employee_gid, string user_gid)
        {

            msSQL = "select user_gid from adm_mst_tuser where user_code = '" + objemployee.user_code + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objemployee.message = "User Code Already in Use";
                return false;
            }
            else
            {

                string lsgender = string.Empty;
                string msBiometricGID = string.Empty;
                string msUserGid = string.Empty;
                string msPermanentAddressGetGID = string.Empty;
                string msTemporaryAddressGetGID = string.Empty;
                string str1 = string.Empty;
                string msEmployeedtlGid = string.Empty;
                string msGetleaveGID = string.Empty;
                string msGetemployeetype = string.Empty;
                string msgetgidof = string.Empty;
                string msGetemployeelog = string.Empty;

                try
                {
                    msUserGid = objcmnfunctions.GetMasterGID("SUSM");
                    if (msUserGid == "E")
                    {
                        objemployee.message = "Create Sequence Code SUSM for User Table";
                        objemployee.status = false;
                    }
                    msEmployeeGID = objcmnfunctions.GetMasterGID("SERM");
                    if (msEmployeeGID == "E")
                    {
                        objemployee.message = "Create Sequence Code SERM for Employee Table";
                        objemployee.status = false;
                    }
                    msPermanentAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
                    if (msPermanentAddressGetGID == "E")
                    {
                        objemployee.message = "Create Sequence Code SADM for Address Table";
                        objemployee.status = false;
                    }
                    msTemporaryAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
                    if (msTemporaryAddressGetGID == "E")
                    {
                        objemployee.message = "Create Sequence Code SADM for Address Table";
                        objemployee.status = false;
                    }
                    msEmployeedtlGid = objcmnfunctions.GetMasterGID("HEMC");
                    if (msEmployeedtlGid == "E")
                    {
                        objemployee.message = "Create Sequence Code HEMC for Employee Detail Table";
                        objemployee.status = false;
                    }
                    //str1 = objcmnfunctions.PopTransactionUpload(objemployee.employee_photo, objemployee.employee_gid, "System", "Photos");
                    if (objemployee.user_password != null & objemployee.user_password != string.Empty & objemployee.user_password != "")
                    {
                        var encripedpassword = objcmnfunctions.ConvertToAscii(objemployee.user_password);
                        msSQL = " Insert into adm_mst_tuser ( " +
                         " user_gid , " +
                         " user_code , " +
                         " user_firstname , " +
                         " user_lastname , " +
                         " user_password , " +
                         " user_status  ," +
                         " created_by, " +
                         " created_date " +
                         " )values ( " +
                         "'" + msUserGid + "'," +
                         " '" + objemployee.user_code + "'," +
                         " '" + objemployee.user_firstname + "'," +
                         " '" + objemployee.user_lastname + "'," +
                         " '" + encripedpassword + "'," +
                         "'Y'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = " Insert into adm_mst_tuser ( " +
                         " user_gid , " +
                         " user_code , " +
                         " user_firstname , " +
                         " user_lastname , " +
                         " user_status , " +
                         " created_by, " +
                         " created_date " +
                         " )values ( " +
                         "'" + msUserGid + "'," +
                         " '" + objemployee.user_code + "'," +
                         " '" + objemployee.user_firstname + "'," +
                         " '" + objemployee.user_lastname + "'," +
                         "'Y'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }


                    if (mnResult == 1)
                    {
                        msSQL = " Insert into hrm_mst_temployee " +
                               " (employee_gid , " +
                               " user_gid," +
                               " employee_externalid," +
                               " designation_gid," +
                               " employee_mobileno , " +
                               " employee_emailid , " +
                               " employee_gender , " +
                               " department_gid," +
                               " employee_photo," +
                               " useraccess," +
                               " engagement_type," +
                               " created_by, " +
                               " created_date, " +
                               " employeereporting_to, " +
                               " role_gid," +
                               " attendance_flag, " +
                               " branch_gid, " +
                               " baselocation_gid, " +
                               " marital_status, " +
                               " marital_status_gid, " +
                               " bloodgroup," +
                               " bloodgroup_gid," +
                               " employee_joiningdate," +
                               " employee_personalno," +
                               " personal_emailid, " +
                               " employee_status," +
                               " subfunction_gid " +
                               " )values( " +
                               "'" + msEmployeeGID + "', " +
                               "'" + msUserGid + "', " +
                               "'" + msEmployeeGID + "', " +
                               "'" + objemployee.designation_gid + "'," +
                               "'" + objemployee.employee_mobileno + "'," +
                               "'" + objemployee.employee_emailid + "'," +
                               "'" + objemployee.gender + "'," +
                               "'" + objemployee.department_gid + "'," +
                               "'" + str1 + "'," +
                               "'" + objemployee.useraccess + "'," +
                               "'Direct'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + objemployee.employee_reportingto + "'," +
                               "'" + objemployee.role_gid + "'," +
                               " 'Y', " +
                               "'" + objemployee.branch_gid + "'," +
                               "'" + objemployee.baselocation_gid + "'," +
                               "'" + objemployee.marital_status + "'," +
                               "'" + objemployee.marital_status_gid + "'," +
                               "'" + objemployee.bloodgroup_name + "'," +
                               "'" + objemployee.bloodgroup_gid + "'," +
                               "'" + objemployee.joiningdate.ToString("yyyy-MM-dd") + "'," +
                               "'" + objemployee.personal_phone_no + "'," +
                               "'" + objemployee.personal_emailid + "'," +
                               "'A'," +
                               "'" + objemployee.subfunction_gid + "')";
                        mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);
                        string lsentity_flag;
                        msSQL = "SELECT entity_flag from adm_mst_tcompany where 1=1";
                        lsentity_flag = objdbconn.GetExecuteScalar(msSQL);
                        if (lsentity_flag == "Y")
                        {
                            msSQL = " update hrm_mst_temployee set entity_gid='" + objemployee.entity_gid + "' where" +
                                   " employee_gid='" + msEmployeeGID + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            msSQL = " update hrm_mst_temployee set entity_name='" + objemployee.company_name + "'" +
                                    " where employee_gid='" + msEmployeeGID + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        msGetleaveGID = objcmnfunctions.GetMasterGID("HLCP");
                        msSQL = "Insert Into hrm_trn_tleavecredits " +
                            "(leavecredits_gid," +
                            "employee_gid)" +
                            " VALUES " +
                            "('" + msGetleaveGID + "'," +
                            " '" + msEmployeeGID + "')";
                        mnResult6 = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    if (mnResult == 1)
                    {
                        msGetemployeetype = objcmnfunctions.GetMasterGID("SETD");
                        if (msGetemployeetype == "E")
                        {
                            objemployee.message = "Create Sequence Code SETD for Employee Table";
                            objemployee.status = false;
                            return false;
                        }

                        msSQL = " insert into hrm_trn_temployeetypedtl(" +
                            " employeetypedtl_gid," +
                            " employee_gid, " +
                            " workertype_gid," +
                            " systemtype_gid, " +
                            " branch_gid," +
                            " wagestype_gid," +
                            " department_gid," +
                            " designation_gid," +
                            " employeetype_name," +
                            " created_date," +
                            "created_by)" +
                            " values " +
                            " ('" + msGetemployeetype + "', " +
                            " '" + msEmployeeGID + "'," +
                            " null, " +
                            " 'Audit', " +
                            " '" + objemployee.branch_gid + "'," +
                            " 'wg001', " +
                            " '" + objemployee.department_gid + "'," +
                            " '" + objemployee.designation_gid + "'," +
                            " 'Roll', " +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            " '" + employee_gid + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 0)
                        {
                            objemployee.message = "Error while inserting employeetype";
                            objemployee.status = false;
                        }
                        msSQL = " insert into adm_mst_taddress " +
                                " (address_gid ," +
                                " parent_gid," +
                                " country_gid, " +
                                " address1, " +
                                " address2, " +
                                " city, " +
                                " postal_code," +
                                " state) " +
                                " values( " +
                                " '" + msPermanentAddressGetGID + "', " +
                                " '" + msEmployeeGID + "', " +
                                " '" + objemployee.per_country_gid + "', " +
                                " '" + objemployee.per_address1 + "', " +
                                " '" + objemployee.per_address2 + "', " +
                                " '" + objemployee.per_city + "', " +
                                " '" + objemployee.per_postal_code + "'," +
                                " '" + objemployee.per_state + "')";
                        mnResult2 = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " insert into adm_mst_taddress " +
                               " (address_gid ," +
                               " parent_gid," +
                               " country_gid, " +
                               " address1, " +
                               " address2, " +
                               " city, " +
                               " postal_code," +
                               " state) " +
                               " values( " +
                               " '" + msTemporaryAddressGetGID + "', " +
                               " '" + msEmployeeGID + "', " +
                               " '" + objemployee.temp_country_gid + "', " +
                               " '" + objemployee.temp_address1 + "', " +
                               " '" + objemployee.temp_address2 + "', " +
                               " '" + objemployee.temp_city + "', " +
                               " '" + objemployee.temp_postal_code + "'," +
                               " '" + objemployee.temp_state + "')";
                        mnResult3 = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult3 == 1)
                        {
                            msSQL = " Insert into hrm_trn_temployeedtl " +
                                " (employeedtl_gid," +
                                " permanentaddress_gid ," +
                                " temporaryaddress_gid," +
                                " employee_gid)" +
                                " VALUE ( " +
                                " '" + msEmployeedtlGid + "'," +
                                " '" + msPermanentAddressGetGID + "'," +
                                " '" + msTemporaryAddressGetGID + "' ," +
                                " '" + msEmployeeGID + "')";
                            mnResult4 = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    //Employee Status - Pending 
                    msGetemployeelog = objcmnfunctions.GetMasterGID("EMLG");
                    msSQL = " Insert into sys_mst_temployeelog " +
                                " (employeelog_gid," +
                                " employee_gid ," +
                                " user_gid," +
                                " user_name," +
                                " created_by," +
                                " status_flag, " +
                                " created_date)" +
                                " VALUE ( " +
                                " '" + msGetemployeelog + "'," +
                                " '" + msEmployeeGID + "'," +
                                "'" + msUserGid + "', " +
                                " '" + objemployee.user_firstname + " " + objemployee.user_lastname + "/" + objemployee.user_code + "'," +
                                " '" + employee_gid + "'," +
                                "'Y', " +
                                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                    mnResult4 = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany )";
                    //lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = "select module2employee_gid from adm_mst_tmodule2employee a  where" +
                    //    " a.module_gid='" + lsmodulereportingto_gid + "' and a.employee_gid='" + msEmployeeGID + "'";
                    //lsmodule2employee_gid = objdbconn.GetExecuteScalar(msSQL);
                    //if (lsmodule2employee_gid == "" || lsmodule2employee_gid == null)
                    //{
                    //    msSQL = "select hierarchy_level from adm_mst_tmodule2employee a where " +
                    //    " a.module_gid='" + lsmodulereportingto_gid + "' and a.employee_gid='" + objemployee.employee_reportingto + "'";
                    //    lshierarchy_level = objdbconn.GetExecuteScalar(msSQL);
                    //    hierarchy_level = Convert.ToInt16(lshierarchy_level);
                    //    hierarchy_level += 1;
                    //    lshierarchy_level = Convert.ToString(hierarchy_level);
                    //    msGetModule2employee_gid = objcmnfunctions.GetMasterGID("SMEM");


                    //    msSQL = " insert into adm_mst_tmodule2employee( " +
                    //         " module2employee_gid, " +
                    //         " module_gid, " +
                    //         " employee_gid, " +
                    //         " hierarchy_level," +
                    //         " employeereporting_to, " +
                    //         " created_by, " +
                    //         " created_date) " +
                    //      " values(" +
                    //      "'" + msGetModule2employee_gid + "'," +
                    //      "'" + lsmodulereportingto_gid + "'," +
                    //      "'" + msEmployeeGID + "'," +
                    //      "'" + lshierarchy_level + "'," +
                    //      "'" + objemployee.employee_reportingto + "'," +
                    //      "'" + user_gid + "'," +
                    //      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    //}

                    //else
                    //{
                    //    msSQL = "select hierarchy_level from adm_mst_tmodule2employee a where " +
                    //   " a.module_gid='" + lsmodulereportingto_gid + "' and a.employee_gid='" + objemployee.employee_reportingto + "'";
                    //    lshierarchy_level = objdbconn.GetExecuteScalar(msSQL);

                    //    hierarchy_level = Convert.ToInt16(lshierarchy_level);
                    //    hierarchy_level += 1;
                    //    lshierarchy_level = Convert.ToString(hierarchy_level);
                    //    msSQL = "Update adm_mst_tmodule2employee set employeereporting_to='" + objemployee.employee_reportingto + "' , " +
                    //            " hierarchy_level='" + lshierarchy_level + "' ," +
                    //            " updated_by ='" + user_gid + "'," +
                    //            " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    //            " where module_gid='" + lsmodulereportingto_gid + "' and employee_gid='" + msEmployeeGID + "'";

                    //}
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Task Initiation & HR DOcument
                    bool TaskSTtaus;
                    if (objemployee.MdlEmployeetasklist != null && objemployee.MdlEmployeetasklist.Count != 0)
                        TaskSTtaus = DaPostTask(msEmployeeGID, employee_gid, objemployee.MdlEmployeetasklist);

                    objemployee.employee_gid = msEmployeeGID;
                    objemployee.message = "Employee Added Successfully";
                    objemployee.status = true;
                    return true;

                }
                catch (Exception ex)
                {
                    objemployee.status = false;
                    objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                    "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                    return false;
                }

            }
        }

        public void DaGetMyTaskCompleteSummary(string employee_gid, MdlTaskList values)
        {
            try
            {
                
                msSQL = " select a.task_name,a.taskinitiate_gid,a.complete_flag,a.task_completeremarks,a.task_remarks,concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as created_by, " +
            " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as completed_by, " +
            " date_format(a.completed_date, '%d-%m-%Y %h:%i %p') as completed_date , a.task_status," +
            " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as employee_name, p.employee_gid" +
            " from sys_mst_ttaskinitiate a " +
            " left join hrm_mst_temployee e on a.created_by = e.employee_gid " +
            " left join adm_mst_tuser f on e.user_gid = f.user_gid " +
            " left join hrm_mst_temployee h on a.completed_by = h.employee_gid " +
            " left join adm_mst_tuser j on h.user_gid = j.user_gid " +
            " left join hrm_mst_temployee p on a.employee_gid = p.employee_gid " +
            " left join adm_mst_tuser q on p.user_gid = q.user_gid " +
            " where a.teammember_gid = '" + employee_gid + "' and a.taskinitiate_flag = 'Y' and a.complete_flag = 'Y'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettask_list = new List<tasksummarylist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettask_list.Add(new tasksummarylist
                        {
                            task_name = (dr_datarow["task_name"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            task_remarks = (dr_datarow["task_remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            taskinitiate_gid = (dr_datarow["taskinitiate_gid"].ToString()),
                            completed_date = (dr_datarow["completed_date"].ToString()),
                            complete_flag = (dr_datarow["complete_flag"].ToString()),
                            completed_by = (dr_datarow["completed_by"].ToString()),
                            task_completeremarks = (dr_datarow["task_completeremarks"].ToString()),
                            task_status = (dr_datarow["task_status"].ToString()),
                        });
                    }
                    values.tasksummarylist = gettask_list;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetMyTaskCount(string employee_gid, countlist values)
        {
            try
            {
                
                msSQL = " select count(*) as pending_count from sys_mst_ttaskinitiate a " +
                        " where a.taskinitiate_flag = 'Y' and complete_flag = 'N' and a.teammember_gid = '" + employee_gid + "'";
                values.pending_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(*) as pending_count from sys_mst_ttaskinitiate a " +
                        " where a.taskinitiate_flag = 'Y' and complete_flag = 'Y' and a.teammember_gid = '" + employee_gid + "'";
                values.completed_count = objdbconn.GetExecuteScalar(msSQL);
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetMyTaskPendingSummary(string employee_gid, MdlTaskList values)
        {
            try
            {
                
                msSQL = " select  a.task_name,a.taskinitiate_gid,a.task_remarks,concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,  a.task_status," +
                    " concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as employee_name, p.employee_gid" +
                    " from sys_mst_ttaskinitiate a " +
                    " left join hrm_mst_temployee e on a.created_by = e.employee_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid  " +
                    " left join hrm_mst_temployee p on a.employee_gid = p.employee_gid " +
                    " left join adm_mst_tuser q on p.user_gid = q.user_gid " +
                    " where a.teammember_gid = '" + employee_gid + "' and a.taskinitiate_flag = 'Y' and complete_flag = 'N' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettask_list = new List<tasksummarylist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettask_list.Add(new tasksummarylist
                        {
                            task_name = (dr_datarow["task_name"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            task_remarks = (dr_datarow["task_remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            taskinitiate_gid = (dr_datarow["taskinitiate_gid"].ToString()),
                            task_status = (dr_datarow["task_status"].ToString()),
                        });
                    }
                    values.tasksummarylist = gettask_list;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetTaskManagementPendingSummary(string employee_gid, MdlTaskList values)
        {
            try
            {
                
                msSQL = " select  a.task_name,a.taskinitiate_gid,a.task_remarks,a.task_status, " +
                    " concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                    "  date_format(a.taskassigned_date, '%d-%m-%Y %h:%i %p') as taskassigned_date, " +
                    " taskassigned_by,teammanager_name,teammember_name,b.assigned_remarks, " +
                    " concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as employee_name, p.employee_gid" +
                    " from sys_mst_ttaskinitiate a " +
                    " left join hrm_mst_temployee e on a.created_by = e.employee_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid  " +
                    " left join hrm_mst_temployee p on a.employee_gid = p.employee_gid " +
                    " left join adm_mst_tuser q on p.user_gid = q.user_gid " +
                    " left join sys_trn_tteam2member b on a.taskinitiate_gid = b.taskinitiate_gid and assigned_flag = 'Y' " +
                    " where a.teammanager_gid = '" + employee_gid + "' and " +
                    " a.task_status in ('Assigned','Inprogress')  and a.taskinitiate_flag = 'Y' and complete_flag = 'N'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettask_list = new List<tasksummarylist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettask_list.Add(new tasksummarylist
                        {
                            task_name = (dr_datarow["task_name"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            task_remarks = (dr_datarow["task_remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            taskinitiate_gid = (dr_datarow["taskinitiate_gid"].ToString()),
                            taskassigned_date = (dr_datarow["taskassigned_date"].ToString()),
                            taskassigned_by = (dr_datarow["teammanager_name"].ToString()),
                            teammember_name = (dr_datarow["teammember_name"].ToString()),
                            assigned_remarks = (dr_datarow["assigned_remarks"].ToString()),
                            task_status = (dr_datarow["task_status"].ToString())
                        });
                    }
                    values.tasksummarylist = gettask_list;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetTaskManagementNewSummary(string employee_gid, MdlTaskList values)
        {
            try
            {
                
                msSQL = " select  a.task_name,a.taskinitiate_gid,a.task_remarks, " +
                    " concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,a.team_gid,a.team_name, " +
                    " concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as employee_name, p.employee_gid" +
                    " from sys_mst_ttaskinitiate a " +
                    " left join hrm_mst_temployee e on a.created_by = e.employee_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid  " +
                    " left join hrm_mst_temployee p on a.employee_gid = p.employee_gid " +
                    " left join adm_mst_tuser q on p.user_gid = q.user_gid " +
                    " where a.teammanager_gid = '" + employee_gid + "' and a.taskinitiate_flag = 'N' and complete_flag = 'N'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettask_list = new List<tasksummarylist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettask_list.Add(new tasksummarylist
                        {

                            task_name = (dr_datarow["task_name"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            task_remarks = (dr_datarow["task_remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            taskinitiate_gid = (dr_datarow["taskinitiate_gid"].ToString()),
                            team_name = (dr_datarow["team_name"].ToString()),
                            team_gid = (dr_datarow["team_gid"].ToString()),
                        });
                    }
                    values.tasksummarylist = gettask_list;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetTaskManagementCompletedSummary(string employee_gid, MdlTaskList values)
        {
            try
            {
                
                msSQL = " select  a.task_name,a.taskinitiate_gid,a.task_remarks,task_completeremarks,completed_date, " +
                    " concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.teammember_name, " +
                    " concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as employee_name, p.employee_gid" +
                    " from sys_mst_ttaskinitiate a " +
                    " left join hrm_mst_temployee e on a.created_by = e.employee_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid  " +
                    " left join hrm_mst_temployee p on a.employee_gid = p.employee_gid " +
                    " left join adm_mst_tuser q on p.user_gid = q.user_gid " +
                    " where a.teammanager_gid = '" + employee_gid + "'  and a.taskinitiate_flag = 'Y' and complete_flag = 'Y' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettask_list = new List<tasksummarylist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettask_list.Add(new tasksummarylist
                        {
                            task_name = (dr_datarow["task_name"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            task_remarks = (dr_datarow["task_remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            taskinitiate_gid = (dr_datarow["taskinitiate_gid"].ToString()),
                            task_completeremarks = (dr_datarow["task_completeremarks"].ToString()),
                            completed_date = (dr_datarow["completed_date"].ToString()),
                            assigned_to = (dr_datarow["teammember_name"].ToString()),
                        });
                    }
                    values.tasksummarylist = gettask_list;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

        public void GetTaskOnboardView(string employee_gid, MdlTaskViewInfoList values)
        {
            try
            {
                
                msSQL = " select  a.task_name,a.taskinitiate_gid,a.task_remarks, " +
                    " concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                    " team_name,date_format(a.taskassigned_date, '%d-%m-%Y') as taskassigned_date,  " +
                    " teammanager_name,teammember_name,task_status, date_format(a.completed_date, '%d-%m-%Y') as completed_date, " +
                    " task_completeremarks " +
                    " from sys_mst_ttaskinitiate a " +
                    " left join hrm_mst_temployee e on a.created_by = e.employee_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid  " +
                    " left join hrm_mst_temployee p on a.employee_gid = p.employee_gid " +
                    " left join adm_mst_tuser q on p.user_gid = q.user_gid " +
                    " where a.employee_gid = '" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettask_list = new List<MdlTaskViewInfo>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettask_list.Add(new MdlTaskViewInfo
                        {
                            task_name = (dr_datarow["task_name"].ToString()),
                            task_remarks = (dr_datarow["task_remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            assigned_date = (dr_datarow["taskassigned_date"].ToString()),
                            assigned_by = (dr_datarow["teammanager_name"].ToString()),
                            assigned_team = (dr_datarow["team_name"].ToString()),
                            assigned_to = (dr_datarow["teammember_name"].ToString()),
                            task_status = (dr_datarow["task_status"].ToString()),
                            completed_date = (dr_datarow["completed_date"].ToString()),
                            completed_remarks = (dr_datarow["task_completeremarks"].ToString()),
                        });
                    }
                    values.MdlTaskViewInfo = gettask_list;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public bool DaGetHRDoclist(string employee_gid, hrdoc_list objemployeedoc_list)
        {
            try
            {
                
                msSQL = " select hrdoc_id,hrdocument_gid,hrdocument_name,hrdoc_name, " +
                        " hrdoc_path,documentsentforsign_flag,esignexpiry_flag,documentsigned_flag, " +
                        " concat(c.user_firstname,' ', c.user_lastname,'/' ,c.user_code) as created_by," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, migration_flag " +
                        " from sys_mst_temployeehrdocument a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.employee_gid = '" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_hrdoc_list = new List<hrdoc>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_hrdoc_list.Add(new hrdoc_list
                        {
                            hrdoc_id = dr_datarow["hrdoc_id"].ToString(),
                            hrdocument_gid = dr_datarow["hrdocument_gid"].ToString(),
                            hrdocument_name = dr_datarow["hrdocument_name"].ToString(),
                            hrdoc_name = dr_datarow["hrdoc_name"].ToString(),
                            hrdoc_path = objcmnstorage.EncryptData((dr_datarow["hrdoc_path"].ToString())),
                            documentsentforsign_flag = dr_datarow["documentsentforsign_flag"].ToString(),
                            esignexpiry_flag = dr_datarow["esignexpiry_flag"].ToString(),
                            documentsigned_flag = dr_datarow["documentsigned_flag"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            migration_flag = dr_datarow["migration_flag"].ToString(),
                        });
                    }
                    objemployeedoc_list.hrdoc = get_hrdoc_list;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                objemployeedoc_list.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public void DaGetTaskManagementCount(string employee_gid, countlist values)
        {
            try
            {
                
                msSQL = " select count(*) as new_count from sys_mst_ttaskinitiate a " +
                        " where taskinitiate_flag = 'N' and complete_flag = 'N' and a.teammanager_gid = '" + employee_gid + "'";
                values.new_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(*) as pending_count from sys_mst_ttaskinitiate a " +
                        " where a.task_status in ('Assigned','Inprogress') and taskinitiate_flag = 'Y' and complete_flag = 'N' " +
                        " and a.teammanager_gid = '" + employee_gid + "'";
                values.pending_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(*) as completed_count from sys_mst_ttaskinitiate a " +
                        " where a.task_status='Completed' and taskinitiate_flag = 'Y' and complete_flag = 'Y' " +
                        " and a.teammanager_gid = '" + employee_gid + "'";
                values.completed_count = objdbconn.GetExecuteScalar(msSQL);
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public bool DaPostAssignTask(string user_gid, MdlTaskAssign values)
        {
            try
            {
                
                msSQL = " update sys_mst_ttaskinitiate set " +
                    " taskinitiate_flag = 'Y', " +
                    " task_status = 'Assigned', " +
                    " teammember_gid = '" + values.assigned_to + "', " +
                    " teammember_name = '" + values.assigned_toname + "'," +
                    " taskassigned_by = '" + user_gid + "'," +
                    " taskassigned_date ='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                    " where taskinitiate_gid = '" + values.taskinitiate_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update sys_trn_tteam2member set " +
                       " status = 'N', " +
                       " assigned_flag = 'Y', " +
                       " assigned_remarks = '" + values.assigned_remarks + "'," +
                       " assigned_by = '" + user_gid + "'," +
                       " assigned_date ='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                       " where taskinitiate_gid = '" + values.taskinitiate_gid + "' and member_gid='" + values.assigned_to + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    // Success
                    values.status = true;
                    values.message = "Task assigned successfully";
                    return true;
                }
                else
                {
                    // Failure
                    values.status = false;
                    values.message = "Error While assigning Task";
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

        public bool DaPostMyTaskStatusUpdate(string employee_gid, string user_gid, MdlTaskStatusUpdate values)
        {
            try
            {
                
                if (values.task_status == "Completed")
                {
                    msSQL = " update sys_mst_ttaskinitiate set " +
                        " complete_flag = 'Y', " +
                        " task_status = 'Completed', " +
                        " task_completeremarks = '" + values.task_remarks + "', " +
                        " completed_by = '" + user_gid + "'," +
                        " completed_date ='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                        " where taskinitiate_gid = '" + values.taskinitiate_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update sys_trn_tteam2member set " +
                           " status = 'Y' " +
                           " where taskinitiate_gid = '" + values.taskinitiate_gid + "' " +
                           " and member_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " update sys_mst_ttaskinitiate set " +
                            " task_status = 'Inprogress' " +
                            " where taskinitiate_gid = '" + values.taskinitiate_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    // Success
                    if (values.task_status == "Completed")
                    {
                        values.status = true;
                        values.message = "Task Completed Successfully";
                        return true;
                    }
                    else
                    {
                        values.status = true;
                        values.message = "Task Status Updated Successfully";
                        return true;
                    }
                }
                else
                {
                    // Failure
                    values.status = false;
                    values.message = "Error in Updating Status";
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
        public bool DaGetTeamMemberlist(string team_gid, member_list objemployee_list)
        {
            try
            {
                
                msSQL = " select member_name, member_gid from sys_mst_tteam2member a  " +
                        " where a.team_gid = '" + team_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_member_list = new List<memberlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_member_list.Add(new member_list
                        {
                            member_name = dr_datarow["member_name"].ToString(),
                            member_gid = dr_datarow["member_gid"].ToString(),
                        });
                    }
                    objemployee_list.memberlist = get_member_list;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                objemployee_list.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
    }
} 
    
