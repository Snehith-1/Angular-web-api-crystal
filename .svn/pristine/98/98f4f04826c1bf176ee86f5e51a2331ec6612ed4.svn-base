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
using static ems.einvoice.Models.MdlInvoicedashboard;

namespace ems.einvoice.Controllers
{
    [RoutePrefix("api/Invoicedashboard")]
    [Authorize]
    public class InvoicedashboardController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaInvoicedashboard objDaInvoicedashboard = new DaInvoicedashboard();

        [ActionName("TileCount")]
        [HttpGet]
        public HttpResponseMessage GetTileCount()
        {
            TileCount values = new TileCount();
            objDaInvoicedashboard.DaTileCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("invoicesummary")]
        [HttpGet]
        public HttpResponseMessage Getinvoicesummary()
        {
            MdlInvoicedashboard values = new MdlInvoicedashboard();
            objDaInvoicedashboard.Dainvoicesummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}

