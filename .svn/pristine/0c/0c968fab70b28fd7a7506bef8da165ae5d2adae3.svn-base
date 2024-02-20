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
    [RoutePrefix("api/SmrRptOrderReport")]
    public class SmrRptEnquiryReportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrRptEnquiryReport objDaSmrRptOrderReport = new DaSmrRptEnquiryReport();

        // GetEnquiryForLastSixMonths

        [ActionName("GetEnquiryForLastSixMonths")]
        [HttpGet]
        public HttpResponseMessage GetChildLoop ()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptEnquiryReport values = new MdlSmrRptEnquiryReport();
            objDaSmrRptOrderReport.DaGetEnquiryForLastSixMonths(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEnquiryForLastSixMonthsSearch")]
        [HttpGet]
        public HttpResponseMessage GetEnquiryForLastSixMonthsSearch(string from_date, string to_date)
        {
            //string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            //getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptEnquiryReport values = new MdlSmrRptEnquiryReport();
            objDaSmrRptOrderReport.DaGetEnquiryForLastSixMonthsSearch(values, from_date, to_date);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetEnquirySummary

        [ActionName("GetEnquirySummary")]
        [HttpGet]
        public HttpResponseMessage GetEnquirySummary(string salesorder_gid )
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptEnquiryReport values = new MdlSmrRptEnquiryReport();
            objDaSmrRptOrderReport.DaGetEnquirySummary(getsessionvalues.employee_gid, salesorder_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetEnquiryDetailSummary

        [ActionName("GetEnquiryDetailSummary")]
        [HttpGet]
        public HttpResponseMessage GetEnquiryDetailSummary(string month,string year)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptEnquiryReport values = new MdlSmrRptEnquiryReport();
            objDaSmrRptOrderReport.DaGetEnquiryDetailSummary(getsessionvalues.employee_gid,month, year, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetMonthwiseOrderReport

        [ActionName("GetMonthwiseOrderReport")]
        [HttpGet]
        public HttpResponseMessage GetMonthwiseOrderReport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptEnquiryReport values = new MdlSmrRptEnquiryReport();
            objDaSmrRptOrderReport.DaGetMonthwiseOrderReport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

  
    }
}