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
    [RoutePrefix("api/LeaveGrade")]
    public class LeaveGradeController :ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLeaveGrade objdashift = new DaLeaveGrade();

        [ActionName("LeaveGradeSummary")]
        [HttpGet]
        public HttpResponseMessage LeaveGradeSummary()
        {
            MdlLeaveGrade values = new MdlLeaveGrade();
            objdashift.DaLeaveGradeSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getleavegradecodesummary")]
        [HttpGet]
        public HttpResponseMessage Getleavegradecodesummary()
        {
            leavegradesubmit_list values = new leavegradesubmit_list();
            objdashift.DaGetleavegradecodesummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LeaveGradeSubmit")]
        [HttpPost]
        public HttpResponseMessage LeaveGradeSubmit(leavegradesubmit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdashift.DaLeaveGradeSubmit( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}