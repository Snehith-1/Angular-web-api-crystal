using ems.finance.DataAccess;
using ems.finance.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace ems.finance.Controllers
{
    [RoutePrefix("api/AccTrnBankbooksummary")]
    [Authorize]
    public class AccTrnBankbooksummaryController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAccTrnBankbooksummary objfinance = new DaAccTrnBankbooksummary();

        [ActionName("GetBankBookSummary")]
        [HttpGet]
        public HttpResponseMessage GetBankBookSummary()
        {
            MdlAccTrnBankbooksummary values = new MdlAccTrnBankbooksummary();
            objfinance.DaGetBankBookSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetSubBankBook")]
        [HttpGet]
        public HttpResponseMessage GetSubBankBook(string bank_gid)
        {
            MdlAccTrnBankbooksummary values = new MdlAccTrnBankbooksummary();
            objfinance.DaGetSubBankBook(values, bank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetBankBookAddSummary")]
        [HttpGet]
        public HttpResponseMessage GetBankBookAddSummary(string bank_gid)
        {
            MdlAccTrnBankbooksummary values = new MdlAccTrnBankbooksummary();
            objfinance.DaGetBankBookAddSummary(bank_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetAccTrnGroupDtl")]
        [HttpGet]
        public HttpResponseMessage GetAccTrnGroupDtl()
        {
            MdlAccTrnBankbooksummary values = new MdlAccTrnBankbooksummary();
            objfinance.DaGetAccTrnGroupDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAccTrnNameDtl")]
        [HttpGet]
        public HttpResponseMessage GetAccTrnNameDtl()
        {
            MdlAccTrnBankbooksummary values = new MdlAccTrnBankbooksummary();
            objfinance.DaGetAccTrnNameDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProductGroupSummary")]
        [HttpPost]
        public HttpResponseMessage PostProductGroupSummary(accountfetch_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objfinance.DaPostProductGroupSummary(getsessionvalues.user_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
