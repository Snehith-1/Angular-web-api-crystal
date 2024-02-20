import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';

@Component({
  selector: 'app-acc-mst-bankmaster',
  templateUrl: './acc-mst-bankmaster.component.html',
  styleUrls: ['./acc-mst-bankmaster.component.scss']
})
export class AccMstBankmasterComponent {
  bankmaster_list: any;
  responsedata: any;

  constructor(public service: SocketService, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService) {
  }
  ngOnInit(): void {
    this.GetBankMasterSummary();
  }
  GetBankMasterSummary() {
    var url = 'AccMstBankMaster/GetBankMasterSummary'
    this.service.get(url).subscribe((result: any) => {

      this.responsedata = result;
      this.bankmaster_list = this.responsedata.GetBankMaster_list;
      setTimeout(() => {
        $('#bankmaster_list').DataTable();
      }, 1);
    });
  }
  onadd() {
    this.router.navigate(['/finance/AccMstBankMasterAdd'])
  }

  onedit(params: any) {
    debugger
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.router.navigate(['/finance/AccMstBankmasterEdit', encryptedParam])
  }





}
