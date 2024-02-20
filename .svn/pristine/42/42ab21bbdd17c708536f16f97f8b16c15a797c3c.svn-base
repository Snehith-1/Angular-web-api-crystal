using ems.pmr.Models;
using ems.pmr.DataAccess;
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
using System.Data.OleDb;

using System.Text.RegularExpressions;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;

namespace ems.pmr.DataAccess
{
    public class DaPmrMstProduct
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string producttype_gid;

        string msEmployeeGID, lsemployee_gid, lsuser_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid, status, mcGetGID, productgroup_code, msGetGID, maGetGID, productuomclass_code, mrGetGID;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetProductSummary(MdlPmrMstProduct values)
        {
            try
            {
               
                msSQL = " SELECT d.producttype_name,b.productgroup_name,b.productgroup_code,a.product_gid, a.product_price, a.cost_price, a.product_code, CONCAT_WS('|',a.product_name,a.size, a.width, a.length) as product_name,  CONCAT(f.user_firstname,' ',f.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date, " +
                    " c.productuomclass_code,e.productuom_code,c.productuomclass_name,(case when a.stockable ='Y' then 'Yes' else 'No ' end) as stockable,e.productuom_name,d.producttype_name as product_type,(case when a.status ='1' then 'Active' else 'Inactive' end) as Status," +
                    " (case when a.serial_flag ='Y' then 'Yes' else 'No' end)as serial_flag,(case when a.avg_lead_time is null then '0 days' else concat(a.avg_lead_time,'  ', 'days') end)as lead_time  from pmr_mst_tproduct a " +
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
                values.message = "Exception occured while Getting Product Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaPostProduct(string user_gid, product_list values)
        {

            try
            {

                msGetGid = objcmnfunctions.GetMasterGID("PPTM");
                msSQL = " SELECT currency_code FROM crm_trn_tcurrencyexchange WHERE currencyexchange_gid='" + values.currency_code + "' ";
                string lscurrency_code = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " insert into pmr_mst_tproduct (" +
                        " product_gid," +
                        " product_code," +
                        " product_name," +
                    " product_desc, " +
                    " productgroup_gid, " +
                    " productuomclass_gid, " +
                    " productuom_gid, " +
                    " currency_code," +
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
                        "'" + values.product_code + "',";
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
                         "'" + values.productuom_name + "'," +
                         "'" + lscurrency_code + "'," +
                         "'" + values.mrp_price + "'," +
                         "'" + values.cost_price + "'," +
                         "'" + values.avg_lead_time + "'," +
                         "'" + "Y" + "'," +
                         "'" + "1" + "'," +
                         "'" + values.producttype_name + "'," +
                         "'" + values.purchasewarrenty_flag + "'," +
                         "'" + values.expirytracking_flag + "'," +
                         "'" + values.batch_flag + "'," +
                         "'" + values.serial_flag + "'," +
                         "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Product Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Please Enter All Mandatory Fields";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Adding Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }
        public void DaPmrMstProductUpdate(string user_gid, product_list values)
        {
            try
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
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            


        }
        public void DaGetProducttype(MdlPmrMstProduct values)
        {
            try
            {
               
                msSQL = " Select producttype_name,producttype_gid  " +
                    " from pmr_mst_tproducttype where producttype_name like 'trad%' " +
                    " or producttype_name like 'Service%' " +
                   // " or producttype_name like 'sem%' " +
                   " or producttype_name like 'fin%' " +
                    " or producttype_name like 'consum%'  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProducttype>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProducttype
                        {
                            producttype_name = dt["producttype_name"].ToString(),
                            producttype_gid = dt["producttype_gid"].ToString(),
                        });
                        values.GetProducttype = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting Product type!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaGetProductGroup(MdlPmrMstProduct values)
        {
            try
            {
              
                msSQL = " Select productgroup_gid, productgroup_name from pmr_mst_tproductgroup  " +
                    " order by productgroup_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProductGroup>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProductGroup
                        {
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                        });
                        values.GetProductGroup = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Product group!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetProductUnitclass(MdlPmrMstProduct values)
        {
            try
            {
               
                msSQL = " Select productuomclass_gid, productuomclass_code, productuomclass_name  " +
                " from pmr_mst_tproductuomclass order by productuomclass_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProductUnitclass>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProductUnitclass
                        {
                            productuomclass_gid = dt["productuomclass_gid"].ToString(),
                            productuomclass_code = dt["productuomclass_code"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                        });
                        values.GetProductUnitclass = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Product unit class!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetProductUnit(MdlPmrMstProduct values)
        {
            try
            {
               
                msSQL = " select productuom_name,productuom_gid from pmr_mst_tproductuom a left join pmr_mst_tproductuomclass b on b.productuomclass_gid= a.productuomclass_gid  " +
            " order by a.sequence_level ";
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
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Product Unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaGetOnChangeProductUnitClass(string productuomclass_gid, MdlPmrMstProduct values)
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
                values.message = "Exception occured while changing Product unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetDeleteProductdetails(string product_gid, product_list values)
        {
            try
            {
               
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
                values.message = "Exception occured while deleting Product Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        public void DaGetViewProductSummary(string product_gid, MdlPmrMstProduct values)
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
                values.message = "Exception occured while viewing Product Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetEditProductSummary(string product_gid, MdlPmrMstProduct values)
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
                values.message = "Exception occured while updating Product Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaproductImportExcel(HttpRequest httpRequest, string user_gid, result objResult, product_list values)
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
                    FileInfo fileinfo = new FileInfo(lsfilePath);
                    Stream ls_readStream;
                    ls_readStream = httpPostedFile.InputStream;
                    MemoryStream ms = new MemoryStream();
                    ls_readStream.CopyTo(ms);

                    //path creation        
                    lspath = lsfilePath + "/";
                    FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                    ms.WriteTo(file);
                    bool status1;


                    status1 = objcmnfunctions.UploadStream(ConfigurationManager.AppSettings["blob_containername"], lscompany_code + "/" + "Lead Import/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, FileExtension, ms);
                    ms.Close();

                    // Connect to the storage account
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"].ToString());
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(ConfigurationManager.AppSettings["blob_containername"].ToString());

                    // Get a reference to the blob
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(lscompany_code + "/" + "Lead Import/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension);
                    string path_url = lscompany_code + "/" + "Lead Import/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;


                    // Download the blob's contents and read Excel file
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // await blockBlob.DownloadToStreamAsync(memoryStream);

                        blockBlob.DownloadToStream(memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.Position = 0;
                        // Load Excel package from the memory stream
                        //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (ExcelPackage package = new ExcelPackage(memoryStream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"]; // worksheet name
                                                                                              // Remove the first row
                            worksheet.DeleteRow(1);

                            // Convert Excel data to array list format
                            List<List<string>> excelData = new List<List<string>>();

                            for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
                            {
                                List<string> rowData = new List<string>();
                                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                                {
                                    var cellValue = worksheet.Cells[row, col].Value?.ToString();
                                    rowData.Add(cellValue);
                                }

                                string producttype_name = rowData[0];
                                string productgroup_name = rowData[1];
                                string product_code = rowData[2];
                                string hsn_code = rowData[3];
                                string product_name = rowData[4];
                                string product_des = rowData[5];
                                string productuomclass_name = rowData[6];
                                string serialtracking_flag = rowData[7];
                                string warrentytracking_flag = rowData[8];
                                string expirytracking_flag = rowData[9];
                                string cost_price = rowData[10];
                                //string country_name = rowData[11];
                                //string source_name = rowData[12];
                                //string customer_type = rowData[13];

                                msSQL = "Select producttype_gid from pmr_mst_tproducttype " +
                                        "where producttype_name = '" + producttype_name + "'";
                                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                                if (objOdbcDataReader.HasRows == true)
                                {
                                    objOdbcDataReader.Read();
                                    producttype_gid = objOdbcDataReader["producttype_gid"].ToString();
                                    objOdbcDataReader.Close();
                                }
                                msSQL = " Select productgroup_gid , productgroup_code from pmr_mst_tproductgroup " +
                                " where productgroup_name = '" + productgroup_name + "'";
                                objOdbcDataReader = objdbconn.GetDataReader(msSQL);

                                if (objOdbcDataReader.HasRows == true)
                                {
                                    objOdbcDataReader.Read();
                                    mcGetGID = objOdbcDataReader["productgroup_gid"].ToString();
                                    productgroup_code = objOdbcDataReader["productgroup_code"].ToString();
                                    objOdbcDataReader.Close();
                                }
                                else
                                {
                                    mcGetGID = objcmnfunctions.GetMasterGID("PPGM");
                                    productgroup_code = objcmnfunctions.GetMasterGID("PPGM");

                                    msSQL = "insert into pmr_mst_tproductgroup (" +
                                                "productgroup_gid, " +
                                                "productgroup_code, " +
                                                "productgroup_name) " +
                                                "values (" +
                                                "'" + mcGetGID + "', " +
                                                "'" + productgroup_code + "', " +
                                                "'" + productgroup_name + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                }
                                msSQL = " Select productuomclass_gid from pmr_mst_tproductuomclass " +
                                        " where productuomclass_name = '" + productuomclass_name + "'";
                                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                                if (objOdbcDataReader.HasRows == true)
                                {
                                    objOdbcDataReader.Read();
                                    maGetGID = objOdbcDataReader["productuomclass_gid"].ToString();
                                    productuomclass_code = objOdbcDataReader["productuomclass_gid"].ToString();

                                    objOdbcDataReader.Close();
                                }
                                else
                                {
                                    maGetGID = objcmnfunctions.GetMasterGID("PUCM");
                                    productuomclass_code = objcmnfunctions.GetMasterGID("PUCM");

                                    msSQL = "insert into pmr_mst_tproductuomclass (" +
                                            "productuomclass_gid, " +
                                            "productuomclass_code, " +
                                            "productuomclass_name) " +
                                            "values(" +
                                            "'" + maGetGID + "', " +
                                            "'" + productuomclass_code + "', " +
                                            "'" + productuomclass_name + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }

                                msSQL = " Select productuom_gid from pmr_mst_tproductuom " +
                                        " where productuom_name = '" + productuomclass_name + "'";
                                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                                if (objOdbcDataReader.HasRows == true)
                                {
                                    objOdbcDataReader.Read();
                                    msGetGID = objOdbcDataReader["productuom_gid"].ToString();
                                    objOdbcDataReader.Close();
                                }
                                else
                                {
                                    msGetGID = objcmnfunctions.GetMasterGID("PPMM");

                                    msSQL = "insert into pmr_mst_tproductuom (" +
                                            "productuom_gid, " +
                                            "productuom_code, " +
                                            "productuom_name, " +
                                            "productuomclass_gid) " +
                                            "values(" +
                                            "'" + msGetGID + "', " +
                                            "'" + productuomclass_name + "', " +
                                            "'" + productuomclass_name + "', " +
                                            "'" + maGetGID + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }

                                //insertion in table

                                //msSQL = " Select product_gid from pmr_mst_tproduct " +
                                //               " where product_code =  '" + exproductcode + "' and product_name = '" + exproduct + "'";
                                //objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                                //if (objOdbcDataReader.HasRows == true)
                                //{
                                //    objOdbcDataReader.Close();
                                //}
                                //else
                                //{
                                //    mrGetGID = objcmnfunctions.GetMasterGID("PPTM");
                                //}
                                mrGetGID = objcmnfunctions.GetMasterGID("PPTM");

                                msSQL = "insert into pmr_mst_tproduct (" +
                                       "product_gid, " +
                                       "productgroup_gid, " +
                                       "productuom_gid, " +
                                       "productuomclass_gid, " +
                                       "product_code, " +
                                       "product_name, " +
                                       "producttype_gid, " +
                                       "status, " +
                                       "cost_price, " +
                                       "serial_flag, " +
                                       "serialtracking_flag, " +
                                       "warrentytracking_flag, " +
                                       "expirytracking_flag, " +
                                       " created_by, " +
                                       " created_date) " +
                                       "values (" +
                                       "'" + mrGetGID + "', " +
                                       "'" + mcGetGID + "', " +
                                       "'" + msGetGID + "'," +
                                       "'" + maGetGID + "'," +
                                       "'" + product_code + "', " +
                                       "'" + product_name + "', " +
                                       "'" + producttype_gid + "', " +
                                       "'1', " +
                                       "'" + cost_price + "', " +
                                       "' Y ', " +
                                       "'" + serialtracking_flag + "', " +
                                       "'" + warrentytracking_flag + "', " +
                                       "'" + expirytracking_flag + "', " +
                                       "'" + user_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);




                                if (mnResult != 0)
                                {
                                    objResult.status = true;
                                    values.message = "Product Imported Successfully";
                                }
                                else
                                {
                                    objResult.status = false;
                                    values.message = "Error While Adding Product";
                                }

                            }
                        }

                    }
                }

                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.Message.ToString();
                }
                if (mnResult != 0)
                {
                    objResult.status = true;
                    values.message = "Product Imported Successfully";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding Product template";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

    }
}
        














