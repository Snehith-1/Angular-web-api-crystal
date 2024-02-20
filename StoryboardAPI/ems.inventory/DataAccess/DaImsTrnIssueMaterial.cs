using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.inventory.Models;


namespace ems.inventory.DataAccess
{
    public class DaImsTrnIssueMaterial
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaGetImsTrnIssueMaterial(MdlImsTrnIssueMaterial values)
        {
            try
            {
                
                msSQL = " select distinct a.materialissued_gid, a.materialrequisition_gid, a.materialissued_status, " +
                " a.materialissued_date,concat(e.user_firstname,'/',d.department_name) as issued_to,m.costcenter_name," +
                " b.user_firstname,concat(g.branch_name,'/',d.department_name) as department_name, a.branch_gid,f.materialrequisition_reference" +
                " from ims_trn_tmaterialissued a " +
                " Left join ims_trn_tmaterialrequisition f on f.materialrequisition_gid = a.materialrequisition_gid " +
                " left join pmr_mst_tcostcenter m on f.costcenter_gid=m.costcenter_gid " +
                " left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                " left join hrm_mst_temployee c on c.user_gid = b.user_gid " +
                " left join hrm_mst_tdepartment d on c.department_gid = d.department_gid " +
                " left join hrm_mst_tbranch g on a.branch_gid = g.branch_gid" +
                " where 1=1 order by  date(a.materialissued_date) desc,a.materialissued_date asc, a.materialissued_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getissuematerial_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getissuematerial_list
                    {

                        productuom_gid = dt["materialissued_gid"].ToString(),
                        materialissued_date = dt["materialissued_date"].ToString(),
                        materialissued_gid = dt["materialissued_gid"].ToString(),
                        costcenter_name = dt["costcenter_name"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        materialrequisition_gid = dt["materialrequisition_gid"].ToString(),
                        materialrequisition_reference = dt["materialrequisition_reference"].ToString(),
                        user_firstname = dt["user_firstname"].ToString(),
                        issued_to = dt["issued_to"].ToString(),


                    });
                    values.Getissuematerial_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = "Exception occured while loading Issue Material !";
                objcmnfunctions.LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + $"DataAccess: {System.Reflection.MethodBase.GetCurrentMethod().Name}" + "***********" + ex.Message.ToString() + "***********" + values.message.ToString() + "*****Query****" + msSQL + "*******Apiref********", "ErrorLog/Inventory/" + "Log" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt");
            }
            
        }
    }
}