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
    public class DaAmsMstProduct
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msGetGid, lsCode, producttype_gid, producttype_name, productuomclass_gid;
        int mnResult;
        public void DaGetproductSummary(MdlAmsMstProduct values)
        {
            msSQL = "select a.product_code,a.serial_flag, a.product_gid,concat(b.productgroup_code,'/',b.productgroup_name) as productgroup_name, " +
                    "b.productgroup_name as productgroup ,z.attribute_name,x.attribute_gid,concat(c.productsubgroup_code,'/',c.productsubgroup_name) as productsubgroup_name, " +
                    "c.productsubgroup_name as productsubgroup,concat(a.product_code,'/',a.product_name) as product_name," +
                  "a.asset_images,a.productgroup_gid, a.productsubgroup_gid from pmr_mst_tproduct a " +
                  "left join pmr_mst_tproductgroup b on a.productgroup_gid=b.productgroup_gid " +
                  "left join ams_mst_tassignattribute2product x on a.product_gid=x.product_gid " +
                  "left join ams_mst_tattribute z on x.attribute_gid=z.attribute_gid " +
                  "left join pmr_mst_tproductsubgroup c on a.productsubgroup_gid=c.productsubgroup_gid where c.productsubgroup_gid!= 'NULL' group by a.product_gid order by a.product_gid desc ";
       dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<product_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new product_list
                    {
                        product_gid = dt["product_gid"].ToString(),
                        productgroup_gid = dt["productgroup_gid"].ToString(),
                        productsubgroup_gid = dt["productsubgroup_gid"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),
                        productgroup = dt["productgroup"].ToString(),
                        attribute_name = dt["attribute_name"].ToString(),
                        productsubgroup_name = dt["productsubgroup_name"].ToString(),
                        productsubgroup = dt["productsubgroup"].ToString(),
                        product_name = dt["product_name"].ToString(),
                        asset_images = dt["asset_images"].ToString(),
                        product_code = dt["product_code"].ToString(),
                        serial_flag = dt["serial_flag"].ToString(),


                    });
                    values.product_list = getModuleList;
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


        public void DaGetAttriute(MdlAmsMstProduct values)
        {
        msSQL = " select attribute_gid, attribute_code, attribute_name  from ams_mst_tattribute" +
                " where status_attribute='Active' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetAttribute>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetAttribute
                    {
                        attribute_code = dt["attribute_code"].ToString(),
                        attribute_gid = dt["attribute_gid"].ToString(),
                        attribute_name = dt["attribute_name"].ToString(),
                    });
                    values.GetAttribute = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetProductsubgroup(MdlAmsMstProduct values)
        {
            msSQL = "select productsubgroup_gid,productsubgroup_name as productsubgroup_name1, concat(productsubgroup_code,'/',productsubgroup_name) as productsubgroup_name " +
                    "from pmr_mst_tproductsubgroup  order by productsubgroup_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetProductsubgroup>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetProductsubgroup
                    {
                        productsubgroup_gid = dt["productsubgroup_gid"].ToString(),
                        productsubgroup_name = dt["productsubgroup_name"].ToString(),
                        productsubgroup_name1 = dt["productsubgroup_name1"].ToString(),
                    });
                    values.GetProductsubgroup = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaPostProductAdd(string user_gid, product_list values)
        {
            string productgroup_code = values.productgroup_name.Split('/')[0];
            //msSQL = "select productgroup_gid from pmr_mst_tproductgroup where productgroup_code='" + productgroup_code + "'";
            //string lsproductgroup_gid = objdbconn.GetExecuteScalar(msSQL);

            //string productsubgroup_code = values.productsubgroup_name.Split('/')[0];
            //msSQL = "select productsubgroup_gid from pmr_mst_tproductsubgroup where productsubgroup_code='" + productsubgroup_code + "'";
            //string lsproductsubgroup_gid = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("PPTM");
            lsCode = objcmnfunctions.GetMasterGID("PC");
            producttype_gid = "PG002";
            producttype_name = "Asset";
            productuomclass_gid = "PUCM1402210001";
            msSQL =     " insert into pmr_mst_tproduct ( " +
                        " product_gid," +
                        " product_code, " +
                        " product_name, " +
                        " productgroup_gid, " +
                        " productsubgroup_gid, " +
                        " producttype_gid, " +
                        " producttype_name, " +
                        " productuomclass_gid," +
                        " serial_flag ," +
                        " created_by, " +
                        " created_date)" +
                        " values ( " +
                " '" + msGetGid + "', " +
                " '" + lsCode + "'," +
                " '" + values.product_name + "'," +
                " '" + values.productgroup_name + "'," +
                " '" + values.productsubgroup_name + "'," +
                " '" + producttype_gid + "'," +
                " '" + producttype_name + "'," +
                " '" + productuomclass_gid + "'," +
                " '" + values.serial_flag + "'," +
                " '" + user_gid + "'," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Product";
            }
        }

        public void DaPostProductUpdate(string user_gid, product_list values)
        {
            string productgroup_name = values.productgroup_name.Split('/')[1];
            msSQL = "select productgroup_gid from pmr_mst_tproductgroup where productgroup_name='" + productgroup_name + "'";
            string lsproductgroup_gid = objdbconn.GetExecuteScalar(msSQL);

            string productsubgroup_code = values.productsubgroup_name.Split('/')[0];
            msSQL = "select productsubgroup_gid from pmr_mst_tproductsubgroup where productsubgroup_code='" + productsubgroup_code + "'";
            string lsproductsubgroup_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " update pmr_mst_tproduct set " +
                         " productgroup_gid =   '" + lsproductgroup_gid + "'," +
                         " productsubgroup_gid =  '" + lsproductsubgroup_gid + "'," +
                         " product_name =  '" + values.product_name + "'," +
                         " serial_flag =  '" + values.serial_flag + "'," +
                         " updated_by='" + user_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where product_gid = '" + values.product_gid + "'";
             
           mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Product";
            }
        }


        public void DaPostAttriuteAdd(string user_gid, product_list values)
        {
          
            msSQL = "select attribute_gid from ams_mst_tattribute where attribute_name='" + values.attribute_name + "'";
            string lsattribute_gid = objdbconn.GetExecuteScalar(msSQL);
  

            msGetGid = objcmnfunctions.GetMasterGID("AECE");
            msSQL = " insert into ams_mst_tassignattribute2product  ( " +
                        " attribute2product_gid," +
                        " attribute_gid, " +
                        " product_gid, " +
                        " productgroup_gid, " +
                        " productsubgroup_gid, " +
                        " created_by, " +
                        " created_date)" +
                        " values ( " +
                " '" + msGetGid + "', " +
                " '" + lsattribute_gid + "'," +
                " '" + values.product_gid + "'," +
                " '" + values.productgroup_gid + "'," +
                " '" + values.productsubgroup_gid + "'," +
                " '" + user_gid + "'," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Attribute taged Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Attribute";
            }
        }

        public void DaDeleteproduct(string product_gid, product_list values)
        {
            msSQL = "  delete from pmr_mst_tproduct where product_gid='" + product_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Product";
            }

        }



    }
}