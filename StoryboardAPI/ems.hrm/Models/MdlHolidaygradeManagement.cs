using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrm.Models
{
    public class MdlHolidaygradeManagement
    {
        public List<holidaygrade_list> holidaygrade_list { get; set; }
        public List<addholidaygrade_list> addholidaygrade_list { get; set; }
        public List<holidaygrade1_list> holidaygrade1_list { get; set; }
        public List<Addholidayassign_list> Addholidayassign_list { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
    }
    public class Addholidayassign_list : result
    {
        public string holidaygrade_gid { get; set; }
        public string holidaygrade_code { get; set; }
        public string holidaygrade_name { get; set; }
        public List<holidaygrade_list> holidaygrade_list { get; set; }

    }
    public class holidaygrade_list : result
    {
        public string holidaygrade_gid { get; set; }
        public string holidaygrade_code { get; set; }
        public string holidaygrade_name { get; set; }
        public string holiday_name { get; set; }
        public string holiday_type { get; set; }
        public DateTime holiday_date { get; set; }
        public string holiday_remarks { get; set; }
        public string holidayremarks { get; set; }
        public string holiday_gid { get; set; }
    }
    public class holidaygrade1_list : result
    {
        public string holidaygrade_gid { get; set; }
        public string holidaygrade_code { get; set; }
        public string holidaygrade_name { get; set; }
        public string holiday_name { get; set; }
        public string holiday_type { get; set; }
        public string holiday_date { get; set; }
        public string holiday_remarks { get; set; }
        public string holidayremarks { get; set; }
        public string holiday_gid { get; set; }


    }

    public class addholidaygrade_list : result
    {
        public string holiday_name { get; set; }
        public string holiday_type { get; set; }
        public string holiday_date { get; set; }
        public string holiday_remarks { get; set; }
        public string holiday_gid { get; set; }
    }
}