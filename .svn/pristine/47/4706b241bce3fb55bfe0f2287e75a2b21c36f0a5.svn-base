import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';

@Component({
  selector: 'app-pmr-trn-purchaserequisition-view',
  templateUrl: './pmr-trn-purchaserequisition-view.component.html',
  styleUrls: ['./pmr-trn-purchaserequisition-view.component.scss']
})
export class PmrTrnPurchaserequisitionViewComponent {

  purchaserequest_list: any [] = [];
  productdetails_list: any [] = [];
  requestview:any;
  responsedata: any;
  constructor(private fb: FormBuilder,private route: ActivatedRoute,private router: Router,private service: SocketService,private ToastrService: ToastrService,) {
  }
  ngOnInit(): void {
  const requestview =this.route.snapshot.paramMap.get('purchaserequisition_gid');
  this.requestview= requestview;
  const secretKey = 'storyboarderp';
  const deencryptedParam = AES.decrypt(this.requestview,secretKey).toString(enc.Utf8);
  console.log(deencryptedParam)
  this.GetViewPurchaseSummary(deencryptedParam);
  }

  GetViewPurchaseSummary(purchaserequisition_gid: any) {
    var url='PmrTrnPurchaseRequisition/GetPurchaseRequisitionView'
    let param = {
      purchaserequisition_gid : purchaserequisition_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.responsedata=result;
    this.purchaserequest_list = result.purchaserequestitionview_list; 
    this.productdetails_list = result.purchaserequestitionview_list

    });
  }
  onback()
  {
    this.router.navigate(['/pmr/PmrTrnPurchaseRequisition']);
  }
}
