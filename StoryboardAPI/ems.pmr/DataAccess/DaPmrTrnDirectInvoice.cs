﻿using ems.pmr.Models;
using ems.utilities.Functions;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.Services.Description;


namespace ems.pmr.DataAccess
{
    public class DaPmrTrnDirectInvoice
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        int mnResult;
        string msgetGID, msGetGID, msDIGetGID;
        public void DaGetBranchName(MdlPmrTrnDirectInvoice values)
        {
            try
            {
                msSQL = " select branch_name, branch_gid from hrm_mst_tbranch ";

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
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetVendornamedropDown(MdlPmrTrnDirectInvoice values)
        {
            try
            {
                msSQL = " select vendor_gid, vendor_companyname, contactperson_name, vendor_code from acp_mst_tvendor ";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<GetVendornamedropdown>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetVendornamedropdown
                        {
                            vendorgid = dt["vendor_gid"].ToString(),
                            vendorcompanyname = dt["vendor_companyname"].ToString(),
                        });
                        values.GetVendornamedropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetOnChangeVendor(string vendor_gid, MdlPmrTrnDirectInvoice values)
        {
            try
            {
                msSQL = " SELECT a.contactperson_name AS vendorcontact, a.contact_telephonenumber as phone, a.vendor_gid, a.vendor_gid as vendorcontact_gid, " +
                        " concat(c.address1,' ',c.address2) as address FROM acp_mst_tvendor a" +
                        " left join adm_mst_taddress c on a.address_gid=c.address_gid" +
                        " where a.vendor_gid = '" + vendor_gid + "'";


                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<GetOnChangeVendor>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetOnChangeVendor
                        {
                            vendorcontact = dt["vendorcontact"].ToString(),
                            phone = dt["phone"].ToString(),
                            address = dt["address"].ToString(),
                        });
                        values.GetOnChangeVendor = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetcurrencyCodedropdown(MdlPmrTrnDirectInvoice values)
        {
            try
            {
                msSQL = " select currency_code, currencyexchange_gid from crm_trn_tcurrencyexchange ";

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
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetPurchaseTypedropDown(MdlPmrTrnDirectInvoice values)
        {
            try
            {
                msSQL = " Select a.account_gid, a.purchasetype_name from pmr_trn_tpurchasetype a where a.account_gid <> 'null' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<GetPurchaseTypedropDown>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetPurchaseTypedropDown
                        {
                            account_gid = dt["account_gid"].ToString(),
                            purchasetype_name = dt["purchasetype_name"].ToString(),
                        });
                        values.GetPurchaseTypedropDown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGettaxnamedropdown(MdlPmrTrnDirectInvoice values)
        {
            try
            {
                msSQL = " select tax_gid, tax_name, percentage from acp_mst_ttax ";

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
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetExtraAddondropDown(MdlPmrTrnDirectInvoice values)
        {
            try
            {
                msSQL = " select additional_gid, additional_name from pmr_trn_tadditional ";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<GetExtraAddondropDown>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetExtraAddondropDown
                        {
                            additional_gid = dt["additional_gid"].ToString(),
                            additional_name = dt["additional_name"].ToString(),
                        });
                        values.GetExtraAddondropDown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaGetExtraDeductiondropDown(MdlPmrTrnDirectInvoice values)
        {
            try
            {
                msSQL = " select discount_gid, discount_name from pmr_trn_tdiscount ";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<GetExtraDeductiondropDown>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetExtraDeductiondropDown
                        {
                            discount_gid = dt["discount_gid"].ToString(),
                            discount_name = dt["discount_name"].ToString(),
                        });
                        values.GetExtraDeductiondropDown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void Dadirectinvoicesubmit(string employee_gid, directsalesinvoicelist values)
        {
            try
            {
                string ls_referenceno = objcmnfunctions.GetMasterGID("PINV");
                msgetGID = objcmnfunctions.GetMasterGID("SIVP");
                msGetGID = objcmnfunctions.GetMasterGID("DINV");
                msDIGetGID = objcmnfunctions.GetMasterGID("SIVC");
                string lstype1 = "services";



                msSQL = " insert into acp_trn_tinvoice(" +
                         " invoice_gid," +
                         " vendor_gid," +
                         " invoice_refno," +
                         " invoice_reference," +
                         " user_gid," +
                         " invoice_date," +
                         " payment_date," +
                         " additionalcharges_amount," +
                         " discount_amount," +
                         " total_amount," +
                         " invoice_amount," +
                         " payment_term," +
                         " created_date," +
                         " invoice_status, " +
                         " invoice_flag, " +
                         " invoice_from, " +
                         " invoice_remarks, " +
                         " additionalcharges_amount_L," +
                         " discount_amount_L," +
                         " total_amount_L," +
                         " currency_code," +
                         " exchange_rate," +
                         " freightcharges," +
                         " extraadditional_amount," +
                         " extradiscount_amount," +
                         " extraadditional_amount_L," +
                         " extradiscount_amount_L," +
                         " buybackorscrap," +
                         " vendorinvoiceref_no," +
                         " branch_gid," +
                         " vendor_contact_person," +
                         " vendor_address," +
                         " invoice_type," +
                         " round_off" +
                         ") values (" +
                         "'" + msgetGID + "'," +
                         "'" + values.direct_invoice_ven_name + "'," +
                         "'" + values.direct_invoice_refno + "'," +
                         "'" + msGetGID + "'," +
                         "'" + employee_gid + "'," +
                         "'" + values.direct_invoice_date.ToString("yyyy-MM-dd ") + "'," +
                         "'" + values.direct_invoice_due_date.ToString("yyyy-MM-dd ") + "'," +
                         "'" + values.direct_invoice_addon_amount + "'," +
                         "'" + values.direct_invoice_discount_amount + "'," +
                         "'" + values.direct_invoice_grand_total + "'," +
                         "'" + values.direct_invoice_amount + "'," +
                         "'" + values.direct_invoice_payterm + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         "'IV Approved'," +
                         "'Payment Pending'," +
                         "'" + lstype1 + "'," +
                         "'" + values.direct_invoice_remarks + "'," +
                         "'" + values.direct_invoice_addon_amount + "'," +
                         "'" + values.direct_invoice_discount_amount + "'," +
                         "'" + values.direct_invoice_grand_total + "'," +
                         "'" + values.direct_invoice_currencycode + "'," +
                         "'" + values.direct_invoice_exchange_rate + "'," +
                         "'" + values.direct_invoice_freight_charges + "'," +
                         "'" + values.direct_invoice_extra_addon + "'," +
                         "'" + values.direct_invoice_extra_deduction + "'," +
                         "'" + values.direct_invoice_addon_amount + "'," +
                         "'" + values.direct_invoice_extra_deduction + "'," +
                         "'" + values.direct_invoice_buyback_scrap_charges + "'," +
                         "'" + values.direct_invoice_ven_ref_no + "'," +
                         "'" + values.direct_invoice_branchgid + "'," +
                         "'" + values.direct_invoice_ven_contact_person + "'," +
                         "'" + values.direct_invoice_ven_address + "'," +
                         "'" + values.direct_invoice_type + "'," +
                         "'" + values.direct_invoice_round_off + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    string lstax1, lstax2;
                    double lspercentage1 = 0;
                    double lspercentage2 = 0;
                    double tax1_amount = 0;
                    double tax2_amount = 0;
                    double lsinvoice_amount = values.direct_invoice_amount;

                    if (values.direct_invoice_taxname1 == "")
                    {
                        lspercentage1 = 0;
                    }
                    else
                    {
                        msSQL = "select percentage from acp_mst_ttax  where tax_gid='" + values.direct_invoice_taxname1 + "'";

                        string lspercentage = objdbconn.GetExecuteScalar(msSQL);

                        tax1_amount = Math.Round(lsinvoice_amount * (Convert.ToDouble(lspercentage) / 100), 2);
                    }

                    if (values.direct_invoice_taxname2 == "")
                    {
                        lspercentage2 = 0;
                    }
                    else
                    {
                        msSQL = "select percentage from acp_mst_ttax  where tax_gid='" + values.direct_invoice_taxname2 + "'";

                        string lspercentage_2 = objdbconn.GetExecuteScalar(msSQL);

                        tax2_amount = Math.Round(lsinvoice_amount * (Convert.ToDouble(lspercentage_2) / 100), 2);
                    }

                    double Invoice_total = Math.Round((lsinvoice_amount + tax1_amount + tax2_amount), 2);

                    if (values.direct_invoice_taxname1 == "")
                    {
                        lstax1 = "0";
                    }
                    else
                    {
                        msSQL = "select tax_name from acp_mst_ttax  where tax_gid='" + values.direct_invoice_taxname1 + "'";

                        lstax1 = objdbconn.GetExecuteScalar(msSQL);
                    }

                    if (values.direct_invoice_taxname2 == "")
                    {
                        lstax2 = "0";
                    }
                    else
                    {
                        msSQL = "select tax_name from acp_mst_ttax  where tax_gid='" + values.direct_invoice_taxname2 + "'";
                        lstax2 = objdbconn.GetExecuteScalar(msSQL);
                    }

                    msSQL = " insert into acp_trn_tinvoicedtl(" +
                            " invoicedtl_gid," +
                            " invoice_gid," +
                            " product_price," +
                            " product_total," +
                            " tax_name," +
                            " tax_name2," +
                            " tax_percentage," +
                            " tax_percentage2," +
                            " tax_amount," +
                            " tax_amount2," +
                            " tax1_gid, " +
                            " tax2_gid, " +
                            " display_field," +
                            " product_price_L," +
                            " tax_amount1_L," +
                            " tax_amount2_L" +
                            ") values (" +
                            "'" + msDIGetGID + "'," +
                            "'" + msgetGID + "'," +
                            "'" + lsinvoice_amount + "'," +
                            "'" + Invoice_total + "'," +
                            "'" + lstax1 + "'," +
                            "'" + lstax2 + "'," +
                            "'" + lspercentage1 + "'," +
                            "'" + lspercentage2 + "'," +
                            "'" + tax1_amount + "'," +
                            "'" + tax2_amount + "'," +
                            "'" + values.direct_invoice_taxname1 + "'," +
                            "'" + values.direct_invoice_taxname2 + "'," +
                            "'" + values.direct_invoice_description + "'," +
                            "'" + lsinvoice_amount + "'," +
                            "'" + tax1_amount + "'," +
                            "'" + tax2_amount + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Invoice raised Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While raising Invoice";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading invoice!";
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/pbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
    }
}