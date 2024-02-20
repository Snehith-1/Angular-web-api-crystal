
Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared

Public Class crm_trn_quatationinvoise
    Inherits System.Web.UI.Page
    Dim msSQL As String
    Dim companycode As String
    Public gs_ConnDB As OdbcConnection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim lsquotation_gid As String
        Dim image_path As String
        Dim dt3 As DataTable = New DataTable()
        Dim DataTable5 As DataTable = New DataTable()


        Dim authDBInstance As New authdbconn()
        authDBInstance.openconn_load()
        Dim myConnection As OdbcConnection = authdbconn.gs_ConnDB

        If myConnection.State <> ConnectionState.Open Then
            myConnection.Open()
        End If

        msSQL = "select a.display_total,a.quotation_gid,if(a.roundoff is null,'0.00',if(a.roundoff='','0.00',cast(a.roundoff as char))) as roundoff,a.termsandconditions,date_format(a.quotation_date,'%d/%m/%Y') as quotation_date,g.gst_number," &
                                                " if(a.freight_charges is null,'0.00',if(a.freight_charges='','0.00',cast(a.freight_charges as char))) as freight_charges,if(a.buyback_charges is null,'0.00',if(a.buyback_charges='','0.00',cast(a.buyback_charges as char))) as buyback_charges," &
                                                " if(a.packing_charges is null,'0.00',if(a.packing_charges='','0.00',cast(a.packing_charges as char))) as packing_charges,if(a.insurance_charges is null,'0.00',if(a.insurance_charges='','0.00',cast(a.insurance_charges as char))) as insurance_charges,format(a.gst_percentage,2)as gst_percentage, " &
                                                " a.quotation_referenceno1,a.payment_days, date_format(date_add(quotation_date,interval a.payment_days day),'%d/%m/%Y') as quotation_remarks ,a.gst_percentage, " &
                                                " a.delivery_days,format(a.Grandtotal,2) as Grandtotal ,a.freight_terms,a.payment_terms as payment_term,if(a.addon_charge is null,'0.00',if(a.addon_charge='','0.00',cast(a.addon_charge as char)))as addon_charge,if(a.additional_discount is null,'0.00',if(a.additional_discount='','0.00',cast(a.additional_discount as char)))as additional_discount, " &
                                                "a.enquiry_gid, format(sum(d.price),2) as total_value,b.leadbank_name,a.currency_code as currency,e.enquiry_referencenumber, f.customer_logo," &
                                                " case when a.customer_contact_person is not null then  a.customer_contact_person " &
                                                "  when a.customer_contact_person is null then c.leadbankcontact_name end as leadbankcontact_name, " &
                                                " case when a.contact_mail is not null then  a.contact_mail " &
                                                "  when a.contact_mail is null then c.email end as email, " &
                                                " case when a.contact_no is not null then  a.contact_no " &
                                                "  when a.contact_no is null then c.mobile end as mobile, " &
                                                " case when a.customer_address is not null then  a.customer_address " &
                                                " when a.customer_address is null then b.leadbank_address1 end as leadbank_address1, " &
                                                " case when a.customer_address is not null then  '' " &
                                                "  when a.customer_address is null then b.leadbank_address2 end as leadbank_address2, " &
                                                " case when a.customer_address is not null then  '' " &
                                                " when a.customer_address is null then b.leadbank_city end as leadbank_city, " &
                                                " case when a.customer_address is not null then  '' " &
                                                " when a.customer_address is null then b.leadbank_state end as leadbank_state, " &
                                                " case when a.customer_address is not null then  '' " &
                                                " when a.customer_address is null then b.leadbank_pin end as leadbank_pin,ifnull(a.customerenquiryref_number,' ') as quotation_referencenumber, " &
                                                " i.branch_name as DataColumn9,i.branch_gid,i.address1 as DataColumn10,i.city  as DataColumn11,i.state  as DataColumn12,i.postal_code  as DataColumn13 " &
                                                " from smr_trn_treceivequotation a " &
                                                " left join crm_trn_tleadbank b on b.leadbank_gid=a.customer_gid " &
                                                " left join crm_trn_tleadbankcontact c on c.leadbank_gid=b.leadbank_gid " &
                                                " left join smr_trn_treceivequotationdtl d on d.quotation_gid=a.quotation_gid  " &
                                                " left join smr_trn_tsalesenquiry e on e.branch_gid=a.branch_gid " &
                                                " left join crm_mst_tcustomer f on f.customer_gid=b.customer_gid " &
                                                " left join crm_mst_tcustomercontact g on g.customer_gid=f.customer_gid " &
                                                "  left join hrm_mst_tbranch i on i.branch_gid=a.branch_gid " &
                                                " where a.quotation_gid='" & Request.QueryString("quotation_gid").ToString & "' group by a.quotation_gid"

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

        msSQL = " select a.slno as product_gid,e.uom_gid,o.vendor_code as vendor_gid,a.margin_percentage,a.selling_price, " &
                                    " a.quotation_gid,a.product_name,concat('£',format(a.product_price,2)) as product_price,concat('£',format(a.product_price*a.qty_quoted,2)) as product_code," &
                                    " a.qty_quoted,a.discount_percentage,a.discount_amount,a.uom_name,a.display_field,a.tax_name,a.tax_amount,a.tax_name2,a.tax_amount2, " &
                                    " a.tax_name3,a.tax_amount3,concat('£',format(a.price,2)) as price, " &
                                    " CASE  WHEN (a.tax_name = '--No Tax--' OR a.tax_name = 'NoTax') THEN 'No Tax'" &
                                    " WHEN (a.tax_name2 = '--No Tax--' OR a.tax_name2 = 'NoTax') THEN CONCAT(a.tax_name) " &
                                    " WHEN (a.tax_name3 = '--No Tax--' OR a.tax_name3 = 'NoTax') THEN CONCAT(a.tax_name, ' , ', a.tax_name2) " &
                                    " Else CONCAT(a.tax_name, ' , ', a.tax_name2, ' , ', a.tax_name3) End As all_taxes," &
                                    " concat('£',format(SUM(a.tax_amount + a.tax_amount2 + a.tax_amount3),2)) As productgroup_name,CONCAT(UPPER(ConvertAmountinWords(a.price)), ' POUNDS ONLY') AS DataColumn8 " &
                                    " from smr_trn_treceivequotationdtl a " &
                                    " left join pmr_mst_tproduct b On b.product_gid=a.product_gid" &
                                    " left join pmr_mst_tproductgroup c On b.productgroup_gid=c.productgroup_gid " &
                                    " left join pmr_mst_tcatalog d On d.product_gid = b.product_gid " &
                                    " left join pmr_mst_tcatalog e On e.uom_gid = b.productuom_gid" &
                                    " left join acp_mst_tvendor o On e.vendor_gid = o.vendor_gid" &
                                    " where a.quotation_gid = '" & Request.QueryString("quotation_gid").ToString & "' group by b.product_gid order by a.quotationdtl_gid asc "


        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable2")


        msSQL = " select a.branch_name,a.branch_gid,a.address1,a.city,a.state,a.postal_code,b.quotation_gid,a.branch_logo_path as branch_logo from hrm_mst_tbranch a " &
                                 " left join smr_trn_treceivequotation b on b.branch_gid = a.branch_gid " &
                                 " where b.quotation_gid='" & Request.QueryString("quotation_gid").ToString & "' group by b.quotation_gid"

        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable3")

        msSQL = "SELECT  tax_name AS sum_tax1, CONCAT('£', CAST(SUM(tax_amount) AS CHAR)) AS sum_tax2 " &
                            "FROM ( " &
                            "SELECT tax_name, tax_amount " &
                            "FROM smr_trn_treceivequotationdtl " &
                            "WHERE quotation_gid = '" & Request.QueryString("quotation_gid").ToString & "' " &
                            "AND NOT (tax_name LIKE '%No%' AND tax_amount = 0) " &
                            "UNION ALL " &
                            "SELECT tax_name2 AS tax_name, tax_amount2 AS tax_amount " &
                            "FROM smr_trn_treceivequotationdtl " &
                            "WHERE quotation_gid = '" & Request.QueryString("quotation_gid").ToString & "' " &
                            "AND NOT (tax_name2 LIKE '%No%' AND tax_amount2 = 0) " &
                            "UNION ALL " &
                            "SELECT tax_name3 AS tax_name, tax_amount3 AS tax_amount " &
                            "FROM smr_trn_treceivequotationdtl " &
                            "WHERE quotation_gid = '" & Request.QueryString("quotation_gid").ToString & "' " &
                            "AND NOT (tax_name3 LIKE '%No%' AND tax_amount3 = 0) " &
                            ") AS subquery " &
                            "GROUP BY sum_tax1;"


        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable4")


        Dim oRpt As New ReportDocument
        oRpt.Load(Server.MapPath("crm_rpt_quotaioninvoice.rpt"))
        oRpt.SetDataSource(myDS)
        'Dim oStream As System.IO.Stream
        oRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, " Quotation Order Receipt")
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