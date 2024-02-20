import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AES, enc } from 'crypto-js';
interface ILoanedit {
  loan_gid: string;
  employee_gid: string;
  loan_refnoedit: string;
  employee_nameedit: string;
  loan_dateedit: string;
  loan_amountedit: string;
  paid_amountedit: string;
  balance_amtedit: string;
  repay_amtedit: string;
  remarksedit: string;
}
@Component({
  selector: 'app-pay-trn-loanedit',
  templateUrl: './pay-trn-loanedit.component.html',
  styleUrls: ['./pay-trn-loanedit.component.scss']
})
export class PayTrnLoaneditComponent {
  reactiveFormEdit: any;
  parameterValue1: any;
  loan_gid: any;
  Loanedit!: ILoanedit;
  responsedata: any;
  editLoanSummary_list:any;
  

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.Loanedit = {} as ILoanedit;
  }

  ngOnInit(): void {
 debugger
    const loan_gid = this.route.snapshot.paramMap.get('loan_gid');
    this.loan_gid = loan_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.loan_gid, secretKey).toString(enc.Utf8);
    this.getEditLoan(deencryptedParam);
    console.log(deencryptedParam)


  // Form values for Edit popup/////
  this.reactiveFormEdit = new FormGroup({
    
    
    loan_refnoedit: new FormControl(this.Loanedit.loan_refnoedit, []),
  
    employee_nameedit: new FormControl(this.Loanedit.employee_nameedit, []),
    
    loan_dateedit: new FormControl(this.Loanedit.loan_dateedit, []),
    
    loan_amountedit: new FormControl(this.Loanedit.loan_amountedit, []),
    
    paid_amountedit: new FormControl(this.Loanedit.paid_amountedit, []), 

    balance_amtedit: new FormControl(this.Loanedit.balance_amtedit, []), 

    repay_amtedit: new FormControl(this.Loanedit.repay_amtedit, []), 

    remarksedit: new FormControl(this.Loanedit.remarksedit, []), 
    
      loan_gid: new FormControl(''),
      employee_gid: new FormControl(''),
    
   

    
  });
 
 
 }
    
      get loan_refnoedit() {
      return this.reactiveFormEdit.get('loan_refnoedit')!;
    }

    get employee_nameedit() {
      return this.reactiveFormEdit.get('employee_nameedit')!;
    }

     get loan_dateedit() {
      return this.reactiveFormEdit.get('loan_dateedit')!;
    }
    get loan_amountedit() {
      return this.reactiveFormEdit.get('loan_amountedit')!;
    }
   
    get paid_amountedit() {
      return this.reactiveFormEdit.get('paid_amountedit')!;
    }
  
    get balance_amtedit() {
      return this.reactiveFormEdit.get('balance_amtedit')!;
    }
    get repay_amtedit() {
      return this.reactiveFormEdit.get('repay_amtedit')!;
    }
    
    get remarksedit() {
      return this.reactiveFormEdit.get('remarksedit')!;
    }

   

    getEditLoan(loan_gid: any) {
      debugger
      var url = 'PayTrnLoanSummary/getEditLoan'
      let param = {loan_gid : loan_gid}
      this.service.getparams(url, param).subscribe((result: any) => {
        this.responsedata=result;
        this.editLoanSummary_list = result.getEditLoan;
  
        // this.product = result;
        console.log(this.loan_gid)
        console.log(this.editLoanSummary_list)
  
        this.reactiveFormEdit.get("loan_gid")?.setValue(this.editLoanSummary_list[0].loan_gid);
        this.reactiveFormEdit.get("loan_refnoedit")?.setValue(this.editLoanSummary_list[0].loan_refnoedit);
        this.reactiveFormEdit.get("employee_nameedit")?.setValue(this.editLoanSummary_list[0].employee_nameedit);
        this.reactiveFormEdit.get("loan_dateedit")?.setValue(this.editLoanSummary_list[0].loan_dateedit);
        this.reactiveFormEdit.get("loan_amountedit")?.setValue(this.editLoanSummary_list[0].loan_amountedit);
        this.reactiveFormEdit.get("paid_amountedit")?.setValue(this.editLoanSummary_list[0].paid_amountedit);
        this.reactiveFormEdit.get("balance_amtedit")?.setValue(this.editLoanSummary_list[0].balance_amtedit);
        this.reactiveFormEdit.get("repay_amtedit")?.setValue(this.editLoanSummary_list[0].repay_amtedit);
        this.reactiveFormEdit.get("remarksedit")?.setValue(this.editLoanSummary_list[0].remarksedit);
      
        
      });
    } 


 onupdate() {
  debugger;
       var url = 'PayTrnLoanSummary/getUpdatedLoan'

       this.service.post(url, this.reactiveFormEdit.value).subscribe(
        (result: any) => {
    
        
         if (result.status == false) {
           this.ToastrService.warning(result.message)
        
         }
         else {
           this.ToastrService.success(result.message)
         
         }

       });
  
 this.router.navigate(['/payroll/PayTrnLoansummary']);
 }
 
 onback(){
  this.router.navigate(['/payroll/PayTrnLoansummary']);
}

}
