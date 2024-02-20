using System;
using System.Collections.Generic;


namespace ems.hrm.Models
{
    public class MdlHrmTrnProfileManagement : result
    {
        public List<HrmTrnProfileManagement> HrmTrnProfileManagement_list { get; set; }
       
        public List<nomination_list> nominationlist { get; set; }
        public List<emergencycontact_list> emergencycontactlist { get; set; }

        public List<dependent_list> dependentlist { get; set; }
        public List<employeename_list> employeenamelist { get; set; }

        public List<education_list> educationlist { get; set; }
        public List<CompanyPolicy> CompanyPolicy { get; set; }

        //public List<bloodgroup_list> bloodgroup_list { get; set; }
        public List<relationshipwith_employee_list> relationshipwith_employee_list { get; set; }
        public List<GetEditEmployee> GetEditEmployee { get; set; }
    }

    public class relationshipwith_employee_list : result
    {
        public string relationship { get; set; }
        public string relation_gid { get; set; }
    }

    public class HrmTrnProfileManagement : result
    {
        public string user_gid { get; set; }
     
    }
    public class personaldetails : result
    {
        public string martial_edit { get; set; }
        public string company_edit { get; set; }
        public string firstedit_name { get; set; }
        public string lastedit_name { get; set; }
        public string gender_edit { get; set; }
        public string dateof_birth { get; set; }
        public string dateof_birthedit { get; set; }
        public string mobile { get; set; }
        public string mobile_edit { get; set; }
        public string personal_no { get; set; }
        public string personal_noedit { get; set; }
        public string qualification { get; set; }
        public string qualification_edit { get; set; }
        public string blood_group { get; set; }
        public string blood_groupedit { get; set; }
        public string experience { get; set; }
        public string experience_edit { get; set; }
    }

    public class GetEditEmployee : result
    {
        public string bloodgroup_name { get; set; }
        public string marital_status { get; set; }
        public string employee_joingdate { get; set; }
        public string jobtype_name { get; set; }
        public string employee_personalno { get; set; }
        public string employee_qualification { get; set; }
        public string temporary_addressgid { get; set; }
        public string permanent_addressgid { get; set; }
        public string permanent_countrygid { get; set; }
        public string temporary_countrygid { get; set; }
        public string entity_gid { get; set; }
        public string branch_gid { get; set; }
        public string permanent_address1 { get; set; }
        public string department_gid { get; set; }
        public string designation_gid { get; set; }
        public string permanent_address2 { get; set; }
        public string permanent_city { get; set; }
        public string permanent_state { get; set; }
        public string permanent_postalcode { get; set; }
        public string permanent_country { get; set; }
        public string temporary_address1 { get; set; }
        public string temporary_address2 { get; set; }
        public string temporary_city { get; set; }
        public string temporary_postalcode { get; set; }
        public string temporary_country { get; set; }
        public string temporary_state { get; set; }
        public string designation_name { get; set; }
        public string employee_photo { get; set; }
        public string employee_gid { get; set; }
        public string employee_gender { get; set; }
        public string entity_name { get; set; }
        public string identity_no { get; set; }
        public string employee_dob { get; set; }
        public string employee_sign { get; set; }
        public string bloodgroup { get; set; }
        public string employee_image { get; set; }
        public string employee_emailid { get; set; }
        public string employee_mobileno { get; set; }
        public string employee_documents { get; set; }
        public string employee_experience { get; set; }
        public string employee_experiencedtl { get; set; }
        public string employeereporting_to { get; set; }
        public string employment_type { get; set; }
        public string user_code { get; set; }
        public string user_firstname { get; set; }
        public string user_lastname { get; set; }
        public string user_status { get; set; }
        public string usergroup_gid { get; set; }
        public string usergroup_code { get; set; }
        public string branch_name { get; set; }
        public string department_name { get; set; }
        public string approveby_name { get; set; }
        public string employee_joiningdate { get; set; }
        public string approver_name { get; set; }
        public string nationality { get; set; }
        public string nric_no { get; set; }


    }



    public class passwordupdate : result
    {
        public string curredit_pwd { get; set; }
        public string newedit_pwd { get; set; }
        public string confedit_pwd { get; set; }

    }
    public class workexperience : result
    {
        public string empl_prevcomp { get; set; }
        public string empl_code { get; set; }
        public string prev_occp { get; set; }
        public string department { get; set; }
        public string date_ofjoining { get; set; }
        public string date_ofreleiving { get; set; }
        public string work_period { get; set; }
        public string HR_name { get; set; }
        public string reason { get; set; }
        public string report { get; set; }
        public string rmrks { get; set; }
    }

    public class nomination : result
    {
        public string name { get; set; }
        public string dateofbirth { get; set; }
        public string age { get; set; }
        public string mobile_no { get; set; }
        public string relt_employee { get; set; }
        public string resign_employee { get; set; }
        public string residing_addr { get; set; }
        public string nominee_for { get; set; }

    }

    public class nomination_list : result
    {
        public string familydtl_gid { get; set; }
        public string familymember_name { get; set; }
        public string dob { get; set; }
        public string age { get; set; }
        public string mobile_no { get; set; }
        public string relationship { get; set; }
        public string residing_with { get; set; }
        public string residing_address { get; set; }
        public string nominee_for { get; set; }

    }

    public class statutory : result
    {
        public string provident_no { get; set; }
        public string date_ofjoinPF { get; set; }
        public string employee_no { get; set; }
    }
    public class emergencycontact : result
    {
        public string contact_person { get; set; }
        public string cont_addr { get; set; }
        public string cont_no { get; set; }
        public string cont_emailid { get; set; }
        public string remarks { get; set; }
       
 }
   

    public class emergencycontact_list : result
    {
        public string emergency_gid { get; set; }
        public string contact_name { get; set; }
        public string contact_no { get; set; }
        public string contact_email { get; set; }
        public string contact_address { get; set; }
        public string reference_details { get; set; }

    }
    public class employeename_list : result
    {
        public string Name { get; set; }
        public string UserCode { get; set; }
        public string Designation { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string Joiningdate { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string email { get; set; }
        public string employeemobileNo { get; set; }

    }

    public class dependent : result
    {
        public string name_user { get; set; }
        public string relationship { get; set; }
        public string date_ofbirth { get; set; }
    }

    public class dependent_list : result
    {
        public string dependent_gid { get; set; }
        public string name { get; set; }
        public string relationship { get; set; }
        public string dob { get; set; }
    }
    public class education : result
    {
        public string inst_name { get; set; }
        public string deg_dip { get; set; }
        public string field_ofstudy { get; set; }
        public string date_ofcompletion { get; set; }
        public string addn_notes { get; set; }

    }

    public class education_list : result
    {
        public string education_gid { get; set; }
        public string institution_name { get; set; }
        public string degree_diploma { get; set; }
        public string field_study { get; set; }
        public string date_completion { get; set; }
        public string address_notes { get; set; }

    }
    public class CompanyPolicy : result
    {
        public string policy_name { get; set; }
        public string policy_desc { get; set; }
    }

    //public class bloodgroup_list : result
    //{
    //    public string bloodgroup_gid { get; set; }
    //    public string api_code { get; set; }
    //    public string bloodgroup_name { get; set; }
    //    public string created_by { get; set; }
    //    public string created_date { get; set; }
    //    public string status { get; set; }

    //}
    


}