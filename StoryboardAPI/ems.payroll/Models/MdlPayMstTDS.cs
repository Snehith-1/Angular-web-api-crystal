using Org.BouncyCastle.Asn1.X9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.payroll.Models
{
    public class MdlPayMstTDS:result
    {
        public List<Personal_details> personalDetails { get; set; }
    }
    public class Personal_details:result
    {
        public string employee_gid { get; set; }
        public string assessment_gid { get; set; }
        public string employee_firstname { get; set; }
        public string employee_lastname { get; set; }
        public DateTime employee_DOB { get; set; }
        public string bloodgroup { get; set; }
        public string postal_code { get; set; }
        public string employee_gender { get; set; }
        public string employee_phone { get; set; }
        public string employee_addressline1 { get; set; }
        public string employee_addressline2 { get; set; }
        public string employee_city { get; set; }
        public string employee_state { get; set; }
        public string country { get;set; }
        public string country_gid { get; set; }
        public string pan_number { get; set; }
        public string uan_number { get; set; }
        public string email_address { get; set; }
    }
    public class quarters:result
    {
        public string employee_gid { get; set; }
        public string assessment_gid { get; set; }
        public string tdsquarter1_receiptno { get; set;}
        public string tdsquarter2_receiptno { get;set;}
        public string tdsquarter3_receiptno { get;set;}
        public string tdsquarter4_receiptno { get;set;}
        public string tdsquarter1_amount_deducted { get; set;}  
        public string tdsquarter2_amount_deducted { get; set;}
        public string tdsquarter3_amount_deducted { get; set;}
        public string tdsquarter4_amount_deducted { get; set;}
        public string tdsquarter1_amount_deposited { get; set;}
        public string tdsquarter2_amount_deposited { get; set;}
        public string tdsquarter3_amount_deposited { get; set;}
        public string tdsquarter4_amount_deposited { get; set;}
        public string tdsquarter1_paidcredited { get; set;} 
        public string tdsquarter2_paidcredited { get; set;}
        public string tdsquarter3_paidcredited { get; set;}
        public string tdsquarter4_paidcredited { get; set;}
        public double tdsquarter_totalamount_deducted { get; set;}
        public double tdsquarter_totalamount_deposited { get; set;}
        public double tdsquarter_totalamount_paidcredited { get; set;}
    }
    public class Income:result
    {
        public string employee_gid { get; set; }
        public string assessment_gid { get; set; }
        public double grosssalary_amount { get; set;}
        public double perquisites_amount { get; set; }
        public double profitinlieu_amount { get; set;}
        public double grosstotal_qualifiying_amount { get; set;}    
        public double transport_totamount { get; set;}
        public double transport_qualifiying_amount { get; set;} 
        public double balance_qualifiying_amount { get; set; }
        public double entertainment_amount { get; set;} 
        public double taxonemployment_amount { get; set;}
        public double aggreegate_qualifiying_amount { get; set;}
        public double incomechargableunder_headsal_deductible_amount { get; set;} 
        public double otherincomeemployee_totamount1 { get; set;}
        public double otherincomeemployee_totamount2 {  get; set;}
        public double otherincomeemployee_totamount3 { get; set;}
        public string otherincome1_name { get; set;}
        public string otherincome2_name { get;set;}
        public string otherincome3_name { get;set; }
        public double otherincomeemployee_qualifiying_amount3 { get; set;}
        public double overallgross_deductible_amount { get; set;}  
        public string lessallowence_name1 { get; set;}
        public string lessallowence_name2 { get; set;}
        public string lessallowence_name3 { get; set;}
        public double lessallowence_amount2 { get; set;}
        public double lessallowence_amount3 { get; set; }

    }
    public class tdsformlist
    {
        public string employee_gid { get; set; }
        public string assessment_gid { get; set; }
        public double section80c_grossamount1 { get; set;}
        public double section80c_grossamount2 { get; set;}
        public double section80c_grossamount3 { get; set;}
        public double section80c_grossamount4 { get; set;}
        public double section80c_grossamount5 { get; set;}
        public double section80c_grossamount6 { get; set;}
        public double section80c_grossamount7 { get; set;}
        public double section80c_deductible_amount7 { get; set;}    
        public double section80ccc_grossamount { get; set;}
        public double section80ccc_deductible_amount { get;set;}
        public double section80ccd_grossamount { get; set; }
        public string section80c_name1 { get; set;}
        public string section80c_name2 { get;set;}
        public string section80c_name3 { get; set;}
        public string section80c_name4 { get; set;}
        public string section80c_name5 { get; set;}
        public string section80c_name6 {  get; set;}
        public string section80c_name7 { get; set;}
        public double section1_grossamount { get; set; }
        public double section1_qualifiying_amount { get; set; }
        public double section1_deductible_amount { get; set;}
        public double section2_grossamount { get; set; }
        public double section2_qualifiying_amount { get; set; }
        public double section2_deductible_amount { get; set; }
        public double section3_grossamount { get; set; }
        public double section3_qualifiying_amount { get; set; }
        public double section3_deductible_amount { get; set; }
        public double section4_grossamount { get; set; }
        public double section4_qualifiying_amount { get; set; }
        public double section4_deductible_amount { get; set; }
        public double section5_grossamount { get; set; }
        public double section5_qualifiying_amount { get; set; }
        public double section5_deductible_amount { get; set; }
        public double aggregatedeductable_totalamount { get; set; }
        public double total_income { get;set; }
        public double tax_total_income { get;set; }
        public double educationcess_amount {  get; set; }
        public double tax_payable12plus13 { get; set; }
        public double less_relief89 { get; set; }
        public double tax_payable14minus15 { get; set; }
        public double less_tds { get; set; }
        public double balance_tax { get; set; }
        public double section80c_amount7 { get; set; }
        public  double aggregate_gross { get; set; }
        public  double aggregate_deduct { get; set; }
        public double section80ccd1b_grossamount { get; set; }
        public double section80ccd1b_deductamount { get; set; }
        public string section1_name { get; set; }
        public string section2_name { get; set; }
        public string section3_name { get; set; }
        public string section4_name { get; set; }
        public string section5_name { get; set; }
    }
}