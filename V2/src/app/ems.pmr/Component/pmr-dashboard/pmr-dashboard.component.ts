import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import * as ApexCharts from 'apexcharts';
import { ApexOptions } from 'apexcharts';
import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart,
  ChartComponent 
} from "ng-apexcharts";
export type ChartOptions1 = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
  DashboardCount_List: any
};
export type ChartOptions4 = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
  DashboardCount_List: any
};

@Component({
  selector: 'app-pmr-dashboard',
  templateUrl: './pmr-dashboard.component.html',
  styleUrls: ['./pmr-dashboard.component.scss']
})

export class PmrDashboardComponent {
  chartOptions1: any = {};
  chartOptions!: ApexOptions; 
  chartOptions4: any = {};
 
  noleadstatus: any;
  response_data :any;

  GetOverallSalesOrderChart_List :any;

  DashboardQuotationAmt_List :any;

  noquotation :any;

  year : any;
  
  noquotation_status : any;

  show = true;
  emptyFlag: boolean=false;
  series_Value: any;
  labels_value: any;
  chartOptions2: any = {};
  GetOrderForLastSixMonths_List: any;
  GetOrderForLastSixMonths_List1: any;
  GetMonthSalesReportCount_list: any;
  GetDaySalesReportCount_List: any;
  GetWeekSalesReportCount_List: any;
  GetMonthSalesReportCount_List: any;
  GetSalesOrderCount_List: any[] = [];
  total_so: any;
  responsedata: any;
 // chartOptions: any;
  parameter: any;
  parameterValue: any;
  Date: string;
  getData: any;
  total_payment: any;
  todaytask_count: any;
  pending_So: any;
  approved_so: any;
  advanced_paid: any;
  rejected_so: any;
  invoice_raised: any
  totalinvoice: any;
  delivery_done_partial: any;
  delivery_completed: any;
  approvalpendinginnvoice: any;
  payment_pending: any;
  approval_pending: any;
  quotation_canceled: any;
  quotation_status: any;
  total_quotation: any;
  completed_quotation: any;
  total_quotation1: number = 0;
  quotation_canceled1: number = 0;
  so_amended: any;
  quotation_amended: any;
  work_in_progress: any;
  payment_don_partial: any;
  compelted_payment: any;
  today_total_so: any;
  today_total_do: any;
  today_total_invoice: any;
  today_total_payment: any;
  today_invoice_amount: any;
  today_payment_amount: any;
  today_outstanding_amount: any;
  yest_total_so: any;
  yest_total_do: any;
  yest_total_invoice: any;
  yest_total_payment: any;
  yest_invoice_amount: any;
  yest_payment_amount: any;
  yest_outstanding_amount: any;
  cw_total_so: any;
  cw_total_do: any;
  cw_total_invoice: any;
  cw_total_payment: any;
  cw_invoice_amount: any;
  cw_payment_amount: any;
  cw_outstanding_amount: any;
  lw_total_so: any;
  lw_total_do: any;
  lw_total_invoice: any;
  lw_total_payment: any;
  lw_invoice_amount: any;
  lw_payment_amount: any;
  lw_outstanding_amount: any;
  cm_total_so: any;
  cm_total_do: any;
  cm_total_invoice: any;
  cm_total_payment: any;
  cm_invoice_amount: any;
  cm_payment_amount: any;
  cm_outstanding_amount: any;
  lm_total_so: any;
  lm_total_do: any;
  lm_total_invoice: any;
  lm_total_payment: any;
  lm_invoice_amount: any;
  lm_payment_amount: any;
  lm_outstanding_amount: any;
  cy_total_so: any;
  cy_total_do: any;
  cy_total_invoice: any;
  cy_total_payment: any;
  cy_invoice_amount: any;
  cy_payment_amount: any;
  cy_outstanding_amount: any;
  ly_total_so: any;
  ly_total_do: any;
  ly_total_invoice: any;
  ly_total_payment: any;
  ly_invoice_amount: any;
  ly_payment_amount: any;
  ly_outstanding_amount: any;
  mtd_over_due_payment: any;
  mtd_over_due_payment_amount: any;
  mtd_over_due_invoice_amount: any;
  mtd_over_due_invoice: any;
  ytd_over_due_payment: any;
  ytd_over_due_payment_amount: any;
  ytd_over_due_invoice_amount: any;
  ytd_over_due_invoice: any;
  mtd_invoice: any;
  mtd_payment: any;
  ytd_invoice: any;
  ytd_payment: any;
  countOwnValues: any;
 
