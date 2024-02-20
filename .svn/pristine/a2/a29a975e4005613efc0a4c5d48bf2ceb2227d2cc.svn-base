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
        [ActionName("GetBonusEmployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetBonusEmployeeSummary(string bonus_gid)
        {
            MdlPayTrnBonus objresult = new MdlPayTrnBonus();
            objdapay.DaGetBonusEmployeeSummary(bonus_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostBonusEmployee")]
        [HttpPost]
        public HttpResponseMessage PostBonusEmployee(string user_gid,string bonus_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objdapay.DaPostBonusEmployee(user_gid,bonus_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }



    }
}