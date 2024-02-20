Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared
Public Class pmr_crp_grnreportcr1
    Inherits System.Web.UI.Page
    Dim msSQL As String
    Dim companycode As String
    Public gs_ConnDB As OdbcConnection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim myConnection As OdbcConnection = authdbconn.gs_ConnDB
        Dim MyCommand As New OdbcCommand
        MyCommand.Connection = myConnection

        Dim authDBInstance As New authdbconn()
        authDBInstance.openconn_load()

        If myConnection.State <> ConnectionState.Open Then
            myConnection.Open()
        End If
        companycode = Request.QueryString("companycode").ToString

        MyCommand.CommandText = " select a.grn_gid, date_format(a.grn_date,'%d-%m-%Y %h:%m:%s %p') as grn_date_new, a.vendor_contact_person, " &
                                        " a.checkeruser_gid, a.purchaseorder_list, a.grn_remarks, a.grn_reference, a.grn_receipt, " &
                                        " concat(b.user_firstname,' - ',d.department_name) as user_firstname, a.invoice_refno as invoice_ref, " &
                                        " if(date_format(a.dc_date,'%d-%m-%Y')='00-00-0000','',date_format(a.dc_date,'%d-%m-%Y %h:%m:%s %p')) as dc_date," &
                                        " if(date_format(a.invoice_date,'%d-%m-%Y')='00-00-0000','',date_format(a.invoice_date,'%d-%m-%Y %h:%m:%s %p')) as invoice_date, " &
                                        " c.employee_emailid as user_email, c.employee_phoneno as user_phone,e.contact_telephonenumber,e.contactperson_name, " &
                                        " e.vendor_gid, e.vendor_companyname,a.dc_no,e.tin_number,e.cst_number, " &
                                        " concat(f.user_firstname,'  ',f.user_lastname) as user_checkername, " &
                                        " concat(g.user_firstname,'  ',g.user_lastname) as user_approvedby, " &
                                        " h.country_gid, h.address1, h.address2, h.city, h.state, h.postal_code, h.parent_gid, h.fax,dc_date, invoice_refno, invoice_date, " &
                                        " i.branch_logo, (i.branch_location) as branch_footer " &
                                        " from pmr_trn_tgrn a " &
                                        " left join adm_mst_tuser b on a.user_gid = b.user_gid " &
                                        " left join hrm_mst_temployee c on c.user_gid = b.user_gid " &
                                        " left join hrm_mst_tdepartment d on c.department_gid = d.department_gid " &
                                        " left join acp_mst_tvendor e on e.vendor_gid = a.vendor_gid " &
                                        " left join adm_mst_tuser g on g.user_gid = a.approved_by " &
                                         " left join adm_mst_tuser f on f.user_gid = a.checkeruser_gid " &
                                        " left join adm_mst_taddress h on h.address_gid = e.address_gid " &
                                        " left join hrm_mst_tbranch i on a.branch_gid = i.branch_gid " &
                                        " where a.grn_gid = '" & Request.QueryString("grn_gid").ToString & "'"
        MyCommand.CommandType = Data.CommandType.Text
        Dim MyDA As New OdbcDataAdapter
        MyDA.SelectCommand = MyCommand
        Dim myDS As New DataSet
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable1")
        MyCommand.CommandText = "select a.grn_gid,a.product_remarks,replace(format(a.qty_delivered,2),',','') as qty_delivered,a.qty_billed," &
                                        " a.qty_rejected,replace(format(b.qty_ordered,2),',','') as qty_ordered,a.qty_invoice," &
                                        " replace(format(a.qtyreceivedas,2),',','') as qtyreceivedas,a.display_field," &
                                        " a.product_name,a.product_code, d.productgroup_name, a.productuom_name," &
                                        " replace(format(b.qty_ordered-sum(b.qty_received),2),',','') as qty_balance from pmr_trn_tgrndtl a" &
                                        " left join pmr_trn_tpurchaseorderdtl b on a.purchaseorderdtl_gid=b.purchaseorderdtl_gid" &
                                        " left join pmr_mst_tproduct c on a.product_gid = c.product_gid" &
                                        " left join pmr_mst_tproductgroup d on c.productgroup_gid = d.productgroup_gid" &
                                        " left join pmr_mst_tproductuom e on e.productuom_gid = a.uom_gid" &
                                        " where a.grn_gid='" & Request.QueryString("grn_gid").ToString & "' group by a.grndtl_gid"
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable2")

        MyCommand.CommandText = "select a.branch_name,a.address1,a.city,a.state,a.postal_code,a.contact_number,a.email,a.branch_gid,a.branch_logo,a.tin_number,a.cst_number from hrm_mst_tbranch a " &
                                        "left join pmr_trn_tgrn b on a.branch_gid=b.branch_gid " &
                                        "where b.grn_gid='" & Request.QueryString("grn_gid").ToString & "'"
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable3")

        MyCommand.CommandText = " select concat(d.user_firstname, ' ',d.user_lastname,' ','/',' ',a.approval_remarks) as approval_remarks  " &
                                        " from pmr_trn_tapproval a left join pmr_trn_tgrn b on a.grnapproval_gid=b.grn_gid " &
                                        " left join hrm_mst_temployee c on a.approved_by=c.employee_gid " &
                                        " left join adm_mst_tuser d on c.user_gid=d.user_gid " &
                                        " where b.grn_gid='" & Request.QueryString("grn_gid").ToString & "' and a.approval_flag='Y' "
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable4")

        'Dim oRpt As New ReportDocument
        'msSQL = "select currency_code from adm_mst_tcompany where company_code='" & Session("company_code") & "'"
        'objdbconn.OpenConn()
        'cur_code = objdbconn.GetExecuteScalar(msSQL)
        'objdbconn.CloseConn()
        'oRpt.Load(Server.MapPath("pmr_crp_grnreportcr.rpt"))
        'oRpt.SetDataSource(myDS)
        'oRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "grninward")
        'Response.End()
        'Response.Close()
        'oRpt.Close()
        'oRpt.Dispose()

        'Dim oRpt As New ReportDocument
        'oRpt.Load(Server.MapPath("pmr_crp_grnreportcr.rpt"))
        'oRpt.SetDataSource(myDS)
        'Dim oStream As System.IO.Stream
        'oStream = oRpt.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)
        'With HttpContext.Current.Response
        '    .ClearContent()
        '    .ClearHeaders()
        '    .ContentType = "application/pdf"
        '    .AddHeader("Content-Disposition", "inline; filename=" & "GRNReport.pdf")
        '    Dim b(oStream.Length) As Byte
        '    oStream.Read(b, 0, CInt(oStream.Length))
        '    .BinaryWrite(b)

        '    .Flush()
        '    .SuppressContent = True
        '    HttpContext.Current.ApplicationInstance.CompleteRequest()
        'End With

        Dim oRpt As New ReportDocument
        If Request.QueryString("companycode").ToString().ToLower() = "boba" Then

            oRpt.Load(Server.MapPath("pmr_crp_grnreportcr.rpt"))
        Else
            oRpt.Load(Server.MapPath("pmr_crp_grnreportcr.rpt"))

        End If

        oRpt.SetDataSource(myDS)
        Dim oStream As System.IO.Stream
        oStream = oRpt.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)

        With HttpContext.Current.Response
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/pdf"
            .AddHeader("Content-Disposition", "inline; filename=" & "GRNInward.pdf")
            Dim b(oStream.Length) As Byte
            oStream.Read(b, 0, CInt(oStream.Length))
            .BinaryWrite(b)

            .Flush()
            .SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End With

        'Dim oRpt As New ReportDocument
        'If Request.QueryString("companycode").ToString().ToLower() = "boba" Then

        '    oRpt.Load(Server.MapPath("pmr_crp_grnreportcr.rpt"))
        'Else
        '    oRpt.Load(Server.MapPath("pmr_crp_grnreportcr.rpt"))
        'End If


        'oRpt.SetDataSource(myDS)
        'oRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "grninward")
        'Response.End()
        'Response.Close()
        'oRpt.Close()

    End Sub

    Public Function GetDatatable(Optional ByVal SQL As String = "null", Optional ByVal module_reference As String = "null", Optional ByVal module_name As String = "Log") As DataTable
        'This function will Retrieve Data and Return as Dataset together with table name
        Dim lobjDataAdapter As New OdbcDataAdapter(SQL, gs_ConnDB)
        Dim lobjDataTable As New DataTable
        Try
            lobjDataAdapter.Fill(lobjDataTable)
        Catch ex As OdbcException


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