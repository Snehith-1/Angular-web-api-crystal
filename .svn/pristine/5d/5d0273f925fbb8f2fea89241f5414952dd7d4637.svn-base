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

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrTrnPurchaseQuotation")]
    [Authorize]
    public class PmrTrnPurchaseQuotationController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrTrnPurchaseQuotaion objpurchase = new DaPmrTrnPurchaseQuotaion();

        [ActionName("GetPmrTrnPurchaseQuotation")]
        [HttpGet]
        public HttpResponseMessage GetPmrTrnPurchaseQuotation()
        {
            MdlPmrTrnPurchaseQuotation values = new MdlPmrTrnPurchaseQuotation();
            objpurchase.DaGetPmrTrnPurchaseQuotation(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("GetVendor")]
        [HttpGet]
        public HttpResponseMessage GetVendor()
        {
            MdlPmrTrnPurchaseQuotation values = new MdlPmrTrnPurchaseQuotation();
            objpurchase.DaGetVendor(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnChangeVendor")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeVendor(string vendor_gid)
        {
            MdlPmrTrnPurchaseQuotation values = new MdlPmrTrnPurchaseQuotation();
            objpurchase.DaGetOnChangeVendor(vendor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetOnChangeCurrency")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeCurrency(string currencyexchange_gid)
        {
            MdlPmrTrnPurchaseQuotation values = new MdlPmrTrnPurchaseQuotation();
            objpurchase.DaGetOnchangeCurrency(currencyexchange_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Currency dropdown

        [ActionName("GetCurrencyDtl")]
        [HttpGet]
        public HttpResponseMessage GetCurrencyDtl()
        {
            MdlPmrTrnPurchaseQuotation values = new MdlPmrTrnPurchaseQuotation();
            objpurchase.DaGetCurrencyDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostAddProduct")]
        [HttpPost]
        public HttpResponseMessage PostAddProduct(summaryprod_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostAddProduct(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTempProductsSummary")]
        [HttpGet]
        public HttpResponseMessage GetTempProductsSummary()
        {
            MdlPmrTrnPurchaseQuotation values = new MdlPmrTrnPurchaseQuotation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaGetTempProductsSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("PostDirectQuotation")]
        [HttpPost]
        public HttpResponseMessage PostDirectQuotation( post_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostDirectQuotation(getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete product summary//
        [ActionName("GetDeleteDirectPOProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeleteDirectPOProductSummary(string quotationdtl_gid)
        {
            quotationPO_list objresult = new quotationPO_list();
            objpurchase.GetDeleteDirectPOProductSummary(quotationdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        //// Terms And Condition dropdown

        [ActionName("GetTermsandConditions")]
        [HttpGet]
        public HttpResponseMessage GetTermsandConditions()
        {
            MdlPmrTrnPurchaseQuotation values = new MdlPmrTrnPurchaseQuotation();
            objpurchase.DaGetTermsandConditions(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // On change t and c
        [ActionName("GetOnChangeTerms")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeTerms(string termsconditions_gid)
        {
            MdlPmrTrnPurchaseQuotation values = new MdlPmrTrnPurchaseQuotation();
            objpurchase.DaGetOnChangeTerms(termsconditions_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

    }
}