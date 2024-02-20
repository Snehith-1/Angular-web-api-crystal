import { Component } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-hrm-trn-biometric-summary',
  templateUrl: './hrm-trn-biometric-summary.component.html',
  styleUrls: ['./hrm-trn-biometric-summary.component.scss']
})
export class HrmTrnBiometricSummaryComponent {
  Biometricsummary: any;
  response_data: any;
  biometricManagement: any;
  parameterValue1: any;
  reactiveFormadd!: FormGroup;
  
  
  constructor(private SocketService: SocketService,public router:Router,private NgxSpinnerService: NgxSpinnerService,private ToastrService: ToastrService){
  
  }

 ngOnInit() {
   this.NgxSpinnerService.show();
   var api = 'Biometric/BiometricSummary';
   this.SocketService.get(api).subscribe((result: any) => {
     $('#biometric').DataTable().destroy();

     this.response_data = result;
     this.biometricManagement = this.response_data.biometricsummary;
     setTimeout(() => {
       $('#biometric').DataTable();
     }, 1);
   });   
  

   this.reactiveFormadd = new FormGroup({

    Employee_Code: new FormControl('', [Validators.required,]),
    Employee_Name: new FormControl('', [Validators.required,]),
    NFC_Code: new FormControl(''),
    biometric_id: new FormControl('', [Validators.required,]),
    biometric_flag: new FormControl('', [Validators.required,]),
    employee_gid:new FormControl(''),

    // branch_gid: new FormControl(''),
  }
  );
}
onclose(){

}


//  Edit popup////

 myModalupdate(parameter: string) {
   this.parameterValue1 = parameter
   this.reactiveFormadd.get("Employee_Code")?.setValue(this.parameterValue1.employee_code);
   this.reactiveFormadd.get("employee_gid")?.setValue(this.parameterValue1.employee_gid);

   this.reactiveFormadd.get("Employee_Name")?.setValue(this.parameterValue1.employee_name);
   this.reactiveFormadd.get("NFC_Code")?.setValue(this.parameterValue1.nfc_id);
   this.reactiveFormadd.get("biometric_id")?.setValue(this.parameterValue1.biometric_id);
   this.reactiveFormadd.get("biometric_flag")?.setValue(this.parameterValue1.user_status);   
}
public validate(): void {
  
    for (const control of Object.keys(this.reactiveFormadd.controls)) {
      this.reactiveFormadd.controls[control].markAsTouched();
    }
    this.reactiveFormadd.value;

    var url = 'Biometric/BiometricUpdate'

    this.SocketService.postparams(url,this.reactiveFormadd.value).pipe().subscribe(result=>{
      this.response_data=result;
      if(result.status ==false){
        this.ToastrService.success("Update successfully")
        this.Biometricsummary();
      }
      // else{
        //   this.ToastrService.success("Update successfully")
        // this.biometricsummary();
      
  });

  
}
  

}
