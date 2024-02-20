using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml;
using ems.payroll.DataAccess;
using ems.payroll.Models;
using ems.system.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.payroll.Controllers
{
    [Authorize]
    [RoutePrefix("api/PayTrnBonus")]
    public class PayTrnBonusController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPayTrnBonus objdapay = new DaPayTrnBonus();

        [ActionName("GetBonusSummary")]
        [HttpGet]
        public HttpResponseMessage GetBonusSummary()
        {
            MdlPayTrnBonus values = new MdlPayTrnBonus();
            objdapay.DaGetBonusSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostBonus")]
        [HttpPost]
        public HttpResponseMessage PostBonus(createbonus_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdapay.DaPostBonus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetBonusEmployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetBonusEmployeeSummary(string bonus_gid)
        {
            MdlPayTrnBonus objresult = new MdlPayTrnBonus();
            objdapay.DaGetBonusEmployeeSummary(bonus_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("deleteBonus")]
        [HttpGet]
        public HttpResponseMessage deleteBonus(string params_gid)
        {
            createbonus_list objresult = new createbonus_list();
            objdapay.DadeleteBonus(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostBonusEmployee")]
        [HttpPost]
        public HttpResponseMessage PostBonusEmployee(selectemployee_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdapay.DaPostBonusEmployee(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetgenerateBonus")]
        [HttpGet]
        public HttpResponseMessage GetgenerateBonus(string bonus_gid)
        {
            MdlPayTrnBonus objresult = new MdlPayTrnBonus();
            objdapay.DaGetgenerateBonus(bonus_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Updatedbonus")]
        [HttpPost]
        public HttpResponseMessage Updatedbonus(string user_gid, updatebonus_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objdapay.DaUpdatedbonus(user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}