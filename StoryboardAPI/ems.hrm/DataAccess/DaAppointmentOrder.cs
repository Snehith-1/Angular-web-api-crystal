using ems.hrm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web;
using System.Data.Odbc;
using static OfficeOpenXml.ExcelErrorValue;
namespace ems.hrm.DataAccess
{
    public class DaAppointmentOrder
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        int mnResult;
        public void DaGetappointmentorderSummary(MdlAppoinmentOrder values)
        {
            try
            {                
                msSQL = " SELECT a.appointmentorder_gid, concat(a.first_name,' ',a.last_name) as user_name , a.gender, a.dob, a.mobile_number, a.email_address, a.qualification,a.employee_gid, " +
                        " a.experience_detail, a.perm_address_gid, a.temp_address_gid, a.template_gid, a.created_by, a.created_date, " +
                        " a.branch_name,a.designation_name,date_format(a.appointment_date, '%d-%m-%Y') as appointment_date " +
                        " FROM hrm_trn_tappointmentorder a " +
                        " left join hrm_trn_temployeetypedtl j on a.employee_gid=j.employee_gid ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<appointmentorder_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new appointmentorder_list
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
                        values.appointmentorder_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }

            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetbranchdropdown(MdlAppoinmentOrder values)
        {
            try
            {                
                msSQL = " select branch_gid,concat(branch_code,'/',branch_name)as branch_name from hrm_mst_tbranch " +
                        " order by branch_name asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getbranchdropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getbranchdropdown
                        {
                            branch_gid = dt["branch_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
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
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetdepartmentdropdown(MdlAppoinmentOrder values)
        {
            try
            {
                msSQL = " SELECT distinct a.department_gid, a.department_name " +
                    " FROM hrm_mst_tdepartment a " +
                    " order by a.department_name asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getdepartmentdropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getdepartmentdropdown
                        {
                            department_gid = dt["department_gid"].ToString(),
                            department_name = dt["department_name"].ToString(),
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
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetdesignationdropdown(MdlAppoinmentOrder values)
        {
            try
            {
                msSQL = " SELECT distinct a.designation_gid, b.designation_name " +
                        " FROM hrm_mst_temployee a " +
                        " left join adm_mst_tdesignation b on a.designation_gid = b.designation_gid " +
                        " order by designation_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getdesignationdropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getdesignationdropdown
                        {
                            designation_gid = dt["designation_gid"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
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
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public editappoinmentorderlist DaGeteditappoinmentorder(string appointmentorder_gid)
        {
            try
            {                
                editappoinmentorderlist objeditappointmentorderlist = new editappoinmentorderlist();
                {
                    msSQL = " Select appointmentorder_gid,branch_gid,employee_salary,branch_name,designation_gid,designation_name,appointmentorder_gid,date_format(joiningdate,'%d-%m-%Y') as joiningdate,first_name, last_name, gender, date_format(dob,'%d-%m-%Y') as dob, mobile_number, email_address, qualification, " +
                    " experience_detail,document_path, perm_address_gid,appointmentordertemplate_content,date_format(appointment_date,'%d-%m-%Y')as appointment_date,temp_address_gid,template, template_gid,appointment_type, created_by,department_gid,department_name, " +
                    " created_date, employee_gid, appointmentordertemplate_content,template_gid,perm_address1,perm_address2,perm_country,perm_state,perm_city,perm_pincode, " +
                    " temp_address1, temp_address2,temp_country,temp_state,temp_city,temp_pincode " +
                    " FROM hrm_trn_tappointmentorder " +
                    " where appointmentorder_gid ='" + appointmentorder_gid + "' ";
                }

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    objeditappointmentorderlist.appointmentorder_gid = objMySqlDataReader["appointmentorder_gid"].ToString();
                    objeditappointmentorderlist.branch_name = objMySqlDataReader["branch_name"].ToString();
                    objeditappointmentorderlist.appointment_date = objMySqlDataReader["appointment_date"].ToString();
                    objeditappointmentorderlist.first_name = objMySqlDataReader["first_name"].ToString();
                    objeditappointmentorderlist.last_name = objMySqlDataReader["last_name"].ToString();
                    objeditappointmentorderlist.branch_gid = objMySqlDataReader["branch_gid"].ToString();
                    objeditappointmentorderlist.department_gid = objMySqlDataReader["department_gid"].ToString();
                    objeditappointmentorderlist.designation_gid = objMySqlDataReader["designation_gid"].ToString();
                    objeditappointmentorderlist.gender = objMySqlDataReader["gender"].ToString();
                    objeditappointmentorderlist.experience_detail = objMySqlDataReader["experience_detail"].ToString();
                    objeditappointmentorderlist.branch_name = objMySqlDataReader["branch_name"].ToString();
                    objeditappointmentorderlist.dob = objMySqlDataReader["dob"].ToString();
                    objeditappointmentorderlist.mobile_number = objMySqlDataReader["mobile_number"].ToString();
                    objeditappointmentorderlist.email_address = objMySqlDataReader["email_address"].ToString();
                    objeditappointmentorderlist.joiningdate = objMySqlDataReader["joiningdate"].ToString();
                    objeditappointmentorderlist.qualification = objMySqlDataReader["qualification"].ToString();
                    objeditappointmentorderlist.department_name = objMySqlDataReader["department_name"].ToString();
                    objeditappointmentorderlist.designation_name = objMySqlDataReader["designation_name"].ToString();
                    objeditappointmentorderlist.employee_salary = objMySqlDataReader["employee_salary"].ToString();
                    objeditappointmentorderlist.perm_address1 = objMySqlDataReader["perm_address1"].ToString();
                    objeditappointmentorderlist.perm_address2 = objMySqlDataReader["perm_address2"].ToString();
                    objeditappointmentorderlist.perm_country = objMySqlDataReader["perm_country"].ToString();
                    objeditappointmentorderlist.perm_state = objMySqlDataReader["perm_state"].ToString();
                    objeditappointmentorderlist.perm_city = objMySqlDataReader["perm_city"].ToString();
                    objeditappointmentorderlist.perm_pincode = objMySqlDataReader["perm_pincode"].ToString();
                    objeditappointmentorderlist.temp_address1 = objMySqlDataReader["temp_address1"].ToString();
                    objeditappointmentorderlist.temp_address2 = objMySqlDataReader["temp_address2"].ToString();
                    objeditappointmentorderlist.temp_country = objMySqlDataReader["temp_country"].ToString();
                    objeditappointmentorderlist.temp_state = objMySqlDataReader["temp_state"].ToString();
                    objeditappointmentorderlist.temp_city = objMySqlDataReader["temp_city"].ToString();
                    objeditappointmentorderlist.temp_pincode = objMySqlDataReader["temp_pincode"].ToString();
                    objeditappointmentorderlist.template_gid = objMySqlDataReader["template_gid"].ToString();

                    objMySqlDataReader.Close();
                }
                return objeditappointmentorderlist;
            }

            catch (Exception ex)
            {
                ex.ToString();
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return null;
            }
        }
        public void DaGetcountrydropdown(MdlAppoinmentOrder values)
        {
            try
            {                
                msSQL = " Select country_gid,country_name as country from adm_mst_tcountry";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<getcountrydropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new getcountrydropdown
                        {
                            country_gid = dt["country_gid"].ToString(),
                            country = dt["country"].ToString(),
                        });
                        values.getcountrydropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaUpdatedappointmentorder(string employee_gid, update_list values)
        {
            try
            {                
                msSQL =   " update  hrm_trn_tappointmentorder  set " +
                          " first_name = '" + values.first_name + "'," +
                          " last_name = '" + values.last_name + "'," +
                          " dob = '" + values.dob.ToString("yyyy-MM-dd") + "'," +
                          " joiningdate = '" + values.joiningdate.ToString("yyyy-MM-dd") + "'," +
                          " employee_salary = '" + values.employee_salary + "'," +
                          " appointment_date = '" + values.appointment_date.ToString("yyyy-MM-dd") + "'," +
                          " designation_gid = '" + values.designation_gid + "'," +
                          " designation_name = '" + values.designation_name + "'," +
                          " department_gid = '" + values.department_gid + "'," +
                          " department_name = '" + values.department_name + "'," +
                          " branch_gid = '" + values.branch_gid + "'," +
                          " branch_name = '" + values.branch_name + "'," +
                          " email_address = '" + values.email_address + "'," +
                          " mobile_number = '" + values.mobile_number + "'," +
                          " gender = '" + values.gender + "'," +
                          " qualification = '" + values.qualification + "'," +
                          " experience_detail = '" + values.experience_detail + "'," +
                          " document_path = '" + values.document_path + "'," +
                          " perm_address1 = '" + values.perm_address1 + "'," +
                          " perm_address2 = '" + values.perm_address2 + "'," +
                          " perm_country = '" + values.perm_country + "'," +
                          " perm_state = '" + values.perm_state + "'," +
                          " perm_city = '" + values.perm_city + "'," +
                          " perm_pincode = '" + values.perm_pincode + "'," +
                          " temp_address1 = '" + values.temp_address1 + "'," +
                          " temp_address2 = '" + values.temp_address2 + "'," +
                          " temp_country = '" + values.temp_country + "'," +
                          " temp_state = '" + values.temp_state + "'," +
                          " temp_city = '" + values.temp_city + "'," +
                          " temp_pincode = '" + values.temp_pincode + "'," +
                          " updated_by = '" + employee_gid + "'," +
                          " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                          " template_gid = '" + values.template_gid + "'," +
                          " appointment_type = '" + values.appointment_type + "'," +
                          " appointmentordertemplate_content = '" + values.appointmentordertemplate_content + "'" +
                          " where appointmentorder_gid='" + values.appointmentorder_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = " Appointmet Order Updated Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaTermsandConditions(MdlAppoinmentOrder values)
        {
            try
            {                
                msSQL = " select template_gid, template_name, template_content from adm_mst_ttemplate ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetAppointmentdropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetAppointmentdropdown
                        {
                            template_gid = dt["template_gid"].ToString(),
                            template_name = dt["template_name"].ToString(),
                            template_content = dt["template_content"].ToString()
                        });
                        values.GetAppointmentdropdown = getModuleList;
                    }
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaOnChangeTerms(string template_gid, MdlAppoinmentOrder values)
        {
            try
            {                
                if (template_gid != null)
                {
                    msSQL = " select template_gid, template_name, template_content from adm_mst_ttemplate where template_gid='" + template_gid + "' ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    var getModuleList = new List<GetAppointmentdropdown>();

                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getModuleList.Add(new GetAppointmentdropdown
                            {
                                template_gid = dt["template_gid"].ToString(),
                                template_name = dt["template_name"].ToString(),
                                template_content = dt["template_content"].ToString(),
                            });
                            values.GetAppointmentdropdown = getModuleList;
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
    }
}
