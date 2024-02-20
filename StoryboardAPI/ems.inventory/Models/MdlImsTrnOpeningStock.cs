using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.inventory.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class MdlImsTrnOpeningStock : result
    {
        public List<stockedit_list> stockedit_list { get; set; }
        public List<stock_list> stock_list { get; set; }
        public List<stockadd_list> stockadd_list { get; set; }
        public List<GetLocation> GetLocation { get; set; }
        public List<Postopeningstock> Postopeningstock { get; set; }

        public List<GetEditOpeningStock> GetEditOpeningStock { get; set; }
        public List<GetproductsCode> ProductsCode { get; set; }


    }

    public class GetproductsCode : result
    {
        public string product_gid { get; set; }
        public string product_code { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string product_name { get; set; }
        public string unitprice { get; set; }

    }
    public class stock_list : result
    {
        public string created_date { get; set; }
        public string branch_name { get; set; }
        public string location_name { get; set; }
        public string productgroup_name { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string productuom_name { get; set; }
        public string opening_stock { get; set; }

        public string stock_gid { get; set; }
        public string product_gid { get; set; }
        public string issued_qty { get; set; }

    }
    public class stockadd_list : result
    {
        public string created_date { get; set; }
        public string branch_name { get; set; }
        public string location_name { get; set; }
        public string productgroup_name { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string productuom_name { get; set; }
        public string opening_stock { get; set; }
        public string productgroup_gid { get; set; }
        public string product_desc { get; set; }
        public string productuom_gid { get; set; }
        public string product_gid { get; set; }
       


    }
    public class GetLocation : result
    {
        public string location_name { get; set; }
        public string location_gid { get; set; }
        public string branch_gid { get; set; }


    }
    public class Postopeningstock : result
    {
        public string location_name { get; set; }
        public string location_gid { get; set; }
        public string product_gid { get; set; }
        public string uom_gid { get; set; }
        public string branch_gid { get; set; }
        public string display_field { get; set; }
        public string stock_qty { get; set; }
        public string unit_price { get; set; }
       



    }
    public class GetEditOpeningStock : result
    {

        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string location_gid { get; set; }
        public string location_name { get; set; }
        public string product_desc { get; set; }
        public string product_gid { get; set; }
        public string productgroup_name { get; set; }

        public string productgroup_gid { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }
      
        public string cost_price { get; set; }
        public string opening_stock { get; set; }

        public string stock_gid { get; set; }
        public string product_status { get; set; }
    }


    public class stockedit_list : result
    {


        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string location_gid { get; set; }
        public string location_name { get; set; }
        public string product_desc { get; set; }
        public string product_gid { get; set; }
        public string productgroup_name { get; set; }

        public string productgroup_gid { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }

        public string cost_price { get; set; }
        public string opening_stock { get; set; }
        public string stock_qty { get; set; }
        public string stock_gid { get; set; }

        public string product_status { get; set; }
    }
}