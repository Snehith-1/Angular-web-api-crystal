using ems.hrm.DataAccess;
using ems.hrm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.hrm.Controllers
{
    [Authorize]
    [RoutePrefix("api/ManualRegulation")]
    public class ManualRegulationController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaManualRegulation objdaManual = new DaManualRegulation();

        [ActionName("GetManualRegulationsummary")]
        [HttpGet]
        public HttpResponseMessage GetManualRegulationsummary(string fromdate,string todate)
        {
            MdlManualRegulation values = new MdlManualRegulation();
            objdaManual.DaManualRegulationsummary(fromdate,todate,values);
            return Request.CreateResponse(HttpStatusCode.OK,values);
        }
    }
}