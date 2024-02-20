import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-pbl-trn-paymentsummary',
  templateUrl: './pbl-trn-paymentsummary.component.html',
  styleUrls: ['./pbl-trn-paymentsummary.component.scss']
})
export class PblTrnPaymentsummaryComponent {
  constructor(public service :SocketService,private route:Router,private ToastrService: ToastrService) {  

  }

  onadd(){
    this.route.navigate(['/finance/PblTrnPaymentaddproceed'])
  }

}
