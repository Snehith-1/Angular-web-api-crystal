import { Component, OnInit } from '@angular/core';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DefaultGlobalConfig, ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AES, enc } from 'crypto-js';
import { ActivatedRoute, Router } from '@angular/router';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { AngularEditorComponent } from '@kolkov/angular-editor';
import { dE } from '@fullcalendar/core/internal-common';
import { data } from 'jquery';
import { ExcelService } from 'src/app/Service/excel.service';
import  jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable'
@Component({
  selector: 'app-hrm-rpt-monthlyattendance-report',
  templateUrl: './hrm-rpt-monthlyattendance-report.component.html',
  styleUrls: ['./hrm-rpt-monthlyattendance-report.component.scss']
})
export class HrmRptMonthlyattendanceReportComponent {

 
  response_data: any;
  monthlyreportbranch: any[]=[];
  GetMonthlyreportbranch_list : any[] = [];
  GetMailId_list: any[] = [];
  reactiveFormSubmit!: FormGroup;
  productunitclass_list: any[] = [];
  producttype_list: any[] = [];
  productgroup_list: any[] = [];
  GetMonthwiseOrderReport_List: any[] = [];
  months: { name: string, value: string }[] = [
    { name: 'January', value: '1' },
    { name: 'February', value: '2' },
    { name: 'March', value: '3' },
    { name: 'April', value: '4' },
    { name: 'May', value: '5' },
    { name: 'June', value: '6' },
    { name: 'July', value: '7' },
    { name: 'August', value: '8' },
    { name: 'September', value: '9' },
    { name: 'October', value: '10' },
    { name: 'November', value: '11' },
    { name: 'December', value: '12' }
  ];
  constructor(private fb: FormBuilder, 
    private service: SocketService,
    private excelService : ExcelService,
    private ToastrService: ToastrService,
  ) { }
  ngOnInit(): void {

    var api = 'MonthlyAttendanceReport/GetMonthlyReportBranch'
    this.service.get(api).subscribe((result: any) => {
      this.response_data = result;
      this.monthlyreportbranch = this.response_data.GetMonthlyreportbranch_list;
      setTimeout(() => {
        $('#monthlyreportbranch').DataTable();
      }, 1);
    });
    debugger
   // const branch = this.reactiveFormSubmit.value.branch_name;
    const CurrentMonthIndex = new Date().getMonth();
    const CurrentMonth = this.months[CurrentMonthIndex].value;
    const CurrentYear = new Date().getFullYear();
    this.reactiveFormSubmit = this.fb.group(
      {
        month: [CurrentMonth],
        year: [CurrentYear],
        
      }
    );
    this.GetMonthlyreport_summary()

  }
  onclose() {

  }

  GetMonthlyreport_summary() {
    debugger

//     const DaysInMonth = new Date(new Date().getFullYear(), new Date().getMonth() + 1, 0).getDate();
// console.log('Days in Current Month:', DaysInMonth);



    //const selectbranch = this.reactiveFormSubmit.value.branch_gid;
    const selectyear = this.reactiveFormSubmit.value.year;
    const selectmonth = this.reactiveFormSubmit.value.month;

    const selectedDayInMonth = new Date( selectyear, selectmonth, 0).getDate()
    console.log(`Days in Selected Month (${selectmonth + 1}/${selectyear}):`, selectedDayInMonth);
  //   for (let day = 29; day <= 31; day--) {
  //     const thElement = document.querySelector(`#MonthlyDetailsReport th:nth-child(${day})`) as HTMLElement; // Cast to HTMLElement
  //     if (thElement) {
  //         if (day > selectedDayInMonth) {
  //             thElement.style.display = 'none';
  //         } else {
  //             thElement.style.display = ''; // Show the <th> element
  //         }
  //     }
  // }

    let params =
    {
      year: selectyear,
      month: selectmonth,
      //branch : selectbranch
    };

    var api = 'MonthlyAttendanceReport/GetMonthlyReportSummaryDetails'
    this.service.getparams(api, params).subscribe((result: any) => {
      this.response_data = result;
      this.GetMonthwiseOrderReport_List = this.response_data.GetMonthlyDetailsReport_list;
      setTimeout(() => {
        $('#GetMonthwiseOrderReport_List').DataTable();
      }, 1);
    });
  }
  onBranchChange() {

  }

