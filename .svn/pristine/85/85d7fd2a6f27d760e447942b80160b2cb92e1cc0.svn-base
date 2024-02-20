using ems.payroll.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System.Web.UI.WebControls;

namespace ems.payroll.DataAccess
{
    public class DaPayMstTDS
    {

        HttpPostedFile httpPostedFile;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;

        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        int mnResult;
        string msGetGid, msGetGid1, lsempoyeegid, exemployee_code, lsemployeegid, lsbankname;
        public void DaGetEmployeePersonalDetails(Personal_details values)
        {
            try
            {
                msSQL = " select b.user_firstname,b.user_lastname,a.employee_dob as employee_dob ,a.employee_emailid,a.employee_gender," +
                        " a.pan_no, a.uan_no,a.bloodgroup,a.employee_mobileno,x.address1,x.address2,x.city,x.state,x.postal_code,x.country_gid " +
                        " from hrm_mst_temployee a inner join adm_mst_tuser b on a.user_gid=b.user_gid " +
                        " left join adm_mst_taddress x on a.employee_gid=x.parent_gid " +
                        " left join hrm_trn_temployeedtl y on x.address_gid=y.permanentaddress_gid "+
                        " left join adm_mst_tcountry z on x.country_gid=z.country_gid " +
                        " where a.employee_gid='" + values.employee_gid + "' ";
                objMySqlDataReader=objdbconn.GetDataReader(msSQL);
                if(objMySqlDataReader.HasRows==true)
                {
                    values.employee_firstname = objMySqlDataReader["user_firstname"].ToString();
                    values.employee_lastname = objMySqlDataReader["user_lastname"].ToString();
                    values.employee_DOB = DateTime.Parse( objMySqlDataReader["employee_dob"].ToString());
                    values.bloodgroup = objMySqlDataReader["bloodgroup"].ToString();
                    values.pan_number = objMySqlDataReader["pan_no"].ToString();
                    values.uan_number = objMySqlDataReader["uan_no"].ToString();
                    values.email_address = objMySqlDataReader["employee_emailid"].ToString();
                    values.employee_phone = objMySqlDataReader["employee_mobileno"].ToString();
                    values.employee_addressline1 = objMySqlDataReader["address1"].ToString();
                    values.employee_addressline2 = objMySqlDataReader["address2"].ToString();
                    values.employee_city = objMySqlDataReader["city"].ToString();
                    values.employee_state = objMySqlDataReader["state"].ToString();
                    values.postal_code = objMySqlDataReader["postal_code"].ToString();
                    values.country_gid = objMySqlDataReader["country_gid"].ToString();
                    values.employee_gender = objMySqlDataReader["employee_gender"].ToString();
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Employee Bank details!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Payroll/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }
        public void DaGetQuarter(quarters values)
        {
            try
            {
                msSQL = "select tdsquarter1_receiptno,tdsquarter2_receiptno,tdsquarter3_receiptno,tdsquarter4_receiptno,tdsquarter1_amount_deducted," +
                   "tdsquarter2_amount_deducted,tdsquarter3_amount_deducted,tdsquarter4_amount_deducted,tdsquarter1_amount_deposited," +
                   "tdsquarter2_amount_deposited,tdsquarter3_amount_deposited,tdsquarter4_amount_deposited,tdsquarter1_paidcredited," +
                   "tdsquarter2_paidcredited,tdsquarter3_paidcredited,tdsquarter4_paidcredited from  pay_trn_ttdssummary where assessment_gid='" + values.assessment_gid + "' " +
                   " and employee_gid='" + values.employee_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    values.tdsquarter1_receiptno = objMySqlDataReader["tdsquarter1_receiptno"].ToString();
                    values.tdsquarter1_amount_deducted = objMySqlDataReader["tdsquarter1_amount_deducted"].ToString();
                    values.tdsquarter1_amount_deposited = objMySqlDataReader["tdsquarter1_amount_deposited"].ToString();
                    values.tdsquarter1_paidcredited = objMySqlDataReader["tdsquarter1_paidcredited"].ToString();

                    values.tdsquarter2_receiptno = objMySqlDataReader["tdsquarter2_receiptno"].ToString();
                    values.tdsquarter2_amount_deducted = objMySqlDataReader["tdsquarter2_amount_deducted"].ToString();
                    values.tdsquarter2_amount_deposited = objMySqlDataReader["tdsquarter2_amount_deposited"].ToString();
                    values.tdsquarter2_paidcredited = objMySqlDataReader["tdsquarter2_paidcredited"].ToString();


                    values.tdsquarter3_receiptno = objMySqlDataReader["tdsquarter3_receiptno"].ToString();
                    values.tdsquarter3_amount_deducted = objMySqlDataReader["tdsquarter3_amount_deducted"].ToString();
                    values.tdsquarter3_amount_deposited = objMySqlDataReader["tdsquarter3_amount_deposited"].ToString();
                    values.tdsquarter3_paidcredited = objMySqlDataReader["tdsquarter3_paidcredited"].ToString();

                    values.tdsquarter4_receiptno = objMySqlDataReader["tdsquarter4_receiptno"].ToString();
                    values.tdsquarter4_amount_deducted = objMySqlDataReader["tdsquarter4_amount_deducted"].ToString();
                    values.tdsquarter4_amount_deposited = objMySqlDataReader["tdsquarter4_amount_deposited"].ToString();
                    values.tdsquarter4_paidcredited = objMySqlDataReader["tdsquarter4_paidcredited"].ToString();

                    values.tdsquarter_totalamount_deducted = double.Parse(objMySqlDataReader["tdsquarter_totalamount_deducted"].ToString());
                    values.tdsquarter_totalamount_deposited = double.Parse(objMySqlDataReader["tdsquarter_totalamount_deposited"].ToString());
                    values.tdsquarter_totalamount_deposited = double.Parse(objMySqlDataReader["totalamount_paidcredited"].ToString());
                }
            }
            catch (Exception ex) 
            {
                values.message = "Exception occured while loading Employee Bank details!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Payroll/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetIncomeDetails(Income values)
        {
            try
            {
                msSQL = " select a.grosssalary_amount,a.perquisites_amount,a.profitinlieu_amount,a.grosstotal_qualifiying_amount,b.transport_totamount,b.transport_qualifiying_amount, " +
                        " b.balance_qualifiying_amount,b.entertainment_amount,b.taxonemployment_amount,b.aggreegate_qualifiying_amount,b.incomechargableunder_headsal_deductible_amount, " +
                        " c.otherincomeemployee_totamount1,c.otherincomeemployee_totamount2,c.otherincomeemployee_totamount3,c.otherincome1_name,c.otherincome2_name,c.otherincome3_name, " +
                        " c.otherincomeemployee_qualifiying_amount3,c.overallgross_deductible_amount,b.lessallowence_name1,b.lessallowence_name2, " +
                        " b.lessallowence_name3,b.lessallowence_amount2,b.lessallowence_amount3 from pay_trn_ttdsgrosssalary a " +
                        " inner join pay_trn_ttdsallowencetotheextent b on a.assessment_gid=b.assessment_gid " +
                        " inner join pay_trn_ttdsotherincomeemployee c on b.assessment_gid=c.assessment_gid where a.assessment_gid='" + values.assessment_gid + "' " +
                        " and a.employee_gid='" + values.employee_gid + "' ";
                objMySqlDataReader =objdbconn.GetDataReader(msSQL);
                if(objMySqlDataReader.HasRows == true)
                {
                    values.grosssalary_amount = double.Parse(objMySqlDataReader["grosssalary_amount"].ToString());
                    values.perquisites_amount = double.Parse(objMySqlDataReader["perquisites_amount"].ToString());
                    values.profitinlieu_amount = double.Parse(objMySqlDataReader["profitinlieu_amount"].ToString());
                    values.grosstotal_qualifiying_amount = double.Parse(objMySqlDataReader["grosstotal_qualifiying_amount"].ToString());
                    values.transport_totamount = double.Parse(objMySqlDataReader["transport_totamount"].ToString());
                    values.transport_qualifiying_amount = double.Parse(objMySqlDataReader["transport_qualifiying_amount"].ToString());
                    values.balance_qualifiying_amount = double.Parse(objMySqlDataReader["balance_qualifiying_amount"].ToString());
                    values.entertainment_amount = double.Parse(objMySqlDataReader["entertainment_amount"].ToString());
                    values.taxonemployment_amount = double.Parse(objMySqlDataReader["taxonemployment_amount"].ToString());
                    values.aggreegate_qualifiying_amount = double.Parse(objMySqlDataReader["aggreegate_qualifiying_amount"].ToString());
                    values.incomechargableunder_headsal_deductible_amount = double.Parse(objMySqlDataReader["incomechargableunder_headsal_deductible_amount"].ToString());
                    values.otherincomeemployee_totamount1 = double.Parse(objMySqlDataReader["otherincomeemployee_totamount1"].ToString());
                    values.otherincomeemployee_totamount2 = double.Parse(objMySqlDataReader["otherincomeemployee_totamount2"].ToString());
                    values.otherincomeemployee_totamount3 = double.Parse(objMySqlDataReader["otherincomeemployee_totamount3"].ToString());
                    values.otherincome1_name = objMySqlDataReader["otherincome1_name"].ToString();
                    values.otherincome2_name = objMySqlDataReader["otherincome2_name"].ToString();
                    values.otherincome3_name = objMySqlDataReader["otherincome3_name"].ToString();
                    values.otherincomeemployee_qualifiying_amount3 = double.Parse(objMySqlDataReader["otherincomeemployee_qualifiying_amount3"].ToString());
                    values.overallgross_deductible_amount = double.Parse(objMySqlDataReader["overallgross_deductible_amount"].ToString());
                    values.lessallowence_name1 = objMySqlDataReader["lessallowence_name1"].ToString();
                    values.lessallowence_name2 = objMySqlDataReader["lessallowence_name2"].ToString();
                    values.lessallowence_name3 = objMySqlDataReader["lessallowence_name3"].ToString();
                    values.lessallowence_amount2 = double.Parse(objMySqlDataReader["lessallowence_amount2"].ToString());
                    values.lessallowence_amount3 = double.Parse(objMySqlDataReader["lessallowence_amount3"].ToString());
                }
                
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Employee Bank details!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Payroll/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

    }

}