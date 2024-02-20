using ems.hrm.DataAccess;
using ems.hrm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;


namespace ems.hrm.Controllers
{
    [RoutePrefix("api/HrmTrnAdmincontrol")]
    [Authorize]
    public class HrmTrnAdmincontrolController  : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaHrmTrnAdmincontrol objdaemployeelist = new DaHrmTrnAdmincontrol();

        [ActionName("GetEmployeedtlSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeedtlSummary()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetEmployeedtlSummary(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDocumentlist")]
        [HttpGet]
        public HttpResponseMessage GetDocumentlist()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetDocumentlist(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getbranchdropdown")]
        [HttpGet]

        public HttpResponseMessage Getbranchdropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetbranchdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getworkertypedropdown")]
        [HttpGet]

        public HttpResponseMessage Getworkertypedropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetworkertypedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getjobtypedropdown")]
        [HttpGet]

        public HttpResponseMessage Getjobtypedropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetjobtypedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getroledropdown")]
        [HttpGet]

        public HttpResponseMessage Getroledropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetroledropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getholidaygradedropdown")]
        [HttpGet]

        public HttpResponseMessage Getholidaygradedropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetholidaygradedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getshifttypedropdown")]
        [HttpGet]

        public HttpResponseMessage Getshifttypedropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetshifttypedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getleavegradedropdown")]
        [HttpGet]

        public HttpResponseMessage Getleavegradedropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetleavegradedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getreportingtodropdown")]
        [HttpGet]

        public HttpResponseMessage Getreportingtodropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetreportingtodropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }




        [ActionName("PostEmployeedetails")]
        [HttpPost]
        public HttpResponseMessage PostEmployeedetails(employee_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaemployeelist.DaPostEmployeedetails(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getentitydropdown")]
        [HttpGet]
        public HttpResponseMessage Getentitydropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetentitydropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getbloodgroupdropdown")]
        [HttpGet]
        public HttpResponseMessage Getbloodgroupdropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetbloodgroupdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getdepartmentdropdown")]
        [HttpGet]
        public HttpResponseMessage Getdepartmentdropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetdepartmentdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getdesignationdropdown")]
        [HttpGet]
        public HttpResponseMessage Getdesignationdropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetdesignationdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcountrydropdown")]
        [HttpGet]
        public HttpResponseMessage Getcountrydropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetcountrydropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("Getcountry2dropdown")]
        [HttpGet]
        public HttpResponseMessage Getcountry2dropdown()
        {
            MdlHrmTrnAdmincontrol values = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetcountry2dropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("GetEditEmployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetEditEmployeeSummary(string employee_gid)
        {
            MdlHrmTrnAdmincontrol objresult = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetEditEmployeeSummary(employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getresetpassword")]
        [HttpPost]
        public HttpResponseMessage Getresetpassword(employee_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaemployeelist.DaGetresetpassword(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getupdateusercode")]
        [HttpPost]
        public HttpResponseMessage Getupdateusercode(employee_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaemployeelist.DaGetupdateusercode(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }




        [ActionName("UpdateEmployeedetails")]
        [HttpPost]
        public HttpResponseMessage UpdateEmployeedetails(employee_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaemployeelist.DaUpdateEmployeedetails(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("Getupdateuserdeactivate")]
        [HttpPost]
        public HttpResponseMessage Getupdateuserdeactivate(employee_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaemployeelist.DaGetupdateuserdeactivate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EmployeeImport")]
        [HttpPost]
        public HttpResponseMessage EmployeeImport()
        {
            HttpRequest httpRequest;
            employee_lists values = new employee_lists();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objdaemployeelist.DaEmployeeImport(httpRequest, getsessionvalues.user_gid, objResult, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetDocumentDtllist")]
        [HttpGet]
        public HttpResponseMessage GetDocumentDtllist(string document_gid)
        {
            MdlHrmTrnAdmincontrol objresult = new MdlHrmTrnAdmincontrol();
            objdaemployeelist.DaGetDocumentDtllist(document_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        //[ActionName("AgeCaluculation")]
        //[HttpGet]
        //public HttpResponseMessage AgeCaluculation(string dob)
        //{
        //    agelist objresult = new agelist();
        //    objresult = objdaemployeelist.DaAgeCaluculation(dob);
        //    return Request.CreateResponse(HttpStatusCode.OK, objresult);
        //}

    }
}