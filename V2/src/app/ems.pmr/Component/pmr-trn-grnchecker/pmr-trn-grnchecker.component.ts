import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-pmr-trn-grnchecker',
  templateUrl: './pmr-trn-grnchecker.component.html',
  styleUrls: ['./pmr-trn-grnchecker.component.scss']
})
export class PmrTrnGrncheckerComponent {
  GetGrnChecker_list :any;
  responsedata: any;
  constructor(public service: SocketService, private route: Router,public NgxSpinnerService:NgxSpinnerService, private ToastrService: ToastrService) {
  }
  ngOnInit(): void {
    this.GetPmrTrnGrnchecker();
  }
  GetPmrTrnGrnchecker() {
    var url = 'PmrTrnGrnchecker/GetPmrTrnGrnchecker'
    this.NgxSpinnerService.show();

    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
      this.GetGrnChecker_list = this.responsedata.GetGrnChecker_list;
      setTimeout(() => {
        $('#GetGrnChecker_list').DataTable();
      }, 1);
      this.NgxSpinnerService.hide();

    });
  }
  onview(){}
  oncheck(params: any){
    const secretKey = 'storyboarderp';
    const param= (params);
    console.log(param)
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.route.navigate(['./pmr/PmrTrnGrnqcchecker', encryptedParam])
  }
}
