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
    [RoutePrefix("api/ProductGroup")]
    public class ProductGroupController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaProductGroup objDacrm = new DaProductGroup();

        [ActionName("GetProductgroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductgroupSummary()
        {
            MdlProductGroup values = new MdlProductGroup();
            objDacrm.DaProductgroupSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProductgroup")]
        [HttpPost]
        public HttpResponseMessage PostProductgroup(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDacrm.DaPostProductgroup(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK,values);
        }
        [ActionName("UpdatedProductgroup")]
        [HttpPost]
        public HttpResponseMessage UpdatedProductgroup(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDacrm.DaUpdatedProductgroup(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("deleteProductgroupSummary")]
        [HttpGet]
        public HttpResponseMessage deleteProductgroupSummary(string productgroup_gid)
        {
            productgroup_list objresult = new productgroup_list();
            objDacrm.DadeleteProductgroupSummary(productgroup_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}