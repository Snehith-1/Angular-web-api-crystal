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
    [RoutePrefix("api/PmrTrnRequestforQuote")]
    [Authorize]
  public class PmrTrnRequestforQuoteController : ApiController

    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrTrnRequestforQuote objpurchase = new DaPmrTrnRequestforQuote();
   
    [ActionName("GetRequestforQuoteSummary")]
    [HttpGet]
    public HttpResponseMessage GetRequestforQuoteSummary()
    {
        MdlPmrTrnRequestforQuote values = new MdlPmrTrnRequestforQuote();
        objpurchase.DaGetRequestforQuoteSummary(values);
        return Request.CreateResponse(HttpStatusCode.OK, values);

    }

        [ActionName("GetEnquirySelect")]
        [HttpGet]
        public HttpResponseMessage GetEnquirySelect()
        {
            MdlPmrTrnRequestforQuote values = new MdlPmrTrnRequestforQuote();
            objpurchase.DaGetEnquirySelect(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEnquiryaddProceed")]
        [HttpGet]
        public HttpResponseMessage GetEnquiryaddProceed(string purchaserequisition_gid)
        {
            MdlPmrTrnRequestforQuote values = new MdlPmrTrnRequestforQuote();
            objpurchase.DaGetEnquiryaddProceed(purchaserequisition_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEnquiryaddConfirm")]
        [HttpGet]
        public HttpResponseMessage GetEnquiryaddConfirm(string purchaserequisition_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPmrTrnRequestforQuote values = new MdlPmrTrnRequestforQuote();
            objpurchase.DaGetEnquiryaddConfirm(purchaserequisition_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEnquiryaddConfirm1")]
        [HttpGet]
        public HttpResponseMessage GetEnquiryaddConfirm1(string purchaserequisition_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPmrTrnRequestforQuote values = new MdlPmrTrnRequestforQuote();
            objpurchase.DaGetEnquiryaddConfirm1(purchaserequisition_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEnquiryproceed")]
        [HttpPost]
        public HttpResponseMessage GetEnquiryproceed(GetEnquiryproceed values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaGetEnquiryproceed(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEnquiryproceedConfirm")]
        [HttpPost]
        public HttpResponseMessage GetEnquiryproceedConfirm(GetEnquiryproceedConfirm values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaGetEnquiryproceedConfirm(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetRequestforQuoteSummarygrid")]
        [HttpGet]
        public HttpResponseMessage GetRequestforQuoteSummarygrid(string enquiry_gid)
        {
            MdlPmrTrnRequestforQuote values = new MdlPmrTrnRequestforQuote();
            objpurchase.DaGetRequestforQuoteSummarygrid(enquiry_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEnquirySelectgrid")]
        [HttpGet]
        public HttpResponseMessage GetEnquirySelectgrid(string purchaserequisition_gid)
        {
            MdlPmrTrnRequestforQuote values = new MdlPmrTrnRequestforQuote();
            objpurchase.DaGetEnquirySelectgrid(purchaserequisition_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetRequestForQuotationView")]
        [HttpGet]
        public HttpResponseMessage GetRequestForQuotationView(string enquiry_gid)
        {
            MdlPmrTrnRequestforQuote objresult = new MdlPmrTrnRequestforQuote();
            objpurchase.DaGetRequestForQuotationView(enquiry_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}