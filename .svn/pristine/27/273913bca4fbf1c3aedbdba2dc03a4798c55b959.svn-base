using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using static ems.sales.Models.MdlSmrMstCurrency;

namespace ems.sales.Controllers
{
    [RoutePrefix("api/SmrMstCurrency")]
    [Authorize]
    public class SmrMstCurrencyController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrMstCurrency objsales = new DaSmrMstCurrency();
        // Module Summary
        [ActionName("GetSmrCurrencySummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrCurrencySummary()
        {
            MdlSmrMstCurrency values = new MdlSmrMstCurrency();
            objsales.DaGetSmrCurrencySummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSmrCountryDtl")]
        [HttpGet]
        public HttpResponseMessage GetSmrCountryDtl()
        {
            MdlSmrMstCurrency values = new MdlSmrMstCurrency();
            objsales.DaGetSmrCountryDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Currency
        [ActionName("PostSmrCurrency")]
        [HttpPost]
        public HttpResponseMessage PostSmrCurrency(Getsales_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaPostSmrCurrency(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SmrCurrencyUpdate")]
        [HttpPost]
        public HttpResponseMessage SmrCurrencyUpdate(Getsales_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaSmrCurrencyUpdate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SmrCurrencySummaryDelete")]
        [HttpGet]
        public HttpResponseMessage PmrCurrencySummaryDelete(string currencyexchange_gid)
        {
            Getsales_list objresult = new Getsales_list();
            objsales.DaSmrCurrencySummaryDelete(currencyexchange_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}