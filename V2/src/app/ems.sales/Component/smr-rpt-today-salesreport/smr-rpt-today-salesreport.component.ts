import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-smr-rpt-today-salesreport',
  templateUrl: './smr-rpt-today-salesreport.component.html',
  styleUrls: ['./smr-rpt-today-salesreport.component.scss']
})
export class SmrRptTodaySalesreportComponent {
  GetTodaySalesReport_List:any;
  salesorder_list :any;
  MyenquiryCount_list :any;
  GetDaySalesReportCount_List :any;
  GetMonthwiseOrderReport_List :any
  tax4_list : any;
  responsedata: any;
  data: any; 
  directorder_date :any 
  today_total_so:any;
  today_total_do:any;
  today_total_invoice:any;
  today_total_payment:any;
  today_invoice_amount:any;
  today_payment_amount:any;
  today_outstanding_amount:any;

  yest_total_so:any;
  yest_total_do:any;
  yest_total_invoice:any;
  yest_total_payment:any;
  yest_invoice_amount:any;
  yest_payment_amount:any;
  yest_outstanding_amount:any;

  cw_total_so:any;
  cw_total_do:any;
  cw_total_invoice:any;
  cw_total_payment:any;
  cw_invoice_amount:any;
  cw_payment_amount:any;
  cw_outstanding_amount:any;

  lw_total_so: any;
  lw_total_do:any;
  lw_total_invoice:any;
  lw_total_payment:any;
  lw_invoice_amount:any;
  lw_payment_amount:any;
  lw_outstanding_amount:any;

  cm_total_so:any;
  cm_total_do:any;
  cm_total_invoice:any;
  cm_total_payment:any;
  cm_invoice_amount:any;
  cm_payment_amount:any;
  cm_outstanding_amount:any;

  lm_total_so: any;
  lm_total_do:any;
  lm_total_invoice:any;
  lm_total_payment:any;
  lm_invoice_amount:any;
  lm_payment_amount:any;
  lm_outstanding_amount:any;

  cy_total_so:any;
  cy_total_do:any;
  cy_total_invoice:any;
  cy_total_payment:any;
  cy_invoice_amount:any;
  cy_payment_amount:any;
  cy_outstanding_amount:any;

  ly_total_so: any;
  ly_total_do:any;
  ly_total_invoice:any;
  ly_total_payment:any;
  ly_invoice_amount:any;
  ly_payment_amount:any;
  ly_outstanding_amount:any;
  parameterValue: any;
  constructor(private formBuilder: FormBuilder,public route:ActivatedRoute,public service :SocketService,private router:Router,private ToastrService: ToastrService) {
  }
  
  ngOnInit(): void { 
    this.GetTodaySalesReport();
    this.GetDaySummary ();
    this.GetWeekSummary();
    this.GetMonthSummary();
    this.GetYearSummary();
  }
  GetYearSummary()
  {
    var url = 'SmrRptTodaysSalesReport/GetYearSalesReportCount'
    this.service.get(url).subscribe((result: any) => {
      console.log(result);
      this.cy_total_so = result.cy_total_so;
      this.cy_total_do = result.cy_total_do;
      this.cy_total_invoice = result.cy_total_invoice;
      this.cy_total_payment = result.cy_total_payment;
      this.cy_invoice_amount = result.cy_invoice_amount;
      this.cy_payment_amount = result.cy_payment_amount;
      this.cy_outstanding_amount = result.cy_outstanding_amount; 

      this.ly_total_so = result.ly_total_so;
      this.ly_total_do = result.ly_total_do;
      this.ly_total_invoice = result.ly_total_invoice;
      this.ly_total_payment = result.ly_total_payment;
      this.ly_invoice_amount = result.ly_invoice_amount;
      this.ly_payment_amount = result.ly_payment_amount;
      this.ly_outstanding_amount = result.ly_outstanding_amount; 
    })
  }
GetMonthSummary()
{
  var url = 'SmrRptTodaysSalesReport/GetMonthSalesReportCount'
  this.service.get(url).subscribe((result: any) => {
    console.log(result);
    this.cm_total_so = result.cm_total_so;
    this.cm_total_do = result.cm_total_do;
    this.cm_total_invoice = result.cm_total_invoice;
    this.cm_total_payment = result.cm_total_payment;
    this.cm_invoice_amount = result.cm_invoice_amount;
    this.cm_payment_amount = result.cm_payment_amount;
    this.cm_outstanding_amount = result.cm_outstanding_amount;
  
    this.lm_total_so = result.lm_total_so;
    this.lm_total_do = result.lm_total_do;
    this.lm_total_invoice = result.lm_total_invoice;
    this.lm_total_payment = result.lm_total_payment;
    this.lm_invoice_amount = result.lm_invoice_amount;
    this.lm_payment_amount = result.lm_payment_amount;
    this.lm_outstanding_amount = result.lm_outstanding_amount;
    
  })
}
  GetWeekSummary()
  {
    var url = 'SmrRptTodaysSalesReport/GetWeekSalesReportCount'
    this.service.get(url).subscribe((result: any) => {
      console.log(result);
      this.cw_total_so = result.cw_total_so;
      this.cw_total_do = result.cw_total_do;
      
      this.cw_total_invoice = result.cw_total_invoice;
      this.cw_total_payment = result.cw_total_payment;
      this.cw_invoice_amount = result.cw_invoice_amount;
      this.cw_payment_amount = result.cw_payment_amount;
      this.cw_outstanding_amount = result.cw_outstanding_amount;
     
      this.lw_total_so = result.lw_total_so;
      this.lw_total_do = result.lw_total_do;
      this.lw_total_invoice = result.lw_total_invoice;
      this.lw_total_payment = result.lw_total_payment;
      this.lw_invoice_amount = result.lw_invoice_amount;
      this.lw_payment_amount = result.lw_payment_amount;
      this.lw_outstanding_amount = result.lw_outstanding_amount;
      
    })
  }
  GetDaySummary()

 {
 
  var url = 'SmrRptTodaysSalesReport/GetDaySalesReportCount'
  this.service.get(url).subscribe((result: any) => {
    console.log(result);
    this.today_total_so = result.today_total_so;
    this.today_total_do = result.today_total_do;
    this.today_total_invoice = result.today_total_invoice;
    this.today_total_payment = result.today_total_payment;
    this.today_invoice_amount = result.today_invoice_amount;
    this.directorder_date =result.directorder_date;
    this.today_payment_amount = result.today_payment_amount;
    this.today_outstanding_amount = result.today_outstanding_amount;
  
    this.yest_total_so = result.yest_total_so;
    this.yest_total_do = result.yest_total_do;
    this.yest_total_invoice = result.yest_total_invoice;
    this.yest_total_payment = result.yest_total_payment;
    this.yest_invoice_amount = result.yest_invoice_amount;
    this.yest_payment_amount = result.yest_payment_amount;
    this.yest_outstanding_amount = result.yest_outstanding_amount;
    
  })
 
  

  
}
GetTodaySalesReport()

 {
  debugger
  var url = 'SmrRptTodaysSalesReport/GetTodaySalesReport'
  this.service.get(url).subscribe((result: any) => {
    $('#GetTodaySalesReport_List').DataTable().destroy();
    this.responsedata = result;
    this.GetTodaySalesReport_List = this.responsedata.GetTodaySalesReport_List;
    setTimeout(() => {
      $('#GetTodaySalesReport_List');
    }, 1);


  })

  
  
}

  openModal1(){};
}
