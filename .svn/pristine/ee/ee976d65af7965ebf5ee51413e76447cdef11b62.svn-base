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
    [RoutePrefix("api/MarketingManager")]
    public class MarketingmanagerController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMarketingmanager objdaMarkeingManager = new DaMarketingmanager();

        //Marketing Manager Total//
        [ActionName("GetMarketingManagerTotalSummary")]
        [HttpGet]
        public HttpResponseMessage GetMarketingManagerTotalSummary()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetMarketingManagerTotalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //teamcount//s
      
        [ActionName("Getteamcount")]
        [HttpGet]
        public HttpResponseMessage Getteamcount()
        {
            MdlMarketingmanager objresult = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetteamcount(getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //Total Lapsed and Longest lead count
        [ActionName("Totallapsedlongest")]
        [HttpGet]
        public HttpResponseMessage Totallapsedlongest()
        {
            MdlMarketingmanager objresult = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaTotallapsedlongest(getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //Marketing Manager Lapsed Lead//
        [ActionName("GetMarketingManagerLapsedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMarketingManagerLapsedSummary()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetMarketingManagerLapsedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Marketing Manager Longest Lead//
        [ActionName("GetMarketingManagerLongestSummary")]
        [HttpGet]
        public HttpResponseMessage GetMarketingManagerLongestSummary()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetMarketingManagerLongestSummary(getsessionvalues.employee_gid, values); 
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMarketingManagerSummary")]
        [HttpGet]
        public HttpResponseMessage GetMarketingManagerSummary()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetMarketingManagerSummary(getsessionvalues.user_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Total tile count
        [ActionName("GetTotaltilecount")]
        [HttpGet]
        public HttpResponseMessage GetTotaltilecount()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetTotaltilecount(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetManagerSummaryDetailTable")]
        [HttpGet]
        public HttpResponseMessage GetManagerSummaryDetailTable(string campaign_gid)
        {
            MdlMarketingmanager objresult = new MdlMarketingmanager();
            objdaMarkeingManager.DaGetManagerSummaryDetailTable(campaign_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetCampaignmanagerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCampaignmanagerSummary(string campaign_gid,string assign_to,string stages)
        {
            MdlMarketingmanager objresult = new MdlMarketingmanager();
            objdaMarkeingManager.DaGetCampaignmanagerSummary(campaign_gid, assign_to,stages,objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetCampaignmanagerTeam")]
        [HttpGet]
        public HttpResponseMessage GetCampaignmanagerTeam(string campaign_gid, string assign_to)
        {
            MdlMarketingmanager objresult = new MdlMarketingmanager();
            objdaMarkeingManager.DaGetCampaignmanagerTeam(campaign_gid, assign_to, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetCampaignMoveToBin")]
        [HttpPost]
        public HttpResponseMessage GetCampaignMoveToBin(campaignbin_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetCampaignMoveToBin(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCampaignMoveToTransfer")]
        [HttpPost]
        public HttpResponseMessage GetCampaignMoveToTransfer(campaigntransfer_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetCampaignMoveToTransfer(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Marketing Manager Transfer
        [ActionName("PostMoveToTransfer")]
        [HttpPost]
        public HttpResponseMessage PostMoveToTransfer(campaigntransfer_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaPostMoveToTransfer(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCampaignSchedule")]
        [HttpPost]
        public HttpResponseMessage GetCampaignSchedule(campaignschedule_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetCampaignSchedule(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        ////Marketing Manager Schedule
        [ActionName("PostManagerSchedule")]
        [HttpPost]
        public HttpResponseMessage PostManagerSchedule(campaignschedule_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaPostManagerSchedule(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTeamNamedropdown")]
        [HttpGet]
        public HttpResponseMessage GetTeamNamedropdown()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetTeamNamedropdown(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTeamEmployeedropdown")]
        [HttpGet]
        public HttpResponseMessage GetTeamEmployeedropdown(string team_gid)
        {
            MdlMarketingmanager objresult = new MdlMarketingmanager();
            objdaMarkeingManager.DaGetTeamEmployeedropdown(team_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        //upcoming//
        [ActionName("GetUpcomingManagerSummary")]
        [HttpGet]
        public HttpResponseMessage GetUpcomingManagerSummary()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetUpcomingManagerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //new//
        [ActionName("GetNewManagerSummary")]
        [HttpGet]
        public HttpResponseMessage GetNewManagerSummary()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetNewManagerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //prospect//
        [ActionName("GetProspectManagerSummary")]
        [HttpGet]
        public HttpResponseMessage GetProspectManagerSummary()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetProspectManagerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //potential//
        [ActionName("GetPotentialtManagerSummary")]
        [HttpGet]
        public HttpResponseMessage GetPotentialtManagerSummary()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetPotentialtManagerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //customer//
        [ActionName("GetCustomerManagerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerManagerSummary()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetCustomerManagerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Drop//
        [ActionName("GetDropManagerSummary")]
        [HttpGet]
        public HttpResponseMessage GetDropManagerSummary()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetDropManagerSummary(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //MTD//
        [ActionName("GetMTD")]
        [HttpGet]
        public HttpResponseMessage GetMTD()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetMTD(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetYTD")]
        [HttpGet]
        public HttpResponseMessage GetYTD()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaMarkeingManager.DaGetYTD(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //schedulelog//
        [ActionName("GetSchedulelogsummary")]
        [HttpGet]
        public HttpResponseMessage GetSchedulelogsummary(string leadbank_gid)
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            objdaMarkeingManager.DaGetSchedulelogsummary(leadbank_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}