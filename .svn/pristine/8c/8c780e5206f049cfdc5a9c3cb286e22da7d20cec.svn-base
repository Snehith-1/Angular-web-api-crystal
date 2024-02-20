using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using System.Data.Odbc;

using ems.hrm.Models;
using MySql.Data.MySqlClient;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.hrm.DataAccess
{
    public class DaBiometric : ApiController
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objMySqlDataReader;
        string msSQL = string.Empty;

        DataTable dt_datatable;
        string msEmployeeGID, msGetGID;
        int mnResult;

        public bool DabiometricSummary(MdlBiometric objbiolist)
        {
            try
            {                
                msSQL = " Select distinct c.employee_gid, a.user_gid,c.useraccess, " +
                " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                " c.employee_gender,  " +
                " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                " d.designation_name,c.designation_gid,c.employee_gid,e.branch_name, " +
                " CASE " +
                " WHEN a.user_status = 'Y' THEN 'Active'  " +
                " WHEN a.user_status = 'N' THEN 'Inactive' " +
                " END as user_status,c.department_gid,c.branch_gid, e.branch_name, g.department_name,n.biometric_gid,n.nfc_cardno " +
                " FROM adm_mst_tuser a " +
                " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid  " +
                " left join hrm_mst_tbiometric n on n.employee_gid=c.employee_gid " +
                " where a.user_status='Y' " +
                " order by c.employee_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbiometric_list = new List<biometricsummary>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        getbiometric_list.Add(new biometricsummary
                        {
                            branch = dr_row["branch_name"].ToString(),
                            department = dr_row["department_name"].ToString(),
                            employee_code = dr_row["user_code"].ToString(),
                            employee_gid = dr_row["employee_gid"].ToString(),
                            employee_name = dr_row["user_name"].ToString(),
                            designation_name = dr_row["designation_name"].ToString(),
                            biometric_id = dr_row["biometric_gid"].ToString(),
                            nfc_id = dr_row["nfc_cardno"].ToString(),
                            user_status = dr_row["user_status"].ToString()
                        });
                    }
                    objbiolist.biometricsummary = getbiometric_list;
                    objbiolist.status = true;
                    return true;
                }
                else
                {
                    objbiolist.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                objbiolist.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return false;
            }
        }
        public void DabiometricUpdate(popupupdate values, string user_gid)
        {
            try
            {
                
                string lsreg = "";

                msSQL = " select biometric_register from hrm_mst_tbiometric where employee_gid='" + values.employee_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    msSQL = " update hrm_mst_tbiometric set" +
                        "  nfc_cardno='" + values.nfc_cardno + "', " +
                        " biometric_gid='" + values.biometric_id + "', " +
                        " biometric_register='" + lsreg + "' " +
                        " where employee_gid='" + values.employee_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objMySqlDataReader.Close();
                }
                else
                {
                    msSQL = "Insert into hrm_mst_tbiometric" +
                        "( biometric_gid," +
                        " biometric_status," +
                        " nfc_cardno, " +
                        " biometric_register, " +
                        " employee_gid) " +
                        " values( " +
                        " '" + values.biometric_id + "', " +
                        " 'N', " +
                        " '" + values.nfc_cardno + "', " +
                        " '" + lsreg + "', " +
                        " '" + values.employee_gid + "') ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objMySqlDataReader.Close();
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