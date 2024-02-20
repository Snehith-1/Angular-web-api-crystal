
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
using StoryboardAPI.Models;

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrDashboard")]
    [Authorize]
    public class PmrDashboardController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrDashboard objpurchase = new DaPmrDashboard();

        //GetPurchaseLiabilityReportChart

        [ActionName("GetPurchaseLiabilityReportChart")]
        [HttpGet]
        public HttpResponseMessage GetPurchaseLiabilityReportChart()
        {
            MdlPmrDashboard values = new MdlPmrDashboard();
            objpurchase.DaGetPurchaseLiabilityReportChart(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //GetPurchaseCount

        [ActionName("GetPurchaseCount")]
        [HttpGet]
        public HttpResponseMessage GetPurchaseCount()
        {
            MdlPmrDashboard values = new MdlPmrDashboard();
            objpurchase.DaGetPurchaseCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //GetInvoiceCount

        [ActionName("GetInvoiceCount")]
        [HttpGet]
        public HttpResponseMessage GetInvoiceCount()
        {
            MdlPmrDashboard values = new MdlPmrDashboard();
            objpurchase.DaGetInvoiceCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //GetPaymentCount

        [ActionName("GetPaymentCount")]
        [HttpGet]
        public HttpResponseMessage GetPaymentCount()
        {
            MdlPmrDashboard values = new MdlPmrDashboard();
            objpurchase.DaGetPaymentCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}