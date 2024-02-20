using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.einvoice.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
//using System.Web;
//using OfficeOpenXml;
using System.Configuration;
using System.IO;
//using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text.RegularExpressions;

using System.Data.OleDb;

namespace ems.einvoice.DataAccess
{
    public class DaProduct
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, msdocument_gid, lsemployee_gid, msGETLOGGID, mcGetGID, msGetGID, mrGetGID, maGetGID, exclproducttypecode, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid, lsproduct_code;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5, importcount;
        int errorlog = 0;

        public void DaGetProductSummary(MdlProduct values)
        {
            try
            {

                msSQL = " SELECT d.producttype_name, b.productgroup_name, b.productgroup_code, a.product_gid, format(a.product_price,2) as product_price, format(a.cost_price,2) as cost_price, a.product_code, CONCAT_WS('|',a.product_name,a.size, a.width, a.length) as product_name, CONCAT(f.user_firstname,' ',f.user_lastname) as created_by, date_format(a.created_date,'%d-%m-%Y') as created_date, " +
                    " c.productuomclass_code, e.productuom_code, c.productuomclass_name, (case when a.stockable ='Y' then 'Yes' else 'No' end) as stockable, e.productuom_name, d.producttype_name as product_type, (case when a.status ='Y' then 'Active' else 'Inactive' end) as Status," +
                    " (case when a.serial_flag ='Y' then 'Yes' else 'No' end) as serial_flag, (case when a.avg_lead_time is null then '0 days' else concat(a.avg_lead_time,'  ', 'days') end) as lead_time from pmr_mst_tproduct a " +
                    " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid " +
                    " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid " +
                    " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid " +
                    " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid " +
                    " left join adm_mst_tuser f on f.user_gid = a.created_by order by a.created_date desc, a.product_gid desc";
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
                            Status = dt["Status"].ToString(),
                            producttype_name = dt["producttype_name"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            productgroup_code = dt["productgroup_code"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            cost_price = dt["cost_price"].ToString(),
                            productuomclass_code = dt["productuomclass_code"].ToString(),
                            productuom_code = dt["productuom_code"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            product_type = dt["product_type"].ToString(),

                        });
                        values.product_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding Product Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
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
                    string lspath = ConfigurationManager.AppSettings["exportexcelfile"] + "/assets/export" + "/" + lscompany_code + "/" + "Purchase/Product/Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                    //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/TestReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    {
                        if ((!System.IO.Directory.Exists(lspath)))
                            System.IO.Directory.CreateDirectory(lspath);
                    }

                    string lsname = "Product_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                    string lspath1 = ConfigurationManager.AppSettings["exportexcelfile"] + "/assets/export" + "/" + lscompany_code + "/" + "Purchase/Product/Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsname;

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
                values.message = "Exception occured while adding product Product report export!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
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
                values.message = "Exception occured while adding Product type!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
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
                values.message = "Exception occured while adding product group!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
        public void DaGethsngroupdropdown(MdlProduct values)
        {
            try
            {

                msSQL = " select hsngroup_code,concat(hsngroup_code,' || ', hsngroup_desc) as hsngroup_desc from rbl_mst_thsngroup ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Gethsngroupdropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Gethsngroupdropdown
                        {
                            hsngroup_code = dt["hsngroup_code"].ToString(),
                            hsngroup_desc = dt["hsngroup_desc"].ToString(),
                        });
                        values.Gethsngroupdropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {

                values.message = "Exception occured while adding  HSN group!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaGetOnChangehsngroup(string hsngroup_code, MdlProduct values)
        {
            try
            {

                msSQL = " select hsn_code, concat(hsn_code,' || ', hsn_desc) as hsn_desc from rbl_mst_thsnmaster where hsngroup_code='" + hsngroup_code + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Gethsngroupcodedropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Gethsngroupcodedropdown
                        {
                            hsn_code = dt["hsn_code"].ToString(),
                            hsn_desc = dt["hsn_desc"].ToString(),
                        });
                        values.Gethsngroupcodedropdown = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding change HSN group!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
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
                values.message = "Exception occured while adding product unit class!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
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
                values.message = "Exception occured while loading product unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
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
                values.message = "Exception occured while loading currency!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }


        public void DaPostProduct(string user_gid, product_list values)
        {
            try
            {

                string lsproduct_name = "";
                msSQL = " select product_code,product_name from pmr_mst_tproduct where product_code = '" + values.product_code + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows)
                {
                    lsproduct_code = objMySqlDataReader["product_code"].ToString();
                    lsproduct_name = objMySqlDataReader["product_name"].ToString();
                }

                if (lsproduct_code != values.product_code)

                {
                    if (lsproduct_name != values.product_name.Trim().Replace("'", ""))
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("PPTM");

                        msSQL = " SELECT hsn_code FROM rbl_mst_thsnmaster WHERE hsn_code='" + values.hsn + "' ";
                        string hsncode = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " SELECT hsn_desc FROM rbl_mst_thsnmaster WHERE hsn_code='" + values.hsn + "' ";
                        string hsndesc = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " insert into pmr_mst_tproduct (" +
                                " product_gid," +
                                " product_code," +
                                " product_name," +
                                " product_desc, " +
                                " productgroup_gid, " +
                                " productuomclass_gid, " +
                                " productuom_gid, " +
                                " currency_code," +
                                " product_price," +
                                " cost_price, " +
                                " stockable, " +
                                " status, " +
                                " hsn_number, " +
                                " hsn_desc, " +
                                " producttype_gid, " +
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
                            msSQL += "'" + values.product_name.Replace("'", "").Trim() + "',";
                        }
                        msSQL += "'" + values.product_desc + "'," +
                                 "'" + values.productgroup_name + "'," +
                                 "'" + values.productuomclass_name + "'," +
                                 "'" + values.productuom_name + "'," +
                                 "'" + "INR" + "'," +
                                 "'" + values.mrp.ToString().Replace("'", "") + "'," +
                                 "'" + values.cost_price.ToString().Replace("'", "") + "'," +
                                 "'" + "1" + "'," +
                                 "'" + "Y" + "'," +
                                 "'" + hsncode + "'," +
                                 "'" + (hsndesc).Replace("'", "\'") + "'," +
                                 "'" + values.producttype_name + "'," +
                                 "'" + user_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
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
                        values.message = "Product Name Already Exist !!";
                    }

                }

                else
                {
                    values.status = false;
                    values.message = "Product Code Already Exist !!";
                }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaeditProductSummary(string product_gid, MdlProduct values)
        {
            try
            {

                msSQL = " SELECT b.productgroup_gid,c.productuomclass_gid,a.product_gid,a.hsn_number,a.hsn_desc,d.producttype_gid,d.producttype_name,b.productgroup_name, a.product_code,a.product_name,a.product_desc, " +
                     " c.productuomclass_name, format(a.product_price,2) as product_price, format(a.cost_price,2) as cost_price,e.productuom_gid, e.productuom_name" +
                         " FROM pmr_mst_tproduct a " +
                     " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid " +
                     " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid " +
                     " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid " +
                     " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid  " +
                     " where a.product_gid ='" + product_gid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<editproductsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new editproductsummary_list
                        {

                            producttype_name = dt["producttype_name"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            mrp = dt["product_price"].ToString(),
                            cost_price = dt["cost_price"].ToString(),
                            product_desc = dt["product_desc"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            producttype_gid = dt["producttype_gid"].ToString(),
                            productuom_gid = dt["productuom_gid"].ToString(),
                            productuomclass_gid = dt["productuomclass_gid"].ToString(),
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                            hsn_code = dt["hsn_number"].ToString(),
                            hsn_desc = dt["hsn_desc"].ToString()



                        });
                        values.editproductsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading edit product summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaGetProductattributesSummary(string user_gid, string product_gid, MdlProduct values)
        {
            try
            {

                msSQL = " select c.product_gid,a.attribute_code, a.attribute_name,b.attribute_make,b.attribute_value from pmr_mst_tproductattributes a " +
                " left join pmr_mst_tattribute2product b on a.productattribute_gid=b.attribute_gid" +
                " left join pmr_mst_tproduct c on b.product_gid=c.product_gid" +
                        " where c.product_gid='" + product_gid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProductattributes_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProductattributes_list
                        {


                            attribute_code = dt["attribute_code"].ToString(),
                            attribute_name = dt["attribute_name"].ToString(),
                            attribute_make = dt["attribute_make"].ToString(),
                            attribute_value = dt["attribute_value"].ToString(),
                            product_gid = dt["product_gid"].ToString(),


                        });
                        values.GetProductattributes_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product Attribute!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        public void DaGetproductunitclassdropdownonchange(string user_gid, string productuomclass_gid, MdlProduct values)
        {
            try
            {

                msSQL = " select productuom_name,productuom_gid from pmr_mst_tproductuom a left join pmr_mst_tproductuomclass b on b.productuomclass_gid= a.productuomclass_gid  " +
                     " where b.productuomclass_gid ='" + productuomclass_gid + "' order by a.sequence_level  ";
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
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product unit class!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }

        public void DaGetproductunitclassdropdownonchangename(string user_gid, string productuomclass_name, MdlProduct values)
        {
            try
            {

                msSQL = " select productuom_name,productuom_gid from pmr_mst_tproductuom a left join pmr_mst_tproductuomclass b on b.productuomclass_gid= a.productuomclass_gid  " +
                     " where b.productuomclass_name ='" + productuomclass_name + "' order by a.sequence_level  ";
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
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading product unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }
        public void DaUpdatedProduct(string user_gid, product_list values)
        {
            try
            {

                msSQL = "select product_gid from pmr_mst_tproduct where" +
                    "product_name='" + values.product_name.Trim().Replace("'", "") + "'";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == false)
                {
                    msSQL = " update  pmr_mst_tproduct  set " +
                            " product_name = '" + values.product_name.Trim().Replace("'", "") + "'," +
                            " product_code = '" + values.product_code + "'," +
                            " product_desc = '" + values.product_desc + "'," +
                            " productgroup_gid = '" + values.productgroup_name + "'," +
                            " producttype_gid = '" + values.producttype_name + "'," +
                            " productuomclass_gid = '" + values.productuomclass_name + "'," +
                            " productuom_gid = '" + values.productuom_name + "'," +
                            " product_price = '" + values.mrp.ToString().Replace(",", "") + "'," +
                            " cost_price = '" + values.cost_price.ToString().Replace(",", "") + "'," +
                            " hsn_number = '" + values.hsn_code + "'," +
                            " hsn_desc = '" + values.hsn_desc + "'," +
                            " updated_by = '" + user_gid + "'," +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                            "' where product_gid='" + values.product_gid + "'  ";
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
                    values.message = "Product Name Already Exist";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading update product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaUpdatedProductcost(string user_gid, product_list values)
        {
            try
            {

                msSQL = " update  pmr_mst_tproduct  set " +
              " cost_price = '" + values.cost_price + "'" +
              "  where product_gid='" + values.product_gid + "'  ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    values.status = true;
                    values.message = "Product Cost Updated Successfully";

                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Product Cost";
                }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding Product cost!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
         
        }

        public void DadeleteProductSummary(string product_gid, product_list values)
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
                values.message = "Exception occured while deleting Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaenquiryProductSummary(string user_gid, string product_gid, product_list values)
        {
            try
            {


                msSQL = " Select product_gid from crm_trn_tcostpricerequest WHERE product_gid='" + product_gid + "' ";
                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                if (objMySqlDataReader.HasRows == true)
                {
                    values.status = false;
                    values.message = "Already Request Raised For Enquiry";
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CSPR");



                    msSQL = " insert into crm_trn_tcostpricerequest (" +
                  " costpricerequest_gid, " +
                  " product_gid, " +
                  " request_flag, " +
                  " requested_by, " +
                  " requested_date " +
                  " )values ( " +
                  "'" + msGetGid + "'," +
                  "'" + product_gid + "'," +
                  "' Requested '," +
                  "'" + user_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Request Raised For Enquiry";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Request Raised For Enquiry";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while inserting raise product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }

        public void DaEinvoiceProductImportExcel(HttpRequest httpRequest, string user_gid, result productresult, product_list values)
        {
            string lscompany_code;
            try
            {
                HttpFileCollection httpFileCollection;
                string lspath, lsfilePath;

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = ConfigurationManager.AppSettings["importexcelfile1"] + lscompany_code + "/" + " Import_Excel/Sales_Module/" + "Product_Master/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

                if (!Directory.Exists(lsfilePath))
                    Directory.CreateDirectory(lsfilePath);

                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
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
                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    string status;
                    status = objcmnfunctions.uploadFile(lsfilePath, FileExtension);
                    file.Close();
                    ms.Close();

                    msSQL = " insert into pmr_tmp_tproductuploadexcellog(" +
                                        " uploadexcellog_gid," +
                                        " fileextenssion," +
                                        " uploaded_by, " +
                                        " uploaded_date)" +
                                        " values(" +
                                        " '" + msdocument_gid + "'," +
                                        " '" + FileExtension + "'," +
                                        "'" + user_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                catch (Exception ex)
                {
                    productresult.status = false;
                    productresult.message = ex.ToString();
                    return;
                }

                //Excel To DataTable
                try
                {
                    DataTable dataTable = new DataTable();
                    int totalSheet = 1;
                    string connectionString = string.Empty;
                    string fileExtension = Path.GetExtension(lspath);

                    lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                    string correctedPath = Regex.Replace(lsfilePath, @"\\+", @"\");

                    try
                    {
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + correctedPath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0';";
                    }
                    catch (Exception ex)
                    {

                    }

                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();
                        DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                        if (schemaTable != null)
                        {
                            var tempDataTable = (from dataRow in schemaTable.AsEnumerable()
                                                 where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                                 select dataRow).CopyToDataTable();

                            schemaTable = tempDataTable;
                            totalSheet = schemaTable.Rows.Count;
                            using (OleDbCommand command = new OleDbCommand())
                            {
                                command.Connection = connection;
                                command.CommandText = "select * from [Sheet1$]";

                                using (OleDbDataReader reader = command.ExecuteReader())
                                {
                                    dataTable.Load(reader);
                                }

                                importcount = 0;

                                foreach (DataRow dt_product in dataTable.Rows)
                                {
                                    string exproducttype = dt_product["PRODUCT TYPE"].ToString();
                                    string exproductgroup = dt_product["PRODUCT GROUP"].ToString();
                                    string exproductcode = dt_product["PRODUCT CODE"].ToString();
                                    string exproducthsncode = dt_product["HSN CODE"].ToString();
                                    string exproduct = dt_product["PRODUCT"].ToString();
                                    string exproductdescription = dt_product["PRODUCT DESCRIPTION"].ToString();
                                    string exproductunit = dt_product["UNITS"].ToString();
                                    string exproductserialno = dt_product["SERIAL NUMBER TRACKER"].ToString();
                                    string exproductwarrenty = dt_product["WARRANTY TRACKER"].ToString();
                                    string exproductExpirydate = dt_product["EXPIRY DATE TRACKER"].ToString();
                                    string exproductcostprice = dt_product["COST PRICE"].ToString();
                                    string lsstatus = "1", lsserialflag = "", lsserialtracking_flag = "", lsstockflag = "";
                                    msSQL = " Select producttype_gid from pmr_mst_tproducttype " +
                                            " where producttype_name = '" + exproducttype.Trim() + "'";
                                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                    if (objMySqlDataReader.HasRows == true)
                                    {
                                        objMySqlDataReader.Read();
                                        exclproducttypecode = objMySqlDataReader["producttype_gid"].ToString();
                                        objMySqlDataReader.Close();
                                    }
                                    msSQL = " Select productgroup_gid from pmr_mst_tproductgroup " +
                                            " where productgroup_name = '" + exproductgroup.Trim() + "'";
                                    objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                    if (objMySqlDataReader.HasRows == true)
                                    {
                                        objMySqlDataReader.Read();
                                        mcGetGID = objMySqlDataReader["productgroup_gid"].ToString();
                                        objMySqlDataReader.Close();
                                    }
                                    else
                                    {
                                        msGETLOGGID = objcmnfunctions.GetMasterGID("MPEL");
                                        msSQL = " insert into pmr_tmp_tproducttemplog ( " +
                                                            " producttemplog_gid, " +
                                                            " uploadexcellog_gid, " +
                                                            " productgroup," +
                                                            " product_name, " +
                                                            " remarks" +
                                                            " ) values ( " +
                                                            "'" + msGETLOGGID + "'," +
                                                            "'" + msdocument_gid + "'," +
                                                            "'" + exproductgroup.Trim() + "'" +
                                                            "'" + exproduct.Trim() + "'," +
                                                            "' This Product Group not found ')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        errorlog++;
                                    }
                                    if (errorlog == 0)
                                    {
                                        msSQL = " insert into pmr_mst_tproductgroup (" +
                                                " productgroup_gid, " +
                                                " productgroup_code, " +
                                                " productgroup_name) " +
                                                " values (" +
                                                "'" + mcGetGID + "', " +
                                                "'" + exproductgroup.Trim() + "', " +
                                                "'" + exproductgroup.Trim() + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    }

                                    {
                                        msSQL = " Select productuomclass_gid from pmr_mst_tproductuomclass " +
                                                " where productuomclass_name = '" + exproductunit.Trim() + "'";
                                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                        if (objMySqlDataReader.HasRows == true)
                                        {
                                            objMySqlDataReader.Read();
                                            maGetGID = objMySqlDataReader["productuomclass_gid"].ToString();
                                            objMySqlDataReader.Close();
                                        }
                                        else
                                        {
                                            msGETLOGGID = objcmnfunctions.GetMasterGID("MPEL");
                                            msSQL = " insert into pmr_tmp_tproducttemplog ( " +
                                                                " producttemplog_gid, " +
                                                                " uploadexcellog_gid, " +
                                                                " productuomclass_name, " +
                                                                " product_name, " +
                                                                " remarks" +
                                                                " ) values ( " +
                                                                "'" + msGETLOGGID + "'," +
                                                                "'" + msdocument_gid + "'," +
                                                                "'" + exproductunit.Trim() + "'," +
                                                                "'" + exproduct.Trim() + "'," +
                                                                "' This Product Unit not found ')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                            errorlog++;
                                        }
                                        if (errorlog == 0)
                                        {
                                            msSQL = " insert into pmr_mst_tproductuomclass (" +
                                                " productuomclass_gid, " +
                                                " productuomclass_code, " +
                                                " productuomclass_name) " +
                                                " values(" +
                                                "'" + maGetGID + "', " +
                                                "'" + exproductunit.Trim() + "', " +
                                                "'" + exproductunit.Trim() + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }

                                        {
                                            msSQL = " Select productuom_gid from pmr_mst_tproductuom " +
                                                    " where productuom_name = '" + exproductunit.Trim() + "'";
                                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                            if (objMySqlDataReader.HasRows == true)
                                            {
                                                objMySqlDataReader.Read();
                                                msGetGID = objMySqlDataReader["productuom_gid"].ToString();
                                                objMySqlDataReader.Close();
                                            }
                                            else
                                            {
                                                msGETLOGGID = objcmnfunctions.GetMasterGID("MPEL");
                                                msSQL = " insert into pmr_tmp_tproducttemplog ( " +
                                                                    " producttemplog_gid, " +
                                                                    " uploadexcellog_gid, " +
                                                                    " productuom_name, " +
                                                                    " product_name, " +
                                                                    " remarks" +
                                                                    " ) values ( " +
                                                                    "'" + msGETLOGGID + "'," +
                                                                    "'" + msdocument_gid + "'," +
                                                                    "'" + exproductunit.Trim() + "'," +
                                                                    "'" + exproduct.Trim() + "'," +
                                                                    "' This Product Unit not found ')";
                                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                errorlog++;
                                            }
                                            if (errorlog == 0)
                                            {
                                                msSQL = " insert into pmr_mst_tproductuom (" +
                                                      " productuom_gid, " +
                                                      " productuom_code, " +
                                                      " productuom_name, " +
                                                      " productuomclass_gid) " +
                                                      " values(" +
                                                      "'" + msGetGID + "', " +
                                                      "'" + exproductunit.Trim() + "', " +
                                                      "'" + exproductunit.Trim() + "', " +
                                                      "'" + maGetGID + "')";
                                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                            }

                                            {
                                                msSQL = " Select product_gid from pmr_mst_tproduct " +
                                                       " where product_code = '" + exproductcode.Trim() + "'";
                                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                                if (objMySqlDataReader.HasRows == true)
                                                {
                                                    msGETLOGGID = objcmnfunctions.GetMasterGID("MPEL");
                                                    msSQL = " insert into pmr_tmp_tproducttemplog ( " +
                                                            " producttemplog_gid, " +
                                                            " uploadexcellog_gid, " +
                                                            " product_code, " +
                                                            " product_name, " +
                                                            " remarks" +
                                                            " ) values ( " +
                                                            "'" + msGETLOGGID + "'," +
                                                            "'" + msdocument_gid + "'," +
                                                            "'" + exproductcode.Trim() + "'," +
                                                            "'" + exproduct.Trim() + "'," +
                                                            "' This Product Code is already exist ')";
                                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                    errorlog++;
                                                    objMySqlDataReader.Close();
                                                }
                                            }

                                            {
                                                msSQL = " Select product_gid from pmr_mst_tproduct " +
                                                        " where product_name = '" + exproduct.Trim() + "'";
                                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                                if (objMySqlDataReader.HasRows == true)
                                                {
                                                    msGETLOGGID = objcmnfunctions.GetMasterGID("MPEL");
                                                    msSQL = " insert into pmr_tmp_tproducttemplog ( " +
                                                            " producttemplog_gid, " +
                                                            " uploadexcellog_gid, " +
                                                            " product_code, " +
                                                            " product_name, " +
                                                            " remarks" +
                                                            " ) values ( " +
                                                            "'" + msGETLOGGID + "'," +
                                                            "'" + msdocument_gid + "'," +
                                                            "'" + exproductcode.Trim() + "'," +
                                                            "'" + exproduct.Trim() + "'," +
                                                            "' This Product is already exist ')";
                                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                    errorlog++;
                                                    objMySqlDataReader.Close();
                                                }
                                                else
                                                {
                                                    mrGetGID = objcmnfunctions.GetMasterGID("PPTM");
                                                }
                                                if (errorlog == 0)
                                                {
                                                    msSQL = " insert into pmr_mst_tproduct (" +
                                                            " product_gid, " +
                                                            " productgroup_gid, " +
                                                            " productuom_gid, " +
                                                            " productuomclass_gid, " +
                                                            " product_code, " +
                                                            " product_name, " +
                                                            " producttype_gid, " +
                                                            " status, " +
                                                            " cost_price, " +
                                                            " serial_flag, " +
                                                            " serialtracking_flag, " +
                                                            " warrentytracking_flag, " +
                                                            " expirytracking_flag, " +
                                                            " created_by, " +
                                                            " created_date) " +
                                                            " values (" +
                                                            "'" + mrGetGID + "', " +
                                                            "'" + mcGetGID + "', " +
                                                            "'" + msGetGID + "'," +
                                                            "'" + maGetGID + "'," +
                                                            "'" + exproductcode.Trim() + "', " +
                                                            "'" + exproduct.Trim() + "', " +
                                                            "'" + exclproducttypecode + "', " +
                                                            "'" + lsstatus + "', " +
                                                            "'" + exproductcostprice.Trim() + "', " +
                                                            "'" + lsserialflag + "', " +
                                                            "'" + lsserialtracking_flag + "', " +
                                                            "'" + exproductwarrenty.Trim() + "', " +
                                                            "'" + exproductwarrenty.Trim() + "', " +
                                                            "'" + user_gid + "'," +
                                                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                    if (mnResult == 1)
                                                    {
                                                        importcount++;
                                                    }
                                                }
                                                else
                                                {
                                                    values.status = false;
                                                    values.message = "Product Name already exist";
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    productresult.status = false;
                    productresult.message = ex.ToString();
                    return;
                }
            }
            catch (Exception ex)
            {
                productresult.status = false;
                productresult.message = ex.ToString();
            }

            msSQL = " update  pmr_tmp_tproductuploadexcellog set " +
                   " importcount = " + importcount + " " +
                   " where uploadexcellog_gid='" + msdocument_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (importcount == 0)
            {
                productresult.status = false;
                productresult.message = "No product data has been imported so Please check the error log.";
            }
            else
            {
                productresult.status = true;
                productresult.message = importcount + "  product data has been imported";
            }

        }

        public void DaGetProductDocumentlist(MdlProduct values)
        {
            try
            {

                msSQL = " select a.uploadexcellog_gid,concat(b.user_firstname, b.user_lastname) as updated_by," +
                  " a.uploaded_date,a.importcount from pmr_tmp_tproductuploadexcellog a " +
                  " left join adm_mst_tuser b on b.user_gid = a.uploaded_by";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<productdocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new productdocument_list
                        {
                            productdocument_name = dt["uploadexcellog_gid"].ToString(),
                            updated_by = dt["updated_by"].ToString(),
                            uploaded_date = dt["uploaded_date"].ToString(),
                            importcount = dt["importcount"].ToString(),
                        });
                        values.productdocument_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding Product Document!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
          
        }

        public void DaGetProductDocumentDtllist(string document_gid, MdlProduct values)
        {
            try
            {

                msSQL = " select product_code,product_name,remarks from pmr_tmp_tproducttemplog " +
                   " where uploadexcellog_gid = '" + document_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<productdocumentdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new productdocumentdtl_list
                        {
                            remarks = dt["remarks"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                        });
                        values.productdocumentdtl_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product document detail!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
"***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/rbl/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
    }
}