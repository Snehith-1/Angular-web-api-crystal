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
    [RoutePrefix("api/PayTrnReportPayment")]
    public class PayTrnReportPaymentController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPayTrnReportPayment objDaRptReportPayment = new DaPayTrnReportPayment();


        [ActionName("GetPaymentSummary")]   
        [HttpGet]
        public HttpResponseMessage GetPaymentSummary()
        {
            MdlPayTrnReportPayment values = new MdlPayTrnReportPayment();
            objDaRptReportPayment.DaPaymentSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetreportPaymentExpand")]
        [HttpGet]
        public HttpResponseMessage GetreportPaymentExpand(string month, string year)
        {
            MdlPayTrnReportPayment values = new MdlPayTrnReportPayment();
            objDaRptReportPayment.DareportPaymentExpand(month, year, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("GetPaymentmodeExpand")]
        [HttpGet]
        public HttpResponseMessage GetPaymentmodeExpand()
        {
            MdlPayTrnReportPayment values = new MdlPayTrnReportPayment();
            objDaRptReportPayment.DaGetPaymentmodeExpand(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        public HttpResponseMessage GetReportExportExcel()
        {
            MdlPayTrnReportPayment values = new MdlPayTrnReportPayment();
            objDaRptReportPayment.DaGetReportExportExcel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}