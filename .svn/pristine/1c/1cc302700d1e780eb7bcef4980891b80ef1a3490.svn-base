using ems.sales.DataAccess;
using ems.sales.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace ems.sales.Controllers
{
    [RoutePrefix("api/SmrTrnQuotation")]
    [Authorize]
    public class SmrTrnQuotationController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSmrTrnQuotation objDaSales = new DaSmrTrnQuotation();
        // Module Summary
        [ActionName("GetSmrTrnQuotation")]
        [HttpGet]
        public HttpResponseMessage GetSmrTrnQuotation()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetSmrTrnQuotation(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //  sales person dropdown

        [ActionName("GetSalesDtl")]
        [HttpGet]
        public HttpResponseMessage GetSalesDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetSalesDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Currency dropdown

        [ActionName("GetCurrencyCodeDtl")]
        [HttpGet]
        public HttpResponseMessage GetCurrencyCodeDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetCurrencyCodeDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 1 dropdown

        [ActionName("GetTaxOnceDtl")]
        [HttpGet]
        public HttpResponseMessage GetTaxOnceDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetTaxOnceDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 2 dropdown

        [ActionName("GetTaxTwiceDtl")]
        [HttpGet]
        public HttpResponseMessage GetTaxTwiceDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetTaxTwiceDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 3 dropdown

        [ActionName("GetTaxThriceDtl")]
        [HttpGet]
        public HttpResponseMessage GetTaxThriceDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetTaxThriceDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Product dropdown

        [ActionName("GetProductNamesDtl")]
        [HttpGet]
        public HttpResponseMessage GetProductNamesDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetProductNamesDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Product dropdown CRM

        [ActionName("GetProductNamesDtlCRM")]
        [HttpGet]
        public HttpResponseMessage GetProductNamesDtlCRM()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetProductNamesDtlCRM(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 4 dropdown

        [ActionName("GetTaxFourSDtl")]
        [HttpGet]
        public HttpResponseMessage GetTaxFourSDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetTaxFourSDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // On change
        [ActionName("GetOnChangeProductsName")]
        [HttpGet]

        public HttpResponseMessage GetOnChangeProductsName(string product_gid, string customercontact_gid)

        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetOnChangeProductsName(product_gid, customercontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        // On change product for amend
        [ActionName("GetOnChangeProductsNames")]
        [HttpGet]

        public HttpResponseMessage GetOnChangeProductsNames(string product_gid)

        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetOnChangeProductsNames(product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        // On change
        [ActionName("GetOnChangeProductsNameQTO")]
        [HttpGet]

        public HttpResponseMessage GetOnChangeProductsNameQTO(string product_gid)

        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.GetOnChangeProductsNameQTO(product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        // Summary bind

        [ActionName("GetRaiseSOSummary")]
        [HttpGet]
        public HttpResponseMessage GetRaiseSOSummary(string quotation_gid)
        {
            MdlSmrTrnQuotation objresult = new MdlSmrTrnQuotation();
            objDaSales.DaGetRaiseSOSummary(quotation_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }



        //Raise Quoatation

        // Branch dropdown

        [ActionName("GetBranchDtl")]
        [HttpGet]
        public HttpResponseMessage GetBranchDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetBranchDt(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //customer

        [ActionName("GetCustomerDtl")]
        [HttpGet]
        public HttpResponseMessage GetCustomerDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetCustomerDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        

        // Person dropdown

        [ActionName("GetPersonDtl")]
        [HttpGet]
        public HttpResponseMessage GetPersonDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetPersonDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Currency dropdown

        [ActionName("GetCurrencyDtl")]
        [HttpGet]
        public HttpResponseMessage GetCurrencyDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetCurrencyDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Product dropdown


        [ActionName("GetProductDtl")]
        [HttpGet]
        public HttpResponseMessage GetProductDtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetProductDtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 1 dropdown

        [ActionName("GetTax1Dtl")]
        [HttpGet]
        public HttpResponseMessage GetTax1Dtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetTax1Dtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 2 dropdown

        [ActionName("GetTax2Dtl")]
        [HttpGet]
        public HttpResponseMessage GetTax2Dtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetTax2Dtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tax 3 dropdown

        [ActionName("GetTax3Dtl")]
        [HttpGet]
        public HttpResponseMessage GetTax3Dtl()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetTax3Dtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Temp Summary
        [ActionName("GetTemporarySummary")]
        [HttpGet]

        public HttpResponseMessage GetTemporarySummary(string quotation_gid)
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaGetTemporarySummary(getsessionvalues.employee_gid, quotation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }


        // Product Add 
        [ActionName("GetProductAdd")]
        [HttpPost]
        public HttpResponseMessage GetProductAdd(summarys_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaGetProductAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // On change customer
        [ActionName("GetOnChangeCustomerDtls")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeCustomerDtls(string customercontact_gid)
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetOnChangeCustomerDtls(customercontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Terms And Condition dropdown

        [ActionName("GetTermsandConditions")]
        [HttpGet]
        public HttpResponseMessage GetTermsandConditions()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetTermsandConditions(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // On change t and c
        [ActionName("GetOnChangeTerms")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeTerms(string template_gid)
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetOnChangeTerms(template_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        // temp product summary by pugaz 

        [ActionName("GetTempProductsSummary")]
        [HttpGet]
        public HttpResponseMessage GetTempProductsSummary()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaGetTempProductsSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // product submit for Add quotation by pugaz
        [ActionName("PostAddProduct")]
        [HttpPost]
        public HttpResponseMessage PostAddProduct(summaryprod_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaPostAddProduct(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // delete for enq to quote event

        [ActionName("GetDeleteQuotationProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeleteQuotationProductSummary(string tmpquotationdtl_gid)
        {
            summaryprod_list objresult = new summaryprod_list();
            objDaSales.DaGetDeleteQuotationProductSummary(tmpquotationdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // Overall submit for QuotationToOrder

        [ActionName("PostQuotationToOrder")]
        [HttpPost]
        public HttpResponseMessage PostQuotationToOrder(MdlSmrTrnQuotation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaPostQuotationToOrder(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Overall submit For Direct Quotation

        [ActionName("PostDirectQuotation")]
        [HttpPost]
        public HttpResponseMessage PostDirectQuotation(Post_List values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaPostDirectQuotation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetViewQuotationSummary")]
        [HttpGet]
        public HttpResponseMessage GetViewQuotationSummary(string quotation_gid)
        {
            MdlSmrTrnQuotation objresult = new MdlSmrTrnQuotation();
            objDaSales.DaGetViewQuotationSummary(quotation_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetOnChangeCurrency")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeCurrency(string currencyexchange_gid)
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetOnchangeCurrency(currencyexchange_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductdetails")]
        [HttpGet]
        public HttpResponseMessage GetProductdetails(string quotation_gid)
        {
            MdlSmrTrnQuotation objresult = new MdlSmrTrnQuotation();
            objDaSales.DaGetProductdetails(quotation_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetQuotationamend")]
        [HttpGet]

        public HttpResponseMessage GetQuotationamend(string quotation_gid)
        {
            Quotationlist values = new Quotationlist();
            values = objDaSales.DaGetQuotationamend(quotation_gid);

            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetQuotattionproductSummary")]
        [HttpGet]
        public HttpResponseMessage GetQuotattionproductSummary(string quotation_gid)
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaSales.DaGetQuotattionproductSummary(values, quotation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("getDeleteQuotation")]
        [HttpGet]
        public HttpResponseMessage getDeleteQuotation(string params_gid)
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DagetDeleteQuotation(params_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getquotationhistorydata")]
        [HttpGet]
        public HttpResponseMessage Getquotationhistorydata(string quotation_gid)
        {
            quotationhistorylist values = new quotationhistorylist();
            values = objDaSales.DaGetquotationhistorydata(quotation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getquotationhistorysummarydata")]
        [HttpGet]
        public HttpResponseMessage Getquotationhistorysummarydata(string quotation_gid)
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();

            objDaSales.DaGetquotationhistorysummarydata(values, quotation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getquotationproductdetails")]
        [HttpGet]
        public HttpResponseMessage Getquotationproductdetails(string quotation_gid)
        {
            MdlSmrTrnQuotation objresult = new MdlSmrTrnQuotation();
            objDaSales.DaGetquotationproductdetails(quotation_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //customer 360

        [ActionName("GetCustomerDtlCRM")]
        [HttpGet]
        public HttpResponseMessage GetCustomerDtlCRM(string leadbank_gid)
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetCustomerDtlCRM(leadbank_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete product summary//
        [ActionName("GetdeleteamendProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetdeleteamendProductSummary(string tmpquotationdtl_gid)
        {
            summaryprod_list objresult = new summaryprod_list();
            objDaSales.GetdeleteamendProductSummary(tmpquotationdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        // Overall submit For Direct Quotation

        [ActionName("postQuotationAmend")]
        [HttpPost]
        public HttpResponseMessage postQuotationAmend(Post_List values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DapostsubmitQuotation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //mail func

        [ActionName("GetTemplatelist")]
        [HttpGet]
        public HttpResponseMessage GetTemplatelist()
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetTemplatelist(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTemplate")]
        [HttpGet]
        public HttpResponseMessage GetTemplate(string template_gid)
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetTemplatet(template_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostMail")]
        [HttpPost]
        public HttpResponseMessage PostMail()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaSales.DaPostMail(httpRequest, getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }


        [ActionName("GetOnChangeProductsNameCRM")]
        [HttpGet]
        public HttpResponseMessage GetOnChangeProductsNameCRM(string product_gid)
        {
            MdlSmrTrnQuotation values = new MdlSmrTrnQuotation();
            objDaSales.DaGetOnChangeProductsNameCRM(product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // DIRECT QUOTATION PRODUCT EDIT

        [ActionName("GetDirectQuotationEditProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetDirectQuotationEditProductSummary(string tmpquotationdtl_gid)
        {
            MdlSmrTrnQuotation objresult = new MdlSmrTrnQuotation();
            objDaSales.DaGetDirectQuotationEditProductSummary(tmpquotationdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        public HttpResponseMessage GetRaiseQuotedetail(string product_gid)
        {
            MdlSmrTrnQuotation objresult = new MdlSmrTrnQuotation();
            objDaSales.DaGetRaiseQuotedetail(product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // UPDATE PRODUCT -- DIRECT QUOTATION PRODUCT SUMMARY

        [ActionName("PostUpdateDirectQuotationProduct")]
        [HttpPost]
        public HttpResponseMessage PostUpdateDirectQuotationProduct(DirecteditQuotationList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaPostUpdateDirectQuotationProduct(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // DELETE EVENT FOR QUOTATION TO ORDER PRODUCT SUMMARY
        [ActionName("GetDeleteQuotetoOrderProductSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeleteQuotetoOrderProductSummary(string tmpsalesorderdtl_gid)
        {
            GetSummaryList objresult = new GetSummaryList();
            objDaSales.DaGetDeleteQuotetoOrderProductSummary(tmpsalesorderdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        //  QUOTATION TO ORDER PRODUCT EDIT

        [ActionName("GetQuotetoOrderProductEditSummary")]
        [HttpGet]
        public HttpResponseMessage GetQuotetoOrderProductEditSummary(string tmpsalesorderdtl_gid)
        {
            MdlSmrTrnQuotation objresult = new MdlSmrTrnQuotation();
            objDaSales.DaGetQuotetoOrderProductEditSummary(tmpsalesorderdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // UPDATE PRODUCT --  QUOTATION TO ORDER PRODUCT SUMMARY

        [ActionName("PostUpdateQuotationtoOrderProductSummary")]
        [HttpPost]
        public HttpResponseMessage PostUpdateQuotationtoOrderProductSummary(DirecteditQuotationList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSales.DaPostUpdateQuotationtoOrderProductSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}

