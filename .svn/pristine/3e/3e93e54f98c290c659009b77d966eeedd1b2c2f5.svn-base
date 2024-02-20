import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { AES, enc } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
interface Ipurchaseview {
  purchaseorder_gid: string;
  manualporef_no: string;
  purchaseorder_date: string;
  branch_name: string;
  vendor_companyname: string;
  deliverytobranch: string;
  vendor_contactnumber: string;
  vendor_contact_person: string;
  vendor_faxnumber: string;
  vendor_emailid: string;
  vendor_address: string;
  exchange_rate: string;
  ship_via: string;
  payment_terms: string;
  freight_terms: string;
  delivery_location: string;
  currency_code: string;
  shipping_address: string;
  purchaseorder_reference: string;
  purchaseorder_remarks: string;
  productgroup_name: string;
  product_code: string;
  product_name: string;
  productuom_name: string;
  qty_ordered: string;
  product_price: string;
  discount_percentage: string;
  tax_gid: string;
  packing_charges: string;
  insurance_charges: string;
  payment_days: string;
  termsandconditions: string;
  priority:string;

}
@Component({
  selector: 'app-pmr-trn-purchaseorder-view',
  templateUrl: './pmr-trn-purchaseorder-view.component.html',
  styleUrls: ['./pmr-trn-purchaseorder-view.component.scss']
})
export class PmrTrnPurchaseorderViewComponent {
  
  config: AngularEditorConfig = {
    editable: false,
    spellcheck: false,
    height: '25rem',
    minHeight: '5rem',
    width: '1230px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };

  purchaseorder_gid: any;
  po_no:any;
  purchaseorder_date:any;
  branch_name:any;
  vendor_companyname:any;
  deliverytobranch:any;
  vendor_contactnumber:any;
  vendor_contact_person:any;
  vendor_faxnumber:any;
  vendor_emailid:any;
  vendor_address:any;
  exchange_rate:any;
  ship_via:any;
  payment_terms:any;
  freight_terms:any;
  delivery_location:any;
  currency_code:any;
  shipping_address:any;
  purchaseorder_reference:any;
  priority_n:any;
  purchaseorder_remarks:any;

  total_amount:any;
  tax_amount:any;
  addon_amount:any;
  discount_amount:any;
  freighttax_amount:any;
  buybackorscrap:any;
  packing_charges:any;
  insurance_charges:any;
  roundoff:any;
  payment_days:any;
  delivery_days:any;
  termsandconditions:any;
  overall_tax:any;








  purchaseorder_list: any;
  params:any;
  purchaseview!: Ipurchaseview;
  parameterValue1: any;
  purchaseorder:any;

  constructor(private formBuilder: FormBuilder,private route:Router,private router:ActivatedRoute,public service :SocketService) {
    this.purchaseview = {} as Ipurchaseview;
  }


  ngOnInit(): void {
    debugger
    this.purchaseorder= this.router.snapshot.paramMap.get('purchaseorder_gid');
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.purchaseorder,secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.GetViewPurchaseOrderSummary(deencryptedParam);    
  }
  GetViewPurchaseOrderSummary(purchaseorder_gid: any) {
    debugger
    var url='PmrTrnPurchaseOrder/GetViewPurchaseOrderSummary'
    let param = {
      purchaseorder_gid : purchaseorder_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.purchaseorder_list = result.GetViewPurchaseOrder;
    this.purchaseorder_gid=this.purchaseorder_list[0].purchaseorder_gid;
    this.po_no=this.purchaseorder_list[0].po_no;
    this.purchaseorder_date=this.purchaseorder_list[0].purchaseorder_date;
    this.branch_name=this.purchaseorder_list[0].branch_name;
    this.deliverytobranch=this.purchaseorder_list[0].deliverytobranch;
    this.vendor_companyname=this.purchaseorder_list[0].vendor_companyname;

    this.vendor_contactnumber=this.purchaseorder_list[0].vendor_contactnumber;
    this.vendor_contact_person=this.purchaseorder_list[0].vendor_contact_person;
    this.vendor_faxnumber=this.purchaseorder_list[0].vendor_faxnumber;
    this.vendor_emailid=this.purchaseorder_list[0].vendor_emailid;
    this.vendor_address=this.purchaseorder_list[0].vendor_address;
    this.exchange_rate=this.purchaseorder_list[0].exchange_rate;
    this.ship_via=this.purchaseorder_list[0].ship_via;
    this.payment_terms=this.purchaseorder_list[0].payment_terms;
    this.freight_terms=this.purchaseorder_list[0].freight_terms;
    this.delivery_location=this.purchaseorder_list[0].delivery_location;
    this.currency_code=this.purchaseorder_list[0].currency_code;
    this.shipping_address=this.purchaseorder_list[0].shipping_address;
    this.purchaseorder_reference=this.purchaseorder_list[0].purchaseorder_reference;
    this.priority_n=this.purchaseorder_list[0].priority_n;
    this.purchaseorder_remarks=this.purchaseorder_list[0].purchaseorder_remarks;

    this.total_amount=this.purchaseorder_list[0].total_amount;
    this.tax_amount=this.purchaseorder_list[0].tax_amount;
    this.addon_amount=this.purchaseorder_list[0].addon_amount;
    this.discount_amount=this.purchaseorder_list[0].discount_amount;
    this.freighttax_amount=this.purchaseorder_list[0].freighttax_amount;
    this.freighttax_amount=this.purchaseorder_list[0].freighttax_amount;
    this.buybackorscrap=this.purchaseorder_list[0].buybackorscrap;
    this.packing_charges=this.purchaseorder_list[0].packing_charges;
    this.insurance_charges=this.purchaseorder_list[0].insurance_charges;
    this.roundoff=this.purchaseorder_list[0].roundoff;
    this.total_amount=this.purchaseorder_list[0].total_amount;
    this.payment_days=this.purchaseorder_list[0].payment_days;
    this.delivery_days=this.purchaseorder_list[0].delivery_days;
    this.termsandconditions=this.purchaseorder_list[0].termsandconditions;
    this.overall_tax=this.purchaseorder_list[0].overall_tax;





  });
  }
  


}
