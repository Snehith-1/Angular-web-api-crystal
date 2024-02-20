Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared

Public Class rbl_mst_paymentsummaryreceiptrpt
    Inherits System.Web.UI.Page
    Dim msSQL As String
    Dim companycode As String
    Public gs_ConnDB As OdbcConnection
    Dim lspayment_type As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim authDBInstance As New authdbconn()
        authDBInstance.openconn_load()

        Dim myConnection As OdbcConnection = authdbconn.gs_ConnDB

        If myConnection.State <> ConnectionState.Open Then
            myConnection.Open()
        End If

        msSQL = "select a.payment_date, a.payment_remarks, a.payment_mode, a.cheque_number, " &
                                "a.amount, a.cheque_date,a.serial_no, " &
                                "b.customer_name,c.invoice_refno as invoice_gid,c.currency_code from rbl_trn_tpayment a " &
                                "left join rbl_trn_tinvoice c on c.invoice_gid=a.invoice_gid " &
                                "left join crm_mst_tcustomer b on b.customer_gid=c.customer_gid " &
                                " where a.payment_gid='" & Request.QueryString("payment_gid") & "' "

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

        If lspayment_type = Request.QueryString("Service") Then
            msSQL = " select a.branch_logo,a.branch_name,a.address1,a.city,a.state,a.postal_code,a.gst_no," &
                                    " a.st_number,a.contact_number,a.email,c.invoice_reference,a.tin_number,a.cst_number,a.authorized_sign from " &
                                    " hrm_mst_tbranch a " &
                                    " left join smr_trn_tsalesorder b on b.branch_gid=a.branch_gid " &
                                    " left join rbl_trn_tinvoice c on c.invoice_reference=b.salesorder_gid " &
                                    " left join rbl_trn_tpayment d on d.invoice_gid=c.invoice_gid " &
                                    " where d.payment_gid='" & Request.QueryString("payment_gid") & "'"
        ElseIf lspayment_type = Request.QueryString("Agreement") Then
            msSQL = " select a.branch_logo,a.branch_name,a.address1,a.city,a.state,a.postal_code,a.gst_no," &
                                    " a.st_number,a.contact_number,a.email,a.tin_number,a.cst_number,a.authorized_sign from " &
                                    " hrm_mst_tbranch a " &
                                    " left join crm_trn_tagreement b on b.branch_gid=a.branch_gid " &
                                    " left join rbl_trn_tinvoice c on c.invoice_reference=b.agreement_gid " &
                                    " left join rbl_trn_tpayment d on d.invoice_gid=c.invoice_gid " &
                                    " where d.payment_gid='" & Request.QueryString("payment_gid") & "'"
        Else
            msSQL = "select a.branch_name,a.address1,a.city,a.state,a.postal_code,a.contact_number,a.gst_no," &
                                    " a.email,a.branch_gid,a.branch_logo , a.authorized_sign from hrm_mst_tbranch a " &
                                    " left join hrm_mst_temployee b on a.branch_gid=b.branch_gid " &
                                    " left join rbl_trn_tpayment c on c.created_by=b.user_gid " &
                                    " where c.payment_gid='" & Request.QueryString("payment_gid") & "'"

        End If
        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable2")




        Dim dt3 As DataTable = New DataTable()
        Dim DataTable3 As DataTable = New DataTable()
        Dim image_path As String

        'msSQL = " select  (branch_logo_path) as company_logo_Path  from hrm_mst_tbranch where branch_gid = '" & Session("branch_gid") & "' "
        msSQL = " select  (branch_logo_path) as company_logo  from hrm_mst_tbranch where branch_gid = '" & Session("branch_gid") & "' and  branch_logo_path is not null"

        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        DataTable3.TableName = "DataTable3"
        myDS.Tables.Add(DataTable3)

        Dim oRpt As New ReportDocument
        'If Request.QueryString("companycode").ToString().ToLower() = "boba" Then

        '    oRpt.Load(Server.MapPath("rbl_crp_paymentreceipt.rpt"))
        'Else
        '    oRpt.Load(Server.MapPath("rbl_crp_invoicereport.rpt"))
        'End If


        'oRpt.SetDataSource(myDS)
        'oRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "accountinginvoice")
        'Response.End()
        'Response.Close()
        'oRpt.Close()


        Try
            oRpt.Load(Server.MapPath("rbl_crp_paymentreceipt.rpt"))
            oRpt.SetDataSource(myDS)
            For Each table As Table In oRpt.Database.Tables
                Dim tableLogOnInfo As TableLogOnInfo = table.LogOnInfo
                Dim connectionInfo As ConnectionInfo = tableLogOnInfo.ConnectionInfo
                Dim connection As String = myConnection.ConnectionString
                connectionInfo.IntegratedSecurity = False
                connectionInfo.ServerName = connection
                connectionInfo.DatabaseName = connection
                connectionInfo.UserID = connection
                connectionInfo.Password = connection
                table.ApplyLogOnInfo(tableLogOnInfo)
            Next
            oRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "accountinginvoice")
            ' Close and dispose of the report document to release resources
            oRpt.Close()
            oRpt.Dispose()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub
    Public Function GetDatatable(Optional ByVal SQL As String = "null", Optional ByVal module_reference As String = "null", Optional ByVal module_name As String = "Log") As DataTable
        'This function will Retrieve Data and Return as Dataset together with table name
        Dim lobjDataAdapter As New OdbcDataAdapter(SQL, gs_ConnDB)
        Dim lobjDataTable As New DataTable
        Try
            lobjDataAdapter.Fill(lobjDataTable)
        Catch ex As OdbcException

            LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + ex.Message.ToString() + "*****Query****" + SQL + "*******Apiref********" + module_reference, module_name)
        End Try
        Return lobjDataTable
    End Function
    Public Sub LogForAudit(ByVal strVal As String, ByVal module_name As String)
        Try
            Dim lspath As String = HttpContext.Current.Server.MapPath("../../documents/") & HttpContext.Current.Session("company_code") & "_" &
                System.IO.Path.GetFileName(HttpContext.Current.Request.Url.ToString()).Replace(".aspx", String.Empty).Replace("?ls=", String.Empty) & "_" & Format(Now(), "dd-MM-yyyy HH-mm-ss") & ".txt"
            If (Not System.IO.File.Exists(lspath)) Then
                System.IO.File.Create(lspath).Dispose()
            End If
            Dim sw As New System.IO.StreamWriter(lspath)
            sw.WriteLine(strVal)
            sw.Close()
        Catch ex As Exception
        End Try
    End Sub

End Class