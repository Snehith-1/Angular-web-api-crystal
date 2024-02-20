import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';



@Component({
  selector: 'app-pmr-trn-vendorenquiry-view',
  templateUrl: './pmr-trn-vendorenquiry-view.component.html',
  styleUrls: ['./pmr-trn-vendorenquiry-view.component.scss']
})
export class PmrTrnVendorenquiryViewComponent {
  enquiry_gid:any;
  enquiry_date:any;
  branch_name:any;
  enquiry_referencenumber:any;
  vendor_companyname:any;
  contact_number:any;
  vendor_contact_person:any;
  contact_email:any;
  vendor_address:any;
  closure_date:any;


  viewvendorenquiry_list:any [] = [];
  vendor: any;
  responsedata: any;

  constructor(private formBuilder: FormBuilder,private route:Router,private router:ActivatedRoute,public service :SocketService,private NgxSpinnerService:NgxSpinnerService) {
   }

  ngOnInit(): void {
    const enquiry_gid =this.router.snapshot.paramMap.get('enquiry_gid');
     this.vendor= enquiry_gid;
     const secretKey = 'storyboarderp';
     const deencryptedParam = AES.decrypt(this.vendor,secretKey).toString(enc.Utf8);
     console.log(deencryptedParam)
     this.GetViewVendorEnquiry(deencryptedParam);
   }

   GetViewVendorEnquiry(enquiry_gid: any) {
    this.NgxSpinnerService.show();
    var url='PmrTrnRaiseEnquiry/GetViewVendorEnquiry'
    let param = {
      enquiry_gid : enquiry_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.responsedata=result;
    this.viewvendorenquiry_list = result.viewvendorenquiry_list;  
    this.enquiry_gid= this.viewvendorenquiry_list[0].enquiry_gid;
    this.enquiry_date= this.viewvendorenquiry_list[0].enquiry_date;
    this.branch_name= this.viewvendorenquiry_list[0].branch_name;
    this.enquiry_referencenumber= this.viewvendorenquiry_list[0].enquiry_referencenumber;
    this.vendor_companyname= this.viewvendorenquiry_list[0].vendor_companyname;
    this.contact_number= this.viewvendorenquiry_list[0].contact_number;
    this.vendor_contact_person= this.viewvendorenquiry_list[0].vendor_contact_person;
    this.contact_email= this.viewvendorenquiry_list[0].contact_email;
    this.vendor_address= this.viewvendorenquiry_list[0].vendor_address;
    this.closure_date= this.viewvendorenquiry_list[0].closure_date;

    this.NgxSpinnerService.hide();

    });
  }

}
