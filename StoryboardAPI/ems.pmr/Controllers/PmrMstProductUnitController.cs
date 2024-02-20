

using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrMstProductUnit")]
    [Authorize]
    public class PmrMstProductUnitController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrMstProductUnit objDaPurchase = new DaPmrMstProductUnit();
        // Product Unit Summary
        [ActionName("GetProductUnitSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductUnitSummary()
        {
            MdlPmrMstProductUnit values = new MdlPmrMstProductUnit();
            objDaPurchase.DaGetProductUnitSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        

        [ActionName("PostProductUnit")]
        [HttpPost]
        public HttpResponseMessage PostProductUnit(productunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostProductUnit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatedProductunit")]
        [HttpPost]
        public HttpResponseMessage UpdatedProductunit(productunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaUpdatedProductunit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deleteProductunitSummary")]
        [HttpGet]
        public HttpResponseMessage deleteProductunitSummary(string productuomclass_gid)
        {
            productunit_list objresult = new productunit_list();
            objDaPurchase.DadeleteProductunitSummary(productuomclass_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetProductUnitSummarygrid")]
        [HttpGet]
        public HttpResponseMessage GetProductUnitSummarygrid(string productuomclass_gid)
        {

            MdlPmrMstProductUnit values = new MdlPmrMstProductUnit();
            objDaPurchase.DaGetProductUnitSummarygrid(productuomclass_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetProductunits")]
        [HttpGet]
        public HttpResponseMessage GetProductunits(string productuomclass_gid)
        {
            MdlPmrMstProductUnit values = new MdlPmrMstProductUnit();
            objDaPurchase.DaGetProductunits(productuomclass_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostProductunits")]
        [HttpPost]
        public HttpResponseMessage PostProductunits(productunitgrid_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.PostProductunits(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}