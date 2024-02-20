using ems.system.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.crm.Models;
using System.Security.Cryptography;



namespace ems.crm.DataAccess
{
    public class DaSource
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        int mnResult;
        string msGetGid, msGetGid1, lssource_name , lssource_gid;

        // Module Master Summary
        public void DaGetSourceSummary(MdlSource values)
        {
            try
            {
                 
                msSQL = "  select  source_gid,source_code, source_name, source_desc, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by, a.created_date " +
                  " from crm_mst_tsource a left join adm_mst_tuser b on b.user_gid = a.created_by order by source_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<source_lists>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new source_lists
                        {
                            source_gid = dt["source_gid"].ToString(),
                            source_code = dt["source_code"].ToString(),
                            source_name = dt["source_name"].ToString(),
                            source_description = dt["source_desc"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                        });
                        values.source_lists = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Source Summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        
          
        }
        public void DaPostSource(string user_gid, source_lists values)

        {
            try
            {
                 
                msSQL = " select source_name from crm_mst_tsource where source_name = '" + values.source_name + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);


                if (objOdbcDataReader .HasRows == true)
                {
                    values.status = false;
                    values.message = "Source Name Already Exist !!";
                }

                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BSEM");
                    msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='BSEM' order by finyear desc limit 0,1 ";
                    string lsCode = objdbconn.GetExecuteScalar(msSQL);

                    string lssource_code = "SCM" + "000" + lsCode;

                    msSQL = " insert into crm_mst_tsource(" +
                            " source_gid," +
                            " source_code," +
                            " source_name," +
                            " source_desc," +
                            " created_by, " +
                            " created_date)" +
                            " values(" +
                            " '" + msGetGid + "'," +
                            " '" + lssource_code + "'," +
                            "'" + values.source_name + "',";
                    if (values.source_description == null || values.source_description == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.source_description.Replace("'", "") + "',";
                    }
                    msSQL += "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Source Added Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Source !!";
                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Inserting Source!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
    
    public void DaGetupdatesourcedetails(string user_gid, source_lists values)

    {
            try
            {
                msSQL = "select source_gid,source_name from crm_mst_tsource where source_name='" + values.sourceedit_name + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows)
                {
                    lssource_gid = objOdbcDataReader ["source_gid"].ToString();
                    lssource_name = objOdbcDataReader ["source_name"].ToString();
                }

                if (lssource_gid == values.source_gid)
                {
                    msSQL = " update  crm_mst_tsource set " +
                " source_name = '" + values.sourceedit_name + "'," +
                " source_desc = '" + values.sourceedit_description + "'," +
                " updated_by = '" + user_gid + "'," +
                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where source_gid='" + values.source_gid + "'  ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Source Updated Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Source !!";
                    }
                }
                else
                {
                    if (string.Equals(lssource_name, values.sourceedit_name, StringComparison.OrdinalIgnoreCase))
                    {
                        values.status = false;
                        values.message = "Source with the same name already exists !!";
                    }
                    else
                    {
                        msSQL = " update  crm_mst_tsource set " +
                                " source_name = '" + values.sourceedit_name + "'," +
                                " source_desc = '" + values.sourceedit_description + "'," +
                                " updated_by = '" + user_gid + "'," +
                                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where source_gid='" + values.source_gid + "'  ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            values.status = true;
                            values.message = "Source Updated Successfully !!";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Source !!";
                        }
                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Source Details";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
           
        }
        public void DaGetdeletesourcedetails(string source_gid, source_lists values)
        {
            try
            {
                 
                msSQL = "select leadbank_gid from crm_trn_tleadbank where source_gid='" + source_gid + "';";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows)
                {
                    values.status = false;
                    values.message = "Source already used hence can't be deleted!!";
                }
                else
                {
                    msSQL = "  delete from crm_mst_tsource where source_gid='" + source_gid + "'  ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Source Deleted Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Deleting Source !!";
                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Deleting Source Details";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
    }
}