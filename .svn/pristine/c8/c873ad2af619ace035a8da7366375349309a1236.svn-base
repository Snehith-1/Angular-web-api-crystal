Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared

Public Class pbl_crp_txtilepaymentreceiptpdfword
    Inherits System.Web.UI.Page
    Dim msSQL As String
    Dim companycode As String
    Public gs_ConnDB As OdbcConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myConnection As New OdbcConnection
        If Request.QueryString("companycode").ToString().ToLower() = "vcidex" Then
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ttsconnection").ToString
        ElseIf Request.QueryString("companycode").ToString().ToLower() = "boba" Then
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("bobaconnection").ToString
        End If
        companycode = Request.QueryString("companycode").ToString
        msSQL = " select a.payment_mode,date_format(a.payment_date,'%d/%m/%Y') as payment_date, a.cheque_no as cheque_number,a.currency_code, " &
                                " a.payment_total as amount, date_format(a.cheque_date,'%d/%m/%Y') as cheque_date," &
                                " group_concat(d.product_name) as account_name,a.payment_gid as invoice_gid,a.payment_remarks from acp_trn_tpayment a" &
                                " left join acp_trn_tinvoice2payment b on a.payment_gid=b.payment_gid " &
                                " left join acp_trn_tinvoicedtl c on c.invoice_gid=b.invoice_gid " &
                                " left join pmr_mst_tproduct d on d.product_gid=c.product_gid " &
                                " where a.payment_gid='" & Request.QueryString("payment_gid") & "' group by a.payment_gid "
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

        msSQL = " select concat(e.branch_name,' ( ',department_name,' ) ')  as company_name from hrm_mst_tdepartment a " &
                                " left join hrm_mst_temployee c on a.department_gid=c.department_gid " &
                                " left join acp_trn_tpayment d on c.user_gid=d.user_gid " &
                                " left join hrm_mst_tbranch e on e.branch_gid = c.branch_gid " &
                                " where d.payment_gid='" & Request.QueryString("payment_gid") & "'"

        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable2")


        Dim oRpt As New ReportDocument
        oRpt.Load(Server.MapPath("pbl_crp_textilevoucher.rpt"))
        oRpt.SetDataSource(myDS)
        oRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "accountinginvoice")
        Response.End()
        Response.Close()
        oRpt.Close()
    End Sub

End Class