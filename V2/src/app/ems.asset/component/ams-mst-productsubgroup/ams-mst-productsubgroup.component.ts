import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
@Component({
  selector: 'app-ams-mst-productsubgroup',
  templateUrl: './ams-mst-productsubgroup.component.html',
  styleUrls: ['./ams-mst-productsubgroup.component.scss']
})
export class AmsMstProductsubgroupComponent {

  reactiveForm!: FormGroup;
  reactiveFormEdit!: FormGroup;
  responsedata: any;
  parameterValue: any;
  parameterValue1: any;
  productsubgroup_list: any[] = [];
  productgroup_list: any[] = [];
  assetblock_list: any[] = [];
  nature_list: any[] = [];
  branchList: any[] = [];


branchname: any;
  parameterValue3: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {}
  ngOnInit(): void {
   this.GetProductsubgroupSummary();
    this.reactiveForm = new FormGroup({
      productsubgroup_name: new FormControl(''),
      productgroup_gid: new FormControl(''),
      subgroup_warranty: new FormControl(''),
      subgroup_additionalwarranty: new FormControl(''),
      subgroup_amc: new FormControl(''),
      subgroup_oem: new FormControl(''),
      productgroup_name: new FormControl(''),
    });
     
    this.reactiveFormEdit = new FormGroup({
      productsubgroup: new FormControl(''),
      productsubgroup_name: new FormControl(''),
      productsubgroup_gid: new FormControl(''),
      productgroup_gid: new FormControl(''),
      productgroup_name: new FormControl(''),
      productgroup: new FormControl(''),
      subgroup_warranty: new FormControl(''),
      subgroup_additionalwarranty: new FormControl(''),
      subgroup_amc: new FormControl(''),
      subgroup_oem: new FormControl(''),
      assetblock_name: new FormControl(''),
      assetblock_gid: new FormControl(''),
      description: new FormControl(''),
    });



    var api1='AmsMstProductsubgroup/GetProductgroup'
    this.service.get(api1).subscribe((result:any)=>{
   this.productgroup_list = result.GetProductgroup;
  });

  var api3='AmsMstProductsubgroup/GetAssetblock'
  this.service.get(api3).subscribe((result:any)=>{
    this.assetblock_list = result.GetAssetblock;
  
  });



  }


  get productsubgroup() {
    return this.reactiveForm.get('productsubgroup')!;
  }
  get productgroup() {
    return this.reactiveForm.get('productgroup')!;
  }
  get productgroup_name() {
    return this.reactiveForm.get('productgroup_name')!;
  }
  get productsubgroup_name() {
    return this.reactiveForm.get('productsubgroup_name')!;
  }
  get productgroup_gid() {
    return this.reactiveForm.get('productgroup_gid')!;
  }
  get subgroup_warranty() {
    return this.reactiveForm.get('subgroup_warranty')!;
  }get subgroup_additionalwarranty() {
    return this.reactiveForm.get('subgroup_additionalwarranty')!;
  }
  get subgroup_amc() {
    return this.reactiveForm.get('subgroup_amc')!;
  }
  get subgroup_oem() {
    return this.reactiveForm.get('subgroup_oem')!;
  }



  GetProductsubgroupSummary(){
    var url = 'AmsMstProductsubgroup/GetproductsubgroupSummary'
    this.service.get(url).subscribe((result: any) => {
  
      this.responsedata = result;
      this.productsubgroup_list = this.responsedata.productsubgroup_list;
  
      setTimeout(() => {
        $('#productsubgroup_list').DataTable();
      }, 1);
  
  
    });
  }
  onDescriptionSelected(parameter: string){

   
   this.parameterValue3 = parameter;
   console.log(this.parameterValue3);
    let param = {
      assetblock_gid : this.parameterValue3 
    }


    var api3='AmsMstProductsubgroup/GetNatureOfAsset'
    this.service.getparams(api3,param).subscribe((result: any) => {
    
      this.nature_list = result.GetNatureOfAsset;
    
    });
  }





  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  
  }



ondelete() {
  console.log(this.parameterValue);
  var url1 = 'AmsMstProductsubgroup/Deleteproductsubgroup'
  let param = {
    productsubgroup_gid : this.parameterValue 
  }
  this.service.getparams(url1,param).subscribe((result: any) => {
    if(result.status ==false){
      this.ToastrService.warning(result.message)
      this.GetProductsubgroupSummary();
      // window.location.reload();

    }
    else{
      this.ToastrService.success(result.message)
      this.GetProductsubgroupSummary();
      // window.location.reload();
    }
    this.GetProductsubgroupSummary();
  


  });
}




