﻿using ems.pmr.Models;
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
using System.Security.Policy;
using System.Web.UI;
using System.Globalization;


namespace ems.pmr.DataAccess
{
    public class DaPmrTrnGrn
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader objOdbcDataReader, objOdbcDataReader1;
        DataTable dt_datatable;
        string msEmployeeGID, txtGRNRefNo, lblpurchasebranch_gid, msGetStockGID, lsgrn_status, lspurchaserequisition_gid, lsqty_billed, lsgrn_gid, lblVendor_gid, msGetGID, lstPR_GRN_flag, lsproductname, lsproductcode, lsproductuomname, lstPO_GRN_flag, lspurchaseorder_status, lsemployee_gid, lblBranch_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msStockGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid, maGetGID, lsvendor_code, msUserGid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5, lsQty_ReceivedAS, lsQty_Ordered, lsQty_Delivered, lsQty_Adjustable;
        int lsPR_Rec, lsPRt_GRNAdj;
        DataSet ds_dataset;
        private string grn_gid;

        public void DaGrninwardSummary(MdlPmrTrnGrn values)
        {
            try
            {
                 

                msSQL = " SELECT a.purchaseorder_gid, a.vendor_gid, a.created_by, a.purchaseorder_status,e.vendor_companyname,j.costcenter_name, " +
                    " a.purchaseorder_date, y.branch_name, c.department_name, d.user_firstname,case when group_concat(distinct f.purchaserequisition_referencenumber)=',' then '' " +
                    " when group_concat(distinct f.purchaserequisition_referencenumber) <> ',' then  group_concat(distinct f.purchaserequisition_referencenumber) end  as refrence_no " +
                    " FROM pmr_trn_tpurchaseorder a  left join hrm_mst_temployee b on a.created_by = b.user_gid " +
                    " left join hrm_mst_tdepartment c on b.department_gid = c.department_gid " +
                    " left join adm_mst_tuser d on d.user_gid = a.created_by  " +
                    " left join acp_mst_tvendor e on e.vendor_gid=a.vendor_gid " +
                    " left join pmr_mst_tcostcenter j on j.costcenter_gid=a.costcenter_gid " +
                    " left join pmr_Trn_tpurchaserequisition f on a.purchaserequisition_gid=f.purchaserequisition_gid " +
                    " left join hrm_mst_tbranch y on a.branch_gid=y.branch_gid " +
                    " where 0=0 and  ((a.purchaseorder_flag = 'PO Approved' and a.grn_flag = 'GRN Pending')  or" +
                    " (a.grn_flag = 'Goods Received Partial' and (a.invoice_flag = 'IV Pending' or a.invoice_flag = 'Invoice Raised Partial'))) " +
                    " group by a.purchaseorder_gid  order by date(a.purchaseorder_date)desc,a.purchaseorder_date asc, a.purchaseorder_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getgrn_lists>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getgrn_lists
                        {

                            purchaseorder_date = dt["purchaseorder_date"].ToString(),
                            purchaseorder_gid = dt["purchaseorder_gid"].ToString(),
                            purchaseorder_status = dt["purchaseorder_status"].ToString(),
                            vendor_companyname = dt["vendor_companyname"].ToString(),
                            costcenter_name = dt["costcenter_name"].ToString(),
                            department_name = dt["department_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                        });
                        values.Getgrn_lists = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting GRN Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                   $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                   ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                   msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                   DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaGetaddgrnsummary(string user_gid, string purchaseorder_gid, MdlPmrTrnGrn values)
        {
            try
            {
                
                grn_gid = objcmnfunctions.GetMasterGID("SUSM");

                if (grn_gid == "E")
                {
                    values.message = "Create Sequence Code PGNP for GRN Table";
                }

                //Raised By Binding Event
                string userFirstNameSQL = " select a.user_gid, concat(a.user_firstname,' - ',c.department_name) as user_firstname " +
                                          " from adm_mst_tuser a " +
                                          " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                                          " left join hrm_mst_tdepartment c on b.department_gid = c.department_gid " +
                                          " where a.user_gid = '" + user_gid + "' ";
                DataTable userFirstNameDataTable = objdbconn.GetDataTable(userFirstNameSQL);

                string userFirstName = string.Empty;

                if (userFirstNameDataTable.Rows.Count > 0)
                {
                    userFirstName = userFirstNameDataTable.Rows[0]["user_firstname"].ToString();
                }
                //--END--//


                //Check by user drop down event
                msSQL = " select branch_gid from pmr_trn_tpurchaseorder where purchaseorder_gid = '" + purchaseorder_gid + "'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows)
                {
                    lblpurchasebranch_gid = objOdbcDataReader["branch_gid"].ToString();
                }
                msSQL = " select b.branch_gid, d.mainbranch_flag " +
                     " from adm_mst_tuser a " +
                     " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                     " left join hrm_mst_tbranch d on b.branch_gid = d.branch_gid " +
                     " where a.user_gid = '" + user_gid + "' ";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows)
                {
                    lblBranch_gid = objOdbcDataReader["branch_gid"].ToString();
                }

                string userFirstName1SQL = "SELECT CONCAT(a.user_firstname, ' ', a.user_lastname) AS user_firstname1, a.user_gid, a.user_code " +
                        "FROM adm_mst_tuser a " +
                        "LEFT JOIN hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                        "WHERE b.branch_gid = '" + lblpurchasebranch_gid + "' OR b.branch_gid = '" + lblBranch_gid + "'";

                DataTable userFirstName1DataTable = objdbconn.GetDataTable(userFirstName1SQL);

                List<string> user_firstname1List = new List<string>();

                foreach (DataRow row in userFirstName1DataTable.Rows)
                {
                    string user_firstname1 = row["user_firstname1"].ToString();
                    user_firstname1List.Add(user_firstname1);
                }

                //--END--//

                //Other fieds biding event query
                msSQL = "SELECT a.purchaseorder_gid, a.branch_gid, b.branch_name, c.vendor_companyname, c.contactperson_name, c.contact_telephonenumber, c.email_id, concat(d.address1, ' ', d.address2) as address " +
                        "FROM pmr_trn_tpurchaseorder a " +
                        "LEFT JOIN hrm_mst_tbranch b ON a.branch_gid = b.branch_gid " +
                        "LEFT JOIN acp_mst_tvendor c ON a.vendor_gid = c.vendor_gid " +
                        "LEFT JOIN adm_mst_taddress d ON c.address_gid = d.address_gid " +
                        "WHERE a.purchaseorder_gid = '" + purchaseorder_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<grn_lists>();

                if (dt_datatable.Rows.Count != 0)
                {

                    int user_firstname1Index = 0;

                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        if (user_firstname1Index < user_firstname1List.Count)
                        {
                            // Get the user_firstname1 value from the list using the counter
                            string user_firstname1 = user_firstname1List[user_firstname1Index];

                            getModuleList.Add(new grn_lists
                            {
                                branch_name = dt["branch_name"].ToString(),
                                vendor_companyname = dt["vendor_companyname"].ToString(),
                                contactperson_name = dt["contactperson_name"].ToString(),
                                contact_telephonenumber = dt["contact_telephonenumber"].ToString(),
                                email_id = dt["email_id"].ToString(),
                                address = dt["address"].ToString(),
                                purchaseorder_gid = dt["purchaseorder_gid"].ToString(),
                                grn_gid = grn_gid,
                                user_firstname = userFirstName,
                                user_firstname1 = user_firstname1,
                            });

                            // Increment the counter for the next iteration
                            user_firstname1Index++;
                        }
                    }

                    values.grn_lists = getModuleList;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting  add GRN Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                   $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                   ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                   msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                   DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaGetsummaryaddgrnsummary(string user_gid, string purchaseorder_gid, MdlPmrTrnGrn values)
        {
            try
            {
                
                msSQL = " delete from pmr_tmp_tgrn where user_gid = '" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = " select a.product_gid,a.producttype_gid, a.uom_gid, a.purchaseorderdtl_gid, a.qty_ordered, a.qty_received, a.qty_grnadjusted, " +
                        " (a.qty_ordered - (a.qty_received + a.qty_grnadjusted)) as qty_delivered, " +
                        " a.purchaseorder_gid, a.product_price, a.display_field_name, a.product_name, a.product_code, a.productuom_name,i.productgroup_name, " +
                        " d.purchaseorder_status " +
                        " from pmr_trn_tpurchaseorderdtl a " +
                        " left join pmr_mst_tproduct b on a.product_gid = b.product_gid " +
                        " left join pmr_mst_tproductuom c on c.productuom_gid = b.productuom_gid " +
                        " left join pmr_mst_tproductgroup i on i.productgroup_gid = b.productgroup_gid " +
                        " left join pmr_trn_tpurchaseorder d on d.purchaseorder_gid = a.purchaseorder_gid " +
                          "WHERE a.purchaseorder_gid = '" + purchaseorder_gid + "'and a.qty_ordered <> a.qty_received order by a.purchaseorder_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<addgrn_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new addgrn_list
                        {

                            productgroup_name = dt["productgroup_name"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            qty_ordered = dt["qty_ordered"].ToString(),
                            qty_received = dt["qty_received"].ToString(),
                            //qty_free = dt["qty_free"].ToString(),
                            qty_grnadjusted = dt["qty_grnadjusted"].ToString(),
                            qty_delivered = dt["qty_delivered"].ToString(),
                        });
                        values.addgrn_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting add GRN Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                   $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                   ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                   msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                   DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        public void DaPostGrnSubmit(string user_gid, addgrn_lists values)
        {
            try
            {
                 
                foreach (var data in values.summary_list)
                {
                    lsQty_Delivered = (int)Convert.ToDouble(data.qty_delivered);
                    lsQty_ReceivedAS = (int)Convert.ToDouble(data.qtyreceivedas);
                    lsQty_Adjustable = (int)Convert.ToDouble(data.qty_grnadjusted);
                    lsQty_Ordered = (int)Convert.ToDouble(data.qty_ordered);

                    if (lsQty_Ordered < lsQty_ReceivedAS)
                    {
                        values.message = "Sum of Qty Received and Qty Received as per Invoice should not be greater than Qty Ordered";
                        values.status = false;
                        return;
                    }

                    msSQL = "select product_gid from pmr_mst_tproduct where product_name='" + data.product_name + "' ";
                    string lsproductgid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select purchaseorderdtl_gid from pmr_trn_tpurchaseorderdtl where product_gid='" + lsproductgid + "' AND purchaseorder_gid='" + values.purchaseorder_gid + "' ";
                    string lspurchaseorderdtlgid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select producttype_gid from pmr_mst_tproduct where  product_gid='" + lsproductgid + "' ";
                    string lsproducttypegid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select a.product_name, a.product_code, a.productuom_name from pmr_trn_tpurchaseorderdtl a  " +
                            " where a.purchaseorderdtl_gid = '" + lspurchaseorderdtlgid + "' and product_gid = '" + lsproductgid + "' ";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader.HasRows)
                    {
                        objOdbcDataReader.Read();
                        lsproductname = objOdbcDataReader["product_name"].ToString();
                        lsproductcode = objOdbcDataReader["product_code"].ToString();
                        lsproductuomname = objOdbcDataReader["productuom_name"].ToString();
                        objOdbcDataReader.Close();
                    }

                    msGetGID = objcmnfunctions.GetMasterGID("PGDC");


                    msSQL = " insert into pmr_trn_tgrndtl (" +
                             " grndtl_gid, " +
                             " grn_gid, " +
                             " purchaseorderdtl_gid, " +
                             " product_gid," +
                             " product_code," +
                             " product_name," +
                             " productuom_name," +
                             " qty_delivered," +
                             " qtyreceivedas," +
                             " producttype_gid, " +
                             " qty_grnadjusted) " +
                             " values (" +
                             "'" + msGetGID + "', " +
                             "'" + values.grn_gid + "', " +
                             "'" + lspurchaseorderdtlgid + "', " +
                             "'" + lsproductgid + "'," +
                             "'" + lsproductcode + "'," +
                             "'" + data.product_name + "', " +
                             "'" + data.productuom_name + "', " +
                             "'" + lsQty_Delivered + "'," +
                             "'" + lsQty_ReceivedAS + "'," +
                             "'" + lsproducttypegid + "'," +
                             "'0')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //msSQL = " Update pmr_trn_tpurchaseorderdtl " +
                    //                     " Set qty_received = '" + lsQty_ReceivedAS + "'," +
                    //                    "  qty_grnadjusted = '" + data.qty_adjustable + "'" +
                    //                     "where purchaseorderdtl_gid = '" + lspurchaseorderdtlgid + "'" +
                    //                     " AND product_gid = '" + lsproductgid + "'";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    double lsSum_Rec = data.qtyreceivedas;
                    double lsSumPOt_GRNAdj = data.qty_grnadjusted;
                    msSQL = " select qty_received, qty_grnadjusted " +
                            " from pmr_trn_tpurchaseorderdtl  where " +
                            " purchaseorderdtl_gid = '" + lspurchaseorderdtlgid + "' and " +
                            " product_gid = '" + lsproductgid + "' ";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader.HasRows)
                    {
                        lsSum_Rec = lsSum_Rec + double.Parse(objOdbcDataReader["qty_received"].ToString());
                        lsSumPOt_GRNAdj = lsSumPOt_GRNAdj + double.Parse(objOdbcDataReader["qty_grnadjusted"].ToString());
                    }
                    msSQL = "UPDATE pmr_trn_tpurchaseorderdtl " +
                            "SET qty_received = '" + lsSum_Rec + "', " +
                            "qty_grnadjusted = '" + lsSumPOt_GRNAdj + "'" +
                            "WHERE purchaseorderdtl_gid = '" + lspurchaseorderdtlgid + "' AND " +
                            "product_gid = '" + lsproductgid + "'  ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 0)
                    {
                        values.status = false;
                        values.message = "Error occured while inserting into Purchaseorder table";
                    }
                    else
                    {
                        lspurchaseorder_status = "PO Completed";
                        lstPO_GRN_flag = "GRN Pending";
                    }

                    msSQL = " SELECT qty_received, qty_grnadjusted, qty_ordered " +
                            " FROM pmr_trn_tpurchaseorderdtl WHERE " +
                            " purchaseorder_gid = '" + values.purchaseorder_gid + "' AND " +
                            " (qty_received + qty_grnadjusted) < qty_ordered";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader.HasRows == true)
                    {
                        lspurchaseorder_status = "PO Work In Progress";
                        lstPO_GRN_flag = "Goods Received Partial";

                    }
                    else
                    {
                        lspurchaseorder_status = "PO Completed";
                        lstPO_GRN_flag = "Goods Received";
                    }



                    msSQL = " Update pmr_trn_tpurchaseorder " +
                                           " Set purchaseorder_status = '" + lspurchaseorder_status + "'," +
                                           " grn_flag = '" + lstPO_GRN_flag + "'" +
                                           " where purchaseorder_gid = '" + values.purchaseorder_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 0)
                    {
                        values.status = false;
                        lsgrn_status = "GRN Pending";
                        values.message = "Error occured while inserting into Purchaseorder table";
                    }

                    msSQL = "select purchaserequisition_gid from pmr_trn_tpurchaseorder where purchaseorder_gid='" + values.purchaseorder_gid + "' ";
                    string lspurchaserequisition_gid = objdbconn.GetExecuteScalar(msSQL);
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    {
                        objOdbcDataReader.Read();
                        lspurchaserequisition_gid = objOdbcDataReader["purchaserequisition_gid"].ToString();
                        objOdbcDataReader.Close();
                    }

                    lsPR_Rec = lsQty_ReceivedAS;
                    lsPRt_GRNAdj = lsQty_Adjustable;

                    msSQL = " select qty_received, qty_grnadjusted " +
                                    " from pmr_trn_tpurchaserequisitiondtl where " +
                                    " purchaserequisition_gid = '" + lspurchaserequisition_gid + "' and " +
                                    " product_gid = '" + lsproductgid + "'";

                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);

                    if (objOdbcDataReader.HasRows)
                    {
                        objOdbcDataReader.Read();
                        lsPR_Rec = lsPR_Rec + int.Parse(objOdbcDataReader["qty_received"].ToString());
                        lsPRt_GRNAdj = lsPRt_GRNAdj + int.Parse(objOdbcDataReader["qty_grnadjusted"].ToString());
                        objOdbcDataReader.Close();
                    }
                    msSQL = " update pmr_trn_tpurchaserequisitiondtl set " +
                                    " qty_received = '" + lsPR_Rec + "'," +
                                    " qty_grnadjusted = '" + lsPRt_GRNAdj + "'" +
                                    " where purchaserequisition_gid = '" + lspurchaserequisition_gid + "' and " +
                                    " product_gid = '" + lsproductgid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 0)
                    {
                        values.status = false;
                        values.message = "Error occured while inserting into Purchase Requisition Detail table";
                    }
                    msSQL = " select qty_received, qty_grnadjusted, qty_requested " +
                                    " from pmr_trn_tpurchaserequisitiondtl  where " +
                                    " purchaserequisition_gid = '" + lspurchaserequisition_gid + "'and" +
                                    " (qty_received + qty_grnadjusted) < qty_requested ";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);

                    if (objOdbcDataReader.HasRows)
                    {
                        lstPR_GRN_flag = "Goods Received Partial";
                    }
                    else
                    {
                        lstPR_GRN_flag = "Goods Received";
                    }
                    msSQL = " Update pmr_trn_tpurchaserequisition " +
                                    " Set grn_flag = '" + lstPR_GRN_flag + "'" +
                                    " where purchaserequisition_gid = '" + lspurchaserequisition_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 0)
                    {
                        values.status = false;
                        values.message = "Error occured while inserting into Purchase Requisition Table";
                    }
                    msSQL = " select branch_gid,vendor_gid from pmr_trn_tpurchaseorder where purchaseorder_gid = '" + values.purchaseorder_gid + "'";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader.HasRows)
                    {
                        objOdbcDataReader.Read();
                        lblpurchasebranch_gid = objOdbcDataReader["branch_gid"].ToString();
                        lblVendor_gid = objOdbcDataReader["vendor_gid"].ToString();
                        objOdbcDataReader.Close();
                    }
                    //DateTime? dcDate = null;
                    //if (!string.IsNullOrWhiteSpace(values.dc_date) && DateTime.TryParse(values.dc_date, out DateTime parsedDcDate))
                    //{
                    //    dcDate = parsedDcDate;
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Error parsing dc_date: Invalid format or empty");

                    //    dcDate = DateTime.MinValue;
                    //}


                    //DateTime? grnDate = null;
                    //if (!string.IsNullOrWhiteSpace(values.grn_date) && DateTime.TryParse(values.grn_date, out DateTime parsedGrnDate))
                    //{
                    //    grnDate = parsedGrnDate;
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Error parsing grn_date: Invalid format or empty");

                    //    grnDate = null;
                    //}

                    //DateTime? invDate = null;
                    //if (!string.IsNullOrWhiteSpace(values.invoice_date) && DateTime.TryParse(values.invoice_date, out DateTime parsedInvDate))
                    //{
                    //    invDate = parsedInvDate;
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Error parsing invoice_date: Invalid format or empty");

                    //    invDate = DateTime.MinValue;
                    //}
                    ////    "'" + (grnDate.HasValue ? grnDate.Value.ToString("yyyy-MM-dd") : string.Empty) + "', " +
                    ////"'" + (invDate.HasValue ? invDate.Value.ToString("yyyy-MM-dd") : string.Empty) + "', " +
                    ////          "'" + (dcDate.HasValue ? dcDate.Value.ToString("yyyy-MM-dd") : string.Empty) + "')";
                    ///
                    string uiDateStr = values.grn_date;
                    DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string grn_date = uiDate.ToString("yyyy-MM-dd");
                    string uiDateStr1 = values.dc_date;
                    DateTime uiDate1 = DateTime.ParseExact(uiDateStr1, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string Dc_date = uiDate1.ToString("yyyy-MM-dd");
                    string uiDateStr2 = values.invoice_date;
                    DateTime uiDate2 = DateTime.ParseExact(uiDateStr2, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string Invoice_date = uiDate1.ToString("yyyy-MM-dd");


                    msSQL = "INSERT INTO pmr_trn_tgrn (" +
                            "grn_gid, " +
                            "branch_gid, " +
                            "purchaseorder_gid, " +
                            "grn_date, " +
                            "vendor_gid, " +
                            "vendor_contact_person, " +
                            "dc_no, " +
                            "invoice_refno, " +
                            "grn_status, " +
                            "grn_flag, " +
                            "grn_remarks, " +
                            "priority, " +
                            "checkeruser_gid, " +
                            "user_gid, " +
                            "created_date, " +
                            "currency_code, " +
                            "invoice_date, " +
                            "dc_date) " +
                            "VALUES (" +
                            "'" + values.grn_gid + "', " +
                            "'" + lblpurchasebranch_gid + "', " +
                            "'" + values.purchaseorder_gid + "', " +
                            "'" + grn_date + "', " +
                            "'" + lblVendor_gid + "', " +
                            "'" + values.contactperson_name + "', " +
                            "'" + values.dc_no + "', " +
                            "'" + values.invoiceref_no + "', " +
                            "'GRN Pending', " +
                            "'GRN Pending QC', " +
                            "'" + values.grn_remarks + "', " +
                             "'" + values.priority_flag + "', " +
                            "'" + user_gid + "', " +
                            "'" + user_gid + "', " +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                            "'INR', " +
                            "'" + Invoice_date + "', " +
                            "'" + Dc_date + "')";


                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 0)
                    {
                        values.status = false;
                    }

                    msSQL = "select branch_gid from hrm_mst_tbranch where branch_name='" + values.branch_name + "' ";
                    string lsbranchgid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select productuom_gid from pmr_mst_tproductuom where productuom_name='" + data.productuom_name + "' ";
                    string lsproductuomgid = objdbconn.GetExecuteScalar(msSQL);

                    msGetStockGID = objcmnfunctions.GetMasterGID("ISKP");

                    msSQL = "INSERT INTO ims_trn_tstock (" +
                            "stock_gid, " +
                            "branch_gid, " +
                            "product_gid, " +
                            "uom_gid, " +
                            "stock_qty, " +
                            "grn_qty, " +
                            "rejected_qty, " +
                            "stocktype_gid, " +
                            "reference_gid, " +
                            "stock_flag, " +
                            "adjusted_qty) " +
                            "VALUES (" +
                            "'" + msGetStockGID + "', " +
                            "'" + lsbranchgid + "', " +
                            "'" + lsproductgid + "', " +
                            "'" + lsproductuomgid + "', " +
                            "'0', " +
                            "'0', " +
                            "'0', " +
                            "'SY0905270002', " +
                            "'" + values.grn_gid + "', " +
                            "'Y', " +
                            "'0')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 0)
                    {
                        values.status = false;
                    }


                    msSQL = "select employee_gid from hrm_mst_temployee where user_gid =  '" + user_gid + "'";
                    string employee_gid = objdbconn.GetExecuteScalar(msSQL);

                    msGetGID = objcmnfunctions.GetMasterGID("PODC");

                    msSQL = "insert into pmr_trn_tapproval ( " +
                                " approval_gid, " +
                                " approved_by, " +
                                " approved_date, " +
                                " submodule_gid, " +
                                " grnapproval_gid " +
                                " ) values ( " +
                                "'" + msGetGID + "'," +
                                " '" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                "'PMRSTKGRA'," +
                                "'" + values.grn_gid + "') ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 0)
                    {
                        values.status = false;
                    }

                    msSQL = " Delete from pmr_tmp_tgrn where user_gid = '" + user_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //msSQL = " Delete from pmr_tmp_tgrnsplit where user_gid = '" + user_gid + "'";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //for (int i = 0; i < values.summary_list.ToArray().Length; i++)
                    //{

                    //    msSQL = "select approval_flag from pmr_trn_tapproval where submodule_gid='PMRSTKGRA' and grnapproval_gid='" + values.grn_gid + "'";
                    //    objOdbcDataReader = objdbconn.GetDataReader(msSQL);

                    //    if (objOdbcDataReader.HasRows == true)
                    //    {
                    //        msSQL = " Update pmr_trn_tgrn " +
                    //            " Set " +
                    //            " grn_status = 'GRN Approved', " +
                    //            " grn_flag = 'Invoice Pending', " +
                    //            " invoice_status = 'IV Pending', " +
                    //            " approved_by = '" + user_gid + "', " +
                    //            " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                    //            " where grn_gid = '" + values.grn_gid + "'";
                    //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //        if (mnResult == 0)
                    //        {
                    //            values.status = false;
                    //        }
                    //        else
                    //        {

                    //            msSQL = " select a.grn_gid, b.product_gid, b.product_remarks, " +
                    //               " (b.qty_delivered - b.qty_rejected) as qty_accepted, " +
                    //               " b.qty_delivered, b.qty_billed, b.qty_excess " +
                    //               " from pmr_trn_tgrn a " +
                    //               " left join pmr_trn_tgrndtl b on a.grn_gid = b.grn_gid " +
                    //               " where a.grn_gid = '" + values.grn_gid + "'";
                    //            objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    //            if (objOdbcDataReader.HasRows)
                    //            {
                    //                objOdbcDataReader.Read();
                    //                lsqty_billed = objOdbcDataReader["qty_accepted"].ToString();
                    //                lsgrn_gid = objOdbcDataReader["grn_gid"].ToString();

                    //            }


                    //            msSQL = " update ims_trn_tstock set " +
                    //                    " stock_flag = 'Y'" +
                    //                    " where reference_gid = '" + lsgrn_gid + "' and " +
                    //                    " product_gid = '" + lsproductgid + "'";
                    //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //            msSQL = " update ims_trn_tstocktracker set " +
                    //                    " stock_flag = 'Y'" +
                    //                    " where reference_gid = '" + lsgrn_gid + "' and " +
                    //                    " product_gid = '" + lsproductgid + "'";
                    //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //            if (mnResult == 0)
                    //            {
                    //                values.status = false;
                    //            }
                    //            if (objOdbcDataReader.HasRows == true)
                    //            {
                    //                msSQL = "select approved_by from pmr_trn_tapproval where submodule_gid='PMRSTKGRA' and grnapproval_gid='" + lsgrn_gid + "'";
                    //                objOdbcDataReader1 = objdbconn.GetDataReader(msSQL);

                    //                if (objOdbcDataReader1.RecordsAffected == 1)
                    //                {
                    //                    objOdbcDataReader1.Read();
                    //                    if (objOdbcDataReader1["approved_by"].ToString() == employee_gid)
                    //                    {
                    //                        msSQL = " update pmr_trn_tapproval set " +
                    //                               " approval_flag = 'Y', " +
                    //                               " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                    //                               " where approved_by = '" + employee_gid + "'" +
                    //                               " and grnapproval_gid = '" + lsgrn_gid + "' and submodule_gid='PMRSTKGRA'";
                    //                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    //                        msSQL = " Update pmr_trn_tgrn " +
                    //                                 " Set " +
                    //                                 " grn_status = 'GRN Approved', " +
                    //                                 " grn_flag = 'Invoice Pending', " +
                    //                                 " invoice_status = 'IV Pending', " +
                    //                                 " approved_by = '" + user_gid + "', " +
                    //                                 " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                    //                                 " where grn_gid = '" + lsgrn_gid + "'";
                    //                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                    //                        msSQL = " select a.grn_gid, b.product_gid, b.product_remarks, " +
                    //                                " (b.qty_delivered - b.qty_rejected) as qty_accepted, " +
                    //                                " b.qty_delivered, b.qty_billed, b.qty_excess " +
                    //                                " from pmr_trn_tgrn a " +
                    //                                " left join pmr_trn_tgrndtl b on a.grn_gid = b.grn_gid " +
                    //                                " where a.grn_gid = '" + values.grn_gid + "'";
                    //                        objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    //                        if (objOdbcDataReader.HasRows)
                    //                        {
                    //                            objOdbcDataReader.Read();
                    //                            lsqty_billed = objOdbcDataReader["qty_accepted"].ToString();
                    //                            lsgrn_gid = objOdbcDataReader["grn_gid"].ToString();

                    //                        }

                    //                        msSQL = " update ims_trn_tstock set " +
                    //                        " stock_flag = 'Y'" +
                    //                        " where reference_gid = '" + values.grn_gid + "' and " +
                    //                        " product_gid = '" + lsproductgid + "'";
                    //                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    //                        msSQL = " update ims_trn_tstocktracker set " +
                    //                        " stock_flag = 'Y'" +
                    //                        " where reference_gid = '" + values.grn_gid + "' and " +
                    //                        " product_gid = '" + lsproductgid + "'";
                    //                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //                        if (mnResult == 0)
                    //                        {
                    //                            values.status = false;
                    //                        }
                    //                        if (objOdbcDataReader1.RecordsAffected >= 1)
                    //                        {
                    //                            msSQL = " update pmr_trn_tapproval set " +
                    //                                   " approval_flag = 'Y', " +
                    //                                   " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                    //                                   " where approved_by = '" + employee_gid + "'" +
                    //                                   " and grnapproval_gid = '" + values.grn_gid + "' and submodule_gid='PMRSTKGRA'";
                    //                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //                        }
                    //                        else
                    //                        {
                    //                            values.status = false;
                    //                        }
                    //                    }
                    //                    objOdbcDataReader1.Close();

                    //                }
                    //            }
                    //            else
                    //            {
                    //                values.status = false;
                    //            }
                    //        }
                    //    }


                    //}

                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "GRN Raised Successfully";

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Raising GRN";
                    }

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Raising GRN!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                   $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                   ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                   msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                   DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

    }
}