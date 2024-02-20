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
using System.Web;
using System.Net.NetworkInformation;
using System.EnterpriseServices.CompensatingResourceManager;

namespace ems.finance.DataAccess
{
    public class DaAccTrnBankbooksummary
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, lsuser_code, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaGetBankBookSummary(MdlAccTrnBankbooksummary values)
        {
            msSQL = " select a.bank_gid,a.bank_name,a.bank_code,d.branch_name,a.account_no," +
                " a.ifsc_code,a.neft_code,a.swift_code,format(a.openning_balance,2) as openning_balance,a.account_gid" +
                " from acc_mst_tbank a left join hrm_mst_tbranch d on d.branch_gid=a.branch_gid " +
                " where 1=1  group by a.bank_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getbankbook_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getbankbook_list
                    {
                        bank_gid = dt["bank_gid"].ToString(),
                        bank_code = dt["bank_code"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        account_no = dt["account_no"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        neft_code = dt["neft_code"].ToString(),
                        swift_code = dt["swift_code"].ToString(),
                        openning_balance = dt["openning_balance"].ToString(),

                    });
                    values.Getbankbook_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetSubBankBook(MdlAccTrnBankbooksummary values,string bank_gid)
        {
            msSQL = "select openning_balance from acc_mst_tbank where bank_gid='" + bank_gid + "'";
            string openning_balance = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select account_gid from acc_mst_tbank where bank_gid='" + bank_gid + "'";
            string account_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select journaldtl_gid,transaction_date,account_name as account_desc,remarks,format(ifnull(credit_amount,0.00),2) as credit_amount," +
             " format(ifnull(debit_amount,0.00),2) as debit_amount,account_gid,reference_gid,bank_name,account_no,journal_gid,journal_refno,closing_amount from" +
             " (select journaldtl_gid,transaction_date,account_name,remarks,credit_amount,debit_amount," +
             " account_gid, reference_gid, bank_name, account_no, journal_gid,journal_refno, " +
             " format(" + (openning_balance) + "+(@runtot := (ifnull(credit_amount,0.00)-ifnull(debit_amount,0.00) + @runtot)),2) as closing_amount  from " +
             " (select a.journaldtl_gid,b.transaction_date,e.account_name,a.remarks,a.account_gid,b.reference_gid,c.bank_name," +
             " c.account_no,a.journal_gid,b.journal_refno, " +
             " case when b.transaction_type  not like '%Opening%'  and a.journal_type='cr' then a.transaction_amount end  as credit_amount, " +
             " case when b.transaction_type  not like '%Opening%'  and a.journal_type='dr' then a.transaction_amount end  as debit_amount " +
             " from acc_trn_journalentrydtl a" +
             " left join acc_trn_journalentry b on a.journal_gid=b.journal_gid" +
             " left join acc_mst_tbank c on c.bank_gid=b.reference_gid" +
             " left join acc_mst_tchartofaccount e on e.account_gid=a.account_gid" +
             " ,(SELECT @runtot:=0) d" +
             " where  a.transaction_gid='" + account_gid + "'" +
             " order by b.transaction_date asc,a.journaldtl_gid asc) x) y" +
             " order by date(y.transaction_date) desc, date(y.transaction_date) asc,y.journaldtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getsubbankbook_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getsubbankbook_list
                    {
                        journal_gid = dt["journal_gid"].ToString(),
                        transaction_date = dt["transaction_date"].ToString(),
                        journal_refno = dt["journal_refno"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        account_no = dt["account_no"].ToString(),
                        account_desc = dt["account_desc"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        credit_amount = dt["credit_amount"].ToString(),
                        debit_amount = dt["debit_amount"].ToString(),
                        closing_amount = dt["closing_amount"].ToString(),
                    });
                    values.Getsubbankbook_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetBankBookAddSummary(string bank_gid,MdlAccTrnBankbooksummary values)
        {
            msSQL = " select a.bank_code,a.bank_name,a.bank_gid, a.gl_code, a.account_no, a.account_type," +
                    " a.ifsc_code, a.neft_code, a.swift_code, a.account_gid " +
                    " from acc_mst_tbank a " +
                    " where a.bank_gid = '" + bank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            
            var getModuleList = new List<Getbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getbank_list
                    {
                        account_gid = dt["account_gid"].ToString(),
                        bank_gid = dt["bank_gid"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        account_no = dt["account_no"].ToString(),
                        account_type = dt["account_type"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        neft_code = dt["neft_code"].ToString(),
                        swift_code = dt["swift_code"].ToString(),
                        gl_code = dt["gl_code"].ToString(),

                    }) ;
                    values.addbank_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetAccTrnGroupDtl( MdlAccTrnBankbooksummary values)
        {
            msSQL = " select  a.accountgroup_gid, a.accountgroup_name " +
                    " from acc_mst_tchartofaccount a ";
                     
                   

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetAccountGroupDropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetAccountGroupDropdown
                    {
                        accountgroup_gid = dt["accountgroup_gid"].ToString(),
                        accountgroup_name = dt["accountgroup_name"].ToString(),
                    });
                    values.GetAccTrnGroupDtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetAccTrnNameDtl(MdlAccTrnBankbooksummary values)
        {
            msSQL = " select  a.account_gid, a.account_name " +
                    " from acc_mst_tchartofaccount a ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetAccountNameDropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetAccountNameDropdown
                    {
                        account_gid = dt["account_gid"].ToString(),
                        account_name = dt["account_name"].ToString(),
                    });
                    values.GetAccTrnNameDtl = getModuleList;
                }
            }
                dt_datatable.Dispose();
            }

        public void DaPostProductGroupSummary(string user_gid, accountfetch_list values)
        {


            msGetGid = objcmnfunctions.GetMasterGID("FPCT");
            msSQL = " insert into acc_ses_journalentry (" +
                    " session_id," +
                    " account_gid," +
                    " account_desc," +
                    " dr_cr," +
                    " transaction_amount," +
                    " journal_desc," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    " '" + values.accountgroup_name + "'," +
                    " '" + values.account_name + "'," +
                    " '" + values.dr_cr + "'," +
                    " '" + values.transaction_amount + "'," +
                    " '" + values.journal_desc + "'," +
                    " '" + user_gid + "'," +
                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Account Group Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Account Group";
            }

        }

    }
}

