using ems.sales.Models;
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
    public class DaSmrRptQuotationReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        DataTable dt_datatable1;
        string msGetGid;
        int mnResult, mnResult1;

        // GetQuotationReportForLastSixMonthsSearch
        public void DaGetQuotationReportForLastSixMonths(MdlSmrRptQuotationReport values)
        {
            try
            {
               

                msSQL = " select  DATE_FORMAT(quotation_date, '%b-%Y')  as quotation_date,substring(date_format(a.quotation_date,'%M'),1,3)as month,a.quotation_gid,year(a.quotation_date) as year, " +
                        " round(sum(a.grandtotal),2)as amount,count(a.quotation_gid)as quotationcount  " +
                        " from smr_trn_treceivequotation a  " +
                        " where a.quotation_date > date_add(now(), interval-6 month) and a.quotation_date<=date(now())  " +
                        " and a.quotation_status not in('SO Amended','Canceled','Rejected') group by date_format(a.quotation_date,'%M') order by a.quotation_date desc   ";
            
            dt_datatable = objdbconn.GetDataTable(msSQL);

            var GetQuotationForLastSixMonths_List = new List<GetQuotationForLastSixMonths_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    GetQuotationForLastSixMonths_List.Add(new GetQuotationForLastSixMonths_List
                    {
                        quotation_date = (dt["quotation_date"].ToString()),
                        month = (dt["month"].ToString()),
                        year = (dt["year"].ToString()),
                        amount = (dt["amount"].ToString()),
                        quotationcount = (dt["quotationcount"].ToString()),
                    });
                    values.GetQuotationForLastSixMonths_List = GetQuotationForLastSixMonths_List;
                }

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Last six months Quotation Report !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // GetQuotationReportForLastSixMonthsSearch
        public void DaGetQuotationReportForLastSixMonthsSearch( MdlSmrRptQuotationReport values, string from_date, string to_date)
        {
            try
            {
                
                if (from_date == null && to_date == null)
            {
                msSQL = " select  DATE_FORMAT(quotation_date, '%b-%Y')  as quotation_date,substring(date_format(a.quotation_date,'%M'),1,3)as month,a.quotation_gid,year(a.quotation_date) as year, " +
                        " round(sum(a.grandtotal),2)as amount,count(a.quotation_gid)as quotationcount  " +
                        " from smr_trn_treceivequotation a  " +
                        " where a.quotation_date > date_add(now(), interval-6 month) and a.quotation_date<=date(now())  " +
                        " and a.quotation_status not in('SO Amended','Canceled','Rejected') group by date_format(a.quotation_date,'%M') order by a.quotation_date desc   ";
            }
            else
            {
                msSQL = " select DATE_FORMAT(quotation_date, '%b-%Y')  as quotation_date,substring(date_format(a.quotation_date,'%M'),1,3)as month,a.quotation_gid,year(a.quotation_date) as year, " +
                        " round(sum(a.grandtotal),2)as amount,count(a.quotation_gid)as quotationcount  " +
                        " from smr_trn_treceivequotation a  " +
                        " where a.quotation_date > date_add(now(), interval-6 month) and a.quotation_date between DATE_FORMAT(STR_TO_DATE('" + from_date + "', '%d-%m-%Y'), '%Y-%m-%d') and DATE_FORMAT(STR_TO_DATE('" + to_date + "', '%d-%m-%Y'), '%Y-%m-%d')  " +
                        " and a.quotation_status not in('SO Amended','Canceled','Rejected') group by date_format(a.quotation_date,'%M') order by a.quotation_date desc   ";

            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            var GetQuotationForLastSixMonths_List = new List<GetQuotationForLastSixMonths_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    GetQuotationForLastSixMonths_List.Add(new GetQuotationForLastSixMonths_List
                    {
                        quotation_date = (dt["quotation_date"].ToString()),
                        month = (dt["month"].ToString()),
                        year = (dt["year"].ToString()),
                        amount = (dt["amount"].ToString()),
                        quotationcount = (dt["quotationcount"].ToString()),
                    });
                    values.GetQuotationForLastSixMonths_List = GetQuotationForLastSixMonths_List;
                }

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Specific  Reports !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // GetQuotationSummary
        public void DaGetQuotationSummary(string employee_gid, MdlSmrRptQuotationReport values)
        {
            try
            {
               
                msSQL = " select substring(date_format(a.quotation_date,'%M'),1,3)as month,year(a.quotation_date) as year, " +
                " round(sum(a.grandtotal_l),2)as amount,count(a.quotation_gid)as quotationcount " +
                " from smr_trn_treceivequotation a " +
                " where a.quotation_date > date_add(now(),interval-6 month) and a.quotation_date<=date(now()) " +
                " and a.quotation_status not in('SO Amended','Cancelled','Rejected')  group by date_format(a.quotation_date,'%M') order by a.quotation_date desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var GetQuotationForLastSixMonths_List = new List<GetQuotationForLastSixMonths_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    GetQuotationForLastSixMonths_List.Add(new GetQuotationForLastSixMonths_List
                    {
                        quotation_date = (dt["quotation_date"].ToString()),
                        month = (dt["month"].ToString()),
                        year = (dt["year"].ToString()),
                        amount = (dt["amount"].ToString()),
                        quotationcount = (dt["quotationcount"].ToString()),
                    });
                    values.GetQuotationForLastSixMonths_List = GetQuotationForLastSixMonths_List;
                }

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Quotation Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        // GetQuotationDetailSummary
        public void DaGetQuotationDetailSummary(string employee_gid ,string month , string year, MdlSmrRptQuotationReport values)
        {
            try
            {
                


                msSQL = " select a.quotation_gid,a.quotation_date,a.customer_name, " +
                    " concat(a.customer_contact_person,'/',a.contact_no,'/',a.contact_mail)as contact_details, " +
                    " a.quotation_type,a.grandtotal_l,concat(c.user_firstname,' ',c.user_lastname)as created_by,a.quotation_status " +
                    " from smr_trn_treceivequotation a " +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid " +
                    " where substring(date_format(a.quotation_date,'%M'),1,3)='" + month + "' and year(a.quotation_date)='" + year + "' " +
                    " and a.quotation_status not in('SO Amended','Cancelled','Rejected') Order by  a.quotation_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var GetQuotationSummary = new List<GetQuotationSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    GetQuotationSummary.Add(new GetQuotationSummary
                    {
                        quotation_date = (dt["quotation_date"].ToString()),
                        customer_name = (dt["customer_name"].ToString()),
                        contact_details = (dt["contact_details"].ToString()),
                        quotation_status = (dt["quotation_status"].ToString()),
                        created_by = (dt["created_by"].ToString()),
                        grandtotal_l = (dt["grandtotal_l"].ToString()),
                    });
                    values.GetQuotationSummary = GetQuotationSummary;
                }

            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Quotation Detail Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

    }
}