import { Component, OnInit, ViewChild } from "@angular/core";
import { ChartComponent } from "ng-apexcharts";
import { FormBuilder, FormGroup, Validators,FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { Subscription, Observable,timer } from 'rxjs';
import { first } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { DatePipe } from '@angular/common';
import { map, share } from "rxjs/operators";
// import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-crm-rpt-enquiryrpt',
  templateUrl: './crm-rpt-enquiryrpt.component.html',
  styleUrls: ['./crm-rpt-enquiryrpt.component.scss']
})
export class CrmRptEnquiryrptComponent {
  response_data :any;
  getenquirychart_List: any; 
  getselectenquirychart_List: any;
  noleadstatus: any;
  show: any;
  emptyFlag1: boolean= false;
  emptyFlag: boolean= false;
  month1: any;
  monthname1: any;
  monthname3: any;
  monthname4: any;
  monthname5: any;
  monthname6: any;
  chartOptions2: any;
  chartOptions: any;
  month2: any;
  month3: any;
  month4: any;
  month5: any;
  month6: any;
  monthname2: any;
  year : any;
  Year : any;
  activitylog_list: any;
  EnquiryReportSummary_list: any;
  EnquiryReportMainSummary_list: any;
  EnquirysubReportSummary_list: any;
  from_date!:string;
  to_date!:string;
  responsedata: any;
  parameterValue1: any;
  Month: any;
  Enquirycount: any;
  parameterValue: any;
  parameter1 : any;
  getCustomerToLeadChartchart_List: any;
  Customer: any;
  Lead: any;
  month: any;
  Customer1: any;
  Lead1: any;
  Customer2: any;
  Lead2: any;
  month_lead: any;
  month_lead1: any;
  month_lead2: any;
  month_lead3: any;
  month_lead4: any;
  month_lead5: any;
  Lead3: any;
  Lead4: any;
  Lead5: any;
  Customer3: any;
  Customer4: any;
  Customer5: any;
  customer: any;
  lead: any;
  customer1: any;
  lead1: any;
  customer2: any;
  lead2: any;
  lead3: any;
  lead4: any;
  lead5: any;
  customer3: any;
  customer4: any;
  customer5: any;
  Month_lead: any;
  Month_lead1: any;
  Month_lead2: any;
  Month_lead3: any;
  Month_lead4: any;
  Month_lead5: any;
  Month1: any;
  Month6: any;
  Month3: any;
  Month4: any;
  Month5: any;
  Monthname1: any;
  Monthname3: any;
  Monthname4: any;
  Monthname5: any;
  Monthname6: any;
  Month2: any;
  Monthname2: any;
  

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: SocketService,
    private ToastrService: ToastrService,
    private datePipe: DatePipe,
  //  private NgxSpinnerService: NgxSpinnerService
   ) 
  {}
 
  ngOnInit(): void {
    this.GetEnquiryReportSummary();
    this.GetEnquirymainReportSummary();
    this.GetEnquiryChartReportSummary();
    this.GetCustomerToLeadReport();
  }
  GetEnquiryReportSummary(){
    var api = 'MarketingReport/GetEnquiryReportSummary';
    this.service.get(api).subscribe((result:any) => {
    this.response_data = result;
    this.EnquiryReportSummary_list = this.response_data.EnquiryReportSummary_list;
    this.year = this.EnquiryReportSummary_list[0].year;
  }); 
  }
  GetEnquirymainReportSummary(){
var api = 'MarketingReport/GetEnquirymainReportSummary';
  this.service.get(api).subscribe((result:any) => {
  this.response_data = result;
  this.EnquiryReportMainSummary_list = this.response_data.EnquiryReportMainSummary_list;
}); 
  }
  GetEnquiryChartReportSummary(){
    // this.NgxSpinnerService.show();
    debugger
    if(this.from_date == null && this.to_date == null){
        var api = 'MarketingReport/GetEnquiryChartReportSummary';
    this.service.get(api).subscribe((result:any) => {
    this.response_data = result;
    this.getenquirychart_List = this.response_data.getenquirychart_List; 
    if(this.getenquirychart_List == null) {
      debugger
      this.noleadstatus = 'Lead Not Available...';
      this.show = true;
      this.emptyFlag1=true;
    }
    else if (this.getenquirychart_List.length == 0) {
      debugger
      this.noleadstatus = 'Lead Not Available...';
      this.show = true;
      this.emptyFlag1=true;
    }
    
    else if (this.getenquirychart_List.length >= 1) {
      const data = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
      const categories = data.map(month => {
        const entry = this.getenquirychart_List.find((item: { lead_monthname: any; }) => item.lead_monthname === month);
        return entry ? Number(entry.lead_count) : 0;
      });
        this.chartOptions2 = {
      
          series: [  
          {
            name: 'Enquiry Count',
            data: categories,
          },
        ],
        chart: {
          fontFamily: 'inherit',
          type: 'bar',
          height: '350',
          toolbar: {
            show: false,
          },
        },
        plotOptions: {
          bar: {
            horizontal: false,
            columnWidth: '40%',
            borderRadius: 3,
          },
        },
        legend: {
          show: false,
        },
        dataLabels: {
          enabled: false,
        },
        stroke: {
          show: true,
          width: 2,
          colors: ['transparent'],
        },
        xaxis: {
          categories: data,
          axisBorder: {
            show: false,
          },
          axisTicks: {
            show: false,
          },
          labels: {
            style: {
              colors: '#000',
              fontSize: '12px',
            },
          },
        },
        yaxis: {
          labels: {
            style: {
              colors: '#000',
              fontSize: '12px',
            },
          },
        },
        fill: {
          type: 'solid',
        },
        states: {
          normal: {
            filter: {
              type: 'none',
              value: 0,
            },
          },
          hover: {
            filter: {
              type: 'none',
              value: 0,
            },
          },
          active: {
            allowMultipleDataPointsSelection: false,
            filter: {
              type: 'none',
              value: 0,
            },
          },
        },
        
        colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
        grid: {
          padding: {
            top: 10,
          },
          borderColor: '#e6ccb2',
          strokeDashArray: 4,
          yaxis: {
            lines: {
              show: true,
            },
          },
        },
        };
    }
  }); 
}

else{
  var api = 'MarketingReport/GetSelectEnquiryChartReportSummary';
  
  let params ={
    from_date:this.from_date,
    to_date:this.to_date
  }
  this.service.getparams(api,params).subscribe((result:any) => {
  this.response_data = result;
  this.getselectenquirychart_List = this.response_data.getselectenquirychart_List; 
  
  // this.chartOptions2.xaxis.categories = this.getselectenquirychart_List.map((entry: { lead_count: any }) => entry.lead_count);
  // this.chartOptions2.series[0].data = this.getselectenquirychart_List.map((entry: { month1: any }) => entry.month1);
  if(this.getselectenquirychart_List == null) {
    debugger
    this.noleadstatus = 'Lead Not Available...';
    this.show = true;
    this.emptyFlag1=true;
    this.chartOptions2 = {
    
      series: [  
      {
        name: 'Enquiry Count',
        data: [],
      },
    ],
    chart: {
      fontFamily: 'inherit',
      type: 'bar',
      height: '350',
      toolbar: {
        show: false,
      },
    },
    plotOptions: {
      bar: {
        horizontal: false,
        columnWidth: '40%',
        borderRadius: 3,
      },
    },
    legend: {
      show: false,
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      show: true,
      width: 2,
      colors: ['transparent'],
    },
    xaxis: {
      categories: [],
      axisBorder: {
        show: false,
      },
      axisTicks: {
        show: false,
      },
      labels: {
        style: {
          colors: '#000',
          fontSize: '12px',
        },
      },
    },
    yaxis: {
      labels: {
        style: {
          colors: '#000',
          fontSize: '12px',
        },
      },
    },
    fill: {
      type: 'solid',
    },
    states: {
      normal: {
        filter: {
          type: 'none',
          value: 0,
        },
      },
      hover: {
        filter: {
          type: 'none',
          value: 0,
        },
      },
      active: {
        allowMultipleDataPointsSelection: false,
        filter: {
          type: 'none',
          value: 0,
        },
      },
    },
    
    colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
    grid: {
      padding: {
        top: 10,
      },
      borderColor: '#e6ccb2',
      strokeDashArray: 4,
      yaxis: {
        lines: {
          show: true,
        },
      },
    },
    };
  }
  else if (this.getselectenquirychart_List.length == 0) {
    debugger
    this.noleadstatus = 'Lead Not Available...';
    this.show = true;
    this.emptyFlag1=true;
    this.chartOptions2 = {
    
      series: [  
      {
        name: 'Enquiry Count',
        data: [],
      },
    ],
    chart: {
      fontFamily: 'inherit',
      type: 'bar',
      height: '350',
      toolbar: {
        show: false,
      },
    },
    plotOptions: {
      bar: {
        horizontal: false,
        columnWidth: '40%',
        borderRadius: 3,
      },
    },
    legend: {
      show: false,
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      show: true,
      width: 2,
      colors: ['transparent'],
    },
    xaxis: {
      categories: [],
      axisBorder: {
        show: false,
      },
      axisTicks: {
        show: false,
      },
      labels: {
        style: {
          colors: '#000',
          fontSize: '12px',
        },
      },
    },
    yaxis: {
      labels: {
        style: {
          colors: '#000',
          fontSize: '12px',
        },
      },
    },
    fill: {
      type: 'solid',
    },
    states: {
      normal: {
        filter: {
          type: 'none',
          value: 0,
        },
      },
      hover: {
        filter: {
          type: 'none',
          value: 0,
        },
      },
      active: {
        allowMultipleDataPointsSelection: false,
        filter: {
          type: 'none',
          value: 0,
        },
      },
    },
    
    colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
    grid: {
      padding: {
        top: 10,
      },
      borderColor: '#e6ccb2',
      strokeDashArray: 4,
      yaxis: {
        lines: {
          show: true,
        },
      },
    },
    };
  }
  else if (this.getselectenquirychart_List.length >= 1) {
    debugger
      const Monthname1 = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
      const Month1 = Monthname1.map(month => {
        const entry = this.getselectenquirychart_List.find((item: { lead_monthname: any; }) => item.lead_monthname === month);
        return entry ? Number(entry.lead_count) : 0;
      });
      this.chartOptions2 = {
    
        series: [  
        {
          name: 'Enquiry Count',
          data: Month1,
        },
      ],
      chart: {
        fontFamily: 'inherit',
        type: 'bar',
        height: '350',
        toolbar: {
          show: false,
        },
      },
      plotOptions: {
        bar: {
          horizontal: false,
          columnWidth: '40%',
          borderRadius: 3,
        },
      },
      legend: {
        show: false,
      },
      dataLabels: {
        enabled: false,
      },
      stroke: {
        show: true,
        width: 2,
        colors: ['transparent'],
      },
      xaxis: {
        categories: Monthname1,
        axisBorder: {
          show: false,
        },
        axisTicks: {
          show: false,
        },
        labels: {
          style: {
            colors: '#000',
            fontSize: '12px',
          },
        },
      },
      yaxis: {
        labels: {
          style: {
            colors: '#000',
            fontSize: '12px',
          },
        },
      },
      fill: {
        type: 'solid',
      },
      states: {
        normal: {
          filter: {
            type: 'none',
            value: 0,
          },
        },
        hover: {
          filter: {
            type: 'none',
            value: 0,
          },
        },
        active: {
          allowMultipleDataPointsSelection: false,
          filter: {
            type: 'none',
            value: 0,
          },
        },
      },
      
      colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      grid: {
        padding: {
          top: 10,
        },
        borderColor: '#e6ccb2',
        strokeDashArray: 4,
        yaxis: {
          lines: {
            show: true,
          },
        },
      },
      };
  }
}); 
}
// this.NgxSpinnerService.show();
  }

  onSearchClick(){
   
    var url = 'MarketingReport/GetSelectedEnquirymainReportSummary';
    let params ={
      from_date:this.from_date,
      to_date:this.to_date
    }
  this.service.getparams(url,params).subscribe((result:any) => {
  this.response_data = result;
  this.EnquiryReportMainSummary_list = this.response_data.EnquiryReportMainSummary_list;
}); 
this.GetEnquiryChartReportSummary();
this.GetCustomerToLeadReport();
  }

  GetCustomerToLeadReport(){
    
    if(this.from_date == null && this.to_date == null){
    var url = 'MarketingReport/GetCustomerToLeadChartReportSummary';
    this.service.get(url).subscribe((result:any) => {
      this.response_data = result;
      this.getCustomerToLeadChartchart_List = this.response_data.getCustomerToLeadChartchart_List; 
    if(this.getCustomerToLeadChartchart_List == null) {
      
      this.noleadstatus = 'Lead Not Available...';
      this.show = true;
      this.emptyFlag=true;
    }
    else if (this.getCustomerToLeadChartchart_List.length == 0) {
      
      this.noleadstatus = 'Lead Not Available...';
      this.show = true;
      this.emptyFlag=true;
    }
    else if (this.getCustomerToLeadChartchart_List.length >= 1) {
      
      const month_lead = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        const Customer = month_lead.map(month => {
          const entry = this.getCustomerToLeadChartchart_List.find((item: { month: any; }) => item.month === month);
          return entry ? Number(entry.customercount) : 0;
        });
        const Lead = month_lead.map(month => {
          const entry = this.getCustomerToLeadChartchart_List.find((item: { month: any; }) => item.month === month);
          return entry ? Number(entry.leadcount) : 0;
        });
    this.chartOptions = {
      series: [
        {
          name: "Enquiry From sales",
          data: Customer,
        },
        {
          name: "Enquiry From Marketing",
          data: Lead,
        }
      ],
      chart: {
        height: 350,
        type: "area",
        toolbar: {
          show: false,
        },
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: "smooth"
      },
      xaxis: {
        type: "month",
        categories: month_lead,
      },
      legend: {
        show: false,
      },
    };
  }
  }); 
}
else{
  
  var url = 'MarketingReport/GetCustomerToLeadChartReportsearchSummary';
  let params ={
    from_date:this.from_date,
    to_date:this.to_date
  }
  this.service.getparams(url,params).subscribe((result:any) => {
  this.response_data = result;
  this.getCustomerToLeadChartchart_List = this.response_data.getCustomerToLeadChartchart_List; 
  if(this.getCustomerToLeadChartchart_List == null) {
    
    this.noleadstatus = 'Lead Not Available...';
    this.show = true;
    this.emptyFlag=true;
    this.chartOptions = {
      series: [
        {
          name: "Enquiry From sales",
          data: [],
        },
        {
          name: "Enquiry From Marketing",
          data: [],
        }
      ],
      chart: {
        height: 350,
        type: "area",
        toolbar: {
          show: false,
        },
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: "smooth"
      },
      xaxis: {
        type: "month",
        categories: [],
      },
      legend: {
        show: false,
      },
    };
  }
  else if (this.getCustomerToLeadChartchart_List.length == 0) {
    
    this.noleadstatus = 'Lead Not Available...';
    this.show = true;
    this.emptyFlag=true;
    this.chartOptions = {
      series: [
        {
          name: "Enquiry From sales",
          data: [],
        },
        {
          name: "Enquiry From Marketing",
          data: [],
        }
      ],
      chart: {
        height: 350,
        type: "area",
        toolbar: {
          show: false,
        },
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: "smooth"
      },
      xaxis: {
        type: "month",
        categories: [],
      },
      legend: {
        show: false,
      },
    };
  }
  else if (this.getCustomerToLeadChartchart_List.length >= 1) {
    
    const month_lead = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    const Customer = month_lead.map(month => {
      const entry = this.getCustomerToLeadChartchart_List.find((item: { month: any; }) => item.month === month);
      return entry ? Number(entry.customercount) : 0;
    });
    const Lead = month_lead.map(month => {
      const entry = this.getCustomerToLeadChartchart_List.find((item: { month: any; }) => item.month === month);
      return entry ? Number(entry.leadcount) : 0;
    });
  this.chartOptions = {
    series: [
      {
        name: "Enquiry From sales",
        data: Customer,
      },
      {
        name: "Enquiry From Marketing",
        data: Lead,
      }
    ],
    chart: {
      height: 350,
      type: "area",
      toolbar: {
        show: false,
      },
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      curve: "smooth"
    },
    xaxis: {
      type: "month",
      categories: month_lead,
    },
    legend: {
      show: false,
    },
  };
}
}); 
}
  
  }

  onrefreshclick(){
    this.from_date = null!;
    this.to_date = null!;
    this.ngOnInit();
  }

  popmodal( Month: string,Year: string){
    this.Month = Month;
    this.Year = this.EnquiryReportMainSummary_list[0].Year;

    var url = 'MarketingReport/GetEnquirysubReportSummary';
    let params = {
      Month : Month,
      Year : Year
    }
    this.service.getparams(url,params).subscribe((result:any)=>{
  this.response_data = result;
  this.EnquirysubReportSummary_list = this.response_data.EnquirysubReportSummary_list;
}); 
  }

  
}