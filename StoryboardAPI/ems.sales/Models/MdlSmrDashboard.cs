using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoryboardAPI.Models;

namespace ems.sales.Models
{

    public class MdlSmrDashboard : result
    {
        public List<GetSalesPerformanceChart_List> GetSalesPerformanceChart_List { get; set; }        
        public List<GetSalesOrderCount_List> GetSalesOrderCount_List { get; set; }
        public List<GetOverallSalesOrderChart_List> GetOverallSalesOrderChart_List { get; set; }
        public List<Mtd_List>Mtd_List { get; set; }

   
        public string totalinvoice { get; set; }
        public string approval_pending { get; set; }
        public string payment_pending { get; set; }
        public string approvalpendinginnvoice { get; set; }
        public string potentialleadcount { get; set; }

        public string mtd_over_due_payment { get; set; }
        public string mtd_over_due_payment_amount { get; set; }
        public string mtd_over_due_invoice_amount { get; set; }
        public string mtd_over_due_invoice { get; set; }

        public string ytd_over_due_payment { get; set; }
        public string ytd_over_due_payment_amount { get; set; }
        public string ytd_over_due_invoice_amount { get; set; }
        public string ytd_over_due_invoice { get; set; }

    }
    public class GetSalesPerformanceChart_List : result
    {
        public string order_amount { get; set; }
        public string amount { get; set; }
        public string payment_day { get; set; }
        public string payment_amount { get; set; }
        public string invoice_amount { get; set; }
        public string orderdate { get; set; }
        public string outstanding_amount { get; set; }
        public string lsemployee_gid { get; set; }
        // public string lsemployee_gid_list { get; set; }

    }
    public class GetSalesOrderCount_List : result
    {
        public string total_so { get; set; }
        public string approved_so { get; set; }
        public string pending_So { get; set; }
        public string rejected_so { get; set; }
        public string total_do { get; set; }
        public string pending_do { get; set; }
        public string completed_do { get; set; }
        public string Partial_done { get; set; }
        public string today_invoice { get; set; }
        public string totalinvoice { get; set; }
        public string so_amended { get; set; }
        public string aproved_invoice { get; set; }
        public string payment_pending { get; set; }
        public string pending_invoice { get; set; }
        public string totalpayment { get; set; }
        public string total_quotation { get; set; }
        public string quotation_canceled { get; set; }
        public string quotation_completed { get; set; } 
        public string payment_completed { get; set; }
        public string payment_don_partial { get; set; } 
        public string invoice_count { get; set; }
    }
    public class GetOverallSalesOrderChart_List : result
    {
        public string count_own { get; set; }
        public string salesorder_status_own { get; set; }
        public string count_Hierarchy { get; set; }
        public string salesorder_status_Hierarchy { get; set; }
        public string do_count_own { get; set; }
        public string delivery_status_own { get; set; }
        public string do_count_Hierarchy { get; set; }
        public string delivery_status_Hierarchy { get; set; }
        public string payment_day { get; set; }
        public string amount { get; set; }

    }


    public class Mtd_List : result
    {

        public string mtd_over_due_payment { get; set; }
        public string mtd_over_due_payment_amount { get; set; }
        public string mtd_over_due_invoice_amount { get; set; }
        public string mtd_over_due_invoice { get; set; }

        public string ytd_over_due_payment { get; set; }
        public string ytd_over_due_payment_amount { get; set; }
        public string ytd_over_due_invoice_amount { get; set; }
        public string ytd_over_due_invoice { get; set; }

    }
}
