﻿using ems.system.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.crm.Models;



namespace ems.crm.DataAccess
{
    public class DaRegion
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        int mnResult;
        string msGetGid, msGetGid1, lsentity_name, lsregion_code, lsregion_name, lsregion_gid, lscity_name_edit;


        public void DaGetRegionSummary(MdlRegion values)
        {
            try
            {

                msSQL = " select region_gid,region_code,region_name,city from crm_mst_tregion order by region_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<region_lists1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new region_lists1
                        {
                            region_gid = dt["region_gid"].ToString(),
                            region_code = dt["region_code"].ToString(),
                            region_name = dt["region_name"].ToString(),
                            city_name = dt["city"].ToString(),


                        });
                        values.region_lists1 = getmodulelist;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Region Summary";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{ System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }
        public void DaPostRegion(string user_gid, region_lists1 values)

        {

            try
            {
                 
                msSQL = " select region_code from crm_mst_tregion where region_code = '" + values.region_code + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);


                if (objOdbcDataReader .HasRows == true)
                {
                    values.status = false;
                    values.message = "Region Code Already Exist !!";
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BRNM");
                    msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='BRNM' order by finyear desc limit 0,1 ";
                    string lsCode = objdbconn.GetExecuteScalar(msSQL);

                    string lsregion_code = "BRN" + "000" + lsCode;
                    msSQL = " select region_name from crm_mst_tregion where region_name = '" + values.region_name + "'";
                    objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                    if (objOdbcDataReader .HasRows == true)
                    {
                        values.status = false;
                        values.message = "Region Name Already Exist !!";
                    }
                    else
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("BRNM");

                        msSQL = " insert into crm_mst_tregion(" +
                                " region_gid," +
                                " region_code," +
                                " region_name," +
                                " city," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                " '" + msGetGid + "'," +
                                " '" + lsregion_code + "'," +
                                "'" + values.region_name + "'," +
                                " '" + values.city_name + "',";



                        msSQL += "'" + user_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Region Added Successfully !!";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Adding Region!!";
                        }
                    }
                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Inserting Region Details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
       

        public void DaUpdateRegion(string user_gid, region_lists1 values)
        {

            try
            {
                 
                msSQL = " select region_gid,region_name  from crm_mst_tregion where region_name = '" + values.region_name_edit + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows)

                {
                    lsregion_gid = objOdbcDataReader ["region_gid"].ToString();
                    lsregion_name = objOdbcDataReader ["region_name"].ToString();
                }

                if (lsregion_gid == values.region_gid)

                {
                    msSQL = " update  crm_mst_tregion set " +
                    " region_name = '" + values.region_name_edit + "'," +
                    " region_code = '" + values.region_code_edit + "'," +
                    " city = '" + values.city_name_edit + "'," +
                    " updated_by = '" + user_gid + "'," +
                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where region_gid='" + values.region_gid + "' ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Region Updated Successfully !!";
                    }

                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Region !!";
                    }
                }
                else
                {
                    if (string.Equals(lsregion_name, values.region_name_edit, StringComparison.OrdinalIgnoreCase))
                    {
                        values.status = false;
                        values.message = "Region with the same name already exists !!";
                    }
                    else
                    {
                        msSQL = " update  crm_mst_tregion set " +
                  " region_name = '" + values.region_name_edit + "'," +
                  " region_code = '" + values.region_code_edit + "'," +
                  " city = '" + values.city_name_edit + "'," +
                  " updated_by = '" + user_gid + "'," +
                  " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where region_gid='" + values.region_gid + "' ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {
                            values.status = true;
                            values.message = "Region Updated Successfully !!";

                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Updating Region !!";
                        }
                    }

                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Region Details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
            
        }


        public void DaDeleteRegion(string region_gid, region_lists1 values)
        {
            try
            {
                 
                msSQL = "select leadbank_gid from crm_trn_tleadbank where leadbank_region='" + region_gid + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows)
                {
                    values.status = false;
                    values.message = "Region already used hence can't be deleted!!";
                }
                else
                {
                    msSQL = "  delete from crm_mst_tregion where region_gid='" + region_gid + "'  ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Region Deleted Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Deleting Region !!";
                    }

                }
                objOdbcDataReader .Close();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Deleting Region Details!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name} " + " * **********" +
              ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Marketing/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
           
        }
    }
}