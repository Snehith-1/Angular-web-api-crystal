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
using System.Web.Http.Results;

namespace ems.asset.Controllers
{
    [RoutePrefix("api/AmsMstProductsubgroup")]
    [Authorize]
    public class AmsMstProductsubgroupController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAmsMstProductsubgroup objDaCustomer = new DaAmsMstProductsubgroup();

        [ActionName("GetProductgroup")]
        [HttpGet]
        public HttpResponseMessage GetProductgroup()
        {
            MdlAmsMstProductsubgroup values = new MdlAmsMstProductsubgroup();
            objDaCustomer.DaGetProductgroup(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAssetblock")]
        [HttpGet]
        public HttpResponseMessage GetAssetblock()
        {
            MdlAmsMstProductsubgroup values = new MdlAmsMstProductsubgroup();
            objDaCustomer.DaGetAssetblock(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetNatureOfAsset")]
        [HttpGet]
        public HttpResponseMessage GetNatureOfAsset(string assetblock_gid)
        {
            MdlAmsMstProductsubgroup objresult = new MdlAmsMstProductsubgroup();
            objDaCustomer.DaGetNatureOfAsset(assetblock_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlAmsMstProductsubgroup objresult = new MdlAmsMstProductsubgroup();
            objDaCustomer.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetproductsubgroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetproductsubgroupSummary()
        {
            MdlAmsMstProductsubgroup values = new MdlAmsMstProductsubgroup();
            objDaCustomer.DaGetproductsubgroupSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProductsugroupAdd")]
        [HttpPost]

        public HttpResponseMessage PostProductsugroupAdd(productsubgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostProductsugroupAdd(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("PostProductsubgroupUpdate")]
        [HttpPost]

        public HttpResponseMessage PostProductsubgroupUpdate(productsubgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostProductsubgroupUpdate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("PostNatureofAsset")]
        [HttpPost]

        public HttpResponseMessage PostNatureofAsset(productsubgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostNatureofAsset(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("Deleteproductsubgroup")]
        [HttpGet]
        public HttpResponseMessage Deleteproductsubgroup(string productsubgroup_gid)
        {
            productsubgroup_list objresult = new productsubgroup_list();
            objDaCustomer.DaDeleteproductsubgroup(productsubgroup_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}