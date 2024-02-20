Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared
Imports Microsoft.VisualBasic.CompilerServices
Imports CrystalDecisions.ReportAppServer.ReportDefModel
Imports ReportDocument = CrystalDecisions.CrystalReports.Engine.ReportDocument

Public Class crm_trn_opendcadd
    Inherits System.Web.UI.Page
    Dim msSQL As String
    Dim objODBCDataReader, objODBCDataReader1, objODBCDataReaders As OdbcDataReader
    Dim companycode As String
    Dim lsAmountInWords As String

    Public gs_ConnDB As OdbcConnection
    Private objcmnfunctions As Object
    Private ReadOnly objdbconn As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim dt3 As DataTable = New DataTable()
        Dim DataTable5 As DataTable = New DataTable()
        Dim MyCommand As New OdbcCommand
        Dim myDS As New DataSet
        Dim MyDA As New OdbcDataAdapter
        Dim authDBInstance As New authdbconn()
        authDBInstance.openconn_load()
        Dim myConnection As OdbcConnection = authdbconn.gs_ConnDB

        If myConnection.State <> ConnectionState.Open Then
            myConnection.Open()
        End If

        msSQL = " select a.directorder_gid,date_format(a.directorder_date,'%d/%m/%Y') as directorder_date,a.directorder_refno," &
                    " a.directorder_remarks,a.terms_condition,format(a.product_grandtotal,2) as product_grandtotal, " &
                    " format(a.addon_amount,2) as addon_amount,format(a.addon_discount,2) as addon_discount, " &
                    " format(a.grandtotal_amount,2) as grandtotal_amount,a.salesorder_gid,a.shipping_to as customer_address2, " &
                   " b.customer_name, " &
                   " a.customer_address,a.customer_contactperson as DataColumn22, a.customer_emailid as email," &
                   "  a.customer_contactnumber as  mobile " &
                    " from smr_trn_tdeliveryorder a " &
                    " left join crm_mst_tcustomer b on b.customer_gid=a.customer_gid " &
                    " left join crm_mst_tcustomercontact c on c.customer_gid=b.customer_gid " &
                     "where a.directorder_gid='" & Request.QueryString("directorder_gid").ToString & "'"


        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time
        MyDA.Fill(myDS, "DataTable1")

        msSQL = " select a.directorderdtl_gid,a.directorder_gid,a.productuom_name,a.product_qty,a.product_price, " &
                    " a.product_description, a.product_qtydelivered, a.tax_name,a.tax_name2,a.tax_name3, " &
                    " a.dc_no,a.mode_of_despatch,a.tracker_id,b.product_code,c.productgroup_name,b.product_name, " &
                    " a.directorderdtl_gid as tax_amount,a.directorderdtl_gid as tax_amount2, " &
                    " a.directorderdtl_gid as tax_amount3, " &
                    " a.directorderdtl_gid as total_tax_amount " &
                    " from smr_trn_tdeliveryorderdtl a " &
                    " left join pmr_mst_tproduct b on b.product_gid=a.product_gid " &
                    " left join pmr_mst_tproductgroup c on b.productgroup_gid=c.productgroup_gid " &
                     "where a.directorder_gid='" & Request.QueryString("directorder_gid").ToString & "'"


        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable2")

        msSQL = "select a.branch_name,a.address1,a.city,a.state,a.postal_code,a.contact_number,a.email, " &
                                "a.branch_gid,a.branch_logo from hrm_mst_tbranch a " &
                                "left join hrm_mst_temployee b on a.branch_gid=b.branch_gid " &
                                "left join smr_trn_tdeliveryorder c on c.created_name=b.employee_gid " &
                                "where c.directorder_gid='" & Request.QueryString("directorder_gid").ToString & "'"


        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable3")


        Dim oRpt As New ReportDocument
        oRpt.Load(Server.MapPath("crm_trn_opendc.rpt"))
        oRpt.SetDataSource(myDS)
        'Dim oStream As System.IO.Stream
        oRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Open Delivery Order")
        ' Close and dispose of the report document to release resources
        oRpt.Close()
        oRpt.Dispose()



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

End Class