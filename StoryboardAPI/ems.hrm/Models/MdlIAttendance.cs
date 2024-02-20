using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ems.hrm.Models
{
    public class MdlIAttendance : result
    {
        public List<Iattendance_list> Iattendance_list { get; set; }
    }
    public class Iattendance_list : result
    {
        public string employee_gid { get; set; }
        public string attendance_date { get; set; }
        public string login_time { get; set; }
        public string login_time_audit { get; set; }
        public string employee_attendance { get; set; }
        public string attendance_source { get; set; }
        public string login_ip { get; set; }
        public string attendance_type { get; set; }
        public string update_flag { get; set; }
        public string halfdayabsent_flag { get; set; }
        public string updated_date { get; set; }
        public string location { get; set; }
        public string created_date { get; set; }
        public string Present { get; set; }
        public string Manual { get; set; }
        





    }
}