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
    [RoutePrefix("api/SysMstDesignation")]

    public class SysMstDesignationController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSysMstDesignation objDasys = new DaSysMstDesignation();


        [ActionName("GetDesignationtSummary")]
        [HttpGet]
        public HttpResponseMessage GetDesignationtSummary()
        {
            MdlSysMstDesignation values = new MdlSysMstDesignation();
            objDasys.DaGetDesignationtSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDesignationAdd")]
        [HttpPost]
        public HttpResponseMessage PostDesignationAdd(Designation_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDasys.DaPostDesignationAdd(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteDesignation")]
        [HttpGet]
        public HttpResponseMessage DeleteDesignation(string params_gid)
        {
            Designation_list objresult = new Designation_list();
            objDasys.DaDeleteDesignation(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("PostUpdateDesignation")]
        [HttpPost]
        public HttpResponseMessage PostUpdateDesignation(string user_gid, Designation_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDasys.DaPostUpdateDesignation(user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("PostDesignationStatus")]
        [HttpPost]
        public HttpResponseMessage PostDesignationStatus(string user_gid, Designation_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDasys.DaPostDesignationStatus(user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
    }
}