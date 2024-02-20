import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Subscription, Observable, timer } from 'rxjs';
import { map, share } from "rxjs/operators";
import { DatePipe } from '@angular/common';
import { environment } from 'src/environments/environment';
import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart
} from "ng-apexcharts";

import * as ApexCharts from 'apexcharts';

export class IAnalytics {
  analytics_listuser: any[] = [];
  analytics_listpage: any[] = [];
  analytics_list: any[] = [];
}

@Component({
  selector: 'app-crm-smm-website',
  templateUrl: './crm-smm-website.component.html',
  styleUrls: ['./crm-smm-website.component.scss']
})


export class CrmSmmWebsiteComponent {
  width: any;
  width1: any;
  analytics_list: any[] = [];
  CurObj: IAnalytics = new IAnalytics();
  responseData: any;
  responsedata: any;
  analytics_summarylist: any[] = [];
  eventCount: any;
  direct: any;
  total_user: any;
  referral: any;
  city: any;
  total_users: any;
  analytics_summarylist1: any[] = [];
  google_list: any;
  time = new Date();
  rxTime = new Date();
  currentDayName: any;
  toDate: any;
  intervalId: any;
  subscription!: Subscription;
  analytics_summarylist2: any[] = [];
  analytics_summarylist3: any[] = [];
  organic: any;
  newUsers: any;
  /////graph////
  weekchart: any = {};
  monthchart: any = {};
  yearchart: any = {};
  emptyFlag: boolean = false;
  emptyFlag1: boolean = false;
  year: any;
  analytics_report: any[] = [];
  noleadstatus: any;
  response_data: any;
  show = true;
  week_date: any;
  week_users: any;
  totalHours: any;
  daywisechart: any = {};
  WebsiteUserchart: any = {};
  WebsiteSessionchart: any = {};
  selectedChartType: any;
  
  
  constructor(private formBuilder: FormBuilder, private NgxSpinnerService: NgxSpinnerService, private route: Router, private router: Router, private ToastrService: ToastrService, public service: SocketService, private datePipe: DatePipe) {
    const today = new Date();
    this.currentDayName = today.toLocaleDateString('en-US', { weekday: 'long' });
  }
  ngOnInit(): void {

    this.toDate = this.datePipe.transform(new Date(), 'dd-MM-yyyy');
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

    async function getSheetDataUser() {
      
    
      try {
        const response = await fetch(environment.scriptUrluser);

        if (response.ok) {
          const responseData = await response.json();
          return responseData
          // Handle your data here
        } else {
          console.error(`Failed to fetch data from the script. Status: ${response.status}`);
        }
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    }
    getSheetDataUser()
      .then((data) => {
        this.CurObj.analytics_list = data;
        //console.log('googleuser', this.CurObj.analytics_list);

        // let newUsers = this.CurObj.analytics_list.reduce((acc, item) => acc + item.newUsers, 0);
        // console.log(newUsers);
        // let total_user = this.CurObj.analytics_list.reduce((acc, item) => acc + item.activeUsers, 0);
        // console.log(total_user);


        // const chunkSize = 10; // Change this to the desired chunk size
        // const chunkedArrays: any[][] = [];
        // for (let i = 0; i < this.CurObj.analytics_list.length; i += chunkSize) {
        //   const chunk = this.CurObj.analytics_list.slice(i, i + chunkSize);
        //   chunkedArrays.push(chunk);
        // }



        console.log();
        var url = "WebsiteAnalytics/PostWebsiteAnalyticsUser"
        this.service.post(url, this.CurObj).pipe().subscribe((result: any) => {
        });
      })
      .catch((error) => {
        console.error('Error:', error);
      });

    async function getSheetDataPage() {
      const scriptUrlpage = environment.scriptUrlpage;

      try {
        const response = await fetch(scriptUrlpage);

        if (response.ok) {
          const responseData = await response.json();
          return responseData
          // Handle your data here
        } else {
          console.error(`Failed to fetch data from the script. Status: ${response.status}`);
        }
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    }
    getSheetDataPage()
      .then((data) => {
        this.CurObj.analytics_list = data;
        //console.log('googlepage', this.CurObj.analytics_list);
        var url = "WebsiteAnalytics/PostWebsiteAnalyticsPage"
        this.service.post(url, this.CurObj).pipe().subscribe((result: any) => {
        });
      })
      .catch((error) => {
        console.error('Error:', error);
      });


    this.GetWebsiteSessionchart();
    this.GetWebsiteUserchart();
    this.GetWebsiteSummary();
    this.Getweekchart();
    this.Getmonthchart();
    this.Getyearchart();
    this.GetWebsitePageSessions();
    this.GetWebsiteAnalyticstiles();
    this.Getdaywisechart();
   
   
  }
  Getdaywisechart(){
    var url = 'WebsiteAnalytics/Getdaywisechart'
    this.service.get(url).subscribe((result: any) => {

      this.response_data = result;

      this.analytics_report = this.response_data.analytics_report;
      // this.monthlySalesData = result || [];

      // Transform data for chart
      const categories = this.analytics_report.map((entry: { daily_date: any; }) => entry.daily_date);
      const data = this.analytics_report.map((entry: { daily_users: any; }) => entry.daily_users);
      // console.log(categories)
      // console.log(data)
      // Initialize chart options
      this.daywisechart = {
        chart: {
          type: 'bar',
          height: 440, // Adjust the height of the chart as needed
          // width: 600,
          background: 'White',
          foreColor: '#0F0F0F',
          toolbar: {
            show: false, // Set to false to hide the toolbar/menu icon
          },
        },
        colors: ['#8062D6', '#FFD54F', '#66BB6A', '#EF5350','#0F0F0F'], // Use a set of colors for better combinations
        plotOptions: {
          bar: {
            horizontal: false,
            columnWidth: '1%', // Adjust the width of the bars
            borderRadius: 0, // Add some border radius for a more modern look
          },
        },
        dataLabels: {
          enabled: false, // Disable data labels for a cleaner look
        },
        stroke: {
          show: true,
          width: 2,
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
            color: '#b55604', //#1a70d9
          },
        ],
      };

    })
  }

  Getweekchart() {

    var url = 'WebsiteAnalytics/Getweekwisechart'
    this.service.get(url).subscribe((result: any) => {

      this.response_data = result;

      this.analytics_report = this.response_data.analytics_report;
      // this.monthlySalesData = result || [];

      // Transform data for chart
      const categories = this.analytics_report.map((entry: { week_date: any; }) => entry.week_date);
      const data = this.analytics_report.map((entry: { week_users: any; }) => entry.week_users);
      // console.log(categories)
      // console.log(data)
      // Initialize chart options
      this.weekchart = {
        chart: {
          type: 'bar',
          height: 400, // Adjust the height of the chart as needed
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
          width: 2,
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
  Getmonthchart() {

    var url = 'WebsiteAnalytics/Getmonthwisechart'
    this.service.get(url).subscribe((result: any) => {

      this.response_data = result;

      this.analytics_report = this.response_data.analytics_report;
      // this.monthlySalesData = result || [];

      // Transform data for chart
      const categories = this.analytics_report.map((entry: { Months: any; }) => entry.Months);
      const data = this.analytics_report.map((entry: { users: any; }) => entry.users);
      // console.log(categories)
      // console.log(data)
      // Initialize chart options
      this.monthchart = {
        chart: {
          type: 'bar',
          height: 400, // Adjust the height of the chart as needed
          // width: 600,
          background: 'White',
          foreColor: '#0F0F0F',
          toolbar: {
            show: false, // Set to false to hide the toolbar/menu icon
          },
        },
        colors: ['#8062D6', '#FFD54F', '#66BB6A', '#EF5350','#0F0F0F'], // Use a set of colors for better combinations
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
          width: 2,
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
            color: '#5d1d82', //#1a70d9
          },
        ],
      };

    })
  }

  Getyearchart() {
    var url = 'WebsiteAnalytics/Getyearwisechart'
    this.service.get(url).subscribe((result: any) => {

      this.response_data = result;

      this.analytics_report = this.response_data.analytics_report;
      // this.monthlySalesData = result || [];

      // Transform data for chart
      const categories = this.analytics_report.map((entry: { year: any; }) => entry.year);
      const data = this.analytics_report.map((entry: { year_users: any; }) => entry.year_users);
      // console.log(categories)
      // console.log(data)
      // Initialize chart options
      this.yearchart = {
        chart: {
          type: 'bar',
          height: 400, // Adjust the height of the chart as needed
          // width: 1100,
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
          width: 2,
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
            color: '#91346c', //#053ef7
          },
        ],
      };

    })
  }
  GetWebsiteSessionchart() {
    this.NgxSpinnerService.show();
    var url = 'WebsiteAnalytics/GetWebsiteSessionchart'
    this.service.get(url).subscribe((result: any) => {

      this.response_data = result;

      this.analytics_report = this.response_data.analytics_summarylist;
      // this.monthlySalesData = result || [];

      // Transform data for chart
      const categories = this.analytics_report.map((entry: { CampaignName: any; }) => entry.CampaignName);
      const data = this.analytics_report.map((entry: { eventCount: any; }) => entry.eventCount);
      // console.log(categories)
      // console.log(data)
      // Initialize chart options
      this.WebsiteSessionchart = {
        chart: {
          type: 'bar',
          height: 150, // Adjust the height of the chart as needed
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
            horizontal: true,
            columnWidth: '5%', // Adjust the width of the bars
            borderRadius: 0, // Add some border radius for a more modern look
          },
        },
        dataLabels: {
          enabled: false, // Disable data labels for a cleaner look
        },
        stroke: {
          show: true,
          width: 2,
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
            color: '#5733FF',
          },
        ],
      };
    })
    this.NgxSpinnerService.hide();
  }

  GetWebsiteUserchart() {
    this.NgxSpinnerService.show();
    var url = 'WebsiteAnalytics/GetWebsiteUserchart'
    this.service.get(url).subscribe((result: any) => {

      this.response_data = result;

      this.analytics_report = this.response_data.analytics_summarylist2;
      // this.monthlySalesData = result || [];

      // Transform data for chart
      const categories = this.analytics_report.map((entry: { city: any; }) => entry.city);
      const filteredDatacity = categories.slice(0, 20);
      const data = this.analytics_report.map((entry: { total_users: any; }) => entry.total_users);
      const filteredData = data.slice(0, 20);
      // console.log(categories)
      // console.log(data)
      // Initialize chart options
      this.WebsiteUserchart = {
        chart: {
          type: 'bar',
          height: 300, // Adjust the height of the chart as needed
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
            horizontal: true,
            columnWidth: '5%', // Adjust the width of the bars
            borderRadius: 0, // Add some border radius for a more modern look
          },
        },
        dataLabels: {
          enabled: false, // Disable data labels for a cleaner look
        },
        stroke: {
          show: true,
          width: 2,
          colors: ['transparent'],
        },
        xaxis: {
          categories: filteredDatacity,
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
            data: filteredData,
            color: '#ad8ad4',
          },
        ],
      };
    })
    this.NgxSpinnerService.hide();
  }

  GetWebsiteSummary() {
    this.NgxSpinnerService.show();
    var url = 'WebsiteAnalytics/GetWebsiteAnalyticsSummary'
    this.service.get(url).subscribe((result: any) => {
      $('#analytics_summarylist1').DataTable().destroy();
      this.responsedata = result;
      this.analytics_summarylist1 = this.responsedata.analytics_summarylist1;

      //console.log(this.entity_list)
      setTimeout(() => {
        $('#analytics_summarylist1').DataTable();
      }, 1);
    });
    this.NgxSpinnerService.hide();
  }
  GetWebsiteAnalyticstiles() {
   
    var url = 'WebsiteAnalytics/GetWebsiteAnalyticstiles'

    this.service.get(url).subscribe((result: any) => {
      this.analytics_report = result.analytics_summarylist;
      this.newUsers = this.analytics_report[0].newUsers;
      this.total_user = this.analytics_report[0].total_user

      
    });
}
  GetWebsitePageSessions() {
    this.NgxSpinnerService.show();
    var url = 'WebsiteAnalytics/GetWebsiteAnalyticsPageSessions'
    this.service.get(url).subscribe((result: any) => {
      $('#analytics_summarylist3').DataTable().destroy();
      this.responsedata = result;
      this.analytics_summarylist3 = this.responsedata.analytics_summarylist3;
      // this.totalHours = this.analytics_summarylist3[0].totalHours;
      // this.totalHours = this.totalHours .split('.');

      // console.log('gyfvyf',this.totalHours[0])

      // setTimeout(() => {
      //   $('#analytics_summarylist3').DataTable();
      // }, 1);
    });
    this.NgxSpinnerService.hide();
  }


updateChart(chartType: string) {
  this.selectedChartType = chartType;
  switch (chartType) {
    case 'DayWise':
      this.Getdaywisechart();
      break;
    case 'week':
      this.Getweekchart();
      break;
    case 'month':
      this.Getmonthchart();
      break;
    case 'year':
      this.Getyearchart();
      break;
    default:
      this.daywisechart();
      break;
  }
}

}

