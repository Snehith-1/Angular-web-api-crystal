using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Collections.Generic;
using System.Web;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.crm.Controllers
{
    [Authorize]
    [RoutePrefix("api/website")]
    public class websiteController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        daWebsite objdaWebsite = new daWebsite();


        [ActionName("chatSummary")]
        [HttpGet]
        public HttpResponseMessage chatSummary()
        {
            MdlWebsite values = new MdlWebsite();
            objdaWebsite.DachatSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);


        }
        [ActionName("Getuserdeatils")]
        [HttpGet]
        public HttpResponseMessage Getuserdeatils(string user_id)
        {
            MdlWebsite values = new MdlWebsite();
            objdaWebsite.DaGetuserdeatils(values, user_id);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getlistofchat")]
        [HttpGet]
        public HttpResponseMessage Getlistofchat()
        {
            MdlWebsite values = new MdlWebsite();
            objdaWebsite.Dalistofchat(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getlistofthreads")]
        [HttpGet]
        public HttpResponseMessage Getlistofthreads()
        {
            Rootobject2 values = new Rootobject2();
            objdaWebsite.Dalistofthreads(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);


        }
        [ActionName("Getindividualchat")]
        [HttpGet]
        public HttpResponseMessage Getindividualchat(string chat_id)
        {
            mdlinlinechat values = new mdlinlinechat();
            objdaWebsite.Daindividualchat(values, chat_id);
            return Request.CreateResponse(HttpStatusCode.OK, values);


        }
        [ActionName("Messagesend")]
        [HttpPost]
        public HttpResponseMessage Messagesend(messagesend values)
        {
            result objResult = new result();
            objResult = objdaWebsite.DaMessagesend(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);

        }
        [ActionName("GetchatAnalyticsUser")]
        [HttpGet]
        public HttpResponseMessage GetchatAnalyticsUser()
        {
            MdlWebsite values = new MdlWebsite();
            objdaWebsite.DaGetchatAnalyticsUser(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetchatAnalyticsSummary")]
        [HttpGet]
        public HttpResponseMessage GetchatAnalyticsSummary()
        {
            MdlWebsite values = new MdlWebsite();
            objdaWebsite.DaGetchatAnalyticsSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getweekwiselist")]
        [HttpGet]
        public HttpResponseMessage Getweekwiselist()
        {
            MdlWebsite values = new MdlWebsite();
            objdaWebsite.DaGetweekwiselist(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("uploadsend")]
        [HttpPost]
        public HttpResponseMessage uploadsend(messagesend values)
        {
            result objResult = new result();
            objResult = objdaWebsite.Dauploadsend(values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("Getaccesstoken")]
        [HttpGet]
        public HttpResponseMessage Getaccesstoken()
        {
            inlinechatconfiguration values = new inlinechatconfiguration();
            objdaWebsite.DaGetaccesstoken(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }


}
