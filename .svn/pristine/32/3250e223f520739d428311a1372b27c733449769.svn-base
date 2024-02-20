using ems.hrm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.hrm.DataAccess;

namespace ems.hrm.Controllers
{
    [RoutePrefix("api/HRDocument")]
    [Authorize]
    public class HRDocumentController : ApiController
    {
        DaHRDocument objDaSysMstHRDocument = new DaHRDocument();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetSysHRDocument")]
        [HttpGet]
        public HttpResponseMessage GetSysHRDocument()
        {
            MdlHRDocument objhrdocument = new MdlHRDocument();
            objDaSysMstHRDocument.DaGetSysHRDocument(objhrdocument);
            return Request.CreateResponse(HttpStatusCode.OK, objhrdocument);
        }

        [ActionName("CreateSysHRDocument")]
        [HttpPost]
        public HttpResponseMessage CreateSysHRDocument(hrdocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSysMstHRDocument.DaCreateSysHRDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditSysHRDocument")]
        [HttpGet]
        public HttpResponseMessage EditSysHRDocument(string hrdocument_gid)
        {
            hrdocument values = new hrdocument();
            objDaSysMstHRDocument.DaEditSysHRDocument(hrdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSysHRDocument")]
        [HttpPost]
        public HttpResponseMessage UpdateSysHRDocument(hrdocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSysMstHRDocument.DaUpdateSysHRDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveSysHRDocument")]
        [HttpPost]
        public HttpResponseMessage InactiveSysHRDocument(hrdocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSysMstHRDocument.DaInactiveSysHRDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveSysHRDocumentHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveSysHRDocumentHistory(string hrdocument_gid)
        {
            SysHRDocumentInactiveHistory objhrdocumenthistory = new SysHRDocumentInactiveHistory();
            objDaSysMstHRDocument.DaInactiveSysHRDocumentHistory(objhrdocumenthistory, hrdocument_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objhrdocumenthistory);
        }
        [ActionName("DeleteSysHRDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteSysHRDocument(string hrdocument_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSysMstHRDocument.DaDeleteSysHRDocument(hrdocument_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSysHRDocumentDropDown")]
        [HttpGet]
        public HttpResponseMessage GetSysHRDocumentDropDown()
        {
            MdlHRDocument objhrdocument = new MdlHRDocument();
            objDaSysMstHRDocument.DaGetSysHRDocumentDropDown(objhrdocument);
            return Request.CreateResponse(HttpStatusCode.OK, objhrdocument);
        }

        //E Signing - Uploading Document to Digio
        [ActionName("UploadDocumenttoDigio")]
        [HttpPost]
        public HttpResponseMessage UploadDocumenttoDigio(MdlFileDetailsEsign values)
        {
            objDaSysMstHRDocument.DaUploadDocumenttoDigio(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //E Signing - Get Document Details
        [ActionName("GetDocumentDetails")]
        [HttpPost]
        public HttpResponseMessage GetDocumentDetails(MdlFileDetailsEsign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSysMstHRDocument.DaGetDocumentDetails(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //E Signing - Download Document from Digio
        [ActionName("DownloadDocfromDigio")]
        [HttpPost]
        public HttpResponseMessage DownloadDocfromDigio(MdlFileDetailsEsign values)
        {
            var ls_response = new Dictionary<string, object>();
            ls_response = objDaSysMstHRDocument.DaDownloadDocfromDigio(values);
            return Request.CreateResponse(HttpStatusCode.OK, ls_response);
        }

        //E Signing - Update Expiry Date of the document
        [ActionName("UpdateExpiryDate")]
        [HttpGet]
        public HttpResponseMessage UpdateExpiryDate(string employee_gid)
        {

            hrdoc_list values = new hrdoc_list();
            objDaSysMstHRDocument.DaUpdateExpiryDate(values, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //E Sign Unsigned Summary
        [ActionName("GetESignUnsignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetESignUnsignedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            hrdoc_list values = new hrdoc_list();
            objDaSysMstHRDocument.DaGetESignUnsignedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //E Sign Signed Summary
        [ActionName("GetESignSignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetESignSignedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            hrdoc_list values = new hrdoc_list();
            objDaSysMstHRDocument.DaGetESignSignedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //E Sign Expired Summary
        [ActionName("GetESignExpiredSummary")]
        [HttpGet]
        public HttpResponseMessage GetESignExpiredSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            hrdoc_list values = new hrdoc_list();
            objDaSysMstHRDocument.DaGetESignExpiredSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //E Sign Report Summary Count
        [ActionName("GetESignReportSummaryCount")]
        [HttpGet]
        public HttpResponseMessage GetESignReportSummaryCount()
        {
            hrdoc_list values = new hrdoc_list();
            objDaSysMstHRDocument.DaGetESignReportSummaryCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //E Sign Report HR Document Excel Export
        [ActionName("GetESignReportHRDocExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetESignReportHRDocExcelExport()
        {
            hrdoc values = new hrdoc();
            objDaSysMstHRDocument.DaGetESignReportHRDocExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ImportHRdocumentData")]
        [HttpPost]
        public HttpResponseMessage ImportHRdocumentData()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaSysMstHRDocument.DaImportHRdocumentData(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
    }
}
    
