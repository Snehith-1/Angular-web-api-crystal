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
    public class DaMarketingReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        int mnResult;
        string msGetGid, msGetGid1, lsentity_name, lsemployee_gid, lsuser_gid, msGetappointmentGid, msGetsheduleGid;

        public void DaGetReportLogSummary(string employee_gid, MdlMarketingReport values)
        {
            try
            {
                 
                msSQL = " select a.log_gid,a.log_desc,a.log_type,a.log_by,(date_format(date(a.log_date),'%d-%m-%Y'))as log_date,a.reference_gid,a.leadbank_gid," +
    " (date_format(date(a.log_date),'%d-%m-%Y'))as created_date, b.leadbank_name,d.campaign_title, " +
    " concat(f.user_firstname,'-',f.user_lastname) as created_name from crm_trn_tlog a" +
    " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid " +
    " left join crm_trn_tlead2campaign c on a.leadbank_gid = c.leadbank_gid   " +
    " left join crm_trn_tcampaign d on c.campaign_gid = d.campaign_gid" +
    " left join adm_mst_tuser f on a.log_by = f.user_gid " +
    " left join hrm_mst_temployee g on g.user_gid = f.user_gid" +
    " where employee_gid ='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<activitylog_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new activitylog_list
                        {
                            campaign_title = dt["campaign_title"].ToString(),
                            leadbank_name = dt["leadbank_name"].ToString(),
                            log_type = dt["log_type"].ToString(),
                            log_date = dt["log_date"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            log_desc = dt["log_desc"].ToString(),
                            created_name = dt["created_name"].ToString(),


                        });

                        values.activitylog_list = getModuleList;
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting log report";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }


        public void DaGetEnquiryChartReportSummary(string employee_gid, MdlMarketingReport values)
        {
            try
            {
                 
                msSQL = "   select substring(date_format(enquiry_date,'%M'),1,3)as month_name,year(enquiry_date) as year,count(enquiry_gid)as lead_count" +
                "   from smr_trn_tsalesenquiry  where enquiry_date > date_add(now(),interval-6 month) and enquiry_date<=date(now()) " +
                "  and created_by in ('" + employee_gid + "')  group by date_format(enquiry_date,'%M') order by enquiry_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<getenquirychart_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new getenquirychart_List
                        {
                            lead_year = (dt["year"].ToString()),
                            lead_monthname = (dt["month_name"].ToString()),
                            lead_count = (dt["lead_count"].ToString()),
                        });
                        values.getenquirychart_List = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting enquiry report";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaGetEnquiryReportSummary(string employee_gid, MdlMarketingReport values)
        {
            try
            {
                 
                msSQL = "select a.enquiry_gid,substring(date_format(a.enquiry_date,'%M'),1,3)as month,year(a.enquiry_date) as year, " +
                "    count(a.enquiry_gid)as enquirycount  from smr_trn_tsalesenquiry a   " +
                "  where a.enquiry_date > date_add(now(),interval-6 month) and a.enquiry_date<=date(now())  and a.created_by in ('" + employee_gid + "') " +
                "   group by date_format(a.enquiry_date,'%M')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<EnquiryReportSummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new EnquiryReportSummary_list
                        {
                            month = (dt["month"].ToString()),
                            year = (dt["year"].ToString()),
                            enquirycount = (dt["enquirycount"].ToString()),
                        });
                        values.EnquiryReportSummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting enquiry report!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }            
        }

        public void DaGetEnquirymainReportSummary(string employee_gid, MdlMarketingReport values)
        {
            try
            {
                 
                msSQL = "    select substring(date_format(a.enquiry_date,'%M'),1,3)as Month,year(a.enquiry_date) as Year,a.enquiry_gid, count(a.enquiry_gid)as Enquirycount" +
               "  from smr_trn_tsalesenquiry a  where a.enquiry_date > date_add(now(),interval-6 month) and a.enquiry_date<=date(now())  and a.created_by in" +
               " ('" + employee_gid + "')group by date_format(a.enquiry_date,'%M') order by a.enquiry_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<EnquiryReportMainSummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new EnquiryReportMainSummary_list
                        {
                            Month = (dt["Month"].ToString()),
                            Year = (dt["Year"].ToString()),
                            Enquirycount = (dt["Enquirycount"].ToString()),
                        });
                        values.EnquiryReportMainSummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting enquiry report";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }          
        }

        public void DaGetEnquirysubReportSummary(string employee_gid, string Month, string year, MdlMarketingReport values)
        {
            try
            {
                 
                msSQL = " Select distinct concat(a.enquiry_gid,' / ',a.enquiry_type) as enquiry_refno,a.potorder_value,a.enquiry_gid," +
             " DATE_FORMAT(a.enquiry_date, '%d-%b-%Y') as enquiry_date,concat(b.user_firstname,' ',b.user_lastname) as user_firstname,a.customer_name, a.customer_gid,a.lead_status," +
             " a.enquiry_gid,s.source_name,a.customer_name,  a.enquiry_status,a.enquiry_type,  concat(b.user_firstname,'-',b.user_lastname) as campaign, " +
             " case when a.contact_person is null then concat(n.leadbankcontact_name,' / ',n.mobile,' / ',n.email)  when a.contact_person is not null then" +
             " concat(a.contact_person,' / ',a.contact_number,' / ',a.contact_email) end as contact_details,  a.enquiry_type from smr_trn_tsalesenquiry a " +
             "left join crm_trn_tleadbank m on m.leadbank_gid=a.customer_gid  left join crm_trn_tleadbankcontact n on n.leadbank_gid=m.leadbank_gid" +
             " left join hrm_mst_temployee k on k.employee_gid=a.created_by   left join adm_mst_tuser b on b.user_gid= k.user_gid" +
             " left join crm_mst_tsource s on s.source_gid=m.source_gid where substring(date_format(a.enquiry_date,'%M'),1,3)='" + Month + "' " +
             " and year(a.enquiry_date)='" + year + "'  and a.created_by in ('" + employee_gid + "')Order by  a.enquiry_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<EnquirysubReportSummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new EnquirysubReportSummary_list
                        {
                            enquiry_date = (dt["enquiry_date"].ToString()),
                            enquiry_refno = (dt["enquiry_refno"].ToString()),
                            customer_name = (dt["customer_name"].ToString()),
                            contact_details = (dt["contact_details"].ToString()),
                            source_name = (dt["source_name"].ToString()),
                            potorder_value = (dt["potorder_value"].ToString()),
                            enquiry_status = (dt["enquiry_status"].ToString()),
                            user_firstname = (dt["user_firstname"].ToString()),

                        });
                        values.EnquirysubReportSummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting enquiry report";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }         
        }

        public void DaGetSelectedEnquiryReportSummary(string employee_gid, string from_date, string to_date, MdlMarketingReport values)
        {
            try
            {
                 
                msSQL = "select a.enquiry_gid,substring(date_format(a.enquiry_date,'%M'),1,3)as month,year(a.enquiry_date) as year,  " +
               "  count(a.enquiry_gid)as enquirycount  from smr_trn_tsalesenquiry a     where a.enquiry_date > date('" + from_date + "')" +
               "    and a.enquiry_date<=date('" + to_date + "')  and a.created_by in ('" + employee_gid + "')    group by date_format(a.enquiry_date,'%M');";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<EnquiryReportSummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new EnquiryReportSummary_list
                        {
                            month = (dt["month"].ToString()),
                            year = (dt["year"].ToString()),
                            enquirycount = (dt["enquirycount"].ToString()),
                        });
                        values.EnquiryReportSummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting enquiry report";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }           
        }
        public void DaGetSelectedEnquirymainReportSummary(string employee_gid, string from_date, string to_date, MdlMarketingReport values)
        {
            try
            {
                 
                msSQL = "select substring(date_format(a.enquiry_date,'%M'),1,3)as Month,year(a.enquiry_date) as Year,a.enquiry_gid, " +
               "  count(a.enquiry_gid)as Enquirycount  from smr_trn_tsalesenquiry a  where a.enquiry_date > date('" + from_date + "')   " +
               " and a.enquiry_date<=date('" + to_date + "')  and a.created_by in ('" + employee_gid + "')group by date_format(a.enquiry_date,'%M') order by a.enquiry_date desc;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<EnquiryReportMainSummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new EnquiryReportMainSummary_list
                        {
                            Month = (dt["Month"].ToString()),
                            Year = (dt["Year"].ToString()),
                            Enquirycount = (dt["Enquirycount"].ToString()),
                        });
                        values.EnquiryReportMainSummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting enquiry report";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }           
        }

        public void DaGetSelectEnquiryChartReportSummary(string employee_gid, string from_date, string to_date, MdlMarketingReport values)
        {
            try
            {
                 
                msSQL = "   select substring(date_format(enquiry_date,'%M'),1,3)as month_name,year(enquiry_date) as year,count(enquiry_gid)as lead_count" +
              "   from smr_trn_tsalesenquiry  where enquiry_date > date('" + from_date + "') and enquiry_date<=date('" + to_date + "') " +
              "  and created_by in ('" + employee_gid + "')  group by date_format(enquiry_date,'%M') order by enquiry_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<getselectenquirychart_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new getselectenquirychart_List
                        {
                            lead_year = (dt["year"].ToString()),
                            lead_monthname = (dt["month_name"].ToString()),
                            lead_count = (dt["lead_count"].ToString()),
                        });
                        values.getselectenquirychart_List = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting enquiry report";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaGetCustomerToLeadChartReportSummary(string employee_gid, MdlMarketingReport values)
        {
            try
            {
                 
                msSQL = " SELECT SUBSTRING(DATE_FORMAT(a.created_date, '%M'), 1, 3) AS month,YEAR(a.created_date) AS year,  " +
              "  COUNT(*) AS customercount, SUM(CASE WHEN l.leadbank_gid IS NOT NULL THEN 1 ELSE 0 END) AS leadcount " +
              " FROM acp_trn_tenquiry a LEFT JOIN crm_trn_tlead2campaign l ON a.customer_gid = l.leadbank_gid " +
              " WHERE a.created_date > date_add(now(),interval-6 month)AND a.created_date <= date(now()) and l.created_by in ('" + employee_gid + "') GROUP BY DATE_FORMAT(a.created_date, '%M')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<getCustomerToLeadChartchart_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new getCustomerToLeadChartchart_List
                        {
                            month = (dt["month"].ToString()),
                            year = (dt["year"].ToString()),
                            customercount = (dt["customercount"].ToString()),
                            leadcount = (dt["leadcount"].ToString()),
                        });
                        values.getCustomerToLeadChartchart_List = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting customer and lead report";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

          
        }

        public void DaGetCustomerToLeadChartReportsearchSummary(string employee_gid, string from_date, string to_date, MdlMarketingReport values)
        {
            try
            {
                 
                msSQL = " SELECT SUBSTRING(DATE_FORMAT(a.created_date, '%M'), 1, 3) AS month,YEAR(a.created_date) AS year,  " +
               "  COUNT(*) AS customercount, SUM(CASE WHEN l.leadbank_gid IS NOT NULL THEN 1 ELSE 0 END) AS leadcount " +
               " FROM acp_trn_tenquiry a LEFT JOIN crm_trn_tlead2campaign l ON a.customer_gid = l.leadbank_gid " +
               " WHERE a.created_date > date('" + from_date + "') AND a.created_date <= date('" + to_date + "')  and " +
               " l.created_by in ('" + employee_gid + "') GROUP BY DATE_FORMAT(a.created_date, '%M')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<getCustomerToLeadChartchart_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new getCustomerToLeadChartchart_List
                        {
                            month = (dt["month"].ToString()),
                            year = (dt["year"].ToString()),
                            customercount = (dt["customercount"].ToString()),
                            leadcount = (dt["leadcount"].ToString()),
                        });
                        values.getCustomerToLeadChartchart_List = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting customer and lead report";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }           
        }
    }

}