﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace ems.pmr.Controllers
//{
//    public class PmrMstPurchaseConfigController
//    {
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrMstPurchaseConfig")]
    [Authorize]
    public class PmrMstPurchaseConfigController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrMstPurchaseConfig objDaSmrMstSalesConfig = new DaPmrMstPurchaseConfig();

        [ActionName("GetAllChargesConfig")]
        [HttpGet]
        public HttpResponseMessage GetAllChargesConfig()
        {
            MdlPmrMstPurchaseConfig values = new MdlPmrMstPurchaseConfig();
            objDaSmrMstSalesConfig.DaGetAllChargesConfig(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAddOnChargesConfig")]
        [HttpPost]
        public HttpResponseMessage UpdateAddOnChargesConfig(salesconfiglist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSmrMstSalesConfig.DaUpdateAddOnChargesConfig(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAdditionalDiscountConfig")]
        [HttpPost]
        public HttpResponseMessage UpdateAdditionalDiscountConfig(salesconfiglist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSmrMstSalesConfig.DaUpdateAdditionalDiscountConfig(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateFreightChargesConfig")]
        [HttpPost]
        public HttpResponseMessage UpdateFreightChargesConfig(salesconfiglist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSmrMstSalesConfig.DaUpdateFreightChargesConfig(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePacking_ForwardingChargesConfig")]
        [HttpPost]
        public HttpResponseMessage UpdatePacking_ForwardingChargesConfig(salesconfiglist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSmrMstSalesConfig.DaUpdatePacking_ForwardingChargesConfig(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateInsuranceChargesConfig")]
        [HttpPost]
        public HttpResponseMessage UpdateInsuranceChargesConfig(salesconfiglist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSmrMstSalesConfig.DaUpdateInsuranceChargesConfig(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}