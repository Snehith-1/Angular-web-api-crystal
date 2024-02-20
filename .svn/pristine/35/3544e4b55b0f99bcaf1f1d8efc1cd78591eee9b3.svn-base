Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared
Public Class rbl_trn_proformainvoice
    Inherits System.Web.UI.Page
    Dim msSQL As String
    Dim companycode As String
    Public gs_ConnDB As OdbcConnection


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim authDBInstance As New authdbconn()
        authDBInstance.openconn_load()
        Dim myConnection As OdbcConnection = authdbconn.gs_ConnDB

        If myConnection.State <> ConnectionState.Open Then
            myConnection.Open()
        End If
        companycode = Request.QueryString("companycode").ToString
        msSQL = " select a.invoice_gid,date_format(a.invoice_date,'%d-%m-%Y')as invoice_date,a.customer_gid,g.quotation_referenceno1, " &
                    " a.invoice_amount,f.salesorder_gid,f.so_referencenumber,a.invoice_refno, a.freightcharges_amount,f.total_price,f.gst_amount," &
                    " a.total_amount, a.advance_amount, concat('M/s.',' ',f.customer_name) as customer_name, " &
                    " b.customer_code, b.tin_number, b.cst_number,   f.customer_address,f.customer_contact_person as customercontact_name, " &
                    " a.customer_email as email,f.customer_mobile as mobile,a.invoice_percent,a.roundoff,a.advance_roundoff,a.invoicepercent_amount," &
                    " if(a.freight_charges is null,'0.00',if(a.freight_charges='','0.00',cast(a.freight_charges as char))) as freight_charges, " &
                    " if(a.buyback_charges is null,'0.00',if(a.buyback_charges='','0.00',cast(a.buyback_charges as char))) as buyback_charges," &
                    " if(a.packing_charges is null,'0.00',if(a.packing_charges='','0.00',cast(a.packing_charges as char))) as packing_charges, " &
                    " if(a.insurance_charges is null,'0.00',if(a.insurance_charges='','0.00',cast(a.insurance_charges as char))) as insurance_charges," &
                    " if(a.additionalcharges_amount is null,'0.00', if(a.additionalcharges_amount='','0.00',cast(a.additionalcharges_amount as char))) as additionalcharges_amount," &
                    " if(a.discount_amount is null,'0.00', if(a.discount_amount='','0.00',cast(a.discount_amount as char))) as discount_amount," &
                    " date_format(f.salesorder_date,'%d-%m-%Y') as customerpo_date, "

        msSQL &= " a.invoice_reference as directorder_gid, a.termsandconditions,a.currency_code as designation,f.so_referenceno1,a.currency_code " &
                        " from rbl_trn_tproformainvoice a" &
                        " left join crm_mst_tcustomer b on b.customer_gid=a.customer_gid" &
                        " left join smr_trn_tsalesorder f on f.salesorder_gid=a.invoice_reference" &
                        " left join smr_trn_treceivequotation g on g.quotation_gid = f.quotation_gid "
        msSQL &= " where a.invoice_gid='" & Request.QueryString("invoicegid").ToString & "' "
        'Dim myConnection As New OdbcConnection
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


        msSQL = " select e.invoice_gid,a.qty_invoice ,a.product_price,a.discount_percentage,a.discount_amount as discount_amount," &
                    " a.tax_percentage,a.tax_amount,a.tax_percentage2,a.tax_amount2,a.tax_percentage3,a.tax_amount3," &
                    " a.tax_name,a.tax_name2,a.tax_name3,a.display_field,b.product_code,a.product_name,c.productgroup_name," &
                    " case when (a.tax_name='--No Tax--' or a.tax_name='NoTax') then 'No Tax'" &
                    " when (a.tax_name2='--No Tax--' or a.tax_name2='NoTax') then concat(a.tax_name, ' : ', CAST(a.tax_amount AS CHAR))" &
                    " when (a.tax_name3='--No Tax--' or a.tax_name3='NoTax') then concat(a.tax_name, ' : ', CAST(a.tax_amount AS CHAR), '     ', a.tax_name2, ' : ', CAST(a.tax_amount2 AS CHAR))" &
                    " else concat(a.tax_name, ' : ', CAST(a.tax_amount AS CHAR), '     ', a.tax_name2, ' : ', CAST(a.tax_amount2 AS CHAR), '    ', a.tax_name3, ' : ', CAST(a.tax_amount3 AS CHAR)) end as all_taxes," &
                    " a.productuom_name, b.productgroup_gid" &
                    " from rbl_trn_tproformainvoicedtl a" &
                    " left join rbl_trn_tso2proformainvoice h on h.invoice_gid=a.invoice_gid" &
                    " left join rbl_trn_tproformainvoice e on e.invoice_gid=a.invoice_gid" &
                    " left join pmr_mst_tproduct b on b.product_gid=a.product_gid" &
                    " left join pmr_mst_tproductgroup c on c.productgroup_gid=b.productgroup_gid" &
                    " left join pmr_mst_tproductuom d on d.productuom_gid=a.uom_gid" &
                    " where a.invoice_gid='" & Request.QueryString("invoicegid").ToString & "'  group by a.invoicedtl_gid order by a.invoicedtl_gid asc"
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable2")


        msSQL = " SELECT  tax_name AS thirdtax_amount, SUM(tax_amount) AS total_tax_amount " &
                        " FROM ( " &
                        " SELECT tax_name, tax_amount " &
                        " FROM rbl_trn_tproformainvoicedtl  " &
                        " WHERE invoice_gid  = '" & Request.QueryString("invoicegid").ToString & "'" &
                        " And Not (tax_name Like '%No%' AND tax_amount = 0) " &
                        " UNION ALL " &
                        " SELECT tax_name2 AS thirdtax_amount, tax_amount2 AS total_tax_amount " &
                        "  FROM rbl_trn_tproformainvoicedtl  " &
                        " WHERE invoice_gid  ='" & Request.QueryString("invoicegid").ToString & "'" &
                        " And Not (tax_name2 LIKE '%No%' AND tax_amount2 = 0) " &
                        " UNION ALL " &
                        " SELECT tax_name3 AS thirdtax_amount, tax_amount3 AS total_tax_amount " &
                        " FROM rbl_trn_tproformainvoicedtl  " &
                        " WHERE invoice_gid  = '" & Request.QueryString("invoicegid").ToString & "'" &
                        " AND NOT (tax_name3 LIKE '%No%' AND tax_amount3 = 0) " &
                        ") AS subquery " &
                        " GROUP BY tax_name"

        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable3")


        msSQL = " select a.branch_name,a.address1,a.city,a.state,a.gst_no, " &
                   " a.postal_code,a.contact_number,a.email,a.tin_number,a.cst_number,a.st_number from hrm_mst_tbranch a" &
                   " left join hrm_mst_temployee b on a.branch_gid=b.branch_gid" &
                   " where b.employee_gid='" & Session("employee_gid") & "'"

        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable4")


        msSQL = " select a.emp_sign from hrm_mst_temployee a " &
                       " left join rbl_trn_tproformainvoice b on b.user_gid = a.user_gid " &
                       " where b.invoice_gid = '" & Request.QueryString("invoice_gid") & "'"

        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable5")


        msSQL = " select a.authorized_sign as authoriser_sign from hrm_mst_tbranch a" &
                    " left join hrm_mst_temployee b on a.branch_gid=b.branch_gid" &
                    " where b.employee_gid='" & Session("employee_gid") & "'"

        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable6")
        Dim oRpt As New ReportDocument
        oRpt.Load(Server.MapPath("rbl_crp_finalsalesproformainvoicereport.rpt"))
        oRpt.SetDataSource(myDS)
        Dim oStream As System.IO.Stream
        oStream = oRpt.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)
        With HttpContext.Current.Response
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/pdf"
            .AddHeader("Content-Disposition", "inline; filename=" & "proformainvoice.pdf")
            Dim b(oStream.Length) As Byte
            oStream.Read(b, 0, CInt(oStream.Length))
            .BinaryWrite(b)

            .Flush()
            .SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End With
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
