using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using StoryboardAPI.Models;

namespace ems.sales.Controllers
{
    [RoutePrefix("api/SmrTrnMyEnquiry")]
    [Authorize]
    public class SmrTrnMyEnquiryController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrTrnMyEnquiry objDaSales = new DaSmrTrnMyEnquiry();
        // Module Summary
        [ActionName("GetSmrTrnMyEnquiry")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnMyEnquiry()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            MdlSmrTrnMyEnquiry values = new MdlSmrTrnMyEnquiry();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaGetSmrTrnMyEnquiry(getsessionvalues.user_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        public HttpResponseMessage GetSmrTrnMyEnquirynew()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            MdlSmrTrnMyEnquiry values = new MdlSmrTrnMyEnquiry();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaGetSmrTrnMyEnquirynew(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        public HttpResponseMessage GetSmrTrnMyEnquiryProspect()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            MdlSmrTrnMyEnquiry values = new MdlSmrTrnMyEnquiry();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaGetSmrTrnMyEnquiryProspect(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        public HttpResponseMessage GetSmrTrnMyEnquiryPotential()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            MdlSmrTrnMyEnquiry values = new MdlSmrTrnMyEnquiry();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaGetSmrTrnMyEnquiryPotential(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        public HttpResponseMessage GetSmrTrnMyEnquiryCompleted()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            MdlSmrTrnMyEnquiry values = new MdlSmrTrnMyEnquiry();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaGetSmrTrnMyEnquiryCompleted(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        public HttpResponseMessage GetSmrTrnMyEnquiryDrop()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            MdlSmrTrnMyEnquiry values = new MdlSmrTrnMyEnquiry();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaGetSmrTrnMyEnquiryDrop(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        public HttpResponseMessage GetSmrTrnMyEnquiryAll()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            MdlSmrTrnMyEnquiry values = new MdlSmrTrnMyEnquiry();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaGetSmrTrnMyEnquiryAll(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetMyenquiryCount")]
        [HttpGet]
        public HttpResponseMessage GetMyenquiryCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrTrnMyEnquiry values = new MdlSmrTrnMyEnquiry();
            objDaSales.DaGetMyenquiryCount (getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       

    }
}