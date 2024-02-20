using ems.einvoice.DataAccess;
using ems.einvoice.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
namespace ems.einvoice.Controllers
{
    [RoutePrefix("api/LandingPage")]
    [Authorize]
    public class LandingPageController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLandingPage objDaLandingPage = new DaLandingPage();

        [ActionName("GetCustomerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerSummary()
        {
            MdlLandingPage values = new MdlLandingPage();
            objDaLandingPage.DaLandingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}