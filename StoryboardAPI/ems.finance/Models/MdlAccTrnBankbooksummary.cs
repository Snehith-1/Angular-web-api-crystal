//using StoryboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace ems.finance.Models
{
    public class MdlAccTrnBankbooksummary : result
 
    {
        public List<Getbankbook_list> Getbankbook_list { get; set; }
        public List<Getsubbankbook_list> Getsubbankbook_list { get; set; }
        public List<Getbank_list> addbank_list { get; set; }
        public List<GetAccountGroupDropdown> GetAccTrnGroupDtl { get; set; }
        public List<GetAccountNameDropdown> GetAccTrnNameDtl { get; set; }
        public List<accountfetch_list> accountfetch_list { get; set; }


    }

    public class Getbankbook_list : result
    {
        public string bank_gid { get; set; }
        public string bank_code { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string account_no { get; set; }
        public string ifsc_code { get; set; }
        public string neft_code { get; set; }
        public string swift_code { get; set; }
        public string openning_balance { get; set; }
    }
  
    public class Getsubbankbook_list : result
    {
        public string journal_gid { get; set; }
        public string bank_gid { get; set; }
        public string transaction_date { get; set; }
        public string journal_refno { get; set; }
        public string bank_name { get; set; }
        public string account_no { get; set; }
        public string account_desc { get; set; }
        public string remarks { get; set; }
        public string credit_amount { get; set; }
        public string debit_amount { get; set; }
        public string closing_amount { get; set; }
        public string opening_balance { get; set; }

    }

    public class Getbank_list : result
    {
        public string account_gid { get; set; }
        public string bank_gid { get; set; }
       
        public string bank_name { get; set; }
        public string account_no { get; set; }
        public string account_type { get; set; }
        public string ifsc_code { get; set; }
        public string neft_code { get; set; }
        public string swift_code { get; set; }
        public string gl_code { get; set; }
       
    }
    public class GetAccountGroupDropdown : result
    {
        public string accountgroup_gid { get; set; }
        public string accountgroup_name { get; set; }
    }

    public class GetAccountNameDropdown : result
    {
        public string account_gid { get; set; }
        public string account_name { get; set; }

    }
    public class accountfetch_list : result
    {
        public string session_id { get; set; }
        public string accountgroup_name { get; set; }
        public string account_name { get; set; }
        public string dr_cr { get; set; }
        public string transaction_amount { get; set; }
        public string journal_desc { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }



    }
}
