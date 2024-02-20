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

using System.Runtime.InteropServices;

namespace ems.sales.DataAccess
{
    public class DaSmrCommissionManagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader, objOdbcDataReader1;
        DataTable dt_datatable;
        string msGetGid;
        int mnResult, mnResult1;
        string lsemployee_gid_list;
        string lspercentage, lspayoutstatus;
        double lspercent, Valcommission_amount, Valpayable_commission;
        double lsCommissionAmount, lsPayableAmount, lstotal_commission, lsbalance_payable;
        public void DaGetCommissionSettingSummary(MdlSmrCommissionManagement values)
        {
            try
            {
               
                msSQL = " select sales_type,neworder_percentage,repeatorder_percentage from crm_mst_tsalestype order by salestype_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetCommissionManagement_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetCommissionManagement_List
                    {
                        sales_type = dt["sales_type"].ToString(),
                        neworder_percentage = dt["neworder_percentage"].ToString(),
                        repeatorder_percentage = dt["repeatorder_percentage"].ToString(),

                    });
                    values.GetCommissionManagement_List = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading CommissionSettingSummary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        public void DaGetInvoiceSummary(MdlSmrCommissionManagement values)
        {

            try
            {
                

                msSQL = " select distinct a.irn,a.created_date,format(ifnull((a.payable_commission-a.commission_amount),0),2) as balance_payable, a.invoice_gid,a.invoice_refno,e.campaign_title,e.campaign_gid,a.customer_gid,salesorder_gid,c.salesperson_gid,format(ifnull((a.payable_commission),0),2) as payable_commission,format(ifnull((a.commission_amount),0),2) as commission_amount, " +
                 " concat(d.user_firstname,'.',d.user_lastname) as user_firstname,b.customer_type,a.irn,a.invoice_date, a.invoice_reference,a.mail_status, " +
                 "  a.additionalcharges_amount,a.discount_amount,format(a.invoice_amount, 2) as invoice_amount, " +
                 "  case when a.customer_contactnumber is null then concat(a.customer_contactperson,' / ',a.customer_contactnumber) else concat(a.customer_contactperson,  " +
                 "  if (a.customer_email = '',' ',concat(' / ', a.customer_email))) end as customer_contactperson,   " +
                  " case when a.currency_code = 'INR' then a.customer_name when a.currency_code is null then a.customer_name      " +
                 "  when a.currency_code is not null and a.currency_code <> 'INR' then concat(a.customer_name) end as customer_name, " +
                  " a.currency_code,  a.customer_contactnumber as mobile,a.invoice_from,   " +
                 "  case when irn is null then 'IRN Pending' when a.irncancel_date is not null then 'IRN Cancelled' " +
                 "  when a.creditnote_status='Y' then 'Credit Note Raised' when a.irn is not null then 'IRN Generated' else 'Invoice Raised' end as status " +
                 "  from rbl_trn_tinvoice a " +
                 "  left join crm_mst_tcustomer b on a.customer_gid=b.customer_gid " +
                 "  left join smr_trn_tsalesorder c on c.salesorder_gid = a.invoice_reference " +
                 " left join adm_mst_tuser d on  c.salesperson_gid = d.user_gid " +
                 " left join hrm_mst_temployee g on g.user_gid = d.user_gid         " +
                  " left join smr_trn_tcampaign2employee f on f.employee_gid =g.employee_gid " +
                  "  left join smr_trn_tcampaign e on e.campaign_gid =f.campaign_gid " +
                  " left join crm_trn_tenquiry2campaign i on i.campaign_gid = e.campaign_gid " +
             " left join crm_trn_tcommissionpayout h on a.invoice_gid=h.invoice_gid" +
            " where  h.invoice_gid is null ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<invoicesummary_list>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new invoicesummary_list
                    {
                        irn = dt["irn"].ToString(),
                        invoice_gid = dt["invoice_gid"].ToString(),
                        invoice_date = Convert.ToDateTime(dt["invoice_date"].ToString()),
                        invoice_refno = dt["invoice_refno"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_contactperson = dt["customer_contactperson"].ToString(),
                        invoice_reference = dt["invoice_reference"].ToString(),
                        invoice_from = dt["invoice_from"].ToString(),
                        mail_status = dt["mail_status"].ToString(),
                        invoice_amount = dt["invoice_amount"].ToString(),
                        invoice_status = dt["status"].ToString(),
                        customer_type = dt["customer_type"].ToString(),
                        campaign_gid = dt["campaign_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        user_firstname = dt["user_firstname"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        payable_commission = dt["payable_commission"].ToString(),
                        balance_payable = dt["balance_payable"].ToString(),
                        commission_amount = dt["commission_amount"].ToString(),

                    });
                    values.invoicesummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();


            for (int i = 0; i < values.invoicesummary_list.ToArray().Length; i++)
            {

                msSQL = " select customer_gid from smr_trn_tsalesorder where customer_gid = '" + values.invoicesummary_list[i].customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count == 1)
                {
                    msSQL = " select neworder_percentage, repeatorder_percentage from crm_mst_tsalestype where sales_type= '" + values.invoicesummary_list[i].customer_type + "' ";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader.HasRows)
                    {
                        lspercentage = objOdbcDataReader["neworder_percentage"].ToString();
                    }
                }
                else
                {
                    msSQL = " select repeatorder_percentage from crm_mst_tsalestype where sales_type= '" + values.invoicesummary_list[i].customer_type + "' ";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader.HasRows)
                    {
                        lspercentage = objOdbcDataReader["repeatorder_percentage"].ToString();
                    }
                }

                if (double.TryParse(lspercentage, out double percentValue))
                {

                    if (double.TryParse(values.invoicesummary_list[i].invoice_amount, out double invoiceAmount))
                    {
                        double payableCommission = (percentValue / 100) * invoiceAmount;
                        values.invoicesummary_list[i].payable_commission = payableCommission.ToString();

                    }
                    else
                    {
                        values.invoicesummary_list[i].payable_commission = "0.00";
                    }
                }
                else
                {
                    values.invoicesummary_list[i].payable_commission = "0.00";
                }


            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading InvoiceSummary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        public void DaPostSetPercentage(string user_gid, MdlSmrCommissionManagement values)
        {
            try
            {
               
                msSQL = " Select salestype_gid from crm_mst_tsalestype where sales_type= '" + values.sales_type + "'";
            string lssalestype = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update  crm_mst_tsalestype set " +
        " neworder_percentage = '" + values.neworder_percentage + "'," +
        " repeatorder_percentage = '" + values.repeatorder_percentage + "'," +
        " updated_by = '" + user_gid + "'," +
        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' where salestype_gid='" + lssalestype + "'  ";


            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Sale Precentage Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating";
            }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Posting Percentage !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           


        }
        public void DaPostCommission(string user_gid, MdlSmrCommissionManagement values)
        {
            try
            {
               
                msSQL = "select invoice_gid from rbl_trn_tinvoice where invoice_refno ='" + values.invoice_refno + "'";
            string lsinvoicegid = objdbconn.GetExecuteScalar(msSQL);

            Valcommission_amount = double.Parse(values.commission_amount);
            Valpayable_commission = double.Parse(values.payable_commission);

            if (Valcommission_amount != Valpayable_commission)
            {
                values.status = false;
                values.message = "Commission Amount should be same as Payable Commission";
                return;
            }
            else
            {
                msSQL = " select commission_amount,payable_commission from rbl_trn_tinvoice where invoice_gid= '" + lsinvoicegid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count > 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {

                        if (dt["commission_amount"].ToString() == null && dt["payable_commission"].ToString() == null)
                        {
                            lsCommissionAmount = 0.00;
                            lsPayableAmount = 0.00;
                            lstotal_commission = 0.00;
                        }
                        else
                        {
                            if (double.TryParse(dt["commission_amount"].ToString(), out lsCommissionAmount))
                            {

                            }

                            if (double.TryParse(dt["payable_commission"].ToString(), out lsPayableAmount))
                            {

                            }
                            if (lsCommissionAmount < lsPayableAmount)
                            {
                                lstotal_commission = lsCommissionAmount + Valcommission_amount;
                            }

                        }
                        lstotal_commission = Valcommission_amount;

                    }


                }
            }


            dt_datatable.Dispose();




            if (Valcommission_amount != 0 && Valpayable_commission != 0 && Valcommission_amount == Valpayable_commission)
            {
                lspayoutstatus = "Payout Done";
            }
            //else if (Valcommission_amount < Valpayable_commission)
            //{
            //    lspayoutstatus = "Partial Payout Done";
            //}
            else if (Valcommission_amount == 0)
            {
                lspayoutstatus = "Payout Pending";
            }
            else
            {
                lspayoutstatus = "Payout Pending";
            }

            msSQL = " update  rbl_trn_tinvoice set " +
                  " payable_commission = '" + Valpayable_commission + "'," +
                   " commission_amount = '" + Valcommission_amount + "'" +
                  " where invoice_gid='" + lsinvoicegid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SCMP");

                msSQL = " insert into crm_trn_tcommissionpayout(" +
                         " commissionpayout_gid," +
                         " invoice_gid," +
                         " generation_date," +
                         " total_invoice," +
                         " invoice_amount," +
                         " payable_commission," +
                         " commission_amount, " +
                         " payout_status, " +
                         " created_by, " +
                         " created_date)" +
                         " values(" +
                         " '" + msGetGid + "'," +
                         " '" + lsinvoicegid + "'," +
                         " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                         " '" + lsinvoicegid + "'," +
                         "'" + values.invoice_amount.Replace(",", "") + "'," +
                         "'" + Valpayable_commission + "'," +
                         "'" + Valcommission_amount + "'," +
                         "'" + lspayoutstatus + "'," +
                         "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Payout updated Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Payout";
                }
            }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Submiting CommissionSettingSummary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
        public void DaGetCommissionPayoutSummary(MdlSmrCommissionManagement values)
        {
            try
            {
               
                msSQL = " select commissionpayout_gid, a.invoice_gid,b.invoice_refno,DATE_FORMAT(a.generation_date, '%d-%m-%Y') AS generation_date  , total_invoice, a.invoice_amount, a.payable_commission, " +
                    " a.commission_amount, payout_status, a.created_by,concat(c.user_firstname, '.', c.user_lastname) as user_name, a.created_date from crm_trn_tcommissionpayout a " +
                    " left join rbl_trn_tinvoice b on a.invoice_gid = b.invoice_gid " +
                    " left join adm_mst_tuser c on c.user_gid = a.created_by; ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetCommissionPayout_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetCommissionPayout_List
                    {
                        commissionpayout_gid = dt["commissionpayout_gid"].ToString(),
                        invoice_gid = dt["invoice_gid"].ToString(),
                        invoice_refno = dt["invoice_refno"].ToString(),
                        generation_date = dt["generation_date"].ToString(),
                        total_invoice = dt["total_invoice"].ToString(),
                        invoice_amount = dt["invoice_amount"].ToString(),
                        payable_commission = dt["payable_commission"].ToString(),
                        commission_amount = dt["commission_amount"].ToString(),
                        payout_status = dt["payout_status"].ToString(),
                        user_name = dt["user_name"].ToString(),

                    });
                    values.GetCommissionPayout_List = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while  Getting CommissionPayoutSummary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
              $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
              msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

           
        }

        public void DaGetCommissionPayoutReport(MdlSmrCommissionManagement values)
        {
            try
            {
                
                msSQL = " select e.campaign_title,e.campaign_gid,count(a.invoice_gid) as total_count,group_concat(d.user_firstname,'.',d.user_lastname) as user_firstname, " +
               "  sum(a.invoice_amount) as invoice_amount,sum(a.payable_commission) as payable_commission, sum(a.commission_amount) as commission_amount " +
               "  from rbl_trn_tinvoice a  " +
               " left join crm_mst_tcustomer b on a.customer_gid=b.customer_gid  " +
               "  left join smr_trn_tsalesorder c on c.salesorder_gid = a.invoice_reference  " +
               " left join adm_mst_tuser d on  c.salesperson_gid = d.user_gid  " +
               "  left join hrm_mst_temployee g on g.user_gid = d.user_gid    " +
               "  left join smr_trn_tcampaign2employee f on f.employee_gid =g.employee_gid  " +
               " left join smr_trn_tcampaign e on e.campaign_gid =f.campaign_gid  " +
               " left join crm_trn_tcommissionpayout h on a.invoice_gid=h.invoice_gid group by e.campaign_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetCommissionPayout_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetCommissionPayout_List
                    {
                        campaign_title = dt["campaign_title"].ToString(),
                        campaign_gid = dt["campaign_gid"].ToString(),
                        total_count = dt["total_count"].ToString(),
                        user_name = dt["user_firstname"].ToString(),

                        invoice_amount = dt["invoice_amount"].ToString(),
                        payable_commission = dt["payable_commission"].ToString(),
                        commission_amount = dt["commission_amount"].ToString(),


                    });
                    values.GetCommissionPayout_List = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading CommissionPayoutReport !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

            
        }

        public void DaGetCommissionPayoutReportDetails(MdlSmrCommissionManagement values ,string  campaign_gid )
        {
            try
            {
               
                msSQL = " select e.campaign_title,e.campaign_gid,a.invoice_gid,a.payable_commission,a.commission_amount, " +
                " a.irn,a.created_date,format(ifnull((a.payable_commission-a.commission_amount),0),2) as balance_payable, a.invoice_gid,a.invoice_refno,e.campaign_title,e.campaign_gid,a.customer_gid,salesorder_gid,c.salesperson_gid,format(ifnull((a.payable_commission),0),2) as payable_commission,format(ifnull((a.commission_amount),0),2) as commission_amount, " +
                 " concat(d.user_firstname,'.',d.user_lastname) as user_firstname,b.customer_type,a.irn,a.invoice_date, a.invoice_reference,a.mail_status, " +
                 "  a.additionalcharges_amount,a.discount_amount,format(a.invoice_amount, 2) as invoice_amount, " +
                 "  case when a.customer_contactnumber is null then concat(a.customer_contactperson,' / ',a.customer_contactnumber) else concat(a.customer_contactperson,  " +
                 "  if (a.customer_email = '',' ',concat(' / ', a.customer_email))) end as customer_contactperson,   " +
                  " case when a.currency_code = 'INR' then a.customer_name when a.currency_code is null then a.customer_name      " +
                 "  when a.currency_code is not null and a.currency_code <> 'INR' then concat(a.customer_name) end as customer_name, " +
                  " a.currency_code,  a.customer_contactnumber as mobile,a.invoice_from,   " +
                 "  case when irn is null then 'IRN Pending' when a.irncancel_date is not null then 'IRN Cancelled' " +
                 "  when a.creditnote_status='Y' then 'Credit Note Raised' when a.irn is not null then 'IRN Generated' else 'Invoice Raised' end as status " +
                 "  from rbl_trn_tinvoice a   " +
                 "  left join crm_mst_tcustomer b on a.customer_gid=b.customer_gid   " +
                  " left join smr_trn_tsalesorder c on c.salesorder_gid = a.invoice_reference   " +
                  " left join adm_mst_tuser d on  c.salesperson_gid = d.user_gid " +
                 "  left join hrm_mst_temployee g on g.user_gid = d.user_gid     " +
                  " left join smr_trn_tcampaign2employee f on f.employee_gid =g.employee_gid   " +
                 "  left join smr_trn_tcampaign e on e.campaign_gid =f.campaign_gid   " +
                  " left join crm_trn_tenquiry2campaign i on i.campaign_gid = e.campaign_gid " +
                  " left join crm_trn_tcommissionpayout h on a.invoice_gid=h.invoice_gid" +
                  " where e.campaign_gid='" + campaign_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<invoicesummary_list>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new invoicesummary_list
                    {
                        irn = dt["irn"].ToString(),
                        invoice_gid = dt["invoice_gid"].ToString(),
                        invoice_date = Convert.ToDateTime(dt["invoice_date"].ToString()),
                        invoice_refno = dt["invoice_refno"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_contactperson = dt["customer_contactperson"].ToString(),
                        invoice_reference = dt["invoice_reference"].ToString(),
                        invoice_from = dt["invoice_from"].ToString(),
                        mail_status = dt["mail_status"].ToString(),
                        invoice_amount = dt["invoice_amount"].ToString(),
                        invoice_status = dt["status"].ToString(),
                        customer_type = dt["customer_type"].ToString(),
                        campaign_gid = dt["campaign_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        user_firstname = dt["user_firstname"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        payable_commission = dt["payable_commission"].ToString(),
                        balance_payable = dt["balance_payable"].ToString(),
                        commission_amount = dt["commission_amount"].ToString(),

                    });
                    values.invoicesummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Commission Payout Report Details !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            


        }
        public void DaGetCommissionEmpwisePayoutReport(MdlSmrCommissionManagement values, string user_gid)
        {
            try
            {
                
                msSQL = " select d.campaign_title,d.campaign_gid,b.invoice_amount, c.salesperson_gid, " +
                    "   concat(f.user_firstname,'.',f.user_lastname) as sales_person , " +
                     "  format(a.commission_amount,2) as commission_amount ,format(a.payable_commission,2) as payable_commission " +
                     "  from crm_trn_tcommissionpayout a  " +
                     "  left join rbl_trn_tinvoice b on a.invoice_gid = b.invoice_gid " +
                      "  left join smr_trn_tsalesorder c on c.salesorder_gid = b.invoice_reference   " +
                       "  left join smr_trn_tcampaign d on d.campaign_gid =c.campaign_gid   " +
                     "  left join crm_trn_tenquiry2campaign e on e.campaign_gid = d.campaign_gid  " +
                      "  left join adm_mst_tuser f on  c.salesperson_gid = f.user_gid   " +
                      " left join hrm_mst_temployee g on g.user_gid = f.user_gid  ";



            if (user_gid != "all")
            {
                msSQL += "WHERE f.user_gid ='" + user_gid + "' ";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetCommissionPayout_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetCommissionPayout_List
                    {
                        campaign_title = dt["campaign_title"].ToString(),
                        campaign_gid = dt["campaign_gid"].ToString(),
                        invoice_amount = dt["invoice_amount"].ToString(),
                        salesperson_gid = dt["salesperson_gid"].ToString(),
                        sales_person = dt["sales_person"].ToString(),
                        payable_commission = dt["payable_commission"].ToString(),
                        commission_amount = dt["commission_amount"].ToString(),


                    });
                    values.GetCommissionPayout_List = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Employeewise Commission PayoutReport !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

    }
}