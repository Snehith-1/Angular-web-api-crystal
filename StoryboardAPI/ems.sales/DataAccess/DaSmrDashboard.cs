using ems.sales.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;


namespace ems.sales.DataAccess
{
    public class DaSmrDashboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msGetGid;
        int mnResult, mnResult1;
        string lsemployee_gid_list;

        // Recursive Looping ChildLoop Function to get Employee GID Hierarchywise

        public void DaGetChildLoop(string employee_gid, MdlSmrDashboard values)
        {
            try
            {
                
                msSQL = " select a.employee_gid, concat(b.user_firstname, '-', b.user_code) as user  from adm_mst_tmodule2employee a  " +
                 " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid   " +
                 " inner join adm_mst_tuser b on c.user_gid = b.user_gid   " +
                 " where a.module_gid = 'MKT'  " +
                 " and a.employeereporting_to = '" + employee_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var SalesPerformanceChart_List = new List<GetSalesPerformanceChart_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string lsemployee_gid = dt["employee_gid"].ToString();
                        msSQL = " select a.*, b.user_gid,c.employee_gid  from adm_mst_tmodule2employee a  " +
                    " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " +
                    " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                    " where a.module_gid = 'MKT' " +
                    " and a.employee_gid ='" + lsemployee_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count != 0)
                        {
                            lsemployee_gid_list = lsemployee_gid + "," + dt["employee_gid"].ToString() + ",";
                            //SalesPerformanceChart_List.Add(new GetSalesPerformanceChart_List
                            //{
                            //    lsemployee_gid_list = lsemployee_gid + (dt["employee_gid"].ToString()) +",",
                            //});
                            //values.GetSalesPerformanceChart_List = SalesPerformanceChart_List;
                        }
                        DaGetChildLoop(lsemployee_gid, values);
                    }

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Sales Performance Chart !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // SalesPerformanceChart
        public void DaGetSalesPerformanceChart(string employee_gid, string user_gid, MdlSmrDashboard values)
        {
            try
            {
                
                msSQL = " select sum(a.grandtotal) as order_amount,sum(a.received_amount) as payment_amount, sum(a.invoice_amount) as invoice_amount, " +
                     " cast(MONTHNAME(a.salesorder_date) as char) as orderdate, " +
                     " sum(a.invoice_amount-a.received_amount)  as outstanding_amount " +
                     " from smr_trn_tsalesorder a " +
                     " where a.salesorder_status in ('Delivery Completed','Approved') " +
                     " and created_by in ( '" + user_gid + "') " +
                     " and a.salesorder_date BETWEEN CURDATE() - INTERVAL 6 MONTH AND CURDATE() " +
                     " group by MONTH(salesorder_date) desc order by a.salesorder_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var SalesPerformanceChart_List = new List<GetSalesPerformanceChart_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        SalesPerformanceChart_List.Add(new GetSalesPerformanceChart_List
                        {
                            order_amount = (dt["order_amount"].ToString()),
                            payment_amount = (dt["payment_amount"].ToString()),
                            invoice_amount = (dt["invoice_amount"].ToString()),
                            orderdate = (dt["orderdate"].ToString()),
                            outstanding_amount = (dt["outstanding_amount"].ToString()),

                        });
                        values.GetSalesPerformanceChart_List = SalesPerformanceChart_List;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Sales Performance Chart !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }


