import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators,FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';

import { Subscription, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { environment } from 'src/environments/environment';
import { ExcelService } from 'src/app/Service/excel.service';
interface IEmployee {

  product_code:string
}

@Component({
  selector: 'app-smr-mst-product-summary',
  templateUrl: './smr-mst-product-summary.component.html',
  styleUrls: ['./smr-mst-product-summary.component.scss']
})
export class SmrMstProductSummaryComponent {
  employee!: IEmployee;

  private unsubscribe: Subscription[] = [];
  file!:File;
  reactiveForm!: FormGroup;
  responsedata: any;
  parameterValue: any;
  fileInputs:any;
  products: any[] = [];
  response_data :any;
  product_list :any[]=[];
  NgxSpinnerService: any;
  productform: any;
  constructor(private fb: FormBuilder,private excelService : ExcelService,private route: ActivatedRoute,private router: Router,private service: SocketService,private ToastrService: ToastrService,) {} 
  
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

    var api = 'SmrMstProduct/GetSalesProductSummary';
    this.service.get(api).subscribe((result:any) => {
      $('#product_list').DataTable().destroy();
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
      this.router.navigate(['/smr/SmrMstProductEdit',encryptedParam]) 
    }
    onview(params:any){
      debugger
      const secretKey = 'storyboarderp';
      const param = (params);
      const encryptedParam = AES.encrypt(param,secretKey).toString();
      this.router.navigate(['/smr/SmrMstProductView',encryptedParam]) 
    }
  
  onadd()
  {
        this.router.navigate(['/smr/SmrMstProductAdd'])

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
            this.router.navigate(['/smr/SmrMstProductsummary']);
            this.ToastrService.success(result.message)
          }
          this.responsedata=result;
        });
    }
  }
  ondelete() {
    console.log(this.parameterValue);
    var url = 'SmrMstProduct/GetDeleteSalesProductdetails'
    let param = {
      product_gid : this.parameterValue 
    }
    this.service.getparams(url,param).subscribe((result: any) => {
      if(result.status ==false){
        this.ToastrService.warning(result.message)
        this.GetProductSummary();

        
       
      }
      else{
           
        this.ToastrService.success(result.message)
        
       
        this.GetProductSummary();
        
      
    }
    this.GetProductSummary();
    this.NgxSpinnerService.show();
       


  
  
    });
  }
  onclose() {
    window.location.reload();

  }

  importexcel()
   {
    let formData = new FormData();
    if (this.file != null && this.file != undefined) {
      window.scrollTo({
        top: 0, // Code is used for scroll top after event done
      });
      formData.append("file", this.file, this.file.name);
      var api = 'SmrMstProduct/ProductImportExcel'
      this.service.postfile(api, formData).subscribe((result: any) => {
        this.response_data = result;
        
        window.location.reload();
        this.ToastrService.success("Excel Uploaded Successfully")
      });
    }
  }
  downloadfileformat() {
    debugger;
    let link = document.createElement("a");
    link.download = "Sales ProductExcel";
     window.location.href = "https://"+ environment.host + "/Templates/Sales ProductExcel.xls";
    link.click();
  }

  /// Product Active - Inactive
  onChange1(event: any) {
    this.file = event.target.files[0];
  }
  openModalinactive(parameter: string){
    this.parameterValue = parameter
  }
  
  
  oninactive(){
    console.log(this.parameterValue);
      var url3 = 'SmrMstProduct/GetcustomerInactive'
      this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
  
        if ( result.status == false) {
         this.ToastrService.warning('Error While Product Inactivated')
        }
        else {
         this.ToastrService.success('Product Inactivated Successfully')
          }
        window.location.reload();
      });
  }
  
  openModalactive(parameter: string){
    this.parameterValue = parameter
  }
  
  onactive(){
    console.log(this.parameterValue);
      var url3 = 'SmrMstProduct/GetcustomerActive'
      this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
  
        if ( result.status == false) {
         this.ToastrService.warning('Error While Customer Activated')
        }
        else {
         this.ToastrService.success('Customer Activated Successfully')
          }
        window.location.reload();
      });
  }
  
  ProductexportExcel() {
    debugger
    // var api7 = 'SmrMstProduct/GetProductReportExport'
    // this.service.generateexcel(api7).subscribe((result: any) => {
    //   this.responsedata = result;
    //   var phyPath = this.responsedata.productexport_list[0].lspath1;
    //   var relPath = phyPath.split("src");
    //   var hosts = window.location.host;
    //   var prefix = location.protocol + "//";
    //   var str = prefix.concat(hosts, relPath[1]);
    //   var link = document.createElement("a");
    //   var name = this.responsedata.productexport_list[0].lsname2.split('.');
    //   link.download = name[0];
    //   link.href = str;
    //   link.click();
    //   this.ToastrService.success("Product Excel Exported Successfully")

    // });
    debugger
    const ProductExcel = this.products.map(item => ({
      ProductCode: item.product_code || '', 
      ProductType : item.producttype_name || '',
      ProductGroup : item.productgroup_name || '',
      Product : item.product_name || '',
      Unit : item.productuomclass_name || '',
      CostPrice : item.cost_price || '',
      AvgLeadTime : item.lead_time || '',
      CreatedDate : item.created_date || '',
      CreatedBy : item.created_by || ''
    }));
  
          
          this.excelService.exportAsExcelFile(ProductExcel, 'Product_Excel');
      
  }
}




