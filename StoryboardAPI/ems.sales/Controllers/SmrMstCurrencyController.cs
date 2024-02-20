using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.sales.Controllers
{
    [RoutePrefix("api/SmrMstCurrency")]
    [Authorize]
    public class SmrMstCurrencyController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrMstCurrency objsales = new DaSmrMstCurrency();

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

        [ActionName("PostSmrCurrency")]
        [HttpPost]
        public HttpResponseMessage PostSmrCurrency(currencyDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaPostSmrCurrency(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SmrCurrencyUpdate")]
        [HttpPost]
        public HttpResponseMessage SmrCurrencyUpdate(currencyDetailsEdit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaSmrCurrencyUpdate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SmrCurrencySummaryDelete")]
        [HttpPost]
        public HttpResponseMessage PmrCurrencySummaryDelete(currencyDetailsDelete values)
        {
            objsales.DaSmrCurrencySummaryDelete(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDefaultCurrency")]
        [HttpGet]
        public HttpResponseMessage GetDefaultCurrency()
        {
            MdlSmrMstCurrency values = new MdlSmrMstCurrency();
            objsales.DaGetDefaultCurrency(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}