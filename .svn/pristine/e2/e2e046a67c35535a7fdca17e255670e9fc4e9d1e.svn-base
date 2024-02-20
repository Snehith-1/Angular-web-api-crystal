using ems.payroll.Models;
using ems.utilities.Functions;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
//using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;
using static OfficeOpenXml.ExcelErrorValue;
using static System.Collections.Specialized.BitVector32;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace ems.payroll.DataAccess
{
    public class DaPayTrnBonus
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;

        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        int mnResult;
        string msGetGID, msGetGid1, lsempoyeegid;

        // Module Master Summary
        public void DaGetBonusSummary(MdlPayTrnBonus values)
        {
            msSQL = "select a.bonus_gid,a.bonus_name,a.bonus_date,date_format(a.bonus_date,'%Y-%m-%d')as bonus_fromdate," +
                    " date_format(a.bonus_todate,'%Y-%m-%d') as bonus_todate,a.bonus_percentage from pay_trn_tbonus a " +
                    " where system_flag='N' order by a.bonus_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetBonus>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetBonus
                    {
                        bonus_gid = dt["bonus_gid"].ToString(),
                        bonus_name = dt["bonus_name"].ToString(),
                        bonus_fromdate = dt["bonus_fromdate"].ToString(),
                        bonus_todate = dt["bonus_todate"].ToString(),
                        bonus_percentage = dt["bonus_percentage"].ToString(),


                    });
                    values.GetBonus = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaPostBonus(createbonus_list values,string user_gid)
        {
            try
            {
                msGetGID = objcmnfunctions.GetMasterGID("PYBS");
                {
                    string uiDateStr = values.bonus_todate;
                    DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string bonus_todate = uiDate.ToString("yyyy-MM-dd");

                    string uiDateSt = values.bonus_date;
                    DateTime uDate = DateTime.ParseExact(uiDateSt, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string bonus_date = uDate.ToString("yyyy-MM-dd");

                    msSQL = " Insert into pay_trn_tbonus" +
                        " (bonus_gid, " +
                        " bonus_name," +
                        " bonus_date," +
                        " bonus_todate, " +
                        " created_by," +
                        " remarks," +
                        " bonus_percentage, " +
                        " created_date, " +
                        " system_flag) " +
                        "Values  " +
                        " ('" + msGetGID + "', " +
                        "'" + values.bonus_name + "'," +
                        " '" + bonus_date + "'," +
                        " '" + bonus_todate + "'," +
                        " '" + values.employee_gid + "'," +
                        " '" + values.remarks + "'," +
                        " '" + values.bonus_percentage + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                        " '" + 'N' + "')";
                }
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Bonus Created Successfully";
                }

                else
                {
                    values.status = false;
                    values.message = "Error Occured !!";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

        public void DadeleteBonus(string bonus_gid, createbonus_list values)
        {
            try
            {

                msSQL = "  delete from pay_trn_tbonus where bonus_gid='" + bonus_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Bonus Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deleting Bonus";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }



        public void DaGetBonusEmployeeSummary(string bonus_gid, MdlPayTrnBonus values)
            {
            try
            {
                msSQL = " Select distinct a.user_gid, " +
                       " a.user_firstname," +
                       " a.user_code,  " +
                       " d.designation_name ,c.employee_gid,e.branch_name, " +
                       " c.department_gid,c.branch_gid, g.department_name " +
                       " FROM adm_mst_tuser a " +
                       " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                       " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                       " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                       " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                       " left join hrm_trn_temployeetypedtl h on c.employee_gid=h.employee_gid " +
                       " left join hrm_mst_twagestype i on h.wagestype_gid= i.wagestype_gid " +
                       " left join hrm_mst_temployeetype j on h.employeetype_name= j.employee_type " +
                       " inner join pay_trn_tsalary s on s.employee_gid=c.employee_gid " +
                       " WHERE a.user_status='Y' " +
                       " and c.employee_gid not in  " +
                       " (select employee_gid from pay_trn_temployee2bonus where bonus_gid='" + bonus_gid + "' and bonus_flag='Y')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<bonusSummarylist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new bonusSummarylist
                        {

                            user_gid = dt["user_gid"].ToString(),
                            user_firstname = dt["user_firstname"].ToString(),
                            user_code = dt["user_code"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            department_gid = dt["department_gid"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),
                            department_name = dt["department_name"].ToString(),

                        });
                        values.bonusSummarylist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while deleting Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

        public void DaPostBonusEmployee(selectemployee_list values, string user_gid)
        {
            try
            {
                foreach (var data in values.bonusSummarylist)
                {
                    msSQL = "SELECT bonus_gid,DATE_FORMAT(bonus_date, '%Y-%m-%d') as bonus_date,date_format(bonus_todate,'%Y-%m-%d') as bonus_todate,bonus_percentage FROM pay_trn_tbonus " +
                "where bonus_gid='" + values.bonus_gid + "'";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader.HasRows == true)
                    {
                          string bonus_gid= objOdbcDataReader["bonus_gid"].ToString();
                          string bonus_date= objOdbcDataReader["bonus_date"].ToString();
                          string bonus_todate= objOdbcDataReader["bonus_todate"].ToString();
                          string bonus_percentage = objOdbcDataReader["bonus_percentage"].ToString();
                    
                    msGetGID = objcmnfunctions.GetMasterGID("PYEB");
                    {
                            msSQL = " insert into pay_trn_temployee2bonus(" +
                      " employee2bonus_gid, " +
                      " bonus_gid, " +
                      " employee_gid, " +
                      " created_by, " +
                      " created_date, " +
                      " bonus_percentage, " +
                      " bonus_flag," +
                      " bonus_from," +
                      " bonus_to) " +
                      " values( " +
                      "'" + msGetGID + "', " +
                      "'" + values.bonus_gid + "', " +
                      "'" + data.employee_gid + "', " +
                      "'" + user_gid + "', " +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                      "'" + bonus_percentage + "', " +
                      " 'Y', " +
                      "'" + bonus_date + "', " +
                      "'" + bonus_todate + "') ";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Employee Selected Successfully";
                    }

                    else
                    {
                        values.status = false;
                        values.message = "Error Occured !!";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

        public void DaGetgenerateBonus(string bonus_gid, MdlPayTrnBonus values)
        {
            try
            {
                {
                    msSQL = " Select distinct a.user_gid, " +
                            " a.user_firstname, " +
                            " a.user_code, " +
                            " d.designation_name, c.employee_gid, e.branch_name, " +
                            " c.department_gid, c.branch_gid, e.branch_name, g.department_name, x.bonus_gid,y.bonus_name, " +
                            " date_format(x.bonus_from,'%d-%m-%Y') as bonus_from, date_format(x.bonus_to,'%d-%m-%Y') as bonus_to, format(x.bonus_amount,2) as bonus_amount ,y.bonus_percentage " +
                            " FROM  pay_trn_temployee2bonus x " +
                            " left join pay_trn_tbonus y on x.bonus_gid=y.bonus_gid " +
                            " left join hrm_mst_temployee c on x.employee_gid=c.employee_gid " +
                            " left join adm_mst_tuser a on  c.user_gid = a.user_gid " +
                            " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                            " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                            " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                            " left join hrm_trn_temployeetypedtl h on c.employee_gid=h.employee_gid " +
                            " left join hrm_mst_twagestype i on h.wagestype_gid= i.wagestype_gid " +
                            " left join hrm_mst_temployeetype j on h.employeetype_name= j.employee_type " +
                            " WHERE x.bonus_flag='Y' and x.bonus_gid='" + bonus_gid + "' ";
                }

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<generatebonus_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new generatebonus_list
                        {

                            user_code = dt["user_code"].ToString(),
                            user_firstname = dt["user_firstname"].ToString(),
                            bonus_name = dt["bonus_name"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            department_gid = dt["department_gid"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),
                            department_name = dt["department_name"].ToString(),
                            bonus_from = dt["bonus_from"].ToString(),
                            bonus_to = dt["bonus_to"].ToString(),
                            bonus_percentage = dt["bonus_percentage"].ToString(),
                            bonus_amount = dt["bonus_amount"].ToString(),
                            bonus_gid = dt["bonus_gid"].ToString(),


                        });
                        values.generatebonus_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

        public void DaUpdatedbonus(string user_gid, updatebonus_list values)
        {
            try
            {

                msSQL = " update  pay_trn_temployee2bonus set " +
             " bonus_amount = '" + values.bonus_amount + "' " +
             " where employee_gid = '" + values.employee_gid + "' and bonus_gid='" + values.bonus_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Bonus Amount Updated Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Bonus Amount";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

    }

}