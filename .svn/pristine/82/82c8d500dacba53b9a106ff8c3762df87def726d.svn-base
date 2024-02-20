using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.finance.Models;

namespace ems.finance.DataAccess
{
    public class DaAccMstOpeningbalance
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        string msSQL1 = string.Empty;
        OdbcDataReader 
            
            
            objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid, maGetGID, lsvendor_code, msUserGid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaGetOpeningbalance(MdlAccMstOpeningbalance values)
        {
            msSQL = " SELECT a.openningbalance_gid,a.account_gid,cast(a.credit_amount as decimal) as credit_amount, " +
                " b.account_name,b.account_gid,b.accountgroup_name,c.branch_name FROM acc_trn_topenningbalance a " +
                " LEFT JOIN acc_mst_tchartofaccount b ON a.account_gid=b.account_gid " +
                " Left JOIN hrm_mst_tbranch c on a.branch_gid=c.branch_gid " +
              " where a.openningbalance_gid NOT LIKE ' % NHA % ' AND a.credit_amount <> 0.00 " +
                " ORDER BY a.openingbalance_date DESC,a.openningbalance_gid DESC " ;

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Openingbalance_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Openingbalance_list
                    {
                        openningbalance_gid = dt["openningbalance_gid"].ToString(),
                        accountgroup_name = dt["accountgroup_name"].ToString(),
                        account_name = dt["account_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        credit_amount = dt["credit_amount"].ToString(),

                    });
                    values.Openingbalance_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetAccMstOpeningbalance(MdlAccMstOpeningbalance values)
        {
            msSQL = " SELECT a.openningbalance_gid,a.account_gid,cast(a.debit_amount as decimal) as debit_amount, " +
              " b.account_name,b.account_gid,b.accountgroup_name,c.branch_name FROM acc_trn_topenningbalance a " +
              " LEFT JOIN acc_mst_tchartofaccount b ON a.account_gid=b.account_gid " +
              " Left JOIN hrm_mst_tbranch c on a.branch_gid=c.branch_gid " +
               " where a.openningbalance_gid NOT LIKE ' % NHA % '  AND a.debit_amount <> 0.00 " +
               " ORDER BY a.openingbalance_date DESC,a.openningbalance_gid DESC ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Openingbalance_lists>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Openingbalance_lists
                    {
                        openningbalance_gid = dt["openningbalance_gid"].ToString(),
                        accountgroup_name = dt["accountgroup_name"].ToString(),
                        account_name = dt["account_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        debit_amount = dt["debit_amount"].ToString(),

                    });
                    values.Openingbalance_lists = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
    }

}
