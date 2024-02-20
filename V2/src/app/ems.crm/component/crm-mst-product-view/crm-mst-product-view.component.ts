import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
@Component({
  selector: 'app-crm-mst-product-view',
  templateUrl: './crm-mst-product-view.component.html',
  styleUrls: ['./crm-mst-product-view.component.scss']
})
export class CrmMstProductViewComponent {
  product: any;
  ViewProductSummary_list:any;
  responsedata: any;

  constructor(private formBuilder: FormBuilder,private route:Router,private router:ActivatedRoute,public service :SocketService) { }

  ngOnInit(): void {
   const product_gid =this.router.snapshot.paramMap.get('product_gid');
    this.product= product_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.product,secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.GetViewProductSummary(deencryptedParam);
  }
  GetViewProductSummary(product_gid: any) {
    var url='Product/GetViewProductSummary'
    let param = {
      product_gid : product_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.responsedata=result;
    this.ViewProductSummary_list = result.GetViewProductSummary;   
    });
  }



}