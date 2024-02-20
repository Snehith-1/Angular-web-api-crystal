using ems.utilities.Functions;
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

namespace ems.hrm.Controllers
{
    public class ManageRoleController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaManageRole objDaRoleMaster = new DaManageRole();
        [ActionName("RoleSummary")]
        [HttpGet]
        public HttpResponseMessage GetRoleSummary()
        {
            rolelist objrolelist = new rolelist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaRoleMaster.DaRoleSummary(objrolelist);
            return Request.CreateResponse(HttpStatusCode.OK, objrolelist);
        }
        [ActionName("RoleAdd")]
        [HttpPost]
        public HttpResponseMessage PostRoleAdd(rolelist objrolelist)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaRoleMaster.DaRoleAdd(objrolelist, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objrolelist);
        }
        [ActionName("RoleEdit")]
        [HttpGet]
        public HttpResponseMessage GetRoleEdit(string role_gid)
        {
            role objrole = new role();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaRoleMaster.DaRoleEdit(objrole, getsessionvalues.user_gid, role_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objrole);
        }
        [ActionName("RoleUpdate")]
        [HttpPost]
        public HttpResponseMessage PostRoleUpdate(role objrole)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaRoleMaster.DaRoleUpdate(objrole, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objrole);
        }
        [ActionName("PopRoleReportingToAdd")]
        [HttpGet]
        public HttpResponseMessage GetPopRoleReportingToAdd()
        {
            rolereporting_to_list objrolereporting_to_list = new rolereporting_to_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaRoleMaster.DaPopRoleRepotingtoAdd(objrolereporting_to_list);
            return Request.CreateResponse(HttpStatusCode.OK, objrolereporting_to_list);
        }
        [ActionName("PopRoleReportingToEdit")]
        [HttpGet]
        public HttpResponseMessage GetPopRoleReportingToEdit(string role_gid)
        {
            rolereporting_to_listEdit objrolereporting_to_list = new rolereporting_to_listEdit();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaRoleMaster.DaPopRoleReportingtoEdit(objrolereporting_to_list, role_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objrolereporting_to_list);
        }
        [ActionName("RoleDelete")]
        [HttpGet]
        public HttpResponseMessage PostRoleDelete(string role_gid)
        {
            role objrole = new Models.role();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaRoleMaster.DaRoleDelete(role_gid, objrole);
            return Request.CreateResponse(HttpStatusCode.OK, objrole);
        }

    }
}

    
