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
    [RoutePrefix("api/SmrRptTodaysSalesReport")]
    public class SmrRptTodaysSalesReportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrRptTodaysSalesReport objDaSmrRptTodaysSalesReport = new DaSmrRptTodaysSalesReport();

        // GetDaySalesReportCount

        [ActionName("GetDaySalesReportCount")]
        [HttpGet]
        public HttpResponseMessage GetDaySalesReportCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptTodaysSalesReport values = new MdlSmrRptTodaysSalesReport();
            objDaSmrRptTodaysSalesReport.DaGetDaySalesReportCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // GetWeekSalesReportCount

        [ActionName("GetWeekSalesReportCount")]
        [HttpGet]
        public HttpResponseMessage GetWeekSalesReportCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptTodaysSalesReport values = new MdlSmrRptTodaysSalesReport();
            objDaSmrRptTodaysSalesReport.DaGetWeekSalesReportCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // GetMonthSalesReportCount

        [ActionName("GetMonthSalesReportCount")]
        [HttpGet]
        public HttpResponseMessage GetMonthSalesReportCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptTodaysSalesReport values = new MdlSmrRptTodaysSalesReport();
            objDaSmrRptTodaysSalesReport.DaGetMonthSalesReportCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetYearSalesReportCount

        [ActionName("GetYearSalesReportCount")]
        [HttpGet]
        public HttpResponseMessage GetYearSalesReportCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptTodaysSalesReport values = new MdlSmrRptTodaysSalesReport();
            objDaSmrRptTodaysSalesReport.DaGetYearSalesReportCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetTodaySalesReport

        [ActionName("GetTodaySalesReport")]
        [HttpGet]
        public HttpResponseMessage GetTodaySalesReport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptTodaysSalesReport values = new MdlSmrRptTodaysSalesReport();
            objDaSmrRptTodaysSalesReport.DaGetTodaySalesReport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetTodayDeliveryOrderReport

        [ActionName("GetTodayDeliveryOrderReport")]
        [HttpGet]
        public HttpResponseMessage GetTodayDeliveryOrderReport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptTodaysSalesReport values = new MdlSmrRptTodaysSalesReport();
            objDaSmrRptTodaysSalesReport.DaGetTodayDeliveryOrderReport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetTodayInvoiceReport

        [ActionName("GetTodayInvoiceReport")]
        [HttpGet]
        public HttpResponseMessage GetTodayInvoiceReport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptTodaysSalesReport values = new MdlSmrRptTodaysSalesReport();
            objDaSmrRptTodaysSalesReport.DaGetTodayInvoiceReport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // GetTodayPaymentReport

        [ActionName("GetTodayPaymentReport")]
        [HttpGet]
        public HttpResponseMessage GetTodayPaymentReport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptTodaysSalesReport values = new MdlSmrRptTodaysSalesReport();
            objDaSmrRptTodaysSalesReport.DaGetTodayPaymentReport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}