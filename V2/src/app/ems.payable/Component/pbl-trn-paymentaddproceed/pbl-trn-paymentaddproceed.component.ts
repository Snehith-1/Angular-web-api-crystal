import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { environment } from 'src/environments/environment.development';
interface IPaymentaddProceed {
  branch_name: string;
  department_name: string;
  month: string;
  year: string;
  branch_gid: string;
  department_gid: string;
  salary_gid:string;
}

@Component({
  selector: 'app-pbl-trn-paymentaddproceed',
  templateUrl: './pbl-trn-paymentaddproceed.component.html',
  styleUrls: ['./pbl-trn-paymentaddproceed.component.scss']
})
export class PblTrnPaymentaddproceedComponent {
  PaymentaddProceed!: IPaymentaddProceed;
  Payment_addlist: any[] = [];
  responsedata: any;
  Payment_expand:any[] = [];
  product_list:any;
  reactiveForm: any;
  Companyname :any;

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.PaymentaddProceed = {} as IPaymentaddProceed;
    }
    ngOnInit(): void {

      this.reactiveForm = new FormGroup({

        Companyname : new FormControl(''),
        
     });
    
    var url = 'PblTrnPaymentRpt/GetPaymentAddproceed'
    const params = {
      vendorname: "",
    };
    this.service.getparams(url,params).subscribe((result: any) => {
    
      this.responsedata = result;
      this.Payment_addlist = this.responsedata.paymentadd;
      setTimeout(() => {
        $('#Payment_addlist').DataTable();
      }, );

    });
  }

  GetpaymentSummary() {
    const selectedCompanyname = this.reactiveForm.value.Companyname;
      const params = {
        vendorname: selectedCompanyname,
      };
      const url = 'PblTrnPaymentRpt/GetPaymentAddproceed'
      this.service.getparams(url,params).subscribe((result: any) => {
  
        this.responsedata = result;
        this.Payment_addlist = this.responsedata.paymentadd;
        // setTimeout(() => {
        //   $('#Payment_addlist').DataTable();
        // }, );
      });
  
  }

  ondetail(vendor_gid: any) {
    debugger;
    var url = 'PblTrnPaymentRpt/GetMakepaymentExpand'
    let param = {
      vendor_gid : vendor_gid 
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      debugger;
    this.Payment_expand = result.paymentExpand;
    
      });
  }
  Productdetails(data: any): void {
    debugger;
    var api1 = 'PblTrnPaymentRpt/Productdetails';
 
    
    let params = {
      invoice_gid: data.invoice_gid,
      // salarygradetype :"Deduction"
    };
 
    this.service.getparams(api1, params).subscribe((result: any) => {
        this.responsedata = result;
        this.product_list = this.responsedata.productdetail_list;
    });

  }

  addpayment(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/payable/PblTrnMultipleinvoice2singlepayment',encryptedParam])
  }

  onback(){
    this.router.navigate(['/payable/PblTrnPaymentsummary'])
  }

}
