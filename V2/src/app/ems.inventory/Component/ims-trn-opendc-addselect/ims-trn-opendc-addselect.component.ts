import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-ims-trn-opendc-addselect',
  templateUrl: './ims-trn-opendc-addselect.component.html',
  styleUrls: ['./ims-trn-opendc-addselect.component.scss']
})
export class ImsTrnOpendcAddselectComponent {

  opendcadd_list:any[]=[];
  responsedata: any;

  constructor(private formBuilder: FormBuilder,public NgxSpinnerService: NgxSpinnerService,public service :SocketService,private route:Router,private ToastrService: ToastrService) {
  }
  
  ngOnInit(): void {
    this.GetImsTrnOpenDcAddSummary();
}
GetImsTrnOpenDcAddSummary(){
  debugger
  var url = 'ImsTrnOpenDcSummary/GetImsTrnOpenDcAddSummary'
  this.NgxSpinnerService.show()
   this.service.get(url).subscribe((result: any) => {
     this.responsedata = result;
     this.opendcadd_list = result.opendcadd_list;

     
     setTimeout(() => {
       $('#opendcadd_list').DataTable();
             }, 1);
      this.NgxSpinnerService.hide()


  })
}
onadd(params:any){
  debugger
  const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/ims/ImsTrnOpendcaddselectUpdate',encryptedParam])
}
}
