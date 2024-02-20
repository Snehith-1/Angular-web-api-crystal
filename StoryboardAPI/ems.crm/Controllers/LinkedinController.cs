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

namespace ems.crm.Controllers
{
    [Authorize]
    [RoutePrefix("api/Linkedin")]
    public class LinkedinController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLinkedin objDaLinkedin = new DaLinkedin();
       
        [ActionName("GetLinkedinProfile")]
        [HttpGet]
        public HttpResponseMessage GetLinkedinProfile()
        {
            string
            values = objDaLinkedin.DaGetLinkedinProfile(getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK,values);
        }

        [ActionName("Postlinkedin")]
        [HttpPost]
        public HttpResponseMessage Postlinkedin(post_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objDaLinkedin.DaPostlinkedin(getsessionvalues.user_gid, values, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetLinkedinUser")]
        [HttpGet]
        public HttpResponseMessage GetLinkedinUser()
        {
            string
            values = objDaLinkedin.DaGetLinkedinUser();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getlinkedin")]
        [HttpGet]
        public HttpResponseMessage Getlinkedin()
        {
            MdlLinkedin values = new MdlLinkedin();
            objDaLinkedin.DaGetlinkedin(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}