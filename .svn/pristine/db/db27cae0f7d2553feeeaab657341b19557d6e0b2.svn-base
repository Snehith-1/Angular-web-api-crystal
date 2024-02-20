using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.payroll.DataAccess;
using ems.payroll.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.payroll.Controllers
{
    [RoutePrefix("api/PayMstAssessmentSummary")]
    [Authorize]
    public class PayMstAssessmentSummaryController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPayMstAssessment objDaPayMstAssessment = new DaPayMstAssessment();

        [ActionName("Getassessmentsummary")]
        [HttpGet]
        public HttpResponseMessage Getassessmentsummary()
        {
            MdlPayMstAssessment values = new MdlPayMstAssessment();
            objDaPayMstAssessment.Daassessmentsummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getassessmentyear")]
        [HttpGet]
        public HttpResponseMessage Getassessmentyear(string assessment_gid)
        {
            MdlPayMstAssessment values = new MdlPayMstAssessment();
            objDaPayMstAssessment.DaGetassessmentyear(values, assessment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getassignempsummary")]
        [HttpGet]
        public HttpResponseMessage Getassignempsummary(string assessment_gid)
        {
            MdlPayMstAssessment values = new MdlPayMstAssessment();
            objDaPayMstAssessment.Daassignempsummary(assessment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Postassignemployee")]
        [HttpPost]
        public HttpResponseMessage Postassignemployee(postassignemployeelist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPayMstAssessment.DaPostassignemployee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getgenerateformsummary")]
        [HttpGet]
        public HttpResponseMessage Getgenerateformsummary(string assessment_gid)
        {
            MdlPayMstAssessment values = new MdlPayMstAssessment();
            objDaPayMstAssessment.Dagenerateformsummary(assessment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPersonaldata")]
        [HttpGet]
        public HttpResponseMessage GetPersonaldata(string employee_gid)
        {
            MdlPersonalData values = new MdlPersonalData();
            values = objDaPayMstAssessment.DaGetPersonaldata(employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePersonalInfo")]
        [HttpPost]
        public HttpResponseMessage UpdatePersonalInfo(updatepersonalinfolist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPayMstAssessment.DaUpdatePersonalInfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getfinyeardropdown")]
        [HttpGet]
        public HttpResponseMessage Getfinyeardropdown()
        {
            MdlPayMstAssessment values = new MdlPayMstAssessment();
            objDaPayMstAssessment.DaGetfinyeardropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostIncometax")]
        [HttpPost]
        public HttpResponseMessage PostIncometax(incometax_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPayMstAssessment.DaPostIncometax(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getincometaxsummary")]
        [HttpGet]
        public HttpResponseMessage Getincometaxsummary()
        {
            MdlPayMstAssessment values = new MdlPayMstAssessment();
            objDaPayMstAssessment.DaGetincometaxsummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostQuartersInfo")]
        [HttpPost]
        public HttpResponseMessage PostQuartersInfo(postquartersinfolist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPayMstAssessment.DaPostQuartersInfo(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetQuatersdata")]
        [HttpGet]
        public HttpResponseMessage GetQuatersdata(string employee_gid)
        {
            MdlQuartersData values = new MdlQuartersData();
            values = objDaPayMstAssessment.DaGetQuatersdata(employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIncomedata")]
        [HttpGet]
        public HttpResponseMessage GetIncomedata(string employee_gid, string assessment_gid)
        {
            MdlPayIncomedata values = new MdlPayIncomedata();
            values = objDaPayMstAssessment.DaGetIncomedata(employee_gid, assessment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostIncome")]
        [HttpPost]
        public HttpResponseMessage PostIncome(MdlPayIncomedata values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPayMstAssessment.DaPostIncome(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}