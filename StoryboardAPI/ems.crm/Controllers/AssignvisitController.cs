using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;


namespace ems.crm.Controllers
{
    [Authorize]
    [RoutePrefix("api/Assignvisit")]
    public class AssignvisitController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAssignvisit objDacrm = new DaAssignvisit();

        [ActionName("GetassignvisitSummary")]
        [HttpGet]
        public HttpResponseMessage GetassignvisitSummary()
        {
            MdlAssignvisit values = new MdlAssignvisit();
            objDacrm.DaGetassignvisitSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getmarketingteamdropdown")]
        [HttpGet]
        public HttpResponseMessage Getmarketingteamdropdown()
        {
            MdlAssignvisit values = new MdlAssignvisit();
            objDacrm.DaGetmarketingteamdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getexecutedropdown")]
        [HttpGet]
        public HttpResponseMessage Getexecutedropdown(string user_gid, string campaign_gid)
        {
            MdlAssignvisit objresult = new MdlAssignvisit();
            objDacrm.DaGetexecutedropdown(user_gid, campaign_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getmarketingteamdropdownonchange")]
        [HttpGet]
        public HttpResponseMessage Getmarketingteamdropdownonchange(string campaign_gid)
        {
            MdlAssignvisit objresult = new MdlAssignvisit();
            objDacrm.DaGetmarketingteamdropdownonchange(campaign_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
       
        [ActionName("GetAssignassignvisit")]
        [HttpPost]
        public HttpResponseMessage GetAssignassignvisit(assignvisitsubmit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDacrm.DaGetAssignassignvisit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
    }
}