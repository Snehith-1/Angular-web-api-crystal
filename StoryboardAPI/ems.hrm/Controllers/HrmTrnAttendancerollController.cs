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
    [RoutePrefix("api/HrmTrnAttendanceroll")]
    [Authorize]
    public class HrmTrnAttendancerollController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaHrmTrnAttendanceroll objdaemployeelist = new DaHrmTrnAttendanceroll();

        [ActionName("GetEmployeedtlSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeedtlSummary(string date)
        {
            MdlHrmTrnAttendanceroll values = new MdlHrmTrnAttendanceroll();
            objdaemployeelist.DaGetEmployeeAttendanceSummary(getsessionvalues.user_gid, values,date);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBranch")]
        [HttpGet]

        public HttpResponseMessage GetBranch()
        {
            MdlHrmTrnAttendanceroll values = new MdlHrmTrnAttendanceroll();
            objdaemployeelist.DaGetBranch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDepartment")]
        [HttpGet]
        public HttpResponseMessage GetDepartment(string branch_gid)
        {
            MdlHrmTrnAttendanceroll values = new MdlHrmTrnAttendanceroll();
            objdaemployeelist.GetDepartment(branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetShift")]
        [HttpGet]
        public HttpResponseMessage GetShift(string branch_gid)
        {
            MdlHrmTrnAttendanceroll values = new MdlHrmTrnAttendanceroll();
            objdaemployeelist.GetShift(branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updatedtl")]
        [HttpPost]
        public HttpResponseMessage Updatedtl(update_lists values)
        { 
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaemployeelist.Updatedtl(values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("cleardtl")]
        [HttpPost]
        public HttpResponseMessage cleardtl(update_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaemployeelist.Dacleardtl(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Updatepunchindtl")]
        [HttpPost]
        public HttpResponseMessage Updatepunchindtl(punchupdatedtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaemployeelist.DaUpdatepunchindtl(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Updatepunchoutdtl")]
        [HttpPost]
        public HttpResponseMessage Updatepunchoutdtl(punchupdatedtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaemployeelist.DaUpdatepunchoutdtl(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}