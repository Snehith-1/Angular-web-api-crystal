using ems.finance.DataAccess;
using ems.finance.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.finance.Controllers
{
    [RoutePrefix("api/AccMstOpeningbalance")]
    [Authorize]
    public class AccMstOpeningbalanceController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAccMstOpeningbalance objDaPurchase = new DaAccMstOpeningbalance();

        // Module Summary

        [ActionName("GetOpeningbalance")]
        [HttpGet]
        public HttpResponseMessage GetOpeningbalance()
        {
            MdlAccMstOpeningbalance values = new MdlAccMstOpeningbalance();
            objDaPurchase.DaGetOpeningbalance(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        public HttpResponseMessage GetAccMstOpeningbalance()
        {
            MdlAccMstOpeningbalance values = new MdlAccMstOpeningbalance();
            objDaPurchase.DaGetAccMstOpeningbalance(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}