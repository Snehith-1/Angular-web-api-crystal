import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { AES } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-rbl-trn-ewaysummary',
  templateUrl: './rbl-trn-ewaysummary.component.html',
  styleUrls: ['./rbl-trn-ewaysummary.component.scss']
})

export class RblTrnEwaysummaryComponent {

  response_data: any;
  ewaybillsummarylist: any;

  constructor(private fb: FormBuilder, private ToastrService: ToastrService, private route: ActivatedRoute, private router: Router, private service: SocketService, public NgxSpinnerService: NgxSpinnerService) { }

  ngOnInit() {
    var api = 'Ewaybill/Getewaybillsummary';
    this.NgxSpinnerService.show();
    this.service.get(api).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      this.response_data = result;
      this.ewaybillsummarylist = this.response_data.ewaybillsummary_list;

      setTimeout(() => {
        $('#eway').DataTable();
      }, 1);
    });
  }

  ewayinvoicesummary() {
    this.router.navigate(['/einvoice/EwayInvoiceSummary'])
  }

  viewewaybill(params: string) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.router.navigate(['/einvoice/Eway-View', encryptedParam])
  }
}
