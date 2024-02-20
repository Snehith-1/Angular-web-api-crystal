﻿using ems.crm.Models;
using ems.system.Models;
using ems.utilities.Functions;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Http.Results;



using static OfficeOpenXml.ExcelErrorValue;

namespace ems.crm.DataAccess
{
    public class DaShopifyCustomer
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;

        string msSQL = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        string lssource_gid;
        string lssource_name, param1, lsleadbank_name, lscategoryindustry_name, lscountry_name, msGetGidInvDt, lscustomergid, lscustomer, lsproductgid, lsproductuom_gid, lsproductuomgid,lscreated_date, lstotal_amount, lstotal_invoice, msINGetGID, lsqty_invoice, lsaddon_charge, lssalesorder_date, lssalesorderdtl_gid, lssalesorder_gid, lsproduct_gid, lsshopify_lineitemid, lsshopify_orderid, lsproduct_name, lsproduct_price, lsqty_quoted, lsproduct_price_l, lsselling_price, lsprice_l, lscustomer_email, lscurrency_code, lscustomer_mobile, lscustomer_gid,lsproductgroup_gid, lsproductgroup_name, lsemployee_gid, lsleadbank_gid, lsaccess_token, lsshopify_productid, lsshopify_store_name, lsstore_month_year, mssalesorderGID, mssalesorderGID1, lscountry_gid, mscusconGetGID, lscountrygid, mscustomerGetGID, msGETcustomercode,
            lsregion_name, lsbankcontact, msGetGid, msGetGid1, msGetGid2, msGetGid3, msGetGid4,
            msGetGid5, msGetGid6, msGetGid7, msGetGid8, msGetGid9, msGetGid10, msGetGid11, lscurrencyexchange_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5,
            mnResult6, mnResult7, mnResult8, mnResult9, mnResult10, mnResult11,
            mnResult12, mnResult13, mnResult14, mnResult15, mnResult16, mnResult17, mnResult18, mnResult19;
        char lsstatus, lsaddtocustomer;
        ///code  by snehith

