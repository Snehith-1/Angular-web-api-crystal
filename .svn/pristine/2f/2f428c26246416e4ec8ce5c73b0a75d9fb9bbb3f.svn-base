using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.hrm.DataAccess;
using ems.hrm.Models;

namespace ems.hrm.DataAccess
{

    [RoutePrefix("api/HrmMaster")]
    [Authorize]
    public class HrmMasterController : ApiController
    {
        DaHrmMaster objDaSystemMaster = new DaHrmMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("CreateSubFunction")]
        [HttpPost]
        public HttpResponseMessage CreateSubFunction(master1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateSubFunction(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetSubFunction")]
        [HttpGet]
        public HttpResponseMessage GetSubFunction()
        {
            MdlHrmMaster objmaster = new MdlHrmMaster();
            objDaSystemMaster.DaGetSubFunction(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("EditSubFunction")]
        [HttpGet]
        public HttpResponseMessage EditSubFunction(string subfunction_gid)
        {
            master1 values = new master1();
            objDaSystemMaster.DaEditSubFunction(subfunction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteSubFunction")]
        [HttpGet]
        public HttpResponseMessage DeleteSubFunction(string subfunction_gid)
        {
            master1 values = new master1();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteSubFunction(subfunction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("SubFunctionInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage SubFunctionInactiveLogview(string subfunction_gid)
        {
            MdlHrmMaster values = new MdlHrmMaster();
            objDaSystemMaster.DaSubFunctionInactiveLogview(subfunction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("InactiveSubFunction")]
        [HttpPost]
        public HttpResponseMessage InactiveSubFunction(master1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveSubFunction(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateSubFunction")]
        [HttpPost]
        public HttpResponseMessage UpdateSubFunction(master1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateSubFunction(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Base Location
        [ActionName("GetBaseLocation")]
        [HttpGet]
        public HttpResponseMessage GetBaseLocation()
        {
            MdlHrmMaster objmaster = new MdlHrmMaster();
            objDaSystemMaster.DaGetBaseLocation(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("GetBaseLocationlist")]
        [HttpGet]
        public HttpResponseMessage GetBaseLocationlist()
        {
            MdlHrmMaster objmaster = new MdlHrmMaster();
            objDaSystemMaster.DaGetBaseLocationlist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("GetBaseLocationlistActive")]
        [HttpGet]
        public HttpResponseMessage GetBaseLocationlistActive()
        {
            MdlHrmMaster objmaster = new MdlHrmMaster();
            objDaSystemMaster.DaGetBaseLocationlistActive(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateBaseLocation")]
        [HttpPost]
        public HttpResponseMessage CreateBaseLocation(master1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateBaseLocation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditBaseLocation")]
        [HttpGet]
        public HttpResponseMessage EditBaseLocation(string baselocation_gid)
        {
            master1 values = new master1();
            objDaSystemMaster.DaEditBaseLocation(baselocation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBaseLocation")]
        [HttpPost]
        public HttpResponseMessage UpdateBaseLocation(master1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateBaseLocation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBaseLocation")]
        [HttpPost]
        public HttpResponseMessage InactiveBaseLocation(master1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveBaseLocation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBaseLocation")]
        [HttpGet]
        public HttpResponseMessage DeleteBaseLocation(string baselocation_gid)
        {
            master1 values = new master1();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteBaseLocation(baselocation_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("BaseLocationInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BaseLocationInactiveLogview(string baselocation_gid)
        {
            MdlHrmMaster values = new MdlHrmMaster();
            objDaSystemMaster.DaBaseLocationInactiveLogview(baselocation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Blood Group
        [ActionName("GetBloodGroup")]
        [HttpGet]
        public HttpResponseMessage GetBloodGroup()
        {
            MdlHrmMaster objmaster = new MdlHrmMaster();
            objDaSystemMaster.DaGetBloodGroup(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }




        [ActionName("GetBloodGroupActive")]
        [HttpGet]
        public HttpResponseMessage GetBloodGroupActive()
        {
            MdlHrmMaster objmaster = new MdlHrmMaster();
            objDaSystemMaster.DaGetBloodGroupActive(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        [ActionName("CreateBloodGroup")]
        [HttpPost]
        public HttpResponseMessage CreateBloodGroup(master1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateBloodGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditBloodGroup")]
        [HttpGet]
        public HttpResponseMessage EditBloodGroup(string bloodgroup_gid)
        {
            master1 values = new master1();
            objDaSystemMaster.DaEditBloodGroup(bloodgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

       


        [ActionName("UpdateBloodGroup")]
        [HttpPost]
        public HttpResponseMessage UpdateBloodGroup(master1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateBloodGroup(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBloodGroup")]
        [HttpPost]
        public HttpResponseMessage InactiveBloodGroup(master1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveBloodGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBloodGroup")]
        [HttpGet]
        public HttpResponseMessage DeleteBloodGroup(string bloodgroup_gid)
        {
            master1 values = new master1();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteBloodGroup(bloodgroup_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BloodGroupInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BloodGroupInactiveLogview(string bloodgroup_gid)
        {
            MdlHrmMaster values = new MdlHrmMaster();
            objDaSystemMaster.DaBloodGroupInactiveLogview(bloodgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Task

        [ActionName("PostTaskAdd")]
        [HttpPost]
        public HttpResponseMessage PostTaskAdd(MdlTask values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostTaskAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetTaskSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaskSummary()
        {
            MdlHrmMaster objmaster = new MdlHrmMaster();
            objDaSystemMaster.DaGetTaskSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
      


        [ActionName("EditTask")]
        [HttpGet]
        public HttpResponseMessage EditTask(string task_gid)
        {
            MdlTask objmaster = new MdlTask();
            objDaSystemMaster.DaEditTask(task_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("UpdateTask")]
        [HttpPost]
        public HttpResponseMessage UpdateTask(MdlTask values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateTask(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTask")]
        [HttpPost]
        public HttpResponseMessage InactiveTask(master1 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveTask(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TaskInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage TaskInactiveLogview(string task_gid)
        {
            MdlHrmMaster values = new MdlHrmMaster();
            objDaSystemMaster.DaTaskInactiveLogview(task_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteTask")]
        [HttpGet]
        public HttpResponseMessage DeleteTask(string task_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteTask(task_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskMultiselectList")]
        [HttpGet]
        public HttpResponseMessage GetTaskMultiselectList(string task_gid)
        {
            MdlTask objmaster = new MdlTask();
            objDaSystemMaster.DaGetTaskMultiselectList(task_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        //Add

        [ActionName("PostTeammaster")]
        [HttpPost]
        public HttpResponseMessage PostTeammaster(Mdlteam values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostTeammaster(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TeamEmployee")]
        [HttpGet]
        public HttpResponseMessage getteamEmployee()
        {
            MdlEmployee objMdlEmployee = new MdlEmployee();
            objDaSystemMaster.DaGetmemberEmployee(objMdlEmployee);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }


        //summary
        [ActionName("GetTeammaster")]
        [HttpGet]
        public HttpResponseMessage GetTeammaster()
        {
            Mdlteam objmaster = new Mdlteam();
            objDaSystemMaster.DaGetTeammaster(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }



        [ActionName("Getteammastermembers")]
        [HttpGet]
        public HttpResponseMessage Getteammastermembers(string team_gid)
        {
            teammemberslist values = new teammemberslist();
            objDaSystemMaster.DaGetteammastermembers(team_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Edit

        [ActionName("GetTeammembersEdit")]
        [HttpGet]
        public HttpResponseMessage GetTeammembersEdit(string team_gid)
        {
            Mdlteam objmaster = new Mdlteam();
            objDaSystemMaster.DaGetTeammembersEdit(team_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("UpdateTeamDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateTeamDtl(Mdlteam values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateTeamDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //updatae
        [ActionName("InactiveTeamMaster")]
        [HttpPost]
        public HttpResponseMessage InactiveTeamMaster(Mdlteam values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveTeamMaster(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TeamMasterInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage TeamMasterInactiveLogview(string team_gid)
        {
            Mdlteam values = new Mdlteam();
            objDaSystemMaster.TeamMasterInactiveLogview(team_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Delete

        [ActionName("DeleteTeammaster")]
        [HttpGet]
        public HttpResponseMessage DeleteTeammaster(string team_gid)
        {
            Mdlteam values = new Mdlteam();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteTeammaster(team_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Employee")]
        [HttpGet]
        public HttpResponseMessage getEmployee()
        {
            MdlEmployee objMdlEmployee = new MdlEmployee();
            objDaSystemMaster.DaGetEmployee(objMdlEmployee);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }




    }
}