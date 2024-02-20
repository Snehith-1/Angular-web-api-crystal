using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.asset.Models;
namespace ems.asset.DataAccess
{
    public class DaAmsMstAttribute
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msGetGid, lsCode, status;
        int mnResult;

        public void DaPostAttribute(string user_gid, attribute_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("ASAT");
            lsCode = objcmnfunctions.GetMasterGID("AC");
            status = "Active";
            msSQL = " insert into ams_mst_tattribute (" +
                " attribute_gid, " +
                " attribute_code," +
                " attribute_name," +
                " status_attribute," +
                " created_by, " +
                " created_date)" +
                " values (" +
                " '" + msGetGid + "', " +
                " '" + lsCode + "'," +
                " '" + values.attribute_name + "'," +
                " '" + status + "'," +
                " '" + user_gid + "'," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Attribute Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Attribute";
            }
        }
        public void DaGetAttributeSummary(MdlAmsMstAttribute values)
        {
            msSQL = "select attribute_gid,attribute_name,attribute_code,status_attribute from ams_mst_tattribute";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<attribute_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new attribute_list
                    {
                        attribute_gid = dt["attribute_gid"].ToString(),
                        attribute_name = dt["attribute_name"].ToString(),
                        attribute_code = dt["attribute_code"].ToString(),
                        status_attribute = dt["status_attribute"].ToString(),


                    });
                    values.attribute_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaUpdateAttribute(string user_gid, attribute_list values)
        {

            msSQL = " update ams_mst_tattribute set  " +
                " attribute_name = '" + values.attribute_name + "'," +
                " attribute_code = '" + values.attribute_code + "'," +
                 " updated_by = '" + user_gid + "'," +
                 " status_attribute = '" + values.status_attribute + "'," +
                 " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where attribute_gid='" + values.attribute_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Attribute Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Attribute";
            }
        }
        public void DaDeleteAttribute(string attribute_gid, attribute_list values)
        {
            msSQL = "  delete from ams_mst_tattribute where attribute_gid='" + attribute_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Attribute Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Attribute";
            }

        }
        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlAmsMstAttribute values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumb_listattribute>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumb_listattribute
                    {


                        module_name1 = dt["module_name1"].ToString(),
                        sref1 = dt["sref1"].ToString(),
                        module_name2 = dt["module_name2"].ToString(),
                        sref2 = dt["sref2"].ToString(),
                        module_name3 = dt["module_name3"].ToString(),
                        sref3 = dt["sref3"].ToString(),

                    });
                    values.breadcrumb_listattribute = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
    }
}