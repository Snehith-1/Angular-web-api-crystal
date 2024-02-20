﻿using ems.crm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using OfficeOpenXml.Style;
using System.ComponentModel;



namespace ems.crm.DataAccess
{
    public class DaCampaignSummary
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lscampaign_title, lscampaign_location, lscampaign_gid, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5, lsRowcount;

        public string team_gid { get; private set; }

        public void DaGetTeamSummary(MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = "SELECT a.campaign_gid, a.campaign_title, a.campaign_location," +
               " b.branch_name,b.branch_gid,a.campaign_description,a.campaign_mailid, " +
               " (SELECT count(lead2campaign_gid) FROM crm_trn_tlead2campaign k " +
               " where k.campaign_gid = a.campaign_gid) as assigned_total " +
               "FROM crm_trn_tcampaign a left join hrm_mst_tbranch b on a.campaign_location = b.branch_gid where 1 = 1 ORDER BY a.campaign_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getModuleList = new List<marketingteam_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new marketingteam_list
                        {
                            campaign_gid = dt["campaign_gid"].ToString(),
                            branch = dt["branch_name"].ToString(),
                            description = dt["campaign_description"].ToString(),
                            mail_id = dt["campaign_mailid"].ToString(),
                            team_name = dt["campaign_title"].ToString(),
                            assigned_total = dt["assigned_total"].ToString(),
                            campaign_location = dt["campaign_location"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),
                        });
                        values.marketingteam_list = getModuleList;
                    }
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Team Summary";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }           
        }

        public void DaGetbranchdropdown(MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = " select branch_gid, branch_name" +
           " from hrm_mst_tbranch ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<branch_list1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new branch_list1
                        {
                            branch_gid = dt["branch_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                        });
                        values.branch_list1 = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Branch Dropdown Details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }            
        }

        public void DaGetteammanagerdropdown(MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = " select a.employee_gid,concat(c.user_firstname, ' ', c.user_lastname)As employee_name" +
               " from adm_mst_tmodule2employee a" +
               " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
               " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
               " where a.module_gid='MKT'  and c.user_status='Y' " +
        " order by employee_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<team_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new team_list
                        {
                            employee_gid = dt["employee_gid"].ToString(),
                            team_manager = dt["employee_name"].ToString(),

                        });
                        values.team_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Team Manager Dropdown";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaPostmarketingteam(string user_gid, marketingteam_list values)
        {
            try
            {
                 
                msSQL = " select campaign_title from crm_trn_tcampaign where campaign_title = '" + values.team_name + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows == true)
                {
                    values.status = false;
                    values.message = "Team Name Already Exist !!";
                }
                else
                {

                    msGetGid = objcmnfunctions.GetMasterGID("BCNP");

                    msSQL = " insert into crm_trn_tcampaign(" +
                             " campaign_gid," +
                             " campaign_title," +
                             " campaign_description," +
                             " campaign_location," +
                             " campaign_manager," +
                             " campaign_mailid," +
                            " created_by, " +
                             " created_date)" +
                             " values(" +
                             " '" + msGetGid + "'," +
                             " '" + values.team_name + "'," +
                             " '" + values.description + "'," +
                             "'" + values.branch + "'," +
                             "'" + values.team_manager + "'," +
                             "'" + values.mail_id + "'," +
                             "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Marketing Team Added Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Marketing Team";
                    }

                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Inserting Marketing Team!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaUpdatedmarketingteam(string user_gid, marketingteam_list values)
        {
            try
            {
                 
                msSQL = " select campaign_title,campaign_gid from crm_trn_tcampaign where campaign_title = '" + values.team_name_edit + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows)
                {
                    lscampaign_title = objOdbcDataReader ["campaign_title"].ToString();
                    lscampaign_gid = objOdbcDataReader ["campaign_gid"].ToString();
                }

                if (lscampaign_gid == values.campaign_gid)
                {

                    msSQL = " update  crm_trn_tcampaign set " +
             " campaign_title  = '" + values.team_name_edit + "'," +
             " campaign_description  = '" + values.description_edit + "'," +
              " campaign_location  = '" + values.branch_edit + "'," +
             " campaign_mailid  = '" + values.mail_id_edit + "'," +
             " updated_by = '" + user_gid + "'," +
             " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
             "' where campaign_gid='" + values.campaign_gid + "'  ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Marketing Team Updated Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Marketing Team";
                    }
                }
                else
                {
                    if (lscampaign_title != values.team_name_edit)
                    {

                        msSQL = " update  crm_trn_tcampaign set " +
                 " campaign_title  = '" + values.team_name_edit + "'," +
                 " campaign_description  = '" + values.description_edit + "'," +
                  " campaign_location  = '" + values.branch_edit + "'," +
                 " campaign_mailid  = '" + values.mail_id_edit + "'," +
                 " updated_by = '" + user_gid + "'," +
                 " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                 "' where campaign_gid='" + values.campaign_gid + "'  ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            values.status = true;
                            values.message = "Marketing Team Updated Successfully !!";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Marketing Team !!";
                        }
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Same Marketing Team Already Exist !!";
                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Marketing Team!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }


        public void DaGetMarketingTeamDetailTable(string campaign_gid, MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = "select campaign_title from crm_trn_tcampaign " +
                   " where campaign_gid= '" + campaign_gid + "' ";


                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows)
                {
                    values.campaign_name = objOdbcDataReader ["campaign_title"].ToString();
                }

                 

                msSQL = " select distinct a.campaign_gid,a.employee_gid,concat(c.user_firstname, '-', c.user_code) as user, " +
                    " ( SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where x.assign_to = a.employee_gid and x.campaign_gid = a.campaign_gid) as total, " +
                    " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where x.assign_to = a.employee_gid and (x.leadstage_gid ='1' or x.leadstage_gid is null) and x.campaign_gid = a.campaign_gid) as newleads, " +
                    " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where x.assign_to = a.employee_gid and x.leadstage_gid ='2' and x.campaign_gid = a.campaign_gid) as followup, " +
                    " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where x.assign_to = a.employee_gid and x.leadstage_gid ='3' and x.campaign_gid = a.campaign_gid) as prospect, " +
                     " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where x.assign_to = a.employee_gid and x.leadstage_gid ='4' and x.campaign_gid = a.campaign_gid) as potential, " +
                    " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where x.assign_to = a.employee_gid and x.leadstage_gid ='5' and x.campaign_gid = a.campaign_gid) as drop_status, " +
                    " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where x.assign_to = a.employee_gid and x.leadstage_gid ='6' and x.campaign_gid = a.campaign_gid) as customer," +
                    "(select count(x.schedulelog_gid) from crm_trn_tschedulelog x where x.assign_to = a.employee_gid and x.schedule_date >= curdate()) as visit " +
                    " from crm_trn_tcampaign2employee a left join hrm_mst_temployee b on a.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.campaign_gid= '" + campaign_gid + "' ";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<marketingteam_list1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new marketingteam_list1
                        {
                            employee_gid = dt["employee_gid"].ToString(),
                            campaign_gid = dt["campaign_gid"].ToString(),
                            user = dt["user"].ToString(),
                            total = dt["total"].ToString(),
                            newleads = dt["newleads"].ToString(),
                            followup = dt["followup"].ToString(),
                            potential = dt["potential"].ToString(),
                            prospect = dt["prospect"].ToString(),
                            drop_status = dt["drop_status"].ToString(),
                            customer = dt["customer"].ToString(),
                            visit = dt["visit"].ToString(),

                        });
                        values.marketingteam_list1 = getModuleList;
                    }
                }

                dt_datatable.Dispose();
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Marketing Team count!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaDeleteTeam(string campaign_gid, marketingteam_list values)
        {

            try
            {
                 
                // Check if the team is assigned for lead process
                msSQL = "select campaign_gid from crm_trn_tlead2campaign where campaign_gid = '" + campaign_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count > 0)
                {
                    //dt_datatable.Close();
                    objdbconn.CloseConn();
                    values.status = false;
                    values.message = "This team is assigned for lead process";
                }
                else
                {
                    // Check if the team has assigned employees
                    msSQL = "select employee_gid from crm_trn_tcampaign2employee where campaign_gid='" + campaign_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    if (dt_datatable.Rows.Count > 0)
                    {
                        values.status = false;
                        values.message = "This Team cannot be deleted. Unassign all employees.";
                    }
                    else
                    {
                        // Check if there are assigned managers to the team
                        msSQL = "select employee_gid from cmn_trn_tmanagerprivilege where team_gid='" + campaign_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);

                        if (dt_datatable.Rows.Count > 0)
                        {
                            values.status = false;
                            values.message = "Unassign all the manager in order to delete the team.";
                        }
                        else
                        {
                            // If all checks pass, proceed with deleting the team
                            msSQL = "delete from crm_trn_tcampaign where campaign_gid='" + campaign_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 0)
                            {
                                values.status = true;
                                values.message = "Marketing Team Deleted Successfully !!";
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error While Deleting Marketing Team !!";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Deleting Marketing Team!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }       
        }

     
        //CODE BY SNEHITH
        public void DaGetUnassignedlist(string campaign_gid, string campaign_location, MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = " select a.employee_gid,concat(c.user_code,'','/','',c.user_firstname,' ',c.user_lastname)AS employee_name,'" + campaign_gid + "' as campaign_gid" +
              " from adm_mst_tmodule2employee a" +
              " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
              " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
              " where a.module_gid = 'MKT'" +
              " and b.branch_gid='" + campaign_location + "'" +
              " and a.hierarchy_level<>'-1'" +
              " and a.employee_gid not in" +
              " (select employee_gid from" +
              " crm_trn_tcampaign2employee" +
              " where campaign_gid= '" + campaign_gid + "')" +
              " group by employee_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetUnassignedlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetUnassignedlist
                        {
                            campaign_gid = dt["campaign_gid"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            employee_name = dt["employee_name"].ToString(),
                        });
                        values.GetUnassignedlist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Unassigned List Details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }            
        }
        public void DaGetAssignedlist(string campaign_gid, string campaign_location, MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = " select a.employee_gid,concat(c.user_code,'','/','',c.user_firstname,' ',c.user_lastname)AS employee_name,'" + campaign_gid + "' as campaign_gid" +
                    " from adm_mst_tmodule2employee a" +
                    " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                    " where a.module_gid = 'MKT'" +
                    " and b.branch_gid='" + campaign_location + "'" +
                    " and a.hierarchy_level<>'-1'" +
                    " and a.employee_gid in " +
                    " (select employee_gid from" +
                    " crm_trn_tcampaign2employee" +
                    " where campaign_gid= '" + campaign_gid + "')" +
                    " group by employee_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetAssignedlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetAssignedlist
                        {
                            campaign_gid = dt["campaign_gid"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            employee_name = dt["employee_name"].ToString(),
                        });
                        values.GetAssignedlist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Assigned List Details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }           
        }


        public void DaPostAssignedlist(string user_gid, campaignassign_list values)
        {
            try
            {
                 
                for (int i = 0; i < values.campaignassign.ToArray().Length; i++)
                {
                    msSQL = " insert into crm_trn_tcampaign2employee ( " +
                            " campaign_gid, " +
                            " employee_gid, " +
                            " created_by," +
                            " created_date ) " +
                            " values (  " +
                            " '" + values.campaignassign[i]._key1 + "', " +
                            " '" + values.campaignassign[i]._id + "', " +
                            " '" + user_gid + "', " +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "' )";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Employee assigned to team successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Employee Assign";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Assigning Employee!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaPostUnassignedlist(string user_gid, campaignassign_list values)
        {
            try
            {
                 
                for (int i = 0; i < values.campaignassign.ToArray().Length; i++)
                {

                    msSQL = " delete from crm_trn_tcampaign2employee  " +
                    " where campaign_gid = '" + values.campaignassign[i]._key1 + "' and employee_gid = '" + values.campaignassign[i]._id + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = false;
                        values.message = "Employee Unassigned to Team Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Employee Unassign";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Unassigning Employee!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }        
        }
        public void DaGetManagerUnassignedlist(string campaign_gid, string campaign_location, MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = " select a.employee_gid,concat(c.user_code,'','/','',c.user_firstname,' ',c.user_lastname)AS employee_name,'" + campaign_gid + "' as campaign_gid" +
                " from adm_mst_tmodule2employee a" +
                " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                " where a.module_gid = 'MKT'" +
                " and b.branch_gid='" + campaign_location + "'" +
                " and a.hierarchy_level<>'-1'" +
                " and a.employee_gid not in" +
                " (select employee_gid from" +
                " cmn_trn_tmanagerprivilege" +
                " where team_gid= '" + campaign_gid + "')" +
                " group by  a.employee_gid, c.user_code, c.user_firstname," +
                " c.user_lastname, campaign_gid ORDER BY" +
                " employee_name ASC;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetManagerUnassignedlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetManagerUnassignedlist
                        {
                            campaign_gid = dt["campaign_gid"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            employee_name = dt["employee_name"].ToString(),
                        });
                        values.GetManagerUnassignedlist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Unassigned Manager List!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            
        }
        public void DaGetManagerAssignedlist(string campaign_gid, string campaign_location, MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = " select a.employee_gid,concat(c.user_code,'','/','',c.user_firstname,' ',c.user_lastname)AS employee_name,'" + campaign_gid + "' as campaign_gid" +
               " from adm_mst_tmodule2employee a" +
               " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
               " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
               " where a.module_gid = 'MKT'" +
               " and b.branch_gid='" + campaign_location + "'" +
               " and a.hierarchy_level<>'-1'" +
               " and a.employee_gid in " +
               " (select employee_gid from" +
               " cmn_trn_tmanagerprivilege" +
               " where team_gid= '" + campaign_gid + "')" +
               " group by a.employee_gid, c.user_code, c.user_firstname, c.user_lastname, " +
               "campaign_gid ORDER BY    employee_name ASC;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetManagerAssignedlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetManagerAssignedlist
                        {
                            campaign_gid = dt["campaign_gid"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            employee_name = dt["employee_name"].ToString(),

                        });
                        values.GetManagerAssignedlist = getModuleList;
                    }
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Assigned Manager List!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }
        public void DaPostManagerAssignedlist(string user_gid, campaignassign_list values)
        {
            try
            {
                 
                for (int i = 0; i < values.campaignassign.ToArray().Length; i++)
                {
                    msSQL = " SELECT employee_gid  FROM crm_trn_tcampaign2employee " +
                       " where campaign_gid = '" + values.campaignassign[i]._key3 + "'" +
                       " and employee_gid='" + values.campaignassign[i]._id + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count == 0)
                    {
                        msSQL = " insert into crm_trn_tcampaign2employee( " +
                             " campaign_gid, " +
                             " employee_gid, " +
                             " created_by, " +
                             " created_date) " +
                             " values( " +
                             "'" + values.campaignassign[i]._key3 + "'," +
                             "'" + values.campaignassign[i]._id + "'," +
                             " '" + user_gid + "', " +
                             " '" + DateTime.Now.ToString("yyyy-MM-dd") + "' )";
                        mnResult2 = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    msSQL = " insert into cmn_trn_tmanagerprivilege( " +
                        " team_gid, " +
                        " employee_gid, " +
                        " module_gid, " +
                        " created_by, " +
                        " created_date) " +
                        " values( " +
                        "'" + values.campaignassign[i]._key3 + "'," +
                        "'" + values.campaignassign[i]._id + "'," +
                        " 'B2BCMPCPM', " +
                        " '" + user_gid + "', " +
                        " '" + DateTime.Now.ToString("yyyy-MM-dd") + "' )";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = false;
                        values.message = "Manager Assigned Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Manager Assign";
                    }

                }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Assigning Manager!";
            }
            
        }

        public void DaPostManagerUnassignedlist(string user_gid, campaignassign_list values)
        {
            try
            {
                 
                for (int i = 0; i < values.campaignassign.ToArray().Length; i++)
                {

                    msSQL = " delete from cmn_trn_tmanagerprivilege   " +
                    " where team_gid = '" + values.campaignassign[i]._key3 + "' and employee_gid = '" + values.campaignassign[i]._id + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {

                        values.status = false;
                        values.message = "Manager Unassigned Successfully";

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Manager Unassign";
                    }

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Unassigning Manager!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }


        public MdlCampaignSummary DaGetUserSummary(MdlSearchParamters values)
        {
            MdlCampaignSummary objMdlCampaignSummary = new MdlCampaignSummary();
            try
            {
                 
               
                msSQL = " select concat(b.user_firstname, ' ', b.user_lastname) as user, " +
                    " (select campaign_title from crm_trn_tcampaign where campaign_gid='" + values.campaign_gid + "') as campaign_name " +
                    " from hrm_mst_temployee a " +
                    " left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                    " where employee_gid = '" + values.employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<marketingteam_list>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new marketingteam_list
                        {
                            campaign_name = dt["campaign_name"].ToString(),
                            user_name = dt["user"].ToString(),
                        });
                        objMdlCampaignSummary.marketingteam_list = getModuleList;
                    }
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Purchase Liability Report Chart!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            return objMdlCampaignSummary;
        }

        public MdlCampaignSummary DaGetAssignSummary(MdlSearchParamters values)
        {
            MdlCampaignSummary objMdlCampaignSummary = new MdlCampaignSummary();

            try
            {
                 
                if (values.company_name == "" || values.source_name == "" || values.industry == "" || values.city == "" || values.region == "" || values.state == "")
                {
                    msSQL = "Select DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date,concat(n.user_firstname, ' ', n.user_lastname,'/',n.user_code) as created_by,  a.leadbank_gid,a.leadbank_name,a.remarks, " +
                        "a.customer_gid, concat(c.leadbankcontact_name, ' / ', c.mobile, ' / ', c.email) as contact_details,f.source_name, " +
                        "concat(d.region_name, ' / ', a.leadbank_city, ' / ', a.leadbank_state) as region_name " +
                        "From crm_trn_tleadbank a " +
                        "left join crm_trn_tleadbankcontact c on a.leadbank_gid = c.leadbank_gid " +
                        "left join crm_mst_tregion d on a.leadbank_region = d.region_gid " +
                        "left join crm_mst_tsource f on a.source_gid = f.source_gid " +
                        "left join hrm_mst_temployee m on a.created_by = m.employee_gid " +
                        "left join adm_mst_tuser n on m.user_gid = n.user_gid " +
                        "where a.leadbank_gid not in (select leadbank_gid from crm_trn_tlead2campaign where 0 = 0) and a.status = 'Y' " +
                        "and c.status = 'Y' ";
                }
                else if (values.company_name != "" || values.source_name != "" || values.industry != "" || values.city != "" || values.region != "" || values.state != "")
                {

                    msSQL += "Select a.customer_type, DATE_FORMAT(a.created_date, '%d-%m-%Y') AS created_date,concat(n.user_firstname, ' ', n.user_lastname,'/',n.user_code) as created_by,  a.leadbank_gid,a.leadbank_name,a.remarks, " +
                            "a.customer_gid, concat(c.leadbankcontact_name, ' / ', c.mobile, ' / ', c.email) as contact_details,f.source_name, " +
                            "concat(d.region_name, ' / ', a.leadbank_city, ' / ', a.leadbank_state) as region_name " +
                            "From crm_trn_tleadbank a " +
                            "left join crm_trn_tleadbankcontact c on a.leadbank_gid = c.leadbank_gid " +
                            "left join crm_mst_tregion d on a.leadbank_region = d.region_gid " +
                            "left join crm_mst_tsource f on a.source_gid = f.source_gid " +
                            "left join hrm_mst_temployee m on a.created_by = m.employee_gid " +
                            "left join adm_mst_tuser n on m.user_gid = n.user_gid " +
                            "where a.leadbank_gid not in (select leadbank_gid from crm_trn_tlead2campaign where 0 = 0) and a.status = 'Y' " +
                            "and c.status = 'Y' and a.leadbank_name like '%" + values.company_name + "%' order by a.leadbank_name";

                }
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<marketingteam_list>();
                lsRowcount = dt_datatable.Rows.Count;

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new marketingteam_list
                        {
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            leadbank_name = dt["leadbank_name"].ToString(),
                            remarks = dt["remarks"].ToString(),
                            contact_details = dt["contact_details"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            region_name = dt["region_name"].ToString(),
                            source_name = dt["source_name"].ToString(),
                            leadbank_gid = dt["leadbank_gid"].ToString(),
                            Rowcount = lsRowcount,
                        });
                        objMdlCampaignSummary.marketingteam_list = getModuleList;
                    }
                }

                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Purchase Liability Report Chart!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
         return objMdlCampaignSummary;
        }
        public void DaGetAssignLead(string employee_gid,assignteam_list1 values)
        {

            try
            {
                 
                for (int i = 0; i < values.summary_list1.ToArray().Length; i++)
                {

                    //msGetGid = objcmnfunctions.GetMasterGID("BLCC");
                    msSQL = " update crm_trn_tleadbank Set " +
                            " lead_status = 'Assigned' " +
                            " where leadbank_gid = '" + values.summary_list1[i].leadbank_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("BLCC");
                        msSQL1 = " Insert into crm_trn_tlead2campaign ( " +
                                " lead2campaign_gid, " +
                                " leadbank_gid, " +
                                " campaign_gid, " +
                                " created_by, " +
                                " created_date, " +
                                " lead_status, " +
                                " internal_notes, " +
                                " leadstage_gid, " +
                                " assign_to ) " +
                                " Values ( " +
                                "'" + msGetGid + "'," +
                                "'" + values.summary_list1[i].leadbank_gid + "'," +
                                "'" + values.campaign_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                "'Open'," +
                                "'" + values.summary_list1[i].schedule_remarks + "'," +
                                "'1'," +
                                "'" + values.employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL1);
                    }
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Lead Assigned Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Assigning Lead";
                }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Assigning Lead to Employee!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaGetSourcedropdown(MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = "  SELECT source_gid, source_name FROM crm_mst_tsource Order by source_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<dropdown_list1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new dropdown_list1
                        {
                            source_gid = dt["source_gid"].ToString(),
                            source_name = dt["source_name"].ToString(),
                        });
                        values.dropdown_list1 = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Source Dropdown!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }           
        }
        public void DaGetRegiondropdown(MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = " SELECT region_gid, region_name FROM crm_mst_tregion Order by region_name asc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<dropdown_list1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new dropdown_list1
                        {
                            region_gid = dt["region_gid"].ToString(),
                            region_name = dt["region_name"].ToString(),
                        });
                        values.dropdown_list1 = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Region Dropdown!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaGetIndustrydropdown(MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = " select categoryindustry_gid,categoryindustry_code,categoryindustry_name from crm_mst_tcategoryindustry  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<dropdown_list1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new dropdown_list1
                        {
                            categoryindustry_gid = dt["categoryindustry_gid"].ToString(),
                            categoryindustry_name = dt["categoryindustry_name"].ToString(),
                        });
                        values.dropdown_list1 = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Industry Dropdown!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        //code by snehith
        public void DaGetUnassigned(string campaign_gid, MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = " select campaign_location from crm_trn_tcampaign where campaign_gid='" + campaign_gid + " '  ";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows)
                {
                    lscampaign_location = objOdbcDataReader ["campaign_location"].ToString();
                }
                msSQL = " select a.employee_gid,concat(c.user_code,'','/','',c.user_firstname,' ',c.user_lastname)AS employee_name,'" + campaign_gid + "' as campaign_gid" +
                  " from adm_mst_tmodule2employee a" +
                  " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                  " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                  " where a.module_gid = 'MKT'" +
                  " and b.branch_gid='" + lscampaign_location + "'" +
                  " and a.hierarchy_level<>'-1'" +
                  " and a.employee_gid not in" +
                  " (select employee_gid from" +
                  " crm_trn_tcampaign2employee" +
                  " where campaign_gid= '" + campaign_gid + "')" +
                  " group by employee_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetUnassigned>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetUnassigned
                        {
                            campaign_gid = dt["campaign_gid"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            employee_name = dt["employee_name"].ToString(),
                        });
                        values.GetUnassigned = getModuleList;
                    }
                }
                dt_datatable.Dispose();
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Purchase Liability Report Chart!";
            }
         }


        public void DaGetManagerUnassigned(string campaign_gid, MdlCampaignSummary values)
        {

            try
            {
                 
                msSQL = " select campaign_location from crm_trn_tcampaign where campaign_gid='" + campaign_gid + " '  ";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader .HasRows)
                {
                    lscampaign_location = objOdbcDataReader ["campaign_location"].ToString();
                }
                msSQL = " select a.employee_gid,concat(c.user_code,'','/','',c.user_firstname,' ',c.user_lastname)AS employee_name,'" + campaign_gid + "' as campaign_gid" +
                    " from adm_mst_tmodule2employee a" +
                    " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                    " where a.module_gid = 'MKT'" +
                    " and b.branch_gid='" + lscampaign_location + "'" +
                    " and a.hierarchy_level<>'-1'" +
                    " and a.employee_gid not in" +
                    " (select employee_gid from" +
                    " cmn_trn_tmanagerprivilege" +
                    " where team_gid= '" + campaign_gid + "')" +
                    " group by employee_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetManagerUnassigned>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetManagerUnassigned
                        {
                            campaign_gid = dt["campaign_gid"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            employee_name = dt["employee_name"].ToString(),
                        });
                        values.GetManagerUnassigned = getModuleList;
                    }
                }
                dt_datatable.Dispose();
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Unassigned Manager List!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }          
        }

        // Marketing Team Count
        public void DaGetMarketingTeamCount(string employee_gid, string user_gid, MdlCampaignSummary values)
        {
            try
            {
                 
                msSQL = " select (select count(leadbank_gid)  from crm_trn_tleadbank where lead_status='Not Assigned' and customer_type is not null and customer_type!='') as unassigned_count, " +
                    " (select count(leadbank_gid) from crm_trn_tleadbank where lead_status='Assigned' and customer_type is not null and customer_type!='') as assigned_count," +
                    " (select count(leadbank_gid) from crm_trn_tleadbank where lead_status != 'Pending' and customer_type is not null and customer_type!='') as total_count ," +
                    "(select count(campaign_title) from crm_trn_tcampaign) as team_count," +
                    " (SELECT count(DISTINCT  employee_gid) FROM crm_trn_tcampaign2employee) as total_employee";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getLeadBankCountList = new List<MarketingTeamCount_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getLeadBankCountList.Add(new MarketingTeamCount_List
                        {
                            unassigned_count = (dt["unassigned_count"].ToString()),
                            assigned_count = (dt["assigned_count"].ToString()),
                            team_count = (dt["team_count"].ToString()),
                            total_count = (dt["total_count"].ToString()),
                            total_employee = (dt["total_employee"].ToString()),
                        });
                        values.MarketingTeamCount_List = getLeadBankCountList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Marketing Team Count!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }           
        }
    }

}