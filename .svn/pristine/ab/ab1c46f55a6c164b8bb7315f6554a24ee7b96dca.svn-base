using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.sales.Controllers
{
    [RoutePrefix("api/SmrRptSalesOrderDetailedReport")]
    [Authorize]
    public class SmrRptSalesOrderDetailedReportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrRptSalesOrderDetailedReport objsales = new DaSmrRptSalesOrderDetailedReport();

        [ActionName("GetSmrTrnSalesorderDetailedReportsummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnSalesorderDetailedReportsummary()
        {
            MdlSmrRptSalesOrderDetailedReport values = new MdlSmrRptSalesOrderDetailedReport();
            objsales.DaGetSmrTrnSalesorderDetailedReportsummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}