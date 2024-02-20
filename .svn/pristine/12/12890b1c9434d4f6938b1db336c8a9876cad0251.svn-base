using ems.pmr.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.pmr.Models;

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrTrnPurchaseRequisition")]
    [Authorize]
    public class PmrTrnPurchaseRequisitionController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrTrnPurchaseRequisition objDapurchase = new DaPmrTrnPurchaseRequisition();

        // Purchase requisition Summary

        [ActionName("GetPmrTrnPurchaseRequisition")]
        [HttpGet]
        public HttpResponseMessage GetPmrTrnPurchaseRequisition()
        {
            MdlPmrTrnPurchaseRequisition values = new MdlPmrTrnPurchaseRequisition();
            objDapurchase.DaGetPmrTrnPurchaseRequisition(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //branch name

        [ActionName("GetBranch1")]
        [HttpGet]
        public HttpResponseMessage GetBranch1()
        {
            MdlPmrTrnPurchaseRequisition values = new MdlPmrTrnPurchaseRequisition();
            objDapurchase.DaGetBranch1(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //department and name  

        [ActionName("Getuserdtl")]
        [HttpGet]
        public HttpResponseMessage Getuserdtl()
        {
            MdlPmrTrnPurchaseRequisition values = new MdlPmrTrnPurchaseRequisition();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDapurchase.DaGetuserdtl(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcostcenter")]
        [HttpGet]
        public HttpResponseMessage Getcostcenter()
        {
            MdlPmrTrnPurchaseRequisition values = new MdlPmrTrnPurchaseRequisition();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDapurchase.DaGetcostcenter(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnChangecostcenter")]
        [HttpGet]
        public HttpResponseMessage GetOnChangecostcenter(string costcenter_gid)
        {
            MdlPmrTrnPurchaseRequisition values = new MdlPmrTrnPurchaseRequisition();            
            objDapurchase.DaGetOnChangecostcenter(costcenter_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProduct1")]
        [HttpGet]
        public HttpResponseMessage GetProduct1()
        {
            MdlPmrTrnPurchaseRequisition values = new MdlPmrTrnPurchaseRequisition();
            objDapurchase.DaGetProduct1(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductCode1")]
        [HttpGet]
        public HttpResponseMessage GetProductCode1()
        {
            MdlPmrTrnPurchaseRequisition values = new MdlPmrTrnPurchaseRequisition();
            objDapurchase.DaGetProductCode1(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnChangeProductCode")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeProductCode(string product_code)
        {
            MdlPmrTrnPurchaseRequisition values = new MdlPmrTrnPurchaseRequisition();
            objDapurchase.DaGetOnChangeProductCode(product_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnChangeProductName")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeProductName(string product_gid)
        {
            MdlPmrTrnPurchaseRequisition values = new MdlPmrTrnPurchaseRequisition();
            objDapurchase.DaGetOnChangeProductName(product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostOnAddpr")]
        [HttpPost]
        public HttpResponseMessage PostOnAddpr(productlist1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDapurchase.DaPostOnAdd(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductSummary()
        {
            MdlPmrTrnPurchaseRequisition values = new MdlPmrTrnPurchaseRequisition();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDapurchase.DaProductSummary(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDeletePrProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeletePrProductSummary(string tmppurchaserequisition_gid)
        {
            productsummary_list1 objresult = new productsummary_list1();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDapurchase.DaGetDeletePrProductSummary(getsessionvalues.user_gid, tmppurchaserequisition_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("PostPurchaseRequisition")]
        [HttpPost]
        public HttpResponseMessage PostPurchaseRequisition(PostAllPR values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDapurchase.DaPostPurchaseRequisition(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductdetails")]
        [HttpGet]
        public HttpResponseMessage GetProductdetails(string purchaserequisition_gid)
        {
            MdlPmrTrnPurchaseRequisition objresult = new MdlPmrTrnPurchaseRequisition();
            objDapurchase.DaGetProductdetails(purchaserequisition_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // PURCHASE REQUISITION VIEW

        [ActionName("GetPurchaseRequisitionView")]
        [HttpGet]
        public HttpResponseMessage GetPurchaseRequisitionView(string purchaserequisition_gid)
        {
            MdlPmrTrnPurchaseRequisition objresult = new MdlPmrTrnPurchaseRequisition();
            objDapurchase.DaGetPurchaseRequisitionView(purchaserequisition_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}