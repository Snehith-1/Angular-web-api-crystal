using System.Collections.Generic;

namespace ems.system.Models
{
    public class MdlSysMstTemplate
    {
        public List<MdlSysMstTemplateSummarylist> templatesummarylist { get; set; }
        public List<GetTemplateTypedropdown> GetTemplateTypedropdown {  get; set; }
        public List<MdlSysMstTemplatelist> MdlSysMstTemplatelist { get; set; }
        public List<MdlSysMstTemplateEditlist> templateeditlist { get; set; }
    }
    public class MdlSysMstTemplateSummarylist : result
    {
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string templatetype_name { get; set; }
        public string created_by { get; set; }
    }        
    public class GetTemplateTypedropdown : result
    {
        public string templatetype_gid { get; set; }
        public string templatetype_name { get; set; }
    }
    public class MdlSysMstTemplatelist : result
    {
        public string template_name { get; set; }
        public string templatetype_gid { get; set; }
        public string template_content { get; set; }
    }
    public class MdlSysMstTemplateEditlist : result
    {
        public string template_gid_edit { get; set; }
        public string template_name_edit { get; set; }
        public string templatetype_gid_edit { get; set; }
        public string template_content_edit { get; set; }
    }
}