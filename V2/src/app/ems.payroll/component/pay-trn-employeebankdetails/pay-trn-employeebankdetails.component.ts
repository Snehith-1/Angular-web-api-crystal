import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { environment } from 'src/environments/environment';
interface IEmployeeBankDetails {
  bank_name: string;
  bankcode_no:string;
  bankacc_no:string;
  pf_no:string;
  esi_no:string;
  pan_no:string;
  uan_no:string;
}

@Component({
  selector: 'app-pay-trn-employeebankdetails',
  templateUrl: './pay-trn-employeebankdetails.component.html',
  styleUrls: ['./pay-trn-employeebankdetails.component.scss']
})
export class PayTrnEmployeebankdetailsComponent {
  file!: File;
  reactiveForm!: FormGroup;
  reactiveFormReset!: FormGroup;
  responsedata: any;
  employeebankdetailslist: any[] = [];
  bankdetailslist: any[] = [];
  bankdetails: any[] = [];
  Document_list: any[] = [];
  Documentdtl_list: any[] = [];
  employeegid:any;
  employeebankdetails!: IEmployeeBankDetails;
  GetEmployeeBankDetailsSummary: any;
  parameterValue: any;
 
 
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {
    this.employeebankdetails = {} as IEmployeeBankDetails;
  }
  ngOnInit(): void {
    // this.GetEmployeeBankDetailsSummary();
     // Form values for Add popup/////
     this.reactiveForm = new FormGroup({
      
      bankcode_no: new FormControl(''),
      employee_gid: new FormControl(''),
      bankacc_no: new FormControl(''),
      pf_no: new FormControl(''),
      esi_no: new FormControl(''),
      pan_no: new FormControl(''),
      uan_no: new FormControl(''),
      bank_name : new FormControl(this.employeebankdetails.bank_name,
         [Validators.required, Validators.minLength(1), Validators.maxLength(250)]),
     

 });

         var api='PayTrnEmployeeBankDetails/GetBankDtl'
         this.service.get(api).subscribe((result:any)=>{
         this.bankdetailslist = result.GetBankDtl;
         //console.log(this.bankdetailslist)
        });

   
   //// Summary Grid//////
    var url = 'PayTrnEmployeeBankDetails/GetEmployeeBankDetailsSummary'
    this.service.get(url).subscribe((result: any) => {
  
      this.responsedata = result;
      this.employeebankdetailslist = this.responsedata.employeebankdetails_list;
      //console.log(this.employeebankdetailslist)
      setTimeout(() => {
        $('#employeebankdetailslist').DataTable();
      }, 1);
  
  
    });
    
    
    
    
    }

  ////////////Add popup validtion////////
get bank_name() {
  return this.reactiveForm.get('bank_name')!;
}
get bankcode_no() {
  return this.reactiveForm.get('bankcode_no');
}
get bankacc_no() {
  return this.reactiveForm.get('bankacc_no')!;
}
get pf_no() {
  return this.reactiveForm.get('pf_no')!;
}
get esi_no() {
  return this.reactiveForm.get('esi_no')!;
}
get pan_no() {
  return this.reactiveForm.get('pan_no')!;
}
get uan_no() {
  return this.reactiveForm.get('uan_no')!;
}

////////////Add popup////////
  public onsubmit(): void {
    
      if (this.reactiveForm.value.bank_name != null && this.reactiveForm.value.bank_name != '') {
  
        for (const control of Object.keys(this.reactiveForm.controls)) {
          this.reactiveForm.controls[control].markAsTouched();
        }
        this.reactiveForm.value;
        var url='PayTrnEmployeeBankDetails/PostEmployeeBankDetails'
              this.service.post(url,this.reactiveForm.value).subscribe((result:any) => {
  
                if(result.status ==false){
                  this.ToastrService.warning(result.message)
                  // this.GetEmployeeBankDetailsSummary();
                }
                else{
                 
                  this.reactiveForm.get("bankcode_no")?.setValue(null);
                  this.reactiveForm.get("bankacc_no")?.setValue(null);
                  this.reactiveForm.get("pf_no")?.setValue(null);
                  this.reactiveForm.get("esi_no")?.setValue(null);
                  this.reactiveForm.get("pan_no")?.setValue(null);
                  this.reactiveForm.get("uan_no")?.setValue(null);
                  this.ToastrService.success(result.message)
                  
                
                  // this.GetEmployeeBankDetailsSummary();
                 
                }
                
              });
                   
      }
      else {
        this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
      }

    }

