using ems.sales.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.sales.Models;

namespace ems.sales.Controllers
{
    [RoutePrefix("api/SmrRptCustomerledgerreport")]
    [Authorize]
    public class SmrRptCustomerledgerreportController: ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrRptCustomerledgerreport objpurchase = new DaSmrRptCustomerledgerreport();

        [ActionName("GetCustomerledgerreportSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerledgerreportSummary()
        {
            MdlSmrRptCustomerledgerreport values = new MdlSmrRptCustomerledgerreport();
            objpurchase.DaGetCustomerledgerreportSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
    }

}