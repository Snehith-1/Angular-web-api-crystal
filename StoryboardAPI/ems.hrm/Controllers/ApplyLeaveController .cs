﻿using ems.hrm.DataAccess;
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
    [RoutePrefix("api/applyLeave")]
    [Authorize]
    public class ApplyLeaveController :ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaApplyLeave objDaApplyLeave = new DaApplyLeave();

        [ActionName("leavetype")]
        [HttpGet]

        public HttpResponseMessage getleavetype()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            leavecountdetails objleavecountdetails = new leavecountdetails();
            var status = objDaApplyLeave.DaGetLeaveType(getsessionvalues.employee_gid, getsessionvalues.user_gid, objleavecountdetails);
            return Request.CreateResponse(HttpStatusCode.OK, objleavecountdetails);
        }

        //[ActionName("getleavetype_name")]
        //[HttpGet]
        //public HttpResponseMessage getemployeecontactdetails(string leavetype_gid)
        //{
        //    applyleavedetails values = new applyleavedetails();
        //    var status = objDaApplyLeave.DaGetLeaveTypeName(leavetype_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("getleavetype_name")]
        [HttpGet]
        public HttpResponseMessage GetLeaveTypeName()
        {
            Mdlapplyleave values = new Mdlapplyleave();
            objDaApplyLeave.DaGetLeaveTypeName(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("leavesummary")]
        [HttpGet]
        public HttpResponseMessage applyleavesummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            applyleavegetleavedetails values = new applyleavegetleavedetails();
            var status = objDaApplyLeave.DaGetApplyLeaveSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("leavePendingDelete")]
        [HttpGet]

        public HttpResponseMessage permissionPendingDelete(string leavetype_gid)
        {
            applyleavegetleavedetails objleavedetails = new applyleavegetleavedetails();
            var status = objDaApplyLeave.DaPostLeavePendingDelete(leavetype_gid, objleavedetails);
            return Request.CreateResponse(HttpStatusCode.OK, objleavedetails);
        }

        // Apply Leave .......//

        [ActionName("applyleavesubmit")]
        [HttpPost]
        public HttpResponseMessage postapplyleave(applyleavedetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objDaApplyLeave.DaPostApplyLeave(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Month Wise Leave Report....//

        [ActionName("leavereport")]
        [HttpGet]
        public HttpResponseMessage leavereport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            monthwise_leavereport values = new monthwise_leavereport();
            var status = objDaApplyLeave.getleavereport_da(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("approveleavesummary")]
        [HttpGet]

        public HttpResponseMessage approveleavesummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            applyleavegetleavedetails values = new applyleavegetleavedetails();
            var status = objDaApplyLeave.DaGetApproveLeaveSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("uploaddocument")]
        [HttpPost]
        public HttpResponseMessage uploaddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            applyleaveuploaddocument documentname = new applyleaveuploaddocument();
            var status = objDaApplyLeave.DaPostUploadDocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("documentDelete")]
        [HttpGet]
        public HttpResponseMessage deletevertical(string tmpdocument_gid)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            applyleaveuploaddocument values = new applyleaveuploaddocument();
            var status = objDaApplyLeave.DaGetDeleteDocument(tmpdocument_gid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("leavevalidate")]
        [HttpPost]
        public HttpResponseMessage leavevalidate(mdlleavevalidate values)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            var status = objDaApplyLeave.Daleavevalidate(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
     
    }
}
