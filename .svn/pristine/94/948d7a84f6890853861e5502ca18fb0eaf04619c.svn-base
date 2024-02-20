import { Component, ElementRef, ViewChild  } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { environment } from 'src/environments/environment.development';
import { ExcelService } from 'src/app/Service/excel.service';
import { get } from 'jquery';
import  jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';


interface IPaymentSummaryReport {
  branch_name: string;
  department_name: string;
  month: string;
  year: string;
  branch_gid: string;
  department_gid: string;
  salary_gid:string;
}

@Component({
  selector: 'app-pay-rpt-paymentreportsummary',
  templateUrl: './pay-rpt-paymentreportsummary.component.html',
  styleUrls: ['./pay-rpt-paymentreportsummary.component.scss']
})
export class PayRptPaymentreportsummaryComponent {
  [x: string]: any;
  reactiveForm!: FormGroup;
  branch_name: any;
  branchlist : any[] = [];
  paymentreport_list : any[] = [];
  Reportpayment_list: any[] = [];
  PaymentSummaryReport!: IPaymentSummaryReport;
  responsedata: any;
  overall: any; 
  grandtotal:any;

  constructor(private formBuilder: FormBuilder,
    private excelService : ExcelService,
     private route: ActivatedRoute,
     private router: Router, private ToastrService: ToastrService, 
     public service: SocketService) {
    this.PaymentSummaryReport = {} as IPaymentSummaryReport;
    }

  ngOnInit(): void {

    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    flatpickr('.date-picker', options);
    this.reactiveForm = new FormGroup({

      branch_name : new FormControl(this.PaymentSummaryReport.branch_name, [
        Validators.required,
        Validators.minLength(1),
      ]),
    });
  

    var api='PayRptPayrunSummary/GetBranchDtl'
    this.service.get(api).subscribe((result:any)=>{
    this.branchlist = result.GetBranchDtl;
    //console.log(this.branchlist)
   });
  
      //// Summary Grid//////
         
         var url = 'PayTrnReportPayment/GetPaymentSummary'
         this.service.get(url).subscribe((result: any) => {
     
           this.responsedata = result;
           this.paymentreport_list = this.responsedata.GetPaylist;
          //  this.overall=0;
           debugger;
           for (let i = 0; i < this.paymentreport_list.length; i++) {
            // Ensure the Amount property is parsed as a number
            const amount = parseFloat(this.paymentreport_list[i].Amount);
            
            // Check if the parsed amount is a valid number
            if (!isNaN(amount)) {
              this.overall = this.overall + amount; // Add the numeric amount to overall
            }
          }
          this.overall=this.formatCurrency(this.overall)
          
           
           setTimeout(() => {
             $('#paymentreport_list').DataTable();
           }, );
     
     
         });
       }

       ondetail(month: any,year:any) {
        debugger;
        var url = 'PayTrnReportPayment/GetreportPaymentExpand'
        let param = {
          month : month, 
          year : year 
        }
        this.service.getparams(url, param).subscribe((result: any) => {
          debugger;
        this.Reportpayment_list = result.GetreportExpand;
        
          });
      }

      exportExcel() :void {
        debugger
        //var api7 = 'PayTrnReportPayment/GetReportExportExcel'
        // this.service.generateexcel(api7).subscribe((result: any) => {
        //   this.responsedata = result;
        //   var phyPath = this.responsedata.GetreportExportExcel[0].lspath1;
        //   var relPath = phyPath.split("src");
        //   var hosts = window.location.host;
        //   var prefix = location.protocol + "//";
        //   var str = prefix.concat(hosts, relPath[1]);
        //   var link = document.createElement("a");
        //   var name = this.responsedata.GetreportExportExcel[0].lsname.split('.');
        //   link.download = name[0];
        //   link.href = str;
        //   link.click();
        // });
        
  //       const year = this.paymentreport_list.length > 0 ? this.paymentreport_list[0].year : '';
  //       const month = this.paymentreport_list.length > 0 ? this.paymentreport_list[0].month : '';
  //       const employeeCount = this.paymentreport_list.length > 0 ? this.paymentreport_list[0].Employee_count : '';
  //       const payment_amount = this.paymentreport_list.length > 0 ? this.paymentreport_list[0].Amount : '';
  // const PaymentReport = [
  //   {
  //     Year: year,
  //     Month: month,
  //     EmployeeCount: employeeCount, 
  //     Amount: payment_amount,
  //   },
  //  
  // ];


  const PaymentReport = this.paymentreport_list.map(item => ({
    Year: item.year || '', 
    Month: item.month || '',
    EmployeeCount: item.Employee_count || '',
    Amount: item.Amount || '',
  }));

        
        this.excelService.exportAsExcelFile(PaymentReport, 'Payment_Report');
    
      }

      // printPage(){
      //   debugger
      //   let printContents, popupWin;
      //   printContents = this.paymentreport_list;

      //   popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
      //   if(popupWin){
      //     popupWin.document.open();
      //     popupWin.document.write(`
      //       <html>
      //         <head>
      //           <title>Anil Pathuri</title>
      //           <style type="text/css">
      //             p {
      //               font-family: "Times New Roman";
      //             }
    
      //             .padding-main-divcls{
      //               padding: 5px;
      //             }
    
      //             .text-center{
      //               text-align: center
      //             }
      //             .width-full{
      //               width: 100%;
      //             }
    
      //             .box{
      //                 border-style: solid;
      //                 border-width: 1px;
      //                 width: 65px;
      //                 height: 100px;
      //                 float: right;
      //                 margin-right: 50px;
      //                 font-size: 10px;
      //                 padding: 5px;
      //             }
      //             .box-divcls{
      //               width: 100%;
      //               display: inline-block;
      //             }
    
      //             .TermsConditionTable, tr , td {
      //               padding: 4px !important;
      //             }
      //             tr, td {
      //               page-break-inside: avoid !important;
      //             }
                
    
      //             .break-after{
      //               page-break-after: always;
      //             }
      //             .top-border-cls{
      //               border-top: solid black 1.0pt;
      //             }
      //           </style>
      //           <body onload="window.print();window.close()">${printContents}</body>
      //         </head>
      //       </html>
      //     `)
      //     popupWin.document.close();
      //   }
    
      // }
pdf()
{ debugger
  const doc = new jsPDF() as any
  var prepare: any[][]=[];
  this.paymentreport_list.forEach(e=>{
    var tempObj =[];
    tempObj.push(e.year);
    tempObj.push(e.month);
    tempObj.push(e.Employee_count);
    tempObj.push(e.Amount);
    prepare.push(tempObj);
  });
  console.log(prepare);
  autoTable(doc,{
      head: [['Year','Month','EmployeeCount','Amount']],
      body: prepare
  });
  doc.save('Payment_Report' + '.pdf');

}
  ondetailed(){

    }
  
    GetPaymentSummary(){

  }

  formatCurrency(value:any) {
    const formattedValue = parseFloat(value).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
    return formattedValue;
  }


}


function setValue(payment_amount: any) {
  throw new Error('Function not implemented.');
}

