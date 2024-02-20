import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
interface IUnit {
  locationunit_code: string;
  unit_prefix: string;
  locationunit_name: string;
  locationunit_address: string;
  locationunit_gid: string;
  locationunitedit_code: string;
  unitedit_prefix:string;
  locationunitedit_name:string;
  locationunitedit_address: string;
}
@Component({
  selector: 'app-ams-mst-unit-summary',
  templateUrl: './ams-mst-unit-summary.component.html',
  styleUrls: ['./ams-mst-unit-summary.component.scss']
})
export class AmsMstUnitSummaryComponent {
  reactiveForm!: FormGroup;
  reactiveFormEdit!: FormGroup;
  responsedata: any;
  parameterValue: any;
  
  parameterValue1: any;
  unit_list: any[] = [];

  unit!: IUnit;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {
    this.unit = {} as IUnit;
  }
  ngOnInit(): void {
   this.GetUnitSummary();
    // Form values for Add popup/////
    this.reactiveForm = new FormGroup({
    

      unit_prefix: new FormControl(this.unit.unit_prefix, [
        Validators.required,

      ]),
      locationunit_name: new FormControl(''),
      locationunit_address: new FormControl(''),
      locationunit_gid: new FormControl(''),
      locationunit_code: new FormControl(''),




    });
      // Form values for Edit popup/////
    this.reactiveFormEdit = new FormGroup({

      unitedit_prefix: new FormControl(this.unit.unitedit_prefix, [
        Validators.required,
      ]),
      locationunitedit_name: new FormControl(''),
      locationunitedit_address: new FormControl(''),
      locationunit_gid: new FormControl(''),
      locationunit_code: new FormControl(''),


    });
  }
 
  //// Summary Grid//////
  GetUnitSummary(){
  var url = 'AmsMstUnit/GetUnitSummary'
  this.service.get(url).subscribe((result: any) => {

    this.responsedata = result;
    this.unit_list = this.responsedata.unitdtl;
    //console.log(this.unitdtl)
    setTimeout(() => {
      $('#unitdtl').DataTable();
    }, 1);


  });
}
////////////Add popup validtion////////
get locationunit_code() {
  return this.reactiveForm.get('locationunit_code')!;
}
get unit_prefix() {
  return this.reactiveForm.get('unit_prefix')!;
}
get locationunit_name() {
  return this.reactiveForm.get('locationunit_name')!;
}
get locationunit_address() {
  return this.reactiveForm.get('locationunit_address')!;
}
////////////Edit popup validtion////////
get locationunitedit_code() {
  return this.reactiveFormEdit.get('locationunit_code')!;
 }
get unitedit_prefix() {
  return this.reactiveFormEdit.get('unitedit_prefix')!;
 }
 get locationunitedit_name() {
  return this.reactiveFormEdit.get('locationunitedit_name')!;
 }
 get locationunitedit_address() {
  return this.reactiveFormEdit.get('locationunitedit_address')!;
 }

////////////Add popup////////
  public onsubmit(): void {
    if (this.reactiveForm.value.locationunit_name != null && this.reactiveForm.value.locationunit_name != '') {

      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      this.reactiveForm.value;
      var url='AmsMstUnit/PostUnit'
            this.service.post(url,this.reactiveForm.value).subscribe((result:any) => {

              if(result.status ==false){
                this.ToastrService.warning(result.message)
                this.GetUnitSummary();
              }
              else{
                this.reactiveForm.get("unit_prefix")?.setValue(null);
                this.reactiveForm.get("locationunit_name")?.setValue(null);
                this.reactiveForm.get("locationunit_address")?.setValue(null);
                this.ToastrService.success(result.message)
                
              
                this.GetUnitSummary();
               
              }
              
            });
            
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
    
  }
  ////////////Edit popup////////
  openModaledit(parameter: string) {
    this.parameterValue1 = parameter
    this.reactiveFormEdit.get("locationunit_code")?.setValue(this.parameterValue1.locationunit_code);
    this.reactiveFormEdit.get("unitedit_prefix")?.setValue(this.parameterValue1.unit_prefix);
    this.reactiveFormEdit.get("locationunitedit_name")?.setValue(this.parameterValue1.locationunit_name);
    this.reactiveFormEdit.get("locationunitedit_address")?.setValue(this.parameterValue1.locationunit_address);
    this.reactiveFormEdit.get("locationunit_gid")?.setValue(this.parameterValue1.locationunit_gid);
   
  }
  ////////////Update popup////////
  public onupdate(): void {
    if (this.reactiveFormEdit.value.unitedit_prefix != null && this.reactiveFormEdit.value.unitedit_prefix != '') {
      for (const control of Object.keys(this.reactiveFormEdit.controls)) {
        this.reactiveFormEdit.controls[control].markAsTouched();
      }
      this.reactiveFormEdit.value;

      //console.log(this.reactiveFormEdit.value)
      var url = 'AmsMstUnit/GetupdateUnitdetails'

      this.service.post(url,this.reactiveFormEdit.value).pipe().subscribe((result:any)=>{
        this.responsedata=result;
        if(result.status ==false){
          this.ToastrService.warning(result.message)
          this.GetUnitSummary();
        }
        else{
          this.ToastrService.success(result.message)
          this.GetUnitSummary();
        }
       
    }); 

    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }
  ////////////Delete popup////////
  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  
  }
  ondelete() {
    console.log(this.parameterValue);
    var url = 'AmsMstUnit/GetdeleteUnitdetails'
    let param = {
      locationunit_gid : this.parameterValue 
    }
    this.service.getparams(url,param).subscribe((result: any) => {
      if(result.status ==false){
        this.ToastrService.warning(result.message)
      }
      else{
        this.ToastrService.success(result.message)
      }
      this.GetUnitSummary();
    
  
  
    });
  }
  onclose() {
    this.reactiveForm.reset();

  }

}
