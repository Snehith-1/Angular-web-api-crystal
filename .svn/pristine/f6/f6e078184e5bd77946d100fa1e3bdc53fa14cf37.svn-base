using ems.sales.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.NetworkInformation;
using System.Web.UI.WebControls;


namespace ems.sales.DataAccess
{
    public class DaSmrMstProductUnit
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsstatus,lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        //summary
        public void DaGetSalesProductUnitSummary(MdlSmrMstProductUnit values)
        {
            try
            {
                
                msSQL = " select  productuomclass_gid,productuomclass_code,productuomclass_name, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                    " from pmr_mst_tproductuomclass  a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<salesproductunit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new salesproductunit_list
                    {
                        productuomclass_gid = dt["productuomclass_gid"].ToString(),
                        productuomclass_code = dt["productuomclass_code"].ToString(),
                        productuomclass_name = dt["productuomclass_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.salesproductunit_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product Unit Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }

        //add
        public void DaPostSalesProductUnit(string user_gid, salesproductunit_list values)
        {
            try
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
            catch (Exception ex)
            {
                values.message = "Exception occured while Adding Sales Product Unit!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            

        }

        // summary grid
        public void DaGetSalesProductUnitSummarygrid(string productuomclass_gid, MdlSmrMstProductUnit values)
        {
            try
            {
                
                msSQL = "select a.productuom_gid, a.productuom_code, a.productuom_name,a.sequence_level,format(a.convertion_rate, 2) as convertion_rate,case when a.baseuom_flag ='N' then 'NO' when baseuom_flag='Y' then 'YES' " +
                    " end as baseuom_flag, count(c.product_gid) as total_count,b.productuomclass_gid, b.productuomclass_name from pmr_mst_tproductuom a " +
                    " left join pmr_mst_tproductuomclass b on a.productuomclass_gid = b.productuomclass_gid  left join pmr_mst_tproduct c on a.productuom_gid=c.productuom_gid" +
                    " where b.productuomclass_gid='" + productuomclass_gid + "'  group by productuom_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<salesproductunitgrid_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new salesproductunitgrid_list
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
                    values.salesproductunitgrid_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product Unit Summary Grid !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        // Update
        public void DaUpdatedSalesProductunit(string user_gid, salesproductunit_list values)
        {
            try
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
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Sales Product Unit  !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }


       


        public void DadeleteSalesProductunitSummary(string productuomclass_gid, salesproductunit_list values)
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
                values.message = "Exception occured while Deleting Sales Product Unit !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            


        }

        // ADD product unit class

        public void DaGetProductunits(string productuomclass_gid,MdlSmrMstProductUnit values)
        {
            try
            {
               
                msSQL = "Select productuomclass_gid, productuomclass_code, productuomclass_name" +
                       " from pmr_mst_tproductuomclass where productuomclass_gid='" + productuomclass_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<salesproductunitgrid_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new salesproductunitgrid_list
                    {


                        productuomclass_gid = dt["productuomclass_gid"].ToString(),
                        productuomclass_code = dt["productuomclass_code"].ToString(),
                        productuomclass_name = dt["productuomclass_name"].ToString(),
                       

                    });
                    values.salesproductunitgrid_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product Unit !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            



        }




        public void PostProductunits(string user_gid, salesproductunitgrid_list values)
        {
            try {
               

                msSQL = "select productuomclass_gid from pmr_mst_tproductuomclass where productuomclass_name ='" + values.productuomclass_name + "'";
            string lsproductuomclassgid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select productuom_gid from pmr_mst_tproductuom where productuom_name ='" + values.productuomclass_name + "'";
            string lsproductuomgid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sequence_level from pmr_mst_tproductuom where productuom_name ='" + values.productuomclass_name + "'";
            string lssequencelevel = objdbconn.GetExecuteScalar(msSQL);



            msSQL = " select productuom_code, productuom_name, productuomclass_gid, a.sequence_level, a.convertion_rate " +
               " from pmr_mst_tproductuom  " +
               " a where productuomclass_gid='" + lsproductuomclassgid + "' " +
               " and a.sequence_level = '" + lssequencelevel + "' and productuom_gid  in ('" + lsproductuomgid + "') ";
            objOdbcDataReader = objdbconn.GetDataReader(msSQL);


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
                values.message = "Exception occured while loading Product Unit !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
    }
}