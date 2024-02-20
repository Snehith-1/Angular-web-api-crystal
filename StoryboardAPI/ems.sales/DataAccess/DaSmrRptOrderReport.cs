﻿using ems.sales.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Runtime.Remoting;
using System.Drawing;

namespace ems.sales.DataAccess
{
    public class DaSmrRptOrderReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        DataTable dt_datatable1;
        string msGetGid;
        int mnResult, mnResult1;


        // GetOrderForLastSixMonths
        public void DaGetOrderForLastSixMonths(string employee_gid, MdlSmrRptOrderReport values)
       {
            try
            {
                
                msSQL = " select DATE_FORMAT(salesorder_date, '%b-%Y')  as salesorder_date,substring(date_format(a.salesorder_date,'%M'),1,3)as month,a.salesorder_gid,year(a.salesorder_date) as year, " +
                 " round(sum(a.grandtotal),2)as amount,count(a.salesorder_gid)as ordercount    " +
                 " from smr_trn_tsalesorder a   " +
                 " where a.salesorder_date > date_add(now(), interval-6 month) and a.salesorder_date<=date(now())   " +
                 " and a.salesorder_status not in('SO Amended','Cancelled','Rejected') group by date_format(a.salesorder_date,'%M') order by a.salesorder_date desc  ";

             dt_datatable = objdbconn.GetDataTable(msSQL);

            var GetOrderForLastSixMonths_List = new List<GetOrderForLastSixMonths_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    GetOrderForLastSixMonths_List.Add(new GetOrderForLastSixMonths_List
                    {
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        month = (dt["month"].ToString()),
                        year = (dt["year"].ToString()),
                        amount = (dt["amount"].ToString()),
                        ordercount = (dt["ordercount"].ToString()),
                    });
                    values.GetOrderForLastSixMonths_List = GetOrderForLastSixMonths_List;
                }

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Order Report For Last Six Months !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
        public void DaGetOrderForLastSixMonthsSearch(MdlSmrRptOrderReport values, string from_date, string to_date)
        {
            try
            {
                objdbconn.OpenConn();
                if (from_date == null && to_date == null)
            {
                msSQL = " select DATE_FORMAT(salesorder_date, '%b-%Y')  as salesorder_date,substring(date_format(a.salesorder_date,'%M'),1,3)as month,a.salesorder_gid,year(a.salesorder_date) as year, " +
                     " round(sum(a.grandtotal),2)as amount,count(a.salesorder_gid)as ordercount    " +
                     " from smr_trn_tsalesorder a   " +
                     " where a.salesorder_date > date_add(now(), interval-6 month) and a.salesorder_date<=date(now())   " +
                     " and a.salesorder_status not in('SO Amended','Cancelled','Rejected') group by date_format(a.salesorder_date,'%M') order by a.salesorder_date desc  ";
            }
            else
            {
                msSQL = " select DATE_FORMAT(salesorder_date, '%b-%Y')  as salesorder_date,substring(date_format(a.salesorder_date,'%M'),1,3)as month,a.salesorder_gid,year(a.salesorder_date) as year, " +
                         " round(sum(a.grandtotal),2)as amount,count(a.salesorder_gid)as ordercount    " +
                         " from smr_trn_tsalesorder a   " +
                         " where a.salesorder_date > date_add(now(), interval-6 month) and a.salesorder_date between DATE_FORMAT(STR_TO_DATE('" + from_date + "', '%d-%m-%Y'), '%Y-%m-%d') and DATE_FORMAT(STR_TO_DATE('" + to_date + "', '%d-%m-%Y'), '%Y-%m-%d')  " +
                         " and a.salesorder_status not in('SO Amended','Cancelled','Rejected') group by date_format(a.salesorder_date,'%M') order by a.salesorder_date desc  ";

            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            var GetOrderForLastSixMonths_List = new List<GetOrderForLastSixMonths_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    GetOrderForLastSixMonths_List.Add(new GetOrderForLastSixMonths_List
                    {
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        month = (dt["month"].ToString()),
                        year = (dt["year"].ToString()),
                        amount = (dt["amount"].ToString()),
                        ordercount = (dt["ordercount"].ToString()),
                    });
                    values.GetOrderForLastSixMonths_List = GetOrderForLastSixMonths_List;
                }

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Specific Date Data !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }

        // GetOrderSummary
        public void DaGetOrderSummary(string employee_gid, string salesorder_gid, MdlSmrRptOrderReport values)
        {
            try
            {
                
                msSQL = " select substring(date_format(a.salesorder_date,'%M'),1,3)as month,year(a.salesorder_date) as year, " +
                " round(sum(a.grandtotal_l),2)as amount,count(a.salesorder_gid)as ordercount " +
                " from smr_trn_tsalesorder a " +
                " where a.salesorder_date > date_add(now(),interval-6 month) and a.salesorder_date<=date(now()) " +
                " and a.salesorder_status not in('SO Amended','Cancelled','Rejected') group by date_format(a.salesorder_date,'%M') order by a.salesorder_date desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var GetOrderForLastSixMonths_List = new List<GetOrderForLastSixMonths_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    GetOrderForLastSixMonths_List.Add(new GetOrderForLastSixMonths_List
                    {
                        month = (dt["month"].ToString()),
                        year = (dt["year"].ToString()),
                        amount = (dt["amount"].ToString()),
                        ordercount = (dt["ordercount"].ToString()),
                    });
                    values.GetOrderForLastSixMonths_List = GetOrderForLastSixMonths_List;
                }

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Order Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        // GetOrderDetailSummary
        public void DaGetOrderDetailSummary(string employee_gid, string month ,string year, MdlSmrRptOrderReport values)
        {
            try
            {
               

                msSQL = " select a.salesorder_gid,date_format(a.salesorder_date,'%y-%m-%d')as salesorder_date,a.customer_name, " +
                    " concat(a.customer_contact_person,'/',a.customer_mobile,'/',a.customer_email)as contact_details " +
                    " ,a.so_type,a.grandtotal_l,concat(b.user_firstname,' ',b.user_lastname)as salesperson_name,a.salesorder_status " +
                    " from smr_trn_tsalesorder a " +
                    " left join adm_mst_tuser b on b.user_gid=a.salesperson_gid " +
                    " where substring(date_format(a.salesorder_date,'%M'),1,3)='" + month + "' and year(a.salesorder_date)='" + year + "' " +
                    " and a.salesorder_status not in('SO Amended','Cancelled','Rejected') ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var GetOrderDetailSummary = new List<GetOrderDetailSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    GetOrderDetailSummary.Add(new GetOrderDetailSummary
                    {
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        customer_name = (dt["customer_name"].ToString()),
                        contact_details = (dt["contact_details"].ToString()),
                        salesorder_status = (dt["salesorder_status"].ToString()),
                        salesperson_name = (dt["salesperson_name"].ToString()),
                        grandtotal_l = (dt["grandtotal_l"].ToString()),
                    });
                    values.GetOrderDetailSummary = GetOrderDetailSummary;
                }

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Order Detail Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // GetMonthwiseOrderReport
        public void DaGetMonthwiseOrderReport(string employee_gid, MdlSmrRptOrderReport values)
        {
            try
            {
               

                msSQL = " select distinct date_format(salesorder_date,'%M/%Y') as month_wise from smr_trn_tsalesorder " +
                    " group by salesorder_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            var MonthwiseOrderReport_List = new List<GetMonthwiseOrderReport_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = "select count(salesorder_gid) as so_total,format(ifnull(sum(Grandtotal*exchange_rate),0),2) as total " +
                        " from smr_trn_tsalesorder where date_format(salesorder_date,'%M/%Y')='" + dt[0].ToString() + "' and salesorder_status " +
                        " not in ('Approve Pending','SO Amended','Cancelled');";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    msSQL = " select format(sum(total_amount*exchange_rate),2) as total_invoice from rbl_trn_tinvoice" +
                       " where date_format(invoice_date,'%M/%Y')='" + dt[0].ToString() + "' ";
                    values.total_invoice = objdbconn.GetExecuteScalar(msSQL);
                    
                    msSQL = "  select format(sum(total_amount * exchange_rate),2) as total_payment from rbl_trn_tpayment" +
                    " where date_format(payment_date,'%M/%Y')='" + dt[0].ToString() + "' ";
                    values.total_payment = objdbconn.GetExecuteScalar(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt1 in dt_datatable.Rows)
                        {
                            
                            MonthwiseOrderReport_List.Add(new GetMonthwiseOrderReport_List
                            {
                                total_invoice = values.total_invoice,
                                total_payment = values.total_payment,
                                month_wise = dt["month_wise"].ToString(),
                                so_total = (dt1["so_total"].ToString()),
                                total = (dt1["total"].ToString()),
                                
                            });
                        }
                    }


                    values.GetMonthwiseOrderReport_List = MonthwiseOrderReport_List;
                }

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Month Wise Order Report !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // GetOrderWiseOrderReport
        public void DaGetOrderWiseOrderReport(string employee_gid,  MdlSmrRptOrderReport values)
        {
            try
            {
               
                msSQL = " select distinct date_format(salesorder_date,'%d/%M/%Y') as month_wise,salesorder_gid from smr_trn_tsalesorder " +
                    " group by salesorder_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var MonthwiseOrderReport_List = new List<GetOrderwiseOrderReport_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = "select count(salesorder_gid) as so_total,format(ifnull(sum(Grandtotal*exchange_rate),0),2) as total " +
                        " from smr_trn_tsalesorder where date_format(salesorder_date,'%d/%M/%Y')='" + dt[0].ToString() + "' and salesorder_status " +
                        " not in ('Approve Pending','SO Amended','Cancelled');";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    msSQL = " select format(sum(total_amount*exchange_rate),2) as total_invoice from rbl_trn_tinvoice" +
                       " where date_format(invoice_date,'%d/%M/%Y')='" + dt[0].ToString() + "' ";
                    values.total_invoice = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "  select format(sum(total_amount * exchange_rate),2) as total_payment from rbl_trn_tpayment" +
                    " where date_format(payment_date,'%d/%M/%Y')='" + dt[0].ToString() + "' ";
                    values.total_payment = objdbconn.GetExecuteScalar(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt1 in dt_datatable.Rows)
                        {

                            MonthwiseOrderReport_List.Add(new GetOrderwiseOrderReport_List
                            {
                               
                                total_invoice = values.total_invoice,
                                total_payment = values.total_payment,
                                month_wise = dt[0].ToString(),
                                so_total = (dt1["so_total"].ToString()),
                                total = (dt1["total"].ToString()),

                            });
                        }
                    }


                    values.GetOrderwiseOrderReport_List = MonthwiseOrderReport_List;
                }

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Order Wise Report !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
    }
}