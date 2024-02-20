import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
@Component({
  selector: 'app-ams-mst-attribute',
  templateUrl: './ams-mst-attribute.component.html',
  styleUrls: ['./ams-mst-attribute.component.scss']
})
export class AmsMstAttributeComponent {
  reactiveForm!: FormGroup;
  reactiveFormEdit!: FormGroup;
  responsedata: any;
  parameterValue: any;
  parameterValue1: any;
  attribute_list: any[] = [];
  branchList: any[] = [];


branchname: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {}
  ngOnInit(): void {
   this.GetAttributeSummary();
    this.reactiveForm = new FormGroup({
      attribute_name: new FormControl(''),
     
    });
     
    this.reactiveFormEdit = new FormGroup({
      attribute_gid: new FormControl(''),
      attribute_name: new FormControl(''),
      attribute_code: new FormControl(''),
      status_attribute: new FormControl(''),
    });

    


  }
  GetAttributeSummary(){
    var url = 'AmsMstAttribute/GetAttributeSummary'
    this.service.get(url).subscribe((result: any) => {
  
      this.responsedata = result;
      this.attribute_list = this.responsedata.attribute_list;
  
      setTimeout(() => {
        $('#block_list').DataTable();
      }, 1);
  
  
    });
  }


  
get attribute_name() {
  return this.reactiveFormEdit.get('attribute_name')!;
}

get attribute_gid() {
  return this.reactiveFormEdit.get('attribute_gid')!;
}
get attribute_code() {
  return this.reactiveFormEdit.get('attribute_code')!;
}

get status_attribute() {
  return this.reactiveFormEdit.get('status_attribute')!;
}

  public onsubmit(): void {
    debugger
    if (this.reactiveForm.value.attribute_name != null && this.reactiveForm.value.attribute_name != '') {

      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      this.reactiveForm.value;
      var url='AmsMstAttribute/PostAttributeAdd'
            this.service.post(url,this.reactiveForm.value).subscribe((result:any) => {

              if(result.status ==false){
                this.ToastrService.warning(result.message)
                this.GetAttributeSummary();
              }
              else{
                this.reactiveForm.get("attribute_name")?.setValue(null);
                this.ToastrService.success(result.message)
                
              
                this.GetAttributeSummary();
               
              }
              
            });
            
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
    
  }

  openModaledit(parameter: string) {
    this.parameterValue1 = parameter
    this.reactiveFormEdit.get("attribute_gid")?.setValue(this.parameterValue1.attribute_gid);
    this.reactiveFormEdit.get("attribute_name")?.setValue(this.parameterValue1.attribute_name);
    this.reactiveFormEdit.get("attribute_code")?.setValue(this.parameterValue1.attribute_code);
    this.reactiveFormEdit.get("status_attribute")?.setValue(this.parameterValue1.status_attribute);
   
   
  }
  ////////////Update popup////////
  public onupdate(): void {
    if (this.reactiveFormEdit.value.attribute_name != null && this.reactiveFormEdit.value.attribute_name != '') {
      for (const control of Object.keys(this.reactiveFormEdit.controls)) {
        this.reactiveFormEdit.controls[control].markAsTouched();
      }
      this.reactiveFormEdit.value;

     
      var url = 'AmsMstAttribute/UpdateAttribute'

      this.service.post(url,this.reactiveFormEdit.value).pipe().subscribe((result:any)=>{
        this.responsedata=result;
        if(result.status ==false){
          this.ToastrService.warning(result.message)
          this.GetAttributeSummary();
        }
        else{
          this.ToastrService.success(result.message)
          this.GetAttributeSummary();
        }
       
    }); 

    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }
 
  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  
  }
  ondelete() {
    console.log(this.parameterValue);
    var url = 'AmsMstAttribute/DeleteAttribute'
    let param = {
      attribute_gid : this.parameterValue 
    }
    this.service.getparams(url,param).subscribe((result: any) => {
      if(result.status ==false){
        this.ToastrService.warning(result.message)
        this.GetAttributeSummary();
      }
      else{
        this.ToastrService.success(result.message)
        this.GetAttributeSummary();
      }
      this.GetAttributeSummary();
    
  
  
    });
  }
  onclose() {
    this.reactiveForm.reset();

  }
  onclose1() {
    this.reactiveFormEdit.reset();

  }

  


}
