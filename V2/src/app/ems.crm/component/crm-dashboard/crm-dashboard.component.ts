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

import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart
} from "ng-apexcharts";
import { SharedService } from "src/app/layout/services/shared.service";

export type ChartOptions1 = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};

export type ChartOptions3 = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};

// export type ChartOptions2 = {
//   series2: ApexNonAxisChartSeries;
//   chart2: ApexChart;
//   responsive: ApexResponsive[];
//   labels: any;
// };

@Component({
  selector: 'app-crm-dashboard',
  templateUrl: './crm-dashboard.component.html',
  styleUrls: ['./crm-dashboard.component.scss']
})

export class CrmDashboardComponent implements OnInit {
  
  config = {
    uiColor: '#ffffff',
    toolbarGroups: [
      //{ name: 'clipboard', groups: ['clipboard', 'undo'] },
    //{ name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
    { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
    { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align'] },
  //  { name: 'document', groups: ['mode', 'document', 'doctools'] },
    { name: 'styles' },
    { name: 'colors' },
    // { name: 'links' }, 
    { name: 'insert' },],
    skin: 'kama',
    resize_enabled: false,
    removePlugins: 'elementspath,save,magicline',
    extraPlugins: 'divarea,smiley,justify,indentblock,colordialog',
    // font: 'Arial/Arial, Helvetica, sans-serif; Calibri/Calibri, sans-serif; Times New Roman/Times New Roman, Times, serif; Verdana/Verdana, Geneva, sans-serif; Tahoma/Tahoma, Geneva, sans-serif; Courier New/Courier New, Courier, monospace',
    colorButton_foreStyle: {
       element: 'font',
       attributes: { 'color': '#(color)' }
    },
    height: 188,
    removeDialogTabs: 'image:advanced;link:advanced',
    removeButtons: 'Subscript,Superscript,Anchor,Source,Table',
    format_tags: 'p;h1;h2;h3;pre;div'
 }
  chartOptions: any = {};

  chartOptions1: any = {};
  chartOptions2 :any = {};
  chartOptions3 :any = {};
  socialmedialeadcount:any = {};
  response_data :any;

  DashboardCount_List :any;

  DashboardQuotationAmt_List :any;
  getleadbasedonemployeeList :any;
  noquotation :any;

  year : any;
  
  noquotation_status : any;
  mycalls_count: any;
  
  show = true;

  menu!: any[] ;
  submenu!: any[] ;
  selectedIndex: number = 0;
  menu_name!:any;
  menu_index!: number;
  noleadstatus: any;
  nomonthlyleadstatus:any;

  shopifyproductcount: any;
shopifycustomercount: any;
shopifyordercount: any;
product_count: any;
customer_count: any;
 order_count:any;
 shopifystorename:any;
 store_name:any;
 contactcount_list: any;
contact_count: any;
messagecount_list: any;
message_count: any;
messageincoming_list: any;
incoming_count:any;
messageoutgoing_list:any;
 sent_count:any;
emailstatus_list: any;
deliverytotal_count: any;
opentotal_count:any;
clicktotal_count:any;
 sentmailcount_list:any;
 mail_sent:any;
 customertotalcount_list:any;
  customer_assigncount: any;
  customerassignedcount_list: any;
  unassign_count:any;
  customerunassignedcount_list:any;
  time = new Date();
  rxTime = new Date();
  intervalId: any;
  subscription!: Subscription;
  currentDayName: string;
  fromDate: any; toDate: any;
  emptyFlag: boolean=false;
  emptyFlag1: boolean=false;
  series_Value: any;
  labels_value: any;
  series2:any;
  data:any;
  lead_count:any;
  month1:any;
  month2:any;
  month3:any;
  month4:any;
  month5:any;
  month6:any;
  month7:any;
  month8:any;
  month9:any;
  month10:any;
  month11:any;
  month12:any;
  monthname1:any;
  monthname2:any;
  monthname3:any;
  monthname4:any;
  monthname5:any;
  monthname6:any;
  monthname7:any;
  monthname8:any;
  monthname9:any;
  monthname10:any;
  monthname11:any;
  monthname12:any;
  whatsapp_count:any;
  shopify_count:any;
  mail_count:any; 
  nodatastatus:any;
  emptyFlag3: boolean=false;
  shopifyproduct_counts:any;
 shopifyproducts_list:any[]=[];
  
  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: SocketService,
    private ToastrService: ToastrService,
    public sharedservice: SharedService,
    private datePipe: DatePipe) 
  {
    const today = new Date();
    this.currentDayName = today.toLocaleDateString('en-US', { weekday: 'long' });
}

change_menu_tab(n:number):void{
  this.submenu = this.menu[n].sub1menu;
  this.selectedIndex = n;
}

ngOnInit(): void {

  this.chartOptions = getChartOptions(350);
  this.chartOptions3 = getChartOptions(350);

  var url66 = 'SocialMedia/GetShopifyProductCounts'
  this.service.get(url66).subscribe((result,) => {

    this.response_data = result;
    this.shopifyproduct_counts =  Number(this.response_data.shopifyproducts_list[0].shopifyproduct_count); 
  });

    var api = 'CrmDashboard/GetDashboardCount';
    this.service.get(api).subscribe((result:any) => {
    this.response_data = result;
    this.DashboardCount_List = this.response_data.getDashboardCount_List; 
    if(this.DashboardCount_List == null) {
      this.noleadstatus = 'Lead Not Available...';
      this.show = true;
    }
    else {
      this.show = false;
    }
    this.mycalls_count = Number(this.DashboardCount_List[0].mycalls_count);
    let myleads_count = Number(this.DashboardCount_List[0].myleads_count);
    let myappointments_count = Number(this.DashboardCount_List[0].myappointments_count);
    let assignvisit_count = Number(this.DashboardCount_List[0].assignvisit_count);
    let completedvisit_count = Number(this.DashboardCount_List[0].completedvisit_count);
    let shared_proposal = Number(this.DashboardCount_List[0].shared_proposal);
    let completedorder_count = Number(this.DashboardCount_List[0].completedorder_count);
    let totalorder_count = Number(this.DashboardCount_List[0].totalorder_count);
    // mycalls_count=0;
    if(this.mycalls_count==0 && myleads_count ==0 && myappointments_count==0 && assignvisit_count ==0 && 
      completedvisit_count==0 && shared_proposal ==0 && completedorder_count==0 && totalorder_count ==0 ){
      this.emptyFlag=true;
     this.series_Value= [0]; 
     this.chartOptions1 = {
      series: this.series_Value,
      chart: {
        width: 430,
        type: "donut", // Use donut type to create a hole in the center
      },
      // plotOptions: {
      //   pie: {
      //     customScale: 0.8,
      //   },
      // },
      plotOptions: {
        donut: {
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
          breakpoint: 480,
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
      labels: ['No leads are found'], // Set the label for the center
      legend: {
        show: false,
      },
      dataLabels: {
        enabled: false, // Disable data labels for percentage
      },

      
    };
      }
      else{
        this.series_Value= [myleads_count, myappointments_count, completedorder_count,totalorder_count];
        this.labels_value= ["My Leads", "Appointments", "Completed Order","Total Order"];  
 
    

    this.chartOptions1 = {

      series: this.series_Value, 
      labels: this.labels_value,    
      chart: {
        width: 430,
        type: "donut"
      },
     
      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              position: "bottom"
            }
          }
        }
      ]
  
      
    };
  }

  }); 

