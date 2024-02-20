using ems.einvoice.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Web;
using static OfficeOpenXml.ExcelErrorValue;
using File = System.IO.File;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Globalization;

namespace ems.einvoice.DataAccess
{
    public class DaEinvoice
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, msINGetGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid2, msGetGid, msGetGid3, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaeinvoiceSummary(Mdlinvoicesummary values)
        {
            try
            {

                msSQL = " select distinct a.irn,a.created_date, a.invoice_gid,a.invoice_refno,a.customer_gid,a.irn,a.invoice_date, a.invoice_reference,a.mail_status," +
                    " a.additionalcharges_amount,a.discount_amount,format(a.invoice_amount, 2) as invoice_amount," +
                    " case when a.customer_contactnumber is null then concat(a.customer_contactperson,' / ',a.customer_contactnumber) else concat(a.customer_contactperson, " +
                    " if (a.customer_email = '',' ',concat(' / ', a.customer_email))) end as customer_contactperson,  " +
                    " case when a.currency_code = 'INR' then a.customer_name when a.currency_code is null then a.customer_name     " +
                    " when a.currency_code is not null and a.currency_code <> 'INR' then concat(a.customer_name) end as customer_name," +
                    " a.currency_code,  a.customer_contactnumber as mobile,a.invoice_from,  " +
                    " case when irn is null then 'IRN Pending' when a.irncancel_date is not null then 'IRN Cancelled'" +
                    " when a.creditnote_status='Y' then 'Credit Note Raised' when a.irn is not null then 'IRN Generated' else 'Invoice Raised' end as status" +
                    " from rbl_trn_tinvoice a group by a.invoice_refno order by  a.created_date desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<invoicesummary_list>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new invoicesummary_list
                        {
                            irn = dt["irn"].ToString(),
                            invoice_gid = dt["invoice_gid"].ToString(),
                            invoice_date = Convert.ToDateTime(dt["invoice_date"].ToString()),
                            invoice_refno = dt["invoice_refno"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            customer_contactperson = dt["customer_contactperson"].ToString(),
                            invoice_reference = dt["invoice_reference"].ToString(),
                            invoice_from = dt["invoice_from"].ToString(),
                            mail_status = dt["mail_status"].ToString(),
                            invoice_amount = dt["invoice_amount"].ToString(),
                            invoice_status = dt["status"].ToString(),
                        });
                        values.invoicesummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Invoice Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetProductNamedropDown(Mdlinvoicesummary values)
        {
            try
            {

                msSQL = " select product_name, product_gid, product_code from pmr_mst_tproduct ";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<Getproductnamedropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getproductnamedropdown
                        {
                            productgid = dt["product_gid"].ToString(),
                            productname = dt["product_name"].ToString(),
                            productcode = dt["product_code"].ToString(),

                        });
                        values.Getproductnamedropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product name!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        public void DaGetCustomerNamedropDown(Mdlinvoicesummary values)
        {
            try
            {

                msSQL = " select customer_name, customer_gid from crm_mst_tcustomer ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetCustomernamedropdown>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetCustomernamedropdown
                        {
                            customergid = dt["customer_gid"].ToString(),
                            customername = dt["customer_name"].ToString(),
                        });
                        values.GetCustomernamedropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Customer name!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetCustomer360dropdowm(Mdlinvoicesummary values, string leadbank_gid)
        {
            try
            {
                msSQL = "Select customer_gid from crm_trn_tleadbank where leadbank_gid='" + leadbank_gid + "' ";
                string lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select customer_name, customer_gid from crm_mst_tcustomer where customer_gid='" + lscustomer_gid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetCustomernamedropdown>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetCustomernamedropdown
                        {
                            customergid = dt["customer_gid"].ToString(),
                            customername = dt["customer_name"].ToString(),
                        });
                        values.GetCustomernamedropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Customer name!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }
        public void DaGetBranchName(Mdlinvoicesummary values)
        {
            try
            {

                msSQL = " select branch_name, branch_gid from hrm_mst_tbranch";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<GetBranchnamedropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetBranchnamedropdown
                        {
                            branch_gid = dt["branch_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                        });
                        values.GetBranchnamedropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Branch name!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetcurrencyCodedropdown(Mdlinvoicesummary values)
        {
            try
            {

                msSQL = " select currency_code, currencyexchange_gid " +
                    " from crm_trn_tcurrencyexchange ";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<Getcurrencycodedropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getcurrencycodedropdown
                        {
                            currency_code = dt["currency_code"].ToString(),
                            currencyexchange_gid = dt["currencyexchange_gid"].ToString(),
                        });
                        values.Getcurrencycodedropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Currency code!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaInvoicePostProduct(string employee_gid, invoiceproductlist values)
        {
            try
            {

                double lstotalamount = Math.Round((values.unitprice), 2) * (values.quantity);
                double discountamount = Math.Round(lstotalamount * ((values.discountpercentage) / 100), 2);
                double lsGrandtotal = Math.Round(((values.unitprice) * (values.quantity)) - discountamount, 2);
                msGetGid = objcmnfunctions.GetMasterGID("SIVC");
                string lsrefno = "";
                double lspercentage1 = 0;
                double lspercentage2 = 0;
                double tax1_amount = 0;
                double tax2_amount = 0;

                string lstax1, lstax2;
                msSQL = "select product_name from pmr_mst_tproduct where product_gid='" + values.productgid + "'";
                string lsproductName = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select productuom_gid from pmr_mst_tproductuom where productuom_name='" + values.productuom_gid + "'";
                string lsproductuomgid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "Select productgroup_gid from pmr_mst_tproductgroup where productgroup_name='" + values.productgroup_gid + "'";
                string lsproductgroupgid = objdbconn.GetExecuteScalar(msSQL);

                if (values.taxname1 == "" || values.taxname1 ==null)
                {
                    lspercentage1 = 0;
                }
                else
                {
                    msSQL = "select percentage from acp_mst_ttax  where tax_gid='" + values.taxname1 + "'";
                    string lspercentage = objdbconn.GetExecuteScalar(msSQL);
                    tax1_amount = Math.Round(lsGrandtotal * (Convert.ToDouble(lspercentage) / 100), 2);
                }
                if (values.taxname2 == "" || values.taxname2==null)
                {
                    lspercentage2 = 0;
                }
                else
                {
                    msSQL = "select percentage from acp_mst_ttax  where tax_gid='" + values.taxname2 + "'";
                    string lspercentage_2 = objdbconn.GetExecuteScalar(msSQL);
                    tax2_amount = Math.Round(lsGrandtotal * (Convert.ToDouble(lspercentage_2) / 100), 2);
                }
                double Grandtotalamount = Math.Round((lsGrandtotal + tax1_amount + tax2_amount), 2);

                if (values.taxname1 == ""|| values.taxname1==null)
                {
                    lstax1 = "0";
                }
                else
                {
                    msSQL = "select tax_name from acp_mst_ttax  where tax_gid='" + values.taxname1 + "'";
                    lstax1 = objdbconn.GetExecuteScalar(msSQL);
                }

                if (values.taxname2 == "" || values.taxname2 == null)
                {
                    lstax2 = "0";
                }
                else
                {
                    msSQL = "select tax_name from acp_mst_ttax where tax_gid='" + values.taxname2 + "'";
                    lstax2 = objdbconn.GetExecuteScalar(msSQL);
                }
                {
                    msSQL = " insert into rbl_tmp_tinvoicedtl ( " +
                            " invoicedtl_gid," +
                            " invoice_gid," +
                            " product_gid," +
                            " qty_invoice," +
                            " product_price," +
                            " discount_percentage," +
                            " discount_amount," +
                            " hsn_code," +
                            " hsn_description," +
                            " tax_amount," +
                            " product_total," +
                            " uom_gid," +
                            " uom_name," +
                            " tax_amount2," +
                            " tax_name," +
                            " tax_name2," +
                            " display_field," +
                            " tax1_gid," +
                            " tax2_gid," +
                            " product_name," +
                            " productgroup_gid," +
                            " productgroup_name, " +
                            " employee_gid, " +
                            " selling_price, " +
                            " product_code," +
                            " customerproduct_code, " +
                            " tax_percentage," +
                            " tax_percentage2," +
                            " vendor_price " +
                            " ) values ( " +
                            "'" + msGetGid + "'," +
                            "'" + lsrefno + "'," +
                            "'" + values.productgid + "'," +
                            "'" + values.quantity + "'," +
                            "'" + values.unitprice + "'," +
                            "'" + values.discountpercentage + "'," +
                            "'" + discountamount + "'," +
                            "'" + values.hsncode + "'," +
                            "'" + values.hsndescription + "'," +
                            "'" + tax1_amount + "'," +
                            "'" + Grandtotalamount + "'," +
                            "'" + lsproductuomgid + "'," +
                            "'" + values.productuom_gid + "'," +
                            "'" + tax2_amount + "'," +
                            "'" + lstax1 + "'," +
                            "'" + lstax2 + "'," +
                            "'" + values.productdescription + "'," +
                            "'" + values.tax_gid1 + "'," +
                            "'" + values.tax_gid2 + "'," +
                            "'" + lsproductName + "'," +
                            "'" + lsproductgroupgid + "'," +
                            "'" + values.productgroup_gid + "'," +
                            "'" + employee_gid + "'," +
                            "'" + values.mrp + "'," +
                            "'" + values.productcode + "'," +
                            "'" + values.hsncode + "'," +
                            "'" + lspercentage1 + "'," +
                            "'" + lspercentage2 + "'," +
                            "'" + Grandtotalamount + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Product Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Adding Product";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Invoice Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaInvoiceSubmit(string employee_gid, invoicelist values)
        {
            try
            {
                string uiDateStr = values.invoicedate;
                DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string mysqlinvoiceDate = uiDate.ToString("yyyy-MM-dd");

                msINGetGID = objcmnfunctions.GetMasterGID("SIVT");
                string lstype1 = "services";
                string ls_referenceno = objcmnfunctions.GetMasterGID("INV");
                msSQL = "select customer_name from crm_mst_tcustomer where customer_gid='" + values.customergid + "'";
                string lsCustomername = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select currency_code from crm_trn_tcurrencyexchange  where currencyexchange_gid='" + values.currencygid + "'";
                string lscurrencycode = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " insert into rbl_trn_tinvoice(" +
                        " invoice_gid," +
                        " invoice_date," +
                        " payment_term, " +
                        " payment_date," +
                        " invoice_type," +
                        " customer_gid," +
                        " customer_name," +
                        " customer_contactperson," +
                        " customer_contactnumber," +
                        " customer_address," +
                        " customer_email," +
                        " total_amount," +
                        " invoice_amount," +
                        " invoice_refno," +
                        " invoice_status," +
                        " invoice_flag," +
                        " user_gid," +
                        " discount_amount," +
                        " additionalcharges_amount," +
                        " total_amount_L," +
                        " invoice_amount_L," +
                        " discount_amount_L," +
                        " additionalcharges_amount_L," +
                        " invoice_remarks," +
                        " termsandconditions," +
                        " currency_code," +
                        " exchange_rate," +
                        " branch_gid, " +
                        " roundoff," +
                        " created_date," +
                        " freight_charges," +
                        " packing_charges," +
                        " insurance_charges " +
                        " ) values (" +
                        " '" + msINGetGID + "'," +
                        "'" + mysqlinvoiceDate + "'," +
                        "'" + values.paymentterm + "'," +
                        "'" + values.duedate.ToString("yyyy-MM-dd ") + "'," +
                        "' " + lstype1 + "'," +
                        " '" + values.customergid + "'," +
                        " '" + lsCustomername + "'," +
                        " '" + values.customercontactperson + "'," +
                        " '" + values.customercontactnumber + "'," +
                        " '" + values.customeraddress + "'," +
                        " '" + values.customeremailaddress + "'," +
                        " '" + values.grandtotal + "'," +
                        " '" + values.grandtotal + "'," +
                        " '" + ls_referenceno + "'," +
                        " 'Payment Pending'," +
                        " 'Invoice Approved'," +
                        " '" + employee_gid + "'," +
                        " '" + values.invoicediscountamount + "'," +
                        " '" + values.addoncharges + "'," +
                        "'" + values.grandtotal + "'," +
                        "'" + values.grandtotal + "'," +
                        "'" + values.invoicediscountamount + "'," +
                        "'" + values.addoncharges + "'," +
                        "'" + values.remarks + "'," +
                        "'" + values.termsandconditions + "', " +
                        "'" + lscurrencycode + "'," +
                        "'" + values.exchangerate + "'," +
                        "'" + values.branchgid + "', " +
                        "'" + values.roundoff + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + values.frieghtcharges + "'," +
                        "'" + values.forwardingCharges + "'," +
                        "'" + values.insurancecharges + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = " select invoicedtl_gid, invoice_gid, product_gid,qty_invoice, product_price, discount_percentage, " +
                            " discount_amount, hsn_code, hsn_description, tax_amount, product_total, uom_gid, uom_name," +
                            " tax_amount2, tax_name, tax_name2, display_field, tax1_gid, tax2_gid, " +
                            " product_name, productgroup_gid,productgroup_name,  employee_gid,  selling_price,  product_code, " +
                            " customerproduct_code,  tax_percentage, tax_percentage2," +
                            " sl_no, vendor_price from rbl_tmp_tinvoicedtl where employee_gid='" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("SIVC");

                        msSQL = " insert into rbl_trn_tinvoicedtl ( " +
                                " invoicedtl_gid," +
                                " invoice_gid," +
                                " product_gid," +
                                " qty_invoice," +
                                " product_price," +
                                " discount_percentage," +
                                " discount_amount," +
                                " hsn_code," +
                                " hsn_description," +
                                " tax_amount," +
                                " product_total," +
                                " uom_gid," +
                                " tax_amount2," +
                                " tax_name," +
                                " tax_name2," +
                                " display_field," +
                                " tax1_gid," +
                                " tax2_gid," +
                                " product_name," +
                                " employee_gid, " +
                                " product_code," +
                                " customerproduct_code, " +
                                " tax_percentage," +
                                " tax_percentage2," +
                                " uom_name," +
                                " productgroup_gid, " +
                                " productgroup_name," +
                                " selling_price," +
                                " vendor_price," +
                                " product_total_L " +
                                " ) values ( " +
                                "'" + msGetGid + "'," +
                                "'" + msINGetGID + "'," +
                                "'" + dt["product_gid"].ToString() + "'," +
                                "'" + dt["qty_invoice"].ToString() + "'," +
                                "'" + dt["product_price"].ToString() + "'," +
                                "'" + dt["discount_percentage"].ToString() + "'," +
                                "'" + dt["discount_amount"].ToString() + "'," +
                                "'" + dt["hsn_code"].ToString() + "'," +
                                "'" + dt["hsn_description"].ToString() + "'," +
                                "'" + dt["tax_amount"].ToString() + "'," +
                                "'" + dt["product_total"].ToString() + "'," +
                                "'" + dt["uom_gid"].ToString() + "'," +
                                "'" + dt["tax_amount2"].ToString() + "'," +
                                "'" + dt["tax_name"].ToString() + "'," +
                                "'" + dt["tax_name2"].ToString() + "'," +
                                "'" + dt["display_field"].ToString() + "'," +
                                "'" + dt["tax1_gid"].ToString() + "'," +
                                "'" + dt["tax2_gid"].ToString() + "'," +
                                "'" + dt["product_name"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + dt["product_code"].ToString() + "'," +
                                "'" + dt["hsn_code"].ToString() + "'," +
                                "'" + dt["tax_percentage"].ToString() + "'," +
                                "'" + dt["tax_percentage2"].ToString() + "'," +
                                "'" + dt["uom_name"].ToString() + "'," +
                                "'" + dt["productgroup_gid"].ToString() + "'," +
                                "'" + dt["productgroup_name"].ToString() + "'," +
                                "'" + dt["selling_price"].ToString() + "'," +
                                "'" + dt["vendor_price"].ToString() + "'," +
                                "'" + dt["product_total"].ToString() + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (mnResult == 1)
                    {
                        msSQL = " delete from rbl_tmp_tinvoicedtl where employee_gid='" + employee_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {

                    }
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Invoice Submitted Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While submitted the invoice";
                    }
                    dt_datatable.Dispose();
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Invoice Submit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaUpdatedInvoice(string employee_gid, invoicelist values)
        {
            try
            {
                string uiDateStr = values.invoicedate;
                DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string mysqlinvoiceDate = uiDate.ToString("yyyy-MM-dd");
                string lstype1 = "services";
                string ls_referenceno = objcmnfunctions.GetMasterGID("INV");
                string msSalesGid = "";
                msSQL = "select customer_name from crm_mst_tcustomer where customer_gid='" + values.customergid + "'";
                string lsCustomername = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update rbl_trn_tinvoice set " +
                        " invoice_date='" + mysqlinvoiceDate + "'," +
                        " payment_term='" + values.paymentterm + "'," +
                        " customer_gid='" + values.customergid + "'," +
                        " customer_name='" + lsCustomername + "'," +
                        " customer_contactperson='" + values.customercontactperson + "'," +
                        " customer_contactnumber='" + values.customercontactnumber + "'," +
                        " customer_address='" + values.customeraddress + "'," +
                        " customer_email='" + values.customeremailaddress + "'," +
                        " total_amount='" + values.grandtotal + "'," +
                        " invoice_amount='" + values.grandtotal + "'," +
                        " invoice_refno='" + values.invoiceref_no + "'," +
                        " user_gid='" + employee_gid + "'," +
                        " discount_amount='" + values.invoicediscountamount + "'," +
                        " additionalcharges_amount='" + values.addoncharges + "'," +
                        " total_amount_L='" + values.grandtotal + "'," +
                        " invoice_amount_L='" + values.grandtotal + "'," +
                        " discount_amount_L='" + values.invoicediscountamount + "'," +
                        " additionalcharges_amount_L='" + values.addoncharges + "'," +
                        " invoice_remarks='" + values.remarks + "'," +
                        " termsandconditions='" + values.termsandconditions + "'," +
                        " currency_code='" + values.currencygid + "'," +
                        " exchange_rate='" + values.exchangerate + "'," +
                        " branch_gid='" + values.branchgid + "'," +
                        " roundoff='" + values.roundoff + "'," +
                        " created_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                        " freight_charges='" + values.frieghtcharges + "'," +
                        " insurance_charges='" + values.insurancecharges + "'" +
                        " where invoice_gid='" + values.invoice_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = " select invoicedtl_gid, invoice_gid, product_gid,qty_invoice, product_price, discount_percentage, " +
                            " discount_amount, hsn_code, hsn_description, tax_amount, product_total, uom_gid, uom_name," +
                            " tax_amount2, tax_name, tax_name2, tax_name3,display_field, tax1_gid, tax2_gid, " +
                            " tax3_gid, product_name, productgroup_gid,productgroup_name,  employee_gid,  selling_price,  product_code, " +
                            " customerproduct_code,  tax_percentage, tax_percentage2, tax_percentage3 , " +
                            " sl_no, vendor_price from rbl_tmp_tinvoicedtl where employee_gid='" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("SIVC");

                        msSQL = " insert into rbl_trn_tinvoicedtl ( " +
                                " invoicedtl_gid," +
                                " invoice_gid," +
                                " product_gid," +
                                " qty_invoice," +
                                " product_price," +
                                " discount_percentage," +
                                " discount_amount," +
                                " hsn_code," +
                                " hsn_description," +
                                " tax_amount," +
                                " product_total," +
                                " uom_gid," +
                                " tax_amount2," +
                                " tax_name," +
                                " tax_name2," +
                                " tax_name3," +
                                " display_field," +
                                " tax1_gid," +
                                " tax2_gid," +
                                " tax3_gid," +
                                " product_name," +
                                " employee_gid, " +
                                " product_code," +
                                " customerproduct_code, " +
                                " tax_percentage," +
                                " tax_percentage2," +
                                " tax_percentage3 ," +
                                " product_total_L " +
                                " ) values ( " +
                                "'" + msGetGid + "'," +
                                "'" + values.invoice_gid + "'," +
                                "'" + dt["product_gid"].ToString() + "'," +
                                "'" + dt["qty_invoice"].ToString() + "'," +
                                "'" + dt["product_price"].ToString() + "'," +
                                "'" + dt["discount_percentage"].ToString() + "'," +
                                "'" + dt["discount_amount"].ToString() + "'," +
                                "'" + dt["hsn_code"].ToString() + "'," +
                                "'" + dt["hsn_description"].ToString() + "'," +
                                "'" + dt["tax_amount"].ToString() + "'," +
                                "'" + dt["product_total"].ToString() + "'," +
                                "'" + dt["uom_gid"].ToString() + "'," +
                                "'" + dt["tax_amount2"].ToString() + "'," +
                                "'" + dt["tax_name"].ToString() + "'," +
                                "'" + dt["tax_name2"].ToString() + "'," +
                                "'" + dt["tax_name3"].ToString() + "'," +
                                "'" + dt["display_field"].ToString() + "'," +
                                "'" + dt["tax1_gid"].ToString() + "'," +
                                "'" + dt["tax2_gid"].ToString() + "'," +
                                "'" + dt["tax3_gid"].ToString() + "'," +
                                "'" + dt["product_name"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + dt["product_code"].ToString() + "'," +
                                "'" + dt["hsn_code"].ToString() + "'," +
                                "'" + dt["tax_percentage"].ToString() + "'," +
                                "'" + dt["tax_percentage2"].ToString() + "'," +
                                "'" + dt["tax_percentage3"].ToString() + "'," +
                                "'" + dt["product_total"].ToString() + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    if (mnResult == 1)
                    {
                        msSQL = " delete from rbl_tmp_tinvoicedtl where employee_gid='" + employee_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable.Dispose();
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Invoice Update!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DainvoiceProductSummary(string employee_gid, Mdlinvoicesummary values)
        {
            try
            {

                double grand_total = 0.00;

                msSQL = "select invoicedtl_gid, invoice_gid, product_gid, qty_invoice, format(product_price,2) as product_price," +
                        " concat(discount_percentage,'%','  -  ' ,format(discount_amount,2)) as discount, " +
                        " concat(hsn_code, ' / ',hsn_description) as hsn, format(product_total,2) as product_total, uom_gid," +
                        " uom_name, CASE WHEN tax_amount <> 0 and tax_amount2 = 0" +
                        " then concat(tax_name,' - ',tax_amount) WHEN tax_amount = 0 and tax_amount2 <> 0" +
                        " then concat(tax_name2,' - ',tax_amount2)" +
                        " WHEN tax_amount <> 0 and tax_amount2 <> 0" +
                        " then concat(tax_name,' - ',tax_amount,'   ',tax_name2,' - ',tax_amount2)" +
                        " WHEN tax_amount <> 0 and tax_amount2 = 0 then concat(tax_name,' - ',tax_amount) " +
                        " WHEN tax_amount = 0 and tax_amount2 <> 0 then concat(tax_name2,' - ',tax_amount2) " +
                        " else concat(tax_name,' - ',tax_amount,'   ',tax_name2,' - ',tax_amount2) end as tax," +
                        " display_field, tax1_gid, tax2_gid, product_name, productgroup_gid, " +
                        " productgroup_name,  employee_gid,  selling_price,  product_code," +
                        " customerproduct_code,  tax_percentage, tax_percentage2, sl_no, " +
                        " format(vendor_price,2) as vendor_price from rbl_tmp_tinvoicedtl where employee_gid='" + employee_gid + "' order by invoicedtl_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<invoiceproductsummary_list>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        grand_total += double.Parse(dt["product_total"].ToString());
                        getModuleList.Add(new invoiceproductsummary_list
                        {
                            invoicedtl_gid = dt["invoicedtl_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            customerproduct_code = dt["customerproduct_code"].ToString(),
                            uom_name = dt["uom_name"].ToString(),
                            qty_invoice = dt["qty_invoice"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            discount = dt["discount"].ToString(),
                            selling_price = dt["selling_price"].ToString(),
                            product_total = dt["product_total"].ToString(),
                            tax = dt["tax"].ToString(),
                            hsn = dt["hsn"].ToString(),
                        });
                        values.invoiceproductsummarylist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
                values.grand_total = Math.Round(grand_total, 2);
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Invoice Product Details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DadeleteInvoiceProductSummary(string employee_gid)
        {
            msSQL = " delete from rbl_tmp_tinvoicedtl where employee_gid='" + employee_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        public void DaGetDeleteInvoiceProductSummary(string invoicedtl_gid, invoiceproductlist values)
        {
            try
            {

                msSQL = " delete from rbl_tmp_tinvoicedtl where invoicedtl_gid='" + invoicedtl_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Product Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deleting Product Group";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Invoice Product Delete!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetDeleteInvoicebackProductSummary(invoiceproductlist values)
        {
            try
            {

                msSQL = " delete from rbl_tmp_tinvoicedtl ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Product Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deleting Product Group";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Invoice Product Delete!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaEInvoiceGeneration(string employee_gid, EinvoiceAddField values)
        {
            string mseinvoiceGID;
            string address;
            string invoice_gid;
            string msGetGID;
            InvoiceData invoicedata = new InvoiceData();
            var transactionDetails = new TransactionDetails();
            var documentDetails = new DocumentDetails();
            var sellerDetails = new SellerDetails();
            var buyerDetails = new BuyerDetails();
            var dispatchDetails = new DispatchDetails();
            var shipDetails = new ShipDetails();
            var itemDetails = new List<ItemDetails>();

            try
            {

                msGetGID = objcmnfunctions.GetMasterGID("RQST");
                mseinvoiceGID = objcmnfunctions.GetMasterGID("EINV");

                msSQL = "select state_code from rbl_mst_tstatecode where state_name='" + values.sellerDetails_Stcd + "' ";
                var lssellerstate_code = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select state_code from rbl_mst_tstatecode where state_name='" + values.buyerDetails_Stcd + "' ";
                var lsbyerstate_code = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select state_code from rbl_mst_tstatecode where state_name='" + values.dispatchDetails_Stcd + "' ";
                var lstxtdisstate = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select state_code from rbl_mst_tstatecode where state_name='" + values.shipDetails_Stcd + "' ";
                var lsshipstate = objdbconn.GetExecuteScalar(msSQL);

                transactionDetails.TaxSch = "GST";
                transactionDetails.SupTyp = values.transactionDetails_SupTyp;
                transactionDetails.RegRev = values.transactionDetails_RegRev;
                transactionDetails.IgstOnIntra = values.transactionDetails_IgstOnIntra;

                documentDetails.Dt = DateTime.Now.ToString("dd/MM/yyyy");
                documentDetails.No = values.invoicerefno;
                documentDetails.Typ = values.documentDetails_Typ;

                int splitLength = ((values.sellerDetails_Address).Length) / 2;

                sellerDetails.Gstin = values.sellerDetails_Gstin;
                sellerDetails.LglNm = values.sellerDetails_LglNm;
                sellerDetails.TrdNm = values.sellerDetails_TrdNm;
                sellerDetails.Addr1 = values.sellerDetails_Address.Substring(0, splitLength);
                sellerDetails.Addr2 = values.sellerDetails_Address.Substring(splitLength);
                sellerDetails.Loc = values.sellerDetails_Loc;
                sellerDetails.Pin = values.sellerDetails_Pin;
                sellerDetails.Stcd = lssellerstate_code;

                splitLength = ((values.buyerDetails_Address).Length) / 2;

                buyerDetails.Gstin = values.buyerDetails_Gstin;
                buyerDetails.LglNm = values.buyerDetails_LglNm;
                buyerDetails.TrdNm = values.buyerDetails_LglNm;
                buyerDetails.Addr1 = values.buyerDetails_Address.Substring(0, splitLength);
                buyerDetails.Addr2 = values.buyerDetails_Address.Substring(splitLength);
                buyerDetails.Loc = values.buyerDetails_Loc;
                buyerDetails.Pos = values.buyerDetails_Pos;
                buyerDetails.Pin = values.buyerDetails_Pin;
                buyerDetails.Stcd = lsbyerstate_code;

                splitLength = ((values.dispatchDetails_Address).Length) / 2;

                dispatchDetails.Nm = values.buyerDetails_LglNm;
                dispatchDetails.Addr1 = values.dispatchDetails_Address.Substring(0, splitLength);
                dispatchDetails.Addr2 = values.dispatchDetails_Address.Substring(splitLength);
                dispatchDetails.Loc = values.dispatchDetails_Loc;
                dispatchDetails.Pin = values.dispatchDetails_Pin;
                dispatchDetails.Stcd = lsbyerstate_code;
                splitLength = ((values.shipDetails_Address).Length) / 2;

                shipDetails.Gstin = values.shipDetails_Gstin;
                shipDetails.LglNm = values.shipDetails_LglNm;
                shipDetails.TrdNm = values.shipDetails_TrdNm;
                shipDetails.Addr1 = values.shipDetails_Address.Substring(0, splitLength);
                shipDetails.Addr2 = values.shipDetails_Address.Substring(splitLength);
                shipDetails.Loc = values.shipDetails_Loc;
                shipDetails.Pin = values.shipDetails_Pin;
                shipDetails.Stcd = lsshipstate;

                double totalinvamt = 0;
                double igstamt = 0;
                double cgstamt = 0;
                double sgstamt = 0;
                double assAmt = 0;
                int i = 1;

                msSQL = "select a.qty_invoice, format((a.vendor_price),2) as unit_price, format((a.vendor_price * a.qty_invoice),2) as total_amount,b.product_code," +
                           "a.discount_percentage,format((a.discount_amount),2)as discount_amount, format(((a.vendor_price * a.qty_invoice) - a.discount_amount),2) as assesable_amt, " +
                           "a.tax_name,a.tax_name2,a.tax_name3,a.tax_amount, a.tax_amount2,a.tax_amount3, format((((a.vendor_price * a.qty_invoice) - a.discount_amount)+a.tax_amount+a.tax_amount2+a.tax_amount3),2) as total_itemval," +
                           "a.product_name, b.product_gid,b.hsn_number,b.hsn_desc,b.is_service,ifnull(z.percentage,0) as percentage ,c.uomname " +
                           "From rbl_trn_tinvoicedtl a " +
                           "left join acp_mst_ttax z on z.tax_name = a.tax_name " +
                           "Left Join rbl_trn_tinvoice h on h.invoice_gid = a.invoice_gid " +
                           "Left Join pmr_mst_tproduct b on b.product_gid=a.product_gid " +
                           "left join pmr_mst_tproductuom c on c.productuom_gid=b.productuom_gid " +
                           "left join rbl_trn_tinvoice e on e.invoice_gid=a.invoice_gid " +
                           "Left Join pmr_mst_tproductgroup g on g.productgroup_gid=b.productgroup_gid " +
                           "where h.invoice_gid ='" + values.invoice_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_datatable.Rows)
                    {
                        double total_amount, discount, lsAssAmt, lsGstRt, lsCgstAmt, lsSgstAmt, lsIgstAmt, lsTotItemVal = 0.0;
                        lsIgstAmt = 0.0;
                        lsCgstAmt = 0.0;
                        lsSgstAmt = 0.0;
                        total_amount = Convert.ToDouble(row["total_amount"].ToString());
                        discount = Convert.ToDouble(row["discount_amount"].ToString());
                        lsAssAmt = total_amount - discount;
                        assAmt += lsAssAmt;
                        if (row["tax_name"].ToString().Contains("CGST") || row["tax_name"].ToString().Contains("SGST") || row["tax_name2"].ToString().Contains("CGST") || row["tax_name2"].ToString().Contains("SGST"))
                        {
                            lsGstRt = Convert.ToDouble(row["percentage"].ToString()) * 2;
                        }
                        else
                        {
                            lsGstRt = Convert.ToDouble(row["percentage"].ToString());
                        }
                        if (lsbyerstate_code == lssellerstate_code)
                        {
                            lsCgstAmt = Math.Round(((lsAssAmt * lsGstRt / 100)) / 2, 2);
                            cgstamt = Math.Round((cgstamt + lsCgstAmt), 2);
                            //cgstamt += cgstamt;
                            lsSgstAmt = Math.Round((((lsAssAmt * lsGstRt / 100)) / 2), 2);
                            sgstamt = Math.Round((sgstamt + lsSgstAmt), 2);
                            //sgstamt += sgstamt;
                        }
                        else
                        {
                            lsIgstAmt = Math.Round((lsAssAmt * lsGstRt / 100), 2);
                            igstamt += Math.Round((igstamt + lsIgstAmt), 2);
                        }

                        lsTotItemVal = Math.Round((lsAssAmt + lsIgstAmt + lsCgstAmt + lsSgstAmt), 2);
                        totalinvamt = Math.Round((totalinvamt + lsTotItemVal), 2);

                        itemDetails.Add(new ItemDetails
                        {
                            SlNo = i.ToString(),
                            PrdDesc = row["hsn_desc"].ToString(),
                            IsServc = row["is_service"].ToString(),
                            HsnCd = row["hsn_number"].ToString(),
                            Qty = double.Parse(row["qty_invoice"].ToString()),
                            Unit = row["uomname"].ToString(),
                            UnitPrice = double.Parse(row["unit_price"].ToString()),
                            TotAmt = double.Parse(row["total_amount"].ToString()),
                            Discount = double.Parse(row["discount_amount"].ToString()),
                            AssAmt = lsAssAmt,
                            GstRt = lsGstRt,
                            CgstAmt = lsCgstAmt,
                            SgstAmt = lsSgstAmt,
                            IgstAmt = lsIgstAmt,
                            TotItemVal = lsTotItemVal,
                        });
                        i = 1 + 1;
                    }
                }

                var valueDetails = new ValueDetails();
                msSQL = "select  format(a.roundoff,2) as roundoff,a.total_amount, Format(a.discount_amount, 2)As discount_amount , " +
                            "Format((a.freight_charges + a.buyback_charges + a.packing_charges + a. insurance_charges + a.additionalcharges_amount),2) as othr_chrg" +
                            " From rbl_trn_tinvoice a " +
                            "where a.invoice_gid ='" + values.invoice_gid + "' " +
                            "Group by a.invoice_gid order by a.invoice_gid";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                double Discount = 0.0;
                double OthChrg = 0.0;
                double RndOffAmt = 0.0;

                if (objMySqlDataReader.HasRows == true)
                {
                    Discount = Double.Parse(objMySqlDataReader["discount_amount"].ToString());
                    OthChrg = Double.Parse(objMySqlDataReader["othr_chrg"].ToString());
                    RndOffAmt = Double.Parse(objMySqlDataReader["roundoff"].ToString());

                    valueDetails.AssVal = Math.Round(assAmt, 2);
                    valueDetails.IgstVal = Math.Round(igstamt, 2);
                    valueDetails.CgstVal = Math.Round(cgstamt, 2);
                    valueDetails.SgstVal = Math.Round(sgstamt, 2);
                    valueDetails.Discount = Double.Parse(objMySqlDataReader["discount_amount"].ToString());
                    valueDetails.OthChrg = Double.Parse(objMySqlDataReader["othr_chrg"].ToString());
                    valueDetails.RndOffAmt = Double.Parse(objMySqlDataReader["roundoff"].ToString());
                    valueDetails.TotInvVal = Math.Round((totalinvamt - Discount + OthChrg - RndOffAmt), 2);
                }

                invoicedata.Version = "1.1";
                invoicedata.TranDtls = transactionDetails;
                invoicedata.DocDtls = documentDetails;
                invoicedata.SellerDtls = sellerDetails;
                invoicedata.BuyerDtls = buyerDetails;
                invoicedata.DispDtls = dispatchDetails;
                invoicedata.ShipDtls = shipDetails;
                invoicedata.ItemList = itemDetails;
                invoicedata.ValDtls = valueDetails;

                string einvoicejson = JsonConvert.SerializeObject(invoicedata);

                string lstoken;
                string lsaccess_token = "";
                string lsexpiry_date1 = "";

                msSQL = "select einvoice_token,einvoice_tokenexpiry from adm_mst_tcompany";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lstoken = objMySqlDataReader["einvoice_token"].ToString();
                    lsexpiry_date1 = objMySqlDataReader["einvoice_tokenexpiry"].ToString();
                    if (lstoken == "")
                    {
                        var auth = AuthCall();
                        msSQL = "update adm_mst_tcompany set einvoice_tokenexpiry=Now() + INTERVAL 28 DAY," +
                                " einvoice_token='" + auth.access_token + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            lsaccess_token = auth.access_token;
                        }
                    }
                    else
                    {
                        var lsexpiry_date = Convert.ToDateTime(lsexpiry_date1);
                        if (lsexpiry_date < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                        {
                            var auth = AuthCall();
                            msSQL = "update adm_mst_tcompany set einvoive_tokenexpiry=Now() + INTERVAL 28 DAY,einvoive_token='" + auth.access_token + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult == 1)
                            {
                                lsaccess_token = auth.access_token;
                            }
                            else
                            {
                                lsaccess_token = lstoken;
                            }
                        }
                        else
                        {
                            lsaccess_token = lstoken;
                        }
                    }
                }
                var token = "bearer" + lsaccess_token;
                var irn = new ApiResponse();
                string lsvar = "IRN";
                irn = GenerateIRN(token, einvoicejson, values.invoice_gid, lsvar);
                if (String.IsNullOrEmpty(irn.qr_code) == false)
                {
                    var signed_qr = new qr_request();
                    signed_qr.data = irn.qr_code;
                    string qrReq_json = JsonConvert.SerializeObject(signed_qr);
                    var qrcode = new qrDownloadResponse();
                    qrcode = GenerateQRcode(token, qrReq_json, values.invoice_gid, irn.request_id);
                    if (qrcode.status == true)
                    {
                        values.status = true;
                        values.message = "IRN and QR Code has been Generated Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error occured while generating QR code!";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = irn.errorMessage;
                }
                objdbconn.CloseConn();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error occured while generating QR code!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaCancelIRN(cancelIrn_list values, string employee_gid)
        {
            try
            {
                string lsirn = "";


                msSQL = "select irn from rbl_trn_tinvoice where invoice_gid='" + values.invoice_gid + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lsirn = objMySqlDataReader["irn"].ToString();
                }

                string lstoken;
                string lsaccess_token = "";
                string lsexpiry_date1 = "";

                msSQL = "select einvoice_token,einvoice_tokenexpiry from adm_mst_tcompany";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lstoken = objMySqlDataReader["einvoice_token"].ToString();
                    lsexpiry_date1 = objMySqlDataReader["einvoice_tokenexpiry"].ToString();
                    if (lstoken == "")
                    {
                        var auth = AuthCall();
                        msSQL = "update adm_mst_tcompany set einvoice_tokenexpiry=Now() + INTERVAL 28 DAY," +
                                " einvoice_token='" + auth.access_token + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            lsaccess_token = auth.access_token;
                        }
                    }
                    else
                    {
                        var lsexpiry_date = Convert.ToDateTime(lsexpiry_date1);
                        if (lsexpiry_date < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                        {
                            var auth = AuthCall();
                            msSQL = "update adm_mst_tcompany set einvoive_tokenexpiry=Now() + INTERVAL 28 DAY,einvoive_token='" + auth.access_token + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult == 1)
                            {
                                lsaccess_token = auth.access_token;
                            }
                            else
                            {
                                lsaccess_token = lstoken;
                            }
                        }
                        else
                        {
                            lsaccess_token = lstoken;
                        }
                    }
                }

                string token = "bearer" + lsaccess_token;
                var CancelInvoiceData = new CancelIRN();

                CancelInvoiceData.irn = lsirn;
                CancelInvoiceData.cnlrem = "wrong entry";
                CancelInvoiceData.cnlrsn = "1";
                var CancelInvoiceDatajson = JsonConvert.SerializeObject(CancelInvoiceData);//here converting to JSON format
                var responseData = new CancelIRN_ResponseData();
                responseData = FNCancelIRN(token, CancelInvoiceDatajson, values.invoice_gid);
                if (responseData.success == true)
                {

                }
                else
                {

                }
                objdbconn.CloseConn();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Invoice Product Cancel!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        private ApiResponse GenerateIRN(string token, string einvoicejson, string invoice_gid, string lsvar)
        {
            string msGetGID;
            string uri = ConfigurationManager.AppSettings["einvoiceIRNGenerate"].ToString();
            Uri ourUri = new Uri(uri);
            string requestBody = einvoicejson;
            msGetGID = objcmnfunctions.GetMasterGID("RQST");
            WebRequest request = WebRequest.Create(uri);
            WebResponse response;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("user_name", ConfigurationManager.AppSettings["einvoiceuser_name"].ToString());
            request.Headers.Add("password", ConfigurationManager.AppSettings["einvoicepwd"].ToString());
            request.Headers.Add("requestid", msGetGID);
            request.Headers.Add("gstin", ConfigurationManager.AppSettings["einvoicegstin"].ToString());
            request.Headers.Add("Authorization", token);

            byte[] requestBodyBytes = Encoding.UTF8.GetBytes(requestBody);
            request.ContentLength = requestBodyBytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
            }

            response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string streamText = reader.ReadToEnd();

            msSQL = "insert into rblt_trn_teinvoice_responselog (" +
                    "request_gid, " +
                    "reponse_json, " +
                    "request_json, " +
                    "invoice_gid" +
                    ") values ( " +
                    "'" + msGetGID + "'," +
                    "'" + streamText + "'," +
                    "'" + einvoicejson + "'," +
                    "'" + invoice_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            ApiResponse irnresponse = new ApiResponse();
            try
            {
                var json = new ResponseData();

                json = JsonConvert.DeserializeObject<ResponseData>(streamText);
                if (json.success == true)
                {
                    msSQL = "update rblt_trn_teinvoice_responselog set " +
                            "success ='" + json.success + "'," +
                            "message ='" + json.message.ToString().Replace("'", "") + "'," +
                            "AckNo ='" + json.result.AckNo + "'," +
                            "AckDt ='" + Convert.ToDateTime(json.result.AckDt).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "Irn ='" + json.result.Irn + "'," +
                            "SignedInvoice ='" + json.result.SignedInvoice + "'," +
                            "SignedQRCode ='" + json.result.SignedQRCode + "'," +
                            "Status ='" + json.result.Status + "'," +
                            "Remarks ='" + json.result.Remarks + "'" +
                            "where request_gid ='" + msGetGID + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        if (lsvar == "IRN")
                        {
                            msSQL = "update rbl_trn_tinvoice set " +
                           "irn ='" + json.result.Irn + "' " +
                           "where invoice_gid ='" + invoice_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            msSQL = "update rbl_trn_tinvoice set " +
                           "irn ='" + json.result.Irn + "',creditnote_status='Y',irncreated_date='" + Convert.ToDateTime(json.result.AckDt).ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                           "where invoice_gid ='" + invoice_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        irnresponse.request_id = msGetGID;
                        irnresponse.qr_code = json.result.SignedQRCode;
                        irnresponse.success = true;
                    }
                    else
                        irnresponse.success = false;
                }
                else
                {
                    irnresponse.success = false;
                    msSQL = "update rblt_trn_teinvoice_responselog set " +
                        "success ='" + json.success + "'," +
                        "message ='" + json.message.ToString().Replace("'", "") + "' " +
                        "where request_gid ='" + msGetGID + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select message from rblt_trn_teinvoice_responselog where request_gid ='" + msGetGID + "'";

                    irnresponse.errorMessage = objdbconn.GetExecuteScalar(msSQL);
                }
            }
            catch (Exception ex)
            {
                irnresponse.success = false;
                msSQL = "update rblt_trn_teinvoice_responselog set " +
                    "message ='Exception occured while generating IRN - " + ex.ToString().Replace("'", "") + "' " +
                    "where request_gid ='" + msGetGID + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            return irnresponse;
        }
        public void DaGettaxnamedropdown(Mdlinvoicesummary values)
        {
            try
            {

                msSQL = " select tax_gid,tax_name,percentage from acp_mst_ttax ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Gettaxnamedropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Gettaxnamedropdown
                        {
                            tax_name = dt["tax_name"].ToString(),
                            tax_gid = dt["tax_gid"].ToString(),
                            tax_percentage = dt["percentage"].ToString(),
                        });
                        values.Gettaxnamedropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        private AuthResponse AuthCall()
        {
            string uri = ConfigurationManager.AppSettings["einvoiceAutenticationURL"].ToString();

            Uri ourUri = new Uri(uri);
            WebRequest request = WebRequest.Create(uri);
            WebResponse response;
            request.Method = "POST";
            request.Headers.Add("gspappid", ConfigurationManager.AppSettings["gspappid"].ToString());
            request.Headers.Add("gspappsecret", ConfigurationManager.AppSettings["gspappsecret"].ToString());
            response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string streamText = reader.ReadToEnd();
            var json = JsonConvert.DeserializeObject<AuthResponse>(streamText);
            return json;
        }
        private qrDownloadResponse GenerateQRcode(string token, string IrnString, string invoice_gid, string request_gid)
        {
            

                string msGetGID;
                string uri = ConfigurationManager.AppSettings["generateQRURL"].ToString();
                Uri ourUri = new Uri(uri);

                msGetGID = objcmnfunctions.GetMasterGID("RQST");
                WebRequest request = WebRequest.Create(uri);
                WebResponse response;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("requestid", msGetGID);
                request.Headers.Add("gstin", ConfigurationManager.AppSettings["einvoicegstin"].ToString());
                request.Headers.Add("Authorization", token);

                qrDownloadResponse qrResponse = new qrDownloadResponse();
                byte[] requestBodyBytes = Encoding.UTF8.GetBytes(IrnString);
                request.ContentLength = requestBodyBytes.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
                }
                byte[] qr_repsonse;
                response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        responseStream.CopyTo(memoryStream);
                        qr_repsonse = memoryStream.ToArray();
                    }
                }

                msSQL = "select company_code from adm_mst_tcompany";
                var lbl_company = objdbconn.GetExecuteScalar(msSQL);

                string filename = "../../erp_documents/" + lbl_company + "/Einvoice/Signed_qr/" + request_gid + ".png";

                string filepath = HttpContext.Current.Server.MapPath(filename);
                // Get the directory path from the physical path
                string directoryPath = Path.GetDirectoryName(filepath);

                // Check if the directory path exists, if not, create it
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                File.WriteAllBytes(filepath, qr_repsonse);

                if (File.Exists(filepath))
                {
                    msSQL = "update rbl_trn_tinvoice set " +
                        "qr_path ='" + filename + "' " +
                        "where invoice_gid ='" + invoice_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    qrResponse.status = true;
                }
                else
                {
                    qrResponse.status = false;
                    qrResponse.message = "Error occured while saving the QR!";
                }
                return qrResponse;
        }

        public void DaGetOnChangeCustomer(Mdlinvoicesummary values, string customer_gid)
        {
            try
            {


                msSQL = " SELECT B.customerbranch_name, B.customercontact_name, B.mobile, B.email, concat(B.address1,', ', B.address2,', ', B.city,' - ', B.zip_code,', ', B.state) AS customer_address " +
                        " FROM crm_mst_tcustomer AS A LEFT JOIN crm_mst_tcustomercontact AS B ON A.customer_gid = B.customer_gid" +
                         " WHERE A.customer_gid = '" + customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getcustomeronchangedetails>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getcustomeronchangedetails
                        {
                            customerbranchname = dt["customerbranch_name"].ToString(),
                            customercontactname = dt["customercontact_name"].ToString(),
                            mobile = dt["mobile"].ToString(),
                            email = dt["email"].ToString(),
                            address = dt["customer_address"].ToString(),
                        });
                        values.Getcustomeronchangedetails = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Change Customer!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaGetOnChangeProduct(Mdlinvoicesummary values, string product_gid)
        {
            try
            {


                msSQL = " SELECT b.productgroup_name, a.product_code, a.hsn_number, a.hsn_desc, c.productuom_name, a.product_price " +
                        " FROM pmr_mst_tproduct AS a LEFT JOIN pmr_mst_tproductgroup AS b ON a.productgroup_gid = b.productgroup_gid LEFT JOIN pmr_mst_tproductuom AS c ON a.productuom_gid = c.productuom_gid " +
                        " WHERE a.product_gid = '" + product_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getproductonchangedetails>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getproductonchangedetails
                        {
                            productgroup_name = dt["productgroup_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            hsn_code = dt["hsn_number"].ToString(),
                            hsn_description = dt["hsn_desc"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            product_price = dt["product_price"].ToString(),
                        });
                        values.Getproductonchangedetails = getModuleList;
                    }
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Change Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaGetOnChangeCurrency(string currencyexchange_gid, Mdlinvoicesummary values)
        {
            try
            {

                msSQL = " select currencyexchange_gid,currency_code,exchange_rate from crm_trn_tcurrencyexchange " +
                    " where currencyexchange_gid='" + currencyexchange_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetOnChangeCurrency>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetOnChangeCurrency
                        {
                            exchange_rate = dt["exchange_rate"].ToString(),
                            currency_code = dt["currency_code"].ToString(),
                        });
                        values.GetOnChangeCurrency = getModuleList;
                    }
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Change Currency!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetOnChangeBranch(string branch_gid, Mdlinvoicesummary values)
        {
            try
            {

                msSQL = " select branch_gid,branch_name,gst_no from hrm_mst_tbranch " +
                    " where branch_gid='" + branch_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetOnChangeBranch>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetOnChangeBranch
                        {
                            GST = dt["gst_no"].ToString(),
                        });
                        values.GetOnChangeBranch = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Change Branch!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaGetEinvoiceData(string invoice_gid, Mdlinvoicesummary values)
        {
            try
            {

                msSQL = "select a.invoice_gid,format(a.roundoff,2) as roundoff,a.invoice_refno," +
                        " date_format(a.invoice_date,'%d-%m-%Y') as invoice_date,a.payment_term," +
                        " k.company_name,k.company_address,p.city,p.postal_code,p.state,p.contact_number,k.company_mail, " +
                        " p.gst_no,r.gst_number, date_format(a.payment_date,'%d-%m-%Y')as payment_date," +
                        " format(a.total_amount,2)as total_amount,a.termsandconditions,a.Tax_amount,a.tax_name,a.tax_percentage," +
                        " format(a.additionalcharges_amount,2)as additionalcharges_amount,format(a.discount_amount,2)as " +
                        " discount_amount ,format(a.invoice_amount,2)as invoice_amount,a.customer_name,a.customer_contactperson," +
                        " a.customer_email,a.customer_address,j.product_total,f.customer_state,f.customer_pin,f.customer_city,  " +
                        " a.invoice_total,a.raised_amount,a.extraadditional_amount,a.extradiscount_amount,i.additional_name," +
                        " h.discount_name,a.extraadditional_code, a.extradiscount_code, format(a.extraadditional_amount,2) as " +
                        " extraadditional_amount, format(a.extradiscount_amount,2) as extradiscount_amount,  " +
                        " case when a.customer_contactnumber is null then g.mobile when a.customer_contactnumber is not null" +
                        " then a.customer_contactnumber end as mobile,a.currency_code,a.exchange_rate, " +
                        " format(a.freight_charges,2)as freight_charges, format(a.buyback_charges,2)as buyback_charges," +
                        " format(a.packing_charges,2)as packing_charges,format(a.insurance_charges,2)as insurance_charges  " +
                        " from rbl_trn_tinvoice a " +
                        " left join pmr_trn_tadditional i on i.additional_gid=a.extraadditional_code  " +
                        " left join crm_mst_tcustomer f on f.customer_gid=a.customer_gid " +
                        " left join crm_mst_tcustomercontact g on g.customer_gid=a.customer_gid  " +
                        " left join pmr_trn_tdiscount h on h.discount_gid = a.extradiscount_code " +
                        " left join rbl_trn_tinvoicedtl j on a.invoice_gid=j.invoice_gid  " +
                        " cross join adm_mst_tcompany k  " +
                        " Left join hrm_mst_tbranch p on p.branch_gid=a.branch_gid  " +
                        " left join crm_mst_tcustomercontact r on r.customer_gid=a.customer_gid  " +
                        " where a.invoice_gid ='" + invoice_gid + "'" +
                        " group by a.invoice_gid order by a.invoice_gid ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                objMySqlDataReader.Read();
                var getEinvoiceAddField = new List<EinvoiceAddField>();
                var lblvendorgstin = "";

                if (objMySqlDataReader != null)
                {
                    string lsfreightcharges, lsbuyback_charges, lspacking_charges, lsinsurance_charges;
                    var buyerDetails_Gstin = objMySqlDataReader["gst_number"].ToString();
                    lblvendorgstin = buyerDetails_Gstin;

                    if (objMySqlDataReader["freight_charges"].ToString() != null)
                    {
                        lsfreightcharges = objMySqlDataReader["freight_charges"].ToString();
                    }
                    else
                    {
                        lsfreightcharges = "0.00";
                    }
                    if (objMySqlDataReader["buyback_charges"].ToString() != null)
                    {
                        lsbuyback_charges = objMySqlDataReader["buyback_charges"].ToString();
                    }
                    else
                    {
                        lsbuyback_charges = "0.00";
                    }
                    if (objMySqlDataReader["packing_charges"].ToString() != null)
                    {
                        lspacking_charges = objMySqlDataReader["packing_charges"].ToString();
                    }
                    else
                    {
                        lspacking_charges = "0.00";
                    }
                    if (objMySqlDataReader["insurance_charges"].ToString() != null)
                    {
                        lsinsurance_charges = objMySqlDataReader["insurance_charges"].ToString();
                    }
                    else
                    {
                        lsinsurance_charges = "0.00";
                    }
                    getEinvoiceAddField.Add(new EinvoiceAddField
                    {
                        lblroundoff = objMySqlDataReader["roundoff"].ToString(),
                        invoice_refno = objMySqlDataReader["invoice_refno"].ToString(),
                        invoice_date = objMySqlDataReader["invoice_date"].ToString(),
                        paymentterms = objMySqlDataReader["payment_term"].ToString(),
                        paymentduedate = objMySqlDataReader["payment_date"].ToString(),
                        buyerDetails_Gstin = objMySqlDataReader["gst_number"].ToString(),
                        buyerDetails_Pos = (lblvendorgstin).Substring(0, 2),
                        buyerDetails_LglNm = objMySqlDataReader["customer_name"].ToString(),
                        customer_contactperson = objMySqlDataReader["customer_contactperson"].ToString(),
                        buyerDetails_Address = objMySqlDataReader["customer_address"].ToString(),
                        buyerDetails_Loc = objMySqlDataReader["customer_city"].ToString(),
                        buyerDetails_Stcd = objMySqlDataReader["customer_state"].ToString(),
                        buyerDetails_Pin = Convert.ToInt32(objMySqlDataReader["customer_pin"].ToString()),

                        sellerDetails_Gstin = objMySqlDataReader["gst_no"].ToString(),
                        sellerDetails_LglNm = objMySqlDataReader["company_name"].ToString(),
                        sellerDetails_TrdNm = objMySqlDataReader["company_name"].ToString(),
                        sellerDetails_Address = objMySqlDataReader["company_address"].ToString(),
                        sellerDetails_Loc = objMySqlDataReader["city"].ToString(),
                        sellerDetails_Pin = Convert.ToInt32(objMySqlDataReader["postal_code"].ToString()),
                        sellerDetails_Stcd = objMySqlDataReader["state"].ToString(),
                        sellercontact_no = objMySqlDataReader["contact_number"].ToString(),
                        selleremail_address = objMySqlDataReader["company_mail"].ToString(),

                        dispatchDetails_Nm = objMySqlDataReader["company_name"].ToString(),
                        dispatchDetails_Address = objMySqlDataReader["company_address"].ToString(),
                        dispatchDetails_Loc = objMySqlDataReader["city"].ToString(),
                        dispatchDetails_Pin = Convert.ToInt32(objMySqlDataReader["postal_code"].ToString()),
                        dispatchDetails_Stcd = objMySqlDataReader["state"].ToString(),

                        shipDetails_Gstin = objMySqlDataReader["gst_number"].ToString(),
                        shipDetails_LglNm = objMySqlDataReader["customer_name"].ToString(),
                        shipDetails_TrdNm = objMySqlDataReader["customer_name"].ToString(),
                        shipDetails_Address = objMySqlDataReader["customer_address"].ToString(),
                        shipDetails_Loc = objMySqlDataReader["customer_city"].ToString(),
                        shipDetails_Stcd = objMySqlDataReader["customer_state"].ToString(),
                        shipDetails_Pin = Convert.ToInt32(objMySqlDataReader["customer_pin"].ToString()),

                        remarks = objMySqlDataReader["product_remarks"].ToString(),
                        vendorEmail = objMySqlDataReader["customer_email"].ToString(),
                        vendorPhoneNo = objMySqlDataReader["mobile"].ToString(),
                        addoncharges = objMySqlDataReader["additionalcharges_amount"].ToString(),
                        additionaldiscount = objMySqlDataReader["discount_amount"].ToString(),
                        discount_amount = objMySqlDataReader["discount_amount"].ToString(),
                        extracharges = objMySqlDataReader["extraadditional_amount"].ToString(),
                        extradiscount = objMySqlDataReader["extradiscount_amount"].ToString(),
                        grandtotal_amountvalue = Convert.ToDouble(objMySqlDataReader["invoice_amount"].ToString()),
                        currencycode_additionaldiscount = objMySqlDataReader["currency_code"].ToString(),
                        currencycode_addon = objMySqlDataReader["currency_code"].ToString(),
                        extracharges_currencycode = objMySqlDataReader["currency_code"].ToString(),
                        extradiscount_currencycode = objMySqlDataReader["currency_code"].ToString(),
                        currencycode_total = objMySqlDataReader["currency_code"].ToString(),
                        currencycode_grandtotal = objMySqlDataReader["currency_code"].ToString(),
                        tax_code = objMySqlDataReader["currency_code"].ToString(),
                        finaltotal_code = objMySqlDataReader["currency_code"].ToString(),
                        extraadditional = objMySqlDataReader["currency_code"].ToString(),
                        additionalamountvalue = objMySqlDataReader["extraadditional_amount"].ToString(),
                        extradiscountamountvalue = objMySqlDataReader["extradiscount_amount"].ToString(),
                        overalltaxt = objMySqlDataReader["Tax_amount"].ToString(),
                        finaltotal = objMySqlDataReader["total_amount"].ToString(),
                        producttotalprice = objMySqlDataReader["price_total"].ToString(),

                        freightcharges = lsfreightcharges,
                        buyback_charges = lsbuyback_charges,
                        packing_charges = lspacking_charges,
                        insurance_charges = lsinsurance_charges,
                        freight_currency_code = objMySqlDataReader["currency_code"].ToString(),
                        buyback_currency_code = objMySqlDataReader["currency_code"].ToString(),
                        packing_currency_code = objMySqlDataReader["currency_code"].ToString(),
                        insurance_currency_code = objMySqlDataReader["currency_code"].ToString(),
                    });
                    values.EinvoiceAddField = getEinvoiceAddField;
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Invoice Data!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaGetEditInvoiceProductSummary(string employee_gid, Mdlinvoicesummary values, string invoice_gid)
        {
            try
            {

                double grand_total = 0.00;
                msSQL = "select invoicedtl_gid, invoice_gid, product_gid, qty_invoice, format(product_price,2) as product_price," +
                    " concat(discount_percentage,'%','  -  ' ,format(discount_amount,2)) as discount, " +
                    " concat(hsn_code, ' / ',hsn_description) as hsn, format(product_total,2) as product_total, uom_gid," +
                    " uom_name, CASE WHEN tax_amount <> 0 and tax_amount2 = 0" +
                    " then concat(tax_name,' - ',tax_amount) WHEN tax_amount = 0 and tax_amount2 <> 0" +
                    " then concat(tax_name2,' - ',tax_amount2)" +
                    " WHEN tax_amount <> 0 and tax_amount2 <> 0" +
                    " then concat(tax_name,' - ',tax_amount,'   ',tax_name2,' - ',tax_amount2)" +
                    " WHEN tax_amount <> 0 and tax_amount2 = 0 then concat(tax_name,' - ',tax_amount) " +
                    " WHEN tax_amount = 0 and tax_amount2 <> 0 then concat(tax_name2,' - ',tax_amount2) " +
                    " else concat(tax_name,' - ',tax_amount,'   ',tax_name2,' - ',tax_amount2) end as tax," +
                    " display_field, tax1_gid, tax2_gid, product_name, productgroup_gid, " +
                    " productgroup_name,  employee_gid,  selling_price,  product_code," +
                    " customerproduct_code,  tax_percentage, tax_percentage2, sl_no, " +
                    " format(vendor_price,2) as vendor_price from rbl_tmp_tinvoicedtl where employee_gid='" + employee_gid + "'" +
                    " union" +
                    " select invoicedtl_gid, invoice_gid, product_gid, qty_invoice, format(product_price,2) as product_price," +
                    " concat(discount_percentage,'%','  -  ' ,format(discount_amount,2)) as discount, " +
                    " concat(hsn_code, ' / ',hsn_description) as hsn, format(product_total,2) as product_total, uom_gid," +
                    " uom_name, CASE WHEN tax_amount <> 0 and tax_amount2 = 0" +
                    " then concat(tax_name,' - ',tax_amount) WHEN tax_amount = 0 and tax_amount2 <> 0" +
                    " then concat(tax_name2,' - ',tax_amount2)" +
                    " WHEN tax_amount <> 0 and tax_amount2 <> 0" +
                    " then concat(tax_name,' - ',tax_amount,'   ',tax_name2,' - ',tax_amount2)" +
                    " WHEN tax_amount <> 0 and tax_amount2 = 0 then concat(tax_name,' - ',tax_amount) " +
                    " WHEN tax_amount = 0 and tax_amount2 <> 0 then concat(tax_name2,' - ',tax_amount2) " +
                    " else concat(tax_name,' - ',tax_amount,'   ',tax_name2,' - ',tax_amount2) end as tax," +
                    " display_field, tax1_gid, tax2_gid, product_name, productgroup_gid, " +
                    " productgroup_name,  employee_gid,  selling_price,  product_code," +
                    " customerproduct_code,  tax_percentage, tax_percentage2, sl_no, " +
                    " format(vendor_price,2) as vendor_price from rbl_trn_tinvoicedtl where invoice_gid='" + invoice_gid + "' order by invoicedtl_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<editinvoiceproductsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        grand_total += double.Parse(dt["product_total"].ToString());
                        getModuleList.Add(new editinvoiceproductsummary_list
                        {
                            invoicedtl_gid = dt["invoicedtl_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            customerproduct_code = dt["customerproduct_code"].ToString(),
                            uom_name = dt["uom_name"].ToString(),
                            qty_invoice = dt["qty_invoice"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            discount = dt["discount"].ToString(),
                            selling_price = dt["selling_price"].ToString(),
                            product_total = dt["product_total"].ToString(),
                            tax = dt["tax"].ToString(),
                            hsn = dt["hsn"].ToString(),
                        });
                        values.editinvoiceproductsummarylist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
                values.grand_total = Math.Round(grand_total, 2);
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Edit Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
       "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public invoicelist DaGetEditInvoice(string invoice_gid)
        {
            try
            {

                invoicelist objinvoiceproductlist = new invoicelist();
                var lblvendorgstin = "";

                msSQL = " select  invoice_gid,customer_name, customer_contactperson, customer_contactnumber, customer_email," +
                    " customer_address, branch_gid, currency_code, exchange_rate, invoice_remarks, invoice_gid," +
                    " invoice_refno, invoice_date, payment_term, payment_date, invoice_amount, additionalcharges_amount," +
                    " discount_amount, freight_charges, buyback_charges, packing_charges, insurance_charges," +
                    " roundoff, total_amount, termsandconditions from rbl_trn_tinvoice where invoice_gid= '" + invoice_gid + "'";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    string lsfreightcharges, lsbuyback_charges, lspacking_charges, lsinsurance_charges;

                    objMySqlDataReader.Read();
                    objinvoiceproductlist.invoice_gid = objMySqlDataReader["invoice_gid"].ToString();

                    objinvoiceproductlist.customergid = objMySqlDataReader["customer_name"].ToString();
                    objinvoiceproductlist.customercontactperson = objMySqlDataReader["customer_contactperson"].ToString();
                    objinvoiceproductlist.customercontactnumber = objMySqlDataReader["customer_contactnumber"].ToString();
                    objinvoiceproductlist.customeremailaddress = objMySqlDataReader["customer_email"].ToString();
                    objinvoiceproductlist.customeraddress = objMySqlDataReader["customer_address"].ToString();
                    objinvoiceproductlist.branchgid = objMySqlDataReader["branch_gid"].ToString();
                    objinvoiceproductlist.currencygid = objMySqlDataReader["currency_code"].ToString();
                    objinvoiceproductlist.exchangerate = objMySqlDataReader["exchange_rate"].ToString();
                    objinvoiceproductlist.remarks = objMySqlDataReader["invoice_remarks"].ToString();
                    objinvoiceproductlist.invoiceref_no = objMySqlDataReader["invoice_refno"].ToString();
                    objinvoiceproductlist.invoicedate = objMySqlDataReader["invoice_date"].ToString();
                    objinvoiceproductlist.paymentterm = objMySqlDataReader["payment_term"].ToString();
                    objinvoiceproductlist.duedate = Convert.ToDateTime(objMySqlDataReader["payment_date"].ToString());
                    objinvoiceproductlist.grandtotal = objMySqlDataReader["invoice_amount"].ToString();
                    objinvoiceproductlist.addoncharges = objMySqlDataReader["additionalcharges_amount"].ToString();
                    objinvoiceproductlist.invoicediscountamount = objMySqlDataReader["discount_amount"].ToString();
                    objinvoiceproductlist.frieghtcharges = objMySqlDataReader["freight_charges"].ToString();
                    objinvoiceproductlist.buybackcharges = objMySqlDataReader["buyback_charges"].ToString();
                    objinvoiceproductlist.forwardingCharges = objMySqlDataReader["packing_charges"].ToString();
                    objinvoiceproductlist.insurancecharges = objMySqlDataReader["insurance_charges"].ToString();
                    objinvoiceproductlist.roundoff = objMySqlDataReader["roundoff"].ToString();
                    objinvoiceproductlist.grandtotal = objMySqlDataReader["total_amount"].ToString();
                    objMySqlDataReader.Close();
                }
                return objinvoiceproductlist;
            }
            catch (Exception ex)
            {

                ex.ToString();
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return null;
            }
          
        }
        public einvoicelist DaGetEInvoicedata(string invoice_gid)
        {
            try
            {

                var lblvendorgstin = "";
                einvoicelist objeinvoicelist = new einvoicelist();

                msSQL = "select a.invoice_gid,format(a.roundoff,2) as roundoff,a.invoice_refno," +
                       " date_format(a.invoice_date,'%d-%m-%Y') as invoice_date,a.payment_term," +
                       " k.company_name,k.company_address,p.city,p.postal_code,p.state,p.contact_number,k.company_mail, " +
                       " p.gst_no,r.gst_number, date_format(a.payment_date,'%d-%m-%Y')as payment_date," +
                       " format(a.total_amount,2)as total_amount,a.termsandconditions,a.Tax_amount,a.tax_name,a.tax_percentage," +
                       " format(a.additionalcharges_amount,2)as additionalcharges_amount,format(a.discount_amount,2)as " +
                       " discount_amount ,format(a.invoice_amount,2)as invoice_amount,a.customer_name,a.customer_contactperson," +
                       " a.customer_email,a.customer_address,format(j.product_total,2) as product_total,f.customer_state,f.customer_pin,f.customer_city,  " +
                       " a.invoice_total,a.raised_amount,a.extraadditional_amount,a.extradiscount_amount,i.additional_name," +
                       " h.discount_name,a.extraadditional_code, a.extradiscount_code, format(a.extraadditional_amount,2) as " +
                       " extraadditional_amount, format(a.extradiscount_amount,2) as extradiscount_amount,  " +
                       " case when a.customer_contactnumber is null then g.mobile when a.customer_contactnumber is not null" +
                       " then a.customer_contactnumber end as mobile,a.currency_code,a.exchange_rate, " +
                       " format(a.freight_charges,2)as freight_charges, format(a.buyback_charges,2)as buyback_charges," +
                       " format(a.packing_charges,2)as packing_charges,format(a.insurance_charges,2)as insurance_charges  " +
                       " from rbl_trn_tinvoice a " +
                       " left join pmr_trn_tadditional i on i.additional_gid=a.extraadditional_code  " +
                       " left join crm_mst_tcustomer f on f.customer_gid=a.customer_gid " +
                       " left join crm_mst_tcustomercontact g on g.customer_gid=a.customer_gid  " +
                       " left join pmr_trn_tdiscount h on h.discount_gid = a.extradiscount_code " +
                       " left join rbl_trn_tinvoicedtl j on a.invoice_gid=j.invoice_gid  " +
                       " cross join adm_mst_tcompany k  " +
                       " Left join hrm_mst_tbranch p on p.branch_name=k.company_name  " +
                       " left join crm_mst_tcustomercontact r on r.customer_gid=a.customer_gid  " +
                       " where a.invoice_gid ='" + invoice_gid + "'" +
                       " group by a.invoice_gid order by a.invoice_gid ";

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    objMySqlDataReader.Read();
                    var buyerDetails_Gstin = objMySqlDataReader["gst_number"].ToString();
                    lblvendorgstin = buyerDetails_Gstin;
                    objeinvoicelist.invoice_gid = objMySqlDataReader["invoice_gid"].ToString();

                    objeinvoicelist.invoice_refno = objMySqlDataReader["invoice_refno"].ToString();

                    objeinvoicelist.invoice_date = objMySqlDataReader["invoice_date"].ToString();
                    objeinvoicelist.payment_term = objMySqlDataReader["payment_term"].ToString();
                    objeinvoicelist.payment_date = objMySqlDataReader["payment_date"].ToString();
                    objeinvoicelist.gst_number = objMySqlDataReader["gst_number"].ToString();
                    objeinvoicelist.company_name = objMySqlDataReader["company_name"].ToString();
                    objeinvoicelist.postalcode = objMySqlDataReader["postal_code"].ToString();
                    objeinvoicelist.gst_no = objMySqlDataReader["gst_no"].ToString();
                    objeinvoicelist.sellercontact_number = objMySqlDataReader["contact_number"].ToString();

                    objeinvoicelist.dispatchDetails_Nm = objMySqlDataReader["company_name"].ToString();
                    objeinvoicelist.dispatchDetails_Address = objMySqlDataReader["company_address"].ToString();
                    objeinvoicelist.dispatchDetails_Loc = objMySqlDataReader["city"].ToString();
                    objeinvoicelist.dispatchDetails_Pin = objMySqlDataReader["postal_code"].ToString();
                    objeinvoicelist.dispatchDetails_Stcd = objMySqlDataReader["state"].ToString();

                    objeinvoicelist.shipDetails_Gstin = objMySqlDataReader["gst_number"].ToString();
                    objeinvoicelist.shipDetails_LglNm = objMySqlDataReader["customer_name"].ToString();
                    objeinvoicelist.shipDetails_TrdNm = objMySqlDataReader["customer_name"].ToString();
                    objeinvoicelist.shipDetails_Address = objMySqlDataReader["customer_address"].ToString();
                    objeinvoicelist.shipDetails_Loc = objMySqlDataReader["customer_city"].ToString();
                    objeinvoicelist.shipDetails_Stcd = objMySqlDataReader["customer_state"].ToString();
                    objeinvoicelist.shipDetails_Pin = objMySqlDataReader["customer_pin"].ToString();

                    objeinvoicelist.customer_address = objMySqlDataReader["customer_address"].ToString();
                    objeinvoicelist.customer_pin = objMySqlDataReader["customer_pin"].ToString();
                    objeinvoicelist.buyerDetails_Pos = (lblvendorgstin).Substring(0, 2);
                    objeinvoicelist.customer_contactperson = objMySqlDataReader["customer_contactperson"].ToString();
                    objeinvoicelist.customer_state = objMySqlDataReader["customer_state"].ToString();
                    objeinvoicelist.customer_city = objMySqlDataReader["customer_city"].ToString();
                    objeinvoicelist.company_address = objMySqlDataReader["company_address"].ToString();
                    objeinvoicelist.state = objMySqlDataReader["state"].ToString();
                    objeinvoicelist.company_mail = objMySqlDataReader["company_mail"].ToString();
                    objeinvoicelist.city = objMySqlDataReader["city"].ToString();
                    objeinvoicelist.customer_name = objMySqlDataReader["customer_name"].ToString();
                    objeinvoicelist.mobile = objMySqlDataReader["mobile"].ToString();
                    objeinvoicelist.customer_email = objMySqlDataReader["customer_email"].ToString();

                    objeinvoicelist.netamount = double.Parse(objMySqlDataReader["product_total"].ToString());
                    objeinvoicelist.addoncharges = objMySqlDataReader["additionalcharges_amount"].ToString();
                    objeinvoicelist.invoicediscountamount = objMySqlDataReader["discount_amount"].ToString();
                    objeinvoicelist.frieghtcharges = objMySqlDataReader["freight_charges"].ToString();
                    objeinvoicelist.buybackcharges = objMySqlDataReader["buyback_charges"].ToString();
                    objeinvoicelist.packing_charges = double.Parse(objMySqlDataReader["packing_charges"].ToString());
                    objeinvoicelist.insurancecharges = objMySqlDataReader["insurance_charges"].ToString();
                    objeinvoicelist.roundoff = objMySqlDataReader["roundoff"].ToString();
                    objeinvoicelist.invoice_amount = objMySqlDataReader["invoice_amount"].ToString();

                    objMySqlDataReader.Close();
                }
                return objeinvoicelist;
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                ex.ToString();
                return null;
            }
            
        }
        public CancelIRN_ResponseData FNCancelIRN(string token, string CancelInvoiceData, string invoice_gid)
        {
            var json = new CancelIRN_ResponseData();
            string msGetGID;
            string uri = ConfigurationManager.AppSettings["cancleIRN"].ToString();
            Uri ourUri = new Uri(uri);
            string requestBody = CancelInvoiceData;
            msGetGID = objcmnfunctions.GetMasterGID("RQST");
            WebRequest request = WebRequest.Create(uri);
            WebResponse response;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("user_name", ConfigurationManager.AppSettings["einvoiceuser_name"].ToString());
            request.Headers.Add("password", ConfigurationManager.AppSettings["einvoicepwd"].ToString());
            request.Headers.Add("requestid", msGetGID);
            request.Headers.Add("gstin", ConfigurationManager.AppSettings["einvoicegstin"].ToString());
            request.Headers.Add("Authorization", token);

            byte[] requestBodyBytes = Encoding.UTF8.GetBytes(requestBody);
            request.ContentLength = requestBodyBytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
            }

            response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string streamText = reader.ReadToEnd();

            msSQL = "insert into rblt_trn_teinvoice_responselog (" +
                    "request_gid, " +
                    "reponse_json, " +
                    "request_json, " +
                    "invoice_gid" +
                    ") values ( " +
                    "'" + msGetGID + "'," +
                    "'" + streamText + "'," +
                    "'" + CancelInvoiceData + "'," +
                    "'" + invoice_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            try
            {

                json = JsonConvert.DeserializeObject<CancelIRN_ResponseData>(streamText);
                if (json.success == true)
                {
                    msSQL = "update rblt_trn_teinvoice_responselog set " +
                        "success ='" + json.success + "'," +
                        "message ='" + json.message.ToString().Replace("'", "") + "'," +
                        "irncancel_date ='" + Convert.ToDateTime(json.result.CancelDate).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "Irn ='" + json.result.Irn + "'" +
                        "where request_gid ='" + msGetGID + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msSQL = "update rbl_trn_tinvoice set " +
                            "irn ='" + json.result.Irn +
                            "' ,irncancel_date= '" + Convert.ToDateTime(json.result.CancelDate).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " cancelirn_limit='N' " +
                            "where invoice_gid ='" + invoice_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {
                    msSQL = "select message from rblt_trn_teinvoice_responselog where request_gid ='" + msGetGID + "'";

                    string errorMessage = objdbconn.GetExecuteScalar(msSQL);
                }
            }
            catch (Exception ex)
            {

                msSQL = "update rblt_trn_teinvoice_responselog set " +
                    "message ='Exception occured while generating IRN - " + ex.ToString().Replace("'", "") + "' " +
                    "where request_gid ='" + msGetGID + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            return json;
        }
        public void DaCreditNote(creditnote_list values)
        {
            try
            {
                InvoiceData invoicedata = new InvoiceData();
                var transactionDetails = new TransactionDetails();
                var documentDetails = new DocumentDetails();
                var sellerDetails = new SellerDetails();
                var buyerDetails = new BuyerDetails();
                var dispatchDetails = new DispatchDetails();
                var shipDetails = new ShipDetails();
                var itemDetails = new List<ItemDetails>();

                string request_body = "";
                msSQL = " select request_json from rblt_trn_teinvoice_responselog where invoice_gid='" + values.invoice_gid + "' and message='IRN generated successfully'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objMySqlDataReader.Read();
                    request_body = objMySqlDataReader["request_json"].ToString();
                }
                InvoiceData lsrequest_body = JsonConvert.DeserializeObject<InvoiceData>(request_body);

                invoicedata.Version = "1.1";
                transactionDetails.TaxSch = "GST";
                transactionDetails.SupTyp = lsrequest_body.TranDtls.SupTyp;
                transactionDetails.RegRev = "N";
                transactionDetails.IgstOnIntra = lsrequest_body.TranDtls.IgstOnIntra;

                invoicedata.TranDtls = transactionDetails;

                documentDetails.Typ = "CRN";
                documentDetails.No = lsrequest_body.DocDtls.No;
                documentDetails.Dt = DateTime.Now.ToString("dd/MM/yyyy");

                invoicedata.DocDtls = documentDetails;

                invoicedata.SellerDtls = lsrequest_body.SellerDtls;
                invoicedata.BuyerDtls = lsrequest_body.BuyerDtls;
                invoicedata.DispDtls = lsrequest_body.DispDtls;
                invoicedata.ShipDtls = lsrequest_body.ShipDtls;
                invoicedata.ItemList = lsrequest_body.ItemList;
                invoicedata.ValDtls = lsrequest_body.ValDtls;

                var einvoicejson = JsonConvert.SerializeObject(invoicedata);

                string lstoken;
                string lsaccess_token = "";
                string lsexpiry_date1 = "";

                msSQL = "select einvoice_token,einvoice_tokenexpiry from adm_mst_tcompany";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    lstoken = objMySqlDataReader["einvoice_token"].ToString();
                    lsexpiry_date1 = objMySqlDataReader["einvoice_tokenexpiry"].ToString();
                    if (lstoken == "")
                    {
                        var auth = AuthCall();
                        msSQL = "update adm_mst_tcompany set einvoice_tokenexpiry=Now() + INTERVAL 28 DAY," +
                                " einvoice_token='" + auth.access_token + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            lsaccess_token = auth.access_token;
                        }
                    }
                    else
                    {
                        var lsexpiry_date = Convert.ToDateTime(lsexpiry_date1);
                        if (lsexpiry_date < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                        {
                            var auth = AuthCall();
                            msSQL = "update adm_mst_tcompany set einvoive_tokenexpiry=Now() + INTERVAL 28 DAY,einvoive_token='" + auth.access_token + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult == 1)
                            {
                                lsaccess_token = auth.access_token;
                            }
                            else
                            {
                                lsaccess_token = lstoken;
                            }
                        }
                        else
                        {
                            lsaccess_token = lstoken;
                        }
                    }
                }
                string token = "bearer" + lsaccess_token;
                var irn = new ApiResponse();
                string lsvar = "CRN";
                irn = GenerateIRN(token, einvoicejson, values.invoice_gid, lsvar);
                if (string.IsNullOrEmpty(irn.qr_code) == false)
                {
                    var signed_qr = new qr_request();
                    signed_qr.data = irn.qr_code;
                    string qrReq_json = JsonConvert.SerializeObject(signed_qr);
                    var qrcode = new qrDownloadResponse();
                    qrcode = GenerateQRcode(token, qrReq_json, values.invoice_gid, irn.request_id);
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Credit Note!";
            }
            finally
            {
                if (objMySqlDataReader != null)
                    objMySqlDataReader.Close();
                objdbconn.CloseConn();
            }
        }
        public GetIRNDetails DaGetIRNDetails(string invoice_gid, GetIRNDetails values)
        {
            try
            {

                msSQL = "select invoice_refno,customer_name,format(invoice_amount,2) as invoice_amount,date_format(invoice_date,'%d-%b-%Y') as invoice_date ," +
                       "invoice_gid,irn from rbl_trn_tinvoice where invoice_gid='" + invoice_gid + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);

                if (objMySqlDataReader.HasRows == true)
                {
                    objMySqlDataReader.Read();
                    values.irn = objMySqlDataReader["irn"].ToString();
                    values.invoice_date = objMySqlDataReader["invoice_date"].ToString();
                    values.invoice_refno = objMySqlDataReader["invoice_refno"].ToString();
                    values.invoice_amount = objMySqlDataReader["invoice_amount"].ToString();
                    values.customer_name = objMySqlDataReader["customer_name"].ToString();
                    objMySqlDataReader.Close();
                }
                return values;

            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                ex.ToString();
                return null;
               
            }
           
        }
        public void DaSalesinvoiceSummary(Mdlinvoicesummary values)
        {
            try
            {

                msSQL = " select distinct b.salesorder_gid as directorder_gid,c.leadbank_gid,d.lead2campaign_gid,b.salesorder_date as directorder_date,b.salesorder_gid as directorder_refno,b.customer_name, " +
                        " b.customer_contact_person, cast(concat(b.so_referenceno1," +
                        " if(b.so_referencenumber<>'',concat(' ',' | ',' ',b.so_referencenumber),'') ) as char) as so_referenceno1,b.progressive_flag, " +
                        " concat(b.customer_contact_person,' / ',b.customer_email,' / ',b.customer_mobile) as mobile, " +
                        " format(b.Grandtotal,2) as grandtotal,format(b.invoice_amount,2) as invoice_amout,format(b.Grandtotal-b.invoice_amount,2) as outstanding_amount, " +
                        " b.currency_code,invoice_flag as status,b.customer_gid, b.so_type as order_type " +
                        " from smr_trn_tsalesorder b " +
                        " left join smr_trn_tdeliveryorder a on b.salesorder_gid=a.salesorder_gid " +
                        " left join crm_trn_tleadbank c on c.customer_gid = b.customer_gid " +
                        " left join crm_trn_tlead2campaign d on d.leadbank_gid = c.leadbank_gid " +
                        " where 1=1 and case when so_type='Sales' then  b.salesorder_status not in  ('SO Amended','Rejected','Cancelled','Approve Pending')" +
                        " else b.salesorder_status in('Delivery Done Partial','Delivery Completed','Approved') end" +
                        " order by directorder_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<salesinvoicesummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new salesinvoicesummary_list
                        {
                            directorder_gid = dt["directorder_gid"].ToString(),
                            directorder_date = Convert.ToDateTime(dt["directorder_date"].ToString()),
                            so_referenceno1 = dt["so_referenceno1"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            mobile = dt["mobile"].ToString(),
                            order_type = dt["order_type"].ToString(),
                            currency_code = dt["currency_code"].ToString(),
                            grandtotal = dt["grandtotal"].ToString(),
                            invoice_amout = dt["invoice_amout"].ToString(),
                            outstanding_amount = dt["outstanding_amount"].ToString(),
                            status = dt["status"].ToString(),
                            leadbank_gid = dt["leadbank_gid"].ToString(),
                            lead2campaign_gid = dt["lead2campaign_gid"].ToString(),
                            customer_gid = dt["customer_gid"].ToString(),
                        });
                        values.salesinvoicesummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
              
                values.message = "Exception occured while loading Sales Invoice!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public SalesInvoicelist DaGetSalesinvoicedata(string directorder_gid)
        {
            try
            {

                SalesInvoicelist objSalesInvoicelist = new SalesInvoicelist();
                {
                    msSQL = " select a.salesorder_gid as serviceorder_gid,h.tax_amount,concat(a.so_referenceno1,case when so_referencenumber='' then '' else concat(' ','-',' ',so_referencenumber) end )as so_reference,date_format(a.salesorder_date,'%d/%m/%Y') as serviceorder_date, " +
                            " a.customer_name,a.currency_code,a.exchange_rate,b.customer_gid,format(a.grandtotal, 2) as grand_total,q.branch_name,a.salesorder_gid as serviceorder_id,d.shipper_address,d.consignee_address," +
                            " a.mawb_no,a.hawb_no,a.invoice_no,a.flight_no,a.pkgs,a.cbm,a.gr_wt,a.lfrom,a.lto,a.sb_no,a.assesable_amount,a.ch_wt,a.igm_no,date_format(a.igm_date,'%Y-%m-%d') as igm_date," +
                            " date_format(a.hawb_date,'%Y-%m-%d') as hawb_date,date_format(a.mawb_date,'%Y-%m-%d') as mawb_date,date_format(a.be_date,'%Y-%m-%d') as be_date,date_format(a.flight_date,'%Y-%m-%d') as flight_date," +
                            " a.customer_contact_person as customercontact_name,a.customer_email as email,b.customer_code,a.termsandconditions,a.customer_mobile," +
                            " format(a.addon_charge, 2) as addon_amount ,format(a.additional_discount, 2) as discount_amount,a.customer_address,format(a.gst_amount,2) as gst_amount," +
                            " format(a.freight_charges,2) as freight_charges,a.invoice_amount, " +
                            " format(a.packing_charges,2)as packing_charges, " +
                            " format(a.buyback_charges,2)as buyback_charges,b.customer_state,b.customer_city,a.description,a.others," +
                            " format(a.insurance_charges,2)as insurance_charges " +
                            " from smr_trn_tsalesorder a" +
                            " left join lgs_trn_tshipment d on a.salesorder_gid=d.salesorder_gid " +
                            " left join crm_mst_tcustomer b on a.customer_gid=b.customer_gid " +
                            " left join hrm_mst_tbranch q on q.branch_gid=a.branch_gid " +
                            " left join smr_trn_tsalesorderdtl h on h.salesorder_gid = a.salesorder_gid  " +
                            " left join crm_mst_tcustomercontact c on b.customer_gid = c.customer_gid " +
                            " where a.salesorder_gid='" + directorder_gid + "'";
                }

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    objMySqlDataReader.Read();
                    objSalesInvoicelist.serviceorder_gid = objMySqlDataReader["serviceorder_gid"].ToString();
                    objSalesInvoicelist.tax_amount = objMySqlDataReader["tax_amount"].ToString();
                    objSalesInvoicelist.so_reference = objMySqlDataReader["so_reference"].ToString();
                    objSalesInvoicelist.serviceorder_date = objMySqlDataReader["serviceorder_date"].ToString();
                    objSalesInvoicelist.discount_amount = objMySqlDataReader["discount_amount"].ToString();
                    objSalesInvoicelist.addon_amount = objMySqlDataReader["addon_amount"].ToString();
                    objSalesInvoicelist.grand_total = objMySqlDataReader["grand_total"].ToString();
                    objSalesInvoicelist.customer_name = objMySqlDataReader["customer_name"].ToString();
                    objSalesInvoicelist.customer_address = objMySqlDataReader["customer_address"].ToString();
                    objSalesInvoicelist.customer_mobile = objMySqlDataReader["customer_mobile"].ToString();
                    objSalesInvoicelist.email = objMySqlDataReader["email"].ToString();
                    objSalesInvoicelist.customercontact_name = objMySqlDataReader["customercontact_name"].ToString();
                    objSalesInvoicelist.customer_gid = objMySqlDataReader["customer_gid"].ToString();
                    objSalesInvoicelist.branch_name = objMySqlDataReader["branch_name"].ToString();
                    objSalesInvoicelist.termsandconditions = objMySqlDataReader["termsandconditions"].ToString();
                    objSalesInvoicelist.gst_amount = objMySqlDataReader["gst_amount"].ToString();
                    objSalesInvoicelist.freight_charges = objMySqlDataReader["freight_charges"].ToString();
                    objSalesInvoicelist.packing_charges = objMySqlDataReader["packing_charges"].ToString();
                    objSalesInvoicelist.buyback_charges = objMySqlDataReader["buyback_charges"].ToString();
                    objSalesInvoicelist.insurance_charges = objMySqlDataReader["insurance_charges"].ToString();
                    objSalesInvoicelist.invoice_no = objMySqlDataReader["invoice_no"].ToString();
                    objSalesInvoicelist.description = objMySqlDataReader["description"].ToString();
                    objSalesInvoicelist.currency_code = objMySqlDataReader["currency_code"].ToString();
                    objSalesInvoicelist.exchange_rate = objMySqlDataReader["exchange_rate"].ToString();
                    objSalesInvoicelist.invoice_amount = objMySqlDataReader["invoice_amount"].ToString();
                    objMySqlDataReader.Close();
                }
                return objSalesInvoicelist;
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                ex.ToString();
                return null;
            }
           
        }
        public void DaGetproductsummarydata(Mdlinvoicesummary values, string directorder_gid)
        {
            try
            {

                msSQL = " select a.product_gid,if(a.customerproduct_code='&nbsp;',' ',a.customerproduct_code) as customerproduct_code,a.salesorderdtl_gid as serviceorderdtl_gid, " +
                " a.tax1_gid,a.tax2_gid,a.tax3_gid,concat(a.tax_name,'-',tax_amount,' ',a.tax_name2,'-',a.tax_amount2,' ',a.tax_name3,'-',a.tax_amount3)as tax,concat(a.tax_name,'-',tax_amount) as tax1,format(a.qty_quoted,2) as qty_quoted,a.uom_gid, " +
                " format((replace(a.vendor_price,',','')-a.discount_amount),2) as amount ,format(a.tax_amount,2) as tax_amount1, " +
                " format(a.tax_amount2,2) as tax_amount2,format(a.tax_amount3,2) as tax_amount3, format(a.price,2) as total_amount, " +
                " a.display_field as description, a.product_name,a.tax_name as tax_name1,a.tax_name2,a.tax_name3," +
                " format(a.margin_percentage,2) as disc_percentage,format(a.margin_amount,2) as discount_amount,format(a.product_price,2) as product_price,a.vendor_price as vendoramount, " +
                " a.margin_percentage as discount_percentage from smr_trn_tsalesorderdtl a " +
                " left join pmr_mst_tproduct b on a.product_gid = b.product_gid " +
                " left join smr_trn_tsalesorder c on a.salesorder_gid=c.salesorder_gid " +
                " where a.salesorder_gid='" + directorder_gid + "'" +
                " order by a.salesorderdtl_gid asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<salesinvoiceproduct_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new salesinvoiceproduct_list
                        {
                            customerproduct_code = dt["customerproduct_code"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            description = dt["description"].ToString(),
                            qty_quoted = dt["qty_quoted"].ToString(),
                            vendoramount = dt["vendoramount"].ToString(),
                            discount_amount = dt["discount_amount"].ToString(),
                            amount1 = dt["product_price"].ToString(),
                            total_amount = dt["total_amount"].ToString(),
                            tax = dt["tax"].ToString(),
                            tax1 = dt["tax1"].ToString(),
                        });
                        values.salesinvoiceproduct_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void Dasalesinvoicesubmit(string employee_gid, salesinvoice_list values)
        {

            try
            {

                msSQL = "select branch_gid from hrm_mst_tbranch where branch_name='" + values.invoiceaccounting_branch + "'";
                string lsbranchgid = objdbconn.GetExecuteScalar(msSQL);


                string lstype1 = "services";
                string ls_referenceno = objcmnfunctions.GetMasterGID("INV");

                msINGetGID = objcmnfunctions.GetMasterGID("SIVT");

                msSQL = " insert into rbl_trn_tinvoice(" +
                     " invoice_gid," +
                     " invoice_date," +
                     " payment_term, " +
                     " payment_date," +
                     " invoice_type," +
                     " invoice_reference," +
                     " customer_gid," +
                     " customer_name," +
                     " customer_contactperson," +
                     " customer_contactnumber," +
                     " customer_address," +
                     " customer_email," +
                     " branch_gid," +
                     " total_amount," +
                     " invoice_amount," +
                     " invoice_refno," +
                     " invoice_status," +
                     " invoice_flag," +
                     " user_gid," +
                     " total_amount_L," +
                     " invoice_amount_L," +
                     " invoice_remarks," +
                     " termsandconditions," +
                     " currency_code," +
                     " exchange_rate," +
                     " created_date," +
                     " freight_charges," +
                     " packing_charges," +
                     " insurance_charges " +
                     " ) values (" +
                     " '" + msINGetGID + "'," +
                     "'" + values.invoice_date.ToString("yyyy-MM-dd ") + "'," +
                     "'" + values.invoiceaccounting_payterm + "'," +
                     "'" + values.invoiceaccounting_duedate.ToString("yyyy-MM-dd ") + "'," +
                     "'" + lstype1 + "'," +
                     "'" + values.invoiceaccounting_salesorder_gid + "'," +
                     "'" + values.customer_gid + "'," +
                     "'" + values.invoiceaccounting_customername + "'," +
                     "'" + values.invoiceaccounting_contactperson + "'," +
                     "'" + values.invoiceaccounting_contactnumber + "'," +
                     "'" + values.invoiceaccounting_customeraddress + "'," +
                     "'" + values.invoiceaccounting_email + "'," +
                     "'" + lsbranchgid + "'," +
                     "'" + values.invoiceaccounting_grandtotal.ToString().Replace(",", "") + "'," +
                     "'" + values.invoiceaccounting_grandtotal.ToString().Replace(",", "") + "'," +
                     "'" + ls_referenceno + "'," +
                     "'Payment Pending'," +
                     "'Invoice Approved'," +
                     "'" + employee_gid + "'," +
                     "'" + values.invoiceaccounting_grandtotal.ToString().Replace(",", "") + "'," +
                     "'" + values.invoiceaccounting_grandtotal.ToString().Replace(",", "") + "'," +
                     "'" + values.invoiceaccounting_remarks + "'," +
                     "'" + values.invoiceaccounting_termsandconditions + "', " +
                     "'" + values.invoiceaccounting_currency + "', " +
                     "'" + values.invoiceaccounting_exchangerate + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     "'" + values.invoiceaccounting_freightcharges + "'," +
                     "'" + values.invoiceaccounting_packingcharges + "'," +
                     "'" + values.invoiceaccounting_insurancecharges + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " select a.product_gid,a.product_code,a.uom_gid,a.uom_name,a.product_price,a.productgroup_gid,a.productgroup_name,if(a.customerproduct_code='&nbsp;',' ',a.customerproduct_code) as customerproduct_code,a.product_price,a.salesorderdtl_gid as serviceorderdtl_gid, " +
                    " a.selling_price,a.tax1_gid,a.tax2_gid,a.tax3_gid,a.tax_amount,a.tax_name,concat(a.tax_name,'-',tax_amount,' ',a.tax_name2,'-',a.tax_amount2,' ',a.tax_name3,'-',a.tax_amount3)as tax,format(a.qty_quoted,2) as qty_quoted,a.uom_gid, " +
                    " format((replace(a.vendor_price,',','')-a.discount_amount),2) as amount ,format(a.tax_amount,2) as tax_amount1, " +
                    " format(a.tax_amount2,2) as tax_amount2,format(a.tax_amount3,2) as tax_amount3, format(a.price,2) as total_amount, " +
                    " a.display_field as description, a.product_name,a.tax_name as tax_name1,a.tax_name2,a.tax_name3," +
                    " format(a.margin_percentage,2) as disc_percentage,format(a.margin_amount,2) as discount_amount,format(a.product_price,2) as amount1,a.vendor_price as vendoramount, " +
                    " a.margin_percentage as discount_percentage from smr_trn_tsalesorderdtl a " +
                    " left join pmr_mst_tproduct b on a.product_gid = b.product_gid " +
                    " left join smr_trn_tsalesorder c on a.salesorder_gid=c.salesorder_gid " +
                    " where a.salesorder_gid='" + values.invoiceaccounting_salesorder_gid + "'" +
                    " order by a.salesorderdtl_gid asc";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("SIVC");
                        msSQL = " insert into rbl_trn_tinvoicedtl ( " +
                                                    " invoicedtl_gid," +
                                                    " invoice_gid," +
                                                    " product_gid," +
                                                    " product_price," +
                                                    " discount_percentage," +
                                                    " discount_amount," +
                                                    " uom_gid," +
                                                    " tax_amount," +
                                                    " tax_name," +
                                                    " display_field," +
                                                    " tax1_gid," +
                                                    " product_name," +
                                                    " employee_gid, " +
                                                    " product_code," +
                                                    " customerproduct_code, " +
                                                    " uom_name," +
                                                    " productgroup_gid, " +
                                                    " productgroup_name," +
                                                    " selling_price," +
                                                    " product_price_L " +
                                                    " ) values ( " +
                                                    "'" + msGetGid + "'," +
                                                    "'" + msINGetGID + "'," +
                                                    "'" + dt["product_gid"].ToString() + "'," +
                                                    "'" + dt["product_price"].ToString() + "'," +
                                                    "'" + dt["discount_percentage"].ToString() + "',";
                        if (dt["discount_amount"].ToString() != "")
                        {
                            msSQL += "'" + dt["discount_amount"].ToString().Replace(",", "") + "',";
                        }
                        else
                        {
                            msSQL += "'0.00', ";
                        }
                        msSQL += "'" + dt["uom_gid"].ToString() + "'," +
                                                     "'" + dt["tax_amount"].ToString().Replace(",", "") + "'," +
                                                     "'" + dt["tax_name"].ToString() + "'," +
                                                     "'" + dt["description"].ToString() + "'," +
                                                     "'" + dt["tax1_gid"].ToString() + "'," +
                                                     "'" + dt["product_name"].ToString() + "'," +
                                                     "'" + employee_gid + "'," +
                                                     "'" + dt["product_code"].ToString() + "'," +
                                                     "'" + dt["customerproduct_code"].ToString() + "'," +
                                                     "'" + dt["uom_name"].ToString() + "'," +
                                                     "'" + dt["productgroup_gid"].ToString() + "'," +
                                                     "'" + dt["productgroup_name"].ToString() + "'," +
                                                     "'" + dt["selling_price"].ToString() + "'," +
                                                     "'" + dt["product_price"].ToString().Replace(",", "") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Invoice Added Successfully";
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Sales Invoice Submit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void Daviewinvoice(string invoice_gid, Mdlinvoicesummary values)
        {
            try
            {

                msSQL = " select a.invoice_gid, a.customer_name, b.customer_code as customer_branch, a.customer_contactperson, " +
                        " a.customer_contactnumber, a.customer_email, a.customer_address, c.branch_name, c.gst_no,a.currency_code, " +
                        " exchange_rate, invoice_remarks, invoice_refno, date_format(a.invoice_date, '%d-%m-%Y') as invoice_date, " +
                        " payment_term, date_format(a.invoice_date, '%d-%m-%Y') as payment_date, d.product_name, d.productgroup_name, " +
                        " d.product_code, concat(d.hsn_code, '/', d.hsn_description) as 'hsn_details',d.hsn_code,d.hsn_description, " +
                        "a.total_amount, d.productuom_name, d.vendor_price,d.qty_invoice,format((d.vendor_price-d.discount_amount),2) as product_price, " +
                        " format(a.additionalcharges_amount,2)as additionalcharges_amount, format(a.discount_amount,2)as discount_amount, format(a.freight_charges,2)as freight_charges, a.buyback_charges, format(a.packing_charges,2)as packing_charges, " +
                        " format(a.insurance_charges,2)as insurance_charges, format(a.roundoff,2) as roundoff, a.invoice_amount, a.termsandconditions,d.discount_percentage, " +
                        " d.tax_name,d.tax_name2,d.tax_name3,format(d.tax_amount,2)as tax_amount,format(d.tax_amount2,2)as tax_amount2 ,format(d.tax_amount3,2)as tax_amount3 ," +
                        " format(((d.vendor_price-d.discount_amount)*d.qty_invoice+d.tax_amount+d.tax_amount2+d.tax_amount3),2)as price, " +
                        " format(a.total_amount,2) as price_total " +
                        " from rbl_trn_tinvoice a " +
                        " left join crm_mst_tcustomer b on a.customer_gid = b.customer_gid left join hrm_mst_tbranch c " +
                        " on a.branch_gid = c.branch_gid left join rbl_trn_tinvoicedtl d on a.invoice_gid = d.invoice_gid " +
                        " where a.invoice_gid='" + invoice_gid + "'  group by invoice_gid ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<viewinvoice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new viewinvoice_list
                        {
                            invoice_gid = dt["invoice_gid"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            customer_branch = dt["customer_branch"].ToString(),
                            customer_contactperson = dt["customer_contactperson"].ToString(),
                            customer_contactnumber = dt["customer_contactnumber"].ToString(),
                            customer_email = dt["customer_email"].ToString(),
                            customer_address = dt["customer_address"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            gst_no = dt["gst_no"].ToString(),
                            currency_code = dt["currency_code"].ToString(),
                            exchange_rate = dt["exchange_rate"].ToString(),
                            invoice_remarks = dt["invoice_remarks"].ToString(),
                            invoice_refno = dt["invoice_refno"].ToString(),
                            invoice_date = dt["invoice_date"].ToString(),
                            payment_term = dt["payment_term"].ToString(),
                            payment_date = dt["payment_date"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            vendor_price = dt["vendor_price"].ToString(),
                            qty_invoice = dt["qty_invoice"].ToString(),
                            hsn_code = dt["hsn_code"].ToString(),
                            hsn_description = dt["hsn_description"].ToString(),
                            hsn_details = dt["hsn_details"].ToString(),
                            total_amount = dt["total_amount"].ToString(),
                            additionalcharges_amount = dt["additionalcharges_amount"].ToString(),
                            discount_amount = dt["discount_amount"].ToString(),
                            freight_charges = dt["freight_charges"].ToString(),
                            buyback_charges = dt["buyback_charges"].ToString(),
                            packing_charges = dt["packing_charges"].ToString(),
                            insurance_charges = dt["insurance_charges"].ToString(),
                            roundoff = dt["roundoff"].ToString(),
                            invoice_amount = dt["invoice_amount"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            discount_percentage = dt["discount_percentage"].ToString(),
                            termsandconditions = dt["termsandconditions"].ToString(),
                            tax_name = dt["tax_name"].ToString(),
                            tax_name2 = dt["tax_name2"].ToString(),
                            tax_name3 = dt["tax_name3"].ToString(),
                            tax_amount = dt["tax_amount"].ToString(),
                            tax_amount2 = dt["tax_amount2"].ToString(),
                            tax_amount3 = dt["tax_amount3"].ToString(),
                            price = dt["price"].ToString(),
                            price_total = dt["price_total"].ToString(),
                        });
                        values.viewinvoice_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Invoice View!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaGetProductdetails(string directorder_gid, Mdlinvoicesummary values)
        {
            try
            {

                msSQL = "select customerproduct_code,product_code,product_name,qty_quoted from smr_trn_tsalesorderdtl where salesorder_gid='" + directorder_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<salesproduct_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new salesproduct_list
                        {
                            product_code = dt["product_code"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            qty_quoted = dt["qty_quoted"].ToString(),
                        });
                        values.salesproduct_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product Details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaGetTermsandConditions(Mdlinvoicesummary values)
        {
            try
            {

                msSQL = " select template_gid, template_name, template_content from adm_mst_ttemplate ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetTermsDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetTermsDropdown
                        {
                            template_gid = dt["template_gid"].ToString(),
                            template_name = dt["template_name"].ToString(),
                            template_content = dt["template_content"].ToString()
                        });
                        values.terms_lists = getModuleList;
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading terms and conditions!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaGetOnChangeTerms(string template_gid, Mdlinvoicesummary values)
        {
            try
            {

                if (template_gid != null)
                {
                    msSQL = " select template_gid, template_name, template_content from adm_mst_ttemplate where template_gid='" + template_gid + "' ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    var getModuleList = new List<GetTermsDropdown>();

                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getModuleList.Add(new GetTermsDropdown
                            {
                                template_gid = dt["template_gid"].ToString(),
                                template_name = dt["template_name"].ToString(),
                                template_content = dt["template_content"].ToString(),
                            });
                            values.terms_lists = getModuleList;
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading change terms!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
    }
}
