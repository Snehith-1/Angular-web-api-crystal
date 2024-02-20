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
    [RoutePrefix("api/ImsTrnDeliveryAcknowledgement")]
    [Authorize]
    public class ImsTrnDeliveryAcknowledgementController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaImsTrnDeliveryAcknowledgement objDaInventory = new DaImsTrnDeliveryAcknowledgement();

        [ActionName("GetImsTrnDeliveryAcknowledgementSummary")]
        [HttpGet]
        public HttpResponseMessage GetImsTrnDeliveryAcknowledgementSummary()
        {

            MdlImsTrnDeliveryAcknowledgement values = new MdlImsTrnDeliveryAcknowledgement();
            objDaInventory.DaGetImsTrnDeliveryAcknowledgementSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetImsTrnDeliveryAcknowledgementAdd")]
        [HttpGet]
        public HttpResponseMessage GetImsTrnDeliveryAcknowledgementAdd()
        {

            MdlImsTrnDeliveryAcknowledgement values = new MdlImsTrnDeliveryAcknowledgement();
            objDaInventory.DaGetImsTrnDeliveryAcknowledgementAdd(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add page

        [ActionName("GetDeliveryAcknowledgeUpdate")]
        [HttpGet]
        public HttpResponseMessage GetDeliveryAcknowledgeUpdate(string directorder_gid)
        {
            MdlImsTrnDeliveryAcknowledgement objresult = new MdlImsTrnDeliveryAcknowledgement();
            objDaInventory.DaGetDeliveryAcknowledgeUpdate(directorder_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDeliveryAcknowledgeUpdateProd")]
        [HttpGet]
        public HttpResponseMessage GetDeliveryAcknowledgeUpdateProd(string directorder_gid)
        {
            MdlImsTrnDeliveryAcknowledgement objresult = new MdlImsTrnDeliveryAcknowledgement();
            objDaInventory.DaGetDeliveryAcknowledgeUpdateProd(directorder_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostDeliveryAckSubmit")]
        [HttpPost]
        public HttpResponseMessage PostDeliveryAckSubmit(postdelivery_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaInventory.DaPostDeliveryAckSubmit(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}