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
using Newtonsoft.Json.Linq;

namespace ems.sales.Controllers
{
    [RoutePrefix("api/SmrTrnSalesorder")]
    [Authorize]
    public class SmrTrnSalesorderController  : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrTrnSalesorder objsales = new DaSmrTrnSalesorder();
        // Module Summary
        [ActionName("GetSmrTrnSalesordersummary")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnSalesordersummary()
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetSmrTrnSalesordersummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetViewsalesorderSummary")]
        [HttpGet]
        public HttpResponseMessage GetViewsalesorderSummary(string salesorder_gid)
        {
            MdlSmrTrnSalesorder objresult = new MdlSmrTrnSalesorder();
            objsales.DaGetViewsalesorderSummary(salesorder_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // Branch dropdown

        [ActionName("GetBranchDtl")]
        [HttpGet]
        public HttpResponseMessage GetBranchDtl()
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetBranchDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Customer dropdown

        [ActionName("GetCustomerDtl")]
        [HttpGet]
        public HttpResponseMessage GetCustomerDtl()
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetCustomerDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        // Customer dropdown 360

        [ActionName("GetCustomerDtlCRM")]
        [HttpGet]
        public HttpResponseMessage GetCustomerDtlCRM(string leadbank_gid)
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetCustomerDtlCRM(values, leadbank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Customer onchange 360

        [ActionName("GetCustomerOnchangeCRM")]
        [HttpGet]
        public HttpResponseMessage GetCustomerOnchangeCRM(string customercontact_gid)
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetCustomerOnchangeCRM(customercontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Person dropdown

        [ActionName("GetPersonDtl")]
        [HttpGet]
        public HttpResponseMessage GetPersonDtl()
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetPersonDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Currency dropdown

        [ActionName("GetCurrencyDtl")]
        [HttpGet]
        public HttpResponseMessage GetCurrencyDtl()
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetCurrencyDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // On change

        [ActionName("GetOnChangeCustomer")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeCustomer(string customer_gid)
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetOnChangeCustomer(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 1 dropdown

        [ActionName("GetTax1Dtl")]
        [HttpGet]
        public HttpResponseMessage GetTax1Dtl()
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetTax1Dtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 2 dropdown

        [ActionName("GetTax2Dtl")]
        [HttpGet]
        public HttpResponseMessage GetTax2Dtl()
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetTax2Dtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 3 dropdown

        [ActionName("GetTax3Dtl")]
        [HttpGet]
        public HttpResponseMessage GetTax3Dtl()
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetTax3Dtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Product dropdown

        [ActionName("GetProductNamDtl")]
        [HttpGet]
        public HttpResponseMessage GetProductNamDtl()
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetProductNamDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Product dropdown CRM

        [ActionName("GetProductNamDtlCRM")]
        [HttpGet]
        public HttpResponseMessage GetProductNamDtlCRM()
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetProductNamDtlCRM(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 4 dropdown

        [ActionName("GetTax4Dtl")]
        [HttpGet]
        public HttpResponseMessage GetTax4Dtl()
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetTax4Dtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnChangeProductsName")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeProductsName(string product_gid,string customercontact_gid)
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetOnChangeProductsName(product_gid, customercontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnChangeProductsNameAmend")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeProductsNameAmend(string product_gid)
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetOnChangeProductsNameAmend(product_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSalesOrder")]
        [HttpPost]
        public HttpResponseMessage PostSalesOrder(postsales_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaPostSalesOrder(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


       // productadd///
        [ActionName("PostOnAdds")]
        [HttpPost]
        public HttpResponseMessage PostOnAdds(salesorders_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaPostOnAdds(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Delete product summary//
        [ActionName("GetDeleteDirectSOProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeleteDirectSOProductSummary(string tmpsalesorderdtl_gid)
        {
            salesorders_list objresult = new salesorders_list();
            objsales.GetDeleteDirectSOProductSummary(tmpsalesorderdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //product summary/
        [ActionName("GetSalesOrdersummary")]
        [HttpGet] 
        public HttpResponseMessage GetSalesOrdersummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetSalesOrdersummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //On change currency
        [ActionName("GetOnChangeCurrency")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeCurrency(string currencyexchange_gid)
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetOnchangeCurrency(currencyexchange_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSalesProductdetails")]
        [HttpGet]
        public HttpResponseMessage GetSalesProductdetails(string salesorder_gid)
        {
            MdlSmrTrnSalesorder objresult = new MdlSmrTrnSalesorder();
            objsales.DaGetSalesProductdetails(salesorder_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getamendsalesorderdtl")]
        [HttpGet]
        public HttpResponseMessage Getamendsalesorderdtl(string salesorder_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrTrnSalesorder objresult = new MdlSmrTrnSalesorder();
            objsales.DaGetamendsalesorderdtl(getsessionvalues.employee_gid,salesorder_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getamendsalesorderdetails")]
        [HttpGet]
        public HttpResponseMessage Getamendsalesorderdetails(string salesorder_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSmrTrnSalesorder objresult = new MdlSmrTrnSalesorder();
            objsales.DaGetamendsalesorderdetails(getsessionvalues.employee_gid, salesorder_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("updateSalesOrderedit")]
        [HttpPost]
        public HttpResponseMessage updateSalesOrderedit(salesorders_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaupdateSalesOrderedit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AmendSalesOrder")]
        [HttpPost]
        public HttpResponseMessage AmendSalesOrder(Getamendsalesorderdtl1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaAmendSalesOrder(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("getCancelSalesOrder")]
        [HttpGet]
        public HttpResponseMessage getCancelSalesOrder(string params_gid)
        {
            MdlSmrTrnSalesorder objresult = new MdlSmrTrnSalesorder();
            objsales.DagetCancelSalesOrder(params_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // CRM product on change
        [ActionName("GetOnChangeProductsNameCRM")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeProductsNameCRM(string product_gid)
        {
            MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
            objsales.DaGetOnChangeProductsNameCRM(product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // DIRECT SALES ORDER PRODUCT EDIT

        [ActionName("GetDirectSalesOrderEditProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetDirectSalesOrderEditProductSummary(string tmpsalesorderdtl_gid)
        {
            MdlSmrTrnSalesorder objresult = new MdlSmrTrnSalesorder();
            objsales.DaGetDirectSalesOrderEditProductSummary(tmpsalesorderdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetSalesorderdetail")]
        [HttpGet]
        public HttpResponseMessage GetSalesorderdetail(string product_gid)
        {
            MdlSmrTrnSalesorder objresult = new MdlSmrTrnSalesorder();
            objsales.DaGetSalesorderdetail(product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        // UPDATE PRODUCT -- DIRECT QUOTATION PRODUCT SUMMARY

        [ActionName("PostUpdateDirectSOProduct")]
        [HttpPost]
        public HttpResponseMessage PostUpdateDirectSOProduct(DirecteditSalesorderList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objsales.DaPostUpdateDirectSOProduct(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getsalesonupdate")]
 [HttpPost]
 public HttpResponseMessage Getsalesonupdate()
 {
     MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
     objsales.DaGetsalesonupdate(getsessionvalues.employee_gid,values);
     return Request.CreateResponse(HttpStatusCode.OK, values);
 }

 [ActionName("Getupdate")]
 [HttpGet]
 public HttpResponseMessage Getupdate()
 {
     MdlSmrTrnSalesorder values = new MdlSmrTrnSalesorder();
     objsales.DaGetupdate(getsessionvalues.employee_gid, values);
     return Request.CreateResponse(HttpStatusCode.OK, values);
 }
    }
}
