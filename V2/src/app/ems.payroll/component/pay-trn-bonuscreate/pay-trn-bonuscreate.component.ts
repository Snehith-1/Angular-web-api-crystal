import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormControl,FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';

@Component({
  selector: 'app-pay-trn-bonuscreate',
  templateUrl: './pay-trn-bonuscreate.component.html',
  styleUrls: ['./pay-trn-bonuscreate.component.scss']
})
export class PayTrnBonuscreateComponent {
  reactiveForm!: FormGroup;
  response_data : any;
  parameterValue: any;



  ngOnInit(): void {
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    flatpickr('.date-picker', options);
    }


    constructor(public service :SocketService,private route:Router,private ToastrService: ToastrService, private FormBuilder: FormBuilder) {
      this.reactiveForm = new FormGroup({
        bonus_name: new FormControl('', Validators.required),
        bonus_date: new FormControl('', Validators.required),
        bonus_todate: new FormControl('', Validators.required),
        bonus_percentage: new FormControl('', Validators.required),
        remarks: new FormControl(''),
      });
    }
  
    get bonus_name() {
      return this.reactiveForm.get('entityname')!;
    }  
    get bonus_percentage() {
      return this.reactiveForm.get('entityname')!;
    }
    get remarks() {
      return this.reactiveForm.get('entityname')!;
    }
    get fromdateControl() {
    return this.reactiveForm.get('bonus_date');
    }
    get todateControl() {
    return this.reactiveForm.get('bonus_todate');
    }

  submit() {
    const api = 'PayTrnBonus/PostBonus'
    this.service.post(api, this.reactiveForm.value).subscribe((result: any) => {
      this.response_data = result;
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.route.navigate(['/payroll/PayTrnBonus']);
        this.ToastrService.success(result.message)
      }
         
    });
  }

 
}
