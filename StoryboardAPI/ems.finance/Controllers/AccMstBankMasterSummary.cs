using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.finance.DataAccess;
using ems.finance.Models;

namespace ems.finance.Controllers
{
    public class AccMstBankMasterSummary
    {
        [RoutePrefix("api/AccMstBankMaster")]
        [Authorize]
        public class AccMstBankMasterController : ApiController
        {
            session_values Objgetgid = new session_values();
            logintoken getsessionvalues = new logintoken();
            DaAccMstBankMasterSummary objpurchase = new DaAccMstBankMasterSummary();

            [ActionName("GetBankMasterSummary")]
            [HttpGet]
            public HttpResponseMessage GetBankMasterSummary()
            {
                MdlAccMstBankMasterSummary values = new MdlAccMstBankMasterSummary();
                objpurchase.DaGetBankMasterSummary(values);
                return Request.CreateResponse(HttpStatusCode.OK, values);

            }
            [ActionName("GetAccountType")]
            [HttpGet]
            public HttpResponseMessage GetAccountType()
            {
                MdlAccMstBankMasterSummary values = new MdlAccMstBankMasterSummary();
                objpurchase.DaGetAccountType(values);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
            [ActionName("GetAccountGroup")]
            [HttpGet]
            public HttpResponseMessage GetAccountGroup()
            {
                MdlAccMstBankMasterSummary values = new MdlAccMstBankMasterSummary();
                objpurchase.DaGetAccountGroup(values);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
            [ActionName("GetBranchName")]
            [HttpGet]
            public HttpResponseMessage GetBranchName()
            {
                MdlAccMstBankMasterSummary values = new MdlAccMstBankMasterSummary();
                objpurchase.DaGetBranchName(values);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
            [ActionName("PostBankMaster")]
            [HttpPost]
            public HttpResponseMessage PostBankMaster(GetBankMaster_list values)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                objpurchase.DaPostBankMaster(getsessionvalues.user_gid, values);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
            [ActionName("GetBankMasterDetail")]
            [HttpGet]
            public HttpResponseMessage GetBankMasterDetail(string bank_gid)
            {
                MdlAccMstBankMasterSummary objresult = new MdlAccMstBankMasterSummary();
                objpurchase.DaGetBankMasterDetail(bank_gid, objresult);
                return Request.CreateResponse(HttpStatusCode.OK, objresult);
            }
            [ActionName("PostBankMasterUpdate")]
            [HttpPost]
            public HttpResponseMessage PostBankMasterUpdate(GetEditBankMaster_list values)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                objpurchase.DaPostBankMasterUpdate(getsessionvalues.user_gid, values);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
        }

    }
}