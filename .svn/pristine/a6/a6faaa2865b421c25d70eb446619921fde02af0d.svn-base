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
    [RoutePrefix("api/AppointmentOrder")]
    [Authorize]
    public class AppointmentOrderController : ApiController

    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAppointmentOrder objDaAppointmentOrder = new DaAppointmentOrder();

        [ActionName("GetappointmentorderSummary")]
        [HttpGet]
        public HttpResponseMessage GetappointmentorderSummary()
        {
            MdlAppoinmentOrder values = new MdlAppoinmentOrder();
            objDaAppointmentOrder.DaGetappointmentorderSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getbranchdropdown")]
        [HttpGet]
        public HttpResponseMessage Getbranchdropdown()
        {
            MdlAppoinmentOrder values = new MdlAppoinmentOrder();
            objDaAppointmentOrder.DaGetbranchdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getdepartmentdropdown")]
        [HttpGet]
        public HttpResponseMessage Getdepartmentdropdown()
        {
            MdlAppoinmentOrder values = new MdlAppoinmentOrder();
            objDaAppointmentOrder.DaGetdepartmentdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getdesignationdropdown")]
        [HttpGet]
        public HttpResponseMessage Getdesignationdropdown()
        {
            MdlAppoinmentOrder values = new MdlAppoinmentOrder();
            objDaAppointmentOrder.DaGetdesignationdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Geteditappoinmentorder")]
        [HttpGet]
        public HttpResponseMessage Geteditappoinmentorder(string appointmentorder_gid)
        {
            editappoinmentorderlist values = new editappoinmentorderlist();
            values= objDaAppointmentOrder.DaGeteditappoinmentorder(appointmentorder_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcountrydropdown")]
        [HttpGet]
        public HttpResponseMessage Getcountrydropdown()
        {
            MdlAppoinmentOrder values = new MdlAppoinmentOrder();
            objDaAppointmentOrder.DaGetcountrydropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Updatedappointmentorder")]
        [HttpPost]
        public HttpResponseMessage Updatedappointmentorder(update_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAppointmentOrder.DaUpdatedappointmentorder(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TermsandConditions")]
        [HttpGet]
        public HttpResponseMessage TermsandConditions()
        {
            MdlAppoinmentOrder values = new MdlAppoinmentOrder();
            objDaAppointmentOrder.DaTermsandConditions(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnChangeTerms")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeTerms(string template_gid)
        {
            MdlAppoinmentOrder values = new MdlAppoinmentOrder();
            objDaAppointmentOrder.DaOnChangeTerms(template_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}