  pdf()
  { debugger
    const doc = new jsPDF() as any
    doc.setPageSize({ width: 595.28, height: 841.89 });
    var prepare: any[][]=[];
    this.GetMonthwiseOrderReport_List.forEach(e=>{
      var tempObj =[];
      tempObj.push(e.user_code);
      tempObj.push(e.user_name);
      tempObj.push(e.date1);
      tempObj.push(e.date2);tempObj.push(e.date3);
      tempObj.push(e.date4);
      tempObj.push(e.date5);tempObj.push(e.date6);
      tempObj.push(e.date7);tempObj.push(e.date8);
      tempObj.push(e.date9);tempObj.push(e.date10);
      tempObj.push(e.date11);tempObj.push(e.date12);
      tempObj.push(e.date13);tempObj.push(e.date14);
      tempObj.push(e.date15);tempObj.push(e.date16);
      tempObj.push(e.date17);tempObj.push(e.date18);
      tempObj.push(e.date19);tempObj.push(e.date20);
      tempObj.push(e.date21);tempObj.push(e.date22);
      tempObj.push(e.date23);tempObj.push(e.date24);tempObj.push(e.date25);
      tempObj.push(e.date26);tempObj.push(e.date27);
      tempObj.push(e.date28);tempObj.push(e.date29);
      tempObj.push(e.date30);tempObj.push(e.date31);
      prepare.push(tempObj);
    });
    console.log(prepare);
    autoTable(doc,{
        head: [['Employee Code','Name','1','2',
 '3','4','5','6','7', '8','9', '10','11', '12',
 '13','14','15','16','17','18','19','20','21',
 '22','23','24','25','26','27','28','29'
 ,'30','31'     
      ]],
        body: prepare,
        styles: {
          cellPadding: 1,
          valign: 'middle', // vertical alignment: 'top', 'middle', 'bottom'
          halign: 'center' // horizontal alignment: 'left', 'center', 'right'
      },
      columnStyles: {
        0: { halign: 'left' , cellWidth: 4 }, // Employee Code column
        1: { halign: 'left' , cellWidth: 4}, // Name column
        // Adjust the alignment for date columns (assuming they start from index 2)
        2: { halign: 'center', cellWidth: 5}, // Date 1
        3: { halign: 'center' , cellWidth: 5 }, // Date 2
        4: { halign: 'center' , cellWidth: 5}, 
        5: { halign: 'center', cellWidth: 5}, 
        6: { halign: 'center', cellWidth: 5 }, 
        7: { halign: 'center', cellWidth: 5}, 
        8: { halign: 'center' , cellWidth: 5}, 
        9: { halign: 'center' , cellWidth: 5}, 
        10: { halign: 'center', cellWidth: 5 }, 
        11: { halign: 'center', cellWidth: 5 }, 
        12: { halign: 'center', cellWidth: 5}, 
        13: { halign: 'center' , cellWidth: 5}, 
        14: { halign: 'center' , cellWidth: 5}, 
        15: { halign: 'center', cellWidth: 5 }, 
        16: { halign: 'center', cellWidth: 5 }, 
        17: { halign: 'center', cellWidth: 5}, 
        18: { halign: 'center' , cellWidth: 5}, 
        19: { halign: 'center' , cellWidth: 5}, 
        20: { halign: 'center' , cellWidth: 5}, 
        21: { halign: 'center' , cellWidth: 5}, 
        22: { halign: 'center', cellWidth: 5 }, 
        23: { halign: 'center', cellWidth: 5 }, 
        24: { halign: 'center', cellWidth: 5}, 
        25: { halign: 'center' , cellWidth: 5}, 
        26: { halign: 'center' , cellWidth: 5}, 
        27: { halign: 'center' , cellWidth: 5}, 
        28: { halign: 'center', cellWidth: 5}, 
        29: { halign: 'center' , cellWidth: 5}, 

        30: { halign: 'center' , cellWidth: 5}, // Date 30
        31: { halign: 'center' , cellWidth: 5}  // Date 31
    },
    margin: { top: 20 }, // adjust top margin to add space between table and top of the page
    startY: 40 //
    });
    doc.save('Attendance_Report' + '.pdf');
  
  }

  exportExcel() :void {
    debugger
   


const AttendanceReport = this.GetMonthwiseOrderReport_List.map(item => ({
  EmployeeCode : item.user_code || '',
EmployeeName: item.user_name || '', 
Date1: item.date1 || '',
Date2: item.date2 || '',
Date3: item.date3 || '',
Date4: item.date4 || '',
Date5: item.date5 || '',
Date6: item.date6 || '',
Date7: item.date7 || '',
Date8: item.date8 || '',
Date9: item.date9 || '',
Date10: item.date10 || '',
Date12: item.date11 || '',
Date13: item.date12 || '',Date14: item.date14 || '',
Date15: item.date15 || '',Date16: item.date16 || '',Date17: item.date17 || '',
Date18: item.date18 || '',Date19: item.date19 || '',
Date20: item.date20 || '',Date21: item.date21 || '',
Date22: item.date22 || '',
Date23: item.date23 || '',Date24: item.date24 || '',
Date25: item.date25 || '',Date26: item.date26 || '',
Date27: item.date27 || '',
Date28: item.date28 || '',
Date29: item.date29 || '',
Date30: item.date30 || '',Date31: item.date31 || '',




}));

    
    this.excelService.exportAsExcelFile(AttendanceReport, 'Attendance_Report');

  }


  
  getColorBasedOnCondition(dataValue: string): string {
    {
      switch (dataValue) {
        case 'WH':
          return 'yellow';
        case 'XX':
          return 'MediumSpringGreen';
        case 'AA':
          return 'orangered';
        case 'CL':
          return 'CornflowerBlue';
        case 'SL':
          return 'CornflowerBlue';
        case 'PL':
          return 'CornflowerBlue';
        case 'LL':
          return 'CornflowerBlue';
        case 'HL':
          return 'orange';
        case 'FH':
          return 'yellow';
        case 'AX':
          return 'yellow';
        case 'AL':
          return 'orange';
        case 'NH':
          return 'yellow';
        case 'DD':
          return 'CornflowerBlue';
        case 'DX':
          return 'CornflowerBlue';
        case 'XD':
          return 'CornflowerBlue';
        case 'EL':
          return 'yellow';
        case 'CO':
          return 'yellow';
        case 'XA':
          return 'orange';
        default:
          return 'transparent';
      }
    }
  }

}
