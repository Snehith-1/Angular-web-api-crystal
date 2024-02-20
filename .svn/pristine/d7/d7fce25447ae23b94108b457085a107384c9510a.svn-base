import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-payable-dashboard',
  templateUrl: './payable-dashboard.component.html',
  styleUrls: ['./payable-dashboard.component.scss']
})
export class PayableDashboardComponent {
  
    response: any;
    count_total: any;
    count_product: any;
    vendor_count: any;
    cancel_invoice: any;
    pending_count: any;
    count_pendingirn: any;
    count_cancelledirn: any;
    count_creditnote: any;
    payablesummary_list: any[] = [];
    response_data: any;
    company_code: any;
  
    constructor(private router: Router, private service: SocketService) { }
  
  
    ngOnInit(): void {
  debugger
      var api = 'PayableDashboard/GetPayablesummary';
      this.service.get(api).subscribe((result: any) => {
        this.response = result;
        this.count_total = result.total_count;
        this.vendor_count = result.vendor_count;
        this.cancel_invoice = result.cancel_invoice;
        this.pending_count = result.pending_count;
        this.count_product = result.product_count
  
      })
  
      var api = 'PayableDashboard/Payabledashboardsummary';
      this.service.get(api).subscribe((result: any) => {
  
        this.response_data = result;
  
        this.payablesummary_list = this.response_data.payablesummary_list;
        setTimeout(() => {
          $('#invoice').DataTable();
        }, 1);
      });
  
  
    }
  
    customer() {
      this.router.navigate(['/payable/CrmMstCustomer']);
    }
  
    product() {
      this.router.navigate(['/payable/CrmMstProduct']);
    }
  
    raiseinvoice() {
      this.router.navigate(['/payable/AddInvoice'])
    }
  
  }

