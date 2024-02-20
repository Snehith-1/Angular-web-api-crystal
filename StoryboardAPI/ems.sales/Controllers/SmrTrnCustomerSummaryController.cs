using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.sales.Models;
using ems.sales.DataAccess;

namespace ems.sales.Controllers
{
    [RoutePrefix("api/SmrTrnCustomerSummary")]
    [Authorize]
    public class SmrTrnCustomerSummaryController:ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrTrnCustomerSummary objDaSales = new DaSmrTrnCustomerSummary();
        // Module Summary
        [ActionName("GetSmrTrnCustomerSummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnCustomerSummary()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetSmrTrnCustomerSummary( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSmrTrnDistributorSummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnDistributorSummary()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetSmrTrnDistributorSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSmrTrnRetailerSummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnRetailerSummary()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetSmrTrnRetailerSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSmrTrnCorporateSummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnCorporateSummary()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetSmrTrnCorporateSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSmrTrnCustomerCount")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnCustomerCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetSmrTrnCustomerCount(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("Getcountry")]
        [HttpGet]
        public HttpResponseMessage Getcountry()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetcountry(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("Getcurency")]
        [HttpGet]
        public HttpResponseMessage Getcurency()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetcurrency(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("Gettax")]
        [HttpGet]
        public HttpResponseMessage Gettax()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGettax(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("Getregion")]
        [HttpGet]
        public HttpResponseMessage Getregion()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetregion(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("Getcustomercity")]
        [HttpGet]
        public HttpResponseMessage Getcustomercity()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetcustomercity(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetCustomerCode")]
        [HttpGet]
        public HttpResponseMessage GetCustomerCode()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetCustomerCode(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetOnChangeCountry")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeCountry(string country_gid)
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.GetOnChangeCountry(country_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("Postcustomer")]
        [HttpPost]
        public HttpResponseMessage Postcustomer(postcustomer_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaPostCustomer(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //View
        [ActionName("GetViewcustomerSummary")]
        [HttpGet]
        public HttpResponseMessage GetViewcustomerSummary( string customer_gid)
        {
            MdlSmrTrnCustomerSummary objresult = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetViewcustomerSummary(customer_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetcustomerInactive")]
        [HttpGet]
        public HttpResponseMessage GetcustomerInactive(string params_gid)
        {
            MdlSmrTrnCustomerSummary objresult = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetcustomerInactive(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetcustomerActive")]
        [HttpGet]
        public HttpResponseMessage GetcustomerActive(string params_gid)
        {
            MdlSmrTrnCustomerSummary objresult = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetcustomerActive(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // Module Summary
        [ActionName("GetProductAssignSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductAssignSummary(string customer_gid)
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetProductAssignSummary(customer_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Customer Edit
        [ActionName("GetEditCustomer")]
        [HttpGet]
        public HttpResponseMessage GetEditCustomer(string customer_gid)
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetEditCustomer(customer_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //customer segment header
        [ActionName("Getcustomername")]
        [HttpGet]
        public HttpResponseMessage Getcustomername(string customer_gid)
        {
            MdlSmrTrnCustomerSummary objresult = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetcustomername(customer_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("DaUpdateCostomer")]
        [HttpPost]
        public HttpResponseMessage DaUpdateCostomer(GetCustomerlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaUpdateCostomer(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //customer Price segment Add
        [ActionName("Customerprice")]
        [HttpPost]
        public HttpResponseMessage Customerprice(Getproductlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaCustomerprice(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Customerpriceupdate")]
        [HttpPost]
        public HttpResponseMessage Customerpriceupdate(Getproductlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaCustomerpriceupdate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerImport")]
        [HttpPost]
        public HttpResponseMessage CustomerImport()
        {
            HttpRequest httpRequest;
            postcustomer_list values = new postcustomer_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaSales.DaCustomerImport(httpRequest, getsessionvalues.user_gid, objResult, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        //Customer Branch Add
        [ActionName("PostCustomerbranch")]
        [HttpPost]
        public HttpResponseMessage PostCustomerbranch(customerbranch_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objDaSales.DaPostCustomerbranch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSmrTrnCustomerBranchSummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnCustomerBranchSummary(string customer_gid)
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetSmrTrnCustomerBranch(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        //[ActionName("GetCustomerReportExport")]
        //[HttpPost]
        public HttpResponseMessage GetCustomerReportExport()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetCustomerReportExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //[ActionName("GetDocumentlist")]
        //[HttpGet]
        //public HttpResponseMessage GetDocumentlist()
        //{
        //    MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
        //    objDaSales.DaGetDocumentlist(getsessionvalues.user_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //[ActionName("GetDocumentDtllist")]
        //[HttpGet]
        //public HttpResponseMessage GetDocumentDtllist(string document_gid)
        //{
        //    MdlSmrTrnCustomerSummary objresult = new MdlSmrTrnCustomerSummary();
        //    objDaSales.DaGetDocumentDtllist(document_gid, objresult);
        //    return Request.CreateResponse(HttpStatusCode.OK, objresult);
        //}

        [ActionName("Getbranch")]
        [HttpGet]
        public HttpResponseMessage Getbranch(string customer_gid)
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetbranch(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        //Customer Contact
        [ActionName("PostCustomercontact")]
        [HttpPost]
        public HttpResponseMessage PostCustomercontact(customercontact_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objDaSales.DaPostCustomercontact(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSmrTrnCustomerContact")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnCustomerContact(string customer_gid)
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetSmrTrnCustomerContact(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerTypeSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerTypeSummary()
        {
            MdlSmrTrnCustomerSummary values = new MdlSmrTrnCustomerSummary();
            objDaSales.DaGetCustomerTypeSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}