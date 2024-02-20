import { Component, } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Subscription, Observable, timer } from 'rxjs';
import { map, share } from "rxjs/operators";
import { DatePipe } from '@angular/common';
import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart
} from "ng-apexcharts";

export class IAnalytics {
  analytics_listuser: any[] = [];
  analytics_listpage: any[] = [];
  analytics_list: any[] = [];
}

@Component({
  selector: 'app-crm-smm-websitechatanalytics',
  templateUrl: './crm-smm-websitechatanalytics.component.html',
  styleUrls: ['./crm-smm-websitechatanalytics.component.scss']
})
export class CrmSmmWebsitechatanalyticsComponent {
  CurObj: IAnalytics = new IAnalytics();
  responsedata: any;
  analytics_summarylist: any[] = [];
  city: any;
  total_users: any;
  chat_analytics1: any[] = [];
  currentDayName: any;
  chat_analytics: any[] = [];
  /////graph////
  weekchart: any = {};
  year: any;
  weekwise_report: any[] = [];
  response_data: any;




  constructor(private formBuilder: FormBuilder, private NgxSpinnerService: NgxSpinnerService, private route: Router, private router: Router, private ToastrService: ToastrService, public service: SocketService, private datePipe: DatePipe) {
    const today = new Date();
    this.currentDayName = today.toLocaleDateString('en-US', { weekday: 'long' });
  }
  ngOnInit(): void {

    this.GetWebsiteUser();
    this.GetWebsiteSummary();
    this.Getweekchart();

    var url6 = 'website/chatSummary'
    this.service.get(url6).subscribe((result,) => {
    });
    var url6 = 'website/Getlistofchat'
    this.service.get(url6).subscribe((result,) => {
    });
    var url6 = 'website/Getlistofthreads'
    this.service.get(url6).subscribe((result,) => {
    });



  }

  Getweekchart() {

    var url = 'website/Getweekwiselist'
    this.service.get(url).subscribe((result: any) => {

      this.response_data = result;

      this.weekwise_report = this.response_data.weekwise_report;
      // this.monthlySalesData = result || [];

      // Transform data for chart
      const categories = this.weekwise_report.map((entry: { week_date: any; }) => entry.week_date);
      const data = this.weekwise_report.map((entry: { week_users: any; }) => entry.week_users);
      console.log(categories)
      console.log(data)
      // Initialize chart options
      this.weekchart = {
        chart: {
          type: 'bar',
          height: 250, // Adjust the height of the chart as needed
          //  width: 600,
          background: 'White',
          foreColor: '#0F0F0F',
          toolbar: {
            show: false, // Set to false to hide the toolbar/menu icon
          },
        },
        colors: ['#8062D6', '#FFD54F', '#66BB6A', '#EF5350'], // Use a set of colors for better combinations
        plotOptions: {
          bar: {
            horizontal: false,
            columnWidth: '10%', // Adjust the width of the bars
            borderRadius: 0, // Add some border radius for a more modern look
          },
        },
        dataLabels: {
          enabled: false, // Disable data labels for a cleaner look
        },
        stroke: {
          show: true,
          width: 1,
          colors: ['transparent'],
        },
        xaxis: {
          categories: categories,
          labels: {
            style: {
              fontWeight: 'bold',
              fontSize: '14px',
              //colors: ['#FF5733', '#33FF57', '#5733FF', '#FFFF33'], // Set different colors for each label
            },
          },
        },
        yaxis: {
          title: {
            text: 'Weeks',
            style: {
              fontWeight: 'bold',
              fontSize: '14px',
              color: '#0F0F0F', // Set a different color for the y-axis title
            },
          },
        },
        series: [
          {
            name: 'Users',
            data: data,
            color: '#096e36', 
          },
        ],
      };
    })
  }
  GetWebsiteUser() {
    this.NgxSpinnerService.show();
    var url = 'website/GetchatAnalyticsUser'
    this.service.get(url).subscribe((result: any) => {
      $('#analytics_summarylist').DataTable().destroy();
      this.responsedata = result;
      this.chat_analytics = this.responsedata.chat_analytics;
      //console.log(this.entity_list)

      setTimeout(() => {
        $('#analytics_summarylist').DataTable();
      }, 1);
    });
    this.NgxSpinnerService.hide();
  }
  GetWebsiteSummary() {
    this.NgxSpinnerService.show();
    var url = 'website/GetchatAnalyticsSummary'
    this.service.get(url).subscribe((result: any) => {
      $('#chat_analytics1').DataTable().destroy();
      this.responsedata = result;
      this.chat_analytics1 = this.responsedata.chat_analytics1;

      //console.log(this.entity_list)
      setTimeout(() => {
        $('#chat_analytics1').DataTable();
      }, 1);
    });
    this.NgxSpinnerService.hide();
  }

}




