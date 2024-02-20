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


namespace ems.asset.Controllers
{
    [RoutePrefix("api/AmsMstUnit")]
    [Authorize]

    public class AmsMstUnitController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAmsMstUnit objDaCustomer = new DaAmsMstUnit();



        [ActionName("PostUnit")]
        [HttpPost]
        public HttpResponseMessage PostUnit(unit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostUnit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("GetUnitSummary")]
        [HttpGet]
        public HttpResponseMessage GetUnitSummary()
        {
            unitllist values = new unitllist();
            objDaCustomer.DaGetUnitSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetupdateUnitdetails")]
        [HttpPost]
        public HttpResponseMessage GetupdateUnitdetails(unit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaGetupdateUnitdetails(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }



        [ActionName("GetdeleteUnitdetails")]
        [HttpGet]
        public HttpResponseMessage GetdeleteUnitdetails(string locationunit_gid)
        {
            unit_list objresult = new unit_list();
            objDaCustomer.DaGetdeleteUnitdetails(locationunit_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlAmsMstUnit objresult = new MdlAmsMstUnit();
            objDaCustomer.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}
  
