﻿using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.crm.Models;

using static ems.crm.Models.MdlAssignvisit;
namespace ems.crm.DataAccess
{
    public class DaAssignvisit
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader  objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaGetassignvisitSummary(MdlAssignvisit values)
        {
            msSQL = " select a.assign_to,a.schedulelog_gid,b.leadbank_region,b.leadbank_gid,a.schedule_remarks,concat(h.user_firstname,'-',h.user_lastname) as assignto, " +
                " cast(concat(a.schedule_date,' ', a.schedule_time) as datetime) as schedule," +
                " concat(c.leadbankcontact_name,' / ',c.mobile,' / ',c.email) as contact_details,concat(f.user_firstname,'  ',f.user_lastname)as updated_by," +
                " concat(b.leadbank_address1,'/',b.leadbank_address2,'/',b.leadbank_city,'/',b.leadbank_state,'-',b.leadbank_pin)as customer_address," +
                 "concat(a.schedule_date, '', a.schedule_time) as schedule_dateandtime," +
                " b.leadbank_name,d.region_name,concat(a.schedule_type,'/',concat(a.schedule_date, '', a.schedule_time)) as schedule_type,a.schedule_remarks  from crm_trn_tschedulelog a " +
                " inner join crm_trn_tleadbank b on a.leadbank_gid=b.leadbank_gid " +
                " inner join crm_trn_tleadbankcontact c on b.leadbank_gid = c.leadbank_gid " +
                " left join crm_mst_tregion d on b.leadbank_region=d.region_gid " +
                " left join crm_mst_tsource e on b.source_gid=e.source_gid " +
                " left join adm_mst_tuser f on f.user_gid=a.created_by " +
                " left join hrm_mst_temployee g on g.employee_gid = a.assign_to " +
                " left join adm_mst_tuser h on h.user_gid = g.user_gid " +
                " where a.schedule_type='Meeting' " +
                " and c.status='Y' and c.main_contact='Y' order by b.leadbank_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<assignvisit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new assignvisit_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        schedulelog_gid = dt["schedulelog_gid"].ToString(),

                        customer_address = dt["customer_address"].ToString(),

                        //leadbank_region = dt["leadbank_region"].ToString(),
                        schedule_type = dt["schedule_type"].ToString(),
                        schedule_dateandtime = dt["schedule_dateandtime"].ToString(),
                        schedule_remarks = dt["schedule_remarks"].ToString(),
                        assign_to = dt["assignto"].ToString(),
                        updated_by = dt["updated_by"].ToString(),



                    });
                    values.assignvisitlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetmarketingteamdropdown(MdlAssignvisit values)
        {
            msSQL = " select campaign_gid,campaign_title from crm_trn_tcampaign";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<marketingteamdropdown_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new marketingteamdropdown_list
                    {
                        campaign_title = dt["campaign_title"].ToString(),
                        campaign_gid = dt["campaign_gid"].ToString(),
                    });
                    values.marketingteamdropdown_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetexecutedropdown(string user_gid, string campaign_gid, MdlAssignvisit values)
        {
            msSQL = "select a.employee_gid, concat(b.user_firstname,' ',b.user_lastname) as executive " +
                 "From crm_trn_tcampaign2employee a " +
                 "Left Join hrm_mst_temployee c on a.employee_gid=c.employee_gid " +
                 "Left Join adm_mst_tuser b on c.user_gid=b.user_gid " +
                 "where a.campaign_gid ='" + campaign_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getexecutedropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getexecutedropdown
                    {
                        employee_gid = dt["employee_gid"].ToString(),
                        executive = dt["executive"].ToString(),
                    });
                    values.Getexecutedropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetmarketingteamdropdownonchange(string campaign_gid, MdlAssignvisit values)
        {
            msSQL = "select a.employee_gid, concat(b.user_firstname,' ',b.user_lastname) as executive " +
                "From crm_trn_tcampaign2employee a " +
                "Left Join hrm_mst_temployee c on a.employee_gid=c.employee_gid " +
                "Left Join adm_mst_tuser b on c.user_gid=b.user_gid " +
                "where a.campaign_gid ='" + campaign_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getexecutedropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getexecutedropdown
                    {

                        employee_gid = dt["employee_gid"].ToString(),
                        executive = dt["executive"].ToString(),

                    });
                    values.Getexecutedropdown = getModuleList;
                }
            }



        }

       
        public void DaGetAssignassignvisit(string user_gid, assignvisitsubmit_list values)
        {
            for (int i = 0; i < values.summary_list.ToArray().Length; i++)
            {

                msSQL = " update crm_trn_tschedulelog set " +
                //" assign_to = '" + values.summary_list[i].executive + "' " +
                 " assign_to = '" + values.executive + "'," +

                " schedule_remarks = '" + values.summary_list[i].schedule_remarks + "'" +
                " where schedulelog_gid='" + values.summary_list[i].schedulelog_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Assigned Sucessfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Occured Assigning";
                }

            }






        }
    }
}