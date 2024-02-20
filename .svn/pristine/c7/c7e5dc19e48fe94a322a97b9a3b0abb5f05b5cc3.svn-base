
using StoryboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrm.Models
{
    public class MdlHrmTrnAttendanceroll : result
    {
        public List<update_lists> update_lists { get; set; }
        public List<shiftList> shiftList { get; set; }
        public List<GetBranch1> GetBranch1 { get; set; }
        public List<GetDepartment1> GetDepartment1 { get; set; }
        public List<employeeAttendace_list> employeeAttendace_list { get; set; }
        public List<employeeattendace_list1> employeeattendace_list1 { get; set; }
        public List<punchupdatedtl> punchupdatedtl { get; set; }
    }
    public class punchupdatedtl : result
    {
        public List<update_lists> update_lists { get; set; }
        public string date { get; set; }
        public string punchin_time { get; set; }
        public string punchout_time { get; set; }

    }
   
        public class update_lists : result
    {
        public string date { get; set; }
        public string login_time { get; set; }
        public string logout_time { get; set; }
        public string user_code { get; set; }
        public string empname { get; set; }
        public string employee_gid { get; set; }

    }


    

        public class GetBranch1 : result
    {
        public string branch_name { get; set; }
        public string branch_gid { get; set; }

    } 
    public class shiftList : result
    {
        public string shift_name { get; set; }
        public string shift_gid { get; set; }

    }
    public class GetDepartment1 : result
    {
        public string department_name { get; set; }
        public string department_gid { get; set; }

    }
    public class employeeattendace_list1 : result
    {
        public string user_code { get; set; }
        public string employee_gid { get; set; }
        public string empname { get; set; }
        public string login_time { get; set; }
        public string logout_time { get; set; }
        public string lunch_in { get; set; }
        public string lunch_out { get; set; }
        public string OT_in { get; set; }
        public string OT_out { get; set; }
        public string OT_duration { get; set; }
        public string emp_status { get; set; }
    }
        public class employeeAttendace_list : result
    {
        public string date { get; set; }
    }
  }