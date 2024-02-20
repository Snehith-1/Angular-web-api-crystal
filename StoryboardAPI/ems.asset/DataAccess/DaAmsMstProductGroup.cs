using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.asset.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
using System.Web;


using System.Configuration;
using System.IO;

using System.Net.Mail;


namespace ems.asset.DataAccess
{
    public class DaAmsMstProductGroup
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msGetGid, lsCode;
        int mnResult;

        public void DaGetProductGroupSummary(MdlAmsMstProductGroup values)
        {
            msSQL = "select productgroup_gid,productgroup_code,productgroup_prefix,productgroup_name,product_type from pmr_mst_tproductgroup " +
                    "where product_type in ('Asset','IT Asset-Hardware','IT Asset-Software') order by productgroup_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<amsmstproductgroupdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new amsmstproductgroupdtl
                    {
                        productgroup_gid = dt["productgroup_gid"].ToString(),
                        productgroup_code = dt["productgroup_code"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),
                        productgroup_prefix = dt["productgroup_prefix"].ToString(),
                        product_type = dt["product_type"].ToString(),

                    });
                    values.amsmstproductgroupdtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostProductGroupAdd(string user_gid, productgroup_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("PPGM");
            lsCode = objcmnfunctions.GetMasterGID("P");

            msSQL = " insert into pmr_mst_tproductgroup(" +
                    " productgroup_gid," +
                    " productgroup_code," +
                    " Productgroup_prefix," +
                    " productgroup_name," +
                    " product_type," +
                    " created_by," +
                    " created_date)values(" +
                " '" + msGetGid + "', " +
                 " '" + lsCode + "'," +
                " '" + values.productgroup_prefix + "'," +
                " '" + values.productgroup_name + "'," +
                 " '" + values.product_type + "'," +
                 "'" + user_gid + "'," +
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

        public void DaPostProductGroupUpdate(string user_gid, productgroup_list values)
        {
            

            msSQL = " update  pmr_mst_tproductgroup set " +
                " productgroup_prefix = '" + values.productgroupedit_prefix + "'," +
                 " productgroup_name = '" + values.productgroupedit_name + "'," +
                 " product_type = '" + values.product_type + "'," +
                 " updated_by = '" + user_gid + "'," +
                 " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                 "' where productgroup_gid='" + values.productgroup_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
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

        public void Dadeleteproductgroup(string productgroup_gid, productgroup_list values)
        {
            msSQL = "  delete from pmr_mst_tproductgroup where productgroup_gid='" + productgroup_gid + "' ";
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

        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlAmsMstProductGroup values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumb_list3>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumb_list3
                    {


                        module_name1 = dt["module_name1"].ToString(),
                        sref1 = dt["sref1"].ToString(),
                        module_name2 = dt["module_name2"].ToString(),
                        sref2 = dt["sref2"].ToString(),
                        module_name3 = dt["module_name3"].ToString(),
                        sref3 = dt["sref3"].ToString(),

                    });
                    values.breadcrumb_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

    }
}