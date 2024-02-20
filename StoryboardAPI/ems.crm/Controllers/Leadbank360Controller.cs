using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.crm.Controllers
{
    [Authorize]
    [RoutePrefix("api/Leadbank360")]
    public class Leadbank360Controller: ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLeadbank360 objDaLeadbank360 = new DaLeadbank360();
        [ActionName("GetWhatsappLeadContact")]
        [HttpGet]
        public HttpResponseMessage GetWhatsappLeadContact(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetWhatsappLeadContact(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreateContact")]
        [HttpPost]
        public HttpResponseMessage CreateContact(mdlCreateContactInput values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objResult = objDaLeadbank360.dacreatecontact(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetWhatsappLeadMessage")]
        [HttpGet]
        public HttpResponseMessage GetWhatsappLeadMessage(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetWhatsappLeadMessage(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostLeadWhatsappMessage")]
        [HttpPost]
        public HttpResponseMessage PostLeadWhatsappMessage(leadwhatsappsendmessage values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objResult = objDaLeadbank360.DaPostLeadWhatsappMessage(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("LeadMailSend")]
        [HttpPost]
        public HttpResponseMessage LeadMailSend(leadmailsummary_list values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objDaLeadbank360.DaLeadMailSend(values, getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetEmailSendDetails")]
        [HttpGet]
        public HttpResponseMessage GetEmailSendDetails(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetEmailSendDetails(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("LeadMailUpload")]
        [HttpPost]
        public HttpResponseMessage LeadMailUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaLeadbank360.DaLeadMailUpload(httpRequest, getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetLeadOrderDetails")]
        [HttpGet]
        public HttpResponseMessage GetLeadOrderDetails(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetLeadOrderDetails(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLeadQuotationDetails")]
        [HttpGet]
        public HttpResponseMessage GetLeadQuotationDetails(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetLeadQuotationDetails(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLeadInvoiceDetails")]
        [HttpGet]
        public HttpResponseMessage GetLeadInvoiceDetails(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetLeadInvoiceDetails(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLeadCountDetails")]
        [HttpGet]
        public HttpResponseMessage GetLeadCountDetails(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetLeadCountDetails(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Document Summary
        [ActionName("GetLeadDocumentDetails")]
        [HttpGet]
        public HttpResponseMessage GetLeadDocumentDetails(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetLeadDocumentDetails(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Document upload
        [ActionName("LeadDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage LeadDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaLeadbank360.DaLeadDocumentUpload(httpRequest, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        //Document Download

        [ActionName("LeadDocumentdownload")]
        [HttpGet]
        public HttpResponseMessage LeadDocumentdownload(string document_gid)
        {
            MdlLeadbank360 objresult = new MdlLeadbank360();
            objDaLeadbank360.DaLeadDocumentdownload(document_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        // Notes summary
        [ActionName("GetNotesSummary")]
        [HttpGet]
        public HttpResponseMessage GetNotesSummary(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetNotesSummary(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Notes upload
        [ActionName("LeadNotesUpload")]
        [HttpPost]
        public HttpResponseMessage LeadNotesUpload(notes values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objDaLeadbank360.DaLeadNotesUpload(values, getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Lead Basic Details
        [ActionName("GetLeadBasicDetails")]
        [HttpGet]
        public HttpResponseMessage GetLeadBasicDetails(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetLeadBasicDetails(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //editcontact//
        [ActionName("Getupdatecontactdetails")]
        [HttpPost]
        public HttpResponseMessage Getupdatecontactdetails(contactedit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
           // getsessionvalues = objgetgid.gettokenvalues(token);
            objDaLeadbank360.DaGetupdatecontactdetails(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get edit contact details
        [ActionName("GetEditContactdetails")]
        [HttpGet]
        public HttpResponseMessage GetEditContactdetails(string leadbank_gid)
        {
            MdlLeadbank360 objresult = new MdlLeadbank360();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLeadbank360.DaGetEditContactdetails(leadbank_gid, objresult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        //Get sales Enquiry details
        [ActionName("GetEnquiryDetails")]
        [HttpGet]
        public HttpResponseMessage GetEnquiryDetails(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetEnquiryDetails(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //GetGidDetails
        [ActionName("GetGidDetails")]
        [HttpGet]
        public HttpResponseMessage GetGidDetails(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            objDaLeadbank360.DaGetGidDetails(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add to customer
        [ActionName("Addtocustomer")]
        [HttpGet]
        public HttpResponseMessage Addtocustomer(string leadbank_gid)
        {
            MdlLeadbank360 values = new MdlLeadbank360();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLeadbank360.DaAddtocustomer(values, leadbank_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

       

    }
}