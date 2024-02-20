
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.inventory.Models
{
    public class MdlImsTrnIssueMaterial:result
    {
        public List<Getissuematerial_list> Getissuematerial_list { get; set; }
    }
    public class Getissuematerial_list : result
    {
        public string productuom_gid { get; set; }
        public string materialissued_date { get; set; }
        public string materialissued_gid { get; set; }
        public string costcenter_name { get; set; }
        public string department_name { get; set; }
        public string materialrequisition_gid { get; set; }

        public string materialrequisition_reference { get; set; }
        public string user_firstname { get; set; }
        public string issued_to { get; set; }

    }
}