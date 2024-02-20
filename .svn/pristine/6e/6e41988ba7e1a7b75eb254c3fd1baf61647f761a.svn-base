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

    public class DaAmsMstUnit
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msGetGid, lsCode;
        int mnResult;


        public void DaPostUnit(string user_gid, unit_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("LUNT");
            lsCode = objcmnfunctions.GetMasterGID("UC");

            msSQL = " insert into ams_mst_tlocationunit  (" +
                " locationunit_gid, " +
                 " locationunit_code, " +
                " locationunit_name, " +
                " locationunit_address, " +
                " unit_prefix, " +
                " created_by, " +
                " created_date )" +
                " values (" +
                " '" + msGetGid + "', " +
                " '" + lsCode + "'," +
                " '" + values.locationunit_name + "'," +
                " '" + values.locationunit_address + "'," +
                " '" + values.unit_prefix + "'," +
                 "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Unit Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Unit";
            }

        }
        public void DaGetUnitSummary(unitllist values)
        {
            msSQL = "select locationunit_code,unit_prefix,locationunit_name,locationunit_address,locationunit_gid  from ams_mst_tlocationunit order by locationunit_code desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<unitdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new unitdtl
                    {
                        locationunit_gid = dt["locationunit_gid"].ToString(),
                        locationunit_code = dt["locationunit_code"].ToString(),
                        unit_prefix = dt["unit_prefix"].ToString(),
                        locationunit_name = dt["locationunit_name"].ToString(),
                        locationunit_address = dt["locationunit_address"].ToString(),
                    });
                    values.unitdtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetupdateUnitdetails(string user_gid, unit_list values)
        {
            msSQL = " update  ams_mst_tlocationunit set " +
                 " unit_prefix = '" + values.unitedit_prefix + "'," +
                 " locationunit_name = '" + values.locationunitedit_name + "'," +
                 " locationunit_address = '" + values.locationunitedit_address + "'," +
                 " updated_by = '" + user_gid + "'," +
                 " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                 "' where locationunit_code='" + values.locationunit_code + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Unit Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Unit";
            }
        }

        public void DaGetdeleteUnitdetails(string locationunit_gid, unit_list values)
        {
            msSQL = "  delete from ams_mst_tlocationunit where locationunit_gid='" + locationunit_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Unit Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Unit";
            }
        }
        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlAmsMstUnit values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumb_list1>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumb_list1
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