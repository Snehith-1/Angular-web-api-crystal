
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.hrm.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using MySql.Data.MySqlClient;
using static OfficeOpenXml.ExcelErrorValue;


namespace ems.hrm.DataAccess
{
    public class DaHrmTrnAssetcustodian
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader objMySqlDataReader;
        DataTable dt_datatable, dt_datatable1, dt_datatable3;
        int mnResult, mnResult1;
        string msUserGid, msEmployeeGID, msBiometricGID, msGetemployeetype, msTemporaryAddressGetGID, msPermanentAddressGetGID, usercode, lsuser_gid, lsemployee_gid, lsuser_code, lscountry_gid2, lscountry_gid;


        public void DaGetBranch(MdlHrmTrnAssetcustodian values)
        {
            try
            {
                
                msSQL = " Select branch_name,branch_gid  " +
                    " from hrm_mst_tbranch ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetBranch>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetBranch
                        {
                            branch_name = dt["branch_name"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),
                        });
                        values.GetBranch = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }

    public void GetDepartment(string branch_gid,MdlHrmTrnAssetcustodian values)
        {
            try
            {
                
                if (branch_gid != "all")
                {
                    msSQL = "select distinct a.department_gid,a.department_name from hrm_mst_tdepartment a " +
                            "inner join hrm_mst_temployee b on a.department_gid = b.department_gid " +
                            "inner join hrm_mst_tbranch c on b.branch_gid = c.branch_gid " +
                            "where c.branch_gid ='" + branch_gid + "' ";
                }
                else
                {
                    msSQL = "select distinct a.department_gid,a.department_name from hrm_mst_tdepartment a " +
                            "inner join hrm_mst_temployee b on a.department_gid = b.department_gid " +
                            "inner join hrm_mst_tbranch c on b.branch_gid = c.branch_gid ";
                }
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetDepartment>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetDepartment
                        {
                            department_gid = dt["department_gid"].ToString(),
                            department_name = dt["department_name"].ToString(),
                        });
                        values.GetDepartment = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }

    public void DaGetusetdtl(string employee_gid, MdlHrmTrnAssetcustodian values)
         {
            try
            {
                
                msSQL = " select concat(a.user_firstname ,' ',a.user_lastname) as user_name,a.user_code from adm_mst_tuser a" +
                    " left join hrm_mst_temployee b on a.user_gid=b.user_gid " +
                    " where b.employee_gid='" + employee_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetAddCustodian>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetAddCustodian
                        {
                            user_code = dt["user_code"].ToString(),
                            user_name = dt["user_name"].ToString(),
                        });
                        values.GetAddCustodian = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

    public void DaGetAddassetcustodian(string employee_gid, MdlHrmTrnAssetcustodian values)
        {
            try
            {
                
                msSQL = "select asset_gid,assetref_no,asset_name from hrm_mst_temployeeassetlist where active_flag = 'Y'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count <= 0)
                {
                    return;
                }
                var getModuleList = new List<GetAddCustodian>();
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " select a.*,b.* from hrm_trn_tassetcustodian a left join hrm_mst_temployeeassetlist b on a.asset_gid=b.asset_gid" +
                        " where a.employee_gid='" + employee_gid + "' " +
                        " and b.asset_gid='" + dr["asset_gid"].ToString() + "' ";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);


                    if (dt_datatable1.Rows.Count <= 0)
                    {
                        msSQL = " select * from hrm_mst_temployeeassetlist where " +
                                " asset_gid='" + dr["asset_gid"].ToString() + "' ";
                        dt_datatable3 = objdbconn.GetDataTable(msSQL);
                    }
                    else
                    {
                        msSQL = " select a.asset_gid,a.asset_id, a.assetref_no, a.asset_name, a.custodian_date, a.custodian_enddate,a.remarks" +
                                " from hrm_trn_tassetcustodian a" +
                                " left join hrm_mst_temployeeassetlist b on a.asset_gid=b.asset_gid" +
                                " where a.employee_gid='" + employee_gid + "'  and a.asset_gid='" + dr["asset_gid"].ToString() + "' ";
                        dt_datatable3 = objdbconn.GetDataTable(msSQL);
                    }

                if (dt_datatable3.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable3.Rows)
                        {
                            getModuleList.Add(new GetAddCustodian
                            {
                                asset_id = dt["asset_id"].ToString(),
                                assetref_no = dt["assetref_no"].ToString(),
                                asset_gid = dt["asset_gid"].ToString(),
                                asset_name = dt["asset_name"].ToString(),
                                custodian_date = dt["custodian_date"].ToString(),
                                custodian_enddate = dt["custodian_enddate"].ToString(),
                                remarks = dt["remarks"].ToString(),
                            });

                        }
                    }
                }

