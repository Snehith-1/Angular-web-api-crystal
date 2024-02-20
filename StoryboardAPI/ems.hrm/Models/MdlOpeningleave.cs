using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrm.Models
{
    public class MdlOpeningleave : result
    {
        public List<leaveopening_list> leaveopening_list { get; set; }
        public List<leave_list> leave_list { get; set; }
        public List<leaveopeningbalance_list> leaveopeningbalance_list { get; set; }
        public List<leavebalance_list> leavebalance_list { get; set; }
    }


    public class leaveopening_list : result
    {
        public string user_gid { get; set; }
        public string appointmentorder_gid { get; set; }
        public string appointment_date { get; set; }
        public string mobile_number { get; set; }
        public string user_name { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string designation_name { get; set; }
        public string designation_gid { get; set; }
        public string employee_gid { get; set; }
        public string branch_name { get; set; }
        public string email_address { get; set; }
        public string qualification { get; set; }
        public string branch_gid { get; set; }

    }

    public class leave_list : result
    {
        public string leavegrade_name { get; set; }
        public string leavegrade_gid { get; set; }
    }

    public class leaveopeningbalance_list : result
    {
        public string leavegrade_gid { get; set; }
        public string leavegrade_code { get; set; }
        public string leavegrade_name { get; set; }
        public string leavetype_name { get; set; }
        public string total_leavecount { get; set; }
        public string available_leavecount { get; set; }
        public string leave_limit { get; set; }
        public string leavetype_gid { get; set; }
        public string branch_gid { get; set; }
        public string active_flag { get; set; }
    }

    public class leavebalance_list : result
    {
        public string branch_gid { get; set; }
        public string employee_gid { get; set; }
        public string leavegrade_gid { get; set; }
        public string employee_name { get; set; }
        public string leavegrade_code { get; set; }
        public string leavegrade_name { get; set; }
        public string attendance_startdate { get; set; }
        public string attendance_enddate { get; set; }
        public string total_leavecount { get; set; }
        public string available_leavecount { get; set; }
        public string leave_limit { get; set; }
        public string flag { get; set; }

    }



}