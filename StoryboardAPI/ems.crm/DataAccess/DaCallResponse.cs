﻿using ems.crm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace ems.crm.DataAccess
{
    public class DaCallResponse
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        int mnResult;
        string msGetGid, msGetGid1, lscall_response, lscallresponse_code, lscallresponse_gid, lsleadstage_gid;

        // Module Master Summary

        public void DaGetCallResponseSummary(MdlCallResponse values)
        {
            try
            {
                 
                msSQL = "  select  callresponse_gid,callresponse_code,call_response,b.leadstage_name, moving_stage, CONCAT(c.user_firstname,' ',c.user_lastname) as created_by, a.created_date " +
                " from crm_mst_callresponse a" +
                 " left join crm_mst_tleadstage b on b.leadstage_gid = a.moving_stage" +
                " left join adm_mst_tuser c on c.user_gid = a.created_by order by callresponse_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<call_lists>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new call_lists
                        {
                            callresponse_gid = dt["callresponse_gid"].ToString(),
                            call_response = dt["call_response"].ToString(),
                            callresponse_code = dt["callresponse_code"].ToString(),
                            moving_stage = dt["leadstage_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.call_lists = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Call Response Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }        
        }
        
        public void DaPostCallResponse(string user_gid, call_lists values)

        {
            try
            {
                 
                msSQL = " select call_response from crm_mst_callresponse where call_response = '" + values.call_response + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);


                if (objOdbcDataReader .HasRows == true)
                {
                    values.status = false;
                    values.message = "Call Response Already Exist !!";
                }

                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("MKCR");
                    msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='MKCR' order by finyear desc limit 0,1 ";
                    string lsCode = objdbconn.GetExecuteScalar(msSQL);

                    string lscallresponse_code = "MCR" + "000" + lsCode;

                    msSQL = " insert into crm_mst_callresponse(" +
                                " callresponse_gid," +
                                " callresponse_code," +
                                " call_response," +
                                " moving_stage," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                " '" + msGetGid + "'," +
                                " '" + lscallresponse_code + "'," +
                                "'" + values.call_response + "'," +
                                " '" + values.moving_stage + "',";



                    msSQL += "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Call Response Added Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Call Response!!";
                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Inserting Call Response Details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }           
        }


        public void GetupdateCallResponsedetails(string user_gid, call_lists values)

        {

            try
            {
                 
                msSQL = "select callresponse_gid,call_response from crm_mst_callresponse where call_response='" + values.callresponseedit_name + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows)
                {
                    lscallresponse_gid = objOdbcDataReader ["callresponse_gid"].ToString();
                    lscall_response = objOdbcDataReader ["call_response"].ToString();
                }

                if (lscallresponse_gid == values.callresponse_gid)
                {

                    if (values.movingstage_edit == "1" || values.movingstage_edit == "2" || values.movingstage_edit == "3" || values.movingstage_edit == "4"
                        || values.movingstage_edit == "5" || values.movingstage_edit == "6")
                    {
                        // Your code here



                        msSQL = " update  crm_mst_callresponse set " +
                    " call_response = '" + values.callresponseedit_name + "'," +
                    " moving_stage = '" + values.movingstage_edit + "'," +
                    " created_by = '" + user_gid + "'," +
                    " created_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where callresponse_gid='" + values.callresponse_gid + "'  ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = "select leadstage_gid  from crm_mst_tleadstage where leadstage_name ='" + values.movingstage_edit + "'";
                        objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                        if (objOdbcDataReader .HasRows)
                        {
                            lsleadstage_gid = objOdbcDataReader ["leadstage_gid"].ToString();

                        }
                        msSQL = " update  crm_mst_callresponse set " +
                                " call_response = '" + values.callresponseedit_name + "'," +
                                " moving_stage = '" + lsleadstage_gid + "'," +
                                " created_by = '" + user_gid + "'," +
                                " created_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where callresponse_gid='" + values.callresponse_gid + "'  ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Call Response Updated Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Call Response !!";
                    }
                }
                else
                {
                    if (string.Equals(lscall_response, values.callresponseedit_name, StringComparison.OrdinalIgnoreCase))
                    {
                        values.status = false;
                        values.message = "Call Response with the same name already exists !!";
                    }
                    else
                    {

                        if (values.movingstage_edit == "New" || values.movingstage_edit == "Follow Up" || values.movingstage_edit == "Prospect" || values.movingstage_edit == "potential"
                        || values.movingstage_edit == "Drop" || values.movingstage_edit == "Customer" || values.movingstage_edit == "Potential")
                        {
                            msSQL = "select leadstage_gid  from crm_mst_tleadstage where leadstage_name ='" + values.movingstage_edit + "'";
                            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                            if (objOdbcDataReader .HasRows)
                            {
                                lsleadstage_gid = objOdbcDataReader ["leadstage_gid"].ToString();


                            msSQL = " update  crm_mst_callresponse set " +
                                    " call_response = '" + values.callresponseedit_name + "'," +
                                    " moving_stage = '" + lsleadstage_gid + "'," +
                                    " created_by = '" + user_gid + "'," +
                                    " created_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where callresponse_gid='" + values.callresponse_gid + "'  ";

                            }
                            else
                            {
                            msSQL = " update  crm_mst_callresponse set " +
                                    " call_response = '" + values.callresponseedit_name + "'," +
                                    " moving_stage = '" + values.movingstage_edit + "'," +
                                    " created_by = '" + user_gid + "'," +
                                    " created_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where callresponse_gid='" + values.callresponse_gid + "'  ";

                            }

                        }

                      
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            values.status = true;
                            values.message = "Call Response Updated Successfully !!";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Call Response !!";
                        }
                    }

                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Call Response Details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }           
        }
        public void DaGetdeleteCallResponsedetails(string callresponse_gid, call_lists values)
        {
            try
            {
                 
                msSQL = " select call_response from crm_mst_callresponse where callresponse_gid='" + callresponse_gid + "'";
                string lscallresponse = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select calllog_gid from crm_trn_tcalllog where call_response = '" + lscallresponse + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows)
                {
                    values.status = false;
                    values.message = "This Call Response is in use and cannot be deleted !!!";

                }
                else
                {
                    msSQL = "  delete from crm_mst_callresponse where callresponse_gid='" + callresponse_gid + "'  ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Call Response Deleted Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Deleting Call Response !!";
                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Deleting Call Response Details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

    }
}