using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrRptOutstandingAmountReport")]
    [Authorize]
    public class PmrRptOutstandingAmountReportController :ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrRptOutstandingAmountReport objpurchase = new DaPmrRptOutstandingAmountReport();

        [ActionName("GetOutstandingAmountReportSummary")]
        [HttpGet]

        public HttpResponseMessage GetOutstandingAmountReportSummary()
        {
            MdlPmrRptOutstandingAmountReport values = new MdlPmrRptOutstandingAmountReport();
            objpurchase.DaGetOutstandingAmountReportSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
    }
}