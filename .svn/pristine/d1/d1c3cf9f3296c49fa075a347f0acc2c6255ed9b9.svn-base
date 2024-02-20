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
using System.Configuration;
using System.IO;


namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrMstVendorRegister")]
    [Authorize]
    public class PmrMstVendorRegisterController:ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrMstVendorRegister objpurchase = new DaPmrMstVendorRegister();
      
        // Module Summary
        [ActionName("GetVendorregisterSummary")]
        [HttpGet]
        public HttpResponseMessage GetVendorregisterSummary()
        {
            MdlPmrMstVendorRegister values = new MdlPmrMstVendorRegister();
            objpurchase.DaGetVendorRegisterSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }


        [ActionName("Getcountry")]
        [HttpGet]
        public HttpResponseMessage Getcountry()
        {
            MdlPmrMstVendorRegister values = new MdlPmrMstVendorRegister();
            objpurchase.DaGetcountry(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("Getcurency")]
        [HttpGet]
        public HttpResponseMessage Getcurency()
        {
            MdlPmrMstVendorRegister values = new MdlPmrMstVendorRegister();
            objpurchase.DaGetcurrency(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("Gettax")]
        [HttpGet]
        public HttpResponseMessage Gettax()
        {
            MdlPmrMstVendorRegister values = new MdlPmrMstVendorRegister();
            objpurchase.DaGettax(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

    
        [ActionName("PostVendorRegister")]
        [HttpPost]
        public HttpResponseMessage PostVendorRegister(vendor_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostVendorRegister(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVendorRegisterDetail")]
        [HttpGet]
        public HttpResponseMessage GetVendorRegisterDetail( string vendorregister_gid)
        {
            MdlPmrMstVendorRegister objresult = new MdlPmrMstVendorRegister();
            objpurchase.DaGetVendorRegisterDetail( vendorregister_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostVendorRegisterUpdate")]
        [HttpPost]
        public HttpResponseMessage PostVendorRegisterUpdate(vendor_listaddinfo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostVendorRegisterUpdate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("VendorRegisterSummaryDelete")]
        [HttpGet]
        public HttpResponseMessage VendorRegisterSummaryDelete(string vendorregister_gid)
        {
            vendor_listaddinfo objresult = new vendor_listaddinfo();
            objpurchase.DaVendorRegisterSummaryDelete(vendorregister_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("PostVendorRegisterAdditionalInformation")]
        [HttpPost]
        public HttpResponseMessage PostVendorRegisterAdditionalInformation(vendor_listaddinfo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostVendorRegisterAdditionalInformation(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDocumentType")]
        [HttpGet]
        public HttpResponseMessage GetDocumentType()
        {
            MdlPmrMstVendorRegister values = new MdlPmrMstVendorRegister();
            objpurchase.DaGetDocumentType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        public HttpResponseMessage VendorImportExcel()
        {
            HttpRequest httpRequest;
            product_list values = new product_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            httpRequest = HttpContext.Current.Request;
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objresult  = new result();
            objpurchase.DaVendorImportExcel(httpRequest, getsessionvalues.user_gid, objresult, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateVendorStatus")]
        [HttpPost]
        public HttpResponseMessage UpdateVendorStatus(ActiveStatus_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaUpdateVendorStatus(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("GetVendorReportExport")]
        //[HttpPost]

        public HttpResponseMessage GetVendorReportExport()
        {
            MdlPmrMstVendorRegister values = new MdlPmrMstVendorRegister();
            objpurchase.DaGetVendorReportExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostdocumentImage")]
        [HttpPost]

        public HttpResponseMessage PostdocumentImage()
        {
            HttpRequest httpRequest;
            //Postassetlocationcreation values = new Postassetlocationcreation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objpurchase.DaPostdocumentImage(httpRequest, objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetDocumentdtl")]
        [HttpGet]
        public HttpResponseMessage GetDocumentdtl(string vendorregister_gid)
        {
            MdlPmrMstVendorRegister objresult = new MdlPmrMstVendorRegister();
            objpurchase.DaGetDocumentdtl(vendorregister_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}