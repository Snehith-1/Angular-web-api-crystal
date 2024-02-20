using System.Collections.Generic;
using ems.pmr.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
//using System.Web;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
using System.Web;
using System;


namespace ems.pmr.DataAccess
{
    public class DaPmrMstProductGroup
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetProductGroupSummary(MdlPmrMstProductGroup values)
        {
            try
            {
                
                msSQL = " SELECT  productgroup_gid, productgroup_name,productgroup_code, " +
                  " CONCAT(b.user_firstname,' ',b.user_lastname) " +
                  " as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                  " from pmr_mst_tproductgroup a " +
                  " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<productgroup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new productgroup_list
                        {
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                            productgroup_code = dt["productgroup_code"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),



                        });
                        values.productgroup_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while getting Product group summary!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }




        public void DaPostProductGroup(string user_gid, productgroup_list values)
        {
            try
            {
               
                msSQL = " select * from pmr_mst_tproductgroup where productgroup_code= '" + values.productgroup_code + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count != 0)
                {
                    //values.status = true;
                    values.message = "Product Group code  Already Exist";


                }


                else
                {
                    msSQL = " select * from pmr_mst_tproductgroup where productgroup_name= '" + values.productgroup_name + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    if (dt_datatable.Rows.Count != 0)
                    {
                        //values.status = true;
                        values.message = "Product Group Name  Already Exist";


                    }


                    else
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("PPGM");
                        msSQL = " SELECT productgroup_code FROM pmr_mst_tproductgroup WHERE productgroup_gid='" + values.productgroup_code + "' ";
                        string lsproductgroup_code = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " insert into pmr_mst_tproductgroup (" +
                                " productgroup_gid," +
                                " productgroup_code," +
                                " productgroup_name," +
                                " created_by, " +
                                " created_date)" +
                                " values(" +
                                " '" + msGetGid + "'," +
                                "'" + values.productgroup_code + "',";
                        if (values.productgroup_name == null || values.productgroup_name == "")
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.productgroup_name.Replace("'", "\\'") + "',";
                        }

                        msSQL += "'" + user_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Product Group Added Successfully";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error While Adding Product Group";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured whileAdding Product Group!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }

        public void DaGetUpdatedProductgroup(string user_gid, productgroup_list values)
        {
            try
            {
                
                msSQL = " update  pmr_mst_tproductgroup  set " +
         " productgroup_code = '" + values.productgroupedit_code + "'," +
  " productgroup_name = '" + values.productgroupedit_name + "'," +
  " updated_by = '" + user_gid + "'," +
  " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where productgroup_gid='" + values.productgroup_gid + "'  ";


                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    values.status = true;
                    values.message = "Product Group Updated Successfully";

                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Product Group";
                }

            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updating Product Group!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
         

        }



        public void DaGetDeleteProductSummary(string productgroup_gid, productgroup_list values)

        {
            try
            {

                msSQL = "  delete from pmr_mst_tproductgroup where productgroup_gid='" + productgroup_gid + "'  ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)

                {

                    values.status = true;

                    values.message = "Product Group Deleted Successfully";

                }

                else

                {

                    values.status = false;

                    values.message = "Error While Deleting Product Group";

                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Deleting Product Group!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" +
                    $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" +
                ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
                msSQL + "*******Apiref********", "ErrorLog/Purchase/" + "Log" +
                DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }
    }
}