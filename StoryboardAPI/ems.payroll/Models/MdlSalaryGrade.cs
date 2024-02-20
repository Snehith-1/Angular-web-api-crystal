using ems.system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.payroll.Models
{
    public class MdlSalaryGrade: result
    {
        public List<componentdetails> componentdetails { get; set; }
        public List<salarygradetemplate_list> salarygrade_list { get; set; }
        public List<addition_list> addition_list { get; set; }
        public List<deduction_list> deduction_list { get; set; }

        public List<GetComponentname> GetComponentname { get; set; }
        public List<Getcomponentamount> Getcomponentamount { get; set; }
        public List<SalaryGradeData> SalaryGradeData { get; set; }
        public List<Editaditional> Editaditional { get; set; }
        public List<Editdeduction> Editdeduction { get; set; }
        public List<UpdateSalaryGradeData> UpdateSalaryGradeData { get; set; }
    }

    public class UpdateSalaryGradeData : result
    {
        public List<Editaditional> Editaditional { get; set; }
        public List<Editdeduction> Editdeduction { get; set; }
        public string salarygradetemplate_gid { get; set; }
        public string NewBasicSalary { get; set; }
        public string template_name { get; set; }
        public string gross_salary { get; set; }
        public string ctc { get; set; }
        public string net_salary { get; set; }
    }

    public class Editaditional : result
    {
        public string salarycomponent_percentage_employer { get; set; }
        public string othercomponent_type { get; set; }
        public string salarycomponent_name { get; set; }
        public string salarycomponent_percentage { get; set; }
        public string salarygradetemplatedtl_gid { get; set; }
        public string salarygradetype { get; set; }
        public string salarycomponent_gid { get; set; }
        public string salarygradetemplate_gid { get; set; }
        public string component_percentage_employer { get; set; }
        public string employer_contribution { get; set; }
        public string component_flag_employer { get; set; }
        public string employee_contribution { get; set; }
        public string component_percentage { get; set; }
        public string component_flag { get; set; }
        public string affect_in { get; set; }
        public string affecting_in { get; set; }
        public string component_type { get; set; }
        public string componentgroup_gid { get; set; }
        public string component_code { get; set; }
        public string component_name { get; set; }
        public string componentgroup_name { get; set; }


    }

    public class Editdeduction : result
    {
        public string salarycomponent_percentage_employer { get; set; }
        public string othercomponent_type { get; set; }
        public string salarycomponent_name { get; set; }
        public string salarycomponent_percentage { get; set; }
        public string salarygradetemplatedtl_gid { get; set; }
        public string salarygradetype { get; set; }
        public string salarycomponent_gid { get; set; }
        public string salarygradetemplate_gid { get; set; }
        public string component_percentage_employer { get; set; }
        public string demployer_contribution { get; set; }
        public string component_flag_employer { get; set; }
        public string demployee_contribution { get; set; }
        public string component_percentage { get; set; }
        public string component_flag { get; set; }
        public string affect_in { get; set; }
        public string affecting_in { get; set; }
        public string component_type { get; set; }
        public string componentgroup_gid { get; set; }
        public string component_code { get; set; }
        public string component_name { get; set; }
        public string componentgroup_name { get; set; }

    }

    public class componentdetails : result
    {
        public string salarygradetemplate_gid { get; set; }
        public string employee_contribution { get; set; }
        public string employer_contribution { get; set; }
        public string component_name { get; set; }

    }

        public class SalaryGradeData : result
    {
        public List<addition_list> addition_list { get; set; }
        public List<deduction_list> deduction_list { get; set; }

        public string NewBasicSalary { get; set; }
        public string template_name { get; set; }
        public string gross_salary { get; set; }
        public string ctc { get; set; }
        public string net_salary { get; set; }
    }
    public class Getcomponentamount : result
    {
        public string affecting_in { get; set; }
        public string component_flag { get; set; }
        public string componentgroup_name { get; set; }
        public string component_percentage { get; set; }
        public string component_amount { get; set; }
        public string component_flag_employer { get; set; }
        public string component_name { get; set; }
        public string component_percentage_employer { get; set; }
        public string component_amount_employer { get; set; }
    }

    public class GetComponentname : result
    {
        public string salarycomponent_name { get; set; }
        public string component_name { get; set; }
        public string salarycomponent_gid { get; set; }
        public string componentgroup_name { get; set; }
    }
        public class addition_list : result
    {
        public string salarycomponent_gid { get; set; }
        public string component_code { get; set; }
        public string component_name { get; set; }
        public string componentgroup_name { get; set; }
        public string componentgroup_gid { get; set; }
        public string component_type { get; set; }
        public string component_flag { get; set; }
        public string affecting_in { get; set; }
        public string component_percentage { get; set; }
        public string component_amount { get; set; }
        public string component_flag_employer { get; set; }
        public string component_amount_employer { get; set; }
        public string component_percentage_employer { get; set; }
        public string employee_contribution { get; set; }
        public string employer_contribution { get; set; }

    }

    public class deduction_list : result
    {
        public string salarycomponent_gid { get; set; }
        public string component_code { get; set; }
        public string component_name { get; set; }
        public string componentgroup_name { get; set; }
        public string componentgroup_gid { get; set; }
        public string component_type { get; set; }
        public string component_flag { get; set; }
        public string affecting_in { get; set; }
        public string component_percentage { get; set; }
        public string component_amount { get; set; }
        public string component_flag_employer { get; set; }
        public string component_amount_employer { get; set; }
        public string demployee_contribution { get; set; }
        public string demployer_contribution { get; set; }
        public string component_percentage_employer { get; set; }

    }

    public class salarygradetemplate_list : result
    {
        public string salarygradetemplate_gid { get; set; }
        public string salarygradetemplate_name { get; set; }
        public string created_date { get; set; }
        public string basic_salary { get; set; }
        public string gross_salary { get;set;}

        public string net_salary { get; set; }



    }
}