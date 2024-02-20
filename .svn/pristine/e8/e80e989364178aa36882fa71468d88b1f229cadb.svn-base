using ems.payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.payroll.Models
{
    public class MdlPayMstEmployeesalarytemplate : result
    {
        public List<salarygrade_list> salarygrade_list { get; set; }
        public List<AddtionalComponent> AddtionalComponent { get; set; }
        public List<Getgradeassign_list> Getgradeassign_list { get; set; }
        public List<template_list> template_list { get; set; }
        public List<summaryaddition_list> summaryaddition_list { get; set; }
        public List<summarydeduction_list> summarydeduction_list { get; set; }
        public List<SalaryGradetoemployee> SalaryGradetoemployee { get; set; }
        public List<Editsummaryaddition_list> Editsummaryaddition_list { get; set; }
        public List<Editsummarydeduction_list> Editsummarydeduction_list { get; set; }
        public List<edittemplate_list> edittemplate_list { get; set; }
        public List<updateSalaryGradetoemployee> updateSalaryGradetoemployee { get; set; }
    }

    public class updateSalaryGradetoemployee : result
    {
        public List<Editsummaryaddition_list> Editsummaryaddition_list { get; set; }
        public List<Editsummarydeduction_list> Editsummarydeduction_list { get; set; }

        public string NewBasicSalary { get; set; }
        public string gross_salary { get; set; }
        public string ctc { get; set; }
        public string net_salary { get; set; }
        public string employee2salarygradetemplate_gid { get; set; }
        public string salarygradetemplate_gid { get; set; }
    }
    public class edittemplate_list : result
    {
        public string employee2salarygradetemplate_gid { get; set; }
        public string employee_gid { get; set; }
        public string salarygradetemplate_gid { get; set; }
        public string template_name { get; set; }
        public string basic_salary { get; set; }
        public string gross_salary { get; set; }
        public string net_salary { get; set; }
        public string ctc { get; set; }

    }
    public class Editsummarydeduction_list : result
    {
        public string employee2salarygradetemplatedtl_gid { get; set; }
        public string employee2salarygradetemplate_gid { get; set; }
        public string salarygradetemplate_giddtl { get; set; }
        public string demployer_contribution { get; set; }
        public string salarycomponent_percentage_employer { get; set; }
        public string salarycomponent_percentage { get; set; }
        public string affect_in { get; set; }
        public string demployee_contribution { get; set; }
        public string componentgroup_name { get; set; }
        public string componentgroup_gid { get; set; }
        public string salarycomponent_name { get; set; }
        public string salarycomponent_gid { get; set; }
        public string salarygradetype { get; set; }

    }
    public class Editsummaryaddition_list : result
    {
        public string employee2salarygradetemplatedtl_gid { get; set; }
        public string employee2salarygradetemplate_gid { get; set; }
        public string salarygradetemplate_giddtl { get; set; }
        public string employer_contribution { get; set; }
        public string salarycomponent_percentage_employer { get; set; }
        public string salarycomponent_percentage { get; set; }
        public string affect_in { get; set; }
        public string employee_contribution { get; set; }
        public string componentgroup_name { get; set; }
        public string componentgroup_gid { get; set; }
        public string salarycomponent_name { get; set; }
        public string salarycomponent_gid { get; set; }
        public string salarygradetype { get; set; }

    }
    public class SalaryGradetoemployee : result
    {
        public List<summaryaddition_list> summaryaddition_list { get; set; }
        public List<summarydeduction_list> summarydeduction_list { get; set; }
        public List<employee_lists> employee_lists { get; set; }

        public string NewBasicSalary { get; set; }
        public string template_name { get; set; }
        public string gross_salary { get; set; }
        public string ctc { get; set; }
        public string net_salary { get; set; }
        public string salarygradetemplate_gid { get; set; }
    }
    public class employee_lists : result
    {
        public string employee_gid { get; set; }
    }
        public class summaryaddition_list : result
    {
        public string salarygradetemplatedtl_gid { get; set; }
        public string employer_contribution { get; set; }
        public string salarycomponent_percentage_employer { get; set; }
        public string salarycomponent_percentage { get; set; }
        public string affect_in { get; set; }
        public string employee_contribution { get; set; }
        public string componentgroup_name { get; set; }
        public string componentgroup_gid { get; set; }
        public string salarycomponent_name { get; set; }
        public string salarycomponent_gid { get; set; }
        public string salarygradetype { get; set; }
  
    }
    public class summarydeduction_list : result
    {
        public string salarygradetemplatedtl_gid { get; set; }
        public string demployer_contribution { get; set; }
        public string salarycomponent_percentage_employer { get; set; }
        public string salarycomponent_percentage { get; set; }
        public string affect_in { get; set; }
        public string demployee_contribution { get; set; }
        public string componentgroup_name { get; set; }
        public string componentgroup_gid { get; set; }
        public string salarycomponent_name { get; set; }
        public string salarycomponent_gid { get; set; }
        public string salarygradetype { get; set; }
    }
        public class template_list : result
    {
        public string template_name { get; set; }

        public string salarygradetemplate_gid { get; set; }
        public string salarygradetemplate_code { get; set; }
        public string salarygradetemplate_name { get; set; }
        public string basic_salary { get; set; }
        public string gross_salary { get; set; }
        public string net_salary { get; set; }
    
    }
        public class AddtionalComponent : result
    {
        public string salarycomponent_name { get; set; }
        public string salarycomponent_percentage { get; set; }
        public string salarycomponent_amount { get; set; }
        public string salarycomponent_amount_employer { get; set; }
        public string other_type { get; set; }

    }


        public class Getgradeassign_list : result
    {
        public string user_gid { get; set; }
        public string unit_name { get; set; }
        public string user_firstname { get; set; }
        public string user_code { get; set; }        
        public string user_status { get; set; }
        public string designation_name { get; set; }
        public string employee_gid { get; set; }
        public string branch_gid { get; set; }
        public string department_gid { get; set; }
        public string branch_name { get; set; }
        public string department_name { get; set; }

    }
        public class salarygrade_list : result
    {
        public string basic_salary { get; set; }
        public string net_salary { get; set; }
        public string unit_name { get; set; }
        public string gross_salary { get; set; }
        public string user_gid { get; set; }
        public string salarygradetemplate_name { get; set; }
        public string employee2salarygradetemplate_gid { get; set; }
        public string user_name { get; set; }
        public string designation_name { get; set; }
        public string employee_gid { get; set; }
        public string branch_name { get; set; }
        public string department_name { get; set; }
       

    }


    }