using ems.hrm.DataAccess;
using ems.hrm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.hrm.Controllers
{
    [Authorize]
    [RoutePrefix("api/LeaveTypeGrade")]
    public class LeaveTypeGradeController :ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLeaveTypeGrade objdashift = new DaLeaveTypeGrade();
        [ActionName("LeavetypeSummary")]
        [HttpGet]
        public HttpResponseMessage LeavetypeSummary()
        {
            MdlLeaveTypeGrade values = new MdlLeaveTypeGrade();
            objdashift.DaLeavetypeSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAddleave")]
        [HttpPost]
        public HttpResponseMessage PostAddleave( string user_gid,Addleave_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objdashift.PostAddleave(user_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}