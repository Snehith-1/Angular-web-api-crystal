using ems.finance.DataAccess;
using ems.finance.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;


namespace ems.finance.Controllers
{

    [RoutePrefix("api/AccTrnCashBookSummary")]
    [Authorize]
    public class AccTrnCashBookSummaryController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAccTrnCashBookSummary objDafinance = new DaAccTrnCashBookSummary();
        // Module Summary
        [ActionName("GetAccTrnCashbooksummary")]
        [HttpGet]
        public HttpResponseMessage GetAccTrnCashbooksummary()
        {
            MdlAccTrnCashBookSummary values = new MdlAccTrnCashBookSummary();
            objDafinance.DaGetAccTrnCashbooksummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAccTrnCashbookSelect")]
        [HttpGet]
        public HttpResponseMessage GetAccTrnCashbookSelect(string branch_gid)
        {
            MdlAccTrnCashBookSummary values = new MdlAccTrnCashBookSummary();
            objDafinance.DaGetAccTrnCashbookSelect(values,branch_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}