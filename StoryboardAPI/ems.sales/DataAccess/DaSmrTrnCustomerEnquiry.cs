using ems.sales.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.NetworkInformation;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Web.Http.Results;

using static OfficeOpenXml.ExcelErrorValue;
using System.Resources;

namespace ems.sales.DataAccess
{
    public class DaSmrTrnCustomerEnquiry
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        private OdbcDataReader objOdbcDataReader;

        DataTable dt_datatable;

        string msEmployeeGID, lsemployee_gid, lsuser_gid, msGetEnquiryGid, closure_date, lspercentage1, lsproductuomgid, lsproduct_type, lsproductgid1, QuoatationGID, EnquiryGID, TempQuoatationGID, lsenquiry_type, lsentity_code, lsleadstagegid, lscustomer_gid, lsleadbank_gid, lscampaign_gid, lspotential_value, lstype1, lsdesignation_code, lslead_status, lsleadstage, lspurchaseenquiry_flag, lsCode, msGetGid, msGetGid1, msgetlead2campaign_gid, msGetPrivilege_gid, msGetModule2employee_gid, status, E;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5, i;
        string tmpquotationdtl_gid, quotation_gid, product_gid, productgroup_gid, productgroup_name, customerproduct_code, product_name, display_field, product_price, qty_quoted, discountpercentage, discountamount;
        string uom_gid, uom_name, selling_price, price, tax_name, tax_name2, tax_name3, tax1_gid, tax2_gid, tax3_gid, slno, product_requireddate, productrequireddate_remarks, tax_percentage, tax_percentage2, tax_percentage3;
        string vendor_gid, tax_amount, tax_amount2, tax_amount3, salesperson, lsQuotationMode;
        string quotation_type, lsQOStatus;
        //Summary
        public void DaGetCustomerEnquirySummary(MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
               
                msSQL = " Select distinct concat(a.enquiry_gid,' / ',a.enquiry_type) as enquiry_refno,format(a.potorder_value,2)as potorder_value," +
                  " concat(s.source_name,' / ',m.referred_by)as source_name," +
                  " a.enquiry_gid,DATE_FORMAT(a.enquiry_date, '%d-%m-%Y') as enquiry_date,m.referred_by,concat(b.user_firstname,' ',b.user_lastname) as campaign," +
                  " a.customer_name,a.branch_gid," +
                  " a.customer_gid,a.lead_status,z.branch_name, " +
                  " concat(o.region_name,' / ',m.leadbank_city,' / ',m.leadbank_state) as region_name," +
                  " a.enquiry_referencenumber,a.enquiry_status,a.enquiry_type, " +
                  " concat(f.user_firstname,' ',f.user_lastname) as user_firstname,a.enquiry_remarks,a.potorder_value ,a.created_date ," +
                  " a.contact_person,a.contact_email,a.contact_address, p.customer_rating, " +
                  " case when a.contact_person is null then concat(n.leadbankcontact_name,' / ',n.mobile,' / ',n.email) " +
                  " when a.contact_person is not null then concat(a.customerbranch_gid,' | ',a.contact_person,' / ',a.contact_number,' / ',a.contact_email) end" +
                  " as contact_details,a.enquiry_referencenumber, " +
                  " r.leadstage_name,a.enquiry_type from smr_trn_tsalesenquiry a  " +
                  " left join crm_trn_tleadbank m on m.leadbank_gid=a.customer_gid " +
                  " left join crm_trn_tleadbankcontact n on n.leadbank_gid=m.leadbank_gid " +
                  " left join crm_mst_tregion o on m.leadbank_region=o.region_gid " +
                  " left join crm_trn_tenquiry2campaign p on p.enquiry_gid=a.enquiry_gid " +
                  " left join crm_mst_tleadstage r on r.leadstage_gid=p.leadstage_gid " +
                  " left join smr_trn_tcampaign q on q.campaign_gid=p.campaign_gid " +
                  " left join hrm_mst_temployee d on d.employee_gid=p.assign_to " +
                  " left join adm_mst_tuser b on b.user_gid= d.user_gid " +
                  " left join hrm_mst_temployee k on k.employee_gid=a.created_by " +
                  " left join adm_mst_tuser f on f.user_gid= k.user_gid " +
                  " left join hrm_mst_tbranch z on a.branch_gid=z.branch_gid " +
                  // " left join crm_trn_tleadbank y on a.leadbank_gid=y.leadbank_gid " +
                  " left join crm_mst_tsource s on s.source_gid=m.source_gid " +
                  " order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetCusEnquiry>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetCusEnquiry
                        {
                            enquiry_gid = dt["enquiry_gid"].ToString(),
                            enquiry_date = dt["enquiry_date"].ToString(),
                            enquiry_refno = dt["enquiry_refno"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            enquiry_referencenumber = dt["enquiry_referencenumber"].ToString(),
                            contact_details = dt["contact_details"].ToString(),
                            campaign = dt["campaign"].ToString(),
                            potorder_value = dt["potorder_value"].ToString(),
                            lead_status = dt["lead_status"].ToString(),
                            enquiry_status = dt["enquiry_status"].ToString(),
                            customer_rating = dt["customer_rating"].ToString(),
                            created_by = dt["user_firstname"].ToString(),

                        });
                        values.cusenquiry_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Loading Customer Enquiry !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        // Lead Dropdown
        public void DaGetLeadDtl(MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
                
                msSQL = " SELECT a.leadstage_gid,a.leadstage_name " +
                    " FROM crm_mst_tleadstage a" +
                    " WHERE (leadstage_gid='5' or leadstage_gid='7') ";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetLeadDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetLeadDropdown
                        {
                            leadstage_gid = dt["leadstage_gid"].ToString(),
                            leadstage_name = dt["leadstage_name"].ToString(),

                        });
                        values.GetLeadDtl = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Lead Stage Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        // Update close
        public void DaGetUpdatedCloseEnquiry(string user_gid, GetCusEnquiry values)
        {
            try
            {
                
                msSQL = "select leadstage_name from crm_mst_tleadstage where leadstage_gid = '" + values.leadstage_name + "'";
                string lsleadstagegid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update crm_trn_tenquiry2campaign set " +
                        " lead_status ='" + lsleadstagegid + "'," +
                        " leadstage_gid = '" + values.leadstage_name + "'," +
                        " internal_notes = '" + values.internal_notes + "', " +
                        " updated_by = '" + user_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        " where enquiry_gid = '" + values.enquiry_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = " update smr_trn_tsalesenquiry set " +
                        " lead_status = '" + lsleadstagegid + "'," +
                        " enquiry_status='Enquiry Closed' " +
                        " where enquiry_gid = '" + values.enquiry_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = " Enquiry Closed Successfully";

                }

                else
                {
                    values.status = false;
                    values.message = "Error While Closing Sales Enquiry";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Closing Sales Enquiry !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           


        }

        // Team Dropdown
        public void DaGetTeamDtl(MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
               
                msSQL = "select campaign_title,campaign_gid " +
                    " from smr_trn_tcampaign ";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetTeamDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetTeamDropdown
                        {

                            campaign_gid = dt["campaign_gid"].ToString(),
                            campaign_title = dt["campaign_title"].ToString(),

                        });
                        values.GetTeamDtl = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Campain Tittle !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        //Employee Dropdown
        public void DaGetEmployeeDtl(MdlSmrTrnCustomerEnquiry values)
        {
            try
            {

                msSQL = " select distinct a.employee_gid," +
                    " concat(c.user_firstname,' ',c.user_lastname)as employee_name" +
                    " from smr_trn_tcampaign2employee a" +
                    " inner join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                    " inner join adm_mst_tuser c on b.user_gid=c.user_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetEmployeeDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetEmployeeDropdown
                        {
                            employee_gid = dt["employee_gid"].ToString(),
                            employee_name = dt["employee_name"].ToString(),

                        });
                        values.GetEmployeeDtl = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Employee Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
        }

        // Update Reassign
        public void DaGetUpdatedReAssignEnquiry(string user_gid, GetCusEnquiry values)
        {
            try
            {
               
                msSQL = " update crm_trn_tenquiry2campaign set " +
                        " assign_to = '" + values.employee_name + "'," +
                        " campaign_gid = '" + values.campaign_title + "', " +
                        " updated_by = '" + user_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        " where enquiry_gid = '" + values.enquiry_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = " Successfully Re-Assigned";

                }

                else
                {
                    values.status = false;
                    values.message = "Error While Re-Assigning";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating  Re-Assigning!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
        public void DaGetProductGrp(MdlSmrTrnCustomerEnquiry values)
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
                values.message = "Exception occured while Getting Product Group Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
        public void DaGetProducts(MdlSmrTrnCustomerEnquiry values)
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
                values.message = "Exception occured while Getting Product  Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           

        }
        public void DaGetProductunit(MdlSmrTrnCustomerEnquiry values)
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
                values.message = "Exception occured while Getting Product Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaGetCustomer(MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
                

                msSQL = "Select a.customer_gid, a.customer_name " +
                     " from crm_mst_tcustomer a " +
                     "where a.status='Active'";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetCustomername>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetCustomername
                        {
                            customer_name = dt["customer_name"].ToString(),
                            customer_gid = dt["customer_gid"].ToString(),
                        });
                        values.GetCustomername = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Customer Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
          
        }

        //Customer 360

        public void DaGetCustomerCRM(MdlSmrTrnCustomerEnquiry values, string leadabank_gid)
        {
            try
            {
               

                msSQL = "Select customer_gid from crm_trn_tleadbank where leadbank_gid='" + leadabank_gid + "' ";
                string lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "Select a.customer_gid, a.customer_name " +
                " from crm_mst_tcustomer a where customer_gid='" + lscustomer_gid + "' ";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetCustomername>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetCustomername

                        {
                            customer_gid = dt["customer_gid"].ToString(),
                            customer_name = dt["customer_name"].ToString(),

                        });
                        values.GetCustomername = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Customer Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
        public void DaGetOnChangeCustomerName(string customercontact_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
               

                if (customercontact_gid != null)
                {
                    msSQL = " select a.customercontact_gid,concat(a.address1,'   ',a.city,'   ',a.state,'   ',a.zip_code) as address1,ifnull(a.address2,'') as address2,ifnull(a.city,'') as city, " +
                    " ifnull(a.state,'') as state,ifnull(a.country_gid,'') as country_gid,ifnull(a.zip_code,'') as zip_code, " +
                    " ifnull(a.mobile,'') as mobile,ifnull(a.email,'') as email,ifnull(b.country_name,'') as country_name,a.customerbranch_name,concat(a.customercontact_name) as " +
                    " customercontact_names, c.customer_gid " +
                    " from crm_mst_tcustomercontact a " +
                    " left join crm_mst_tcustomer c on a.customer_gid=c.customer_gid " +
                    " left join adm_mst_tcountry b on a.country_gid=b.country_gid " +
                    " where c.customer_gid='" + customercontact_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    var getModuleList = new List<GetCustomer>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getModuleList.Add(new GetCustomer
                            {
                                customercontact_name = dt["customercontact_names"].ToString(),
                                customerbranch_name = dt["customerbranch_name"].ToString(),
                                country_name = dt["country_name"].ToString(),
                                contact_email = dt["email"].ToString(),
                                contact_number = dt["mobile"].ToString(),
                                zip_code = dt["zip_code"].ToString(),
                                country_gid = dt["country_gid"].ToString(),
                                state = dt["state"].ToString(),
                                city = dt["city"].ToString(),
                                address2 = dt["address2"].ToString(),
                                contact_address = dt["address1"].ToString(),
                                customercontact_gid = dt["customercontact_gid"].ToString(),
                                customer_gid = dt["customer_gid"].ToString(),

                            });
                            values.GetCustomer = getModuleList;
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Customer Detailes !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }

        // 360 onchange customer

        public void DaGetOnChangeCustomerNameCRM(string customercontact_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
               


                if (customercontact_gid != null)
                {
                    msSQL = " select a.customercontact_gid,concat(a.address1,'   ',a.city,'   ',a.state,'   ',a.zip_code) as address1,ifnull(a.address2,'') as address2,ifnull(a.city,'') as city, " +
                    " ifnull(a.state,'') as state,ifnull(a.country_gid,'') as country_gid,ifnull(a.zip_code,'') as zip_code, " +
                    " ifnull(a.mobile,'') as mobile,ifnull(a.email,'') as email,ifnull(b.country_name,'') as country_name,a.customerbranch_name,concat(a.customercontact_name) as " +
                    " customercontact_names, c.customer_gid " +
                    " from crm_mst_tcustomercontact a " +
                    " left join crm_mst_tcustomer c on a.customer_gid=c.customer_gid " +
                    " left join adm_mst_tcountry b on a.country_gid=b.country_gid " +
                    " where c.customer_gid='" + customercontact_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    var getModuleList = new List<GetCustomer>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getModuleList.Add(new GetCustomer
                            {
                                customercontact_name = dt["customercontact_names"].ToString(),
                                customerbranch_name = dt["customerbranch_name"].ToString(),
                                country_name = dt["country_name"].ToString(),
                                contact_email = dt["email"].ToString(),
                                contact_number = dt["mobile"].ToString(),
                                zip_code = dt["zip_code"].ToString(),
                                country_gid = dt["country_gid"].ToString(),
                                state = dt["state"].ToString(),
                                city = dt["city"].ToString(),
                                address2 = dt["address2"].ToString(),
                                contact_address = dt["address1"].ToString(),
                                //customercontact_gid = dt["leadbankcontact_gid"].ToString(),
                                customer_gid = dt["customer_gid"].ToString(),

                            });
                            values.GetCustomer = getModuleList;
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Customer Detailes !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }
        public void DaGetOnChangeProductsName(string product_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
                
                if (product_gid != null)
                {
                    msSQL = " Select a.product_name,a.product_gid, a.product_code, b.productuom_gid,b.productuom_name,c.productgroup_name,c.productgroup_gid,a.productuom_gid  from pmr_mst_tproduct a  " +
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
                                product_gid = dt["product_gid"].ToString(),
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
                values.message = "Exception occured while Getting Product Detailes !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }
        public void DaGetProductsSummary(string user_gid, string employee_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {

                msSQL = " select a.tmpsalesenquiry_gid,a.customerproduct_code,a.qty_requested,a.display_field, " +
                    " date_format(a.product_requireddate,'%d-%m-%Y') as product_requireddate, " +
                    " d.productgroup_name,b.product_code,b.product_name,c.productuom_name,a.product_gid, " +
                    " format(a.potential_value,2)as potential_value,a.product_requireddateremarks " +
                    " from smr_tmp_tsalesenquiry a left join pmr_mst_tproduct b on a.product_gid=b.product_gid " +
                    " left join pmr_mst_tproductuom c on a.uom_gid=c.productuom_gid " +
                    " left join pmr_mst_tproductgroup d on a.productgroup_gid= d.productgroup_gid" +
                    " where a.created_by='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<productsummarys_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new productsummarys_list
                        {
                            tmpsalesenquiry_gid = dt["tmpsalesenquiry_gid"].ToString(),
                            customerproduct_code = dt["customerproduct_code"].ToString(),
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
                values.message = "Exception occured while Getting Product Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaGetEditProductsSummary(string enquiry_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
                

                msSQL = " select a.tmpsalesenquiry_gid,a.enquiry_gid,a.customerproduct_code,a.qty_requested,a.display_field, " +
                    " date_format(a.product_requireddate,'%d-%m-%Y') as product_requireddate, " +
                    " d.productgroup_name,b.product_code,b.product_name,c.productuom_name,a.product_gid, " +
                    " format(a.potential_value,2)as potential_value,a.product_requireddateremarks " +
                    " from smr_tmp_tsalesenquiry a left join pmr_mst_tproduct b on a.product_gid=b.product_gid " +
                    " left join pmr_mst_tproductuom c on a.uom_gid=c.productuom_gid " +
                    " left join pmr_mst_tproductgroup d on a.productgroup_gid= d.productgroup_gid" +
                    " where a.enquiry_gid='" + enquiry_gid + "' group by b.product_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<prodenquirylist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new prodenquirylist
                        {
                            tmpsalesenquiry_gid = dt["tmpsalesenquiry_gid"].ToString(),                          
                            qty_requested = dt["qty_requested"].ToString(),
                            product_requireddate = dt["product_requireddate"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            potential_value = dt["potential_value"].ToString(),
                        });
                        values.prodenquiry_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Product Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }
        public void DaPostOnAdds(string user_gid, string employee_gid, productsummarys_list values)
        {
            try
            {
               
                msGetGid = objcmnfunctions.GetMasterGID("PPDC");
                EnquiryGID = objcmnfunctions.GetMasterGID("PPTP");

                if (values.product_requireddate == null || values.product_requireddate == "")
                {
                    product_requireddate = "0000-00-00";
                }
                else
                {
                    string uiDateStr2 = values.product_requireddate;
                    DateTime uiDate2 = DateTime.ParseExact(uiDateStr2, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    product_requireddate = uiDate2.ToString("yyyy-MM-dd");
                }

                msSQL = "select product_gid from pmr_mst_tproduct where product_name='" + values.product_name + "'";
                string lsproductgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select productuom_gid from pmr_mst_tproductuom where productuom_name='" + values.productuom_name + "'";
                string lsproductuomgid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "Select productgroup_gid from pmr_mst_tproductgroup where productgroup_name='" + values.productgroup_name + "'";
                string lsproductgroupgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " SELECT a.producttype_name FROM pmr_mst_tproducttype a " +
                 " INNER JOIN pmr_mst_tproduct b ON a.producttype_gid=b.producttype_gid  " +
                 " WHERE product_gid='" + values.product_name + "'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows == true)
                {
                    if (objOdbcDataReader["producttype_name"].ToString() != "Services")
                    {
                        lsenquiry_type = "Sales";
                    }
                    else
                    {
                        lsenquiry_type = "Services";
                    }

                }

                msSQL = " insert into smr_tmp_tsalesenquiry( " +
                        " tmpsalesenquiry_gid , " +
                        " enquiry_gid, " +
                        " productgroup_gid, " +
                        " product_gid, " +
                        " potential_value," +
                        " uom_gid," +
                        " qty_requested, " +
                        " created_by, " +
                        " product_requireddate," +
                        " enquiry_type) " +
                        " values( " +
                         "'" + msGetGid + "'," +
                         "'" + EnquiryGID + "'," +
                        "'" + lsproductgroupgid + "'," +
                        "'" + lsproductgid + "'," +
                        "'" + values.potential_value + "'," +
                        "'" + lsproductuomgid + "'," +
                        "'" + values.qty_requested + "', " +
                        "'" + employee_gid + "', " +
                        "'" + product_requireddate + "'," +
                        "'" + lsenquiry_type + "') ";
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
                values.message = "Exception occured while Adding Product !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaPostEditOnAdds(string employee_gid, productsummarys_list values)
        {
            try
            {
                
                msGetGid = objcmnfunctions.GetMasterGID("PPDC");
                msGetGid1 = objcmnfunctions.GetMasterGID("PPTM");

                if (values.product_requireddate == null || values.product_requireddate == "")
                {
                    product_requireddate = "0000-00-00";
                }
                else
                {
                    string uiDateStr = values.product_requireddate;
                    DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    product_requireddate = uiDate.ToString("yyyy-MM-dd");
                }


                msSQL = "select product_gid from pmr_mst_tproduct where product_name='" + values.product_name + "'";
                string lsproductgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select productuom_gid from pmr_mst_tproductuom where productuom_name='" + values.productuom_name + "'";
                string lsproductuomgid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "Select productgroup_gid from pmr_mst_tproductgroup where productgroup_name='" + values.productgroup_name + "'";
                string lsproductgroupgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " SELECT a.producttype_name FROM pmr_mst_tproducttype a " +
                 " INNER JOIN pmr_mst_tproduct b ON a.producttype_gid=b.producttype_gid  " +
                 " WHERE product_gid='" + lsproductgid + "'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows == true)
                {
                    if (objOdbcDataReader["producttype_name"].ToString() != "Services")
                    {
                        lsenquiry_type = "Sales";
                    }
                    else
                    {
                        lsenquiry_type = "Services";
                    }

                }

                msSQL = " insert into smr_tmp_tsalesenquiry( " +
                        " tmpsalesenquiry_gid , " +
                        " enquiry_gid, " +
                        " productgroup_gid, " +
                        " product_gid, " +
                        " potential_value," +
                        " uom_gid," +
                        " qty_requested, " +
                        " user_gid, " +
                        " product_requireddate," +
                        " enquiry_type) " +
                        " values( " +
                         "'" + msGetGid + "'," +
                         "'" + values.enquiry_gid + "'," +
                        "'" + lsproductgroupgid + "'," +
                        "'" + lsproductgid + "'," +
                        "'" + values.potential_value.Replace(", ", "") + "'," +
                        "'" + lsproductuomgid + "'," +
                        "'" + values.qty_requested + "', " +
                        "'" + employee_gid + "', " +
                        "'" + product_requireddate + "'," +
                        "'" + lsenquiry_type + "') ";
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
                values.message = "Exception occured while Adding Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           

        }

        // Currency
        public void DaGetCurrencyDets(MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
           


                msSQL = "currencyexchange_gid,currency_code,default_currency,exchange_rate from crm_trn_tcurrencyexchange order by currency_code asc ";
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
                            default_currency = dt["default_currency"].ToString(),
                            exchange_rate = dt["exchange_rate"].ToString(),

                        });
                        values.GetCurrencyDets = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Currency Code !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }


        // Currency
        public void DaGetBranchDet(MdlSmrTrnCustomerEnquiry values)
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
                values.message = "Exception occured while Getting Branch Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }


        //Product for raise quote

        public void DaGetProductDets(MdlSmrTrnCustomerEnquiry values)
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
                values.message = "Exception occured while Getting Product Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        // on change product for enquiry to quotation
        public void DaGetOnChangeProductNameDets(string product_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
              

                if (product_gid != null)
                {
                    msSQL = " Select a.product_name, a.product_code,a.cost_price, b.productuom_gid,b.productuom_name,c.productgroup_name,c.productgroup_gid,a.productuom_gid  from pmr_mst_tproduct a  " +
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
                                unitprice = dt["cost_price"].ToString()

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
                values.message = "Exception occured while Getting Product Detailes !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        // Tax 1
        public void DaGetFirstTax(MdlSmrTrnCustomerEnquiry values)
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
                values.message = "Exception occured while Getting Tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        // Tax 2
        public void DaGetSecondTax(MdlSmrTrnCustomerEnquiry values)
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
                values.message = "Exception occured while Getting Tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        // Tax 3
        public void DaGetThirdTax(MdlSmrTrnCustomerEnquiry values)
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
                values.message = "Exception occured while Getting Tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }

        // Tax 4
        public void DaGetFourthTax(MdlSmrTrnCustomerEnquiry values)
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
                values.message = "Exception occured while Getting Tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        //summary bind

        public void DaGetQOSummary(string enquiry_gid, string employee_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {

                //double product_total = 0.00;
                //double grand_total = 0.00;
                msSQL = " select  a.enquiry_gid,a.customerbranch_gid,a.enquiry_referencenumber,DATE_FORMAT(a.enquiry_date,'%d-%m-%Y') as enquiry_date,a.enquiry_remarks,a.contact_number," +
                        " d.branch_name,a.contact_email,a.contact_person, contact_address as customer_address,a.leadbank_gid,a.customer_name,a.customer_gid, i.product_name," +
                        " i.qty_quoted, format(i.price, 2) as price,i.product_code, i.uom_name from smr_trn_tsalesenquiry a " +
                        " left join crm_trn_tleadbank b on b.customer_gid = a.customer_gid  left join hrm_mst_tbranch d on a.branch_gid = d.branch_gid" +
                        " left join smr_tmp_treceivequotationdtl i on a.enquiry_gid = i.enquiry_gid   where a.enquiry_gid = '" + enquiry_gid + "' group by a.enquiry_gid, i.product_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetQOSummaryList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        // grand_total += double.Parse(dt["price"].ToString());
                        // product_total += double.Parse(dt["price"].ToString());
                        getModuleList.Add(new GetQOSummaryList
                        {
                            enquiry_gid = dt["enquiry_gid"].ToString(),
                            quotation_date = dt["enquiry_date"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            customer_contact = dt["contact_person"].ToString(),
                            customer_mobile = dt["contact_number"].ToString(),
                            customer_email = dt["contact_email"].ToString(),
                            so_remarks = dt["enquiry_remarks"].ToString(),
                            customer_address = dt["customer_address"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            productuom_name = dt["uom_name"].ToString(),
                            quantity = dt["qty_quoted"].ToString(),
                            totalamount = dt["price"].ToString(),
                            producttotalamount = dt["price"].ToString(),
                            grandtotal = dt["price"].ToString(),
                        });
                        values.Quote_list = getModuleList;

                    }
                    dt_datatable.Dispose();
                    //values.grandtotal = Math.Round(grand_total, 2);
                    //values.producttotalamount = Math.Round(product_total, 2);





                    msSQL = " select a.enquiry_gid, a.enquirydtl_gid, a.product_gid, a.productgroup_gid,b.product_name, a.uom_gid, b.product_code," +
                            " a.qty_enquired,c.productgroup_name, d.productuom_name,a.potential_value, a.created_by from smr_trn_tsalesenquirydtl a" +
                            " left join pmr_mst_tproduct b on a.product_gid = b.product_gid" +
                            " left join pmr_mst_tproductgroup c on a.productgroup_gid = a.productgroup_gid" +
                            " left join pmr_mst_tproductuom d on a.uom_gid = d.productuom_gid" +
                            " where a.enquiry_gid='" + enquiry_gid + "'  AND enquirydtl_gid  NOT IN(SELECT enquirydtl_gid FROM smr_tmp_treceivequotationdtl WHERE enquiry_gid ='" + enquiry_gid + "') group by a.product_gid";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            QuoatationGID = objcmnfunctions.GetMasterGID("VQDC");
                            TempQuoatationGID = objcmnfunctions.GetMasterGID("VQDT");

                            msSQL = " insert into smr_tmp_treceivequotationdtl (" +
                                                   " tmpquotationdtl_gid, " +
                                                   " quotation_gid," +
                                                   " enquiry_gid," +
                                                   " enquirydtl_gid," +
                                                   " product_gid," +
                                                   " productgroup_gid," +
                                                   " productgroup_name," +
                                                   " quotation_type," +
                                                   " product_name," +
                                                   " product_code," +
                                                   " uom_name," +
                                                   " uom_gid," +
                                                   " created_by, " +
                                                   " qty_quoted," +
                                                   " price" +
                                                   " )values(" +
                                                   "'" + TempQuoatationGID + "'," +
                                                   "'" + QuoatationGID + "'," +
                                                   "'" + enquiry_gid + "'," +
                                                   "'" + dt["enquirydtl_gid"] + "', " +
                                                   "'" + dt["product_gid"] + "', " +
                                                   "'" + dt["productgroup_gid"] + "', " +
                                                   "'" + dt["productgroup_name"] + "', " +
                                                   "'" + lsenquiry_type + "'," +
                                                   "'" + dt["product_name"] + "', " +
                                                   "'" + dt["product_code"] + "', " +
                                                   "'" + dt["productuom_name"] + "', " +
                                                   "'" + dt["uom_gid"] + "', " +
                                                   "'" + dt["created_by"] + "', " +
                                                   "'" + dt["qty_enquired"] + "'," +
                                                   "'" + dt["potential_value"].ToString().Replace(", ", "") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                values.message = "Exception occured while Submitting Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
        
           
        }

        // Temp summary for enquiry to quotation

        public void DaGetTempSummary(string enquiry_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
                
                double grand_total = 0.00;

                msSQL = " select enquiry_gid, tmpquotationdtl_gid, quotation_gid,product_gid, productgroup_gid,productgroup_name," +
                        " product_name,product_price,qty_quoted,format(discount_percentage,2) as discount_percentage," +
                        " format(discount_amount,2) as discount_amount,uom_gid,uom_name,format(price,2) as price,tax_name,  " +
                        " tax1_gid,slno,price,tax_percentage,tax_amount,product_code from smr_tmp_treceivequotationdtl " +
                        " where enquiry_gid='" + enquiry_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetTempsummary>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        grand_total += double.Parse(dt["price"].ToString());
                        getModuleList.Add(new GetTempsummary

                        {
                            tmpquotationdtl_gid = dt["tmpquotationdtl_gid"].ToString(),
                            enquiry_gid = dt["enquiry_gid"].ToString(),
                            quotation_gid = dt["quotation_gid"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_code = dt["product_code"].ToString(),                            
                            productgroup_name = dt["productgroup_name"].ToString(),                           
                            product_name = dt["product_name"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            quantity = dt["qty_quoted"].ToString(),
                            discountpercentage = dt["discount_percentage"].ToString(),
                            discountamount = dt["discount_amount"].ToString(),                           
                            productuom_name = dt["uom_name"].ToString(),                            
                            totalamount = dt["price"].ToString(),
                            grand_total = dt["price"].ToString(),
                            tax_name = dt["tax_name"].ToString(),                           
                            slno = dt["slno"].ToString(),                            
                            tax_amount = dt["tax_amount"].ToString(),                        
                           

                        });
                        values.EnqtoQuote_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
                values.grand_total = grand_total;
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Temporary Product Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        // Tax 4
        public void DaGetEmployeePerson(MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
               


                msSQL = " select a.employee_gid,d.campaign_gid,d.campaign_title,concat(e.department_name,' / ',b.user_firstname,' / ',b.user_code) as user_firstname " +
                        " from hrm_mst_temployee a " +
                        " inner join adm_mst_tuser b on b.user_gid = a.user_gid " +
                        " inner join smr_trn_tcampaign2employee c on c.employee_gid = a.employee_gid " +
                        " inner join smr_trn_tcampaign d on d.campaign_gid = c.campaign_gid " +
                        " left join hrm_mst_tdepartment e on a.department_gid = e.department_gid " +
                        " where e.department_name='Technical' group by employee_gid ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetAssignDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetAssignDropdown

                        {
                            employee_gid = dt["employee_gid"].ToString(),
                            campaign_gid = dt["campaign_gid"].ToString(),
                            user_firstname = dt["user_firstname"].ToString(),
                            campaign_title = dt["campaign_title"].ToString(),

                        });
                        values.GetEmployeePerson = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Employee Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        //Terms And Condition Dropdown

        public void DaGetTerms(MdlSmrTrnCustomerEnquiry values)

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
                values.message = "Exception occured while Getting Template Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }

        public void DaGetOnChangeTerms(string template_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
                
                if (template_gid != null)
                {
                    msSQL = " select a.template_gid, c.template_name, c.template_content from adm_trn_ttemplate2module a " +
                   " left join adm_mst_tmodule b on a.module_gid = b.module_gid " +
                   " left join adm_mst_ttemplate c on a.template_gid = c.template_gid " +
                   " left join adm_mst_ttemplatetype d on c.templatetype_gid = d.templatetype_gid " +
                   " where a.module_gid = 'SMR' and c.template_gid = '" + template_gid + "' ";
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
                                template_content = dt["template_content"].ToString(),
                            });
                            values.terms_lists = getModuleList;
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Template Detailes !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        //product Add
        public void DaGetOnProductAdd(string employee_gid, Productsummarys_lists values)
        {
            try
            {
               
                msGetGid = objcmnfunctions.GetMasterGID("VQDT");
                QuoatationGID = objcmnfunctions.GetMasterGID("VQNP");

                msSQL = "select product_gid from pmr_mst_tproduct where product_name='" + values.product_name + "'";
                string lsproductgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "Select productgroup_gid from pmr_mst_tproductgroup where productgroup_name='" + values.productgroup_name + "'";
                string lsproductgroupgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select productuom_gid from pmr_mst_tproductuom where productuom_name='" + values.productuom_name + "'";
                string lsproductuomgid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select tax_name from acp_mst_ttax where tax_gid='" + values.tax_name + "'";
                string lstaxname1 = objdbconn.GetExecuteScalar(msSQL);                

                msSQL = "select percentage from acp_mst_ttax where tax_gid='" + values.tax_name + "'";
                string lspercentage1 = objdbconn.GetExecuteScalar(msSQL);
               

                msSQL = " insert into smr_tmp_treceivequotationdtl (" +
                                    " tmpquotationdtl_gid, " +
                                    " quotation_gid," +
                                    " product_price," +                                    
                                    " product_gid," +
                                    " productgroup_gid," +
                                    " productgroup_name," +                                    
                                    " product_name," +
                                    " product_code," +                                   
                                    " uom_name," +
                                    " uom_gid," +
                                    " created_by, " +
                                    " quotation_type, " +
                                    " qty_quoted," +
                                    " discount_percentage," +
                                    " discount_amount," +
                                    " tax_percentage," +
                                    " tax_amount," +
                                    " tax_name," +
                                    " tax1_gid," +                                   
                                    " price," +
                                    "enquiry_gid" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + QuoatationGID + "'," +
                                    "'" + values.unitprice + "'," +                                   
                                    "'" + lsproductgid + "', " +
                                    "'" + lsproductgroupgid + "', " +
                                    "'" + values.productgroup_name + "', " +                                    
                                    "'" + values.product_name + "', " +
                                    "'" + values.product_code + "', " +                                   
                                    "'" + values.productuom_name + "', " +
                                    "'" + lsproductuomgid + "', " +
                                    "'" + employee_gid + "', " +
                                    "'" + values.quotation_type + "', " +
                                    "'" + values.quantity + "'," +
                                    "'" + values.discountpercentage + "'," +
                                    "'" + values.discountamount + "'," +
                                    "'" + lspercentage1 + "'," +
                                    "'" + values.tax_amount + "'," +
                                    "'" + lstaxname1 + "'," +
                                    "'" + values.tax_name + "'," +
                                    "'" + values.totalamount + "'," +
                                    "'" + values.enquiry_gid + "')";
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
                values.message = "Exception occured while  Adding Product !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }

        // delete Product
        public void DaGetDeleteEnquiryProductSummary(string tmpsalesenquiry_gid, productsummarys_list values)
        {
            try
            {
               
                msSQL = "delete from smr_tmp_tsalesenquiry where tmpsalesenquiry_gid='" + tmpsalesenquiry_gid + "'  ";
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
                values.message = "Exception occured while  Deleting Product !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }

        public void DaGetDeleteEnquiryEditProductSummary(string tmpsalesenquiry_gid, productsummarys_list values)
        {
            try
            {

                msSQL = "delete from smr_tmp_tsalesenquiry where tmpsalesenquiry_gid='" + tmpsalesenquiry_gid + "'";
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
                values.message = "Exception occured while Deleting Product !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
          
        }

        // Delete for Enquiry to Quote
        public void DaGetDeleteQuoteProductSummary(string tmpquotationdtl_gid, productsummarys_list values)
        {
            try
            {
               
                msSQL = "delete from smr_tmp_treceivequotationdtl where tmpquotationdtl_gid='" + tmpquotationdtl_gid + "'  ";
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
                values.message = "Exception occured while Deleting Product !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }


        public void DaPostCustomerEnquiry(string employee_gid, string user_gid, PostAll values)

        {
            try
            {
               

                msSQL = "SELECT * FROM smr_tmp_tsalesenquiry WHERE created_by='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Select one Product to Raise Enquiry";
                }

                msGetGid1 = objcmnfunctions.GetMasterGID("PPTP");

                if (msGetGid1 == "E") // Assuming "E" is a string constant
                {
                    values.status = true;
                    values.message = "Create Sequence Code PPTP for Sales Enquiry Details";
                }


                msSQL = "SELECT DISTINCT " +
                    "a.product_gid, a.product_remarks, a.customerproduct_code, a.potential_value,a.created_by," +
                    "a.qty_requested, a.uom_gid, a.display_field,a.product_requireddate, a.product_requireddateremarks, " +
                    "a.productgroup_gid" +
                    " FROM smr_tmp_tsalesenquiry a WHERE" +
                    " a.created_by = '" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("PPDC");

                        if (msGetGid == "E") // Assuming "E" is a string constant
                        {
                            values.status = true;
                            values.message = "Create Sequence Code PPDC for Sales Enquiry Details";
                        }

                        string lsnewproduct_flag = "Y";

                        msSQL = " Insert into smr_trn_tsalesenquirydtl " +
                               " (enquirydtl_gid," +
                               " enquiry_gid , " +
                               //" customerproduct_code," +
                               " product_gid," +
                               " potential_value," +
                               " uom_gid," +
                               " productgroup_gid," +
                               " qty_enquired, " +
                               " created_by, " +
                               " newproduct_flag, " +
                               " product_requireddate) " +
                               //" product_requireddateremarks," +
                               //" display_field 
                               " values (" +
                               "'" + msGetGid + "'," +
                               "'" + msGetGid1 + "'," +
                               //"'" + dt["customerproduct_code"].ToString() + "'," +
                               "'" + dt["product_gid"].ToString() + "'," +
                               "'" + dt["potential_value"].ToString() + "'," +
                               "'" + dt["uom_gid"].ToString() + "'," +
                               "'" + dt["productgroup_gid"].ToString() + "'," +
                               "'" + dt["qty_requested"].ToString() + "', " +
                               "'" + dt["created_by"].ToString() + "', " +
                               "'" + lsnewproduct_flag + "', ";

                        if (dt["product_requireddate"].ToString() == null || DBNull.Value.Equals(dt["product_requireddate"].ToString()))
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(dt["product_requireddate"]).ToString("yyyy-MM-dd") + "') ";


                        }
                        //msSQL += "'" + dt["product_requireddateremarks"].ToString() + "',";
                        //msSQL += "'" + dt["display_field"].ToString() + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        if (mnResult != 0)
                        {
                            msSQL = " Insert into acp_trn_tenquirydtl " +
                                " (enquirydtl_gid," +
                                " enquiry_gid , " +
                                //" customerproduct_code," +
                                " product_gid," +
                                " potential_value," +
                                " uom_gid," +
                                " productgroup_gid," +
                                " qty_enquired, " +
                                " created_by, " +
                                " product_requireddate) " +
                                //" product_requireddateremarks," +
                                //" display_field ) " +
                                " values (" +
                                "'" + msGetGid + "'," +
                                "'" + msGetGid1 + "'," +
                               //"'" + dt["customerproduct_code"].ToString() + "'," +
                               "'" + dt["product_gid"].ToString() + "'," +
                               "'" + dt["potential_value"].ToString() + "'," +
                               "'" + dt["uom_gid"].ToString() + "'," +
                               "'" + dt["productgroup_gid"].ToString() + "'," +
                               "'" + dt["qty_requested"].ToString() + "'," +
                               "'" + dt["created_by"].ToString() + "',";

                            if (dt["product_requireddate"].ToString() == null || DBNull.Value.Equals(dt["product_requireddate"].ToString()))
                            {
                                msSQL += "null";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(dt["product_requireddate"]).ToString("yyyy-MM-dd") + "') ";

                            }
                            //    msSQL += "'" + dt["product_requireddateremarks"].ToString() + "',";
                            //    msSQL += "'" + dt["display_field"].ToString() + "')";
                        }
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {
                    values.message = "Please add the product to raise enquiry";
                }
                if (mnResult == 1)
                {

                    string lsenquiry_status = "Enquiry Raised";
                    string lspurchaseenquiry_flag = "Enquiry Raised";
                    string lslead_status = "Assigned";
                    //string lsenquiry_status = "Enquiry Raised";

                    msSQL = "select sum(potential_value) as potential_value from smr_trn_tsalesenquirydtl where enquiry_gid='" + msGetGid1 + "'";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader.HasRows == true)
                    {
                        lspotential_value = objOdbcDataReader["potential_value"].ToString();

                    }

                    if (values.customer_gid == null)
                    {
                        msSQL = " select customer_gid from crm_trn_tleadbank where leadbank_gid='" + values.leadbank_gid + "' ";
                        values.customer_gid = objdbconn.GetExecuteScalar(msSQL);
                    }
                    else
                    {
                        msSQL = " select leadbank_gid from crm_trn_tleadbank where customer_gid='" + values.customer_gid + "' ";
                        lsleadbank_gid = objdbconn.GetExecuteScalar(msSQL);

                        string uiDateStr = values.enquiry_date;
                        DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        string enquiry_date = uiDate.ToString("yyyy-MM-dd");

                        if (values.closure_date == null || values.closure_date == "")
                        {
                            closure_date = "0000-00-00";
                        }
                        else
                        {
                            string uiDateStr2 = values.closure_date;
                            DateTime uiDate2 = DateTime.ParseExact(uiDateStr2, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            closure_date = uiDate2.ToString("yyyy-MM-dd");
                        }

                        msSQL = " Insert into smr_trn_tsalesenquiry " +
                                    " (enquiry_gid, " +
                                    " branch_gid, " +
                                    " leadbank_gid, " +
                                    " customer_gid, " +
                                    " customer_name, " +
                                    " contact_number, " +
                                    " contact_person, " +
                                    " contact_email, " +
                                    " customerbranch_gid," +
                                    " contact_address, " +
                                    " enquiry_type, " +
                                    " enquiry_date, " +
                                    " enquiry_remarks, " +
                                    " enquiry_status, " +
                                    " enquiry_referencenumber, " +
                                    " closure_date," +
                                    " created_by, " +
                                    " created_date, " +
                                    " purchaseenquiry_flag, " +
                                    " potorder_value," +
                                    " customer_requirement," +
                                    " landmark," +
                                    " lead_status," +
                                    " enquiry_assignedby, " +
                                    " product_count)" +
                                    " values (" +
                                    "'" + msGetGid1 + "', " +
                                    "'" + values.branch_name + "', " +
                                    "'" + lsleadbank_gid + "'," +
                                    "'" + values.customer_gid + "'," +
                                    "'" + values.customer_name + "', " +
                                    "'" + values.contact_number + "'," +
                                    "'" + values.customercontact_name + "'," +
                                    "'" + values.contact_email + "'," +
                                    "'" + values.customerbranch_name + "'," +
                                    "'" + values.contact_address + "'," +
                                     "'" + lsenquiry_type + "'," +
                                    "'" + enquiry_date + "', " +
                                    "'" + values.enquiry_remarks + "', " +
                                    "' " + lsenquiry_status + "'," +
                                    "'" + values.enquiry_referencenumber + "', " +
                                    "'" + closure_date + "', ";                       
                        msSQL += "'" + employee_gid + "', " +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss") + "', " +
                                 "'" + lspurchaseenquiry_flag + "'," +
                                 "'" + lspotential_value + "'," +
                                 "'" + values.customer_requirement + "'," +
                                 "'" + values.landmark + "'," +
                                 "'" + lslead_status + "'," +
                                 "'" + employee_gid + "', " +
                                 "'" + dt_datatable.Rows.Count + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                    string lsenquiry_flag = "PR Pending Approval";

                    if (mnResult != 0)
                    {
                        string uiDateStr = values.enquiry_date;
                        DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        string enquiry_date = uiDate.ToString("yyyy-MM-dd");

                        msSQL = " Insert into acp_trn_tenquiry " +
                            " (enquiry_gid, " +
                            " branch_gid, " +
                            " leadbank_gid, " +
                            " customer_gid," +
                            " customer_name, " +
                            " contact_number, " +
                            " contact_person, " +
                            " contact_email, " +
                            " customerbranch_gid," +
                            " contact_address, " +
                            " enquiry_date, " +
                            " enquiry_remarks, " +
                            " enquiry_status, " +
                            " enquiry_referencenumber, " +
                            " customer_requirement," +
                            " landmark," +
                            " created_by, " +
                            " created_date, " +
                            " purchaseenquiry_flag, " +
                            " enquiry_assignedby, " +
                            " product_count)" +
                            " values (" +
                            "'" + msGetGid1 + "', " +
                            "'" + values.branch_name + "'," +
                            "'" + lsleadbank_gid + "'," +
                            "'" + values.customer_gid + "'," +
                            "'" + values.customer_name + "'," +
                            "'" + values.contact_number + "'," +
                            "'" + values.contact_person + "'," +
                            "'" + values.contact_email + "'," +
                            "'" + values.customerbranch_name + "'," +
                            "'" + values.contact_address + "'," +
                            "'" + enquiry_date + "', " +
                            "'" + values.enquiry_remarks + "', " +
                            "' " + lsenquiry_status + "'," +
                            "'" + values.enquiry_referencenumber + "', " +
                            "'" + values.customer_requirement + "'," +
                            "'" + values.landmark + "'," +
                            "'" + employee_gid + "', " +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss") + "', " +
                            "' " + lsenquiry_flag + "', " +
                            "'" + employee_gid + "', " +
                            "'" + dt_datatable.Rows.Count + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (mnResult != 0)
                    {
                        msSQL = "select distinct enquiry_type from smr_tmp_tsalesenquiry where created_by='" + employee_gid + "' ";
                        objOdbcDataReader = objdbconn.GetDataReader(msSQL);

                        if (objOdbcDataReader.HasRows == true)
                        {
                            lsenquiry_type = "Sales";
                        }

                        else
                        {
                            lsenquiry_type = "Service";
                        }



                    }
                }

                string lslead = "Open";

                msSQL = " update smr_trn_tsalesenquiry set enquiry_type='" + lsenquiry_type + "' where enquiry_gid='" + msGetGid1 + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " update acp_trn_tenquiry set enquiry_type='" + lsenquiry_type + "' where enquiry_gid='" + msGetGid1 + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msgetlead2campaign_gid = objcmnfunctions.GetMasterGID("BLCC");
                lscampaign_gid = objcmnfunctions.GetMasterGID("BCNP");

                if (msgetlead2campaign_gid == "E")
                {
                    values.status = true;
                    values.message = "Create sequence code BLCC for Enquiry to campaign ";
                }

                msSQL = " Insert into crm_trn_tenquiry2campaign ( " +
                                                    " lead2campaign_gid, " +
                                                    " enquiry_gid, " +
                                                    " created_by, " +
                                                    " created_date, " +
                                                    " lead_status, " +
                                                    " customer_rating, " +
                                                    " leadstage_gid, " +
                                                    " campaign_gid," +
                                                    " assign_to ) " +
                                                    " Values ( " +
                                                    "'" + msgetlead2campaign_gid + "'," +
                                                    "'" + msGetGid1 + "'," +
                                                    "'" + user_gid + "'," +
                                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                                    "' " + lslead + " '," +
                                                    "'" + values.customer_rating + "'," +
                                                    "'" + lsleadstage + "', " +
                                                    "'" + lscampaign_gid + "'," +
                                                    "'" + values.user_firstname + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                lslead_status = "Enquiry Raised";
                msSQL = " update smr_trn_tsalesenquiry Set " +
                                   " lead_status = '" + lslead_status + "' " +
                                   " where enquiry_gid = '" + msGetGid1 + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = " select customer_gid from crm_trn_tleadbank " +
                        " where leadbank_gid='" + lsleadbank_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        if (DBNull.Value.Equals(dt["customer_gid"]))
                        {
                            lsleadstage = "6";
                        }
                        else
                        {
                            msSQL = " select enquiry_gid from smr_trn_tsalesenquiry where customer_gid='" + values.customer_gid + "'";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable.Rows.Count != 0)
                            {
                                lsleadstage = "3";
                            }

                        }


                        msSQL = " update crm_trn_tlead2campaign  set " +
                                            " leadstage_gid='" + lsleadstage + "'" +
                                            " where leadbank_gid='" + lsleadbank_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }

                else
                {


                    if (mnResult != 0)
                    {
                        string msgetlead2campaign_gid = objcmnfunctions.GetMasterGID("BLCC");
                        lscampaign_gid = objcmnfunctions.GetMasterGID("BCNP");
                        msSQL = " Insert into crm_trn_tenquiry2campaign ( " +
                                                  " lead2campaign_gid, " +
                                                  " enquiry_gid, " +
                                                  " created_by, " +
                                                  " created_date, " +
                                                  " lead_status, " +
                                                  " customer_rating, " +
                                                  " leadstage_gid, " +
                                                  " campaign_gid," +
                                                  " assign_to ) " +
                                                  " Values ( " +
                                                  "'" + msgetlead2campaign_gid + "'," +
                                                  "'" + msGetGid1 + "'," +
                                                  "'" + user_gid + "'," +
                                                  "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                                  "' " + lslead + " '," +
                                                  "'" + values.customer_rating + "'," +
                                                  "'" + lsleadstage + "', " +
                                                  "'" + lscampaign_gid + "'," +
                                                  "'" + values.user_firstname + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                    msSQL = " update smr_trn_tsalesenquiry Set " +
                               " lead_status = '" + lslead_status + "' " +
                               " where enquiry_gid = '" + msGetGid1 + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select customer_gid from crm_trn_tleadbank " +
                       " where leadbank_gid='" + lsleadbank_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            if (DBNull.Value.Equals(dt["customer_gid"]))
                            {
                                lsleadstage = "1";
                            }
                            else
                            {
                                msSQL = " select enquiry_gid from smr_trn_tsalesenquiry where customer_gid='" + values.customer_gid + "'";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                if (dt_datatable.Rows.Count != 0)
                                {
                                    lsleadstage = "5";
                                }

                            }


                            msSQL = " update crm_trn_tlead2campaign  set " +
                                      " leadstage_gid='" + lsleadstage + "'" +
                                      " where leadbank_gid='" + lsleadbank_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }


                    }

                }
                if (mnResult != 0 || mnResult == 0)
                {
                    msSQL = "delete FROM smr_tmp_tsalesenquiry WHERE created_by='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Enquiry Raised Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Raising Enquiry";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Raising Enquiry !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaPostCustomerEnquirytoQuotation(string employee_gid, postquotation_list values)
        {
            try
            {
              
                string quorefno_V1 = objcmnfunctions.GetMasterGID("VQDC");
                int i = 0;
                msSQL = " select " +
                        " tmpquotationdtl_gid," +
                        " quotation_gid," +
                        " enquiry_gid," +
                        " product_gid," +
                        " product_code," +
                        " productgroup_gid," +
                        " productgroup_name," +                       
                        " product_name," +                       
                        " product_price," +
                        " qty_quoted," +
                        " format(discount_percentage,2) as margin_percentage," +
                        " format(discount_amount,2) as margin_amount, " +
                        " uom_gid," +
                        " uom_name," +                     
                        " format(price,2) as price," +
                         " tax_name, " +                                           
                         " tax_percentage," +                        
                         " tax_amount " +                        
                         " from smr_tmp_treceivequotationdtl  where enquiry_gid='" + values.enquiry_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetReceiveQuoteDtl_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetReceiveQuoteDtl_List
                        {
                            tmpquotationdtl_gid = dt["tmpquotationdtl_gid"].ToString(),
                            quotation_gid = dt["quotation_gid"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),                            
                            product_name = dt["product_name"].ToString(),                            
                            product_price = dt["product_price"].ToString(),
                            qty_quoted = dt["qty_quoted"].ToString(),
                            margin_percentage = dt["margin_percentage"].ToString(),
                            margin_amount = dt["margin_amount"].ToString(),
                            uom_gid = dt["uom_gid"].ToString(),
                            uom_name = dt["uom_name"].ToString(),                            
                            price = dt["price"].ToString(),
                            tax_name = dt["tax_name"].ToString(),                           
                            tax_percentage = dt["tax_percentage"].ToString(),                            
                            tax_amount = dt["tax_amount"].ToString(),                            
                        });

                        string msQuotationGIDdtl = objcmnfunctions.GetMasterGID("VQDC");

                        msSQL = "insert into smr_trn_treceivequotationdtl (" +
                               " quotationdtl_gid ," +
                               " quotation_gid," +
                               " product_gid ," +                               
                               " product_code ," +                               
                               " productgroup_gid," +
                               " productgroup_name," +
                               " product_name," +                              
                               " product_price," +
                               " qty_quoted," +
                               " discount_percentage," +
                               " discount_amount," +                               
                               " uom_gid," +
                               " uom_name," +
                               " price," +                              
                               " tax_name," +                             
                               " tax_percentage," +
                               " tax_amount," +
                               " slno " +
                               ")values(" +
                               " '" + msQuotationGIDdtl + "'," +
                               " '" + quorefno_V1 + "'," +
                               " '" + dt["product_gid"].ToString() + "'," +                              
                               " '" + dt["product_code"].ToString() + "'," +                              
                               " '" + dt["productgroup_gid"].ToString() + "'," +
                               " '" + dt["productgroup_name"].ToString() + "'," +
                               " '" + dt["product_name"].ToString() + "'," +                              
                               " '" + dt["product_price"].ToString().Replace(",", "") + "'," +
                               " '" + dt["qty_quoted"].ToString() + "'," +
                               " '" + dt["margin_percentage"].ToString().Replace(", ", "") + "'," +
                               " '" + dt["margin_amount"].ToString().Replace(",", "") + "'," +                               
                               " '" + dt["uom_gid"].ToString() + "'," +
                               " '" + dt["uom_name"].ToString() + "'," +
                               " '" + dt["price"].ToString().Replace(",", "") + "'," +                                                         
                               " '" + dt["tax_name"].ToString() + "'," +                              
                               " '" + dt["tax_percentage"].ToString() + "'," +
                               " '" + dt["tax_amount"].ToString() + "'," +                                
                                  " '" + i + 1 + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 0)
                        {
                            values.status = false;
                            values.message = "Error While Inserting into Quotationdtl";
                            objdbconn.CloseConn();
                        }

                        //values.postquotation_list = getModuleList;
                    }
                }



                msSQL = "SELECT IFNULL(c.user_gid, '') AS user_gid FROM smr_trn_tsalesenquiry a " +
                    "LEFT JOIN hrm_mst_temployee b ON a.enquiry_assignedby = b.employee_gid " +
                    "LEFT JOIN adm_mst_tuser c ON b.user_gid = c.user_gid " +
                    "WHERE enquiry_gid = '" + values.enq + "'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows == true)
                {
                    salesperson = objOdbcDataReader["user_gid"].ToString();
                }
                objOdbcDataReader.Close();

                msSQL = " select customer_gid from crm_mst_tcustomer where customer_name='" + values.customer_name + "'";
                lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select branch_gid from hrm_mst_tbranch where branch_name='" + values.branch_name + "'";
                string lsbranchgid = objdbconn.GetExecuteScalar(msSQL); 

                msSQL = " select customerbranchcontact_gid from crm_mst_tcustomercontact where customercontact_name='" + values.customer_contact + "'";
                string lscustomerbranch = objdbconn.GetExecuteScalar(msSQL);

                string uiDateStr = values.quotation_date;
                DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string quotation_date = uiDate.ToString("yyyy-MM-dd");
               

                msSQL = "INSERT INTO smr_trn_treceivequotation ( " +
                "quotation_gid," +
                " branch_gid," +
                "quotation_date," +
                "customer_name, " +
                "customerbranch_gid," +
                "customer_contact_person," +
                "customer_gid," +
                " enquiry_gid," +
                " quotation_referencenumber," +
                " quotation_referenceno1," +
                " quotation_remarks, " +
                "quotation_status," +
                " Grandtotal," +
                " termsandconditions, " +
                "payment_days, " +
                "delivery_days, " +
                "addon_charge," +
                " created_by, " +
                "contact_no, " +
                "contact_mail, " +
                "freight_terms, " +
                "payment_terms, " +               
                "currency_code," +
                " exchange_rate," +
                " currency_gid, " +
                " pricingsheet_gid," +
                " pricingsheet_refno, " +
                " customer_address, " +
                " total_price, " +
                " total_amount, " +               
                " tax_gid, " +
                " tax_name," +
                " salesperson_gid," +
                " roundoff," +
                " customerenquiryref_number, " +
                "additional_discount," +
                " freight_charges, " +
                "buyback_charges," +
                " packing_charges, " +
                "insurance_charges )" +
                " VALUES(" +
               "'" + quorefno_V1 + "'," +
               "'" + lsbranchgid + "'," +
               "'" + quotation_date + "'," +
               "'" + values.customer_name + "'," +
               "'" + lscustomerbranch + "'," +
               "'" + values.customer_contact + "'," +
               "'" + lscustomer_gid + "'," +
               "'" + values.enq + "'," +
               "'" + values.quotation_referenceno1 + "'," +
               "'" + values.quotation_referenceno1 + "'," +
               "'" + values.remarks + "',";
                if (lsQuotationMode == "Draft")
                {
                    msSQL += "'Draft', ";
                }
                else
                {
                    msSQL += "'Approved', ";
                }

                msSQL += "'" + values.grandtotal + "'," +
                         "'" + values.template_content + "'," +
                         " '" + values.payment_days + "'," +
                         " '" + values.delivery_days + "'," +
                         "'" + values.addon_charge + "'," +
                         "'" + employee_gid + "'," +
                         "'" + values.customer_mobile + "'," +
                         "'" + values.customer_email + "'," +
                         "'" + values.freight_terms + "'," +
                         "'" + values.payment_terms + "'," +                        
                   "'" + values.currency_code + "'," +
                   "'" + values.exchange_rate + "'," +
                    "'" + values.currency_gid + "',";

                if (values.pricingsheet_gid == "")
                {
                    msSQL += "null, '" + values.pricingsheet_gid + "',";
                }
                else
                {
                    msSQL += "null, '" + values.pricingsheet_refno + "',";
                }

                msSQL += "'" + values.customer_address + "'," +
                    "'" + values.producttotalamount + "',";
                if (values.total_amount == null)
                {
                    msSQL += "'0.00', ";
                }
                else
                {
                    msSQL += "'" + values.total_amount + "',";

                }
               
                msSQL += "'" + values.tax_gid + "'," +
                    "'" + values.tax_name + "'," +
                    "'" + values.salesperson_gid + "',";


                if (values.roundoff != "")
                {
                    msSQL += "'" + values.roundoff + "',";
                }
                else
                {
                    msSQL += "'0.00', ";
                }

                msSQL += "'" + values.customerenquiryref_number + "',";
                if (values.additional_discount != "")
                {
                    msSQL += "'" + values.additional_discount + "',";
                }
                else
                {
                    msSQL += "'0.00', ";
                }
                if (values.freight_charges != "")
                {
                    msSQL += "'" + values.freight_charges + "',";
                }
                else
                {
                    msSQL += "'0.00', ";
                }
                if (values.buyback_charges != "")
                {
                    msSQL += "'" + values.buyback_charges + "',";
                }
                else
                {
                    msSQL += "'0.00', ";
                }
                if (values.packing_charges != "")
                {
                    msSQL += "'" + values.packing_charges + "',";
                }
                else
                {
                    msSQL += "'0.00', ";
                }
                if (values.insurance_charges != "")
                {
                    msSQL += "'" + values.insurance_charges + "')";
                }
                else
                {
                    msSQL += "'0.00') ";
                }

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 0)
                {
                    values.status = false;
                    values.message = "Error While Inserting Quotation";
                    objdbconn.CloseConn();
                }
                // Now 'msSQL' contains the complete SQL INSERT statement
                else
                {
                    msSQL = " update smr_trn_tsalesenquiry set enquiry_status='Quotation Raised' where enquiry_gid='" + values.enq + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = " update crm_trn_tenquiry2campaign set leadstage_gid='5',updated_date= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where enquiry_gid='" + values.enq + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select distinct quotation_type from smr_tmp_treceivequotationdtl where created_by= '" + employee_gid + "'";

                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader.HasRows == true)
                    {
                        if (objOdbcDataReader["quotation_type"].ToString() != "Services")
                        {
                            quotation_type = "Sales";
                        }
                        else
                        {
                            quotation_type = "Services";
                        }
                        msSQL = " update smr_trn_treceivequotation set quotation_type='" + quotation_type + "' where quotation_gid='" + quorefno_V1 + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0 || mnResult == 0)
                        {
                            msSQL = "delete from smr_tmp_treceivequotationdtl where created_by= '" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }


                        if (mnResult == 1)
                        {
                            values.status = true;
                            values.message = "Quotation Raised Successfully";

                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Raising Quotation";

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Raising Quotation !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            

        }

        public void DaGetSmrTrnRaiseProposal(string enquiry_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
              
                msSQL = "select customer_name,customer_gid, enquiry_gid from smr_trn_tsalesenquiry where enquiry_gid='" + enquiry_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<proposal_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new proposal_list
                        {
                            enquiry_gid = dt["enquiry_gid"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            customer_gid = dt["customer_gid"].ToString()

                        });
                        values.proposal_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Customer !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }

        public void DaGetDocumentType(MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
                
                msSQL = " select a.template_gid, a.template_name, a.template_content from adm_mst_ttemplate a " +
                " left join adm_mst_ttemplatetype b on b.templatetype_gid = a.templatetype_gid " +
                " where a.templatetype_gid='1' ";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<document_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new document_list
                        {
                            template_gid = dt["template_gid"].ToString(),
                            template_name = dt["template_name"].ToString(),
                        });
                        values.document_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Template Type !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        public void DaUploaddocument(HttpRequest httpRequest, string user_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
             
                msSQL = " select doc_gid,file_name,file_path,temp_gid" +
                        " FROM crm_mst_ttemplatedoc where doc_gid='";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<uploaddocument>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new uploaddocument
                        {

                            file_name = dt["file_name"].ToString(),
                            doc_gid = dt["doc_gid"].ToString(),
                            file_path = dt["document_path"].ToString(),
                        });
                        values.uploaddocument = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting File Name !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }

        public void DaGetProposalSummary(string enquiry_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
                
                msSQL = " SELECT a.proposal_gid, date_format(a.created_date,'%d-%m-%Y') as created_date, a.enquiry_gid, a.template_name, concat(c.user_firstname,' ',c.user_lastname) as created_by,b.customer_name,b.customer_gid,a.proposal_from,a.document_path " +
                 " FROM crm_mst_tproposaltemplate a" +
                 " LEFT JOIN smr_trn_tsalesenquiry b ON b.enquiry_gid = a.enquiry_gid " +
                 " left join adm_mst_tuser c on c.user_gid= a.created_by " +
                 " where a.enquiry_gid='" + enquiry_gid + "'";



                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<proposalsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new proposalsummary_list
                        {
                            enquiry_gid = dt["enquiry_gid"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            customer_gid = dt["customer_gid"].ToString(),
                            proposal_gid = dt["proposal_gid"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            template_name = dt["template_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            proposal_from = dt["proposal_from"].ToString(),
                            document_path = dt["document_path"].ToString(),


                        });
                        values.proposalsummary_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Proposal Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }


        public void DaPostproposal(HttpRequest httpRequest, result objResult, string user_gid)
        {
            try
            {
              
                HttpFileCollection httpFileCollection;
                string lsfilepath = string.Empty;
                string lsdocument_gid = string.Empty;
                MemoryStream ms_stream = new MemoryStream();
                string document_gid = string.Empty;
                string lscompany_code = string.Empty;
                HttpPostedFile httpPostedFile;

                string lspath;
                string msGetGid;
                string enquiry_gid = httpRequest.Form["enquiry_gid"];
                string proposal_name = httpRequest.Form["proposal_name"];
                string template_content = httpRequest.Form["template_content"];
                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);


                MemoryStream ms = new MemoryStream();
                lspath = ConfigurationManager.AppSettings["upload_file"] + "erpdocument" + "/" + lscompany_code + "/" + "RaiseProposal/upload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(lspath)))
                        System.IO.Directory.CreateDirectory(lspath);
                }
                try
                {
                    if (httpRequest.Files.Count > 0)
                    {
                        string lsfirstdocument_filepath = string.Empty;
                        httpFileCollection = httpRequest.Files;
                        for (int i = 0; i < httpFileCollection.Count; i++)
                        {
                            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                            httpPostedFile = httpFileCollection[i];
                            string FileExtension = httpPostedFile.FileName;
                            string lsfile_gid = msdocument_gid;
                            string lscompany_document_flag = string.Empty;
                            FileExtension = Path.GetExtension(FileExtension).ToLower();
                            lsfile_gid = lsfile_gid + FileExtension;
                            Stream ls_readStream;
                            ls_readStream = httpPostedFile.InputStream;
                            ls_readStream.CopyTo(ms);

                            lspath = ConfigurationManager.AppSettings["upload_file"] + "/erpdocument" + "/" + lscompany_code + "/" + "RaiseProposal/upload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                            string status;
                            status = objcmnfunctions.uploadFile(lspath + msdocument_gid, FileExtension);

                            ms.Close();
                            lspath = "assets/media/images/erpdocument" + "/" + lscompany_code + "/" + "RaiseProposal/upload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension; ; ;

                            string final_path = lspath + msdocument_gid + FileExtension;



                            msGetGid = objcmnfunctions.GetMasterGID("BPPL");


                            msSQL = " Insert into crm_mst_tproposaltemplate ( " +
                                                           " proposal_gid, " +
                                                           " enquiry_gid, " +
                                                           " template_name, " +
                                                           " template_content, " +
                                                           " document_path, " +
                                                           " proposal_from," +
                                                           " created_date, " +
                                                           " created_by ) " +
                                                           " Values ( " +
                                                           "'" + msGetGid + "'," +
                                                           "'" + enquiry_gid + "'," +
                                                           "'" + proposal_name + "'," +
                                                           "'" + template_content + "'," +
                                                           "' " + lspath + " '," +
                                                           "'enquiry', " +
                                                           "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                                           "'" + user_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                objResult.status = true;
                                objResult.message = "Proposal Raised  Successfully";
                            }
                            else
                            {
                                objResult.status = false;
                                objResult.message = "Error While Proposal Raised";
                            }

                        }

                    }

                }
                catch (Exception ex)
                {
                    objResult.message = ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                objResult.message = "Exception occured while Proposal Raised!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                objResult.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           


        }


        public void DaGetViewEnquirySummary(string enquiry_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
              
                msSQL = "select distinct a.enquiry_gid,d.customercontact_gid,a.enquiry_date,b.branch_gid,b.branch_name,a.leadbank_gid," +
              " a.customer_name,a.customer_gid,  a.contact_number, a.contact_email, a.contact_address,l.user_firstname," +
              " x.customer_address, x.customer_address2,x.customer_city,x.customer_state, e.country_name, x.customer_pin,  d.mobile, d.email,a.contact_person," +
              " a.customerbranch_gid, a.enquiry_remarks, a.enquiry_referencenumber, a.customer_requirement,a.landmark,a.closure_date," +
              " format(g.potential_value, 2) as potential_value,g.qty_enquired,g.product_requireddate," +
              " i.product_code,i.product_name,c.productuom_name,k.productgroup_name " +
              " from smr_trn_tsalesenquiry a " +
              " left join hrm_mst_tbranch b on a.branch_gid = b.branch_gid " +
              " left join crm_trn_tleadbank h on a.customer_gid = h.customer_gid " +
              " left join crm_mst_tcustomer x on h.customer_gid = x.customer_gid " +
              " left join crm_mst_tcustomercontact d on d.customer_gid = x.customer_gid " +
              " left join adm_mst_tcountry e on x.customer_country = e.country_gid " +
              " left join crm_trn_tenquiry2campaign f on a.enquiry_gid = f.enquiry_gid " +
              " left join smr_trn_tsalesenquirydtl g  on a.enquiry_gid = g.enquiry_gid " +
              " left join pmr_mst_tproduct i on g.product_gid = i.product_gid " +
              " left join pmr_mst_tproductuom c on g.uom_gid = c.productuom_gid " +
              " left join pmr_mst_tproductgroup k on g.productgroup_gid = k.productgroup_gid " +
              " left join hrm_mst_temployee j on f.assign_to = j.employee_gid " +
              " left join adm_mst_tuser l on j.user_gid = l.user_gid " +
              " where a.enquiry_gid = '" + enquiry_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<enquirylist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new enquirylist
                        {
                            enquiry_gid = dt["enquiry_gid"].ToString(),
                            enquiry_date = dt["enquiry_date"].ToString().Replace("00:00:00", ""),
                            branch_name = dt["branch_name"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),
                            enquiry_referencenumber = dt["enquiry_referencenumber"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            contact_number = dt["contact_number"].ToString(),
                            contact_person = dt["contact_person"].ToString(),
                            assign_to = dt["user_firstname"].ToString(),
                            contact_email = dt["contact_email"].ToString(),
                            enquiry_remarks = dt["enquiry_remarks"].ToString(),
                            contact_address = dt["contact_address"].ToString(),
                            customer_requirement = dt["customer_requirement"].ToString(),
                            landmark = dt["landmark"].ToString(),
                            closure_date = dt["closure_date"].ToString().Replace("00:00:00", ""),
                            productgroup_name = dt["productgroup_name"].ToString(),                           
                            product_code = dt["product_code"].ToString(),
                            product_name = dt["product_name"].ToString(),                            
                            productuom_name = dt["productuom_name"].ToString(),
                            qty_requested = dt["qty_enquired"].ToString(),
                            potential_value = dt["potential_value"].ToString(),
                            product_requireddate = dt["product_requireddate"].ToString().Replace("00:00:00", ""),                            
                            customer_gid = dt["customer_gid"].ToString(),

                        });

                        values.enquiry_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while ViewEnquirySummary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");


            }
           
        }


        // EDIT CUSTOMER ENQUIRY

        public void DaGetEditEnquirySummary(string enquiry_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {

                msSQL = "select distinct a.enquiry_gid,d.customercontact_gid,DATE_FORMAT(a.enquiry_date,'%d-%m-%Y') as enquiry_date,b.branch_gid,b.branch_name,a.leadbank_gid," +
              " a.customer_name,a.customer_gid,  a.contact_number, a.contact_email, a.contact_address,l.user_firstname," +
              " x.customer_address, x.customer_address2,x.customer_city,x.customer_state, e.country_name, x.customer_pin,  d.mobile, d.email,a.contact_person," +
              " a.customerbranch_gid, a.enquiry_remarks, a.enquiry_referencenumber, a.customer_requirement,a.landmark,DATE_FORMAT(a.closure_date,'%d-%m-%Y') as closure_date," +
              " format(g.potential_value, 2) as potential_value,g.qty_requested,DATE_FORMAT(g.product_requireddate,'%d-%m-%Y') as product_requireddate, " +
              " i.product_code,i.product_name,c.productuom_name,k.productgroup_name " +
              " from smr_trn_tsalesenquiry a " +
              " left join hrm_mst_tbranch b on a.branch_gid = b.branch_gid " +
              " left join crm_trn_tleadbank h on a.customer_gid = h.customer_gid " +
              " left join crm_mst_tcustomer x on h.customer_gid = x.customer_gid " +
              " left join crm_mst_tcustomercontact d on d.customer_gid = x.customer_gid " +
              " left join adm_mst_tcountry e on x.customer_country = e.country_gid " +
              " left join crm_trn_tenquiry2campaign f on a.enquiry_gid = f.enquiry_gid " +
              " left join smr_tmp_tsalesenquiry g  on a.enquiry_gid = g.enquiry_gid " +
              " left join pmr_mst_tproduct i on g.product_gid = i.product_gid " +
              " left join pmr_mst_tproductuom c on g.uom_gid = c.productuom_gid " +
              " left join pmr_mst_tproductgroup k on g.productgroup_gid = k.productgroup_gid " +
              " left join hrm_mst_temployee j on f.assign_to = j.employee_gid " +
              " left join adm_mst_tuser l on j.user_gid = l.user_gid " +
              " where a.enquiry_gid = '" + enquiry_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<enquirylist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new enquirylist
                        {
                            enquiry_gid = dt["enquiry_gid"].ToString(),
                            enquiry_date = dt["enquiry_date"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),
                            enquiry_referencenumber = dt["enquiry_referencenumber"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            contact_number = dt["contact_number"].ToString(),
                            contact_person = dt["contact_person"].ToString(),
                            assign_to = dt["user_firstname"].ToString(),
                            contact_email = dt["contact_email"].ToString(),
                            enquiry_remarks = dt["enquiry_remarks"].ToString(),
                            contact_address = dt["contact_address"].ToString(),
                            customer_requirement = dt["customer_requirement"].ToString(),
                            landmark = dt["landmark"].ToString(),
                            closure_date = dt["closure_date"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),                           
                            product_code = dt["product_code"].ToString(),
                            product_name = dt["product_name"].ToString(),                           
                            productuom_name = dt["productuom_name"].ToString(),
                            qty_requested = dt["qty_requested"].ToString(),
                            potential_value = dt["potential_value"].ToString(),
                            product_requireddate = dt["product_requireddate"].ToString(),                            
                            customer_gid = dt["customer_gid"].ToString(),

                        });
                        values.enquiry_list = getModuleList;

                    }

                    dt_datatable.Dispose();
                }
                msSQL = " select a.enquiry_gid,a.enquirydtl_gid,DATE_FORMAT(a.product_requireddate,'%Y-%m-%d') as product_requireddate, a.product_gid, a.productgroup_gid, a.uom_gid," +
                            " a.qty_enquired,a.potential_value, a.created_by from smr_trn_tsalesenquirydtl a" +                            
                            " where a.enquiry_gid='" + enquiry_gid + "'  group by a.product_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        
                        string TmpEnquiryGID = objcmnfunctions.GetMasterGID("PPDC");

                        msSQL = " insert into smr_tmp_tsalesenquiry (" +
                                               " tmpsalesenquiry_gid, " +                                               
                                               " enquiry_gid," +                                                                                                                     
                                               " product_gid," +
                                               " productgroup_gid," +                                       
                                               " uom_gid," +
                                               " created_by, " +
                                               " qty_requested," +
                                               " product_requireddate," +
                                               " potential_value" +
                                               " )values(" +
                                               "'" + TmpEnquiryGID + "'," +                                              
                                               "'" + enquiry_gid + "'," +                                                                                                                                     
                                               "'" + dt["product_gid"] + "', " +
                                               "'" + dt["productgroup_gid"] + "', " +
                                               "'" + dt["uom_gid"] + "', " +
                                               "'" + dt["created_by"] + "', " +
                                               "'" + dt["qty_enquired"] + "'," +
                                               "'" + dt["product_requireddate"] + "'," +
                                               "'" + dt["potential_value"].ToString().Replace(", ", "") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                   
                }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while ViewEnquirySummary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");


            }

        }

        public void DaPostCustomerEnquiryEdit(string employee_gid, string user_gid, PostAll values)

        {
            try
            {             
      
                msSQL = "SELECT * FROM smr_tmp_tsalesenquiry WHERE enquiry_gid='" + values.enquiry_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (mnResult != 0)

                {
                    values.status = true;
                    values.message = "Select one Product to Raise Enquiry";
                }

                msGetGid1 = objcmnfunctions.GetMasterGID("PPTP");

                if (msGetGid1 == "E") // Assuming "E" is a string constant
                {
                    values.status = true;
                    values.message = "Create Sequence Code PPTP for Sales Enquiry Details";
                }


                msSQL = "SELECT DISTINCT " +
                    "a.product_gid, a.product_remarks, a.customerproduct_code, a.potential_value," +
                    "a.qty_requested, a.uom_gid, a.display_field, a.product_requireddate, a.product_requireddateremarks, " +
                    "a.productgroup_gid" +
                    " FROM smr_tmp_tsalesenquiry a WHERE" +
                    " a.enquiry_gid = '" +values.enquiry_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("PPDC");

                        if (msGetGid == "E") // Assuming "E" is a string constant
                        {
                            values.status = true;
                            values.message = "Create Sequence Code PPDC for Sales Enquiry Details";
                        }
                       

                            string lsnewproduct_flag = "Y";

                            msSQL = " Insert into smr_trn_tsalesenquirydtl " +
                                   " (enquirydtl_gid," +
                                   " enquiry_gid , " +
                                   " customerproduct_code," +
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
                                   "'" + values.enquiry_gid + "'," +
                                   "'" + dt["customerproduct_code"].ToString() + "'," +
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


                            if (mnResult != 0)
                            {
                                msSQL = " Insert into acp_trn_tenquirydtl " +
                                    " (enquirydtl_gid," +
                                    " enquiry_gid , " +
                                    " customerproduct_code," +
                                    " product_gid," +
                                    " potential_value," +
                                    " uom_gid," +
                                    " productgroup_gid," +
                                    " qty_enquired, " +
                                    " product_requireddate, " +
                                    " product_requireddateremarks," +
                                    " display_field ) " +
                                    " values (" +
                                    "'" + msGetGid + "'," +
                                    "'" + values.enquiry_gid + "'," +
                                      "'" + dt["customerproduct_code"].ToString() + "'," +
                                   "'" + dt["product_gid"].ToString() + "'," +
                                   "'" + dt["potential_value"].ToString() + "'," +
                                   "'" + dt["uom_gid"].ToString() + "'," +
                                   "'" + dt["productgroup_gid"].ToString() + "'," +
                                   "'" + dt["qty_requested"].ToString() + "', ";

                                if (dt["product_requireddate"].ToString() == null || DBNull.Value.Equals(dt["product_requireddate"].ToString()))
                                {
                                    msSQL += "null";
                                }
                                else
                                {
                                    string formattedDate = ((DateTime)dt["product_requireddate"]).ToString("yyyy-MM-dd");
                                    msSQL += "'" + formattedDate + "',";
                                }
                                msSQL += "'" + dt["product_requireddateremarks"].ToString() + "',";
                                msSQL += "'" + dt["display_field"].ToString() + "')";
                            }
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                
                
             

                if (mnResult != 0)
                {

                    msSQL = "select sum(potential_value) as potential_value from smr_trn_tsalesenquirydtl where enquiry_gid='" + values.enquiry_gid + "'";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader.HasRows == true)
                    {
                        lspotential_value = objOdbcDataReader["potential_value"].ToString();

                    }

                    msSQL = "select branch_gid, branch_name from hrm_mst_tbranch where branch_name='" + values.branch_name + "' ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            if (values.closure_date == null || values.closure_date == "")
                            {
                                closure_date = "0000-00-00";
                            }
                            else
                            {
                                string uiDateStr1 = values.closure_date;
                                DateTime uiDate1 = DateTime.ParseExact(uiDateStr1, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                closure_date = uiDate1.ToString("yyyy-MM-dd");
                            }

                            

                            msSQL = "UPDATE smr_trn_tsalesenquiry SET " +
                            "branch_gid = '" + values.branch_gid + "', " +
                            "customer_gid = '" + values.customer_gid + "', " +
                            "customer_name = '" + values.customer_name + "', " +
                            "contact_number = '" + values.contact_number + "', " +
                            "contact_person = '" + values.contact_person + "', " +
                            "contact_email = '" + values.contact_email + "', " +
                            "customerbranch_gid = '" + values.customerbranch_name + "', " +
                            "contact_address = '" + values.contact_address + "', " +
                            "enquiry_remarks = '" + values.enquiry_remarks + "', " +
                            "enquiry_referencenumber = '" + values.enquiry_referencenumber + "', " +
                            "closure_date = '" + closure_date + "', " +
                            "potorder_value = '" + lspotential_value + "', " +
                            "customer_requirement = '" + values.customer_requirement + "', " +
                            "landmark = '" + values.landmark + "', " +
                            "product_count = '" + dt_datatable.Rows.Count + "' " +
                            "WHERE enquiry_gid = '" + values.enquiry_gid + "'";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            if (mnResult != 0)
                            {


                                msSQL = "UPDATE acp_trn_tenquiry SET " +
                                        "branch_gid = '" + dt["branch_gid"].ToString() + "', " +
                                        "customer_gid = '" + values.customer_gid + "', " +
                                        "customer_name = '" + values.customer_name + "', " +
                                        "contact_number = '" + values.contact_number + "', " +
                                        "contact_person = '" + values.contact_person + "', " +
                                        "contact_email = '" + values.contact_email + "', " +
                                        "customerbranch_gid = '" + values.customerbranch_name + "', " +
                                        "contact_address = '" + values.contact_address + "', " +
                                        "enquiry_remarks = '" + values.enquiry_remarks + "', " +
                                        "enquiry_referencenumber = '" + values.enquiry_referencenumber + "', " +
                                        "customer_requirement = '" + values.customer_requirement + "', " +
                                        "landmark = '" + values.landmark + "', " +
                                        "product_count = '" + dt_datatable.Rows.Count + "' " +
                                        "WHERE enquiry_gid = '" + values.enquiry_gid + "'";


                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            objdbconn.CloseConn();
                        }
                    }

                    if(mnResult == 1 || mnResult == 0)
                    {
                        msSQL = "delete FROM smr_tmp_tsalesenquiry WHERE created_by='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (mnResult != 0)
                    {
                        values.status = true;
                       

                        values.message = "Enquiry Updated Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Enquiry";
                    }

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting  Updating Enquiry !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            

        }

        public void DaGetOnEditCustomerName(string customercontact_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
               
                if (customercontact_gid != null)
                {
                    msSQL = "select a.customer_gid,a.customercontact_gid from crm_mst_tcustomercontact a left join crm_mst_tcustomer " +
                        "b on a.customer_gid=b.customer_gid where customer_name='" + customercontact_gid + "'";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    if (objOdbcDataReader.HasRows == true)
                    {
                        lscustomer_gid = objOdbcDataReader["customer_gid"].ToString();

                    }

                    msSQL = " select a.customercontact_gid,concat(a.address1, '   ', a.city, '   ', a.state, '   ', a.zip_code) as address1,ifnull(a.address2, '') as address2," +
                           " ifnull(a.city, '') as city,  ifnull(a.state, '') as state,ifnull(a.country_gid, '') as country_gid,ifnull(a.zip_code, '') as zip_code, " +
                           " ifnull(a.mobile, '') as mobile,ifnull(a.email, '') as email,ifnull(b.country_name, '') as country_name,a.customerbranch_name, " +
                           " concat(a.customercontact_name) as customercontact_names  from crm_mst_tcustomercontact a left join adm_mst_tcountry b on a.country_gid = b.country_gid " +
                           " where a.customer_gid = '" + lscustomer_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    var getModuleList = new List<GetCustomer>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getModuleList.Add(new GetCustomer
                            {
                                customercontact_name = dt["customercontact_names"].ToString(),
                                customerbranch_name = dt["customerbranch_name"].ToString(),
                                country_name = dt["country_name"].ToString(),
                                contact_email = dt["email"].ToString(),
                                contact_number = dt["mobile"].ToString(),
                                zip_code = dt["zip_code"].ToString(),
                                country_gid = dt["country_gid"].ToString(),
                                state = dt["state"].ToString(),
                                city = dt["city"].ToString(),
                                address2 = dt["address2"].ToString(),
                                contact_address = dt["address1"].ToString(),
                                customercontact_gid = dt["customercontact_gid"].ToString(),

                            });
                            values.GetCustomer = getModuleList;
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Edit CustomerName !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }


        // ENQUIRY TO QUOTATION PRODUCT SUMMARY EDIT
        public void DaGetEnqtoQuoteEditProductSummary(string tmpquotationdtl_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
              

                msSQL = " Select enquiry_gid, quotation_gid,product_gid, product_name,productgroup_gid, productgroup_name, uom_gid, uom_name, product_code, product_price, discount_percentage, discount_amount," +
                        " price, tax_name, tax_amount, tax_percentage, qty_quoted from smr_tmp_treceivequotationdtl where quotation_gid='" + tmpquotationdtl_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<EditEtoQ_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new EditEtoQ_list
                        {
                            tmpquotationdtl_gid = dt["tmpquotationdtl_gid"].ToString(),
                            product_gid = dt["product_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            productuom_name = dt["uom_name"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                            quantity = dt["qty_quoted"].ToString(),
                            product_code = dt["product_code"].ToString(),
                            unitprice = dt["product_price"].ToString(),
                            discountpercentage = dt["discount_percentage"].ToString(),
                            discountamount = dt["discount_amount"].ToString(),
                            totalamount = dt["price"].ToString(),
                            tax_name = dt["tax_name"].ToString(),
                            tax_amount = dt["tax_amount"].ToString(),
                        });
                        values.editenquirytoquote_list = getModuleList;
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while EnqtoQuote Edit ProductSummary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        // DIRECT ENQUIRY PRODUCT EDIT

        public void DaGetDirectEnquiryEditProductSummary(string tmpsalesenquiry_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
                
                msSQL = " Select a.tmpsalesenquiry_gid,a.enquiry_gid,a.product_gid,a.qty_requested,a.uom_gid,a.productgroup_gid,a.potential_value," +
                        " DATE_FORMAT(a.product_requireddate, '%d-%m-%Y') AS product_requireddate," +
                        " c.product_name, c.product_code,b.productgroup_name,d.productuom_name from smr_tmp_tsalesenquiry a" +
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
                values.message = "Exception occured while Direct Enquiry Edit ProductSummary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
            
        }

        // PRODUCT UPDATE DIRECT ENQUIRY 

        public void DaPostUpdateEnquiryProduct(string employee_gid, productsummarys_list values)
        {
            try
            {

                if (values.product_requireddate == null || values.product_requireddate == "")
                {
                    product_requireddate = "0000-00-00";
                }
                else
                {
                    string uiDateStr = values.product_requireddate;
                    DateTime uiDate = DateTime.ParseExact(uiDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    product_requireddate = uiDate.ToString("yyyy-MM-dd");
                }
                if (values.product_gid != null)
                {
                    lsproductgid1 = values.product_gid;
                    msSQL = "Select product_name from pmr_mst_tproduct where product_gid='" + lsproductgid1 + "'";
                    values.product_name = objdbconn.GetExecuteScalar(msSQL);
                }
                else
                {
                    msSQL = " Select product_gid from pmr_mst_tproduct where product_name = '" + values.product_name + "'";
                    lsproductgid1 = objdbconn.GetExecuteScalar(msSQL);
                }
                msSQL = "Select productgroup_gid from pmr_mst_tproductgroup where productgroup_name='" + values.productgroup_name + "'";
                string lsproductgroupgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "Select productuom_gid from pmr_mst_tproductuom where productuom_name='" + values.productuom_name + "'";
                string lsproductuomgid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update smr_tmp_tsalesenquiry set " +
                        " productgroup_gid = '" + lsproductgroupgid + "', " +
                        " potential_value ='" + values.potential_value + "', " +
                        " qty_requested='" + values.qty_requested + "', " +
                        " uom_gid='" + lsproductuomgid + "'," +
                        " product_gid='" + lsproductgid1 + "'," +
                        " product_requireddate = '" + product_requireddate + "'," +
                        " created_by= '" + employee_gid + "'," +
                        " enquiry_type= '" + lsenquiry_type + "'" +
                        " where tmpsalesenquiry_gid='" + values.tmpsalesenquiry_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select qty_requested,display_field,customerproduct_code,potential_value,tmpsalesenquiry_gid " +
                       " from smr_tmp_tsalesenquiry where " +
                       " product_gid = '" + lsproductgid1 + "' and " +
                       " uom_gid='" + lsproductuomgid + "' and  " +
                       " created_by = '" + employee_gid + "' and" +
                       " enquiry_type='" + lsenquiry_type + "' and product_requireddate='" + product_requireddate + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
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
                values.message = "Exception occured while Updating Product Details !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        // ENQUIRY TO QUOTATION PRODUCT SUMMARY DETAIL BUTTON
        public void DaGetRaiseQuotedetail(string product_gid, MdlSmrTrnCustomerEnquiry values)
        {
            try
            {
              
                msSQL = " select a.quotation_gid,a.quotationdtl_gid,a.customerproduct_code,a.product_gid,b.currency_code,a.product_requireddate as product_requireddate," +
                        " d.product_name,date_format(b.quotation_date,'%d-%m-%Y') as quotation_date," +
                        " b.customer_gid,b.customer_name,a.qty_quoted,format(a.product_price,2) as product_price,c.leadbank_name " +
                        " from smr_trn_treceivequotationdtl a left join smr_trn_treceivequotation b on a.quotation_gid=b.quotation_gid " +
                        " left join pmr_mst_tproduct d on a.product_gid = d.product_gid " +
                        " left join crm_trn_tleadbank c on b.customer_gid=c.leadbank_gid " +
                        " where a.product_gid='" + product_gid + "' group by a.product_price " +
                        " order by b.quotation_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Directeddetailslist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Directeddetailslist
                        {
                            product_gid = dt["product_gid"].ToString(),
                            product_name = dt["product_name"].ToString(),
                            quotation_date = dt["quotation_date"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            qty_quoted = dt["qty_quoted"].ToString(),
                            product_price = dt["product_price"].ToString(),
                            currency_code = dt["currency_code"].ToString(),
                        });
                        values.Directeddetailslist = getModuleList;
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Raise Quotedetail !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           
        }

        // PRODUCT UPPDATE -- ENQUIRY TO QUOTATION 
        public void DaPostUpdateEnquirytoQuotationProduct(string employee_gid, productsummarys_list values)
        {
            try
            {
              
                msSQL = "Select product_gid from pmr_mst_tproduct where product_name='" + values.product_name + "' and delete_flag='N' and" +
                    " product_code='" + values.product_code + "'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows == true)
                {
                    values.product_gid = objOdbcDataReader["product_gid"].ToString();
                }
                msSQL = " SELECT a.producttype_name FROM pmr_mst_tproducttype a " +
                        " left join pmr_mst_tproduct b ON a.producttype_gid=b.producttype_gid  " +
                        " WHERE product_gid='" + values.product_gid + "'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows == true)
                {
                    lsproduct_type = "Sales";

                }
                else
                {
                    lsproduct_type = "Services";
                }

                msSQL = " update smr_tmp_treceivequotationdtl set " +
                        " product_gid='" + values.product_gid + "'," +
                        " product_name='" + values.product_name + "'," +
                        " product_price='" + values.unitprice + "'," +
                        " qty_quoted='" + values.quantity + "'," +
                        " discount_percentage='" + values.discountpercentage + "'," +
                        " discount_amount='" + values.discountamount + "'," +
                        " uom_gid='" + lsproductuomgid + "'," +
                        " uom_name='" + values.productuom_name + "'," +
                        " price='" + values.totalamount.Replace(",", "") + "'," +
                        " tax_percentage='" + lspercentage1 + "'," +
                        " tax_name='" + values.taxname1 + "'," +
                        " tax_amount='" + values.taxamount1 + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                        " product_code='" + values.product_code + "'," +
                        " quotation_type='" + lsproduct_type + "'" +
                        " where tmpquotationdtl_gid='" + values.tmpquotationdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                values.message = "Exception occured while UpdateEnquirytoQuotation Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" +
                values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");

            }
           

        }
    }

}  









