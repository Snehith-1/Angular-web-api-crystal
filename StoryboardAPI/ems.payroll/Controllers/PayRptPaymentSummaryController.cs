using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.payroll.DataAccess;
using ems.payroll.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
namespace ems.payroll.Controllers
{

    [Authorize]
    [RoutePrefix("api/PayRptPaymentSummary")]
    public class PayRptPaymentSummaryController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPayRptPaymentSummary objDaPayRptPaymentSummary = new DaPayRptPaymentSummary();

        [ActionName("GetPaymentSummary")]
        [HttpGet]
        public HttpResponseMessage GetPaymentSummary()
        {
            MdlPayRptPaymentSummary values = new MdlPayRptPaymentSummary();
            objDaPayRptPaymentSummary.DaGetPaymentSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
 
 }

