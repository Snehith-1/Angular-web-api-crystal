using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.system.DataAccess;
using ems.system.DataAccess;
using ems.system.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.system.Controllers
{
    [Authorize]
    [RoutePrefix("api/Department")]

    public class DepartmentController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaDepartment objDaDepartment = new DaDepartment();


        [ActionName("GetDepartmentSummary")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentSummary()
        {
            MdlDepartment values = new MdlDepartment();
            objDaDepartment.DaGetDepartmentSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDepartmentAddDropdown")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentAddDropdown()
        {
            MdlDepartment values = new MdlDepartment();
            objDaDepartment.GetDepartmentAddDropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDepartment")]
        [HttpPost]
        public HttpResponseMessage PostBranch(department_list values, string user_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objDaDepartment.DaPostDepartment(user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("getUpdatedDepartment")]
        [HttpPost]
        public HttpResponseMessage getUpdatedDepartment(string user_gid, department_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDepartment.DagetUpdatedDepartment(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteDepartment")]
        [HttpGet]
        public HttpResponseMessage DeleteDepartment(string params_gid)
        {
            department_list objresult = new department_list();
            objDaDepartment.DaDeleteDepartment(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}


