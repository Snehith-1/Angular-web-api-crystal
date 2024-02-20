import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-ims-trn-deliveryacknowlegdement-add',
  templateUrl: './ims-trn-deliveryacknowlegdement-add.component.html',
  styleUrls: ['./ims-trn-deliveryacknowlegdement-add.component.scss']
})
export class ImsTrnDeliveryacknowlegdementAddComponent {

  deliveryadd_list:any[]=[];
  deliverycus_list:any[]=[];
  responsedata: any;

  constructor(private formBuilder: FormBuilder,public NgxSpinnerService: NgxSpinnerService,public service :SocketService,private route:Router,private ToastrService: ToastrService) {
  }

  ngOnInit(): void {
    this.GetImsTrnDeliveryAcknowledgementSummary();
}
// // //// Summary Grid//////
GetImsTrnDeliveryAcknowledgementSummary() {
  debugger
  var url = 'ImsTrnDeliveryAcknowledgement/GetImsTrnDeliveryAcknowledgementAdd'
  this.NgxSpinnerService.show()
   this.service.get(url).subscribe((result: any) => {
     this.responsedata = result;
     this.deliveryadd_list = result.deliveryadd_list;

     
     setTimeout(() => {
       $('#deliveryadd_list').DataTable();
             }, 1);
             this.NgxSpinnerService.hide()


  })
 
 
}
onadd(params:any){
  debugger
  const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/ims/ImsTrnDeliveryacknowledgementUpdate',encryptedParam])
  


}
}
