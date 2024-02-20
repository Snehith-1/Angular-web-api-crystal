
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
        //GetExchangeRateAPICredential

        [ActionName("GetExchangeRateAPICredential")]
        [HttpGet]
        public HttpResponseMessage GetExchangeRateAPICredential()
        {
            MdlPmrDashboard values = new MdlPmrDashboard();
            objpurchase.DaGetExchangeRateAPICredential(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SmrSalesExchangeRateUpdate")]
        [HttpPost]
        public HttpResponseMessage SmrSalesExchangeRateUpdate(GetExchangeRateAPICredential_List values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaSmrSalesExchangeRateUpdate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //GetExchangeRateAPISummary

        [ActionName("GetExchangeRateAPISummary")]
        [HttpGet]
        public HttpResponseMessage GetExchangeRateAPISummary()
        {
            MdlPmrDashboard values = new MdlPmrDashboard();
            objpurchase.DaGetExchangeRateAPISummary(values);
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

        [ActionName("GetExchangeRateAsync")]
        [HttpGet]
        public HttpResponseMessage GetExchangeRateAsync()
        {
            MdlPmrDashboard values = new MdlPmrDashboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            values = objpurchase.DaGetExchangeRateAsync(getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}