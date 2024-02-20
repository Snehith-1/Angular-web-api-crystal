Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared
Public Class pbl_trn_paymentsummaryrpt
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
        companycode = Request.QueryString("companycode").ToString


        msSQL = " select a.payment_gid,a.payment_date,a.vendor_gid,a.payment_remarks,a.payment_total,a.payment_status,a.user_gid, " &
                                        " date_format(a.created_date,'%d-%m-%y') as created_date,a.payment_reference,a.purchaseorder_gid,a.advance_total,a.payment_mode,a.bank_name,a.branch_name, " &
                                        " concat(a.cheque_no,a.dd_no)as cheque_no,a.city_name,a.currency_code,a.exchange_rate,a.tds_amount,a.tdscalculated_finalamount," &
                                        " a.priority,a.priority_remarks,a.approved_by,a.approved_date,a.reject_reason,a.bank_gid,a.payment_from, " &
                                        " a.addon_amount,a.additional_discount,a.additional_gid,a.discount_gid,b.*,c.* " &
                                        " from acp_trn_tpayment a " &
                                        " left join acp_trn_tpaymentdtl c on a.payment_gid=c.payment_gid " &
                                        " left join acp_mst_tvendor b on b.vendor_gid=a.vendor_gid " &
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

        msSQL = " select a.branch_name, a.address1 as branch_address, a.city, a.state, a.postal_code," &
                                        " a.branch_logo, a.contact_number, a.email, a.tin_number, a.cst_number,b.currency_code,c.currency_symbol," &
                                        " a.authorise_sign from hrm_mst_tbranch a  left join acp_trn_tinvoice x on a.branch_gid=x.branch_gid" &
                                        " left join acp_trn_tinvoice2payment y on x.invoice_gid=y.invoice_gid" &
                                        " left join acp_trn_tpayment b on y.payment_gid=b.payment_gid" &
                                        " left join crm_trn_tcurrencyexchange c on b.currency_code = c.currency_code " &
                                        " where b.payment_gid='" & Request.QueryString("payment_gid") & "'"

        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable2")


        Dim dt3 As DataTable = New DataTable()
        Dim DataTable3 As DataTable = New DataTable()
        msSQL = " select  (branch_logo_path) as company_logo  from hrm_mst_tbranch where branch_gid = '" & Session("branch_gid") & "' and  branch_logo_path is not null"

        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        DataTable3.TableName = "DataTable3"
        myDS.Tables.Add(DataTable3)
        Dim oRpt As New ReportDocument
        oRpt.SetDataSource(myDS)
        Dim oStream As System.IO.Stream
        oStream = oRpt.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)
        With HttpContext.Current.Response
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/pdf"
            .AddHeader("Content-Disposition", "inline; filename=" & "Receipt.pdf")
            Dim b(oStream.Length) As Byte
            oStream.Read(b, 0, CInt(oStream.Length))
            .BinaryWrite(b)

            .Flush()
            .SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End With

        oRpt.Load(Server.MapPath("pbl_crp_paymentadvicereport.rpt"))
        oRpt.SetDataSource(myDS)
        oRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "accountinginvoice")
        Response.End()
        Response.Close()
        oRpt.Close()

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