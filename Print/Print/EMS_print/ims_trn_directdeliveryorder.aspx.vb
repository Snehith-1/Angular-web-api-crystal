
Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared

Public Class ims_trn_directdeliveryorder
    Inherits System.Web.UI.Page
    Dim msSQL As String
    Dim companycode As String
    Public gs_ConnDB As OdbcConnection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim image_path As String
        Dim dt3 As DataTable = New DataTable()
        Dim DataTable5 As DataTable = New DataTable()
        Dim authDBInstance As New authdbconn()
        authDBInstance.openconn_load()
        Dim myConnection As OdbcConnection = authdbconn.gs_ConnDB

        If myConnection.State <> ConnectionState.Open Then
            myConnection.Open()
        End If


        msSQL = " select a.directorder_gid,date_format(a.directorder_date,'%d-%m-%Y') as directorder_date,a.directorder_refno," &
                                " a.directorder_remarks,a.terms_condition,format(a.product_grandtotal,2) as product_grandtotal, " &
                                " format(a.addon_amount,2) as addon_amount,format(a.addon_discount,2) as addon_discount, " &
                                " format(a.grandtotal_amount,2) as grandtotal_amount,k.so_referenceno1 as customerpo_no, " &
                                " so_referencenumber as customerpo_no, a.shipping_to as customer_address2," &
                                " b.customer_name,a.dc_type, a.dc_no, a.mode_of_despatch, a.tracker_id," &
                                " a.customer_address,a.customer_contactperson as DataColumn21, a.customer_emailid as email," &
                                " a.customer_contactnumber as  mobile " &
                                " from smr_trn_tdeliveryorder a " &
                                " left join smr_trn_tsalesorder k on k.salesorder_gid=a.salesorder_gid " &
                                " left join crm_mst_tcustomer b on b.customer_gid=a.customer_gid " &
                                " left join crm_mst_tcustomercontact c on c.customer_gid=b.customer_gid " &
                                " where a.directorder_gid='" & Request.QueryString("directorder_gid").ToString & "' group by a.directorder_gid"

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



        msSQL = " select a.directorderdtl_gid,a.directorder_gid,a.productuom_name,a.product_qty,a.product_price, if(f.customerproduct_code='&nbsp;',' ',f.customerproduct_code) as customerproduct_code, " &
                    " a.product_description, a.product_qtydelivered, a.tax_name,a.tax_name2,a.tax_name3, sum(product_qtydelivered) as sumqtytotal," &
                    " d.dc_no,d.mode_of_despatch,d.tracker_id,a.product_code,c.productgroup_name,a.product_name, " &
                    " a.directorderdtl_gid as tax_amount,a.directorderdtl_gid as tax_amount2,e.design_no,e.color_name, " &
                    " a.directorderdtl_gid as tax_amount3, " &
                    " a.directorderdtl_gid as total_tax_amount " &
                    " from smr_trn_tdeliveryorderdtl a " &
                    " left join pmr_mst_tproduct b on b.product_gid=a.product_gid " &
                    " left join pmr_mst_tproductgroup c on b.productgroup_gid=c.productgroup_gid " &
                    " left join smr_trn_tdeliveryorder d on d.directorder_gid=a.directorder_gid " &
                    " left join acp_trn_torderdtl e on e.salesorder_gid=d.salesorder_gid and a.product_gid=e.product_gid " &
                    " left join smr_trn_tsalesorderdtl f on f.salesorderdtl_gid = a.salesorderdtl_gid " &
                    " where a.directorder_gid='" & Request.QueryString("directorder_gid").ToString & "' group by directorderdtl_gid order by directorderdtl_gid asc "
        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable2")

        msSQL = "select a.branch_name,a.address1,a.city,a.state,a.postal_code,a.contact_number,a.email as email_address,a.gst_no, " &
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


        msSQL = " select a.salesorder_gid,b.salesorder_gid,b.directorder_gid,a.design_no from acp_trn_torderdtl a " &
                    " left join smr_trn_tdeliveryorder b on b.salesorder_gid = a.salesorder_gid " &
                    " where b.directorder_gid='" & Request.QueryString("directorder_gid").ToString & "'"

        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable4")


        Dim oRpt As New ReportDocument
        oRpt.Load(Server.MapPath("ims_rpt_deliveryorder.rpt"))
        oRpt.SetDataSource(myDS)
        'Dim oStream As System.IO.Stream
        oRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, " Delivery Order Receipt")
        'Close and dispose of the report document to release resources
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