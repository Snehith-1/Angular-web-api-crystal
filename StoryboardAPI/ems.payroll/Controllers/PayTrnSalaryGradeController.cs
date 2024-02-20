using ems.payroll.DataAccess;
using ems.payroll.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.payroll.Controllers
{
    [Authorize]
    [RoutePrefix("api/PayTrnSalaryGrade")]
    public class PayTrnSalaryGradeController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPayTrnSalaryGrade objdasalarygrade = new DaPayTrnSalaryGrade();


        [ActionName("SalarygradeSummary")]
        [HttpGet]
        public HttpResponseMessage SalarygradeSummary()
       {
            MdlSalaryGrade values = new MdlSalaryGrade();
            objdasalarygrade.DaSalarygradeSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AdditionComponentSummary")]
        [HttpGet]
        public HttpResponseMessage AdditionComponentSummary()
        {
            MdlSalaryGrade values = new MdlSalaryGrade();
            objdasalarygrade.DaAdditionComponentSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeductionComponentSummary")]
        [HttpGet]
        public HttpResponseMessage DeductionComponentSummary()
        {
            MdlSalaryGrade values = new MdlSalaryGrade();
            objdasalarygrade.DaDeductionComponentSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcomponentname")]
        [HttpGet]
        public HttpResponseMessage Getcomponentname(string componentgroup_name)
        {
            MdlSalaryGrade values = new MdlSalaryGrade();
            objdasalarygrade.DaGetcomponentname(componentgroup_name,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcomponentamount")]
        [HttpGet]
        public HttpResponseMessage Getcomponentamount(string salarycomponent_gid)
        {
            MdlSalaryGrade values = new MdlSalaryGrade();
            objdasalarygrade.DaGetcomponentamount(salarycomponent_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostSalaryGrade")]
        [HttpPost]
        public HttpResponseMessage PostSalaryGrade(SalaryGradeData values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdasalarygrade.DaPostSalaryGrade(getsessionvalues.user_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getpopup")]
        [HttpGet]
        public HttpResponseMessage Getpopup(string salarygradetemplate_gid,string salarygradetype)
        {
            MdlSalaryGrade values = new MdlSalaryGrade();
            objdasalarygrade.DaGetpopup(salarygradetemplate_gid, salarygradetype, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditgrade")]
        [HttpGet]
        public HttpResponseMessage GetEditgrade(string salarygradetemplate_gid )
        {
            MdlSalaryGrade values = new MdlSalaryGrade();
            objdasalarygrade.DaGetEditgrade(salarygradetemplate_gid,  values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("Editdeduction")]
        [HttpGet]
        public HttpResponseMessage Editdeduction(string salarygradetemplate_gid)
        {
            MdlSalaryGrade values = new MdlSalaryGrade();
            objdasalarygrade.Editdeduction(salarygradetemplate_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSalaryGrade")]
        [HttpPost]
        public HttpResponseMessage UpdateSalaryGrade(UpdateSalaryGradeData values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdasalarygrade.DaUpdateSalaryGrade(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}