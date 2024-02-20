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

        [ActionName("AssignEmployeeSummary")]
        [HttpGet]
        public HttpResponseMessage AssignEmployeeSummary()
        {
            MdlLeaveGrade values = new MdlLeaveGrade();
            objdashift.DaAssignEmployeeSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Leavegradeassign")]
        [HttpGet]
        public HttpResponseMessage Leavegradeassign(string leavegrade_gid)
        {
            MdlLeaveGrade objresult = new MdlLeaveGrade();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdashift.DaLeavegradeassign(objresult, leavegrade_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Leavegradeunassign")]
        [HttpGet]
        public HttpResponseMessage Leavegradeunassign(string leavegrade_gid)
        {
            MdlLeaveGrade objresult = new MdlLeaveGrade();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdashift.DaLeavegradeunassign(objresult, leavegrade_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Postforunassign")]
        [HttpPost]
        public HttpResponseMessage Postforunassign(assignsubmit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objdashift.DaPostforunassign(values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UnassignEmployeeSummary")]
        [HttpGet]
        public HttpResponseMessage UnassignEmployeeSummary(string leavegrade_gid)
        {
            MdlLeaveGrade values = new MdlLeaveGrade();
            objdashift.DaUnassignEmployeeSummary(leavegrade_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteUnassignemployee")]
        [HttpPost]
        public HttpResponseMessage DeleteUnassignemployee(unassignsubmit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objdashift.DaDeleteUnassignemployee(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values) ;
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