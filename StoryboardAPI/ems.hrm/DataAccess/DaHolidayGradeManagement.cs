using ems.hrm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace ems.hrm.DataAccess
{
    public class DaHolidayGradeManagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string lsholidaygrade_code, lsholidaygrade_name;

        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        int mnResult;
        string msGetGid, msGetGid1, lsempoyeegid, msgetshift;

        public void DaHolidayGradeSummary(MdlHolidaygradeManagement values)
        {
            try
            {
                
                msSQL = "select  holidaygrade_gid,holidaygrade_code,holidaygrade_name from hrm_mst_tholidaygrade";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<holidaygrade_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new holidaygrade_list
                        {
                            holidaygrade_gid = dt["holidaygrade_gid"].ToString(),
                            holidaygrade_code = dt["holidaygrade_code"].ToString(),
                            holidaygrade_name = dt["holidaygrade_name"].ToString(),


                        });
                        values.holidaygrade_list = getModuleList;
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
        public void DaAddHolidayGradesubmit(addholidaygrade_list values)
        {
            try
            {
                
                string uiDateStr = values.holiday_date;
                DateTime holiday_date = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string holidaymysqlDate = holiday_date.ToString("yyyy-MM-dd");

                msGetGid = objcmnfunctions.GetMasterGID("HHDM");
                {
                    msSQL = " Insert into hrm_mst_tholiday( " +
                        " holiday_gid," +
                        " holiday_name, " +
                        " holiday_type, " +
                        " holiday_date," +
                        " holiday_remarks" + " )" +
                        "values( " +
                        " '" + msGetGid + "'," +
                        "'" + values.holiday_name + "'," +
                        "'" + values.holiday_type + "'," +
                        "'" + holidaymysqlDate + "'," +
                        "'" + values.holiday_remarks + "')";
                }
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Holiday Grade Added Sucessfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Adding HolidayGrade";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaAddholidaysummary(MdlHolidaygradeManagement values)
        {
            try
            {
                
                msSQL = " select holiday_gid,date_format(holiday_date, '%d/%m/%Y') as holiday_date," +
                  " left(holiday_remarks,15) as holidayremarks,holiday_remarks,holiday_name,holiday_type " +
                 " from hrm_mst_tholiday where year(holiday_date)>='" + DateTime.Now.ToString("yyyy") + "' order by DATE(holiday_date) asc, holiday_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<holidaygrade1_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new holidaygrade1_list
                        {
                            holiday_gid = dt["holiday_gid"].ToString(),
                            holiday_date = dt["holiday_date"].ToString(),
                            holidayremarks = dt["holidayremarks"].ToString(),
                            holiday_remarks = dt["holiday_remarks"].ToString(),
                            holiday_name = dt["holiday_name"].ToString(),
                            holiday_type = dt["holiday_type"].ToString(),
                        });
                        values.holidaygrade1_list = getModuleList;
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
        public void DaHolidayAssignSubmit(string user_gid, Addholidayassign_list values) 
        {
            try
            {
                
                msSQL = "select holidaygrade_code,holidaygrade_name from hrm_mst_tholidaygrade";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lsholidaygrade_code = objMySqlDataReader["holidaygrade_code"].ToString();
                    lsholidaygrade_name = objMySqlDataReader["holidaygrade_name"].ToString();
                }
                msGetGid = objcmnfunctions.GetMasterGID("HOGD");
                msSQL = " insert into hrm_mst_tholidaygrade( " +
                             " holidaygrade_gid , " +
                             " holidaygrade_code, " +
                             " holidaygrade_name, " +
                             " created_by, " +
                             " created_date ) " +
                             " values( " +
                             " '" + msGetGid + "'," +
                             "'" + values.holidaygrade_code + "'," +
                             "'" + values.holidaygrade_name + "'," +
                             "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                foreach (var data in values.holidaygrade_list)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("HO2G");
                    msSQL = " insert into hrm_mst_tholiday2grade ( " +
                                         " holiday2gradedtl_gid, " +
                                         " holidaygrade_gid, " +
                                          " holiday_gid, " +
                                          " holiday_date, " +
                                           " holiday_name) " +
                                         " values ( " +
                                         "'" + msGetGid1 + "', " +
                                         "'" + msGetGid + "', " +
                                         " '" + data.holiday_gid + "'," +
                                        "'" + data.holiday_date.ToString("yyyy-MM-dd") + "'," +
                                        "'" + data.holiday_name + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Holiday Grade Assign Sucessfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Assign Holiday grade";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaDeleteholiday(string holiday_gid, MdlHolidaygradeManagement values)
        {
            try
            {
                
                msSQL = "  delete from hrm_mst_tholiday where holiday_gid='" + holiday_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = " Add Holiday Deleted Successfully";
                }
                else
                {
                     values.status = false;
                     values.message = "Error While Deleting Holiday Adding";
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