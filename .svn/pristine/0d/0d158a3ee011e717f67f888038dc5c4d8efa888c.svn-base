using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.hrm.Models;
using ems.hrm.DataAccess;
using ems.utilities.Functions;

namespace ems.hrm.Controllers
{
    [RoutePrefix("api/MonthlyAttendanceReport")]
    [Authorize]
    public class MonthlyAttendanceReportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMonthlyAttendanceReport objmonthlyreport = new DaMonthlyAttendanceReport();

        [ActionName("GetMonthlyReportBranch")]
        [HttpGet]
        public HttpResponseMessage GetMonthlyReportBranch()
        {
            MdlMonthlyAttendanceReport values = new MdlMonthlyAttendanceReport();
            objmonthlyreport.DaGetMonthlyReportBranch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMonthlyReportSummaryDetails")]
        [HttpGet]
        public HttpResponseMessage GetMonthlyReportSummaryDetails( string year, string month)
        {
            MdlMonthlyAttendanceReport values = new MdlMonthlyAttendanceReport();
            objmonthlyreport.DaGetMonthlyReportSummaryDetails( year, month,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}   