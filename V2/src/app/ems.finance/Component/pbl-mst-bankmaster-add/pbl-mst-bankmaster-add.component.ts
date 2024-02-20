import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-pbl-mst-bankmaster-add',
  templateUrl: './pbl-mst-bankmaster-add.component.html',
  styleUrls: ['./pbl-mst-bankmaster-add.component.scss']
})
export class PblMstBankmasterAddComponent {

  reactiveform: FormGroup | any;
  bankmaster_list: any;
  accounttype_list: any;
  accountgroup_list: any;
  branchname_list: any;

  constructor(private formBuilder: FormBuilder, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.reactiveform = new FormGroup({
      bank_code: new FormControl('', Validators.required),
      bank_name: new FormControl('', Validators.required),
      account_no: new FormControl('', Validators.required),
      ifsc_code: new FormControl('', Validators.required),
      neft_code: new FormControl('', Validators.required),
      swift_code: new FormControl('', Validators.required),
      remarks: new FormControl('', Validators.required),
      accountgroup_gid: new FormControl(''),
      branch_name: new FormControl(''),
      accountgroup_name: new FormControl(''),
      account_type: new FormControl(''),
      openning_balance: new FormControl('', Validators.required),
      date: new FormControl('', Validators.required),
    })
  }

  get bank_code() {
    return this.reactiveform.get('bank_code')!;
  }
  get bank_name() {
    return this.reactiveform.get('bank_Name')!;
  }
  get account_no() {
    return this.reactiveform.get('account_no')!;
  }
  get openning_balance() {
    return this.reactiveform.get('openning_balance')!;
  }
  get date() {
    return this.reactiveform.get('date')!;
  }



  ngOnInit(): void {
    ////Drop Down///
    var url = 'PblMstBankMaster/GetAccountType'
    this.service.get(url).subscribe((result: any) => {
      this.accounttype_list = result.GetAccountType;
    });
    var url = 'PblMstBankMaster/GetAccountGroup'
    this.service.get(url).subscribe((result: any) => {
      this.accountgroup_list = result.GetAccountGroup;
    });
    var url = 'PblMstBankMaster/GetBranchName'
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
      var api = 'PblMstBankMaster/PostBankMaster';
      this.service.post(api, this.reactiveform.value).subscribe((result: any) => {
        if (result.status == false) {
          this.ToastrService.warning("error While Adding Bank Master")
          // this.BranchSummary();
        }
        else {
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
          this.reactiveform.get("date")?.setValue(null);
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
    this.router.navigate(['/finance/PblMstBankMasterSummary'])
  }
}
