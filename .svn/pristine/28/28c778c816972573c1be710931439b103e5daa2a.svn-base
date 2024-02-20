using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.inventory.Models;
using System.Globalization;


namespace ems.inventory.DataAccess
{
    public class DaImsTrnDeliveryAcknowledgement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsuom_gid, lsbranch_gid, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetImsTrnDeliveryAcknowledgementSummary(MdlImsTrnDeliveryAcknowledgement values)
        {
            try
            {
                msSQL = " select directorder_gid, directorder_refno, DATE_FORMAT(a.directorder_date, '%d-%m-%Y') as directorder_date, " +
                " customer_name, customer_branchname, customer_contactperson, directorder_status,delivery_status, " +
                " concat(CAST(date_format(delivered_date,'%d-%m-%Y') as CHAR),' / ',delivered_to) as delivery_details, " +
                " concat(a.customer_contactperson,' / ',a.customer_contactnumber,' / ',a.customer_emailid) as contact, " +
                " a.delivered_by,a.delivered_remarks from smr_trn_tdeliveryorder a " +
                " where delivery_status='Delivery Completed' group by a.directorder_gid order by date(a.directorder_date) desc,a.directorder_date asc, a.directorder_gid desc ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<deliverysummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new deliverysummary_list
                    {
                        directorder_gid = dt["directorder_gid"].ToString(),
                        directorder_refno = dt["directorder_refno"].ToString(),
                        directorder_date = dt["directorder_date"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_branchname = dt["customer_branchname"].ToString(),
                        customer_contactperson = dt["customer_contactperson"].ToString(),
                        directorder_status = dt["directorder_status"].ToString(),
                        delivery_status = dt["delivery_status"].ToString(),
                        delivery_details = dt["delivery_details"].ToString(),
                        contact = dt["contact"].ToString(),
                        delivered_by = dt["delivered_by"].ToString(),
                        delivered_remarks = dt["delivered_remarks"].ToString(),
                       
                    });
                    values.deliverysummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Delivery Acknowledgement Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +  ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetImsTrnDeliveryAcknowledgementAdd(MdlImsTrnDeliveryAcknowledgement values)
        {
            try
            {
                msSQL = " select directorder_gid, directorder_refno, date_format(directorder_date,'%d-%m-%Y') as directorder_date, " +
                " customer_name, customer_branchname, customer_contactperson, directorder_status,delivery_status, " +
                " concat(CAST(date_format(delivered_date,'%d-%m-%Y') as CHAR),'/',delivered_to) as delivery_details, " +
                " case when a.customer_contactnumber is null then  concat(c.customercontact_name,'/',c.mobile,'/',c.email) " +
                " when a.customer_contactnumber is not null then concat(a.customer_contactperson,'/',a.customer_contactnumber,'/',a.customer_emailid) end as contact " +
                "  from smr_trn_tdeliveryorder a " +
                 " left join crm_mst_tcustomercontact c on c.customer_gid=a.customer_gid " +
                " where  delivery_status='Delivery Pending' group by directorder_gid DESC";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<deliveryadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new deliveryadd_list
                    {
                        directorder_gid = dt["directorder_gid"].ToString(),
                        directorder_refno = dt["directorder_refno"].ToString(),
                        directorder_date = dt["directorder_date"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_branchname = dt["customer_branchname"].ToString(),
                        customer_contactperson = dt["customer_contactperson"].ToString(),
                        directorder_status = dt["directorder_status"].ToString(),
                        delivery_status = dt["delivery_status"].ToString(),
                        delivery_details = dt["delivery_details"].ToString(),
                        contact = dt["contact"].ToString(),
                        

                    });
                    values.deliveryadd_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Adding Delivery Acknowledgement!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // Add page
        public void DaGetDeliveryAcknowledgeUpdate(string directorder_gid, MdlImsTrnDeliveryAcknowledgement values)
        {
            try
            {               
                msSQL = "select a.directorder_refno," +
                    " date_format(a.directorder_date,'%d/%m/%Y') as directorder_date, " +
                    "a.customer_name," +
                    "a.customer_branchname ," +
                    "a.customer_contactperson," +
                    "a.customer_contactnumber," +
                    " a.customer_address,  " +
                    " a.directorder_remarks," +
                    " a.terms_condition, " +
                    " a.Landline_no, " +
                    " a.customer_department,  " +
                    " a.grandtotal_amount,  " +
                    " a.addon_amount,  " +
                    " case when a.customer_contactnumber is null then b.mobile when a.customer_contactnumber is not null then a.customer_contactnumber end as mobile, " +
                    " a.customer_emailid,b.designation " +
                    " from smr_trn_tdeliveryorder a " +
                    " left join crm_mst_tcustomercontact b on b.customer_gid=a.customer_gid " +
                    " where directorder_gid = '" + directorder_gid + "' group by directorder_gid ASC ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<deliverycus_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new deliverycus_list
                    {

                        directorder_refno = dt["directorder_refno"].ToString(),
                        directorder_date = dt["directorder_date"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_branchname = dt["customer_branchname"].ToString(),
                        customer_contactperson = dt["customer_contactperson"].ToString(),
                        customer_contactnumber = dt["customer_contactnumber"].ToString(),
                        customer_address = dt["customer_address"].ToString(),
                        directorder_remarks = dt["directorder_remarks"].ToString(),
                        terms_condition = dt["terms_condition"].ToString(),
                        Landline_no = dt["Landline_no"].ToString(),
                        customer_department = dt["customer_department"].ToString(),
                        grandtotal_amount = dt["grandtotal_amount"].ToString(),
                        addon_amount = dt["addon_amount"].ToString(),
                        mobile = dt["mobile"].ToString(),
                        customer_emailid = dt["customer_emailid"].ToString(),
                        designation = dt["designation"].ToString(),


                    });
                    values.deliverycus_list = getModuleList;
                }
            }

            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Delivery Acknowledgement !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }           
        }

        public void DaGetDeliveryAcknowledgeUpdateProd(string directorder_gid, MdlImsTrnDeliveryAcknowledgement values)
        {
            try
            {
                
                msSQL = "select a.productgroup_name," +
                " b.product_code, " +
                " a.product_name," +
                " a.product_description," +
                " a.productuom_name," +
                " format(a.product_qty,2) as product_qty, " +
                " format(a.product_qtydelivered,2) as product_qtydelivered, " +
                " format(a.discount_amount,2) as discount_amount, " +
                " format(a.tax_amount,2) as tax_amount, " +
                " format(a.product_total,2) as product_total " +
                " from smr_trn_tdeliveryorderdtl a " +
                " left join pmr_mst_tproduct b on a.product_gid= b.product_gid " +
                " where directorder_gid = '" + directorder_gid + "' order by directorder_gid asc  ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<deliverycusprod_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new deliverycusprod_list
                    {

                        productgroup_name = dt["productgroup_name"].ToString(),
                        product_code = dt["product_code"].ToString(),
                        product_name = dt["product_name"].ToString(),
                        product_description = dt["product_description"].ToString(),
                        productuom_name = dt["productuom_name"].ToString(),
                        product_qty = dt["product_qty"].ToString(),
                        product_qtydelivered = dt["product_qtydelivered"].ToString(),
                        discount_amount = dt["discount_amount"].ToString(),
                        tax_amount = dt["tax_amount"].ToString(),
                        product_total = dt["product_total"].ToString(),
                        

                    });
                    values.deliverycusprod_list = getModuleList;
                }
            }

            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Delivery Acknowledgement Update !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        public void DaPostDeliveryAckSubmit( postdelivery_list values)
        {
            string uiDateStr = values.delivery_date;
            DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string delivery_date = uiDate.ToString("yyyy-MM-dd");
            try
            {
               
                msSQL = "update smr_trn_tdeliveryorder set delivered_to='" + values.delivery_to + "', " +
            " delivered_date='" + delivery_date + "',delivery_status='Delivery Completed', " +
            "delivered_by='" + values.delivery_by + "',delivered_remarks='" + values.remarks + "' " +
                "where directorder_gid='" + values.directorder_gid + "'";
            mnResult = mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Delivery order Acknowledgement Raised Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error while Raising Delivery order Acknowledgement";
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Submmitting Delivery Order Acknowledgement !";
            }
           
        }
    }
}
   
