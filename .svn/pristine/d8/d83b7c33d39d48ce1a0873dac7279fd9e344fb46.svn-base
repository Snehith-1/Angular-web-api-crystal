using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.crm.DataAccess;
using ems.crm.Models;
using ems.system.DataAccess;
using ems.system.Models;
using ems.utilities.Functions;
using ems.utilities.Models;


namespace ems.crm.Controllers
{
    [Authorize]
    [RoutePrefix("api/Source")]
    public class SourceController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSource objdasourcelist = new DaSource();

        [ActionName("GetSourceSummary")]
        [HttpGet]
        public HttpResponseMessage GetSourceSummary()
        {
            MdlSource values = new MdlSource();
            objdasourcelist.DaGetSourceSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSource")]
        [HttpPost]
        public HttpResponseMessage PostSource(source_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdasourcelist.DaPostSource(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getupdatesourcedetails")]
        [HttpPost]
        public HttpResponseMessage Getupdatesourcedetails(source_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdasourcelist.DaGetupdatesourcedetails(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getdeletesourcedetails")]
        [HttpGet]
        public HttpResponseMessage Getdeletesourcedetails(string source_gid)
        {
            source_lists objresult = new source_lists();
            objdasourcelist.DaGetdeletesourcedetails(source_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}