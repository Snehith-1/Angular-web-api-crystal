using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.einvoice.DataAccess;
using ems.einvoice.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.einvoice.Controllers
{
    [RoutePrefix("api/Ewaybill")]
    [Authorize]
    public class EwaybillController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaEwaybill objDaEwaybill = new DaEwaybill();

        [ActionName("Getewaybillsummary")]
        [HttpGet]
        public HttpResponseMessage Getewaybillsummary()
        {
            MdlEwaybill values = new MdlEwaybill();
            objDaEwaybill.Daewaybillsummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getewaybillinvoicesummary")]
        [HttpGet]
        public HttpResponseMessage Getewaybillinvoicesummary()
        {
            MdlEwaybill values = new MdlEwaybill();
            objDaEwaybill.Daewaybillinvoicesummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetewaybillInvoicedata")]
        [HttpGet]
        public HttpResponseMessage GetewaybillInvoicedata(string invoice_gid)
        {
            ewaybillinvoicedata_list values = new ewaybillinvoicedata_list();
            values = objDaEwaybill.DaewaybillInvoicedata(invoice_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Addewaybill")]
        [HttpPost]
        public HttpResponseMessage Addewaybill(addewaybill_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaEwaybill.DaAddewaybill(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}