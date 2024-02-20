using System.Collections.Generic;

namespace ems.sales.Models
{
    public class MdlSmrMstCurrency : result
    {
        public List<Getsales_list> salescurrency_list { get; set; }
        public List<Getcountrydropdown> GetSmrCountryDtl { get; set; }
        public string currency_code { get; set; }
    }

    public class Getsales_list
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

    public class Getcountrydropdown
    {
        public string country_gid { get; set; }
        public string country_name { get; set; }

    }

    public class currencyDetails : result
    {
        public string currency_code { get; set; }
        public string exchange_rate { get; set; }
        public string country_name { get; set; }
    }

    public class currencyDetailsEdit : result
    {
        public string currency_codeedit { get; set; }
        public string exchange_rateedit { get; set; }
        public string country_nameedit { get; set; }
        public string currencyexchange_gid { get; set; }
    }

    public class currencyDetailsDelete : result
    {
        public string currencyexchange_gid { get; set; }
    }
}
