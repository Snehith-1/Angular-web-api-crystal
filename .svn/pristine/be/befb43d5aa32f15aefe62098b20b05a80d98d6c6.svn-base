Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared

Public Class trv_crp_customerinvoice3
    Inherits System.Web.UI.Page
    Dim msSQL As String
    Dim companycode As String
    Public gs_ConnDB As OdbcConnection


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim myConnection As New OdbcConnection
        Dim authDBInstance As New authdbconn()
        authDBInstance.openconn_load()
        Dim myConnection As OdbcConnection = authdbconn.gs_ConnDB

        If myConnection.State <> ConnectionState.Open Then
            myConnection.Open()
        End If
        Dim authdb As New authdbconn
        'companycode = authdb.connectionstring("").ToString
        msSQL = "select a.invoice_gid,a.invoice_date,a.invoice_amount,a.irn ,a.complaint_gid," &
                            " a.invoice_refno, a.freightcharges_amount, a.additionalcharges_amount, " &
                            " a.discount_amount,a.total_amount as total_amount, a.advance_amount, concat('M/s.',' ',a.customer_name) as customer_name, c.gst_number as customergst_no," &
                            " b.customer_id, a.tin_number, a.cst_number, " &
                            " a.customer_address,a.customer_contactperson as customercontact_name, a.customer_email as email, " &
                            " a.customer_contactnumber  as mobile, " &
                            " a.invoice_reference as directorder_gid, a.termsandconditions,a.currency_code as designation,a.currency_code " &
                            " from rbl_trn_tinvoice a " &
                            " left join crm_mst_tcustomer b on b.customer_gid=a.customer_gid " &
                            " left join crm_mst_tcustomercontact c on c.customer_gid=b.customer_gid " &
                            " where a.invoice_gid='" & Request.QueryString("invoicegid").ToString & "' group by a.invoice_gid"
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



        msSQL = " select @rn:=@rn+1 as row_no,invoice_gid, qty_invoice, (vendor_price-discount_amount) as product_price, discount_percentage, vendor_price, format(qty_invoice*(vendor_price-discount_amount),2) as net_price," &
                             " discount_amount,product_code, tax_percentage, tax_amount, tax_percentage2, tax_amount2, tax_percentage3, tax_amount3,hsn_number, " &
                              " total, tax_name, tax_name2, tax_name3, display_field,progressive_percent, SUBSTRING_INDEX(product_code, '/', 1) as Lproduct_code, SUBSTRING_INDEX(product_code, '/', -1) as Rproduct_code, product_name, productgroup_name,customerproduct_code, " &
                              " all_taxes,taxable_amount, nontax_amount,SGST,SGSTP,CGST,CGSTP,IGSTP,IGST,productuom_name from (select a.invoice_gid, a.qty_invoice ,a.vendor_price,a.product_price,a.discount_percentage,a.discount_amount,b.hsn_number," &
                              " a.tax_percentage,a.tax_amount,a.tax_percentage2,a.tax_amount2,a.tax_percentage3,a.tax_amount3, a.product_total as total, " &
                              " a.tax_name,a.tax_name2,a.tax_name3,a.display_field,a.product_code,a.product_name,c.productgroup_name,a.progressive_percent,a.customerproduct_code, " &
                              " case when (a.tax_name='--No Tax--' or a.tax_name='NoTax') then 'No Tax'" &
                              " when (a.tax_name2='--No Tax--' or a.tax_name2='NoTax') then concat(a.tax_name, ' : ', CAST(a.tax_amount AS CHAR))" &
                              " when (a.tax_name3='--No Tax--' or a.tax_name3='NoTax') then concat(a.tax_name, ' : ', CAST(a.tax_amount AS CHAR),'<br>', a.tax_name2, ' : ', CAST(a.tax_amount2 AS CHAR))" &
                              " else concat(a.tax_name, ' : ', CAST(a.tax_amount AS CHAR),'<br>', a.tax_name2, ' : ', CAST(a.tax_amount2 AS CHAR),'<br>', a.tax_name3, ' : ', CAST(a.tax_amount3 AS CHAR)) end as all_taxes," &
                              " if (a.tax_amount = 0 and a.tax_amount2 = 0 and a.tax_amount3 = 0,a.product_price,0.00)as nontax_amount," &
                              " if (a.tax_amount <> 0 or a.tax_amount2 <> 0 or a.tax_amount3 <> 0,a.product_price,0.00)as taxable_amount," &
                              " if (a.tax_name like '%SGST%' OR a.tax_name2 like '%SGST%' OR a.tax_name3 like '%SGST%',a.tax_percentage2,0.00) as SGSTP," &
                              " if (a.tax_name like '%SGST%' OR a.tax_name2 like '%SGST%' OR a.tax_name3 like '%SGST%',a.tax_amount2,0.00) as SGST," &
                              " if (a.tax_name like '%CGST%' OR a.tax_name2 like '%CGST%' OR a.tax_name3 like '%CGST%',a.tax_percentage,0.00) as CGSTP," &
                              " if (a.tax_name like '%CGST%' OR a.tax_name2 like '%CGST%' OR a.tax_name3 like '%CGST%',a.tax_amount,0.00) as CGST," &
                              " if (a.tax_name like '%IGST%' OR a.tax_name2 like '%IGST%' OR a.tax_name3 like '%IGST%',a.tax_percentage3,0.00) as IGSTP," &
                              " IF (a.tax_name like '%IGST%' OR a.tax_name2 like '%IGST%' OR a.tax_name3 like '%IGST%',a.tax_amount3,0.00) as IGST," &
                              " a.productuom_name" &
                              " from rbl_trn_tinvoicedtl a" &
                              " left join rbl_trn_tinvoice e on e.invoice_gid=a.invoice_gid " &
                              " left join pmr_mst_tproduct b on b.product_gid=a.product_gid " &
                              " left join pmr_mst_tproductgroup c on c.productgroup_gid=b.productgroup_gid " &
                              " left join pmr_mst_tproductuom d on d.productuom_gid=a.uom_gid " &
                              " left join smr_trn_tsalesorderdtl m on m.salesorderdtl_gid=a.salesorderdtl_gid " &
                              " where a.invoice_gid='" & Request.QueryString("invoicegid").ToString & "'  group by a.invoicedtl_gid order by a.invoicedtl_gid asc ) " &
                              " x,(SELECT @rn:=0) t2; "

        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable2")


        msSQL = " select a.invoice_gid, case when (a.tax_name='--No Tax--' or a.tax_name='NoTax')  then ' ' when (a.tax_name<>'--No Tax--' or a.tax_name<>'NoTax') then a.tax_name end as firsttax_name," &
                        " (a.tax_amount)as firsttax_amount,case when (a.tax_name2='--No Tax--' or a.tax_name2='NoTax') then ' ' when (a.tax_name2<>'--No Tax--' or a.tax_name2<>'NoTax') then a.tax_name2 end as secondtax_name2," &
                        " (a.tax_amount2)as secondtax_amount,case when (a.tax_name3='--No Tax--' or a.tax_name3='NoTax') then ' ' when (a.tax_name3<>'--No Tax--' or a.tax_name3<>'NoTax') then a.tax_name3 end as thirdtax_name3," &
                        " case when format(sum(a.tax_amount),2)=0.00 then ' ' when format(sum(a.tax_amount),2)<>0.00 then format(sum(a.tax_amount),2) end as sum_tax1 ," &
                        " case when format(sum(a.tax_amount2),2)=0.00 then ' ' when format(sum(a.tax_amount2),2)<>0.00 then format(sum(a.tax_amount2),2) end as sum_tax2," &
                        " case when format(sum(a.tax_amount3),2)=0.00 then ' ' when format(sum(a.tax_amount3),2)<>0.00 then format(sum(a.tax_amount3),2) end as sum_tax3," &
                        " format(sum((a.tax_amount + a.tax_amount2 + a.tax_amount3)),2) as total_tax_amount,(a.tax_amount3)as thirdtax_amount,sum(a.product_price)as total_productprice" &
                        " from rbl_trn_tinvoicedtl a " &
                        " left join rbl_trn_tinvoice e on e.invoice_gid=a.invoice_gid " &
                        " left join pmr_mst_tproduct b on b.product_gid=a.product_gid " &
                        " left join pmr_mst_tproductgroup c on b.productgroup_gid=c.productgroup_gid " &
                        " left join pmr_mst_tproductuom d on d.productuom_gid=a.uom_gid " &
                        " where a.invoice_gid = '" & Request.QueryString("invoicegid").ToString & "' group by a.invoicedtl_gid order by a.invoicedtl_gid asc "
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable3")

        msSQL = " select a.branch_logo,a.branch_name,a.address1,a.city,a.state,a.postal_code,a.gst_no, " &
                            " a.gst_no as gst,a.st_number,a.contact_number,a.email,a.email_id,c.invoice_reference,a.tin_number,a.cst_number from " &
                            " hrm_mst_tbranch a " &
                            " left join smr_trn_tsalesorder b on b.branch_gid=a.branch_gid " &
                            " left join adm_mst_tcompany k on 1=1" &
                            " left join rbl_trn_tinvoice c on c.invoice_reference=b.salesorder_gid " &
                            " where c.invoice_gid='" & Request.QueryString("invoicegid").ToString & "'"
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable4")
        msSQL = " select a.emp_sign from hrm_mst_temployee a " &
                           " left join rbl_trn_tinvoice b on b.user_gid = a.user_gid " &
                           " where b.invoice_gid = '" & Request.QueryString("invoice_gid") & "'"

        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable5")
        msSQL = " select authorized_sign as authorised_sign from hrm_mst_tbranch a " &
                        " left join rbl_trn_tinvoice b on a.branch_gid=b.branch_gid where  " &
                        " b.invoice_gid='" & Request.QueryString("invoicegid").ToString & "'"
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable6")


        'Payment History DataTable
        msSQL = " select a.invoice_gid,a.amount,date_format(b.invoice_date,'%d-%m-%y') as invoice_date,date_format(a.payment_date,'%d-%m-%y') as payment_date,b.total_amount,c.mode_of_despatch from rbl_trn_tpayment a " &
                        " inner join rbl_trn_tinvoice b on a.invoice_gid=b.invoice_gid " &
                        " left join smr_trn_tdeliveryorder c on b.invoice_reference=c.salesorder_gid" &
                        " where b.invoice_gid='" & Request.QueryString("invoicegid").ToString & "' "
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable7")



        msSQL = " select a.invoice_gid,ifnull((select sum(x.tax_amount) from rbl_trn_vinvoicetax  x where tax_name like 'CGST%'" &
                        " and x.invoice_gid=a.invoice_gid),0.00) as cgst," &
                        " ifnull((select sum(x.tax_amount) from rbl_trn_vinvoicetax x where tax_name like 'SGST%'" &
                        " and x.invoice_gid=a.invoice_gid),0.00) as sgst," &
                        " ifnull((select sum(x.tax_amount) from rbl_trn_vinvoicetax x where tax_name like 'IGST%'" &
                        " and x.invoice_gid=a.invoice_gid),0.00) as igst, " &
                        " ifnull((select sum(x.tax_amount) from rbl_trn_vinvoicetax x " &
                        " where x.invoice_gid=a.invoice_gid),0.00) as total_amount" &
                       " from rbl_trn_tinvoice a where a.invoice_gid='" & Request.QueryString("invoicegid").ToString & "' "
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable8")


        msSQL = "select(select ifnull(sum(tax_amount),0.00)as SGST0 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and" &
                        "(tax_name = 'SGST0' OR tax_name2 = 'SGST0' OR tax_name3 = 'SGST0'))as SGST0," &
                        "(select ifnull(sum(tax_amount),0.00)as SGST25 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and " &
                        "(tax_name = 'SGST25' OR tax_name2 = 'SGST25' OR tax_name3 = 'SGST25'))as SGST2," &
                        "(select ifnull(sum(tax_amount),0.00)as SGST6 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'SGST6' OR tax_name2 = 'SGST6' OR tax_name3 = 'SGST6'))as SGST6," &
                        "(select ifnull(sum(tax_amount),0.00)as SGST9 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'SGST9' OR tax_name2 = 'SGST9' OR tax_name3 = 'SGST9') )as SGST9," &
                        "(select ifnull(sum(tax_amount),0.00)as SGST14 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'SGST14' OR tax_name2 = 'SGST14' OR tax_name3 = 'SGST14'))as SGST14 ," &
                        "(select ifnull(sum(tax_amount),0.00)as CGST0 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'CGST0' OR tax_name2 = 'CGST0' OR tax_name3 = 'CGST0'))as CGST0," &
                        "(select ifnull(sum(tax_amount),0.00)as CGST25 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'CGST25' OR tax_name2 = 'CGST25' OR tax_name3 = 'CGST25'))as CGST2, " &
                        "(select ifnull(sum(tax_amount),0.00)as CGST6 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'CGST6' OR tax_name2 = 'CGST6' OR tax_name3 = 'CGST6'))as CGST6," &
                        "(select ifnull(sum(tax_amount),0.00)as CGST9 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'CGST9' OR tax_name2 = 'CGST9' OR tax_name3 = 'CGST9') )as CGST9, " &
                        "(select ifnull(sum(tax_amount),0.00)as CGST14 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'CGST14' OR tax_name2 = 'CGST14' OR tax_name3 = 'CGST14'))as CGST14," &
                        "(select ifnull(sum(tax_amount),0.00)as IGST0 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'IGST0' OR tax_name2 = 'IGST0' OR tax_name3 = 'IGST0'))as IGST0," &
                        "(select ifnull(sum(tax_amount),0.00)as IGST5 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'IGST5' OR tax_name2 = 'IGST5' OR tax_name3 = 'IGST5'))as IGST5," &
                        "(select ifnull(sum(tax_amount),0.00)as IGST12 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'IGST12' OR tax_name2 = 'IGST12' OR tax_name3 = 'IGST12'))as IGST12," &
                        "(select ifnull(sum(tax_amount),0.00)as IGST18 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'IGST18' OR tax_name2 = 'IGST18' OR tax_name3 = 'IGST18') )as IGST18," &
                        "(select ifnull(sum(tax_amount),0.00)as IGST28 from rbl_trn_tinvoicedtl where  invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and   " &
                        "(tax_name = 'IGST28' OR tax_name2 = 'IGST28' OR tax_name3 = 'IGST28'))as IGST28"







        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable9")


        msSQL = "select sum(qty_invoice*product_price)nettotal from rbl_trn_tinvoicedtl where invoice_gid ='" & Request.QueryString("invoicegid").ToString & "'"
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable10")
        Dim dt3 As DataTable = New DataTable()
        Dim DataTable11 As DataTable = New DataTable()
        Dim image_path As String

        'msSQL = " select  (branch_logo_path) as company_logo_Path  from hrm_mst_tbranch where branch_gid = '" & Session("branch_gid") & "' "
        ''msSQL = " select  (branch_logo_path) as company_logo_Path  from hrm_mst_tbranch where branch_gid = '" & Session("branch_gid") & "' and  branch_logo_path is not null"
        ''dt3 = GetDatatable(msSQL)
        'DataTable11.Columns.Add("company_logo", Type.[GetType]("System.Byte[]"))
        'If dt3.Rows.Count > 0 Then
        '    For Each row As DataRow In dt3.Rows
        '        image_path = Server.MapPath(row("company_logo_Path"))
        '        image_path = image_path.Replace("/", "\\")

        '        '---Convert  Image Path to Byte
        '        Dim image As System.Drawing.Image = System.Drawing.Image.FromFile(image_path)

        '        Dim imageConverter As New System.Drawing.ImageConverter
        '        Dim imageByte As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())

        '        DataTable11.Rows.Add(imageByte)
        '    Next
        'End If
        'DataTable11.TableName = "DataTable11"
        'myDS.Tables.Add(DataTable11)


        'Dim dt4 As DataTable = New DataTable()
        'Dim DataTable13 As DataTable = New DataTable()
        'Dim sign_path As String

        ''msSQL = " select  (branch_logo_path) as company_logo_Path  from hrm_mst_tbranch where branch_gid = '" & Session("branch_gid") & "' "
        ''msSQL = " select  (authorized_sign_path) as authorized_sign  from hrm_mst_tbranch where branch_gid = '" & Session("branch_gid") & "' and  authorized_sign_path is not null"
        '''dt4 = GetDatatable(msSQL)
        'DataTable13.Columns.Add("authorized_sign", Type.[GetType]("System.Byte[]"))
        'If dt4.Rows.Count > 0 Then
        '    For Each row As DataRow In dt4.Rows
        '        sign_path = Server.MapPath(row("authorized_sign"))
        '        sign_path = sign_path.Replace("/", "\\")

        '        '---Convert  Image Path to Byte
        '        Dim image As System.Drawing.Image = System.Drawing.Image.FromFile(sign_path)
        '        Dim imageConverter As New System.Drawing.ImageConverter
        '        Dim imageByte As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())

        '        DataTable13.Rows.Add(imageByte)
        '    Next
        'End If
        'DataTable13.TableName = "DataTable13"
        'myDS.Tables.Add(DataTable13)
        'Dim dt14 As DataTable = New DataTable()
        'Dim DataTable14 As DataTable = New DataTable()
        'msSQL = "select qr_path from rbl_trn_tinvoice where invoice_gid='" & Request.QueryString("invoicegid").ToString & "' and qr_path is not null"
        'dt14 = GetDatatable(msSQL)
        'DataTable14.Columns.Add("qr_path", Type.[GetType]("System.Byte[]"))
        'If dt14.Rows.Count > 0 Then
        '    For Each row As DataRow In dt14.Rows
        '        image_path = Server.MapPath(row("qr_path"))
        '        image_path = image_path.Replace("/", "\\")

        '        '---Convert  Image Path to Byte
        '        Dim image As System.Drawing.Image = System.Drawing.Image.FromFile(image_path)

        '        Dim imageConverter As New System.Drawing.ImageConverter
        '        Dim imageByte As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())

        '        DataTable14.Rows.Add(imageByte)
        '    Next
        'End If
        'DataTable14.TableName = "DataTable14"
        'myDS.Tables.Add(DataTable14)

        msSQL = " select " &
                           " a.gst_no as gstno,a.branch_name as branchname from " &
                           " hrm_mst_tbranch a " &
                           " left join rbl_trn_tinvoice b on b.branch_gid=a.branch_gid " &
                           " where b.invoice_gid='" & Request.QueryString("invoicegid").ToString & "'"
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable12")



        Dim oRpt As New ReportDocument
        If Request.QueryString("companycode").ToString().ToLower() = "boba" Then

            oRpt.Load(Server.MapPath("rbl_crp_bobateainvoicereport.rpt"))

        ElseIf Request.QueryString("companycode").ToString().ToLower() = "bobatea" Then
            oRpt.Load(Server.MapPath("rbl_crp_bobateainvoicereport.rpt"))
        Else

            oRpt.Load(Server.MapPath("rbl_crp_invoicereport.rpt"))

        End If

        oRpt.SetDataSource(myDS)
        Dim oStream As System.IO.Stream
        oStream = oRpt.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)
        With HttpContext.Current.Response
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/pdf"
            .AddHeader("Content-Disposition", "inline; filename=" & "Invoice.pdf")
            Dim b(oStream.Length) As Byte
            oStream.Read(b, 0, CInt(oStream.Length))
            .BinaryWrite(b)

            .Flush()
            .SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End With
        'Response.Clear()
        'Context.Response.AddHeader("Content-Disposition", "Attachment;Filename=CustomerInvoice.pdf")
        'Response.Buffer = True
        'Response.ContentType = "application/pdf"
        'Response.BinaryWrite(oStream.ToArray())
        'oRpt.Close()
        'oRpt.Dispose()
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
