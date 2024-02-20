using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.einvoice.Models;
using static ems.einvoice.Models.MdlInvoicedashboard;


namespace ems.einvoice.DataAccess
{
    public class DaInvoicedashboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid2, msGetGid, msGetGid3, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaTileCount(TileCount values)
        {
            try
            {

                msSQL = "select count(*) as total_raised_invoice, " +
                "(select count(*) from rbl_trn_tinvoice where invoice_status = 'Invoice Cancelled') as 'invoice_cancelled'," +
                "(select count(*)  from rbl_trn_tinvoice where irn is null) as 'irn_pending'," +
                "(select count(*) from rbl_trn_tinvoice where irn is not null) as 'irn_generated'," +
                "(select count(*) from rbl_trn_tinvoice where irncancel_date is not null) as 'irn_cancelled'," +
                "(select count(*) from rbl_trn_tinvoice where creditnote_status ='Y') as 'creditnote' " +
                "from rbl_trn_tinvoice";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader != null)
                {
                    objMySqlDataReader.Read();

                    values.total_raised_invoice = objMySqlDataReader["total_raised_invoice"].ToString();
                    values.invoice_cancelled = objMySqlDataReader["invoice_cancelled"].ToString();
                    values.irn_Pending = objMySqlDataReader["irn_pending"].ToString();
                    values.irn_generated = objMySqlDataReader["irn_generated"].ToString();
                    values.irn_cancelled = objMySqlDataReader["irn_cancelled"].ToString();
                    values.credit_note = objMySqlDataReader["creditnote"].ToString();

                    objMySqlDataReader.Close();
                }
                msSQL = "select count(*) as 'Product_count' from pmr_mst_tproduct where 1=1";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader != null)
                {
                    objMySqlDataReader.Read();
                    values.product_count = objMySqlDataReader["Product_count"].ToString();
                    objMySqlDataReader.Close();
                }
                msSQL = "select count(*) as 'customer_count' from crm_mst_tcustomer where 1=1";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader != null)
                {
                    objMySqlDataReader.Read();
                    values.customer_count = objMySqlDataReader["customer_count"].ToString();
                    objMySqlDataReader.Close();
                }

                return;
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Title Count!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        public void Dainvoicesummary(MdlInvoicedashboard values)
        {
            try
            {

                msSQL = "select distinct a.invoice_gid,a.invoice_refno,a.customer_gid,a.irn,a.invoice_date, a.invoice_reference,a.mail_status," +
                    " a.additionalcharges_amount,a.discount_amount,format(a.invoice_amount, 2) as invoice_amount," +
                    " case when a.customer_contactnumber is null then concat(a.customer_contactperson,' / ',a.customer_contactnumber)" +
                    " else concat(a.customer_contactperson, if (a.customer_email = '',' ',concat(' / ', a.customer_email))) end as customer_contactperson, " +
                    " case when a.currency_code = '  currency  ' then a.customer_name when a.currency_code is null then a.customer_name  " +
                    " when a.currency_code is not null and a.currency_code <> '  currency  ' then concat(a.customer_name) end as customer_name," +
                    " a.currency_code,  a.customer_contactnumber as mobile,a.invoice_from," +
                    " case when a.irn is not null then 'IRN Generated' when irn is null  then 'IRN Pending' end  as status" +
                    " from rbl_trn_tinvoice a group by a.invoice_refno order by date(a.invoice_date) desc,a.invoice_date asc, a.invoice_gid desc,a.invoice_gid desc limit 5 ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<einvoicesummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new einvoicesummary_list
                        {
                            irn = dt["irn"].ToString(),
                            invoice_gid = dt["invoice_gid"].ToString(),
                            invoice_date = Convert.ToDateTime(dt["invoice_date"].ToString()),
                            invoice_refno = dt["invoice_refno"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            customer_contactperson = dt["customer_contactperson"].ToString(),
                            invoice_reference = dt["invoice_reference"].ToString(),
                            //Details = dt["Details"].ToString(),
                            invoice_from = dt["invoice_from"].ToString(),
                            mail_status = dt["mail_status"].ToString(),
                            invoice_amount = dt["invoice_amount"].ToString(),
                            invoice_status = dt["status"].ToString(),
                        });
                        values.einvoicesummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Invoice!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }

    }
}
