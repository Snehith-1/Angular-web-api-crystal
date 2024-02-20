using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Web;
using ems.system.Models;
using ems.utilities.Functions;



namespace ems.system.DataAccess{
    public class DaSysMstTemplate
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
        int mnResult;
        public void DaTemplateSummary(MdlSysMstTemplate values)
        {
            msSQL = " select a.template_gid, a.template_name, c.templatetype_name, " +
                    " concat_ws(' ', b.user_firstname, b.user_lastname) as created_by from adm_mst_ttemplate a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " left join adm_mst_ttemplatetype c on a.templatetype_gid = c.templatetype_gid order by a.created_on desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getModuleList = new List<MdlSysMstTemplateSummarylist>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new MdlSysMstTemplateSummarylist
                    {
                        template_gid = dt["template_gid"].ToString(),
                        template_name = dt["template_name"].ToString(),
                        templatetype_name = dt["templatetype_name"].ToString(),
                        created_by = dt["created_by"].ToString()
                    });
                    values.templatesummarylist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetTemplateType(MdlSysMstTemplate values)
        {
            msSQL = " select templatetype_gid, templatetype_name from adm_mst_ttemplatetype ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getModuleList = new List<GetTemplateTypedropdown>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetTemplateTypedropdown
                    {
                        templatetype_gid = dt["templatetype_gid"].ToString(),
                        templatetype_name = dt["templatetype_name"].ToString(),
                    });
                    values.GetTemplateTypedropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaPostTemplate(string user_gid, MdlSysMstTemplatelist values)
        {
            msSQL = "select template_gid from adm_mst_ttemplate where template_name='" + (values.template_name).Trim().Replace("'", "") + "'";
            
            objOdbcDataReader = objdbconn.GetDataReader(msSQL);
            
            if (objOdbcDataReader.HasRows == false)
            {
                string msGetGID = objcmnfunctions.GetMasterGID("TMPL");
                
                msSQL = " insert into adm_mst_ttemplate ( " +
                        " template_gid, " +
                        " template_name, " +
                        " templatetype_gid, " +
                        " template_content, " +
                        " created_by, " +
                        " created_on) " +
                        " values(" +
                        " '" + msGetGID + "'," +
                        " '" + values.template_name + "'," +
                        " '" + values.templatetype_gid + "'," +
                        " '" + values.template_content.Replace("'","") + "'," +
                        " '" + user_gid + "'," +
                        " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Template Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Adding Template";
                }
            }
            else
            {
                values.status = false;
                values.message = "Template Name Already Exist";
            }
        }
        public void DaGetTemplateEditdata(MdlSysMstTemplate values, string template_gid)
        {
            msSQL = " select template_gid, template_name, templatetype_gid, template_content from adm_mst_ttemplate " +
                    " where template_gid = '" + template_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getModuleList = new List<MdlSysMstTemplateEditlist>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new MdlSysMstTemplateEditlist
                    {
                        template_gid_edit = dt["template_gid"].ToString(),
                        template_name_edit = dt["template_name"].ToString(),
                        templatetype_gid_edit = dt["templatetype_gid"].ToString(),
                        template_content_edit = dt["template_content"].ToString()
                    });
                    values.templateeditlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaUpdatedTemplate(string user_gid, MdlSysMstTemplateEditlist values)
        {
            msSQL = " select template_name from adm_mst_ttemplate where template_name = '" + values.template_name_edit + "' ";
            
            objOdbcDataReader = objdbconn.GetDataReader(msSQL);

            msSQL = " update adm_mst_ttemplate SET " +
                    " template_name = '" + values.template_name_edit + "'," +
                    " templatetype_gid = '" + values.templatetype_gid_edit + "'," +
                    " template_content = '" + values.template_content_edit + "'," +
                    " updated_by = '" + user_gid + "'," +
                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    " where template_gid = '" + values.template_gid_edit + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Template Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Template";
            }
        }
        public void DaDeleteTemplate(string template_gid, MdlSysMstTemplateEditlist values)
        {
            msSQL = " delete from adm_mst_ttemplate where template_gid='" + template_gid + "' ";
            
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Template Deleted Successfully";
            }
            else
            {
                    values.status = false;
                    values.message = "Error While Deleting Template";
            }
        }
    }
}