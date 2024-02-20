using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace ems.payroll.Models
{
    public class MdlPayMstAssessment : result
    {
        public List<assessmentsummary_list> assessmentsummary_list { get; set; }
        public List<assignempsummary_list> assignempsummary_list { get; set; }
        public List<generateformsummary_list> generateformsummary_list { get; set; }
        public List<postassignemployeelist> postassignemployeelist { get; set; }
       
        public List<Getfinyeardropdown> Getfinyeardropdown { get; set; }

        public List<incometax_lists> incometax_lists { get; set; }

        public List<incometaxsummary_lists> incometaxsummary_lists { get; set; }
        public List<MdlPayIncomedata> MdlPayIncomedata { get; set; }
    }
    public class assessmentsummary_list : result
    {
        public string assessment_gid { get; set; }
        public string duration { get; set; }        
        public string fin_duration { get; set; }
    }
    public class assignempsummary_list : result
    {
        public string employee_gid { get; set; }
        public string branch_name { get; set; }
        public string user_code { get; set; }
        public string employee_name { get; set; }
        public string department_name { get; set; }
        public string designation_name { get; set; }
    }
    public class postassignemployeelist : result
    {
        public string assessment_gid { get; set; }
        public List<assignempsummary_list> assignempsummary_list { get; set; }
    }
    public class generateformsummary_list : result
    {
        public string branch_name { get; set; }
        public string user_code { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string assessment_gid { get; set; }
        public string department_name { get; set; }
        public string designation_name { get; set; }
    }
    public class MdlPersonalData : result
    {
        public string address_gid { get; set; }
        public string user_firstname { get; set; }
        public string user_lastname { get; set; }
        public string employee_dob { get; set; }
        public string employee_emailid { get; set; }
        public string employee_gender { get; set; }
        public string pan_no { get; set; }
        public string uan_no { get; set; }
        public string bloodgroup { get; set; }
        public string employee_mobileno { get; set; }        
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country_gid { get; set; }
    }
    public class updatepersonalinfolist : result
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime dob { get; set; }
        public string email_id { get; set; }
        public string active_flag { get; set; }
        public string pan_number { get; set; }
        public string uan_number { get; set; }
        public string blood_group { get; set; }
        public string phone { get; set; }
        public string country { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string state { get; set; }
    }
    public class Getfinyeardropdown : result
    {
        public string finyear_gid { get; set; }
        public string finyear_range { get; set; }
    }
    public class incometax_lists : result
    {
        public string finyear_range { get; set; }
        public string documenttype_gid { get; set; }
        public string document_title { get; set; }
        public DateTime remarks { get; set; }
        public string user_gid { get; set; }

    }
    public class incometaxsummary_lists : result
    {
        public string taxdocument_gid { get; set; }
        public string fin_year { get; set; }
        public string documenttype_gid { get; set; }
        public string document_title { get; set; }
        public DateTime created_date { get; set; }
        public string user_gid { get; set; }
    }
    public class postquartersinfolist : result
    {
        public string q1_rpt_original_statement { get; set; }
        public string assessment_gid { get; set; }
        public string employee_gid { get; set; }
        public string q1_amt_paid_credited { get; set; }
        public string q1_amt_tax_deducted { get; set; }
        public string q1_amt_tax_deposited { get; set; }
        public string q2_rpt_original_statement { get; set; }
        public string q2_amt_paid_credited { get; set; }
        public string q2_amt_tax_deducted { get; set; }
        public string q2_amt_tax_deposited { get; set; }
        public string q3_rpt_original_statement { get; set; }
        public string q3_amt_paid_credited { get; set; }
        public string q3_amt_tax_deducted { get; set; }
        public string q3_amt_tax_deposited { get; set; }
        public string q4_rpt_original_statement { get; set; }
        public string q4_amt_paid_credited { get; set; }
        public string q4_amt_tax_deducted { get; set; }
        public string q4_amt_tax_deposited { get; set; }
        public string total_amt_paid_credited { get; set; }
        public string total_amt_tax_deducted { get; set; }
        public string total_amt_tax_deposited { get; set; }
    }
    public class MdlQuartersData : result
    {
        public string tdsquarter1_receiptno { get; set; }
        public string tdsquarter1_paidcredited { get; set; }
        public string tdsquarter1_amount_deposited { get; set; }
        public string tdsquarter1_amount_deducted { get; set; }
        public string tdsquarter2_receiptno { get; set; }
        public string tdsquarter2_paidcredited { get; set; }
        public string tdsquarter2_amount_deposited { get; set; }
        public string tdsquarter2_amount_deducted { get; set; }
        public string tdsquarter3_receiptno { get; set; }
        public string tdsquarter3_paidcredited { get; set; }
        public string tdsquarter3_amount_deposited { get; set; }
        public string tdsquarter3_amount_deducted { get; set; }
        public string tdsquarter4_receiptno { get; set; }
        public string tdsquarter4_paidcredited { get; set; }
        public string tdsquarter4_amount_deposited { get; set; }
        public string tdsquarter4_amount_deducted { get; set; }
        public string totalamount_paidcredited { get; set; }
        public string tdsquarter_totalamount_deposited { get; set; }
        public string tdsquarter_totalamount_deducted { get; set; }
    }

    public class MdlPayIncomedata : result
    {
        public string grosssalary_amount { get; set; }
        public string perquisites_amount { get; set; }
        public string profitof_salary { get; set; }
        public string totalamount { get; set; }
        public string component1 { get; set; }
        public string pfamount1 { get; set; }
        public string component2 { get; set; }
        public string pfamount2 { get; set; }
        public string component3 { get; set; }
        public string balanceamount { get; set; }
        public string taxon_emp { get; set; }
        public string aggreagateof_ab { get; set; }
        public string incomecharge_headsalaries { get; set; }
        public string employee_income1 { get; set; }
        public string employeeincome_rs1 { get; set; }
        public string employee_income2 { get; set; }
        public string employeeincome_rs2 { get; set; }
        public string employee_income3 { get; set; }
        public string employeeincome_rs3 { get; set; }
        public string employeeincome_total { get; set; }
        public string grosstotal_income { get; set; }
        public string assessment_gid { get; set; }
        public string employee_gid { get; set; }
        public string lessallowancetotal { get; set; }
        public string aggreagateofab { get; set; }



    }
}