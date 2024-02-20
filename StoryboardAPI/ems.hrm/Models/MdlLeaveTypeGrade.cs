using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrm.Models
{
    public class MdlLeaveTypeGrade
    {
        public List<Leavetype_list> Leavetype_list { get; set; }
        public List<Addleave_list> Addleave_list { get; set; }

        public string message { get; set; }
        public bool status { get; set; }
    }
    public class Leavetype_list : result
    {
        public string leavetype_gid { get; set; }
        public string user_name { get; set; }
        public string leavetype_code { get; set; }
        public string leavetype_count { get; set; }
        public string consider_as { get; set; }
        public string leavetypestatus { get; set; }
        public string leavetype_name { get; set; }

    }
    public class Addleave_list : result
    {
        public string leavetype_code { get; set; }

        public string leavetype_name { get; set; }
        public string leavetype_status { get; set; }
        public string consider_as { get; set; }
        public string weekoff_applicable { get; set; }
        public string holiday_applicable { get; set; }
        public string carryforward { get; set; }
        public string accrud { get; set; }
        public string beyond_eligible { get; set; }
        public string leave_days { get; set; }

    }


}