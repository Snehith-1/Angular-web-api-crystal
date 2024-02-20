import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';
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
  showInput1: boolean = false;
  showInput2: boolean = false;
  showInput3: boolean = false;
  showInput4: boolean = false;
  showInput5: boolean = false;
  inputValue: string = ''
  reactiveForm: any;
  employeelist: any[] = [];
  employeegid:any;
  bankdetailslist: any[] = [];


 

 
  Loanadd!: ILoanadd;
  repayamtpermonth: number = 0;
  durationperiod: number = 0;
  loanamount:number=0

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.Loanadd = {} as ILoanadd;
    this.reactiveForm = new FormGroup({
    
    
   
    
      loan_name: new FormControl(this.Loanadd.loan_name, [
    
  
      ]),
      loan_amount: new FormControl(this.Loanadd.loan_amount, [
       
  
      ]),
      
      loan_advance: new FormControl(this.Loanadd.loan_advance, [
       
  
      ]), 
  
      date: new FormControl(this.Loanadd.date, [
        
  
      ]), 
  
     
     
  
      branch_name: new FormControl(this.Loanadd.branch_name, [
       
  
      ]),
     
      transaction_refno: new FormControl(this.Loanadd.transaction_refno, [
       
  
      ]), 
      
      
      
      loan_refno: new FormControl(''),
      type: new FormControl(''),
      repay_amt: new FormControl(''),
      repaymentstartdate: new FormControl(''),
      repayamtpermonth: new FormControl(''),
      durationperiod: new FormControl(''),
      remarks: new FormControl(''),




     
      employee : new FormControl(this.Loanadd.employee, [
       
       
        ]),
       
        bank : new FormControl(this.Loanadd.bank, [
         
         
          ]),
      
          loan_gid: new FormControl(''),
          employee_gid: new FormControl(''),
          payment_mode: new FormControl(''),
          bank_name: new FormControl(''),
          payment_date: new FormControl(''),

          cheque_no: new FormControl(''),
          transaction_no: new FormControl(''),
          card_name: new FormControl(''),
          dd_no: new FormControl(''),
         

    });
  }
  pendingamountcalc() {
    this.repayamtpermonth = this.loanamount / this.durationperiod;
  }

  ngOnInit(): void {
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };

    flatpickr('.date-picker', options);
 
  
        
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

  var api7 = 'PayTrnLoanSummary/PostLoan'

  this.service.post(api7, this.reactiveForm.value).subscribe((result: any) => {
    if (result.status == false) {
      this.ToastrService.warning(result.message)
    }
    else {
      this.router.navigate(['/payroll/PayTrnLoansummary']);
      this.ToastrService.success(result.message)
    }
  });

 
  

  
 
 

 }

 oncancel()
  {
    this.router.navigate(['/payroll/PayTrnLoansummary']);
  }

  showTextBox(event: Event) {
    debugger
    const target = event.target as HTMLInputElement;
    this.showInput = target.value === 'Cash';
    this.showInput1 = target.value === 'Cheque';
    this.showInput2 = target.value === 'DD';
    this.showInput3 = target.value === 'NEFT';
    
  }


}
