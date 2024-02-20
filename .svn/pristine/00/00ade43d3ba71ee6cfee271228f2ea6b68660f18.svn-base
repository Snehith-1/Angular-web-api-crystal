using ems.einvoice.DataAccess;
using ems.einvoice.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.einvoice.Controllers
{

    [RoutePrefix("api/Receipt")]
    [Authorize]
    public class ReceiptController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaReceipt objDaReceipt = new DaReceipt();

        [ActionName("GetReceiptSummary")]
        [HttpGet]
        public HttpResponseMessage GetReceiptSummary()
        {
            MdlReceipt values = new MdlReceipt();
            objDaReceipt.DaGetReceiptSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getmodeofpayment")]
        [HttpGet]
        public HttpResponseMessage Getmodeofpayment()
        {
            MdlReceipt values = new MdlReceipt();
            objDaReceipt.DaGetmodeofpayment(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAddReceiptSummary")]
        [HttpGet]
        public HttpResponseMessage GetAddReceiptSummary()
        {
            MdlReceipt values = new MdlReceipt();
            objDaReceipt.DaGetAddReceiptSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMakeReceiptdata")]
        [HttpGet]
        public HttpResponseMessage GetMakeReceiptdata(string customer_gid)
        {
            MdlReceipt values = new MdlReceipt();
            objDaReceipt.DaGetMakeReceiptdata(values, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatedMakeReceipt")]
        [HttpPost]
        public HttpResponseMessage UpdatedMakeReceipt(updatereceipt_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaReceipt.DaUpdatedMakeReceipt(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deleteReceiptSummary")]
        [HttpGet]
        public HttpResponseMessage deleteReceiptSummary(string params_gid)
        {
            MdlReceipt objresult = new MdlReceipt();
            objDaReceipt.DadeleteReceiptSummary(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getreceiptdetails")]
        [HttpGet]
        public HttpResponseMessage Getreceiptdetails(string invoice_gid)
        {
            MdlReceipt objresult = new MdlReceipt();
            objDaReceipt.DaGetreceiptdetails(invoice_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }



    }
}