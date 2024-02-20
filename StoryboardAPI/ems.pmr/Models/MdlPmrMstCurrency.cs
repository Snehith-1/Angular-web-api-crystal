
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.pmr.Models
{

  
    public class MdlPmrMstCurrency : result
    {
        public List<currency_list> currency_list { get; set; }
        public List<Getcountrydropdown> GetPmrCountryDtl { get; set; }
        public List<breadcrumb_list> breadcrumb_list { get; set; }



    }

    public class currency_list : result
    {
        public string currencyexchange_gid { get; set; }
        public string currency_code { get; set; }
        public string exchange_rate { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string country_name { get; set; }
        public string country_gid { get; set; }
        public string country_nameedit { get; set; }
        public string currency_codeedit { get; set; }
        public string exchange_rateedit { get; set; }
        

    }


    public class Getcountrydropdown : result
    {
        public string country_gid { get; set; }
        public string country_name { get; set; }

    }
    public class breadcrumb_list : result
    {

        public string module_name1 { get; set; }
        public string sref1 { get; set; }
        public string module_name2 { get; set; }
        public string sref2 { get; set; }
        public string module_name3 { get; set; }
        public string sref3 { get; set; }


    }



}