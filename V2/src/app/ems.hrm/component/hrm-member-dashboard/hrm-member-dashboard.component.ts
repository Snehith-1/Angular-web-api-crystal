import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service'; 
import { ToastrService } from 'ngx-toastr';
import { ApexChart, ApexNonAxisChartSeries, ApexResponsive } from 'ng-apexcharts';
import { NgxSpinnerService } from 'ngx-spinner';
interface MonthlyChartDataItem {
  monthname: string;
  countPresentm: string;
  countAbsentm: string;
  // ... other properties ...
}

export type ChartOptions1 = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};

@Component({
  selector: 'app-hrm-member-dashboard',
  templateUrl: './hrm-member-dashboard.component.html',
  styleUrls: ['./hrm-member-dashboard.component.scss']
})

export class HrmMemberDashboardComponent {

  chartOptions1: any = {};
  chartOptions2: any = {};
  response_data: any;

// Declare and initialize variables
totalDays: number = 0;
countPresent: number = 0;
countAbsent: number = 0;
countLeave: number = 0;
countholiday: number = 0;
countWeekOff: number = 0;

  DashboardCount_List: any[] = [];
  DashboardQuotationAmt_List: any;
  noquotation: any;
  year: any;
  noquotation_status: any;
  show = true;
  Date: string;
  buttonClicked: boolean | undefined;
  button1Clicked: boolean | undefined;
  onduty_details : any;
  responsedata: any;
  loginsummary_list: any;
  logoutsummary_list: any;
  ondutysummary_list: any;
  compoffsummary_list: any;
  permissionsummary_list: any;
  login_time_audit: any;
  logout_time_audit: any;
  applyLoginReqForm!: FormGroup;
  applyLogoutReqForm!: FormGroup;
  applyODForm!: FormGroup;
  applycompoffForm!: FormGroup;
  applypermissionForm!: FormGroup;

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: SocketService,
    private ToastrService: ToastrService,
    private NgxSpinnerService: NgxSpinnerService) {
    this.Date = new Date().toString();

    this.applyLoginReqForm = new FormGroup({
      loginreq_date: new FormControl(''),
      logintime: new FormControl(''),
      loginreq_reason: new FormControl(''),
      login_date: new FormControl('')
    })

    this.applyLogoutReqForm = new FormGroup ({
      logoutattendence_date : new FormControl(''),
      logouttime : new FormControl(''),
      logouttime_reason : new FormControl(''),
    })

    this.applyODForm = new FormGroup ({
      od_date : new FormControl(''),
      od_fromhr : new FormControl(''),
      od_tohr : new FormControl(''),
      onduty_period : new FormControl(''),
      od_session : new FormControl(''),
      od_reason : new FormControl(''),
    })

    this.applycompoffForm = new FormGroup ({
      actualworking_date : new FormControl(''),
      compensatoryoff_applydate : new FormControl(''),
      compoff_reason : new FormControl(''),
    })

    this.applypermissionForm = new FormGroup ({
      permission_date : new FormControl(''),
      permission_fromhr : new FormControl(''),
      permission_tohr : new FormControl(''),
      permission_reason : new FormControl(''),
    })

    HrmMemberDashboardComponent.constructor(); {
      setInterval(() => {
        this.Date = new Date().toString();
      }, 1000);
    }
  }

  ngOnInit(): void {
   
    this.chartOptions2 = getbarChartOptions(350);

    setInterval(() => {
      this.Date = new Date().toString();
    }, 1000);


    const labelColor = '#000';
    const borderColor = '#e6ccb2';
    const baseColor = '#89DA59';
    const secondaryColor = '#FF420E'

     var api = 'hrmTrnDashboard/monthlyAttendence';
    this.service.getdtl(api).subscribe((result: any) => {
      this.responsedata = result;
      this.DashboardCount_List = this.responsedata.last6MonthAttendence_list;

      // this.totalDays = Number(this.DashboardCount_List[0].totalDays);
      // this.countPresent = Number(this.DashboardCount_List[0].countPresent);
      // this.countAbsent = Number(this.DashboardCount_List[0].countAbsent);
      // this.countLeave = Number(this.DashboardCount_List[0].countLeave);
      // this.countholiday = Number(this.DashboardCount_List[0].countholiday);
      // this.countWeekOff = Number(this.DashboardCount_List[0].countWeekOff);
      
      this.totalDays = 38;
      this.countPresent = 30;
      this.countAbsent = 4;
      this.countLeave =4;
      this.countholiday = 8
      this.countWeekOff = 0;
      
      if(this.totalDays==0){
        this.totalDays=1;
      }

      // this.chartOptions1 = {
      //   series: [this.totalDays, this.countPresent, this.countAbsent, this.countLeave, this.countholiday, this.countWeekOff],
      //   chart: {
      //     width: 430,
      //     type: "pie"
      //   },
      //   labels: ["Total No.Of Days:", "No.Of Days Present:", "No.Of Days Absent:", "No.Of Days Leave:", "No.Of Days Holiday:", "No.Of Days WeekOff:"],
      //   responsive: [
      //     {
      //       breakpoint: 500,
      //       options: {
      //         chart: {
      //           width: 250,

      //         },
      //         legend: {
      //           position: "bottom"
      //         }
      //       }
      //     }
      //   ]
      // };
       this.chartOptions1 = {
        series: [this.totalDays, this.countPresent, this.countAbsent, this.countLeave, this.countholiday, this.countWeekOff],
        chart: {
          width: 430,
          type: "pie"
        },
        labels: ["Total No.Of Days:", "No.Of Days Present:", "No.Of Days Absent:", "No.Of Days Leave:", "No.Of Days Holiday:", "No.Of Days WeekOff:"],
        responsive: [
          {
            breakpoint: 500,
            options: {
              chart: {
                width: 250,

              },
              legend: {
                position: "bottom"
              }
            }
          }
        ]
      };

      const monthlyChartData: MonthlyChartDataItem[] =this.responsedata.last6MonthAttendence_list;

      // Format the data to match the structure expected by chartOptions2
      const formattedMonthlyChartData = {
        // present: monthlyChartData.map(item => Number(item.countPresentm)),
        // absent: monthlyChartData.map(item => Number(item.countAbsentm)),
        //present: monthlyChartData.map(item => Number(item.countPresentm)),
        //absent: monthlyChartData.map(item => Number(item.countAbsentm)),
      };
      this.chartOptions2 = {
        chart: {
          fontFamily: 'inherit',
          type: 'bar',
          height: 212,
          toolbar: {
            show: false,
          },
        },
    
        colors: [baseColor, secondaryColor, borderColor],
    
        series: [
          {
            name: 'Present',
           data: [20, 25, 22, 24, 20, 25, 22, 24, 20, 30, 3, 0],
          },
          {
            name: 'Absent',
           data: [10, 5, 8, 0, 10, 5, 8, 6, 0, 0, 0, 0],
          },
        ],
    
        plotOptions: {
          bar: {
            columnWidth: '85%',
          },
        },
    
        dataLabels: {
          enabled: false,
        },
    
        legend: {
          show: true,
        },
    
        xaxis: {
          categories: monthlyChartData.map(item => item.monthname),
          axisBorder: { show: false, },
          axisTicks: { show: false, },
    
          labels: {
            style: {
              colors: labelColor,
              fontSize: '12px',
            },
          },
        },
    
        yaxis: {
          min: 0,
          max: 30,
          tickAmount: 3,
    
          labels: {
            style: {
              colors: labelColor,
              fontSize: '12px',
            },
          },
        },
    
      }
    });


    var api = 'hrmTrnDashboard/loginSummary';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.loginsummary_list = this.responsedata.loginsummary_list;

      setTimeout(() => {
        $('#login').DataTable();
      }, 1);
    });

    var api = 'hrmTrnDashboard/logoutSummary';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.logoutsummary_list = this.responsedata.logoutsummary_list;

      setTimeout(() => {
        $('#logout').DataTable();
      }, 1);
    });

    var api = 'hrmTrnDashboard/ondutySummary';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.ondutysummary_list = this.response_data.onduty_details;

      setTimeout(() => {
        $('#onduty').DataTable();
      }, 1);
    });

    var api = 'hrmTrnDashboard/compOffSummary';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.compoffsummary_list = this.responsedata.compoffSummary_details;

      setTimeout(() => {
        $('#compoff').DataTable();
      }, 1);
    });

    var api = 'hrmTrnDashboard/permissionSummary';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.permissionsummary_list = this.responsedata.permissionSummary_details;

      setTimeout(() => {
        $('#permission').DataTable();
      }, 1);
    });
  }


  punchin() {
    this.buttonClicked = !this.buttonClicked;

    var param = {
      login_time_audit: this.login_time_audit
    }

    var url = 'hrmTrnDashboard/punchIn';
    this.service.post(url, param).subscribe((result: any) => {
      if (result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }

  punchout() {
    this.button1Clicked = !this.button1Clicked;

    var param = {
      login_time_audit: this.login_time_audit
    }
    window.location.reload();
  }

  onapplylogin() {
    debugger;
    var url = 'hrmTrnDashboard/applyLoginReq';
    this.NgxSpinnerService.show();
    this.service.post(url, this.applyLoginReqForm.value).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      if (result.status == true) {
        this.ToastrService.success(result.message)
        
      }
      else {
        this.ToastrService.warning(result.message)
        
      }
    });
  }

  onclose(){

  }

  onapplylogout() {
    var url = 'hrmTrnDashboard/applyLogoutReq';
    this.service.post(url,this.applyLogoutReqForm.value).subscribe((result: any) => {
      if (result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }

  onapplyod() {
    var url = 'hrmTrnDashboard/applyonduty';
    this.service.post(url,this.applyODForm.value).subscribe((result: any) => {
      if (result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }

  onapplycompoff(){
    var url = 'hrmTrnDashboard/applyCompoffReq';
    this.service.post(url,this.applycompoffForm.value).subscribe((result: any) => {
      if (result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }

  onapplypermission() {
    var url = 'hrmTrnDashboard/applyPermission';
    this.service.post(url, this.applypermissionForm.value).subscribe((result: any) => {
      if (result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }

  myprofile() {
    this.router.navigate(['/hrm/Employeeprofile'])
  }

  myleave() {
    this.router.navigate(['/hrm/HrmMyLeave'])
  }

 approveleave () {
    this.router.navigate(['/hrm/HrmApproveLeave'])
  }

  companypolicies() {
    this.router.navigate(['/hrm/HrmTrnCompanyPolicy'])
  }

  officecalendar() {
    this.router.navigate(['/hrm/HrmOfficeCalendar'])
  }

  monthlyattendance() {
    this.router.navigate(['/hrm/HrmMonthlyAttendance'])
  }
}

// function getpieChartOptions(
//   totalDays: number,
//   countPresent: number,
//   countAbsent: number,
//   countLeave: number,
//   countholiday: number,
//   countWeekOff: number
// ) 
//   {
   
//   return {
    
//     series: [totalDays,countPresent,countAbsent,countLeave,countholiday, countWeekOff],
//     chart: {
//       width: 430,
//       type: "pie"
//     },
//     labels: ["Total No.Of Days:", "No.Of Days Present:", "No.Of Days Absent:", "No.Of Days Leave:", "No.Of Days Holiday:", "No.Of Days WeekOff:"],
//     responsive: [
//       {
//         breakpoint: 500,
//         options: {
//           chart: {
//             width: 250,

//           },
//           legend: {
//             position: "bottom"
//           }
//         }
//       }
//     ]
//   };
// }

function getbarChartOptions(height: number) {
  const labelColor = '#000';
  const borderColor = '#e6ccb2';
  const baseColor = '#89DA59';
  const secondaryColor = '#FF420E'

  return {
      };
}