        public get DaGetShopifyCustomer()
        {
            get objresult = new get();
            try
            {
                msSQL = " SELECT access_token,shopify_store_name,store_month_year FROM crm_smm_shopify_service limit 1 ";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows)
                {

                    lsaccess_token = objOdbcDataReader ["access_token"].ToString();
                    lsshopify_store_name = objOdbcDataReader ["shopify_store_name"].ToString();
                    lsstore_month_year = objOdbcDataReader ["store_month_year"].ToString();

                }
                 
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var client = new RestClient("https://" + lsshopify_store_name + ".myshopify.com");
                var request = new RestRequest("/admin/api/" + lsstore_month_year + "/customers.json?limit=250", Method.GET);
                request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
                request.AddHeader("Cookie", "_master_udr=eyJfcmFpbHMiOnsibWVzc2FnZSI6IkJBaEpJaWxqWXpsak9UQXhPUzAyWkRZMkxUUXlOR1F0T0RKbVl5MDNaVEZsTnpFM09EY3dOV0lHT2daRlJnPT0iLCJleHAiOiIyMDI1LTEwLTIwVDA4OjI3OjU2LjU4MloiLCJwdXIiOiJjb29raWUuX21hc3Rlcl91ZHIifX0%3D--6f6310c22570c2812426da811c5f9f64d2d35161; _secure_admin_session_id=bbc22793fbba552b04eeebfaeb0de080; _secure_admin_session_id_csrf=bbc22793fbba552b04eeebfaeb0de080; identity-state=BAhbB0kiJWVhODM3YTZhN2M3Njg1MzhlNWQ3MTNhYzg2NmM5MWUwBjoGRUZJIiUwY2M0MWQ1ZjE4ZTQwZTcwYWQ1ZTVkMWUzMDBkMzZlYgY7AEY%3D--69633ab3c25bb20e105bbe14b912f36422abe9b1; identity-state-0cc41d5f18e40e70ad5e5d1e300d36eb=BAh7DEkiDnJldHVybi10bwY6BkVUSSI0aHR0cHM6Ly9lNDQ1NWYtMi5teXNob3BpZnkuY29tL2FkbWluL2F1dGgvbG9naW4GOwBUSSIRcmVkaXJlY3QtdXJpBjsAVEkiQGh0dHBzOi8vZTQ0NTVmLTIubXlzaG9waWZ5LmNvbS9hZG1pbi9hdXRoL2lkZW50aXR5L2NhbGxiYWNrBjsAVEkiEHNlc3Npb24ta2V5BjsAVDoMYWNjb3VudEkiD2NyZWF0ZWQtYXQGOwBUZhcxNjk3NzkxOTE0LjQyODUwMDJJIgpub25jZQY7AFRJIiU1YTQyNzRiZTI5ZmVjODE0MDU4ZTlmNGU3ZGZiNzU4MwY7AEZJIgpzY29wZQY7AFRbEEkiCmVtYWlsBjsAVEkiN2h0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvZGVzdGluYXRpb25zLnJlYWRvbmx5BjsAVEkiC29wZW5pZAY7AFRJIgxwcm9maWxlBjsAVEkiTmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvcGFydG5lcnMuY29sbGFib3JhdG9yLXJlbGF0aW9uc2hpcHMucmVhZG9ubHkGOwBUSSIwaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9iYW5raW5nLm1hbmFnZQY7AFRJIkJodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL21lcmNoYW50LXNldHVwLWRhc2hib2FyZC5ncmFwaHFsBjsAVEkiPGh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvc2hvcGlmeS1jaGF0LmFkbWluLmdyYXBocWwGOwBUSSI3aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9mbG93LndvcmtmbG93cy5tYW5hZ2UGOwBUSSI%2BaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9vcmdhbml6YXRpb24taWRlbnRpdHkubWFuYWdlBjsAVEkiPmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvbWVyY2hhbnQtYmFuay1hY2NvdW50Lm1hbmFnZQY7AFRJIg9jb25maWcta2V5BjsAVEkiDGRlZmF1bHQGOwBU--eb8709d3d8002d429911a5b1c28afca37dd02431; identity-state-ea837a6a7c768538e5d713ac866c91e0=BAh7DEkiDnJldHVybi10bwY6BkVUSSI0aHR0cHM6Ly9lNDQ1NWYtMi5teXNob3BpZnkuY29tL2FkbWluL2F1dGgvbG9naW4GOwBUSSIRcmVkaXJlY3QtdXJpBjsAVEkiQGh0dHBzOi8vZTQ0NTVmLTIubXlzaG9waWZ5LmNvbS9hZG1pbi9hdXRoL2lkZW50aXR5L2NhbGxiYWNrBjsAVEkiEHNlc3Npb24ta2V5BjsAVDoMYWNjb3VudEkiD2NyZWF0ZWQtYXQGOwBUZhYxNjk3NzkwNDc2LjU5MTkxOEkiCm5vbmNlBjsAVEkiJWM5NjYwYTQ2NmZhMTlhZTJlNDQyYmM3NjU0NDMxZWMzBjsARkkiCnNjb3BlBjsAVFsQSSIKZW1haWwGOwBUSSI3aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9kZXN0aW5hdGlvbnMucmVhZG9ubHkGOwBUSSILb3BlbmlkBjsAVEkiDHByb2ZpbGUGOwBUSSJOaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9wYXJ0bmVycy5jb2xsYWJvcmF0b3ItcmVsYXRpb25zaGlwcy5yZWFkb25seQY7AFRJIjBodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL2JhbmtpbmcubWFuYWdlBjsAVEkiQmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvbWVyY2hhbnQtc2V0dXAtZGFzaGJvYXJkLmdyYXBocWwGOwBUSSI8aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9zaG9waWZ5LWNoYXQuYWRtaW4uZ3JhcGhxbAY7AFRJIjdodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL2Zsb3cud29ya2Zsb3dzLm1hbmFnZQY7AFRJIj5odHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL29yZ2FuaXphdGlvbi1pZGVudGl0eS5tYW5hZ2UGOwBUSSI%2BaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9tZXJjaGFudC1iYW5rLWFjY291bnQubWFuYWdlBjsAVEkiD2NvbmZpZy1rZXkGOwBUSSIMZGVmYXVsdAY7AFQ%3D--7f37bdb0df101ca716441e71427765ef41612063");
                IRestResponse response = client.Execute(request);
                string response_content = response.Content;
                shopifycustomerlist objMdlShopifyMessageResponse = new shopifycustomerlist();
                objMdlShopifyMessageResponse = JsonConvert.DeserializeObject<shopifycustomerlist>(response_content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string url = response.Headers[16].Value.ToString();
                    string[] array = url.Split('<', '>');
                    if (array.Length > 1)
                    {
                        Uri myUri = new Uri(array[1]);
                        param1 = HttpUtility.ParseQueryString(myUri.Query).Get("page_info");
                    }
                    else
                    {
                        param1 = null;
                    }
                    //msSQL = "delete from crm_trn_tshopifycustomer";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objresult.status = fnLoadCustomers(objMdlShopifyMessageResponse, param1);
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
            return objresult;
        }

        public bool fnLoadCustomers(shopifycustomerlist objMdlShopifyMessageResponse, string pageToken)
        {
            try
            {


                foreach (var item in objMdlShopifyMessageResponse.customers)
                {

                    msSQL = " select shopify_id  from crm_trn_tshopifycustomer where shopify_id = '" + item.id + "'";
                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader .HasRows != true)
                    {
                        msSQL = " insert into crm_trn_tshopifycustomer(" +

                                " shopify_id," +

                                " email," +

                                " orders_count," +

                                " total_spent," +

                                " first_name," +

                                " last_name," +

                                " email_state," +

                                " default_company," +

                                " default_address1," +

                                " default_address2," +

                                " default_city," +

                                " default_country," +

                                " default_countrycode," +

                                " default_zip," +

                                " default_phone," +

                                " last_order_id)" +

                                " values(" +

                                 "'" + item.id + "'," +
                               "'" + item.email + "'," +
                               "'" + item.orders_count + "'," +
                               "'" + item.total_spent + "',";
                        if (item.first_name == null || item.first_name == "")
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + item.first_name.ToString().Replace("'", " ").Replace("'", " ") + "',";
                        }
                        if (item.last_name == null || item.last_name == "")
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + item.last_name.ToString().Replace("'", " ").Replace("'", " ") + "',";
                        }
                        if (item.email_marketing_consent == null)
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + item.email_marketing_consent.state + "',";
                        }
                        if (item.default_address == null || item.default_address.company == null)
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + item.default_address.company.ToString().Replace("'", " ") + "',";
                        }
                        if (item.default_address == null || item.default_address.address1 == null)
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + item.default_address.address1.ToString().Replace("'", " ").Replace("，", ",").Replace("，", ",").Replace("，", ",").Replace("'", ",").Replace("\'", ",") + "',";
                        }
                        if (item.default_address == null || item.default_address.address2 == null)
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + item.default_address.address2.ToString().Replace("'", " ").Replace("，", ",").Replace("，", ",").Replace("，", ",").Replace("'", ",").Replace("\'", ",") + "',";
                        }
                        if (item.default_address == null)
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + item.default_address.city + "',";
                        }
                        if (item.default_address == null)
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + item.default_address.country + "',";
                        }
                        if (item.default_address == null)
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + item.default_address.country_code + "',";
                        }
                        if (item.default_address == null)
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + item.default_address.zip + "',";
                        }
                        if (item.default_address == null)
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + item.default_address.phone + "',";
                        }
                        msSQL += "'" + item.last_order_id + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 0)
                        {
                            objcmnfunctions.LogForAudit(msSQL);
                        }
                    }
                     
                }
                try
                {
                    msSQL = " SELECT access_token,shopify_store_name,store_month_year FROM crm_smm_shopify_service limit 1 ";
                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader .HasRows)
                    {

                        lsaccess_token = objOdbcDataReader ["access_token"].ToString();
                        lsshopify_store_name = objOdbcDataReader ["shopify_store_name"].ToString();
                        lsstore_month_year = objOdbcDataReader ["store_month_year"].ToString();

                    }
                     
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    var client = new RestClient("https://" + lsshopify_store_name + ".myshopify.com");
                    var request = new RestRequest("/admin/api/" + lsstore_month_year + "/customers.json?limit=250&page_info=" + pageToken, Method.GET);
                    request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
                    request.AddHeader("Cookie", "_master_udr=eyJfcmFpbHMiOnsibWVzc2FnZSI6IkJBaEpJaWxqWXpsak9UQXhPUzAyWkRZMkxUUXlOR1F0T0RKbVl5MDNaVEZsTnpFM09EY3dOV0lHT2daRlJnPT0iLCJleHAiOiIyMDI1LTEwLTIwVDA4OjI3OjU2LjU4MloiLCJwdXIiOiJjb29raWUuX21hc3Rlcl91ZHIifX0%3D--6f6310c22570c2812426da811c5f9f64d2d35161; _secure_admin_session_id=bbc22793fbba552b04eeebfaeb0de080; _secure_admin_session_id_csrf=bbc22793fbba552b04eeebfaeb0de080; identity-state=BAhbB0kiJWVhODM3YTZhN2M3Njg1MzhlNWQ3MTNhYzg2NmM5MWUwBjoGRUZJIiUwY2M0MWQ1ZjE4ZTQwZTcwYWQ1ZTVkMWUzMDBkMzZlYgY7AEY%3D--69633ab3c25bb20e105bbe14b912f36422abe9b1; identity-state-0cc41d5f18e40e70ad5e5d1e300d36eb=BAh7DEkiDnJldHVybi10bwY6BkVUSSI0aHR0cHM6Ly9lNDQ1NWYtMi5teXNob3BpZnkuY29tL2FkbWluL2F1dGgvbG9naW4GOwBUSSIRcmVkaXJlY3QtdXJpBjsAVEkiQGh0dHBzOi8vZTQ0NTVmLTIubXlzaG9waWZ5LmNvbS9hZG1pbi9hdXRoL2lkZW50aXR5L2NhbGxiYWNrBjsAVEkiEHNlc3Npb24ta2V5BjsAVDoMYWNjb3VudEkiD2NyZWF0ZWQtYXQGOwBUZhcxNjk3NzkxOTE0LjQyODUwMDJJIgpub25jZQY7AFRJIiU1YTQyNzRiZTI5ZmVjODE0MDU4ZTlmNGU3ZGZiNzU4MwY7AEZJIgpzY29wZQY7AFRbEEkiCmVtYWlsBjsAVEkiN2h0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvZGVzdGluYXRpb25zLnJlYWRvbmx5BjsAVEkiC29wZW5pZAY7AFRJIgxwcm9maWxlBjsAVEkiTmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvcGFydG5lcnMuY29sbGFib3JhdG9yLXJlbGF0aW9uc2hpcHMucmVhZG9ubHkGOwBUSSIwaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9iYW5raW5nLm1hbmFnZQY7AFRJIkJodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL21lcmNoYW50LXNldHVwLWRhc2hib2FyZC5ncmFwaHFsBjsAVEkiPGh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvc2hvcGlmeS1jaGF0LmFkbWluLmdyYXBocWwGOwBUSSI3aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9mbG93LndvcmtmbG93cy5tYW5hZ2UGOwBUSSI%2BaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9vcmdhbml6YXRpb24taWRlbnRpdHkubWFuYWdlBjsAVEkiPmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvbWVyY2hhbnQtYmFuay1hY2NvdW50Lm1hbmFnZQY7AFRJIg9jb25maWcta2V5BjsAVEkiDGRlZmF1bHQGOwBU--eb8709d3d8002d429911a5b1c28afca37dd02431; identity-state-ea837a6a7c768538e5d713ac866c91e0=BAh7DEkiDnJldHVybi10bwY6BkVUSSI0aHR0cHM6Ly9lNDQ1NWYtMi5teXNob3BpZnkuY29tL2FkbWluL2F1dGgvbG9naW4GOwBUSSIRcmVkaXJlY3QtdXJpBjsAVEkiQGh0dHBzOi8vZTQ0NTVmLTIubXlzaG9waWZ5LmNvbS9hZG1pbi9hdXRoL2lkZW50aXR5L2NhbGxiYWNrBjsAVEkiEHNlc3Npb24ta2V5BjsAVDoMYWNjb3VudEkiD2NyZWF0ZWQtYXQGOwBUZhYxNjk3NzkwNDc2LjU5MTkxOEkiCm5vbmNlBjsAVEkiJWM5NjYwYTQ2NmZhMTlhZTJlNDQyYmM3NjU0NDMxZWMzBjsARkkiCnNjb3BlBjsAVFsQSSIKZW1haWwGOwBUSSI3aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9kZXN0aW5hdGlvbnMucmVhZG9ubHkGOwBUSSILb3BlbmlkBjsAVEkiDHByb2ZpbGUGOwBUSSJOaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9wYXJ0bmVycy5jb2xsYWJvcmF0b3ItcmVsYXRpb25zaGlwcy5yZWFkb25seQY7AFRJIjBodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL2JhbmtpbmcubWFuYWdlBjsAVEkiQmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvbWVyY2hhbnQtc2V0dXAtZGFzaGJvYXJkLmdyYXBocWwGOwBUSSI8aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9zaG9waWZ5LWNoYXQuYWRtaW4uZ3JhcGhxbAY7AFRJIjdodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL2Zsb3cud29ya2Zsb3dzLm1hbmFnZQY7AFRJIj5odHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL29yZ2FuaXphdGlvbi1pZGVudGl0eS5tYW5hZ2UGOwBUSSI%2BaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9tZXJjaGFudC1iYW5rLWFjY291bnQubWFuYWdlBjsAVEkiD2NvbmZpZy1rZXkGOwBUSSIMZGVmYXVsdAY7AFQ%3D--7f37bdb0df101ca716441e71427765ef41612063");
                    IRestResponse response = client.Execute(request);
                    string response_content = response.Content;
                    shopifycustomerlist objMdlShopifyMessageResponse1 = new shopifycustomerlist();
                    objMdlShopifyMessageResponse1 = JsonConvert.DeserializeObject<shopifycustomerlist>(response_content);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string param1;
                        string url = response.Headers[16].Value.ToString();
                        string[] array = url.Split('<', '>');
                        if (url.Contains("rel=\"next\""))
                        {
                            Uri myUri = new Uri(array[3]);
                            param1 = HttpUtility.ParseQueryString(myUri.Query).Get("page_info");
                            fnLoadCustomers(objMdlShopifyMessageResponse1, param1);
                        }
                        else
                        {
                            foreach (var item in objMdlShopifyMessageResponse1.customers)
                            {
                                msSQL = " select shopify_id  from crm_trn_tshopifycustomer where shopify_id = '" + item.id + "'";
                                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                                if (objOdbcDataReader .HasRows != true)
                                {
                                    msSQL = " insert into crm_trn_tshopifycustomer(" +
                               " shopify_id," +
                               " email," +
                               " orders_count," +
                               " total_spent," +
                               " first_name," +
                               " last_name," +
                               " email_state," +
                               " default_company," +
                               " default_address1," +
                               " default_address2," +
                               " default_city," +
                               " default_country," +
                               " default_countrycode," +
                               " default_zip," +
                               " default_phone," +
                               " last_order_id)" +
                               " values(" +

                                 "'" + item.id + "'," +
                               "'" + item.email + "'," +
                               "'" + item.orders_count + "'," +
                               "'" + item.total_spent + "',";
                                    if (item.first_name == null || item.first_name == "")
                                    {
                                        msSQL += "'" + null + "',"; ;
                                    }
                                    else
                                    {
                                        msSQL += "'" + item.first_name.ToString().Replace("'", "").Replace("'", "") + "',";
                                    }
                                    if (item.last_name == null || item.last_name == "")
                                    {
                                        msSQL += "'" + null + "',"; ;
                                    }
                                    else
                                    {
                                        msSQL += "'" + item.last_name.ToString().Replace("'", " ").Replace("'", "") + "',";
                                    }
                                    if (item.email_marketing_consent == null)
                                    {
                                        msSQL += "'" + null + "',"; ;
                                    }
                                    else
                                    {
                                        msSQL += "'" + item.email_marketing_consent.state + "',";
                                    }
                                    if (item.default_address == null || item.default_address.company == null)
                                    {
                                        msSQL += "'" + null + "',"; ;
                                    }
                                    else
                                    {
                                        msSQL += "'" + item.default_address.company.ToString().Replace("'", " ") + "',";
                                    }
                                    if (item.default_address == null || item.default_address.address1 == null)
                                    {
                                        msSQL += "'" + null + "',"; ;
                                    }
                                    else
                                    {
                                        msSQL += "'" + item.default_address.address1.ToString().Replace("'", " ").Replace("，", ",").Replace("，", ",").Replace("，", ",").Replace("'", ",").Replace("\'", ",") + "',";
                                    }
                                    if (item.default_address == null || item.default_address.address2 == null)
                                    {
                                        msSQL += "'" + null + "',"; ;
                                    }
                                    else
                                    {
                                        msSQL += "'" + item.default_address.address2.ToString().Replace("'", " ").Replace("，", ",").Replace("，", ",").Replace("，", ",").Replace("'", ",").Replace("\'", ",") + "',";
                                    }
                                    if (item.default_address == null)
                                    {
                                        msSQL += "'" + null + "',"; ;
                                    }
                                    else
                                    {
                                        msSQL += "'" + item.default_address.city + "',";
                                    }
                                    if (item.default_address == null)
                                    {
                                        msSQL += "'" + null + "',"; ;
                                    }
                                    else
                                    {
                                        msSQL += "'" + item.default_address.country + "',";
                                    }
                                    if (item.default_address == null)
                                    {
                                        msSQL += "'" + null + "',"; ;
                                    }
                                    else
                                    {
                                        msSQL += "'" + item.default_address.country_code + "',";
                                    }
                                    if (item.default_address == null)
                                    {
                                        msSQL += "'" + null + "',"; ;
                                    }
                                    else
                                    {
                                        msSQL += "'" + item.default_address.zip + "',";
                                    }
                                    if (item.default_address == null)
                                    {
                                        msSQL += "'" + null + "',"; ;
                                    }
                                    else
                                    {
                                        msSQL += "'" + item.default_address.phone + "',";
                                    }
                                    msSQL += "'" + item.last_order_id + "')";

                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                        }
                    }
                    objOdbcDataReader .Close();
                }
                catch (Exception ex)
                {
                    objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

                    return false;
                }
            }
            catch (Exception ex)
            {

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
          
            return true;
        }

        public void DaGetShopifyCustomersList(MdlShopifyCustomer values)
        {
            try
            {


                msSQL = " select a.shopify_id,first_name,id, last_name, email, orders_count, last_order_id, total_spent, email_state, default_company, default_address1, default_address2, " +
                        " default_city, default_country, default_countrycode, default_zip, default_phone ,(case when a.shopify_id=b.shopify_id then 'Assigned' when b.shopify_id is null then 'Not Assign' end) as status_flag," +
                        " (case when a.shopify_id = c.customer_gid then 'Order Raised' when b.shopify_id = null then 'Not Raised' when b.customer_gid is null then 'Not Raised' end) as order_status " +
                        " from crm_trn_tshopifycustomer a " +
                        " left join crm_trn_tleadbank b on a.shopify_id=b.shopify_id " +
                        " left join cmr_smm_tshopifysalesorder c on a.shopify_id = c.customer_gid " +
                        " group by a.shopify_id";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<shopifycustomers_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new shopifycustomers_list
                        {
                            shopify_id = dt["shopify_id"].ToString(),
                            first_name = dt["first_name"].ToString(),
                            id = dt["id"].ToString(),
                            last_name = dt["last_name"].ToString(),
                            email = dt["email"].ToString(),
                            orders_count = dt["orders_count"].ToString(),
                            last_order_id = dt["last_order_id"].ToString(),
                            total_spent = dt["total_spent"].ToString(),
                            email_state = dt["email_state"].ToString(),
                            order_status = dt["order_status"].ToString(),
                            default_company = dt["default_company"].ToString(),
                            default_address1 = dt["default_address1"].ToString(),
                            default_address2 = dt["default_address2"].ToString(),
                            default_city = dt["default_city"].ToString(),
                            default_country = dt["default_country"].ToString(),
                            default_countrycode = dt["default_countrycode"].ToString(),
                            default_zip = dt["default_zip"].ToString(),
                            default_phone = dt["default_phone"].ToString(),
                            status_flag = dt["status_flag"].ToString(),

                        });
                        values.shopifycustomers_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Error while Fetching Shopify CustomersList";

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }
        public void DaGetShopifyCustomersAssignedList(MdlShopifyCustomer values)
        {
            try
            {


                msSQL = " select a.shopify_id,first_name,id, last_name, email, orders_count, last_order_id, total_spent, email_state, default_company, default_address1, default_address2, " +
                        " default_city, default_country, default_countrycode, default_zip, default_phone ,(case when a.shopify_id=b.shopify_id then 'Assigned' when b.shopify_id is null then 'Not Assign' end) as status_flag, " +
                        " (case when a.shopify_id = c.customer_gid then 'Order Raised' when b.shopify_id = null then 'Not Raised' when b.customer_gid is null then 'Not Raised' end) as order_status " +
                        " from crm_trn_tshopifycustomer a " +
                         " left join cmr_smm_tshopifysalesorder c on a.shopify_id=c.customer_gid " +
                        " left join crm_trn_tleadbank b on a.shopify_id=b.shopify_id where b.shopify_id is not null  group by a.shopify_id";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<shopifycustomersassigned_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new shopifycustomersassigned_list
                        {
                            shopify_id = dt["shopify_id"].ToString(),
                            first_name = dt["first_name"].ToString(),
                            id = dt["id"].ToString(),
                            last_name = dt["last_name"].ToString(),
                            email = dt["email"].ToString(),
                            orders_count = dt["orders_count"].ToString(),
                            last_order_id = dt["last_order_id"].ToString(),
                            total_spent = dt["total_spent"].ToString(),
                            order_status = dt["order_status"].ToString(),
                            email_state = dt["email_state"].ToString(),
                            default_company = dt["default_company"].ToString(),
                            default_address1 = dt["default_address1"].ToString(),
                            default_address2 = dt["default_address2"].ToString(),
                            default_city = dt["default_city"].ToString(),
                            default_country = dt["default_country"].ToString(),
                            default_countrycode = dt["default_countrycode"].ToString(),
                            default_zip = dt["default_zip"].ToString(),
                            default_phone = dt["default_phone"].ToString(),
                            status_flag = dt["status_flag"].ToString(),

                        });
                        values.shopifycustomersassigned_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Error while Fetching CustomersAssignedList";

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
        }
        public void DaGetShopifyCustomersUnassignedList(MdlShopifyCustomer values)
        {
            try
            {


                msSQL = " select a.shopify_id,first_name,id, last_name, email, orders_count, last_order_id, total_spent, email_state, default_company, default_address1, default_address2, " +
                        " default_city, default_country, default_countrycode, default_zip, default_phone ,(case when a.shopify_id=b.shopify_id then 'Assigned' when b.shopify_id is null then 'Not Assign' end) as status_flag , " +
                        " (case when a.shopify_id = c.customer_gid then 'Order Raised' when b.shopify_id = null then 'Not Raised' when b.customer_gid is null then 'Not Raised' end) as order_status " +
                        " from crm_trn_tshopifycustomer a " +
                        " left join crm_trn_tleadbank b on a.shopify_id=b.shopify_id " +
                        " left join cmr_smm_tshopifysalesorder c on a.shopify_id=c.customer_gid " +
                        "where b.shopify_id is null  group by a.shopify_id";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<shopifycustomersunassigned_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new shopifycustomersunassigned_list
                        {
                            shopify_id = dt["shopify_id"].ToString(),
                            first_name = dt["first_name"].ToString(),
                            id = dt["id"].ToString(),
                            last_name = dt["last_name"].ToString(),
                            email = dt["email"].ToString(),
                            orders_count = dt["orders_count"].ToString(),
                            last_order_id = dt["last_order_id"].ToString(),
                            total_spent = dt["total_spent"].ToString(),
                            email_state = dt["email_state"].ToString(),
                            order_status = dt["order_status"].ToString(),
                            default_company = dt["default_company"].ToString(),
                            default_address1 = dt["default_address1"].ToString(),
                            default_address2 = dt["default_address2"].ToString(),
                            default_city = dt["default_city"].ToString(),
                            default_country = dt["default_country"].ToString(),
                            default_countrycode = dt["default_countrycode"].ToString(),
                            default_zip = dt["default_zip"].ToString(),
                            default_phone = dt["default_phone"].ToString(),
                            status_flag = dt["status_flag"].ToString(),

                        });
                        values.shopifycustomersunassigned_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Error While fetching CustomersUnassignedList";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaGetCustomerTotalCount(MdlShopifyCustomer values)
        {
            try
            {


                msSQL = " select count(shopify_id) as customer_totalcount from crm_trn_tshopifycustomer";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<customertotalcount_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new customertotalcount_list
                        {
                            customer_totalcount = dt["customer_totalcount"].ToString(),


                        });
                        values.customertotalcount_list = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Error While fetching CustomerTotalCount";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }
        public void DaGetCustomerAssignedCount(MdlShopifyCustomer values)
        {
            try
            {

                msSQL = "  select count(a.shopify_id) as customer_assigncount from crm_trn_tleadbank a left join crm_trn_tshopifycustomer b on a.shopify_id=b.shopify_id where b.shopify_id is not null ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<customerassignedcount_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new customerassignedcount_list
                        {
                            customer_assigncount = dt["customer_assigncount"].ToString(),


                        });
                        values.customerassignedcount_list = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Error While fetching CustomerAssignedCount";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
          
        }
        public void DaGetCustomerUnassignedCount(MdlShopifyCustomer values)
        {
            try
            {


                msSQL = "  select count(a.shopify_id) as unassign_count from crm_trn_tshopifycustomer   a left  join crm_trn_tleadbank b on a.shopify_id = b.shopify_id where b.shopify_id is  null ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<customerunassignedcount_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new customerunassignedcount_list
                        {
                            unassign_count = dt["unassign_count"].ToString(),


                        });
                        values.customerunassignedcount_list = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Error While fetching CustomersUnassignedList";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaGetLeadmoved(string user_gid, shopifycustomermovingtolead values)
        {
            try
            {


                for (int i = 0; i < values.shopifycustomers_lists.ToArray().Length; i++)
                {
                    msSQL = "select leadbank_gid from crm_trn_tleadbank where shopify_id='" + values.shopifycustomers_lists[i].shopify_id + "' ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count == 0)
                    {
                        msSQL = " Select source_gid  from crm_mst_tsource where source_name = '" + values.source_name + "' ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lssource_name = objOdbcDataReader ["source_gid"].ToString();
                        }

                        //msSQL = " Select leadbank_name  from crm_trn_tleadbank where  leadbank_gid = '" + values.leadbank_name + "'";
                        //string lsleadbank_name = objdbconn.GetExecuteScalar(msSQL);
                        //msSQL = " Select  categoryindustry_name from crm_mst_tcategoryindustry where categoryindustry_gid = '" + values.categoryindustry_name + "'";
                        //string lscategoryindustry_name = objdbconn.GetExecuteScalar(msSQL);
                        //msSQL = " Select country_gid  from adm_mst_tcountry where country_name = '" + values.shopifycustomers_lists[i].default_country + "'";
                        //string lscountry_gid = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " Select country_gid  from adm_mst_tcountry where country_name = '" + values.shopifycustomers_lists[i].default_country + "' ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lscountry_gid = objOdbcDataReader ["country_gid"].ToString();
                        }
                        msSQL = " Select  employee_gid  from hrm_mst_temployee where  user_gid = '" + user_gid + "'";
                        string employee_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select customer_type from crm_mst_tcustomertype Where customertype_gid='" + values.customer_type + "'";
                        string lscustomer_type = objdbconn.GetExecuteScalar(msSQL);


                        msGetGid = objcmnfunctions.GetMasterGID("BMCC");
                        msGetGid1 = objcmnfunctions.GetMasterGID("BLBP");

                        msSQL = " INSERT INTO crm_trn_tleadbank(" +
                                " leadbank_gid," +
                                " shopify_id," +
                                " source_gid," +
                                " leadbank_id," +
                                " status," +
                                " approval_flag, " +
                                " lead_status," +
                                " leadbank_code," +
                                " leadbank_address1," +
                                " leadbank_address2," +
                                " leadbank_country," +
                                " leadbank_city," +
                                " leadbank_pin," +
                                " customer_type," +
                                " customertype_gid," +
                                " main_branch," +
                                " leadbank_name," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                " '" + msGetGid1 + "'," +
                                " '" + values.shopifycustomers_lists[i].shopify_id + "'," +
                                " '" + lssource_name + "'," +
                                " '" + msGetGid + "'," +
                                " 'y'," +
                                " 'Approved'," +
                                " 'Not Assigned'," +
                                " 'H.Q'," +
                                " '" + values.shopifycustomers_lists[i].default_address1 + "'," +
                                " '" + values.shopifycustomers_lists[i].default_address2 + "'," +
                                " '" + values.shopifycustomers_lists[i].default_country + "'," +
                                " '" + values.shopifycustomers_lists[i].default_city + "'," +
                                " '" + values.shopifycustomers_lists[i].default_zip + "'," +
                                " '" + lscustomer_type + "'," +
                                " '" + values.customer_type + "'," +
                                "'Y',";
                        if (values.shopifycustomers_lists[i].default_company == null || values.shopifycustomers_lists[i].default_company == "")
                        {
                            msSQL += "'" + values.shopifycustomers_lists[i].email + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + values.shopifycustomers_lists[i].default_company + "',";
                        }
                        msSQL += "'" + employee_gid + "'," +
                                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msGetGid2 = objcmnfunctions.GetMasterGID("BLBP");
                        if (msGetGid2 == "E")
                        {
                            values.status = false;
                            values.message = "Create sequence code BLCC for Lead Bank";
                        }
                        msSQL = " INSERT INTO crm_trn_tleadbankcontact" +
                            " (leadbankcontact_gid," +
                            " leadbank_gid," +
                            " leadbankcontact_name," +
                            " email, mobile," +
                            " country_code1," +
                            " created_date," +
                            " created_by," +
                            " leadbankbranch_name, " +
                            " address1, " +
                            " address2, " +
                            " city, " +
                            " pincode, " +
                            " country_gid, " +
                            " main_contact)" +
                            " values( " +
                            " '" + msGetGid2 + "'," +
                            " '" + msGetGid1 + "'," +
                            " '" + values.shopifycustomers_lists[i].first_name + "  " + values.shopifycustomers_lists[i].last_name + "'," +
                            " '" + values.shopifycustomers_lists[i].email + "'," +
                            " '" + values.shopifycustomers_lists[i].default_phone + "'," +
                            " '" + values.shopifycustomers_lists[i].default_countrycode + "'," +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            " '" + employee_gid + "'," +
                            " 'H.Q'," +
                            " '" + values.shopifycustomers_lists[i].default_address1 + "'," +
                            " '" + values.shopifycustomers_lists[i].default_address2 + "'," +
                      " '" + values.shopifycustomers_lists[i].default_city + "'," +
                            " '" + values.shopifycustomers_lists[i].default_zip + "'," +
                            " '" + lscountry_gid + "'," +
                            " 'y'" + ")";
                        mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult1 != 0)
                        {
                            msSQL = "select leadbankcontact_name, SUBSTRING_INDEX(leadbankcontact_name, ' ', 1) AS firstName," +
                               "CASE WHEN LOCATE(' ', leadbankcontact_name) > 0 THEN SUBSTRING_INDEX(leadbankcontact_name, ' ', -1)ELSE ''END AS lastName," +
                               "mobile from crm_trn_tleadbankcontact where leadbank_gid = '" + msGetGid1 + "' and leadbankcontact_gid='" + msGetGid2 + "'";
                            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                            if (objOdbcDataReader .HasRows)
                            {
                                string leadbankcontact_name = objOdbcDataReader ["leadbankcontact_name"].ToString();
                                string mobile = objOdbcDataReader ["mobile"].ToString();
                                string firstName = objOdbcDataReader ["firstName"].ToString();
                                string lastName = objOdbcDataReader ["lastName"].ToString();
                                if (mobile != null && mobile != "")
                                {
                                    Rootobject objRootobject = new Rootobject();
                                    string contactjson = "{\"displayName\":\"" + leadbankcontact_name + "\",\"identifiers\":[{\"key\":\"phonenumber\",\"value\":\"" + mobile + "\"}],\"firstName\":\"" + firstName + "\",\"lastName\":\"" + lastName + "\"}";
                                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                                    var client = new RestClient(ConfigurationManager.AppSettings["messagebirdbaseurl"].ToString());
                                    var request = new RestRequest(ConfigurationManager.AppSettings["messagebirdcontact"].ToString(), Method.POST);
                                    request.AddHeader("authorization", ConfigurationManager.AppSettings["messagebirdaccesskey"].ToString());
                                    request.AddParameter("application/json", contactjson, ParameterType.RequestBody);
                                    IRestResponse response = client.Execute(request);
                                    var responseoutput = response.Content;
                                    objRootobject = JsonConvert.DeserializeObject<Rootobject>(responseoutput);
                                    if (response.StatusCode == HttpStatusCode.Created)
                                    {
                                        msSQL = "insert into crm_smm_whatsapp(leadbank_gid,leadbankcontact_gid,id,wkey,wvalue,displayName,firstName,lastName,created_date,created_by)values(" +
                                                " '" + msGetGid2 + "'," +
                                                " '" + msGetGid1 + "'," +
                                                "'" + objRootobject.id + "'," +
                                                "'phonenumber'," +
                                                "'" + mobile + "'," +
                                                "'" + leadbankcontact_name + "'," +
                                                "'" + firstName + "'," +
                                                "'" + lastName + "'," +
                                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                "'" + user_gid + "')";

                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        if (mnResult == 1)
                                        {
                                            msSQL = "update crm_trn_tleadbank set wh_flag = 'Y', wh_id = '" + objRootobject.id + "' where leadbank_gid = '" + msGetGid1 + "'";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        }

                                    }

                                }

                            }

                            msSQL = " select currencyexchange_gid from crm_trn_tcurrencyexchange where country = '" + values.shopifycustomers_lists[i].default_country + "'  limit 1 ";
                            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                            if (objOdbcDataReader .HasRows)
                            {
                                lscurrencyexchange_gid = objOdbcDataReader ["currencyexchange_gid"].ToString();

                            }
                             
                        }
                        msSQL = " select currencyexchange_gid from crm_trn_tcurrencyexchange where country = '" + values.shopifycustomers_lists[i].default_country + "'  limit 1 ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lscurrencyexchange_gid = objOdbcDataReader ["currencyexchange_gid"].ToString();

                        }
                         

                        mscustomerGetGID = objcmnfunctions.GetMasterGID("BCRM");
                        msGETcustomercode = objcmnfunctions.GetMasterGID("CO");
                        msSQL = " INSERT INTO crm_mst_tcustomer" +
                   " (customer_gid," +
                   " shopify_id," +
                   " customer_type," +
                   " customer_id," +
                   " customer_name," +
                   " customer_code," +
                   " customer_address," +
                   " customer_address2," +
                   " customer_country," +
                   " currency_gid," +
                   " customer_city," +
                   " customer_pin," +
                   " main_branch," +
                   " status, " +
                   " created_by," +
                   " created_date)" +
                   " values( " +
                   "'" + mscustomerGetGID + "'," +
                   "'" + values.shopifycustomers_lists[i].shopify_id + "'," +
                   "'" + values.customer_type + "'," +
                   "'" + msGETcustomercode + "',";
                        if (values.shopifycustomers_lists[i].default_company == null || values.shopifycustomers_lists[i].default_company == "")
                        {
                            msSQL += "'" + values.shopifycustomers_lists[i].email + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + values.shopifycustomers_lists[i].default_company + "',";
                        }
                        msSQL += "'H.Q'," +
                          " '" + values.shopifycustomers_lists[i].default_address1 + "'," +
           " '" + values.shopifycustomers_lists[i].default_address2 + "'," +
                         " '" + values.shopifycustomers_lists[i].default_country + "'," +
                         "'" + lscurrencyexchange_gid + "'," +
                         " '" + values.shopifycustomers_lists[i].default_city + "'," +
                         " '" + values.shopifycustomers_lists[i].default_zip + "'," +
                         " 'Y'," +
                         " 'Active'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = "update crm_trn_tleadbank set " +
                              "customer_gid = '" + mscustomerGetGID + "'" +
                              "where leadbank_gid='" + msGetGid1 + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = " select country_gid from crm_trn_tcurrencyexchange where country = '" + values.shopifycustomers_lists[i].default_country + "'  limit 1 ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lscountrygid = objOdbcDataReader ["country_gid"].ToString();

                        }
                         
                        mscusconGetGID = objcmnfunctions.GetMasterGID("BCCM");
                        msSQL = " INSERT INTO crm_mst_tcustomercontact" +
                       " (customercontact_gid," +
                       " customer_gid," +
                       " customerbranch_name, " +
                       " customercontact_name," +
                       " email," +
                       " created_date," +
                       " created_by," +
                       " address1, " +
                       " address2, " +
                       " country_gid, " +
                       " city, " +
                       " zip_code, " +
                       " country_code," +
                       " phone)" +
                       " values( " +
                       "'" + mscusconGetGID + "'," +
                       "'" + mscustomerGetGID + "'," +
                       "'H.Q', " +
                       "'" + values.shopifycustomers_lists[i].first_name + "  " + values.shopifycustomers_lists[i].last_name + "'," +
                       "'" + values.shopifycustomers_lists[i].email + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                       "'" + employee_gid + "', " +
                       " '" + values.shopifycustomers_lists[i].default_address1 + "'," +
                       " '" + values.shopifycustomers_lists[i].default_address2 + "'," +
                       "'" + lscountrygid + "'," +
                      " '" + values.shopifycustomers_lists[i].default_city + "'," +
                      " '" + values.shopifycustomers_lists[i].default_zip + "'," +
                       "'" + values.shopifycustomers_lists[i].default_countrycode + "'," +
                       "'" + values.shopifycustomers_lists[i].default_phone + "')";
                        mnResult2 = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "select module2employee_gid from adm_mst_tmodule2employee where hierarchy_level ='5' and module_gid = 'MKT' and employee_gid='" + employee_gid + "' ";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count != 0)
                        {
                            msSQL = "select a.campaign_gid,b.campaign_title from crm_trn_tcampaign2employee a " +
                             " left join crm_trn_tcampaign b on a.campaign_gid=b.campaign_gid " +
                             " where a.employee_gid='" + employee_gid + "'";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            string lscampaign_gid = objdbconn.GetExecuteScalar(msSQL);
                            if (dt_datatable.Rows.Count != 0)
                            {
                                msGetGid6 = objcmnfunctions.GetMasterGID("BLCC");
                                if (msGetGid6 == "E")
                                {
                                    values.status = false;
                                    values.message = "Create sequence code BLCC for lead bank";
                                }
                                msSQL = " Insert into crm_trn_tlead2campaign ( " +
                                           " lead2campaign_gid, " +
                                           " leadbank_gid, " +
                                           " campaign_gid, " +
                                           " created_by, " +
                                           " created_date, " +
                                           " lead_status, " +
                                           " leadstage_gid, " +
                                           " assign_to ) " +
                                           " Values ( " +
                                           " '" + msGetGid6 + "'," +
                                           " '" + msGetGid1 + "'," +
                                           " '" + lscampaign_gid + "'," +
                                           " '" + employee_gid + "'," +
                                           " '" + DateTime.Now.ToString("yyyy-MM-dd") +
                                           " 'Open'," +
                                           " '1'," +
                                           " '" + employee_gid + "'," + "')";
                                mnResult9 = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult9 == 1)
                                {
                                    msSQL = " update crm_trn_tleadbank Set " +
                                       " lead_status = 'Assigned' " +
                                       " where leadbank_gid = '" + msGetGid1 + "' ";
                                }
                            }
                        }

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Lead Moved Successfully";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error Occurred While Lead Moving";
                        }


                    } 
                }
                objOdbcDataReader .Close();

            }
            catch (Exception ex)
            {
                values.message = "Error Occurred While Lead Moving";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }
        //By code snehith
        public getorders DaGetShopifyOrder(string user_gid)
        {
            getorders objresult = new getorders();
            try
            {


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                msSQL = " SELECT access_token,shopify_store_name,store_month_year FROM crm_smm_shopify_service limit 1 ";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows)
                {

                    lsaccess_token = objOdbcDataReader ["access_token"].ToString();
                    lsshopify_store_name = objOdbcDataReader ["shopify_store_name"].ToString();
                    lsstore_month_year = objOdbcDataReader ["store_month_year"].ToString();

                }
                 
                var client = new RestClient("https://" + lsshopify_store_name + ".myshopify.com");
                var request = new RestRequest("/admin/api/" + lsstore_month_year + "/orders.json?limit=250", Method.GET);
                request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
                IRestResponse response = client.Execute(request);
                string response_content = response.Content;
                shopifyorder_lists objMdlShopifyMessageResponse = new shopifyorder_lists();
                objMdlShopifyMessageResponse = JsonConvert.DeserializeObject<shopifyorder_lists>(response_content);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    foreach (var item in objMdlShopifyMessageResponse.orders)
                    {

                        msSQL = " select shopify_orderid  from cmr_smm_tshopifysalesorder where shopify_orderid = '" + item.id + "'";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows != true)
                        {


                            msSQL = " select employee_gid  from hrm_mst_temployee where user_gid = '" + user_gid + "'";

                            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                            if (objOdbcDataReader .HasRows)
                            {
                                lsemployee_gid = objOdbcDataReader ["employee_gid"].ToString();

                            }
                             
                            mssalesorderGID = objcmnfunctions.GetMasterGID("VSOP");
                            // Parse the original date string to a DateTime object
                            DateTime originalDate = DateTime.ParseExact(item.created_at.ToString(), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);

                            // Convert the DateTime object to the desired format
                            string formattedDate = originalDate.ToString("yyyy-MM-dd");

                            msSQL = " insert  into cmr_smm_tshopifysalesorder (" +
                                     " salesorder_gid ," +
                                     " shopify_orderid ," +
                                     " salesorder_date," +
                                     " shopifyorder_number," +
                                     " customer_gid," +
                                     " customer_name," +
                                     " customer_contact_person," +
                                     " created_by," +
                                     " Grandtotal, " +
                                     " salesorder_status, " +
                                     " addon_charge, " +
                                     " grandtotal_l, " +
                                     " currency_code, " +
                                     " customer_mobile, " +
                                     " customer_address," +
                                     " shipping_to," +
                                     " customer_email " +
                                      "  )values(" +
                                     " '" + mssalesorderGID + "'," +
                                     " '" + item.id + "'," +
                                     " '" + formattedDate + "'," +
                                     " '" + item.name + "'," +
                                     " '" + item.customer.id + "'," +
                                     " '" + item.customer.first_name.ToString().Replace("'", "").Replace("'", "") + " " + item.customer.last_name.ToString().Replace("'", "").Replace("'", "") + "'," +
                                     " '" + item.customer.first_name.ToString().Replace("'", "").Replace("'", "") + " " + item.customer.last_name.ToString().Replace("'", "").Replace("'", "") + "'," +
                                     " '" + lsemployee_gid + "'," +
                                     " '" + item.total_price + "'," +
                                     " '" + item.financial_status + "'," +
                                     " '" + item.total_shipping_price_set.shop_money.amount + "'," +
                                     " '" + item.total_price + "'," +
                                     " '" + item.currency + "',";
                            if (item.customer.phone == null || item.customer == null)
                            {
                                msSQL += "'" + null + "',"; ;
                            }
                            else
                            {
                                msSQL += "'" + item.customer.phone + "',";
                            }
                            if (item.customer.default_address == null || item.customer == null)
                            {
                                msSQL += "'" + null + "',"; ;
                            }
                            else
                            {
                                msSQL += "'" + item.customer.default_address.address1.ToString().Replace("'", " ").Replace("，", ",").Replace("，", ",").Replace("，", ",").Replace("'", ",").Replace("\'", ",") + " , " + item.customer.default_address.city.ToString().Replace("'", " ").Replace("，", ",").Replace("，", ",").Replace("，", ",").Replace("'", ",").Replace("\'", ",") + ", " + item.customer.default_address.country.ToString().Replace("'", " ").Replace("，", ",").Replace("，", ",").Replace("，", ",").Replace("'", ",").Replace("\'", ",") + "',";
                            }
                            if (item.shipping_address == null || item.shipping_address == null)
                            {
                                msSQL += "'" + null + "',"; ;
                            }
                            else
                            {
                                msSQL += "'" + item.shipping_address.address1.ToString().Replace("'", " ").Replace("，", ",").Replace("，", ",").Replace("，", ",").Replace("'", ",").Replace("\'", ",") + " , " + item.shipping_address.city.ToString().Replace("'", " ").Replace("，", ",").Replace("，", ",").Replace("，", ",").Replace("'", ",").Replace("\'", ",") + ", " + item.shipping_address.country.ToString().Replace("'", " ").Replace("，", ",").Replace("，", ",").Replace("，", ",").Replace("'", ",").Replace("\'", ",") + "',";
                            }
                            msSQL += "'" + item.email + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {

                                foreach (var item1 in item.line_items)
                                {

                                    mssalesorderGID1 = objcmnfunctions.GetMasterGID("VSDC");

                                    msSQL = " insert into cmr_smm_tshopifysalesorderdtl (" +
                             " salesorderdtl_gid ," +
                             " salesorder_gid," +
                             " product_gid ," +
                             " shopify_lineitemid ," +
                             " shopify_orderid ," +
                             " product_name," +
                             " display_field," +
                             " product_price," +
                             " qty_quoted," +
                             " selling_price," +
                             " product_price_l, " +
                             " price_l" +
                             ")values(" +
                             " '" + mssalesorderGID1 + "'," +
                             " '" + mssalesorderGID + "'," +
                             " '" + item1.product_id + "'," +
                             " '" + item1.id + "'," +
                             " '" + item.id + "'," +
                             " '" + item1.name.Replace("'", "\\'").Replace("）", ")").Replace("（", "(") + "'," +
                             " '" + item1.name.Replace("'", "\\'").Replace("）", ")").Replace("（", "(") + "'," +
                             " '" + item1.price + "'," +
                             " '" + item1.quantity + "'," +
                             " '" + item1.price + "'," +
                             " '" + item1.price + "'," +
                             " '" + item1.price + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                }

                            }

                        }
                         



                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
            return objresult;
        }


        public void DaGetShopifyOrderSummary(MdlShopifyCustomer values)
        {
            try
            {


                msSQL = " select    a.customer_address,a.salesorder_gid,a.shopifyorder_number,date_format(a.salesorder_date,'%d-%m-%Y') as salesorder_date,a.shopify_orderid,a.customer_contact_person,a.Grandtotal,a.salesorder_status,(case when b.shopify_orderid=a.shopify_orderid then  sum(b.qty_quoted) else sum(b.qty_quoted) end  ) as item_count,(case when c.shopify_orderid=a.shopify_orderid then 'Assigned' when c.shopify_orderid is null then 'Not Assign' end) as status_flag from cmr_smm_tshopifysalesorder a  left join cmr_smm_tshopifysalesorderdtl  b  on b.shopify_orderid=a.shopify_orderid left join smr_trn_tsalesorder  c  on c.shopify_orderid=a.shopify_orderid  group by  b.shopify_orderid    order by shopifyorder_number desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<shopifyordersummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new shopifyordersummary_list
                        {
                            salesorder_gid = dt["salesorder_gid"].ToString(),
                            shopifyorder_number = dt["shopifyorder_number"].ToString(),
                            salesorder_date = dt["salesorder_date"].ToString(),
                            shopify_orderid = dt["shopify_orderid"].ToString(),
                            customer_contact_person = dt["customer_contact_person"].ToString(),
                            Grandtotal = dt["Grandtotal"].ToString(),
                            customer_address = dt["customer_address"].ToString(),
                            salesorder_status = dt["salesorder_status"].ToString(),
                            item_count = dt["item_count"].ToString(),
                            status_flag = dt["status_flag"].ToString(),


                        });
                        values.shopifyordersummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Error While Fetching Shopify OrderSummary";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }

        }
        public void DaGetShopifyPaymentSummary(MdlShopifyCustomer values)
        {
            try
            {


                msSQL = " select    a.customer_address,a.salesorder_gid,a.shopifyorder_number,date_format(a.salesorder_date,'%d-%m-%Y') as salesorder_date,a.shopify_orderid,a.customer_contact_person,a.Grandtotal,a.salesorder_status,(case when b.shopify_orderid=a.shopify_orderid then  sum(b.qty_quoted) else sum(b.qty_quoted) end  ) as item_count,(case when c.shopify_orderid=a.shopify_orderid then 'Assigned' when c.shopify_orderid is null then 'Not Assign' end) as status_flag from cmr_smm_tshopifysalesorder a  left join cmr_smm_tshopifysalesorderdtl  b  on b.shopify_orderid=a.shopify_orderid  left join rbl_trn_tinvoice  c  on c.shopify_orderid=a.shopify_orderid  where a.salesorder_status='paid'  group by  b.shopify_orderid  order by shopifyorder_number desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<shopifypaymentsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new shopifypaymentsummary_list
                        {
                            salesorder_gid = dt["salesorder_gid"].ToString(),
                            shopifyorder_number = dt["shopifyorder_number"].ToString(),
                            salesorder_date = dt["salesorder_date"].ToString(),
                            shopify_orderid = dt["shopify_orderid"].ToString(),
                            customer_contact_person = dt["customer_contact_person"].ToString(),
                            Grandtotal = dt["Grandtotal"].ToString(),
                            customer_address = dt["customer_address"].ToString(),
                            salesorder_status = dt["salesorder_status"].ToString(),
                            item_count = dt["item_count"].ToString(),
                            status_flag = dt["status_flag"].ToString(),


                        });
                        values.shopifypaymentsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Error While Fetching Shopify PaymentSummary";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
          
        }

        public void DaGetShopifyOrderCountSummary(MdlShopifyCustomer values)
        {
            try
            {


                msSQL = " select ( select  count(shopify_orderid) as order_paidcount from cmr_smm_tshopifysalesorder  where salesorder_status='paid') as order_paidcount, " +
                                    " (select  count(shopify_orderid) as order_penidngcount from cmr_smm_tshopifysalesorder  where salesorder_status='pending') as order_penidngcount," +
                                " (select  count(shopify_orderid) as order_refundedcount from cmr_smm_tshopifysalesorder  where salesorder_status='refunded') as order_refundedcount, " +
                                  " (select  count(shopify_productid) as product_count from crm_smm_tshopifyproduct  ) as product_count, " +
                                "  (select  count(shopify_orderid) as total_order from cmr_smm_tshopifysalesorder  ) as total_order, " +
                                " (select  count(shopify_orderid) as order_partiallycount from cmr_smm_tshopifysalesorder  where salesorder_status='partially_refunded') as order_partiallycount";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<shopifyordercountsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new shopifyordercountsummary_list
                        {
                            order_paidcount = dt["order_paidcount"].ToString(),
                            order_penidngcount = dt["order_penidngcount"].ToString(),
                            order_refundedcount = dt["order_refundedcount"].ToString(),
                            order_partiallycount = dt["order_partiallycount"].ToString(),
                            product_count = dt["product_count"].ToString(),
                            total_order = dt["total_order"].ToString(),


                        });
                        values.shopifyordercountsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Error While Fetching Shopify OrderCountSummary";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
          
        }


        public void DaSendorder(string user_gid, shopifyordermovingtoorder values)
        {
            try {

                for (int i = 0; i < values.shopifyorderlists.ToArray().Length; i++)
                {
                    msSQL = "select shopify_orderid from smr_trn_tsalesorder where shopify_orderid='" + values.shopifyorderlists[i].shopify_orderid + "' ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count == 0)
                    {

                        msSQL = " select employee_gid  from hrm_mst_temployee where user_gid = '" + user_gid + "'";

                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsemployee_gid = objOdbcDataReader ["employee_gid"].ToString();

                        }
                         
                        msSQL = " select customer_gid,addon_charge,currency_code,customer_mobile,customer_email,date_format(salesorder_date,'%Y-%m-%d') as salesorder_date  from cmr_smm_tshopifysalesorder where shopify_orderid='" + values.shopifyorderlists[i].shopify_orderid + "' ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lssalesorder_date = objOdbcDataReader ["salesorder_date"].ToString();
                            lscustomer_gid = objOdbcDataReader ["customer_gid"].ToString();
                            lsaddon_charge = objOdbcDataReader ["addon_charge"].ToString();
                            lscurrency_code = objOdbcDataReader ["currency_code"].ToString();
                            lscustomer_mobile = objOdbcDataReader ["customer_mobile"].ToString();
                            lscustomer_email = objOdbcDataReader ["customer_email"].ToString();
                        }
                         
                        msSQL = " select customer_gid from crm_mst_tcustomer where shopify_id='" + lscustomer_gid + "' ";

                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lscustomergid = objOdbcDataReader ["customer_gid"].ToString();


                        }
                        if (lscustomergid != null && lscustomergid != "")
                        {
                            lscustomer = lscustomergid;
                        }
                        else
                        {
                            lscustomer = null;
                        }


                         
                        msSQL = " insert  into smr_trn_tsalesorder (" +
                                      " salesorder_gid ," +
                                      " shopify_orderid ," +
                                      " salesorder_date," +
                                      " customer_gid," +
                                      " shopifyorder_number," +
                                      " shopifycustomer_id," +
                                      " customer_name," +
                                      " customer_contact_person," +
                                      " created_by," +
                                      " Grandtotal, " +
                                      " salesorder_status, " +
                                      " addon_charge, " +
                                      " grandtotal_l, " +
                                      " currency_code, " +
                                      " customer_mobile, " +
                                      " customer_address," +
                                      " shipping_to," +
                                      " customer_email " +
                                       "  )values(" +
                                      " '" + values.shopifyorderlists[i].salesorder_gid + "'," +
                                      " '" + values.shopifyorderlists[i].shopify_orderid + "'," +
                                      " '" + lssalesorder_date + "'," +
                                       " '" + lscustomer + "'," +
                                      " '" + values.shopifyorderlists[i].shopifyorder_number + "'," +
                                      " '" + lscustomer_gid + "'," +
                                      " '" + values.shopifyorderlists[i].customer_contact_person + "'," +
                                      " '" + values.shopifyorderlists[i].customer_contact_person + "'," +
                                      " '" + lsemployee_gid + "'," +
                                      " '" + values.shopifyorderlists[i].Grandtotal + lsaddon_charge + "'," +
                                      " '" + values.shopifyorderlists[i].salesorder_status + "'," +
                                      " '" + lsaddon_charge + "'," +
                                      " '" + values.shopifyorderlists[i].Grandtotal + lsaddon_charge + "'," +
                                      " '" + lscurrency_code + "',";
                        if (lscustomer_mobile == null || lscustomer_mobile == "")
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + lscustomer_mobile + "',";
                        }
                        if (values.shopifyorderlists[i].customer_address == null || (values.shopifyorderlists[i].customer_address == null))
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + (values.shopifyorderlists[i].customer_address) + "',";
                        }
                        if (values.shopifyorderlists[i].customer_address == null || (values.shopifyorderlists[i].customer_address == null))
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + (values.shopifyorderlists[i].customer_address) + "',";
                        }
                        msSQL += "'" + lscustomer_email + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = " select salesorderdtl_gid,salesorder_gid,product_gid,shopify_lineitemid,shopify_orderid,product_name,product_price,qty_quoted,product_price_l,selling_price,price_l  from cmr_smm_tshopifysalesorderdtl where shopify_orderid='" + values.shopifyorderlists[i].shopify_orderid + "'";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow dt in dt_datatable.Rows)
                                {
                                    msSQL = " select product_gid ,productuom_gid  from pmr_mst_tproduct where shopify_productid='" + dt.ItemArray[2] + "' ";

                                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                                    if (objOdbcDataReader .HasRows)
                                    {
                                        lsproduct_gid = objOdbcDataReader ["product_gid"].ToString();
                                        lsproductuom_gid = objOdbcDataReader ["productuom_gid"].ToString();

                                    }
                                    if (lsproduct_gid != null && lsproduct_gid != "")
                                    {
                                        lsproductgid = lsproduct_gid;
                                    }
                                    else
                                    {
                                        lsproductgid = null;
                                    }
                                    if (lsproductuom_gid != null && lsproductuom_gid != "")
                                    {
                                        lsproductuomgid = lsproductuom_gid;
                                    }
                                    else
                                    {
                                        lsproductuomgid = null;
                                    }

                                     
                                    msSQL = " insert into smr_trn_tsalesorderdtl (" +
                                 " salesorderdtl_gid ," +
                                 " salesorder_gid," +
                                 " shopify_productid ," +
                                  " product_gid ," +
                                   " uom_gid ," +
                                 " shopify_lineitemid ," +
                                 " shopify_orderid ," +
                                 " product_name," +
                                 " display_field," +
                                 " product_price," +
                                 " qty_quoted," +
                                 " selling_price," +
                                 " product_price_l, " +
                                 " price_l" +
                                 ")values(" +
                                 " '" + dt.ItemArray[0] + "'," +
                                 " '" + dt.ItemArray[1] + "'," +
                                 " '" + dt.ItemArray[2] + "'," +
                                 " '" + lsproductgid + "'," +
                                 " '" + lsproductuomgid + "'," +
                                 " '" + dt.ItemArray[3] + "'," +
                                 " '" + dt.ItemArray[4] + "'," +
                                 " '" + dt.ItemArray[5] + "'," +
                                 " '" + dt.ItemArray[5] + "'," +
                                 " '" + dt.ItemArray[6] + "'," +
                                 " '" + dt.ItemArray[7] + "'," +
                                 " '" + dt.ItemArray[8] + "'," +
                                 " '" + dt.ItemArray[9] + "'," +
                                 " '" + dt.ItemArray[9] + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " select (product_price*qty_quoted) as total_amount  from smr_trn_tsalesorderdtl where salesorderdtl_gid='" + dt.ItemArray[0] + "' ";
                                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                                    if (objOdbcDataReader .HasRows)
                                    {
                                        lstotal_amount = objOdbcDataReader ["total_amount"].ToString();


                                    }
                                     

                                    if (mnResult != 0)
                                    {
                                        msSQL = " update  smr_trn_tsalesorderdtl set " +
                                 " price = '" + lstotal_amount + "'" +
                                 " where salesorderdtl_gid='" + dt.ItemArray[0] + "' ";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        if (mnResult != 0)
                                        {
                                            values.status = true;
                                            values.message = "Order Sent Successfully";
                                        }
                                        else
                                        {
                                            values.status = false;
                                            values.message = "Error Occurred While Sending Order ";
                                        }

                                    }
                                    else
                                    {
                                        values.status = false;
                                        values.message = "Error Occurred While Sending Order ";
                                    }
                                }
                            }



                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error Occurred While Order Sending";
                        }

                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {

                values.message = "Error Occurred While Order Sending";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }


        public void DaSendpayment(string user_gid, shopifyordermovingtopayment values)
        {
            try
            {


                for (int i = 0; i < values.shopifypaymentlists.ToArray().Length; i++)
                {
                    msSQL = "select shopify_orderid from smr_trn_tsalesorder where shopify_orderid='" + values.shopifypaymentlists[i].shopify_orderid + "' ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count == 0)
                    {

                        msSQL = " select employee_gid  from hrm_mst_temployee where user_gid = '" + user_gid + "'";

                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsemployee_gid = objOdbcDataReader ["employee_gid"].ToString();

                        }
                         
                        msSQL = " select customer_gid,addon_charge,currency_code,customer_mobile,customer_email,date_format(salesorder_date,'%Y-%m-%d') as salesorder_date  from cmr_smm_tshopifysalesorder where shopify_orderid='" + values.shopifypaymentlists[i].shopify_orderid + "' ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lssalesorder_date = objOdbcDataReader ["salesorder_date"].ToString();
                            lscustomer_gid = objOdbcDataReader ["customer_gid"].ToString();
                            lsaddon_charge = objOdbcDataReader ["addon_charge"].ToString();
                            lscurrency_code = objOdbcDataReader ["currency_code"].ToString();
                            lscustomer_mobile = objOdbcDataReader ["customer_mobile"].ToString();
                            lscustomer_email = objOdbcDataReader ["customer_email"].ToString();
                        }
                         
                        msSQL = " select customer_gid from crm_mst_tcustomer where shopify_id='" + lscustomer_gid + "' ";

                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lscustomer_gid = objOdbcDataReader ["customer_gid"].ToString();


                        }
                        if (lscustomer_gid != null && lscustomer_gid != "")
                        {
                            lscustomergid = lscustomer_gid;
                        }
                        else
                        {
                            lscustomergid = null;
                        }


                         

                        msSQL = " insert  into smr_trn_tsalesorder (" +
                                      " salesorder_gid ," +
                                      " shopify_orderid ," +
                                      " salesorder_date," +
                                      " customer_gid," +
                                      " shopifyorder_number," +
                                      " shopifycustomer_id," +
                                      " customer_name," +
                                      " customer_contact_person," +
                                      " created_by," +
                                      " Grandtotal, " +
                                      " salesorder_status, " +
                                      " addon_charge, " +
                                      " grandtotal_l, " +
                                      " currency_code, " +
                                      " customer_mobile, " +
                                      " customer_address," +
                                      " shipping_to," +
                                      " customer_email " +
                                       "  )values(" +
                                      " '" + values.shopifypaymentlists[i].salesorder_gid + "'," +
                                      " '" + values.shopifypaymentlists[i].shopify_orderid + "'," +
                                      " '" + lssalesorder_date + "'," +
                                       " '" + lscustomergid + "'," +
                                      " '" + values.shopifypaymentlists[i].shopifyorder_number + "'," +
                                      " '" + lscustomer_gid + "'," +
                                      " '" + values.shopifypaymentlists[i].customer_contact_person + "'," +
                                      " '" + values.shopifypaymentlists[i].customer_contact_person + "'," +
                                      " '" + lsemployee_gid + "'," +
                                      " '" + values.shopifypaymentlists[i].Grandtotal + lsaddon_charge + "'," +
                                      " '" + values.shopifypaymentlists[i].salesorder_status + "'," +
                                      " '" + lsaddon_charge + "'," +
                                      " '" + values.shopifypaymentlists[i].Grandtotal + lsaddon_charge + "'," +
                                      " '" + lscurrency_code + "',";
                        if (lscustomer_mobile == null || lscustomer_mobile == "")
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + lscustomer_mobile + "',";
                        }
                        if (values.shopifypaymentlists[i].customer_address == null || (values.shopifypaymentlists[i].customer_address == null))
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + (values.shopifypaymentlists[i].customer_address) + "',";
                        }
                        if (values.shopifypaymentlists[i].customer_address == null || (values.shopifypaymentlists[i].customer_address == null))
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + (values.shopifypaymentlists[i].customer_address) + "',";
                        }
                        msSQL += "'" + lscustomer_email + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = " select salesorderdtl_gid,salesorder_gid,product_gid,shopify_lineitemid,shopify_orderid,product_name,product_price,qty_quoted,product_price_l,selling_price,price_l  from cmr_smm_tshopifysalesorderdtl where shopify_orderid='" + values.shopifypaymentlists[i].shopify_orderid + "'";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow dt in dt_datatable.Rows)
                                {
                                    msSQL = " select product_gid ,productuom_gid  from pmr_mst_tproduct where shopify_productid='" + dt.ItemArray[2] + "' ";

                                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                                    if (objOdbcDataReader .HasRows)
                                    {
                                        lsproduct_gid = objOdbcDataReader ["product_gid"].ToString();
                                        lsproductuom_gid = objOdbcDataReader ["productuom_gid"].ToString();

                                    }
                                    if (lsproduct_gid != null && lsproduct_gid != "")
                                    {
                                        lsproductgid = lsproduct_gid;
                                    }
                                    else
                                    {
                                        lsproductgid = null;
                                    }
                                    if (lsproductuom_gid != null && lsproductuom_gid != "")
                                    {
                                        lsproductuomgid = lsproductuom_gid;
                                    }
                                    else
                                    {
                                        lsproductuomgid = null;
                                    }

                                     

                                    msSQL = " insert into smr_trn_tsalesorderdtl (" +
                                 " salesorderdtl_gid ," +
                                 " salesorder_gid," +
                                 " shopify_productid ," +
                                  " product_gid ," +
                                   " uom_gid ," +
                                 " shopify_lineitemid ," +
                                 " shopify_orderid ," +
                                 " product_name," +
                                 " display_field," +
                                 " product_price," +
                                 " qty_quoted" +
                                 ")values(" +
                                 " '" + dt.ItemArray[0] + "'," +
                                 " '" + dt.ItemArray[1] + "'," +
                                 " '" + dt.ItemArray[2] + "'," +
                                 " '" + lsproductgid + "'," +
                                 " '" + lsproductuomgid + "'," +
                                 " '" + dt.ItemArray[3] + "'," +
                                 " '" + dt.ItemArray[4] + "'," +
                                 " '" + dt.ItemArray[5] + "'," +
                                 " '" + dt.ItemArray[5] + "'," +
                                 " '" + dt.ItemArray[6] + "'," +
                                 " '" + dt.ItemArray[7] + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    msSQL = " select (product_price*qty_quoted) as total_amount  from smr_trn_tsalesorderdtl where salesorderdtl_gid='" + dt.ItemArray[0] + "' ";
                                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                                    if (objOdbcDataReader .HasRows)
                                    {
                                        lstotal_amount = objOdbcDataReader ["total_amount"].ToString();


                                    }
                                     

                                    if (mnResult != 0)
                                    {

                                        msSQL = " update  smr_trn_tsalesorderdtl set " +
                                 " price = '" + lstotal_amount + "'" +
                                 " where salesorderdtl_gid='" + dt.ItemArray[0] + "' ";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        if (mnResult != 0)
                                        {
                                            values.status = true;
                                            values.message = "Payment Approved Successfully";
                                        }
                                        else
                                        {
                                            values.status = false;
                                            values.message = "Error Occurred While Payment Approve";
                                        }
                                    }
                                    else
                                    {
                                        values.status = false;
                                        values.message = "Error Occurred While Payment Approve";
                                    }

                                }
                            }



                        }


                    }
                    msSQL = "select shopify_orderid from rbl_trn_tinvoice where shopify_orderid='" + values.shopifypaymentlists[i].shopify_orderid + "' ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count == 0)
                    {

                        msSQL = " select employee_gid  from hrm_mst_temployee where user_gid = '" + user_gid + "'";

                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsemployee_gid = objOdbcDataReader ["employee_gid"].ToString();

                        }
                         
                        msSQL = " select customer_gid,addon_charge,currency_code,customer_mobile,customer_email,date_format(salesorder_date,'%Y-%m-%d') as salesorder_date  from cmr_smm_tshopifysalesorder where shopify_orderid='" + values.shopifypaymentlists[i].shopify_orderid + "' ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lssalesorder_date = objOdbcDataReader ["salesorder_date"].ToString();
                            lscustomer_gid = objOdbcDataReader ["customer_gid"].ToString();
                            lsaddon_charge = objOdbcDataReader ["addon_charge"].ToString();
                            lscurrency_code = objOdbcDataReader ["currency_code"].ToString();
                            lscustomer_mobile = objOdbcDataReader ["customer_mobile"].ToString();
                            lscustomer_email = objOdbcDataReader ["customer_email"].ToString();
                        }
                         
                        msINGetGID = objcmnfunctions.GetMasterGID("SIVT");
                        msSQL = " select customer_gid from crm_mst_tcustomer where shopify_id='" + lscustomer_gid + "' ";

                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lscustomer_gid = objOdbcDataReader ["customer_gid"].ToString();


                        }
                        if (lscustomer_gid != null && lscustomer_gid != "")
                        {
                            lscustomergid = lscustomer_gid;
                        }
                        else
                        {
                            lscustomergid = null;
                        }


                         
                        msSQL = " insert  into rbl_trn_tinvoice (" +
                                      " invoice_gid ," +
                                      " shopify_orderid ," +
                                      " invoice_date," +
                                      " customer_gid," +
                                      " shopifyorder_number," +
                                      " shopifycustomer_id," +
                                      " customer_name," +
                                      " customer_contactperson," +
                                      " approved_by," +
                                      " invoice_amount, " +
                                      " payment_flag, " +
                                      " additionalcharges_amount_L, " +
                                      " payment_amount, " +
                                      " total_amount, " +
                                      " currency_code, " +
                                      " customer_address," +
                                      " customer_email " +
                                       "  )values(" +
                                      " '" + msINGetGID + "'," +
                                      " '" + values.shopifypaymentlists[i].shopify_orderid + "'," +
                                      " '" + lssalesorder_date + "'," +
                                       " '" + lscustomergid + "'," +
                                      " '" + values.shopifypaymentlists[i].shopifyorder_number + "'," +
                                      " '" + lscustomer_gid + "'," +
                                      " '" + values.shopifypaymentlists[i].customer_contact_person + "'," +
                                      " '" + values.shopifypaymentlists[i].customer_contact_person + "'," +
                                      " '" + lsemployee_gid + "'," +
                                      " '" + values.shopifypaymentlists[i].Grandtotal + lsaddon_charge + "'," +
                                      " '" + values.shopifypaymentlists[i].salesorder_status + "'," +
                                      " '" + lsaddon_charge + "'," +
                                      " '" + values.shopifypaymentlists[i].Grandtotal + lsaddon_charge + "'," +
                                      " '" + values.shopifypaymentlists[i].Grandtotal + lsaddon_charge + "'," +
                                      " '" + lscurrency_code + "',";
                        if (values.shopifypaymentlists[i].customer_address == null || (values.shopifypaymentlists[i].customer_address == null))
                        {
                            msSQL += "'" + null + "',"; ;
                        }
                        else
                        {
                            msSQL += "'" + (values.shopifypaymentlists[i].customer_address) + "',";
                        }
                        msSQL += "'" + lscustomer_email + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = " select salesorderdtl_gid,salesorder_gid,product_gid,shopify_lineitemid,shopify_orderid,product_name,product_price,qty_quoted,product_price_l,selling_price,price_l  from cmr_smm_tshopifysalesorderdtl where shopify_orderid='" + values.shopifypaymentlists[i].shopify_orderid + "'";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow dt in dt_datatable.Rows)
                                {
                                    msSQL = " select product_gid ,productuom_gid  from pmr_mst_tproduct where shopify_productid='" + dt.ItemArray[2] + "' ";

                                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                                    if (objOdbcDataReader .HasRows)
                                    {
                                        lsproduct_gid = objOdbcDataReader ["product_gid"].ToString();
                                        lsproductuom_gid = objOdbcDataReader ["productuom_gid"].ToString();

                                    }
                                    if (lsproduct_gid != null && lsproduct_gid != "")
                                    {
                                        lsproductgid = lsproduct_gid;
                                    }
                                    else
                                    {
                                        lsproductgid = null;
                                    }
                                    if (lsproductuom_gid != null && lsproductuom_gid != "")
                                    {
                                        lsproductuomgid = lsproductuom_gid;
                                    }
                                    else
                                    {
                                        lsproductuomgid = null;
                                    }

                                     

                                    msGetGidInvDt = objcmnfunctions.GetMasterGID("SIVC");
                                    msSQL = " insert into rbl_trn_tinvoicedtl (" +
                                    " invoicedtl_gid ," +
                                    " invoice_gid," +
                                     " product_gid," +
                                      " uom_gid," +
                                    " shopify_productid ," +
                                    " shopify_lineitemid ," +
                                    " shopify_orderid ," +
                                    " product_name," +
                                    " product_price," +
                                    " qty_invoice," +
                                    " vendor_price" +
                                    ")values(" +
                                    " '" + msGetGidInvDt + "'," +
                                    " '" + msINGetGID + "'," +
                                    " '" + lsproductgid + "'," +
                                    " '" + lsproductuomgid + "'," +
                                    " '" + dt.ItemArray[2] + "'," +
                                    " '" + dt.ItemArray[3] + "'," +
                                    " '" + dt.ItemArray[4] + "'," +
                                    " '" + dt.ItemArray[5] + "'," +
                                    " '" + dt.ItemArray[6] + "'," +
                                    " '" + dt.ItemArray[7] + "'," +
                                    " '" + dt.ItemArray[6] + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    if (mnResult != 0)
                                    {
                                        msSQL = " select (product_price*qty_invoice) as total_invoice  from rbl_trn_tinvoicedtl where invoicedtl_gid='" + msGetGidInvDt + "' ";
                                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                                        if (objOdbcDataReader .HasRows)
                                        {
                                            lstotal_invoice = objOdbcDataReader ["total_invoice"].ToString();


                                        }
                                         
                                        if (mnResult != 0)
                                        {

                                            msSQL = " update  rbl_trn_tinvoicedtl set " +
                                     " product_total = '" + lstotal_invoice + "'" +
                                     "  where invoicedtl_gid='" + msGetGidInvDt + "' ";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                            if (mnResult != 0)
                                            {
                                                values.status = true;
                                                values.message = "Payment Approved Successfully";
                                            }
                                            else
                                            {
                                                values.status = false;
                                                values.message = "Error Occurred While Payment Approve";
                                            }

                                        }

                                        else
                                        {
                                            values.status = false;
                                            values.message = "Error Occurred While Payment Approve";
                                        }
                                    }
                                }



                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error Occurred While Payment Approve";
                            }
                        }
                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Error Occurred While Payment Approve";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
          
        }
        public void DaSendinventorystock(string user_gid, shopifyinventorystocksend values)
        {
            try
            {


                for (int i = 0; i < values.shopifyinventorystocksendlist.ToArray().Length; i++)
                {
                    msSQL = "select shopify_productid from ims_trn_tstock where shopify_productid='" + values.shopifyinventorystocksendlist[i].shopify_productid + "' ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count == 0)
                    {

                        msSQL = " select date_format(created_date,'%Y-%m-%d') as created_date   from crm_smm_tshopifyproduct where shopify_productid='" + values.shopifyinventorystocksendlist[i].shopify_productid + "' ";

                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lscreated_date = objOdbcDataReader ["created_date"].ToString();

                        }
                         
                        msSQL = " select product_gid ,productuom_gid  from pmr_mst_tproduct where shopify_productid='" + values.shopifyinventorystocksendlist[i].shopify_productid + "' ";

                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsproduct_gid = objOdbcDataReader ["product_gid"].ToString();
                            lsproductuom_gid = objOdbcDataReader ["productuom_gid"].ToString();

                        }
                        if (lsproduct_gid != null && lsproduct_gid != "")
                        {
                            lsproductgid = lsproduct_gid;
                        }
                        else
                        {
                            lsproductgid = null;
                        }
                        if (lsproductuom_gid != null && lsproductuom_gid != "")
                        {
                            lsproductuomgid = lsproductuom_gid;
                        }
                        else
                        {
                            lsproductuomgid = null;
                        }

                         
                        msGetGid = objcmnfunctions.GetMasterGID("ISKP");
                        msSQL = " insert  into ims_trn_tstock (" +
                                      " stock_gid ," +
                                      " shopify_productid ," +
                                      " product_gid," +
                                        " uom_gid," +
                                      " created_date," +
                                      " display_field," +
                                      " stock_qty," +
                                      " grn_qty," +
                                      " unit_price," +
                                      " created_by," +
                                      " stock_flag " +
                                       "  )values(" +
                                      " '" + msGetGid + "'," +
                                      " '" + values.shopifyinventorystocksendlist[i].shopify_productid + "'," +
                                      " '" + lsproductgid + "'," +
                                      " '" + lsproductuomgid + "'," +
                                      " '" + lscreated_date + "'," +
                                      " '" + values.shopifyinventorystocksendlist[i].product_name + "'," +
                                      " '" + values.shopifyinventorystocksendlist[i].inventory_quantity + "'," +
                                      " '" + values.shopifyinventorystocksendlist[i].old_inventory_quantity + "'," +
                                      " '" + values.shopifyinventorystocksendlist[i].product_price + "'," +
                                      " '" + user_gid + "'," +
                                    "'Y')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Moved To Stock Successfully";




                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error Occurred While Moving Stock";
                        }

                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Error Occurred While Moving Stock";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }


    }
}