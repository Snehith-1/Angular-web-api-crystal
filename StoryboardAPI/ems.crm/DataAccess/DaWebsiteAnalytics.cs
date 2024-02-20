using ems.crm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.system.Models;
using System.Text.RegularExpressions;


namespace ems.crm.DataAccess
{
    public class DaWebsiteAnalytics
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader  objOdbcDataReader ;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaPostWebsiteAnalyticsUser(string user_gid, assignvisitsubmit_list6 values)
        {
            try { 
                           
            msSQL = "select distinct(date_format(created_date,'%Y-%m-%d') )as created_date from crm_smm_tgoogleanalyticsuser ;";
            string created_date = objdbconn.GetExecuteScalar(msSQL);
            if(created_date != DateTime.Now.ToString("yyyy-MM-dd")) 
            { 

                msSQL = "  delete from crm_smm_tgoogleanalyticsuser;";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    for (int i = 0; i < values.analytics_list.ToArray().Length; i++)
                    {

                        msSQL = "INSERT INTO crm_smm_tgoogleanalyticsuser (firstSessionDate,firstUserCampaignName,country," +
                            "activeUsers,newUsers,totalUsers,eventCount,created_date) " +
                            "VALUES (     " +
                            "STR_TO_DATE('" + values.analytics_list[i].firstSessionDate + "','%Y%m%d')," +
                            "'" + values.analytics_list[i].firstUserCampaignName + "'," +
                            "'" + values.analytics_list[i].country + "'," +
                            "'" + values.analytics_list[i].activeUsers + "'," +
                            "'" + values.analytics_list[i].newUsers + "'," +
                            "'" + values.analytics_list[i].totalUsers + "'," +
                            "'" + values.analytics_list[i].eventCount + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        //if (mnResult != 0)
                        //{
                        //    values.status = true;

                        //}
                        //else
                        //{
                        //    values.status = false;
                        //    values.message = "Error While Adding";
                        //}
                    }
                }
         
            
            }
            }
            catch (Exception ex)
            {
                values.message = "Exception While Adding User";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Websiteanalytics/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }


