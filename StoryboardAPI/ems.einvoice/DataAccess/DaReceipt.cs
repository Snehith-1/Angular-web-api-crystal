﻿using ems.einvoice.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using OfficeOpenXml.Style;
using System.Web.UI.WebControls;


namespace ems.einvoice.DataAccess
{
    public class DaReceipt
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lscustomergid, lsentity_code, lsdesignation_code, lsCode, msGetGid, payment_status, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid, lsproduct_code;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaGetReceiptSummary(MdlReceipt values)
        {
            try
            {

                msSQL = "select currency_code from crm_trn_tcurrencyexchange where default_currency='Y'";
                string currency = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select a.payment_gid,b.invoice_refno,b.customer_gid, b.invoice_gid, a.directorder_gid, format((a.amount_L - a.bank_charge+a.exchange_gain-a.exchange_loss+a.adjust_amount_L ),2) as amount, a.payment_mode,a.currency_code, " +
                    " a.payment_date ,a.approval_status,format(a.total_amount,2)as total_amount, " +
                    " case when a.currency_code = '" + currency +
                    "' then b.customer_name " +
                    " when a.currency_code is null then b.customer_name " +
                    " when a.currency_code is not null and a.currency_code <> '" + currency +
                    "' then concat(b.customer_name,' / ',a.currency_code) end as customer_name, " +
                    " concat(b.customer_contactperson,' / ',b.customer_contactnumber,' / ',b.customer_email) as contact, " +
                    " b.customer_gid,a.payment_type " +
                    " from rbl_trn_tpayment a" +
                    " left join rbl_trn_tinvoice b on b.invoice_gid=a.invoice_gid " +
                    " left join crm_mst_tcustomer c on c.customer_gid=b.customer_gid " +
                    " where 1 = 1 and a.payment_return='0' " +
                    " order by a.payment_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<receiptsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new receiptsummary_list
                        {
                            payment_date = dt["payment_date"].ToString(),
                            invoice_refno = dt["invoice_refno"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            contact = dt["contact"].ToString(),
                            total_amount = dt["total_amount"].ToString(),
                            payment_mode = dt["payment_mode"].ToString(),
                            approval_status = dt["approval_status"].ToString(),
                            payment_gid = dt["payment_gid"].ToString(),
                            invoice_gid = dt["invoice_gid"].ToString(),


                        });
                        values.receiptsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Receipt!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        public void DaGetAddReceiptSummary(MdlReceipt values)
        {
            try
            {

                msSQL = " select a.invoice_gid,a.invoice_refno,b.customer_gid,b.customer_name,a.invoice_status,format(a.invoice_amount,2)as invoice_amount," +
                " format(a.payment_amount,2) as payment_amount,format((a.invoice_amount-a.payment_amount),2) as outstanding,a.invoice_from, " +
                " case when a.customer_contactnumber is null then  concat(c.customercontact_name,'/',c.mobile,'/',c.email) " +
                " when a.customer_contactnumber is not null then concat(a.customer_contactperson,'/',a.customer_contactnumber,'/',c.email) end as contact " +
                " from rbl_trn_tinvoice a" +
                " left join crm_mst_tcustomer b on b.customer_gid=a.customer_gid " +
                " left join crm_mst_tcustomercontact c on c.customer_gid=a.customer_gid " +
                " where a.invoice_amount>a.payment_amount and a.invoice_date<='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                " and a.invoice_flag in('Invoice Approved') and a.invoice_status not in('Payment done') " +
                " group by a.invoice_gid  order by a.invoice_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<receiptaddsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new receiptaddsummary_list
                        {
                            invoice_refno = dt["invoice_refno"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            contact = dt["contact"].ToString(),
                            invoice_from = dt["invoice_from"].ToString(),
                            invoice_status = dt["invoice_status"].ToString(),
                            invoice_amount = dt["invoice_amount"].ToString(),
                            payment_amount = dt["payment_amount"].ToString(),
                            outstanding = dt["outstanding"].ToString(),
                            customer_gid = dt["customer_gid"].ToString(),

                        });
                        values.receiptaddsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {

                values.message = "Exception occured while loading Receipt!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaGetmodeofpayment(MdlReceipt values)
        {
            try
            {

                msSQL = " Select modeofpayment_gid, payment_type from pay_mst_tmodeofpayment  " +
                   " order by payment_type asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getmodeofpaymentlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getmodeofpaymentlist
                        {
                            modeofpayment_gid = dt["modeofpayment_gid"].ToString(),
                            payment_type = dt["payment_type"].ToString(),
                        });
                        values.Getmodeofpaymentlist = getModuleList;
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

        public void DaGetMakeReceiptdata(MdlReceipt values, string customer_gid)
        {
            try
            {

                msSQL = " select a.customer_gid,a.invoice_gid,format(a.invoice_amount,2)as invoice_amount,a.currency_code,c.customer_name,c.customer_address,a.customer_contactnumber,a.customer_email,g.exchange_rate,a.invoice_amount_l,a.invoice_status,a.invoice_refno as invoice_id," +
                " c.customer_name,h.branch_name,format(a.payment_amount,2) as payment_amount,e.directorder_gid, a.invoice_reference as serviceorder_gid,a.currency_code,format(d.grandtotal, 2) as total_amount,  " +
                " case when format(d.salesorder_advance,2) is null then '0.0'" +
                " when format(d.salesorder_advance,2) is not null then format(d.salesorder_advance,2) end as advance_adjust,format(d.salesorder_advance, 2) as advance_amount, " +
                " case when format(d.updated_advancewht,2) is null then '0.0'" +
                " when format(d.updated_advancewht,2) is not null then format(d.updated_advancewht,2) end as updated_advancewht," +
                " format((a.invoice_amount-ifnull((a.payment_amount+updated_advance+updated_advancewht),0.00)),2) as os_amount from rbl_trn_tinvoice a" +
                " left join crm_mst_tcustomer c on c.customer_gid=a.customer_gid " +
                " left join hrm_mst_tbranch h on h.branch_gid = a.branch_gid " +
                " left join smr_trn_tsalesorder d on d.salesorder_gid=a.invoice_reference " +
                " left join smr_trn_torderadvance f on d.salesorder_gid=f.order_gid " +
                " left join smr_trn_tdeliveryorder e on e.directorder_gid=a.invoice_reference " +
                " left join crm_trn_tcurrencyexchange g on a.currency_code=g.currency_code" +
                " where a.customer_gid='" + customer_gid +
                " 'and a.invoice_status='Payment Pending' and a.invoice_flag in('Invoice Approved')" +
                " group by a.invoice_gid order by a.invoice_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<makereceipt_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new makereceipt_list
                        {
                            customer_name = dt["customer_name"].ToString(),
                            customer_address = dt["customer_address"].ToString(),
                            customer_contactnumber = dt["customer_contactnumber"].ToString(),
                            customer_email = dt["customer_email"].ToString(),
                            invoice_id = dt["invoice_id"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            currency_code = dt["currency_code"].ToString(),
                            invoice_amount = dt["invoice_amount"].ToString(),
                            advance_amount = dt["advance_amount"].ToString(),
                            os_amount = dt["os_amount"].ToString(),
                            payment_amount = dt["payment_amount"].ToString(),
                            total_amount = dt["total_amount"].ToString(),
                            invoice_gid = dt["invoice_gid"].ToString(),


                        });

                        values.makereceipt_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Receipt data!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }



        public void DaUpdatedMakeReceipt(string user_gid, updatereceipt_list values)
        {
            try
            {

                msGetGid = objcmnfunctions.GetMasterGID("BPTP");
                msSQL = " insert into rbl_trn_tpayment (" +
                    " payment_gid, " +
                    " payment_date," +
                    " invoice_gid," +
                    " amount," +
                    " total_amount," +
                    " tds_amount," +
                    " adjust_amount," +
                    " branch," +
                    " cheque_date," +
                    " created_by," +
                    " created_date," +
                    " cash_date," +
                    " neft_date," +
                    " currency_code, " +
                    " payment_type," +
                    " amount_L," +
                    " total_amount_L," +
                    " adjust_amount_L," +
                    " tds_amount_L " + ") values (" +
                    "'" + msGetGid + "'," +
                    "'" + values.receipt_paymentdate + "'," +
                    "'" + values.invoice_gid + "'," +
                    "'" + values.receipt_payment_amount.ToString().Replace(",", "") + "'," +
                    "'" + values.receipt_total_amount.ToString().Replace(",", "") + "', " +
                    "'" + values.tds_receivable.ToString().Replace(",", "") + "', " +
                    "'" + values.adjust_amount.ToString().Replace(",", "") + "', " +
                    "'" + values.receipt_branch_name + "'," +
                    "'" + values.cheque_date.ToString("yyyy-MM-dd") + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                    "'" + values.cash_date.ToString("yyyy-MM-dd") + "'," +
                    "'" + values.neft_date.ToString("yyyy-MM-dd") + "'," +
                    "'" + values.currency_code + "'," +
                    "'" + values.payment_type + "'," +
                    "'" + values.receipt_payment_amount.ToString().Replace(",", "") + "'," +
                    "'" + values.receipt_total_amount.ToString().Replace(",", "") + "'," +
                    "'" + values.tds_receivable.ToString().Replace(",", "") + "'," +
                    "'" + values.adjust_amount.ToString().Replace(",", "") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = "update rbl_trn_tinvoice set payment_amount= '" + values.receipt_payment_amount.ToString().Replace(",", "") + "' where invoice_gid='" + values.invoice_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        msSQL = " select invoice_amount, payment_amount, advance_amount " +
                                    " from rbl_trn_tinvoice  where " +
                                   " invoice_gid = '" + values.invoice_gid + "'and" +
                                   " invoice_amount > (payment_amount) ";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                        if (objMySqlDataReader.HasRows == true)
                        {

                            msSQL = " update rbl_trn_tpayment set " + " approval_status = 'Payment  Done'" + " where payment_gid = '" + msGetGid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            msSQL = " update rbl_trn_tpayment set " + " approval_status = 'Payment done Partial'" + " where payment_gid = '" + msGetGid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        if (mnResult != 0)
                        {

                            values.status = true;
                            values.message = "Payment done successfully";

                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error occured while updating";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while updating Receipt!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DadeleteReceiptSummary(string payment_gid, MdlReceipt values)
        {
            try
            {

                msSQL = "  delete from rbl_trn_tpayment where payment_gid='" + payment_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Receipt Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deleting Receipt";
                }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while deleting Receipt!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }

        public void DaGetreceiptdetails(string invoice_gid, MdlReceipt values)
        {
            try
            {


                msSQL = "select invoice_refno,invoice_amount,invoice_date,payment_amount,total_amount from rbl_trn_tinvoice where invoice_gid='" + invoice_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<invoice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new invoice_list
                        {

                            invoice_refno = dt["invoice_refno"].ToString(),
                            invoice_amount = dt["invoice_amount"].ToString(),
                            invoice_date = dt["invoice_date"].ToString(),
                            payment_amount = dt["payment_amount"].ToString(),
                            total_amount = dt["total_amount"].ToString(),


                        });
                        values.invoice_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Receipt details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            


        }
    }
}

