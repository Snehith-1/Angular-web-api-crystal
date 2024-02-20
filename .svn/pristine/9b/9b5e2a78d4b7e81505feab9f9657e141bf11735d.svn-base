using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;


namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PayableDashboard")]
    [Authorize]
    public class PayableDashboardController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPayableDashboard objDaPayable = new DaPayableDashboard();


        [ActionName("GetPayablesummary")]
        [HttpGet]
        public HttpResponseMessage GetPayablesummary()
        {
            payable_tile values = new payable_tile();
            objDaPayable.DaGetPayablesummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Payabledashboardsummary")]
        [HttpGet]
        public HttpResponseMessage Payabledashboardsummary()
        {
            MdlPayableDashboard values = new MdlPayableDashboard();
            objDaPayable.DaPayabledashboardsummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}