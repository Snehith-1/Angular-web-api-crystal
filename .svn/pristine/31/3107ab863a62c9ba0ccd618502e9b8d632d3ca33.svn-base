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
import { NgxSpinnerService } from 'ngx-spinner';

import { SharedService } from "src/app/layout/services/shared.service";

@Component({
  selector: 'app-crm-rpt-productgroupreport',
  templateUrl: './crm-rpt-productgroupreport.component.html',
  styleUrls: ['./crm-rpt-productgroupreport.component.scss']
})
export class CrmRptProductgroupreportComponent {
  response_data:any;
  ProductGroupwiseChart_list: any;
  emptyFlag: boolean=false;
  show = true;
  nomonthlyproductstatus: any;
  chartOptions: any = {};
  month1: any;
  monthname1: any;
  GetQuotationForLastSixMonths_List: any;
  chart: ApexCharts | null = null;
  quotation_date: any;
  Quotation_date: any;
  amount: any;
  Amount: any;
  
  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: SocketService,
    private ToastrService: ToastrService,
    public sharedservice: SharedService,
    private datePipe: DatePipe,
    private NgxSpinnerService: NgxSpinnerService) 
  {}

  
  ngOnInit(): void {
    this.GetProductGroupwiseChart()
  }

  // private initializeChart(categories: any, data: any): void {
  //   debugger
  //   this.chartOptions = {
  //     chart: {
  //       type: 'bar',
  //       height: 400,
  //       width: 600,
  //       background: 'White',
  //       foreColor: '#0F0F0F',
  //       fontFamily: 'inherit',
  //       toolbar: {
  //         show: false,
  //       },
  //     },
  //     colors: ['#7FC7D9', '#FFD54F', '#66BB6A', '#EF5350'],
  //     plotOptions: {
  //       bar: {
  //         horizontal: false,
  //         columnWidth: '30%',
  //         borderRadius: 3,
  //       },
  //     },
  //     dataLabels: {
  //       enabled: false,
  //     },
  //     stroke: {
  //       show: true,
  //       width: 2,
  //       colors: ['transparent'],
  //     },
  //     xaxis: {
  //       categories:categories,
  //       labels: {
  //         style: {
  //           fontWeight: 'bold',
  //           fontSize: '14px',
  //         },
  //       },
  //     },
  //     yaxis: {
  //       title: {
  //         style: {
  //           fontWeight: 'bold',
  //           fontSize: '14px',
  //           color: '#7FC7D9',
  //         },
  //       },
  //     },
  //     series: [
  //       {
  //         name: 'Quotation Amount',
  //         data:data,
  //       },
  //     ],
  //   };

  //   // this.renderChart();
  // }

  GetProductGroupwiseChart(){
    // debugger
    // var api = 'ProductReport/GetProductGroupwiseChart';
    // this.service.get(api).subscribe((result:any) => {
    // this.response_data = result;
    // this.ProductGroupwiseChart_list = this.response_data.ProductGroupwiseChart_list; 
    // this.NgxSpinnerService.show();
    debugger
    var url = 'ProductReport/GetProductGroupwiseChart'
    this.service.get(url).subscribe((result: any) => {
      $('#ProductGroupwiseChart_list').DataTable().destroy();
      this.response_data = result;
      this.ProductGroupwiseChart_list = this.response_data.ProductGroupwiseChart_list;
    if(this.ProductGroupwiseChart_list == null) {
      this.nomonthlyproductstatus = 'Product Not Available...';
      this.show = true;
      this.emptyFlag=true;
    }
    else if (this.ProductGroupwiseChart_list.length == 0) {
      this.nomonthlyproductstatus = 'Product Not Available...';
      this.show = true;
      this.emptyFlag=true;
    }
    else {
      debugger
      
        this.quotation_date = this.ProductGroupwiseChart_list.map((entry: { productgroup_name: any }) => entry.productgroup_name),
        this.amount = this.ProductGroupwiseChart_list.map((entry: { amount: any }) => entry.amount)
        this.chartOptions = {
          chart: {
            type: 'bar',
            height: 400,
            width: '100%',
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
              columnWidth: '50%',
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
            categories:this.quotation_date,
            labels: {
              style: {
                
                fontSize: '12px',
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
              name: 'Quotation Amount',
              data:this.amount,
            },
          ],
        };
      
    }
}

  
  ); 
  }
  from_date(){

  }

  to_date(){

  }

  onSearchClick(){
    debugger
    // this.NgxSpinnerService.show();
    var url= 'ProductReport/GetProductGroupwiseChartSearch';
    let params ={
      from_date:this.from_date,
      to_date:this.to_date
    }
    
    this.service.getparams(url,params).subscribe((result: any) => {
      $('#ProductGroupwiseChart_list').DataTable().destroy();
      this.response_data = result;
      this.ProductGroupwiseChart_list = this.response_data.ProductGroupwiseChart_list;
    
      if(this.ProductGroupwiseChart_list == null) {
        debugger
        this.nomonthlyproductstatus = 'Product Not Available...';
        this.show = true;
        this.emptyFlag=true;
        this.chartOptions = {
          chart: {
            type: 'bar',
            height: 400,
            width: '100%',
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
              columnWidth: '50%',
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
            categories:'0',
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
              name: 'Quotation Amount',
              data:'0',
            },
          ],
        };
      }
      else if (this.ProductGroupwiseChart_list.length == 0) {
        debugger
        this.nomonthlyproductstatus = 'Product Not Available...';
        this.show = true;
        this.emptyFlag=true;
        this.chartOptions = {
          chart: {
            type: 'bar',
            height: 400,
            width: '100%',
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
              columnWidth: '50%',
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
            categories:'0',
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
              name: 'Quotation Amount',
              data:'0',
            },
          ],
        };
      }
      else {
        debugger
        this.emptyFlag=false;
          this.Quotation_date = this.ProductGroupwiseChart_list.map((entry: { productgroup_name: any }) => entry.productgroup_name),
          this.Amount = this.ProductGroupwiseChart_list.map((entry: { amount: any }) => entry.amount)
          this.chartOptions = {
            chart: {
              type: 'bar',
              height: 400,
              width: '100%',
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
                columnWidth: '50%',
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
              categories:this.Quotation_date,
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
                name: 'Quotation Amount',
                data:this.Amount,
              },
            ],
          };
        
      }
    });
    }


  onrefreshclick(){
    this.from_date = null!;
    this.to_date = null!;
    this.GetProductGroupwiseChart()
    window.location.reload();
  }
}
