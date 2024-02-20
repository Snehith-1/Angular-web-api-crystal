using ems.sales.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;


namespace ems.sales.DataAccess
{
    public class DaSmrTrnDeliveryorderSummary
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;





        public void DaGetSmrTrnDeliveryorderSummary (MdlSmrTrnDeliveryorderSummary values)
        {
            try {
               
                msSQL = " select distinct a.directorder_gid,s.branch_name,cast(concat(c.so_referenceno1," +
                " if(c.so_referencenumber<>'',concat(' | ',c.so_referencenumber),'') ) as char)as so_referenceno1," +
                " directorder_refno, directorder_date, n.user_firstname, a.dc_no,a.salesorder_gid, " +
                " a.customer_name, customer_branchname, customer_contactperson, directorder_status,delivery_status, " +
                " concat(CAST(date_format(delivered_date,'%d-%m-%Y') as CHAR),'/',delivered_to) as delivery_details, " +
                " case when a.customer_contactnumber is null then concat(e.customercontact_name,'/',e.mobile,'/',e.email) " +
                " when a.customer_contactnumber is not null then concat(a.customer_contactperson,' / ',a.customer_contactnumber,' / ',a.customer_emailid) end as contact" +
                " from smr_trn_tdeliveryorder a " +
                " inner join crm_mst_tcustomercontact e on e.customer_gid = a.customer_gid " +
                " inner join hrm_mst_temployee m on m.employee_gid=a.created_name " +
                " left join hrm_mst_tbranch s on s.branch_gid=a.customerbranch_gid " +
                " inner join adm_mst_tuser n on n.user_gid= m.user_gid " +
                " left join smr_trn_tdeliveryorderdtl b on a.directorder_gid =b.directorder_gid " +
                " left join smr_trn_tsalesorder c on a.salesorder_gid=c.salesorder_gid " +
                " where dc_type<>'Direct DC' "+
                " order by a.directorder_date DESC,directorder_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<deliveryorder_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new deliveryorder_list
                    { 
                        
                        directorder_date = dt["directorder_date"].ToString(),
                        directorder_refno = dt["directorder_refno"].ToString(),
                        so_referenceno1 = dt["so_referenceno1"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        contact = dt["contact"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        salesorder_status = dt["salesorder_status"].ToString(),

                    });
                    values.deliveryorder_list = getModuleList;
                }
            }
            dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
        public void DaGetSmrTrnAddDeliveryorderSummary( MdlSmrTrnDeliveryorderSummary values)
        {
            try {
               
                msSQL = " select distinct a.salesorder_gid,y.branch_name, cast(concat(a.so_referenceno1," +
               " if(a.so_referencenumber<>'',concat(' | ',a.so_referencenumber),'') ) as char)as so_referenceno1, date_format(a.salesorder_date,'%d-%m-%Y') as salesorder_date, " +
                " sum(b.qty_quoted) as qty_quoted,sum(b.product_delivered) as product_delivered," +
                " a.customer_name,  a.customer_contact_person, a.salesorder_status,c.mobile, " +
                " a.despatch_status, " +
                " case when a.customer_email is null then concat(c.customercontact_name,'/',c.mobile,'/',c.email) " +
                " when a.customer_email is not null then concat(a.customer_contact_person,' / ',a.customer_mobile,' / ',a.customer_email) end as contact " +
                " from smr_trn_tsalesorder a " +
                " left join smr_trn_tsalesorderdtl b on b.salesorder_gid = a.salesorder_gid " +
                " left join crm_mst_tcustomercontact c on c.customer_gid=a.customer_gid " +
                " left join hrm_mst_tbranch y on y.branch_gid=a.branch_gid" +
                 " group by salesorder_gid " +
                 " having(qty_quoted <> product_delivered)  order by a.salesorder_date desc, a.customer_name desc ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<adddeliveryorder_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new adddeliveryorder_list
                    {
                        salesorder_date = dt["salesorder_date"].ToString(),
                        salesorder_gid = dt["salesorder_gid"].ToString(),
                        so_referenceno1 = dt["so_referenceno1"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        contact = dt["contact"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        salesorder_status = dt["salesorder_status"].ToString(),
                      
                    });
                    values.adddeliveryorder_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Add DeliveryorderSummary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

    }
}