using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.sales.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static ems.sales.Models.MdlSmrMstPricesegmentSummary;
using System.Security.Cryptography;


namespace ems.sales.DataAccess
{
    public class DaSmrMstPricesegmentSummary
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, msSTOCKGetGID, msSTOCKHISTORYGetGID, lsold_price, lsentity_code, msGetGID1, lsdesignation_code, msGetGID, lsCode, msGetGid, msGetGid1, msGetGid2, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        //summary
        public void DaGetSmrMstPricesegmentSummary(MdlSmrMstPricesegmentSummary values)
        {
            try
            {
                
                msSQL = " select pricesegment_gid,pricesegment_name,pricesegment_code from smr_trn_tpricesegment ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getpricesegment_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getpricesegment_list
                        {
                            pricesegment_gid = dt["pricesegment_gid"].ToString(),
                            pricesegment_code = dt["pricesegment_code"].ToString(),
                            pricesegment_name = dt["pricesegment_name"].ToString()

                        });
                        values.pricesegment_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while  Loading Pricesegment Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaPostPostPriceSegment(string user_gid, Getpricesegment_list values)
        {
            try
            {
               

                msGetGid = objcmnfunctions.GetMasterGID("SPRS");

                msSQL = " insert into smr_trn_tpricesegment ( " +
                   " pricesegment_gid," +
                   " pricesegment_code, " +
                   " pricesegment_name " +
                   " ) values( " +
                   " '" + msGetGid + "', " +
                   " '" + values.pricesegment_code + "'," +
                   " '" + values.pricesegment_name + "' )";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Price Segment Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Adding Price Segment";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Submiting Price Segment ummary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaUpdatePriceSegment(string user_gid, Getpricesegment_list values)
        {
            try
            {
                

                msSQL = " update  smr_trn_tpricesegment   set " +
              " pricesegment_code  = '" + values.pricesegmentedit_code + "'," +
              " pricesegment_name = '" + values.pricesegmentedit_name + "'," +
              " updated_by = '" + user_gid + "'," +
              " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where pricesegment_gid ='" + values.pricesegment_gid + "'  ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    values.status = true;
                    values.message = "Price Segment Updated Successfully";

                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Price Segment";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating  Pricesegment Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DadeletePriceSegmentSummary(string pricesegment_gid, Getpricesegment_list values)
        {
            try
            {
               
                msSQL = "  delete from smr_trn_tpricesegment  where pricesegment_gid='" + pricesegment_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Price Segment Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deleting Price Segment";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Deletting Price Segment  !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaGetPricesegmentgrid(string pricesegment_gid, MdlSmrMstPricesegmentSummary values)
        {
            try
            {
               
                msSQL = " select a.customer_gid,a.pricesegment_gid,a.customer_name,b.customer_id, " +
                             " concat(d.customercontact_name,'/',d.mobile,'/',d.email) as contact_details, " +
                             " case when c.region_name is null then concat(b.customer_city,'',b.customer_state) " +
                             " else Concat(c.region_name,'',b.customer_city,'',b.customer_state) end as region_name " +
                             " from smr_trn_tpricesegment2customer a " +
                             " left join crm_mst_tcustomer b on a.customer_gid=b.customer_gid " +
                             " left join crm_mst_tregion c on b.customer_region =c.region_gid " +
                             " left join crm_mst_tcustomercontact d on b.customer_gid=d.customer_gid where pricesegment_gid='" + pricesegment_gid + "' " +
                             " and b.status='Active' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getpricesegmentgrid_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getpricesegmentgrid_list
                        {
                            pricesegment_gid = dt["pricesegment_gid"].ToString(),
                            customer_gid = dt["customer_gid"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            customer_id = dt["customer_id"].ToString(),
                            contact_details = dt["contact_details"].ToString(),
                            region_name = dt["region_name"].ToString(),

                        });
                        values.pricesegmentgrid_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Pricesegmentgrid !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        //product group drodown
        public void DaGetSmrGroupDtl(MdlSmrMstPricesegmentSummary values)
        {
            try
            {
                
                msSQL = " select distinct productgroup_gid,concat(productgroup_code,'|',productgroup_name) as productgroup_name " +
                       " from pmr_mst_tproductgroup where delete_flag='N' order by productgroup_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProductGroupDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProductGroupDropdown
                        {
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                        });
                        values.GetSmrGroupDtl = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Product Group  Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        //product Name drodown
        public void DaGetSmrProductDtl(MdlSmrMstPricesegmentSummary values)
        {
            try
            {
               
                msSQL = " select distinct a.product_gid,concat(product_code,'|',product_name) as product_name from pmr_mst_tproduct a where delete_flag='N' " +
                    " order by product_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProductNameDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProductNameDropdown
                        {
                            product_gid = dt["product_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                        });
                        values.GetSmrProductDtl = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Product Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        //product Unit drodown
        public void DaGetSmrUnitDtl(MdlSmrMstPricesegmentSummary values)
        {
            try
            {
               
                msSQL = " select distinct productuom_gid,productuom_name from pmr_mst_tproductuom where delete_flag='N' " +
                    " order by productuom_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProductUnitDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProductUnitDropdown
                        {
                            productuom_gid = dt["productuom_gid"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                        });
                        values.GetSmrUnitDtl = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Product Uom Name  !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        //Product Assign
        public void DaGetSmrMstProductAssignSummary(MdlSmrMstPricesegmentSummary values)
        {
            try
            {
               
                msSQL = " select a.stock_gid,b.product_name,b.product_code,e.pricesegment_gid, e.pricesegment_name,e.customerproduct_code,d.productgroup_gid,b.product_gid,d.productgroup_name,c.productuom_name, " +
                   " c.productuom_gid,format(a.unit_price,2)as unit_price, a.created_date from ims_trn_tstock a " +
                   "  left join pmr_mst_tproduct b on a.product_gid=b.product_gid " +
                    " left join pmr_mst_tproductuom c on a.uom_gid=c.productuom_gid " +
                    " left join pmr_mst_tproductgroup d on  b.productgroup_gid=d.productgroup_gid " +
                    " left join smr_trn_tpricesegment2product e on a.product_gid=e.product_gid " +
                    " where a.created_date in (select max(x.created_date) from ims_trn_tstock x group by x.product_gid,x.uom_gid) group by a.product_gid,a.uom_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetProduct>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetProduct
                        {
                            pricesegment_gid = dt["pricesegment_gid"].ToString(),
                            pricesegment_name = dt["pricesegment_name"].ToString(),
                            stock_gid = dt["stock_gid"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            product_price = dt["unit_price"].ToString(),
                            customerproduct_code = dt["customerproduct_code"].ToString(),
                            created_date = dt["created_date"].ToString()

                        });
                        values.productgroup_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading  ProductAssignSummary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        // on change
        public void DaGetOnChangeProductName(string product_gid, MdlSmrMstPricesegmentSummary values)
        {
            try
            {
               
                if (product_gid != null)
                {
                    msSQL = " Select distinct a.product_gid,a.product_name,a.product_code,a.productgroup_gid,d.productgroup_name,c.productuom_gid, c.productuom_name " +
                            " from pmr_mst_tproduct a " +
                            " left join pmr_mst_tproductuom c on a.productuom_gid= c.productuom_gid " +
                            " left join pmr_mst_tproductgroup d on a.productgroup_gid=d.productgroup_gid " +
                            " where a.product_gid = '" + product_gid + "'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    var getModuleList = new List<GetProductName>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getModuleList.Add(new GetProductName
                            {
                                product_gid = dt["product_gid"].ToString(),
                                product_name = dt["product_name"].ToString(),
                                productuom_name = dt["productuom_name"].ToString(),
                                productgroup_name = dt["productgroup_name"].ToString(),
                                productuom_gid = dt["productuom_gid"].ToString(),
                                productgroup_gid = dt["productgroup_gid"].ToString(),

                            });
                            values.OnChangeProductName = getModuleList;
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception Occured while Onchange Product Name  !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }

        // Product
        public void DaPostProduct(string user_gid, string branch_gid, Getproduct_list values)
        {
            try
            {
                
                string lsemployeegid;
                msSQL = " select b.employee_gid from adm_mst_tuser a " +
                    " left join hrm_mst_temployee b on a.user_gid=b.user_gid " +
                    " where a.user_gid ='" + user_gid + "'";
                lsemployeegid = objdbconn.GetExecuteScalar(msSQL);

                msGetGid2 = objcmnfunctions.GetMasterGID("SRCT");

                msSQL = " insert into smr_trn_tpricesegment2product ( " +
                   " pricesegment2product_gid," +
                    " product_code, " +
                    " product_name," +
                    " product_gid," +
                    " pricesegment_gid," +
                    " pricesegment_name," +
                    " productuom_gid," +
                    " productuom_name," +
                    " productgroup_code," +
                    " productgroup , " +
                    " product_price, " +
                    " customerproduct_code, " +
                    " created_by," +
                    " created_date " +
                   " ) values( " +
                   " '" + msGetGid2 + "', " +
                   " '" + values.product_code + "'," +
                   " '" + values.product_name + "'," +
                   " '" + values.product_gid + "'," +
                   " '" + values.pricesegment_gid + "'," +
                   " '" + values.pricesegment_name + "'," +
                   " '" + values.productuom_gid + "'," +
                   " '" + values.productuom_name + "'," +
                   " '" + values.productgroup_gid + "'," +
                   " '" + values.productgroup_name + "'," +
                   " '" + values.product_price + "'," +
                   " '" + values.customerproduct_code + "'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {

                    msSTOCKGetGID = objcmnfunctions.GetMasterGID("ISKP");
                    msGetGid1 = objcmnfunctions.GetMasterGID("PGNP");

                    msSQL = " insert into ims_trn_tstock " +
                    " (stock_gid, " +
                    " branch_gid, " +
                    " product_gid, " +
                    " uom_gid, " +
                    " stock_qty, " +
                    " grn_qty, " +
                    " unit_price, " +
                    " remarks, " +
                    " stocktype_gid, " +
                    " reference_gid, " +
                    " stock_flag, " +
                    " created_by, " +
                    " created_date)" +
                    " values( " +
                    " '" + msSTOCKGetGID + "'," +
                    " '" + branch_gid + "'," +
                    " '" + values.product_name + "', " +
                    " '" + values.productuom_name + "'," +
                    " '0.00'," +
                    " '0.00'," +
                     " '" + values.product_price + "'," +
                     " '" + values.remarks + "'," +
                     "'SY0905270003'," +
                    " '" + msGetGid1 + "'," +
                    " 'Y'," +
                    " '" + lsemployeegid + "'," +
                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

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
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Submitting Product !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        //Product Head
        public void DaGetSmrMstProductHead(MdlSmrMstPricesegmentSummary values)
        {
            try
            {
                
                msSQL = " select pricesegment_gid, pricesegment_name from smr_trn_tpricesegment group by pricesegment_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getpricesegment_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getpricesegment_list
                        {
                            pricesegment_gid = dt["pricesegment_gid"].ToString(),
                            pricesegment_name = dt["pricesegment_name"].ToString(),

                        });
                        values.pricesegment_list = getModuleList;
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Pricesegment Name  !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }

        // Update

        public void DaGetUpdateProduct(string employee_gid, Getproduct_list values)

        {
            try
            {
                
                string lsold_price = "0.00";


                msSQL = " INSERT INTO smr_trn_tstockpricehistory( " +
                        " product_gid, " +
                        " pricesegment_gid, " +
                        " customerproduct_code," +
                        " old_price, " +
                        " stock_gid, " +
                        " updated_price, " +
                        " updated_by ," +
                        " updated_date " +
                        " ) VALUES ( " +
                        " '" + values.product_gid + "', " +
                        " '" + msGetGid + "', " +
                        " '" + values.customerproduct_code + "'," +
                        " ' " + lsold_price + "', " +
                        " '" + msSTOCKGetGID + "', " +
                        " '" + values.product_price + "', " +
                        " '" + employee_gid + "', " +
                        " '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ) ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update smr_trn_tpricesegment2product set " +
                        " product_price ='" + values.product_price + "', " +
                        " customerproduct_code='" + values.customerproduct_code + "', " +
                       " updated_by = '" + employee_gid + "'," +
                       " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                      " where product_gid='" + values.product_gid + "' and productuom_gid='" + values.productuom_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ims_trn_tstock set unit_price ='" + values.product_price + "' " +
                              " where stock_gid= '" + msSTOCKGetGID + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
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
                values.message = "Exception occured while Update Product Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        // Un Assign
        public void DaGetUnAssignProduct(string product_gid, GetProduct values)
        {
            try
            {
               
                msSQL = " update  smr_trn_tpricesegment2product set unassigned_flag='Y' where product_gid='" + product_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Product Un Assigned Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Un Assigning Product ";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Un Assign Product  !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        //Assign - Unassign
        public void DaGetUnassignedlists(string pricesegment_gid, MdlSmrMstPricesegmentSummary values)
        {
            try
            {
                
                msSQL = " select a.customer_gid,a.customer_name from crm_mst_tcustomer a where a.customer_gid not in" +
                    " (select distinct b.customer_gid from smr_trn_tpricesegment2customer b" +
                    " where 1=1 )  and a.status='Active' and a.customer_name!='' order by a.customer_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetUnassignedlists>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetUnassignedlists
                        {


                            employee_gid = dt["customer_gid"].ToString(),
                            employee_name = dt["customer_name"].ToString(),




                        });
                        values.GetUnassignedlists = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Employee Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaGetAssignedlists(string pricesegment_gid, MdlSmrMstPricesegmentSummary values)
        {
            try
            {
                

                msSQL = " select distinct customer_gid,customer_name from smr_trn_tpricesegment2customer a " +
                    " where pricesegment_gid = '" + pricesegment_gid + "' order by customer_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetAssignedlists>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetAssignedlists
                        {
                            employee_gid = dt["customer_gid"].ToString(),
                            employee_name = dt["customer_name"].ToString(),
                        });
                        values.GetAssignedlists = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Assigned Customer  !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaGetUnassigned(string pricesegment_gid, MdlSmrMstPricesegmentSummary values)
        {
            try
            {
               
                msSQL = " select a.customer_gid,a.customer_name from crm_mst_tcustomer a where a.customer_gid not in" +
                   " (select distinct b.customer_gid from smr_trn_tpricesegment2customer b" +
                   " where 1=1 )  and a.status='Active' order by a.customer_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetUnassigned>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetUnassigned
                        {
                            employee_gid = dt["customer_gid"].ToString(),
                            employee_name = dt["customer_name"].ToString(),
                        });
                        values.GetUnassigned = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while UnAssigned Customer !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaPostAssignedlist(string user_gid, campaignassign_list values)
        {
            try
            {
                
                msSQL = " select pricesegment_gid from smr_trn_tpricesegment " +
                                " where pricesegment_name='" + values.pricesegment_name + "'";
                string lspricesegmentgid = objdbconn.GetExecuteScalar(msSQL);

                for (int i = 0; i < values.campaignassign.ToArray().Length; i++)
                {
                    msGetGID = objcmnfunctions.GetMasterGID("VPDC");
                    //msGetGID1 = objcmnfunctions.GetMasterGID("SPRS");
                    msSQL = " insert into smr_trn_tpricesegment2customer(" +
                                " pricesegment2customer_gid, " +
                                " pricesegment_gid, " +
                                " pricesegment_name," +
                                " customer_gid, " +
                                " customer_name, " +
                                " created_by, " +
                                " created_date" +
                                " )values( " +
                                "'" + msGetGID + "', " +
                                "'" + lspricesegmentgid + "'," +
                                "'" + values.campaignassign[i]._key3 + "'," +
                                "'" + values.campaignassign[i]._id + "', " +
                                "'" + values.campaignassign[i]._name + "', " +
                                "'" + user_gid + "', " +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = false;
                        values.message = "Customer assigned to PriceSegment successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Customer Assign";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Customer Assigne !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaPostUnassignedlist(string user_gid, campaignassign_list values)
        {
            try
            {
               
                for (int i = 0; i < values.campaignassign.ToArray().Length; i++)
                {

                    msSQL = " delete from smr_trn_tpricesegment2customer " +
                            " where pricesegment_gid = '" + values.campaignassign[i]._key1 + "' and customer_gid = '" + values.campaignassign[i]._id + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = false;
                        values.message = "Customer Unassigned to PriceSegment Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Customer Unassign";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Customer Unassign !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }
    }

}