public onsubmit(): void {
  debugger
  if (this.reactiveForm.value.productsubgroup_name != null && this.reactiveForm.value.productsubgroup_name != '') {

    for (const control of Object.keys(this.reactiveForm.controls)) {
      this.reactiveForm.controls[control].markAsTouched();
    }
    this.reactiveForm.value;
    var url='AmsMstProductsubgroup/PostProductsugroupAdd'
          this.service.post(url,this.reactiveForm.value).subscribe((result:any) => {

            if(result.status ==false){
              this.ToastrService.warning(result.message)
              this.GetProductsubgroupSummary();
            }
            else{
              this.reactiveForm.get("productsubgroup_name")?.setValue(null);
              this.reactiveForm.get("productgroup_gid")?.setValue(null);
              this.reactiveForm.get("subgroup_warranty")?.setValue(null);
              if (this.reactiveForm.value.subgroup_warranty === 'N') {
                this.reactiveForm.get("subgroup_additionalwarranty")?.setValue('N');
              }
              else{this.reactiveForm.get("subgroup_additionalwarranty")?.setValue(null);}
              this.reactiveForm.get("subgroup_amc")?.setValue(null);
              this.reactiveForm.get("subgroup_oem")?.setValue(null);
              this.ToastrService.success(result.message)
              
            
              this.GetProductsubgroupSummary();
             
            }
            
          });
          
  }
  else {
    this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
  }
  
}



onclose() {
  this.reactiveForm.reset();
  this.reactiveFormEdit.reset();
}




onSubgroupWarrantyChange(value: string) {
  if (value === 'N') {
    this.reactiveForm.get('subgroup_additionalwarranty')?.setValue('N');
    this.reactiveForm.get('subgroup_additionalwarranty')?.disable();
  } 
}

openModaledit(parameter: string) {
  this.parameterValue1 = parameter
  this.reactiveFormEdit.get("productsubgroup_gid")?.setValue(this.parameterValue1.productsubgroup_gid);
  this.reactiveFormEdit.get("productsubgroup")?.setValue(this.parameterValue1.productsubgroup);
  this.reactiveFormEdit.get("productsubgroup_name")?.setValue(this.parameterValue1.productsubgroup);
  this.reactiveFormEdit.get("productgroup_gid")?.setValue(this.parameterValue1.productgroup_gid);
  this.reactiveFormEdit.get("productgroup_name")?.setValue(this.parameterValue1.productgroup_name);
  this.reactiveFormEdit.get("productgroup")?.setValue(this.parameterValue1.productgroup);

  if (this.parameterValue1.subgroup_warranty === 'No') {
    this.reactiveFormEdit.get("subgroup_warranty")?.setValue('N');}
  else {this.reactiveFormEdit.get("subgroup_warranty")?.setValue('Y');}

  
  if (this.parameterValue1.subgroup_additionalwarranty === 'No') {
    this.reactiveFormEdit.get("subgroup_additionalwarranty")?.setValue('N');}
  else {this.reactiveFormEdit.get("subgroup_additionalwarranty")?.setValue('Y');}


  if (this.parameterValue1.subgroup_amc === 'No') {
    this.reactiveFormEdit.get("subgroup_amc")?.setValue('N');}
  else {this.reactiveFormEdit.get("subgroup_amc")?.setValue('Y');}
  
  if (this.parameterValue1.subgroup_oem === 'No') {
    this.reactiveFormEdit.get("subgroup_oem")?.setValue('N');}
  else {this.reactiveFormEdit.get("subgroup_oem")?.setValue('Y');}

 
}



openModalNOA(parameter: string) {
  this.parameterValue1 = parameter
  this.reactiveFormEdit.get("productsubgroup_gid")?.setValue(this.parameterValue1.productsubgroup_gid);
  this.reactiveFormEdit.get("productsubgroup")?.setValue(this.parameterValue1.productsubgroup);
  this.reactiveFormEdit.get("productsubgroup_name")?.setValue(this.parameterValue1.productsubgroup);}




 ////////////Update popup////////
 public onupdate(): void {
  if (this.reactiveFormEdit.value.productsubgroup != null && this.reactiveFormEdit.value.productsubgroup != '') {
    for (const control of Object.keys(this.reactiveFormEdit.controls)) {
      this.reactiveFormEdit.controls[control].markAsTouched();
    }
    this.reactiveFormEdit.value;

   
    var url = 'AmsMstProductsubgroup/PostProductsubgroupUpdate'

    this.service.post(url,this.reactiveFormEdit.value).pipe().subscribe((result:any)=>{
      this.responsedata=result;
      if(result.status ==false){
        this.ToastrService.warning(result.message)
        this.GetProductsubgroupSummary();
      }
      else{
        this.ToastrService.success(result.message)
        this.GetProductsubgroupSummary();
      }
     
  }); 

  }
  else {
    this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
  }
}



 public onupdate1(): void {
  if (this.reactiveFormEdit.value.productsubgroup_gid != null && this.reactiveFormEdit.value.productsubgroup_gid != '') {
    for (const control of Object.keys(this.reactiveFormEdit.controls)) {
      this.reactiveFormEdit.controls[control].markAsTouched();
    }
    this.reactiveFormEdit.value;

   
    var url = 'AmsMstProductsubgroup/PostNatureofAsset'

    this.service.post(url,this.reactiveFormEdit.value).pipe().subscribe((result:any)=>{
      this.responsedata=result;
      if(result.status ==false){
        this.ToastrService.warning(result.message)
        this.GetProductsubgroupSummary();
      }
      else{
        this.ToastrService.success(result.message)
        this.GetProductsubgroupSummary();
      }
     
  }); 

  }
  else {
    this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
  }
}






}