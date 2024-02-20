﻿using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.hrm.DataAccess;
using ems.hrm.Models;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.hrm.Controllers
{
    [RoutePrefix("api/hrmTrnDashboard")]
    [Authorize]
    public class HrmTrnDashboardController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaHrmTrnDashboard objDaHrmDashboard = new DaHrmTrnDashboard();

        [ActionName("GetCompanyPolicies")]
        [HttpGet]
        public HttpResponseMessage GetCompanyPolicies()
        {
            mdlcompanypolicies values = new mdlcompanypolicies();
            objDaHrmDashboard.DaGetCompanyPolicies(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("punchIn")]
        [HttpPost]
        public HttpResponseMessage PostIAttendanceLogin(mdliAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostIAttendanceLogin(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("punchOut")]
        [HttpPost]
        public HttpResponseMessage PostIAttendanceLogout(mdliAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostIAttendanceLogout(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("iattendence")]
        [HttpGet]
        public HttpResponseMessage iAttendancepunchout()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdliAttendance objiAttendance = new mdliAttendance();
            objDaHrmDashboard.DaIAttendencePunchOut(getsessionvalues.employee_gid, objiAttendance);
            return Request.CreateResponse(HttpStatusCode.OK, objiAttendance);
        }

        [ActionName("applyLoginReq")]
        [HttpPost]
        public HttpResponseMessage PostLoginReq(mdlloginreq values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostAttendanceLogin(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("applyLogoutReq")]
        [HttpPost]
        public HttpResponseMessage PostLogoutReq(mdllogoutreq values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostAttendanceLogout(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("applyonduty")]
        [HttpPost]
        public HttpResponseMessage PostApplyOnduty(applyondutydetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostApplyOnduty(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("applyPermission")]
        [HttpPost]
        public HttpResponseMessage PostApplyPermission(permission_details values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostApplyPermission(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("applyCompoffReq")]
        [HttpPost]
        public HttpResponseMessage PostCompoffReq(mdlcompffreq values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostCompoff(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("monthlyAttendence")]
        [HttpGet]
        public HttpResponseMessage GetMonthlyAttendence(string user_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            monthlyAttendence objmonthlyAttendence = new monthlyAttendence();
            objDaHrmDashboard.DaGetMonthlyAttendence(objmonthlyAttendence, user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmonthlyAttendence);
        }
        [ActionName("todayactivity")]
        [HttpGet]
        public HttpResponseMessage GetTodayActivity()
        {
            eventdetail objresult = new eventdetail();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);


            objDaHrmDashboard.DaGettodayactivity(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("event")]
        [HttpPost]
        public HttpResponseMessage GetEvent(eventdetail objresult)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaGetEvent(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("holidaycalender")]
        [HttpGet]
        public HttpResponseMessage GetHolidayCalender()
        {
            holidaycalender objresult = new holidaycalender();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaHrmDashboard.DaGetHoliday(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("loginSummary")]
        [HttpGet]
        public HttpResponseMessage loginSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlloginsummary objloginsummary = new mdlloginsummary();
            objDaHrmDashboard.DaGetLoginSummary(getsessionvalues.employee_gid, objloginsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objloginsummary);
        }

        [ActionName("logoutSummary")]
        [HttpGet]
        public HttpResponseMessage LogoutSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdllogoutsummary objlogoutsummary = new mdllogoutsummary();
            objDaHrmDashboard.DaGetLogoutSummary(getsessionvalues.employee_gid, objlogoutsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objlogoutsummary);
        }

        [ActionName("ondutySummary")]
        [HttpGet]
        public HttpResponseMessage GetOnDutySummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            onduty_detail_list objonduty_details = new onduty_detail_list();
            objDaHrmDashboard.DaGetOnDutySummary(getsessionvalues.employee_gid, objonduty_details);
            return Request.CreateResponse(HttpStatusCode.OK, objonduty_details);
        }

        [ActionName("compOffSummary")]
        [HttpGet]
        public HttpResponseMessage GetCompOffSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            compoff_list objcompoff_details = new compoff_list();
            objDaHrmDashboard.DaGetCompOffSummary(getsessionvalues.employee_gid, objcompoff_details);
            return Request.CreateResponse(HttpStatusCode.OK, objcompoff_details);
        }

        [ActionName("permissionSummary")]
        [HttpGet]
        public HttpResponseMessage GetPermissionSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            permission_details_list objpermission_details = new permission_details_list();
            objDaHrmDashboard.DaGetPermissionSummary(getsessionvalues.employee_gid, objpermission_details);
            return Request.CreateResponse(HttpStatusCode.OK, objpermission_details);
        }


        [ActionName("monthlyAttendenceReport")]
        [HttpGet]
        public HttpResponseMessage GetmonthlyAttendenceReport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            monthlyAttendenceReport objmonthlyAttendence = new monthlyAttendenceReport();
            objDaHrmDashboard.DamonthlyAttendenceReport(getsessionvalues.employee_gid, objmonthlyAttendence);
            return Request.CreateResponse(HttpStatusCode.OK, objmonthlyAttendence);
        }
    }
}