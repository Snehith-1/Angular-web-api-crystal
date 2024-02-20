using ems.crm.Models;
using ems.crm.DataAccess;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
using System.Web.DynamicData;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using static OfficeOpenXml.ExcelErrorValue;
using System.Diagnostics;
using System.Web.Http.Results;


namespace ems.crm.DataAccess
{
    public class DaProduct
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;

        string msEmployeeGID, lsemployee_gid, lsuser_gid, lsentity_code, lsproducttype_name, lsproduct_name, lslocation_id, lsproduct,lsshopify, lsshopify_flag, lsshopify_productid, lsaccess_token, lsshopify_store_name, lsstore_month_year, lsdesignation_code, lsCode, msGetGid9, lsproductgroup_gid, lsproductgroupgid, msGetGid5, msGetGid4, lsproducttypegid, lsproducttype_gid, final_path,lsproductuomgid, productuom_gid, lsproductuom_gid, lsproductuomclass_gid, lsproductuomclassgid, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid, lsproduct_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public getproduct DaGetShopifyProductdetails(string user_gid)
        {

            getproduct objresult = new getproduct();
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
            var request = new RestRequest("/admin/api/" + lsstore_month_year + "/products.json?limit=250", Method.GET);
            request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
            request.AddHeader("Cookie", "_master_udr=eyJfcmFpbHMiOnsibWVzc2FnZSI6IkJBaEpJaWxqWXpsak9UQXhPUzAyWkRZMkxUUXlOR1F0T0RKbVl5MDNaVEZsTnpFM09EY3dOV0lHT2daRlJnPT0iLCJleHAiOiIyMDI1LTEwLTIwVDA4OjI3OjU2LjU4MloiLCJwdXIiOiJjb29raWUuX21hc3Rlcl91ZHIifX0%3D--6f6310c22570c2812426da811c5f9f64d2d35161; _secure_admin_session_id=bbc22793fbba552b04eeebfaeb0de080; _secure_admin_session_id_csrf=bbc22793fbba552b04eeebfaeb0de080; identity-state=BAhbB0kiJWVhODM3YTZhN2M3Njg1MzhlNWQ3MTNhYzg2NmM5MWUwBjoGRUZJIiUwY2M0MWQ1ZjE4ZTQwZTcwYWQ1ZTVkMWUzMDBkMzZlYgY7AEY%3D--69633ab3c25bb20e105bbe14b912f36422abe9b1; identity-state-0cc41d5f18e40e70ad5e5d1e300d36eb=BAh7DEkiDnJldHVybi10bwY6BkVUSSI0aHR0cHM6Ly9lNDQ1NWYtMi5teXNob3BpZnkuY29tL2FkbWluL2F1dGgvbG9naW4GOwBUSSIRcmVkaXJlY3QtdXJpBjsAVEkiQGh0dHBzOi8vZTQ0NTVmLTIubXlzaG9waWZ5LmNvbS9hZG1pbi9hdXRoL2lkZW50aXR5L2NhbGxiYWNrBjsAVEkiEHNlc3Npb24ta2V5BjsAVDoMYWNjb3VudEkiD2NyZWF0ZWQtYXQGOwBUZhcxNjk3NzkxOTE0LjQyODUwMDJJIgpub25jZQY7AFRJIiU1YTQyNzRiZTI5ZmVjODE0MDU4ZTlmNGU3ZGZiNzU4MwY7AEZJIgpzY29wZQY7AFRbEEkiCmVtYWlsBjsAVEkiN2h0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvZGVzdGluYXRpb25zLnJlYWRvbmx5BjsAVEkiC29wZW5pZAY7AFRJIgxwcm9maWxlBjsAVEkiTmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvcGFydG5lcnMuY29sbGFib3JhdG9yLXJlbGF0aW9uc2hpcHMucmVhZG9ubHkGOwBUSSIwaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9iYW5raW5nLm1hbmFnZQY7AFRJIkJodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL21lcmNoYW50LXNldHVwLWRhc2hib2FyZC5ncmFwaHFsBjsAVEkiPGh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvc2hvcGlmeS1jaGF0LmFkbWluLmdyYXBocWwGOwBUSSI3aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9mbG93LndvcmtmbG93cy5tYW5hZ2UGOwBUSSI%2BaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9vcmdhbml6YXRpb24taWRlbnRpdHkubWFuYWdlBjsAVEkiPmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvbWVyY2hhbnQtYmFuay1hY2NvdW50Lm1hbmFnZQY7AFRJIg9jb25maWcta2V5BjsAVEkiDGRlZmF1bHQGOwBU--eb8709d3d8002d429911a5b1c28afca37dd02431; identity-state-ea837a6a7c768538e5d713ac866c91e0=BAh7DEkiDnJldHVybi10bwY6BkVUSSI0aHR0cHM6Ly9lNDQ1NWYtMi5teXNob3BpZnkuY29tL2FkbWluL2F1dGgvbG9naW4GOwBUSSIRcmVkaXJlY3QtdXJpBjsAVEkiQGh0dHBzOi8vZTQ0NTVmLTIubXlzaG9waWZ5LmNvbS9hZG1pbi9hdXRoL2lkZW50aXR5L2NhbGxiYWNrBjsAVEkiEHNlc3Npb24ta2V5BjsAVDoMYWNjb3VudEkiD2NyZWF0ZWQtYXQGOwBUZhYxNjk3NzkwNDc2LjU5MTkxOEkiCm5vbmNlBjsAVEkiJWM5NjYwYTQ2NmZhMTlhZTJlNDQyYmM3NjU0NDMxZWMzBjsARkkiCnNjb3BlBjsAVFsQSSIKZW1haWwGOwBUSSI3aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9kZXN0aW5hdGlvbnMucmVhZG9ubHkGOwBUSSILb3BlbmlkBjsAVEkiDHByb2ZpbGUGOwBUSSJOaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9wYXJ0bmVycy5jb2xsYWJvcmF0b3ItcmVsYXRpb25zaGlwcy5yZWFkb25seQY7AFRJIjBodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL2JhbmtpbmcubWFuYWdlBjsAVEkiQmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvbWVyY2hhbnQtc2V0dXAtZGFzaGJvYXJkLmdyYXBocWwGOwBUSSI8aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9zaG9waWZ5LWNoYXQuYWRtaW4uZ3JhcGhxbAY7AFRJIjdodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL2Zsb3cud29ya2Zsb3dzLm1hbmFnZQY7AFRJIj5odHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL29yZ2FuaXphdGlvbi1pZGVudGl0eS5tYW5hZ2UGOwBUSSI%2BaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9tZXJjaGFudC1iYW5rLWFjY291bnQubWFuYWdlBjsAVEkiD2NvbmZpZy1rZXkGOwBUSSIMZGVmYXVsdAY7AFQ%3D--7f37bdb0df101ca716441e71427765ef41612063");
            IRestResponse response = client.Execute(request);
            string response_content = response.Content;
            shopifyproductlist objMdlShopifyMessageResponse = new shopifyproductlist();
            objMdlShopifyMessageResponse = JsonConvert.DeserializeObject<shopifyproductlist>(response_content);
            if (response.StatusCode == HttpStatusCode.OK)
            {

                foreach (var item in objMdlShopifyMessageResponse.products)
                {

                    msSQL = " select shopify_productid  from crm_smm_tshopifyproduct where shopify_productid = '" + item.id + "'";
                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader .HasRows != true)
                    {

                        // Parse the original date string to a DateTime object
                        DateTime originalDate = DateTime.ParseExact(item.created_at.ToString(), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);

                        // Convert the DateTime object to the desired format
                        string formattedDate = originalDate.ToString("yyyy-MM-dd");
                        msSQL = " insert into crm_smm_tshopifyproduct (" +
                                 " shopify_productid," +
                                   " product_name," +
                                " variant_id, " +
                                " option1, " +
                                 " product_type, " +
                                 " inventory_item_id, " +
                                " inventory_quantity, " +
                                " old_inventory_quantity, " +
                                " status, " +
                                " product_image, " +
                                " grams, " +
                                "  weight, " +
                                "  weight_unit, " +
                                " price, " +
                                " compare_at_price, " +
                                " vendor_name, " +
                                   " created_by, " +
                                   " created_date)" +
                                   " values(" +
                                   " '" + item.id + "',";
                        if (item.title == null || item.title == "")
                        {
                            msSQL += "'',";
                        }
                        else
                        {

                            msSQL += "'" + item.title.Replace("'", "\\'").Replace("）", ")").Replace("（", "(") + "',";
                        }
                        msSQL += "'" + item.variants[0].id + "'," +
                            "'" + item.variants[0].option1 + "'," +
                            "'" + item.product_type + "'," +
                           "'" + item.variants[0].inventory_item_id + "'," +
                        "'" + item.variants[0].inventory_quantity + "'," +
                        "'" + item.variants[0].old_inventory_quantity + "',";
                        if (item.status == "active")
                        {
                            msSQL += "'1',";
                        }
                        else if (item.status == "draft")
                        {
                            msSQL += "'2',";
                        }
                        else if (item.status == "archived")
                        {
                            msSQL += "'3',";
                        }
                        if (item.image == null)
                        {
                            msSQL += "'0',";
                        }
                        else
                        {
                            msSQL += "'" + item.images[0].src + "',";
                        }
                        msSQL += "'" + item.variants[0].grams + "'," +
                             "'" + item.variants[0].weight + "'," +
                             "'" + item.variants[0].weight_unit + "'," +
                       "'" + item.variants[0].price + "'," +
                       "'" + item.variants[0].compare_at_price + "'," +
                       "'" + item.vendor + "'," +
                        "'" + user_gid + "'," +
                        "'" + formattedDate + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                     



                }
                objOdbcDataReader .Close();
            }
            

            return objresult;
        }
        public getproduct DaGetShopifyProduct(string user_gid)
        {
            getproduct objresult = new getproduct();
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
            var request = new RestRequest("/admin/api/" + lsstore_month_year + "/products.json?limit=250", Method.GET);
            request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
            request.AddHeader("Cookie", "_master_udr=eyJfcmFpbHMiOnsibWVzc2FnZSI6IkJBaEpJaWxqWXpsak9UQXhPUzAyWkRZMkxUUXlOR1F0T0RKbVl5MDNaVEZsTnpFM09EY3dOV0lHT2daRlJnPT0iLCJleHAiOiIyMDI1LTEwLTIwVDA4OjI3OjU2LjU4MloiLCJwdXIiOiJjb29raWUuX21hc3Rlcl91ZHIifX0%3D--6f6310c22570c2812426da811c5f9f64d2d35161; _secure_admin_session_id=bbc22793fbba552b04eeebfaeb0de080; _secure_admin_session_id_csrf=bbc22793fbba552b04eeebfaeb0de080; identity-state=BAhbB0kiJWVhODM3YTZhN2M3Njg1MzhlNWQ3MTNhYzg2NmM5MWUwBjoGRUZJIiUwY2M0MWQ1ZjE4ZTQwZTcwYWQ1ZTVkMWUzMDBkMzZlYgY7AEY%3D--69633ab3c25bb20e105bbe14b912f36422abe9b1; identity-state-0cc41d5f18e40e70ad5e5d1e300d36eb=BAh7DEkiDnJldHVybi10bwY6BkVUSSI0aHR0cHM6Ly9lNDQ1NWYtMi5teXNob3BpZnkuY29tL2FkbWluL2F1dGgvbG9naW4GOwBUSSIRcmVkaXJlY3QtdXJpBjsAVEkiQGh0dHBzOi8vZTQ0NTVmLTIubXlzaG9waWZ5LmNvbS9hZG1pbi9hdXRoL2lkZW50aXR5L2NhbGxiYWNrBjsAVEkiEHNlc3Npb24ta2V5BjsAVDoMYWNjb3VudEkiD2NyZWF0ZWQtYXQGOwBUZhcxNjk3NzkxOTE0LjQyODUwMDJJIgpub25jZQY7AFRJIiU1YTQyNzRiZTI5ZmVjODE0MDU4ZTlmNGU3ZGZiNzU4MwY7AEZJIgpzY29wZQY7AFRbEEkiCmVtYWlsBjsAVEkiN2h0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvZGVzdGluYXRpb25zLnJlYWRvbmx5BjsAVEkiC29wZW5pZAY7AFRJIgxwcm9maWxlBjsAVEkiTmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvcGFydG5lcnMuY29sbGFib3JhdG9yLXJlbGF0aW9uc2hpcHMucmVhZG9ubHkGOwBUSSIwaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9iYW5raW5nLm1hbmFnZQY7AFRJIkJodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL21lcmNoYW50LXNldHVwLWRhc2hib2FyZC5ncmFwaHFsBjsAVEkiPGh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvc2hvcGlmeS1jaGF0LmFkbWluLmdyYXBocWwGOwBUSSI3aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9mbG93LndvcmtmbG93cy5tYW5hZ2UGOwBUSSI%2BaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9vcmdhbml6YXRpb24taWRlbnRpdHkubWFuYWdlBjsAVEkiPmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvbWVyY2hhbnQtYmFuay1hY2NvdW50Lm1hbmFnZQY7AFRJIg9jb25maWcta2V5BjsAVEkiDGRlZmF1bHQGOwBU--eb8709d3d8002d429911a5b1c28afca37dd02431; identity-state-ea837a6a7c768538e5d713ac866c91e0=BAh7DEkiDnJldHVybi10bwY6BkVUSSI0aHR0cHM6Ly9lNDQ1NWYtMi5teXNob3BpZnkuY29tL2FkbWluL2F1dGgvbG9naW4GOwBUSSIRcmVkaXJlY3QtdXJpBjsAVEkiQGh0dHBzOi8vZTQ0NTVmLTIubXlzaG9waWZ5LmNvbS9hZG1pbi9hdXRoL2lkZW50aXR5L2NhbGxiYWNrBjsAVEkiEHNlc3Npb24ta2V5BjsAVDoMYWNjb3VudEkiD2NyZWF0ZWQtYXQGOwBUZhYxNjk3NzkwNDc2LjU5MTkxOEkiCm5vbmNlBjsAVEkiJWM5NjYwYTQ2NmZhMTlhZTJlNDQyYmM3NjU0NDMxZWMzBjsARkkiCnNjb3BlBjsAVFsQSSIKZW1haWwGOwBUSSI3aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9kZXN0aW5hdGlvbnMucmVhZG9ubHkGOwBUSSILb3BlbmlkBjsAVEkiDHByb2ZpbGUGOwBUSSJOaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9wYXJ0bmVycy5jb2xsYWJvcmF0b3ItcmVsYXRpb25zaGlwcy5yZWFkb25seQY7AFRJIjBodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL2JhbmtpbmcubWFuYWdlBjsAVEkiQmh0dHBzOi8vYXBpLnNob3BpZnkuY29tL2F1dGgvbWVyY2hhbnQtc2V0dXAtZGFzaGJvYXJkLmdyYXBocWwGOwBUSSI8aHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9zaG9waWZ5LWNoYXQuYWRtaW4uZ3JhcGhxbAY7AFRJIjdodHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL2Zsb3cud29ya2Zsb3dzLm1hbmFnZQY7AFRJIj5odHRwczovL2FwaS5zaG9waWZ5LmNvbS9hdXRoL29yZ2FuaXphdGlvbi1pZGVudGl0eS5tYW5hZ2UGOwBUSSI%2BaHR0cHM6Ly9hcGkuc2hvcGlmeS5jb20vYXV0aC9tZXJjaGFudC1iYW5rLWFjY291bnQubWFuYWdlBjsAVEkiD2NvbmZpZy1rZXkGOwBUSSIMZGVmYXVsdAY7AFQ%3D--7f37bdb0df101ca716441e71427765ef41612063");
            IRestResponse response = client.Execute(request);
            string response_content = response.Content;
            shopifyproductlist objMdlShopifyMessageResponse = new shopifyproductlist();
            objMdlShopifyMessageResponse = JsonConvert.DeserializeObject<shopifyproductlist>(response_content);
            if (response.StatusCode == HttpStatusCode.OK)
            {

                foreach (var item in objMdlShopifyMessageResponse.products)
                {

                    msSQL = " select shopify_productid  from pmr_mst_tproduct where shopify_productid = '" + item.id + "'";
                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader .HasRows != true)
                    {
                        msSQL = " select productuomclass_gid  from pmr_mst_tproductuomclass where productuomclass_name = '" + item.variants[0].weight_unit + "' limit 1 ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsproductuomclass_gid = objOdbcDataReader ["productuomclass_gid"].ToString();

                        }
                         
                        if (lsproductuomclass_gid != null)
                        {
                            lsproductuomclassgid = lsproductuomclass_gid;
                        }
                        else
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("PUCM");
                            msSQL = " insert into pmr_mst_tproductuomclass (" +
                            " productuomclass_gid," +
                            " productuomclass_name ," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "',";
                            if (item.variants[0].weight_unit == null || item.variants[0].weight_unit == "")
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + item.variants[0].weight_unit.Replace("'", "\\'") + "',";
                            }
                            msSQL += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            lsproductuomclassgid = msGetGid;
                        }

