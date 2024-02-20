using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.sales.Controllers
{
    [Authorize]
    [RoutePrefix("api/SmrTrnSalesManager")]
    public class SmrTrnSalesManagerController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrTrnSalesManager objDasales = new DaSmrTrnSalesManager();

        //total summary
        [ActionName("GetSalesManagerTotal")]
        [HttpGet]
        public HttpResponseMessage GetSalesManagerTotal()
        {
            MdlSmrTrnSalesManager values = new MdlSmrTrnSalesManager();
            objDasales.DaGetSalesManagerTotal( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        //complete summary
        [ActionName("GetSalesManagerComplete")]
        [HttpGet]
        public HttpResponseMessage GetSalesManagerComplete()
        {
            MdlSmrTrnSalesManager values = new MdlSmrTrnSalesManager();
            objDasales.DaGetSalesManagerComplete(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        //prospect summary
        [ActionName("GetSalesManagerProspect")]
        [HttpGet]
        public HttpResponseMessage GetSalesManagerProspect()
        {
            MdlSmrTrnSalesManager values = new MdlSmrTrnSalesManager();
            objDasales.DaGetSalesManagerProspect(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        //potential summary
        [ActionName("GetSalesManagerPotential")]
        [HttpGet]
        public HttpResponseMessage GetSalesManagerPotential()
        {
            MdlSmrTrnSalesManager values = new MdlSmrTrnSalesManager();
            objDasales.DaGetSalesManagerPotential(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }


        //Drop summary
        [ActionName("GetSalesManagerdrop")]
        [HttpGet]
        public HttpResponseMessage GetSalesManagerdrop()
        {
            MdlSmrTrnSalesManager values = new MdlSmrTrnSalesManager();
            objDasales.DaGetSalesManagerDrop(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }


        //Count 
        [ActionName("GetSmrTrnManagerCount")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnManagerCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrTrnSalesManager values = new MdlSmrTrnSalesManager();
            objDasales.DaGetSmrTrnManagerCount(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}