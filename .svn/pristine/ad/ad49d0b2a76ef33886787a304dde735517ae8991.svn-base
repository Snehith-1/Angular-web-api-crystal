using ems.pmr.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using static ems.pmr.Models.MdlPmrRptPurchaseorderdetailedreport;


namespace ems.pmr.DataAccess
{
    public class DaPmrRptPurchaseorderdetailedreport
    {
       
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid, maGetGID, lsvendor_code, msUserGid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetPmrRptPurchaseorderdetailedreportSummary(MdlPmrRptPurchaseorderdetailedreport values)
        {
            try
            {
                
                msSQL = " select a.purchaseorder_date,format(a.total_amount,2) as total_amount,a.purchaseorder_flag,a.purchaseorder_gid,b.vendor_companyname,d.costcenter_name,a.grn_flag,a.invoice_flag,a.payment_flag,c.user_firstname " +
                " from pmr_trn_tpurchaseorder a " +
                " left join acp_mst_tvendor b on  a.vendor_gid=b.vendor_gid " +
                " left join adm_mst_tuser c on a.created_by=c.user_gid " +
                " left join pmr_mst_tcostcenter d on a.costcenter_gid=d.costcenter_gid " +
                " where a.purchaseorder_flag = 'PO Approved'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<PmrRptPurchaseorderdetailedreport_lists>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new PmrRptPurchaseorderdetailedreport_lists
                    {

                        purchaseorder_date = dt["purchaseorder_date"].ToString(),
                        purchaseorder_gid = dt["purchaseorder_gid"].ToString(),
                        vendor_companyname = dt["vendor_companyname"].ToString(),
                        costcenter_name = dt["costcenter_name"].ToString(),
                        total_amount = dt["total_amount"].ToString(),
                        grn_flag = dt["grn_flag"].ToString(),
                        invoice_flag = dt["invoice_flag"].ToString(),
                        payment_flag = dt["payment_flag"].ToString(),
                        

                    });
                    values.PmrRptPurchaseorderdetailedreport_lists = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Purchaseorder detailed report Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

    }
}
