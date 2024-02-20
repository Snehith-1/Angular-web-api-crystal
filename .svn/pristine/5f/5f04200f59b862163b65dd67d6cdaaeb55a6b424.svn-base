
using ems.inventory.DataAccess;
using ems.inventory.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.inventory.DataAccess;

namespace ems.inventory.Controllers
{

    [RoutePrefix("api/ImsTrnOpeningStock")]
    [Authorize]
    public class ImsTrnOpeningStockController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaImsTrnOpeningStock objDaInventory = new DaImsTrnOpeningStock();
        // Module Summary
        [ActionName("GetImsTrnOpeningstockSummary")]
        [HttpGet]
        public HttpResponseMessage GetImsTrnOpeningstockSummary()
        {
            MdlImsTrnOpeningStock values = new MdlImsTrnOpeningStock();
            objDaInventory.DaGetImsTrnOpeningstockSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetImsTrnOpeningstockAdd")]
        [HttpGet]
        public HttpResponseMessage GetImsTrnOpeningstockAdd()
        {
            MdlImsTrnOpeningStock values = new MdlImsTrnOpeningStock();
            objDaInventory.DaGetImsTrnOpeningstockAdd(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnChangeLocation")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeLocation()
        {
            MdlImsTrnOpeningStock values = new MdlImsTrnOpeningStock();
            objDaInventory.DaGetOnChangeLocation(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }



        [ActionName("PostOpeningstock")]
        [HttpPost]
        public HttpResponseMessage PostOpeningstock(Postopeningstock values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaInventory.DaPostOpeningstock(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditOpeningStockSummary")]
        [HttpGet]
        public HttpResponseMessage GetEditOpeningStockSummary(string stock_gid)
        {
            MdlImsTrnOpeningStock objresult = new MdlImsTrnOpeningStock();
            objDaInventory.DaGetEditOpeningStockSummary(stock_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostOpeningStockUpdate")]
        [HttpPost]
        public HttpResponseMessage PostOpeningStockUpdate(stockedit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaInventory.DaPostOpeningStockUpdate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetOnChangeproductName")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeproductName(string product_gid)
        {
            MdlImsTrnOpeningStock values = new MdlImsTrnOpeningStock();
            objDaInventory.DaGetOnChangeproductName(product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}