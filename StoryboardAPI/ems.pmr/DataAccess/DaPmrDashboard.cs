using ems.pmr.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using System.Web.Http.Results;
using static ems.pmr.Models.addgrn_lists;
using System.Web.UI.WebControls;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using System.Data.SqlClient;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.pmr.DataAccess
{
    public class DaPmrDashboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string lsexchange_to_currency, lsexchange_from_currency, lsapi_url, lsapi_key, lsapi_host, msGetGid, lscreated_date, lscurrency_code;
        int mnResult;
        string currencyCode, lscountry, lsdefault_currency, lscountry_gid;

        public void DaGetPurchaseLiabilityReportChart(MdlPmrDashboard values)
        {
            try
            {

                msSQL = "select distinct sum(a.total_amount) as total_amount,sum(j.invoice_amount) as invoice_amount,sum(j.payment_amount) as payment_amount,sum(j.invoice_amount- j.payment_amount)" +
                " as outstanding_amount,cast(MONTHNAME(a.purchaseorder_date)as char) as purchasemonth,DATE_FORMAT(purchaseorder_date, '%b-%Y')  as purchaseorder_date " +
                " from pmr_trn_tpurchaseorder a" +
                " left join acp_trn_tpo2invoice i on i.purchaseorder_gid=a.purchaseorder_gid" +
                " left join acp_trn_tinvoice j on j.invoice_gid=i.invoice_gid" +
                " where a.purchaseorder_flag ='PO Approved' and a.purchaseorder_date < Now() " +
                // "  AND YEAR(a.purchaseorder_date) = 2018 " +
                " AND a.purchaseorder_date >= DATE_SUB(NOW(), INTERVAL 6 MONTH)" +
                " group by DATE_FORMAT(purchaseorder_date, '%b-%Y') ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetPurchaseLiability_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetPurchaseLiability_List
                        {

                            total_amount = dt["total_amount"].ToString(),
                            invoice_amount = dt["invoice_amount"].ToString(),
                            payment_amount = dt["payment_amount"].ToString(),
                            outstanding_amount = dt["outstanding_amount"].ToString(),
                            purchasemonth = dt["purchaseorder_date"].ToString(),

                        });
                        values.GetPurchaseLiability_List = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading the Purchase Liability Report Chart!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }

        // GetPurchaseCount
        public void DaGetPurchaseCount(MdlPmrDashboard values)
        {
            try
            {

                msSQL = " select count(vendor_gid) as total_vendor from acp_mst_tvendor";
                values.total_vendor = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(a.product_gid) as total_product from pmr_mst_tproduct a ";
                // " left join pmr_mst_tproducttype b on a.producttype_gid=b.producttype_gid " +
                //"  where a.producttype_gid=b.producttype_gid group by a.producttype_gid; ";
                values.total_product = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(purchaseorder_gid) as pototalcount from pmr_trn_tpurchaseorder; ";
                values.pototalcount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(invoice_gid) as invctotalcount from acp_trn_tinvoice ";
                values.invctotalcount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(grn_gid) as grntotalcount from pmr_trn_tgrn ";
                values.grntotalcount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(payment_gid) as total_payment from acp_trn_tpayment ";
                values.total_payment = objdbconn.GetExecuteScalar(msSQL);
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting the puchase count!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }


        //GetInvoiceCount

        public void DaGetInvoiceCount(MdlPmrDashboard values)
        {
            try
            {

                msSQL = "select count(invoice_gid) as total_invoice, " +
                    " (select count(invoice_status)as cancel_invoice  from acp_trn_tinvoice where invoice_status='IV Canceled') as cancel_invoice ," +
                    " (select count(invoice_status) as pending_invoice  from acp_trn_tinvoice where invoice_status = 'IV Work In Progress') as pending_invoice, " +
                    " (select count(invoice_status) as completed_invoice  from acp_trn_tinvoice where invoice_status = 'IV Completed') as completed_invoice " +
                    " from acp_trn_tinvoice";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows)
                {
                    objOdbcDataReader.Read();
                    values.total_invoice = objOdbcDataReader["total_invoice"].ToString();
                    values.cancelled_invoice = objOdbcDataReader["cancel_invoice"].ToString();
                    values.pending_invoice = objOdbcDataReader["pending_invoice"].ToString();
                    values.completed_invoice = objOdbcDataReader["completed_invoice"].ToString();
                }
                objOdbcDataReader.Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting the invoice count!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }

        //GetPaymentCount

        public void DaGetPaymentCount(MdlPmrDashboard values)
        {
            try
            {

                msSQL = " select count(payment_gid) as total_payment, " +
                    " (select count(payment_status) as cancelled_payment  from acp_trn_tpayment where payment_status = 'Payment Cancelled' or payment_status = 'PY Canceled') as cancelled_payment , " +
                    " (select count(payment_status) as pending_payment  from acp_trn_tpayment where payment_status = 'PY Approved') as pending_payment, " +
                    " (select count(payment_status) as completed_payment  from acp_trn_tpayment where payment_status = 'Payment Done') as completed_payment " +
                    " from acp_trn_tpayment; ";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows)
                {
                    objOdbcDataReader.Read();
                    values.total_payment = objOdbcDataReader["total_payment"].ToString();
                    values.cancelled_payment = objOdbcDataReader["cancelled_payment"].ToString();
                    values.pending_payment = objOdbcDataReader["pending_payment"].ToString();
                    values.completed_payment = objOdbcDataReader["completed_payment"].ToString();
                }
                objOdbcDataReader.Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting the PaymentCount!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }

        public void DaGetExchangeRateAPISummary(MdlPmrDashboard values)
        {
            try
            {

                msSQL = "select currencyexchangeapihistory_gid, updated_date, currency_code, " +
                    " currency_symbol, exchange_rate, country, default_currency, country_gid, updated_by, created_by, DATE_FORMAT(created_date, '%d-%m-%Y') AS created_date " +
                    "  from crm_trn_tcurrencyexchangeapihistory order by created_date desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetExchangeRateAPI_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetExchangeRateAPI_List
                        {

                            currencyexchangeapihistory_gid = dt["currencyexchangeapihistory_gid"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            updated_date = dt["updated_date"].ToString(),
                            currency_code = dt["currency_code"].ToString(),
                            exchange_rate = dt["exchange_rate"].ToString(),


                        });
                        values.GetExchangeRateAPI_List = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading the Purchase Liability Report Chart!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }
        public void DaSmrSalesExchangeRateUpdate(string user_gid, GetExchangeRateAPICredential_List values)
        {
            try
            {

                msSQL = " SELECT api_url,api_key,api_host FROM crm_smm_exchangerate_service where s_no='1' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    msSQL = " update  crm_smm_exchangerate_service set " +
                 " api_url = '" + values.api_url + "'," +
                 " api_key = '" + values.api_key + "'," +
                 " api_host = '" + values.api_host + "'," +
                 " updated_by = '" + user_gid + "'," +
                 " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where s_no='1'  ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Currency Exchange Rate Updated Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating  Exchange Rate";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updatuin Currency !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }


        }
        public void DaGetExchangeRateAPICredential(MdlPmrDashboard values)
        {
            try
            {

                msSQL = " SELECT api_url,api_key,api_host FROM crm_smm_exchangerate_service limit 1 ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetExchangeRateAPICredential_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetExchangeRateAPICredential_List
                        {

                            api_url = dt["api_url"].ToString(),
                            api_key = dt["api_key"].ToString(),
                            api_host = dt["api_host"].ToString(),


                        });
                        values.GetExchangeRateAPICredential_List = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading the Purchase Liability Report Chart!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }
        public MdlPmrDashboard DaGetExchangeRateAsync(string user_gid)
        {
            MdlPmrDashboard objresult = new MdlPmrDashboard();
            ExchangeRate_List objexchangerate = new ExchangeRate_List();
            Rates rates = new Rates();

            // lsexchange_to_currency = exchange_to_currency;
            lsexchange_to_currency = "USD";
            msSQL = " select currency_code from crm_trn_tcurrencyexchange where default_currency = 'Y' limit 1";
            objOdbcDataReader = objdbconn.GetDataReader(msSQL);
            if (objOdbcDataReader.HasRows)
            {
                lsexchange_from_currency = objOdbcDataReader["currency_code"].ToString();
            }
            msSQL = " SELECT api_url,api_key,api_host FROM crm_smm_exchangerate_service limit 1 ";
            objOdbcDataReader = objdbconn.GetDataReader(msSQL);
            if (objOdbcDataReader.HasRows)
            {
                lsapi_url = objOdbcDataReader["api_url"].ToString();
                lsapi_key = objOdbcDataReader["api_key"].ToString();
                lsapi_host = objOdbcDataReader["api_host"].ToString();

            }
            objOdbcDataReader.Close();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            msSQL = " select created_date from crm_trn_tcurrencyexchangeapihistory where created_date like '%" + DateTime.Now.ToString("yyyy-MM-dd") + "%' ";
            objOdbcDataReader = objdbconn.GetDataReader(msSQL);
            if (objOdbcDataReader.HasRows == true)
            {
                lscreated_date = objOdbcDataReader["created_date"].ToString();
                return objresult;
            }

            else
            {
                var client = new RestClient("https://" + lsapi_url + "" + lsexchange_to_currency + "");
                var request = new RestRequest(Method.GET);
                request.AddHeader("X-RapidAPI-Key", "" + lsapi_key + "");
                request.AddHeader("X-RapidAPI-Host", "" + lsapi_host + "");
                IRestResponse response = client.Execute(request);
                string response_content = response.Content;

                //var client = new RestClient("https://exchangerate-api.p.rapidapi.com/rapid/latest/USD");
                //var request = new RestRequest(Method.GET);
                //request.AddHeader("X-RapidAPI-Key", "14da834b12msh710e57f3bd88c1fp19cc93jsnba653ed40922");
                //request.AddHeader("X-RapidAPI-Host", "exchangerate-api.p.rapidapi.com");
                //IRestResponse response = client.Execute(request);
                //string response_content = response.Content;  

                ExchangeRate_List objMdlExchangeRateMessageResponse = JsonConvert.DeserializeObject<ExchangeRate_List>(response_content);
                float usdRate = objMdlExchangeRateMessageResponse.rates.USD;
                float aedRate = objMdlExchangeRateMessageResponse.rates.AED;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    foreach (var property in typeof(Rates).GetProperties())
                    {
                        string currencyCode = property.Name;
                        string exchangeRateString = property.GetValue(objMdlExchangeRateMessageResponse.rates).ToString();

                        if (float.TryParse(exchangeRateString, out float exchangeRate))
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("CURH");
                            msSQL = " insert into crm_trn_tcurrencyexchangeapihistory(" +
                                " currencyexchangeapihistory_gid," +
                                " currency_code," +
                                " exchange_rate," +
                                " created_by, " +
                                " created_date)" +
                                " values(" +
                                " '" + msGetGid + "'," +
                                " '" + currencyCode + "'," +
                                " '" + exchangeRateString + "',";
                            msSQL += "'" + user_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        // %%%% Exchange rate integration query with currency master - DONT DELETE %%%%

                        //msSQL = "SELECT currency_code,country,country_gid,default_currency FROM crm_trn_tcurrencyexchange WHERE currency_code = '" + currencyCode + "' ";
                        //objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                        //if (objOdbcDataReader.HasRows == true)
                        //{

                        //    lscurrency_code = objOdbcDataReader["currency_code"].ToString();
                        //    lscountry = objOdbcDataReader["country"].ToString();
                        //    lsdefault_currency = objOdbcDataReader["default_currency"].ToString();
                        //    lscountry_gid = objOdbcDataReader["country_gid"].ToString();

                        //    dt_datatable = objdbconn.GetDataTable(msSQL);

                        //    if (dt_datatable.Rows.Count != 0)
                        //    {
                        //        foreach (DataRow dt in dt_datatable.Rows)
                        //        {
                        //            string msGetGid = objcmnfunctions.GetMasterGID("CUR");
                        //            msSQL = " insert into crm_trn_tcurrencyexchange(" +
                        //                    " currencyexchange_gid," +
                        //                    " currency_code," +
                        //                    " exchange_rate," +
                        //                    " country," +
                        //                    " default_currency," +
                        //                    " country_gid," +
                        //                    " created_by, " +
                        //                    " created_date)" +
                        //                    " values(" +
                        //                    " '" + msGetGid + "'," +
                        //                    " '" + lscurrency_code + "'," +
                        //                    " '" + exchangeRateString + "'," +
                        //                    " '" + lscountry + "'," +
                        //                    " '" + lsdefault_currency + "'," +
                        //                    " '" + lscountry_gid + "'," +
                        //                    "'" + user_gid + "'," +
                        //                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //        }
                        //    }
                        //}
                    }

                }
                if (mnResult != 0)
                {
                    objresult.status = true;
                    objresult.message = "Currency Exchange History Saved Successfully";
                }
                else
                {
                    objresult.status = false;
                    objresult.message = "Error While Adding Currency";
                }
            }
            return objresult;

        }
    }
}