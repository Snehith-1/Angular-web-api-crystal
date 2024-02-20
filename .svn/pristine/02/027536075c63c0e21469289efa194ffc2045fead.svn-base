using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.IO;


namespace ems.crm.Controllers
{
    [Authorize]
    [RoutePrefix("api/ShopifyCustomer")]
    public class ShopifyCustomerController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaShopifyCustomer objDaleadbank = new DaShopifyCustomer();
        // code By snehith
        [ActionName("GetShopifyCustomer")]
        [HttpGet]
        public HttpResponseMessage GetShopifyCustomer()
        {
            get values = new get();
            values = objDaleadbank.DaGetShopifyCustomer();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetShopifyCustomersList")]
        [HttpGet]
        public HttpResponseMessage GetShopifyCustomersList()
        {
            MdlShopifyCustomer values = new MdlShopifyCustomer();
            objDaleadbank.DaGetShopifyCustomersList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetShopifyCustomersAssignedList")]
        [HttpGet]
        public HttpResponseMessage GetShopifyCustomersAssignedList()
        {
            MdlShopifyCustomer values = new MdlShopifyCustomer();
            objDaleadbank.DaGetShopifyCustomersAssignedList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetShopifyCustomersUnassignedList")]
        [HttpGet]
        public HttpResponseMessage GetShopifyCustomersUnassignedList()
        {
            MdlShopifyCustomer values = new MdlShopifyCustomer();
            objDaleadbank.DaGetShopifyCustomersUnassignedList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLeadmoved")]
        [HttpPost]
        public HttpResponseMessage GetLeadmoved(shopifycustomermovingtolead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaleadbank.DaGetLeadmoved(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("GetCustomerTotalCount")]
        [HttpGet]
        public HttpResponseMessage GetCustomerTotalCount()
        {
            MdlShopifyCustomer values = new MdlShopifyCustomer();
            objDaleadbank.DaGetCustomerTotalCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerAssignedCount")]
        [HttpGet]
        public HttpResponseMessage GetCustomerAssignedCount()
        {
            MdlShopifyCustomer values = new MdlShopifyCustomer();
            objDaleadbank.DaGetCustomerAssignedCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
            [ActionName("GetCustomerUnassignedCount")]
            [HttpGet]
            public HttpResponseMessage GetCustomerUnassignedCount()
            {
                MdlShopifyCustomer values = new MdlShopifyCustomer();
                objDaleadbank.DaGetCustomerUnassignedCount(values);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
        // code By snehith
        [ActionName("GetShopifyOrder")]
        [HttpGet]
        public HttpResponseMessage GetShopifyOrder()
        {
            getorders values = new getorders();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            values = objDaleadbank.DaGetShopifyOrder(getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetShopifyOrderSummary")]
        [HttpGet]
        public HttpResponseMessage GetShopifyOrderSummary()
        {
            MdlShopifyCustomer values = new MdlShopifyCustomer();
            objDaleadbank.DaGetShopifyOrderSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetShopifyPaymentSummary")]
        [HttpGet]
        public HttpResponseMessage GetShopifyPaymentSummary()
        {
            MdlShopifyCustomer values = new MdlShopifyCustomer();
            objDaleadbank.DaGetShopifyPaymentSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetShopifyOrderCountSummary")]
        [HttpGet]
        public HttpResponseMessage GetShopifyOrderCountSummary()
        {
            MdlShopifyCustomer values = new MdlShopifyCustomer();
            objDaleadbank.DaGetShopifyOrderCountSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Sendorder")]
        [HttpPost]
        public HttpResponseMessage Sendorder(shopifyordermovingtoorder values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaleadbank.DaSendorder(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("Sendpayment")]
        [HttpPost]
        public HttpResponseMessage Sendpayment(shopifyordermovingtopayment values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaleadbank.DaSendpayment(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("Sendinventorystock")]
        [HttpPost]
        public HttpResponseMessage Sendinventorystock(shopifyinventorystocksend values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaleadbank.DaSendinventorystock(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        
    }
}