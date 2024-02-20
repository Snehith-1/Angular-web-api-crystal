import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
interface ILoanadd {
  loan_gid: string;
  employee_gid: string;
  employee: string;
  loan_name: string;
  loan_amount: string;
  remarks: string;
  loan_advance: string;
  date: string;
  cheque_no: string;
  bank_name: string;
  bank: string;
  transaction_refno: string;
  branch_name: string;
}
@Component({
  selector: 'app-pay-trn-loanadd',
  templateUrl: './pay-trn-loanadd.component.html',
  styleUrls: ['./pay-trn-loanadd.component.scss']
})
export class PayTrnLoanaddComponent {
  showInput: boolean = false;
  showInput2: boolean = false;
  showInput3: boolean = false;
  inputValue: string = ''
  reactiveForm: any;
  employeelist: any[] = [];
  employeegid:any;
  bankdetailslist: any[] = [];

 

 
  Loanadd!: ILoanadd;

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.Loanadd = {} as ILoanadd;
  }

  ngOnInit(): void {
 
  // Form values for Add popup/////
  this.reactiveForm = new FormGroup({
    
    
   
    
    loan_name: new FormControl(this.Loanadd.loan_name, [
  

    ]),
    loan_amount: new FormControl(this.Loanadd.loan_amount, [
     

    ]),
    remarks: new FormControl(this.Loanadd.remarks, [
      

    ]),
    loan_advance: new FormControl(this.Loanadd.loan_advance, [
     

    ]), 

    date: new FormControl(this.Loanadd.date, [
      

    ]), 

    cheque_no: new FormControl(this.Loanadd.cheque_no, [
     

    ]),
    bank_name: new FormControl(this.Loanadd.bank_name, [
      

    ]),

    branch_name: new FormControl(this.Loanadd.branch_name, [
     

    ]),
   
    transaction_refno: new FormControl(this.Loanadd.transaction_refno, [
     

    ]), 
    
    
    
    loan_refno: new FormControl(''),
    type: new FormControl(''),
    repay_amt: new FormControl(''),
    paid_amt: new FormControl(''),
    pend_amt: new FormControl(''),
   
    employee : new FormControl(this.Loanadd.employee, [
     
     
      ]),
     
      bank : new FormControl(this.Loanadd.bank, [
       
       
        ]),
    
        loan_gid: new FormControl(''),
        employee_gid: new FormControl(''),
  });
        
        var api='PayTrnLoanSummary/GetEmployeeDtl'
        this.service.get(api).subscribe((result:any)=>{
        this.employeelist = result.GetEmployeeDtl;
        //console.log(this.employeelist)
        });
 
        var api='PayTrnLoanSummary/GetBankDetail'
        this.service.get(api).subscribe((result:any)=>{
        this.bankdetailslist = result.GetBankNameDtl;
        //console.log(this.bankdetailslist)
        });
 
      }


    
    get employee() {
      return this.reactiveForm.get('employee')!;
    }
    get loan_name() {
      return this.reactiveForm.get('loan_name')!;
    }
   
    get loan_amount() {
      return this.reactiveForm.get('loan_amount')!;
    }
  
     get remarks() {
      return this.reactiveForm.get('remarks')!;
    }
    get loan_advance() {
      return this.reactiveForm.get('loan_advance')!;
    }
    get date() {
      return this.reactiveForm.get('date')!;
    }

    get cheque_no() {
      return this.reactiveForm.get('cheque_no')!;
    }
    
    get bank_name() {
      return this.reactiveForm.get('bank_name')!;
    }

    get branch_name() {
      return this.reactiveForm.get('branch_name')!;
    }
    get bank() {
      return this.reactiveForm.get('bank')!;
    }
    get transaction_refno() {
      return this.reactiveForm.get('transaction_refno')!;
    }


 onconfirm(): void {
  debugger;
 
  if (this.reactiveForm.value.employee != null && this.reactiveForm.value.employee != '')

  if (this.reactiveForm.value.loan_name != null && this.reactiveForm.value.loan_name != '')
 
  if (this.reactiveForm.value.loan_amount != null && this.reactiveForm.value.loan_amount != '')
 
  if (this.reactiveForm.value.remarks != null && this.reactiveForm.value.remarks != '')
 
  if (this.reactiveForm.value.loan_advance != null && this.reactiveForm.value.loan_advance != '')
 
  if (this.reactiveForm.value.date != null && this.reactiveForm.value.date != '')

  if (this.reactiveForm.value.paid_amt != null && this.reactiveForm.value.paid_amt != '')

  if (this.reactiveForm.value.pend_amt != null && this.reactiveForm.value.pend_amt != '')

  if (this.reactiveForm.value.repay_amt != null && this.reactiveForm.value.repay_amt != '')

  if (this.reactiveForm.value.type != null && this.reactiveForm.value.type != '')

  if (this.reactiveForm.value.loan_refno != null && this.reactiveForm.value.loan_refno != '')
 
  
 
  {
  for (const control of Object.keys(this.reactiveForm.controls)) {
  this.reactiveForm.controls[control].markAsTouched();
  }
 this.reactiveForm.value;
 var url1 = 'PayTrnLoanSummary/PostLoan'

        this.service.postparams(url1, this.reactiveForm.value).subscribe((result: any) => {

            if (result.status == false) {

               this.ToastrService.warning(result.message)
            // this.LoanSummary();
            }

            else {
              this.reactiveForm.get("loan_gid")?.setValue(null);
              this.reactiveForm.get("loan_refno")?.setValue(null);
              this.reactiveForm.get("type")?.setValue(null);
              this.reactiveForm.get("employee")?.setValue(null);
              this.reactiveForm.get("loan_name")?.setValue(null);
              this.reactiveForm.get("loan_amount")?.setValue(null);
              this.reactiveForm.get("repay_amt")?.setValue(null);
              this.reactiveForm.get("paid_amt")?.setValue(null);
              this.reactiveForm.get("pend_amt")?.setValue(null);
              this.reactiveForm.get("remarks")?.setValue(null);
              this.reactiveForm.get("loan_advance")?.setValue(null);
              this.reactiveForm.get("date")?.setValue(null);
              
             
                this.ToastrService.success("Loan Add successfully");

                this.router.navigate(['/payroll/PayTrnLoansummary']);
            }
          });
        }
      else {
           this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
        }



 }
 oncancel()
  {
    this.router.navigate(['/payroll/PayTrnLoansummary']);
  }

showTextBox(event: Event) {
  const target = event.target as HTMLInputElement;
  this.showInput = target.value === 'Cheque';
  this.showInput2 = target.value === 'DD';
  this.showInput3 = target.value === 'NEFT';
}


}
