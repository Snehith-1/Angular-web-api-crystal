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
using System.Drawing;
using ems.system.DataAccess;
using ems.system.Models;
using System.Web.Http.Results;


namespace ems.crm.Controllers
{
    [RoutePrefix("api/CampaignSummary")]
    [Authorize]
    public class CampaignSummaryController : ApiController
    {
            session_values Objgetgid = new session_values();
            logintoken getsessionvalues = new logintoken();
            DaCampaignSummary objCampaignSummary = new DaCampaignSummary();

        [ActionName("GetTeamSummary")]
        [HttpGet]
        public HttpResponseMessage GetTeamSummary()
            {
                MdlCampaignSummary values = new MdlCampaignSummary();
                objCampaignSummary.DaGetTeamSummary(values);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
        [ActionName("Getbranchdropdown")]
        [HttpGet]
        public HttpResponseMessage Getbranchdropdown()
        {
            MdlCampaignSummary values = new MdlCampaignSummary();
            objCampaignSummary.DaGetbranchdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getteammanagerdropdown")]
        [HttpGet]
        public HttpResponseMessage Getteammanagerdropdown()
        {
            MdlCampaignSummary values = new MdlCampaignSummary();
            objCampaignSummary.DaGetteammanagerdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("Postmarketingteam")]
        [HttpPost]
        public HttpResponseMessage Postmarketingteam(marketingteam_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objCampaignSummary.DaPostmarketingteam(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Updatedmarketingteam")]
        [HttpPost]
        public HttpResponseMessage Updatedmarketingteam(marketingteam_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objCampaignSummary.DaUpdatedmarketingteam(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMarketingTeamDetailTable")]
        [HttpGet]
        public HttpResponseMessage GetMarketingTeamDetailTable(string campaign_gid)
        {
           
            MdlCampaignSummary values = new MdlCampaignSummary();
            objCampaignSummary.DaGetMarketingTeamDetailTable(campaign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteTeam")]
        [HttpGet]
        public HttpResponseMessage DeleteTeam(string campaign_gid)
        {
            marketingteam_list values = new marketingteam_list();
            objCampaignSummary.DaDeleteTeam(campaign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // CODE BY SNEHITH

        [ActionName("GetUnassignedlist")]
        [HttpGet]
        public HttpResponseMessage GetUnassignedlist(string campaign_gid, string campaign_location)
        {
            MdlCampaignSummary objresult = new MdlCampaignSummary();
            objCampaignSummary.DaGetUnassignedlist(campaign_gid, campaign_location, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetUnassigned")]
        [HttpGet]
        public HttpResponseMessage GetUnassigned(string campaign_gid)
        {
            MdlCampaignSummary objresult = new MdlCampaignSummary();
            objCampaignSummary.DaGetUnassigned(campaign_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetAssignedlist")]
        [HttpGet]
        public HttpResponseMessage GetAssignedlist(string campaign_gid, string campaign_location)
        {
            MdlCampaignSummary objresult = new MdlCampaignSummary();
            objCampaignSummary.DaGetAssignedlist(campaign_gid, campaign_location, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostAssignedlist")]
        [HttpPost]
        public HttpResponseMessage PostAssignedlist(campaignassign_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objCampaignSummary.DaPostAssignedlist(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("PostUnassignedlist")]
        [HttpPost]
        public HttpResponseMessage PostUnassignedlist(campaignassign_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objCampaignSummary.DaPostUnassignedlist(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("GetManagerUnassignedlist")]
        [HttpGet]
        public HttpResponseMessage GetManagerUnassignedlist(string campaign_gid, string campaign_location)
        {
            MdlCampaignSummary objresult = new MdlCampaignSummary();
            objCampaignSummary.DaGetManagerUnassignedlist(campaign_gid, campaign_location, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetManagerAssignedlist")]
        [HttpGet]
        public HttpResponseMessage GetManagerAssignedlist(string campaign_gid, string campaign_location)
        {
            MdlCampaignSummary objresult = new MdlCampaignSummary();
            objCampaignSummary.DaGetManagerAssignedlist(campaign_gid, campaign_location, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetManagerUnassigned")]
        [HttpGet]
        public HttpResponseMessage GetManagerUnassigned(string campaign_gid)
        {
            MdlCampaignSummary objresult = new MdlCampaignSummary();
            objCampaignSummary.DaGetManagerUnassigned(campaign_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("PostManagerAssignedlist")]
        [HttpPost]
        public HttpResponseMessage PostManagerAssignedlist(campaignassign_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objCampaignSummary.DaPostManagerAssignedlist(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("PostManagerUnassignedlist")]
        [HttpPost]
        public HttpResponseMessage PostManagerUnassignedlist(campaignassign_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objCampaignSummary.DaPostManagerUnassignedlist(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("GetUserSummary")]
        [HttpPost]
        public HttpResponseMessage GetUserSummary(MdlSearchParamters values)
        {
            MdlCampaignSummary objMdlCampaignSummary = new MdlCampaignSummary();
            objMdlCampaignSummary = objCampaignSummary.DaGetUserSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCampaignSummary);
        }


        [ActionName("GetAssignSummary")]
        [HttpPost]
        public HttpResponseMessage GetAssignSummary(MdlSearchParamters values)
        {
            MdlCampaignSummary objMdlCampaignSummary = new MdlCampaignSummary();
            objMdlCampaignSummary = objCampaignSummary.DaGetAssignSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCampaignSummary);
        }
        [ActionName("GetAssignLead")]
        [HttpPost]
        public HttpResponseMessage GetAssignLead(assignteam_list1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objCampaignSummary.DaGetAssignLead(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSourcedropdown")]
        [HttpGet]
        public HttpResponseMessage GetSourcedropdown()
        {
            MdlCampaignSummary values = new MdlCampaignSummary();
            objCampaignSummary.DaGetSourcedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRegiondropdown")]
        [HttpGet]
        public HttpResponseMessage GetRegiondropdown()
        {
            MdlCampaignSummary values = new MdlCampaignSummary();
            objCampaignSummary.DaGetRegiondropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndustrydropdown")]
        [HttpGet]
        public HttpResponseMessage GetIndustrydropdown()
        {
            MdlCampaignSummary values = new MdlCampaignSummary();
            objCampaignSummary.DaGetIndustrydropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Marketing Team Count
        [ActionName("GetMarketingTeamCount")]
        [HttpGet]
        public HttpResponseMessage GetMarketingTeamCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCampaignSummary values = new MdlCampaignSummary();
            objCampaignSummary.DaGetMarketingTeamCount(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}

