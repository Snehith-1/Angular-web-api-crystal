using ems.hrm.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.hrm.Models;

namespace ems.hrm.Controllers
{

    [RoutePrefix("api/Probationperiod")]
    [Authorize]
    public class ProbationperiodController : ApiController

    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaProbationperiod objDaProbationperiod = new DaProbationperiod();

        [ActionName("GetProbationperiodSummary")]
        [HttpGet]
        public HttpResponseMessage GetProbationperiodSummary()
        {
            MdlProbationperiod values = new MdlProbationperiod();
            objDaProbationperiod.DaGetProbationperiodSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetProbationhistorySummary")]
        [HttpGet]
        public HttpResponseMessage GetProbationhistorySummary(string employee_gid)
        {
            MdlProbationperiod values = new MdlProbationperiod();
            objDaProbationperiod.DaGetProbationhistorySummary(values, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getleavegradedropdown")]
        [HttpGet]
        public HttpResponseMessage Getleavegradedropdown()
        {
            MdlProbationperiod values = new MdlProbationperiod();
            objDaProbationperiod.DaGetleavegradedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getjobtypedropdown")]
        [HttpGet]
        public HttpResponseMessage Getjobtypedropdown()
        {
            MdlProbationperiod values = new MdlProbationperiod();
            objDaProbationperiod.DaGetjobtypedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetleavegradeSummary")]
        [HttpGet]
        public HttpResponseMessage GetleavegradeSummary(string leavegrade_gid)
        {
            MdlProbationperiod values = new MdlProbationperiod();
            objDaProbationperiod.DaGetleavegradeSummary(values, leavegrade_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }




    }
}