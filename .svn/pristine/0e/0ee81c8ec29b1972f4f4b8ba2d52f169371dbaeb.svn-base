import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormRecord, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { DefaultGlobalConfig, ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';


interface IInvoiceadd{
  vendor_gid: string;
serviceorder_gid:string;
serviceorder_date: string;
invoice_reference: string;
invoice_refno: string;
payment_term: string;
payment_date:string;
invoice_date: string;
invoice_gid: string;
branch_name: string;
email_id: string;
contactperson_name: string;
currency_code: string;
vendor_companyname: string;
exchange_rate: string;
invoice_remarks: string;
  purchasetype_name: string;
  product_name: string;
  description: string;
  quantity: string;
  unit_price: string;
  discount_amount: string;
  total_amount: string;
  tax_name1: string;
  tax_amount1: string;
  tax_name2: string;
  tax_amount2: string;
  tax_name3: string;
  tax_amount3: string;
  product_price: string;
  invoice_amount:string;
  grand_total:string;
  product_total: string;
  tax_gid:string;  
  

}

@Component({
  selector: 'app-pmr-trn-invoice-accountingaddconfirm',
  templateUrl: './pmr-trn-invoice-accountingaddconfirm.component.html',
  styleUrls: ['./pmr-trn-invoice-accountingaddconfirm.component.scss']
})
export class PmrTrnInvoiceAccountingaddconfirmComponent {

addinvoiceform: FormGroup | any;
purchasetype_name:any;
invoiceaddcomfirm_list: any[] = [];
purchasetype_list: any[] = [];
taxcalculation_list: any[] = [];
taxcalculation2_list: any[] = [];
taxcalculation3_list: any[] = [];
invoiceaadd!: IInvoiceadd;
invoicegid: any;
serviceorder: any;
payment_term1:any;
product_total: any ;
product_price:number=0;
tax_name1:number = 0;
tax_name2:number = 0;
tax_name3:number = 0;
parameter: any;
parameter1:any;
parameterValue1: any;
tax_percentage: number = 0;
taxamount1: any;
taxamount2: any;
tax_amount3: number = 0;
percentage1: number = 0;
percentage2: number = 0;
tax_list : any;
taxpercentage: any;
responsedata : any;
invoice_amount: any;
grand_total : number=0;
disc_amount : any;
addon_amount : any;
taxname1:any;
taxname2:any;


  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService,
     private router:ActivatedRoute, private route:Router, public service: SocketService,
     public NgxSpinnerService:NgxSpinnerService) {
    this.invoiceaadd = {} as IInvoiceadd;
  
  }
  ngOnInit(): void {
    

    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    flatpickr('.date-picker', options);
    this.addinvoiceform = new FormGroup({
  
      purchasetype_name: new FormControl(this.invoiceaadd.purchasetype_name, [
        Validators.required]),
        
vendor_gid: new FormControl(''),
serviceorder_gid: new FormControl(''),
tax_gid: new FormControl(''),
invoice_reference: new FormControl(''),
serviceorder_date:  new FormControl(''),
invoice_refno:  new FormControl(''),
payment_term:  new FormControl(''),
payment_date: new FormControl(''),
invoice_date: new FormControl(''),
invoice_gid:  new FormControl(''),
email_id:  new FormControl(''),
contactperson_name:  new FormControl(''),
currency_code:  new FormControl(''),
vendor_companyname:  new FormControl(''),
exchange_rate:  new FormControl(''),
branch_name: new FormControl(''),
invoice_remarks: new FormControl(''),
product_name: new FormControl(''),
description:new FormControl(''),
quantity:new FormControl(''),
unit_price:new FormControl(''),
discount_amount: new FormControl(''),
total_amount:new FormControl(''),
taxname1: new FormControl(''),
taxamount1: new FormControl(''),
taxname2: new FormControl(''),
taxamount2: new FormControl(''),
tax_name3:new FormControl(''),
tax_amount3: new FormControl(''),
product_price: new FormControl(''),
invoice_amount: new FormControl(''),
grand_total:new FormControl(''),
product_total: new FormControl(''),
tax_amount:new FormControl(''),
percentage1: new FormControl(''),
percentage2: new FormControl(''),
percentage3: new FormControl(''),
addon_amount: new FormControl(''),
outstanding_amount: new FormControl(''),
disc_amount : new FormControl(''),
invoice_from : new FormControl('')
});

const currentDate = new Date();
    const day = currentDate.getDate();
    const month = currentDate.getMonth() + 1;
    const year = currentDate.getFullYear();
 
    const formattedDate = `${day.toString().padStart(2, '0')}-${month.toString().padStart(2, '0')}-${year}`;
    this.addinvoiceform.get('invoice_date')?.setValue(formattedDate);
    this.addinvoiceform.get('payment_date')?.setValue(formattedDate);
    this.payment_term1='0';
    this.serviceorder = this.router.snapshot.paramMap.get('vendor_gid');

    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.serviceorder, secretKey).toString(enc.Utf8);



    this.GetEditInvoiceSummary(deencryptedParam);
    this.addinvoiceform.get("grand_total")?.setValue(0);
    this.addinvoiceform.get("addon_amount")?.setValue('0');
    this.disc_amount='0'


  }

  GetEditInvoiceSummary(vendor_gid: any) {

    var url = 'PmrTrnInvoice/GetEditInvoiceSummary'

    let param = {

      vendor_gid: vendor_gid

    }
    this.NgxSpinnerService.show();
    this.service.getparams(url, param).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      this.invoiceaddcomfirm_list = result.invoiceaddcomfirm_list;
      debugger;
      this.addinvoiceform.get("invoice_gid")?.setValue(this.invoiceaddcomfirm_list[0].invoice_refno);
      this.addinvoiceform.get("vendor_gid")?.setValue(this.invoiceaddcomfirm_list[0].vendor_gid);

      //this.addinvoiceform.get("invoice_date")?.setValue(this.invoiceaddcomfirm_list[0].invoice_date);
     // this.addinvoiceform.get("payment_term")?.setValue(this.invoiceaddcomfirm_list[0].payment_term);
      //this.addinvoiceform.get("payment_date")?.setValue(this.invoiceaddcomfirm_list[0].payment_date);
      this.addinvoiceform.get("branch_name")?.setValue(this.invoiceaddcomfirm_list[0].branch_name);
      this.addinvoiceform.get("serviceorder_gid")?.setValue(this.invoiceaddcomfirm_list[0].serviceorder_gid);
      this.addinvoiceform.get("serviceorder_date")?.setValue(this.invoiceaddcomfirm_list[0].serviceorder_date);
      this.addinvoiceform.get("vendor_companyname")?.setValue(this.invoiceaddcomfirm_list[0].vendor_companyname);
      this.addinvoiceform.get("email_id")?.setValue(this.invoiceaddcomfirm_list[0].email_id);
      this.addinvoiceform.get("contactperson_name")?.setValue(this.invoiceaddcomfirm_list[0].contactperson_name);
      this.addinvoiceform.get("currency_code")?.setValue(this.invoiceaddcomfirm_list[0].currency_code);
      this.addinvoiceform.get("exchange_rate")?.setValue(this.invoiceaddcomfirm_list[0].exchange_rate);
      this.addinvoiceform.get("invoice_remarks")?.setValue(this.invoiceaddcomfirm_list[0].invoice_remarks);
      this.purchasetype_name=this.invoiceaddcomfirm_list[0].purchasetype_name;
      this.addinvoiceform.get("purchasetype_name")?.setValue(this.invoiceaddcomfirm_list[0].purchasetype_name);
      this.addinvoiceform.get("tax_name1")?.setValue(this.invoiceaddcomfirm_list[0].tax_name1);
      this.addinvoiceform.get("tax_amount1")?.setValue(this.invoiceaddcomfirm_list[0].tax_amount1);
      this.addinvoiceform.get("tax_name2")?.setValue(this.invoiceaddcomfirm_list[0].tax_name2);
      this.addinvoiceform.get("tax_amount2")?.setValue(this.invoiceaddcomfirm_list[0].tax_amount2);
      this.addinvoiceform.get("tax_name3")?.setValue(this.invoiceaddcomfirm_list[0].tax_name3);
      this.addinvoiceform.get("tax_amount3")?.setValue(this.invoiceaddcomfirm_list[0].tax_amount3);
      this.addinvoiceform.get("invoice_amount")?.setValue(this.invoiceaddcomfirm_list[0].invoice_amount);
      this.addinvoiceform.get("grand_total")?.setValue(this.invoiceaddcomfirm_list[0].grand_total);
      this.addinvoiceform.get("discount_amount")?.setValue(this.invoiceaddcomfirm_list[0].discount_amount);
      this.addinvoiceform.get("addon_amount")?.setValue(this.invoiceaddcomfirm_list[0].addon_amount);
      this.addinvoiceform.get("branch_gid")?.setValue(this.invoiceaddcomfirm_list[0].branch_gid);
      this.addinvoiceform.get("outstanding_amount")?.setValue(this.invoiceaddcomfirm_list[0].branch_gid);
      this.addinvoiceform.get("invoice_from")?.setValue(this.invoiceaddcomfirm_list[0].invoice_from);


    });

    var url = 'PmrTrnInvoice/GetPmrPurchaseDtl'
    this.service.get(url).subscribe((result: any) => {
      this.purchasetype_list = result.GetPmrPurchaseDtl;

    });

    var url = 'PmrTrnInvoice/Gettaxnamedropdown';
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
      this.tax_list = this.responsedata.taxnamedropdown;
    });
  }

  getDimensionsByFilter(tax_gid: any) {
    return this.tax_list.filter((x: { tax_gid: any; }) => x.tax_gid === tax_gid);

  }


  taxAmount1() {
    let tax_gid = this.addinvoiceform.get('taxname1')?.value;

    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    let tax_percentage = this.taxpercentage[0].tax_percentage;
    console.group(tax_percentage)

    this.taxamount1 = ((tax_percentage) * (this.invoice_amount) / 100);
     console.log(this.taxamount1);
     if (this.taxamount1 == undefined) {
     
     }
     else {
       const invoice_amount = parseFloat(this.invoice_amount);      
       this.product_total = ((invoice_amount) + (+this.taxamount1));
     }
     this.grandtotalcal();
  }

   taxAmount2() {
    let tax_gid = this.addinvoiceform.get('taxname2')?.value;

    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].tax_percentage;
    console.group(tax_percentage)

    this.taxamount2 = ((tax_percentage) * (this.invoice_amount) / 100);

    if (this.taxamount2 == undefined) {
     
    }
    else {
      const invoice_amount = parseFloat(this.invoice_amount);      
      this.product_total = ((invoice_amount) + (+this.taxamount1) + (+this.taxamount2));
    }
    this.grandtotalcal();
  }

  prodtotalcal() {

    const invoice_amount = parseFloat(this.invoice_amount) || 0;
    const taxAmount1 = parseFloat(this.taxamount1) || 0;
    const taxAmount2 = parseFloat(this.taxamount2) || 0;
    this.product_total = ((invoice_amount) + (taxAmount1) + (taxAmount2)) || 0;
    this.grandtotalcal();
  }

  grandtotalcal()
  {
    this.grand_total = ((this.product_total ||0) - (+this.disc_amount ||0) + (+this.addon_amount ||0))
  }


pblinvoicesubmit() {
  window.scrollTo({ top: 0, behavior: 'smooth' });
    debugger
    let f=0;
    if(this.addinvoiceform.value.purchasetype_name==null ||
      this.addinvoiceform.value.purchasetype_name==""){
      f=1;
    }
    else if(this.addinvoiceform.value.invoice_amount==null){
     f=2;
    }
    if(f==0){
    var api = 'PmrTrnInvoice/pblinvoicesubmit';
    this.service.post(api, this.addinvoiceform.value).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.route.navigate(['/payable/PmrTrnInvoiceAddselect']);
        this.ToastrService.success(result.message)
      }
    },);
  }
  else if(f==1){
    this.ToastrService.warning("Fill All Mandatory Fields")
  }
  else{
    this.ToastrService.warning("Enter Invoice Amount")
  }
  }

close(){
    this.route.navigate(['/payable/PmrTrnInvoiceAddselect']);
  }

}
