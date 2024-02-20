Imports System
Imports System.Web.UI
    Imports System.Data
    Imports System.Data.Odbc
    Imports System.IO
    Imports CrystalDecisions.CrystalReports.Engine
    Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports CrystalDecisions.Shared
Imports Microsoft.VisualBasic.CompilerServices

Public Class pmr_trn_purchaseordersummary
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



        Dim authDBInstance As New authdbconn()
        authDBInstance.openconn_load()
        Dim myConnection As OdbcConnection = authdbconn.gs_ConnDB

        If myConnection.State <> ConnectionState.Open Then
            myConnection.Open()
        End If
        msSQL = " select a.purchaseorder_gid,k.currency_symbol,a.purchaseorder_remarks, a.purchaserequisition_gid,a.currency_code,CONCAT(cast(a.payment_days as char(20)),' Days') as payment_day ,a.tax_amount,a.ship_via,a.freight_terms,a.delivery_location,a.payment_terms, " &
                         "  DATE_FORMAT(a.purchaseorder_date, '%d-%m-%Y') as purchaseorder_date ,n.tax_name, h.gst_no as companygst_no,date_format(Date_add(purchaseorder_date,Interval a.delivery_days day),'%d-%m-%y') as ExpDate," &
                         " a.vendor_address, a.vendor_contact_person, a.created_by as user_gid, " &
                         " a.purchaseorder_reference,format(a.total_amount,2) as total_amount , " &
                         " a.vendor_faxnumber as fax, a.vendor_contactnumber as contact_telephonenumber," &
                         " a.termsandconditions,a.purchaseorder_reference,b.pan_number, " &
                         " CASE when a.quote_referenceno = '--Select--' then '' " &
                         " else a.quote_referenceno end as 'quotation_ref', " &
                         " b.vendor_companyname,a.vendor_emailid as email_id," &
                         " format(a.freightcharges+a.freighttax_amount,2) as freight_charges,format(a.buybackorscrap,2) as buyback, " &
                         " format(a.packing_charges,2)as packing_charges ,format(a.insurance_charges,2)as insurance_charges," &
                         "(a.total_amount- a.discount_amount + a.tax_amount) as total_amount1, " &
                         " concat(c.user_firstname,' - ',e.department_name) as user_firstname,p.salesperson_gid,q.slno,(r.user_firstname)as salesperson_name,a.roundoff, " &
                         " d.employee_emailid as user_email, d.employee_phoneno as user_phone , f.city , f.state , f.postal_code , g.country_name, " &
                         " h.branch_name, h.branch_header, b.ifsc_code as ecc_no, b.rtgs_code as tngst_no, b.cst_number as cst, " &
                         " b.tin_number as tin_no, (h.branch_location) as branch_footer,a.discount_percentage,a.discount_amount as discount_amount1,a.shipping_address, " &
                         " i.costcenter_code as cost_center,i.costcenter_name,j.quotation_referenceno,date_format(l.purchaserequisition_date,'%d-%m-%Y')as purchaserequisition_date,b.gst_number from pmr_trn_tpurchaseorder a " &
                         " left join pmr_mst_tcostcenter i on i.costcenter_gid = a.costcenter_gid " &
                         " left join acp_mst_tvendor b on a.vendor_gid = b.vendor_gid " &
                         " left join adm_mst_tuser c on c.user_gid = a.created_by " &
                         " left join hrm_mst_temployee d on d.user_gid = c.user_gid " &
                         " left join hrm_mst_tdepartment e on e.department_gid = d.department_gid " &
                         " left join adm_mst_taddress f on  f.address_gid = b.address_gid " &
                         " left join adm_mst_tcountry g on g.country_gid = f.country_gid " &
                         " left join hrm_mst_tbranch h on a.branch_gid = h.branch_gid " &
                         " left join crm_trn_tcurrencyexchange k on g.country_gid=k.country_gid " &
                         " left join pmr_trn_tquotationvendordetails j on a.quotation_gid=j.quotation_gid" &
                         " left join pmr_trn_tpurchaserequisition l on a.purchaserequisition_gid=l.purchaserequisition_gid" &
                         " left join acp_mst_ttax n on a.tax_gid = n.tax_gid " &
                         " left join pmr_trn_tsalesorder2purchaseorder p on a.purchaseorder_gid=p.purchaseorder_gid " &
                         " left join smr_trn_tsalesorderdtl q on p.salesorder_gid=q.salesorder_gid " &
                         " left join adm_mst_tuser r on p.salesperson_gid=r.user_gid " &
         " where a.purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' group by a.purchaseorder_gid"
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

        msSQL = " select  a.product_gid,b.hsn_number,a.purchaseorderdtl_gid,CONCAT('£ ', FORMAT(a.product_price, 2)) AS product_price,CONCAT('£ ', ROUND(a.product_price, 2)) AS product_price_round, a.qty_ordered,  a.discount_percentage, " &
                        " CONCAT('£ ',format(a.discount_amount,2)) AS discount_amount,a.tax_name,a.tax_amount,a.tax_name2,a.tax_amount2,a.tax_name3,a.tax_amount3,a.tax_percentage,a.tax_percentage2,a.tax_percentage3,e.addon_amount,a.needby_date as ExpDate , " &
                        " a.product_remarks,  (a.qty_ordered * a.product_price) as created_by,a.needby_date,  " &
                        " CONCAT('£ ', FORMAT((((a.qty_ordered * a.product_price) - a.discount_amount) + a.tax_amount + a.tax_amount2 + a.tax_amount3), 2)) AS product_total ,  SUBSTRING_INDEX(a.product_code, '/', 1) as Lproduct_code, SUBSTRING_INDEX(a.product_code, '/', -1) as Rproduct_code, a.product_code, " &
                        " a.product_name,  c.productgroup_name, a.productuom_name, " &
                        " case when a.display_field_name is null then a.product_name when a.display_field_name is not null then a.display_field_name end as display_field_name, " &
                        " (sum((a.qty_ordered * a.product_price) - a.discount_amount) + a.tax_amount + a.tax_amount2 + a.tax_amount3) as sum_product_total,format(e.discount_amount,2) as dis_amount," &
                        " format(e.total_amount,2) as grand_total," &
                        " d.productuom_name,  e.total_amount, " &
                        " CONCAT(a.tax_name, ' : £', a.tax_amount) AS DataColumn2,e.payment_terms as DataColumn3 " &
                        " from pmr_trn_tpurchaseorderdtl a " &
                        " left join pmr_trn_tpurchaseorder e on e.purchaseorder_gid = a.purchaseorder_gid " &
                        " left join pmr_mst_tproduct b on a.product_gid = b.product_gid " &
                        " left join pmr_mst_tproductgroup c on b.productgroup_gid = c.productgroup_gid " &
                        " Left join pmr_mst_tproductuom d on d.productuom_gid = a.uom_gid " &
                        " where a.purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' group by a.purchaseorder_gid"
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable2")



        msSQL = " select a.user_gid,a.user_firstname,a.user_lastname " &
                                        " from adm_mst_tuser a " &
                                        " where user_gid = '" & Session("user_gid") & "'"
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable3")



        msSQL = "SELECT e.purchaseorder_date, e.delivery_days, " &
                        "FORMAT(SUM((a.qty_ordered * a.product_price) - a.discount_amount),2) AS total_amount, " &
                        "FORMAT(e.discount_amount,2) AS dis_amount, " &
                        "concat('£ ',FORMAT(e.total_amount,2)) AS grand_total, " &
                        "CONCAT(UPPER(ConvertAmountinWords(e.total_amount)), ' POUNDS ONLY') AS DataColumn5, " &
                        "purchaseorder_reference, e.currency_code " &
                        "FROM pmr_trn_tpurchaseorderdtl a " &
                        "LEFT JOIN pmr_trn_tpurchaseorder e ON e.purchaseorder_gid = a.purchaseorder_gid " &
                        "LEFT JOIN pmr_mst_tproduct b ON a.product_gid = b.product_gid " &
                        "LEFT JOIN pmr_mst_tproductgroup c ON b.productgroup_gid = c.productgroup_gid " &
                        "LEFT JOIN pmr_mst_tproductuom d ON d.productuom_gid = a.uom_gid " &
                        "WHERE a.purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' " &
                        "GROUP BY a.purchaseorder_gid"

        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable4")







        msSQL = " select a.address1,a.branch_name,a.city,a.gst_no,a.state,a.postal_code,a.contact_number,a.email,a.email_id,a.branch_gid,a.branch_logo,a.tin_number,a.cst_number from hrm_mst_tbranch a " &
                            " left join pmr_trn_tpurchaseorder b on b.branch_gid = a.branch_gid " &
  " where b.purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "'"
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable5")

        msSQL = " select a.purchaseorderdtl_gid, a.purchaseorder_gid, a.product_gid, a.uom_gid, a.product_price, a.qty_ordered, " &
                             " a.qty_received, a.discount_percentage, a.discount_amount, a.tax_percentage, format(sum(a.tax_amount),2) as tax_amount, " &
                             " a.product_remarks, a.excise_percentage, a.excise_amount, a.qty_grnadjusted, a.qty_invoice, a.display_field_name, a.tax_percentage2, " &
                             " format(sum(a.tax_amount2),2) as tax_amount2, a.tax_name, a.tax_name2, a.tax_name3, format(sum(a.tax_amount3),2) as tax_amount3, " &
                             " a.tax_percentage3, a.qty_poadjusted, a.producttype_gid, format(sum((a.tax_amount + a.tax_amount2 + a.tax_amount3)),2) as total_tax_amount " &
                             " from pmr_trn_tpurchaseorderdtl a " &
                             " left join pmr_trn_tpurchaseorder b on b.purchaseorder_gid = a.purchaseorder_gid " &
                             " where a.purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' group by a.purchaseorder_gid"
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable6")


        msSQL = "  select a.employee_sign as employee_sign,a.employee_sign,'-' as emp_sign,'-' as approved_by from hrm_mst_temployee a " &
                       " left join pmr_trn_tpurchaseorder b on b.authorized_by = a.employee_gid" &
                       " where b.purchaseorder_gid = '" & Request.QueryString("purchaseorder_gid").ToString & "'"
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable7")

        msSQL = "  select a.employee_sign as created_by,a.employee_sign,'-' as emp_sign, '-' as created_by from pmr_trn_tpurchaseorder b  " &
                           " left join hrm_mst_temployee a on b.prepared_by= a.employee_gid" &
                           " where b.purchaseorder_gid = '" & Request.QueryString("purchaseorder_gid").ToString & "'"

        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        'myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable8")



        msSQL = "  select authorised_sign,authoriser_sign from adm_mst_tcompany "
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        'myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable9")

        msSQL = " select b.currency_code from pmr_trn_tpurchaseorder a " &
                         " left join crm_trn_tcurrencyexchange b on b.currency_code=a.currency_code " &
                         " where a.purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' "
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable10")

        msSQL = " select ( select a.parameter_value  from adm_mst_tconfiguration a where a.module_name = 'purchase' and a.parameter_code='PMRFH' and a.parameter_name='po_footer_hide') as parameter_value, " &
                                " ( select a.parameter_value  from adm_mst_tconfiguration a where a.module_name = 'purchase' and a.parameter_code='PMRRF' and a.parameter_name='po_amt_roundoff') as round_off "
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        'This is our DataSet created at Design Time 
        MyDA.Fill(myDS, "DataTable11")


        msSQL = "select(select ifnull(sum(tax_amount),0.00)as SGST0 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and" &
                        "(tax_name = 'SGST0' OR tax_name2 = 'SGST0' OR tax_name3 = 'SGST0'))as SGST0," &
                        "(select ifnull(sum(tax_amount),0.00)as SGST25 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and " &
                        "(tax_name = 'SGST25' OR tax_name2 = 'SGST25' OR tax_name3 = 'SGST25'))as SGST2," &
                        "(select ifnull(sum(tax_amount),0.00)as SGST6 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'SGST6' OR tax_name2 = 'SGST6' OR tax_name3 = 'SGST6'))as SGST6," &
                        "(select ifnull(sum(tax_amount),0.00)as SGST9 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'SGST9' OR tax_name2 = 'SGST9' OR tax_name3 = 'SGST9') )as SGST9," &
                        "(select ifnull(sum(tax_amount),0.00)as SGST14 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'SGST14' OR tax_name2 = 'SGST14' OR tax_name3 = 'SGST14'))as SGST14 ," &
                        "(select ifnull(sum(tax_amount),0.00)as CGST0 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'CGST0' OR tax_name2 = 'CGST0' OR tax_name3 = 'CGST0'))as CGST0," &
                        "(select ifnull(sum(tax_amount),0.00)as CGST25 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'CGST25' OR tax_name2 = 'CGST25' OR tax_name3 = 'CGST25'))as CGST2, " &
                        "(select ifnull(sum(tax_amount),0.00)as CGST6 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'CGST6' OR tax_name2 = 'CGST6' OR tax_name3 = 'CGST6'))as CGST6," &
                        "(select ifnull(sum(tax_amount),0.00)as CGST9 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'CGST9' OR tax_name2 = 'CGST9' OR tax_name3 = 'CGST9') )as CGST9, " &
                        "(select ifnull(sum(tax_amount),0.00)as CGST14 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'CGST14' OR tax_name2 = 'CGST14' OR tax_name3 = 'CGST14'))as CGST14," &
                        "(select ifnull(sum(tax_amount),0.00)as IGST0 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'IGST0' OR tax_name2 = 'IGST0' OR tax_name3 = 'IGST0'))as IGST0," &
                        "(select ifnull(sum(tax_amount),0.00)as IGST5 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'IGST5' OR tax_name2 = 'IGST5' OR tax_name3 = 'IGST5'))as IGST5," &
                        "(select ifnull(sum(tax_amount),0.00)as IGST12 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'IGST12' OR tax_name2 = 'IGST12' OR tax_name3 = 'IGST12'))as IGST12," &
                        "(select ifnull(sum(tax_amount),0.00)as IGST18 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'IGST18' OR tax_name2 = 'IGST18' OR tax_name3 = 'IGST18') )as IGST18," &
                        "(select ifnull(sum(tax_amount),0.00)as IGST28 from pmr_trn_tpurchaseorderdtl where  purchaseorder_gid='" & Request.QueryString("purchaseorder_gid").ToString & "' and   " &
                        "(tax_name = 'IGST28' OR tax_name2 = 'IGST28' OR tax_name3 = 'IGST28'))as IGST28"


        MyCommand.Connection = myConnection
        MyCommand.CommandText = msSQL
        MyCommand.CommandType = Data.CommandType.Text
        MyDA.SelectCommand = MyCommand
        myDS.EnforceConstraints = False
        MyDA.Fill(myDS, "DataTable12")


        Dim oRpt As New ReportDocument
        oRpt.Load(Server.MapPath("pmr_rpt_purchaseorder.rpt"))
        oRpt.SetDataSource(myDS)
        'Dim oStream As System.IO.Stream
        oRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, " Purchase Order Receipt")
        'Close and dispose of the report document to release resources
        oRpt.Close()
        oRpt.Dispose()



        'oRpt.Load(Server.MapPath("pmr_rpt_purchaseorder.rpt"))
        'oRpt.SetDataSource(myDS)

        'Dim oRpt As New ReportDocument
        'oRpt.Load(Server.MapPath("pmr_rpt_purchaseorder.rpt"))
        'oRpt.SetDataSource(myDS)
        ''Dim oStream As System.IO.Stream
        'oRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, " Purchase Order Receipt")
        ''Close and dispose of the report document to release resources
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


        End Try
        Return lobjDataTable
    End Function

End Class