using System;
using System.Collections.Generic;
using System.Drawing;
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
    [RoutePrefix("api/Region")]
    public class RegionController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaRegion objdaregion = new DaRegion();


        [ActionName("GetRegionSummary")]
        [HttpGet]
        public HttpResponseMessage GetRegionSummary()
        {
            MdlRegion values = new MdlRegion();
            objdaregion.DaGetRegionSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostRegion")]
        [HttpPost]
        public HttpResponseMessage PostRegion(region_lists1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaregion.DaPostRegion(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateRegion")]
        [HttpPost]
        public HttpResponseMessage UpdateRegion(region_lists1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaregion.DaUpdateRegion(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteRegion")]
        [HttpGet]
        public HttpResponseMessage DeleteRegion(string region_gid)
        {
            region_lists1 objresult = new region_lists1();
            objdaregion.DaDeleteRegion(region_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}