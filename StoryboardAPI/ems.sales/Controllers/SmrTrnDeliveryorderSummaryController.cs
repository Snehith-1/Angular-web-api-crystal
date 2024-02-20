using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.sales.Controllers
{

    [RoutePrefix("api/SmrTrnDeliveryorderSummary")]
    [Authorize]
    public class SmrTrnDeliveryorderSummaryController: ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrTrnDeliveryorderSummary objDaSales = new DaSmrTrnDeliveryorderSummary();

        [ActionName("GetSmrTrnAddDeliveryorderSummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnAddDeliveryorderSummary()
        {
            MdlSmrTrnDeliveryorderSummary values = new MdlSmrTrnDeliveryorderSummary();
            objDaSales.DaGetSmrTrnAddDeliveryorderSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetSmrTrnDeliveryorderSummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnDeliveryorderSummary()
        {
            MdlSmrTrnDeliveryorderSummary values = new MdlSmrTrnDeliveryorderSummary();
            objDaSales.DaGetSmrTrnDeliveryorderSummary(values);
           
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
    }
}