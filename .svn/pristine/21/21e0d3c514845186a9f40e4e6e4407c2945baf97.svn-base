using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.system.DataAccess;
using ems.system.Models;
using System.Web;
//using ems.hbapiconn.Models;

namespace ems.system.Controllers
{
    [Authorize]
    [RoutePrefix("api/ManageEmployee")]
    public class ManageEmployeeController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaManageEmployee objDaManageEmployee = new DaManageEmployee();

        [ActionName("EmployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeSummary()
        {
            employee_list objemployee_list = new employee_list();
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
            employee_list objemployee_list = new employee_list();
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
            employee_list objemployee_list = new employee_list();
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
            employee_list objemployee_list = new employee_list();
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
            employee_list objemployee_list = new employee_list();
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
            employee_list objemployee_list = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopBranch(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        [ActionName("PopDepartment")]
        [HttpGet]
        public HttpResponseMessage GetPopDepartment()
        {
            employee_list objemployee_list = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopDepartment(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        [ActionName("PopSubfunction")]
        [HttpGet]
        public HttpResponseMessage GetPopClientRole()
        {
            employee_list objemployee_list = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopSubfunction(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }
        //ProfileView


        [ActionName("EmployeeProfileView")]
        [HttpGet]
        public HttpResponseMessage EmployeeProfileView()
        {
            employee_list objemployee = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeProfileView(objemployee, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }

        [ActionName("GetEmployeename")]
        [HttpGet]
        public HttpResponseMessage GetEmployeename(string user_gid)
        {
            employee_list objresult = new employee_list();
            objDaManageEmployee.DaGetEmployeename(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}
