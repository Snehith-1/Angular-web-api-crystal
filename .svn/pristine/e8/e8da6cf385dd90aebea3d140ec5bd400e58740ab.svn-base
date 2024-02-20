using ems.pmr.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using ems.pmr.Models;
using System.Linq;

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrTrnDirectInvoice")]
    [Authorize]
    public class PmrTrnDirectInvoiceController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrTrnDirectInvoice objDainvoice = new DaPmrTrnDirectInvoice();

        [ActionName("GetBranchName")]
        [HttpGet]
        public HttpResponseMessage GetBranchName()
        {
            MdlPmrTrnDirectInvoice values = new MdlPmrTrnDirectInvoice();
            objDainvoice.DaGetBranchName(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVendorNamedropDown")]
        [HttpGet]
        public HttpResponseMessage GetVendorNamedropDown()
        {
            MdlPmrTrnDirectInvoice values = new MdlPmrTrnDirectInvoice();
            objDainvoice.DaGetVendornamedropDown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnChangeVendor")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeVendor(string vendor_gid)
        {
            MdlPmrTrnDirectInvoice values = new MdlPmrTrnDirectInvoice();
            objDainvoice.DaGetOnChangeVendor(vendor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetcurrencyCodedropdown")]
        [HttpGet]
        public HttpResponseMessage GetcurrencyCodedropdown()
        {
            MdlPmrTrnDirectInvoice values = new MdlPmrTrnDirectInvoice();
            objDainvoice.DaGetcurrencyCodedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPurchaseTypedropDown")]
        [HttpGet]
        public HttpResponseMessage GetPurchaseTypedropDown()
        {
            MdlPmrTrnDirectInvoice values = new MdlPmrTrnDirectInvoice();
            objDainvoice.DaGetPurchaseTypedropDown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Gettaxnamedropdown")]
        [HttpGet]
        public HttpResponseMessage Gettaxnamedropdown()
        {
            MdlPmrTrnDirectInvoice values = new MdlPmrTrnDirectInvoice();
            objDainvoice.DaGettaxnamedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetExtraAddondropDown")]
        [HttpGet]
        public HttpResponseMessage GetExtraAddondropDown()
        {
            MdlPmrTrnDirectInvoice values = new MdlPmrTrnDirectInvoice();
            objDainvoice.DaGetExtraAddondropDown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetExtraDeductiondropDown")]
        [HttpGet]
        public HttpResponseMessage GetExtraDeductiondropDown()
        {
            MdlPmrTrnDirectInvoice values = new MdlPmrTrnDirectInvoice();
            objDainvoice.DaGetExtraDeductiondropDown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("directinvoicesubmit")]
        [HttpPost]
        public HttpResponseMessage directinvoicesubmit(directsalesinvoicelist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDainvoice.Dadirectinvoicesubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}