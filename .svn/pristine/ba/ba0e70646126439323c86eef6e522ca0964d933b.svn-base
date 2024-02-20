import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators,FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';

import { Subscription, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { environment } from 'src/environments/environment';
interface IEmployee {
}
@Component({
  selector: 'app-pmr-mst-product-summary',
  templateUrl: './pmr-mst-product-summary.component.html',
  styleUrls: ['./pmr-mst-product-summary.component.scss']
})
export class PmrMstProductSummaryComponent {
  employee!: IEmployee;

  private unsubscribe: Subscription[] = [];
  file!:File;
  reactiveForm!: FormGroup;
  responsedata: any;
  parameterValue: any;
  fileInputs: any;
  products: any[] = [];
  response_data :any;
  NgxSpinnerService: any;
  constructor(private fb: FormBuilder,private route: ActivatedRoute,private router: Router,private service: SocketService,private ToastrService: ToastrService,) {} 
  

  ngOnInit(): void {
    this.GetProductSummary();

    this.reactiveForm = new FormGroup({
      file: new FormControl(''),

      // password: new FormControl(this.employee.password, [
      //   Validators.required,
      // ]),
      // confirmpassword: new FormControl(''),
      // employee_gid: new FormControl(''),

    });
  }
  GetProductSummary(){

    var api = 'PmrMstProduct/GetProductSummary';
    this.service.get(api).subscribe((result:any) => {
      this.response_data = result;
      this.products = this.response_data.product_list;
      setTimeout(()=>{  
        $('#product_list').DataTable();
      }, 1);
    });
  
  }
  onChange2(event:any) {
    this.file =event.target.files[0];
    // var api='Employeelist/EmployeeProfileUpload'
    // //console.log(this.file)
    //   this.service.EmployeeProfileUpload(api,this.file).subscribe((result:any) => {
    //     this.responsedata=result;
    //   });
    }
    onedit(params:any){
      const secretKey = 'storyboarderp';
      const param = (params);
      const encryptedParam = AES.encrypt(param,secretKey).toString();
      this.router.navigate(['/pmr/PmrMstProductEdit',encryptedParam]) 
    }
    onview(params:any){
      const secretKey = 'storyboarderp';
      const param = (params);
      const encryptedParam = AES.encrypt(param,secretKey).toString();
      this.router.navigate(['/pmr/PmrMstProductView',encryptedParam])
    }
  
  onadd()
  {
        this.router.navigate(['/pmr/PmrMstProductAdd'])

  }
  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  
  }

  public validate(): void {
    this.employee = this.reactiveForm.value;
    let formData = new FormData();
    if(this.file !=null &&  this.file != undefined){

    formData.append("file", this.file,this.file.name);
    var api='Product/ProductimageUpload'

    this.service.postfile(api,formData).subscribe((result:any) => {
      this.responsedata=result;
      if(result.status ==false){
        this.ToastrService.warning(result.message)
      }
      else{
        this.reactiveForm.reset();
        this.ToastrService.success(result.message)
      }
    });
  }
    else{
      var api7='Product/Postimage'
      //console.log(this.file)
        this.service.post(api7,this.employee).subscribe((result:any) => {

          if(result.status ==false){
            this.ToastrService.warning(result.message)
          }
          else{
            this.router.navigate(['/crm/CrmMstProductsummary']);
            this.ToastrService.success(result.message)
          }
          this.responsedata=result;
        });
    }
  }
  ondelete() {
    console.log(this.parameterValue);
    var url = 'PmrMstProduct/GetDeleteProductdetails'
    let param = {
      product_gid : this.parameterValue 
    }
    this.service.getparams(url,param).subscribe((result: any) => {
      if(result.status ==false){
        this.ToastrService.warning(result.message)
      }
      else{
           
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
        this.GetProductSummary();
       window.location.reload();
    }

  
  
    });
  }
  onclose() {
    window.location.reload();

  }
  // importexcel()
  //  {
  //   let formData = new FormData();
  //   if (this.file != null && this.file != undefined) {
  //     window.scrollTo({
  //       top: 0, // Code is used for scroll top after event done
  //     });
  //     formData.append("file", this.file, this.file.name);
  //     var api = 'PmrMstProduct/ProductImportExcel'
  //     this.service.postfile(api, formData).subscribe((result: any) => {
  //       this.response_data = result;
        
  //       window.location.reload();
  //       this.ToastrService.success("Excel Uploaded Successfully")
  //     });
  //   }
  // }

  importexcel() {

    debugger
    //  this.NgxSpinnerService.show();
     let formData = new FormData();
     if (this.file != null && this.file != undefined) {
       window.scrollTo({
         top: 0, // Code is used for scroll top after event done
       });
       formData.append("file", this.file, this.file.name);
       var api = 'PmrMstProduct/ProductImportExcel'
       this.service.postfile(api, formData).subscribe((result: any) => {
         this.responsedata = result;
         if (result.status == false) {
 
           this.NgxSpinnerService.hide();
           this.ToastrService.warning('Error While Occured Excel Upload')
         }
         else {
 
           this.NgxSpinnerService.hide();
           // window.location.reload();
           this.fileInputs= null;
           this.ToastrService.success("Excel Uploaded Successfully")
 
         }
         
 
       });
     }
   }
  downloadfileformat() {
    debugger;
    // let link = document.createElement("a");
    // link.download = "Product Excel";
    //  window.location.href = "http://"+ environment.host + "/Templates/ProductExcel.xlsx";
    // link.click();
    let link = document.createElement("a"); 
    link.download = "Product Template";
    link.href = "assets/media/Excels/producttemplate/product.xlsx";
    link.click();
  }
  onChange1(event: any) {
    this.file = event.target.files[0];
  }
}




