import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
interface IGrnchecker {

  product_gid:string;
  qty_delivered:string;
      qty_shortage:string;
      qty_delivereds:string;
      rejected_qty:string;
      display_field:string;
      location_name:string;
      bin_number:string;
      GetGrnQcChecker_lists:string;
      grn_gid:string;
      vendor_gid:string;
    }
@Component({
  selector: 'app-pmr-trn-grnqcchecker',
  templateUrl: './pmr-trn-grnqcchecker.component.html',
  styleUrls: ['./pmr-trn-grnqcchecker.component.scss']
})
export class PmrTrnGrnqccheckerComponent {
  GetGrnQcChecker_list : any;
  Grnchecker!: IGrnchecker;
  GetGrnQcChecker_lists : any;
  file: any;
  reactiveForm!: FormGroup;
  vendor: any;
  responsedata: any;
  grn_gid: any;
  grninward: any;
  grngid: any;
  journal_refno : any;
  grn_remarks : any;
  grn_reference: any;
  vendor_gid:any;
  purchaseorderdtl_gid:any;
  grndtl_gid:any;
  purchaseorder_gid:any;
  rejected_qty: number=0;


  constructor(private route:ActivatedRoute,
    private router:Router,private ToastrService:ToastrService,
    public service :SocketService,private formBuilder:FormBuilder  ) { 

  }
  ngOnInit(): void {

    this.reactiveForm = new FormGroup({
      grn_gid: new FormControl(''),
      grn_date: new FormControl(''),
      vendor_companyname: new FormControl(''),
      vendor_contact_person: new FormControl(''),
      contact_telephonenumber: new FormControl(''),
      email_id: new FormControl(''),
      address: new FormControl(''),
      user_firstname: new FormControl(''),
      purchaseorder_list: new FormControl(''),
      user_checkername: new FormControl(''),
      dc_no: new FormControl(''),
      grn_remarks: new FormControl(''),
      grn_reference: new FormControl(''),
      productgroup_name: new FormControl(''),
      product_code: new FormControl(''),
      product_name: new FormControl(''),
      productuom_name: new FormControl(''),
      qty_delivered: new FormControl(''),
      qty_shortage: new FormControl(''),
      qty_delivereds: new FormControl(''),
      rejected_qty: new FormControl(''),
      display_field: new FormControl(''),
      location_name: new FormControl(''),
      bin_number: new FormControl(''),
      vendor_gid:new FormControl(''),
      purchaseorderdtl_gid:new FormControl(''),
      grndtl_gid:new FormControl(''),
      purchaseorder_gid:new FormControl(''),
      GetGrnQcChecker_lists:this.formBuilder.array([]),

    })
  
    
    debugger
    
        this.grninward= this.route.snapshot.paramMap.get('grn_gid');
        const secretKey = 'storyboarderp';
        const deencryptedParam = AES.decrypt(this.grninward,secretKey).toString(enc.Utf8);
        console.log(deencryptedParam)
        this.GetPmrTrnGrnQcchecker(deencryptedParam);    
      }
      GetPmrTrnGrnQcchecker(grn_gid: any) {
        var url='PmrTrnGrnQcchecker/GetPmrTrnGrnQcchecker'
        let param = {
          grn_gid : grn_gid
        }
        this.service.getparams(url,param).subscribe((result:any)=>{
        this.GetGrnQcChecker_list = result.GetGrnQcChecker_list;
        //console.log(this.employeeedit_list)
      this.reactiveForm.get("grn_gid")?.setValue(this.GetGrnQcChecker_list[0].grn_gid);
      this.reactiveForm.get("grn_date")?.setValue(this.GetGrnQcChecker_list[0].grn_date);
      this.reactiveForm.get("vendor_gid")?.setValue(this.GetGrnQcChecker_list[0].vendor_gid);
      this.reactiveForm.get("vendor_companyname")?.setValue(this.GetGrnQcChecker_list[0].vendor_companyname);
      this.reactiveForm.get("vendor_contact_person")?.setValue(this.GetGrnQcChecker_list[0].vendor_contact_person);
      this.reactiveForm.get("contact_telephonenumber")?.setValue(this.GetGrnQcChecker_list[0].contact_telephonenumber);
      this.reactiveForm.get("email_id")?.setValue(this.GetGrnQcChecker_list[0].email_id);
      this.reactiveForm.get("address")?.setValue(this.GetGrnQcChecker_list[0].address);
      this.reactiveForm.get("user_firstname")?.setValue(this.GetGrnQcChecker_list[0].user_firstname);
      this.reactiveForm.get("purchaseorder_list")?.setValue(this.GetGrnQcChecker_list[0].purchaseorder_list);
      this.reactiveForm.get("user_checkername")?.setValue(this.GetGrnQcChecker_list[0].user_checkername);
      this.reactiveForm.get("dc_no")?.setValue(this.GetGrnQcChecker_list[0].dc_no);
      this.reactiveForm.get("grn_remarks")?.setValue(this.GetGrnQcChecker_list[0].grn_remarks);
      this.reactiveForm.get("grn_reference")?.setValue(this.GetGrnQcChecker_list[0].grn_reference);
      this.reactiveForm.get("purchaseorderdtl_gid")?.setValue(this.GetGrnQcChecker_list[0].purchaseorderdtl_gid);
      this.reactiveForm.get("purchaseorder_gid")?.setValue(this.GetGrnQcChecker_list[0].purchaseorder_gid);
      });

      this.grninward= this.route.snapshot.paramMap.get('grn_gid');
        const secretKey = 'storyboarderp';
        const deencryptedParam = AES.decrypt(this.grninward,secretKey).toString(enc.Utf8);
        console.log(deencryptedParam)
        this.GetPmrTrnGrnQccheckerpo(deencryptedParam);    
      }
      GetPmrTrnGrnQccheckerpo(grn_gid: any) {
        var url='PmrTrnGrnQcchecker/GetPmrTrnGrnQccheckerpo'
        let param = {
          grn_gid : grn_gid
        }
        this.service.getparams(url,param).subscribe((result:any)=>{
        this.  GetGrnQcChecker_lists = result.GetGrnQcChecker_lists;
        const GetGrnQcChecker_lists = this.GetGrnQcChecker_lists;
        //console.log(this.employeeedit_list)
        this.reactiveForm.get("productgroup_name")?.setValue(this.GetGrnQcChecker_lists[0].productgroup_name);
        this.reactiveForm.get("product_code")?.setValue(this.GetGrnQcChecker_lists[0].product_code);
        this.reactiveForm.get("product_gid")?.setValue(this.GetGrnQcChecker_lists[0].product_gid);
        this.reactiveForm.get("product_name")?.setValue(this.GetGrnQcChecker_lists[0].product_name);
        this.reactiveForm.get("productuom_name")?.setValue(this.GetGrnQcChecker_lists[0].productuom_name);
        this.reactiveForm.get("qty_delivered")?.setValue(this.GetGrnQcChecker_lists[0].qty_delivered);
        this.reactiveForm.get("location_name")?.setValue(this.GetGrnQcChecker_lists[0].location_name);
        this.reactiveForm.get("bin_number")?.setValue(this.GetGrnQcChecker_lists[0].bin_number);
        this.reactiveForm.get("purchaseorderdtl_gid")?.setValue(this.GetGrnQcChecker_lists[0].purchaseorderdtl_gid);
        this.reactiveForm.get("grndtl_gid")?.setValue(this.GetGrnQcChecker_lists[0].grndtl_gid);
        for (let i = 0; i < this.GetGrnQcChecker_lists.length; i++) {
       
          this.reactiveForm.addControl(`qty_shortage_${i}`, new FormControl(this.GetGrnQcChecker_lists[i].qty_shortage));
          this.reactiveForm.addControl(`qty_delivered_${i}`, new FormControl(this.GetGrnQcChecker_lists[i].qty_delivered));
          this.reactiveForm.addControl(`rejected_qty_${i}`, new FormControl(this.GetGrnQcChecker_lists[i].rejected_qty));
          this.reactiveForm.addControl(`display_field_${i}`, new FormControl(this.GetGrnQcChecker_lists[i].display_field));
        }
    
      });
      
    
      }

//       validate(){
//         debugger
//         console.log(this.reactiveForm.value)
//         this.grninward = this.reactiveForm.value;
//         if(   this.grninward.qty_delivered !=null && this.grninward.Qty_Shortage !=null  && this.grninward.Qty_Delivered !=null){
//           let formData = new FormData();
//           if(this.file !=null &&  this.file != undefined){
            
//          formData.append("Qty_Rejected", this.grninward.Qty_Rejected);
//          formData.append("Qty_Shortage", this.grninward.Qty_Shortage);
//          formData.append("Qty_Delivered", this.grninward.Qty_Delivered);
// debugger
//              for (const item of this.grninward) {
//                const Qty_Rejected = parseFloat(item.Qty_Rejected);
//                const Qty_Shortage = parseFloat(item.Qty_Shortage);
//                const Qty_Delivered = parseFloat(item.Qty_Delivered);
         
//                if (!isNaN(Qty_Rejected) && Qty_Rejected !== 0) {
//                  if (Qty_Rejected > Qty_Delivered) {
//                    this.grninward = `Qty Rejected cannot be greater than Qty Delivered for the product ${item.productName}`;
//                    return;
//                  }
//                  this.grninward = 'GRN QC Rejected';
//                } else if (Qty_Rejected === 0 || item.Qty_Rejected === '') {
//                  item.Qty_Rejected = '0.00';
//                } else {
//                  this.grninward = this.grninward.GetErrMsg('PMR_WAR_160');
//                  return;
//                }
         
//                if (!isNaN(Qty_Shortage) && Qty_Shortage !== 0) {
//                  if (Qty_Shortage > Qty_Delivered) {
//                    this.grninward = `Qty Shortage cannot be greater than Qty Delivered for the product ${item.productName}`;
//                    return;
//                  }
//                  this.grninward = 'GRN Shortage';
//                } else if (Qty_Shortage === 0 || item.Qty_Shortage === '') {
//                  item.Qty_Shortage = '0.00';
//                } else {
//                  this.grninward = this.grninward.GetErrMsg('PMR_WAR_160');
//                  return;
            
//                 }
//                 console.log(this.reactiveForm.value)
//                 const api = 'PmrTrnGrnQcchecker/PostPmrTrnGrnQcchecker';
//                 this.service.post(api, this.reactiveForm.value).subscribe(
//                   (result: any) => {
              
//                     if(result.status ==false){
            
//                       this.ToastrService.warning(result.message)
            
//                     }
//                     else{
//                       this.ToastrService.success(result.message)
//                       this.router.navigate(['/pmr/PmrTrnGrncheckerSummary']);
                        
            
//                     }
            
//                   });
              
//               }
//             }
//           }
//         }
         
    
    
public validate(): void {
  debugger
  const grnChecker = this.reactiveForm.value;
  
  if (grnChecker.grn_gid != null && grnChecker.qty_delivered != null && grnChecker.GetGrnQcChecker_lists != null) {
    console.log(this.reactiveForm.value);

    const api = 'PmrTrnGrnQcchecker/PostPmrTrnGrnQcchecker';
    
    // Assuming your API expects an array of objects
    const postData = {
      ...grnChecker,
      GetGrnQcChecker_lists: this.GetGrnQcChecker_lists,
      vendor_gid: this.reactiveForm.value.vendor_gid,
      grn_gid: this.reactiveForm.value.grn_gid,
      purchaseorderdtl_gid: this.reactiveForm.value.purchaseorderdtl_gid,
      grndtl_gid: this.reactiveForm.value.grndtl_gid,
      purchaseorder_gid: this.reactiveForm.value.purchaseorder_gid,
    };

    this.service.post(api, postData).subscribe(
      (result: any) => {
        if (result.status == false) {
          this.ToastrService.warning(result.message);
        } else {
          this.ToastrService.success(result.message);
          this.router.navigate(['/pmr/PmrTrnGrncheckerSummary']);
        }
      }
    );
  }}  
  onChange2(event: any) {
    this.file = event.target.files[0];

  }
  
  

}
