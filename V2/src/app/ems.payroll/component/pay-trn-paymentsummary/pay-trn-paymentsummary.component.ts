import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

interface IPaymentReport {

}

@Component({
  selector: 'app-pay-trn-paymentsummary',
  templateUrl: './pay-trn-paymentsummary.component.html',
  styleUrls: ['./pay-trn-paymentsummary.component.scss']
})
export class PayTrnPaymentsummaryComponent {
  reactiveFormSubmit!: FormGroup;
  PaymentReport!: IPaymentReport;
  responsedata: any;
  payment_list: any[] = [];
  paymentadd_list: any[] = [];
  paymentadd1_list: any[] = [];
  payadd_list: any[] = [];
  payment_gid: any;
  employee_gid: any;
  parameterValue: any;
  parameterValue1: any;
  Document_list: any;
  data2:any;
  data:any;
  data1:any;
  paymentexpend_list: any;
  

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.PaymentReport = {} as IPaymentReport;
    }
   
    ngOnInit(): void {
    
      //// Summary Grid//////
   var url = 'PayTrnSalaryPayment/GetSalaryPaymentSummary'
      
   this.service.get(url).subscribe((result: any) => {
   this.responsedata = result;
   this.payment_list = this.responsedata.paymentlist;
     setTimeout(() => {
       $('#payment_list').DataTable();
       }, );
 });
}
  

  ondetail(month: any,year:any) {
    debugger;
    var url = 'PayTrnSalaryPayment/GetSalaryPaymentExpand'
    let param = {
      month : month, 
      year : year 
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      debugger;
    this.paymentadd_list = result.getpayment;
      });
  }


 

  
 
ondetail1(month: any, year: any, payment_date: any, payment_type: any) {
  
  function formatDate(inputDate: any) {
    var dateParts = inputDate.split("-");
    var formattedDate = dateParts[2] + "-" + dateParts[1] + "-" + dateParts[0];
    return formattedDate;
}

  var url = 'PayTrnSalaryPayment/GetSalaryPaymentExpand2';
 
  var formattedPaymentDate = formatDate(payment_date);

  let param = {
      month: month,
      year: year,
      payment_date: formattedPaymentDate,
      modeof_payment: payment_type
  };

  this.service.getparams(url, param).subscribe((result: any) => {
      this.paymentexpend_list = result.getpayment1;
  });
}

  makepayment(params: any,params1:any){
    // this.router.navigate(['/payroll/PayTrnMakepayment'])
    const secretKey = 'storyboarderp';
    const param = (params+'+'+params1);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/payroll/PayTrnMakepayment',encryptedParam]) 
  }
 
 
    openModalpayment(parameter: string,parameter1:string) {
   
    this.router.navigate(['/payroll/PayTrnMakepayment'])
   
    this.parameterValue = parameter
    this.parameterValue1 = parameter1

    this.reactiveFormSubmit.get("payment_gid")?.setValue(this.parameterValue1.payment_gid);
    this.reactiveFormSubmit.get("employee_gid")?.setValue(this.parameterValue.employee_gid);
    console.log(this.reactiveFormSubmit)
    const Paymentgid = this.reactiveFormSubmit.value.payment_gid;
    const Employeegid = this.reactiveFormSubmit.value.employee_gid;
    if (this.reactiveFormSubmit.value.payment_gid != null && this.reactiveFormSubmit.value.employee_gid != '') {
      for (const control of Object.keys(this.reactiveFormSubmit.controls)) {
        this.reactiveFormSubmit.controls[control].markAsTouched();
      }
      const params = {
        payment_gid: Paymentgid,
        employee_gid: Employeegid
      };
    
      var url = 'PayTrnSalaryPayment/AssetDocument'
      this.service.getparams(url, params).subscribe((result: any) => {
        this.Document_list = result.Assetcustodian;
     

      
      });
    }

 
  }

  openModalpaymentedit(){
    this.router.navigate(['/payroll/PayTrnPaymentedit'])
  }
  openModalpaymentdelete(parameter: string){
    this.parameterValue = parameter
  }


  ondelete(){
    debugger;
    console.log(this.parameterValue);
    var url3 = 'PayTrnSalaryPayment/getDeletePayment'
    this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
        
        }
      else {
        this.ToastrService.success(result.message)
      
         }

    });
  }
}