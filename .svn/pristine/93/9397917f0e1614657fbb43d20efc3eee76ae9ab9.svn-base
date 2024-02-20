using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.system.DataAccess;
using ems.system.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.system.Controllers
{
    [Authorize]
    [RoutePrefix("api/SystemRptEmployeeReport")]
    public class SystemRptEmployeeReportController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSystemRptEmployeeReport objdaemployeereportlist = new DaSystemRptEmployeeReport();

        [ActionName("GetEmployeeReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeReportSummary(string branch_gid)
        {
            MdlSystemRptEmployeeReport values = new MdlSystemRptEmployeeReport();
            objdaemployeereportlist.DaGetEmployeeReportSummary(branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBranchDtl")]
        [HttpGet]
        public HttpResponseMessage GetBranchDtl()
        {
            MdlSystemRptEmployeeReport values = new MdlSystemRptEmployeeReport();
            objdaemployeereportlist.DaGetBranchDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetReportExportExcel")]
        [HttpGet]
        public HttpResponseMessage GetReportExportExcel()
        {
            MdlSystemRptEmployeeReport values = new MdlSystemRptEmployeeReport();
            objdaemployeereportlist.DaGetReportExportExcel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}