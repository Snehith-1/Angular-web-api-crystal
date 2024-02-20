using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Configuration;
using System.IO;

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrMstProductGroup")]
    [Authorize]
    public class PmrMstProductGroupController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrMstProductGroup objpurchase = new DaPmrMstProductGroup();
        //[ActionName("PostVendorregisterdetails")]
        //[HttpPost]
        //public HttpResponseMessage PostEmployeedetails(Getvendor_lists values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objpurchase.DaPostVendorRegister(values, getsessionvalues.user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
        // Module Summary
        [ActionName("GetProductGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductGroupSummary()
        {
            MdlPmrMstProductGroup values = new MdlPmrMstProductGroup();
            objpurchase.DaGetProductGroupSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        //Add event

        [ActionName("PostProductGroup")]
        [HttpPost]
        public HttpResponseMessage PostProductGroup(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostProductGroup(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Event

        [ActionName("GetUpdatedProductgroup")]
        [HttpPost]
        public HttpResponseMessage GetUpdatedProductgroup(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaGetUpdatedProductgroup(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // delete event

        [ActionName("GetDeleteProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeleteProductSummary(string productgroup_gid)
        {
            productgroup_list objresult = new productgroup_list();
            objpurchase.DaGetDeleteProductSummary(productgroup_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}
