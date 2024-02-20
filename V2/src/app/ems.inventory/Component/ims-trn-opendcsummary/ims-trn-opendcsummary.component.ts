import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-ims-trn-opendcsummary',
  templateUrl: './ims-trn-opendcsummary.component.html',
  styleUrls: ['./ims-trn-opendcsummary.component.scss']
})
export class ImsTrnOpendcsummaryComponent {
  opndcsummary_list :any[]=[];
  responsedata: any;
  company_code: any;


  constructor(private formBuilder: FormBuilder,public NgxSpinnerService: NgxSpinnerService,public service :SocketService,private route:Router,private ToastrService: ToastrService) {
  }
  ngOnInit(): void {
    this.GetImsTrnOpenDcSummary();
}

// PrintPDF(directorder_gid: string) {
//   this.company_code = localStorage.getItem('c_code')
//   window.location.href = "http://" + environment.host + "/Print/EMS_print/crm_trn_opendcadd.aspx?directorder_gid=" + directorder_gid + "&companycode=" + this.company_code
// }




PrintPDF(directorder_gid: any) {
  // API endpoint URL
  const api = 'ImsTrnOpenDcSummary/GetOpenDCRpt';
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

///Summary ////
GetImsTrnOpenDcSummary() {
  debugger
  var url = 'ImsTrnOpenDcSummary/GetImsTrnOpenDeliveryOrderSummary'
  this.NgxSpinnerService.show()
   this.service.get(url).subscribe((result: any) => {
     this.responsedata = result;
     this.opndcsummary_list = result.opndcsummary_list;

     
     setTimeout(() => {
       $('#opndcsummary_list').DataTable();
             }, 1);
             this.NgxSpinnerService.hide();


  })
 
 
}

}