    public onback(): void {
     
    }

    

    openBankDetails(parameter: string) {
      this.parameterValue = parameter
      this.reactiveForm.get("employee_gid")?.setValue(this.parameterValue.employee_gid);
      console.log(this.reactiveForm)
      const employeegid = this.reactiveForm.value.employee_gid;
      this.getbankdetails(employeegid);

    }
    getbankdetails(employee_gid:any){
      var url = 'PayTrnEmployeeBankDetails/Getbankdetails'
      let param = {
         employee_gid : employee_gid 
          }
      this.service.getparams(url, param).subscribe((result: any) => {
     this.bankdetails = result.GetBank;
    
    
    });



    }



      openBank(parameter: string) {
        this.parameterValue = parameter
      this.reactiveForm.get("employee_gid")?.setValue(this.parameterValue.employee_gid);
      console.log(this.reactiveForm)
      const employeegid = this.reactiveForm.value.employee_gid;
      this.getbankdetails1(employeegid);
      }
      getbankdetails1(employee_gid:any){
        var url = 'PayTrnEmployeeBankDetails/Getbankdetails'
        let param = {
           employee_gid : employee_gid 
            }
        this.service.getparams(url, param).subscribe((result: any) => {
       this.bankdetails = result.GetBank;
       this.reactiveForm.get("bank_name")?.setValue(this.bankdetails[0].bank);
       this.reactiveForm.get("bankcode_no")?.setValue(this.bankdetails[0].bank_code);
       this.reactiveForm.get("bankacc_no")?.setValue(this.bankdetails[0].ac_no);
       this.reactiveForm.get("pf_no")?.setValue(this.bankdetails[0].pf_no);
       this.reactiveForm.get("esi_no")?.setValue(this.bankdetails[0].esi_no);
       this.reactiveForm.get("pan_no")?.setValue(this.bankdetails[0].pan_no);
       this.reactiveForm.get("uan_no")?.setValue(this.bankdetails[0].uan_no);
      
      });
  
  
  
      }

  get employee_gid() {
  return this.reactiveForm.get('employee_gid')!;
         }
  importexcel(){
    debugger;
    let formData = new FormData();
    if (this.file != null && this.file != undefined) {
      window.scrollTo({
        top: 0, // Code is used for scroll top after event done
      });
      formData.append("file", this.file, this.file.name);
      var api = 'PayTrnEmployeeBankDetails/BankDtlImport'
      this.service.postfile(api, formData).subscribe((result: any) => {
        this.responsedata = result;
        // this.router.navigate(['/crm/CrmMstProductsummary']);
        window.location.reload();
        this.ToastrService.success("Excel Uploaded Successfully")
      });
    }
    }
  onChange1(event: any) {
          this.file = event.target.files[0];
    }
  
  downloadfileformat() {
          let link = document.createElement("a");
          link.download = "Bank Details";
          link.href = "assets/media/Excels/Bank/bankdetails.xls";
          link.click();
    }
    ondetail(document_name:any){
      debugger;
      var api1='PayTrnEmployeeBankDetails/GetDocumentDtllist'
      var param={
        document_gid:document_name,
      }
      this.service.getparams(api1,param).subscribe((result:any)=>{
     
        this.responsedata=result;
        this.Documentdtl_list = this.responsedata.documentdtl_list;  
      });
    }
    getdocumentlist(){
      debugger;

      var api1='PayTrnEmployeeBankDetails/GetDocumentlist'
         this.service.get(api1).subscribe((result:any)=>{
        this.responsedata=result;
        this.Document_list = this.responsedata.document_list;
          });
          }
          onclose() {
            this.reactiveFormReset.reset();
        
          }
  }

 



  