                        msSQL = " select productuom_gid  from pmr_mst_tproductuom where productuomclass_gid = '" + lsproductuomclass_gid + "' and productuom_gid = '" + item.variants[0].weight + "' limit 1 ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsproductuom_gid = objOdbcDataReader ["productuom_gid"].ToString();

                        }
                         
                        if (lsproductuom_gid != null)
                        {
                            lsproductuomgid = lsproductuom_gid;
                        }
                        else
                        {
                            msGetGid1 = objcmnfunctions.GetMasterGID("PPMM");
                            msSQL = " insert into pmr_mst_tproductuom (" +
                            " productuom_gid," +
                         " productuomclass_gid," +
                         " productuom_name ," +
                         " created_by, " +
                         " created_date)" +
                         " values(" +
                         "'" + msGetGid1 + "'," +
                         " '" + lsproductuomclass_gid + "'," +
                             "'" + item.variants[0].weight + "'," +
                            "'" + user_gid + "',";
                            msSQL += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            lsproductuomgid = msGetGid1;

                        }




                        msSQL = " select productgroup_gid from pmr_mst_tproductgroup where productgroup_name = '" + item.product_type + "'  limit 1 ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsproductgroup_gid = objOdbcDataReader ["productgroup_gid"].ToString();

                        }
                         
                        if (lsproductgroup_gid != null)
                        {
                            lsproductgroupgid = lsproductgroup_gid;
                        }
                        else
                        {
                            msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='PPGM' order by finyear desc limit 0,1 ";
                            string lsCode1 = objdbconn.GetExecuteScalar(msSQL);

                            string lsproductgroup_code = "PGC" + "00" + lsCode1;
                            msGetGid5 = objcmnfunctions.GetMasterGID("PPGM");

                            msSQL = " insert into pmr_mst_tproductgroup (" +
                                        " productgroup_gid," +
                                        " productgroup_code," +
                                        " productgroup_name," +
                                        " created_by, " +
                                        " created_date)" +
                                        " values(" +
                                        " '" + msGetGid5 + "'," +
                                        " '" + lsproductgroup_code + "'," +
                                        "'" + item.product_type + "'," +
                                        "'" + user_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            lsproductgroupgid = msGetGid5;

                        }





                        msSQL = " select producttype_gid from pmr_mst_tproducttype where producttype_name  = '" + item.product_type + "'";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsproducttype_gid = objOdbcDataReader ["producttype_gid"].ToString();

                        }
                         
                        if (lsproducttype_gid != null)
                        {
                            lsproducttypegid = lsproducttype_gid;
                        }
                        else
                        {
                            msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='PPTM' order by finyear desc limit 0,1 ";
                            string lsCode6 = objdbconn.GetExecuteScalar(msSQL);

                            string lsproduct_typegid = "PG" + "00" + lsCode6;

                            msSQL = " insert into pmr_mst_tproducttype (" +
                            " producttype_gid," +
                         " producttype_name)" +
                         " values(" +
                         "'" + lsproduct_typegid + "',";
                            msSQL += "'" + item.product_type + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            lsproducttypegid = lsproduct_typegid;

                        }

                        msGetGid = objcmnfunctions.GetMasterGID("PPTM");
                        msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='PPTM' order by finyear desc limit 0,1 ";
                        string lsCode7 = objdbconn.GetExecuteScalar(msSQL);

                        string lsproduct_code = "PC" + "00" + lsCode7;


                        msGetGid9 = objcmnfunctions.GetMasterGID("PPTM");
                        msSQL = " insert into pmr_mst_tproduct (" +
                               " product_gid," +
                               " product_code," +
                               " product_name," +
                            " productgroup_gid, " +
                            " productuomclass_gid, " +
                            " productuom_gid, " +
                            " stockable, " +
                            " producttype_gid, " +
                            " variant_id, " +
                            " inventory_quantity, " +
                            " old_inventory_quantity, " +
                            " status, " +
                            " product_image, " +
                            " grams, " +
                            " price, " +
                            " compare_at_price, " +
                            " vendor_name, " +
                            " shopify_productid," +
                               " created_by, " +
                               " created_date)" +
                               " values(" +
                               " '" + msGetGid9 + "'," +
                               "'" + lsproduct_code + "',";
                        if (item.title == null || item.title == "")
                        {
                            msSQL += "'',";
                        }
                        else
                        {

                            msSQL += "'" + item.title.Replace("'", "\\'").Replace("）", ")").Replace("（", "(") + "',";
                        }
                        msSQL += "'" + lsproductgroupgid + "'," +
                                 "'" + lsproductuomclassgid + "'," +
                                 "'" + lsproductuomgid + "'," +
                        "'" + "Y" + "'," +
                        "'" + lsproducttypegid + "'," +
                        "'" + item.variants[0].id + "'," +
                        "'" + item.variants[0].inventory_quantity + "'," +
                        "'" + item.variants[0].old_inventory_quantity + "',";
                        if (item.status == "active")
                        {
                            msSQL += "'1',";
                        }
                        else if (item.status == "draft")
                        {
                            msSQL += "'2',";
                        }
                        else if (item.status == "archived")
                        {
                            msSQL += "'3',";
                        }
                        if (item.image == null)
                        {
                            msSQL += "'0',";
                        }
                        else
                        {
                            msSQL += "'" + item.images[0].src + "',";
                        }
                        msSQL += "'" + item.variants[0].grams + "'," +
                       "'" + item.variants[0].price + "'," +
                       "'" + item.variants[0].compare_at_price + "'," +
                       "'" + item.vendor + "'," +
                        "'" + item.id + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                     



                }
                objOdbcDataReader .Close();
            }
            

            return objresult;
        }
        public void DaGetProductSummary(MdlProduct values)
        {
            try
            {
                 
                msSQL = " SELECT d.producttype_name,b.productgroup_name,b.productgroup_code,a.product_gid, a.product_price, a.cost_price, a.product_code, CONCAT_WS('|',a.product_name,a.size, a.width, a.length) as product_name,  CONCAT(f.user_firstname,' ',f.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date, " +
                   " c.productuomclass_code,e.productuom_code,c.productuomclass_name,(case when a.stockable ='Y' then 'Yes' else 'No ' end) as stockable,e.productuom_name,d.producttype_name as product_type,(case when a.status ='1' then 'Active'  when a.status ='2' then 'Draft'  when a.status ='3' then 'Archived' else 'Inactive' end) as Status," +
                   " (case when a.serial_flag ='Y' then 'Yes' else 'No' end)as serial_flag,(case when a.avg_lead_time is null then '0 days' else concat(a.avg_lead_time,'  ', 'days') end)as lead_time,(case when a.shopify_productid is not null then 'No'  else 'Yes' end)as shopify_productid,a.product_image  from pmr_mst_tproduct a " +
                   " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid " +
                   " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid " +
                   " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid " +
                   " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid " +
                   " left join adm_mst_tuser f on f.user_gid=a.created_by order by a.created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<product_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new product_list
                        {
                            shopify_productid = dt["shopify_productid"].ToString(),
                            product_image = dt["product_image"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                            producttype_name = dt["producttype_name"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            productgroup_code = dt["productgroup_code"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            cost_price = dt["cost_price"].ToString(),
                            productuomclass_code = dt["productuomclass_code"].ToString(),
                            productuom_code = dt["productuom_code"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            stockable = dt["stockable"].ToString(),

                            productuom_name = dt["productuom_name"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            Status = dt["Status"].ToString(),
                            serial_flag = dt["serial_flag"].ToString(),
                            lead_time = dt["lead_time"].ToString(),


                        });
                        values.product_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting product summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
           
           
        }
        public void DaGetShopifyProductSummary(MdlProduct values)
        {
            try
            {
                 
                msSQL = " SELECT a.option1,a.inventory_quantity,a.old_inventory_quantity,a.variant_id,a.inventory_item_id,a.weight_unit,a.weight,a.price,a.grams,d.producttype_name,b.productgroup_name,b.productgroup_code,a.product_gid, a.product_price, a.cost_price, a.product_code, CONCAT_WS('|',a.product_name,a.size, a.width, a.length) as product_name,  CONCAT(f.user_firstname,' ',f.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date, " +
        " c.productuomclass_code,e.productuom_code,c.productuomclass_name,a.product_type,a.vendor_name,(case when a.stockable ='Y' then 'Yes' else 'No ' end) as stockable,e.productuom_name,d.producttype_name as product_type,(case when a.status ='1' then 'active'  when a.status ='2' then 'draft'  when a.status ='draft' then 'draft' when a.status ='active' then 'active' when a.status ='inactive' then 'inactive' when a.status ='archived' then 'archived'   when a.status ='3' then 'archived' else 'inactive' end)  as Status," +
        " (case when a.serial_flag ='Y' then 'Yes' else 'No' end)as serial_flag,(case when a.shopify_productid=s.shopify_productid then 'Assigned' when s.shopify_productid is null then 'Not Assign' end) as status_flag,(case when a.avg_lead_time is null then '0 days' else concat(a.avg_lead_time,'  ', 'days') end)as lead_time,a.shopify_productid,(case when a.product_image ='0' then 'no' else a.product_image end)  as product_image  from crm_smm_tshopifyproduct a " +
        " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid " +
        " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid " +
        " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid " +
        " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid " +
          " left join pmr_mst_tproduct s on a.shopify_productid = s.shopify_productid " +
        " left join adm_mst_tuser f on f.user_gid=a.created_by order by a.id desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<product_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new product_list
                        {
                            option1 = dt["option1"].ToString(),
                            inventory_quantity = dt["inventory_quantity"].ToString(),
                            old_inventory_quantity = dt["old_inventory_quantity"].ToString(),
                            variant_id = dt["variant_id"].ToString(),
                            inventory_item_id = dt["inventory_item_id"].ToString(),
                            weight_unit = dt["weight_unit"].ToString(),
                            weight = dt["weight"].ToString(),
                            price = dt["price"].ToString(),
                            status_flag = dt["status_flag"].ToString(),
                            shopify_productid = dt["shopify_productid"].ToString(),
                            product_image = dt["product_image"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            vendor_name = dt["vendor_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            grams = dt["grams"].ToString(),
                            producttype_name = dt["producttype_name"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            productgroup_code = dt["productgroup_code"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            cost_price = dt["cost_price"].ToString(),
                            productuomclass_code = dt["productuomclass_code"].ToString(),
                            productuom_code = dt["productuom_code"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            stockable = dt["stockable"].ToString(),

                            productuom_name = dt["productuom_name"].ToString(),

                            Status = dt["Status"].ToString(),
                            serial_flag = dt["serial_flag"].ToString(),
                            lead_time = dt["lead_time"].ToString(),


                        });
                        values.product_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Getting Shopify Product Summary!! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }
        public void DaGetShopifyProductInventorySummary(MdlProduct values)
        {
            try
            {
                 
                msSQL = " SELECT inventory_item_id,a.variant_id,a.inventory_quantity,a.old_inventory_quantity,d.producttype_name,b.productgroup_name,b.productgroup_code,a.product_gid, a.product_price, a.cost_price, a.product_code, CONCAT_WS('|',a.product_name,a.size, a.width, a.length) as product_name,  CONCAT(f.user_firstname,' ',f.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date, " +
        " c.productuomclass_code,e.productuom_code,c.productuomclass_name,a.product_type,a.vendor_name,(case when a.stockable ='Y' then 'Yes' else 'No ' end) as stockable,e.productuom_name,d.producttype_name as product_type,(case when a.status ='1' then 'active'  when a.status ='2' then 'draft'  when a.status ='draft' then 'draft' when a.status ='active' then 'active' when a.status ='inactive' then 'inactive' when a.status ='archived' then 'archived'   when a.status ='3' then 'archived' else 'inactive' end)  as Status," +
        " (case when a.serial_flag ='Y' then 'Yes' else 'No' end)as serial_flag,(case when a.shopify_productid=s.shopify_productid then 'Assigned' when s.shopify_productid is null then 'Not Assign' end) as status_flag,(case when a.avg_lead_time is null then '0 days' else concat(a.avg_lead_time,'  ', 'days') end)as lead_time,a.shopify_productid,(case when a.product_image ='0' then 'no' else a.product_image end)  as product_image  from crm_smm_tshopifyproduct a " +
        " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid " +
        " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid " +
        " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid " +
        " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid " +
          " left join ims_trn_tstock s on a.shopify_productid = s.shopify_productid " +
        " left join adm_mst_tuser f on f.user_gid=a.created_by order by a.id desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<productinventory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new productinventory_list
                        {

                            inventory_item_id = dt["inventory_item_id"].ToString(),
                            variant_id = dt["variant_id"].ToString(),
                            inventory_quantity = dt["inventory_quantity"].ToString(),
                            old_inventory_quantity = dt["old_inventory_quantity"].ToString(),
                            status_flag = dt["status_flag"].ToString(),
                            shopify_productid = dt["shopify_productid"].ToString(),
                            product_image = dt["product_image"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            vendor_name = dt["vendor_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                            producttype_name = dt["producttype_name"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            productgroup_code = dt["productgroup_code"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            cost_price = dt["cost_price"].ToString(),
                            productuomclass_code = dt["productuomclass_code"].ToString(),
                            productuom_code = dt["productuom_code"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            stockable = dt["stockable"].ToString(),

                            productuom_name = dt["productuom_name"].ToString(),

                            Status = dt["Status"].ToString(),
                            serial_flag = dt["serial_flag"].ToString(),
                            lead_time = dt["lead_time"].ToString(),


                        });
                        values.productinventory_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Getting Shopify Product Inventory Summary!! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }
        public void DaGetproducttypedropdown(MdlProduct values)
        {
            try
            {
                 
                msSQL = " Select producttype_name,producttype_gid  " +
                 " from pmr_mst_tproducttype ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getproducttypedropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getproducttypedropdown
                        {
                            producttype_name = dt["producttype_name"].ToString(),
                            producttype_gid = dt["producttype_gid"].ToString(),
                        });
                        values.Getproducttypedropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting product type!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            
         
        }
        public void DaGetproductgroupdropdown(MdlProduct values)
        {
            try
            {
                 
                msSQL = " Select productgroup_gid, productgroup_name from pmr_mst_tproductgroup  " +
                   " order by productgroup_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getproductgroupdropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getproductgroupdropdown
                        {
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                        });
                        values.Getproductgroupdropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting product group!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            
           
        }

        public void DaGetproductunitclassdropdown(MdlProduct values)
        {
            try
            {
                 
                msSQL = " Select productuomclass_gid, productuomclass_code, productuomclass_name  " +
                  " from pmr_mst_tproductuomclass order by productuomclass_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getproductunitclassdropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getproductunitclassdropdown
                        {
                            productuomclass_gid = dt["productuomclass_gid"].ToString(),
                            productuomclass_code = dt["productuomclass_code"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                        });
                        values.Getproductunitclassdropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured getting product unit class!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            
          
        }
        public void DaGetproductunitdropdown(MdlProduct values)
        {
            try
            {
                 
                msSQL = " select productuom_name,productuom_gid from pmr_mst_tproductuom a left join pmr_mst_tproductuomclass b on b.productuomclass_gid= a.productuomclass_gid  " +
                  " order by a.sequence_level ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getproductunitdropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getproductunitdropdown
                        {
                            productuom_name = dt["productuom_name"].ToString(),
                            productuom_gid = dt["productuom_gid"].ToString(),

                        });
                        values.Getproductunitdropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting product unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            
          
        }
        public void DaGetcurrencydropdown(MdlProduct values)
        {
            try
            {
                 
                msSQL = " select currency_code,currencyexchange_gid  " +
                   " from crm_trn_tcurrencyexchange ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getcurrencydropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getcurrencydropdown
                        {
                            currency_code = dt["currency_code"].ToString(),
                            currencyexchange_gid = dt["currencyexchange_gid"].ToString(),

                        });
                        values.Getcurrencydropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting currency!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
           
           
        }
        public void DaPostShopifyProduct(string user_gid, shopifyproduct_list values)
        {
            try
            {
                 
                msSQL = " select product_name from crm_smm_tshopifyproduct where product_name = '" + values.product_name + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows == true)
                {
                    values.status = false;
                    values.message = "product Name Already Exist !!";
                }
                else
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
                    var request = new RestRequest("/admin/api/" + lsstore_month_year + "/products.json", Method.POST);
                    request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Cookie", "request_method=PUT");
                    var body = "{\"product\":{\"title\":" + "\"" + values.product_name + "\"" + ",\"body_html\":" + "\"" + values.product_desc + "\"" + ",\"product_type\":" + "\"" + values.product_type + "\"" + ",\"vendor\":" + "\"" + values.vendor + "\"" + ",\"status\":" + "\"" + values.product_status + "\"" + "}}";
                    var body_content = JsonConvert.DeserializeObject(body);
                    request.AddParameter("application/json", body_content, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    string errornetsuiteJSON = response.Content;
                    productpost_list objMdlMailCampaignResponse = new productpost_list();
                    objMdlMailCampaignResponse = JsonConvert.DeserializeObject<productpost_list>(errornetsuiteJSON);
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        // Parse the original date string to a DateTime object
                        DateTime originalDate = DateTime.ParseExact(objMdlMailCampaignResponse.product.created_at.ToString(), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);

                        // Convert the DateTime object to the desired format
                        string formattedDate = originalDate.ToString("yyyy-MM-dd");
                        msSQL = " insert into crm_smm_tshopifyproduct (" +
                             " shopify_productid," +
                               " product_name," +
                            " variant_id, " +
                             " product_type, " +
                             " inventory_item_id, " +
                             " option1, " +
                             " product_desc, " +
                            " inventory_quantity, " +
                            " old_inventory_quantity, " +
                            " status, " +
                            " product_image, " +
                            " grams, " +
                            " price, " +
                            " compare_at_price, " +
                            " vendor_name, " +
                               " created_by, " +
                               " created_date)" +
                               " values(" +
                               " '" + objMdlMailCampaignResponse.product.id + "',";
                        if (objMdlMailCampaignResponse.product.title == null || objMdlMailCampaignResponse.product.title == "")
                        {
                            msSQL += "'',";
                        }
                        else
                        {

                            msSQL += "'" + objMdlMailCampaignResponse.product.title.Replace("'", "\\'").Replace("）", ")").Replace("（", "(") + "',";
                        }
                        msSQL += "'" + objMdlMailCampaignResponse.product.variants[0].id + "'," +
                            "'" + objMdlMailCampaignResponse.product.product_type + "'," +
                            "'" + objMdlMailCampaignResponse.product.variants[0].inventory_item_id + "'," +
                            "'" + objMdlMailCampaignResponse.product.variants[0].option1 + "'," +
                            "'" + objMdlMailCampaignResponse.product.body_html + "'," +
                        "'" + objMdlMailCampaignResponse.product.variants[0].inventory_quantity + "'," +
                        "'" + objMdlMailCampaignResponse.product.variants[0].old_inventory_quantity + "',";
                        if (objMdlMailCampaignResponse.product.status == "active")
                        {
                            msSQL += "'1',";
                        }
                        else if (objMdlMailCampaignResponse.product.status == "draft")
                        {
                            msSQL += "'2',";
                        }
                        else if (objMdlMailCampaignResponse.product.status == "archived")
                        {
                            msSQL += "'3',";
                        }
                        if (objMdlMailCampaignResponse.product.image == null)
                        {
                            msSQL += "'0',";
                        }
                        else
                        {
                            msSQL += "'" + objMdlMailCampaignResponse.product.images[0].src + "',";
                        }
                        msSQL += "'" + objMdlMailCampaignResponse.product.variants[0].grams + "'," +
                       "'" + objMdlMailCampaignResponse.product.variants[0].price + "'," +
                       "'" + objMdlMailCampaignResponse.product.variants[0].compare_at_price + "'," +
                       "'" + objMdlMailCampaignResponse.product.vendor + "'," +
                        "'" + user_gid + "'," +
                        "'" + formattedDate + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

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
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Product";
                    }
                }
                objOdbcDataReader .Close();
            }

            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Adding Shopify Product!! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

            


        }

        public void DaShopifyProductUpdate(string user_gid, shopifyproduct_list values)
        {
            try
            {
                 
                msSQL = " select product_name,shopify_productid from crm_smm_tshopifyproduct where product_name = '" + values.product_name + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows)
                {
                    lsproduct_name = objOdbcDataReader ["product_name"].ToString();

                    lsshopify_productid = objOdbcDataReader ["shopify_productid"].ToString();
                }
                if (lsproduct_name == null || lsproduct_name == "")
                {
                    lsproduct = null;
                }
                else
                {
                    lsproduct = lsproduct_name.ToUpper();

                }
                if (lsshopify_productid == null || lsshopify_productid == "")
                {
                    lsshopify = null;
                }
                else
                {
                    lsshopify = lsshopify_productid;
                }
                string productname = values.product_name.ToUpper();

                if (lsshopify == values.shopify_productid)
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
                    var request = new RestRequest("/admin/api/" + lsstore_month_year + "/products/" + values.shopify_productid + ".json", Method.PUT);
                    request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Cookie", "request_method=PUT");
                    var body = "{\"product\":{\"title\":" + "\"" + values.product_name + "\"" + ",\"body_html\":" + "\"" + values.product_desc + "\"" + ",\"vendor\":" + "\"" + values.vendor + "\"" + ",\"status\":" + "\"" + values.product_status + "\"" + ",\"product_type\":" + "\"" + values.product_type + "\"" + "}}";
                    var body_content = JsonConvert.DeserializeObject(body);
                    request.AddParameter("application/json", body_content, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    string errornetsuiteJSON = response.Content;
                    productpost_list objMdlMailCampaignResponse = new productpost_list();
                    objMdlMailCampaignResponse = JsonConvert.DeserializeObject<productpost_list>(errornetsuiteJSON);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // Parse the original date string to a DateTime object
                        DateTime originalDate = DateTime.ParseExact(objMdlMailCampaignResponse.product.created_at.ToString(), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);

                        // Convert the DateTime object to the desired format
                        string formattedDate = originalDate.ToString("yyyy-MM-dd");
                        msSQL = " update  crm_smm_tshopifyproduct  set " +
                " product_name = '" + objMdlMailCampaignResponse.product.title + "'," +
                " product_desc = '" + objMdlMailCampaignResponse.product.body_html + "'," +
                " inventory_item_id = '" + objMdlMailCampaignResponse.product.variants[0].inventory_item_id + "'," +
                " product_type = '" + objMdlMailCampaignResponse.product.product_type + "'," +
                " status = '" + objMdlMailCampaignResponse.product.status + "'," +
                " vendor_name = '" + objMdlMailCampaignResponse.product.vendor + "'," +
                " updated_by = '" + user_gid + "'," +
                " updated_date = '" + formattedDate + "' where shopify_productid='" + values.shopify_productid + "'  ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Product Updated Successfully";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Product";
                        }
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Product";
                    }
                }
                else if (lsproduct != productname)
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
                    var request = new RestRequest("/admin/api/" + lsstore_month_year + "/products/" + values.shopify_productid + ".json", Method.PUT);
                    request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Cookie", "request_method=PUT");
                    var body = "{\"product\":{\"title\":" + "\"" + values.product_name + "\"" + ",\"body_html\":" + "\"" + values.product_desc + "\"" + ",\"vendor\":" + "\"" + values.vendor + "\"" + ",\"status\":" + "\"" + values.product_status + "\"" + ",\"product_type\":" + "\"" + values.product_type + "\"" + "}}";
                    var body_content = JsonConvert.DeserializeObject(body);
                    request.AddParameter("application/json", body_content, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    string errornetsuiteJSON = response.Content;
                    productpost_list objMdlMailCampaignResponse = new productpost_list();
                    objMdlMailCampaignResponse = JsonConvert.DeserializeObject<productpost_list>(errornetsuiteJSON);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // Parse the original date string to a DateTime object
                        DateTime originalDate = DateTime.ParseExact(objMdlMailCampaignResponse.product.created_at.ToString(), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);

                        // Convert the DateTime object to the desired format
                        string formattedDate = originalDate.ToString("yyyy-MM-dd");
                        msSQL = " update  crm_smm_tshopifyproduct  set " +
                " product_name = '" + objMdlMailCampaignResponse.product.title + "'," +
                " product_desc = '" + objMdlMailCampaignResponse.product.body_html + "'," +
                " product_type = '" + objMdlMailCampaignResponse.product.product_type + "'," +
                " status = '" + objMdlMailCampaignResponse.product.status + "'," +
                " vendor_name = '" + objMdlMailCampaignResponse.product.vendor + "'," +
                " updated_by = '" + user_gid + "'," +
                " updated_date = '" + formattedDate + "' where shopify_productid='" + values.shopify_productid + "'  ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Product Updated Successfully";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Product";
                        }
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Product";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Product Name Already Exist !";
                }

                objOdbcDataReader .Close();

            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Updating Shopify Product!! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaPostProduct(string user_gid, product_list values)
        {
            try
            {
                 
                msSQL = " select product_name from pmr_mst_tproduct where product_name = '" + values.product_name + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows == true)
                {
                    values.status = false;
                    values.message = "product Name Already Exist !!";
                }

                else
                {
                    msSQL = " select producttype_name  from pmr_mst_tproducttype where producttype_gid = '" + values.producttype_name + "'";
                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader .HasRows)
                    {

                        lsproducttype_name = objOdbcDataReader ["producttype_name"].ToString();

                    }

                     
                    msGetGid = objcmnfunctions.GetMasterGID("PPTM");
                    msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='PPTM' order by finyear desc limit 0,1 ";
                    string lsCode = objdbconn.GetExecuteScalar(msSQL);

                    string lsproduct_code = "PC" + "00" + lsCode;

                    msGetGid = objcmnfunctions.GetMasterGID("PPTM");
                    msSQL = " insert into pmr_mst_tproduct (" +
                       " product_gid," +
                       " product_code," +
                       " product_name," +
                   " product_desc, " +
                   " productgroup_gid, " +
                   " productuomclass_gid, " +
                   " productuom_gid, " +
                   " mrp_price, " +
                   " cost_price, " +
                   " avg_lead_time, " +
                   " stockable, " +
                   " status, " +
                   " producttype_gid, " +
                   " purchasewarrenty_flag, " +
                   " expirytracking_flag, " +
                   " batch_flag," +
                   " serial_flag," +
                       " created_by, " +
                       " created_date)" +
                       " values(" +
                       " '" + msGetGid + "'," +
                       "'" + lsproduct_code + "',";
                    if (values.product_name == null || values.product_name == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.product_name.Replace("'", "\\'") + "',";
                    }
                    msSQL += "'" + values.product_desc + "'," +
                             "'" + values.productgroup_name + "'," +
                             "'" + values.productuomclass_name + "'," +
                             "'" + values.productuom_name + "',";
                    if (values.mrp_price == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.mrp_price + "',";
                    }
                    if (values.cost_price == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.cost_price + "',";
                    }
                    msSQL += "'" + values.avg_lead_time + "'," +
                    "'" + "Y" + "'," +
                    "'" + "1" + "'," +
                    "'" + values.producttype_name + "'," +
                    "'" + values.purchasewarrenty_flag + "'," +
                    "'" + values.expirytracking_flag + "'," +
                    "'" + values.batch_flag + "'," +
                    "'" + values.serial_flag + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    if (mnResult != 0)
                    {

                        values.status = true;
                        values.message = "Product Added Successfully !";

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Product";
                    }

                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding product!";
            }
          


        }
        public void DaProductUpdate(string user_gid, product_list values)
        {
            try
            {

                msSQL = " select product_name from pmr_mst_tproduct where product_name = '" + values.product_name + "'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader.HasRows == true)
                {
                    values.status = false;
                    values.message = "product Name Already Exist !!";
                }
                else
                {

                    msSQL = " SELECT productgroup_gid FROM pmr_mst_tproductgroup WHERE productgroup_name='" + values.productgroupname + "' ";
                    string lsproductgroup_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " SELECT producttype_gid FROM pmr_mst_tproducttype WHERE producttype_name='" + values.producttypename + "' ";
                    string lsproducttype_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " SELECT productuomclass_gid FROM pmr_mst_tproductuomclass WHERE productuomclass_name='" + values.productuomclassname + "' ";
                    string lsproductuomclass_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " SELECT productuom_gid FROM pmr_mst_tproductuom WHERE productuom_name='" + values.productuomname + "' ";
                    string lsproductuom_gid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " update  pmr_mst_tproduct  set " +
                  " product_name = '" + values.product_name + "'," +
                  " product_code = '" + values.product_code + "'," +
                  " product_desc = '" + values.product_desc + "'," +
                  " currency_code = '" + values.currency_code + "'," +
                  " productgroup_gid = '" + lsproductgroup_gid + "'," +
                  " producttype_gid = '" + lsproducttype_gid + "'," +
                  " productuomclass_gid = '" + lsproductuomclass_gid + "'," +
                  " productuom_gid = '" + lsproductuom_gid + "'," +
                  " mrp_price = '" + values.mrp_price + "'," +
                  " cost_price = '" + values.cost_price + "'," +
                  " avg_lead_time = '" + values.avg_lead_time + "'," +
                  " purchasewarrenty_flag = '" + values.purchasewarrenty_flag + "'," +
                  " expirytracking_flag = '" + values.expirytracking_flag + "'," +
                  " batch_flag = '" + values.batch_flag + "'," +
                  " serial_flag = '" + values.serial_flag + "'," +
                  " updated_by = '" + user_gid + "'," +
                  " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where product_gid='" + values.product_gid + "'  ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {


                        values.status = true;
                        values.message = "Product Updated Successfully";


                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Product";
                    }



                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while updating product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            
        

        }
        public void DaGetEditProductSummary(string product_gid, MdlProduct values)
        {
            try
            {
                 
                msSQL = "  select  a.batch_flag,a.serial_flag, a.purchasewarrenty_flag,a.expirytracking_flag,a.product_desc,a.avg_lead_time," +
  "  a.mrp_price,a.cost_price,a.product_gid,a.product_name,a.product_code,b.productgroup_gid,b.productgroup_name,c.productuomclass_gid,c.productuomclass_name," +
  "  d.producttype_gid,d.producttype_name,e.productuom_gid,e.productuom_name from pmr_mst_tproduct a " +
  " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid" +
  " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid" +
  " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid " +
  " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid" +
  " where a.product_gid='" + product_gid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetEditProductSummary>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetEditProductSummary
                        {


                            productgroup_name = dt["productgroup_name"].ToString(),
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            productuomclass_gid = dt["productuomclass_gid"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            productuom_gid = dt["productuom_gid"].ToString(),
                            producttype_name = dt["producttype_name"].ToString(),
                            producttype_gid = dt["producttype_gid"].ToString(),
                            batch_flag = dt["batch_flag"].ToString(),
                            serial_flag = dt["serial_flag"].ToString(),
                            expirytracking_flag = dt["expirytracking_flag"].ToString(),
                            purchasewarrenty_flag = dt["purchasewarrenty_flag"].ToString(),
                            cost_price = dt["cost_price"].ToString(),
                            mrp_price = dt["mrp_price"].ToString(),
                            product_desc = dt["product_desc"].ToString(),
                            avg_lead_time = dt["avg_lead_time"].ToString(),



                        });
                        values.GetEditProductSummary = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting edit product summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");

            }
            

        }
    
        public void DaGetEditShopifyProductSummary(string shopifyproductid, MdlProduct values)
        {
            try
            {
                 
                msSQL = "  select  a.batch_flag,a.serial_flag,(case when a.status ='1' then 'active'  when a.status ='2' then 'draft'  when a.status ='draft' then 'draft' when a.status ='active' then 'active' when a.status ='inactive' then 'inactive' when a.status ='archived' then 'archived'   when a.status ='3' then 'archived' else 'inactive' end) as product_status, a.purchasewarrenty_flag,a.expirytracking_flag,a.product_desc,a.avg_lead_time," +
             "  a.mrp_price,a.cost_price,a.product_gid,a.vendor_name,a.shopify_productid,a.id,a.product_name,a.product_type,a.product_code,b.productgroup_gid,b.productgroup_name,c.productuomclass_gid,c.productuomclass_name," +
             "  d.producttype_gid,d.producttype_name,e.productuom_gid,e.productuom_name from crm_smm_tshopifyproduct a " +
             " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid" +
             " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid" +
             " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid " +
             " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid" +
             " where a.shopify_productid='" + shopifyproductid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetEditProductSummary>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetEditProductSummary
                        {


                            id = dt["id"].ToString(),
                            shopify_productid = dt["shopify_productid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_status = dt["product_status"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            productuomclass_gid = dt["productuomclass_gid"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            productuom_gid = dt["productuom_gid"].ToString(),
                            producttype_name = dt["producttype_name"].ToString(),
                            producttype_gid = dt["producttype_gid"].ToString(),
                            batch_flag = dt["batch_flag"].ToString(),
                            serial_flag = dt["serial_flag"].ToString(),
                            expirytracking_flag = dt["expirytracking_flag"].ToString(),
                            purchasewarrenty_flag = dt["purchasewarrenty_flag"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            vendor_name = dt["vendor_name"].ToString(),
                            product_desc = dt["product_desc"].ToString(),
                            avg_lead_time = dt["avg_lead_time"].ToString(),



                        });
                        values.GetEditProductSummary = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Getting Edit Shopify Product Summary!! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
           
        }
        public void DaGetViewProductSummary(string product_gid, MdlProduct values)
        {
            try
            {
                 
                msSQL = "  select CASE WHEN a.batch_flag = 'N' THEN 'NO' ELSE 'YES' END AS batch_flag,CASE WHEN a.serial_flag = 'N' THEN 'NO' ELSE 'YES' END AS serial_flag," +
        " CASE WHEN a.purchasewarrenty_flag = 'N' THEN 'NO' ELSE 'YES' END AS purchasewarrenty_flag,CASE WHEN a.expirytracking_flag = 'N' THEN 'NO' ELSE 'YES' END AS expirytracking_flag," +
        "  a.product_desc,a.avg_lead_time,a.mrp_price,e.producttype_name,a.cost_price,b.currency_code,a.product_gid,c.productgroup_name,a.product_name,f.productuomclass_name,a.product_code,d.productuom_name " +
        "  from pmr_mst_tproduct a " +
        "  left join crm_trn_tcurrencyexchange b on b.currency_code=a.currency_code" +
        "  left  join pmr_mst_tproductgroup c on a.productgroup_gid=c.productgroup_gid" +
        "  left join pmr_mst_tproductuom d on a.productuom_gid=d.productuom_gid" +
        "  left  join pmr_mst_tproducttype e on a.producttype_gid=e.producttype_gid" +
        "  left  join pmr_mst_tproductuomclass f on a.productuomclass_gid=f.productuomclass_gid" +
        "  where a.product_gid='" + product_gid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetViewProductSummary>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetViewProductSummary
                        {


                            productgroup_name = dt["productgroup_name"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            producttype_name = dt["producttype_name"].ToString(),
                            batch_flag = dt["batch_flag"].ToString(),
                            serial_flag = dt["serial_flag"].ToString(),
                            expirytracking_flag = dt["expirytracking_flag"].ToString(),
                            purchasewarrenty_flag = dt["purchasewarrenty_flag"].ToString(),
                            cost_price = dt["cost_price"].ToString(),
                            mrp_price = dt["mrp_price"].ToString(),
                            product_desc = dt["product_desc"].ToString(),
                            avg_lead_time = dt["avg_lead_time"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            currency_code = dt["currency_code"].ToString(),

                        });
                        values.GetViewProductSummary = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting product view!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            

        }
        public void DaGetOnChangeProductUnitClass(string productuomclass_gid, MdlProduct values)
        {
            try
            {
                 
                  msSQL = " select productuom_name,productuom_gid from pmr_mst_tproductuom a left join pmr_mst_tproductuomclass b on b.productuomclass_gid= a.productuomclass_gid  " +
                     " where b.productuomclass_gid ='" + productuomclass_gid + "' order by a.sequence_level  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetProductUnit>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetProductUnit
                    {
                        productuom_name = dt["productuom_name"].ToString(),
                        productuom_gid = dt["productuom_gid"].ToString(),

                    });
                    values.GetProductUnit = getModuleList;
                }
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while changing product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
           
          
        }

        public void DaGetProductImage(HttpRequest httpRequest, result objResult, string user_gid)
        {

            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            HttpPostedFile httpPostedFile;

            string lspath;
            string msGetGid;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            string product_gid = httpRequest.Form[0];

            MemoryStream ms = new MemoryStream();
           
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        bool status1;


                        status1 = objcmnfunctions.UploadStream(ConfigurationManager.AppSettings["blob_containername"], lscompany_code + "/" + "CRM/Product/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, FileExtension, ms);
                        ms.Close();
       
                        final_path = ConfigurationManager.AppSettings["blob_containername"] + "/" + lscompany_code + "/" + "CRM/Product/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        msSQL = "update pmr_mst_tproduct set " +
                                                       " product_image='" + ConfigurationManager.AppSettings["blob_imagepath1"] + final_path + msdocument_gid + FileExtension + ConfigurationManager.AppSettings["blob_imagepath2"] +
                                                       '&'+ ConfigurationManager.AppSettings["blob_imagepath3"] + '&' + ConfigurationManager.AppSettings["blob_imagepath4"] + '&' + ConfigurationManager.AppSettings["blob_imagepath5"] +
                                                       '&' + ConfigurationManager.AppSettings["blob_imagepath6"] + '&' + ConfigurationManager.AppSettings["blob_imagepath7"] + '&' + ConfigurationManager.AppSettings["blob_imagepath8"] + "'" +
                                                        " where product_gid='" + product_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }


                if (mnResult != 0)
                {
                    objResult.status = true;
                    objResult.message = "Product Image Added Successfully !!";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error While Adding Product Image !!";
                }

            }

            catch (Exception ex)
            {
                objResult.message = ex.ToString();
            }
            //return true;

        }
        public void DaGetdeleteproductdetails(string product_gid, product_list values)
        {
            try { 
            msSQL = "  delete from pmr_mst_tproduct where product_gid='" + product_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Product";
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while deleting product details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaProductUploadExcels(HttpRequest httpRequest, string user_gid, result objResult, product_list values)
        {
            try
            {
                 
                string lscompany_code;
                string excelRange, endRange;
                int rowCount, columnCount;

                try
                {
                    int insertCount = 0;
                    HttpFileCollection httpFileCollection;
                    DataTable dt = null;
                    string lspath, lsfilePath;

                    msSQL = " select company_code from adm_mst_tcompany";
                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                    // Create Directory
                    lsfilePath = ConfigurationManager.AppSettings["importexcelfile1"];

                    if (!Directory.Exists(lsfilePath))
                        Directory.CreateDirectory(lsfilePath);

                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        httpPostedFile = httpFileCollection[i];
                    }
                    string FileExtension = httpPostedFile.FileName;

                    string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                    string lsfile_gid = msdocument_gid;
                    FileExtension = Path.GetExtension(FileExtension).ToLower();
                    lsfile_gid = lsfile_gid + FileExtension;

                    Stream ls_readStream;
                    ls_readStream = httpPostedFile.InputStream;
                    MemoryStream ms = new MemoryStream();
                    ls_readStream.CopyTo(ms);

                    //path creation        
                    lspath = lsfilePath + "/";
                    FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                    ms.WriteTo(file);
                    try
                    {
                        using (ExcelPackage xlPackage = new ExcelPackage(ms))
                        {
                            ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets["Product Report"];
                            rowCount = worksheet.Dimension.End.Row;
                            columnCount = worksheet.Dimension.End.Column;
                            endRange = worksheet.Dimension.End.Address;
                        }
                        string status;
                        status = objcmnfunctions.uploadFile(lsfilePath, FileExtension);
                        file.Close();
                        ms.Close();

                        objcmnfunctions.uploadFile(lspath, lsfile_gid);
                    }
                    catch (Exception ex)
                    {
                        objResult.status = false;
                        objResult.message = ex.ToString();
                        return;
                    }

                    //Excel To DataTable
                    try
                    {
                        lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                        excelRange = "A1:" + endRange + rowCount.ToString();
                        dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                        dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                    }
                    catch (Exception ex)
                    {
                        objResult.status = false;
                        objResult.message = ex.ToString();
                        return;
                    }
                    //  Nullable<DateTime> ldcodecreation_date;

                    string[] columnNames = dt.Columns.Cast<DataColumn>()
                                         .Select(x => x.ColumnName)
                                         .ToArray();
                    string Header_name = "", Header_value = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (var i in columnNames)
                        {
                            Header_name = i.Trim();
                            Header_name = Header_name.Replace("*", "");
                            Header_name = Header_name.Replace(" ", "_");
                            Header_value = row[i].ToString();
                        }
                    }
                    for (int r = 0; r <= dt.Rows.Count - 1; r++)
                    {
                        // Your code logic here
                    }
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.ToString();
                }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while uploading product excel!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
           
           
        }
        public void DaGetProductReportExport(MdlProduct values)
        {
            try
            {
                 
                msSQL = " SELECT d.producttype_name as 'Product Type',b.productgroup_name as 'Product Group', a.product_code as 'Product Code', CONCAT_WS('|',a.product_name,a.size, a.width, a.length) as 'Product',c.productuomclass_name as 'Unit', a.cost_price as 'Cost Price', " +
        " (case when a.avg_lead_time is null then '0 days' else concat(a.avg_lead_time,'  ', 'days') end)as 'Avg Lead Time',(case when a.status ='1' then 'Active' else 'Inactive' end) as 'Product Status'" +
        "   from pmr_mst_tproduct a " +
        " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid " +
        " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid " +
        " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid " +
        " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid " +
        " left join adm_mst_tuser f on f.user_gid=a.created_by order by a.created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                string lscompany_code = string.Empty;
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("Product Report");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";

                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    string lspath = ConfigurationManager.AppSettings["exportexcelfile"] + "/product/export" + "/" + lscompany_code + "/" + "CRM/Product/Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                    //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/TestReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    {
                        if ((!System.IO.Directory.Exists(lspath)))
                            System.IO.Directory.CreateDirectory(lspath);
                    }

                    string lsname = "Product_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                    string lspath1 = ConfigurationManager.AppSettings["exportexcelfile"] + "/product/export" + "/" + lscompany_code + "/" + "CRM/Product/Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsname;

                    workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                    FileInfo file = new FileInfo(lspath1);
                    using (var range = workSheet.Cells[1, 1, 1, 8])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                        range.Style.Font.Color.SetColor(Color.White);
                    }
                    excel.SaveAs(file);

                    var getModuleList = new List<productexport_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {

                        getModuleList.Add(new productexport_list
                        {

                            lsname = lsname,
                            lspath1 = lspath1,



                        });
                        values.productexport_list = getModuleList;

                    }
                    dt_datatable.Dispose();
                    values.status = true;
                    values.message = "Success";
                }
                catch (Exception ex)
                {
                    values.status = false;
                    values.message = "Failure";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting product report excel!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            

        }
        public void DadownloadImages(string product_gid, MdlProduct values)
        {
            try
            {
                 
                msSQL = "SELECT product_image, name FROM pmr_mst_tproduct WHERE product_gid = '" + product_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<product_images>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new product_images
                        {
                            product_image = dt["product_image"].ToString(),
                            name = dt["name"].ToString()
                        });
                    }
                    values.product_images = getModuleList;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while downloading image!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
           
      
        }
        public void DaSendproductmaster(string user_gid, shopifyproductmove_list values)
        {
            try
            {
                 
                for (int i = 0; i < values.shopifyproduct_lists.ToArray().Length; i++)
                {
                    msSQL = " select shopify_productid  from pmr_mst_tproduct where shopify_productid = '" + values.shopifyproduct_lists[i].shopify_productid + "'";
                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader .HasRows != true)
                    {
                        msSQL = " select productuomclass_gid  from pmr_mst_tproductuomclass where productuomclass_name = '" + values.shopifyproduct_lists[i].weight + "' limit 1 ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsproductuomclass_gid = objOdbcDataReader ["productuomclass_gid"].ToString();

                        }
                         
                        if (lsproductuomclass_gid != null)
                        {
                            lsproductuomclassgid = lsproductuomclass_gid;
                        }
                        else
                        {
                            if (values.shopifyproduct_lists[i].weight != null && values.shopifyproduct_lists[i].weight != "")
                            {
                                msGetGid = objcmnfunctions.GetMasterGID("PUCM");
                                msSQL = " insert into pmr_mst_tproductuomclass (" +
                                " productuomclass_gid," +
                                " productuomclass_name ," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "',";
                                if (values.shopifyproduct_lists[i].weight == null || values.shopifyproduct_lists[i].weight == "")
                                {
                                    msSQL += "'',";
                                }
                                else
                                {
                                    msSQL += "'" + values.shopifyproduct_lists[i].weight.Replace("'", "\\'") + "',";
                                }
                                msSQL += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                lsproductuomclassgid = msGetGid;
                            }
                            else
                            {
                                lsproductuomclassgid = "";
                            }
                        }

                        msSQL = " select productuom_gid  from pmr_mst_tproductuom where productuomclass_gid = '" + lsproductuomclass_gid + "' and productuom_name = '" + values.shopifyproduct_lists[i].weight_unit + "' limit 1 ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsproductuom_gid = objOdbcDataReader ["productuom_gid"].ToString();

                        }
                         
                        if (lsproductuom_gid != null)
                        {
                            lsproductuomgid = lsproductuom_gid;
                        }
                        else
                        {
                            if (values.shopifyproduct_lists[i].weight_unit != null && values.shopifyproduct_lists[i].weight_unit != "")
                            {
                                msGetGid1 = objcmnfunctions.GetMasterGID("PPMM");
                                msSQL = " insert into pmr_mst_tproductuom (" +
                                " productuom_gid," +
                             " productuomclass_gid," +
                             " productuom_name ," +
                             " created_by, " +
                             " created_date)" +
                             " values(" +
                             "'" + msGetGid1 + "'," +
                             " '" + lsproductuomclass_gid + "'," +
                                 "'" + values.shopifyproduct_lists[i].weight_unit + "'," +
                                "'" + user_gid + "',";
                                msSQL += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                lsproductuomgid = msGetGid1;
                            }
                            else
                            {

                                lsproductuomgid = "";
                            }

                        }




                        msSQL = " select productgroup_gid from pmr_mst_tproductgroup where productgroup_name = '" + values.shopifyproduct_lists[i].product_type + "'  limit 1 ";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsproductgroup_gid = objOdbcDataReader ["productgroup_gid"].ToString();

                        }
                         
                        if (lsproductgroup_gid != null)
                        {
                            lsproductgroupgid = lsproductgroup_gid;
                        }
                        else
                        {
                            msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='PPGM' order by finyear desc limit 0,1 ";
                            string lsCode1 = objdbconn.GetExecuteScalar(msSQL);

                            string lsproductgroup_code = "PGC" + "00" + lsCode1;
                            msGetGid5 = objcmnfunctions.GetMasterGID("PPGM");

                            msSQL = " insert into pmr_mst_tproductgroup (" +
                                        " productgroup_gid," +
                                        " productgroup_code," +
                                        " productgroup_name," +
                                        " created_by, " +
                                        " created_date)" +
                                        " values(" +
                                        " '" + msGetGid5 + "'," +
                                        " '" + lsproductgroup_code + "'," +
                                        "'" + values.shopifyproduct_lists[i].product_type + "'," +
                                        "'" + user_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            lsproductgroupgid = msGetGid5;

                        }





                        msSQL = " select producttype_gid from pmr_mst_tproducttype where producttype_name  = '" + values.shopifyproduct_lists[i].product_type + "'";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                        if (objOdbcDataReader .HasRows)
                        {
                            lsproducttype_gid = objOdbcDataReader ["producttype_gid"].ToString();

                        }
                         
                        if (lsproducttype_gid != null)
                        {
                            lsproducttypegid = lsproducttype_gid;
                        }
                        else
                        {
                            msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='PPTM' order by finyear desc limit 0,1 ";
                            string lsCode6 = objdbconn.GetExecuteScalar(msSQL);

                            string lsproduct_typegid = "PG" + "00" + lsCode6;

                            msSQL = " insert into pmr_mst_tproducttype (" +
                            " producttype_gid," +
                         " producttype_name)" +
                         " values(" +
                         "'" + lsproduct_typegid + "',";
                            msSQL += "'" + values.shopifyproduct_lists[i].product_type + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            lsproducttypegid = lsproduct_typegid;

                        }

                        msGetGid = objcmnfunctions.GetMasterGID("PPTM");
                        msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='PPTM' order by finyear desc limit 0,1 ";
                        string lsCode7 = objdbconn.GetExecuteScalar(msSQL);

                        string lsproduct_code = "PC" + "00" + lsCode7;


                        msGetGid9 = objcmnfunctions.GetMasterGID("PPTM");
                        msSQL = " insert into pmr_mst_tproduct (" +
                               " product_gid," +
                               " product_code," +
                               " product_name," +
                            " productgroup_gid, " +
                            " productuomclass_gid, " +
                            " productuom_gid, " +
                            " stockable, " +
                            " producttype_gid, " +
                            " variant_id, " +
                            " inventory_quantity, " +
                            " inventory_item_id, " +
                            " old_inventory_quantity, " +
                            " status, " +
                            " product_image, " +
                            " grams, " +
                            " cost_price, " +
                            " mrp_price, " +
                            " shopify_productid," +
                               " created_by, " +
                               " created_date)" +
                               " values(" +
                               " '" + msGetGid9 + "'," +
                               "'" + lsproduct_code + "',";
                        if (values.shopifyproduct_lists[i].product_name == null || values.shopifyproduct_lists[i].product_name == "")
                        {
                            msSQL += "'',";
                        }
                        else
                        {

                            msSQL += "'" + values.shopifyproduct_lists[i].product_name.Replace("'", "\\'").Replace("）", ")").Replace("（", "(") + "',";
                        }
                        msSQL += "'" + lsproductgroupgid + "'," +
                                 "'" + lsproductuomclassgid + "'," +
                                 "'" + lsproductuomgid + "'," +
                        "'" + "Y" + "'," +
                        "'" + lsproducttypegid + "'," +
                        "'" + values.shopifyproduct_lists[i].variant_id + "'," +
                        "'" + values.shopifyproduct_lists[i].inventory_quantity + "'," +
                       "'" + values.shopifyproduct_lists[i].inventory_item_id + "'," +
                        "'" + values.shopifyproduct_lists[i].old_inventory_quantity + "',";
                        if (values.shopifyproduct_lists[i].Status == "active")
                        {
                            msSQL += "'1',";
                        }
                        else if (values.shopifyproduct_lists[i].Status == "draft")
                        {
                            msSQL += "'2',";
                        }
                        else if (values.shopifyproduct_lists[i].Status == "archived")
                        {
                            msSQL += "'3',";
                        }
                        if (values.shopifyproduct_lists[i].product_image == null)
                        {
                            msSQL += "'0',";
                        }
                        else
                        {
                            msSQL += "'" + values.shopifyproduct_lists[i].product_image + "',";
                        }
                        msSQL += "'" + values.shopifyproduct_lists[i].grams + "'," +
                       "'" + values.shopifyproduct_lists[i].price + "'," +
                       "'" + values.shopifyproduct_lists[i].price + "'," +
                        "'" + values.shopifyproduct_lists[i].shopify_productid + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                     

                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Sending product master!! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }
        public void DaUpdateShopifyProductPrice (string user_gid, productprice_list values)
        {
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
                var request = new RestRequest("/admin/api/" + lsstore_month_year + "/variants/" + values.variant_id + ".json", Method.PUT);
                request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cookie", "request_method=PUT");
                var body = "{\"variant\":{\"option1\":" + "\"" + values.attribute + "\"" + ",\"price\":" + "\"" + values.price + "\"" + ",\"inventory_management\":\"shopify\"}}";
                var body_content = JsonConvert.DeserializeObject(body);
                request.AddParameter("application/json", body_content, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    msSQL = " update  crm_smm_tshopifyproduct  set " +
            " price = '" + values.price + "'," +
            " option1 = '" + values.attribute + "'," +
            " updated_by = '" + user_gid + "'," +
            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where shopify_productid='" + values.shopify_productid + "'  ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = " update  pmr_mst_tproduct  set " +
                        " mrp_price = '" + values.price + "'," +
                        " option1 = '" + values.attribute + "'," +
                        " cost_price = '" + values.price + "'," +
                        " updated_by = '" + user_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where shopify_productid='" + values.shopify_productid + "'  ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Product Price Updated Successfully !";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Product Price";
                        }
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Product Price";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Product Price";
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Update Shopify Product Price!! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }
        public void DaUpdateShopifyProductQuantity(string user_gid, productquantity_list values)
        {
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
                 
                msSQL = " SELECT location_id FROM crm_smm_tshopifylocation limit 1 ";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows)
                {

                    lslocation_id = objOdbcDataReader ["location_id"].ToString();


                }
                 



                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var client = new RestClient("https://" + lsshopify_store_name + ".myshopify.com");
                var request = new RestRequest("/admin/api/" + lsstore_month_year + "/inventory_levels/set.json", Method.POST);
                request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
                request.AddHeader("Content-Type", "application/json");
                var body = "{\"location_id\":" + "\"" + lslocation_id + "\"" + ",\"inventory_item_id\":" + "\"" + values.inventory_item_id + "\"" + ",\"available\":" + "\"" + values.inventory_quantity + "\"" + "}";
                var body_content = JsonConvert.DeserializeObject(body);
                request.AddParameter("application/json", body_content, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    msSQL = " update  crm_smm_tshopifyproduct  set " +
            " inventory_quantity = '" + values.inventory_quantity + "'," +
            " old_inventory_quantity = '" + values.inventory_quantity + "'," +
            " updated_by = '" + user_gid + "'," +
            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where shopify_productid='" + values.shopify_productid + "'  ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = " update  pmr_mst_tproduct  set " +
                          " inventory_quantity = '" + values.inventory_quantity + "'," +
                          " old_inventory_quantity = '" + values.inventory_quantity + "'," +
                        " updated_by = '" + user_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where shopify_productid='" + values.shopify_productid + "'  ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = " update  ims_trn_tstock  set " +
                         " stock_qty = '" + values.inventory_quantity + "'," +
                         " grn_qty = '" + values.inventory_quantity + "'," +
                       " updated_by = '" + user_gid + "'," +
                       " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where shopify_productid='" + values.shopify_productid + "'  ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = true;
                        values.message = "Inventory Quantity Updated Successfully !";

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Inventory Quantity";
                    }
                }
                else if (response.StatusDescription == "Unprocessable Entity")
                {
                    values.status = false;
                    values.message = "Kindly Set the Product Price First !";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Inventory Quantity";
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Update Shopify Product Quantity !! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            


        }
        public getproduct DaGetShopifyLocation(string user_gid)
        {
            getproduct objresult = new getproduct();
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
            var request = new RestRequest("/admin/api/" + lsstore_month_year + "/locations.json?limit=250", Method.GET);
            request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
            IRestResponse response = client.Execute(request);
            string response_content = response.Content;
            locationlist objMdlShopifyMessageResponse = new locationlist();
            objMdlShopifyMessageResponse = JsonConvert.DeserializeObject<locationlist>(response_content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                msSQL = "select location_id from crm_smm_tshopifylocation where location_id='" + objMdlShopifyMessageResponse.locations[0].id + "' ";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows != true)
                {
                    msSQL = " insert into crm_smm_tshopifylocation (" +
                         " location_id," +
                            " name)" +
                            " values(" +
                            " '" + objMdlShopifyMessageResponse.locations[0].id + "'," +
                        "'" + objMdlShopifyMessageResponse.locations[0].name + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objOdbcDataReader .Close();
            }
            return objresult;
            
        }
        public void DaUpdateShopifyProductImage(string user_gid, updateshopifyimage_list values)
        {
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
                var request = new RestRequest("/admin/api/" + lsstore_month_year + "/products/" + values.shopify_productid + "/images.json", Method.POST);
                request.AddHeader("X-Shopify-Access-Token", "" + lsaccess_token + "");
                request.AddHeader("Content-Type", "application/json");
                var body = "{\"image\":{\"position\":\"1\",\"attachment\":\"" + values.path + "\"}}";
                var body_content = JsonConvert.DeserializeObject(body);
                request.AddParameter("application/json", body_content, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string errornetsuiteJSON = response.Content;
                shopifyproductImage_list objMdlMailCampaignResponse = new shopifyproductImage_list();
                objMdlMailCampaignResponse = JsonConvert.DeserializeObject<shopifyproductImage_list>(errornetsuiteJSON);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    msSQL = " update  crm_smm_tshopifyproduct  set " +
                    " product_image = '" + objMdlMailCampaignResponse.image.src + "'," +
                    " updated_by = '" + user_gid + "'," +
                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where shopify_productid='" + values.shopify_productid + "'  ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = " update  pmr_mst_tproduct  set " +
                        " product_image = '" + objMdlMailCampaignResponse.image.src + "'," +
                        " updated_by = '" + user_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where shopify_productid='" + values.shopify_productid + "'  ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Product Image Updated Successfully !";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Product Image";
                        }
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Product Image";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Product Image";
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "*************Query****" + "Error occured while Update Shopify Product Image !! " + " *******" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Shopify/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }

    }
}
