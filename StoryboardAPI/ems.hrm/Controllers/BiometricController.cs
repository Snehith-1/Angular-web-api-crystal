using ems.hrm.DataAccess;
using ems.hrm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.hrm.Controllers
{
    [Authorize]
    [RoutePrefix("api/Biometric")]
    public class BiometricController :ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaBiometric objDaBiometric = new DaBiometric();

        [ActionName("BiometricSummary")]
        [HttpGet]
        public HttpResponseMessage getBiometricSummary()
        {
            MdlBiometric objbiolist = new MdlBiometric();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaBiometric.DabiometricSummary(objbiolist);
            return Request.CreateResponse(HttpStatusCode.OK, objbiolist);
        }
        [ActionName("BiometricUpdate")]
        [HttpPost]
        public HttpResponseMessage postBiometricUpdate(popupupdate objbiolist)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaBiometric.DabiometricUpdate(objbiolist, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objbiolist);
        }

       
    }
}