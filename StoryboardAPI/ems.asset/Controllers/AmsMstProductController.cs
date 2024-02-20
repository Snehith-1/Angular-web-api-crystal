using ems.asset.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.asset.Models;
using ems.asset.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.asset.Controllers
{
    [RoutePrefix("api/AmsMstProduct")]
    [Authorize]
    public class AmsMstProductController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAmsMstProduct objDaCustomer = new DaAmsMstProduct();

        [ActionName("GetproductSummary")]
        [HttpGet]
        public HttpResponseMessage GetproductSummary()
        {
            MdlAmsMstProduct values = new MdlAmsMstProduct();
            objDaCustomer.DaGetproductSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductgroup")]
        [HttpGet]
        public HttpResponseMessage GetProductgroup()
        {
            MdlAmsMstProductsubgroup values = new MdlAmsMstProductsubgroup();
            objDaCustomer.DaGetProductgroup(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductsubgroup")]
        [HttpGet]
        public HttpResponseMessage GetProductsubgroup()
        {
            MdlAmsMstProduct values = new MdlAmsMstProduct();
            objDaCustomer.DaGetProductsubgroup(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAttriute")]
        [HttpGet]
        public HttpResponseMessage GetAttriute()
        {
            MdlAmsMstProduct values = new MdlAmsMstProduct();
            objDaCustomer.DaGetAttriute(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProductAdd")]
        [HttpPost]

        public HttpResponseMessage PostProductAdd(product_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostProductAdd(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("PostProductUpdate")]
        [HttpPost]

        public HttpResponseMessage PostProductUpdate(product_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostProductUpdate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("PostAttriuteAdd")]
        [HttpPost]

        public HttpResponseMessage PostAttriuteAdd(product_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostAttriuteAdd(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("Deleteproduct")]
        [HttpGet]
        public HttpResponseMessage Deleteproduct(string product_gid)
        {
            product_list objresult = new product_list();
            objDaCustomer.DaDeleteproduct(product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}