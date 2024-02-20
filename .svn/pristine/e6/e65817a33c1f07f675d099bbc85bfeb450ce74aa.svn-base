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
    [RoutePrefix("api/Facebook")]
    public class FacebookController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaFacebook objDaFacebook = new DaFacebook();
        [ActionName("GetFacebook")]
        [HttpGet]
        public HttpResponseMessage GetFacebook()
        {
            facebooklist values = new facebooklist();
            values = objDaFacebook.DaGetFacebook();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetFacebookuserdetails")]
        [HttpGet]
        public HttpResponseMessage GetFacebookuserdetails()
        {
            MdlFacebook values = new MdlFacebook();
            objDaFacebook.DaGetFacebookuserdetails(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPagedetails")]
        [HttpGet]
        public HttpResponseMessage GetPagedetails()
        {
            MdlFacebook values = new MdlFacebook();
            objDaFacebook.DaGetPagedetails(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //[ActionName("GetPagesummary")]
        //[HttpGet]
        //public HttpResponseMessage GetPagesummary()
        //{
        //    MdlFacebook values = new MdlFacebook();
        //   objDaFacebook.DaGetPagesummary(values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
        [ActionName("GetPageuserdetails")]
        [HttpGet]
        public HttpResponseMessage GetPageuserdetails()
        {
            MdlFacebook values = new MdlFacebook();
            objDaFacebook.DaGetPageuserdetails(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadImage")]
        [HttpPost]
        public HttpResponseMessage UploadImage()
        {
            HttpRequest httpRequest;
            //Postassetlocationcreation values = new Postassetlocationcreation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaFacebook.DaUploadImage(httpRequest, getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("ScheduleUploadImage")]
        [HttpPost]
        public HttpResponseMessage ScheduleUploadImage()
        {
            HttpRequest httpRequest;
            //Postassetlocationcreation values = new Postassetlocationcreation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaFacebook.DaScheduleUploadImage(httpRequest, getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetViewFacebook")]
        [HttpGet]
        public HttpResponseMessage GetViewFacebook(string facebookmain_gid)
        {
            mdlFbPostView objresult = new mdlFbPostView();
            objDaFacebook.DaGetViewFacebook(facebookmain_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getschedulelogdetails")]
        [HttpGet]
        public HttpResponseMessage Getschedulelogdetails()
        {
            MdlFacebook values = new MdlFacebook();
            objDaFacebook.DaGetschedulelogdetails(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }

    }