        // GetSalesOrderCount
        public void DaGetSalesOrderCount(string employee_gid, string user_gid, MdlSmrDashboard values)
        {
            try
            {
                
                msSQL = " SELECT (SELECT COUNT(salesorder_gid)FROM smr_trn_tsalesorder) as total_so,  " +

                    " (SELECT COUNT(*) AS approved_so FROM smr_trn_tsalesorder c WHERE c.salesorder_status = 'Approved') AS approved_so, " +

                    "(SELECT COUNT(*)FROM smr_trn_tsalesorder d WHERE d.salesorder_status = 'SO Amended') AS pending_So, " +

                    " (SELECT COUNT(*) FROM smr_trn_tsalesorder f WHERE f.salesorder_status = 'Cancelled') AS rejected_so," +

                    " (SELECT COUNT(directorder_gid) FROM smr_trn_tdeliveryorder)  as total_do , " +

                    " (SELECT COUNT(*) FROM smr_trn_tdeliveryorder  WHERE delivery_status = 'Delivery Pending') AS pending_do, " +

                    "(SELECT COUNT(*) FROM smr_trn_tdeliveryorder  WHERE delivery_status = 'Delivery Completed') AS completed_do, " +

                    " (SELECT COUNT(*) FROM smr_trn_tdeliveryorder  WHERE  delivery_status = 'Delivery Partial Done') AS Partial_done, " +


                    " (SELECT COUNT(quotation_gid) FROM smr_trn_treceivequotation) AS total_quotation, " +

                    " (SELECT COUNT(*) FROM smr_trn_treceivequotation b WHERE b.quotation_status = 'Quotation Amended') AS quotation_amended, " +

                    " (SELECT COUNT(*) FROM smr_trn_tsalesdelete WHERE record_reference = 'Quotation') AS quotation_cancelled, " +

                   " (SELECT COUNT(invoice_gid)  FROM rbl_trn_tinvoice)  as totalinvoice ,   " +

                   " (SELECT COUNT(*) as invoice_count FROM rbl_trn_tinvoice WHERE DATE(invoice_date) = CURDATE()) as today_invoice,   " +

                   " (SELECT COUNT(*) FROM rbl_trn_tinvoice c WHERE c.invoice_flag = 'Invoice Approved') AS aproved_invoice, " +

                   " (SELECT COUNT(*) FROM rbl_trn_tinvoice c WHERE c.invoice_flag = 'Invoice Pending') AS pending_invoice, " +

                   " (SELECT COUNT(invoice_gid) FROM rbl_trn_tpayment) as totalpayment ,   " +

                   " (SELECT COUNT(*) FROM rbl_trn_tpayment c WHERE c.approval_status = 'Payment Done') AS payment_completed, " +

                   " (SELECT COUNT(*) FROM rbl_trn_tpayment c WHERE c.approval_status = 'Payment done Partial') AS payment_don_partial, " +

                    " (SELECT COUNT(*) FROM rbl_trn_tinvoice c WHERE c.payment_amount = 0) AS payment_pending " +

                  " FROM smr_trn_tsalesorder a " +

                  " LEFT JOIN rbl_trn_tinvoice b ON b.customer_gid = a.customer_gid " +

                  " WHERE a.salesorder_status<> 'SO Amended' " +

                  " GROUP BY salesorder_gid ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var GetSalesOrderCount_List = new List<GetSalesOrderCount_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        GetSalesOrderCount_List.Add(new GetSalesOrderCount_List
                        {
                            total_so = (dt["total_so"].ToString()),
                            approved_so = (dt["approved_so"].ToString()),
                            pending_So = (dt["pending_So"].ToString()),
                            rejected_so = (dt["rejected_so"].ToString()),
                            total_do = (dt["total_do"].ToString()),
                            pending_do = (dt["pending_do"].ToString()),
                            completed_do = (dt["completed_do"].ToString()),
                            Partial_done = (dt["Partial_done"].ToString()),
                            total_quotation = (dt["total_quotation"].ToString()),
                            quotation_canceled = (dt["quotation_amended"].ToString()),
                            quotation_completed = (dt["quotation_cancelled"].ToString()),
                            totalinvoice = (dt["totalinvoice"].ToString()),
                            today_invoice = (dt["today_invoice"].ToString()),
                            aproved_invoice = (dt["aproved_invoice"].ToString()),
                            pending_invoice = (dt["pending_invoice"].ToString()),
                            totalpayment = (dt["totalpayment"].ToString()),
                            payment_completed = (dt["payment_completed"].ToString()),
                            payment_don_partial = (dt["payment_don_partial"].ToString()),
                            payment_pending = (dt["payment_pending"].ToString()),


                        });
                        values.GetSalesOrderCount_List = GetSalesOrderCount_List;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting  SalesOrder Count !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }

