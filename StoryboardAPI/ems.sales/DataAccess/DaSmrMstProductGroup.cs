using ems.sales.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.utilities.Functions;


namespace ems.sales.DataAccess
{
    public class DaSmrMstProductGroup
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaGetSalesProductGroupSummary(MdlSmrMstProductGroup values)
        {
            try {
               
                msSQL = " SELECT  productgroup_gid, productgroup_name,productgroup_code, " +
                  " CONCAT(b.user_firstname,' ',b.user_lastname) " +
                  " as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                  " from pmr_mst_tproductgroup a " +
                  " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<salesproductgroup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new salesproductgroup_list
                    {
                        productgroup_gid = dt["productgroup_gid"].ToString(),
                        productgroup_code = dt["productgroup_code"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),



                    });
                    values.salesproductgroup_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Product Group Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        //Add Event
        public void DaPostSalesProductGroup(string user_gid, salesproductgroup_list values)
        {
            try {
                
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
                values.message = "Exception occured while Submiting Product Group !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }

        public void DaGetUpdatedSalesProductgroup(string user_gid, salesproductgroup_list values)
        {
            try {
               
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
                values.message = "Exception occured while Updating Product Group !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }

        public void DaGetDeleteSalesProductSummary(string productgroup_gid, salesproductgroup_list values)

        {
            try {
               

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
                values.message = "Exception occured while Deleting Product Group !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           

        }

        public void DaGetUpdatedSalesTax(string user_gid, salesproductgroup_list values)

        {
            try {
               


                msSQL = " update  pmr_mst_tproductgroup  set " +
                    " tax1_gid = '" + values.tax1_gid + "'," +
                    " tax2_gid = '" + values.tax2_gid + "'," +
                    " tax3_gid = '" + values.tax3_gid + "'," +
                    " tax1_name = '" + values.tax1_name + "'," +
                    " tax2_name = '" + values.tax2_name + "'," +
                    " tax3_name = '" + values.tax3_name + "'," +
                    " updated_by = '" + user_gid + "'," +
                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where productgroup_gid='" + values.productgroup_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Product Tax Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Tax";
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while  Updating Product Tax !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            



        }



        // Tax 1
        public void DaGetTaxDtl(MdlSmrMstProductGroup values)
        {
            try {
               
                msSQL = " Select tax_gid, tax_name " +
                " from acp_mst_ttax ";



            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetTaxDropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetTaxDropdown
                    {
                        tax1_gid = dt["tax_gid"].ToString(),
                        tax1_name = dt["tax_name"].ToString(),
                    });
                    values.GetTaxDtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Tax !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }

        // tax 2 dropdown
        public void DaGetTax2Dtl(MdlSmrMstProductGroup values)
        {
            try {
               
                msSQL = " Select tax_gid, tax_name " +
                " from acp_mst_ttax ";



            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetTax2Dropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetTax2Dropdown
                    {
                        tax2_gid = dt["tax_gid"].ToString(),
                        tax2_name = dt["tax_name"].ToString(),
                    });
                    values.GetTax2Dtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured  while Getting Tax !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }

        // tax 3 dropdown
        public void DaGetTax3Dtl(MdlSmrMstProductGroup values)
        {
            try {
               
                msSQL = " Select tax_gid, tax_name " +
                " from acp_mst_ttax ";



            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetTax3Dropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetTax3Dropdown
                    {
                        tax3_gid = dt["tax_gid"].ToString(),
                        tax3_name = dt["tax_name"].ToString(),
                    });
                    values.GetTax3Dtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Tax!";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
           
        }



    }
}