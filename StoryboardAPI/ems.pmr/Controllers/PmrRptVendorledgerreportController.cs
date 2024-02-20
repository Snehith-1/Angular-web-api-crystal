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
    [RoutePrefix("api/PmrRptVendorLedgerreport")]
    [Authorize]
    public class PmrRptVendorledgerreportController: ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrRptVendorledgerreport objpurchase = new DaPmrRptVendorledgerreport();

        [ActionName("GetVendorledgerReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetVendorledgerReportSummary()
        {
            MdlPmrRptVendorledgerreport values = new MdlPmrRptVendorledgerreport();
            objpurchase.DaGetVendorledgerReportSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
    }

}