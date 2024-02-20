using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrm.Models
{
    public class MdlBiometric : biometricsummary
    {
        public List<biometricsummary> biometricsummary { get; set; }
        public List<popupupdate> popupupdate { get; set; }
    }
    public class biometricsummary : result
    {
        public string branch { get; set; }
        public string department { get; set; }
        public string employee_code { get; set; }
        public string employee_name { get; set; }
        public string designation_name { get; set; }
        public string biometric_id { get; set; }
        public string nfc_id { get; set; }
        public string user_status { get; set; }
        public string employee_gid { get; set; }
     
    }

    public class popupupdate: result
    {
        public string nfc_cardno { get; set; }
        public string biometric_gid { get; set; }
        public string biometric_register { get; set; }
        public string employee_gid { get; set; }
        public string biometric_id { get; set; }
        public string biometric_status { get; set; }
    }

}
