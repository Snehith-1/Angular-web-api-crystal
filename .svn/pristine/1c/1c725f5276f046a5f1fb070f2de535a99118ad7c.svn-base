import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-acc-mst-bankmaster-add',
  templateUrl: './acc-mst-bankmaster-add.component.html',
  styleUrls: ['./acc-mst-bankmaster-add.component.scss']
})
export class AccMstBankmasterAddComponent {

  reactiveform: FormGroup | any;
  bankmaster_list: any;
  accounttype_list: any;
  accountgroup_list: any;
  branchname_list: any;
  mdlAccountType:any;
  mdlAccountGroup:any;
  mdlBranchName:any;

  constructor(private formBuilder: FormBuilder, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.reactiveform = new FormGroup({
      bank_code: new FormControl('', Validators.required),
      bank_name: new FormControl('', Validators.required),
      account_no: new FormControl('', Validators.required),
      ifsc_code: new FormControl('', Validators.required),
      neft_code: new FormControl('', Validators.required),
      swift_code: new FormControl('', Validators.required),
      remarks: new FormControl('', Validators.required),
      accountgroup_gid: new FormControl('',Validators.required),
      branch_name: new FormControl(''),
      accountgroup_name: new FormControl('',Validators.required),
      account_type: new FormControl('',Validators.required),
      openning_balance: new FormControl('', Validators.required),
      created_date: new FormControl('', Validators.required),
    })
  }

  get bank_code() {
    return this.reactiveform.get('bank_code')!;
  }
  get bank_name() {
    return this.reactiveform.get('bank_name')!;
  }
  get account_no() {
    return this.reactiveform.get('account_no')!;
  }
  get openning_balance() {
    return this.reactiveform.get('openning_balance')!;
  }
  get created_date() {
    return this.reactiveform.get('created_date')!;
  }
  get branch_name() {
    return this.reactiveform.get('branch_name')!;
  }
  get accountgroup_name() {
    return this.reactiveform.get('accountgroup_name')!;
  }
  get account_type() {
    return this.reactiveform.get('account_type')!;
  }



  ngOnInit(): void {
    ////Drop Down///
    var url = 'AccMstBankMaster/GetAccountType'
    this.service.get(url).subscribe((result: any) => {
      this.accounttype_list = result.GetAccountType;
    });
    var url = 'AccMstBankMaster/GetAccountGroup'
    this.service.get(url).subscribe((result: any) => {
      this.accountgroup_list = result.GetAccountGroup;
    });
    var url = 'AccMstBankMaster/GetBranchName'
    this.service.get(url).subscribe((result: any) => {
      this.branchname_list = result.GetBranchName;
    });
  }
  ////Submit Function////
  public onsubmit(): void {

    if (this.reactiveform.value.bank_name != null && this.reactiveform.value.bank_name != null) {
      for (const control of Object.keys(this.reactiveform.controls)) {
        this.reactiveform.controls[control].markAsTouched();
      }
      this.reactiveform.value;
      var api = 'AccMstBankMaster/PostBankMaster';
      this.service.post(api, this.reactiveform.value).subscribe((result: any) => {
        if (result.status == false) {
          this.ToastrService.warning("Kindly Fill All Mandatory Fields !! ")
          // this.BranchSummary();
        }
        else{
          this.reactiveform.get("bank_code")?.setValue(null);
          this.reactiveform.get("bank_name")?.setValue(null);
          this.reactiveform.get("account_gid")?.setValue(null);
          this.reactiveform.get("account_no")?.setValue(null);
          this.reactiveform.get("ifsc_code")?.setValue(null);
          this.reactiveform.get("neft_code")?.setValue(null);
          this.reactiveform.get("swift_code")?.setValue(null);
          this.reactiveform.get("accountgroup_gid")?.setValue(null);
          this.reactiveform.get("openning_balance")?.setValue(null);
          this.reactiveform.get("branch_gid")?.setValue(null);
          this.reactiveform.get("created_date")?.setValue(null);
          this.reactiveform.get("remarks")?.setValue(null);
          this.ToastrService.success("Bank Master Added Successfully");
          // this.BranchSummary();

        }
      });
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }

  onadd() { }
  redirecttolist() {
    this.router.navigate(['/finance/AccMstBankMasterSummary'])
  }


}
