using ems.system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlMailmanagement : result
    {
        public List<from_list> from_list { get; set; }
        public List<mail_list> mail_list { get; set; }
        public List<GetmailViewSummary> GetmailViewSummary { get; set; }



    }
    public class from_list
    {
        public string mail { get; set; }
        public string company_gid { get; set; }

        public string sub { get; set; }
        public string to { get; set; }
        public string body { get; set; }
    }

    public class mail_list : result
    {


        public string mailmanagement_gid { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string sub { get; set; }

        public string body { get; set; }
      
        public string created_by { get; set; }
        public string created_date { get; set; }

       



    }
    public class GetmailViewSummary
    {
      
        public string mailmanagement_gid { get; set; }
        public string from { get; set; }
        public string sub { get; set; }
        public string to { get; set; }
        public string body { get; set; }
    }

}