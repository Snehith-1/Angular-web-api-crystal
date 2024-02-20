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
using Org.BouncyCastle.Asn1.Ocsp;

namespace ems.payroll.DataAccess
{
    public class DaLoan
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string msGetloangid;
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        int mnResult;

        public void DaGetLoanSummary(MdlPayTrnLoanSummary values)
        {
            msSQL = " select a.loan_gid,a.loan_refno,a.created_date,a.employee_gid,date_format(a.loan_repayment_startfrom,'%Y-%m-%d') as loan_repayment_startfrom,loan_status, concat(b.user_firstname,' ',b.user_lastname) as created_by, concat(c.user_firstname,' ',c.user_lastname,'-',e.branch_name) as employee_name, format((a.loan_amount),2) as loanamount,a.loan_duration" +
                " from pay_trn_tloan a " +
                 " inner join hrm_mst_temployee d on d.employee_gid=a.employee_gid " +
                "  inner join adm_mst_tuser c on c.user_gid=d.user_gid " +
                 " inner join hrm_mst_tbranch e on e.branch_gid=d.branch_gid " +
                " inner join adm_mst_tuser b on b.user_gid=a.created_by " +
                " order by a.loan_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<loan_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new loan_list
                    {
                        loan_gid = dt["loan_gid"].ToString(),
                        loan_refno = dt["loan_refno"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        loanamount = double.Parse(dt["loanamount"].ToString()),
                        loan_duration = dt["loan_duration"].ToString(),
                        loan_repayment_startfrom = dt["loan_repayment_startfrom"].ToString(),
                        created_by = dt["created_by"].ToString()

                    });
                    values.loanlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetEmployeeDtl(MdlPayTrnLoanSummary values)
        {
            msSQL = "select a.employee_gid,concat(b.user_code,'/',b.user_firstname,' ',b.user_lastname) as employee_name from hrm_mst_temployee a " +
                    " inner join adm_mst_tuser b on b.user_gid=a.user_gid" +
                    " inner join hrm_mst_tbranch c on c.branch_gid=a.branch_gid";
           
                dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetEmployeedropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetEmployeedropdown
                    {
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    values.GetEmployeeDtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetBankDetail(MdlPayTrnLoanSummary values)
        {
            msSQL = "select bank_name From acc_mst_tallbank";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetBankNamedropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetBankNamedropdown
                    {

                        bank_name = dt["bank_name"].ToString(),
                    });
                    values.GetBankNameDtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostLoan(string user_gid, loan_list values)
        {



            string uiDateStr = values.repaymentstartdate;
            DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string mysqlrepaymetDate = uiDate.ToString("yyyy-MM-dd");

            msGetloangid = objcmnfunctions.GetMasterGID("LOAN");

            msSQL = "insert into pay_trn_tloan" +
                     " (loan_gid, " +
                     " employee_gid, " +
                     " loan_amount, " +
                     " loan_duration, " +
                     " loan_repayment_startfrom," +
                     " loan_remarks, " +
                     " created_by, " +
                     " created_date, " +
                     " type, " +
                      " loan_id, " +
                      " grade_name, " +
                     " payment_mode, " +
                     " cheque_no, " +
                     " bank_name, " +
                     " branch_name, " +
                     " transactionref_no, " +
                     " payment_date, " +
                     " deposit_bank, " +
                     " bank_gid, " +
                     " loan_refno) " +
                     " values( " +
                     "'" + msGetloangid + "', " +
                      "'" + values.employee + "', " +
                      "'" + values.loan_amount + "', " +
                       "'" + values.durationperiod + "'," +
                       "'" + mysqlrepaymetDate + "', " +
                       "'" + values.remarks + "'," +
                       "'" + user_gid + "', " +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                       "'" + values.type + "', " +
                        "'" + msGetloangid + "', " +
                      " 'Loan', " +
                       " '" + values.payment_mode + "', " +
                       "'" + values.cheque_no + "', " +
                       "'" + values.bank_name + "'," +
                       "'" + values.branch_name + "'," +
                       "'" + values.transaction_refno + "'," +
                       "'" + values.payment_date.ToString("yyyy-MM-dd") + "'," +
                       "'" + values.bank + "'," +
                        "'" + values.bankgid + "', " +
                       "'" + values.loan_refno + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            DateTime dt = Convert.ToDateTime(mysqlrepaymetDate);
            DateTime dt1 = dt;

            for (int i = 0; i < values.durationperiod; i++)
            {

                double lsrepayamt = values.loan_amount / values.durationperiod;
                string msGetloanrepaygid = objcmnfunctions.GetMasterGID("LOANREPAY");

                msSQL = "insert into pay_trn_tloanrepayment " +
                    "(repayment_gid, " + 
                    "loan_gid, " +
                    " repayment_duration, " +
                    "repayment_amount, " + 
                    "created_by, " +
                    "type, " +
                    "created_date) " + 
                    "values( " + 
                    "'" + msGetloanrepaygid + "', " +
                    "'" + msGetloangid + "', " + 
                    "'" + dt1.ToString("yyyy-MM-dd") + "', " +
                    "'" + lsrepayamt + "', " +
                    "'" + user_gid + "', " + 
                    "'" + values.payment_mode + "', " +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "') ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                dt1 = dt1.AddMonths(1);

            }


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Loan Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Loan";
            }

        }

        public void DagetUpdatedLoan(string user_gid, loan_list values)
        {

            msSQL = " update pay_trn_tloan set" +
                " repayment_amount='" + values.repay_amtedit + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                " updated_by='" + user_gid + "'" +
                " where loan_gid='" + values.loan_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Loan Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Loan";
            }
        }


        public void DagetEditLoan(string loan_gid,MdlPayTrnLoanSummary values)
        {
            msSQL = " select a.loan_gid,a.repayment_amount,concat(e.user_firstname,' ',e.user_lastname,'----', d.branch_name) as employee_name,  " +
                    " a.loan_amount,date_format(a.created_date,'%d-%m-%Y') as createddate, " +
                    " a.loan_refno,a.created_by,concat(f.user_firstname,' ',f.user_lastname) as createdby, " +
                    " c.employee_gid,a.loan_remarks,paid_amount,balance_amount from pay_trn_tloan a  " +
                    " left join hrm_mst_temployee c on c.employee_gid=a.employee_gid  " +
                    " left join adm_mst_tuser e on e.user_gid=c.user_gid  " +
                    " left join adm_mst_tuser f on f.user_gid=a.created_by  " +
                    " left join hrm_mst_tbranch d on d.branch_gid=c.branch_gid " +
                    " where a.loan_gid= '" + loan_gid + " '";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<loanedit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new loanedit_list
                    {
                        loan_gid = dt["loan_gid"].ToString(),
                        loan_refnoedit = dt["loan_refno"].ToString(),
                        employee_nameedit = dt["employee_name"].ToString(),
                        loan_dateedit = dt["createddate"].ToString(),
                        loan_amountedit = dt["loan_amount"].ToString(),
                        paid_amountedit = dt["paid_amount"].ToString(),
                        balance_amtedit = dt["balance_amount"].ToString(),
                        remarksedit = dt["loan_remarks"].ToString(),
                        repay_amtedit = dt["repayment_amount"].ToString(),
                        created_by = dt["created_by"].ToString()

                    });
                    values.getEditLoan = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DagetViewLoanSummary(string loan_gid, MdlPayTrnLoanSummary values)
        {

            msSQL = " select a.loan_gid,concat(e.user_firstname,' ',e.user_lastname,'-', d.branch_name) as employee_name,a.loan_amount,date_format(a.created_date,'%d-%m-%Y') as createddate, " +
                    " a.loan_duration,date_format(a.loan_repayment_startfrom,'%d-%m-%Y') as loanrepayment," +
                    " a.loan_refno,a.created_by,concat(f.user_firstname,' ',f.user_lastname) as createdby,c.employee_gid,a.loan_remarks,cast(concat(a.grade_name,' ','-',' ',a.net_salary ) as char) as grade from pay_trn_tloan a" +
                    " left join hrm_mst_temployee c on c.employee_gid=a.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid=c.user_gid" +
                    " left join adm_mst_tuser f on f.user_gid=a.created_by" +
                    " left join hrm_mst_tbranch d on d.branch_gid=c.branch_gid" +
                    " where a.loan_gid= '" + loan_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getViewLoanSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getViewLoanSummary
                    {


                        loan_refnoedit = dt["loan_refno"].ToString(),
                        employee_nameedit = dt["employee_name"].ToString(),
                        loan_dateedit = dt["createddate"].ToString(),
                        loan_amountedit = dt["loan_amount"].ToString(),
                        loan_duration = dt["loan_duration"].ToString(),
                        loanrepayment = dt["loanrepayment"].ToString(),
                        remarksedit = dt["loan_remarks"].ToString(),
                        loan_gid = dt["loan_gid"].ToString(),
                        created_by = dt["created_by"].ToString(),
                       

                    });
                    values.getViewLoanSummary = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DagetLoanrepaySummary(string loan_gid, MdlPayTrnLoanSummary values)
        {

            msSQL = " select a.loan_gid,b.repayment_gid,date_format(b.repayment_duration,concat('%M','-','%Y')) as month,format(ifnull(b.repayment_amount,'0.00'),2) as repaymentamount ,format(ifnull(b.repaid_amount,'0.00'),2) as repaidamount , " +
                " cast(ifnull(b.actual_date,'Not Paid') as char) as actual_date from pay_trn_tloan a" +
                " left join pay_trn_tloanrepayment b on a.loan_gid=b.loan_gid" +
                " where a.loan_gid='" + loan_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getRepayViewLoanSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getRepayViewLoanSummary
                    {


                        month = dt["month"].ToString(),
                        repaymentamount = dt["repaymentamount"].ToString(),
                        repaidamount = dt["repaidamount"].ToString(),
                        actual_date = dt["actual_date"].ToString(),
                       


                    });
                    values.getRepayViewLoanSummary = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }



    }



}