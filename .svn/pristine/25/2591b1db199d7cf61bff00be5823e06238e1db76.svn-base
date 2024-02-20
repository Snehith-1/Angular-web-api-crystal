using System;
using System.Collections.Generic;
using System.Web;
using ems.payroll.Models;
using ems.utilities.Functions;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using File = System.IO.File;
using System.Data;
using System.Globalization;
using System.Data.Odbc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ems.payroll.DataAccess
{
    public class DaPayMstAssessment
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        string msGetGid;
        int mnResult;
        public void Daassessmentsummary(MdlPayMstAssessment values)
        {
            try
            {
                msSQL = " select assessment_gid, cast(concat(assessmentyear_startdate,' ','to',' ', assessmentyear_enddate) as char) as duration, " +
                        " cast(concat(financialyear_startdate,' ','to',' ', financialyear_enddate) as char) as fin_duration " +
                        " from pay_mst_tassessmentyear " +
                        " order by assessment_gid asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<assessmentsummary_list>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new assessmentsummary_list
                        {
                            assessment_gid = dt["assessment_gid"].ToString(),
                            duration = dt["duration"].ToString(),
                            fin_duration = dt["fin_duration"].ToString(),
                        });
                        values.assessmentsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading assessment details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/payroll/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetassessmentyear(MdlPayMstAssessment values, string assessment_gid)
        {
            try
            {
                msSQL = " select assessment_gid, cast(concat(assessmentyear_startdate,' ','to',' ', assessmentyear_enddate) as char) as duration, " +
                        " cast(concat(financialyear_startdate,' ','to',' ', financialyear_enddate) as char) as fin_duration " +
                        " from pay_mst_tassessmentyear " +
                        " where assessment_gid = '" + assessment_gid + "' " +
                        " order by assessment_gid asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<assessmentsummary_list>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new assessmentsummary_list
                        {
                            assessment_gid = dt["assessment_gid"].ToString(),
                            duration = dt["duration"].ToString(),
                            fin_duration = dt["fin_duration"].ToString(),
                        });
                        values.assessmentsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading assessment details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/payroll/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void Daassignempsummary(string assessment_gid, MdlPayMstAssessment values)
        {
            try
            {
                msSQL = " Select distinct c.employee_gid, a.user_code, concat(a.user_firstname,' ',a.user_lastname) as employee_name, " +
                        " d.designation_name, e.branch_name, g.department_name " +
                        " from hrm_mst_temployee c " +
                        " inner join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                        " inner join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                        " inner join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                        " inner join adm_mst_tuser a on c.user_gid=a.user_gid " +
                        " where a.user_status = 'Y' " +
                        " and c.employee_gid not in " +
                        " (select x.employee_gid from pay_trn_tassessment2employee x where x.assessment_gid='" + assessment_gid + "') " +
                        " order by e.branch_name,a.user_code asc ";               

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<assignempsummary_list>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new assignempsummary_list
                        {
                            employee_gid = dt["employee_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            user_code = dt["user_code"].ToString(),
                            employee_name = dt["employee_name"].ToString(),
                            department_name = dt["department_name"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                        });
                        values.assignempsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading assigning employee details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/payroll/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaPostassignemployee(postassignemployeelist values, string employee_gid)
        {
            try
            {
                foreach (var data in values.assignempsummary_list)
                {
                    string msGetGid = objcmnfunctions.GetMasterGID("PAAE");

                    msSQL = " select DATE_FORMAT(assessmentyear_startdate, '%Y-%m-%d') as assessmentyear_startdate, DATE_FORMAT(assessmentyear_enddate, '%Y-%m-%d') as assessmentyear_enddate from pay_mst_tassessmentyear " +
                            " where assessment_gid='" + values.assessment_gid + "'";
                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                    if (objMySqlDataReader.HasRows == true)
                    {
                        objMySqlDataReader.Read();
                        string assessmentyear_startdate = objMySqlDataReader["assessmentyear_startdate"].ToString();
                        string assessmentyear_enddate = objMySqlDataReader["assessmentyear_enddate"].ToString();
                        objMySqlDataReader.Close();

                    msSQL = " select b.employee_gid, sum(a.earned_basic_salary) as total_basic, " +
                            " sum(a.earned_gross_salary) as total_gross, " +
                            " (select sum(x.earned_salarycomponent_amount) from pay_trn_tsalarydtl x " +
                            " inner join pay_trn_tsalary y on x.salary_gid = y.salary_gid " +
                            " inner join pay_trn_tpayment z on y.salary_gid = z.salary_gid " +
                            " where y.employee_gid='" + data.employee_gid + "' and x.salarygradetype='Addition' " +
                            " and (cast(concat(z.payment_year,'-',MONTH(STR_TO_DATE(substring(z.payment_month,1,3),'%b')),'-01') as date)  between '" + assessmentyear_startdate + "'  and '" + assessmentyear_enddate + "')) as total_addition, " +
                            " (select sum(x.earned_salarycomponent_amount) from pay_trn_tsalarydtl x " +
                            " inner join pay_trn_tsalary y on x.salary_gid=y.salary_gid " +
                            " inner join pay_trn_tpayment z on y.salary_gid=z.salary_gid " +
                            " where y.employee_gid='" + data.employee_gid + "' and  x.salarygradetype='Deduction' " +
                            " and (cast(concat(z.payment_year,'-',MONTH(STR_TO_DATE(substring(z.payment_month,1,3),'%b')),'-01') as date)  between '" + assessmentyear_startdate + "'  and '" + assessmentyear_enddate + "')) as total_deduction, " +
                            " sum(a.earned_net_salary) as total_net " + " from pay_trn_tsalary a " +
                            " inner join pay_trn_tpayment b on a.salary_gid=b.salary_gid " +
                            " where (cast(concat(payment_year,'-',MONTH(STR_TO_DATE(substring(b.payment_month,1,3),'%b')),'-01') as date)  between '" + assessmentyear_startdate + "'  and '" + assessmentyear_enddate + "') " + " and b.employee_gid='" + data.employee_gid + "' " +
                            " group by b.employee_gid ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    }

                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " insert into pay_trn_tassessment2employee(" +
                            " assessment2employee_gid, " +
                            " assessment_gid, " +
                            " employee_gid, " +
                            " basic_salary, " +
                            " total_addition, " +
                            " gross_salary, " +
                            " total_deduction," +
                            " net_salary) " +
                            " values( " +
                            "'" + msGetGid + "', " +
                            "'" + values.assessment_gid + "', " +
                            "'" + data.employee_gid + "', " +
                            "'" + dt["total_basic"].ToString() + "'," +
                            "'" + dt["total_addition"].ToString() + "'," +
                            "'" + dt["total_gross"].ToString() + "'," +
                            "'" + dt["total_deduction"].ToString() + "'," +
                            "'" + dt["total_net"].ToString() + "') ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Employee selected Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While selecting employee";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while selecting employee details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/payroll/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void Dagenerateformsummary(string assessment_gid, MdlPayMstAssessment values)
        {
            try
            {
                msSQL = " Select distinct c.employee_gid, z.assessment_gid, a.user_code, concat(a.user_firstname,' ',a.user_lastname) as employee_name,z.assessment_gid, " +
                        " d.designation_name, e.branch_name, g.department_name " +
                        " from pay_trn_tassessment2employee z " +
                        " inner join hrm_mst_temployee c on z.employee_gid = c.employee_gid " +
                        " inner join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                        " inner join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                        " inner join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                        " inner join adm_mst_tuser a on c.user_gid=a.user_gid " +
                        " where z.assessment_gid='" + assessment_gid + "' " +
                        " order by e.branch_name,a.user_code asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<generateformsummary_list>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new generateformsummary_list
                        {
                            branch_name = dt["branch_name"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            assessment_gid = dt["assessment_gid"].ToString(),
                            user_code = dt["user_code"].ToString(),
                            employee_name = dt["employee_name"].ToString(),
                            department_name = dt["department_name"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                        });
                        values.generateformsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading assigning employee details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/payroll/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public MdlPersonalData DaGetPersonaldata(string employee_gid)
        {
            try
            {
                MdlPersonalData objpersonaldatalist = new MdlPersonalData();

                msSQL = " select permanentaddress_gid from hrm_trn_temployeedtl " +
                        " where a.employee_gid='" + employee_gid + "' ";
                
                string lspermanentaddress_gid = objdbconn.GetExecuteScalar(msSQL);                
                    
                msSQL = " select b.user_firstname, b.user_lastname, Date_Format(a.employee_dob,'%Y-%m-%d') as employee_dob, a.employee_emailid, a.employee_gender, " +
                        " a.pan_no, a.uan_no, a.bloodgroup, a.employee_mobileno, x.address1, x.address2, x.city, x.state, x.postal_code, x.country_gid " +
                        " from hrm_mst_temployee a inner join adm_mst_tuser b on a.user_gid = b.user_gid " +
                        " left join adm_mst_taddress x on a.employee_gid = x.parent_gid " +
                        " left join hrm_trn_temployeedtl y on x.address_gid = y.permanentaddress_gid and y.permanentaddress_gid='" + lspermanentaddress_gid + "' " +
                        " left join adm_mst_tcountry z on x.country_gid = z.country_gid " +
                        " where a.employee_gid='" + employee_gid + "' ";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    objMySqlDataReader.Read();

                    objpersonaldatalist.user_firstname = objMySqlDataReader["user_firstname"].ToString();
                    objpersonaldatalist.user_lastname = objMySqlDataReader["user_lastname"].ToString();
                    objpersonaldatalist.employee_dob = objMySqlDataReader["employee_dob"].ToString();
                    objpersonaldatalist.employee_emailid = objMySqlDataReader["employee_emailid"].ToString();
                    objpersonaldatalist.employee_gender = objMySqlDataReader["employee_gender"].ToString();
                    objpersonaldatalist.pan_no = objMySqlDataReader["pan_no"].ToString();
                    objpersonaldatalist.uan_no = objMySqlDataReader["uan_no"].ToString();
                    objpersonaldatalist.bloodgroup = objMySqlDataReader["bloodgroup"].ToString();
                    objpersonaldatalist.employee_mobileno = objMySqlDataReader["employee_mobileno"].ToString();
                    objpersonaldatalist.address1 = objMySqlDataReader["address1"].ToString();
                    objpersonaldatalist.address2 = objMySqlDataReader["address2"].ToString();
                    objpersonaldatalist.city = objMySqlDataReader["city"].ToString();
                    objpersonaldatalist.state = objMySqlDataReader["state"].ToString();
                    objpersonaldatalist.postal_code = objMySqlDataReader["postal_code"].ToString();
                    objpersonaldatalist.country_gid = objMySqlDataReader["country_gid"].ToString();

                    objMySqlDataReader.Close();
                }
                return objpersonaldatalist;
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Payroll/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                ex.ToString();
                return null;
            }
        }
        public void DaUpdatePersonalInfo(string employee_gid, updatepersonalinfolist values)
        {
            try
            {
                msSQL = " select user_gid from hrm_mst_temployee where employee_gid='" + employee_gid + "'";
                string lsUserGid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select permanentaddress_gid from hrm_trn_temployeedtl " +
                        " where a.employee_gid='" + employee_gid + "' ";

                string lspermanentaddress_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update adm_mst_tuser set " +
                        " user_firstname = '" + values.first_name + "'," +
                        " user_lastname = '" + values.last_name + "'," +
                        " updated_by = '" + employee_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where user_gid = '" + lsUserGid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update hrm_mst_temployee set " +
                        " employee_dob='" + values.dob.ToString("yyyy-MM-dd ") + "'," +
                        " employee_emailid = '" + values.email_id + "'," +
                        " employee_gender = '" + values.active_flag + "'," +
                        " pan_no = '" + values.pan_number + "'," +
                        " uan_no = '" + values.uan_number + "'," +
                        " bloodgroup = '" + values.blood_group + "'," +
                        " employee_mobileno = '" + values.phone + "'," +
                        " updated_by = '" + employee_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where employee_gid = '" + employee_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update adm_mst_taddress set " +
                        " country_gid='" + values.country + "'," +
                        " address1 = '" + values.address_line1 + "'," +
                        " address2 = '" + values.address_line2 + "'," +
                        " city = '" + values.city + "'," +
                        " postal_code = '" + values.postal_code + "'," +
                        " state = '" + values.state + "'," +
                        " updated_by = '" + employee_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where address_gid = '" + lspermanentaddress_gid + "' and " +
                        " parent_gid = '" + employee_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Personal Information Updated Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error occured while updating personal information";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetfinyeardropdown(MdlPayMstAssessment values)
        {
            try
            {
                msSQL = " SELECT finyear_gid, CONCAT(YEAR(fyear_start), ' - ', YEAR(IFNULL(fyear_end, NOW()))) AS finyear_range FROM adm_mst_tyearendactivities ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getfinyeardropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getfinyeardropdown
                        {
                            finyear_gid = dt["finyear_gid"].ToString(),
                            finyear_range = dt["finyear_range"].ToString(),

                        });
                        values.Getfinyeardropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Finyear!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaPostIncometax(incometax_lists values, string user_gid)
        {
            try
            {
                msGetGid = objcmnfunctions.GetMasterGID("TDMP");
                {
                    msSQL = " insert into pay_trn_ttaxdocument " +
                            " (taxdocument_gid, " +
                            " fin_year ," +
                            " documenttype_gid, " +
                            " document_title," +
                            " remarks, " +
                            " created_by, " +
                            " created_date) " +
                            " values( " +
                            "'" + msGetGid + "'," +
                            "'" + values.finyear_range + "'," +
                            "'" + values.documenttype_gid + "'," +
                            "'" + values.document_title + "'," +
                            "'" + values.remarks + "'," +
                            "'" + values.user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                }
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Income Tax Document Updated Successfully";
                }

                else
                {
                    values.status = false;
                    values.message = "Error Occured while Updating !!";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Finyear!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetincometaxsummary(MdlPayMstAssessment values)
        {
            try
            {
                msSQL = " select taxdocument_gid,date_format(created_date,'%Y-%m-%d')as created_date,fin_year,documenttype_gid,document_title from pay_trn_ttaxdocument ORDER BY taxdocument_gid DESC";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<incometaxsummary_lists>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new incometaxsummary_lists
                        {
                            taxdocument_gid = dt["taxdocument_gid"].ToString(),
                            fin_year = dt["fin_year"].ToString(),
                            created_date = Convert.ToDateTime(dt["created_date"].ToString()),
                            documenttype_gid = dt["documenttype_gid"].ToString(),
                            document_title = dt["document_title"].ToString(),
                        });
                        values.incometaxsummary_lists = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Finyear!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaPostQuartersInfo(postquartersinfolist values)
        {
            try
            {
                msSQL = " delete from pay_trn_ttdssummary where employee_gid = '" + values.employee_gid + "' and assessment_gid = '" + values.assessment_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                string msGetGid = objcmnfunctions.GetMasterGID("PAQA");

                msSQL = " insert into pay_trn_ttdssummary  ( " +
                        " tdssummary_gid, " +
                        " assessment_gid, " +
                        " employee_gid, " +
                        " tdsquarter1_receiptno, " +
                        " tdsquarter1_paidcredited, " +
                        " tdsquarter1_amount_deducted, " +
                        " tdsquarter1_amount_deposited, " +
                        " tdsquarter2_receiptno, " +
                        " tdsquarter2_paidcredited, " +
                        " tdsquarter2_amount_deducted, " +
                        " tdsquarter2_amount_deposited, " +
                        " tdsquarter3_receiptno, " +
                        " tdsquarter3_paidcredited, " +
                        " tdsquarter3_amount_deducted, " +
                        " tdsquarter3_amount_deposited, " +
                        " tdsquarter4_receiptno, " +
                        " tdsquarter4_paidcredited, " +
                        " tdsquarter4_amount_deducted, " +
                        " tdsquarter4_amount_deposited, " +
                        " totalamount_paidcredited, " +
                        " tdsquarter_totalamount_deducted, " +
                        " tdsquarter_totalamount_deposited, " +
                        " created_by, " +
                        " created_date " +
                        " ) values ( " +
                        "'" + msGetGid + "'," +
                        "'" + values.assessment_gid + "'," +
                        "'" + values.employee_gid + "'," +
                        "'" + values.q1_rpt_original_statement + "'," +
                        "'" + values.q1_amt_paid_credited + "'," +
                        "'" + values.q1_amt_tax_deducted + "'," +
                        "'" + values.q1_amt_tax_deposited + "'," +
                        "'" + values.q2_rpt_original_statement + "'," +
                        "'" + values.q2_amt_paid_credited + "'," +
                        "'" + values.q2_amt_tax_deducted + "'," +
                        "'" + values.q2_amt_tax_deposited + "'," +
                        "'" + values.q3_rpt_original_statement + "'," +
                        "'" + values.q3_amt_paid_credited + "'," +
                        "'" + values.q3_amt_tax_deducted + "'," +
                        "'" + values.q3_amt_tax_deposited + "'," +
                        "'" + values.q4_rpt_original_statement + "'," +
                        "'" + values.q4_amt_paid_credited + "'," +
                        "'" + values.q4_amt_tax_deducted + "'," +
                        "'" + values.q4_amt_tax_deposited + "'," +
                        "'" + values.total_amt_paid_credited + "'," +
                        "'" + values.total_amt_tax_deducted + "'," +
                        "'" + values.total_amt_tax_deposited + "', " +
                        "'" + values.employee_gid + "', " +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Quarter Details are Inserted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred while inserting quarter details";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public MdlQuartersData DaGetQuatersdata(string employee_gid)
        {
            try
            {
                MdlQuartersData objquartersdatalist = new MdlQuartersData();

                msSQL = " select tdsquarter1_receiptno, format(tdsquarter1_paidcredited, 2) as tdsquarter1_paidcredited, format(tdsquarter1_amount_deposited, 2) as tdsquarter1_amount_deposited, format(tdsquarter1_amount_deducted, 2) as tdsquarter1_amount_deducted, " +
                        " tdsquarter2_receiptno, format(tdsquarter2_paidcredited, 2) as tdsquarter2_paidcredited, format(tdsquarter2_amount_deposited, 2) as tdsquarter2_amount_deposited, format(tdsquarter2_amount_deducted, 2) as tdsquarter2_amount_deducted, " +
                        " tdsquarter3_receiptno, format(tdsquarter3_paidcredited, 2) as tdsquarter3_paidcredited, format(tdsquarter3_amount_deposited, 2) as tdsquarter3_amount_deposited, format(tdsquarter3_amount_deducted, 2) as tdsquarter3_amount_deducted, " +
                        " tdsquarter4_receiptno, format(tdsquarter4_paidcredited, 2) as tdsquarter4_paidcredited, format(tdsquarter4_amount_deposited, 2) as tdsquarter4_amount_deposited, format(tdsquarter4_amount_deducted, 2) as tdsquarter4_amount_deducted, " +
                        " format(totalamount_paidcredited, 2) as totalamount_paidcredited, format(tdsquarter_totalamount_deposited, 2) as tdsquarter_totalamount_deposited, format(tdsquarter_totalamount_deducted, 2) as tdsquarter_totalamount_deducted " +
                        " from pay_trn_ttdssummary " +
                        " where employee_gid='" + employee_gid + "' ";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    objMySqlDataReader.Read();
                    objquartersdatalist.tdsquarter1_receiptno = objMySqlDataReader["tdsquarter1_receiptno"].ToString();
                    objquartersdatalist.tdsquarter1_paidcredited = objMySqlDataReader["tdsquarter1_paidcredited"].ToString();
                    objquartersdatalist.tdsquarter1_amount_deducted = objMySqlDataReader["tdsquarter1_amount_deducted"].ToString();
                    objquartersdatalist.tdsquarter1_amount_deposited = objMySqlDataReader["tdsquarter1_amount_deposited"].ToString();                    
                    objquartersdatalist.tdsquarter2_receiptno = objMySqlDataReader["tdsquarter2_receiptno"].ToString();
                    objquartersdatalist.tdsquarter2_paidcredited = objMySqlDataReader["tdsquarter2_paidcredited"].ToString();
                    objquartersdatalist.tdsquarter2_amount_deducted = objMySqlDataReader["tdsquarter2_amount_deducted"].ToString();
                    objquartersdatalist.tdsquarter2_amount_deposited = objMySqlDataReader["tdsquarter2_amount_deposited"].ToString();                    
                    objquartersdatalist.tdsquarter3_receiptno = objMySqlDataReader["tdsquarter3_receiptno"].ToString();
                    objquartersdatalist.tdsquarter3_paidcredited = objMySqlDataReader["tdsquarter3_paidcredited"].ToString();
                    objquartersdatalist.tdsquarter3_amount_deducted = objMySqlDataReader["tdsquarter3_amount_deducted"].ToString();
                    objquartersdatalist.tdsquarter3_amount_deposited = objMySqlDataReader["tdsquarter3_amount_deposited"].ToString();                    
                    objquartersdatalist.tdsquarter4_receiptno = objMySqlDataReader["tdsquarter4_receiptno"].ToString();
                    objquartersdatalist.tdsquarter4_paidcredited = objMySqlDataReader["tdsquarter4_paidcredited"].ToString();
                    objquartersdatalist.tdsquarter4_amount_deducted = objMySqlDataReader["tdsquarter4_amount_deducted"].ToString();
                    objquartersdatalist.tdsquarter4_amount_deposited = objMySqlDataReader["tdsquarter4_amount_deposited"].ToString();                    
                    objquartersdatalist.totalamount_paidcredited = objMySqlDataReader["totalamount_paidcredited"].ToString();
                    objquartersdatalist.tdsquarter_totalamount_deducted = objMySqlDataReader["tdsquarter_totalamount_deducted"].ToString();
                    objquartersdatalist.tdsquarter_totalamount_deposited = objMySqlDataReader["tdsquarter_totalamount_deposited"].ToString();

                    objMySqlDataReader.Close();
                }
                return objquartersdatalist;
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Payroll/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                ex.ToString();
                return null;
            }
        }

        public MdlPayIncomedata DaGetIncomedata(string employee_gid, string assessment_gid)
        {
            try
            {

                MdlPayIncomedata objincomedatalist = new MdlPayIncomedata();

                msSQL = " select a.grosssalary_amount,a.perquisites_amount,a.profitinlieu_amount,a.grosstotal_qualifiying_amount,b.transport_totamount,b.transport_qualifiying_amount, " +
                        " b.balance_qualifiying_amount,b.entertainment_amount,b.taxonemployment_amount,b.aggreegate_qualifiying_amount,b.incomechargableunder_headsal_deductible_amount, " +
                        " c.otherincomeemployee_totamount1,c.otherincomeemployee_totamount2,c.otherincomeemployee_totamount3,c.otherincome1_name,c.otherincome2_name,c.otherincome3_name, " +
                        " c.otherincomeemployee_qualifiying_amount3,c.overallgross_deductible_amount,b.lessallowence_name1,b.lessallowence_name2, " +
                        " b.lessallowence_name3,b.lessallowence_amount2,b.lessallowence_amount3 from pay_trn_ttdsgrosssalary a " +
                        " inner join pay_trn_ttdsallowencetotheextent b on a.assessment_gid=b.assessment_gid " +
                        " inner join pay_trn_ttdsotherincomeemployee c on b.assessment_gid=c.assessment_gid where a.assessment_gid='" + assessment_gid + "' " +
                        " and a.employee_gid='" + employee_gid + "' ";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    objMySqlDataReader.Read();

                    objincomedatalist.grosssalary_amount = objMySqlDataReader["grosssalary_amount"].ToString();
                    objincomedatalist.perquisites_amount = objMySqlDataReader["perquisites_amount"].ToString();
                    objincomedatalist.profitof_salary = objMySqlDataReader["profitinlieu_amount"].ToString();
                    objincomedatalist.totalamount = objMySqlDataReader["grosstotal_qualifiying_amount"].ToString();
                    objincomedatalist.component1 = objMySqlDataReader["lessallowence_name1"].ToString();
                    objincomedatalist.pfamount1 = objMySqlDataReader["lessallowence_amount2"].ToString();
                    objincomedatalist.component2 = objMySqlDataReader["lessallowence_name2"].ToString();
                    objincomedatalist.pfamount2 = objMySqlDataReader["lessallowence_amount3"].ToString();
                    objincomedatalist.component3 = objMySqlDataReader["lessallowence_name3"].ToString();
                    objincomedatalist.balanceamount = objMySqlDataReader["balance_qualifiying_amount"].ToString();
                    objincomedatalist.taxon_emp = objMySqlDataReader["taxonemployment_amount"].ToString();
                    objincomedatalist.aggreagateofab = objMySqlDataReader["aggreegate_qualifiying_amount"].ToString();
                    objincomedatalist.incomecharge_headsalaries = objMySqlDataReader["incomechargableunder_headsal_deductible_amount"].ToString();
                    objincomedatalist.employee_income1 = objMySqlDataReader["otherincome1_name"].ToString();
                    objincomedatalist.employeeincome_rs1 = objMySqlDataReader["otherincomeemployee_totamount1"].ToString();
                    objincomedatalist.employee_income2 = objMySqlDataReader["otherincome2_name"].ToString();
                    objincomedatalist.employeeincome_rs2 = objMySqlDataReader["otherincomeemployee_totamount2"].ToString();
                    objincomedatalist.employee_income3 = objMySqlDataReader["otherincome3_name"].ToString();
                    objincomedatalist.employeeincome_rs3 = objMySqlDataReader["otherincomeemployee_totamount3"].ToString();
                    objincomedatalist.employeeincome_total = objMySqlDataReader["otherincomeemployee_qualifiying_amount3"].ToString();
                    objincomedatalist.grosstotal_income = objMySqlDataReader["overallgross_deductible_amount"].ToString();
                    //objincomedatalist.lessallowancetotal = objMySqlDataReader["lessallowancetotal"].ToString();

                    objMySqlDataReader.Close();
                }
                return objincomedatalist;


            }

            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return null;
            }
        }

        public void DaPostIncome(MdlPayIncomedata values, string user_gid)
        {
            try
            {
                string msGetGid = objcmnfunctions.GetMasterGID("PAGS");

                msSQL = "insert into pay_trn_ttdsgrosssalary ( " +
                    " tdsgrosssalary_gid, " +
                    " assessment_gid, " +
                    " employee_gid, " +
                    " grosssalary_amount, " +
                    " perquisites_amount, " +
                    " profitinlieu_amount, " +
                    " grosstotal_qualifiying_amount " +
                    " ) values ( " +
                    " '" + msGetGid + "', " +
                    " '" + values.assessment_gid + "', " +
                    " '" + values.employee_gid + "', " +
                    " '" + values.grosssalary_amount + "', " +
                    " '" + values.perquisites_amount + "', " +
                    " '" + values.profitof_salary + "', " +
                    " '" + values.totalamount + "') ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    string msgetGid = objcmnfunctions.GetMasterGID("PAAE");

                    msSQL = " insert into pay_trn_ttdsallowencetotheextent ( " +
                        " tdsallowencetotheextent_gid, " +
                        " assessment_gid, " +
                        " employee_gid, " +
                        " transport_totamount, " +
                        " transport_qualifiying_amount, " +
                        " balance_qualifiying_amount, " +
                        " entertainment_amount, " +
                        " taxonemployment_amount, " +
                        " aggreegate_qualifiying_amount, " +
                        " lessallowence_name1, " +
                        " lessallowence_name2, " +
                        " lessallowence_name3, " +
                        " lessallowence_amount2, " +
                        " lessallowence_amount3, " +
                        " incomechargableunder_headsal_deductible_amount " +
                        " ) values ( " +
                        " '" + msgetGid + "', " +
                        " '" + values.assessment_gid + "', " +
                        " '" + values.employee_gid + "', " +
                        " '" + values.totalamount + "', " +
                        " '" + values.balanceamount + "', " +
                        " '" + values.balanceamount + "', " +
                        " '" + values.balanceamount + "', " +
                        " '" + values.taxon_emp + "', " +
                        " '" + values.taxon_emp + "', " +
                        " '" + values.aggreagateof_ab + "', " +
                        " '" + values.component1 + "', " +
                        " '" + values.component2 + "', " +
                        " '" + values.lessallowancetotal + "', " +
                        " '" + values.pfamount1 + "', " +
                        " '" + values.pfamount2 + "') ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult == 1)
                {
                    string msgetGid = objcmnfunctions.GetMasterGID("PAOI");

                    msSQL = " insert into pay_trn_ttdsotherincomeemployee ( " +
                        " tdsotherincomeemployee_gid, " +
                        " assessment_gid, " +
                        " employee_gid, " +
                        " otherincomeemployee_totamount1, " +
                        " otherincomeemployee_totamount2, " +
                        " otherincomeemployee_totamount3, " +
                        " otherincomeemployee_qualifiying_amount3, " +
                        " overallgross_deductible_amount, " +
                        " otherincome1_name, " +
                        " otherincome2_name, " +
                        " otherincome3_name " +
                        " ) values ( " +
                        " '" + msgetGid + "', " +
                        " '" + values.assessment_gid + "', " +
                        " '" + values.employee_gid + "', " +
                        " '" + values.employeeincome_rs1 + "', " +
                        " '" + values.employeeincome_rs2 + "', " +
                        " '" + values.employeeincome_rs3 + "', " +
                        " '" + values.employeeincome_total + "', " +
                        " '" + values.grosstotal_income + "', " +
                        " '" + values.employee_income1 + "', " +
                        " '" + values.employee_income2 + "', " +
                        " '" + values.employee_income3 + "') ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Income Details are Inserted Successfully !!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred while inserting income details !!";
                }
            }

            catch (Exception ex)
            {

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
    }
}
