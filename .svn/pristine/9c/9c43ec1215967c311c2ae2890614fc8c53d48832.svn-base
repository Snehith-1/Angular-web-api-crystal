using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrTrnGrnInward")]
    [Authorize]
    public class PmrTrnGrnInwardController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrTrnGrnInward objpurchase = new DaPmrTrnGrnInward();

        [ActionName("GetGrnInwardSummary")]
        [HttpGet]
        public HttpResponseMessage GetGrnInwardSummary()
        {
            MdlPmrTrnGrnInward values = new MdlPmrTrnGrnInward();
            objpurchase.DaGetGrnInwardSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditGrnInward")]
        [HttpGet]
        public HttpResponseMessage GetEditGrnInward( string grn_gid)
        {
            MdlPmrTrnGrnInward objresult = new MdlPmrTrnGrnInward();
            objpurchase.DaGetEditGrnInward(grn_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);      
        }
        [ActionName("GetEditGrnInwardproduct")]
        [HttpGet]
        public HttpResponseMessage GetEditGrnInwardproduct(string grn_gid)
        {
            MdlPmrTrnGrnInward objresult = new MdlPmrTrnGrnInward();
            objpurchase.DaGetEditGrnInwardproduct(grn_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("GetPurchaseOrderDetails")]
        [HttpGet]
        public HttpResponseMessage GetPurchaseOrderDetails(string purchaseorder_gid)
        {
            MdlPmrTrnGrnInward objresult = new MdlPmrTrnGrnInward();
            objpurchase.DaGetPurchaseOrderDetails(purchaseorder_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}