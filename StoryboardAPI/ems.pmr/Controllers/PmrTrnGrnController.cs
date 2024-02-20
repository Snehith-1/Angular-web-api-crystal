using ems.pmr.DataAccess;
using ems.pmr.Models;
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

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrTrnGrn")]
    [Authorize]
    public class PmrTrnGrnController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrTrnGrn objpurchase = new DaPmrTrnGrn();

        [ActionName("GetGrninwardSummary")]
        [HttpGet]
        public HttpResponseMessage GetGrninwardSummary()
        {
            MdlPmrTrnGrn values = new MdlPmrTrnGrn();
            objpurchase.DaGrninwardSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);


        }

        [ActionName("Getaddgrnsummary")]
        [HttpGet]
        public HttpResponseMessage Getaddgrnsummary(string purchaseorder_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            MdlPmrTrnGrn values = new MdlPmrTrnGrn();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaGetaddgrnsummary(getsessionvalues.user_gid, purchaseorder_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("Getsummaryaddgrn")]
        [HttpGet]
           public HttpResponseMessage Getsummaryaddgrn(string purchaseorder_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            MdlPmrTrnGrn values = new MdlPmrTrnGrn();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaGetsummaryaddgrnsummary(getsessionvalues.user_gid, purchaseorder_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("PostGrnSubmit")]
        [HttpPost]
        public HttpResponseMessage PostGrnSubmit(addgrn_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostGrnSubmit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}