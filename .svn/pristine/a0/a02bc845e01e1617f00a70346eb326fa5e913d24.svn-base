import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-ims-trn-opendcaddselect-update',
  templateUrl: './ims-trn-opendcaddselect-update.component.html',
  styleUrls: ['./ims-trn-opendcaddselect-update.component.scss']
})
export class ImsTrnOpendcaddselectUpdateComponent {
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '25rem',
    minHeight: '5rem',
    width: '1145px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };
  combinedFormData: FormGroup | any;
  opendcaddsel_list: any[] = [];
  salesorder_gid: any;
  opendc: any;
  opendcaddselprod_list: any[] = [];

  constructor(private http: HttpClient, private fb: FormBuilder,public NgxSpinnerService: NgxSpinnerService, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) {
    this.combinedFormData = new FormGroup({
      customer_address_so: new FormControl(''),
      shipping_to: new FormControl(''),
      dc_no: new FormControl(''),
      despatch_mode: new FormControl(''),
      tracker_id: new FormControl(''),
      despatch_quantity: new FormControl(''),
      template_name: new FormControl(''),
      termsandconditions: new FormControl(''),

    })

  }
  ngOnInit(): void {

    // this.combinedFormData = new FormGroup({
    //   customer_address_so: new FormControl(''),
    //   shipping_to: new FormControl(''),
    //   dc_no: new FormControl(''),
    //   despatch_mode: new FormControl(''),
    //   tracker_id: new FormControl(''),
    //   despatch_quantity: new FormControl(''),
    //   template_name: new FormControl(''),
    //   termsandconditions: new FormControl(''),

    // })

    debugger
    this.opendc = this.route.snapshot.paramMap.get('salesorder_gid');
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.opendc, secretKey).toString(enc.Utf8);
    this.GetOpenDcSummary(deencryptedParam);
    this.salesorder_gid = deencryptedParam;
  }


  GetOpenDcSummary(salesorder_gid: any) {
    debugger
    var api = 'ImsTrnOpenDcSummary/GetOpenDcUpdate';
    this.NgxSpinnerService.show()
    let params = {

      salesorder_gid: salesorder_gid,

    }
    this.service.getparams(api, params).subscribe((result: any) => {
      this.opendcaddsel_list = result.opendcaddsel_list;
      setTimeout(() => {
        $('#opendcaddsel_list').DataTable();
      }, 1);
      this.NgxSpinnerService.hide();


      this.combinedFormData.get("customer_address_so")?.setValue(this.opendcaddsel_list[0].customer_address_so);

    });

    var api = 'ImsTrnOpenDcSummary/GetOpenDcUpdateProd';
    let param1 = {

      salesorder_gid: salesorder_gid,

    }
    this.service.getparams(api, param1).subscribe((result: any) => {
      this.opendcaddselprod_list = result.opendcaddselprod_list;
      setTimeout(() => {
        $('#opendcaddselprod_list').DataTable();
      }, 1);
    });
  }

  OnDelDcSubmit() {

    debugger
    if (this.combinedFormData.value.despatch_quantity !== null && this.combinedFormData.value.despatch_quantity !== "") {
      
    
    console.log(this.opendcaddsel_list)
    var params = {

      directorder_refno: this.opendcaddsel_list[0].directorder_refno,
      directorder_date: this.opendcaddsel_list[0].directorder_date,
      customercontact_name: this.opendcaddsel_list[0].customercontact_name,
      customer_code: this.opendcaddsel_list[0].customer_code,
      stock_qty: this.opendcaddsel_list[0].stock_qty,
      customer_contact_person: this.opendcaddsel_list[0].customer_contact_person,
      customer_email: this.opendcaddsel_list[0].customer_email,
      customer_mobile: this.opendcaddsel_list[0].customer_mobile,
      salesorder_gid: this.opendcaddsel_list[0].salesorder_gid,
      customer_address_so: this.combinedFormData.value.customer_address_so,
      shipping_to: this.combinedFormData.value.shipping_to,
      dc_no: this.combinedFormData.value.dc_no,
      despatch_mode: this.combinedFormData.value.despatch_mode,
      tracker_id: this.combinedFormData.value.tracker_id,
      despatch_quantity: this.combinedFormData.value.despatch_quantity,
      termsandconditions: this.combinedFormData.value.termsandconditions,
      productgroup_name: this.opendcaddselprod_list[0].productgroup_name,
      product_name: this.opendcaddselprod_list[0].product_name,
      display_field: this.opendcaddselprod_list[0].display_field,
      uom_name: this.opendcaddselprod_list[0].uom_name,
      qty_quoted: this.opendcaddselprod_list[0].qty_quoted,
      available_quantity: this.opendcaddselprod_list[0].available_quantity,
    }
   

    var url = 'ImsTrnOpenDcSummary/PostOpenDcSubmit'
    this.NgxSpinnerService.show()

    this.service.post(url, params).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.ToastrService.success(result.message)
        this.router.navigate(['/ims/ImsTrnOpendcsummary']);   
        this.NgxSpinnerService.hide()
      }
    });
  }

else {
  this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
}
return;
  }

  redirecttolist(){
    this.router.navigate(['/ims/ImsTrnOpendcAddselect']);
  
  }
}
