import { Component, ElementRef, Renderer2 } from '@angular/core';

import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgSelectModule } from '@ng-select/ng-select';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-pmr-trn-openinginvoice',
  templateUrl: './pmr-trn-openinginvoice.component.html',
  styleUrls: ['./pmr-trn-openinginvoice.component.scss']
})

export class PmrTrnOpeninginvoiceComponent {
  currentYear: number = new Date().getFullYear();

  vendorform: FormGroup | any;
  responsedata: any;
  vendor_list: any;
  branch_list: any;
  currency_list: any;
  currencylist: any;
  currencylist1: any;

  vendorlist: any;
  mdlBranchName: any;



  ngOnInit(): void {

    var api = 'PmrTrnOpeningInvoice/Getvendor';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.vendor_list = this.responsedata.Getvendor;
      console.log(this.vendor_list)
    });

    var api = 'PmrTrnOpeningInvoice/Getbranch';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.branch_list = this.responsedata.Getbranch;
      console.log(this.branch_list)
    });

    var api = 'PmrTrnOpeningInvoice/Getcurrency';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.currency_list = this.responsedata.Getcurrency;
      console.log(this.currency_list)
    });
  }

  constructor(private fb: FormBuilder, private router: ActivatedRoute, private route: Router, private service: SocketService, private ToastrService: ToastrService,) {
    this.vendorform = new FormGroup({
      vendor_companyname: new FormControl('', Validators.required),
      Vendor_Contact_Person: new FormControl('', Validators.required),
      Vendor_Phone_No: new FormControl('', Validators.required),
      address: new FormControl('', Validators.required),
      branch_name: new FormControl('', Validators.required),
      invoice_refno: new FormControl('', Validators.required),
      invoice_date: new FormControl('', Validators.required),
      fax: new FormControl('', Validators.required),
      invoice_remarks: new FormControl('', Validators.required),
      currency_code: new FormControl('', Validators.required),
      exchange_rate: new FormControl('', Validators.required),
      Order_Total: new FormControl('', Validators.required),
      received_amount: new FormControl('', Validators.required),
      received_year: new FormControl('', Validators.required),
      currencycode: new FormControl('', Validators.required),

    })

  }
  get Vendor_Contact_Person() {
    return this.vendorform.get('Vendor_Contact_Person')!;
  }

  get Vendor_Phone_No() {
    return this.vendorform.get('Vendor_Phone_No')!;
  }

  get address() {
    return this.vendorform.get('address')!;
  }

  get fax() {
    return this.vendorform.get('fax')!;
  }

  get exchange_rate() {
    return this.vendorform.get('exchange_rate')!;
  }

  get currencycode() {
    return this.vendorform.get('currencycode')!;
  }

  OnChangevonder() {
    let vendor_gid = this.vendorform.get("vendor_companyname")?.value;
    console.log(vendor_gid)
    let param = {
      vendor_gid: vendor_gid
    }
    var url = 'PmrTrnOpeningInvoice/GetOnChangevonder';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;

      this.vendorlist = this.responsedata.Getvendor;

      this.vendorform.get("Vendor_Contact_Person")?.setValue(this.vendorlist[0].contactperson_name);
      this.vendorform.get("Vendor_Phone_No")?.setValue(this.vendorlist[0].contact_telephonenumber);


    });

    var url = 'PmrTrnOpeningInvoice/GetOnChangevonderAddress';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.vendorlist = this.responsedata.GetvendorAddress;
      this.vendorform.get("address")?.setValue(this.vendorlist[0].address);
      this.vendorform.get("fax")?.setValue(this.vendorlist[0].fax);
    });



  }

  OnChangeCurrency() {
    let currencyexchange_gid = this.vendorform.get("currency_code")?.value;
    console.log(currencyexchange_gid)
    let param = {
      currencyexchange_gid: currencyexchange_gid
    }
    var url = 'PmrTrnOpeningInvoice/GetOnChangeCurrency';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.currencylist1 = this.responsedata.GetOnChangeCurrency;
      this.vendorform.get("exchange_rate")?.setValue(result.exchange_rate);
      // this.vendorform.get("currencycode")?.setValue(result.currency_code);

    });
  }


  onSubmit() {

    if (this.vendorform.value.vendor_companyname != null && this.vendorform.value.Vendor_Contact_Person != null && this.vendorform.value.address) {

      for (const control of Object.keys(this.vendorform.controls)) {
        this.vendorform.controls[control].markAsTouched();
      }
      this.vendorform.value;
      var url = 'PmrTrnOpeningInvoice/PostOpeningIvoicedtl'
      this.service.post(url, this.vendorform.value).subscribe((result: any) => {

        if (result.status == false) {
          this.ToastrService.warning(result.message)

        }
        else {
          this.vendorform.get("vendor_companyname")?.setValue(null);
          this.vendorform.get("Vendor_Contact_Person")?.setValue(null);
          this.vendorform.get("Vendor_Phone_No")?.setValue(null);
          this.vendorform.get("address")?.setValue(null);
          this.vendorform.get("branch_name")?.setValue(null);
          this.vendorform.get("invoice_refno")?.setValue(null);
          this.vendorform.get("invoice_date")?.setValue(null);
          this.vendorform.get("fax")?.setValue(null);
          this.vendorform.get("invoice_remarks")?.setValue(null);
          this.vendorform.get("currency_code")?.setValue(null);
          this.vendorform.get("exchange_rate")?.setValue(null);
          this.vendorform.get("Order_Total")?.setValue(null);
          this.vendorform.get("received_amount")?.setValue(null);
          this.vendorform.get("received_year")?.setValue(null);
          this.route.navigate(['/payable/PmrTrnOpeninginvoiceSummary']);
          this.ToastrService.success(result.message)
          this.vendorform.reset();


        }


      });

    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }

  }
  // redirecttolist() {
  //   this.router.navigate(['/pmr/PmrTrnOpeninginvoiceSummary']);

  // }



}

