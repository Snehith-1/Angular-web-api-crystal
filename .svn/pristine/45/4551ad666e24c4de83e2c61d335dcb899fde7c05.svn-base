
using System.Collections.Generic;
using System;
using ems.system.Models;
using ems.utilities.Functions;

using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using static OfficeOpenXml.ExcelErrorValue;
using System.Data.Odbc;


namespace ems.system.DataAccess
{
    public class DaSysMstModuleManage
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL, msSQL2 = string.Empty;
        DataTable dt_datatable, dt_levelone;
       // Dictionary<string, object> objGetReaderScalar;
        OdbcDataReader objGetReaderScalar;
        OdbcDataReader objGetReaderData;
        //List<Dictionary<string, object>> objGetReaderData;
        int mnResult;
        String msGetGid = string.Empty;

        public bool DaGetModuleListSummary(mdlModuleList objmodulelist)
        {
            try
            {
                msSQL = " select a.module_gid, a.module_name, CONCAT( COALESCE(c.user_firstname, ''), COALESCE(c.user_lastname, ''), " +
                        " CASE WHEN c.user_code IS NOT NULL THEN CONCAT(' / ', c.user_code)  ELSE '' END) as module_manager, " +
                        " a.modulemanager_gid from adm_mst_tmodule a  " +
                        " left join hrm_mst_temployee b on a.modulemanager_gid = b.employee_gid " +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                        " where menu_level = 1 and status<> 0 " +
                        " Order by menu_level asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_ModuleList = new List<mdlModuleDtl>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_ModuleList.Add(new mdlModuleDtl
                        {
                            module_gid = dr_datarow["module_gid"].ToString(),
                            module_name = dr_datarow["module_name"].ToString(),
                            module_manager = dr_datarow["module_manager"].ToString(),
                            modulemanager_gid = dr_datarow["modulemanager_gid"].ToString(),
                        });
                    }
                    objmodulelist.mdlModuleDtl = get_ModuleList;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        public bool DaPostManagerAssign(mdlManagerAssignDtl objvalues)
        {
            string lshierarchy = "";
            msSQL = " select max(hierarchy_level) as hierarchy_level from adm_mst_tmodule2employee " +
                     " where module_gid = '" + objvalues.module_gid + "'";
            lshierarchy = objdbconn.GetExecuteScalar(msSQL);
            if (lshierarchy == "1" || lshierarchy == "" || lshierarchy == "0")
            {
                msSQL = " Update adm_mst_tmodule set " +
                        " modulemanager_gid='" + objvalues.modulemanager_gid + "'" +
                        " where module_gid = '" + objvalues.module_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = " update adm_mst_tmodule2employee set " +
                            " hierarchy_level='-1'" +
                            " where module_gid = '" + objvalues.module_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 0)
                    {
                        objvalues.status = false;
                        objvalues.message = "Error Occured While Assigning Module Manager";
                        return false;
                    }

                    msSQL = " select employeereporting_to from adm_mst_tmodule2employee" +
                            " where employeereporting_to='EM1006040001' and module_gid='" + objvalues.module_gid + "'";
                    objGetReaderScalar = objdbconn.GetDataReader(msSQL);
                    if (objGetReaderScalar != null )
                    {
                        msSQL = " update adm_mst_tmodule2employee set " +
                                " hierarchy_level='1'" +
                                " where employeereporting_to = 'EM1006040001'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 0)
                        {
                            objvalues.status = false;
                            objvalues.message = "Error Occured While Assigning Module Manager";
                            return false;
                        }
                    }
                    else
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("SMEM");
                        msSQL = " insert into adm_mst_tmodule2employee " +
                                " (module2employee_gid, " +
                                " hierarchy_level, " +
                                " employee_gid, " +
                                " employeereporting_to, " +
                                " module_gid) " +
                                " values ( " +
                                "'" + msGetGid + "'," +
                                "'1'," +
                                "'" + objvalues.modulemanager_gid + "'," +
                                "'EM1006040001'," +
                                "'" + objvalues.module_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objvalues.message = "Module Manager Assigned Successsfully";
                    objvalues.status = true;
                    return true;
                }
                else
                {
                    objvalues.status = false;
                    objvalues.message = "Error Occured While Assigning Module Manager";
                    return false;
                }
            }
            else
            {
                objvalues.status = false;
                objvalues.message = "If you want to change manager, kindly remove hierarchy";
                return false;
            }
        }

        public void DaGetEmployeeAssignlist(string module_gid, mdlemployee objmaster)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' || ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' and " +
                   " employee_gid not in (select employee_gid from adm_mst_tmodule2employee " +
                   " where module_gid='" + module_gid + "' and hierarchy_level<>'-1') order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employeelist>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    objmaster.employeelist = dt_datatable.AsEnumerable().Select(row =>
                      new employeelist
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }
        public bool DaGetModuleAssignedEmployee(string module_gid, mdlModuleAssignedList objmodulelist)
        {
            try
            {
                msSQL = " select a.employee_gid, concat(b.user_code,'/',b.user_firstname,' ',b.user_lastname) as user_name " +
                        " from adm_mst_tmodule2employee a" +
                        " left join hrm_mst_temployee c on a.employee_gid=c.employee_gid" +
                        " left join adm_mst_tuser b on c.user_gid=b.user_gid" +
                        " where a.module_gid='" + module_gid + "' and a.hierarchy_level<>'-1' and b.user_status='Y' " +
                        " order by b.user_firstname asc,a.hierarchy_level asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_dataList = new List<mdlModuleHierarchy>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_dataList.Add(new mdlModuleHierarchy
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            user_name = dr_datarow["user_name"].ToString(),
                        });
                    }
                    objmodulelist.mdlModuleHierarchy = get_dataList;
                }
                dt_datatable.Dispose();

                msSQL = "  SELECT a.employee_gid, c.user_gid,c.user_code, " +
                        " concat(c.user_firstname,' ',c.user_lastname) as user_name, " +
                        " CASE WHEN c.user_status = 'Y' THEN 'Active'  WHEN c.user_status = 'N' THEN 'Inactive' " +
                        " END as user_status, " +
                        " (select count(module_gid) from adm_mst_tprivilege where module_gid= '" + module_gid + "' " +
                        " and user_gid = c.user_gid) as menuaccess FROM adm_mst_tmodule2employee a " +
                        " left join hrm_mst_temployee b on a.employee_gid = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.module_gid = '" + module_gid + "' order by a.module2employee_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_ModuleList = new List<mdlModuleAssigneddtl>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_ModuleList.Add(new mdlModuleAssigneddtl
                        {
                            user_gid = dr_datarow["user_gid"].ToString(),
                            user_code = dr_datarow["user_code"].ToString(),
                            user_name = dr_datarow["user_name"].ToString(),
                            user_status = dr_datarow["user_status"].ToString(),
                            menuaccess = dr_datarow["menuaccess"].ToString(),
                        });
                    }
                    objmodulelist.mdlModuleAssigneddtl = get_ModuleList;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public bool DaPostModuleEmployeeAssign(mdlModuleemployeedtl objvalues, string user_gid)
        {
            msSQL = " Select modulemanager_gid from adm_mst_tmodule Where module_gid = '" + objvalues.module_gid + "' ";
            string lsmodulemanager_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select hierarchy_level from adm_mst_tmodule2employee " +
                    " where employee_gid='" + objvalues.assign_hierarchy + "' " +
                    " and module_gid='" + objvalues.module_gid + "'";
            int lshierarchy_level = Convert.ToInt16(objdbconn.GetExecuteScalar(msSQL));
            int hlevel = lshierarchy_level + 1;

            msSQL = $@" INSERT INTO adm_mst_tmodule2employee (module2employee_gid,module_gid, employee_gid,hierarchy_level, " +
                      " employeereporting_to,created_by,created_date) VALUES ";
            List<string> valueRows = new List<string>();
            foreach (var i in objvalues.Mdlassignemployeelist)
            {
                if (!string.IsNullOrWhiteSpace(i.employee_gid))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SMEM");
                    valueRows.Add($"('{msGetGid}','{objvalues.module_gid}', '{i.employee_gid}', '{hlevel}', '{objvalues.assign_hierarchy}','{user_gid}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }
            }
            msSQL += string.Join(", ", valueRows);
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objvalues.status = true;
                objvalues.message = "Employee Assigned Successfully";
                return true;
            }
            else
            {
                objvalues.status = false;
                objvalues.message = "Error Occurred While Inserting Module";
                return false;
            }
        }


        public void DaGetUserRoleList(MdlSelectedModule objvalues, menu_response values)
        {
            var ModuleAssigned_user = new DataTable();
            List<sys_menu> getmenu = new List<sys_menu>();
            List<mdlMenuData> mdlMenuData = new List<mdlMenuData>();
            List<mdlMenuData> mdlUserAssignedData = new List<mdlMenuData>();

            if (!string.IsNullOrWhiteSpace(objvalues.user_gid))
            {
                msSQL = " select  module_gid, module_parent_gid as module_gid_parent from adm_mst_tprivilege " +
                       " where user_gid = '" + objvalues.user_gid + "' and module_parent_gid = '" + objvalues.module_parentgid + "'";
                ModuleAssigned_user = objdbconn.GetDataTable(msSQL);
                if (ModuleAssigned_user != null)
                    mdlUserAssignedData = cmnfunctions.ConvertDataTable<mdlMenuData>(ModuleAssigned_user);
            }

            //msSQL = " EXEC dbo.adm_mst_spGetMenuListByModuleGid " +
            //        " @module_gid='" + objvalues.module_parentgid + "'";
            //msSQL = " SELECT m.module_gid, m.module_name, m.menu_level,m.display_order, m.module_gid_parent FROM adm_mst_tmodule m" +
            //       " where m.module_gid like '" + objvalues.module_parentgid + "%'";
            msSQL = " SELECT m.module_gid, m.module_name, m.menu_level,m.display_order, m.module_gid_parent,CASE WHEN m.module_gid  in (select module_gid from adm_mst_tprivilege where user_gid='" + objvalues.user_gid + "')" +
                   " THEN 'Y' ELSE 'N'END AS menu_access FROM  adm_mst_tmodule m" +
                   " LEFT JOIN  adm_mst_tprivilege p ON m.module_gid = p.module_gid" +
                   " WHERE m.module_gid like '" + objvalues.module_parentgid + "%' group by m.module_gid";

            dt_levelone = objdbconn.GetDataTable(msSQL);
            if (dt_levelone != null)
            {
                mdlMenuData = cmnfunctions.ConvertDataTable<mdlMenuData>(dt_levelone);
                try
                {
                    List<mdlMenuData> getFirstLevel = mdlMenuData.Where(a => a.menu_level == "1").ToList();
                    if (getFirstLevel.Count != 0)
                    {
                        foreach (var i in getFirstLevel)
                        {
                            List<mdlMenuData> getSecondLevel = mdlMenuData.Where(a => a.menu_level == "2" && a.module_gid_parent == i.module_gid).OrderBy(a => a.display_order).GroupBy(a => a.module_gid)
                                   .Select(group => new mdlMenuData
                                   {
                                       module_gid = group.Key,
                                       module_name = group.First().module_name,
                                       menu_level = group.First().menu_level,
                                       display_order = group.First().display_order,
                                       menu_access = group.First().menu_access
                                   }).ToList();
                            List<sys_submenu> getmenu2 = new List<sys_submenu>();
                            if (getSecondLevel != null)
                            {
                                foreach (var j in getSecondLevel)
                                {
                                    List<mdlMenuData> getThirdLevel = mdlMenuData.Where(a => a.menu_level == "3" && a.module_gid_parent == j.module_gid).OrderBy(a => a.display_order).GroupBy(a => a.module_gid)
                                    .Select(group => new mdlMenuData
                                    {
                                        module_gid = group.Key,
                                        module_name = group.First().module_name,
                                        menu_level = group.First().menu_level,
                                        display_order = group.First().display_order,
                                        menu_access = group.First().menu_access

                                    }).ToList();
                                    List<sys_sub1menu> getmenu3 = new List<sys_sub1menu>();
                                    if (getThirdLevel != null)
                                    {
                                        foreach (var k in getThirdLevel)
                                        {
                                            //var getFourthLevel = mdlMenuData.Where(a => a.menu_level == "4" && a.module_gid_parent == k.module_gid)
                                            //                     .OrderBy(a => a.display_order)
                                            //                     .GroupBy(a => a.module_gid).ToList();
                                            List<sys_sub2menu> getmenu4 = new List<sys_sub2menu>();
                                            //if (getFourthLevel != null)
                                            //{
                                            //    getmenu4 = getFourthLevel.SelectMany(group => group).Select(row => new sys_sub2menu
                                            //    {
                                            //        text = row.module_name,
                                            //        module_gid = row.module_gid,
                                            //        module_checked = mdlUserAssignedData?.Where(z => z.module_gid == row.module_gid && z.module_gid_parent == objvalues.module_parentgid).ToList()?.Any() ?? false
                                            //    }).ToList();
                                            //}
                                            getmenu3.Add(new sys_sub1menu
                                            {
                                                text = k.module_name,
                                                module_gid = k.module_gid,
                                                sub2menu = getmenu4,
                                                menu_access = k.menu_access
                                            });
                                        }
                                    }
                                    getmenu2.Add(new sys_submenu
                                    {
                                        text = j.module_name,
                                        module_gid = j.module_gid,
                                        sub1menu = getmenu3
                                    });
                                }
                            }
                            else
                            {
                            }
                            getmenu.Add(new sys_menu
                            {
                                text = i.module_name,
                                module_gid = i.module_gid,
                                submenu = getmenu2
                            });
                            values.menu_list = getmenu;
                        }
                    }
                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                }
                finally
                {
                }
                dt_levelone.Dispose();
                values.status = true;
                return;
            }
            values.message = "No data Found";
            dt_levelone.Dispose();

        }

        public bool DaPostPrivilege(MdlSelectedModule objvalues)
        {
            string msGet_PrivGID = "";

            msSQL = " delete from adm_mst_tprivilege where user_gid = '" + objvalues.user_gid + "' " +
                    " and module_parent_gid like '" + objvalues.module_parentgid + "%'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGet_PrivGID = objcmnfunctions.GetMasterGID("SPGM");
            if (msGet_PrivGID == "E")
            {
                return false;
            }
            //string selectedModuleGid = "(" + string.Join(",", objvalues.module_gid.Select(id => $"{id}")) + ")";
            List<mdlMenuData> mdlMenuData = new List<mdlMenuData>();
            msSQL = " select module_gid_parent as module_gid from adm_mst_tmodule where module_gid in ('" + (objvalues.module_gid).Replace(",","','") + "') group by module_gid_parent";
            dt_levelone = objdbconn.GetDataTable(msSQL);
            msSQL2 = " update  adm_mst_tmodule set lw_flag = 'Y'  " +
                     " where module_gid = '" + objvalues.module_parentgid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL2);
            if (dt_levelone != null)
            {
                mdlMenuData = cmnfunctions.ConvertDataTable<mdlMenuData>(dt_levelone);
                try
                {
                    msSQL = $@" INSERT INTO adm_mst_tprivilege (privilege_gid,module_gid, user_gid,module_parent_gid) VALUES ";
                    List<string> valueRows = new List<string>();
                    foreach (var i in mdlMenuData)
                    {
                        if (!string.IsNullOrWhiteSpace(i.module_gid))
                        {
                            msGet_PrivGID = objcmnfunctions.GetMasterGID("SPGM");
                            valueRows.Add($"('{msGet_PrivGID}','{i.module_gid}', '{objvalues.user_gid}', '{objvalues.module_parentgid}')");
                            msSQL2 = " update  adm_mst_tmodule set lw_flag = 'Y'  " +
                            " where module_gid = '" + i.module_gid + "'";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL2);
                        }
                    }
                    msGet_PrivGID = objcmnfunctions.GetMasterGID("SPGM");

                    valueRows.Add($"('{msGet_PrivGID}','{objvalues.module_parentgid}', '{objvalues.user_gid}', '{objvalues.module_parentgid}')");
                    string msSQL1= " select  module_gid,module_gid_parent from adm_mst_tmodule where module_gid in ('" + (objvalues.module_gid).Replace(",", "','") + "') ";
                   DataTable dt_leveltwo = objdbconn.GetDataTable(msSQL1);
                    if(dt_leveltwo != null )
                    {
                        mdlMenuData = cmnfunctions.ConvertDataTable<mdlMenuData>(dt_leveltwo);
                        foreach (var j in mdlMenuData)
                        {
                            if (!string.IsNullOrWhiteSpace(j.module_gid))
                            {
                                msGet_PrivGID = objcmnfunctions.GetMasterGID("SPGM");
                                valueRows.Add($"('{msGet_PrivGID}','{j.module_gid}', '{objvalues.user_gid}', '{j.module_gid_parent}')");
                                msSQL2 = " update  adm_mst_tmodule set lw_flag = 'Y'  " +
                                         " where module_gid = '" + j.module_gid + "'";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL2);
                            }
                        }


                    }

                    msSQL += string.Join(", ", valueRows);
                    
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                catch (Exception ex)
                {
                    objvalues.message = ex.ToString();
                    objvalues.status = false;
                    return false;
                }
                dt_levelone.Dispose();
                objvalues.message = "User Role Assigned Successfully";
                objvalues.status = true;
                return true;
            }
            objvalues.message = "No data Found";
            dt_levelone.Dispose();
            return true;
        }

        public void Dadeletehierarchy(mdlManagerAssignDtl objvalues)
        {
            msSQL = " delete from adm_mst_tmodule2employee" +
                    " where employeereporting_to<>'EM1006040001' and module_gid='" + objvalues.module_gid + "'";
            
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                objvalues.status = true;
                objvalues.message = "Clear Hierarchy Successfully";
            }
            else
            {
                objvalues.status = false;
                objvalues.message = "Error Occured While Clearing";
            }
        }

    }
}
