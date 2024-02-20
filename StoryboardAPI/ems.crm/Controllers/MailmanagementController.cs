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
using ems.system.DataAccess;


namespace ems.crm.Controllers
{
    [Authorize]
    [RoutePrefix("api/Mailmanagement")]
    public class MailmanagementController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMailManagement objdamailmanagement = new DaMailManagement();

        [ActionName("GetMailSummary")]
        [HttpGet]
        public HttpResponseMessage GetMailSummary()
        {
            MdlMailmanagement values = new MdlMailmanagement();
            objdamailmanagement.DaGetMailSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getfrommailiddropdown")]
        [HttpGet]
        public HttpResponseMessage Getfrommailiddropdown()
        {
            MdlMailmanagement values = new MdlMailmanagement();
            objdamailmanagement.DaGetfrommaildropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("mailmanagementupload")]
        [HttpPost]
        public HttpResponseMessage mailmanagement()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objdamailmanagement.Damailmanagementupload(httpRequest, objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
       
        [ActionName("mailmanagementsend")]
        [HttpPost]
        public HttpResponseMessage mailfunction(from_list values)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objdamailmanagement.Damailmanagementsend(httpRequest, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
       
    }
}