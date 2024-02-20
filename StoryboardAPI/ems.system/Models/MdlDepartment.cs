using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ems.system.Models
{
    public class MdlDepartment : result
    {
        public List<department_list> department_list { get; set; }
        public List<GetDepartmentAddDropdown> GetDepartmentAddDropdown { get; set; }


    }
    public class department_list : result
    {
        public string department_gid { get; set; }
        public string department_code { get; set; }
        public string department_prefix { get; set; }
        public string department_name { get; set; }
        public string department_manager { get; set; }
        public string department_code_edit { get; set; }
        public string department_prefix_edit { get; set; }
        public string department_name_edit { get; set; }
        public string department_manager_edit { get; set; }
        public string employee_gid { get; set; }
    }

    public class GetDepartmentAddDropdown : result
    {
        public string employee_gid { get; set; }

        public string department_manager { get; set; }
    }
}
