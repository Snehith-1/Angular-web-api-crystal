import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
interface IProductGroup {
  productgroup_code: string;
  productgroup_prefix: string;
  productgroup_name: string;
  product_type: string;
  productgroup_gid: string;
  productgroupedit_code: string;
  productgroupedit_prefix:string;
  productgroupedit_name:string;
  productedit_type: string;
}
@Component({
  selector: 'app-ams-mst-productgroup-summary',
  templateUrl: './ams-mst-productgroup-summary.component.html',
  styleUrls: ['./ams-mst-productgroup-summary.component.scss']
})
export class AmsMstProductgroupSummaryComponent {
  reactiveForm!: FormGroup;
  reactiveFormEdit!: FormGroup;
  responsedata: any;
  parameterValue: any;
  Asset: any;
  ITAssetHardware: any;
  ITAssetSoftware: any;
  parameterValue1: any;
  productgroup_list: any[] = [];

  productgroup!: IProductGroup;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {
    this.productgroup = {} as IProductGroup;
  }
  ngOnInit(): void {
   this.GetProductGroupSummary();
    // Form values for Add popup/////
    this.reactiveForm = new FormGroup({
    

      productgroup_prefix: new FormControl(this.productgroup.productgroup_prefix, [
        Validators.required,

      ]),
      productgroup_name: new FormControl(''),
      product_type: new FormControl(''),
      productgroup_gid: new FormControl(''),
      productgroup_code: new FormControl(''),




    });
      // Form values for Edit popup/////
    this.reactiveFormEdit = new FormGroup({

      productgroupedit_prefix: new FormControl(this.productgroup.productgroupedit_prefix, [
        Validators.required,
      ]),
      productgroupedit_name: new FormControl(''),
      product_type: new FormControl(''),
      productgroup_gid: new FormControl(''),
      productgroup_code: new FormControl(''),


    });
  }
 
  //// Summary Grid//////
  GetProductGroupSummary(){
  var url = 'AmsMstProductgroup/GetProductGroupSummary'
  this.service.get(url).subscribe((result: any) => {

    this.responsedata = result;
    this.productgroup_list = this.responsedata.amsmstproductgroupdtl;
    //console.log(this.amsmstproductgroupdtl)
    setTimeout(() => {
      $('#amsmstproductgroupdtl').DataTable();
    }, 1);


  });
}
////////////Add popup validtion////////
get productgroup_code() {
  return this.reactiveForm.get('productgroup_code')!;
}
get productgroup_prefix() {
  return this.reactiveForm.get('productgroup_prefix')!;
}
get productgroup_name() {
  return this.reactiveForm.get('productgroup_name')!;
}
get product_type() {
  return this.reactiveForm.get('product_type')!;
}
////////////Edit popup validtion////////
get productgroupedit_code() {
  return this.reactiveFormEdit.get('productgroup_code')!;
 }
get productgroupedit_prefix() {
  return this.reactiveFormEdit.get('productgroupedit_prefix')!;
 }
 get productgroupedit_name() {
  return this.reactiveFormEdit.get('productgroupedit_name')!;
 }
 get productedit_type() {
  return this.reactiveFormEdit.get('product_type')!;
 }

////////////Add popup////////
  public onsubmit(): void {
    if (this.reactiveForm.value.productgroup_name != null && this.reactiveForm.value.productgroup_name != '') {

      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      this.reactiveForm.value;
      var url='AmsMstProductgroup/PostProductGroupAdd'
            this.service.post(url,this.reactiveForm.value).subscribe((result:any) => {

              if(result.status ==false){
                this.ToastrService.warning(result.message)
                this.GetProductGroupSummary();
              }
              else{
                this.reactiveForm.get("productgroup_prefix")?.setValue(null);
                this.reactiveForm.get("productgroup_name")?.setValue(null);
                this.reactiveForm.get("product_type")?.setValue(null);
                this.ToastrService.success(result.message)
                
              
                this.GetProductGroupSummary();
               
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
    this.reactiveFormEdit.get("productgroup_code")?.setValue(this.parameterValue1.productgroup_code);
    this.reactiveFormEdit.get("productgroupedit_prefix")?.setValue(this.parameterValue1.productgroup_prefix);
    this.reactiveFormEdit.get("productgroupedit_name")?.setValue(this.parameterValue1.productgroup_name);
    this.reactiveFormEdit.get("product_type")?.setValue(this.parameterValue1.product_type);
    this.reactiveFormEdit.get("productgroup_gid")?.setValue(this.parameterValue1.productgroup_gid);
   
  }
  ////////////Update popup////////
  public onupdate(): void {
    if (this.reactiveFormEdit.value.productgroupedit_prefix != null && this.reactiveFormEdit.value.productgroupedit_prefix != '') {
      for (const control of Object.keys(this.reactiveFormEdit.controls)) {
        this.reactiveFormEdit.controls[control].markAsTouched();
      }
      this.reactiveFormEdit.value;

      //console.log(this.reactiveFormEdit.value)
      var url = 'AmsMstProductgroup/PostProductGroupUpdate'

      this.service.post(url,this.reactiveFormEdit.value).pipe().subscribe((result:any)=>{
        this.responsedata=result;
        if(result.status ==false){
          this.ToastrService.warning(result.message)
          this.GetProductGroupSummary();
        }
        else{
          this.ToastrService.success(result.message)
          this.GetProductGroupSummary();
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
    var url = 'AmsMstProductgroup/deleteproductgroup'
    let param = {
      productgroup_gid : this.parameterValue 
    }
    this.service.getparams(url,param).subscribe((result: any) => {
      if(result.status ==false){
        this.ToastrService.warning(result.message)
      }
      else{
        this.ToastrService.success(result.message)
      }
      this.GetProductGroupSummary();
    
  
  
    });
  }
  onclose() {
    this.reactiveForm.reset();

  }

}
