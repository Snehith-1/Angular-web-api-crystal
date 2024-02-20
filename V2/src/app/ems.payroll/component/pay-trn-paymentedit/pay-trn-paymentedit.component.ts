import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

interface IEditPaymentReport {
 payment_date: string;
}

@Component({
  selector: 'app-pay-trn-paymentedit',
  templateUrl: './pay-trn-paymentedit.component.html',
  styleUrls: ['./pay-trn-paymentedit.component.scss']
})
export class PayTrnPaymenteditComponent {
  reactiveForm!: FormGroup;
  EditPaymentReport!: IEditPaymentReport;
  
  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.EditPaymentReport = {} as IEditPaymentReport;
    }
    
    
    onupdate(){

    }

    onback(){
      this.router.navigate(['/payroll/PayTrnPaymentsummary'])
    }


  }
