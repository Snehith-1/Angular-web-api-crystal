using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using static ems.sales.Models.MdlSmrMstPricesegmentSummary;

namespace ems.sales.Controllers
{
    [RoutePrefix("api/SmrMstPricesegmentSummary")]
    [Authorize]
    public class SmrMstPricesegmentSummaryController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrMstPricesegmentSummary objsales = new DaSmrMstPricesegmentSummary();

        // Price Segment Summary

        [ActionName("GetSmrMstPricesegmentSummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrMstPricesegmentSummary()
        {
            MdlSmrMstPricesegmentSummary values = new MdlSmrMstPricesegmentSummary();
            objsales.DaGetSmrMstPricesegmentSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostPriceSegment")]
        [HttpPost]
        public HttpResponseMessage PostPriceSegment(Getpricesegment_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaPostPostPriceSegment(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdatedPriceSegment")]
        [HttpPost]
        public HttpResponseMessage UpdatedPriceSegment(Getpricesegment_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaUpdatePriceSegment(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("deletePriceSegmentSummary")]
        [HttpGet]
        public HttpResponseMessage deletePriceSegmentSummary(string pricesegment_gid)
        {
            Getpricesegment_list objresult = new Getpricesegment_list();
            objsales.DadeletePriceSegmentSummary(pricesegment_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetPricesegmentgrid")]
        [HttpGet]
        public HttpResponseMessage GetPricesegmentgrid(string pricesegment_gid)
        {

            MdlSmrMstPricesegmentSummary values = new MdlSmrMstPricesegmentSummary();
            objsales.DaGetPricesegmentgrid(pricesegment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Product Group Dropdown
        [ActionName("GetSmrGroupDtl")]
        [HttpGet]
        public HttpResponseMessage GetSmrGroupDtl()
        {
            MdlSmrMstPricesegmentSummary values = new MdlSmrMstPricesegmentSummary();
            objsales.DaGetSmrGroupDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Product Name Dropdown
        [ActionName("GetSmrProductDtl")]
        [HttpGet]
        public HttpResponseMessage GetSmrProductDtl()
        {
            MdlSmrMstPricesegmentSummary values = new MdlSmrMstPricesegmentSummary();
            objsales.DaGetSmrProductDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Product Unit Dropdown
        [ActionName("GetSmrUnitDtl")]
        [HttpGet]
        public HttpResponseMessage GetSmrUnitDtl()
        {
            MdlSmrMstPricesegmentSummary values = new MdlSmrMstPricesegmentSummary();
            objsales.DaGetSmrUnitDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Product Assign Summary
        [ActionName("GetSmrMstProductAssignSummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrMstProductAssignSummary()
        {
            MdlSmrMstPricesegmentSummary values = new MdlSmrMstPricesegmentSummary();
            objsales.DaGetSmrMstProductAssignSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //On change Product Name
        [ActionName("GetOnChangeProductName")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeProductName(string product_gid)
        {
            MdlSmrMstPricesegmentSummary values = new MdlSmrMstPricesegmentSummary();
            objsales.DaGetOnChangeProductName(product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post Product
        [ActionName("PostProduct")]
        [HttpPost]
        public HttpResponseMessage PostProduct(Getproduct_list values, string branch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaPostProduct(getsessionvalues.user_gid, branch_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Product Head

        [ActionName("GetSmrMstProductHead")]
        [HttpGet]
        public HttpResponseMessage GetSmrMstProductHead()
        {
            MdlSmrMstPricesegmentSummary values = new MdlSmrMstPricesegmentSummary();
            objsales.DaGetSmrMstProductHead(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update

        [ActionName("GetUpdateProduct")]
        [HttpPost]
        public HttpResponseMessage GetUpdateProduct(Getproduct_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaGetUpdateProduct(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Un Assign

        [ActionName("GetUnAssignProduct")]
        [HttpGet]
        public HttpResponseMessage GetUnAssignProduct(string product_gid)
        {
            GetProduct objresult = new GetProduct();
            objsales.DaGetUnAssignProduct(product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetUnassignedlists")]
        [HttpGet]
        public HttpResponseMessage GetUnassignedlists(string pricesegment_gid)
        {
            MdlSmrMstPricesegmentSummary objresult = new MdlSmrMstPricesegmentSummary();
            objsales.DaGetUnassignedlists(pricesegment_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetAssignedlists")]
        [HttpGet]
        public HttpResponseMessage GetAssignedlists(string pricesegment_gid)
        {
            MdlSmrMstPricesegmentSummary objresult = new MdlSmrMstPricesegmentSummary();
            objsales.DaGetAssignedlists(pricesegment_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetUnassigned")]
        [HttpGet]
        public HttpResponseMessage GetUnassigned(string pricesegment_gid)
        {
            MdlSmrMstPricesegmentSummary objresult = new MdlSmrMstPricesegmentSummary();
            objsales.DaGetUnassigned(pricesegment_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("PostAssignedlist")]
        [HttpPost]
        public HttpResponseMessage PostAssignedlist(campaignassign_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaPostAssignedlist(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("PostUnassignedlist")]
        [HttpPost]
        public HttpResponseMessage PostUnassignedlist(campaignassign_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaPostUnassignedlist(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
    }
}
            