using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.sales.Models;
using ems.utilities.Functions;
//using Newtonsoft.Json;
using static OfficeOpenXml.ExcelErrorValue;
using File = System.IO.File;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
//using MySql.Data.MySqlClient;
//using System.Data.SqlClient;

namespace ems.sales.DataAccess
{
    public class DaSmrMstSalesConfig
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
       // MySqlDataReader objMySqlDataReader;
        int mnResult;
        string lsaddoncharges, lsadditionaldiscount, lsfreightcharges, lspacking_forwardingcharges, lsinsurancecharges;

        public void DaGetAllChargesConfig(string employee_gid, MdlSmrMstSalesConfig values)
        {
            try
            {
                msSQL = " select id, charges, flag from smr_mst_tsalesconfig ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<salesconfigalllist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new salesconfigalllist
                        {
                            id = dt["id"].ToString(),
                            charges = dt["charges"].ToString(),
                            flag = dt["flag"].ToString(),
                        });
                        values.salesconfigalllist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }
        public void DaUpdateAddOnChargesConfig(string employee_gid, salesconfiglist values)
        {
            try
            {
                if (values.addoncharges == true)
                {
                    lsaddoncharges = "Y";
                }
                else
                {
                    lsaddoncharges = "N";
                }

                msSQL = " update smr_mst_tsalesconfig set " +
                        " flag = '" + lsaddoncharges + "'," +
                        " updated_by = '" + employee_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where id = 1 ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Configured Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error occured while configuration";
                }
            }

            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }


        }
        public void DaUpdateAdditionalDiscountConfig(string employee_gid, salesconfiglist values)
        {
            try
            {
                if (values.additionaldiscount == true)
                {
                    lsadditionaldiscount = "Y";
                }
                else
                {
                    lsadditionaldiscount = "N";
                }

                msSQL = " update smr_mst_tsalesconfig set " +
                        " flag = '" + lsadditionaldiscount + "'," +
                        " updated_by = '" + employee_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where id = 2 ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Configured Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error occured while configuration";
                }
            }

            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }


        }
        public void DaUpdateFreightChargesConfig(string employee_gid, salesconfiglist values)
        {
            try
            {
                if (values.freightcharges == true)
                {
                    lsfreightcharges = "Y";
                }
                else
                {
                    lsfreightcharges = "N";
                }

                msSQL = " update smr_mst_tsalesconfig set " +
                        " flag = '" + lsfreightcharges + "'," +
                        " updated_by = '" + employee_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where id = 3 ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Configured Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error occured while configuration";
                }
            }

            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaUpdatePacking_ForwardingChargesConfig(string employee_gid, salesconfiglist values)
        {
            try
            {
                if (values.packing_forwardingcharges == true)
                {
                    lspacking_forwardingcharges = "Y";
                }
                else
                {
                    lspacking_forwardingcharges = "N";
                }

                msSQL = " update smr_mst_tsalesconfig set " +
                        " flag = '" + lspacking_forwardingcharges + "'," +
                        " updated_by = '" + employee_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where id = 4 ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Configured Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error occured while configuration";
                }
            }

            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
        public void DaUpdateInsuranceChargesConfig(string employee_gid, salesconfiglist values)
        {
            try
            {
                if (values.insurancecharges == true)
                {
                    lsinsurancecharges = "Y";
                }
                else
                {
                    lsinsurancecharges = "N";
                }

                msSQL = " update smr_mst_tsalesconfig set " +
                        " flag = '" + lsinsurancecharges + "'," +
                        " updated_by = '" + employee_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where id = 5 ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Configured Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error occured while configuration";
                }
            }

            catch (Exception ex)
            {
                values.status = false;
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Sales/ " + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }
    }
}