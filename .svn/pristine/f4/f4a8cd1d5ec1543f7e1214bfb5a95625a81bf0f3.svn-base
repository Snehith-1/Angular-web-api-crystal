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
    [RoutePrefix("api/Employeelist")]
    public class EmployeelistController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaEmployeelist objdaemployeelist = new DaEmployeelist();
        [ActionName("PostEmployeedetails")]
        [HttpPost]
        public HttpResponseMessage PostEmployeedetails(employee_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaemployeelist.DaPostEmployeedetails(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateEmployeedetails")]
        [HttpPost]
        public HttpResponseMessage UpdateEmployeedetails(employee_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaemployeelist.DaUpdateEmployeedetails(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("Getentitydropdown")]
        [HttpGet]
        public HttpResponseMessage Getentitydropdown()
        {
            MdlEmployeelist values = new MdlEmployeelist();
            objdaemployeelist.DaGetentitydropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EmployeeProfileUpload")]
        [HttpPost]
        public HttpResponseMessage EmployeeProfileUpload()
        {
            HttpRequest httpRequest;
            //Postassetlocationcreation values = new Postassetlocationcreation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objdaemployeelist.DaEmployeeProfileUpload(httpRequest, objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("UpdateEmployeeProfileUpload")]
        [HttpPost]
        public HttpResponseMessage UpdateEmployeeProfileUpload()
        {
            HttpRequest httpRequest;
            //Postassetlocationcreation values = new Postassetlocationcreation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objdaemployeelist.DaUpdateEmployeeProfileUpload(httpRequest, objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetEmployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeSummary()
        {
            MdlEmployeelist values = new MdlEmployeelist();
            objdaemployeelist.DaGetEmployeeSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("Getbranchdropdown")]
        [HttpGet]
        public HttpResponseMessage Getbranchdropdown()
        {
            MdlEmployeelist values = new MdlEmployeelist();
            objdaemployeelist.DaGetbranchdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getdepartmentdropdown")]
        [HttpGet]
        public HttpResponseMessage Getdepartmentdropdown()
        {
            MdlEmployeelist values = new MdlEmployeelist();
            objdaemployeelist.DaGetdepartmentdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getdesignationdropdown")]
        [HttpGet]
        public HttpResponseMessage Getdesignationdropdown()
        {
            MdlEmployeelist values = new MdlEmployeelist();
            objdaemployeelist.DaGetdesignationdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcountrydropdown")]
        [HttpGet]
        public HttpResponseMessage Getcountrydropdown()
        {
            MdlEmployeelist values = new MdlEmployeelist();
            objdaemployeelist.DaGetcountrydropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("Getcountry2dropdown")]
        [HttpGet]
        public HttpResponseMessage Getcountry2dropdown()
        {
            MdlEmployeelist values = new MdlEmployeelist();
            objdaemployeelist.DaGetcountry2dropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("GetEditEmployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetEditEmployeeSummary(string employee_gid)
        {
            MdlEmployeelist objresult = new MdlEmployeelist();
            objdaemployeelist.DaGetEditEmployeeSummary(employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getresetpassword")]
        [HttpPost]
        public HttpResponseMessage Getresetpassword(employee_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaemployeelist.DaGetresetpassword(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getupdateusercode")]
        [HttpPost]
        public HttpResponseMessage Getupdateusercode(employee_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaemployeelist.DaGetupdateusercode(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getupdateuserdeactivate")]
        [HttpPost]
        public HttpResponseMessage Getupdateuserdeactivate(employee_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaemployeelist.DaGetupdateuserdeactivate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
    }