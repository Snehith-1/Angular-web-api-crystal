import { Component, ElementRef, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, MinLengthValidator, Validators } from '@angular/forms';
import { Router } from '@angular/router'; 
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AES , enc} from 'crypto-js';
import { ActivatedRoute } from '@angular/router';
interface IVendor {
  branch_code:string;
  openning_balance: string;
  externalgl_code: string;
  gl_code: string;
  transaction_date: string;
  remarks: string;
  branch_name: string;
  
}

@Component({
  selector: 'app-acc-trn-cashbookedit',
  templateUrl: './acc-trn-cashbookedit.component.html',
  styleUrls: ['./acc-trn-cashbookedit.component.scss']
})
export class AccTrnCashbookeditComponent {


  vendor!: IVendor;
  reactiveForm: FormGroup | any;
  country_list: any[] = [];
  currency_list: any[] = [];
  tax_list: any[] = [];
  responsedata: any;
  selectedVendorcode:any;
  branch_code:any;
  CashBookedit_list:any;
  constructor(private renderer: Renderer2, private el: ElementRef,public service :SocketService,private ToastrService: ToastrService,private route:Router,private router: ActivatedRoute ) {
    this.vendor = {} as IVendor;
  }

  
  ngOnInit(): void {
  
    const branch_code = this.router.snapshot.paramMap.get('branch_gid');
    // console.log(termsconditions_gid)
    this.branch_code = branch_code;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.branch_code, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.GetCashBookedit(deencryptedParam)
   
    this.reactiveForm = new FormGroup({
    
      openning_balance: new FormControl(this.vendor.openning_balance, [
        Validators.required,
      
      ]),
      externalgl_code: new FormControl(this.vendor.externalgl_code, [
        Validators.required,
      ]),
      transaction_date: new FormControl(this.vendor.transaction_date, [
        Validators.required,
      ]),
      branch_name: new FormControl(this.vendor.branch_name, [
        Validators.required,

      ]),
      gl_code: new FormControl(this.vendor.gl_code, [
        Validators.required,
        
      ]),
      remarks: new FormControl(this.vendor.remarks, [
        Validators.required,
        
      ]),
     
      branch_code: new FormControl(this.vendor.branch_code),
      
      
            
             
 });
  }

  GetCashBookedit(branch_gid: any) {
  debugger
  var url1='AccTrnCashBookSummary/GetCashBookedit'
  let param = {
    branch_gid : branch_gid 
  }
  this.service.getparams(url1, param).subscribe((result: any) => {
    this.CashBookedit_list = result.CashBookedit_list;
    console.log(this.CashBookedit_list)
    this.reactiveForm.get("branch_code")?.setValue(this.CashBookedit_list[0].branch_code);
    this.reactiveForm.get("openning_balance")?.setValue(this.CashBookedit_list[0].openning_balance);
    this.reactiveForm.get("externalgl_code")?.setValue(this.CashBookedit_list[0].externalgl_code);
    this.reactiveForm.get("gl_code")?.setValue(this.CashBookedit_list[0].gl_code);
    this.reactiveForm.get("transaction_date")?.setValue(this.CashBookedit_list[0].transaction_date);
    this.reactiveForm.get("branch_name")?.setValue(this.CashBookedit_list[0].branch_name);
    this.reactiveForm.get("remarks")?.setValue(this.CashBookedit_list[0].remarks);
 
  });
}
  onChange2(event:any) {
    this.reactiveForm =event.target.files[0];

    }
    get openning_balance() {
      return this.reactiveForm.get('openning_balance')!;
    }
    get externalgl_code() {
      return this.reactiveForm.get('externalgl_code')!;
    }
    get gl_code() {
      return this.reactiveForm.get('gl_code')!;
    }
    get transaction_date() {
      return this.reactiveForm.get('transaction_date')!;
    }
    get branch_name() {
      return this.reactiveForm.get('branch_name')!;
    }
    get remarks() {
      return this.reactiveForm.get('remarks')!;
    }
    
    

  
 
    
    public validate(): void {
      debugger
      console.log(this.reactiveForm.value)
        this.vendor = this.reactiveForm.value;
        debugger
        if(  this.vendor.openning_balance !=null  && this.vendor.transaction_date !=null &&
           this.vendor.externalgl_code !=null  && this.vendor.branch_name !=null
            && this.vendor.remarks !=null ){
          let formData = new FormData();
          if(this.reactiveForm !=null &&  this.reactiveForm != undefined){
          
        formData.append("branch_code", this.vendor.branch_code);
         formData.append("openning_balance", this.vendor.openning_balance);
         formData.append("externalgl_code", this.vendor.externalgl_code);
         formData.append("gl_code", this.vendor.gl_code);
         formData.append("transaction_date", this.vendor.transaction_date);
         formData.append("branch_name", this.vendor.branch_name);
         formData.append("remarks", this.vendor.remarks);
          
         var api='AccTrnCashBookSummary/PostCashBookUpdate'
            this.service.postfile(api,formData).subscribe((result:any) => {
              this.responsedata=result;
              if(result.status ==false){
                this.ToastrService.warning(result.message)
              }
              else{
                this.route.navigate(['/finance/AccTrnCashbooksummary']);
                this.ToastrService.success(result.message)
              }
            });
        
        }
        // else{
        //   var api7='AccTrnCashBookSummary/PostCashBookUpdate'
      
        //     this.service.post(api7,this.vendor).subscribe((result:any) => {

        //       if(result.status ==false){
        //         this.ToastrService.warning(result.message)
        //       }
        //       else{
        //         this.route.navigate(['/finance/AccTrnCashbooksummary']);
        //         this.ToastrService.success(result.message)
        //       }
        //       this.responsedata=result;
        //     });
        // }
        }
        else{
          this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
        }
        return;
      

    }
}

