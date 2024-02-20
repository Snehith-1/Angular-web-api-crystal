using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


namespace ems.hrm.DataAccess
{
    public class hrClass
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objMySqlDataReader, objMySqlDataReader1, objMySqlDataReader2;
        DataTable objTbl, objTblemployee, objDtable;
        string msSQL;
        int mnResult;
        DateTime lsdate, lsenddate;
        string leave_month, leave_name;
        double leave_taken, ls_limit, leave_year;
        double month, lslength;
        double lsleave_available, lsavailable_leave, lstotal_leave;
        string lscarry, lsaccrual, leavecarry_count, lsleavegrade_name;
        string lsleavegrade_gid, lsleavetype_gid;

        public bool openingbalance(string employee_gid)
        {
            msSQL = "select* from hrm_mst_tleavecreditsdtl where employee_gid = '" + employee_gid + "'";
            objTbl = objdbconn.GetDataTable(msSQL);
            if (objTbl.Rows.Count == 0)
            {
                objTbl.Dispose();
                return false;
            }
            msSQL = " select attendance_startdate,attendance_enddate " +
                    " from adm_mst_tcompany  ";
            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
            if (objMySqlDataReader.HasRows == true)
            {
                lsdate = Convert.ToDateTime(objMySqlDataReader["attendance_startdate"]);
                lsenddate = Convert.ToDateTime(objMySqlDataReader["attendance_enddate"]);
            }
            objMySqlDataReader.Close();
            leave_year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            string monthlength = Convert.ToString(month);
            lslength = monthlength.Length;
            if (Convert.ToString(lslength) == "2")
            {
                leave_month = Convert.ToString(month);
            }
            else
            {
                leave_month = "0" + month;
            }
            msSQL = " select a.employee_gid,a.leavegrade_gid,a.leavegrade_code,a.leavegrade_name,b.leavetype_gid,c.leavetype_name," +
                     " b.total_leavecount, b.available_leavecount, b.leave_limit,c.carryforward,c.accrud " +
                     " from hrm_trn_tleavegrade2employee a " +
                     " left join hrm_mst_tleavegradedtl b on a.leavegrade_gid = b.leavegrade_gid " +
                     " left join hrm_mst_tleavetype c on c.leavetype_gid = b.leavetype_gid " +
                     " where a.employee_gid='" + employee_gid + "'";
            objTblemployee = objdbconn.GetDataTable(msSQL);
            if (objTblemployee.Rows.Count > 0)
            {
                foreach (DataRow objtblemploteedatarow in objTblemployee.Rows)
                {
                    lsleave_available = 0.0;
                    lscarry = objtblemploteedatarow["carryforward"].ToString();
                    lsaccrual = objtblemploteedatarow["accrud"].ToString();
                    ls_limit = 0.0;
                    leavecarry_count = "0.0";
                    lsleavegrade_name = objtblemploteedatarow["leavegrade_name"].ToString();
                    lsleavegrade_gid = objtblemploteedatarow["leavegrade_gid"].ToString();
                    lsleavetype_gid = objtblemploteedatarow["leavetype_gid"].ToString();
                    leave_name = objtblemploteedatarow["leavetype_name"].ToString();
                    lsavailable_leave = Convert.ToDouble(objtblemploteedatarow["available_leavecount"]);
                    ls_limit = Convert.ToDouble(objtblemploteedatarow["leave_limit"]);
                    lstotal_leave = Convert.ToDouble(objtblemploteedatarow["total_leavecount"]);

                    if (lsaccrual == "N")
                    {
                        msSQL = " SELECT sum(b.leave_count) as totalleave FROM hrm_trn_tleave a " +
                            " left join hrm_trn_tleavedtl b on a.leave_gid = b.leave_gid " +
                            " where a.leavetype_gid = '" + lsleavetype_gid + "' " +
                            " and a.employee_gid = '" + employee_gid + "' " +
                            " and b.leavedtl_date>= '" + lsdate.ToString("yyyy-MM-dd") + "' and b.leavedtl_date<='" + lsenddate.ToString("yyyy-MM-dd") + "'" +
                            " and a.leave_status = 'Approved' " +
                            " group by a.leavetype_gid ";
                        objDtable = objdbconn.GetDataTable(msSQL);
                        if (objDtable.Rows.Count > 0)
                        {
                            foreach (DataRow objTblrow in objDtable.Rows)
                            {
                                if ((objTblrow.IsNull("totalleave")) == true)
                                {
                                    leave_taken = 0.0;
                                }
                                else
                                {
                                    leave_taken = Convert.ToDouble(objTblrow["totalleave"]);
                                    lsleave_available = lstotal_leave - leave_taken;
                                }
                            }
                            objDtable.Dispose();
                        }
                        else
                        {
                            leave_taken = 0.0;
                            lsleave_available = lstotal_leave - leave_taken;
                        }
                    }
                    else if (lsaccrual == "Y")
                    {
                        msSQL = " SELECT sum(b.leave_count) as totalleave " +
                           " FROM hrm_trn_tleave a " +
                           " left join hrm_trn_tleavedtl b on a.leave_gid = b.leave_gid " +
                           " where a.leavetype_gid = '" + lsleavetype_gid + "' " +
                           " and a.employee_gid = '" + employee_gid + "' " +
                           " and a.leave_status = 'Approved' " +
                           " and date_format(b.leavedtl_date,'%m') <= '" + leave_month + "' and " +
                           " date_format(b.leavedtl_date,'%Y') = '" + leave_year + "'" +
                           " and b.leavedtl_date>= '" + lsdate.ToString("yyyy-MM-dd") + "' and b.leavedtl_date<='" + lsenddate.ToString("yyyy-MM-dd") + "'" +
                           " group by a.leavetype_gid";
                        objDtable = objdbconn.GetDataTable(msSQL);
                        if (objDtable.Rows.Count > 0)
                        {
                            foreach (DataRow objTblrow in objDtable.Rows)
                            {
                                if ((objTblrow.IsNull("totalleave")) == true)
                                {
                                    leave_taken = 0.0;
                                }
                                else
                                {
                                    leave_taken = Convert.ToDouble(objTblrow["totalleave"]);
                                }
                            }
                            objDtable.Dispose();
                        }
                        else
                        {
                            leave_taken = 0.0;
                        }
                    }
                    else if (lsaccrual == "E")
                    {
                        msSQL = " SELECT sum(b.leave_count) as totalleave " +
                           " FROM hrm_trn_tleave a " +
                           " left join hrm_trn_tleavedtl b on a.leave_gid = b.leave_gid " +
                           " where a.leavetype_gid = '" + lsleavetype_gid + "' " +
                           " and a.employee_gid = '" + employee_gid + "' " +
                           " and a.leave_status = 'Approved' " +
                           " and date_format(b.leavedtl_date,'%m') = '" + leave_month + "' and " +
                           " date_format(b.leavedtl_date,'%Y') = '" + leave_year + "'" +
                           " and b.leavedtl_date>= '" + lsdate.ToString("yyyy-MM-dd") + "' and b.leavedtl_date<='" + lsenddate.ToString("yyyy-MM-dd") + "'" +
                           " group by a.leavetype_gid";
                        objDtable = objdbconn.GetDataTable(msSQL);
                        if (objDtable.Rows.Count > 0)
                        {
                            foreach (DataRow objTblrow in objDtable.Rows)
                            {
                                if ((objTblrow.IsNull("totalleave")) == true)
                                {
                                    leave_taken = 0.0;
                                }
                                else
                                {
                                    leave_taken = Convert.ToDouble(objTblrow["totalleave"]);
                                }
                            }
                            objDtable.Dispose();
                        }
                        else
                        {
                            leave_taken = 0.0;
                        }
                    }

                    if (lsaccrual == "Y")
                    {
                        if (lscarry == "N")
                        {
                            msSQL = " select sum(leavecarry_count) as  leavecarry_count from hrm_mst_tleavecreditsdtl where employee_gid='" + employee_gid + "' " +
                                " and leavetype_gid='" + lsleavetype_gid + "' " +
                            " and month >= '" + lsdate.ToString("MM") + "' and month<='" + lsenddate.ToString("MM") + "'" +
                             " and year >= '" + lsdate.ToString("yyyy") + "' and year<='" + lsenddate.ToString("yyyy") + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {

                                lscarry = objMySqlDataReader["leavecarry_count"].ToString();
                                if (lscarry != null || lscarry != "")
                                {
                                    lsavailable_leave = Convert.ToDouble(lscarry) - leave_taken;
                                    objMySqlDataReader.Close();
                                    msSQL = "update hrm_mst_tleavecreditsdtl set " +
                                                   " total_leavecount='" + lscarry + "', " +
                                                   " leave_taken='" + leave_taken + "', " +
                                                   " available_leavecount='" + lsavailable_leave + "' " +
                                                   " where employee_gid='" + employee_gid + "' and leavetype_gid='" + lsleavetype_gid + "' and month ='" + leave_month + "' and year='" + leave_year + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                }
                            }

                        }
                        else
                        {
                            msSQL = " select sum(leavecarry_count) as  leavecarry_count from hrm_mst_tleavecreditsdtl where employee_gid='" + employee_gid + "' " +
                                 " and leavetype_gid='" + lsleavetype_gid + "' " +
                            " and month >= '" + lsdate.ToString("MM") + "' and month<='" + lsenddate.ToString("MM") + "'" +
                             " and year >= '" + lsdate.ToString("yyyy") + "' and year<='" + lsenddate.ToString("yyyy") + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {

                                lscarry = objMySqlDataReader["leavecarry_count"].ToString();
                                if (lscarry != null || lscarry != "")
                                {
                                    lsavailable_leave = Convert.ToDouble(lscarry) - leave_taken;
                                    objMySqlDataReader.Close();

                                    msSQL = "update hrm_mst_tleavecreditsdtl set " +
                                               " total_leavecount='" + lscarry + "', " +
                                               " leave_taken='" + leave_taken + "', " +
                                               " available_leavecount='" + lsavailable_leave + "' " +
                                               " where employee_gid='" + employee_gid + "' and leavetype_gid='" + lsleavetype_gid + "' and month ='" + leave_month + "' and year='" + leave_year + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                }
                            }
                        }
                    }
                    else if (lsaccrual == "N")
                    {
                        if (lscarry == "N")
                        {
                            msSQL = " select total_leavecount from hrm_mst_tleavecreditsdtl where employee_gid='" + employee_gid + "' " +
                         " and leavetype_gid='" + lsleavetype_gid + "' and year='" + leave_year + "' and month = '" + leave_month + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {

                                lscarry = objMySqlDataReader["total_leavecount"].ToString();
                                if (lscarry != null || lscarry != "")
                                {
                                    lsavailable_leave = Convert.ToDouble(lscarry) - leave_taken;
                                    objMySqlDataReader.Close();

                                    msSQL = "update hrm_mst_tleavecreditsdtl set " +
                                         " leave_taken='" + leave_taken + "', " +
                                         " available_leavecount='" + lsavailable_leave + "' " +
                                         " where employee_gid='" + employee_gid + "' and leavetype_gid='" + lsleavetype_gid + "' and month ='" + leave_month + "' and year='" + leave_year + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                objMySqlDataReader.Close();
                                msSQL = " select total_leavecount from hrm_mst_tleavecreditsdtl where employee_gid='" + employee_gid + "' " +
                                        " and leavetype_gid='" + lsleavetype_gid + "' and year='" + leave_year + "' and month = '" + leave_month + "'";
                                objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                                if (objMySqlDataReader.HasRows == true)
                                {

                                    lscarry = objMySqlDataReader["total_leavecount"].ToString();
                                    if (lscarry != null || lscarry != "")
                                    {
                                        lsavailable_leave = Convert.ToDouble(lscarry) - leave_taken;
                                        objMySqlDataReader.Close();
                                        msSQL = "update hrm_mst_tleavecreditsdtl set " +
                                                " leave_taken='" + leave_taken + "', " +
                                                " available_leavecount='" + lsavailable_leave + "' " +
                                                " where employee_gid='" + employee_gid + "' and leavetype_gid='" + lsleavetype_gid + "' and month ='" + leave_month + "' and year='" + leave_year + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    }
                                }

                            }
                        }
                        else if (lsaccrual == "E")
                        {
                            msSQL = " select total_leavecount from hrm_mst_tleavecreditsdtl where employee_gid='" + employee_gid + "' " +
                                    " and leavetype_gid='" + lsleavetype_gid + "' and year='" + leave_year + "' and month = '" + leave_month + "'";
                            objMySqlDataReader = objdbconn.GetDataReader(msSQL);
                            if (objMySqlDataReader.HasRows == true)
                            {

                                lscarry = objMySqlDataReader["total_leavecount"].ToString();
                                if (lscarry != null || lscarry != "")
                                {
                                    lsavailable_leave = Convert.ToDouble(lscarry) - leave_taken;
                                    objMySqlDataReader.Close();
                                    msSQL = "update hrm_mst_tleavecreditsdtl set " +
                                            " leave_taken='" + leave_taken + "', " +
                                            " available_leavecount='" + lsavailable_leave + "' " +
                                            " where employee_gid='" + employee_gid + "' and leavetype_gid='" + lsleavetype_gid + "' and month ='" + leave_month + "' and year='" + leave_year + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                }
                            }
                        }
                    }
                }

            }

            return true;
        }

    }
}