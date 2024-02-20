using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.IO;
using System.Configuration;

namespace ems.crm.Controllers
{
    [Authorize]
    [RoutePrefix("api/MarketingReport")]
    public class MarketingReportController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMarketingReport objdaMarkeingLog = new DaMarketingReport();

        [ActionName("GetReportLogSummary")]
        [HttpGet]
        public HttpResponseMessage GetReportLogSummary()
        {
            MdlMarketingReport values = new MdlMarketingReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingLog.DaGetReportLogSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEnquiryChartReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetEnquiryChartReportSummary()
        {
            MdlMarketingReport values = new MdlMarketingReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingLog.DaGetEnquiryChartReportSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEnquiryReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetEnquiryReportSummary()
        {
            MdlMarketingReport values = new MdlMarketingReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingLog.DaGetEnquiryReportSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEnquirymainReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetEnquirymainReportSummary()
        {
            MdlMarketingReport values = new MdlMarketingReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingLog.DaGetEnquirymainReportSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEnquirysubReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetEnquirysubReportSummary(string Month,string year)
        {
            MdlMarketingReport values = new MdlMarketingReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingLog.DaGetEnquirysubReportSummary(getsessionvalues.employee_gid, Month, year, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSelectedEnquiryReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetSelectedEnquiryReportSummary(string from_date,string to_date)
        {
            MdlMarketingReport values = new MdlMarketingReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingLog.DaGetSelectedEnquiryReportSummary(getsessionvalues.employee_gid, from_date, to_date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSelectedEnquirymainReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetSelectedEnquirymainReportSummary(string from_date, string to_date)
        {
            MdlMarketingReport values = new MdlMarketingReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingLog.DaGetSelectedEnquirymainReportSummary(getsessionvalues.employee_gid, from_date, to_date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSelectEnquiryChartReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetSelectEnquiryChartReportSummary(string from_date, string to_date)
        {
            MdlMarketingReport values = new MdlMarketingReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingLog.DaGetSelectEnquiryChartReportSummary(getsessionvalues.employee_gid, from_date, to_date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCustomerToLeadChartReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerToLeadChartReportSummary()
        {
            MdlMarketingReport values = new MdlMarketingReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingLog.DaGetCustomerToLeadChartReportSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCustomerToLeadChartReportsearchSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerToLeadChartReportsearchSummary(string from_date, string to_date)
        {
            MdlMarketingReport values = new MdlMarketingReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingLog.DaGetCustomerToLeadChartReportsearchSummary(getsessionvalues.employee_gid, from_date, to_date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
