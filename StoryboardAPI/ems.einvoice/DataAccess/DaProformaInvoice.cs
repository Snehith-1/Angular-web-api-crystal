using ems.einvoice.Models;
using ems.utilities.Functions;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Web;
using System;
using System.Net.Mail;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;

using System.Configuration;
using static OfficeOpenXml.ExcelErrorValue;


namespace ems.einvoice.DataAccess
{
    public class DaProformaInvoice
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objMySqlDataReader;
        string msINGetGID, msGetGid, msGet_att_Gid, msenquiryloggid;
        string lspop_server, lspop_mail, lspop_password, lscompany, lscompany_code;
        string lspath1, lspath2, mail_path, mail_filepath, pdf_name = "";
        int mnResult, lspop_port;
        DataTable dt_datatable, mail_datatable;
        MailMessage message = new MailMessage();
        private string att;

        public void DaGetProformaInvoiceSummary(MdlProformaInvoice values)
        {
            try
            {

                msSQL = "select currency_code from crm_trn_tcurrencyexchange where default_currency='Y'";
                string currency = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select distinct a.invoice_gid, a.invoice_refno, a.invoice_status, a.invoice_flag,a.mail_status,a.customer_gid,a.invoice_date,a.invoice_reference,a.additionalcharges_amount,a.discount_amount, " +
                        " a.invoice_flag as overall_status, a.mail_status," +
                        " a.payment_flag,  format(a.initialinvoice_amount,2) as initialinvoice_amount, " +
                        " case when a.invoicepercent_amount <> 0 then format(a.invoicepercent_amount,2) " +
                        " else format(a.invoice_amount,2) " +
                        " end as invoice_amount,a.customer_contactperson,j.so_referencenumber, " +
                        " case when a.currency_code = '" + currency +
                        "'then a.customer_name when a.currency_code is null then a.customer_name " +
                        " when a.currency_code is not null and a.currency_code <> '" + currency +
                        "'then concat(a.customer_name,' / ',h.country) end as customer_name, " +
                        " case when a.customer_contactnumber is null then e.mobile when a.customer_contactnumber is not null then a.customer_contactnumber end as mobile,a.invoice_from " +
                        " from rbl_trn_tproformainvoice a " +
                        " left join rbl_trn_tproformainvoicedtl b on a.invoice_gid = b.invoice_gid " +
                        " left join rbl_trn_tso2proformainvoice f on f.invoice_gid=a.invoice_gid " +
                        " left join smr_trn_tdeliveryorder d on d.directorder_gid = f.directorder_gid " +
                        " left join crm_mst_tcustomer c on a.customer_gid = c.customer_gid " +
                        " left join crm_mst_tcustomercontact e on e.customer_gid=c.customer_gid " +
                        " left join crm_trn_tcurrencyexchange h on a.currency_code = h.currency_code " +
                        " left join smr_trn_tsalesorder j on j.salesorder_gid=f.directorder_gid " + " where 0=0 " +
                        " order by invoice_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<proformainvoicesummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new proformainvoicesummary_list
                        {
                            invoice_refno = dt["invoice_refno"].ToString(),
                            invoice_date = dt["invoice_date"].ToString(),
                            invoice_gid = dt["invoice_gid"].ToString(),
                            so_referencenumber = dt["so_referencenumber"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            customer_contactperson = dt["customer_contactperson"].ToString(),
                            mobile = dt["mobile"].ToString(),
                            invoice_from = dt["invoice_from"].ToString(),
                            mail_status = dt["mail_status"].ToString(),
                            invoice_amount = dt["invoice_amount"].ToString(),
                            overall_status = dt["overall_status"].ToString(),
                        });
                        values.proformainvoicesummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Proformal Invoice!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }
        public void DaGetProformaInvoiceAddSummary(MdlProformaInvoice values)
        {
            try
            {

                msSQL = " select distinct a.salesorder_gid as directorder_gid,a.salesorder_date as directorder_date,a.salesorder_gid as directorder_refno,d.customer_name, " +
                        " a.customer_contact_person,  " +
                        " case when a.customer_email is null then concat(e.customercontact_name,' / ',e.mobile,' / ',e.email) " +
                        " when a.customer_email is not null then concat(a.customer_contact_person,' / ',a.customer_mobile,' / ',a.customer_email) end as contact_details, " +
                        " case when a.grandtotal_l ='0.00' then format(a.Grandtotal,2) else format(a.grandtotal_l,2) end as grandtotal," +
                        " a.invoice_flag as status,a.customer_gid, a.so_type as order_type " +
                        " from smr_trn_tsalesorder a " + " left join crm_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                        " left join crm_mst_tcustomercontact e on d.customer_gid=e.customer_gid " + " where grandtotal <> pinvoice_amount and a.salesorder_status not in ('SO Amended','Approve Pending','Rejected','Cancelled') " +
                        " order by directorder_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<proformainvoiceaddsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new proformainvoiceaddsummary_list
                        {
                            directorder_gid = dt["directorder_gid"].ToString(),
                            directorder_date = dt["directorder_date"].ToString(),
                            directorder_refno = dt["directorder_refno"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            contact_details = dt["contact_details"].ToString(),
                            grandtotal = dt["grandtotal"].ToString(),
                            status = dt["status"].ToString(),
                        });
                        values.proformainvoiceaddsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding Proformal Invoice!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
         

        }
        public ProformaInvoicelist DaGetProformaInvoicedata(string directorder_gid)
        {
            try
            {
                ProformaInvoicelist objProformaInvoicelist = new ProformaInvoicelist();
                {
                    msSQL = " select date_format(a.salesorder_date,'%d-%m-%Y') as salesorder_date,a.salesorder_gid,concat(j.user_code,' ','/',' ',j.user_firstname,' ',j.user_lastname) as user_firstname, " +
                            " date_format(a.start_date,'%d/%m/%Y') as start_date , date_format(a.end_date,'%d/%m/%Y') as end_date,a.customer_gid, " +
                            " b.customer_name,a.customer_address,a.currency_code,c.customercontact_name,a.termsandconditions,a.so_referenceno1,a.so_referencenumber,a.payment_terms,a.freight_terms,d.product_name,d.display_field, " +
                            " d.productgroup_name,a.addon_charge,a.additional_discount,format(a.total_amount,2)as total_amount,a.gst_amount,a.total_price," +
                            " case when a.customer_mobile is null then c.mobile when a.customer_mobile is not null then a.customer_mobile end as mobile, " +
                            " a.customer_contact_person,a.customer_email,format(a.roundoff,2) as roundoff,d.product_gid,a.exchange_rate," +
                            " format(d.product_price,2)as product_price,format(d.margin_percentage,2)as margin_percentage, " +
                            " format(d.margin_amount,2)as margin_amount,d.tax_name,d.tax_name2,d.tax_name3,d.tax1_gid," +
                            " d.tax2_gid,d.tax3_gid,d.tax_percentage,d.tax_percentage2,d.tax_percentage3,d.tax_amount," +
                            " d.tax_amount2,d.tax_amount3,d.qty_quoted,d.product_remarks,format(d.price,2)as final_amount, e.branch_name," +
                            " d.salesorderdtl_gid,format(a.freight_charges,2)as freight_charges," +
                            " format(a.buyback_charges,2)as buyback_charges,format(a.packing_charges,2)as packing_charges," +
                            " format(a.insurance_charges,2)as insurance_charges,format(a.roundoff,2)as roundoff," +
                            " format(a.Grandtotal,2)as Grandtotal from smr_trn_tsalesorder a" +
                            " left join crm_mst_tcustomer b on b.customer_gid=a.customer_gid" +
                            " left join hrm_mst_tbranch e on e.branch_gid=a.branch_gid " +
                            " left join crm_mst_tcustomercontact c on c.customer_gid=a.customer_gid" +
                            " left join smr_trn_tsalesorderdtl d on d.salesorder_Gid=a.salesorder_Gid" +
                            " left join adm_mst_tuser j on j.user_gid  = a.salesperson_gid " +
                            " left join crm_trn_tcurrencyexchange p on a.currency_code  = p.currency_code " +
                            " where a.salesorder_Gid='" + directorder_gid + "'" +
                            " group by a.salesorder_Gid ";
                }

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    objMySqlDataReader.Read();
                    objProformaInvoicelist.salesorder_gid = objMySqlDataReader["salesorder_gid"].ToString();
                    objProformaInvoicelist.salesorder_date = objMySqlDataReader["salesorder_date"].ToString();
                    objProformaInvoicelist.user_firstname = objMySqlDataReader["user_firstname"].ToString();
                    objProformaInvoicelist.start_date = objMySqlDataReader["start_date"].ToString();
                    objProformaInvoicelist.end_date = objMySqlDataReader["end_date"].ToString();
                    objProformaInvoicelist.so_referencenumber = objMySqlDataReader["so_referencenumber"].ToString();
                    objProformaInvoicelist.product_remarks = objMySqlDataReader["product_remarks"].ToString();
                    objProformaInvoicelist.payment_terms = objMySqlDataReader["payment_terms"].ToString();
                    objProformaInvoicelist.freight_terms = objMySqlDataReader["freight_terms"].ToString();
                    objProformaInvoicelist.salesorderdtl_gid = objMySqlDataReader["salesorderdtl_gid"].ToString();
                    objProformaInvoicelist.branch_name = objMySqlDataReader["branch_name"].ToString();
                    objProformaInvoicelist.so_referenceno1 = objMySqlDataReader["so_referenceno1"].ToString();
                    objProformaInvoicelist.customer_name = objMySqlDataReader["customer_name"].ToString();
                    objProformaInvoicelist.customer_contact_person = objMySqlDataReader["customer_contact_person"].ToString();
                    objProformaInvoicelist.mobile = objMySqlDataReader["mobile"].ToString();
                    objProformaInvoicelist.customer_email = objMySqlDataReader["customer_email"].ToString();
                    objProformaInvoicelist.customer_address = objMySqlDataReader["customer_address"].ToString();
                    objProformaInvoicelist.product_gid = objMySqlDataReader["product_gid"].ToString();
                    objProformaInvoicelist.product_price = objMySqlDataReader["product_price"].ToString();
                    objProformaInvoicelist.product_name = objMySqlDataReader["product_name"].ToString();
                    objProformaInvoicelist.productgroup_name = objMySqlDataReader["productgroup_name"].ToString();
                    objProformaInvoicelist.customer_gid = objMySqlDataReader["customer_gid"].ToString();
                    objProformaInvoicelist.display_field = objMySqlDataReader["display_field"].ToString();
                    objProformaInvoicelist.qty_quoted = objMySqlDataReader["qty_quoted"].ToString();
                    objProformaInvoicelist.margin_percentage = objMySqlDataReader["margin_percentage"].ToString();
                    objProformaInvoicelist.margin_amount = objMySqlDataReader["margin_amount"].ToString();
                    objProformaInvoicelist.tax_name = objMySqlDataReader["tax_name"].ToString();
                    objProformaInvoicelist.tax_amount = objMySqlDataReader["tax_amount"].ToString();
                    objProformaInvoicelist.tax_name2 = objMySqlDataReader["tax_name2"].ToString();
                    objProformaInvoicelist.tax_amount2 = objMySqlDataReader["tax_amount2"].ToString();
                    objProformaInvoicelist.tax1_gid = objMySqlDataReader["tax1_gid"].ToString();
                    objProformaInvoicelist.tax2_gid = objMySqlDataReader["tax2_gid"].ToString();
                    objProformaInvoicelist.tax3_gid = objMySqlDataReader["tax3_gid"].ToString();
                    objProformaInvoicelist.tax_name3 = objMySqlDataReader["tax_name3"].ToString();
                    objProformaInvoicelist.tax_percentage = objMySqlDataReader["tax_percentage"].ToString();
                    objProformaInvoicelist.tax_percentage2 = objMySqlDataReader["tax_percentage2"].ToString();
                    objProformaInvoicelist.tax_percentage3 = objMySqlDataReader["tax_percentage3"].ToString();
                    objProformaInvoicelist.total_amount = objMySqlDataReader["total_amount"].ToString();
                    objProformaInvoicelist.gst_amount = objMySqlDataReader["gst_amount"].ToString();
                    objProformaInvoicelist.final_amount = objMySqlDataReader["final_amount"].ToString();
                    objProformaInvoicelist.addon_charge = objMySqlDataReader["addon_charge"].ToString();
                    objProformaInvoicelist.additional_discount = objMySqlDataReader["additional_discount"].ToString();
                    objProformaInvoicelist.freight_charges = objMySqlDataReader["freight_charges"].ToString();
                    objProformaInvoicelist.buyback_charges = objMySqlDataReader["buyback_charges"].ToString();
                    objProformaInvoicelist.packing_charges = objMySqlDataReader["packing_charges"].ToString();
                    objProformaInvoicelist.insurance_charges = objMySqlDataReader["insurance_charges"].ToString();
                    objProformaInvoicelist.roundoff = objMySqlDataReader["roundoff"].ToString();
                    objProformaInvoicelist.total_price = objMySqlDataReader["total_price"].ToString();
                    objProformaInvoicelist.Grandtotal = objMySqlDataReader["Grandtotal"].ToString();
                    objProformaInvoicelist.roundoff = objMySqlDataReader["roundoff"].ToString();
                    objProformaInvoicelist.termsandconditions = objMySqlDataReader["termsandconditions"].ToString();
                    objProformaInvoicelist.customercontact_name = objMySqlDataReader["customercontact_name"].ToString();
                    objProformaInvoicelist.currency_code = objMySqlDataReader["currency_code"].ToString();
                    objProformaInvoicelist.exchange_rate = objMySqlDataReader["exchange_rate"].ToString();
                    objMySqlDataReader.Close();
                }
                return objProformaInvoicelist;
            }
            catch (Exception ex)
            {
                ex.ToString();
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return null;
            }
        }
        public void DaGetEditProformaInvoicedata(MdlProformaInvoice values, string directorder_gid)
        {
            try
            {


                msSQL = " select a.salesorderdtl_gid,a.customerproduct_code,a.salesorder_gid,a.product_gid,f.productgroup_name,a.product_code,a.product_name,a.display_field, a.uom_name as productuom_name," +
                    " format(a.vendor_price,2) as vendor_price ,format((a.vendor_price-a.margin_amount),2) as product_price,a.qty_quoted as qty_invoice," +
                    " format(((a.qty_quoted)*a.product_price),2) as total,concat(a.tax_name,'-',tax_amount,' ',a.tax_name2,'-',a.tax_amount2,' ',a.tax_name3,'-',a.tax_amount3)as tax,format(a.tax_amount,2) as tax_amount ,format(a.tax_amount2,2) as tax_amount2 ," +
                    " format(a.tax_amount3,2) as tax_amount3,format((((a.qty_quoted)*a.product_price)+(a.tax_amount+a.tax_amount2+a.tax_amount3)),2)as" +
                    " final_amount,a.display_field as product_description,k.vendor_companyname," +
                    " g.productuom_gid,(case when (h.percentage is null) then '0.00'" +
                    " when (h.percentage='')then '0.00' else cast(h.percentage as char) end )as tax_percentage1,(case when(i.percentage is null) then '0.00'" +
                    " when (i.percentage='')then '0.00' else cast(i.percentage as char) end)as tax_percentage2,(case when(j.percentage is null) then '0.00'" +
                    " when (j.percentage='') then '0.00' else cast(j.percentage as char) end )as tax_percentage3," +
                    " a.tax1_gid,a.tax2_gid,a.tax3_gid,format(a.margin_percentage,2)as margin_percentage,format(a.margin_amount,2)as margin_amount " +
                    " from smr_trn_tsalesorderdtl a" +
                    " left join smr_trn_tsalesorder d on d.salesorder_gid=a.salesorder_gid" +
                    " left join pmr_mst_tproduct e on e.product_gid=a.product_gid" +
                    " left join pmr_mst_tproductgroup f on f.productgroup_gid=e.productgroup_gid" +
                    " left join pmr_mst_tproductuom g on g.productuom_gid=a.uom_gid " +
                    " left join acp_mst_ttax h on h.tax_name=a.tax_name" +
                    " left join acp_mst_ttax i on i.tax_name=a.tax_name2" +
                    " left join acp_mst_ttax j on j.tax_name=a.tax_name3" +
                    " left join acp_mst_tvendor k on a.vendor_gid=k.vendor_gid " +
                    " where a.salesorder_gid='" + directorder_gid + "'" +
                    " order by a.salesorderdtl_gid  asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<editproformainvoice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new editproformainvoice_list
                        {
                            productgroup_name = dt["productgroup_name"].ToString(),
                            customerproduct_code = dt["customerproduct_code"].ToString(),
                            product_description = dt["product_description"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            qty_invoice = dt["qty_invoice"].ToString(),
                            Vendor_price = dt["Vendor_price"].ToString(),
                            margin_percentage = dt["margin_percentage"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            final_amount = dt["final_amount"].ToString(),
                            tax = dt["tax"].ToString(),
                        });
                        values.editproformainvoice_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while edit Proformal Invoice!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }
        public void DaProformaInvoiceSubmit(string employee_gid, proformainvoicelist values)
        {
            try
            {


                msINGetGID = objcmnfunctions.GetMasterGID("SIPT");
                string ls_referenceno = objcmnfunctions.GetMasterGID("PRIN");

                msSQL = "insert into rbl_trn_tproformainvoice (" +
                        " invoice_gid," +
                        " invoice_refno," +
                        " user_gid," +
                        " invoice_date," +
                        " payment_date," +
                        " freightcharges_amount," +
                        " discount_amount," +
                        " total_amount," +
                        " payment_term," +
                        " created_date," +
                        " customer_name," +
                        " customer_contactperson," +
                        " customer_contactnumber," +
                        " advance_amount," +
                        " invoice_remarks," +
                        " currency_code," +
                        " exchange_rate," +
                        " total_amount_L," +
                        " invoice_amount_L," +
                        " discount_amount_L," +
                        " customer_address," +
                        " customer_email," +
                        " advance_roundoff," +
                        " roundoff," +
                        " freight_charges," +
                        " buyback_charges," +
                        " packing_charges," +
                        " insurance_charges " +
                        " ) values (" +
                        "'" + msINGetGID + "'," +
                        "'" + ls_referenceno + "'," +
                        "'" + values.employee_gid + "'," +
                        "'" + values.proforma_invoice_date.ToString("yyyy-MM-dd  ") + "'," +
                        "'" + values.proforma_invoice_due_date.ToString("yyyy-MM-dd ") + "'," +
                        "'" + values.proforma_freight_charges + "'," +
                        "'" + values.proforma_maximum_addon_discount_amount + "'," +
                        "'" + values.proforma_grandtotal.ToString().Replace(",", "") + "'," +
                        "'" + values.proforma_invoice_payterm + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + values.proforma_customer_name + "'," +
                        "'" + values.proforma_contact_person + "'," +
                        "'" + values.proforma_contact_no + "'," +
                        "'" + values.proforma_advance_amount + "'," +
                        "'" + values.proforma_remarks + "'," +
                        "'" + values.proforma_currency + "'," +
                        "'" + values.proforma_exchange_rate + "'," +
                        "'" + values.proforma_grandtotal.ToString().Replace(",", "") + "'," +
                        "'" + values.proforma_grandtotal.ToString().Replace(",", "") + "'," +
                        "'" + values.proforma_advance_amount + "'," +
                        "'" + values.proforma_address + "'," +
                        "'" + values.proforma_email_address + "'," +
                        "'" + values.proforma_advance_roundoff + "'," +
                        "'" + values.proforma_roundoff + "'," +
                        "'" + values.proforma_freight_charges + "'," +
                        "'" + values.proforma_buy_back_scrap_charges + "'," +
                        "'" + values.proforma_packing_forwarding_charges + "'," +
                        "'" + values.proforma_insurance_charges + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " select a.salesorderdtl_gid,a.customerproduct_code,a.salesorder_gid,a.product_gid,f.productgroup_name,a.product_code,a.product_name,a.display_field,a.uom_gid,a.uom_name as productuom_name," +
                            " format(a.vendor_price,2) as vendor_price ,format((a.vendor_price-a.margin_amount),2) as product_price,a.qty_quoted as qty_invoice," +
                            " format(((a.qty_quoted)*a.product_price),2) as total,a.tax_name,a.tax_name2,a.tax_name3,format(a.tax_amount,2) as tax_amount ,format(a.tax_amount2,2) as tax_amount2 ," +
                            " format(a.tax_amount3,2) as tax_amount3,format((((a.qty_quoted)*a.product_price)+(a.tax_amount+a.tax_amount2+a.tax_amount3)),2)as" +
                            " final_amount,a.display_field as product_description,k.vendor_companyname," +
                            " g.productuom_gid,(case when (h.percentage is null) then '0.00'" +
                            " when (h.percentage='')then '0.00' else cast(h.percentage as char) end )as tax_percentage1,(case when(i.percentage is null) then '0.00'" +
                            " when (i.percentage='')then '0.00' else cast(i.percentage as char) end)as tax_percentage2,(case when(j.percentage is null) then '0.00'" +
                            " when (j.percentage='') then '0.00' else cast(j.percentage as char) end )as tax_percentage3," +
                            " a.tax1_gid,a.tax2_gid,a.tax3_gid,format(a.margin_percentage,2)as margin_percentage,format(a.margin_amount,2)as margin_amount " +
                            " from smr_trn_tsalesorderdtl a" + " left join smr_trn_tsalesorder d on d.salesorder_gid=a.salesorder_gid" +
                            " left join pmr_mst_tproduct e on e.product_gid=a.product_gid" +
                            " left join pmr_mst_tproductgroup f on f.productgroup_gid=e.productgroup_gid" +
                            " left join pmr_mst_tproductuom g on g.productuom_gid=a.uom_gid " +
                            " left join acp_mst_ttax h on h.tax_name=a.tax_name" +
                            " left join acp_mst_ttax i on i.tax_name=a.tax_name2" +
                            " left join acp_mst_ttax j on j.tax_name=a.tax_name3" +
                            " left join acp_mst_tvendor k on a.vendor_gid=k.vendor_gid " +
                            " where a.salesorder_gid='" + values.proforma_salesorder_gid + "'" +
                            " order by a.salesorderdtl_gid  asc ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("SIPC");

                        msSQL = " insert into rbl_trn_tproformainvoicedtl (" +
                                " invoicedtl_gid, " +
                                " invoice_gid, " +
                                " customerproduct_code," +
                                " product_gid, " +
                                " product_code, " +
                                " product_name, " +
                                " productuom_name, " +
                                " uom_gid, " +
                                " product_price, " +
                                " discount_percentage, " +
                                " tax_name, " +
                                " tax_name2, " +
                                " tax_name3, " +
                                " tax_percentage, " +
                                " tax_percentage2, " +
                                " tax_percentage3, " +
                                " tax_amount, " +
                                " tax_amount2, " +
                                " tax_amount3, " +
                                " qty_invoice, " +
                                " product_total, " +
                                " product_price_L, " +
                                " discount_amount_L, " +
                                " tax_amount1_L, " +
                                " tax_amount2_L, " +
                                " tax_amount3_L, " +
                                " product_total_L, " +
                                " display_field)" +
                                " values ( " +
                                "'" + msGetGid + "', " +
                                "'" + msINGetGID + "'," +
                                "'" + dt["customerproduct_code"].ToString() + "'," +
                                "'" + dt["product_gid"].ToString() + "'," +
                                "'" + dt["product_code"].ToString() + "'," +
                                "'" + dt["product_name"].ToString() + "'," +
                                "'" + dt["productuom_name"].ToString() + "'," +
                                "'" + dt["uom_gid"].ToString() + "'," +
                                "'" + dt["final_amount"].ToString().Replace(",", "") + "'," +
                                "'" + dt["margin_percentage"].ToString() + "'," +
                                "'" + dt["tax_name"].ToString() + "'," +
                                "'" + dt["tax_name2"].ToString() + "'," +
                                "'" + dt["tax_name3"].ToString() + "'," +
                                "'" + dt["tax_percentage1"].ToString() + "'," +
                                "'" + dt["tax_percentage2"].ToString() + "'," +
                                "'" + dt["tax_percentage3"].ToString() + "'," +
                                "'" + dt["tax_amount"].ToString().Replace(",", "") + "'," +
                                "'" + dt["tax_amount2"].ToString().Replace(",", "") + "'," +
                                "'" + dt["tax_amount3"].ToString() + "'," +
                                "'" + dt["qty_invoice"].ToString() + "'," +
                                "'" + dt["final_amount"].ToString().Replace(",", "") + "'," +
                                "'" + dt["final_amount"].ToString().Replace(",", "") + "'," +
                                "'" + dt["margin_amount"].ToString() + "'," +
                                "'" + dt["tax_amount"].ToString().Replace(",", "") + "'," +
                                "'" + dt["tax_amount2"].ToString().Replace(",", "") + "'," +
                                "'" + dt["tax_amount3"].ToString() + "'," +
                                "'" + dt["final_amount"].ToString().Replace(",", "") + "'," +
                                "'" + dt["display_field"].ToString() + "')";
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
                values.message = "Exception occured while adding Proformal Invoice!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaGetProformaInvoiceEditdata(MdlProformaInvoice values, string invoice_gid)
        {
            try
            {

                msSQL = " select g.so_referenceno1,a.invoice_gid,a.invoice_refno,date_format(a.invoice_date,'%d-%m-%Y') as invoice_date,a.payment_term,date_format(a.payment_date,'%d-%m-%Y')as payment_date, " +
                    " g.salesorder_gid,g.total_price,g.gst_amount,format(a.total_amount, 2) as total_amount,g.salesorder_gid,a.termsandconditions, " +
                    " concat(j.user_code, ' ', '/', ' ', j.user_firstname, ' ', j.user_lastname) as user_firstname,  " +
                    " format(g.addon_charge, 2) as additionalcharges_amount,format(g.additional_discount, 2) as discount_amount1 ,format(a.invoice_amount, 2) as invoice_amount, " +
                    " a.customer_name,a.customer_contactperson,a.customer_email,a.customer_address,a.invoice_percent,f.branch_name,g.so_referencenumber,g.start_date,g.end_date,g.freight_terms,g.payment_terms," +
                    " case when a.customer_contactnumber is null then d.mobile when a.customer_contactnumber is not null then a.customer_contactnumber end as mobile,g.currency_code,g.exchange_rate,  " +
                    " a.invoice_remarks,format(a.roundoff, 2) as roundoff,  " +
                    " format(a.freight_charges, 2) as freight_charges, " +
                    " format(a.buyback_charges, 2) as buyback_charges,  " +
                    " format(a.packing_charges, 2) as packing_charges, " +
                    " format(a.insurance_charges, 2) as insurance_charges, " +
                    " format(a.advance_roundoff, 2) as advanceroundoff,format(a.invoicepercent_amount, 2) as invoicepercent_amount ," +
                    " h.invoicedtl_gid,h.qty_invoice,format(h.product_price, 2) as product_price1, " +
                    " h.customerproduct_code,h.discount_percentage,format(h.discount_amount, 2) as discount_amount, " +
                    " format(h.tax_amount, 2) as tax_amount,format(h.tax_amount2, 2) as tax_amount2 ,format(h.tax_amount3, 2) as tax_amount3,h.tax_name, " +
                    " h.tax_name2,h.tax_name3,format((h.product_price + h.discount_amount), 2) as vendor_price, " +
                    " format(((h.product_price * h.qty_invoice) + h.tax_amount + h.tax_amount2 + h.tax_amount3), 2) as price,h.display_field,  " +
                    " h.product_gid, h.product_code, h.product_name,i.productgroup_gid,m.productgroup_name, " +
                    " h.productuom_name,h.uom_gid " +
                    " from rbl_trn_tproformainvoice a " +
                    " left join rbl_trn_tso2proformainvoice b on b.invoice_gid = a.invoice_gid " +
                    " left join crm_mst_tcustomer c on c.customer_gid = a.customer_gid " +
                    " left join crm_mst_tcustomercontact d on d.customer_gid = a.customer_gid " +
                    " left join rbl_trn_tinvoicedtl e on e.invoice_gid = a.invoice_gid " +
                    " left join smr_trn_tsalesorder g on g.salesorder_gid = b.directorder_gid " +
                    " left join hrm_mst_tbranch f on f.branch_gid = g.branch_gid " +
                    " left join adm_mst_tuser j on j.user_gid = g.salesperson_gid " +
                    " left join rbl_trn_tproformainvoicedtl h  on a.invoice_gid = h.invoice_gid " +
                    " left join pmr_mst_tproduct i on i.product_gid = h.product_gid " +
                    " left join pmr_mst_tproductuom k on k.productuom_gid = h.uom_gid" +
                    " left join rbl_trn_tinvoice l on l.invoice_gid = h.invoice_gid " +
                    " left join pmr_mst_tproductgroup m on m.productgroup_gid = i.productgroup_gid " +
                    " where a.invoice_gid = '" + invoice_gid + "' group by invoice_gid,product_name";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<ProformaInvoiceEditlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new ProformaInvoiceEditlist
                        {
                            salesorder_gid = dt["salesorder_gid"].ToString(),
                            invoice_gid = dt["invoice_gid"].ToString(),
                            invoice_refno = dt["invoice_refno"].ToString(),
                            invoice_date = dt["invoice_date"].ToString(),
                            payment_term = dt["payment_term"].ToString(),
                            so_referenceno1 = dt["so_referenceno1"].ToString(),
                            total_price = dt["total_price"].ToString(),
                            invoicepercent_amount = dt["invoicepercent_amount"].ToString(),
                            payment_date = dt["payment_date"].ToString(),
                            gst_amount = dt["gst_amount"].ToString(),
                            total_amount = dt["total_amount"].ToString(),
                            termsandconditions = dt["termsandconditions"].ToString(),
                            user_firstname = dt["user_firstname"].ToString(),
                            additionalcharges_amount = dt["additionalcharges_amount"].ToString(),
                            discount_amount1 = dt["discount_amount1"].ToString(),
                            invoice_amount = dt["invoice_amount"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            customer_contactperson = dt["customer_contactperson"].ToString(),
                            customer_email = dt["customer_email"].ToString(),
                            customer_address = dt["customer_address"].ToString(),
                            invoice_percent = dt["invoice_percent"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            so_referencenumber = dt["so_referencenumber"].ToString(),
                            start_date = dt["start_date"].ToString(),
                            end_date = dt["end_date"].ToString(),
                            freight_terms = dt["freight_terms"].ToString(),
                            payment_terms = dt["payment_terms"].ToString(),
                            mobile = dt["mobile"].ToString(),
                            currency_code = dt["currency_code"].ToString(),
                            exchange_rate = dt["exchange_rate"].ToString(),
                            invoice_remarks = dt["invoice_remarks"].ToString(),
                            roundoff = dt["roundoff"].ToString(),
                            freight_charges = dt["freight_charges"].ToString(),
                            buyback_charges = dt["buyback_charges"].ToString(),
                            packing_charges = dt["packing_charges"].ToString(),
                            insurance_charges = dt["insurance_charges"].ToString(),
                            advanceroundoff = dt["advanceroundoff"].ToString(),
                            invoicedtl_gid = dt["invoicedtl_gid"].ToString(),
                            qty_invoice = dt["qty_invoice"].ToString(),
                            product_price1 = dt["product_price1"].ToString(),
                            customerproduct_code = dt["customerproduct_code"].ToString(),
                            discount_percentage = dt["discount_percentage"].ToString(),
                            tax_amount = dt["tax_amount"].ToString(),
                            tax_amount2 = dt["tax_amount2"].ToString(),
                            tax_amount3 = dt["tax_amount3"].ToString(),
                            tax_name = dt["tax_name"].ToString(),
                            tax_name2 = dt["tax_name2"].ToString(),
                            tax_name3 = dt["tax_name3"].ToString(),
                            discount_amount = dt["discount_amount"].ToString(),
                            vendor_price = dt["vendor_price"].ToString(),
                            price = dt["price"].ToString(),
                            display_field = dt["display_field"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            uom_gid = dt["uom_gid"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                        });
                        values.ProformaInvoiceEditlist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while edit Proformal Invoice!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaUpdateProformainvoice(ProformaInvoiceEditlist Values)
        {
            try
            {

                msSQL = " update rbl_trn_tproformainvoicedtl set " +
                    " display_field = '" + Values.display_field + "'" +
                    " where invoicedtl_gid = '" + Values.invoice_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " update rbl_trn_tproformainvoice set " +
                            " termsandconditions='" + Values.edit_proforma_invoice_termsandconditions + "'," +
                            " customer_contactperson='" + Values.edit_proforma_invoice_contact_person + "'," +
                            " customer_contactnumber='" + Values.edit_proforma_invoice_contact_no + "'," +
                            " customer_address='" + Values.edit_proforma_invoice_address + "'," +
                            " invoice_percent  =   '" + Values.invoice_percent + "'," +
                            " invoicepercent_amount =   '" + Values.edit_proforma_invoice_advance_amount + "'," +
                            " roundoff =   '" + Values.edit_proforma_invoice_roundoff + "'," +
                            " customer_email='" + Values.edit_proforma_invoice_email_address + "'" +
                            " where invoice_gid='" + Values.invoice_gid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Values.message = "Exception occured while updating Proformal Invoice!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public ProformaInvoiceAdvancelist DaGetProformaInvoiceAdvancedata(string invoice_gid)
        {
            try
            {
                ProformaInvoiceAdvancelist objProformaInvoiceAdvancelist = new ProformaInvoiceAdvancelist();
                {
                    msSQL = " select a.salesorder_gid, d.invoice_gid, date_format(a.salesorder_date,'%d-%m-%Y') as salesorder_date, d.invoicepercent_amount, " +
                            " format((d.invoicepercent_amount-a.salesorder_advance),2) as outstandingadvance," +
                            " a.so_referencenumber, format(a.addon_charge,2) as addon_charge, format(a.salesorder_advance,2) as salesorder_advance," +
                            " a.so_remarks, a.payment_days, a.delivery_days, a.so_referenceno1, c.customer_address, " +
                            " format(sum(b.price),2) as total_value, format(a.additional_discount,2) as additional_discount," +
                            " c.customer_address2, c.customer_city, c.customer_state, c.customer_pin, a.currency_code, a.exchange_rate, " +
                            " a.customer_gid, a.invoice_flag, a.termsandconditions, format(a.Grandtotal,2) as Grandtotal, " +
                            " a.customer_name, a.customer_mobile, a.customer_email, a.customer_address as customer_address_so, " +
                            " a.customer_contact_person, a.branch_gid, date_format(d.invoice_date,'%d-%m-%Y') as invoice_date, d.invoice_refno from smr_trn_tsalesorder a" +
                            " left join smr_trn_tsalesorderdtl b on a.salesorder_gid=b.salesorder_gid " +
                            " left join crm_mst_tcustomer c on a.customer_gid=c.customer_gid " +
                            " left join rbl_trn_tproformainvoice d on a.salesorder_gid=d.invoice_reference " +
                            " where d.invoice_gid='" + invoice_gid + "' group by d.invoice_gid";
                }

                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    objMySqlDataReader.Read();
                    objProformaInvoiceAdvancelist.salesorder_gid = objMySqlDataReader["salesorder_gid"].ToString();
                    objProformaInvoiceAdvancelist.invoice_gid = objMySqlDataReader["invoice_gid"].ToString();
                    objProformaInvoiceAdvancelist.invoice_refno = objMySqlDataReader["invoice_refno"].ToString();
                    objProformaInvoiceAdvancelist.invoice_date = objMySqlDataReader["invoice_date"].ToString();
                    objProformaInvoiceAdvancelist.so_referencenumber = objMySqlDataReader["so_referencenumber"].ToString();
                    objProformaInvoiceAdvancelist.salesorder_date = objMySqlDataReader["salesorder_date"].ToString();
                    objProformaInvoiceAdvancelist.customer_gid = objMySqlDataReader["customer_gid"].ToString();
                    objProformaInvoiceAdvancelist.customer_name = objMySqlDataReader["customer_name"].ToString();
                    objProformaInvoiceAdvancelist.customer_contact_person = objMySqlDataReader["customer_contact_person"].ToString();
                    objProformaInvoiceAdvancelist.customer_mobile = objMySqlDataReader["customer_mobile"].ToString();
                    objProformaInvoiceAdvancelist.customer_email = objMySqlDataReader["customer_email"].ToString();
                    objProformaInvoiceAdvancelist.customer_address_so = objMySqlDataReader["customer_address_so"].ToString();
                    objProformaInvoiceAdvancelist.so_referenceno1 = objMySqlDataReader["so_referenceno1"].ToString();
                    objProformaInvoiceAdvancelist.so_remarks = objMySqlDataReader["so_remarks"].ToString();
                    objProformaInvoiceAdvancelist.branch_gid = objMySqlDataReader["branch_gid"].ToString();
                    objProformaInvoiceAdvancelist.payment_days = objMySqlDataReader["payment_days"].ToString();
                    objProformaInvoiceAdvancelist.delivery_days = objMySqlDataReader["delivery_days"].ToString();
                    objProformaInvoiceAdvancelist.total_value = objMySqlDataReader["total_value"].ToString();
                    objProformaInvoiceAdvancelist.addon_charge = objMySqlDataReader["addon_charge"].ToString();
                    objProformaInvoiceAdvancelist.additional_discount = objMySqlDataReader["additional_discount"].ToString();
                    objProformaInvoiceAdvancelist.Grandtotal = objMySqlDataReader["Grandtotal"].ToString();
                    objProformaInvoiceAdvancelist.salesorder_advance = objMySqlDataReader["salesorder_advance"].ToString();
                    objProformaInvoiceAdvancelist.outstandingadvance = objMySqlDataReader["outstandingadvance"].ToString();
                    objProformaInvoiceAdvancelist.termsandconditions = objMySqlDataReader["termsandconditions"].ToString();

                    objMySqlDataReader.Close();
                }
                return objProformaInvoiceAdvancelist;
            }
            catch (Exception ex)
            {
                ex.ToString();
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
                return null;
            }
        }
        public void DaGetProformaInvoiceProductdata(string invoice_gid, MdlProformaInvoice values)
        {
            try
            {

                msSQL = " select invoicedtl_gid, invoice_gid, customerproduct_code, product_gid, product_code, product_name, productuom_name, uom_gid, format(product_price,2) as product_price, " +
                    " discount_percentage, tax_name, tax_name2, tax_name3, tax_percentage, tax_percentage2, tax_percentage3, format(tax_amount,2) as tax_amount, format(tax_amount2,2) as tax_amount2, format(tax_amount3,2) as tax_amount3, " +
                    " qty_invoice, format(product_total,2) as product_total, format(product_price_L,2) as product_price_L, format(discount_amount_L,2) as discount_amount_L, format(tax_amount1_L,2) as tax_amount1_L, format(tax_amount2_L,2) as tax_amount2_L, format(tax_amount3_L,2) as tax_amount3_L, format(product_total_L,2) as product_total_L, " +
                    " display_field from rbl_trn_tproformainvoicedtl " +
                    " where invoice_gid ='" + invoice_gid + "' order by invoice_gid asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<MdlProformaInvoiceProductdata>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new MdlProformaInvoiceProductdata
                        {
                            product_name = dt["product_name"].ToString(),
                            uom_name = dt["productuom_name"].ToString(),
                            qty_invoice = dt["qty_invoice"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            discount_amount = dt["discount_amount_L"].ToString(),
                            tax_percentage = dt["tax_percentage"].ToString(),
                            tax_amount = dt["tax_amount"].ToString(),
                            product_total = dt["product_total"].ToString(),

                        });
                        values.MdlProformaInvoiceProductdata = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Proformal Invoice!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
       
        }
        public void DaGetProformaInvoicemodeofpayment(MdlProformaInvoice values)
        {
            try
            {

                msSQL = " Select modeofpayment_gid, payment_type from pay_mst_tmodeofpayment  " +
                    " order by payment_type asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetProformaInvoicemodeofpaymentlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetProformaInvoicemodeofpaymentlist
                    {
                        modeofpayment_gid = dt["modeofpayment_gid"].ToString(),
                        payment_type = dt["payment_type"].ToString(),
                    });
                    values.GetProformaInvoicemodeofpaymentlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding mode of payment!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
         
        }
        public void DaMaillId(string employee_gid, MdlProformaInvoice values)
        {
            try
            {

                msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid = '" + employee_gid + "' union select pop_username from adm_mst_tcompany";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetMailId_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetMailId_list
                        {
                            employee_emailid = dt["employee_emailid"].ToString(),

                        });
                        values.GetMailId_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Mail!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }


        public void DaAdvrptProformaInvoiceSubmit(string user_gid, MdlAdvrptProformaInvoicelist values)
        {
            try
            {


                string msPIADGetGID = objcmnfunctions.GetMasterGID("VSOM");
                double diff_amount = values.proforma_advrpt_net_amount - values.proforma_advrpt_advance;

                msSQL = " insert into smr_trn_torderadvance(" +
                        " orderadvance_gid, " +
                        " reference_gid, " +
                        " order_gid, " +
                        " advance_date, " +
                        " payment_mode, " +
                        " advance_amount, " +
                        " tds_amount, " +
                        " created_by, " +
                        " created_date, " +
                        " received_amount, " +
                        " diff_amount) " +
                        " values(" +
                        " '" + msPIADGetGID + "'," +
                        " '" + values.proforma_advrpt_salesorder_gid + "'," +
                        " '" + values.proforma_advrpt_invoice_gid + "'," +
                        " '" + Convert.ToDateTime(values.proforma_advrpt_invoice_date).ToString("yyyy-MM-dd") + "'," +
                        " '" + values.proforma_advrpt_payment_mode + "'," +
                        " '" + values.proforma_advrpt_advance + "'," +
                        " '" + values.proforma_advrpt_hold_with_tax + "'," +
                        " '" + user_gid + "'," +
                        " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " '" + values.proforma_advrpt_advance + "'," +
                        " '" + diff_amount + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Advance Receipt added Successfully";
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Proformal Invoice Submit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        public void DaGetTemplatelist(MdlProformaInvoice values)
        {
            try
            {


                msSQL = " select a.template_gid, c.template_name, c.template_content from adm_trn_ttemplate2module a " +
                 " left join adm_mst_tmodule b on a.module_gid = b.module_gid " +
                 " left join adm_mst_ttemplate c on a.template_gid = c.template_gid " +
                 " left join adm_mst_ttemplatetype d on c.templatetype_gid = d.templatetype_gid " +
                 " where a.module_gid = 'MKT' and c.templatetype_gid='2' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<templatelist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new templatelist
                        {
                            template_gid = dt["template_gid"].ToString(),
                            template_name = dt["template_name"].ToString(),
                            template_content = dt["template_content"].ToString(),
                        });
                        values.templatelist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Template!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
         
        }
        public void DaGetTemplatet(string template_gid, MdlProformaInvoice values)
        {
            try
            {


                msSQL = " select a.template_gid, c.template_name, c.template_content from adm_trn_ttemplate2module a " +
                 " left join adm_mst_tmodule b on a.module_gid = b.module_gid " +
                 " left join adm_mst_ttemplate c on a.template_gid = c.template_gid " +
                 " left join adm_mst_ttemplatetype d on c.templatetype_gid = d.templatetype_gid " +
                 " where a.module_gid = 'MKT' and c.template_gid='" + template_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<templatelist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new templatelist
                        {
                            template_gid = dt["template_gid"].ToString(),
                            template_name = dt["template_name"].ToString(),
                            template_content = dt["template_content"].ToString(),
                        });
                        values.templatelist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Template!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaPostMail(HttpRequest httpRequest, string user_gid, result objResult)
        {
            {

                msSQL = " select pop_server,pop_port,pop_username,pop_password,company_name,company_code from adm_mst_tcompany where company_gid='1'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    objMySqlDataReader.Read();

                    lspop_server = objMySqlDataReader["pop_server"].ToString();
                    lspop_port = Convert.ToInt32(objMySqlDataReader["pop_port"]);
                    lspop_mail = objMySqlDataReader["pop_username"].ToString();
                    lspop_password = objMySqlDataReader["pop_password"].ToString();
                    lscompany = objMySqlDataReader["company_name"].ToString();
                    lscompany_code = objMySqlDataReader["company_code"].ToString();
                    objMySqlDataReader.Close();
                }

                // attachment get function

                HttpFileCollection httpFileCollection;
                string lsfilepath = string.Empty;
                string lsdocument_gif = string.Empty;
                MemoryStream ms_stream = new MemoryStream();

                //split function

                string employee_emailid = httpRequest.Form[1];
                string sub = httpRequest.Form[2];
                string to = httpRequest.Form[3];
                string body = httpRequest.Form[4];
                string bcc = httpRequest.Form[5];
                string cc = httpRequest.Form[6];

                HttpPostedFile httpPostedFile;

                // save path

                string lsPath = string.Empty;
                lsPath = ConfigurationManager.AppSettings["Doc_upload_file"] + "/erp_documents" + "/" + lscompany_code + "/" + "Mail/Post/ProformaInvoice/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(lsPath)))
                        System.IO.Directory.CreateDirectory(lsPath);
                }
                try
                {
                    if (httpRequest.Files.Count > 0)
                    {
                        string file_path = string.Empty;
                        httpFileCollection = httpRequest.Files;
                        for (int i = 0; i < httpFileCollection.Count; i++)
                        {
                            string document_gid = objcmnfunctions.GetMasterGID("UPLF");
                            httpPostedFile = httpFileCollection[i];
                            string FileExtension = httpPostedFile.FileName;
                            pdf_name = Path.GetFileNameWithoutExtension(httpPostedFile.FileName);
                            string lsfilepath_gid = document_gid;
                            FileExtension = Path.GetExtension(FileExtension).ToLower();
                            string lsfilepaths_gid = lsfilepath_gid + FileExtension;
                            Stream ls_stream;
                            ls_stream = httpPostedFile.InputStream;
                            ls_stream.CopyTo(ms_stream);

                            //lspath = "/erp_documents" + "/" + lscompany_code + "/" + "mail/post/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                            string return_path, attachement_path;
                            // take last 4 digit

                            string last_4_digit = pdf_name + lsfilepath_gid;
                            string get_last_4_digit = objcmnfunctions.ExtractLast4Digits(last_4_digit);

                            lspath1 = "erp_documents" + "/" + lscompany_code + "/" + "Mail/Post/ProformaInvoice/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfilepath_gid + FileExtension;
                            lspath2 = "erp_documents" + "/" + lscompany_code + "/" + "Mail/Post/ProformaInvoice/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + pdf_name + "-" + get_last_4_digit + FileExtension;
                            mail_path = lspath1;
                            attachement_path = lspath2;

                            // upload file
                            return_path = objcmnfunctions.uploadFile(lsPath + pdf_name + "-" + get_last_4_digit, FileExtension);
                            ms_stream.Close();

                            // Get file attachment from path

                            //mail_filepath =  System.IO.Path.GetFileName(document_gid);
                            msGet_att_Gid = objcmnfunctions.GetMasterGID("BEAC");
                            msenquiryloggid = objcmnfunctions.GetMasterGID("BELP");
                            msSQL = " insert into acc_trn_temailattachments (" +
                                     " emailattachment_gid, " +
                                     " email_gid, " +
                                     " attachment_systemname, " +
                                     " attachment_path, " +
                                     " inbuild_attachment, " +
                                     " attachment_type " +
                                     " ) values ( " +
                                     "'" + msGet_att_Gid + "'," +
                                     "'" + msenquiryloggid + "'," +
                                     "'" + pdf_name + "'," +
                                     "'" + attachement_path + "', " +
                                     "'" + mail_path + "', " +
                                     "'" + FileExtension + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                }
                catch (Exception errormessege)
                {

                }

                msSQL = " select attachment_path from acc_trn_temailattachments where email_gid='" + msenquiryloggid + "'";
                mail_datatable = objdbconn.GetDataTable(msSQL);

                //  message of mail

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                message.From = new MailAddress(employee_emailid);
                message.To.Add(new MailAddress(to));
                if (!string.IsNullOrEmpty(cc))
                {
                    string[] lscc = cc.Split(',');
                    if (lscc.Length == 1)
                    {
                        message.CC.Add(new MailAddress(cc));
                    }
                    else
                    {
                        foreach (string mailCC in lscc)
                        {
                            message.CC.Add(new MailAddress(mailCC.Trim()));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(bcc))
                {
                    string[] lsbcc = bcc.Split(',');
                    if (lsbcc.Length == 1)
                    {
                        message.Bcc.Add(new MailAddress(bcc));
                    }
                    else
                    {
                        foreach (string mailBCC in lsbcc)
                        {
                            message.Bcc.Add(new MailAddress(mailBCC.Trim()));
                        }
                    }
                }
                message.Body = body;
                message.Subject = sub;
                message.IsBodyHtml = true; // convert into html
                message.Priority = MailPriority.Normal;

                foreach (DataRow dt in mail_datatable.Rows)
                {
                    if (mail_datatable.Rows.Count > 0)
                    {
                        message.Attachments.Add(new Attachment(HttpContext.Current.Server.MapPath("../../../" + dt["attachment_path"].ToString())));
                    }
                }

                // mail send 

                SmtpClient client = new SmtpClient();
                client.Host = lspop_server;
                client.Port = lspop_port;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                client.Credentials = new NetworkCredential(lspop_mail, lspop_password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                try
                {
                    client.Send(message);
                    objResult.status = true;
                    objResult.message = "Mail Send Successfully !!";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    objResult.status = false;
                    objResult.message = "Mail Not Send !!";
                }
            }
        }
        public void DaGetProductdetails(string invoice_gid, MdlProformaInvoice values)
        {
            try
            {

                msSQL = "select product_code,product_name,display_field,productuom_name,qty_invoice from rbl_trn_tproformainvoicedtl where invoice_gid='" + invoice_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<proformaproduct_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new proformaproduct_list
                        {

                            product_code = dt["product_code"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            qty_invoice = dt["qty_invoice"].ToString(),
                            display_field = dt["display_field"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                        });
                        values.proformaproduct_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
         
        }

        public void DaGetaddproformaProductdetails(string directorder_gid, MdlProformaInvoice values)
        {
            try
            {


                msSQL = "select product_code,product_name,delivery_quantity from smr_trn_tsalesorderdtl where salesorder_gid='" + directorder_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<proformaaddproduct_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new proformaaddproduct_list
                        {

                            product_code = dt["product_code"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            delivery_quantity = dt["delivery_quantity"].ToString(),
                        });
                        values.proformaaddproduct_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding Proformal Invoice details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          

        }
    }
}