        public void DaPostWebsiteAnalyticsPage(string user_gid, assignvisitsubmit_list6 values)
        {
            try
            {

                msSQL = "select distinct(date_format(created_date,'%Y-%m-%d') )as created_date from crm_smm_tgoogleanalyticspage ;";
                string created_date = objdbconn.GetExecuteScalar(msSQL);
                if (created_date != DateTime.Now.ToString("yyyy-MM-dd"))
                {

                    msSQL = "  delete from crm_smm_tgoogleanalyticspage;";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        for (int i = 0; i < values.analytics_list.ToArray().Length; i++)
                        {

                            msSQL = "INSERT INTO crm_smm_tgoogleanalyticspage (firstUserCampaignName ,date,hour," +
                                "minute,deviceCategory,operatingSystem,pageTitle,city,country,activeUsers,newUsers," +
                                "totalUsers,screenPageViews,screenPageViewsPerUser,eventCount,created_date) " +
                                "VALUES (     " +
                                "'" + values.analytics_list[i].firstUserCampaignName + "'," +
                                "'" + values.analytics_list[i].date + "'," +
                                "'" + values.analytics_list[i].hour + "'," +
                                "'" + values.analytics_list[i].minute + "'," +
                                "'" + values.analytics_list[i].deviceCategory + "'," +
                                "'" + values.analytics_list[i].operatingSystem + "'," +
                                "'" + values.analytics_list[i].pageTitle + "'," +
                                "'" + values.analytics_list[i].city + "'," +
                                "'" + values.analytics_list[i].country + "'," +
                                "'" + values.analytics_list[i].activeUsers + "'," +
                                "'" + values.analytics_list[i].newUsers + "'," +
                                "'" + values.analytics_list[i].totalUsers + "'," +
                                "'" + values.analytics_list[i].screenPageViews + "'," +
                                "'" + values.analytics_list[i].screenPageViewsPerUser + "'," +
                                "'" + values.analytics_list[i].eventCount + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            //if (mnResult != 0)
                            //{
                            //    values.status = true;

                            //}
                            //else
                            //{
                            //    values.status = false;
                            //    values.message = "Error While Adding";
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                values.message = "Exception While Adding Page";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Websiteanalytics/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }


        public void DaGetWebsiteSessionchart(MdlWebsiteAnalytics values)
        {
            try
            {

                msSQL = "SELECT CONCAT(UPPER(SUBSTRING(firstUserCampaignName, 2, 1)), LOWER(SUBSTRING(firstUserCampaignName, 3, CHAR_LENGTH(firstUserCampaignName) - 3))) as CampaignName,COUNT(eventCount) as eventCount FROM crm_smm_tgoogleanalyticsuser WHERE firstUserCampaignName != '(not set)' GROUP BY CampaignName; ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<analytics_summarylist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new analytics_summarylist
                        {
                            CampaignName = dt["CampaignName"].ToString(),
                            eventCount = dt["eventCount"].ToString(),
                            //direct = dt["direct"].ToString(),
                            //referral = dt["referral"].ToString(),
                            //organic = dt["organic"].ToString(),
                        });
                    }
                    values.analytics_summarylist = getmodulelist;
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception While Get Session Chart";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Websiteanalytics/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }

        public void DaGetWebsiteUserchart(MdlWebsiteAnalytics values)
        {
            try
            {

                msSQL = "SELECT city,country, SUM(activeUsers) AS total_users FROM crm_smm_tgoogleanalyticspage where city!='(not set)' GROUP BY city order by total_users desc; ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<analytics_summarylist2>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new analytics_summarylist2
                        {
                            city = dt["city"].ToString(),
                            country = dt["country"].ToString(),
                            total_users = dt["total_users"].ToString(),
                            //referral = dt["referral"].ToString(),
                        });
                    }
                    values.analytics_summarylist2 = getmodulelist;
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception While Get User Chart";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Websiteanalytics/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaGetWebsiteAnalyticsSummary(MdlWebsiteAnalytics values)
        {
            try
            {
    
                msSQL = "select city,country,newUsers,firstUserCampaignName,deviceCategory,pageTitle,eventCount from crm_smm_tgoogleanalyticspage ;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<analytics_summarylist1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new analytics_summarylist1
                        {
                            city = dt["city"].ToString(),
                            country = dt["country"].ToString(),
                            newUsers = dt["newUsers"].ToString(),
                            firstUserCampaignName = dt["firstUserCampaignName"].ToString(),
                            deviceCategory = dt["deviceCategory"].ToString(),
                            eventCount = dt["eventCount"].ToString(),
                            pageTitle = dt["pageTitle"].ToString(),
                        });
                    }
                    values.analytics_summarylist1 = getmodulelist;
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception While Getting Analytic Summary";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Websiteanalytics/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaGetdaywisechart(MdlWebsiteAnalytics values)
        {
            try
            {

                msSQL = "SELECT DATE_FORMAT(firstSessionDate, '%b-%d') AS daily_date,sum(activeUsers)  AS daily_users FROM crm_smm_tgoogleanalyticsuser WHERE MONTH(firstSessionDate) = MONTH(CURDATE()) GROUP BY daily_date ORDER BY daily_date;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<analytics_report>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new analytics_report
                        {
                            daily_date = dt["daily_date"].ToString(),
                            daily_users = dt["daily_users"].ToString(),
                        });
                    }
                    values.analytics_report = getmodulelist;
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception While Getting Daywise Chart";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Websiteanalytics/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }
        public void DaGetweekwisechart(MdlWebsiteAnalytics values)
        {
            try
            {

                msSQL = "SELECT DATE_FORMAT(firstSessionDate, '%a-%d') AS week_date,firstSessionDate,sum(activeUsers)  AS users FROM crm_smm_tgoogleanalyticsuser WHERE firstSessionDate >= CURDATE() - INTERVAL 7 DAY GROUP BY week_date ORDER BY firstSessionDate;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<analytics_report>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new analytics_report
                        {
                            week_date = dt["week_date"].ToString(),
                            week_users = dt["users"].ToString(),
                        });
                    }
                    values.analytics_report = getmodulelist;
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception While Getting Weekwise Chart";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Websiteanalytics/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }

        public void DaGetmonthwisechart(MdlWebsiteAnalytics values)
        {
            try
            {
   

                msSQL = "SELECT DATE_FORMAT(firstSessionDate, '%b') AS Months, SUM(activeUsers) AS users FROM crm_smm_tgoogleanalyticsuser WHERE firstSessionDate >= CURDATE() - INTERVAL 6 MONTH GROUP BY Months ORDER BY firstSessionDate;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<analytics_report>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new analytics_report
                        {
                            Months = dt["Months"].ToString(),
                            users = dt["users"].ToString(),
                        });
                    }
                    values.analytics_report = getmodulelist;
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception While Getting Monthwise Chart";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Websiteanalytics/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }

        public void DaGetyearwisechart(MdlWebsiteAnalytics values)
        {
            try
            {

                msSQL = "SELECT  DATE_FORMAT(firstSessionDate, '%b-%y') AS years,YEAR(firstSessionDate) AS year,sum(activeUsers)  AS year_users FROM crm_smm_tgoogleanalyticsuser GROUP BY year ORDER BY year;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<analytics_report>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new analytics_report
                        {
                            year = dt["year"].ToString(),
                            year_users = dt["year_users"].ToString(),
                        });
                    }
                    values.analytics_report = getmodulelist;
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception While Getting Yearwise Chart";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Websiteanalytics/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }
        public void DaGetWebsiteAnalyticsPageSessions(MdlWebsiteAnalytics values)
        {
            try
            {

                msSQL = "SELECT pageTitle,CONCAT(FLOOR(SUM(hour * 60 + minute) / 60), 'h ',SUM(hour * 60 + minute) % 60, 'm') AS totalDuration FROM crm_smm_tgoogleanalyticspage GROUP BY pageTitle";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<analytics_summarylist3>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new analytics_summarylist3
                        {
                            pageTitle = dt["pageTitle"].ToString(),
                            totalDuration = dt["totalDuration"].ToString(),
                        });
                    }
                    values.analytics_summarylist3 = getmodulelist;
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception While Getting Page Session";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Websiteanalytics/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }
        public void DaGetWebsiteAnalyticstiles(MdlWebsiteAnalytics values)
        {
            try
            {

                msSQL = "select (select sum(activeUsers) as user from crm_smm_tgoogleanalyticsuser) as total_user," +
             "(select sum(newUsers) as user from crm_smm_tgoogleanalyticsuser) as newUsers," +
             "(select count(eventCount) as user from crm_smm_tgoogleanalyticsuser " +
             "where firstUserCampaignName='(direct)') as direct," +
             "(select count(eventCount) as user from crm_smm_tgoogleanalyticsuser " +
             "where firstUserCampaignName='(referral)') as referral," +
             "(select count(eventCount) as user from crm_smm_tgoogleanalyticsuser " +
             "where firstUserCampaignName='(organic)') as organic;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmodulelist = new List<analytics_summarylist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmodulelist.Add(new analytics_summarylist
                        {
                            newUsers = dt["newUsers"].ToString(),
                            total_user = dt["total_user"].ToString(),
                        });
                    }
                    values.analytics_summarylist = getmodulelist;
                }

                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception While Getting Page Tiles";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess:{System.Reflection.MethodBase.GetCurrentMethod().Name}" + " * **********" + ex.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "SocialMedia/ErrorLog/Websiteanalytics/" + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

    }

}
