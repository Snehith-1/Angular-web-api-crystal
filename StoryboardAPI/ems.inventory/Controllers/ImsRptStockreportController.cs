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

namespace ems.inventory.Controllers
{

    [RoutePrefix("api/ImsRptStockreport")]
    [Authorize]


    public class ImsRptStockreportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaImsRptStockreport objDaInventory = new DaImsRptStockreport();

        [ActionName("GetImsRptStockreport")]
        [HttpGet]
        public HttpResponseMessage GetImsRptStockreport(string branch_gid)
        {
            MdlImsRptStockreport values = new MdlImsRptStockreport();
            objDaInventory.DaGetImsRptStockreport(branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBranch")]
        [HttpGet]
        public HttpResponseMessage GetBranch()
        {
            MdlImsRptStockreport values = new MdlImsRptStockreport();
            objDaInventory.DaGetBranch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }




    }
    
}