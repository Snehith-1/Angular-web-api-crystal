import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AES } from 'crypto-js';

import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';

interface ILeadmaster {

  leadbank_gid: string;
  leadbank_name: string;
  contact_details: string;
  region_city_state: string;
  source: string;
  remarks: string;
  status: string;
  assigned_to: string;
}
@Component({
  selector: 'app-crm-trn-leadmastersummary',
  templateUrl: './crm-trn-leadmastersummary.component.html',
  styleUrls: ['./crm-trn-leadmastersummary.component.scss']
})
export class CrmTrnLeadmastersummaryComponent {

  reactiveForm!: FormGroup;
  reactiveFormEdit!: FormGroup;
  responsedata: any;
  parameterValue: any;
  parameterValue1: any;
  remarks: any;
  leadbank_name: any;
  leadmaster_list: any[] = [];
  GetRegisterLeadSummary_list2: any[] = [];
  GetRegisterLeadSummary_list1: any[] = [];
  RegisterLeadCountList: any[] = [];

  formData: any = {};
  isSubmitting = false;
  leadmaster!: ILeadmaster;

  constructor(public service: SocketService, private route: Router, private ToastrService: ToastrService, private NgxSpinnerService: NgxSpinnerService) {
    this.leadmaster = {} as ILeadmaster;
  }
  ngOnInit(): void {

    this.GetRegisterLeadSummary();
    this.GetRegisterLeadSummary1();
    this.GetRegisterLeadSummary2();
    // Form values for Add popup/////

    this.reactiveForm = new FormGroup({
      leadbank_gid: new FormControl(this.leadmaster.leadbank_gid, [
        Validators.required,
      ]),
      leadbank_name: new FormControl(this.leadmaster.leadbank_name, [
        Validators.required,
      ]),
    });

    var api = 'registerlead/GetRegisterLeadCount'
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.RegisterLeadCountList = this.responsedata.RegisterLeadCount_List;
    });
  }

  GetRegisterLeadSummary1() {
    this.NgxSpinnerService.show();
    var api3 = 'registerlead/GetRegisterLeadSummary1'
    this.service.get(api3).subscribe((result: any) => {
      $('#GetRegisterLeadSummary_list1').DataTable().destroy();
      this.responsedata = result;
      this.GetRegisterLeadSummary_list1 = this.responsedata.GetRegisterLeadSummary_list1;
      this.NgxSpinnerService.hide();
      //console.log(this.source_list)
      setTimeout(() => {
        $('#GetRegisterLeadSummary_list1').DataTable();
      }, 1);
    });
  }
  GetRegisterLeadSummary2() {
    this.NgxSpinnerService.show();
    var api3 = 'registerlead/GetRegisterLeadSummary2'
    this.service.get(api3).subscribe((result: any) => {
      $('#GetRegisterLeadSummary_list2').DataTable().destroy();
      this.responsedata = result;
      this.GetRegisterLeadSummary_list2 = this.responsedata.GetRegisterLeadSummary_list2;
      this.NgxSpinnerService.hide();
      //console.log(this.source_list)
      setTimeout(() => {
        $('#GetRegisterLeadSummary_list2').DataTable();
      }, 1);
    });
  }
  GetRegisterLeadSummary() {
    this.NgxSpinnerService.show();
    var api3 = 'registerlead/GetRegisterLeadSummary'
    this.service.get(api3).subscribe((result: any) => {
      $('#leadmaster_list').DataTable().destroy();
      this.responsedata = result;
      this.leadmaster_list = this.responsedata.GetRegisterLeadSummary_list;
      this.NgxSpinnerService.hide();
      //console.log(this.source_list)
      setTimeout(() => {
        $('#leadmaster_list').DataTable();
      }, 1);
    });
  }
  //Delete //
  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  }

  ondelete() {
    console.log(this.parameterValue);
    var url = 'registerlead/deleteregisterleadSummary'
    let param = {
      leadbank_gid: this.parameterValue
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      if (result.status == false) {
        window.scrollTo({
          top: 0, // Code is used for scroll top after event done
        });
       
        this.ToastrService.warning(result.message)
        setTimeout(() => {
          window.location.reload();
        }, 2000);
      }
      else {
        window.scrollTo({
          top: 0, // Code is used for scroll top after event done
        });
        window.location.reload()
        this.ToastrService.success(result.message)
      }
      this.GetRegisterLeadSummary();
      setTimeout(() => {
        window.location.reload();
      }, 2000);
    });

  }
  //delete 1//
  onadd() {

    const secretKey = 'storyboarderp';
    const lspage1 = 'Registerleaddistributor';
    const lspage = AES.encrypt(lspage1, secretKey).toString();
    this.route.navigate(['/crm/CrmTrnLeadmasteradd', lspage]);
  }

  onview(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const lspage1 = "Registerleaddistributor";
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    const lspage = AES.encrypt(lspage1, secretKey).toString();
    console.log(encryptedParam);
    this.route.navigate(['/crm/CrmTrnLeadbankview', encryptedParam, lspage])
  }
  onedit(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const lspage1 = "Registerleaddistributor";
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    const lspage = AES.encrypt(lspage1, secretKey).toString();
    this.route.navigate(['/crm/CrmTrnLeadbankedit', encryptedParam, lspage])
  }
  popmodal(parameter: string, parameter1: string) {

    this.remarks = parameter;
    this.leadbank_name = parameter1;
  }


}