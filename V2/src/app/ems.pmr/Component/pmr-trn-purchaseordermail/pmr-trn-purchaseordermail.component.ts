


import { Component } from '@angular/core';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AES, enc } from 'crypto-js';
import { ActivatedRoute, Router } from '@angular/router';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { AngularEditorComponent } from '@kolkov/angular-editor';


interface IMailform {
  mail_from: string;
  sub: string;
  to: string;
  body: string;
  bcc: any;
  cc: any;
  reply_to: any;
  template_content:string;


}

@Component({
  selector: 'app-pmr-trn-purchaseordermail',
  templateUrl: './pmr-trn-purchaseordermail.component.html',
  styleUrls: ['./pmr-trn-purchaseordermail.component.scss']
})
export class PmrTrnPurchaseordermailComponent {
  
  file!: File;
  file1!: FileList;
  file_name: any;
  responsedata: any;
  formDataObject: FormData = new FormData();
  reactiveForm!: FormGroup;
  mailid_list: any[] = [];
  mailform!: IMailform;
  purchaseorder_gid:any;
  templatelist :any[] = [];
  branchList: any[] = [];
  designation_list: any[] = [];
  country_list2: any[] = [];
  mail_form: any;
  template_content: any[] = [];
  GetTemplatelist: any;
  body: any;
  result : any
  Templateterm: any;
  files: File[] = [];
  allattchement: any[] = [];
  AutoIDkey: any;
  GetFromMailDropdownlist :any[] = [];
  FromMailterm: any;
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '12rem',
    minHeight: '0rem',
    width: '1129px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',

  };
  isDropdownOpen = false;


  constructor(private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder, 
    private service: SocketService,
    private ToastrService: ToastrService  ) {
    this.mailform = {} as IMailform;

  }

  ngOnInit(): void {
debugger;
debugger;
var api='PmrTrnPurchaseorder/GetTemplatelist'
this.service.get(api).subscribe((result:any)=>{
this.templatelist = result.templatelist;
//console.log(this.componentgrouplist)
});
var api='PmrTrnPurchaseorder/GetFromMailDropdown'
this.service.get(api).subscribe((result:any)=>{
this.GetFromMailDropdownlist = result.GetFromMailDropdownlist;
//console.log(this.componentgrouplist)
});

    this.reactiveForm = new FormGroup({
      template_gid:new FormControl(''),
      template_name:new FormControl(''),
      template_content: new FormControl(''),
      mail_from: new FormControl(this.mailform.mail_from, [
        Validators.required,
      ]),
      sub: new FormControl(this.mailform.sub, [
        Validators.required,
      ]),

      file: new FormControl(''),
      body: new FormControl(''),
      bcc: new FormControl(''),
      cc: new FormControl(''),
      reply_to: new FormControl(''),
      schedule_time: new FormControl(''),
      to: new FormControl(this.mailform.to, [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(250), Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')
        // emailValidator(),
      ]),
    });
const purchaseorder_gid = this.route.snapshot.paramMap.get('purchaseorder_gid');
   
    this.purchaseorder_gid = purchaseorder_gid

    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.purchaseorder_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.onChange2(deencryptedParam)
   
    
  }


  
  onChange2($event: any): void {
    this.file1 = $event.target.files;
  
    if (this.file1 != null && this.file1.length !== 0) {
      for (let i = 0; i < this.file1.length; i++) {
        this.AutoIDkey = this.generateKey();
        this.formDataObject.append(this.AutoIDkey, this.file1[i]);
         this.file_name = this.file1[i].name;
        this.allattchement.push({
          AutoID_Key: this.AutoIDkey,
          file_name: this.file1[i].name
        });
        console.log(this.file1[i]);
      }
    }
  
    //console.log(this.files[i]);
  }

  generateKey(): string {

    return `AutoIDKey${new Date().getTime()}`;
  }
  GetOnChangeTerms() {
    debugger

    let template_gid = this.reactiveForm.value.template_name;
    let param = {
      template_gid: template_gid
    }
    var url = 'PmrTrnPurchaseorder/GetTemplate';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.reactiveForm.get("template_content")?.setValue(result.templatelist[0].template_content);
      this.reactiveForm.value.template_gid = result.templatelist[0].template_gid
      //this.cusraiseform.value.productuom_gid = result.GetProductsName[0].productuom_gid
    });
  }
  
  // onChange2(event: any) {
  //   debugger;
  //   this.file = event.target.files[0];
    
  // }
 
  sendMessage() {
    // Add your send message logic here
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
  get mail_from() {
    return this.reactiveForm.get('mail_from')!;
  }
  get to() {
    return this.reactiveForm.get('to')!;
  }
  get sub() {
    return this.reactiveForm.get('sub')!;
  }
  get reply_to() {
    return this.reactiveForm.get('reply_to')!;
  }
  get cc() {
    return this.reactiveForm.get('cc')!;
  }
  get bcc() {
    return this.reactiveForm.get('bcc')!;
  }

  public onadd(): void {
    debugger;
    console.log(this.reactiveForm.value)

    this.mailform = this.reactiveForm.value;
    if (this.mailform.mail_from != null && this.mailform.sub != null && this.mailform.to != null) {
      
      const allattchement = "" + JSON.stringify(this.allattchement) + "";
      if (this.file1 != null && this.file1 != undefined) {
        this.formDataObject.append("filename", allattchement);
        this.formDataObject.append("mail_from", this.mailform.mail_from);
        this.formDataObject.append("sub", this.mailform.sub);
        this.formDataObject.append("to", this.mailform.to);
        this.formDataObject.append("body", this.mailform.body);
        this.formDataObject.append("body", this.mailform.template_content);
        this.formDataObject.append("bcc", this.mailform.bcc);
        this.formDataObject.append("cc", this.mailform.cc);
        

        console.log(this.formDataObject)

        var api7 = 'PmrTrnPurchaseorder/PostMail'
        this.service.post(api7, this.formDataObject).subscribe((result: any) => {
          this.responsedata = result;
          if (result.status == false) {
            this.ToastrService.warning(result.message)
          }
          else{
            window.scrollTo({

              top: 0, // Code is used for scroll top after event done

            });
            this.router.navigate(['/pmr/PmrTrnPurchaseorderSummary']);
            this.ToastrService.success(result.message)
          }
        });
      }
      else{
      var api7 = 'PmrTrnPurchaseorder/PostMail'
      //console.log(this.file)
      this.service.post(api7, this.mailform).subscribe((result: any) => {

        if (result.status == false) {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.ToastrService.warning(result.message)
        }
      
        else {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.ToastrService.success(result.message)
          this.router.navigate(['/pmr/PmrTrnPurchaseorderSummary']);
          
        }
        this.responsedata = result;
      });
    }
    }

    else {
      window.scrollTo({

        top: 0, // Code is used for scroll top after event done

      });
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }

    // console.info('Name:', this.employee);
    return;


  }
  
 
  

}

function setValue(arg0: any) {
  throw new Error('Function not implemented.');
}

