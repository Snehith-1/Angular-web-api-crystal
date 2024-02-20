using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.inventory.Models;
using System.Windows.Media.Media3D;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Configuration;


namespace ems.inventory.DataAccess
{
    public class DaImsTrnOpenDcSummary
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsproduct_qty, msstockGID, lsproduct_desc, lsreference_gid, lssendername, msGetGID, lsbranch, lssender_contactnumber, lssenderdesignation, mssalesorderGID, lsuom_gid, lsbranch_gid, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaGetImsTrnOpenDeliveryOrderSummary(MdlImsTrnOpenDCSummary values)
        {
            try
            {

                msSQL = " select directorder_gid, directorder_refno, date_format(directorder_date, '%d-%b-%Y') as directorder_date,n.user_firstname, " +
               " customer_name, customer_branchname, customer_contactperson,a.created_by, directorder_status,delivery_status, " +
               " concat(CAST(date_format(delivered_date,'%d-%m-%Y') as CHAR),'/',delivered_to) as delivery_details, " +
               " concat(a.customer_contactperson,' / ',a.customer_contactnumber,' / ',a.customer_emailid)  as contact " +
               " from smr_trn_tdeliveryorder a " +
               " left join hrm_mst_temployee m on m.employee_gid=a.created_name " +
               " left join adm_mst_tuser n on n.user_gid= m.user_gid " +
               " where dc_type='Actual DC' order by date(a.directorder_date) desc,a.directorder_date asc, a.directorder_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<opndcsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new opndcsummary_list
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
                            user_firstname = dt["user_firstname"].ToString(),
                            created_by = dt["created_by"].ToString()

                        });
                        values.opndcsummary_list = getModuleList;
                    }
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Open Delivery Order Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }
        public void DaGetImsTrnOpenDcAddSummary(MdlImsTrnOpenDCSummary values)
        {
            try
            {

                msSQL = "select distinct a.salesorder_gid, a.so_referenceno1, date_format(a.salesorder_date,'%d-%m-%Y') as salesorder_date, " +
               " sum(b.qty_quoted) as qty_quoted,sum(b.product_delivered) as product_delivered," +
               " a.customer_name,  a.customer_contact_person, a.salesorder_status,c.mobile, " +
               " a.despatch_status, " +
               " case when a.customer_email is null then concat(c.customercontact_name,'/',c.mobile,'/',c.email) " +
               " when a.customer_email is not null then concat(a.customer_contact_person,' / ',a.customer_mobile,' / ',a.customer_email) end as contact " +
               " from smr_trn_tsalesorder a " +
               " left join smr_trn_tsalesorderdtl b on b.salesorder_gid = a.salesorder_gid " +
               " left join crm_mst_tcustomercontact c on c.customer_gid=a.customer_gid " +
               " where a.salesorder_status not in ('Approve Pending','SO Amended','Rejected','Canceled') " +
               " and so_type='Sales'  group by salesorder_gid " +
               " having(qty_quoted <> product_delivered)  order by a.salesorder_date desc, a.customer_name desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<opendcadd_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new opendcadd_list
                        {
                            salesorder_gid = dt["salesorder_gid"].ToString(),
                            so_referenceno1 = dt["salesorder_gid"].ToString(),
                            salesorder_date = dt["salesorder_date"].ToString(),
                            qty_quoted = dt["qty_quoted"].ToString(),
                            product_delivered = dt["product_delivered"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            customer_contact_person = dt["customer_contact_person"].ToString(),
                            salesorder_status = dt["salesorder_status"].ToString(),
                            mobile = dt["mobile"].ToString(),
                            despatch_status = dt["despatch_status"].ToString(),
                            contact = dt["contact"].ToString(),


                        });
                        values.opendcadd_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Open DC Add !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }

        // Add page
        public void DaGetOpenDcUpdate(string salesorder_gid, MdlImsTrnOpenDCSummary values)
        {
            try
            {

                 msSQL = " select d.directorder_date,d.directorder_refno,a.salesorder_gid,a.salesorder_date,a.termsandconditions,b.customer_gid,b.customer_code,format(a.grandtotal,2) as grandtotal, b.customer_name," +
                    " concat(b.customer_address,b.customer_address2,b.customer_city,b.customer_state,b.customer_pin) as customer_address," +
                    " c.designation,c.customercontact_name,c.email,c.mobile,a.currency_code,a.shipping_to, " +
                    "  a.customer_mobile,a.customer_email,a.customer_address as customer_address_so, " +
                    " a.customer_contact_person,a.shipping_to from smr_trn_tsalesorder a" +
                    " left join crm_mst_tcustomer b on b.customer_gid=a.customer_gid " +
                    " left join crm_mst_tcustomercontact c on c.customer_gid=a.customer_gid " +
                    " left join smr_trn_tdeliveryorder d on a.salesorder_gid= d.salesorder_gid " +
                    " where a.salesorder_gid='" + salesorder_gid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<opendcaddsel_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new opendcaddsel_list
                        {

                            salesorder_gid = dt["salesorder_gid"].ToString(),
                            salesorder_date = dt["salesorder_date"].ToString(),
                            termsandconditions = dt["termsandconditions"].ToString(),
                            customer_gid = dt["customer_gid"].ToString(),
                            customer_code = dt["customer_code"].ToString(),
                            grandtotal = dt["grandtotal"].ToString(),
                            customer_address = dt["customer_address"].ToString(),
                            designation = dt["designation"].ToString(),
                            customercontact_name = dt["customercontact_name"].ToString(),
                            email = dt["email"].ToString(),
                            mobile = dt["mobile"].ToString(),
                            currency_code = dt["currency_code"].ToString(),
                            shipping_to = dt["shipping_to"].ToString(),
                            customer_mobile = dt["customer_mobile"].ToString(),
                            customer_email = dt["customer_email"].ToString(),
                            customer_address_so = dt["customer_address_so"].ToString(),
                            customer_contact_person = dt["customer_contact_person"].ToString(),
                            directorder_refno = dt["directorder_refno"].ToString(),
                            directorder_date = dt["directorder_date"].ToString(),


                        });
                        values.opendcaddsel_list = getModuleList;

                    }
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Open Dc !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }

        public void DaGetOpenDcUpdateProd(string salesorder_gid, MdlImsTrnOpenDCSummary values)
        {
            try
            {

                msSQL = " select a.salesorderdtl_gid,a.salesorder_gid,a.product_gid,z.productgroup_gid,z.productgroup_name,a.product_name,a.uom_gid,a.uom_name,a.qty_quoted," +
                " a.display_field,a.product_delivered,format(a.product_price,2) as product_price, a.discount_percentage,format(a.discount_amount,2) as discount_amount," +
                " format(a.tax_amount,2) as tax_amount,format(a.tax_amount2,2) as tax_amount2,format(a.tax_amount3,2) as tax_amount3, " +
                " a.tax_name,a.tax_name2,a.tax_name3,format(a.price,2) as price,b.stockable, " +
                " (select ifnull(sum(m.stock_qty)+sum(m.amend_qty)-sum(m.damaged_qty)-sum(m.issued_qty)-sum(m.transfer_qty),0) as available_quantity from " +
                " ims_trn_tstock m where m.stock_flag='Y' and m.product_gid=a.product_gid and m.branch_gid='" + lsbranch_gid + "' and " +
                " m.uom_gid=a.uom_gid) as available_quantity,b.serial_flag,b.branch_gid, " +
                " a.tax1_gid,a.tax2_gid,a.tax3_gid " +
                " from smr_trn_tsalesorderdtl a " +
                " left join smr_trn_tdeliveryorderdtl z on z.product_gid=a.product_gid" +
                " left join pmr_mst_tproduct b on a.product_gid=b.product_gid " +
                " where a.salesorder_gid = '" + salesorder_gid + "'" +
                " group by salesorderdtl_gid order by salesorderdtl_gid asc  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<opendcaddselprod_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new opendcaddselprod_list
                        {

                            salesorderdtl_gid = dt["salesorderdtl_gid"].ToString(),
                            salesorder_gid = dt["salesorder_gid"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            uom_gid = dt["uom_gid"].ToString(),
                            uom_name = dt["uom_name"].ToString(),
                            qty_quoted = dt["qty_quoted"].ToString(),
                            display_field = dt["display_field"].ToString(),
                            product_delivered = dt["product_delivered"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            discount_percentage = dt["discount_percentage"].ToString(),
                            discount_amount = dt["discount_amount"].ToString(),
                            tax_amount = dt["tax_amount"].ToString(),
                            tax_amount2 = dt["tax_amount2"].ToString(),
                            tax_amount3 = dt["tax_amount3"].ToString(),
                            tax_name = dt["tax_name"].ToString(),
                            tax_name2 = dt["tax_name2"].ToString(),
                            tax_name3 = dt["tax_name3"].ToString(),
                            tax1_gid = dt["tax_name"].ToString(),
                            tax2_gid = dt["tax_name2"].ToString(),
                            tax3_gid = dt["tax_name3"].ToString(),
                            price = dt["price"].ToString(),
                            stockable = dt["stockable"].ToString(),
                            available_quantity = dt["available_quantity"].ToString(),
                            serial_flag = dt["serial_flag"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),


                        });
                        values.opendcaddselprod_list = getModuleList;
                    }
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Opening DC Product !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }

        // Submit Event
        public void DaPostOpenDcSubmit(string employee_gid, opendc_list values)
        {
            try
            {

                if (Convert.ToDouble(values.despatch_quantity) <= Convert.ToDouble(values.available_quantity))
                {
                    values.message = "Sum of the despatch quantity and delivered quantity must be less than or equal to the ordered quantity";
                }

                msSQL = " select * from hrm_mst_temployee where employee_gid='" + employee_gid + "' ";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows)
                {
                    lssendername = objOdbcDataReader["employee_gid"].ToString();
                    lssenderdesignation = objOdbcDataReader["designation_gid"].ToString();
                    lssender_contactnumber = objOdbcDataReader["employee_mobileno"].ToString();

                }



                msSQL = "SELECT salesorderdtl_gid  FROM smr_trn_tsalesorderdtl WHERE product_name = '" + values.product_name + "'";
                string lssalesorderdtlgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "SELECT salesorder_gid  FROM smr_trn_tsalesorderdtl WHERE salesorderdtl_gid = '" + lssalesorderdtlgid + "'";
                string lssalesordergid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "SELECT customer_gid  FROM smr_trn_tsalesorder WHERE salesorder_gid = '" + lssalesordergid + "'";
                string lscustomergid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "SELECT branch_gid  FROM smr_trn_tsalesorder WHERE salesorder_gid = '" + lssalesordergid + "'";
                string lsbranch = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "SELECT customer_name FROM crm_mst_tcustomer WHERE customerbranch_name = '" + lscustomergid + "'";
                string lscustomername = objdbconn.GetExecuteScalar(msSQL);

                try { 
                mssalesorderGID = objcmnfunctions.GetMasterGID("VDOP");

                msSQL = " insert into smr_trn_tdeliveryorder (" +
                " directorder_gid, " +
                " directorder_date," +
                " directorder_refno, " +
                " salesorder_gid, " +
                " customer_gid, " +
                " customer_name , " +
                " customerbranch_gid, " +
                " customer_branchname, " +
                " customer_contactperson, " +
                " customer_contactnumber, " +
                " customer_address, " +
                " directorder_status, " +
                " terms_condition, " +
                " created_date, " +
                " created_name, " +
                " sender_name," +
                " sender_designation," +
                " sender_contactnumber, " +
                " grandtotal_amount, " +
                 " shipping_to, " +
                   " dc_type, " +
                " customer_emailid " +
                " ) values (" +
                "'" + mssalesorderGID + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                "'" + mssalesorderGID + "'," +
                "'" + lssalesordergid + "'," +
                "'" + lscustomergid + "'," +
                "'" + lscustomername + "'," +
                "'" + lsbranch + "'," +
                "'" + values.customer_code + "'," +
                "'" + values.customer_contact_person + "'," +
                "'" + values.customer_mobile + "'," +
                " '" + values.customer_address_so + "'," +
                "'Despatch Done'," +
                "'" + values.termsandconditions + "', " +
                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                "'" + employee_gid + "'," +
                "'" + lssendername + "'," +
                "'" + lssenderdesignation + "'," +
                "'" + lssender_contactnumber + "',";
                if (values.grandtotal == null || values.grandtotal == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.grandtotal + "',";
                }
                msSQL += "'" + values.shipping_to + "'," +
                     "'Direct DC'," +
                    "'" + values.customer_email + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult == 0)
                {
                    values.status = false;
                    values.message = "Error occurred while inserting records!";
                }
            }
            catch (Exception ex)
            {
                values.message = "Error occurred while inserting records in Open DC !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
                try
                {
                    msGetGID = objcmnfunctions.GetMasterGID("VDDC");

                    msSQL = "update smr_trn_tsalesorderdtl set product_delivered='" + values.available_quantity + "' where salesorderdtl_gid='" + lssalesorderdtlgid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " SELECT productgroup_gid FROM pmr_mst_tproduct WHERE  product_name='" + values.product_name + "' ";
                    string lsproductgroupgid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " SELECT product_gid FROM pmr_mst_tproduct WHERE product_name='" + values.product_name + "' ";
                    string lsproductgid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " SELECT productuom_gid FROM pmr_mst_tproductuom WHERE productuom_name='" + values.uom_name + "' ";
                    string lsproductuomgid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " insert into smr_trn_tdeliveryorderdtl (" +
                                    " directorderdtl_gid, " +
                                    " directorder_gid, " +
                                    " productgroup_gid, " +
                                    " productgroup_name, " +
                                    " product_gid," +
                                    " product_name, " +
                                    " product_uom_gid, " +
                                    " productuom_name, " +
                                    " product_qty, " +
                                    " product_description, " +
                                    " product_price, " +
                                    " discount_percentage, " +
                                    " discount_amount, " +
                                    " tax_name, " +
                                    " tax_name2, " +
                                    " product_total, " +
                                    " tax_name3, " +
                                     " dc_no, " +
                                    " mode_of_despatch, " +
                                    " tracker_id, " +
                                    " tax_amount, " +
                                    " tax_amount2, " +
                                    " tax_amount3, " +
                                    " tax1_gid, " +
                                    " tax2_gid, " +
                                    " tax3_gid, " +
                                    " product_qtydelivered, " +
                                    " salesorderdtl_gid " +
                                    "  ) " +
                                    " values ( " +
                                    "'" + msGetGID + "', " +
                                    "'" + mssalesorderGID + "'," +
                                    "'" + lsproductgroupgid + "', " +
                                    "'" + values.productgroup_name + "', " +
                                    "'" + lsproductgid + "', " +
                                    "'" + values.product_name + "', " +
                                    "'" + lsproductuomgid + "', " +
                                    "'" + values.uom_name + "'," +
                                     "'" + values.qty_quoted + "', " +
                                    "'" + values.display_field + "',";
                    if (values.price == null || values.price == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.price + "',";
                    }
                    if (values.discount_percentage == null || values.discount_percentage == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.discount_percentage + "',";
                    }
                    if (values.discount_amount == null || values.discount_amount == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.discount_amount + "',";
                    }
                    msSQL += "'" + values.tax_name + "'," +
                                     "'" + values.tax_name2 + "',";
                    if (values.total_amount == null || values.total_amount == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.total_amount + "',";
                    }
                    msSQL += "'" + values.tax_name3 + "'," +
                                    "'" + values.dc_no + "'," +
                                    "'" + values.despatch_mode + "'," +
                                    "'" + values.tracker_id + "',";
                    if (values.tax_amount == null || values.tax_amount == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.tax_amount + "',";
                    }

                    if (values.tax_amount2 == null || values.tax_amount2 == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.tax_amount2 + "',";
                    }

                    if (values.tax_amount3 == null || values.tax_amount3 == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.tax_amount3 + "',";
                    }
                    msSQL += "'" + values.tax1_gid + "'," +
                                       "'" + values.tax2_gid + "'," +
                                        "'" + values.tax3_gid + "'," +
                                    "'" + values.available_quantity + "'," +
                                    "'" + lssalesorderdtlgid + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msSQL = " select directorderdtl_gid, directorder_gid, productgroup_gid, productgroup_name, " +
                                      " product_gid, product_name, product_uom_gid, productuom_name, product_qty, " +
                                      " product_description, product_price, discount_percentage, discount_amount, " +
                                      " tax_name, tax_name2, tax_name3, tax_percentage, tax_percentage2, tax_percentage3, " +
                                      " tax_amount, tax_amount2, tax_amount3, product_total, total_amount, product_qtydelivered, dc_no, " +
                                        " mode_of_despatch, tracker_id, qty_returned, tax1_gid, tax2_gid, tax3_gid, " +
                                        " product_remarks, salesorderdtl_gid, warranty_date, created_by, created_date," +
                                        " updated_by, updated_date from smr_trn_tdeliveryorderdtl " +
                                        " where directorder_gid='" + mssalesorderGID + "' ";

                        objOdbcDataReader = objdbconn.GetDataReader(msSQL);

                        if (objOdbcDataReader.HasRows)
                        {
                            lsproduct_qty = objOdbcDataReader["product_qty"].ToString();
                            lsreference_gid = objOdbcDataReader["directorder_gid"].ToString();
                            lsproduct_desc = objOdbcDataReader["product_description"].ToString();
                        }

                        msstockGID = objcmnfunctions.GetMasterGID("ISKP");

                        msSQL = " insert into ims_trn_tstock(" +
                             " stock_gid," +
                             " branch_gid," +
                             " product_gid," +
                             " uom_gid," +
                             " created_by," +
                             " created_date," +
                             " unit_price," +
                             " stock_qty," +
                             " reference_gid, " +
                             " stock_flag, " +
                             " display_field " +
                             " ) values (" +
                             "'" + msstockGID + "'," +
                             "'" + lsbranch + "'," +
                             "'" + lsproductgid + "'," +
                             "'" + lsproductuomgid + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', ";
                        if (values.price == null || values.price == "")
                        {
                            msSQL += "'0.00',";
                        }
                        else
                        {
                            msSQL += "'" + values.price + "',";
                        }
                        msSQL += "'" + lsproduct_qty + "'," +
                             "'" + lsreference_gid + "'," +
                             "'Y'," +
                             "'" + lsproduct_desc + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }

                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Stock Details Added Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Stock Details";

                    }
                
                 }
            catch (Exception ex)
            {
                values.message = "Exception occured While Adding Stock Details in Open DC !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
            catch (Exception ex)
            {
                values.message = "Exception occured while Submitting Open DC !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }

        //download Report files
        public Dictionary<string, object> DaGetOpenDCRpt(string directorder_gid, MdlImsTrnOpenDCSummary values)
        {

            OdbcConnection myConnection = new OdbcConnection();
            myConnection.ConnectionString = objdbconn.GetConnectionString();
            OdbcCommand MyCommand = new OdbcCommand();
            MyCommand.Connection = myConnection;
            DataSet myDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();
            Fnazurestorage objFnazurestorage = new Fnazurestorage();

            msSQL = " select a.directorder_gid,date_format(a.directorder_date,'%d-%m-%Y') as directorder_date,a.directorder_refno," +
                                 " a.directorder_remarks,a.terms_condition,format(a.product_grandtotal,2) as product_grandtotal, " +
                                 " format(a.addon_amount,2) as addon_amount,format(a.addon_discount,2) as addon_discount, " +
                                 " format(a.grandtotal_amount,2) as grandtotal_amount,k.so_referenceno1 as customerpo_no, " +
                                 " so_referencenumber as customerpo_no, a.shipping_to as customer_address2," +
                                 " b.customer_name,a.dc_type, a.dc_no, a.mode_of_despatch, a.tracker_id," +
                                 " a.customer_address,a.customer_contactperson as DataColumn22, a.customer_emailid as email," +
                                 " a.customer_contactnumber as  mobile " +
                                 " from smr_trn_tdeliveryorder a " +
                                 " left join smr_trn_tsalesorder k on k.salesorder_gid=a.salesorder_gid " +
                                 " left join crm_mst_tcustomer b on b.customer_gid=a.customer_gid " +
                                 " left join crm_mst_tcustomercontact c on c.customer_gid=b.customer_gid " +
                                 " where a.directorder_gid='" + directorder_gid + "' group by a.directorder_gid ";


            MyCommand.CommandText = msSQL;
            MyCommand.CommandType = System.Data.CommandType.Text;
            MyDA.SelectCommand = MyCommand;
            myDS.EnforceConstraints = false;
            MyDA.Fill(myDS, "DataTable1");

            msSQL = " select a.directorderdtl_gid,a.directorder_gid,a.productuom_name,a.product_qty,a.product_price, if(f.customerproduct_code='&nbsp;',' ',f.customerproduct_code) as customerproduct_code, " +
                    " a.product_description, a.product_qtydelivered, a.tax_name,a.tax_name2,a.tax_name3, sum(product_qtydelivered) as sumqtytotal," +
                    " d.dc_no,d.mode_of_despatch,d.tracker_id,a.product_code,c.productgroup_name,a.product_name, " +
                    " a.directorderdtl_gid as tax_amount,a.directorderdtl_gid as tax_amount2,e.design_no,e.color_name, " +
                    " a.directorderdtl_gid as tax_amount3, " +
                    " a.directorderdtl_gid as total_tax_amount " +
                    " from smr_trn_tdeliveryorderdtl a " +
                    " left join pmr_mst_tproduct b on b.product_gid=a.product_gid " +
                    " left join pmr_mst_tproductgroup c on b.productgroup_gid=c.productgroup_gid " +
                    " left join smr_trn_tdeliveryorder d on d.directorder_gid=a.directorder_gid " +
                    " left join acp_trn_torderdtl e on e.salesorder_gid=d.salesorder_gid and a.product_gid=e.product_gid " +
                    " left join smr_trn_tsalesorderdtl f on f.salesorderdtl_gid = a.salesorderdtl_gid " +
                    " where a.directorder_gid='" + directorder_gid + "' group by directorderdtl_gid order by directorderdtl_gid asc ";

            MyCommand.CommandText = msSQL;
            MyCommand.CommandType = System.Data.CommandType.Text;
            MyDA.SelectCommand = MyCommand;
            myDS.EnforceConstraints = false;
            MyDA.Fill(myDS, "DataTable2");

            msSQL = "select a.branch_name,a.address1,a.city,a.state,a.postal_code,a.contact_number,a.email as email_address,a.gst_no, " +
                                  "a.branch_gid,a.branch_logo from hrm_mst_tbranch a " +
                                  "left join hrm_mst_temployee b on a.branch_gid=b.branch_gid " +
                                  "left join smr_trn_tdeliveryorder c on c.created_name=b.employee_gid " +
                                  "where c.directorder_gid='" + directorder_gid + "'";


            MyCommand.CommandText = msSQL;
            MyCommand.CommandType = System.Data.CommandType.Text;
            MyDA.SelectCommand = MyCommand;
            myDS.EnforceConstraints = false;
            MyDA.Fill(myDS, "DataTable3");

            msSQL = " select a.salesorder_gid,b.salesorder_gid,b.directorder_gid,a.design_no from acp_trn_torderdtl a " +
                       " left join smr_trn_tdeliveryorder b on b.salesorder_gid = a.salesorder_gid " +
                       " where b.directorder_gid='" + directorder_gid + "'";

            MyCommand.CommandText = msSQL;
            MyCommand.CommandType = System.Data.CommandType.Text;
            MyDA.SelectCommand = MyCommand;
            myDS.EnforceConstraints = false;
            MyDA.Fill(myDS, "DataTable4");

            ReportDocument oRpt = new ReportDocument();
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path_inventory"].ToString(), "ImsCrpDeliveryOrder.rpt"));
            oRpt.SetDataSource(myDS);
            string path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "Delivery Order_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            myConnection.Close();

            var ls_response = objFnazurestorage.reportStreamDownload(path);
            File.Delete(path);
            return ls_response;

        }

    }
}