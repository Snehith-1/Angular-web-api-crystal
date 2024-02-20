using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.pmr.Models
{
    public class MdlPmrRptPurchaseorderdetailedreport:result

    {
        public List<PmrRptPurchaseorderdetailedreport_lists> PmrRptPurchaseorderdetailedreport_lists { get; set; }


    }
        public class PmrRptPurchaseorderdetailedreport_lists : result
        {
            public string purchaseorder_date  { get; set; }
            public string purchaseorder_gid   { get; set; }
            public string vendor_companyname  { get; set; }
            public string costcenter_name     { get; set; }
            public string total_amount        { get; set; }  
            public string grn_flag { get; set; }
            public string invoice_flag  { get; set; }
            public string payment_flag        { get; set; }







        }
    
}