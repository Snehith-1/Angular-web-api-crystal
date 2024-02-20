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
    [RoutePrefix("api/PmrTrnPurchaseOrder")]
    [Authorize]
    public class PmrTrnPurchaseOrderController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrTrnPurchaseOrder objpurchase = new DaPmrTrnPurchaseOrder();

        [ActionName("GetPurchaseOrderSummary")]
        [HttpGet]
        public HttpResponseMessage GetPurchaseOrderSummary()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetPurchaseOrderSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("GetBranch")]
        [HttpGet]
        public HttpResponseMessage GetBranch()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetBranch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTax")]
        [HttpGet]
        public HttpResponseMessage GetTax()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetTax(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTax4Dtl")]
        [HttpGet]
        public HttpResponseMessage GetTax4Dtl()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetTax4Dtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVendor")]
        [HttpGet]
        public HttpResponseMessage GetVendor()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetVendor(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDispatchToBranch")]
        [HttpGet]
        public HttpResponseMessage GetDispatchToBranch()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetDispatchToBranch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCurrency")]
        [HttpGet]
        public HttpResponseMessage GetCurrency()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetCurrency(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductCode")]
        [HttpGet]
        public HttpResponseMessage GetProductCode()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetProductCode(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetViewPurchaseOrderSummary")]
        [HttpGet]
        public HttpResponseMessage GetViewPurchaseOrderSummary(string purchaseorder_gid)
        {
            MdlPmrTrnPurchaseOrder objresult = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetViewPurchaseOrderSummary(purchaseorder_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetOnChangeBranch")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeBranch(string branch_gid)
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetOnChangeBranch(branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnChangeProductCode")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeProductCode(string product_code)
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetOnChangeProductCode(product_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnChangeProductName")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeProductName(string product_gid)
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetOnChangeProductName(product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnChangeVendor")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeVendor(string vendorregister_gid)
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetOnChangeVendor(vendorregister_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProduct")]
        [HttpGet]
        public HttpResponseMessage GetProduct()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetProduct(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnChangeCurrency")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeCurrency(string currencyexchange_gid)
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetOnchangeCurrency(currencyexchange_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnAdd")]
        [HttpPost]
        public HttpResponseMessage GetOnAdd(productlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaGetOnAdd(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductSummary()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaProductSummary(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Submit

        [ActionName("ProductSubmit")]
        [HttpPost]
        public HttpResponseMessage ProductSubmit(GetViewPurchaseOrder values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaProductSubmit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteProductSummary")]
        [HttpGet]
        public HttpResponseMessage DeleteProductSummary(string tmppurchaseorderdtl_gid)
        {
            productlist objresult = new productlist();
            objpurchase.DaDeleteProductSummary(tmppurchaseorderdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //mailfunction

        [ActionName("GetTemplatelist")]
        [HttpGet]
        public HttpResponseMessage GetTemplatelist()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetTemplatelist(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTemplate")]
        [HttpGet]
        public HttpResponseMessage GetTemplate(string template_gid)
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetTemplatet(template_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostMail")]
        [HttpPost]
        public HttpResponseMessage PostMail()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objpurchase.DaPostMail(httpRequest, getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        //download Report files
        [ActionName("GetPurchaseOrderRpt")]
        [HttpGet]
        public HttpResponseMessage GetPurchaseOrderRpt(string purchaseorder_gid)
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            var ls_response = new Dictionary<string, object>();
            ls_response = objpurchase.DaGetPurchaseOrderRpt(purchaseorder_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, ls_response);
        }
        [ActionName("GetFromMailDropdown")]
        [HttpGet]
        public HttpResponseMessage GetFromMailDropdown()
        {
            MdlPmrTrnPurchaseOrder values = new MdlPmrTrnPurchaseOrder();
            objpurchase.DaGetFromMailDropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}