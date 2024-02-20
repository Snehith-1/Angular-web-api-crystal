import { Component, } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators,} from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { data } from 'jquery';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';


@Component({
  selector: 'app-smr-rpt-orderreport',
  templateUrl: './smr-rpt-orderreport.component.html',
  styleUrls: ['./smr-rpt-orderreport.component.scss']
})

export class SmrRptOrderreportComponent {
  chartOptions: any;
  Date: string;
  chart: ApexCharts | null = null;
  GetOrderForLastSixMonths_List :any;
  GetOrderDetailSummary :any;
  reactiveForm: FormGroup | any;
  responsedata: any;
  salesteamgrid_list :any;
  getData: any;
  salesorder_gid : any;
  data: any;  
  parameterValue: any;
  from_date!:string;
  to_date!:string;
  

  constructor(private formBuilder: FormBuilder,public route:ActivatedRoute,public service :SocketService,private router:Router,private ToastrService: ToastrService) {
    this.Date = new Date().toString();
    
    
    

  }
  

  ngOnInit(): void {
    
    this.GetOrderForLastSixMonths();
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    flatpickr('.date-picker', options);
  }
  GetOrderForLastSixMonths( )

 {
  debugger
  var url = 'SmrRptOrderReport/GetOrderForLastSixMonths'
  this.service.get(url).subscribe((result: any) => {
    $('#GetOrderForLastSixMonths_List').DataTable().destroy();
    this.responsedata = result;
    this.GetOrderForLastSixMonths_List = this.responsedata.GetOrderForLastSixMonths_List;
    const categories = this.GetOrderForLastSixMonths_List.map((entry: { salesorder_date: any; }) => entry.salesorder_date);
    const data = this.GetOrderForLastSixMonths_List.map((entry: { amount: any; }) => entry.amount);

    // this.chartOptions.xaxis.categories = categories;
    // this.chartOptions.series[0].data = data;
    // setTimeout(() => {
    //   $('#GetOrderForLastSixMonths_List');
    // }, 1);
    this.chartOptions={
      chart: {
        type: 'bar',
        height: 400,
        width: 570,
        background: 'White',
        foreColor: '#0F0F0F',
        fontFamily: 'inherit',
        toolbar: {
          show: false,
        },
      },
      colors: ['#7FC7D9', '#FFD54F', '#66BB6A', '#EF5350'],
      plotOptions: {
        bar: {
          horizontal: false,
          columnWidth: '25%',
          borderRadius: 3,
        },
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
        categories:categories,
        labels: {
          style: {
            fontWeight: 'bold',
            fontSize: '14px',
          },
        },
      },
      yaxis: {
        title: {
          style: {
            fontWeight: 'bold',
            fontSize: '14px',
            color: '#7FC7D9',
          },
        },
      },
      series: [
        {
          name: 'Sales Amount',
          color:'#87CEEB',
          data:data,
        },
      ],
    };
    this.renderChart()
  
  })

}
private renderChart(): void {
  if (this.chart) {
    this.chart.updateOptions(this.chartOptions); // Update existing chart with new options
  } else {
    this.chart = new ApexCharts(document.getElementById('chart'), this.chartOptions);
    this.chart.render();
  }

}
onSearchClick(){
  debugger
  var url='SmrRptOrderReport/GetOrderReportForLastSixMonthsSearch';
    let params ={
      from_date:this.from_date,
      to_date:this.to_date
    }
    this.service.getparams(url,params).subscribe((result: any) => {
      $('#GetOrderForLastSixMonths_List').DataTable().destroy();
      this.responsedata = result;
      this.GetOrderForLastSixMonths_List = this.responsedata.GetOrderForLastSixMonths_List;
      ApexCharts.exec('chart', 'destroy');
      this.chartOptions.xaxis.categories = this.GetOrderForLastSixMonths_List.map((entry: { salesorder_date: any }) => entry.salesorder_date);
    this.chartOptions.series[0].data = this.GetOrderForLastSixMonths_List.map((entry: { amount: any }) => entry.amount);


    
    this.renderChart()
    })

}
onrefreshclick(){
  this.from_date = null!;
    this.to_date = null!;
    this.GetOrderForLastSixMonths()
    window.location.reload()
}

ondetail(month: any,year:any ,parameter: string,quotation_gid: string) {
  debugger
  var url = 'SmrRptOrderReport/GetOrderDetailSummary'
  let param = {
    quotation_gid : quotation_gid ,
    month: month, year: year
  }
  this.service.getparams(url, param).subscribe((result: any) => {
    this.responsedata = result;
    this.GetOrderDetailSummary = result.GetOrderDetailSummary;
    console.log(this.GetOrderDetailSummary)
    setTimeout(() => {
      $('#GetOrderDetailSummary');
    }, 1);

  });
}
}
// function getChartOptions(height: number) {
//   const labelColor = '#000000';
//   const borderColor = '#e6ccb2';
//   const strokeColor = '#6e0a0a';
//   const color = '#06a813';

  // this.chartOptions={
  //   chart: {
  //     fontFamily: 'inherit',
  //     type: 'area',
  //     height: 400,
  //     toolbar: { show: false },
  //     sparkline: { enabled: false },
  //   },

  //   xaxis: {
  //     categories:categories,
  //     axisBorder: { show: false },
  //     axisTicks: { show: false },
  //     labels: {
  //       show: true,
  //       style: { colors: "#000000", fontSize: '12px' },
  //     },
  //     crosshairs: {
  //       show: false,
  //       position: 'front',
  //       stroke: {
  //         color: "#e6ccb2",
  //         width: 1,
  //         dashArray: 3,
  //       },
  //     },
  //   },

  //   series: [
  //     {
  //       name: 'Total Sales',
  //       data:data,
  //     },
  //   ],

  //   legend: {
  //     show: false,
  //   },

  //   dataLabels: {
  //     enabled: false,
  //   },

  //   fill: {
  //     type: 'solid',
  //     opacity: 0,
  //   },

  //   stroke: {
  //     curve: 'smooth',
  //     show: true,
  //     width: 3,
  //     colors: "#6e0a0a",
  //   },

  //   yaxis: {
  //     labels: {
  //       show: true,
  //       style: {
  //         colors: "#000000",
  //         fontSize: '12px',
  //       },
  //     },
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

  //   tooltip: {
  //     style: {
  //       fontSize: '12px',
  //     },
  //     y: {
  //       formatter: function (val: number) {
  //         return val;
  //       },
  //     },
  //     marker: {
  //       show: false,
  //     },
  //   },

  //   colors: ['transparent'],
  //   markers: {
  //     colors: "#6a813",
  //     strokeColors: "#6e0a0a",
  //     strokeWidth: 3,
  //   },
  // };







