import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
interface IBlock {
  locationunit_name: string;
  locationblock_name: string;
  editlocationblock_name: string;
}
@Component({
  selector: 'app-ams-mst-block',
  templateUrl: './ams-mst-block.component.html',
  styleUrls: ['./ams-mst-block.component.scss']
})
export class AmsMstBlockComponent { 
  reactiveForm!: FormGroup;
  reactiveFormEdit!: FormGroup;
  responsedata: any;
  parameterValue: any;
  parameterValue1: any;
  block_list: any[] = [];
  branchList: any[] = [];
  block!: IBlock;

branchname: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {
    this.block = {} as IBlock;}
  ngOnInit(): void {
   this.GetBlockSummary();
    this.reactiveForm = new FormGroup({
      locationunit_name: new FormControl(''),
      locationblock_name: new FormControl(''),
      block_prefix: new FormControl(''),
    });
     
    this.reactiveFormEdit = new FormGroup({
      locationunit_name: new FormControl(''),
      locationblock_name: new FormControl(''),
      locationblock_gid: new FormControl(''),
      block_prefix: new FormControl(''),
    });

    var api1='AmsMstBlock/Getunitdropdown'
   this.service.get(api1).subscribe((result:any)=>{
  this.block_list = result.block_list;
});


  }
 
  
  GetBlockSummary(){
  var url = 'AmsMstBlock/GetBlockSummary'
  this.service.get(url).subscribe((result: any) => {

    this.responsedata = result;
    this.block_list = this.responsedata.blockdtl;

    setTimeout(() => {
      $('#block_list').DataTable();
    }, 1);


  });
}

get locationunit_name() {
  return this.reactiveForm.get('locationunit_name')!;
}

get locationunit_gid() {
  return this.reactiveFormEdit.get('locationunit_gid')!;
}
get block_prefix() {
  return this.reactiveForm.get('locationunit_name')!;
}

get locationblock_name() {
  return this.reactiveFormEdit.get('locationblock_name')!;
}

  public onsubmit(): void {
    debugger
    if (this.reactiveForm.value.locationblock_name != null && this.reactiveForm.value.locationunit_name != '') {

      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      this.reactiveForm.value;
      var url='AmsMstBlock/PostBlock'
            this.service.post(url,this.reactiveForm.value).subscribe((result:any) => {

              if(result.status ==false){
                this.ToastrService.warning(result.message)
                this.GetBlockSummary();
              }
              else{
                this.reactiveForm.get("locationunit_gid")?.setValue(null);
                this.reactiveForm.get("locationunit_name")?.setValue(null);
                this.reactiveForm.get("locationblock_name")?.setValue(null);
                this.reactiveForm.get("block_prefix")?.setValue(null);
                this.ToastrService.success(result.message)
                
              
                this.GetBlockSummary();
               
              }
              
            });
            
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
    
  }

  openModaledit(parameter: string) {
    this.parameterValue1 = parameter
    this.reactiveFormEdit.get("locationunit_gid")?.setValue(this.parameterValue1.locationunit_gid);
    this.reactiveFormEdit.get("locationunit_name")?.setValue(this.parameterValue1.locationunit_name);
    this.reactiveFormEdit.get("block_prefix")?.setValue(this.parameterValue1.block_prefix);
    this.reactiveFormEdit.get("locationblock_name")?.setValue(this.parameterValue1.locationblock_name);
    this.reactiveFormEdit.get("locationblock_gid")?.setValue(this.parameterValue1.locationblock_gid);
   
   
  }
  ////////////Update popup////////
  public onupdate(): void {
    if (this.reactiveFormEdit.value.locationblock_name != null && this.reactiveFormEdit.value.locationblock_name != '') {
      for (const control of Object.keys(this.reactiveFormEdit.controls)) {
        this.reactiveFormEdit.controls[control].markAsTouched();
      }
      this.reactiveFormEdit.value;

     
      var url = 'AmsMstBlock/updateBlock'

      this.service.post(url,this.reactiveFormEdit.value).pipe().subscribe((result:any)=>{
        this.responsedata=result;
        if(result.status ==false){
          this.ToastrService.warning(result.message)
          this.GetBlockSummary();
        }
        else{
          this.ToastrService.success(result.message)
          this.GetBlockSummary();
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
    var url = 'AmsMstBlock/GetdeleteBlockdetails'
    let param = {
      locationblock_gid : this.parameterValue 
    }
    this.service.getparams(url,param).subscribe((result: any) => {
      if(result.status ==false){
        this.ToastrService.warning(result.message)
        this.GetBlockSummary();
      }
      else{
        this.ToastrService.success(result.message)
        this.GetBlockSummary();
      }
      this.GetBlockSummary();
    
  
  
    });
  }
  onclose() {
    this.reactiveForm.reset();

  }
  onclose1() {
    this.reactiveFormEdit.reset();

  }
  
  
}
