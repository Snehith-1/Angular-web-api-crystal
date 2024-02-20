using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.sales.Controllers

{
    [RoutePrefix("api/SmrRptcustomerledgerdetail")]
    [Authorize]
    public class SmrRptcustomerledgerdetailController:ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        Dacustomerledgerdetail objsales = new Dacustomerledgerdetail();

        [ActionName("GetCustomerledgersalesorder")]
        [HttpGet]
        public HttpResponseMessage GetCustomerledgersalesorder(string customer_gid)
        {
            Mdlcustomerledgerdetail values = new Mdlcustomerledgerdetail();
            objsales.DaGetcustomerledgersalesorder(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCustomerledgersalesorderdetail")]
        [HttpGet]
        public HttpResponseMessage GetCustomerledgersalesorderdetail(string salesorder_gid)
        {
            Mdlcustomerledgerdetail values = new Mdlcustomerledgerdetail();
            objsales.DaGetcustomerledgersalesorderdetail(salesorder_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerledgerinvoice")]
        [HttpGet]
        public HttpResponseMessage GetCustomerledgerinvoice(string customer_gid)
        {
            Mdlcustomerledgerdetail values = new Mdlcustomerledgerdetail();
            objsales.DaGetcustomerledgerinvoice(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerledgerinvoicedetail")]
        [HttpGet]
        public HttpResponseMessage GetCustomerledgerinvoicedetail(string invoice_gid)
        {
            Mdlcustomerledgerdetail values = new Mdlcustomerledgerdetail();
            objsales.DaGetcustomerledgerinvoicedetail(invoice_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerledgerpayment")]
        [HttpGet]
        public HttpResponseMessage GetCustomerledgerpayment(string customer_gid)
        {
            Mdlcustomerledgerdetail values = new Mdlcustomerledgerdetail();
            objsales.DaGetcustomerledgerpayment(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerledgerpaymentdetail")]
        [HttpGet]
        public HttpResponseMessage GetCustomerledgerpaymentdetail(string payment_gid)
        {
            Mdlcustomerledgerdetail values = new Mdlcustomerledgerdetail();
            objsales.DaGetcustomerledgerpaymentdetail(payment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerledgeroutstanding")]
        [HttpGet]
        public HttpResponseMessage GetCustomerledgeroutstanding(string customer_gid)
        {
            Mdlcustomerledgerdetail values = new Mdlcustomerledgerdetail();
            objsales.DaGetcustomerledgeroutstanding(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcustomerledgercount")]
        [HttpGet]
        public HttpResponseMessage Getcustomerledgercount(string customer_gid)
        {
            Mdlcustomerledgerdetail values = new Mdlcustomerledgerdetail();
            objsales.DaGetcustomerledgercount(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}