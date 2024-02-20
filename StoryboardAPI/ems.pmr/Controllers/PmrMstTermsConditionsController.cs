using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Web.Http.Results;

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrMstTermsConditions")]
    [Authorize]
    public class PmrMstTermsConditionsController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrMstTermsConditions objpurchase = new DaPmrMstTermsConditions();

        [ActionName("GetTermsConditionsSummary")]
        [HttpGet]
        public HttpResponseMessage GetTermsConditionsSummary()
        {
            MdlPmrMstTermsConditions values = new MdlPmrMstTermsConditions();
            objpurchase.DaGetTermsConditionsSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("PostTermsConditions")]
        [HttpPost]
        public HttpResponseMessage PostTermsConditions(template_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objpurchase.DaPostTermsConditions(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTemplateEditdata")]
        [HttpGet]
        public HttpResponseMessage GetTemplateEditdata(string termsconditions_gid)
        {
            MdlPmrMstTermsConditions values = new MdlPmrMstTermsConditions();
            objpurchase.DaGetTemplateEditdata(values, termsconditions_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatedTemplate")]
        [HttpPost]
        public HttpResponseMessage UpdatedTemplate(templateupdate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objpurchase.DaUpdatedTemplate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteTemplate")]
        [HttpGet]
        public HttpResponseMessage DeleteTemplate(string termsconditions_gid)
        {
            templatedelete_list objresult = new templatedelete_list();
            objpurchase.DaDeleteTemplate(termsconditions_gid,objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        

    }
}