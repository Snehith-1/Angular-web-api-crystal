using ems.inventory.DataAccess;
using ems.inventory.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.inventory.Controllers
{
    [RoutePrefix("api/ImsTrnOpenDcSummary")]
    [Authorize]
    public class ImsTrnOpenDcSummaryController :ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaImsTrnOpenDcSummary objDaInventory = new DaImsTrnOpenDcSummary();

        [ActionName("GetImsTrnOpenDeliveryOrderSummary")]
        [HttpGet]
        public HttpResponseMessage GetImsTrnOpenDeliveryOrderSummary()
        {

            MdlImsTrnOpenDCSummary values = new MdlImsTrnOpenDCSummary();
            objDaInventory.DaGetImsTrnOpenDeliveryOrderSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetImsTrnOpenDcAddSummary")]
        [HttpGet]
        public HttpResponseMessage GetImsTrnOpenDcAddSummary()
        {

            MdlImsTrnOpenDCSummary values = new MdlImsTrnOpenDCSummary();
            objDaInventory.DaGetImsTrnOpenDcAddSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOpenDcUpdate")]
        [HttpGet]
        public HttpResponseMessage GetOpenDcUpdate(string salesorder_gid)
        {
            MdlImsTrnOpenDCSummary objresult = new MdlImsTrnOpenDCSummary();
            objDaInventory.DaGetOpenDcUpdate(salesorder_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetOpenDcUpdateProd")]
        [HttpGet]
        public HttpResponseMessage GetOpenDcUpdateProd(string salesorder_gid)
        {
            MdlImsTrnOpenDCSummary objresult = new MdlImsTrnOpenDCSummary();
            objDaInventory.DaGetOpenDcUpdateProd(salesorder_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostOpenDcSubmit")]
        [HttpPost]
        public HttpResponseMessage PostOpenDcSubmit(opendc_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaInventory.DaPostOpenDcSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}