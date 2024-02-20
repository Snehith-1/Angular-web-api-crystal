using ems.system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.payroll.Models
{
    public class MdlPayTrnSalaryManagement : result
    {
        public List<employeesalary_list> employeesalarylist { get; set; }
        public List<GetEmployeeSelect> GetEmployeeSelect { get; set; }
        public List<GetEmployeelist> GetEmployeelist { get; set; }
        public List<employeeleave_list> employeeleavelist { get; set; }
        public List<GetPayrunlist1> payrunlist { get; set; }
        public List<payrunviewlist> payrunviewlist { get; set; }
        public List<addsummary1> addsummary1 { get; set; }
     
    }
    public class addsummary1 : result
    {
        public string salary_gid { get; set; }
        public string earned_amount { get; set; }
        public string salarycomponent_name { get; set; }
    }
    public class Getmonthlypayrun : result
    {
        public string month { get; set; }
        public string year { get; set; }
        public List<employeeleave_list> employeeleave_list { get; set; }

    }

    public class payrunviewlist : result
    {                   
        public string salary_gid { get; set; }
        public string employee_gid { get; set; }
        public string user_code { get; set; }
        public string employee_name { get; set; }
        public string branch_gid { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string branch_name { get; set; }
        public string department { get; set; }
        public string leave_taken { get; set; }
        public string lop { get; set; }
        public string department_gid { get; set; }
        public string leave_wage { get; set; }
        public string ot_hours { get; set; }
        public string ot_rate { get; set; }
        public string designation_name { get; set; }
        public string user_gid { get; set; }
        public string earned_basic_salary { get; set; }
        public string basic_salary { get; set; }
        public string gross_salary { get; set; }
        public string earned_gross_salary { get; set; }
        public string public_holidays { get; set; }
        public string permission_wage { get; set; }
        public string net_salary { get; set; }
        public string earned_net_salary { get; set; }
        public string actual_month_workingdays { get; set; }
        public string month_workingdays { get; set; }








    }
    public class GetEmployeelist : result
    {
        public List<detailsdtl_list> detailsdtl_list { get; set; }
        public string month { get; set; }
        public string year { get; set; }
    }
    public class detailsdtl_list : result
    { 
        public string employee_gid { get; set; }

    }
    public class GetEmployeeSelect : result
    {
        public string employee_gid { get; set; }
        public string user_code { get; set; }
        public string employee_name { get; set; }
        public string designation_name { get; set; }
        public string branch_name { get; set; }
        public string joiningmonth_number { get; set; }
        public string department_gid { get; set; }
        public string branch_gid { get; set; }
        public string department_name { get; set; }


    }

    public class employeesalary_list : result
    {
        public string salary_gid { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string Workingdays { get; set; }
        public string generated_by { get; set; }
        public string totalemployee { get; set; }
        public string net_salary { get; set; }
        public string earned_net_salary { get; set; }
       
      }

    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class employeeleave_list : result
    {
        public string totaldays { get; set; }

        public string weekoff_days { get; set; }
        public string salary_days { get; set; }
        public string absent { get; set; }
        public string adjusted_lop { get; set; }
        public string employee_gid { get; set; }
        public string user_code { get; set; }
        public string username { get; set; }
        public string leavecount { get; set; }
        public string month_workingdays { get; set; }
        public string lop { get; set; }
        public string actual_lop { get; set; }
        public string user_gid { get; set; }
        public string holidaycount { get; set; }
        public string actualworkingdays { get; set; }
        public string weekoffcount { get; set; }


    }
    public class GetPayrunlist1 : result
    {
        public string salary_gid { get; set; }
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string department { get; set; }
        public string employee_gid { get; set; }
        public string user_code { get; set; }
        public string employee_name { get; set; }
        public string leave_taken { get; set; }
        public string lop { get; set; }
        public string public_holidays { get; set; }
        public string basic_salary { get; set; }
        public string earned_basic_salary { get; set; }
        public string gross_salary { get; set; }
        public string earned_gross_salary { get; set; }
        public string net_salary { get; set; }
        public string earned_net_salary { get; set; }
        public string actual_month_workingdays { get; set; }
        public string month_workingdays { get; set; }

    }
}