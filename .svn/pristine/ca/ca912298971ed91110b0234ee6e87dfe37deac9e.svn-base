using ems.hrm.DataAccess;
using ems.hrm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.hrm.Controllers
{
    [Authorize]
    [RoutePrefix("api/HolidayGradeManagement")]
    public class HolidayGradeManagementController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaHolidayGradeManagement objdaHoliday = new DaHolidayGradeManagement();

        [ActionName("HolidayGradeSummary")]
        [HttpGet]
        public HttpResponseMessage HolidayGradeSummary()
        {
            MdlHolidaygradeManagement values = new MdlHolidaygradeManagement();
            objdaHoliday.DaHolidayGradeSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AddHolidayGradesubmit")]
        [HttpPost]
        public HttpResponseMessage AddHolidayGradesubmit(addholidaygrade_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaHoliday.DaAddHolidayGradesubmit( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Addholidaysummary")]
        [HttpGet]
        public HttpResponseMessage Addholidaysummary()
        {
            MdlHolidaygradeManagement values = new MdlHolidaygradeManagement();
            objdaHoliday.DaAddholidaysummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HolidayAssignSubmit")]
        [HttpPost]
        public HttpResponseMessage HolidayAssignSubmit(Addholidayassign_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaHoliday.DaHolidayAssignSubmit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Deleteholiday")]
        [HttpGet]
        public HttpResponseMessage Deleteholiday(string params_gid)
        {
            MdlHolidaygradeManagement objresult = new MdlHolidaygradeManagement();
            objdaHoliday.DaDeleteholiday(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}