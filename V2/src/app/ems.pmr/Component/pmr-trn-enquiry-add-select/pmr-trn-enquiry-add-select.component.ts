import { Component, OnInit, OnDestroy, ChangeDetectorRef, Renderer2, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { SelectionModel } from '@angular/cdk/collections';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { Subscription, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { NgbTimepickerModule, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';

export class IEmployee {
  enquiryselect_list: string[] = [];
  employee_gid:any;
}

@Component({
  selector: 'app-pmr-trn-enquiry-add-select',
  templateUrl: './pmr-trn-enquiry-add-select.component.html',
  styleUrls: ['./pmr-trn-enquiry-add-select.component.scss']
})

export class PmrTrnEnquiryAddSelectComponent  {
  enquiryselect_list: any[] = [];
  enquiryselect_list1: any[] = [];
  select_list: any[] = [];
  detailsdtl_list: any[] = [];
  reactiveForm!: FormGroup;
  selection = new SelectionModel<IEmployee>(true, []);
  monthyear: any;
  month:any;
  year:any;
  working_days: any;
  parameterValue1:any;
  responsedata:any;
  GetEnquirySelectgrid_lists:any[] = [];
  purchaserequisition_gid:any;
  
  constructor(    
    private renderer: Renderer2,
    private el: ElementRef,
    public service: SocketService,
    private ToastrService: ToastrService,
    private route: Router,
    private router: ActivatedRoute,
    private formBuilder: FormBuilder,
    private NgxSpinnerService:NgxSpinnerService
       )
   { }
   ngOnInit(): void {
    const secretKey = 'storyboarderp';
    this.GetEnquirySelect();
    this.reactiveForm = this.formBuilder.group({
     
    });
   
    

  }
  GetEnquirySelect() {
    var url = 'PmrTrnRequestforQuote/GetEnquirySelect';
    this.NgxSpinnerService.show();
    let param = {
    };
    this.service.getparams(url, param).subscribe((result: any) => {
      this.enquiryselect_list = result.GetEnquirySelect;
      this.NgxSpinnerService.hide();
    });
  }

  onaddproceed(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/pmr/PmrTrnEnquiryaddProceed',encryptedParam]);
  }

  
  submit() {
    const secretKey = 'storyboarderp';
    const purchaserequisition_gid = AES.encrypt(this.purchaserequisition_gid, secretKey).toString();
  
    const selectedData = this.selection.selected; // Get the selected items
    if (selectedData.length === 0) {
      this.ToastrService.warning("Select at least one raise an enquiry");
      return;
    }

    for (const data of selectedData) {
      this.enquiryselect_list1.push(data);
    }
    this.purchaserequisition_gid = this.enquiryselect_list1[0].purchaserequisition_gid;
    console.log(this.enquiryselect_list1)
 
    const encryptedParam = AES.encrypt(this.purchaserequisition_gid, secretKey).toString();
    this.route.navigate(['/pmr/PmrTrnEnquiryaddProceed', encryptedParam]);
  }
  redirecttolist() {
    this.route.navigate(['/pmr/PmrTrnRequestForQuoteSummary']);

  }
  
  
   isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.enquiryselect_list.length;
    return numSelected === numRows;
  }
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.enquiryselect_list.forEach((row: IEmployee) => this.selection.select(row));
  }
  Details(parameter: string,purchaserequisition_gid: string){
    this.parameterValue1 = parameter;
    this.purchaserequisition_gid = parameter;
    
    var url = 'PmrTrnRequestforQuote/GetEnquirySelectgrid'
    let param = {
      purchaserequisition_gid: purchaserequisition_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.GetEnquirySelectgrid_lists = result.GetEnquirySelectgrid_lists;
      console.log(this.GetEnquirySelectgrid_lists)
      setTimeout(() => {
        $('#GetEnquirySelectgrid_lists').DataTable();
      }, 1);
  
    });
  }



}