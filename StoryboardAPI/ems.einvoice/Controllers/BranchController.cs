using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.einvoice.DataAccess;
using ems.einvoice.Models;

namespace ems.einvoice.Controllers
{
    [Authorize]
    [RoutePrefix("api/Branch")]
    public class BranchController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaBranch objDaBranch = new DaBranch();


        [ActionName("BranchSummary")]
        [HttpGet]
        public HttpResponseMessage BranchSummary()
       {
            MdlBranch values = new MdlBranch();
            objDaBranch.DaBranchSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostBranch")]
        [HttpPost]
        public HttpResponseMessage PostBranch(branch_list1 values, string user_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objDaBranch.DaPostBranch(user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("getUpdatedBranch")]
        [HttpPost]
        public HttpResponseMessage getUpdatedBranch(string user_gid,  branch_list1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            //getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaBranch.DagetUpdatedBranch(user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("DeleteBranch")]
        [HttpGet]
        public HttpResponseMessage DeleteBranch(string params_gid)
        {
            branch_list1 objresult = new branch_list1();
            objDaBranch.DaDeleteBranch(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        
        [ActionName("BranchSummarydetail")]
        [HttpPost]
        public HttpResponseMessage BranchSummarydetail(string branch_gid, branch_list1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            //getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaBranch.DaBranchSummarydetail(branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("Updatedbranchlogo")]
        [HttpPost]
        public HttpResponseMessage Updatedbranchlogo()
        {
            HttpRequest httpRequest;
            //Postassetlocationcreation values = new Postassetlocationcreation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaBranch.DaUpdatedbranchlogo(httpRequest, objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

    }
}

    
