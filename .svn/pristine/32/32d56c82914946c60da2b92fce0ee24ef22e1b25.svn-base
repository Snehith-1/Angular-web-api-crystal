using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using ems.sales.Models;

namespace ems.sales.DataAccess
{
    public class DaSmrMstCurrency
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msGetGid;
        int mnResult;
        public void DaGetSmrCurrencySummary(MdlSmrMstCurrency values)
        {
            try
            {

                msSQL = " select currencyexchange_gid,currency_code,exchange_rate,country as country_name , CONCAT(b.user_firstname,' ',b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                        " from crm_trn_tcurrencyexchange a " +
                        " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_date desc";

                // %%%% Exchange rate integration query - DONT DELETE %%%%

                //                msSQL = " SELECT e.currencyexchange_gid,e.currency_code,e.exchange_rate,e.country AS country_name,CONCAT(b.user_firstname, ' ', b.user_lastname) AS created_by, " +
                //" DATE_FORMAT(e.created_date, '%d-%m-%Y') AS created_date FROM crm_trn_tcurrencyexchange e JOIN (SELECT currency_code, MAX(created_date) AS max_created_date " +
                //" FROM crm_trn_tcurrencyexchange GROUP BY currency_code) m ON e.currency_code = m.currency_code AND e.created_date = m.max_created_date " +
                //" LEFT JOIN adm_mst_tuser b ON b.user_gid = e.created_by GROUP BY e.currency_code  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getsales_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getsales_list
                        {
                            currencyexchange_gid = dt["currencyexchange_gid"].ToString(),
                            currency_code = dt["currency_code"].ToString(),
                            exchange_rate = dt["exchange_rate"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            country_name = dt["country_name"].ToString()
                        });
                        values.salescurrency_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Currency Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }

        public void DaGetSmrCountryDtl(MdlSmrMstCurrency values)
        {
            try
            {
                msSQL = " select  country_gid, country_name from adm_mst_tcountry";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getcountrydropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getcountrydropdown
                        {
                            country_gid = dt["country_gid"].ToString(),
                            country_name = dt["country_name"].ToString(),
                        });
                        values.GetSmrCountryDtl = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Getting Country Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
        }

        public void DaPostSmrCurrency(string user_gid, currencyDetails values)
        {
            try
            {
                msSQL = "select country_gid from crm_trn_tcurrencyexchange where country_gid= '" + values.country_name + "'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                objOdbcDataReader.Read();
                if (objOdbcDataReader.HasRows)
                {
                    values.message = "Country Name Already Exist";
                }
                else
                {
                    msSQL = " select currency_code from crm_trn_tcurrencyexchange where currency_code= '" + values.currency_code + "'";
                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                    objOdbcDataReader.Read();
                    if (objOdbcDataReader.HasRows)
                    {
                        values.message = "Currency Code Already Exist";
                    }
                    else
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("CUR");
                        msSQL = " Select country_name from adm_mst_tcountry where country_gid= '" + values.country_name + "'";
                        string lscountry_name = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "insert into crm_trn_tcurrencyexchange(" +
                                "currencyexchange_gid," +
                                "currency_code," +
                                "country_gid," +
                                "exchange_rate," +
                                "country," +
                                "created_by, " +
                                "created_date)" +
                                "values(" +
                                "'" + msGetGid + "'," +
                                "'" + values.currency_code.Replace("'", "\\'") + "'," +
                                "'" + values.country_name + "'," +
                                "'" + values.exchange_rate.Replace("'", "\\'") + "'," +
                                "'" + (String.IsNullOrEmpty(lscountry_name) ? lscountry_name : lscountry_name.Replace("'", "\\'")) + "'," +
                                "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Currency Added Successfully";
                        }
                        else
                        {
                            values.message = "Error While Adding Currency";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Submiting  Currency !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            if (objOdbcDataReader != null)
                objOdbcDataReader.Close();
        }

        public void DaSmrCurrencyUpdate(string user_gid, currencyDetailsEdit values)
        {
            try
            {
                msSQL = "select currency_code,exchange_rate,country from crm_trn_tcurrencyexchange where currencyexchange_gid = '" + values.currencyexchange_gid + "'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                objOdbcDataReader.Read();
                if (values.currency_codeedit != objOdbcDataReader["currency_code"].ToString() || values.country_nameedit != objOdbcDataReader["country"].ToString() || values.exchange_rateedit != objOdbcDataReader["exchange_rate"].ToString())
                {
                    msSQL = "insert into crm_trn_tcurrencyexchangehistory select * from crm_trn_tcurrencyexchange where currencyexchange_gid='" +values.currencyexchange_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = " Select country_gid from adm_mst_tcountry where country_name= '" + values.country_nameedit + "'";
                        string lscountry_gid = objdbconn.GetExecuteScalar(msSQL);
                        if(values.currency_codeedit != objOdbcDataReader["currency_code"].ToString())
                        {
                            msSQL = " select currency_code from crm_trn_tcurrencyexchange where currency_code= '" + values.currency_codeedit + "'";
                            objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                            objOdbcDataReader.Read();
                            if (objOdbcDataReader.HasRows)
                            {
                                values.status = false;
                                values.message = "Currency Code Already Exist";
                                return;
                            }
                        }
                        if (values.country_nameedit != objOdbcDataReader["country"].ToString())
                        {
                            msSQL = "select country_gid from crm_trn_tcurrencyexchange where country_gid= '" + lscountry_gid + "'";
                            objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                            objOdbcDataReader.Read();
                            if (objOdbcDataReader.HasRows)
                            {
                                values.message = "Country Name Already Exist";
                                return;
                            }
                        }
                        msSQL = " update  crm_trn_tcurrencyexchange set " +
                                " currency_code = '" + values.currency_codeedit + "'," +
                                " exchange_rate = '" + values.exchange_rateedit + "'," +
                                " country = '" + values.country_nameedit + "'," +
                                " country_gid = '" + lscountry_gid + "'," +
                                " updated_by = '" + user_gid + "'," +
                                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where currencyexchange_gid='" + values.currencyexchange_gid + "'  ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Currency Updated Successfully";
                        }
                        else
                        {
                            values.message = "Error While Updating Currency";
                        }
                    }
                    else
                    {
                        values.message = "Error While Adding Currency";
                    }
                }
                else
                {
                    values.message = "No changes in the update!";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Updatuin Currency !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            if (objOdbcDataReader != null)
                objOdbcDataReader.Close();
        }

        public void DaSmrCurrencySummaryDelete(currencyDetailsDelete values)
        {
            try
            {
                bool quotation_flag = false, sales_flag = false;
                msSQL = "SELECT * FROM smr_trn_treceivequotationdtl a  " +
                        " inner join smr_trn_treceivequotation b on a.quotation_gid=b.quotation_gid " +
                        " where '" + values.currencyexchange_gid +"' in (b.currency_gid) and b.delete_flag='N'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                objOdbcDataReader.Read();
                if(objOdbcDataReader.HasRows)
                {
                    quotation_flag = true;
                }
                msSQL = " SELECT * FROM smr_trn_tsalesorderdtl a " +
                        " inner join smr_trn_tsalesorder b on a.salesorder_gid=b.salesorder_gid " +
                        " where '" + values.currencyexchange_gid + "' in (b.currency_gid) ";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                objOdbcDataReader.Read();
                if (objOdbcDataReader.HasRows)
                {
                    sales_flag = true;
                }
                if(!(sales_flag || quotation_flag))
                {
                    msSQL = "  delete from crm_trn_tcurrencyexchange where currencyexchange_gid='" + values.currencyexchange_gid + "'  ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Currency Deleted Successfully";
                    }
                    else
                    {
                        values.message = "Error While Deleting Currency";
                    }
                }
                else
                {
                    values.message = "Cannot delete currency since it is involved in transactions!";
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while Deleting Currency Summary !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            if (objOdbcDataReader != null)
                objOdbcDataReader.Close();
        }

        public void DaGetDefaultCurrency(MdlSmrMstCurrency values)
        {
            try
            {

                msSQL = "select currency_code,exchange_rate from crm_trn_tcurrencyexchange where default_currency='Y'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Getsales_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Getsales_list
                        {

                            currency_code = dt["currency_code"].ToString(),
                            exchange_rate = dt["exchange_rate"].ToString(),

                        });
                        values.salescurrency_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = "Exception occured getting default Currency !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:" +
               $" {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" +
               msSQL + "*******Apiref********", "ErrorLog/Sales/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }

        }

    }
}