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
using ems.utilities.Models;


namespace ems.payroll.Controllers
{
    [Authorize]
    [RoutePrefix("api/PayTrnSalaryManagement")]
    public class PayTrnSalaryManagementController :ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPayTrnSalaryManagement objdaemployeesalary = new DaPayTrnSalaryManagement();


        [ActionName("GetEmployeeSalaryManagement")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeSalaryManagement()
        {
            MdlPayTrnSalaryManagement values = new MdlPayTrnSalaryManagement();
            objdaemployeesalary.DaEmployeeSalaryManagement(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEmployeeSelect")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeSelect(string month,string year)
        {
            MdlPayTrnSalaryManagement values = new MdlPayTrnSalaryManagement();
            objdaemployeesalary.DaGetEmployeeSelect(month,year,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getpayrunsummary")]
        [HttpGet]
        public HttpResponseMessage Getpayrunsummary(string month, string year)
        {
            MdlPayTrnSalaryManagement values = new MdlPayTrnSalaryManagement();
            objdaemployeesalary.DaGetpayrunsummary(month, year, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Postforpayrun")]
        [HttpPost]
        public HttpResponseMessage Postforpayrun(GetEmployeelist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaemployeesalary.DaPostforpayrun(values);
            return Request.CreateResponse(HttpStatusCode.OK,values);
        }

        [ActionName("Updatemonthlypayrun")]
        [HttpPost]
        public HttpResponseMessage Updatemonthlypayrun(Getmonthlypayrun values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdaemployeesalary.DaUpdatemonthlypayrun(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetManageLeave")]
        [HttpGet]
        public HttpResponseMessage GetManageLeave(string month, string year)
        {
            MdlPayTrnSalaryManagement values = new MdlPayTrnSalaryManagement();
            objdaemployeesalary.DaGetManageLeave(month,year,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Additionalsubsummary")]
        [HttpGet]
        public HttpResponseMessage Additionalsubsummary(string salary_gid)
        {
            MdlPayTrnSalaryManagement values = new MdlPayTrnSalaryManagement();
            objdaemployeesalary.DaAdditionalsubsummary(salary_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Deductsubsummary")]
        [HttpGet]
        public HttpResponseMessage Deductsubsummary(string salary_gid)
        {
            MdlPayTrnSalaryManagement values = new MdlPayTrnSalaryManagement();
            objdaemployeesalary.DaDeductsubsummary(salary_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



    }
}
   
