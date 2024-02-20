using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.asset.Models;
using ems.utilities.Functions;
namespace ems.asset.DataAccess
{
    public class DaLandingPage
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDataReader;
        string msSQL;
        // Get Landing Page Data from Database
        public bool DaGetLandingPage(landingpagemodel objlandingpage,string employee_gid, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.employee_photo, concat(b.user_firstName,' ',b.user_lastname) as user_name FROM hrm_mst_temployee a " +
                     " LEFT JOIN adm_mst_tuser b ON a.user_gid=b.user_gid " +
                     " WHERE a.employee_gid='" + employee_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    objlandingpage.user_name = objODBCDataReader["user_name"].ToString();
                    objlandingpage.employee_phote = objODBCDataReader["employee_photo"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " SELECT  a.count_surrender,b.count_temphandover,c.count_acknowledgement,d.count_myasset,e.count_response,m.count_tmpsurrender,n.count_tmpholding, " +
                       " sum(f.count_department + g.count_service + h.count_management + i.count_dependency + j.count_cabapproval) as count_myapproval from " +
                       " (SELECT COUNT(status) AS count_acknowledgement FROM ams_trn_tasset2custodian WHERE status = 'Acknowledgement Pending' " +
                       " AND custodiantracker_gid = (SELECT custodiantracker_gid from ams_trn_tcustodiantracker where employee_gid ='" + employee_gid + "')) AS c, " +
                       " (SELECT COUNT(status) AS count_surrender FROM ams_trn_tasset2custodian WHERE status = 'Surrender Pending' " +
                       " AND custodiantracker_gid = (SELECT custodiantracker_gid from ams_trn_tcustodiantracker where employee_gid ='" + employee_gid + "')) AS a, " +
                       " (SELECT COUNT(status) AS count_tmpholding FROM ams_trn_tasset2custodian WHERE status = 'Temporary Handover' " +
                       " AND temporaryhandover_gid ='" + employee_gid + "') AS n, " +
                       " (SELECT COUNT(status) AS count_tmpsurrender FROM ams_trn_tasset2custodian WHERE status = 'Temporary Handover Surrender Pending' " +
                       " AND temporaryhandover_gid ='" + employee_gid + "') AS m, " +
                       " (SELECT COUNT(status) AS count_temphandover FROM ams_trn_tasset2custodian WHERE status = 'Temporary Handover Pending' " +
                       " AND custodiantracker_gid = (SELECT custodiantracker_gid from ams_trn_tcustodiantracker where employee_gid ='" + employee_gid + "')) AS b, " +
                       " (SELECT COUNT(response_new) AS count_response FROM its_trn_tresponselog WHERE response_new = 'Y' " +
                       " and assigned_to ='" + employee_gid + "') AS e, " +
                       " (SELECT COUNT(status) AS count_myasset FROM ams_trn_tasset2custodian WHERE status = 'Acknowledged' " +
                       " AND custodiantracker_gid = (SELECT custodiantracker_gid from ams_trn_tcustodiantracker where employee_gid ='" + employee_gid + "')) AS d, " +
                       " (SELECT count(status) as count_department FROM its_trn_tserviceapproval WHERE status = 'Department Approval Pending' and ticket_approval = 'Y' " +
                       " and dept_manager = '" + employee_gid + "') as f, " +
                       " (SELECT count(status) as count_service FROM its_trn_tserviceapproval WHERE status = 'Service Department Approval Pending' " +
                       " and service_manager ='" + employee_gid + "') as g, " +
                       " (SELECT count(status) as count_management FROM its_trn_tserviceapproval WHERE status = 'Management Approval Pending'" +
                       " and manager_gid ='" + employee_gid + "') as h," +
                       " (select COUNT(dependency_status) As count_dependency from its_trn_tdependencyapproval where dependency_status = 'Approval Pending...'" +
                       " and approval_gid = '" + employee_gid + "') As i," +
                       " (select count(approval_status) as count_cabapproval from its_trn_tcacapproval where approval_status = 'Approval Pending...'" +
                       " and approval_member = '" + employee_gid + "') As j," +
                       " (select count(activity) as taskapproval_count from ocs_trn_ttask2activity where activity = 'Department Head Approval' " +
                       " and assigned_gid = '" + user_gid + "' and status=('Pending')) As k " +
                       " group by a.count_surrender";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    objlandingpage.count_acknowledgement = Convert.ToInt32(objODBCDataReader["count_acknowledgement"]);
                    objlandingpage.count_myasset = Convert.ToInt32(objODBCDataReader["count_myasset"]);
                    objlandingpage.count_surrender = Convert.ToInt32(objODBCDataReader["count_surrender"]);
                    objlandingpage.count_tmpholding = Convert.ToInt32(objODBCDataReader["count_tmpholding"]);
                    objlandingpage.count_tmpsurrender = Convert.ToInt32(objODBCDataReader["count_tmpsurrender"]);
                    objlandingpage.count_temporaryhandover = Convert.ToInt32(objODBCDataReader["count_temphandover"]);
                    objlandingpage.count_response = Convert.ToInt32(objODBCDataReader["count_response"]);
                    objlandingpage.count_myapprovals = Convert.ToInt32(objODBCDataReader["count_myapproval"]);
                }
                objODBCDataReader.Close();
                objlandingpage.status = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

     

    }
}