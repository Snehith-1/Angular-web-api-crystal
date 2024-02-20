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
    [RoutePrefix("api/Leaveopening")]
    [Authorize]
    public class LeaveopeningController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLeaveopening objDaLeaveopening = new DaLeaveopening();

        [ActionName("GetLeaveopening")]
        [HttpGet]
        public HttpResponseMessage GetLeaveopening()
        {
            MdlOpeningleave values = new MdlOpeningleave();
            objDaLeaveopening.DaGetLeaveopening(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getleavegradeopeningdropdown")]
        [HttpGet]
        public HttpResponseMessage Getleavegradeopeningdropdown()
        {
            MdlOpeningleave values = new MdlOpeningleave();
            objDaLeaveopening.DaGetleavegradeopeningdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("editleaveopening")]
        [HttpGet]
        public HttpResponseMessage editleaveopening(string leavegrade_gid)
        {
            MdlOpeningleave values = new MdlOpeningleave();
            objDaLeaveopening.Daeditleaveopening(values, leavegrade_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Postleavebalance")]
        [HttpPost]
        public HttpResponseMessage Postleavebalance(leavebalance_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLeaveopening.DaPostleavebalance(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}