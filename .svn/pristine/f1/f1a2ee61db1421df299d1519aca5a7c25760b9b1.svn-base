using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.sales.Models;
using ems.sales.DataAccess;

namespace ems.sales.Controllers
{
    [RoutePrefix("api/SmrMstTaxSummary")]
    [Authorize]
    public class SmrMstTaxSummaryController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrMstTaxSummary objDaSales = new DaSmrMstTaxSummary();
        // Module Summary
        [ActionName("GetTaxSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaxSummary()
        {
            MdlSmrMstTaxSummary values = new MdlSmrMstTaxSummary();
            objDaSales.DaGetTaxSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post Tax
        [ActionName("PostTax")]
        [HttpPost]
        public HttpResponseMessage PostTax(smrtax_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaPostTax(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatedTaxSummary")]
        [HttpPost]
        public HttpResponseMessage UpdatedTaxSummary(smrtax_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaUpdatedTax(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deleteTaxSummary")]
        [HttpGet]
        public HttpResponseMessage deleteTaxSummary(string tax_gid)
        {
            smrtax_list objresult = new smrtax_list();
            objDaSales.DadeleteTaxSummary(tax_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }



    }


}