                values.GetAddCustodian = getModuleList;

                dt_datatable3.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

    public void DaGetassetcustodiansummary(string branch_name, string department_name, MdlHrmTrnAssetcustodian values)
        {
            try
            {
                
                msSQL = " Select distinct a.user_gid,c.useraccess, " +
                  " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                  " c.employee_gender,  " +
                  " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                  " d.designation_name,c.designation_gid,c.employee_gid,e.branch_name, " +
                  " CASE " +
                  " WHEN a.user_status = 'Y' THEN 'Active'  " +
                  " WHEN a.user_status = 'N' THEN 'Inactive' " +
                  " END as user_status,c.department_gid,c.branch_gid,g.department_name " +
                  " FROM adm_mst_tuser a " +
                  " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                  " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                  " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                  " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                  " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                  " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                  " left join hrm_mst_tsectionassign2employee i on i.employee_gid=c.employee_gid" +
                  " left join hrm_trn_temployeedtl m on m.employee_gid=c.employee_gid ";



                if (department_name != "all" && branch_name != "all")
                {
                    msSQL += "WHERE c.branch_gid ='" + branch_name + "' and  c.department_gid='" + department_name + "' ";
                }
                else
                {
                    if (department_name != "all")
                    {

                        msSQL += " WHERE c.department_gid ='" + department_name + "' ";
                    }
                    if (branch_name != "all")
                    {

                        msSQL += " WHERE c.branch_gid ='" + branch_name + "' ";
                    }

                }

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<custodian_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new custodian_list
                        {
                            user_gid = dt["user_gid"].ToString(),
                            useraccess = dt["useraccess"].ToString(),
                            user_code = dt["user_code"].ToString(),
                            user_name = dt["user_name"].ToString(),
                            employee_joiningdate = dt["employee_joiningdate"].ToString(),
                            employee_gender = dt["employee_gender"].ToString(),
                            emp_address = dt["emp_address"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                            designation_gid = dt["designation_gid"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            user_status = dt["user_status"].ToString(),
                            department_gid = dt["department_gid"].ToString(),
                            branch_gid = dt["branch_gid"].ToString(),
                            department_name = dt["department_name"].ToString(),


                        });
                        values.custodian_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

        public void DaPostcusdotiandtl(Assetcustodian values)
        {
            try
            {
                
                if (values.flag == "1")
                {
                    msSQL = " delete from hrm_trn_tassetcustodian where employee_gid='" + values.employee_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (values.asset_id != "" && values.custodian_date != "" && values.custodian_enddate != "" && values.remarks != "")
                {
                    msUserGid = objcmnfunctions.GetMasterGID("ASTC");
                    msSQL = " insert into hrm_trn_tassetcustodian(" +
                              " assetcustodian_gid," +
                              " asset_gid," +
                              " employee_gid," +
                              " assetref_no," +
                              " asset_name," +
                              " asset_id," +
                              " custodian_date," +
                              " custodian_enddate," +
                              " remarks" +
                              ")values(" +
                    " '" + msUserGid + "'," +
                    " '" + values.asset_gid + "'," +
                    " '" + values.employee_gid + "'," +
                    " '" + values.assetref_no + "'," +
                    " '" + values.asset_name + "'," +
                    " '" + values.asset_id + "'," +
                    " '" + values.custodian_date + "'," +
                    " '" + values.custodian_enddate + "'," +
                     "'" + values.remarks + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Asset Custodian Added Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Adding Asset Custodian";
                    }
                }
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

    public void DaGetassetcustodianExpand(string employee_gid, MdlHrmTrnAssetcustodian values)
        {
            try
            {
                
                msSQL = " select a.employee_gid,concat(c.user_code,'/',c.user_firstname,' ',c.user_lastname) as empname, " +
                "a.assetref_no, a.asset_name, a.asset_id,a.asset_gid, a.custodian_date, a.custodian_enddate, a.remarks, a.status " +
                "from hrm_trn_tassetcustodian a " +
                "left join hrm_mst_temployee b on a.employee_gid = b.employee_gid " +
                "left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                "left join hrm_mst_tsectionassign2employee i on i.employee_gid = a.employee_gid " +
                " where a.employee_gid ='" + employee_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<GetAddCustodian>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new GetAddCustodian
                        {
                            asset_id = dt["asset_id"].ToString(),
                            assetref_no = dt["assetref_no"].ToString(),
                            asset_gid = dt["asset_gid"].ToString(),
                            asset_name = dt["asset_name"].ToString(),
                            custodian_date = dt["custodian_date"].ToString(),
                            custodian_enddate = dt["custodian_enddate"].ToString(),
                            remarks = dt["remarks"].ToString(),
                        });
                        values.GetAddCustodian = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

    public void DaAssetDocument(string assed_gid, string employee_gid, MdlHrmTrnAssetcustodian values)
        {
            try
            {
                
                msSQL = "select * from hrm_trn_tassetdocument where asset_gid='" + assed_gid + "' and " +
                    " employee_gid='" + employee_gid + "'";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<Assetcustodian>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new Assetcustodian
                        {

                            asset_gid = dt["asset_gid"].ToString(),
                            document_gid = dt["document_gid"].ToString(),
                            document_path = dt["document_path"].ToString(),
                        });
                        values.Assetcustodian = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

    public void DaUpdateAssetdocument(HttpRequest httpRequest, result objResult, string user_gid)
        {

            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            HttpPostedFile httpPostedFile;

            string lspath;
            string msGetGid;
            string asset_gid = httpRequest.Form[0];
            string employee_gid = httpRequest.Form[1];


            MemoryStream ms = new MemoryStream();
            lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + "Asset/Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
            }
            try
            {
                
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + "Asset/Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        string status;
                        status = objcmnfunctions.uploadFile(lspath + msdocument_gid, FileExtension);
                 
                        ms.Close();
                        lspath = "erpdocument" + "/" + "Asset/Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        string final_path = lspath + msdocument_gid + FileExtension;

                        msSQL = " SELECT document_gid FROM hrm_trn_tassetdocument  where employee_gid = '" + asset_gid + "' and employee_gid = '" + employee_gid + "'  ";
                        objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                     
                            msSQL = " insert into hrm_trn_tassetdocument( " +
                            " document_gid," +
                            " asset_gid," +
                            " employee_gid," +
                            " document_path," +
                            " document_name," +
                            " created_by," +
                            " created_date" +
                            " )values(" +
                            " '" + msdocument_gid + "'," +
                            " '" + asset_gid + "'," +
                            " '" + employee_gid + "'," +
                            " '" + final_path + "'," +
                            " '" + FileExtension + "'," +
                            "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            objResult.status = true;
                            objResult.message = "Document Updated Successfully !!";
                        }
                        else
                        {
                            objResult.status = false;
                            objResult.message = "Error While Updating  Document !!";
                        }
                    }

                }
            }

            catch (Exception ex)
            {
                objResult.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }
        }

    public void DadownloadFile(string document_gid, MdlHrmTrnAssetcustodian values)
        {
            try
            {
                
                msSQL = "select * from hrm_trn_tassetdocument where document_gid='" + document_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<DownloadFile>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new DownloadFile
                        {
                            document_path = dt["document_path"].ToString(),
                            document_name = dt["document_name"].ToString(),

                        });
                        values.DownloadFile = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }
    public void DaGetCompanyPolicies(MdlHrmTrnAssetcustodian values)
        {
            try
            {
                
                msSQL = " select policy_name,policy_desc from hrm_trn_tcompanypolicy; ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<CompanyPoliies>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new CompanyPoliies
                        {
                            policy_name = dt["policy_name"].ToString(),
                            policy_desc = dt["policy_desc"].ToString(),
                        });
                        values.CompanyPoliies = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;

                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() +
                 "***********" + ex.Message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/HR/ " + "Log" + DateTime.Now.ToString("yyyy - MM - dd HH") + ".txt");
            }

        }
    }
}