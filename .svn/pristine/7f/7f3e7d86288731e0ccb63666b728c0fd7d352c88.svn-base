using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrm.Models
{
    public class MdlShiftType
    {
        public List<shift_list> shift_list { get; set; }
        public List<Time_list> Time_list { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
    }

    public class shift_list : result
    {
        public string shifttype_gid { get; set; }
        public string shifttype_name { get; set; }
        public string branch_name { get; set; }
        public string message { get; set; }
        public string status { get; set; }

    }
    public class Time_list : result
    {
        public string shifttypedtl_gid { get; set; }
        public string shifttypedtl_name { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }

    }
    public class weekday_list : result
    {
        public string weekday_gid { get; set; }
        public string weekday { get; set; }
        public DateTime logintime { get; set; }
        public DateTime logouttime { get; set; }
        public DateTime Ot_cutoff { get; set; }

    }
    public class shifttypeadd_list : result
    {
        public string login_scheduler { get; set; }
        public string entrycutoff_time { get; set; }
        public string overnight_flag { get; set; }
        public string inovernight_flag { get; set; }
        public string outovernight_flag { get; set; }
        public string logout_schedular { get; set; }
        public string shift_name { get; set; }
        public string email_list { get; set; }
        public string grace_time { get; set; }
        public string logintime { get; set; }
        public string logouttime { get; set; }
        public string Ot_cutoff { get; set; }

        public List<weekday_list> weekday_list { get; set; }


    }
}