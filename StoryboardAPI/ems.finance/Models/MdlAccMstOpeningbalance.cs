using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ems.finance.Models
{
    public class MdlAccMstOpeningbalance: result
    {
        public List<Openingbalance_list> Openingbalance_list { get; set; }
        public List<Openingbalance_lists> Openingbalance_lists { get; set; }
    }
    public class Openingbalance_list : result
    {
        public string accountgroup_name { get; set; }
        public string openningbalance_gid { get; set; }

        public string account_name { get; set; }
        public string branch_name { get; set; }
        public string credit_amount { get; set; }

    }
    public class Openingbalance_lists : result
    {
        public string openningbalance_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string account_name { get; set; }
        public string branch_name { get; set; }
        public string debit_amount { get; set; }

    }

}