using ems.hrm.DataAccess;
using ems.hrm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
namespace ems.hrm.Controllers
{
    [RoutePrefix("api/Iattendance")]
    [Authorize]
    public class IAttendanceController : ApiController

    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIAttendance objDaIAttendance = new DaIAttendance();

        [ActionName("GetAttendancedata")]
        [HttpGet]
        public HttpResponseMessage GetAttendancedata()
        {
            MdlIAttendance values = new MdlIAttendance();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaIAttendance.DaGetAttendancedata(getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSignIn")]
        [HttpPost]
        public HttpResponseMessage PostSignIn(Iattendance_list values)
        
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaIAttendance.DaPostSignIn(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSignOut")]
        [HttpPost]
            public HttpResponseMessage PostSignOut(Iattendance_list values)
            {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaIAttendance.DaPostSignOut(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
     }
   
}