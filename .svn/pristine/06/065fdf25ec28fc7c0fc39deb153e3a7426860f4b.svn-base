using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrm.Models
{
    public class MdlLeaveGrade : result
    {
        public List<leavegrade1_list> leavegrade_list { get; set; }
        public List<assignemployee_list> assign_employeelist { get; set; }
        public List<unassignemployee_list> unassign_employeelist { get; set; }
        public List<Leaveassign_type> Leaveassign_type { get; set; }
        public List<Leaveunassign_type> Leaveunassign_type { get; set; }


        public string branch_gid { get; set; }
        public string employee_gid { get; set; }

    }


    public class leavegrade1_list : result
    {
        public string leavegrade_gid { get; set; }
        public string leavegrade_code { get; set; }
        public string leavegrade_name { get; set; }
        public string leavetype_name { get; set; }
        public string total_leavecount { get; set; }
        public string available_leavecount { get; set; }
        public string leave_limit { get; set; }
    }

    public class Leaveassign_type : result
    {
        public string leavegrade_code { get; set; }
        public string leavegrade_name { get; set; }

    }

    public class Leaveunassign_type : result
    {
        public string leavegrade_code { get; set; }
        public string leavegrade_name { get; set; }

    }
    public class leavegradecode_list : result
    {
        public string leavetype_name { get; set; }
        public string leavetype_code { get; set; }
        public string leavetype_gid { get; set; }
        public string leave_limit { get; set; }
        public string available_leavecount { get; set; }
        public string total_leavecount { get; set; }
    }

    public class leavegradesubmit_list: result
    {
        public string leavetype_gid { get; set; }
        public string leavegrade_code { get; set; }
        public string leavegrade_name { get; set; }
        public string leavegrade_gid { get; set; }
        public string leave_limit { get; set; }
        public string available_leavecount { get; set; }
        public string total_leavecount { get; set; }
        public List<leavegradecode_list> leavegradecode_list { get; set; }


    }

    public class assignemployee_list : result
    {
        public string user_gid { get; set; }
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string employee_gender { get; set; }
        public string department_gid { get; set; }
        public string employee_gid { get; set; }
        public string designation_gid { get; set; }
        public string designation_name { get; set; }
        public string department_name { get; set; }
    
    }
    public class unassignemployee_list : result
    {
        public string user_gid { get; set; }
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string employee_gender { get; set; }
        public string leavegrade_gid { get; set; }
        public string department_gid { get; set; }
        public string employee_gid { get; set; }
        public string designation_gid { get; set; }
        public string designation_name { get; set; }
        public string department_name { get; set; }

    }
    public class assignsubmit_list : result
    {
        public List<assignemployee_list> assign_employeelist { get; set; }
        public string leavegrade_gid { get; set; }
        public string employee_name { get; set; }


    }

    public class unassignsubmit_list : result
    {
        public List<unassignemployee_list> unassign_employeelist { get; set; }
        public string leavegrade_gid { get; set; }
        public string employee_gid { get; set; }


    }
}