  // Donut chart
  var api = 'CrmDashboard/Getsocialmedialeadcount';
    this.service.get(api).subscribe((result:any) => {
    this.response_data = result;
    this.socialmedialeadcount = this.response_data.socialmedialeadcount; 
    if(this.socialmedialeadcount == null) {
      this.nodatastatus = 'Data Not Available...';
      this.show = true;
    }
    else {
      this.show = false;
    }
    this.whatsapp_count = Number(this.socialmedialeadcount[0].whatsapp_count);   
    this.shopify_count = Number(this.socialmedialeadcount[0].shopify_count);   
    this.mail_count = Number(this.socialmedialeadcount[0].mail_count);   
    // mycalls_count=0;
    if(this.whatsapp_count== 0 && this.shopify_count == 0 && this.mail_count== 0){
      this.emptyFlag3=true;
     this.series_Value= [0]; 
     this.chartOptions3 = {
      series: this.series_Value,
      chart: {
        width: 430,
        type: "donut", // Use donut type to create a hole in the center
      },
      // plotOptions: {
      //   pie: {
      //     customScale: 0.8,
      //   },
      // },
      plotOptions: {
        donut: {
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
          breakpoint: 480,
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
      labels: ['No Data Found'], // Set the label for the center
      legend: {
        show: false,
      },
      dataLabels: {
        enabled: false, // Disable data labels for percentage
      },

      
    };
      }
      else{
        this.series_Value= [this.whatsapp_count, this.shopify_count, this.mail_count,];
        this.labels_value= ["Whatsapp", "Shopify", "Mail"];  

    this.chartOptions3 = {

      series: this.series_Value, 
      labels: this.labels_value,    
      chart: {
        width: 430,
        type: "donut"
      },
     
      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              position: "bottom"
            }
          }
        }
      ]
  
      
    };
  }

  }); 

    
    var api = 'CrmDashboard/GetDashboardQuotationAmount';
    this.service.get(api).subscribe((result:any) => {
    this.response_data = result;
    this.DashboardQuotationAmt_List = this.response_data.getDashboardQuotationAmt_List; 
    // console.log (this.DashboardQuotationAmt_List,'amt');
    if(this.DashboardQuotationAmt_List == null) {
      this.noquotation = 'Quotation Not Available...';
      this.show = true;
    }
    else {
      this.show = false;
    }
    this.year=this.DashboardQuotationAmt_List[0].year;
    
    }); 
      
    let  user_gid = localStorage.getItem('user_gid');
    let param = {
      user_gid : user_gid 
    }
    var url = 'User/Dashboardprivilegelevel';
    this.service.getparams(url,param).subscribe((result: any) => {
      for(let i = 0; i< result.menu_list.length; i++){
        if(result.menu_list[i].text == 'Marketing'){
          this.menu_index = i;
        }
      }
      this.menu = result.menu_list[this.menu_index].submenu;
      console.log(this.menu,'test menu');
      this.submenu = this.menu[0].sub1menu;
      console.log(this.submenu,'test submenu');
      // this.change_menu_tab(this.menu_index);
    });

    var url1 = 'SocialMedia/GetShopifyProductCount'
    this.service.get(url1).subscribe((result,) => {
  
      this.shopifyproductcount = result;
      this.product_count = this.shopifyproductcount.count;
    
  
    });

    var url4 = 'SocialMedia/GetShopifyStoreName'
  this.service.get(url4).subscribe((result,) => {

    this.shopifystorename = result;
    this.store_name = this.shopifystorename.shop.name;
  
   

  });
  var url5 = 'SocialMedia/GetContactCount'
  this.service.get(url5).subscribe((result,) => {

    this.contactcount_list = result;
    this.contact_count = this.contactcount_list.contactcount_list[0].contact_count1;
  
   

  });
  var url6 = 'SocialMedia/GetMessageCount'
  this.service.get(url6).subscribe((result,) => {

    this.messagecount_list = result;
    this.message_count = this.messagecount_list.messagecount_list[0].message_count;
  
    //  console.log('customer',this.messagecount_list)

  });
  var url7 = 'SocialMedia/GetMessageIncomingCount'
  this.service.get(url7).subscribe((result,) => {

    this.messageincoming_list = result;
    this.incoming_count = this.messageincoming_list.messageincoming_list[0].incoming_count;
  
    // console.log('customer',this.incoming_count)

  });
  var url8 = 'SocialMedia/GetMessageOutgoingCount'
  this.service.get(url8).subscribe((result,) => {

    this.messageoutgoing_list = result;
    this.sent_count = this.messageoutgoing_list.messageoutgoing_list[0].sent_count;
  
    // console.log('customer',this.sent_count)

  });
  var url9 = 'SocialMedia/GetEmailStatusCount'
  this.service.get(url9).subscribe((result,) => {

    this.emailstatus_list = result;
    this.deliverytotal_count = this.emailstatus_list.emailstatus_list[0].deliverytotal_count;
    this.opentotal_count = this.emailstatus_list.emailstatus_list[0].opentotal_count;
    this.clicktotal_count = this.emailstatus_list.emailstatus_list[0].clicktotal_count;
    
    //  console.log('customer',this.sent_count)
    

  });
  var url10 = 'SocialMedia/GetSentCount'
  this.service.get(url10).subscribe((result,) => {

    this.sentmailcount_list = result;
    this.mail_sent = this.sentmailcount_list.sentmailcount_list[0].mail_sent;
  
    // console.log('customer',this.mail_sent)

  });
  var url11 = 'ShopifyCustomer/GetCustomerAssignedCount'
  this.service.get(url11).subscribe((result,) => {

    this.customerassignedcount_list = result;
    this.customer_assigncount = this.customerassignedcount_list.customerassignedcount_list[0].customer_assigncount;
  

  });
  var url12 = 'ShopifyCustomer/GetCustomerUnassignedCount'
  this.service.get(url12).subscribe((result,) => {

    this.customerunassignedcount_list = result;
    this.unassign_count = this.customerunassignedcount_list.customerunassignedcount_list[0].unassign_count;
  

  });
  var api = 'CrmDashboard/GetBarchartMonthlyLead';
  this.service.get(api).subscribe((result:any) => {
  this.response_data = result;
  this.getleadbasedonemployeeList = this.response_data.getleadbasedonemployee_List; 
  if(this.getleadbasedonemployeeList == null) {
        this.nomonthlyleadstatus = 'Lead Not Available...';
        this.show = true;
        this.emptyFlag1=true;
      }
      else if (this.getleadbasedonemployeeList.length == 0) {
        this.nomonthlyleadstatus = 'Lead Not Available...';
        this.show = true;
        this.emptyFlag1=true;
      }
      else if (this.getleadbasedonemployeeList.length >= 1) {
        const data = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        const categories = data.map(month => {
          const entry = this.getleadbasedonemployeeList.find((item: { lead_monthname: any; }) => item.lead_monthname === month);
          return entry ? Number(entry.lead_count) : 0;
        });
          // this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
          // this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
          this.chartOptions2 = {
        
            series: [  
            {
              name: 'Lead Count',
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
      // else if (this.getleadbasedonemployeeList.length == 2) {
      // this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
      // this.month2 = Number(this.getleadbasedonemployeeList[1].lead_count);
      // this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
      // this.monthname2 = this.getleadbasedonemployeeList[1].lead_monthname;
      // this.chartOptions2 = {

      //   series: [  
      //   {
      //     name: 'Lead Count',
      //     data: [this.month1, this.month2,],
      //   },
      // ],
      // chart: {
      //   fontFamily: 'inherit',
      //   type: 'bar',
      //   height: '350',
      //   toolbar: {
      //     show: false,
      //   },
      // },
      // plotOptions: {
      //   bar: {
      //     horizontal: false,
      //     columnWidth: '40%',
      //     borderRadius: 3,
      //   },
      // },
      // legend: {
      //   show: false,
      // },
      // dataLabels: {
      //   enabled: false,
      // },
      // stroke: {
      //   show: true,
      //   width: 2,
      //   colors: ['transparent'],
      // },
      // xaxis: {
      //   categories: [this.monthname1,this.monthname2],
      //   axisBorder: {
      //     show: false,
      //   },
      //   axisTicks: {
      //     show: false,
      //   },
      //   labels: {
      //     style: {
      //       colors: '#000',
      //       fontSize: '12px',
      //     },
      //   },
      // },
      // yaxis: {
      //   labels: {
      //     style: {
      //       colors: '#000',
      //       fontSize: '12px',
      //     },
      //   },
      // },
      // fill: {
      //   type: 'solid',
      // },
      // states: {
      //   normal: {
      //     filter: {
      //       type: 'none',
      //       value: 0,
      //     },
      //   },
      //   hover: {
      //     filter: {
      //       type: 'none',
      //       value: 0,
      //     },
      //   },
      //   active: {
      //     allowMultipleDataPointsSelection: false,
      //     filter: {
      //       type: 'none',
      //       value: 0,
      //     },
      //   },
      // },
      
      // colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      // grid: {
      //   padding: {
      //     top: 10,
      //   },
      //   borderColor: '#e6ccb2',
      //   strokeDashArray: 4,
      //   yaxis: {
      //     lines: {
      //       show: true,
      //     },
      //   },
      // },
      // };
      // }
      // else if (this.getleadbasedonemployeeList.length == 3) {
      //   this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
      //   this.month2 = Number(this.getleadbasedonemployeeList[1].lead_count);
      //   this.month3 = Number(this.getleadbasedonemployeeList[2].lead_count);
      //   this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
      //   this.monthname2 = this.getleadbasedonemployeeList[1].lead_monthname;
      //   this.monthname3 = this.getleadbasedonemployeeList[2].lead_monthname;
      //   this.chartOptions2 = {
      
      //     series: [  
      //     {
      //       name: 'Lead Count',
      //       data: [this.month1, this.month2,this.month3,],
      //     },
      //   ],
      //   chart: {
      //     fontFamily: 'inherit',
      //     type: 'bar',
      //     height: '350',
      //     toolbar: {
      //       show: false,
      //     },
      //   },
      //   plotOptions: {
      //     bar: {
      //       horizontal: false,
      //       columnWidth: '40%',
      //       borderRadius: 3,
      //     },
      //   },
      //   legend: {
      //     show: false,
      //   },
      //   dataLabels: {
      //     enabled: false,
      //   },
      //   stroke: {
      //     show: true,
      //     width: 2,
      //     colors: ['transparent'],
      //   },
      //   xaxis: {
      //     categories: [this.monthname1,this.monthname2,this.monthname3],
      //     axisBorder: {
      //       show: false,
      //     },
      //     axisTicks: {
      //       show: false,
      //     },
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   yaxis: {
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   fill: {
      //     type: 'solid',
      //   },
      //   states: {
      //     normal: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     hover: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     active: {
      //       allowMultipleDataPointsSelection: false,
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //   },
        
      //   colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      //   grid: {
      //     padding: {
      //       top: 10,
      //     },
      //     borderColor: '#e6ccb2',
      //     strokeDashArray: 4,
      //     yaxis: {
      //       lines: {
      //         show: true,
      //       },
      //     },
      //   },
      //   };
      // }
      // else if (this.getleadbasedonemployeeList.length == 4) {
      //   this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
      //   this.month2 = Number(this.getleadbasedonemployeeList[1].lead_count);
      //   this.month3 = Number(this.getleadbasedonemployeeList[2].lead_count);
      //   this.month4 = Number(this.getleadbasedonemployeeList[3].lead_count);
      //   this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
      //   this.monthname2 = this.getleadbasedonemployeeList[1].lead_monthname;
      //   this.monthname3 = this.getleadbasedonemployeeList[2].lead_monthname;
      //   this.monthname4 = this.getleadbasedonemployeeList[3].lead_monthname;
      //   this.chartOptions2 = {
      
      //     series: [  
      //     {
      //       name: 'Lead Count',
      //       data: [this.month1, this.month2,this.month3,this.month4,],
      //     },
      //   ],
      //   chart: {
      //     fontFamily: 'inherit',
      //     type: 'bar',
      //     height: '350',
      //     toolbar: {
      //       show: false,
      //     },
      //   },
      //   plotOptions: {
      //     bar: {
      //       horizontal: false,
      //       columnWidth: '40%',
      //       borderRadius: 3,
      //     },
      //   },
      //   legend: {
      //     show: false,
      //   },
      //   dataLabels: {
      //     enabled: false,
      //   },
      //   stroke: {
      //     show: true,
      //     width: 2,
      //     colors: ['transparent'],
      //   },
      //   xaxis: {
      //     categories: [this.monthname1,this.monthname2,this.monthname3,this.monthname4,],
      //     axisBorder: {
      //       show: false,
      //     },
      //     axisTicks: {
      //       show: false,
      //     },
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   yaxis: {
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   fill: {
      //     type: 'solid',
      //   },
      //   states: {
      //     normal: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     hover: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     active: {
      //       allowMultipleDataPointsSelection: false,
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //   },
        
      //   colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      //   grid: {
      //     padding: {
      //       top: 10,
      //     },
      //     borderColor: '#e6ccb2',
      //     strokeDashArray: 4,
      //     yaxis: {
      //       lines: {
      //         show: true,
      //       },
      //     },
      //   },
      //   };
      // }
      // else if (this.getleadbasedonemployeeList.length == 5) {
      //   this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
      //   this.month2 = Number(this.getleadbasedonemployeeList[1].lead_count);
      //   this.month3 = Number(this.getleadbasedonemployeeList[2].lead_count);
      //   this.month4 = Number(this.getleadbasedonemployeeList[3].lead_count);
      //   this.month5 = Number(this.getleadbasedonemployeeList[4].lead_count);
      //   this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
      //   this.monthname2 = this.getleadbasedonemployeeList[1].lead_monthname;
      //   this.monthname3 = this.getleadbasedonemployeeList[2].lead_monthname;
      //   this.monthname4 = this.getleadbasedonemployeeList[3].lead_monthname;
      //   this.monthname5 = this.getleadbasedonemployeeList[4].lead_monthname;
      //   this.chartOptions2 = {
      
      //     series: [  
      //     {
      //       name: 'Lead Count',
      //       data: [this.month1, this.month2,this.month3,this.month4,this.month5,],
      //     },
      //   ],
      //   chart: {
      //     fontFamily: 'inherit',
      //     type: 'bar',
      //     height: '350',
      //     toolbar: {
      //       show: false,
      //     },
      //   },
      //   plotOptions: {
      //     bar: {
      //       horizontal: false,
      //       columnWidth: '40%',
      //       borderRadius: 3,
      //     },
      //   },
      //   legend: {
      //     show: false,
      //   },
      //   dataLabels: {
      //     enabled: false,
      //   },
      //   stroke: {
      //     show: true,
      //     width: 2,
      //     colors: ['transparent'],
      //   },
      //   xaxis: {
      //     categories: [this.monthname1,this.monthname2,this.monthname3,this.monthname4,this.monthname5],
      //     axisBorder: {
      //       show: false,
      //     },
      //     axisTicks: {
      //       show: false,
      //     },
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   yaxis: {
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   fill: {
      //     type: 'solid',
      //   },
      //   states: {
      //     normal: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     hover: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     active: {
      //       allowMultipleDataPointsSelection: false,
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //   },
        
      //   colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      //   grid: {
      //     padding: {
      //       top: 10,
      //     },
      //     borderColor: '#e6ccb2',
      //     strokeDashArray: 4,
      //     yaxis: {
      //       lines: {
      //         show: true,
      //       },
      //     },
      //   },
      //   };
      // }
      // else if (this.getleadbasedonemployeeList.length == 6) {
      //   this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
      //   this.month2 = Number(this.getleadbasedonemployeeList[1].lead_count);
      //   this.month3 = Number(this.getleadbasedonemployeeList[2].lead_count);
      //   this.month4 = Number(this.getleadbasedonemployeeList[3].lead_count);
      //   this.month5 = Number(this.getleadbasedonemployeeList[4].lead_count);
      //   this.month6 = Number(this.getleadbasedonemployeeList[5].lead_count);
      //   this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
      //   this.monthname2 = this.getleadbasedonemployeeList[1].lead_monthname;
      //   this.monthname3 = this.getleadbasedonemployeeList[2].lead_monthname;
      //   this.monthname4 = this.getleadbasedonemployeeList[3].lead_monthname;
      //   this.monthname5 = this.getleadbasedonemployeeList[4].lead_monthname;
      //   this.monthname6 = this.getleadbasedonemployeeList[5].lead_monthname;
      //   this.chartOptions2 = {
      
      //     series: [  
      //     {
      //       name: 'Lead Count',
      //       data: [this.month1, this.month2,this.month3,this.month4,this.month5,this.month6,],
      //     },
      //   ],
      //   chart: {
      //     fontFamily: 'inherit',
      //     type: 'bar',
      //     height: '350',
      //     toolbar: {
      //       show: false,
      //     },
      //   },
      //   plotOptions: {
      //     bar: {
      //       horizontal: false,
      //       columnWidth: '40%',
      //       borderRadius: 3,
      //     },
      //   },
      //   legend: {
      //     show: false,
      //   },
      //   dataLabels: {
      //     enabled: false,
      //   },
      //   stroke: {
      //     show: true,
      //     width: 2,
      //     colors: ['transparent'],
      //   },
      //   xaxis: {
      //     categories: [this.monthname1,this.monthname2,this.monthname3,this.monthname4,this.monthname5,this.monthname6],
      //     axisBorder: {
      //       show: false,
      //     },
      //     axisTicks: {
      //       show: false,
      //     },
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   yaxis: {
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   fill: {
      //     type: 'solid',
      //   },
      //   states: {
      //     normal: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     hover: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     active: {
      //       allowMultipleDataPointsSelection: false,
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //   },
        
      //   colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      //   grid: {
      //     padding: {
      //       top: 10,
      //     },
      //     borderColor: '#e6ccb2',
      //     strokeDashArray: 4,
      //     yaxis: {
      //       lines: {
      //         show: true,
      //       },
      //     },
      //   },
      //   };
      // }
      // else if (this.getleadbasedonemployeeList.length == 7) {
      //   this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
      //   this.month2 = Number(this.getleadbasedonemployeeList[1].lead_count);
      //   this.month3 = Number(this.getleadbasedonemployeeList[2].lead_count);
      //   this.month4 = Number(this.getleadbasedonemployeeList[3].lead_count);
      //   this.month5 = Number(this.getleadbasedonemployeeList[4].lead_count);
      //   this.month6 = Number(this.getleadbasedonemployeeList[5].lead_count);
      //   this.month7 = Number(this.getleadbasedonemployeeList[6].lead_count);
      //   this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
      //   this.monthname2 = this.getleadbasedonemployeeList[1].lead_monthname;
      //   this.monthname3 = this.getleadbasedonemployeeList[2].lead_monthname;
      //   this.monthname4 = this.getleadbasedonemployeeList[3].lead_monthname;
      //   this.monthname5 = this.getleadbasedonemployeeList[4].lead_monthname;
      //   this.monthname6 = this.getleadbasedonemployeeList[5].lead_monthname;
      //   this.monthname7 = this.getleadbasedonemployeeList[6].lead_monthname;
      //   this.chartOptions2 = {
      
      //     series: [  
      //     {
      //       name: 'Lead Count',
      //       data: [this.month1, this.month2,this.month3,this.month4,this.month5,this.month6,this.month7,],
      //     },
      //   ],
      //   chart: {
      //     fontFamily: 'inherit',
      //     type: 'bar',
      //     height: '350',
      //     toolbar: {
      //       show: false,
      //     },
      //   },
      //   plotOptions: {
      //     bar: {
      //       horizontal: false,
      //       columnWidth: '40%',
      //       borderRadius: 3,
      //     },
      //   },
      //   legend: {
      //     show: false,
      //   },
      //   dataLabels: {
      //     enabled: false,
      //   },
      //   stroke: {
      //     show: true,
      //     width: 2,
      //     colors: ['transparent'],
      //   },
      //   xaxis: {
      //     categories: [this.monthname1,this.monthname2,this.monthname3,this.monthname4,this.monthname5,this.monthname6,this.monthname7],
      //     axisBorder: {
      //       show: false,
      //     },
      //     axisTicks: {
      //       show: false,
      //     },
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   yaxis: {
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   fill: {
      //     type: 'solid',
      //   },
      //   states: {
      //     normal: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     hover: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     active: {
      //       allowMultipleDataPointsSelection: false,
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //   },
        
      //   colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      //   grid: {
      //     padding: {
      //       top: 10,
      //     },
      //     borderColor: '#e6ccb2',
      //     strokeDashArray: 4,
      //     yaxis: {
      //       lines: {
      //         show: true,
      //       },
      //     },
      //   },
      //   };
      // }
      // else if (this.getleadbasedonemployeeList.length == 8) {
      //   this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
      //   this.month2 = Number(this.getleadbasedonemployeeList[1].lead_count);
      //   this.month3 = Number(this.getleadbasedonemployeeList[2].lead_count);
      //   this.month4 = Number(this.getleadbasedonemployeeList[3].lead_count);
      //   this.month5 = Number(this.getleadbasedonemployeeList[4].lead_count);
      //   this.month6 = Number(this.getleadbasedonemployeeList[5].lead_count);
      //   this.month7 = Number(this.getleadbasedonemployeeList[6].lead_count);
      //   this.month8 = Number(this.getleadbasedonemployeeList[7].lead_count);
      //   this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
      //   this.monthname2 = this.getleadbasedonemployeeList[1].lead_monthname;
      //   this.monthname3 = this.getleadbasedonemployeeList[2].lead_monthname;
      //   this.monthname4 = this.getleadbasedonemployeeList[3].lead_monthname;
      //   this.monthname5 = this.getleadbasedonemployeeList[4].lead_monthname;
      //   this.monthname6 = this.getleadbasedonemployeeList[5].lead_monthname;
      //   this.monthname7 = this.getleadbasedonemployeeList[6].lead_monthname;
      //   this.monthname8 = this.getleadbasedonemployeeList[7].lead_monthname;
      //   this.chartOptions2 = {
      
      //     series: [  
      //     {
      //       name: 'Lead Count',
      //       data: [this.month1, this.month2,this.month3,this.month4,this.month5,this.month6,this.month7,this.month8,],
      //     },
      //   ],
      //   chart: {
      //     fontFamily: 'inherit',
      //     type: 'bar',
      //     height: '350',
      //     toolbar: {
      //       show: false,
      //     },
      //   },
      //   plotOptions: {
      //     bar: {
      //       horizontal: false,
      //       columnWidth: '40%',
      //       borderRadius: 3,
      //     },
      //   },
      //   legend: {
      //     show: false,
      //   },
      //   dataLabels: {
      //     enabled: false,
      //   },
      //   stroke: {
      //     show: true,
      //     width: 2,
      //     colors: ['transparent'],
      //   },
      //   xaxis: {
      //     categories: [this.monthname1,this.monthname2,this.monthname3,this.monthname4,this.monthname5,this.monthname6,this.monthname7,this.monthname8],
      //     axisBorder: {
      //       show: false,
      //     },
      //     axisTicks: {
      //       show: false,
      //     },
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   yaxis: {
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   fill: {
      //     type: 'solid',
      //   },
      //   states: {
      //     normal: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     hover: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     active: {
      //       allowMultipleDataPointsSelection: false,
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //   },
        
      //   colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      //   grid: {
      //     padding: {
      //       top: 10,
      //     },
      //     borderColor: '#e6ccb2',
      //     strokeDashArray: 4,
      //     yaxis: {
      //       lines: {
      //         show: true,
      //       },
      //     },
      //   },
      //   };
      // }
      // else if (this.getleadbasedonemployeeList.length == 9) {
      //   this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
      //   this.month2 = Number(this.getleadbasedonemployeeList[1].lead_count);
      //   this.month3 = Number(this.getleadbasedonemployeeList[2].lead_count);
      //   this.month4 = Number(this.getleadbasedonemployeeList[3].lead_count);
      //   this.month5 = Number(this.getleadbasedonemployeeList[4].lead_count);
      //   this.month6 = Number(this.getleadbasedonemployeeList[5].lead_count);
      //   this.month7 = Number(this.getleadbasedonemployeeList[6].lead_count);
      //   this.month8 = Number(this.getleadbasedonemployeeList[7].lead_count);
      //   this.month9 = Number(this.getleadbasedonemployeeList[8].lead_count);
      //   this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
      //   this.monthname2 = this.getleadbasedonemployeeList[1].lead_monthname;
      //   this.monthname3 = this.getleadbasedonemployeeList[2].lead_monthname;
      //   this.monthname4 = this.getleadbasedonemployeeList[3].lead_monthname;
      //   this.monthname5 = this.getleadbasedonemployeeList[4].lead_monthname;
      //   this.monthname6 = this.getleadbasedonemployeeList[5].lead_monthname;
      //   this.monthname7 = this.getleadbasedonemployeeList[6].lead_monthname;
      //   this.monthname8 = this.getleadbasedonemployeeList[7].lead_monthname;
      //   this.monthname9 = this.getleadbasedonemployeeList[8].lead_monthname;
      //   this.chartOptions2 = {
      
      //     series: [  
      //     {
      //       name: 'Lead Count',
      //       data: [this.month1, this.month2,this.month3,this.month4,this.month5,this.month6,this.month7,this.month8,this.month9,],
      //     },
      //   ],
      //   chart: {
      //     fontFamily: 'inherit',
      //     type: 'bar',
      //     height: '350',
      //     toolbar: {
      //       show: false,
      //     },
      //   },
      //   plotOptions: {
      //     bar: {
      //       horizontal: false,
      //       columnWidth: '40%',
      //       borderRadius: 3,
      //     },
      //   },
      //   legend: {
      //     show: false,
      //   },
      //   dataLabels: {
      //     enabled: false,
      //   },
      //   stroke: {
      //     show: true,
      //     width: 2,
      //     colors: ['transparent'],
      //   },
      //   xaxis: {
      //     categories: [this.monthname1,this.monthname2,this.monthname3,this.monthname4,this.monthname5,this.monthname6,this.monthname7,this.monthname8,this.monthname9],
      //     axisBorder: {
      //       show: false,
      //     },
      //     axisTicks: {
      //       show: false,
      //     },
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   yaxis: {
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   fill: {
      //     type: 'solid',
      //   },
      //   states: {
      //     normal: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     hover: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     active: {
      //       allowMultipleDataPointsSelection: false,
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //   },
        
      //   colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      //   grid: {
      //     padding: {
      //       top: 10,
      //     },
      //     borderColor: '#e6ccb2',
      //     strokeDashArray: 4,
      //     yaxis: {
      //       lines: {
      //         show: true,
      //       },
      //     },
      //   },
      //   };
      // }
      // else if (this.getleadbasedonemployeeList.length == 10) {
      //   this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
      //   this.month2 = Number(this.getleadbasedonemployeeList[1].lead_count);
      //   this.month3 = Number(this.getleadbasedonemployeeList[2].lead_count);
      //   this.month4 = Number(this.getleadbasedonemployeeList[3].lead_count);
      //   this.month5 = Number(this.getleadbasedonemployeeList[4].lead_count);
      //   this.month6 = Number(this.getleadbasedonemployeeList[5].lead_count);
      //   this.month7 = Number(this.getleadbasedonemployeeList[6].lead_count);
      //   this.month8 = Number(this.getleadbasedonemployeeList[7].lead_count);
      //   this.month9 = Number(this.getleadbasedonemployeeList[8].lead_count);
      //   this.month10 = Number(this.getleadbasedonemployeeList[9].lead_count);
      //   this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
      //   this.monthname2 = this.getleadbasedonemployeeList[1].lead_monthname;
      //   this.monthname3 = this.getleadbasedonemployeeList[2].lead_monthname;
      //   this.monthname4 = this.getleadbasedonemployeeList[3].lead_monthname;
      //   this.monthname5 = this.getleadbasedonemployeeList[4].lead_monthname;
      //   this.monthname6 = this.getleadbasedonemployeeList[5].lead_monthname;
      //   this.monthname7 = this.getleadbasedonemployeeList[6].lead_monthname;
      //   this.monthname8 = this.getleadbasedonemployeeList[7].lead_monthname;
      //   this.monthname9 = this.getleadbasedonemployeeList[8].lead_monthname;
      //   this.monthname10 = this.getleadbasedonemployeeList[9].lead_monthname;
      //   this.chartOptions2 = {
      
      //     series: [  
      //     {
      //       name: 'Lead Count',
      //       data: [this.month1, this.month2,this.month3,this.month4,this.month5,this.month6,this.month7,this.month8,this.month9,this.month10,],
      //     },
      //   ],
      //   chart: {
      //     fontFamily: 'inherit',
      //     type: 'bar',
      //     height: '350',
      //     toolbar: {
      //       show: false,
      //     },
      //   },
      //   plotOptions: {
      //     bar: {
      //       horizontal: false,
      //       columnWidth: '40%',
      //       borderRadius: 3,
      //     },
      //   },
      //   legend: {
      //     show: false,
      //   },
      //   dataLabels: {
      //     enabled: false,
      //   },
      //   stroke: {
      //     show: true,
      //     width: 2,
      //     colors: ['transparent'],
      //   },
      //   xaxis: {
      //     categories: [this.monthname1,this.monthname2,this.monthname3,this.monthname4,this.monthname5,this.monthname6,this.monthname7,this.monthname8,this.monthname9,this.monthname10],
      //     axisBorder: {
      //       show: false,
      //     },
      //     axisTicks: {
      //       show: false,
      //     },
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   yaxis: {
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   fill: {
      //     type: 'solid',
      //   },
      //   states: {
      //     normal: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     hover: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     active: {
      //       allowMultipleDataPointsSelection: false,
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //   },
        
      //   colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      //   grid: {
      //     padding: {
      //       top: 10,
      //     },
      //     borderColor: '#e6ccb2',
      //     strokeDashArray: 4,
      //     yaxis: {
      //       lines: {
      //         show: true,
      //       },
      //     },
      //   },
      //   };
      // }
      // else if (this.getleadbasedonemployeeList.length == 11) {
      //   this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
      //   this.month2 = Number(this.getleadbasedonemployeeList[1].lead_count);
      //   this.month3 = Number(this.getleadbasedonemployeeList[2].lead_count);
      //   this.month4 = Number(this.getleadbasedonemployeeList[3].lead_count);
      //   this.month5 = Number(this.getleadbasedonemployeeList[4].lead_count);
      //   this.month6 = Number(this.getleadbasedonemployeeList[5].lead_count);
      //   this.month7 = Number(this.getleadbasedonemployeeList[6].lead_count);
      //   this.month8 = Number(this.getleadbasedonemployeeList[7].lead_count);
      //   this.month9 = Number(this.getleadbasedonemployeeList[8].lead_count);
      //   this.month10 = Number(this.getleadbasedonemployeeList[9].lead_count);
      //   this.month11 = Number(this.getleadbasedonemployeeList[10].lead_count);
      //   this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
      //   this.monthname2 = this.getleadbasedonemployeeList[1].lead_monthname;
      //   this.monthname3 = this.getleadbasedonemployeeList[2].lead_monthname;
      //   this.monthname4 = this.getleadbasedonemployeeList[3].lead_monthname;
      //   this.monthname5 = this.getleadbasedonemployeeList[4].lead_monthname;
      //   this.monthname6 = this.getleadbasedonemployeeList[5].lead_monthname;
      //   this.monthname7 = this.getleadbasedonemployeeList[6].lead_monthname;
      //   this.monthname8 = this.getleadbasedonemployeeList[7].lead_monthname;
      //   this.monthname9 = this.getleadbasedonemployeeList[8].lead_monthname;
      //   this.monthname10 = this.getleadbasedonemployeeList[9].lead_monthname;
      //   this.monthname11 = this.getleadbasedonemployeeList[10].lead_monthname;
      //   this.chartOptions2 = {
      
      //     series: [  
      //     {
      //       name: 'Lead Count',
      //       data: [this.month1, this.month2,this.month3,this.month4,this.month5,this.month6,this.month7,this.month8,this.month9,this.month10,this.month11,],
      //     },
      //   ],
      //   chart: {
      //     fontFamily: 'inherit',
      //     type: 'bar',
      //     height: '350',
      //     toolbar: {
      //       show: false,
      //     },
      //   },
      //   plotOptions: {
      //     bar: {
      //       horizontal: false,
      //       columnWidth: '40%',
      //       borderRadius: 3,
      //     },
      //   },
      //   legend: {
      //     show: false,
      //   },
      //   dataLabels: {
      //     enabled: false,
      //   },
      //   stroke: {
      //     show: true,
      //     width: 2,
      //     colors: ['transparent'],
      //   },
      //   xaxis: {
      //     categories: [this.monthname1,this.monthname2,this.monthname3,this.monthname4,this.monthname5,this.monthname6,this.monthname7,this.monthname8,this.monthname9,this.monthname10,this.monthname11],
      //     axisBorder: {
      //       show: false,
      //     },
      //     axisTicks: {
      //       show: false,
      //     },
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   yaxis: {
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   fill: {
      //     type: 'solid',
      //   },
      //   states: {
      //     normal: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     hover: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     active: {
      //       allowMultipleDataPointsSelection: false,
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //   },
        
      //   colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      //   grid: {
      //     padding: {
      //       top: 10,
      //     },
      //     borderColor: '#e6ccb2',
      //     strokeDashArray: 4,
      //     yaxis: {
      //       lines: {
      //         show: true,
      //       },
      //     },
      //   },
      //   };
      // }
      // else if (this.getleadbasedonemployeeList.length == 11) {
      //   this.month1 = Number(this.getleadbasedonemployeeList[0].lead_count);
      //   this.month2 = Number(this.getleadbasedonemployeeList[1].lead_count);
      //   this.month3 = Number(this.getleadbasedonemployeeList[2].lead_count);
      //   this.month4 = Number(this.getleadbasedonemployeeList[3].lead_count);
      //   this.month5 = Number(this.getleadbasedonemployeeList[4].lead_count);
      //   this.month6 = Number(this.getleadbasedonemployeeList[5].lead_count);
      //   this.month7 = Number(this.getleadbasedonemployeeList[6].lead_count);
      //   this.month8 = Number(this.getleadbasedonemployeeList[7].lead_count);
      //   this.month9 = Number(this.getleadbasedonemployeeList[8].lead_count);
      //   this.month10 = Number(this.getleadbasedonemployeeList[9].lead_count);
      //   this.month11 = Number(this.getleadbasedonemployeeList[10].lead_count);
      //   this.month12 = Number(this.getleadbasedonemployeeList[11].lead_count);
      //   this.monthname1 = this.getleadbasedonemployeeList[0].lead_monthname;
      //   this.monthname2 = this.getleadbasedonemployeeList[1].lead_monthname;
      //   this.monthname3 = this.getleadbasedonemployeeList[2].lead_monthname;
      //   this.monthname4 = this.getleadbasedonemployeeList[3].lead_monthname;
      //   this.monthname5 = this.getleadbasedonemployeeList[4].lead_monthname;
      //   this.monthname6 = this.getleadbasedonemployeeList[5].lead_monthname;
      //   this.monthname7 = this.getleadbasedonemployeeList[6].lead_monthname;
      //   this.monthname8 = this.getleadbasedonemployeeList[7].lead_monthname;
      //   this.monthname9 = this.getleadbasedonemployeeList[8].lead_monthname;
      //   this.monthname10 = this.getleadbasedonemployeeList[9].lead_monthname;
      //   this.monthname11 = this.getleadbasedonemployeeList[10].lead_monthname;
      //   this.monthname12 = this.getleadbasedonemployeeList[11].lead_monthname;
      //   this.chartOptions2 = {
      
      //     series: [  
      //     {
      //       name: 'Lead Count',
      //       data: [this.month1, this.month2,this.month3,this.month4,this.month5,this.month6,this.month7,this.month8,this.month9,this.month10,this.month11,this.month12,],
      //     },
      //   ],
      //   chart: {
      //     fontFamily: 'inherit',
      //     type: 'bar',
      //     height: '350',
      //     toolbar: {
      //       show: false,
      //     },
      //   },
      //   plotOptions: {
      //     bar: {
      //       horizontal: false,
      //       columnWidth: '40%',
      //       borderRadius: 3,
      //     },
      //   },
      //   legend: {
      //     show: false,
      //   },
      //   dataLabels: {
      //     enabled: false,
      //   },
      //   stroke: {
      //     show: true,
      //     width: 2,
      //     colors: ['transparent'],
      //   },
      //   xaxis: {
      //     categories: [this.monthname1,this.monthname2,this.monthname3,this.monthname4,this.monthname5,this.monthname6,this.monthname7,this.monthname8,this.monthname9,this.monthname10,this.monthname11,this.monthname12],
      //     axisBorder: {
      //       show: false,
      //     },
      //     axisTicks: {
      //       show: false,
      //     },
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   yaxis: {
      //     labels: {
      //       style: {
      //         colors: '#000',
      //         fontSize: '12px',
      //       },
      //     },
      //   },
      //   fill: {
      //     type: 'solid',
      //   },
      //   states: {
      //     normal: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     hover: {
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //     active: {
      //       allowMultipleDataPointsSelection: false,
      //       filter: {
      //         type: 'none',
      //         value: 0,
      //       },
      //     },
      //   },
        
      //   colors: ['#9D76C1', '#f1841e', '#047beb', '#e63423'],
      //   grid: {
      //     padding: {
      //       top: 10,
      //     },
      //     borderColor: '#e6ccb2',
      //     strokeDashArray: 4,
      //     yaxis: {
      //       lines: {
      //         show: true,
      //       },
      //     },
      //   },
      //   };
      // }
}); 



  var url13 = 'ShopifyCustomer/GetCustomerTotalCount'
  this.service.get(url13).subscribe((result,) => {

    this.customertotalcount_list = result;
    this.customer_count = this.customertotalcount_list.customertotalcount_list[0].customer_totalcount;
  

  });

  let yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    // console.log(yesterday);
    this.fromDate = this.datePipe.transform(yesterday, 'dd-MM-yyyy');
    this.toDate = this.datePipe.transform(new Date(), 'dd-MM-yyyy');
    // console.log(this.fromDate); 

    this.intervalId = setInterval(() => {
      this.time = new Date();
    }, 1000);

    // Using RxJS Timer
    this.subscription = timer(0, 1000)
      .pipe(
        map(() => new Date()),
        share()
      )
      .subscribe(time => {
        let hour = this.rxTime.getHours();
        let minuts = this.rxTime.getMinutes();
        let seconds = this.rxTime.getSeconds();
        //let a = time.toLocaleString('en-US', { hour: 'numeric', hour12: true });
        let NewTime = hour + ":" + minuts + ":" + seconds
        // console.log(NewTime);
        this.rxTime = time;
      });
   
} 

redirect_menu(data:string, j:number, k:number):void{
  if(data != null && data !="" && data !="#"){
    this.router.navigate([data]);
    // this.menu;
    // this.submenu;
    // this.sharedservice.menuOne();
    // this.sharedservice.setMenuTwo(this.menu[this.selectedIndex]);
    // this.sharedservice.setMenuThree(this.submenu[j]);
    // this.sharedservice.setMenuFour(this.submenu[j].sub2menu[k]);
    // this.sharedservice.menufunctionToCall();
    // this.submenu[j].sub2menu[k];
    // this.router.navigate([data]);
  }
}

}

 

function getChartOptions(height: number) {
  const labelColor = '#000'; 
  const borderColor = '#e6ccb2';
  const secondaryColor = '#f1841e'
  const baseColor1 = '#047beb';
  const secondaryColor1 = '#e63423'
  const baseColor = '#047beb';  

  return {
    series: [

      
      // {
      //   name: 'Active users',
      //   data: [50, 60, 70, 80, 60, 50, 70, 60],
      // },
      // {
      //   name: 'Inactive Users',
      //   data: [50, 60, 70, 80, 60, 50, 70, 60],
      // },
      {
        name: 'Onboarding',
        data: [20, 40, 30, 70, 60, 10],
      },
      // {
      //   name: 'Relieving',
      //   data: [50, 60, 70, 80, 60, 50, 70, 60],
      // },
    ],
    chart: {
      fontFamily: 'inherit',
      type: 'bar',
      height: height,
      toolbar: {
        show: false,
      },
    },
    plotOptions: {
      bar: {
        horizontal: false,
        columnWidth: '20%',
        borderRadius: 2,
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
      categories: ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
      axisBorder: {
        show: false,
      },
      axisTicks: {
        show: false,
      },
      labels: {
        style: {
          colors: labelColor,
          fontSize: '12px',
        },
      },
    },
    yaxis: {
      labels: {
        style: {
          colors: labelColor,
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

    colors: [baseColor, secondaryColor,baseColor1, secondaryColor1],
    grid: {
      padding: {
        top: 10,
      },
      borderColor: borderColor,
      strokeDashArray: 4,
      yaxis: {
        lines: {
          show: true,
        },
      },
    },
  };
 
  
}