  paymentDay: string[] = [];
  amount: number[] = [];
  monthlySalesData: any;
  total_vendor: any;
  total_product: any;
  pototalcount: any;
  invctotalcount: any;
  grntotalcount: any;
  count_total: any;
  vendor_count: any;
  cancel_invoice: any;
  pending_count: any;
  count_product: any;
  payablesummary_list: any;
  total_invoice: any;
  cancelled_invoice: any;
  pending_invoice: any;
  completed_invoice: any;
  cancelled_payment: any;
  pending_payment: any;
  completed_payment: any;

 
  constructor(private router: Router, private service: SocketService) {
    this.Date = new Date().toString();
    PmrDashboardComponent.constructor(); {
      setInterval(() => {
        this.Date = new Date().toString();
      }, 1000);

    }
   

  }

  ngOnInit() {

    var api = 'PayableDashboard/GetPayablesummary';
      this.service.get(api).subscribe((result: any) => {
        this.responsedata = result;
        this.count_total = result.total_count;
        this.vendor_count = result.vendor_count;
        this.cancel_invoice = result.cancel_invoice;
        this.pending_count = result.pending_count;
        this.count_product = result.product_count
  
      })
  
      var api = 'PayableDashboard/Payabledashboardsummary';
      this.service.get(api).subscribe((result: any) => {
  
        this.responsedata = result;
  
        this.payablesummary_list = this.responsedata.payablesummary_list;
        setTimeout(() => {
          $('#invoice').DataTable();
        }, 1);
      });
    var url = 'PmrDashboard/GetPurchaseCount';
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
     
      this.total_vendor = result.total_vendor;
      this.total_product = result.total_product;
      this.pototalcount = result.pototalcount;
      this.invctotalcount = result.invctotalcount;
      this.grntotalcount = result.grntotalcount;
      this.total_payment = result.total_payment;
    });
    var url = 'PmrDashboard/GetInvoiceCount';
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
     
      this.total_invoice = result.total_invoice;
      this.cancelled_invoice = result.cancelled_invoice;
      this.pending_invoice = result.pending_invoice;
      this.completed_invoice = result.completed_invoice;
      
    });
    var url = 'PmrDashboard/GetPaymentCount';
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
     
      this.total_payment = result.total_payment;
      this.cancelled_payment = result.cancelled_payment;
      this.pending_payment = result.pending_payment;
      this.completed_payment = result.completed_payment;
      
    });
    this.getMonthlySalesChart();
    var api = 'SmrDashboard/GetOwnOverallSalesOrderChart';
    
    this.service.get(api).subscribe((result: any) => {
      this.response_data = result;
      
      this.GetOverallSalesOrderChart_List = this.response_data.GetOverallSalesOrderChart_List; 
      if(this.GetOverallSalesOrderChart_List==null){
        this.series_Value= [0]; 
        this.chartOptions1 = {
         series: this.series_Value,
         chart: {
           width: 390,
           type: "bar", // Use donut type to create a hole in the center
         },
         // plotOptions: {
         //   pie: {
         //     customScale: 0.8,
         //   },
         // },
         plotOptions: {
           pie: {
             customScale: 0.8,
             dataLabels: {
               offset: -5, // Adjust the offset for better positioning
               minAngleToShowLabel: 10, // Set a minimum angle to show labels
               enabled: true,
             },
           },
         },
         responsive: [
           {
             breakpoint: 390,
             options: {
               chart: {
                 width: 200,
               },
               legend: {
                 position: "top",
               },
             },
           },
         ],
         fill: {
           colors: ['#3498db'], // Set the color for the center label
         },
         labels: ['There is No Sales Status Found'], // Set the label for the center
         legend: {
           show: false,
         },
         dataLabels: {
           enabled: false, // Disable data labels for percentage
         },
   
         
       };
         }
         else{
      const totalCount = this.GetOverallSalesOrderChart_List.reduce((acc: number, item: { count_own: string; }) => acc + parseInt(item.count_own), 0);
      this.chartOptions1.series = this.GetOverallSalesOrderChart_List.map((item: { count_own: any; }) =>  (parseInt(item.count_own) / totalCount) * 100);
      this.chartOptions1.labels = this.GetOverallSalesOrderChart_List.map((item: {
        count_own: any; salesorder_status_own: any; 
}) => `${item.salesorder_status_own}: ${item.count_own}`);
    }
  });
    var api = 'SmrDashboard/GetOwnOverallDeliveryOrderChart';
    
    this.service.get(api).subscribe((result: any) => {
      this.response_data = result;
      
      this.GetOverallSalesOrderChart_List = this.response_data.GetOverallSalesOrderChart_List; 
      if(this.GetOverallSalesOrderChart_List==null){
        this.series_Value= [0]; 
        this.chartOptions4 = {
         series: this.series_Value,
         chart: {
           width: 390,
           type: "bar", // Use donut type to create a hole in the center
         },
         // plotOptions: {
         //   pie: {
         //     customScale: 0.8,
         //   },
         // },
         plotOptions: {
           pie: {
             customScale: 0.8,
             dataLabels: {
               offset: -5, // Adjust the offset for better positioning
               minAngleToShowLabel: 10, // Set a minimum angle to show labels
               enabled: true,
             },
           },
         },
         responsive: [
           {
             breakpoint:390,
             options: {
               chart: {
                 width: 200,
               },
               legend: {
                 position: "top",
               },
             },
           },
         ],
         fill: {
           colors: ['#3498db'], // Set the color for the center label
         },
         labels: ['There is No Delivery Status Found'], // Set the label for the center
         legend: {
           show: false,
         },
         dataLabels: {
           enabled: false, // Disable data labels for percentage
         },
   
         
       };
         }
         else{
      
      const totalCount = this.GetOverallSalesOrderChart_List.reduce((acc: number, item: { do_count_own: string; }) => acc + parseInt(item.do_count_own), 0);
      this.chartOptions4.series = this.GetOverallSalesOrderChart_List.map((item: { do_count_own: any; }) =>  (parseInt(item.do_count_own) / totalCount) * 100);
      this.chartOptions4.labels = this.GetOverallSalesOrderChart_List.map((item: {
        do_count_own: any; delivery_status_own: any; 
}) => `${item.delivery_status_own}: ${item.do_count_own}`);
         }
    });
  //  // this.chartOptions = getChartOptions(350);
  //   this.chartOptions1 = getChartOptions1(380);
  //   this.chartOptions4 = getChartOptions4(380);
  //   //this.chartOptions2 = getChartOptions2(380);
 

    setInterval(() => {
      this.Date = new Date().toString();
    }, 1000);
  }
  GoToProduct() {
    this.router.navigate(['/pmr/PmrMstProductSummary']);
  }
  GoToVendor() {
    this.router.navigate(['/pmr/PmrMstVendorregister']);
  }
  GoToPO() {
    this.router.navigate(['/pmr/PmrTrnPurchaseorderSummary']);
  }
  GoToGRN() {
    this.router.navigate(['/pmr/PmrTrnGrninward']);
  }
  GoToInv() {
    this.router.navigate(['/einvoice/Invoice-Summary']);
  }
  GoToPay() {
    this.router.navigate(['/einvoice/ReceiptSummary']);
  }
 
 
  
  getMonthlySalesChart() {

    var url = 'PmrDashboard/GetPurchaseLiabilityReportChart'
    this.service.get(url).subscribe((result: any) => {
     
      this.response_data = result;

      this.GetOverallSalesOrderChart_List = this.response_data.GetPurchaseLiability_List; 

    const categories = this.GetOverallSalesOrderChart_List.map((entry: { purchasemonth: any; }) => entry.purchasemonth);
    const totalAmountData = this.GetOverallSalesOrderChart_List.map((entry: { total_amount: any }) => entry.total_amount);
    const invoiceAmountData = this.GetOverallSalesOrderChart_List.map((entry: { invoice_amount: any }) => entry.invoice_amount);
    const paymentAmountData = this.GetOverallSalesOrderChart_List.map((entry: { payment_amount: any }) => entry.payment_amount);
    const outstandingAmountData = this.GetOverallSalesOrderChart_List.map((entry: { outstanding_amount: any }) => entry.outstanding_amount);
   
    if (categories.length === 0) {
      // If no records are returned, set series data arrays to contain zero values
      totalAmountData.push(0);
      invoiceAmountData.push(0);
      paymentAmountData.push(0);
      outstandingAmountData.push(0);

      categories.push('No Data');
    }
      this.chartOptions = {
        chart: {
          type: 'bar',
          height: 300,
          width: 600,
          background: 'White',
          foreColor: '#0F0F0F',
          toolbar: {
            show: false,
          },
        },
        colors: ['#8062D6', '#FFD54F', '#66BB6A', '#EF5350'], // Use a set of colors for better combinations
        plotOptions: {
          bar: {
            horizontal: false,
            columnWidth: '30%', // Adjust the width of the bars
            borderRadius: 0,
          },
        },
        
        dataLabels: {
          enabled: false,
        },
        stroke: {
          show: true,
          width: 20,
          colors: ['transparent'],
        },
        xaxis: {
          categories: categories,
          labels: {
            style: {
              fontWeight: 'bold',
              fontSize: '16px',
            },
          },
        },
        yaxis: {
          show: true,
          title: {
            text: 'Amount',
            
            style: {
              
              fontWeight: 'bold',
              fontSize: '14px',
              color: '#0F0F0F',
            },
          },
        },
        // tooltip: {
        //   y: {
        //     formatter: function (val: any) {
        //       return val.toFixed(2);
        //     },
        //   },
        // },
        series: [
          {
            name: 'Total Amount',
            data: totalAmountData,
          },
          {
            name: 'Invoice Amount',
            data: invoiceAmountData,
          },
          {
            name: 'Payment Amount',
            data: paymentAmountData,
          },
          {
            name: 'Outstanding Amount',
            data: outstandingAmountData,
          },
        ],
      };

      var chart = new ApexCharts(document.getElementById('chart'), this.chartOptions);
      chart.render();

      
    })
  }
}

// function getChartOptions1(height: number) {
//   const commonWidth = 418;
//   return {
//    // series: [47, 25, 38, 39],
//     chart: {
//       width: commonWidth,
//       type: "pie"
//     },
//    // labels: ["Item1", "Item1 2", "Item1 3", "Item1 4"],
//     // responsive: [
//     //   {
//     //     breakpoint:commonWidth,
//     //     options: {
//     //       chart: {
//     //         width: '100%',
//     //       },
//     //       legend: {
//     //         position: "center"
//     //       }

//     //     }
//     //   }
//     // ]
//   };
// }
// function getChartOptions4(height: number) {
//   const commonWidth = 460;
//   return {
//    // series: [47, 25, 38, 39],
//     chart: {
//       width: commonWidth,
//       type: "pie"
//     },
//    // labels: ["Item1", "Item1 2", "Item1 3", "Item1 4"],
//     // responsive: [
//     //   {
//     //     breakpoint:commonWidth,
//     //     options: {
//     //       chart: {
//     //         width: '100%',
//     //       },
//     //       legend: {
//     //         position: "center"
//     //       }

//     //     }
//     //   }
//     // ]
//   };
// }







