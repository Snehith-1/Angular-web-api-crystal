using ems.hrm.DataAccess;
using ems.hrm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using Org.BouncyCastle.Asn1.Ocsp;
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
    [RoutePrefix("api/OfferLetter")]
    public class OfferLetterController :ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaOfferLetter objdaoffer = new DaOfferLetter();

        [ActionName("OfferLetterSummary")]
        [HttpGet]
        public HttpResponseMessage OfferLetterSummary()
        {
            MdlOfferLetter values = new MdlOfferLetter();
            objdaoffer.DaOfferLetterSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Addofferletter")]
        [HttpPost]
        public HttpResponseMessage Addofferletter(AddOfferletter_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaoffer.DaAddofferletter(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}