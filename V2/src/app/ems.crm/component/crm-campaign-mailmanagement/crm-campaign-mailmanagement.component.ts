import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';



interface IMailform {
  mail_from: string;
  sub: string;
  to: string;
  body: string;


}

@Component({
  selector: 'app-crm-campaign-mailmanagement',
  templateUrl: './crm-campaign-mailmanagement.component.html',
  styleUrls: ['./crm-campaign-mailmanagement.component.scss']
})
export class CrmCampaignMailmanagementComponent {
  file!: File;

  responsedata: any;
  reactiveForm!: FormGroup;
  mailid_list: any[] = [];
  mailform!: IMailform;

  branchList: any[] = [];
  designation_list: any[] = [];
  country_list2: any[] = [];
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '12rem',
    minHeight: '5rem',
    width: '1000px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',

  };





  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private route: Router,

  ) {
    this.mailform = {} as IMailform;

  }

  ngOnInit(): void {

    this.reactiveForm = new FormGroup({

      mail_from: new FormControl(this.mailform.mail_from, [
        Validators.required,
      ]),
      sub: new FormControl(this.mailform.sub, [
        Validators.required,
      ]),

      file: new FormControl(''),
      body: new FormControl(''),
      to: new FormControl(this.mailform.to, [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(250), Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')
        // emailValidator(),
      ]),
    })



    var api6 = 'Mailmanagement/Getfrommailiddropdown'
    this.service.get(api6).subscribe((result: any) => {
      this.responsedata = result;
      this.mailid_list = this.responsedata.from_list;
    });
  }
  onChange2(event: any) {
    this.file = event.target.files[0];
    // var api='Employeelist/EmployeeProfileUpload'
    // //console.log(this.file)
    //   this.service.EmployeeProfileUpload(api,this.file).subscribe((result:any) => {
    //     this.responsedata=result;
    //   });
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
  public onadd(): void {
    console.log(this.reactiveForm.value)

    this.mailform = this.reactiveForm.value;
    // this.service.Profileupload(this.reactiveForm.value).subscribe(result => {  
    //   this.responsedata=result;
    // });   
    if (this.mailform.mail_from != null && this.mailform.sub != null && this.mailform.to != null) {
      let formData = new FormData();
      if (this.file != null && this.file != undefined) {



        formData.append("file", this.file, this.file.name);
        formData.append("mail_from", this.mailform.mail_from);
        formData.append("sub", this.mailform.sub);
        formData.append("to", this.mailform.to);
        formData.append("body", this.mailform.body);



        //console.log(this.file)
        var api7 = 'Mailmanagement/mailmanagementupload'
        //console.log(this.file)
        this.service.postfile(api7, formData).subscribe((result: any) => {

          if (result.status == true) {
            this.ToastrService.warning(result.message)
          }
          else {
            window.scrollTo({

              top: 0, // Code is used for scroll top after event done
  
            });
            this.route.navigate(['/crm/CrmCampaignMailmanagementsummary']);
            this.ToastrService.success("Mail sent Successfully")
          }
        });

      }
      else {
        var api7 = 'Mailmanagement/mailmanagementsend'
        //console.log(this.file)
        this.service.post(api7, this.mailform).subscribe((result: any) => {

          if (result.status == true) {
            this.ToastrService.warning(result.message)
          }
          else {
            window.scrollTo({

              top: 0, // Code is used for scroll top after event done
  
            });
            this.route.navigate(['/crm/CrmCampaignMailmanagementsummary']);
            this.ToastrService.success("Mail sent Successfully")
          }
          this.responsedata = result;
        });
      }
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }

    // console.info('Name:', this.employee);
    return;


  }
  redirecttolist() {
    this.route.navigate(['/crm/CrmCampaignMailmanagementsummary']);

  }


}
