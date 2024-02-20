using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Configuration;
using System.IO;
using StoryboardAPI.Models;
using Newtonsoft.Json.Linq;

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrTrnOpeningInvoice")]
    [Authorize]
    public class PmrTrnOpeningInvoiceController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrTrnOpeningInvoice objopinvoice = new DaPmrTrnOpeningInvoice();

        [ActionName("GetOpeningInvoiveSummary")]
        [HttpGet]

        public HttpResponseMessage GetOpeningInvoiveSummary()
        {
            MdlPmrTrnOpeningInvoice values = new MdlPmrTrnOpeningInvoice();
            objopinvoice.DaGetOpeningInvoiceSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getvendor")]
        [HttpGet]
        public HttpResponseMessage Getvendor()
        {
            MdlPmrTrnOpeningInvoice values = new MdlPmrTrnOpeningInvoice();
            objopinvoice.DaGetvendor(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnChangevonder")]
        [HttpGet]
        public HttpResponseMessage GetOnChangevonder(string vendor_gid)
        {
            MdlPmrTrnOpeningInvoice values = new MdlPmrTrnOpeningInvoice();
            objopinvoice.DaGetOnChangevonder(vendor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnChangevonderAddress")]
        [HttpGet]
        public HttpResponseMessage GetOnChangevonderAddress(string vendor_gid)
        {
            MdlPmrTrnOpeningInvoice values = new MdlPmrTrnOpeningInvoice();
            objopinvoice.DaGetOnChangevonderAddress(vendor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getbranch")]
        [HttpGet]
        public HttpResponseMessage Getbranch()
        {
            MdlPmrTrnOpeningInvoice values = new MdlPmrTrnOpeningInvoice();
            objopinvoice.DaGetbranch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcurrency")]
        [HttpGet]
        public HttpResponseMessage Getcurrency()
        {
            MdlPmrTrnOpeningInvoice values = new MdlPmrTrnOpeningInvoice();
            objopinvoice.DaGetcurrency(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnChangeCurrency")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeCurrency(string currencyexchange_gid)
        {
            MdlPmrTrnOpeningInvoice values = new MdlPmrTrnOpeningInvoice();
            objopinvoice.DaGetOnChangeCurrency(currencyexchange_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostOpeningIvoicedtl")]
        [HttpPost]
        public HttpResponseMessage PostOpeningIvoicedtl(OpeningIvoicedtl values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objopinvoice.DaPostOpeningIvoicedtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
