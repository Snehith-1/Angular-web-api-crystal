using ems.asset.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Net.Http;
using System.Web.Http;
using ems.asset.Models;
using System.Web.Http.Results;


namespace ems.asset.Controllers
{
    [RoutePrefix("api/AmsMstProductgroup")]
    [Authorize]

    public class AmsMstProductGroupController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAmsMstProductGroup objDaCustomer = new DaAmsMstProductGroup();

        [ActionName("GetProductGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductGroupSummary()
        {
            MdlAmsMstProductGroup values = new MdlAmsMstProductGroup();
            objDaCustomer.DaGetProductGroupSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostProductGroupAdd")]
        [HttpPost]
        public HttpResponseMessage PostProductGroupAdd(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostProductGroupAdd(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
       
        [ActionName("PostProductGroupUpdate")]
        [HttpPost]
        public HttpResponseMessage PostProductGroupUpdate(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostProductGroupUpdate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
     
        [ActionName("deleteproductgroup")]
        [HttpGet]
        public HttpResponseMessage deleteproductgroup(string productgroup_gid)
        {
            productgroup_list objresult = new productgroup_list();
            objDaCustomer.Dadeleteproductgroup(productgroup_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlAmsMstProductGroup objresult = new MdlAmsMstProductGroup();
            objDaCustomer.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}