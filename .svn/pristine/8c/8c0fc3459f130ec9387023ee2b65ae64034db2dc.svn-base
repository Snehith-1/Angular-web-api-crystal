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
    [Authorize]
    [RoutePrefix("api/ShiftType")]
    public class ShiftTypeController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaShifttype objdashift = new DaShifttype();

        [ActionName("GetShiftSummary")]
        [HttpGet]
        public HttpResponseMessage GetShiftSummary()
        {
            MdlShiftType values = new MdlShiftType();
            objdashift.DaShiftSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetWeekdaysummary")]
        [HttpGet]
        public HttpResponseMessage GetWeekdaysummary()
        {
            shifttypeadd_list values = new shifttypeadd_list();
            objdashift.DaGetWeekdaysummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Shiftweekdaystime")]
        [HttpPost]
        public HttpResponseMessage Shiftweekdaystime(shifttypeadd_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdashift.Dashiftweekdaystime(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetshiftTimepopup")]
        [HttpGet]
        public HttpResponseMessage GetshiftTimepopup(string shifttype_gid)
        {
            MdlShiftType values = new MdlShiftType();
            objdashift.DaGetshiftTimepopup(shifttype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteShift")]
        [HttpGet]
        public HttpResponseMessage DeleteShift(string params_gid)
        {
            MdlShiftType objresult = new MdlShiftType();
            objdashift.DaDeleteShift(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetshiftActive")]
        [HttpGet]
        public HttpResponseMessage GetshiftActive(string params_gid)
        {
            MdlShiftType objresult = new MdlShiftType();
            objdashift.DaGetshiftActive(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetshiftInActive")]
        [HttpGet]
        public HttpResponseMessage GetshiftInActive(string params_gid)
        {
            MdlShiftType objresult = new MdlShiftType();
            objdashift.DaGetshiftInActive(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}