﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.sales.Controllers
{
    [Authorize]
    [RoutePrefix("api/SmrDashboard")]
    public class SmrDashboardController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrDashboard objDaSmrDashboard = new DaSmrDashboard();

        // Recursive Looping ChildLoop Function to get Employee GID Hierarchywise

        [ActionName("GetChildLoop")]
        [HttpGet]
        public HttpResponseMessage GetChildLoop()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrDashboard values = new MdlSmrDashboard();
            objDaSmrDashboard.DaGetChildLoop(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // SalesPerformanceChart

        [ActionName("GetSalesPerformanceChart")]
        [HttpGet]
        public HttpResponseMessage GetDashboardCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrDashboard values = new MdlSmrDashboard();
            objDaSmrDashboard.DaGetSalesPerformanceChart(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // GetSalesOrderCount

        [ActionName("GetSalesOrderCount")]
        [HttpGet]
        public HttpResponseMessage GetSalesOrderCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrDashboard values = new MdlSmrDashboard();
            objDaSmrDashboard.DaGetSalesOrderCount(getsessionvalues.employee_gid, getsessionvalues.employee_gid , values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // GetSalesMoreOrderCount

        [ActionName("GetMoreSalesOrderCount")]
        [HttpGet]
        public HttpResponseMessage GetMoreSalesOrderCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrDashboard values = new MdlSmrDashboard();
            objDaSmrDashboard.DaGetMoreSalesOrderCount(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetOwnOverallSalesOrderChart

        [ActionName("GetOwnOverallSalesOrderChart")]
        [HttpGet]
        public HttpResponseMessage GetOwnOverallSalesOrderChart()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrDashboard values = new MdlSmrDashboard();
            objDaSmrDashboard.DaGetOwnOverallSalesOrderChart(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetHierarchyOverallSalesOrderChart

        [ActionName("GetHierarchyOverallSalesOrderChart")]
        [HttpGet]
        public HttpResponseMessage GetHierarchyOverallSalesOrderChart()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrDashboard values = new MdlSmrDashboard();
            objDaSmrDashboard.DaGetHierarchyOverallSalesOrderChart(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetOwnOverallDeliveryOrderChart

        [ActionName("GetOwnOverallDeliveryOrderChart")]
        [HttpGet]
        public HttpResponseMessage GetOwnOverallDeliveryOrderChart()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrDashboard values = new MdlSmrDashboard();
            objDaSmrDashboard.DaGetOwnOverallDeliveryOrderChart(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetHierarchyOverallDeliveryOrderChart

        [ActionName("GetHierarchyOverallDeliveryOrderChart")]
        [HttpGet]
        public HttpResponseMessage GetHierarchyOverallDeliveryOrderChart()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrDashboard values = new MdlSmrDashboard();
            objDaSmrDashboard.DaGetHierarchyOverallDeliveryOrderChart(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GetMTDCounts

        [ActionName("GetMTDCounts")]
        [HttpGet]
        public HttpResponseMessage GetMTDCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrDashboard values = new MdlSmrDashboard();
            objDaSmrDashboard.DaGetMTDCounts(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // GetMTDCounts

        [ActionName("GetYTDCounts")]
        [HttpGet]
        public HttpResponseMessage GetYTDCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrDashboard values = new MdlSmrDashboard();
            objDaSmrDashboard.DaGetYTDCounts(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //GetMonthlySalesChart

        [ActionName("GetMonthlySalesChart")]
        [HttpGet]
        public HttpResponseMessage GetMonthlySalesChart()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrDashboard values = new MdlSmrDashboard();
            objDaSmrDashboard.DaGetMonthlySalesChart(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}