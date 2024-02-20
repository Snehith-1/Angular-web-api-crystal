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
    [RoutePrefix("api/PmrRptOverallReport")]
    [Authorize]
    public class PmrRptOverallReportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrRptOverallReport objpurchase = new DaPmrRptOverallReport();


        [ActionName("GetOverallReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetOverallReportSummary()
        {
            MdlPmrRptOverallReport values = new MdlPmrRptOverallReport();
            objpurchase.DaGetOverallReportSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
    }
}