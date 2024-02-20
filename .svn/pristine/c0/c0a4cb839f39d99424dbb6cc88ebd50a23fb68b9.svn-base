using ems.system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ems.payroll.Models
{
    public class MdlPayTrnSalaryPayment : result
    {
        public List<GetPaymentlist> paymentlist { get; set; }
        public List<payment_listdtl> payment_listdtl { get; set; }
        public List<GetMakePaymentlist> makepaymentlist { get; set; }
        public List<GetEmployeeBankdropdown> getemployeebankdtl { get; set; }
        public List<GetPayment> getpayment { get; set; }
        public List<GetPayment1> getpayment1 { get; set; }
        public List<payment_list> paymentllist { get; set; }
    }
    public class payment_listdtl : result
    {
        public string month { get; set; }
        public string year { get; set; }
        public string payment_type { get; set; }
        public string bank_name { get; set; }
        public string cheque_bank { get; set; }
        public string bank_branch { get; set; }
        public string account_no { get; set; }
        public string cheque_number { get; set; }

        public List<payment_list> payment_list { get; set; }
    }
        public class GetPaymentlist : result
    {
        public string payment_gid { get; set; }
        public string payment_month { get; set; }
        public string payment_year { get; set; }
        public string total_working_days { get; set; }
        public string no_of_employee { get; set; }
        public string total_salary { get; set; }
        public string paid_employee_count { get; set; }
        public string salary_disposed { get; set; }
        public string outstanding_amount { get; set; }
    }

    public class GetMakePaymentlist : result
    {
        public string employee_gid { get; set; }
        public string user_code { get; set; }
        public string employee_name { get; set; }
        public string branch_name { get; set; }
        public string department_name { get; set; }
        public string earned_net_salary { get; set; }
        public double outstanding_amount { get; set; }

    }
    public class GetEmployeeBankdropdown : result
    {
        public string bank_name { get; set; }
       
    }
    public class GetPayment : result
    {
        public string payment_gid { get; set; }
        public string payment_date { get; set; }
        public string paid_amount { get; set; }
        public string no_of_employees { get; set; }
        public string modeof_payment { get; set; }
        public string payment_type { get; set; }
        public string paid_by { get; set; }
       
       }
   
    public class GetPayment1 : result
    {
        public string payment_gid { get; set; }
        public string payment_date { get; set; }
        public string user_code { get; set; }
        public string employee_name { get; set; }
        public string branch_name { get; set; }
        public string department_name { get; set; }
        public string designation_name { get; set; }
        public string payment_amount { get; set; }
        public string paid_amount { get; set; }
        public string payment_type { get; set; }


    }
    public class payment_list : result
    {
        public string bank_gid { get; set; }
        public string msgetpayment_gid { get; set; }
        public string salary_gid { get; set; }
        public string paymentlimit_gid { get; set; }
        public string employee_gid { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string no_of_workeddays { get; set; }
        public string empbranch_gid { get; set; }
        public string payable_amount { get; set; }
        public double net_salary { get; set; }
        public string payment_date { get; set; }
        public string payment_type { get; set; }
        public string cheque_bank { get; set; }
        public string bank_branch { get; set; }
        public string paymentcount_gid { get; set; }
        public string account_no { get; set; }
        public string employee_bankname { get; set; }
        public string employee_accountno { get; set; }
        public string cheque_number { get; set; }
        public string company_check_number { get; set; }
        public string earned_net_salary { get; set; }




    }

}