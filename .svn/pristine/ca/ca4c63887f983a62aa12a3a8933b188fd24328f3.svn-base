

using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.pmr.Models;
using System.Reflection;

using System.Globalization;

namespace ems.pmr.DataAccess
{
    public class DaPmrTrnRaiseEnquiry
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msPOGetGID, msGetGID, msGetGid, msGetGid1;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5, lsenquiry_type, product_requireddate;

        public void DaGetProductGrp(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 
                msSQL = " select distinct(a.productgroup_gid),b.productgroup_name " +
                " from pmr_mst_tproduct a," +
                " pmr_mst_tproductgroup b where a.productgroup_gid=b.productgroup_gid  and b.delete_flag='N' " +
                " order by b.productgroup_name ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProductGrp>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProductGrp
                        {
                            productgroup_name = dt["productgroup_name"].ToString(),
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                        });
                        values.GetProductGrp = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting Product group!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaGetProducts(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 
                msSQL = "select product_gid,product_name from pmr_mst_tproduct" +
            " where product_name = product_name  and delete_flag='N'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProducts>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProducts
                        {
                            product_name = dt["product_name"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                        });
                        values.GetProducts = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetProductunit(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                msSQL = " Select a.productuom_gid as uom_gid, a.productuom_name " +
       " from pmr_mst_tproductuom a " +
       " where a.delete_flag='N' and a.productuomclass_gid in (select productuomclass_gid from pmr_mst_tproduct where delete_flag='N' ) ";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProductUnits>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProductUnits
                        {
                            productuom_name = dt["productuom_name"].ToString(),
                            uom_gid = dt["uom_gid"].ToString(),
                        });
                        values.GetProductUnits = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting product unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaGetOnChangeProductsName(string product_gid, MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 

                if (product_gid != null)
                {
                    msSQL = " Select a.product_name, a.product_code, b.productuom_gid,b.productuom_name,c.productgroup_name,c.productgroup_gid,a.productuom_gid  from pmr_mst_tproduct a  " +
                         " left join pmr_mst_tproductuom b on a.productuom_gid = b.productuom_gid  " +
                        " left join pmr_mst_tproductgroup c on a.productgroup_gid = c.productgroup_gid  " +
                    " where a.product_gid='" + product_gid + "' ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    var getModuleList = new List<GetProductsName>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getModuleList.Add(new GetProductsName
                            {
                                product_name = dt["product_name"].ToString(),
                                product_code = dt["product_code"].ToString(),
                                productuom_name = dt["productuom_name"].ToString(),
                                productgroup_name = dt["productgroup_name"].ToString(),
                                productuom_gid = dt["productuom_gid"].ToString(),
                                productgroup_gid = dt["productgroup_gid"].ToString(),

                            });
                            values.GetProductsName = getModuleList;
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured whileon changing product name!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetDirectEnquiryEditProductSummary(string tmpsalesenquiry_gid, MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 
                msSQL = " Select a.tmpsalesenquiry_gid,a.enquiry_gid,a.product_gid,a.qty_requested,a.uom_gid,a.productgroup_gid,a.potential_value," +
    " DATE_FORMAT(a.product_requireddate, '%d/%m/%Y') AS product_requireddate," +
    " c.product_name, c.product_code,b.productgroup_name,d.productuom_name from pmr_tmp_tvendorenquiry a" +
    " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid " +
    " left join pmr_mst_tproduct c on a.product_gid = c.product_gid" +
    " left join pmr_mst_tproductuom d on a.uom_gid = c.productuom_gid " +
    " where a.tmpsalesenquiry_gid = '" + tmpsalesenquiry_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<DirecteditenquiryList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new DirecteditenquiryList
                        {
                            tmpsalesenquiry_gid = dt["tmpsalesenquiry_gid"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                            qty_requested = dt["qty_requested"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            potential_value = dt["potential_value"].ToString(),
                            product_requireddate = dt["product_requireddate"].ToString(),

                        });
                        values.directeditenquiry_list = getModuleList;
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while direct enquiry product summary edit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             
        }
        public void DaPostUpdateEnquiryProduct(string employee_gid, productsummarys_list values)
        {
            try
            {
                


                msSQL = "Select product_gid from pmr_mst_tproduct where product_name='" + values.product_name + "'";
                string lsproductgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "Select productgroup_gid from pmr_mst_tproductgroup where productgroup_name='" + values.productgroup_name + "'";
                string lsproductgroupgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "Select productuom_gid from pmr_mst_tproductuom where productuom_name='" + values.productuom_name + "'";
                string lsproductuomgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update smr_tmp_tsalesenquiry set " +
                        " productgroup_gid = '" + lsproductgroupgid + "', " +
                        " potential_value ='" + values.potential_value + "', " +
                        " qty_requested='" + values.qty_requested + "', " +
                        " uom_gid='" + lsproductuomgid + "'," +
                        " product_gid='" + lsproductgid + "'," +
                        " product_requireddate = '" + values.product_requireddate + "'," +
                        " created_by= '" + employee_gid + "'," +
                        " enquiry_type= '" + lsenquiry_type + "'," +
                        " where tmpsalesenquiry_gid='" + values.tmpsalesenquiry_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select qty_requested,display_field,customerproduct_code,potential_value,tmpsalesenquiry_gid " +
                       " from smr_tmp_tsalesenquiry where " +
                       " product_gid = '" + lsproductgid + "' and " +
                       " uom_gid='" + lsproductuomgid + "' and  " +
                       " created_by = '" + employee_gid + "' and" +
                       " enquiry_type='" + lsenquiry_type + "' and product_requireddate='" + (product_requireddate, "yyyy-MM-dd") + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count >= 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string lsqtyrequested = dt["qty_requested"].ToString();
                        string lspotentialvalue = dt["potential_value"].ToString();

                        msSQL = " update smr_tmp_tsalesenquiry set " +
                                " qty_requested='" + lsqtyrequested + "', " +
                                " potential_value ='" + lspotentialvalue + "' " +
                                " where tmpsalesenquiry_gid='" + values.tmpsalesenquiry_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = " Product Updated Successfully ";
                }
                else
                {
                    values.status = false;
                    values.message = " Error While Updating Product Details ";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Product Details in purchase enquiry";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }


        public void DaPostOnAdds(string user_gid, productsummarys_list values)
        {
            try
            {
                 
                msGetGid = objcmnfunctions.GetMasterGID("PPVP");
                msGetGid1 = objcmnfunctions.GetMasterGID("PPTM");


                msSQL = "select product_gid from pmr_mst_tproduct where product_name='" + values.product_name + "'";
                string lsproductgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select productuom_gid from pmr_mst_tproductuom where productuom_name='" + values.productuom_name + "'";
                string lsproductuomgid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "Select productgroup_gid from pmr_mst_tproductgroup where productgroup_name='" + values.productgroup_name + "'";
                string lsproductgroupgid = objdbconn.GetExecuteScalar(msSQL);
                DateTime? dcDate = null;
                //if (!string.IsNullOrWhiteSpace(values.product_requireddate) && DateTime.TryParse(values.product_requireddate, out DateTime parsedDcDate))
                //{
                //    dcDate = parsedDcDate;
                //}
                //else
                //{
                //    Console.WriteLine("Error parsing dc_date: Invalid format or empty");
                //    dcDate = DateTime.MinValue;
                //}
                string uiDateStr = values.product_requireddate;
                DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string product_requireddate = uiDate.ToString("yyyy-MM-dd");

                msSQL = " insert into pmr_tmp_tvendorenquiry( " +
                        " tmpsalesenquiry_gid , " +
                        " productgroup_gid, " +
                        " vendorproduct_code," +
                        " product_gid, " +
                        " potential_value," +
                        " uom_gid," +
                        " qty_requested, " +
                        " user_gid, " +
                        " enquiry_type," +
                        " product_requireddate, " +
                        " product_requireddateremarks," +
                        " display_field) " +
                        " values( " +
                         "'" + msGetGid + "'," +
                        "'" + lsproductgroupgid + "'," +
                        "'" + values.vendorproduct_code + "'," +
                        "'" + lsproductgid + "'," +
                        "'" + values.potential_value + "'," +
                        "'" + lsproductuomgid + "'," +
                        "'" + values.qty_requested + "', " +
                        "'" + user_gid + "', " +
                        "'" + values.enquiry_type + "', " +
                        "'" + product_requireddate + "', " +
                        "'" + values.product_requireddateremarks + "'," +
                        "'" + values.display_field + "') ";
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
            catch (Exception ex)
            {
                values.message = "Exception occured whileadding product in purchase enquiry!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // Currency
        public void DaGetCurrencyDets(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {


                msSQL = "select currencyexchange_gid,currency_code from crm_trn_tcurrencyexchange order by currency_code asc ";

//                msSQL = " SELECT e.currencyexchange_gid,e.currency_code,e.exchange_rate,e.country AS country_name,CONCAT(b.user_firstname, ' ', b.user_lastname) AS created_by, " +
//" DATE_FORMAT(e.created_date, '%d-%m-%Y') AS created_date FROM crm_trn_tcurrencyexchange e JOIN (SELECT currency_code, MAX(created_date) AS max_created_date " +
//" FROM crm_trn_tcurrencyexchange GROUP BY currency_code) m ON e.currency_code = m.currency_code AND e.created_date = m.max_created_date " +
//" LEFT JOIN adm_mst_tuser b ON b.user_gid = e.created_by GROUP BY e.currency_code  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetCurrencyDetsDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetCurrencyDetsDropdown

                        {
                            currencyexchange_gid = dt["currencyexchange_gid"].ToString(),
                            currency_code = dt["currency_code"].ToString(),

                        });
                        values.GetCurrencyDets = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting currency details  in purchase enquiry!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             
        }


        // Currency
        public void DaGetBranchDet(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                

                msSQL = "select branch_gid, branch_name from hrm_mst_tbranch ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetBranchDetsDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetBranchDetsDropdown

                        {
                            branch_gid = dt["branch_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),

                        });
                        values.GetBranchDet = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting branch details in purchase enquiry!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }


        //Product for raise quote

        public void DaGetProductDets(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 
                msSQL = "select product_gid,product_name from pmr_mst_tproduct" +
" where product_name = product_name  and delete_flag='N'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProductDetDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProductDetDropdown
                        {
                            product_name = dt["product_name"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                        });
                        values.GetProductDets = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting product details  in purchase enquiry!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        // on change product for enquiry to quotation
        public void DaGetOnChangeProductNameDets(string product_gid, MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                

                if (product_gid != null)
                {
                    msSQL = " Select a.product_name, a.product_code, b.productuom_gid,b.productuom_name,c.productgroup_name,c.productgroup_gid,a.productuom_gid  from pmr_mst_tproduct a  " +
                         " left join pmr_mst_tproductuom b on a.productuom_gid = b.productuom_gid  " +
                        " left join pmr_mst_tproductgroup c on a.productgroup_gid = c.productgroup_gid  " +
                    " where a.product_gid='" + product_gid + "' ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    var getModuleList = new List<GetProductNameDetsDropdown>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getModuleList.Add(new GetProductNameDetsDropdown
                            {
                                product_name = dt["product_name"].ToString(),
                                product_code = dt["product_code"].ToString(),
                                productuom_name = dt["productuom_name"].ToString(),
                                productgroup_name = dt["productgroup_name"].ToString(),
                                productuom_gid = dt["productuom_gid"].ToString(),
                                productgroup_gid = dt["productgroup_gid"].ToString(),

                            });
                            values.GetProductNameDets = getModuleList;
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while on changing prioduct name in purchase enquiry!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // Tax 1
        public void DaGetFirstTax(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 


                msSQL = " select tax_name,tax_gid,percentage from acp_mst_ttax where active_flag='Y' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetFirsttaxDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetFirsttaxDropdown

                        {
                            tax_gid = dt["tax_gid"].ToString(),
                            tax_name = dt["tax_name"].ToString(),
                            percentage = dt["percentage"].ToString()

                        });
                        values.GetFirstTax = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting tax1!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // Tax 2
        public void DaGetSecondTax(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 

                msSQL = " select tax_name,tax_gid,percentage from acp_mst_ttax where active_flag='Y' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetSecondtaxDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetSecondtaxDropdown

                        {
                            tax_gid2 = dt["tax_gid"].ToString(),
                            tax_name2 = dt["tax_name"].ToString(),
                            percentage = dt["percentage"].ToString()


                        });
                        values.GetSecondTax = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting tax2 !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             
        }

        // Tax 3
        public void DaGetThirdTax(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                


                msSQL = " select tax_name,tax_gid,percentage from acp_mst_ttax where active_flag='Y' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetThirdtaxDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetThirdtaxDropdown

                        {
                            tax_gid3 = dt["tax_gid"].ToString(),
                            tax_name3 = dt["tax_name"].ToString(),
                            percentage = dt["percentage"].ToString()


                        });
                        values.GetThirdTax = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting tax3!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // Tax 4
        public void DaGetFourthTax(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                

                msSQL = " select tax_name,tax_gid,percentage from acp_mst_ttax where active_flag='Y' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetFourthtaxDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetFourthtaxDropdown

                        {
                            tax_gid4 = dt["tax_gid"].ToString(),
                            tax_name4 = dt["tax_name"].ToString(),
                            percentage = dt["percentage"].ToString()

                        });
                        values.GetFourthTax = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting  tax4!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }


        public void DaGetTerms(MdlPmrTrnRaiseEnquiry values)

        {
            try
            {
                 


                msSQL = "  select a.template_gid, c.template_name, c.template_content from adm_trn_ttemplate2module a " +

" left join adm_mst_tmodule b on a.module_gid = b.module_gid " +

" left join adm_mst_ttemplate c on a.template_gid = c.template_gid " +

" left join adm_mst_ttemplatetype d on c.templatetype_gid = d.templatetype_gid " +

" where a.module_gid = 'SMR' ";

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

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting tersm and conditions!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }



        public void DaGetVendorName(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 
                msSQL = "  Select a.vendor_companyname,a.vendorregister_gid,a.email_id,a.contactperson_name,a.contact_telephonenumber," +
"a.email_id,b.country_gid,b.address1,b.address2,c.country_name,b.city,b.state, b.postal_code,b.branch_name " +
"from acp_mst_tvendorregister a" +
" left join adm_mst_taddress b on a.address_gid = b.address_gid" +
"  left join adm_mst_tcountry c on b.country_gid = c.country_gid " +
"where a.active_flag = 'Y'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetVendorname>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetVendorname
                        {
                            vendor_companyname = dt["vendor_companyname"].ToString(),
                            vendor_gid = dt["vendorregister_gid"].ToString(),

                        });
                        values.GetVendor = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting vendor!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaGetVendorDtl(string vendor_gid, MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 
                msSQL = "  Select a.vendor_companyname,a.vendorregister_gid,a.email_id,a.contactperson_name,a.contact_telephonenumber," +
"a.email_id,b.country_gid,b.address1,b.address2,c.country_name,b.city,b.state, b.postal_code,b.branch_name " +
"from acp_mst_tvendorregister a" +
" left join adm_mst_taddress b on a.address_gid = b.address_gid" +
"  left join adm_mst_tcountry c on b.country_gid = c.country_gid where vendorregister_gid='" + vendor_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetVendorraise>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetVendorraise
                        {
                            vendor_companyname = dt["vendor_companyname"].ToString(),
                            vendor_gid = dt["vendorregister_gid"].ToString(),
                            email_id = dt["email_id"].ToString(),
                            contactperson_name = dt["contactperson_name"].ToString(),
                            contact_telephonenumber = dt["contact_telephonenumber"].ToString(),
                            country_gid = dt["country_gid"].ToString(),
                            address1 = dt["address1"].ToString(),
                            country_name = dt["country_name"].ToString(),
                            address2 = dt["address2"].ToString(),
                            city = dt["city"].ToString(),
                            state = dt["state"].ToString(),
                            postal_code = dt["postal_code"].ToString(),
                            vendorbranch_name = dt["branch_name"].ToString(),
                        });
                        values.GetVendorlist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting vendor  in purchase enquiry!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaGetEmployeePerson(string employee_gid, MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 

                msSQL = " select a.*, concat(b.user_firstname, '-', b.user_code) as user,c.employee_gid, b.user_firstname from adm_mst_tmodule2employee a  " +
" inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid" +
" inner join adm_mst_tuser b on c.user_gid = b.user_gid" +
" where a.module_gid = 'SMR'" +
" and a.employeereporting_to = '" + employee_gid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetAssignDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetAssignDropdown

                        {
                            employee_gid = dt["employee_gid"].ToString(),
                            user_firstname = dt["user_firstname"].ToString(),


                        });
                        values.GetEmployeePerson = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting employee person  in purchase enquiry!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             
        }

        public void DaProductsSummary(string user_gid, MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 

                msSQL = " select a.tmpsalesenquiry_gid,a.vendorproduct_code,a.qty_requested,a.display_field, " +
" date_format(a.product_requireddate,'%d-%m-%Y') as product_requireddate, " +
" d.productgroup_name,b.product_code,b.product_name,c.productuom_name,a.product_gid, " +
" format(a.potential_value,2)as potential_value,a.product_requireddateremarks " +
" from pmr_tmp_tvendorenquiry a left join pmr_mst_tproduct b on a.product_gid=b.product_gid " +
" left join pmr_mst_tproductuom c on a.uom_gid=c.productuom_gid " +
" left join pmr_mst_tproductgroup d on a.productgroup_gid= d.productgroup_gid" +
" where a.user_gid='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<productsummarys_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new productsummarys_list
                        {
                            tmpsalesenquiry_gid = dt["tmpsalesenquiry_gid"].ToString(),
                            vendorproduct_code = dt["vendorproduct_code"].ToString(),
                            qty_requested = dt["qty_requested"].ToString(),
                            product_requireddate = dt["product_requireddate"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            potential_value = dt["potential_value"].ToString(),
                            product_requireddateremarks = dt["product_requireddateremarks"].ToString(),


                        });
                        values.productsummarys_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting product in purchase enquiry!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaGetDeleteEnquiryProductSummary(string tmpsalesenquiry_gid, productsummarys_list values)
        {
            try
            {
                 
                msSQL = "delete from pmr_tmp_tvendorenquiry where tmpsalesenquiry_gid='" + tmpsalesenquiry_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Product  Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deleting Product";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while deleting product  in purchase enquiry!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaPostVendorEnquiry(string employee_gid, string user_gid, PostAll values)

        {
            try
            {
                

                msSQL = "SELECT * FROM pmr_tmp_tvendorenquiry WHERE user_gid='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Select one Product to Raise Enquiry";
                }

                msGetGid1 = objcmnfunctions.GetMasterGID("PEYP");

                if (msGetGid1 == "E") // Assuming "E" is a string constant
                {
                    values.status = true;
                    values.message = "Create Sequence Code PEYP for Sales Enquiry Details";
                }


                msSQL = "SELECT DISTINCT " +
                    "a.product_gid, a.product_remarks, a.vendorproduct_code, a.branch_gid,a.potential_value," +
                    "a.qty_requested, a.uom_gid, a.display_field, a.product_requireddate, a.product_requireddateremarks, " +
                    "a.productgroup_gid" +
                    " FROM pmr_tmp_tvendorenquiry a WHERE" +
                    " a.user_gid = '" + user_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("PEDC");

                        if (msGetGid == "E") // Assuming "E" is a string constant
                        {
                            values.status = true;
                            values.message = "Create Sequence Code PEDC for Sales Enquiry Details";
                        }

                        string lsnewproduct_flag = "Y";

                        msSQL = " Insert into pmr_trn_tenquirydtl " +
                               " (enquirydtl_gid," +
                               " enquiry_gid , " +
                               " vendorproduct_code," +
                               " product_gid," +
                               " potential_value," +
                               " uom_gid," +
                               " productgroup_gid," +
                               " qty_enquired, " +
                               " newproduct_flag, " +
                               " product_requireddate, " +
                               " product_requireddateremarks," +
                               " display_field ) " +
                               " values (" +
                               "'" + msGetGid + "'," +
                               "'" + msGetGid1 + "'," +
                               "'" + dt["vendorproduct_code"].ToString() + "'," +
                               "'" + dt["product_gid"].ToString() + "'," +
                               "'" + dt["potential_value"].ToString() + "'," +
                               "'" + dt["uom_gid"].ToString() + "'," +
                               "'" + dt["productgroup_gid"].ToString() + "'," +
                               "'" + dt["qty_requested"].ToString() + "', " +
                               "'" + lsnewproduct_flag + "', ";

                        if (dt["product_requireddate"].ToString() == null || DBNull.Value.Equals(dt["product_requireddate"].ToString()))
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            string formattedDate = ((DateTime)dt["product_requireddate"]).ToString("yyyy-MM-dd");
                            msSQL += "'" + formattedDate + "',";
                        }
                        msSQL += "'" + dt["product_requireddateremarks"].ToString() + "',";
                        msSQL += "'" + dt["display_field"].ToString() + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                    }
                }
                //else
                //{
                //    values.message = "Please add the product to raise enquiry";
                //}
                if (mnResult == 1)
                {
                    string lsenquiry_status = "Enquiry Raised";
                    string lsenquiry_flag = "PR Pending Approval";
                    //string lspurchaseenquiry_flag = "Enquiry Raised";
                    string lslead_status = "Assigned";

                    string uiDateStr = values.enquiry_date; DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string enquiry_date = uiDate.ToString("yyyy-MM-dd HH:mm:ss");

                    string uiDateStr1 = values.closure_date; DateTime uiDate1 = DateTime.ParseExact(uiDateStr1, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string closure_date = uiDate1.ToString("yyyy-MM-dd HH:mm:ss");



                    msSQL = "INSERT INTO pmr_trn_tenquiry " +
                                                   "(enquiry_gid, " +
                                                   "branch_gid, " +
                                                   "branch_name, " +
                                                   "vendor_gid, " +
                                                   "contact_number, " +
                                                   "vendor_companyname, " +
                                                   "contact_email, " +
                                                   "vendor_contact_person, " +
                                                   "vendor_address, " +
                                                   "enquiry_date, " +
                                                   "enquiry_remarks, " +
                                                   "enquiry_status, " +
                                                   "enquiry_referencenumber, " +
                                                   "created_by, " +
                                                   "created_date, " +
                                                   "vendor_requirement, " +
                                                   "customer_rating, " +
                                                    "assigned_by, " +
                                                   "closure_date, " +
                                                   "enquiry_flag ) " +
                                                   "VALUES (" +
                                                   "'" + msGetGid1 + "'," +
                                                   "'" + values.branch_name + "'," +
                                                    "'" + values.branch_gid + "'," +
                                                   "'" + values.vendor_gid + "'," +
                                                   "'" + values.contact_telephonenumber + "'," +
                                                   "'" + values.vendor_companyname + "'," +
                                                   "'" + values.email_id + "'," +
                                                   "'" + values.contactperson_name + "'," +
                                                   "'" + values.address1 + "'," +
                                                   "'" + enquiry_date + "'," +
                                                   "'" + values.enquiry_remarks + "'," +
                                                    "'" + lsenquiry_status + "'," +
                                                   "'" + values.enquiry_referencenumber + "'," +
                                                   "'" + user_gid + "'," +
                                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                   "'" + values.vendor_requirement + "'," +
                                                   "'" + values.customer_rating + "'," +
                                                     "'" + values.user_firstname + "'," +
                                                   "'" + closure_date + "'," +
                                                    "'" + lsenquiry_flag + "')";



                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Enquiry Raised Successfully";
                        msSQL = " delete from pmr_tmp_tvendorenquiry " +
                                " where user_gid = '" + user_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Raising Enquiry";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding purchase enquiry!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             
        }


        public void DaGetVendorEnquirySummary(MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                
                msSQL = "select  a.enquiry_gid as enquiry_refno,date_format(a.enquiry_date,'%d-%m-%Y') as enquiry_date,a.enquiry_referencenumber,f.branch_name,a.vendor_address,a.enquiry_status,a.vendor_gid," +
                        "  a.assigned_by,a.created_by,d.potential_value,a.customer_rating,a.created_date,b.vendorproduct_code,e.user_firstname,c.vendor_companyname from pmr_trn_tenquiry a " +
                        " left join pmr_trn_tenquirydtl b on a.enquiry_gid = b.enquiry_gid " +
                        " left join acp_mst_tvendorregister c on a.vendor_gid = c.vendorregister_gid " +
                        "  left join pmr_trn_tenquirydtl d on b.product_gid = d.product_gid " +
                        " left join hrm_mst_tbranch f on a.branch_gid = f.branch_gid " +
                        " left join adm_mst_tuser e on a.created_by= e.user_gid " +
                        " where 1=1  group by enquiry_refno order by enquiry_refno desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetCusEnquiry>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetCusEnquiry
                        {
                            enquiry_gid = dt["enquiry_refno"].ToString(),
                            enquiry_date = dt["enquiry_date"].ToString(),
                            enquiry_refno = dt["enquiry_refno"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            branch_gid = dt["branch_name"].ToString(),
                            vendor_name = dt["vendor_companyname"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            enquiry_referencenumber = dt["enquiry_referencenumber"].ToString(),
                            contact_details = dt["vendor_address"].ToString(),
                            potorder_value = dt["potential_value"].ToString(),
                            enquiry_status = dt["enquiry_status"].ToString(),
                            vendor_rating = dt["customer_rating"].ToString(),
                            created_by = dt["user_firstname"].ToString(),
                            assigned_by = dt["assigned_by"].ToString(),

                        });
                        values.cusenquiry_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting purchase enquiry!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void Daviewvendorenquiry(string enquiry_gid, MdlPmrTrnRaiseEnquiry values)
        {
            try
            {
                 
                msSQL = " select a.enquiry_gid,a.enquiry_date,b.branch_gid,b.branch_name,a.enquiry_referencenumber,a.vendor_companyname,a.vendor_contact_person,a.contact_number,a.contact_email,a.vendor_address,a.closure_date,c.productgroup_name,d.productuom_name,e.product_requireddate,e.potential_value,e.vendorproduct_code,e.qty_enquired,f.product_name,f.product_code from pmr_trn_tenquiry a " +
" left join hrm_mst_tbranch b on b.branch_gid= a.branch_gid" +
" left join pmr_trn_tenquirydtl e on e.enquiry_gid = a.enquiry_gid" +
" left join pmr_mst_tproduct f on f.product_gid = e.product_gid" +
" left join pmr_mst_tproductuom d on d.productuom_gid = f.productuom_gid" +
" left join pmr_mst_tproductgroup c on c.productgroup_gid = e.productgroup_gid" +
" where a.enquiry_gid='" + enquiry_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<viewvendorenquiry_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new viewvendorenquiry_list
                        {
                            enquiry_gid = dt["enquiry_gid"].ToString(),
                            enquiry_date = Convert.ToDateTime(dt["enquiry_date"]).ToString("dd-MM-yyyy"),
                            branch_name = dt["branch_name"].ToString(),
                            branch_gid = dt["branch_name"].ToString(),
                            enquiry_referencenumber = dt["enquiry_referencenumber"].ToString(),
                            vendor_companyname = dt["vendor_companyname"].ToString(),
                            contact_number = dt["contact_number"].ToString(),
                            vendor_contact_person = dt["vendor_contact_person"].ToString(),
                            contact_email = dt["contact_email"].ToString(),
                            vendor_address = dt["vendor_address"].ToString(),
                            closure_date = Convert.ToDateTime(dt["closure_date"]).ToString("dd-MM-yyyy"),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            vendorproduct_code = dt["vendorproduct_code"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            qty_enquired = dt["qty_enquired"].ToString(),
                            potential_value = dt["potential_value"].ToString(),
                            product_requireddate = dt["product_requireddate"].ToString()
                        });
                        values.viewvendorenquiry_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting purchase enquiry view!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
         $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
         ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
         msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
         DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
    }
}
