using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.finance.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
//using System.Web;
//using OfficeOpenXml;
using System.Configuration;
using System.IO;
//using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
using System.Text;

namespace ems.finance.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class DaAccTrnCashBookSummary
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msGetGid;
        int mnResult, mnResult1;
    public void DaGetAccTrnCashbooksummary(MdlAccTrnCashBookSummary values)

         {
            
            msSQL = " select a.branch_gid,a.branch_name,a.branch_code,a.gl_code, " +
                    " format(a.openning_balance,2) as openning_balance,a.account_gid " +
                    " from hrm_mst_tbranch a " +
                    " left join acc_trn_journalentry b on a.branch_gid=b.branch_gid " +
                    " left join acc_trn_journalentrydtl c on c.journal_gid=b.journal_gid " +
                    " where 1=1 group by a.branch_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<CashBook_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {   
                    getModuleList.Add(new CashBook_list
                    {

                        branch_code = dt["branch_code"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        gl_code = dt["gl_code"].ToString(),
                        openning_balance = dt["openning_balance"].ToString(),
                        branch_gid = dt["branch_gid"].ToString(),




                    });
                    values.CashBook_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetAccTrnCashbookSelect(MdlAccTrnCashBookSummary values, string branch_gid)
        {
            msSQL = "select account_gid from hrm_mst_tbranch where branch_gid='" + branch_gid + "' ";
            string account_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select ifnull(openning_balance,0.00) as openning_balance from hrm_mst_tbranch where branch_gid='" + branch_gid + "' ";
            string openning_balance = objdbconn.GetExecuteScalar(msSQL);



            msSQL = " select journaldtl_gid,transaction_date,account_name as account_desc,remarks,format(credit_amount,2) as credit_amount," +
                " format(debit_amount,2) as debit_amount,account_gid,reference_gid,branch_name,journal_gid,journal_refno,closing_amount from" +
                " (select journaldtl_gid,transaction_date,account_name,remarks,credit_amount,debit_amount," +
                " account_gid, reference_gid, branch_name,journal_gid,journal_refno, " +
                " format(" + (openning_balance) + " +(@runtot := (credit_amount-debit_amount + @runtot)),2) as closing_amount  from" +
                " (select a.journaldtl_gid,b.transaction_date,e.account_name,a.remarks,a.account_gid,b.reference_gid,c.branch_name," +
                " a.journal_gid,b.journal_refno, ifnull(case when b.transaction_type  not like '%Opening%'  and a.journal_type='cr' then a.transaction_amount end,0.00) as credit_amount, " +
                " ifnull(case when b.transaction_type  not like '%Opening%'  and a.journal_type='dr' then a.transaction_amount end,0.00) as debit_amount" +
                " from acc_trn_journalentrydtl a" +
                " left join acc_trn_journalentry b on a.journal_gid=b.journal_gid" +
                " left join hrm_mst_tbranch c on c.branch_gid=b.branch_gid" +
                " left join acc_mst_tchartofaccount e on e.account_gid=a.account_gid" +
                " ,(SELECT @runtot:=0) d" +
                " where a.account_gid <> '" + account_gid + "'  " +
                "  and c.branch_gid='" + branch_gid + "' " +
                " order by b.transaction_date asc,a.journaldtl_gid asc) x) y" +
                " order by date(y.transaction_date) desc, date(y.transaction_date) asc,y.journaldtl_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<CashBookSelect_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new CashBookSelect_list
                    {

                        transaction_date = dt["transaction_date"].ToString(),
                        journal_refno = dt["journal_refno"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        account_desc = dt["account_desc"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        credit_amount = dt["credit_amount"].ToString(),
                        debit_amount = dt["debit_amount"].ToString(),
                        closing_amount = dt["closing_amount"].ToString(),
                      




                    });
                    values.CashBookSelect_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
    }
}