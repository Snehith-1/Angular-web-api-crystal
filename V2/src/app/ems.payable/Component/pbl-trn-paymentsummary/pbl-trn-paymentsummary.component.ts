import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { environment } from 'src/environments/environment';
import { AES } from 'crypto-js';
interface IPaymentRpt {
 
}
@Component({
  selector: 'app-pbl-trn-paymentsummary',
  templateUrl: './pbl-trn-paymentsummary.component.html',
  styleUrls: ['./pbl-trn-paymentsummary.component.scss']
})
export class PblTrnPaymentsummaryComponent {
  responsedata: any;
  paymentrpt_list: any[] = [];
  PaymentRpt!: IPaymentRpt;
  company_code : any;
  parameterValue: any;
  invoicedetails:any;
  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.PaymentRpt = {} as IPaymentRpt;
  }
    
  ngOnInit(): void {
    //// Summary Grid//////

    var url = 'PblTrnPaymentRpt/GetPaymentRptSummary'
    this.service.get(url).subscribe((result: any) => {
   
      this.responsedata = result;
      this.paymentrpt_list = this.responsedata.paymentrptlist;
      //console.log(this.paymentrptlist)
      setTimeout(() => {
        $('#paymentrptlist').DataTable();
      }, 1);
   
   
    });
   
    }
    addpayment(){
      this.router.navigate(['/payable/PblTrnPaymentAddProceed'])
    }
    openModaldetail(){
      
    }

    openModalcancel(){

    }
    openModalapproval(){

    }
    openModalprint(){

    }
    opendetails(parameter: string){
      debugger;
      this.parameterValue = parameter
      //this.reactiveForm.get("invoice_gid")?.setValue(this.parameterValue.invoice_gid);
      console.log(this.parameterValue)
      const invoicegid = this.parameterValue;
      this.getInvoicedetails(invoicegid);
    }
    
    onview(params:any){
      const secretKey = 'storyboarderp';
      const param = (params);
      const encryptedParam = AES.encrypt(param,secretKey).toString();
      this.router.navigate(['/payable/PblTrnPaymentview',encryptedParam]) 
        
      }
    
    openModalpayment(payment_gid:string){
      debugger
      this.company_code = localStorage.getItem('c_code')
    window.location.href = "http://" + environment.host + "/Print/EMS_print/pbl_trn_paymentsummaryrpt.aspx?payment_gid=" + payment_gid + "&companycode=" + this.company_code
    }
    openModalword(payment_gid:string){
      debugger
      this.company_code = localStorage.getItem('c_code')
    window.location.href = "http://" + environment.host + "/Print/EMS_print/pbl_crp_txtilepaymentreceiptpdfword.aspx?payment_gid=" + payment_gid + "&companycode=" + this.company_code
    }
    getInvoicedetails(invoice_gid:any){
      debugger
      var url = 'PblTrnPaymentRpt/GetInvoicedetails'
      let param = {
        invoice_gid : invoice_gid 
          }
      this.service.getparams(url, param).subscribe((result: any) => {
        this.responsedata = result;
        this.invoicedetails = this.responsedata.getinvoice;
     //this.invoicedetails = result.getinvoice;
    
    
    });
    }
    oncancel(params:any){
      const secretKey = 'storyboarderp';
      const param = (params);
      const encryptedParam = AES.encrypt(param,secretKey).toString();
      this.router.navigate(['/payable/PblTrnPaymentCancel',encryptedParam]) 
    }
  }



  

