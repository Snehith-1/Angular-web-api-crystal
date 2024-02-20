using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Security.Cryptography;


namespace ems.pmr.Controllers
{
      [RoutePrefix("api/PmrRptPurchaseorderdetailedreport")]
        [Authorize]
        public class PmrRptPurchaseorderdetailedreportController: ApiController
        {

            session_values Objgetgid = new session_values();
            logintoken getsessionvalues = new logintoken();
            DaPmrRptPurchaseorderdetailedreport objpurchase = new DaPmrRptPurchaseorderdetailedreport();

            [ActionName("GetPmrRptPurchaseorderdetailedreportSummary")]
            [HttpGet]
            public HttpResponseMessage GetPmrRptPurchaseorderdetailedreportSummary()
            {
                MdlPmrRptPurchaseorderdetailedreport values = new MdlPmrRptPurchaseorderdetailedreport();
                objpurchase.DaGetPmrRptPurchaseorderdetailedreportSummary(values);
                return Request.CreateResponse(HttpStatusCode.OK, values);

            }
        }
    
}