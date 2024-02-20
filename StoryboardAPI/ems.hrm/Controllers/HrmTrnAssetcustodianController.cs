using ems.hrm.DataAccess;
using ems.hrm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
//using StoryboardAPI.Models;

namespace ems.hrm.Controllers
{
    [RoutePrefix("api/HrmTrnAssetcustodian")]
    [Authorize]
    public class HrmTrnAssetcustodianController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaHrmTrnAssetcustodian objdalist = new DaHrmTrnAssetcustodian();

        [ActionName("GetBranch")]
        [HttpGet]

        public HttpResponseMessage GetBranch()
        {
            MdlHrmTrnAssetcustodian values = new MdlHrmTrnAssetcustodian();
            objdalist.DaGetBranch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDepartment")]
        [HttpGet]
        public HttpResponseMessage GetDepartment(string branch_gid)
        {
            MdlHrmTrnAssetcustodian values = new MdlHrmTrnAssetcustodian();
            objdalist.GetDepartment(branch_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getassetcustodiansummary")]
        [HttpGet]
        public HttpResponseMessage Getassetcustodiansummary(string branch_name, string department_name )
        {
            MdlHrmTrnAssetcustodian values = new MdlHrmTrnAssetcustodian();
            objdalist.DaGetassetcustodiansummary(branch_name, department_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAddassetcustodian")]
        [HttpGet]
        public HttpResponseMessage GetAddassetcustodian(string employee_gid)
        {
            MdlHrmTrnAssetcustodian values = new MdlHrmTrnAssetcustodian();
            objdalist.DaGetAddassetcustodian(employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetassetcustodianExpand")]
        [HttpGet]
        public HttpResponseMessage GetassetcustodianExpand(string employee_gid)
        {
            MdlHrmTrnAssetcustodian values = new MdlHrmTrnAssetcustodian();
            objdalist.DaGetassetcustodianExpand(employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getusetdtl")]
        [HttpGet]
        public HttpResponseMessage Getusetdtl(string employee_gid)
        {
            MdlHrmTrnAssetcustodian values = new MdlHrmTrnAssetcustodian();
            objdalist.DaGetusetdtl(employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AssetDocument")]
        [HttpGet]
        public HttpResponseMessage AssetDocument(string asset_gid, string employee_gid)
        {
            MdlHrmTrnAssetcustodian values = new MdlHrmTrnAssetcustodian();
            objdalist.DaAssetDocument(asset_gid, employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("Postcusdotiandtl")]
        [HttpPost]
        public HttpResponseMessage Postcusdotiandtl(Assetcustodian values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdalist.DaPostcusdotiandtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("UpdateAssetdocument")]
        [HttpPost]
        public HttpResponseMessage UpdateAssetdocument()
        {
            HttpRequest httpRequest;
            //Postassetlocationcreation values = new Postassetlocationcreation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objdalist.DaUpdateAssetdocument(httpRequest, objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("downloadFile")]
        [HttpGet]
        public HttpResponseMessage downloadFile(string document_gid)
        {
            MdlHrmTrnAssetcustodian values = new MdlHrmTrnAssetcustodian();
            objdalist.DadownloadFile(document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }






        [ActionName("GetCompanyPolicies")]
        [HttpGet]

        public HttpResponseMessage GetCompanyPolicies()
        {
            MdlHrmTrnAssetcustodian values = new MdlHrmTrnAssetcustodian();
            objdalist.DaGetCompanyPolicies(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }





    }
}