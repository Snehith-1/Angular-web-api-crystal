import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormGroup,FormControl } from '@angular/forms';
import { AES } from 'crypto-js';
@Component({
  selector: 'app-pmr-trn-invoice-addselect',
  templateUrl: './pmr-trn-invoice-addselect.component.html',
  styleUrls: ['./pmr-trn-invoice-addselect.component.scss']
})
export class PmrTrnInvoiceAddselectComponent {
  invoice_list:any;
  response_data :any;
  reactiveForm!: FormGroup;
  responsedata: any;
  parameterValue: any;
  invoice:any;

  constructor(private fb: FormBuilder,private route: ActivatedRoute,private router: Router,private service: SocketService,private ToastrService: ToastrService,) {} 
  ngOnInit(): void {
    this.GetPmrTrnInvoiceAddSelectSummary();

    this.reactiveForm = new FormGroup({
      file: new FormControl(''),

     
    });
  }
  GetPmrTrnInvoiceAddSelectSummary(){

    debugger
    var api = 'PmrTrnInvoice/GetPmrTrnInvoiceAddSelectSummary';
    this.service.get(api).subscribe((result:any) => {
      this.response_data = result;
      this.invoice = this.response_data.invoice_list;
      setTimeout(()=>{  
        $('#invoice_list').DataTable();
      }, 1);
    });
  
  }
  onview(params:any){
    debugger;
    const secretKey = 'storyboarderp';

    const param = (params);

    const encryptedParam = AES.encrypt(param,secretKey).toString();

    this.router.navigate(['/payable/PmrTrnInvoiceAccountingaddconfirm',encryptedParam])

}

back(){
  this.router.navigate(['/payable/PmrTrnInvoice'])
}
}
