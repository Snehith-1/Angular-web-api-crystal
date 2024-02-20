Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared

Public Class pay_rpt_payempolyeeslip
    Inherits System.Web.UI.Page
    Dim companycode As String
    Public gs_ConnDB As OdbcConnection
    Dim msSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim authDBInstance As New authdbconn()
        authDBInstance.openconn_load()
        Dim myConnection As OdbcConnection = authdbconn.gs_ConnDB

        If myConnection.State <> ConnectionState.Open Then
            myConnection.Open()
        End If
        companycode = Request.QueryString("companycode").ToString
        msSQL = " select (b.display_name) as salarycomponent_name,a.salarycomponent_amount,a.earned_salarycomponent_amount from pay_trn_tsalarydtl a  " &
                       " inner join pay_mst_tsalarycomponent b on a.salarycomponent_gid=b.salarycomponent_gid where " &
                        " a.salarygradetype='Addition' and b.primecomponent_flag='N' and a.salary_gid='" & Request.QueryString("salarygid").ToString() & "'"
        Dim MyCommand As New OdbcCommand
        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        Dim MyDA As New OdbcDataAdapter
        MyDA.SelectCommand = MyCommand
        Dim myDS As New DataSet
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time
        MyDA.Fill(myDS, "DataTable1")

        msSQL = "select company_name,company_mail,company_address as company_website from adm_mst_tcompany"

        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable2")

        msSQL = " select (b.display_name) as salarycomponent_name,a.salarycomponent_amount,a.earned_salarycomponent_amount from pay_trn_tsalarydtl a  " &
                     " inner join pay_mst_tsalarycomponent b on a.salarycomponent_gid=b.salarycomponent_gid where " &
                      " a.salarygradetype='Deduction' and b.primecomponent_flag='N' and a.salary_gid='" & Request.QueryString("salarygid").ToString() & "'"

        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable3")


        msSQL = " select  a.salary_gid, a.month, a.year, b.passport_no,a.employee_gid,d.user_code as employee_code, concat(d.user_firstname,' ',d.user_lastname)as employee_name, " &
                        " a.basic_salary, a.earned_basic_salary, a.gross_salary, a.earned_gross_salary, a.net_salary,b.uan_no,b.pan_no,concat(f.department_name,'-',e.branch_code) as department_withbranch, " &
                        " a.earned_net_salary, a.month_workingdays, a.actual_month_workingdays,b.pf_no,b.esi_no," &
                        " b.bank as bank,b.ac_no as ac_no,e.branch_name, " &
                        " a.leave_taken, a.lop_days as lop,a.month_workingdays, a.public_holidays,f.department_name as department,g.designation_name,date_format(b.employee_joiningdate,'%d-%m-%Y') as employee_joiningdate, " &
                        " (select sum(earned_salarycomponent_amount) from pay_trn_tsalarydtl x " &
                        " left join pay_trn_tsalary y on y.salary_gid=x.salary_gid where salarygradetype='Deduction'  and y.month='" & Request.QueryString("month").ToString() & "'" &
                        " and y.year='" & Request.QueryString("year").ToString() & "' and " &
                        " y.salary_gid='" & Request.QueryString("salarygid").ToString() & "') as totaldeduction, " &
                        " (select sum(earned_salarycomponent_amount) from pay_trn_tsalarydtl x1 " &
                        " left join pay_trn_tsalary y1 on y1.salary_gid=x1.salary_gid where salarygradetype='Addition'  and y1.month='" & Request.QueryString("month").ToString() & "'" &
                        " and y1.year='" & Request.QueryString("year").ToString() & "' and " &
                        " y1.salary_gid='" & Request.QueryString("salarygid").ToString() & "') as totaladdition, " &
                        " cast(ifnull((select  format(y.repayment_amount,2) as advance_amount from pay_trn_tloanrepayment y  inner join  pay_trn_tloan z on y.loan_gid=z.loan_gid " &
                        " where z.employee_gid=a.employee_gid and date_format(y.repayment_duration,'%M')='" & Request.QueryString("month").ToString() & "' and  date_format(y.repayment_duration,'%Y')='" & Request.QueryString("year").ToString() & "'  " &
                        " and y.type='advance' group by a.employee_gid),'0.00')as char) as advance, " &
                        " cast(ifnull((select  format(y.repayment_amount,2) as loan_amount from pay_trn_tloanrepayment y  inner join  pay_trn_tloan z on y.loan_gid=z.loan_gid " &
                        " where z.employee_gid=a.employee_gid and date_format(y.repayment_duration,'%M')='" & Request.QueryString("month").ToString() & "' and  date_format(y.repayment_duration,'%Y')='" & Request.QueryString("year").ToString() & "' " &
                        " and y.type='loan' group by a.employee_gid),'0.00')as char) as loan " &
                        " from  pay_trn_tsalary a " &
                        " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid " &
                        " left join adm_mst_tuser d on b.user_gid=d.user_gid " &
                        " left join hrm_mst_tbranch e on b.branch_gid=e.branch_gid " &
                        " left join hrm_mst_tdepartment f on b.department_gid=f.department_gid  " &
                        " left join adm_mst_tdesignation g on b.designation_gid=g.designation_gid " &
                        " left join pay_trn_tpayment  h on a.salary_gid=h.salary_gid " &
                        " where a.month='" & Request.QueryString("month").ToString() & "' " &
                        " and a.year='" & Request.QueryString("year").ToString() & "' and a.salary_gid='" & Request.QueryString("salarygid").ToString() & "' " &
                        " group by salary_gid order by e.branch_name,d.user_code,a.basic_salary asc "

        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable4")


        'msSQL = " select a.authorized_sign as authorised_sign from hrm_mst_tbranch a" &
        '          " left join hrm_mst_temployee b on a.branch_gid=b.branch_gid" &
        '          " where b.employee_gid='" & Session("employee_gid") & "'"

        'MyCommand.CommandText = msSQL
        'MyCommand.CommandType = Data.CommandType.Text
        'MyDA.SelectCommand = MyCommand
        'myDS.EnforceConstraints = False
        ''This is our DataSet created at Design Time 
        'MyDA.Fill(myDS, "DataTable5")

        'msSQL = " select b.leavetype_name,total_leavecount,leave_taken,available_leavecount from hrm_mst_tleavecreditsdtl a " &
        '" left join hrm_mst_tleavetype b on a.leavetype_gid = b.leavetype_gid " &
        '" where employee_gid='" & Request.QueryString("employee_gid").ToString() & "' and year='" & Request.QueryString("year").ToString() & "' and month='" & Request.QueryString("month").ToString() & "' group by a.leavetype_gid"

        'MyCommand.CommandText = msSQL
        'MyCommand.CommandType = Data.CommandType.Text
        'MyDA.SelectCommand = MyCommand
        'myDS.EnforceConstraints = False
        ''This is our DataSet created at Design Time 
        'MyDA.Fill(myDS, "DataTable6")
        Dim oRpt As New ReportDocument
        oRpt.Load(Server.MapPath("pay_rpt_payaauraaslip.rpt"))
        oRpt.SetDataSource(myDS)
        Dim oStream As System.IO.Stream
        oStream = oRpt.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)
        With HttpContext.Current.Response
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/pdf"
            .AddHeader("Content-Disposition", "inline; filename=" & "payslip.pdf")
            Dim b(oStream.Length) As Byte
            oStream.Read(b, 0, CInt(oStream.Length))
            .BinaryWrite(b)

            .Flush()
            .SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End With
    End Sub

End Class