        // GetSalesMoreOrderCount
        public void DaGetMoreSalesOrderCount(string employee_gid, string user_gid, MdlSmrDashboard values)
        {
            try
            {
               
                msSQL = " SELECT count(invoice_gid) as totalinvoice FROM rbl_trn_tinvoice where invoice_flag <> 'Invoice Cancelled' and user_gid in ( '" + user_gid + "') ";
                values.totalinvoice = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT count(invoice_gid) as approval_pending FROM rbl_trn_tinvoice where invoice_flag = 'Invoice Approved' and user_gid in ( '" + user_gid + "') ";
                values.approval_pending = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select count(invoice_gid) as payment_pending from rbl_trn_tinvoice where  payment_amount='0.00' and user_gid in ( '" + user_gid + "') ";
                values.payment_pending = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT count(invoice_gid) as totalinvoice FROM rbl_trn_tinvoice where invoice_flag = 'Invoice Approval Pending' and user_gid in ( '" + user_gid + "') ";
                values.approvalpendinginnvoice = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select count(lead2campaign_gid) as potentialleadcount from crm_trn_tenquiry2campaign where leadstage_gid='5' and created_by in ( ( '" + employee_gid + "') ";
                values.potentialleadcount = objdbconn.GetExecuteScalar(msSQL);
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting  SalesOrder Count !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        // GetOwnOverallSalesOrderChart
        public void DaGetOwnOverallSalesOrderChart(string employee_gid, string user_gid, MdlSmrDashboard values)
        {
            try
            {
               
                msSQL = " select count(*) as count_own,salesorder_status as status from smr_trn_tsalesorder  " +
                    // " where created_by='" + employee_gid + "' and so_type='Sales' " +
                    " group by salesorder_status ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var OverallSalesOrderChart_List = new List<GetOverallSalesOrderChart_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        OverallSalesOrderChart_List.Add(new GetOverallSalesOrderChart_List
                        {
                            count_own = (dt["count_own"].ToString()),
                            salesorder_status_own = (dt["status"].ToString()),

                        });
                        values.GetOverallSalesOrderChart_List = OverallSalesOrderChart_List;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting  SalesOrder Count !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        // GetHierarchyOverallSalesOrderChart
        public void DaGetHierarchyOverallSalesOrderChart(string employee_gid, string user_gid, MdlSmrDashboard values)
        {
            try {
                

                msSQL = " select count(*) as count_Hierarchy,salesorder_status_Hierarchy as status from smr_trn_tsalesorder  " +
                //  " where created_by in ('" + employee_gid + "') and so_type='Sales' " +
                " group by salesorder_status ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var OverallSalesOrderChart_List = new List<GetOverallSalesOrderChart_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    OverallSalesOrderChart_List.Add(new GetOverallSalesOrderChart_List
                    {
                        count_Hierarchy = (dt["count_Hierarchy"].ToString()),
                        salesorder_status_Hierarchy = (dt["salesorder_status_Hierarchy"].ToString()),

                    });
                    values.GetOverallSalesOrderChart_List = OverallSalesOrderChart_List;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting OverallDeliveryOrder Chart !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        // GetOwnOverallDeliveryOrderChart
        public void DaGetOwnOverallDeliveryOrderChart(string employee_gid, string user_gid, MdlSmrDashboard values)
        {
            try
            {
               
                msSQL = " select count(*) as do_count_own,delivery_status as delivery_status_own from smr_trn_tdeliveryorder  " +
                    //  " where created_name='" + employee_gid + "' " +
                    " group by delivery_status ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var OverallSalesOrderChart_List = new List<GetOverallSalesOrderChart_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        OverallSalesOrderChart_List.Add(new GetOverallSalesOrderChart_List
                        {
                            do_count_own = (dt["do_count_own"].ToString()),
                            delivery_status_own = (dt["delivery_status_own"].ToString()),

                        });
                        values.GetOverallSalesOrderChart_List = OverallSalesOrderChart_List;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting OverallDeliveryOrder Chart !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        // GetHierarchyOverallDeliveryOrderChart
        public void DaGetHierarchyOverallDeliveryOrderChart(string employee_gid, string user_gid, MdlSmrDashboard values)
        {
            try
            {
                
                msSQL = " select count(*) as count,delivery_status as delivery_status_own from smr_trn_tdeliveryorder where " +
                    " created_name in ('" + employee_gid + "') " +
                    " group by delivery_status ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var OverallSalesOrderChart_List = new List<GetOverallSalesOrderChart_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        OverallSalesOrderChart_List.Add(new GetOverallSalesOrderChart_List
                        {
                            do_count_Hierarchy = (dt["do_count_Hierarchy"].ToString()),
                            delivery_status_Hierarchy = (dt["delivery_status_own"].ToString()),

                        });
                        values.GetOverallSalesOrderChart_List = OverallSalesOrderChart_List;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured Overall DeliveryOrderChart !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // GetMonthlySalesPipelineChart
        public void DaGetMonthlySalesPipelineChart(string employee_gid, string user_gid, MdlSmrDashboard values)
        {
            try
            {
               
                msSQL = " SELECT cast(concat(monthname(payment_date),'-',year(payment_date)) as char) as payment_day,  " +
                    "(sum(amount)*exchange_rate) as amount " +
                    " FROM rbl_trn_tpayment group by YEAR(payment_date),MONTH(payment_date) ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var OverallSalesOrderChart_List = new List<GetOverallSalesOrderChart_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        OverallSalesOrderChart_List.Add(new GetOverallSalesOrderChart_List
                        {
                            payment_day = (dt["payment_day"].ToString()),
                            amount = (dt["amount"].ToString()),

                        });
                        values.GetOverallSalesOrderChart_List = OverallSalesOrderChart_List;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Monthly Sales Chart !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        public void DaGetMTDCounts(MdlSmrDashboard values)
        {
            try
            {
               
                msSQL = "  select ifnull(format(sum(total_amount),2),'0.00') as mtd_over_due_payment from rbl_trn_tpayment  where payment_date >= DATE_FORMAT(curdate(), '%Y-%m-01') and payment_date <= DATE_FORMAT(curdate(), '%Y-%m-%d')  ";

                values.mtd_over_due_payment = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "  SELECT IFNULL(COUNT(*), 0) AS mtd_over_due_payment_amount FROM rbl_trn_tpayment WHERE payment_date >= DATE_FORMAT(CURDATE(), '%Y-%m-01') AND payment_date <= CURDATE() ";
                values.mtd_over_due_payment_amount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select ifnull(format(sum(invoice_amount),2),'0.00') as mtd_over_due_invoice_amount from rbl_trn_tinvoice  where invoice_date >= DATE_FORMAT(curdate(), '%Y-%m-01') and invoice_date <= DATE_FORMAT(curdate(), '%Y-%m-%d') ";
                values.mtd_over_due_invoice_amount = objdbconn.GetExecuteScalar(msSQL);
                 
                msSQL = " SELECT IFNULL(COUNT(*), 0) AS mtd_over_due_invoice FROM rbl_trn_tinvoice WHERE invoice_date >= DATE_FORMAT(CURDATE(), '%Y-%m-01') AND invoice_date <= CURDATE() ";
                string mtd_over_due_invoice = objdbconn.GetExecuteScalar(msSQL);
                values.mtd_over_due_invoice = mtd_over_due_invoice;
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting MTD Counts !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }
        public void DaGetYTDCounts(MdlSmrDashboard values)
        {
            try
            {
               
                msSQL = "   SELECT IFNULL(COUNT(*), 0) AS ytd_over_due_payment FROM rbl_trn_tpayment WHERE YEAR(payment_date) = YEAR(CURDATE())";
                values.ytd_over_due_payment = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "  SELECT ifnull(format(sum(total_amount),2),'0.00')  as ytd_over_due_payment_amount  FROM rbl_trn_tpayment WHERE YEAR(payment_date) = YEAR(curdate()) ";
                values.ytd_over_due_payment_amount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " SELECT IFNULL(COUNT(*), 0) AS ytd_over_due_invoice FROM rbl_trn_tinvoice WHERE YEAR(invoice_date) = YEAR(CURDATE())";
                values.ytd_over_due_invoice_amount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "  SELECT ifnull(format(sum(invoice_amount),2),'0.00')  as ytd_over_due_invoice_amount  FROM rbl_trn_tinvoice WHERE YEAR(invoice_date) = YEAR(curdate()) ";
                values.ytd_over_due_invoice = objdbconn.GetExecuteScalar(msSQL);
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting YTD Counts !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }

        public void DaGetMonthlySalesChart(string employee_gid, string user_gid, MdlSmrDashboard values)
        {
            try
            {
                
                msSQL = " SELECT DATE_FORMAT(payment_date, '%b-%Y')  as payment_day, " +
                    " (sum(amount)*exchange_rate) as amount " +
                    " FROM rbl_trn_tpayment group by YEAR(payment_date),MONTH(payment_date) ORDER BY payment_date DESC LIMIT 12";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var SalesPerformanceChart_List = new List<GetSalesPerformanceChart_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        SalesPerformanceChart_List.Add(new GetSalesPerformanceChart_List
                        {
                            payment_day = (dt["payment_day"].ToString()),
                            amount = (dt["amount"].ToString()),


                        });
                        values.GetSalesPerformanceChart_List = SalesPerformanceChart_List;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Monthly SalesChart !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }
    }

    
}