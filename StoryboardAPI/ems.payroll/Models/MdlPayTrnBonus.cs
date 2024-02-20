
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ems.payroll.Models
{
    public class MdlPayTrnBonus : result
    {
        public List<GetBonus> GetBonus { get; set; }
        public List<bonusSummarylist> bonusSummarylist { get; set; }
        public List<createbonus_list> createbonus_list { get; set; }
        public List<generatebonus_list> generatebonus_list { get; set; }
        public List<updatebonus_list> updatebonus_list { get; set; }

    }
    public class bonusSummarylist : result
    {
        public string user_gid { get; set; }
        public string user_firstname { get; set; }
        public string user_code { get; set; }
        public string designation_name { get; set; }
        public string employee_gid { get; set; }
        public string branch_name { get; set; }
        public string department_gid { get; set; }
        public string department_name { get; set; }
        public string branch_gid { get; set; }
    }
    public class GetBonus : result
    {
        public string bonus_gid { get; set; }
        public string bonus_name { get; set; }
        public string bonus_fromdate { get; set; }
        public string bonus_todate { get; set; }
        public string bonus_percentage { get; set; }


    }

    public class createbonus_list : result
    {
        public string bonus_gid { get; set; }
        public string bonus_name { get; set; }
        public string bonus_todate { get; set; }
        public string bonus_date { get; set; }
        public string employee_gid { get; set; }
        public string bonus_percentage { get; set; }
        public string remarks { get; set; }


    }
    public class selectemployee_list : result
    {
        public string bonus_gid { get; set; }
        public List<bonusSummarylist> bonusSummarylist { get; set; }

    }

    public class generatebonus_list : result
    {
        public string user_code { get; set; }
        public string user_firstname { get; set; }
        public string bonus_gid { get; set; }
        public string bonus_name { get; set; }
        public string designation_name { get; set; }
        public string employee_gid { get; set; }
        public string bonus_percentage { get; set; }
        public string branch_name { get; set; }
        public string department_gid { get; set; }
        public string branch_gid { get; set; }
        public string department_name { get; set; }
        public string bonus_from { get; set; }
        public string bonus_to { get; set; }
        public string bonus_amount { get; set; }


    }
    public class updatebonus_list : result
    {
        public string bonus_gid { get; set; }
        public string employee_gid { get; set; }
        public string bonus_amount { get; set; }

    }


}