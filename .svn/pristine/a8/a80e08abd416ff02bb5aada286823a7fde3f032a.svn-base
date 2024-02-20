using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace ems.sales.Controllers
{
    [Authorize]
    [RoutePrefix("api/SmrMstSalesteamSummary")]
    public class SmrMstSalesteamSummaryController: ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrMstSalesteamSummary objsales = new DaSmrMstSalesteamSummary();

        [ActionName("GetSmrMstSalesteamSummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrMstSalesteamSummary()
        {
            MdlSmrMstSalesteamSummary values = new MdlSmrMstSalesteamSummary();
            objsales.DaGetSmrMstSalesteamSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSmrMstSalesteamgrid")]
        [HttpGet]
        public HttpResponseMessage GetSmrMstSalesteamgrid (string campaign_gid)
        {
            MdlSmrMstSalesteamSummary values = new MdlSmrMstSalesteamSummary();
            objsales.DaGetSmrMstSalesteamgrid(campaign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getemployee")]
        [HttpGet]
        public HttpResponseMessage Getemployee()
        {
            MdlSmrMstSalesteamSummary values = new MdlSmrMstSalesteamSummary();
            objsales.DaGetemployee(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSalesTeam")]
        [HttpPost]
        public HttpResponseMessage Postmarketingteam(salesteamgrid_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objsales.PostSalesTeam(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditSalesTeamSummary")]
        [HttpGet]
        public HttpResponseMessage GetEditSalesTeamSummary(string campaign_gid)
        {
            MdlSmrMstSalesteamSummary objresult = new MdlSmrMstSalesteamSummary();
            objsales.DaGetEditSalesTeamSummary(campaign_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostUpdateSalesTeam")]
        [HttpPost]
        public HttpResponseMessage PostUpdateSalesTeam(editsalesteam_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objsales.PostUpdateSalesTeam(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUnassignedEmplist")]
        [HttpGet]
        public HttpResponseMessage GetUnassignedEmplist(string campaign_gid, string campaign_location)
        {
            MdlSmrMstSalesteamSummary values = new MdlSmrMstSalesteamSummary();
            objsales.DaGetUnassignedEmplist(campaign_gid, campaign_location, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUnassignedlist")]
        [HttpGet]
        public HttpResponseMessage GetUnassignedlist(string campaign_gid)
        {
            MdlSmrMstSalesteamSummary values = new MdlSmrMstSalesteamSummary();
            objsales.DaGetUnassignedlist(campaign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAssignedEmplist")]
        [HttpGet]
        public HttpResponseMessage GetAssignedEmplist(string campaign_gid, string campaign_location)
        {
            MdlSmrMstSalesteamSummary values = new MdlSmrMstSalesteamSummary();
            objsales.DaGetAssignedEmplist(campaign_gid, campaign_location, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAssignedEmplist")]
        [HttpPost]
        public HttpResponseMessage PostAssignedEmplist(campaignassignemp_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objsales.DaPostAssignedEmplist(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUnassignedEmplist")]
        [HttpPost]
        public HttpResponseMessage PostUnassignedEmplist(campaignassignemp_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objsales.DaPostUnassignedEmplist(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUnassignedManagerlist")]
        [HttpGet]
        public HttpResponseMessage GetUnassignedManagerlist(string campaign_gid)
        {
            MdlSmrMstSalesteamSummary values = new MdlSmrMstSalesteamSummary();
            objsales.DaGetUnassignedManagerlist(campaign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAssignedManagerlist")]
        [HttpGet]
        public HttpResponseMessage GetAssignedManagerlist(string campaign_gid)
        {
            MdlSmrMstSalesteamSummary values = new MdlSmrMstSalesteamSummary();
            objsales.DaGetAssignedManagerlist(campaign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUnassignedManager")]
        [HttpGet]
        public HttpResponseMessage GetUnassignedManager(string campaign_gid)
        {
            MdlSmrMstSalesteamSummary values = new MdlSmrMstSalesteamSummary();
            objsales.DaGetUnassignedManager(campaign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAssignedManagerlist")]
        [HttpPost]
        public HttpResponseMessage PostAssignedManagerlist(campaignassignmanager_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objsales.DaPostAssignedManagerlist(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUnassignedManagerlist")]
        [HttpPost]
        public HttpResponseMessage PostUnassignedManagerlist(campaignassignmanager_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objsales.DaPostUnassignedManagerlist(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}