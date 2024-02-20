using ems.asset.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using ems.asset.Models;



namespace ems.asset.Controllers
{
    [RoutePrefix("api/AmsMstAttribute")]
    [Authorize]
    public class AmsMstAttributeController: ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAmsMstAttribute objDaCustomer = new DaAmsMstAttribute();

        [ActionName("PostAttributeAdd")]
        [HttpPost]

        public HttpResponseMessage PostAttribute(attribute_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostAttribute(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("GetAttributeSummary")]
        [HttpGet]
        public HttpResponseMessage GetAttributeSummary()
        {
            MdlAmsMstAttribute values = new MdlAmsMstAttribute();
            objDaCustomer.DaGetAttributeSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAttribute")]
        [HttpGet]
        public HttpResponseMessage DeleteAttribute(string attribute_gid)
        {
            attribute_list objresult = new attribute_list();
            objDaCustomer.DaDeleteAttribute(attribute_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("UpdateAttribute")]
        [HttpPost]

        public HttpResponseMessage UpdateAttribute(attribute_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaUpdateAttribute(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlAmsMstAttribute objresult = new MdlAmsMstAttribute();
            objDaCustomer.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}