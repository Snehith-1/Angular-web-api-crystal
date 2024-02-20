import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { AES } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-rbl-trn-ewayinvoicesummary',
  templateUrl: './rbl-trn-ewayinvoicesummary.component.html',
  styleUrls: ['./rbl-trn-ewayinvoicesummary.component.scss']
})
export class RblTrnEwayinvoicesummaryComponent {

  response_data: any;
  ewaybillinvoicesummarylist: any;

  constructor(private fb: FormBuilder, private ToastrService: ToastrService, private route: ActivatedRoute, private router: Router, private service: SocketService, public NgxSpinnerService: NgxSpinnerService) { }

  ngOnInit() {
    var api = 'Ewaybill/Getewaybillinvoicesummary';
    this.NgxSpinnerService.show();
    this.service.get(api).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      this.response_data = result;
      this.ewaybillinvoicesummarylist = this.response_data.ewaybillinvoicesummary_list;

      setTimeout(() => {
        $('#invoice').DataTable();
      }, 1);
    });
  }

  addewaybill(params: string) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.router.navigate(['/einvoice/Eway-Add', encryptedParam])
  }
}
