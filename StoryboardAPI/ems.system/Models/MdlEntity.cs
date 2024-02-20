using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.system.Models
{
    public class MdlEntity : result
    {
        public List<entity_lists> entity_lists { get; set; }
    }

    public class entity_lists : result
    {
        public string entity_gid { get; set; }
        public string params_gid { get; set; }
        public string entity_name { get; set; }
        public string entity_description { get; set; }
        public string entity_code { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string entityedit_name { get; set; }
        public string entityedit_description { get; set; }

    }
}