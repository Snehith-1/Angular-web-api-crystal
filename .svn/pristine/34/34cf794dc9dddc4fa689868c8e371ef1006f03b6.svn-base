Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared
Public Class hrm_crp_appointmentletter
    Inherits System.Web.UI.Page
    Dim msSQL As String
    Dim companycode As String
    Dim MyCommand As New OdbcCommand
    Dim myDS As New DataSet
    Dim image_path As String
    Public gs_ConnDB As OdbcConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim authDBInstance As New authdbconn()
        authDBInstance.openconn_load()
        Dim myConnection As OdbcConnection = authdbconn.gs_ConnDB

        If myConnection.State <> ConnectionState.Open Then
            myConnection.Open()
        End If
        msSQL = " select a.appointmentorder_gid,a.appointmentordertemplate_content,date_format(a.appointment_date,'%d-%m-%y') as appointment_date " &
                               " from  hrm_trn_tappointmentorder a " &
                               " where a.appointmentorder_gid  = '" & Request.QueryString("appointmentorder_gid").ToString() & "'"

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


        Dim dt As DataTable = New DataTable()
        Dim DataTable5 As DataTable = New DataTable()

        msSQL = " select  (branch_logo_path) as company_logo_Path  from hrm_mst_tbranch where branch_gid = '" & Session("branch_gid") & "' and  branch_logo_path is not null"
        'dt = objdbconn.GetDatatable(msSQL)
        DataTable5.Columns.Add("company_logo", Type.[GetType]("System.Byte[]"))
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                image_path = Server.MapPath(row("company_logo_Path"))
                image_path = image_path.Replace("/", "\\")



                '---Convert  Image Path to Byte
                If (System.IO.File.Exists(image_path) = True) Then
                    Dim image As System.Drawing.Image = System.Drawing.Image.FromFile(image_path)
                    Dim imageConverter As New System.Drawing.ImageConverter
                    Dim imageByte As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())



                    DataTable5.Rows.Add(imageByte)
                End If

            Next
        End If
        DataTable5.TableName = "DataTable5"
        myds.Tables.Add(DataTable5)

        MyCommand.CommandText = " select company_name,company_address from adm_mst_tcompany"
        MyCommand.CommandType = Data.CommandType.Text
        myda.SelectCommand = MyCommand
        myds.EnforceConstraints = False
        myda.Fill(myds, "DataTable3")

        MyCommand.CommandText = " select company_name,company_address from adm_mst_tcompany"
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable2")

        Dim oRpt As New ReportDocument
        oRpt.Load(Server.MapPath("hrm_crp_appointmentletterreport.rpt"))
        oRpt.SetDataSource(myds)
        Dim oStream As System.IO.Stream
        oStream = oRpt.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)
        With HttpContext.Current.Response
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/pdf"
            .AddHeader("Content-Disposition", "inline; filename=" & "appointmentorder.pdf")
            Dim b(oStream.Length) As Byte
            oStream.Read(b, 0, CInt(oStream.Length))
            .BinaryWrite(b)

            .Flush()
            .SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End With
    End Sub

End Class