using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using ems.utilities.Models;

namespace ems.utilities.Functions
{
    public class session_values
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCdatareader;
        string msSQL = string.Empty;

        public logintoken gettokenvalues(string token)
        {
            logintoken getlogintoken = new logintoken();
           
            msSQL = " select employee_gid,user_gid,department_gid from adm_mst_ttoken WHERE token = '" + token + "'";
            objODBCdatareader = objdbconn .GetDataReader(msSQL);
            if (objODBCdatareader.HasRows == true)
            {
                objODBCdatareader.Read();
                getlogintoken.employee_gid = objODBCdatareader["employee_gid"].ToString();
                getlogintoken.user_gid = objODBCdatareader["user_gid"].ToString();
                getlogintoken.department_gid = objODBCdatareader["department_gid"].ToString();
                objODBCdatareader.Close();
            }
            else
                objODBCdatareader.Close();
            return getlogintoken;
        }
    }
}