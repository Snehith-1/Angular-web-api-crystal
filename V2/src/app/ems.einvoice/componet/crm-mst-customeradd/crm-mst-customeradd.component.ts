import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, MaxValidator, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-crm-mst-customeradd',
  templateUrl: './crm-mst-customeradd.component.html',
  styleUrls: ['./crm-mst-customeradd.component.scss']
})

export class CrmMstCustomerAddComponent implements OnInit, OnDestroy {
  // KeenThemes mock, change it to:
  defaultAuth: any = {};
  region_list: any[] = [];
  currency_list: any[] = [];
  country_list: any[] = [];
  customerform: FormGroup | any;
  hasError?: boolean;
  returnUrl?: string;
  submitted = false;
  // private fields
  private unsubscribe: Subscription[] = [];
  responsedata: any;
  result: any;

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) {
    this.customerform = new FormGroup({
      customercode: new FormControl('', Validators.required),
      customername: new FormControl('', Validators.required),
      contactpersonname: new FormControl('', Validators.required),
      designation: new FormControl(''),
      contacttelephonenumber: new FormControl('', [Validators.required, Validators.pattern(/^[0-9]+$/), Validators.maxLength(10)]),
      Email_ID: new FormControl('', [Validators.required, Validators.pattern(/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/)]),
      Contact_No: new FormControl('', Validators.pattern(/^[0-9]+$/)),
      Fax: new FormControl(''),
      Address1: new FormControl('', Validators.required),
      address2: new FormControl(''),
      city: new FormControl(''),
      state: new FormControl(''),
      pincode: new FormControl('', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(6)]),
      country: new FormControl(''),
      Region: new FormControl(''),
      CompanyWebsite: new FormControl('', Validators.required),
      currency: new FormControl('', Validators.required),
      gstnumber: new FormControl('', [Validators.required, Validators.pattern(/[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[0-9]{1}[A-Z]{1}[0-9A-Z]{1}$/)]),
    });
  }

  ngOnInit(): void {
    var api = 'EinvoiceCustomer/Getcurrencydropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.currency_list = this.responsedata.Getcurrencydropdown;
      setTimeout(() => {
        $('#currency_list').DataTable();
      }, 0.1);
    });

    var api = 'EinvoiceCustomer/Getcountrydropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.country_list = this.responsedata.Getcountrydropdown;
      setTimeout(() => {
        $('#country_list').DataTable();
      }, 0.1);
    });

    var api = 'EinvoiceCustomer/GetRegiondropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.region_list = this.responsedata.GetRegiondropdown;
      setTimeout(() => {
        $('#region_list').DataTable();
      }, 0.1);
    });
  }

  get customercodeControl() {
    return this.customerform.get('customercode');
  }
  get customernameControl() {
    return this.customerform.get('customername')
  }
  get contactpersonnameControl() {
    return this.customerform.get('contactpersonname')
  }
  get contacttelephonenumberControl() {
    return this.customerform.get('contacttelephonenumber')
  }
  get authmailControl() {
    return this.customerform.get('Email_ID')
  }
  get addressControl() {
    return this.customerform.get('Address1')
  }
  get CurrencyCodecontol() {
    return this.customerform.get('currency')
  }
  get CompanyWebsitecontrol() {
    return this.customerform.get('CompanyWebsite')
  }
  get gstControl() {
    return this.customerform.get('gstnumber');
  }
  get pincode() {
    return this.customerform.get('pincode');
  }

  submit() {
    const api = 'EinvoiceCustomer/PostCustomer';
    this.service.post(api, this.customerform.value).subscribe((result: any) => {
          this.responsedata = result;
    
           if (result.status == false) {
            this.ToastrService.warning(result.message)
          }
          else {
            this.ToastrService.success(result.message)
            this.router.navigate(['/einvoice/CrmMstCustomer']);
          }    
  });
}

  redirecttolist() {
    this.router.navigate(['/einvoice/CrmMstCustomer']);
  }

  ngOnDestroy(): void {
  }
}
