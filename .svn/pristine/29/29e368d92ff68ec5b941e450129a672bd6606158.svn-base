using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.payroll.Models
{
    public class MdlPayTrnReportPayment
    {
        public List<GetPaylist> GetPaylist { get; set; }
        public List<GetreportExpand> GetreportExpand { get; set; }
        public List<GetreportExportExcel> GetreportExportExcel { get; set; }
        public double grand_total { get; set; }
        public bool status { get; set; }
        public string message { get; set; }



    }

    public class GetPaylist : result
    {
        public string Employee_count { get; set; }
        public string Amount { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public double grandtotal { get; set; }
       

    }
    public class GetreportExpand : result
    {
        public string employee_gid { get; set; }
        public string user_code { get; set; }
        public string employee_name { get; set; }
        public string branch_name { get; set; }
        public string department_name { get; set; }
        public string net_salary { get; set; }
        public string designation_name { get; set; }

    }

    public class GetreportExportExcel : result
    {
        public string lspath1 { get; set; }


        public string lsname { get; set; }

    }


}