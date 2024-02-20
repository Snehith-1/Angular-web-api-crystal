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
    public class DaAmsMstProductsubgroup
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msGetGid, lsCode, status;
        int mnResult;

        public void DaDeleteproductsubgroup(string productsubgroup_gid, productsubgroup_list values)
        {
            msSQL = "  delete from pmr_mst_tproductsubgroup where productsubgroup_gid='" + productsubgroup_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Productsubgroup Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Productsubgroup";
            }

        }
        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlAmsMstProductsubgroup values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumb_listproductsubgroup>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumb_listproductsubgroup
                    {


                        module_name1 = dt["module_name1"].ToString(),
                        sref1 = dt["sref1"].ToString(),
                        module_name2 = dt["module_name2"].ToString(),
                        sref2 = dt["sref2"].ToString(),
                        module_name3 = dt["module_name3"].ToString(),
                        sref3 = dt["sref3"].ToString(),

                    });
                    values.breadcrumb_listproductsubgroup = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetproductsubgroupSummary(MdlAmsMstProductsubgroup values)
        {
            msSQL = "select concat(d.assetblock_code,'/',d.assetblock_name) as assetblock_name,d.assetblock_code,d.assetblock_gid,c.natureofasset_code,"+
                 "concat(c.natureofasset_code,'/',c.description) as description,c.natureofasset_gid,"+
                 "concat(f.productgroup_code,'/',a.productgroup_name) as productgroup,a.productgroup_gid,"+
                 "a.productsubgroup_name as productsubgroup,a.productsubgroup_code,c.description,b.productgroup_name,"+
                 "concat(a.productsubgroup_code,'/',a.productsubgroup_name) as productsubgroup_name," +
                 "a.productsubgroup_gid,a.subgroup_status,a.custodian_type,case when a.subgroup_oem='N' then 'No' else 'Yes' end as subgroup_oem, " +
                 "case when a.subgroup_warranty='N' then 'No' else 'Yes' end as subgroup_warranty, case when a.subgroup_additionalwarranty='N' then 'No' else 'Yes' end as subgroup_additionalwarranty , " +
                 "case when a.subgroup_amc='N' then 'No' else 'Yes' end as  subgroup_amc " +
                 "from pmr_mst_tproductsubgroup a  " +
                 "left join pmr_mst_tproductgroup f on f.productgroup_gid=a.productgroup_gid " +
                 "left join pmr_mst_tproductgroup b on b.productgroup_gid = a.productgroup_gid  " +
                 "left join ams_mst_tnatureofasset c on c.natureofasset_gid = a.natureofasset_gid " +
                 "left join ams_mst_tassetblock d on d.assetblock_gid = a.assetblock_gid where b.product_type='Asset'  order by a.natureofasset_gid is null desc, a.productsubgroup_gid desc ";
         dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<productsubgroup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new productsubgroup_list
                    {
                        productgroup_gid = dt["productgroup_gid"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),
                        productgroup = dt["productgroup"].ToString(),
                        productsubgroup_name = dt["productsubgroup_name"].ToString(),
                        productsubgroup = dt["productsubgroup"].ToString(),
                        productsubgroup_code = dt["productsubgroup_code"].ToString(),
                        productsubgroup_gid = dt["productsubgroup_gid"].ToString(),
                        subgroup_warranty = dt["subgroup_warranty"].ToString(),
                        subgroup_additionalwarranty = dt["subgroup_additionalwarranty"].ToString(),
                        subgroup_oem = dt["subgroup_oem"].ToString(),
                        subgroup_amc = dt["subgroup_amc"].ToString(),
                        assetblock_name = dt["assetblock_name"].ToString(),
                        description = dt["description"].ToString(),
                        natureofasset_gid = dt["natureofasset_gid"].ToString(),
                        assetblock_gid = dt["assetblock_gid"].ToString(),
                        natureofasset_code = dt["natureofasset_code"].ToString(),
                        assetblock_code = dt["assetblock_code"].ToString(),

                    });
                    values.productsubgroup_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetProductgroup(MdlAmsMstProductsubgroup values)
        {
            msSQL = "select productgroup_gid,productgroup_name as productgroup_name1, concat(productgroup_code,'/',productgroup_name) as productgroup_name " +
                    "from pmr_mst_tproductgroup  where product_type in ('Asset','IT Asset-Hardware','IT Asset-software')  order by productgroup_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetProductgroup>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetProductgroup
                    {
                        productgroup_gid = dt["productgroup_gid"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),
                        productgroup_name1 = dt["productgroup_name1"].ToString(),
                    });
                    values.GetProductgroup = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetAssetblock(MdlAmsMstProductsubgroup values)
        {
            msSQL = " select assetblock_gid, concat(assetblock_code,'/',assetblock_name) as assetblock_name from ams_mst_tassetblock" +
                    " where block_status='Active' order by assetblock_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetAssetblock>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetAssetblock
                    {
                        assetblock_gid = dt["assetblock_gid"].ToString(),
                        assetblock_name = dt["assetblock_name"].ToString(),
                    });
                    values.GetAssetblock = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetNatureOfAsset(string assetblock_gid,MdlAmsMstProductsubgroup values)
        {
            msSQL = "select natureofasset_gid, concat(natureofasset_code,'/',description) as description from ams_mst_tnatureofasset" +
                    " where assetblock_gid ='" + assetblock_gid + "'  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetNatureOfAsset>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetNatureOfAsset
                    {
                        natureofasset_gid = dt["natureofasset_gid"].ToString(),
                        description = dt["description"].ToString(),

                    });
                    values.GetNatureOfAsset = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaPostProductsugroupAdd(string user_gid, productsubgroup_list values)
        {
            msSQL = "select productgroup_name from pmr_mst_tproductgroup where productgroup_gid='" + values.productgroup_name + "'";
            string lsproductgroup_name = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("ASSG");
            lsCode = objcmnfunctions.GetMasterGID("SGC");
            status = "Active";
            msSQL = " insert into pmr_mst_tproductsubgroup (" +
                " productsubgroup_gid, " +
                " productgroup_gid," +
                " productgroup_name," +
                " productsubgroup_code," +
                " productsubgroup_name," +
                " subgroup_status," +
                " subgroup_warranty," +
                " subgroup_additionalwarranty," +
                " subgroup_amc," +
                " subgroup_oem," +
                " created_by, " +
                " created_date)" +
                " values (" +
                " '" + msGetGid + "', " +
                " '" + values.productgroup_name + "'," +
                " '" + lsproductgroup_name + "'," +
                " '" + lsCode + "'," +
                " '" + values.productsubgroup_name + "'," +
                " '" + status + "'," +
                " '" + values.subgroup_warranty + "'," +
                 " " + (values.subgroup_warranty == "N" ? "'N'" : "'" + values.subgroup_additionalwarranty + "'") + "," +
                " '" + values.subgroup_amc + "'," +
                " '" + values.subgroup_oem + "'," +
                " '" + user_gid + "'," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "ProductSubGroup Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding ProductSubGroup";
            }
        }
        public void DaPostProductsubgroupUpdate(string user_gid, productsubgroup_list values)
        {
            msSQL = "select productgroup_name from pmr_mst_tproductgroup where productgroup_gid='" + values.productgroup_gid + "'";
            string lsproductgroup_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update pmr_mst_tproductsubgroup set  " +
                " productgroup_gid = '" + values.productgroup_gid + "'," +
                " productsubgroup_name = '" + values.productsubgroup + "'," +
                " productgroup_name = '" + lsproductgroup_name + "'," +
                " subgroup_warranty = '" + values.subgroup_warranty + "'," +
                " subgroup_additionalwarranty = '" + values.subgroup_additionalwarranty + "'," +
                " subgroup_amc = '" + values.subgroup_amc + "'," +
                " subgroup_oem = '" + values.subgroup_oem + "'," +
                " updated_by = '" + user_gid + "'," +
                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where productsubgroup_gid='" + values.productsubgroup_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Productsubgroup Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Productsubgroup";
            }
        }
        public void DaPostNatureofAsset(string user_gid, productsubgroup_list values)
        {
            //string assetblockCode = values.assetblock_name.Split('/')[0];
            //msSQL = "select assetblock_gid from ams_mst_tassetblock where assetblock_code='" + assetblockCode + "'";
            //string lsassetblock_gid = objdbconn.GetExecuteScalar(msSQL);
            string natureofasset_code = values.description.Split('/')[0];
            msSQL = "select natureofasset_gid from ams_mst_tnatureofasset where natureofasset_code='" + natureofasset_code + "'";
            string lsnatureofasset_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " update pmr_mst_tproductsubgroup set  " +
                " natureofasset_gid = '" + lsnatureofasset_gid + "'," +
                " assetblock_gid = '" + values.assetblock_name + "'" +
                " where productsubgroup_gid='" + values.productsubgroup_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Nature of Asset Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Nature of Asset";
            }
        }
    }
}