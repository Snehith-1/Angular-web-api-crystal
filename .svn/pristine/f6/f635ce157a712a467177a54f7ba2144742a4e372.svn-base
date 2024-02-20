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

namespace ems.einvoice.Controllers
{
    [RoutePrefix("api/Product")]
    [Authorize]
    public class ProductController : ApiController
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
        //[ActionName("GetProductCodeCheck")]
        //[HttpPost]
        //public HttpResponseMessage GetProductCodeCheck(product_list values)
        //{
        //    //product_list values = new product_list();
        //    objDaPurchase.DaGetProductcodeCheck(values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

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
            return Request.CreateResponse(HttpStatusCode.OK, true);
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
            objDaPurchase.DadeleteProductSummary(params_gid,  objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("editProductSummary")]
        [HttpGet]
        public HttpResponseMessage editProductSummary(string product_gid)
        {
            MdlProduct objresult = new MdlProduct();
            objDaPurchase.DaeditProductSummary( product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getproductunitclassdropdownonchange")]
        [HttpGet]
        public HttpResponseMessage Getproductunitclassdropdownonchange(string user_gid, string productuomclass_gid)
        {
            MdlProduct objresult = new MdlProduct();
            objDaPurchase.DaGetproductunitclassdropdownonchange(user_gid, productuomclass_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getproductunitclassdropdownonchangename")]
        [HttpGet]
        public HttpResponseMessage Getproductunitclassdropdownonchangename(string user_gid, string productuomclass_name)
        {
            MdlProduct objresult = new MdlProduct();
            objDaPurchase.DaGetproductunitclassdropdownonchangename(user_gid, productuomclass_name, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("UpdatedProductcost")]
        [HttpPost]
        public HttpResponseMessage UpdatedProductcost(product_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaUpdatedProductcost(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("enquiryProductSummary")]
        [HttpGet]
        public HttpResponseMessage enquiryProductSummary(string user_gid, string product_gid)
        {
            product_list objresult = new product_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaenquiryProductSummary(getsessionvalues.user_gid, product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetProductattributesSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductattributesSummary(string user_gid, string product_gid)
        {
            MdlProduct objresult = new MdlProduct();
            objDaPurchase.DaGetProductattributesSummary(user_gid, product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        //[ActionName("ProductUploadExcel")]
        //[HttpPost]
        //public HttpResponseMessage ProductUploadExcel()
        //{
        //    HttpRequest httpRequest;
        //    product_list values = new product_list();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    httpRequest = HttpContext.Current.Request;
        //    result objResult = new result();
        //    objDaPurchase.DaProductUploadExcel(httpRequest, getsessionvalues.user_gid, objResult, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, objResult);
        //}
        [ActionName("GetProductReportExport")]
        [HttpGet]
        public HttpResponseMessage GetProductReportExport()
        {
            MdlProduct values = new MdlProduct();
            objDaPurchase.DaGetProductReportExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}