using ems.crm.Models;
using ems.system.Models;
using ems.utilities.Functions;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Web;
using static OfficeOpenXml.ExcelErrorValue;
using static System.Collections.Specialized.BitVector32;



namespace ems.system.DataAccess
{
    public class DaIndustry
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        int mnResult;
        string msGetGid, msGetGid1, lsindustry_name, lsindustry_code, lscategoryindustry_code, lscategoryindustry_name, lscategoryindustry_gid;

        // Module Master Summary
        public void DaGetIndustrySummary(MdlIndustry values)
        {
            msSQL = " select categoryindustry_gid,   categoryindustry_code, categoryindustry_name,  category_desc ,created_by,created_date from crm_mst_tcategoryindustry order by categoryindustry_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<industry_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new industry_list
                    {
                        industry_gid = dt["categoryindustry_gid"].ToString(),
                        industry_code = dt["categoryindustry_code"].ToString(),
                        industry_name = dt["categoryindustry_name"].ToString(),
                        category_desc = dt["category_desc"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.industry_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaPostIndustry(string user_gid, industry_list values)

        {
            msSQL = " select categoryindustry_code from crm_mst_tcategoryindustry where categoryindustry_code = '" + values.industry_code + "'";
            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);


            if (objOdbcDataReader .HasRows == true)
            {
                values.status = false;
                values.message = "Industry Code Already Exist !!";
            }





            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("BCIM");
                msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='BCIM' order by finyear desc limit 0,1 ";
                string lsCode = objdbconn.GetExecuteScalar(msSQL);

                string lscategoryindustry_code = "BCM" + "000" + lsCode;

                msSQL = " select categoryindustry_name from crm_mst_tcategoryindustry where categoryindustry_name = '" + values.industry_name + "'";
                objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

                if (objOdbcDataReader .HasRows == true)
                {
                    values.status = false;
                    values.message = "Industry Name Already Exist !!";
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BCIM");

                    msSQL = " insert into crm_mst_tcategoryindustry(" +
                         " categoryindustry_gid ," +
                         " categoryindustry_code," +
                         " categoryindustry_name," +
                         " category_desc," +
                         " created_by, " +
                         " created_date)" +
                         " values(" +
                          " '" + msGetGid + "'," +
                            " '" + lscategoryindustry_code + "'," +
                            "'" + values.industry_name + "'," +
                             "'" + values.industry_description + "',";

                    msSQL += "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = " Industry Added Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Industry !!";

                    }
                }

                //    if (lscategoryindustry_code != values.industry_code && lscategoryindustry_name != values.industry_name)
                //    {

                //        msGetGid = objcmnfunctions.GetMasterGID("BCIM");

                //        msSQL = " insert into crm_mst_tcategoryindustry(" +
                //             " categoryindustry_gid ," +
                //             " categoryindustry_code," +
                //             " categoryindustry_name," +
                //             " category_desc," +
                //             " created_by, " +
                //             " created_date)" +
                //             " values(" +
                //              " '" + msGetGid + "'," +
                //                " '" + values.industry_code + "'," +
                //                "'" + values.industry_name + "'," +
                //                 "'" + values.industry_description + "',";

                //        msSQL += "'" + user_gid + "'," +
                //                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //        if (mnResult != 0)
                //        {
                //            values.status = true;
                //            values.message = " Industry Added Successfully !!";
                //        }
                //        else
                //        {
                //            values.status = false;
                //            values.message = "Error While Adding Industry !!";

                //        }
                //    }


                //    else
                //    {
                //        values.status = false;
                //        values.message = "Same industry name Already Exist !!";
                //    }


                //}
                //else
                //{
                //    values.status = false;
                //    values.message = "industry name Already Exist !!";
                //}

            }

        }


        public void DaGetupdateindustrydetails(string user_gid, industry_list values)

        {
            msSQL = " select categoryindustry_name,categoryindustry_gid from crm_mst_tcategoryindustry where categoryindustry_name = '" + values.industryedit_name + "'";
            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

            if (objOdbcDataReader .HasRows)
            {
                lscategoryindustry_name = objOdbcDataReader ["categoryindustry_name"].ToString();
                lscategoryindustry_gid = objOdbcDataReader ["categoryindustry_gid"].ToString();
            }

            if (lscategoryindustry_gid == values.industry_gid)
            {
                msSQL = " update  crm_mst_tcategoryindustry set " +
           " categoryindustry_name = '" + values.industryedit_name + "'," +
           " category_desc = '" + values.industryedit_description + "'," +
           " updated_by = '" + user_gid + "'," +
           " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where categoryindustry_gid='" + values.industry_gid + "'  ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)

                {
                    values.status = true;
                    values.message = "Industry Updated Successfully !!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Industry !!";
                }

            }
            else
            {
                if (string.Equals(lscategoryindustry_name, values.industryedit_name, StringComparison.OrdinalIgnoreCase))
                {
                    values.status = false;
                    values.message = "Industry with the same name already exists !!";
                }
                else
                {

                    msSQL = " update  crm_mst_tcategoryindustry set " +
                            " categoryindustry_name = '" + values.industryedit_name + "'," +
                            " category_desc = '" + values.industryedit_description + "'," +
                            " updated_by = '" + user_gid + "'," +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where categoryindustry_gid='" + values.industry_gid + "'  ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)

                    {
                        values.status = true;
                        values.message = "Industry Updated Successfully !!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Updating Industry !!";
                    }

                }
            }
        }

        public void DaGetdeleteindustrydetails(string industry_gid, industry_list values)

        {
            msSQL = "select leadbank_gid from crm_trn_tleadbank where categoryindustry_gid='" + industry_gid + "';";
            objOdbcDataReader  = objdbconn.GetDataReader(msSQL);

            if (objOdbcDataReader .HasRows)
            {
                values.status = false;
                values.message = "Industry already used hence can't be deleted!!";
            }
            else
            {

                msSQL = "  delete from crm_mst_tcategoryindustry where categoryindustry_gid='" + industry_gid + "'  ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)

                {

                    values.status = true;

                    values.message = "Industry Deleted Successfully !!";

                }

                else

                {

                    values.status = false;

                    values.message = "Error While Deleting Industry !!";

                }



            }
        }

     

    }
}
        

     
 
