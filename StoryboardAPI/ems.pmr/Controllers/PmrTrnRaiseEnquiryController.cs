
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
    [RoutePrefix("api/PmrTrnRaiseEnquiry")]
    [Authorize]
    public class PmrTrnRaiseEnquiryController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrTrnRaiseEnquiry objpurchase = new DaPmrTrnRaiseEnquiry();


        [ActionName("GetProductGroup")]

        [HttpGet]

        public HttpResponseMessage GetProductGroup()

        {

            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();

            objpurchase.DaGetProductGrp(values);

            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetProduct")]

        [HttpGet]

        public HttpResponseMessage GetProduct()

        {

            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();

            objpurchase.DaGetProducts(values);

            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("GetProductsSummary")]

        [HttpGet]

        public HttpResponseMessage GetProductSummary()

        {

            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            getsessionvalues = Objgetgid.gettokenvalues(token);

            objpurchase.DaProductsSummary(getsessionvalues.user_gid, values);

            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetProductUnit")]

        [HttpGet]

        public HttpResponseMessage GetProductUnit()

        {

            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();

            objpurchase.DaGetProductunit(values);

            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetOnChangeProductsName")]

        [HttpGet]

        public HttpResponseMessage GetOnChangeProductsName(string product_gid)

        {

            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();

            objpurchase.DaGetOnChangeProductsName(product_gid, values);

            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        

        [ActionName("PostOnAdds")]
        [HttpPost]
        public HttpResponseMessage PostOnAdds(productsummarys_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostOnAdds(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDirectEnquiryEditProductSummary")]

        [HttpGet]

        public HttpResponseMessage GetDirectEnquiryEditProductSummary(string tmpsalesenquiry_gid)

        {

            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();

            objpurchase.DaGetDirectEnquiryEditProductSummary(tmpsalesenquiry_gid, values);

            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        // Tax 1 dropdown

        [ActionName("GetFirstTax")]
        [HttpGet]
        public HttpResponseMessage GetFirstTax()
        {
            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();
            objpurchase.DaGetFirstTax(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 2 dropdown

        [ActionName("GetSecondTax")]
        [HttpGet]
        public HttpResponseMessage GetSecondTax()
        {
            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();
            objpurchase.DaGetSecondTax(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 3 dropdown

        [ActionName("GetThirdTax")]
        [HttpGet]
        public HttpResponseMessage GetThirdTax()
        {
            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();
            objpurchase.DaGetThirdTax(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 4 dropdown

        [ActionName("GetFourthTax")]
        [HttpGet]
        public HttpResponseMessage GetFourthTax()
        {
            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();
            objpurchase.DaGetFourthTax(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Branch dropdown

        [ActionName("GetBranchDet")]
        [HttpGet]
        public HttpResponseMessage GetBranchDet()
        {
            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();
            objpurchase.DaGetBranchDet(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Terms And Condition dropdown

        [ActionName("GetTerms")]
        [HttpGet]
        public HttpResponseMessage GetTerms()
        {
            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();
            objpurchase.DaGetTerms(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVendorName")]

        [HttpGet]

        public HttpResponseMessage GetVendorName()

        {

            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();

            objpurchase.DaGetVendorName( values);

            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("GetVendorDtl")]

        [HttpGet]

        public HttpResponseMessage GetVendorDtl(string vendor_gid)

        {

            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();

            objpurchase.DaGetVendorDtl(vendor_gid, values);

            return Request.CreateResponse(HttpStatusCode.OK, values);


        }
        [ActionName("GetEmployeePerson")]
        [HttpGet]
        public HttpResponseMessage GetEmployeePerson()
        
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaGetEmployeePerson(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDeleteEnquiryProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeleteEnquiryProductSummary(string tmpsalesenquiry_gid)
        {
            productsummarys_list objresult = new productsummarys_list();
            objpurchase.DaGetDeleteEnquiryProductSummary(tmpsalesenquiry_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("PostVendorEnquiry")] 
        [HttpPost]
        public HttpResponseMessage PostVendorEnquiry(PostAll values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostVendorEnquiry(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVendorEnquirySummary")]

        [HttpGet]

        public HttpResponseMessage GetVendorEnquirySummary()

        {

            MdlPmrTrnRaiseEnquiry values = new MdlPmrTrnRaiseEnquiry();

            objpurchase.DaGetVendorEnquirySummary(values);

            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("GetViewVendorEnquiry")]
        [HttpGet]
        public HttpResponseMessage GetViewVendorEnquiry(string enquiry_gid)
        {
            MdlPmrTrnRaiseEnquiry objresult = new MdlPmrTrnRaiseEnquiry();
            objpurchase.Daviewvendorenquiry(enquiry_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}