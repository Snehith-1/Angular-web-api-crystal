using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace ems.crm.Controllers
{
    [Authorize]
    [RoutePrefix("api/ProductReport")]
    
    public class ProductReportController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaProductReport objreport = new DaProductReport();

        [ActionName("GetProductConsumptionReport")]
        [HttpGet]
        public HttpResponseMessage GetProductConsumptionReport()
        {
            MdlProductReport values = new MdlProductReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objreport.DaGetProductConsumptionReport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductReportgrid")]
        [HttpGet]
        public HttpResponseMessage GetProductReportgrid(string product_gid)
        {
            MdlProductReport values = new MdlProductReport();
            objreport.DaProductReportgrid(product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductGroupwiseChart")]
        [HttpGet]
        public HttpResponseMessage GetProductGroupwiseChart()
        {
            MdlProductReport values = new MdlProductReport();
            objreport.DaProductGroupwiseChart(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductGroupwiseChartSearch")]
        [HttpGet]
        public HttpResponseMessage GetProductGroupwiseChartSearch(string from_date, string to_date)
        {
            MdlProductReport values = new MdlProductReport();
            objreport.DaProductGroupwiseChartSearch(values,from_date, to_date);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductdropdown")]
        [HttpGet]
        public HttpResponseMessage GetProductdropdown()
        {
            MdlProductReport values = new MdlProductReport();
            objreport.DaGetProductdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetReportProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetReportProductSummary(string product_name)
        {
            MdlProductReport values = new MdlProductReport();
            objreport.DaGetReportProductSummary(values, product_name);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetReportSalesOrderSummary")]
        [HttpGet]
        public HttpResponseMessage GetReportSalesOrderSummary(string product_name)
        {
            MdlProductReport values = new MdlProductReport();
            objreport.DaGetReportSalesOrderSummary(values, product_name);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}