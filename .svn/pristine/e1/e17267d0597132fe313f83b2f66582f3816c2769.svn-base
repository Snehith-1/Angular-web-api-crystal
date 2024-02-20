import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { environment } from '../../../../environments/environment.development';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-pmr-trn-grninward',
  templateUrl: './pmr-trn-grninward.component.html',
  styleUrls: ['./pmr-trn-grninward.component.scss']
})
export class PmrTrnGrninwardComponent {
  GrnInward_lists: any[] = [];
  responsedata: any;
  company_code : any;

  constructor(public service: SocketService, private route: Router, private ToastrService: ToastrService,public NgxSpinnerService:NgxSpinnerService) {
  }
  ngOnInit(): void {
    this.GetGrnInwardSummary();
  }
  GetGrnInwardSummary() {
    var url = 'PmrTrnGrnInward/GetGrnInwardSummary'
    this.NgxSpinnerService.show()
    this.service.get(url).subscribe((result: any) => {

      this.responsedata = result;
      this.GrnInward_lists = this.responsedata.GetGrnInward_lists;
      setTimeout(() => {
        $('#GrnInward_lists').DataTable();
      }, 1);
      this.NgxSpinnerService.hide()
    });
    
  }
  onview(params: any) {

    const secretKey = 'storyboarderp';
    const param = (params);
    console.log(param)
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.route.navigate(['./pmr/PmrTrnGrninwardView', encryptedParam])
  }

  PrintPDF(grn_gid: string) {
    this.company_code = localStorage.getItem('c_code')
    window.location.href = "http://" + environment.host + "/Print/EMS_print/pmr_crp_grnreportcr.aspx?grn_gid=" + grn_gid + "&companycode=" + this.company_code
  }



  onadd() { 
    this.route.navigate(['./pmr/PmrTrnGrninwardadd'])
  }

}
