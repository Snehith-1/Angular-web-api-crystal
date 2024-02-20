﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.pmr.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
//using System.Web;
//using OfficeOpenXml;
using System.Configuration;
using System.IO;
//using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;


namespace ems.pmr.DataAccess
{
    public class DaPmrMstProductUnit
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetProductUnitSummary(MdlPmrMstProductUnit values)
        {
            try
            {
                
                msSQL = " select  productuomclass_gid,productuomclass_code,productuomclass_name, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                    " from pmr_mst_tproductuomclass  a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<productunit_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new productunit_list
                        {
                            productuomclass_gid = dt["productuomclass_gid"].ToString(),
                            productuomclass_code = dt["productuomclass_code"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                        });
                        values.productunit_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting Product unit summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                   $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
               ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
               DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }
        public void DaGetProductUnitSummarygrid(string productuomclass_gid, MdlPmrMstProductUnit values)
        {
            try
            {
                
                msSQL = "select a.productuom_gid, a.productuom_code, a.productuom_name,a.sequence_level,format(a.convertion_rate, 2) as convertion_rate,case when a.baseuom_flag ='N' then 'NO' when baseuom_flag='Y' then 'YES' " +
                    " end as baseuom_flag, count(c.product_gid) as total_count,b.productuomclass_gid, b.productuomclass_name from pmr_mst_tproductuom a " +
                    " left join pmr_mst_tproductuomclass b on a.productuomclass_gid = b.productuomclass_gid  left join pmr_mst_tproduct c on a.productuom_gid=c.productuom_gid" +
                    " where b.productuomclass_gid='" + productuomclass_gid + "'  group by productuom_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<productunitgrid_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new productunitgrid_list
                        {


                            productuom_gid = dt["productuom_gid"].ToString(),
                            productuom_code = dt["productuom_code"].ToString(),
                            productuom_name = dt["productuom_name"].ToString(),
                            sequence_level = dt["sequence_level"].ToString(),
                            convertion_rate = dt["convertion_rate"].ToString(),
                            baseuom_flag = dt["baseuom_flag"].ToString(),
                            total_count = dt["total_count"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),
                            productuomclass_gid = dt["productuomclass_gid"].ToString(),

                        });
                        values.productunitgrid_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting Product unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                   $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
               ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
               DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }


        public void DaPostProductUnit(string user_gid, productunit_list values)
        {
            try
            {
                msSQL = " select * from pmr_mst_tproductuomclass where productuomclass_name= '" + values.productuomclass_name + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    //values.status = true;
                    values.message = "Product Unit Already Exist";
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("PUCM");


                    msSQL = " insert into pmr_mst_tproductuomclass (" +
                            " productuomclass_gid," +
                            " productuomclass_code," +
                            " productuomclass_name ," +
                            " created_by, " +
                            " created_date)" +
                            " values(" +
                            " '" + msGetGid + "'," +
                            "'" + values.productuomclass_code + "',";
                    if (values.productuomclass_name == null || values.productuomclass_name == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.productuomclass_name.Replace("'", "\\'") + "',";
                    }
                    msSQL += "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Product Unit Added Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Product Unit";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding Product unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                   $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
               ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
               DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }
        public void DaUpdatedProductunit(string user_gid, productunit_list values)
        {
            try
            {
                msSQL = " select * from pmr_mst_tproductuomclass where productuomclass_name= '" + values.productuomclassedit_name + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    //values.status = true;
                    values.message = "Product Unit Already Exist";
                }
                else
                {

                    msSQL = " update  pmr_mst_tproductuomclass  set " +
                            " productuomclass_code = '" + values.productuomclassedit_code + "'," +
                            " productuomclass_name = '" + values.productuomclassedit_name + "'," +
                            " updated_by = '" + user_gid + "'," +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where productuomclass_gid='" + values.productuomclass_gid + "'  ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {

                        values.status = true;
                        values.message = "Product Unit Updated Successfully";

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Product Unit";
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Product unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                   $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
               ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
               DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             
        }






        public void DadeleteProductunitSummary(string productuomclass_gid, productunit_list values)
        {
            try
            {
                
                msSQL = "  delete from pmr_mst_tproductuomclass where productuomclass_gid='" + productuomclass_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Product Unit Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Deleting Product Unit";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while deleting Product!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                   $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
               ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
               DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             
        }

        public void DaGetProductunits(string productuomclass_gid, MdlPmrMstProductUnit values)
        {
            try
            {
               
                msSQL = "Select productuomclass_gid, productuomclass_code, productuomclass_name" +
       " from pmr_mst_tproductuomclass where productuomclass_gid='" + productuomclass_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<productunitgrid_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new productunitgrid_list
                        {


                            productuomclass_gid = dt["productuomclass_gid"].ToString(),
                            productuomclass_code = dt["productuomclass_code"].ToString(),
                            productuomclass_name = dt["productuomclass_name"].ToString(),


                        });
                        values.productunitgrid_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting Product unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                   $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
               ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
               DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             

        }
        public void PostProductunits(string user_gid, productunitgrid_list values)
        {
            try
            {
                

                msSQL = "select productuomclass_gid from pmr_mst_tproductuomclass where productuomclass_name ='" + values.productuomclass_name + "'";
                string lsproductuomclassgid = objdbconn.GetExecuteScalar(msSQL);

                //msSQL = "select productuom_gid from pmr_mst_tproductuom where productuom_name ='" + values.productuomclass_name + "'";
                //string lsproductuomgid = objdbconn.GetExecuteScalar(msSQL);

                //msSQL = "select sequence_level from pmr_mst_tproductuom where productuom_name ='" + values.productuomclass_name + "'";
                //string lssequencelevel = objdbconn.GetExecuteScalar(msSQL);



                //msSQL = " select productuom_code, productuom_name, productuomclass_gid, a.sequence_level, a.convertion_rate " +
                //   " from pmr_mst_tproductuom  " +
                //   " a where productuomclass_gid='" + lsproductuomclassgid + "' " +
                //   " and a.sequence_level = '" + lssequencelevel + "' and productuom_gid  in ('" + lsproductuomgid + "') ";
                //objOdbcDataReader = objdbconn.GetDataReader(msSQL);


                //msSQL = " update pmr_mst_tproductuom set " +
                //    " productuom_name = '" +values.productuomclassedit_name1 + "'," +
                //    " sequence_level = '" + values.sequence_level + "'," +
                //    " convertion_rate = '" + values.conversion_rate + "'," +
                //    " baseuom_flag = '" + values.batch_flag + "'" +
                //    " where productuom_gid = '" + lsproductuomgid + "'";

                msGetGid = objcmnfunctions.GetMasterGID("PPMM");

                msSQL = " insert into pmr_mst_tproductuom " +
                    " (productuom_gid, " +
                    " productuom_code," +
                    " productuom_name, " +
                    " sequence_level, " +
                    " convertion_rate, " +
                    " baseuom_flag, " +
                    " productuomclass_gid " +
                    " ) values ( " +
                    " '" + msGetGid + "'," +
                    " '" + values.productuomclassedit_code1 + "'," +
                    " '" + values.productuomclassedit_name1 + "'," +
                    " '" + values.sequence_level + "'," +
                    " '" + values.conversion_rate + "'," +
                     " '" + values.batch_flag + "'," +
                    " '" + lsproductuomclassgid + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Product Unit Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Adding Product Unit";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while adding Product unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                   $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
               ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
               DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
             
        }
    }
}