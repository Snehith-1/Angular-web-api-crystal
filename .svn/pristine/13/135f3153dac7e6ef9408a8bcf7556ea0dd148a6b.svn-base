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
import { NgModel } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
export class IEmployee {
  enquiryaddprodeed_list: string[] = [];
  employee_gid:any;
}

@Component({
  selector: 'app-pmr-trn-enquiryadd-proceed',
  templateUrl: './pmr-trn-enquiryadd-proceed.component.html',
  styleUrls: ['./pmr-trn-enquiryadd-proceed.component.scss']
})

export class PmrTrnEnquiryaddProceedComponent   {
  enquiryaddprodeed_list: any[] = [];
  enquiryaddprodeed_list1: any[] = [];
  select_list: any[] = [];
  detailsdtl_list: any[] = [];
  reactiveForm!: FormGroup;
  selection = new SelectionModel<IEmployee>(true, []);
  monthyear: any;
  month:any;
  year:any;
  working_days: any;
  purchaserequisition_gid:any;
  data: any;

  
  constructor(    
    private renderer: Renderer2,
    private el: ElementRef,
    public service: SocketService,
    private ToastrService: ToastrService,
    private route: Router,
    private router: ActivatedRoute,
    private formBuilder: FormBuilder,
    public NgxSpinnerService:NgxSpinnerService,
    
       )
   { }
   ngOnInit(): void {
    const purchaserequisition_gid = this.router.snapshot.paramMap.get('purchaserequisition_gid');
    this.purchaserequisition_gid = purchaserequisition_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.purchaserequisition_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)

    this.GetEnquiryaddProceed(deencryptedParam);
    this.reactiveForm = this.formBuilder.group({
     
    });
   
    

  }
  GetEnquiryaddProceed(param1 :any) {
    this.NgxSpinnerService.show();
    var url = 'PmrTrnRequestforQuote/GetEnquiryaddProceed';
    let param = {
      purchaserequisition_gid : param1 
    };
    this.service.getparams(url, param).subscribe((result: any) => {
      this.enquiryaddprodeed_list = result.GetEnquiryaddProceed;
      this.NgxSpinnerService.hide();
    });
  }
  // onaddconfirm(){
  //   // const secretKey = 'storyboarderp';
  //   // const param = (params);
  //   // const encryptedParam = AES.encrypt(param,secretKey).toString(),encryptedParam;
  //   //this.route.navigate(['/pmr/PmrTrnEnquiryAddConfirm']);
  // }


  submit() {
    const secretKey = 'storyboarderp';
    const purchaserequisition_gid = AES.encrypt(this.purchaserequisition_gid, secretKey).toString();
   

    const selectedData = this.selection.selected; // Get the selected items
    if (selectedData.length === 0) {
      this.ToastrService.warning("Select Atleast one Product  to raise enquiry");
      return;
    } 
    
    for (const data of selectedData) {
      this.enquiryaddprodeed_list1.push(data);
 }

   var params={
    enquiryaddprodeed_list1 :this.enquiryaddprodeed_list1
   }
   var url = 'PmrTrnRequestforQuote/GetEnquiryproceed'
  this.NgxSpinnerService.show();
    this.service.postparams(url,params).subscribe((result: any) => {
      
      this.NgxSpinnerService.hide();
      if (result.status == false) {
        this.ToastrService.warning(result.message)
     }
     else{
      this.ToastrService.success(result.message)
      this.route.navigate(['/pmr/PmrTrnEnquiryAddConfirm', purchaserequisition_gid]);
     }
    });

  }
  
   isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.enquiryaddprodeed_list.length;
    return numSelected === numRows;
  }
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.enquiryaddprodeed_list.forEach((row: IEmployee) => this.selection.select(row));
  }
}