import { Component} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { HttpClient } from '@angular/common/http';
interface Assign {
     productgroup_gid: string;
      productgroup_name: string;
      product_gid: string;
      product_name: string;
      productuom_gid: string;
      productuom_name: string;
      customerproduct_code: string;
      product_price: string;
      pricesegment_gid: string;
      pricesegment_name: string;

}

@Component({
  selector: 'app-smr-mst-product-assign',
  templateUrl: './smr-mst-product-assign.component.html',
  styleUrls: ['./smr-mst-product-assign.component.scss']
})
export class SmrMstProductAssignComponent {

productArray : any [] = [];
  productgroup_list: any [] = [];
  producthead_list: any [] = [];
  ProductForm!: FormGroup;
  product_list: any [] = [];
  productuom_list: any [] = [];
  group_list: any [] = [];
  head_list: any [] = [];
  assign!:Assign;
  mdlProductName:any;
  responsedata: any;
  productasgn: any;
  data:any;
  mdlProductUnitName:any;
  mdlProducGrouptName:any;
  mdlProductUom:any;
  NgxSpinnerService: any;
  parameterValue: any;
  originalValues: any[] = [];

  constructor(private http:HttpClient, private fb: FormBuilder, private router: ActivatedRoute, private route: Router, private service: SocketService, private ToastrService: ToastrService) {
   this.assign = {} as Assign
   }
  ngOnInit():void {
    debugger
//this.LoadAllProducts();
    this.ProductForm = new FormGroup({
      product_gid: new FormControl(''),
      product_name: new FormControl(''),
      productgroup_gid: new FormControl(''),
      productgroup_name: new FormControl(''),
      productuom_gid: new FormControl(''),
      productuom_name: new FormControl(''),
      // customerproduct_code: new FormControl(''),
      product_price: new FormControl(''),

    });
    

    ////Product Group Dropdown//////
    
   var url = 'SmrMstPricesegmentSummary/GetSmrGroupDtl'
   this.service.get(url).subscribe((result:any)=>{
    this.group_list = result.GetSmrGroupDtl;
   });
//// Product Name Dropdown ///////
var url = 'SmrMstPricesegmentSummary/GetSmrProductDtl'
this.service.get(url).subscribe((result:any)=>{
  this.product_list = result.GetSmrProductDtl;
});

    //////Product Unit DropDown //////
    
    var api3='SmrMstPricesegmentSummary/GetSmrUnitDtl'
    this.service.get(api3).subscribe((result:any)=>{
      this.productuom_list = result.GetSmrUnitDtl;
    
    });
    
   
  

    this.productasgn = this.router.snapshot.paramMap.get('pricesegment_gid');
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.productasgn, secretKey).toString(enc.Utf8);
    this.GetSmrMstProductAssignSummary(deencryptedParam);
   console.log(deencryptedParam);

   //this.GetSmrMstProductHead(deencryptedParam) ;
    
  
  }
// LoadAllProducts(){
//   this.http.get("https://jsonplaceholder.typicode.com/product").subscribe((result: any)=>
//   {
//     this.productArray = result;
//   })
// }
// GetSmrMstProductHead(pricesegment_name: any) {       
//   debugger
//   let param = {
//     pricesegment_name:  pricesegment_name ,

//   }
//     var url = 'SmrMstPricesegmentSummary/GetSmrMstProductHead'
//     this.service.getparams(url,param).subscribe((result: any) => {  
//      this.producthead_list = result.pricesegment_list
//      this.ProductForm.get("pricesegment_name")?.setValue(this.producthead_list[0].pricesegment_name);
//     });
//   }
  GetSmrMstProductAssignSummary(pricesegment_gid: any) {       
  
    let param = {
      pricesegment_gid: pricesegment_gid,

    }
    var url = 'SmrMstPricesegmentSummary/GetSmrMstProductAssignSummary'
    this.service.get(url).subscribe((result: any) => {
      $('#productgroup_list').DataTable().destroy();
     this.responsedata=result;
     this.productgroup_list = result.productgroup_list
     setTimeout(() => {
      $('#productgroup_list').DataTable();
    }, 1);

    });
  }

 
  


    update(parameter : string){
      this.parameterValue = parameter
      var url = 'SmrMstPricesegmentSummary/GetUnAssignProduct'
      let param = {
        product_gid : this.parameterValue 
      }
      this.service.getparams(url,param).subscribe((result: any) => {
        if(result.status ==false){
          this.ToastrService.warning(result.message)
        }
        else{
          
          this.ToastrService.success(result.message)
          
        }
       
        this.NgxSpinnerService.show();
    
      });

  }
  openModaledit(data: any){
    this.productgroup_list.forEach(element => {
    element.isEdit = false;
    data.originalProductPrice = data.product_price;
 
   });
   data.isEdit = true;
  

    
  }
  edupdate(){
   

      var url = 'SmrMstPricesegmentSummary/GetUpdateProduct'
      this.service.post(url, this.productgroup_list[0]).pipe().subscribe((result:any)=>{
        this.responsedata=result;
        if(result.status ==false){
          this.ToastrService.warning(result.message)
        
        }
        else{
          this.ToastrService.success(result.message)
        
        }
    }); 

  }
  

  GetOnChangeProductName(){
    
    let product_gid = this.ProductForm.value.product_name.product_gid;
    let param = {
      product_gid: product_gid
    }
    var url = 'SmrMstPricesegmentSummary/GetOnChangeProductName';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.ProductForm.get("productgroup_name")?.setValue(result.OnChangeProductName[0].productgroup_name);
      this.ProductForm.get("productuom_name")?.setValue(result.OnChangeProductName[0].productuom_name);
      this.ProductForm.value.productgroup_gid = result.OnChangeProductName[0].productgroup_gid,
        this.ProductForm.value.productuom_gid = result.OnChangeProductName[0].productuom_gid
    });

  }
  get product_name() {
    return this.ProductForm.get('product_name')!;
  }
  get product_price() {
    return this.ProductForm.get('product_price')!;
  }

  onsubmit(){
  
   if (this.ProductForm.value.product_name != null && this.ProductForm.value.product_price != '') {

    for (const control of Object.keys(this.ProductForm.controls)) {
      this.ProductForm.controls[control].markAsTouched();
    }
   
      this.ProductForm.value;
      var url='SmrMstPricesegmentSummary/PostProduct'
      this.service.post(url,this.ProductForm.value).subscribe((result:any) => {

        if(result.status == false){
          this.ToastrService.warning(result.message)
          this.GetSmrMstProductAssignSummary;
        } 
        else{
          this.ProductForm.get("productgroup_name")?.setValue(null);
          this.ProductForm.get("product_name")?.setValue(null);
          this.ProductForm.get("productuom_name")?.setValue(null);
          // this.ProductForm.get("customerproduct_code")?.setValue(null);
          this.ProductForm.get("product_price")?.setValue(null);
          
          this.ToastrService.success(result.message) 
          this.ProductForm.reset();
         
          this.GetSmrMstProductAssignSummary;
          this.NgxSpinnerService.show();
          
          
         
        }
      
              
            });
            
          }
          else {
            this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
          }
   
    

  }
  onclose(data: any){  
    if (data.isEdit) {
      data.isEdit = false;
}
  }

 
}
