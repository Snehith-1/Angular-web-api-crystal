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
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.crm.Controllers
{
    [Authorize]
    [RoutePrefix("api/Telegram")]
    public class TelegramController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaTelegram objDaTelegram = new DaTelegram();
        [ActionName("GetTelegram")]
        [HttpGet]
        public HttpResponseMessage GetTelegram()
        {
            telegramlist values = new telegramlist();
            values = objDaTelegram.DaGetTelegram();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    
       [ActionName("GetTelegramdetails")]
       [HttpGet]
       public HttpResponseMessage GetTelegramdetails()
     {
            telegramlist values = new telegramlist();
            objDaTelegram.DaGetTelegramdetails(values);
        return Request.CreateResponse(HttpStatusCode.OK, values);
     }
    [ActionName("TelegramUpload")]
        [HttpPost]
        public HttpResponseMessage TelegramUpload()
        {
            HttpRequest httpRequest;
            //Postassetlocationcreation values = new Postassetlocationcreation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaTelegram.DaTelegramUpload(httpRequest, getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("Telegrammessage")]
        [HttpPost]
        public HttpResponseMessage Telegrammessage(message_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objDaTelegram.DaTelegrammessage(getsessionvalues.user_gid, values, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetTelegramImage")]
        [HttpGet]
        public HttpResponseMessage GetTelegramImage()
        {
            MdlTelegram values = new MdlTelegram();
            objDaTelegram.DaGetTelegramImage(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Telegramrepostupload")]
        [HttpPost]
        public HttpResponseMessage Telegramrepostupload(telegramsummary_list values)
        {
            telegramsummary_list objresult = new telegramsummary_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTelegram.DaTelegramrepostupload(values, getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}