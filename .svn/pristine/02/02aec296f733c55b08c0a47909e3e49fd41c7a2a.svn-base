using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.crm.Controllers
{

    [Authorize]
    [RoutePrefix("api/CallResponse")]
    public class CallResponseController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaCallResponse objdacalllist = new DaCallResponse();

        [ActionName("GetCallResponseSummary")]
        [HttpGet]
        public HttpResponseMessage GetCallResponseSummary()
        {
            MdlCallResponse values = new MdlCallResponse();
            objdacalllist.DaGetCallResponseSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCallResponse")]
        [HttpPost]
        public HttpResponseMessage PostCallResponse(call_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdacalllist.DaPostCallResponse(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetupdateCallResponsedetails")]
        [HttpPost]
        public HttpResponseMessage GetupdateCallResponsedetails(call_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdacalllist.GetupdateCallResponsedetails(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetdeleteCallResponsedetails")]
        [HttpGet]
        public HttpResponseMessage GetdeleteCallResponsedetails(string callresponse_gid)
        {
            call_lists objresult = new call_lists();
            objdacalllist.DaGetdeleteCallResponsedetails(callresponse_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


    }
}