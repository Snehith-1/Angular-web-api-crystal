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
    [RoutePrefix("api/SmrRptQuotationReport")]
    public class SmrRptQuotationReportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrRptQuotationReport objDaSmrRptQuotationReport = new DaSmrRptQuotationReport();
        // GetQuotationForLastSixMonths

        [ActionName("GetQuotationReportForLastSixMonths")]
        [HttpGet]
        public HttpResponseMessage GetQuotationReportForLastSixMonths()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptQuotationReport values = new MdlSmrRptQuotationReport();
            objDaSmrRptQuotationReport.DaGetQuotationReportForLastSixMonths(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetQuotationForLastSixMonths

        [ActionName("GetQuotationReportForLastSixMonthsSearch")]
        [HttpGet]
        public HttpResponseMessage GetQuotationReportForLastSixMonthsSearch(string from_date,string to_date)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptQuotationReport values = new MdlSmrRptQuotationReport();
            objDaSmrRptQuotationReport.DaGetQuotationReportForLastSixMonthsSearch( values, from_date,to_date);
            return Request.CreateResponse(HttpStatusCode.OK, values);     
        }

        // GetQuotationSummary

        [ActionName("GetQuotationSummary")]
        [HttpGet]
        public HttpResponseMessage GetQuotationSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptQuotationReport values = new MdlSmrRptQuotationReport();
            objDaSmrRptQuotationReport.DaGetQuotationSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetQuotationDetailSummary

        [ActionName("GetQuotationDetailSummary")]
        [HttpGet]
        public HttpResponseMessage GetQuotationDetailSummary(string month, string year)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrRptQuotationReport values = new MdlSmrRptQuotationReport();
            objDaSmrRptQuotationReport.DaGetQuotationDetailSummary(getsessionvalues.employee_gid,month,year, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
