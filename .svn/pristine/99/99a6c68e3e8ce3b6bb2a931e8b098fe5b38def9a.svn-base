using ems.inventory.DataAccess;
using ems.inventory.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.inventory.Controllers
{
    [RoutePrefix("api/ImsTrnIssueMaterial")]
    [Authorize]
    public class ImsTrnIssueMaterialController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaImsTrnIssueMaterial objInventory = new DaImsTrnIssueMaterial();

        [ActionName("GetIssueMaterialSummary")]
        [HttpGet]

        public HttpResponseMessage GetIssueMaterialSummary()
        {
            MdlImsTrnIssueMaterial values = new MdlImsTrnIssueMaterial();
            objInventory.DaGetImsTrnIssueMaterial(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
    }
}