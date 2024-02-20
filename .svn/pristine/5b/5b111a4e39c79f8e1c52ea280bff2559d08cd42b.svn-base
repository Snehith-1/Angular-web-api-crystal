using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.system.DataAccess;
using ems.system.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.system.Controllers
{

    [Authorize]
    [RoutePrefix("api/Entitylist")]
    public class EntitylistController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaEntity objdaentitylist = new DaEntity();

        [ActionName("GetEntitySummary")]
        [HttpGet]
        public HttpResponseMessage GetEntitySummary()
        {
            MdlEntity values = new MdlEntity();
            objdaentitylist.DaGetEntitySummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEntity")]
        [HttpPost]
        public HttpResponseMessage PostEntity(entity_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaentitylist.DaPostEntity(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getupdateentitydetails")]
        [HttpPost]
        public HttpResponseMessage Getupdateentitydetails(entity_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaentitylist.DaGetupdateentitydetails(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getdeleteentitydetails")]
        [HttpGet]
        public HttpResponseMessage Getdeleteentitydetails(string entity_gid)
        {
            entity_lists objresult = new entity_lists();
            objdaentitylist.DaGetdeleteentitydetails(entity_gid,objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}