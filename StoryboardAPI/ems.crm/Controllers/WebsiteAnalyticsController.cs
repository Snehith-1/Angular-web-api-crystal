using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.crm.DataAccess;
using ems.crm.Models;
using ems.system.DataAccess;
using ems.system.Models;
using ems.utilities.Functions;
using ems.utilities.Models;


namespace ems.crm.Controllers
{
    [Authorize]
    [RoutePrefix("api/WebsiteAnalytics")]
    public class WebsiteAnalyticsController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaWebsiteAnalytics objWebsiteAnalytics = new DaWebsiteAnalytics();

        [ActionName("PostWebsiteAnalyticsUser")]
        [HttpPost]
        public HttpResponseMessage PostWebsiteAnalyticsUser(assignvisitsubmit_list6 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objWebsiteAnalytics.DaPostWebsiteAnalyticsUser(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostWebsiteAnalyticsPage")]
        [HttpPost]
        public HttpResponseMessage PostWebsiteAnalyticsPage(assignvisitsubmit_list6 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objWebsiteAnalytics.DaPostWebsiteAnalyticsPage(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWebsiteSessionchart")]
        [HttpGet]
        public HttpResponseMessage GetWebsiteSessionchart()
        {
            MdlWebsiteAnalytics values = new MdlWebsiteAnalytics();
            objWebsiteAnalytics.DaGetWebsiteSessionchart(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWebsiteUserchart")]
        [HttpGet]
        public HttpResponseMessage GetWebsiteUserchart()
        {
            MdlWebsiteAnalytics values = new MdlWebsiteAnalytics();
            objWebsiteAnalytics.DaGetWebsiteUserchart(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWebsiteAnalyticsSummary")]
        [HttpGet]
        public HttpResponseMessage GetWebsiteAnalyticsSummary()
        {
            MdlWebsiteAnalytics values = new MdlWebsiteAnalytics();
            objWebsiteAnalytics.DaGetWebsiteAnalyticsSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getdaywisechart")]
        [HttpGet]
        public HttpResponseMessage Getdaywisechart()
        {
            MdlWebsiteAnalytics values = new MdlWebsiteAnalytics();
            objWebsiteAnalytics.DaGetdaywisechart(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getweekwisechart")]
        [HttpGet]
        public HttpResponseMessage Getweekwisechart()
        {
            MdlWebsiteAnalytics values = new MdlWebsiteAnalytics();
            objWebsiteAnalytics.DaGetweekwisechart(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getmonthwisechart")]
        [HttpGet]
        public HttpResponseMessage Getmonthwisechart()
        {
            MdlWebsiteAnalytics values = new MdlWebsiteAnalytics();
            objWebsiteAnalytics.DaGetmonthwisechart(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getyearwisechart")]
        [HttpGet]
        public HttpResponseMessage Getyearwisechart()
        {
            MdlWebsiteAnalytics values = new MdlWebsiteAnalytics();
            objWebsiteAnalytics.DaGetyearwisechart(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetWebsiteAnalyticsPageSessions")]
        [HttpGet]
        public HttpResponseMessage GetWebsiteAnalyticsPageSessions()
        {
            MdlWebsiteAnalytics values = new MdlWebsiteAnalytics();
            objWebsiteAnalytics.DaGetWebsiteAnalyticsPageSessions(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetWebsiteAnalyticstiles")]
        [HttpGet]
        public HttpResponseMessage GetWebsiteAnalyticstiles()
        {
            MdlWebsiteAnalytics values = new MdlWebsiteAnalytics();
            objWebsiteAnalytics.DaGetWebsiteAnalyticstiles(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}