import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../../environments/environment.development';
import { AES } from 'crypto-js';

@Component({
  selector: 'app-rbl-mst-receiptadd',
  templateUrl: './rbl-mst-receiptadd.component.html',
  styleUrls: ['./rbl-mst-receiptadd.component.scss']
})
export class RblMstReceiptaddComponent {
  addreceipt : any;
  response_data : any;


  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) { }

  ngOnInit(): void {
    var api = 'Receipt/GetAddReceiptSummary';
    this.service.get(api).subscribe((result: any) => {
      this.response_data = result;
      this.addreceipt = this.response_data.receiptaddsummary_list;
      setTimeout(() => {
        $('#addreceipt').DataTable();
      }, 1);
    });
  }

  redirecttolist(){
    this.router.navigate(['/einvoice/ReceiptSummary'])
  }



  makereceipt(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.router.navigate(['/einvoice/MakeReceipt', encryptedParam])
  }

}
