using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.sales.Controllers
{
    [Authorize]
    [RoutePrefix("api/SmrCommissionManagement")]
    public class SmrCommissionManagementController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrCommissionManagement objCommissionManagement = new DaSmrCommissionManagement();

        // GetCommissionSettingSummary

        [ActionName("GetCommissionSettingSummary")]
        [HttpGet]
        public HttpResponseMessage GetCommissionSettingSummary()
        {
            MdlSmrCommissionManagement values = new MdlSmrCommissionManagement();
            objCommissionManagement.DaGetCommissionSettingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInvoiceSummary")]
        [HttpGet]
        public HttpResponseMessage einvoiceSummary()
        {
            MdlSmrCommissionManagement values = new MdlSmrCommissionManagement();
            objCommissionManagement.DaGetInvoiceSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Post SetPercentage
        [ActionName("PostSetPercentage")]
        [HttpPost]
        public HttpResponseMessage PostSetPercentage(MdlSmrCommissionManagement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objCommissionManagement.DaPostSetPercentage(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Post Commission
        [ActionName("PostCommission")]
        [HttpPost]
        public HttpResponseMessage PostCommission(MdlSmrCommissionManagement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objCommissionManagement.DaPostCommission(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCommissionPayoutSummary")]
        [HttpGet]
        public HttpResponseMessage GetCommissionPayoutSummary()
        {
            MdlSmrCommissionManagement values = new MdlSmrCommissionManagement();
            objCommissionManagement.DaGetCommissionPayoutSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCommissionPayoutReport")]
        [HttpGet]
        public HttpResponseMessage GetCommissionPayoutReport()
        {
            MdlSmrCommissionManagement values = new MdlSmrCommissionManagement();
            objCommissionManagement.DaGetCommissionPayoutReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCommissionPayoutReportDetails")]
        [HttpGet]
        public HttpResponseMessage GetCommissionPayoutReportDetails(string campaign_gid)
        {
            MdlSmrCommissionManagement values = new MdlSmrCommissionManagement();
            objCommissionManagement.DaGetCommissionPayoutReportDetails(values, campaign_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetCommissionEmpwisePayoutReport")]
        [HttpGet]
        public HttpResponseMessage GetCommissionEmpwisePayoutReport(string user_gid)
        {
            MdlSmrCommissionManagement values = new MdlSmrCommissionManagement();
            objCommissionManagement.DaGetCommissionEmpwisePayoutReport(values,user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}