using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.finance.Models
{
    public class MdlAccMstBankMasterSummary
    {
        public List<GetBankMaster_list> GetBankMaster_list { get; set; }
        public List<GetAccountType> GetAccountType { get; set; }
        public List<GetAccountGroup> GetAccountGroup { get; set; }
        public List<GetBranchName> GetBranchName { get; set; }
        public List<GetEditBankMaster_list> GetEditBankMaster_list { get; set; }



    }

    public class GetBankMaster_list : result
    {
        public string bank_gid { get; set; }
        public string bank_code { get; set; }
        public string branch_name { get; set; }
        public string bank_name { get; set; }
        public string account_type { get; set; }
        public string account_no { get; set; }
        public string ifsc_code { get; set; }
        public string neft_code { get; set; }
        public string swift_code { get; set; }
        public string openning_balance { get; set; }
        public string branch_gid { get; set; }
        public string account_gid { get; set; }
        public string accountgroup_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }


    }
    public class GetAccountType : result
    {
        public string account_gid { get; set; }
        public string account_type { get; set; }
    }
    public class GetAccountGroup : result
    {
        public string accountgroup_gid { get; set; }
        public string accountgroup_name { get; set; }
    }
    public class GetBranchName : result
    {
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
    }
    public class GetEditBankMaster_list : result
    {
        public string bank_gid { get; set; }
        public string bank_code { get; set; }
        public string branch_name { get; set; }
        public string bank_name { get; set; }
        public string account_type { get; set; }
        public string account_no { get; set; }
        public string ifsc_code { get; set; }
        public string neft_code { get; set; }
        public string swift_code { get; set; }
        public string openning_balance { get; set; }
        public string branch_gid { get; set; }
        public string account_gid { get; set; }
        public string accountgroup_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string user_gid { get; set; }


    }

}
