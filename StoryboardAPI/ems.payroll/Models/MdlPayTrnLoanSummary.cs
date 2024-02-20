using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.payroll.Models
{
    public class MdlPayTrnLoanSummary
    {
        public List<loan_list> loanlist { get; set; }
        public List<GetEmployeedropdown> GetEmployeeDtl { get; set; }

        public List<GetBankNamedropdown> GetBankNameDtl { get; set; }

        public List<loanedit_list> getEditLoan { get; set; }
        public List<getViewLoanSummary> getViewLoanSummary { get; set; }
        public List<getRepayViewLoanSummary> getRepayViewLoanSummary { get; set; }
    }

    public class loan_list : result
    {
        public string employee { get; set; }
        public string loan_name { get; set; }
        public string loan_gid { get; set; }
        public string loan_refno { get; set; }
        public string created_date { get; set; }
        public string loan_duration { get; set; }
        public string loan_repayment_startfrom { get; set; }
        public string employee_name { get; set; }
        public string repaymentstartdate { get; set; }
        public string payment_mode { get; set; }
        public double loanamount { get; set; }
        public string paid_amount { get; set; }
        public string balance_amount { get; set; }
        public string created_by { get; set; }
        public string loan_advance { get; set; }

        public double loan_amount { get; set; }
        public double durationperiod { get; set; }
        public string paid_amt { get; set; }
        public string pend_amt { get; set; }
        public string repay_amt { get; set; }
        public string remarks { get; set; }

        public string type { get; set; }
        public string loan_id { get; set; }
        public string cheque_no { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string transaction_refno { get; set; }
        public DateTime payment_date { get; set; }
        public string bank { get; set; }
        public string bankgid { get; set; }

        public string loan_refnoedit { get; set; }
        public string employee_nameedit { get; set; }
        public string loan_dateedit { get; set; }
        public string loan_amountedit { get; set; }
        public string paid_amountedit { get; set; }
        public string balance_amtedit { get; set; }
        public string repay_amtedit { get; set; }
        public string remarksedit { get; set; }
       





    }

    public class GetEmployeedropdown : result
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }


    }

    public class GetBankNamedropdown : result
    {
        public string bank_name { get; set; }
    }

    public class loanedit_list : result
    {
        public string loan_gid { get; set; }
        public string loan_refnoedit { get; set; }
        public string employee_nameedit { get; set; }
        public string loan_dateedit { get; set; }
        public string loan_amountedit { get; set; }
        public string paid_amountedit { get; set; }
        public string balance_amtedit { get; set; }
        public string remarksedit { get; set; }
        public string repay_amtedit { get; set; }

        public string created_by { get; set; }
    }

    public class getViewLoanSummary : result
    {
        public string loan_refnoedit { get; set; }
        public string employee_nameedit { get; set; }
        public string loan_dateedit { get; set; }
        public string loan_amountedit { get; set; }
        public string paid_amountedit { get; set; }
        public string balance_amtedit { get; set; }
        public string loan_duration { get; set; }
        public string loanrepayment { get; set; }
        public string created_by { get; set; }
        public string loan_gid { get; set; }
        public string remarksedit { get; set; }

    }
    public class getRepayViewLoanSummary:result
    {
        public string month { get; set; }
        public string repaymentamount { get; set; }
        public string repaidamount { get; set; }
        public string actual_date { get; set; }


    }

}