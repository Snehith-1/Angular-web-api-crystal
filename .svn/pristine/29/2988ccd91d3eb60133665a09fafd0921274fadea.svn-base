





Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared


Public Class print1
    Inherits System.Web.UI.Page
    Dim msSQL As String
    Dim companycode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myConnection As New OdbcConnection
        If Request.QueryString("companycode").ToString = "TTC" Then
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ttcconnection").ToString
        ElseIf Request.QueryString("companycode").ToString = "XYZ" Then
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("xyzconnection").ToString
        ElseIf Request.QueryString("companycode").ToString = "ABC" Then
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("abcconnection").ToString
        ElseIf Request.QueryString("companycode").ToString = "123" Then
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("123connection").ToString
        ElseIf Request.QueryString("companycode").ToString = "tts" Then
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("ttsconnection").ToString
        End If

        companycode = Request.QueryString("companycode").ToString

        msSQL = " select a.quotation_gid, a.enquiry_gid, a.quotation_refno, a.quotation_date, a.customer_name,a.terms_conditions, " &
                " a.contact_type, a.company_name, a.contact_no, a.email_address, a.address, a.country, a.currency, " &
                " b.country_name, a.remarks, a.quotation_amount," &
                " a.total, a.discount, a.addon_charge, a.net_amount " &
                " from sal_trn_tquotation a " &
                " left join sys_mst_tcurrency b on b.country_code=a.country " &
                " where a.quotation_gid='" & Request.QueryString("quotationid").ToString & "' "
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

        msSQL = " select quotation_gid,quotationdtl_gid,service_details,remarks, amount,unitprice,quantity,description, " &
                " discount,netamount,unit_gid,unit_name from sal_trn_tquotationdtl " &
                " where quotation_gid='" & Request.QueryString("quotationid").ToString & "' "
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable2")

        msSQL = "select concat('" & ConfigurationManager.AppSettings("server").ToString & "','" & companycode & "','/UploadDocs/letterheadlogo','/',letterheadlogo_filename) as letterhead_logo from sys_mst_tcompany where" &
               " company_code ='" & Request.QueryString("companycode").ToString & "'"
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable3")

        Dim oRpt As New ReportDocument
        oRpt.Load(Server.MapPath("trv_crp_quotation.rpt"))
        oRpt.SetDataSource(myDS)
        Dim oStream As System.IO.Stream = Nothing
        Dim byteArray As Byte() = Nothing
        oStream = oRpt.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)
        byteArray = New Byte(oStream.Length - 1) {}
        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1))
        Response.Clear()
        Context.Response.AddHeader("Content-Disposition", "Attachment;Filename=Quotation.pdf")
        Response.Buffer = False
        Response.ContentType = "application/pdf"
        Response.BinaryWrite(byteArray)
        oRpt.Close()
        oRpt.Dispose()

    End Sub

End Class