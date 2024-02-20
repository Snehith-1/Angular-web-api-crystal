import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';
import { NgxSpinnerService } from 'ngx-spinner';

export class IGrn {
  summary_list: string[] = [];
  addgrn_lists : string []=[];
  qtyreceivedas: string = "";
  qty_delivered: string = "";
  qty_grnadjusted: string = "";
  
}

interface IGrnadd {  
  branch_name: string;
  vendor_companyname: string;
  contactperson_name: string;
  dc_no: string;
  grn_date: string;
  invoice_date: string;
  dc_date: string;
  invoiceref_no: string;
  contact_telephonenumber: string;
  email_id: string;
  grn_remarks: string;
  address: string;
  grn_gid: string;
  qtyreceivedas: string;
  qty_delivered: string;
  qty_grnadjusted: string;
  user_firstname: string;
  user_firstname1: string;
  addgrn_lists: string
}

@Component({
  selector: 'app-pmr-trn-grninwardadd',
  templateUrl: './pmr-trn-grninwardadd.component.html',
  styleUrls: ['./pmr-trn-grninwardadd.component.scss']
})

export class PmrTrnGrninwardaddComponent implements OnInit {
  purchaseorder_gid: any;
  addgrnform!: FormGroup;
  productform!: FormGroup;
  grn_lists: any;
  tableData: any[] = [];
  summary_list: any[] = [];
  addgrn_lists: any[] = [];
  productgroup_list: any[] = [];
  userlist: any[] = [];
  summary_list1: any[] = [];
  file!: File;
  grnaadd!: IGrnadd;
  pick: Array<any> = [];
  invoicegid: any;
  qtyReceivedAs: string = '';
  parameter: any;
  parameterValue1: any;
  purchaseordergid: any;
  assignvisitlist: any[] = [];
  responsedata: any;
  CurObj: IGrn = new IGrn();
  selection = new SelectionModel<IGrn>(true, []);
  IGrn: any;
  purchaseorder_list: any;
  
