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
    [RoutePrefix("api/Industry")]
    public class IndustryController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIndustry objdaindustry = new DaIndustry();


        [ActionName("GetIndustrySummary")]
        [HttpGet]
        public HttpResponseMessage GetIndustrySummary()
        {
            MdlIndustry values = new MdlIndustry();
            objdaindustry.DaGetIndustrySummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostIndustry")]
        [HttpPost]
        public HttpResponseMessage PostIndustry(industry_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaindustry.DaPostIndustry(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getupdateindustrydetails")]
        [HttpPost]
        public HttpResponseMessage Getupdateindustrydetails(industry_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaindustry.DaGetupdateindustrydetails(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getdeleteindustrydetails")]
        [HttpGet]
        public HttpResponseMessage Getdeleteindustrydetails(string industry_gid)
        {
            industry_list objresult = new industry_list();
            objdaindustry.DaGetdeleteindustrydetails(industry_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
       

    }
}