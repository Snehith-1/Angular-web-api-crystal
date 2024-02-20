import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

interface IAddbank{
  accountgroup_gid: string;
  account_gid:any;
  jounrnal_gid: string;
  bank_gid: string;
  session_id: string;
  journal_refno: string;
  transaction_date: string;
  bank_name: string;
  account_no: string;
  account_type: string;
  ifsc_code: string;
  neft_code: string;
  swift_code: string;
  gl_code: string;
  remarks: string;
  accountgroup_name: string;
  account_name: string;
  dr_cr: string;
  transaction_amount: string;
  journal_desc: string;


}
@Component({
  selector: 'app-acc-trn-bankbookaddentry',
  templateUrl: './acc-trn-bankbookaddentry.component.html',
  styleUrls: ['./acc-trn-bankbookaddentry.component.scss']
})
export class AccTrnBankbookaddentryComponent {
  addbankentryform!: FormGroup;
  addbank!: IAddbank;
  addbank_list: any [] = [];
  account_list: any [] = [];
  accountgroup_list: [] = [];
  accountname_list: [] = [];
  accountfetch_list: any [] = [];
  bankentry:any;
  responsedata: any;
  mdlAccGrp:any
  mdlAccname:any;
  mdlAcctrn:any;

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, private router:ActivatedRoute, private route:Router, public service: SocketService) {
    this.addbank = {} as IAddbank;
  }
  ngOnInit(): void {
    this.addbankentryform = new FormGroup({
      accountgroup_gid: new FormControl(''),
      account_gid: new FormControl(''),
      jounrnal_gid: new FormControl(''),
      bank_gid: new FormControl(''),
      session_id: new FormControl(''),
      journal_refno: new FormControl(''),
  transaction_date: new FormControl(''),
  bank_name: new FormControl(''),
  account_no: new FormControl(''),
  account_type: new FormControl(''),
  ifsc_code: new FormControl(''),
  neft_code: new FormControl(''),
  swift_code: new FormControl(''),
  gl_code: new FormControl(''),
  remarks: new FormControl(''),
  accountgroup_name: new FormControl(''),
  account_name: new FormControl(''),
  dr_cr: new FormControl(''),
  transaction_amount: new FormControl(''),
  journal_desc: new FormControl(''),

      });
      debugger;
      const bank_gid = this.router.snapshot.paramMap.get('bank_gid');
      this.bankentry=bank_gid

    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.bankentry,secretKey).toString(enc.Utf8);



    this.GetBankBookAddSummary(deencryptedParam);
    }

    GetBankBookAddSummary(bank_gid: any) {
      debugger;
      
          var url = 'AccTrnBankbooksummary/GetBankBookAddSummary'
      
          let param = {
      
            bank_gid: bank_gid
      
          }
      
          this.service.getparams(url, param).subscribe((result: any) => {
      
            this.addbank_list = result.addbank_list;
            this.addbankentryform.get("bank_name")?.setValue(this.addbank_list[0].bank_name);
            this.addbankentryform.get("account_no")?.setValue(this.addbank_list[0].account_no);
            this.addbankentryform.get("account_type")?.setValue(this.addbank_list[0].account_type);
            this.addbankentryform.get("ifsc_code")?.setValue(this.addbank_list[0].ifsc_code);
            this.addbankentryform.get("neft_code")?.setValue(this.addbank_list[0].neft_code);
            this.addbankentryform.get("swift_code")?.setValue(this.addbank_list[0].swift_code);
            this.addbankentryform.get("gl_code")?.setValue(this.addbank_list[0].gl_code);
            
      
          });

          var url = 'AccTrnBankbooksummary/GetAccTrnGroupDtl'
     this.service.get(url).subscribe((result: any) => {
       this.accountgroup_list = result.GetAccTrnGroupDtl;
    });

        var url = 'AccTrnBankbooksummary/GetAccTrnNameDtl'
         this.service.get(url).subscribe((result: any) => {
          this.accountname_list = result.GetAccTrnNameDtl;
            });
          }

          public AddSummary (){
            debugger
            if (this.addbankentryform.value.accountgroup_name != null && this.addbankentryform.value.account_name != '') {

              for (const control of Object.keys(this.addbankentryform.controls)) {
                this.addbankentryform.controls[control].markAsTouched();
              }
              this.addbankentryform.value;
              var url='AccTrnBankbooksummary/PostProductGroupSummary'
              this.service.post(url,this.addbankentryform.value).subscribe((result:any) => {
        
                if(result.status ==false){
                  this.ToastrService.warning(result.message)
                  this.PostProductGroupSummary();
                }
                else{
                  this.addbankentryform.get("accountgroup_name")?.setValue(null);
                  this.addbankentryform.get("account_name")?.setValue(null);
                  this.addbankentryform.get("dr_cr")?.setValue(null);
                  this.addbankentryform.get("transaction_amount")?.setValue(null);
                  this.addbankentryform.get("journal_desc")?.setValue(null);
                  this.ToastrService.success(result.message)
                  this.PostProductGroupSummary();
                 
                }
              
                      
                    });
                    
            }
            else {
              this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
            }
          }
          PostProductGroupSummary(){
            var url = 'PmrMstProductGroup/PostProductGroupSummary'
            this.service.get(url).subscribe((result: any) => {
          
              this.responsedata = result;
              this.accountfetch_list = this.responsedata.accountfetch_list;
              //console.log(this.entity_list)
              setTimeout(() => {
                $('#accountfetch_list').DataTable();
              }, 1);
          
          
            });
          
         }
         get accountgroupcontrol_name() {
          return this.addbankentryform.get('accountgroup_name')!;
        }
        get accountcontrol_name() {
          return this.addbankentryform.get('account_name')!;
        }
        get transtypecontrol_name() {
          return this.addbankentryform.get('dr_cr')!;
        }
        get transamtcontrol_name() {
          return this.addbankentryform.get('transaction_amount')!;
        }
    
      
  onSubmit(){

  }
}
