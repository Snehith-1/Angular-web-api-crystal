using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.sales.Models
{
    public class MdlSmrMstSalesConfig : result
    {
        public List<salesconfiglist> salesconfiglist { get; set; }
        public List<salesconfigalllist> salesconfigalllist { get; set; }
    }
    public class salesconfigalllist : result
    {
        public string id { get; set; }
        public string charges { get; set; }
        public string flag { get; set; }
    }
    public class salesconfiglist : result
    {
        public Boolean addoncharges { get; set; }
        public Boolean additionaldiscount { get; set; }
        public Boolean freightcharges { get; set; }
        public Boolean packing_forwardingcharges { get; set; }
        public Boolean insurancecharges { get; set; }
    }
}