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
    public class DaAmsMstBlock
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msGetGid, lsCode, lsunit_name, locationunit_name;
        int mnResult;




        public void DaGetBlockSummary(blocklist values)
        {
            msSQL = "select locationblock_code,block_prefix,locationblock_name,locationunit_name ,locationblock_gid  from ams_mst_tlocationblock a  " +
            " left join ams_mst_tlocationunit b on a.locationunit_gid= b.locationunit_gid  order by locationblock_code desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<blockdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new blockdtl
                    {
                        locationunit_name = dt["locationunit_name"].ToString(),
                        locationblock_gid = dt["locationblock_gid"].ToString(),
                        locationblock_code = dt["locationblock_code"].ToString(),
                        block_prefix = dt["block_prefix"].ToString(),
                        locationblock_name = dt["locationblock_name"].ToString(),

                    });
                    values.blockdtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetunitdropdown(MdlAmsMstBlock values)
        {
            msSQL = " select  locationunit_gid,locationunit_code,locationunit_name  " +
                    " from ams_mst_tlocationunit ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<block_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new block_list
                    {
                        locationunit_gid = dt["locationunit_gid"].ToString(),
                        locationunit_name = dt["locationunit_name"].ToString(),
                    });
                    values.block_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaPostBlock(string user_gid, block_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("LBLK");
            lsCode = objcmnfunctions.GetMasterGID("BC");

            msSQL = " Select locationunit_gid from ams_mst_tlocationunit where locationunit_name = '" + values.locationunit_name + "'";
            string lslocationunit_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " insert into ams_mst_tlocationblock(" +
                " locationblock_gid, " +
               " locationunit_gid, " +
                 " locationblock_code, " +
                " locationblock_name, " +
                " block_prefix, " +
                " created_by, " +
                " created_date )" +
                " values (" +
                " '" + msGetGid + "', " +
                "'" + lslocationunit_gid + "'," +
                " '" + lsCode + "'," +
                " '" + values.locationblock_name + "'," +
                " '" + values.block_prefix + "'," +
                 "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Block Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Block";
            }
        }
        public void DaupdateBlock(string user_gid, block_list values)
        {
            msSQL = " select locationunit_gid from ams_mst_tlocationunit where locationunit_name = '" + values.locationunit_name + "'";
            string lslocationunit_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " update ams_mst_tlocationblock set  " +
                " block_prefix = '" + values.block_prefix + "'," +
                " locationblock_name = '" + values.locationblock_name + "'," +
                 " updated_by = '" + user_gid + "'," +
                 " locationunit_gid = '" + lslocationunit_gid + "'," +
                 " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where locationblock_gid='" + values.locationblock_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Block Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Block";
            }
        }
        public void DaGetdeleteBlockdetails(string locationblock_gid, block_list values)
        {
            msSQL = "  delete from ams_mst_tlocationblock where locationblock_gid='" + locationblock_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Block Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Block";
            }

        }


        public void DaGetbreadcrumb(string user_gid, string module_gid, blocklist values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumb_listblock>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumb_listblock
                    {


                        module_name1 = dt["module_name1"].ToString(),
                        sref1 = dt["sref1"].ToString(),
                        module_name2 = dt["module_name2"].ToString(),
                        sref2 = dt["sref2"].ToString(),
                        module_name3 = dt["module_name3"].ToString(),
                        sref3 = dt["sref3"].ToString(),

                    });
                    values.breadcrumb_listblock = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
    }
}