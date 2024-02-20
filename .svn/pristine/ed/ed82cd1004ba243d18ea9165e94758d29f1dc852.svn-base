
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

namespace ems.pmr.Controllers
{

    [RoutePrefix("api/PmrMstCurrency")]
    [Authorize]
    public class PmrMstCurrencyController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrMstCurrency objDaPurchase = new DaPmrMstCurrency();
        // Module Summary
        [ActionName("GetPmrCurrencySummary")]
        [HttpGet]
        public HttpResponseMessage GetPmrCurrencySummary()
        {
            MdlPmrMstCurrency values = new MdlPmrMstCurrency();
            objDaPurchase.DaGetPmrCurrencySummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPmrCountryDtl")]
        [HttpGet]
        public HttpResponseMessage GetPmrCountryDtl()
        {
            MdlPmrMstCurrency values = new MdlPmrMstCurrency();
            objDaPurchase.DaGetPmrCountryDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Currency
        [ActionName("PostPmrCurrency")]
        [HttpPost]
        public HttpResponseMessage PostPmrCurrency(currency_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostPmrCurrency(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PmrCurrencyUpdate")]
        [HttpPost]
        public HttpResponseMessage PmrCurrencyUpdate(currency_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPmrCurrencyUpdate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PmrCurrencySummaryDelete")]
        [HttpGet]
        public HttpResponseMessage PmrCurrencySummaryDelete(string currencyexchange_gid)
        {
            currency_list objresult = new currency_list();
            objDaPurchase.DaPmrCurrencySummaryDelete(currencyexchange_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetBreadCrumb")]
        [HttpGet]
        public HttpResponseMessage GetBreadCrumb(string user_gid, string module_gid)
        {
            MdlPmrMstCurrency objresult = new MdlPmrMstCurrency();
            objDaPurchase.DaGetBreadCrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}