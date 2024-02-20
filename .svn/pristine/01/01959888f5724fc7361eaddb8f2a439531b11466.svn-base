import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { AES } from 'crypto-js';
import { environment } from '../../../../environments/environment.development';

@Component({
  selector: 'app-einvoicedashboard',
  templateUrl: './einvoicedashboard.component.html',
  styleUrls: ['./einvoicedashboard.component.scss']
})
export class EinvoicedashboardComponent {
  response: any;
  count_customer: any;
  count_product: any;
  count_raisedinvoice: any;
  count_cancelledinvoice: any;
  count_irngeneration: any;
  count_pendingirn: any;
  count_cancelledirn: any;
  count_creditnote: any;
  einvoicesummarylist: any[] = [];
  response_data: any;
  company_code: any;

  constructor(private router: Router, private service: SocketService) { }


  ngOnInit(): void {

    var api = 'Invoicedashboard/TileCount';
    this.service.get(api).subscribe((result: any) => {
      this.response = result;
      this.count_customer = result.customer_count;
      this.count_product = result.product_count;
      this.count_raisedinvoice = result.total_raised_invoice;
      this.count_cancelledinvoice = result.invoice_cancelled;
      this.count_irngeneration = result.irn_generated;
      this.count_pendingirn = result.irn_Pending;
      this.count_cancelledirn = result.irn_cancelled;
      this.count_creditnote = result.credit_note;
    })

    var api = 'Invoicedashboard/invoicesummary';
    this.service.get(api).subscribe((result: any) => {

      this.response_data = result;

      this.einvoicesummarylist = this.response_data.einvoicesummary_list;
      setTimeout(() => {
        $('#invoice').DataTable();
      }, 1);
    });


  }

  customer() {
    this.router.navigate(['/einvoice/CrmMstCustomer']);
  }

  product() {
    this.router.navigate(['/einvoice/CrmMstProduct']);
  }

  raiseinvoice() {
    this.router.navigate(['/einvoice/AddInvoice'])
  }

}