  payment_days: any;
  delivery_days: any;
  total_amount: any;
  total_tax: any;
  discount_amount: any;
  addon_amount: any;
  freight_charges: any;
  buybackorscrap: any;
  grand_total: any;
  currency_code: any;

  
  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService, private ToastrService: ToastrService, private route: Router, private FormBuilder: FormBuilder, private router: ActivatedRoute,public NgxSpinnerService:NgxSpinnerService) {
    this.grnaadd = {} as IGrnadd;
  }

  ngOnInit(): void {
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    flatpickr('.date-picker', options)
    const purchaseorder_gid = this.router.snapshot.paramMap.get('purchaseorder_gid');
    this.purchaseordergid = purchaseorder_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.purchaseordergid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.Getaddgrnsummary(deencryptedParam);
    this.Getsummaryaddgrn(deencryptedParam);

    this.addgrnform = new FormGroup({
      grn_gid: new FormControl(''),
      grn_date: new FormControl(this.getCurrentDate()),
      branch_name: new FormControl(''),
      vendor_companyname: new FormControl(''),
      contactperson_name: new FormControl(''),
      contact_telephonenumber: new FormControl(''),
      email_id: new FormControl(''),
      address: new FormControl(''),

      purchaseorder_gid: new FormControl(''),
      user_firstname: new FormControl(''),
      user_firstname1: new FormControl(''),
      grn_remarks: new FormControl(''),

      dc_no: new FormControl('', [Validators.required]),
      dc_date: new FormControl(''),
      invoiceref_no: new FormControl(''),
      invoice_date: new FormControl(''),
      priority_flag: new FormControl('N', Validators.required),

      file: new FormControl(''),
      qtyreceivedas: new FormControl(''),
      qty_delivered: new FormControl(''),
      qty_grnadjusted: new FormControl(''),      
      addgrn_lists: this.FormBuilder.array([]),
      required: new FormControl('Y', Validators.required),
    });
  }
  getCurrentDate(): string {
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
    const yyyy = today.getFullYear();
   
    return dd + '-' + mm + '-' + yyyy;
  }

  Getaddgrnsummary(purchaseorder_gid: any) {
    debugger;
 
    var url = 'PmrTrnGrn/Getaddgrnsummary'
    this.NgxSpinnerService.show()
    let param = {
      purchaseorder_gid: purchaseorder_gid
    }
    debugger;
    this.service.getparams(url, param).subscribe((result: any) => {
      this.grn_lists = result.grn_lists;
      console.log(this.grn_lists)
      this.addgrnform.get("branch_name")?.setValue(this.grn_lists[0].branch_name);
      this.addgrnform.get("vendor_companyname")?.setValue(this.grn_lists[0].vendor_companyname);
      this.addgrnform.get("contactperson_name")?.setValue(this.grn_lists[0].contactperson_name);
      this.addgrnform.get("dc_no")?.setValue(this.grn_lists[0].dc_no);
      this.addgrnform.get("invoiceref_no")?.setValue(this.grn_lists[0].invoiceref_no);
      this.addgrnform.get("grn_date")?.setValue(this.grn_lists[0].grn_date);
      this.addgrnform.get("invoice_date")?.setValue(this.grn_lists[0].invoice_date);
      this.addgrnform.get("dc_date")?.setValue(this.grn_lists[0].dc_date);
      this.addgrnform.get("contact_telephonenumber")?.setValue(this.grn_lists[0].contact_telephonenumber);
      this.addgrnform.get("email_id")?.setValue(this.grn_lists[0].email_id);
      this.addgrnform.get("grn_remarks")?.setValue(this.grn_lists[0].grn_remarks);
      this.addgrnform.get("address")?.setValue(this.grn_lists[0].address);
      this.addgrnform.get("purchaseorder_gid")?.setValue(this.grn_lists[0].purchaseorder_gid);
      this.addgrnform.get("grn_gid")?.setValue(this.grn_lists[0].grn_gid);
      this.addgrnform.get("user_firstname")?.setValue(this.grn_lists[0].user_firstname);
      this.addgrnform.get("user_firstname1")?.setValue(this.grn_lists[0].user_firstname1);
      this.NgxSpinnerService.hide();
    });
    
  }
  Getsummaryaddgrn(purchaseorder_gid: any) {
    debugger;
    this.NgxSpinnerService.show();
    var url = 'PmrTrnGrn/Getsummaryaddgrn'
    let param = {
      purchaseorder_gid: purchaseorder_gid
    }
    debugger;
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
          this.addgrn_lists = this.responsedata.addgrn_list;
          console.log(this.addgrn_lists)
      this.addgrnform.get("productgroup_name")?.setValue(this.addgrn_lists[0].productgroup_name);
      this.addgrnform.get("product_name")?.setValue(this.addgrn_lists[0].product_name);
      this.addgrnform.get("productuom_name")?.setValue(this.addgrn_lists[0].productuom_name);
      this.addgrnform.get("qty_ordered")?.setValue(this.addgrn_lists[0].qty_ordered);
      // this.addgrnform.get("qty_received")?.setValue(this.addgrn_lists[0].qty_received);

      for (let i = 0; i < this.addgrn_lists.length; i++) {
        this.addgrnform.addControl(`qtyreceivedas_${i}`, new FormControl(this.addgrn_lists[i].qtyreceivedas));
        this.addgrnform.addControl(`qty_delivered_${i}`, new FormControl(this.addgrn_lists[i].qty_delivered));
        this.addgrnform.addControl(`qty_grnadjusted_${i}`, new FormControl(this.addgrn_lists[i].qty_grnadjusted));
        this.addgrnform.addControl(`qty_received_${i}`, new FormControl(this.addgrn_lists[i].qty_received));
      }
      this.NgxSpinnerService.hide();
    });
  }

  onChange2(event: any) {
    this.file = event.target.files[0];
  }

  get branch_name() {
    return this.addgrnform.get('branch_name')!;
  }
  get vendor_companyname() {
    return this.addgrnform.get('vendor_companyname')!;
  }
  get contactperson_name() {
    return this.addgrnform.get('contactperson_name')!;
  }
  get dc_no() {
    return this.addgrnform.get('dc_no')!;
  }
  get grn_date() {
    return this.addgrnform.get('grn_date')!;
  }
  get invoice_date() {
    return this.addgrnform.get('invoice_date')!;
  }
  get dc_date() {
    return this.addgrnform.get('dc_date')!;
  }
  get invoiceref_no() {
    return this.addgrnform.get('invoiceref_no')!;
  }
  get contact_telephonenumber() {
    return this.addgrnform.get('contact_telephonenumber')!;
  }
  get email_id() {
    return this.addgrnform.get('email_id')!;
  }
  get grn_remarks() {
    return this.addgrnform.get('grn_remarks')!;
  }
  get address() {
    return this.addgrnform.get('address')!;
  }
  // get purchaseorder_gid() {
  //   return this.addgrnform.get('purchaseorder_gid')!;
  // }
  get grn_gid() {
    return this.addgrnform.get('grn_gid')!;
  }
  get user_firstname() {
    return this.addgrnform.get('user_firstname')!;
  }
  get qtyreceivedas() {
    return this.addgrnform.get('qtyreceivedas')!;
  }
  get user_firstname1() {
    return this.addgrnform.get('user_firstname1')!;
  }
  get qty_delivered() {
    return this.addgrnform.get('qty_delivered')!;
  }
  get qty_grnadjusted() {
    return this.addgrnform.get('qty_grnadjusted')!;
  }
  get dc_noControl() {
    return this.addgrnform.get('dc_no')!;
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.addgrn_lists.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.addgrn_lists.forEach((row: IGrn) => this.selection.select(row));
  }

  onsubmit() {

    this.selection.selected.forEach(selectedRow => {
      const rowIndex = this.addgrn_lists.findIndex(row => row === selectedRow);
      if (rowIndex !== -1) {
        this.addgrn_lists[rowIndex].qtyreceivedas = this.addgrnform.get('qtyreceivedas_' + rowIndex)?.value;
        this.addgrn_lists[rowIndex].qty_delivered = this.addgrnform.get('qty_delivered_' + rowIndex)?.value;
        this.addgrn_lists[rowIndex].qty_grnadjusted = this.addgrnform.get('qty_grnadjusted_' + rowIndex)?.value;
      }
    });


    this.pick = this.selection.selected
    let list = this.pick
    this.CurObj.summary_list = list
    // Assuming you want to update the form values based on the selection
    this.addgrn_lists.forEach((data, index) => {
      if (this.selection.isSelected(data)) {
        this.addgrnform.get('qtyreceivedas_' + index)?.setValue(data.qtyreceivedas);
        this.addgrnform.get('qty_delivered_' + index)?.setValue(data.qty_delivered);
        this.addgrnform.get('qty_grnadjusted_' + index)?.setValue(data.qty_grnadjusted);
      }
    });

    //this.pick = this.selection.selected;
    this.CurObj.qtyreceivedas = this.addgrnform.value.qtyreceivedas;
    this.CurObj.qty_delivered = this.addgrnform.value.qty_delivered;
    this.CurObj.qty_grnadjusted = this.addgrnform.value.qty_grnadjusted;
    ///this.CurObj.summary_list = this.pick;

    if (this.CurObj.summary_list.length !== 0) {
      debugger
      for (const control of Object.keys(this.addgrnform.controls)) {
        this.addgrnform.controls[control].markAsTouched();
      }

      const selectedData = this.selection.selected; // Get the selected items
      if (selectedData.length === 0) {
        this.ToastrService.warning("Select Atleast one Employee to payrun");
        return;
      } 
      
      for (const data of selectedData) {
        this.summary_list1.push(data);
   }

    debugger
      const requestData = {
        
        branch_name: this.addgrnform.value.branch_name,
        vendor_companyname: this.addgrnform.value.vendor_companyname,
        contactperson_name: this.addgrnform.value.contactperson_name,
        dc_no: this.addgrnform.value.dc_no,
        invoiceref_no: this.addgrnform.value.invoiceref_no,
        grn_date: this.addgrnform.value.grn_date,
        invoice_date: this.addgrnform.value.invoice_date,
        dc_date: this.addgrnform.value.dc_date,
        contact_telephonenumber: this.addgrnform.value.contact_telephonenumber,
        email_id: this.addgrnform.value.email_id,
        grn_remarks: this.addgrnform.value.grn_remarks,
        purchaseorder_gid: this.addgrnform.value.purchaseorder_gid,
        address: this.addgrnform.value.address,
        grn_gid: this.addgrnform.value.grn_gid,
        user_firstname: this.addgrnform.value.user_firstname,
        user_firstname1: this.addgrnform.value.user_firstname1,
        priority_flag: this.addgrnform.value.priority_flag,
        summary_list: this.summary_list1,
      };

      // var url = 'PmrTrnGrn/PostGrnSubmit';

      // this.service.post(url, requestData).subscribe((result: any) => {
      //   if (result.status == false) {
      //     this.ToastrService.warning(result.message);
      //     // this.Getsummaryaddgrn();
      //   } else {
      //     this.addgrnform.reset();
      //     this.route.navigate(['/pmr/PmrTrnGrninward']);
      //     this.ToastrService.success(result.message)
      //   }
        
    var url='PmrTrnGrn/PostGrnSubmit'
    this.NgxSpinnerService.show(); 

 
    this.service.post(url, requestData).subscribe((result: any) => {
     
      if(result.status ==false){
        this.NgxSpinnerService.hide();
        this.ToastrService.warning(result.message)
     
      }
      else{
        this.NgxSpinnerService.hide();
        this.ToastrService.success(result.message)
        this.route.navigate(['/pmr/PmrTrnGrninward']);
       
      }
      this.NgxSpinnerService.hide();
      });
    } else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ');
    }
  }

  event(data: any, i: number) {
    const qty_received = +this.addgrnform.get(`qty_received_${i}`)?.value || 0; 
    const qty_received_as = +data.qtyreceivedas || 0;
    const sum = qty_received + qty_received_as;
    this.addgrnform.get([`qty_delivered_${i}`])?.setValue(sum);
  }
  
     GetPurchaseOrderDetails() {
      debugger
      this.NgxSpinnerService.show();
    var url = 'PmrTrnGrnInward/GetPurchaseOrderDetails'
    const purchaseorder_gid = this.router.snapshot.paramMap.get('purchaseorder_gid');
    this.purchaseordergid = purchaseorder_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.purchaseordergid, secretKey).toString(enc.Utf8);
    let param = {
           purchaseorder_gid : deencryptedParam 
      }
    this.service.getparams(url,param).subscribe((result: any) => {
      // $('#purchaseorder_list').DataTable().destroy();
      this.responsedata = result;
      this.purchaseorder_list = this.responsedata.Getpurchaseorder_list;

      this.payment_days = this.purchaseorder_list[0].payment_days;
      this.delivery_days = this.purchaseorder_list[0].delivery_days;

      this.total_amount = this.purchaseorder_list[0].total_amount;
      this.total_tax = this.purchaseorder_list[0].total_tax;
      this.discount_amount = this.purchaseorder_list[0].discount_amount;
      this.addon_amount = this.purchaseorder_list[0].addon_amount;
      this.freight_charges = this.purchaseorder_list[0].freight_charges;
      this.buybackorscrap = this.purchaseorder_list[0].buybackorscrap;
      this.grand_total = this.purchaseorder_list[0].grand_total;
      this.currency_code = this.purchaseorder_list[0].currency_code;

      // setTimeout(() => {
      //   $('#purchaseorder_list').DataTable();
      // }, 1);
      this.NgxSpinnerService.hide();
    })
  }
}