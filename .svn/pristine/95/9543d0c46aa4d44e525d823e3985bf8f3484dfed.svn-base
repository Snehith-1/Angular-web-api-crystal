using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ems.hrm.DataAccess;
using ems.hrm.Models;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Net.Http;

namespace ems.hrm.Models
{
    [Authorize]
    [RoutePrefix("api/EmployeeOnboard")]
    public class EmployeeOnboardController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaEmployeeOnboard objDaManageEmployee = new DaEmployeeOnboard();

        [ActionName("EmployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeSummary()
        {
            MdlEmployeeOnboard objemployee_list = new MdlEmployeeOnboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeSummary(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }
        // Active Users
        [ActionName("EmployeeActiveSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeActiveSummary()
        {
            MdlEmployeeOnboard objemployee_list = new MdlEmployeeOnboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeActiveSummary(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        // Pending Users
        [ActionName("EmployeePendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeePendingSummary()
        {
            MdlEmployeeOnboard objemployee_list = new MdlEmployeeOnboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeePendingSummary(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        // Relieving Users
        [ActionName("EmployeeRelievedSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeRelievedSummary()
        {
           MdlEmployeeOnboard objemployee_list = new MdlEmployeeOnboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeRelievedSummary(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        // Inactive Users
        [ActionName("EmployeeInactiveSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeInactiveSummary()
        {
           MdlEmployeeOnboard objemployee_list = new MdlEmployeeOnboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeInactiveSummary(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        [ActionName("PopEntity")]
        [HttpGet]
        public HttpResponseMessage GetPopEntity()
        {
            entity_list objentity_list = new entity_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopEntity(objentity_list);
            return Request.CreateResponse(HttpStatusCode.OK, objentity_list);
        }

        [ActionName("PopBranch")]
        [HttpGet]
        public HttpResponseMessage GetPopBranch()
        {
           MdlEmployeeOnboard objemployee_list = new MdlEmployeeOnboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopBranch(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        [ActionName("PopDepartment")]
        [HttpGet]
        public HttpResponseMessage GetPopDepartment()
        {
           MdlEmployeeOnboard objemployee_list = new MdlEmployeeOnboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopDepartment(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        [ActionName("PopSubfunction")]
        [HttpGet]
        public HttpResponseMessage GetPopClientRole()
        {
           MdlEmployeeOnboard objemployee_list = new MdlEmployeeOnboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopSubfunction(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        [ActionName("PopDesignation")]
        [HttpGet]
        public HttpResponseMessage GetPopDesignation()
        {
           MdlEmployeeOnboard objemployee_list = new MdlEmployeeOnboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopDesignation(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        [ActionName("PopCountry")]
        [HttpGet]
        public HttpResponseMessage GetPopCountry()
        {
            country_list objcountry_list = new country_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopCountry(objcountry_list);
            return Request.CreateResponse(HttpStatusCode.OK, objcountry_list);
        }

        [ActionName("PopRole")]
        [HttpGet]
        public HttpResponseMessage GetPopRole()
        {
            role_list objrolemaster = new role_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopRole(objrolemaster);
            return Request.CreateResponse(HttpStatusCode.OK, objrolemaster);
        }

        [ActionName("PopReportingTo")]
        [HttpGet]
        public HttpResponseMessage GetPopReportingTo()
        {
            reportingto_list objreportingto = new reportingto_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopReportingTo(objreportingto);
            return Request.CreateResponse(HttpStatusCode.OK, objreportingto);
        }

        [ActionName("EmployeeEditView")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeEditView(string employee_gid)
        {
            MdlEmployeeOnboard objemployee = new MdlEmployeeOnboard();
            objDaManageEmployee.DaEmployeeEditView(objemployee, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }

        // Pending View
        [ActionName("EmployeePendingEditView")]
        [HttpGet]
        public HttpResponseMessage GetEmployeePendingEditView(string employee_gid)
        {
            MdlEmployeeOnboard objemployee = new MdlEmployeeOnboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeePendingEditView(objemployee, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }
        //ProfileView


        [ActionName("EmployeeProfileView")]
        [HttpGet]
        public HttpResponseMessage EmployeeProfileView()
        {
            MdlEmployeeOnboard objemployee = new MdlEmployeeOnboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeProfileView(objemployee, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }

        [ActionName("GetHRDocProfilelist")]
        [HttpGet]

        //HRDocProfilelist
        public HttpResponseMessage GetHRDocProfilelist()
        {
            hrdoc_list objemployeedoc_list = new hrdoc_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaGetHRDocProfilelist(getsessionvalues.employee_gid, objemployeedoc_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployeedoc_list);
        }

        //Pending Update
        [ActionName("EmployeePendingUpdate")]
        [HttpPost]
        public HttpResponseMessage PostEmployeePendingUpdate(employee objemployee)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeePendingUpdate(objemployee, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }

        [ActionName("EmployeeUpdate")]
        [HttpPost]
        public HttpResponseMessage PostEmployeeUpdate(employee objemployee)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeUpdate(objemployee, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }


        [ActionName("EmployeeAdd")]
        [HttpPost]
        public HttpResponseMessage EmployeeAdd(employee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeAdd(values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskList")]
        [HttpGet]
        public HttpResponseMessage GetTaskList()
        {
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetTaskList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyTaskCount")]
        [HttpGet]
        public HttpResponseMessage GetMyTaskCount()
        {
            countlist values = new countlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaGetMyTaskCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTeamList")]
        [HttpGet]
        public HttpResponseMessage GetTeamList()
        {
            MdlTeamList values = new MdlTeamList();
            objDaManageEmployee.DaGetTeamList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskList")]
        [HttpGet]
        public HttpResponseMessage GetTaskList(string employee_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetTaskList(employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HRDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage HRDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            HRuploaddocument documentname = new HRuploaddocument();
            objDaManageEmployee.DaPostHRDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetMyTaskCompleteSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyTaskCompleteSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetMyTaskCompleteSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyTaskPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyTaskPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetMyTaskPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskManagementPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaskManagementPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetTaskManagementPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskManagementNewSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaskManagementNewSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetTaskManagementNewSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskManagementCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaskManagementCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetTaskManagementCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetTaskManagementCount")]
        [HttpGet]
        public HttpResponseMessage GetTaskManagementCount()
        {
            countlist values = new countlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaGetTaskManagementCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskOnboardView")]
        [HttpGet]
        public HttpResponseMessage GetTaskOnboardView(string employee_gid)
        {
            MdlTaskViewInfoList values = new MdlTaskViewInfoList();
            objDaManageEmployee.GetTaskOnboardView(employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHRDoclist")]
        [HttpGet]
        public HttpResponseMessage GetHRDoclist(string employee_gid)
        {
            hrdoc_list objemployeedoc_list = new hrdoc_list();
            objDaManageEmployee.DaGetHRDoclist(employee_gid, objemployeedoc_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployeedoc_list);
        }

        [ActionName("PostAssignTask")]
        [HttpPost]
        public HttpResponseMessage PostAssignTask(MdlTaskAssign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPostAssignTask(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostMyTaskStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage PostMyTaskStatusUpdate(MdlTaskStatusUpdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPostMyTaskStatusUpdate(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTeamMemberlist")]
        [HttpGet]
        public HttpResponseMessage DaGetTeamMemberlist(string team_gid)
        {
            member_list values = new member_list();
            objDaManageEmployee.DaGetTeamMemberlist(team_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}



