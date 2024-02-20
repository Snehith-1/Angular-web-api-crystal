import { Component, OnInit, OnDestroy, ChangeDetectorRef, } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../../environments/environment.development';
import { AES } from 'crypto-js';

@Component({
  selector: 'app-crm-mst-product',
  templateUrl: './crm-mst-product.component.html',
  styleUrls: ['./crm-mst-product.component.scss']
})

export class CrmMstProductComponent {
  private unsubscribe: Subscription[] = []; // Read more: => https://brianflove.com/2016/12/11/anguar-2-unsubscribe-observables/
  products: any[] = [];
  ProductDocumentdtl_list: any[] = [];
  ProductDocument_list: any[] = [];
  file!: File;
  response_data :any;
  parameterValue: any;
  reactiveForm!: FormGroup;

  constructor(private fb: FormBuilder,private route: ActivatedRoute,private router: Router,private service: SocketService, private ToastrService: ToastrService) {} 

  ngOnInit(): void {
    var api = 'EinvoiceProduct/GetProductSummary';
    this.service.get(api).subscribe((result:any) => {
      this.response_data = result;
      this.products = this.response_data.product_list;
      setTimeout(()=>{  
        $('#product').DataTable();
      }, 1);
    });
  }

  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  }

  ondelete() {
    console.log(this.parameterValue);
    var url3 = 'EinvoiceProduct/deleteProductSummary'
    this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.ToastrService.success(result.message)
      }
      window.location.reload();
    });
  }

  onedit(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/einvoice/CrmMstProductEdit',encryptedParam]) 
  }

  onview(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/einvoice/CrmMstProductView',encryptedParam]) 
  }

  onadd()
  {
    this.router.navigate(['/einvoice/CrmMstProductAdd'])
  }
  importexcel()
   {
    let formData = new FormData();
    if (this.file != null && this.file != undefined) {
      window.scrollTo({
        top: 0, // Code is used for scroll top after event done
      });
      formData.append("file", this.file, this.file.name);
      var api = 'EinvoiceProduct/EinvoiceProductImportExcel'
      this.service.postfile(api, formData).subscribe((result: any) => {
        this.response_data = result;
        if (result.status == false) {
          this.ToastrService.warning(result.message)
        }
        else {
          this.ToastrService.success(result.message)
        }
        window.location.reload();
      });
    }
  }
  downloadfileformat() {
    debugger;
    let link = document.createElement("a");
    link.download = "Sales ProductExcel";
     window.location.href = "http://"+ environment.host + "/Templates/Sales ProductExcel.xls";
    link.click();
  }
  onChange1(event: any) {
    this.file = event.target.files[0];
  } 
  getdocumentlist ()
  {
    var api1='EinvoiceProduct/GetProductDocumentlist'
    this.service.get(api1).subscribe((result:any)=>{
    this.response_data=result;
    this.ProductDocument_list = this.response_data.productdocument_list;
     });
  }
  onclose ()
  {

  }
  ondetail(productdocument_name:any)
  {
    var api1='EinvoiceProduct/GetProductDocumentDtllist'
    var param={
      document_gid:productdocument_name,
    }
    this.service.getparams(api1,param).subscribe((result:any)=>{
   
      this.response_data=result;
      this.ProductDocumentdtl_list = this.response_data.productdocumentdtl_list;  
    });
  }
}