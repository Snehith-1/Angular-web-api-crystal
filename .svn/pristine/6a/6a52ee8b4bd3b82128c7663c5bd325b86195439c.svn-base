import { Component } from '@angular/core';
import { FormBuilder, FormGroup,} from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AES } from 'crypto-js';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-ims-trn-deliveryorder',
  templateUrl: './ims-trn-deliveryorder.component.html',
  styleUrls: ['./ims-trn-deliveryorder.component.scss']
})
export class ImsTrnDeliveryorderComponent {
  responsedata: any;
  deliveryorders_list: any[] = [];
  getData: any;
  company_code: any;
  constructor(private formBuilder: FormBuilder,public NgxSpinnerService: NgxSpinnerService,public service :SocketService,private route:Router,private ToastrService: ToastrService) {
  }
  ngOnInit(): void {
     this.GetImsTrnDeliveryorderSummary();
     
 }
// // //// Summary Grid//////
GetImsTrnDeliveryorderSummary() {
   var url = 'ImsTrnDeliveryorderSummary/GetImsTrnDeliveryorderSummary'
   this.NgxSpinnerService.show()
    this.service.get(url).subscribe((result: any) => {
      $('#deliveryorder_list').DataTable().destroy();
      this.responsedata = result;
      this.deliveryorders_list = result.deliveryorder_list;
      setTimeout(() => {
        $('#deliveryorder_list').DataTable();
              }, 1);
      this.NgxSpinnerService.hide()

              
   })
  
  
}
PrintPDF(directorder_gid: any) {
  // API endpoint URL
  const api = 'ImsTrnDeliveryorderSummary/GetDeliveryOrderRpt';
  this.NgxSpinnerService.show()
  let param = {
    directorder_gid:directorder_gid
  }
  this.service.getparams(api,param).subscribe((result: any) => {
    if(result!=null){
      this.service.filedownload1(result);
    }
    this.NgxSpinnerService.hide()
  });
}
 
// PrintPDF(directorder_gid: any) {
//   this.company_code = localStorage.getItem('c_code')
//   window.location.href = "http://" + environment.host + "/Print/EMS_print/ims_trn_directdeliveryorder.aspx?directorder_gid=" + directorder_gid + "&companycode=" + this.company_code
// }

  
 openModaledit(){}
 onview(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/ims/ImsTrnDeliveryorderView',encryptedParam]);
  }

   openModalamend(){}
   onaddinfo(){}

}
