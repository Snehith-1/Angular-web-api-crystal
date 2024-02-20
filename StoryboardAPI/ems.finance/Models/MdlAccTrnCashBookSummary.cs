using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.finance.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class MdlAccTrnCashBookSummary : result
    {
        public List<CashBook_list> CashBook_list { get; set; }
        public List<CashBookSelect_list> CashBookSelect_list { get; set; }


    }

    public class CashBook_list : result
    {
        public string externalgl_code { get; set; }
        public string branch_name { get; set; }
        public string openning_balance { get; set; }
        public string gl_code { get; set; }
        public string branch_code { get; set; }
        public string branch_gid { get; set; }

        public string closing_amount { get; set; }


    }

    public class CashBookSelect_list : result
    {
        public string transaction_date { get; set; }
        public string journal_refno { get; set; }
        public string branch_name { get; set; }
        public string account_desc { get; set; }
        public string remarks { get; set; }
        public string credit_amount { get; set; }
        public string debit_amount { get; set; }
        public string closing_amount { get; set; }
      


    }
}