using ems.hrm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Globalization;

namespace ems.hrm.DataAccess
{
    public class DaOfferLetter
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;

        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        int mnResult;
        string msGetGid, msGetGid1, lsempoyeegid, msgetshift;
        public void DaOfferLetterSummary(MdlOfferLetter values)
        {
            try
            {

                msSQL = " SELECT a.offer_gid, concat(a.first_name,' ',a.last_name) as first_name, a.gender, a.dob, a.mobile_number, a.email_address, a.qualification,a.offerletter_type, " +
                " a.experience_detail, a.perm_address_gid, a.temp_address_gid, a.template_gid, a.created_by, a.created_date,a.employee_gid, " +
                " a.branch_name,a.designation_name,a.offer_date " +
                " FROM hrm_trn_tofferletter a " +
                " left join hrm_trn_temployeetypedtl j on a.employee_gid=j.employee_gid " +
                " order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Offersummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Offersummary_list
                        {
                            offer_gid = dt["offer_gid"].ToString(),
                            first_name = dt["first_name"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            offer_date = dt["offer_date"].ToString(),

                        });
                        values.Offersummary_list = getModuleList;
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

        public void DaAddofferletter(string employee_gid, AddOfferletter_list values)
        {

            try
            {
                msSQL = "select branch_name from hrm_mst_tbranch where branch_gid='" + values.branch_gid + "'";
                string lsbranchName = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select designation_name from adm_mst_tdesignation where designation_gid='" + values.designation_gid + "'";
                string lsdesignation = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select department_name from hrm_mst_Tdepartment where department_gid='" + values.department_gid + "'";
                string lsdeparmentname = objdbconn.GetExecuteScalar(msSQL);
                msGetGid = objcmnfunctions.GetMasterGID("HOFP");
                if (DateTime.TryParseExact(values.offer_date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                   values.offer_date = parsedDate.ToString("yyyy-MM-dd");
                }
                if (DateTime.TryParseExact(values.dob, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate1))
                {
                    values.dob = parsedDate.ToString("yyyy-MM-dd");
                }
                if (DateTime.TryParseExact(values.joiningdate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate2))
                {
                    values.joiningdate = parsedDate.ToString("yyyy-MM-dd");
                }

                msSQL = " insert into hrm_trn_tofferletter " +
                     " ( offer_gid ," +
                     " offer_refno," +
                     " offer_date," +
                     " first_name," +
                     " last_name, " +
                     " gender, " +
                     " dob, " +
                     " mobile_number, " +
                     " email_address," +
                     " qualification," +
                     " experience_detail," +
                     " document_path," +
                     " created_by," +
                     " created_date, " +
                     " employee_salary, " +
                     " perm_address1, " +
                     " perm_address2, " +
                     " perm_country, " +
                     " perm_state, " +
                     " perm_city, " +
                     " perm_pincode, " +
                     " temp_address1, " +
                     " temp_address2, " +
                     " temp_country, " +
                     " temp_state, " +
                     " temp_city, " +
                     " temp_pincode, " +
                     " joiningdate, " +
                     " template_gid, " +
                     " letter_flag, " +
                     " employee_gid," +
                     " designation_gid," +
                     " designation_name, " +
                     " department_gid," +
                     " department_name, " +
                     " branch_gid," +
                     " offerletter_type," +
                     " offertemplate_content," +
                     " branch_name " +
                     " )values ( " +
                     "'" + msGetGid + "', " +
                     "'" + values.offer_refno + "'," +
                     "'" + values.offer_date + "'," +
                     "'" + values.first_name + "'," +
                     "'" + values.last_name + "'," +
                     "'" + values.gender + "'," +
                     "'" + values.dob + "'," +
                     "'" + values.mobile_number + "'," +
                     "'" + values.email_address + "'," +
                     "'" + values.qualification + "'," +
                     "'" + values.experience_detail + "'," +
                     "'" + values.document_path + "', " +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                     "'" + values.employee_salary + "'," +
                     "'" + values.perm_address1 + "'," +
                     "'" + values.perm_address2 + "'," +
                     "'" + values.perm_country + "'," +
                     "'" + values.perm_state + "'," +
                     "'" + values.perm_city + "'," +
                     "'" + values.perm_pincode + "'," +
                     "'" + values.temp_address1 + "'," +
                     "'" + values.temp_address2 + "'," +
                     "'" + values.temp_country + "'," +
                     "'" + values.temp_state + "'," +
                     "'" + values.temp_city + "'," +
                     "'" + values.temp_pincode + "',"+
                     "'" + values.joiningdate + "'," +
                     "'" + values.template_gid + "', "+
                     "'Pending'," +
                     "'" + employee_gid + "'," +
                     "'" + values.designation_gid + "'," +
                     "'" + lsdesignation + "'," +
                     "'" + values.department_gid + "'," +
                     "'" + lsdeparmentname + "',"+
                     "'" + values.branch_gid + "'," +
                     "'" + values.offerletter_type + "'," +
                     "'" + values.offertemplate_content + "'," +
                     "'" + lsbranchName + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
             
               
                  
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Offer Letter Added Sucessfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Adding Offer Letter";
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