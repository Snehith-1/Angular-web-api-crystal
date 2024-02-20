using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;


namespace ems.sales.Controllers
{
    [RoutePrefix("api/SmrMstProductUnit")]
    [Authorize]
    public class SmrMstProductUnitController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrMstProductUnit objsales = new DaSmrMstProductUnit();
        // Product Unit Summary
        [ActionName("GetSalesProductUnitSummary")]
        [HttpGet]
        public HttpResponseMessage GetSalesProductUnitSummary()
        {
            MdlSmrMstProductUnit values = new MdlSmrMstProductUnit();
            objsales.DaGetSalesProductUnitSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSalesProductUnit")]
        [HttpPost]
        public HttpResponseMessage PostSalesProductUnit(salesproductunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaPostSalesProductUnit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSalesProductUnitSummarygrid")]
        [HttpGet]
        public HttpResponseMessage GetSalesProductUnitSummarygrid(string productuomclass_gid)
        {

            MdlSmrMstProductUnit values = new MdlSmrMstProductUnit();
            objsales.DaGetSalesProductUnitSummarygrid(productuomclass_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatedSalesProductunit")]
        [HttpPost]
        public HttpResponseMessage UpdatedSalesProductunit(salesproductunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaUpdatedSalesProductunit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deleteSalesProductunitSummary")]
        [HttpGet]
        public HttpResponseMessage deleteSalesProductunitSummary(string productuomclass_gid)
        {
            salesproductunit_list objresult = new salesproductunit_list();
            objsales.DadeleteSalesProductunitSummary(productuomclass_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }




        // for Product unit class


        [ActionName("GetProductunits")]
        [HttpGet]
        public HttpResponseMessage GetProductunits(string productuomclass_gid)
        {
            MdlSmrMstProductUnit values = new MdlSmrMstProductUnit();
            objsales.DaGetProductunits(productuomclass_gid ,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostProductunits")]
        [HttpPost]
        public HttpResponseMessage PostProductunits(salesproductunitgrid_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.PostProductunits(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}