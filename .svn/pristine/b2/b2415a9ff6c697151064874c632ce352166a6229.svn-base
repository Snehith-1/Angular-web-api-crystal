using ems.einvoice.DataAccess;
using ems.einvoice.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.einvoice.DataAccess;
using ems.einvoice.Models;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.einvoice.Controllers
{
    [RoutePrefix("api/EinvoiceProduct")]
    [Authorize]
    public class EinvoiceProductController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaProduct objDaPurchase = new DaProduct();

        // Module Summary

        [ActionName("GetProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductSummary()
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaGetProductSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       

        [ActionName("Getproducttypedropdown")]
        [HttpGet]
        public HttpResponseMessage Getproducttypedropdown()
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaGetproducttypedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getproductgroupdropdown")]
        [HttpGet]
        public HttpResponseMessage Getproductgroupdropdown()
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaGetproductgroupdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Gethsngroupdropdown")]
        [HttpGet]
        public HttpResponseMessage Gethsngroupdropdown()
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaGethsngroupdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnChangehsngroup")]
        [HttpGet]
        public HttpResponseMessage GetOnChangehsngroup(string hsngroup_code)
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaGetOnChangehsngroup(hsngroup_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getproductunitclassdropdown")]
        [HttpGet]
        public HttpResponseMessage Getproductunitclassdropdown()
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaGetproductunitclassdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getproductunitdropdown")]
        [HttpGet]
        public HttpResponseMessage Getproductunitdropdown()
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaGetproductunitdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcurrencydropdown")]
        [HttpGet]
        public HttpResponseMessage Getcurrencydropdown()
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaGetcurrencydropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("PostProduct")]
        [HttpPost]
        public HttpResponseMessage PostProduct(product_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostProduct(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatedProduct")]
        [HttpPost]
        public HttpResponseMessage UpdatedProduct(product_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaUpdatedProduct(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("deleteProductSummary")]
        [HttpGet]
        public HttpResponseMessage deleteProductSummary(string params_gid)
        {
            product_list objresult = new product_list();
            objDaPurchase.DadeleteProductSummary(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("editProductSummary")]
        [HttpGet]
        public HttpResponseMessage editProductSummary(string product_gid)
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaeditProductSummary(product_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EinvoiceProductImportExcel")]
        [HttpPost]
        public HttpResponseMessage EinvoiceProductImportExcel()
        {
            HttpRequest httpRequest;
            product_list values = new product_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            httpRequest = HttpContext.Current.Request;
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result productresult = new result();
            objDaPurchase.DaEinvoiceProductImportExcel(httpRequest, getsessionvalues.user_gid, productresult, values);
            return Request.CreateResponse(HttpStatusCode.OK, productresult);
        }
        [ActionName("GetProductDocumentlist")]
        [HttpGet]
        public HttpResponseMessage GetProductDocumentlist()
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaGetProductDocumentlist(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductDocumentDtllist")]
        [HttpGet]
        public HttpResponseMessage GetProductDocumentDtllist(string document_gid)
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaGetProductDocumentDtllist(document_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}