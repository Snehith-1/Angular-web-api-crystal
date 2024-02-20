import { Component } from '@angular/core'; import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { ToastrService } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AES } from 'crypto-js';
import { NgxSpinnerService } from 'ngx-spinner';



interface IWhatsappcampaign {
  platform: string;
  localae: string;
  template_name: string;
  category: string;
  version_id: string;
  last_updated: string;
  updated_date: string;
  type: string;
  description: string;
  body: string;
  footer: string;
  project_id: string;
  value1: string;
  id: string;
  p_template_body: string;
  p_name: string;
  p_type: string;

  Total_messages: string;
  Sent_messages: string;
  Received_messages: string;
  //template_name: string;
  template_description: string;

  contact_count1: string;
  p_nameedit: string;

  templateedit_description: string;

}
@Component({
  selector: 'app-crm-smm-whatsappcampaign',
  templateUrl: './crm-smm-whatsappcampaign.component.html',
  styleUrls: ['./crm-smm-whatsappcampaign.component.scss']
})
export class CrmSmmWhatsappcampaignComponent {
  isReadOnly = true;
  whatsappCampaign: any;
  responsedata: any;
  contactcount_list: any;
  file!: File;
  image_path: any;
  campaignservice_list: any;
  template_list: any[] = [];
  reactiveForm!: FormGroup;
  reactiveFormadd!: FormGroup;
  reactiveMessageForm!: FormGroup;
  channel_name: any;
  mobile_number: any;
  Whatsappcampaign!: IWhatsappcampaign;
  parameterValue1: any;
  contacts_list: any[] = [];
  parameterValue: any;
  parameterValue3: any;
  parameterValue4: any;
  parameterValue5: any;
  contact_count1: any[] = [];
  message_count: any[] = [];
  templateview_list: any;
  delivered_messages: any;
  footers: any;
  project_count: any;
  lsmtd: any;
  lsytd: any;
  isButtonTrue: boolean = true;
  isButtonFalse: boolean = false;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService,
    private route: Router, public service: SocketService, private NgxSpinnerService: NgxSpinnerService) {
    this.Whatsappcampaign = {} as IWhatsappcampaign;
  }
  ngOnInit(): void {

    this.Getcampaign();
    this.GetWhatsappMessageCount();
    this.GetContactCount();
    this.GetTemplate();
    this.GetWhatsappSummarycampaign();
    this.getservicewindow();



    this.reactiveForm = new FormGroup({
      platform: new FormControl(this.Whatsappcampaign.platform, [
        Validators.required,
      ]),
      localae: new FormControl(this.Whatsappcampaign.localae, [
      ]),
      template_name: new FormControl(this.Whatsappcampaign.template_name, [
      ]),
      version_id: new FormControl(this.Whatsappcampaign.version_id, [
      ]),
      last_updated: new FormControl(this.Whatsappcampaign.last_updated, [
      ]),
      updated_date: new FormControl(this.Whatsappcampaign.updated_date, [
      ]),
      contact_count1: new FormControl(this.Whatsappcampaign.contact_count1, [
      ]),
      Total_messages: new FormControl(this.Whatsappcampaign.Total_messages, [
      ]),
      Sent_messages: new FormControl(this.Whatsappcampaign.Sent_messages, [
      ]),
      Received_messages: new FormControl(this.Whatsappcampaign.Received_messages, [
      ]),
      type: new FormControl(this.Whatsappcampaign.type, [
      ]),
      project_id: new FormControl(this.Whatsappcampaign.project_id, [
      ]),

      p_type: new FormControl(this.Whatsappcampaign.p_type, [
      ]),
      p_name: new FormControl(this.Whatsappcampaign.p_name, [
      ]),

      p_template_body: new FormControl(this.Whatsappcampaign.p_template_body, [
      ]),

      description: new FormControl(this.Whatsappcampaign.description, [
      ]),
      value1: new FormControl(this.Whatsappcampaign.value1, [
      ]),

      body: new FormControl(this.Whatsappcampaign.body, [
        Validators.required,
      ]),
      footer: new FormControl(this.Whatsappcampaign.footer, [
        Validators.required,
      ]),
      file: new FormControl(''),
      fileExtension: new FormControl(''),
      fileName: new FormControl(''),
      imagePath: new FormControl(''),
      id: new FormControl(this.Whatsappcampaign.id, [
      ]),
    });

  }

  get last_updated() {
    return this.reactiveForm.get('last_updated')!;
  }
  get Total_messages() {
    return this.reactiveForm.get('Total_messages')!;
  } get Sent_messages() {
    return this.reactiveForm.get('Sent_messages')!;
  }
  get Received_messages() {
    return this.reactiveForm.get('Received_messages')!;
  }
  get updated_date() {
    return this.reactiveForm.get('updated_date')!;
  }
  get version_id() {
    return this.reactiveForm.get('version_id')!;
  }
  get category() {
    return this.reactiveForm.get('category')!;
  }
  get value() {
    return this.reactiveForm.get('value1')!;
  }
  get category_change() {
    return this.reactiveForm.get('category_change')!;
  }
  get message_type() {
    return this.reactiveForm.get('message_type')!;
  }
  get project_type() {
    return this.reactiveForm.get('type')!;
  }
  get project_name() {
    return this.reactiveForm.get('name')!;
  }
  get project_description() {
    return this.reactiveForm.get('description')!;
  }
  get body() {
    return this.reactiveForm.get('body')!;
  }
  get footer() {
    return this.reactiveForm.get('footer')!;
  }
  get image() {
    return this.reactiveForm.get('image')!;
  }

  get template_description() {
    return this.reactiveForm.get('template_description')!;
  }
  get project_id() {
    return this.reactiveForm.get('project_id')!;
  }
  // get template_name() {
  //   return this.reactiveForm.get('template_name')!;
  // }

  onChange1(event: any) {
    this.file = event.target.files[0];
  }
 
  getservicewindow() {
   
    var url = 'Whatsapp/Getservicewindowcontact'
    this.service.get(url).subscribe((result: any) => {
    });
  }

  //   this.whatsappmessage = this.reactiveForm.value;
  //   if (this.whatsappmessage.type != null &&this.whatsappmessage.name != null 
  //      && this.whatsappmessage.description != null) {
  //     console.log(this.reactiveForm.value)
  //     //  this.leadbank.region_name != null,this.leadbank.country_name != null  &&
  //     var api = 'Whatsapp/CreateProject';
  //     this.service.post(api, this.whatsappmessage).subscribe((result: any) => {
  //       console.log(result);
  //       if (result.status == false) {
  //         window.location.reload()
  //         this.ToastrService.warning(result.message)
  //       }
  //       else {
  //         // this.GetleadbranchaddSummary(this.leadbank_gid);
  //         // this.route.navigate(['/crm/CrmTrnLeadBankbranch',this.leadbank_gid]);
  //         window.location.reload()
  //         this.ToastrService.success(result.message)
  //       }
  //     });
  //   }
  // }


  // onChange1(event: any) {
  //   this.file = event.target.files[0];
  // }
  // onclose() {
  //   this.reactiveMessageForm.reset();
  // }

  //  onprojectcreate(){
  //   this.whatsappmessage = this.reactiveForm.value;
  //   if (this.whatsappmessage.type != null &&this.whatsappmessage.name != null 
  //      && this.whatsappmessage.description != null) {
  //     console.log(this.reactiveForm.value)
  //     //  this.leadbank.region_name != null,this.leadbank.country_name != null  &&
  //     var api = 'Whatsapp/CreateProject';
  //     this.service.post(api, this.whatsappmessage).subscribe((result: any) => {
  //       console.log(result);
  //       if (result.status == false) {
  //         window.location.reload()
  //         this.ToastrService.warning(result.message)
  //       }
  //       else {
  //         // this.GetleadbranchaddSummary(this.leadbank_gid);
  //         // this.route.navigate(['/crm/CrmTrnLeadBankbranch',this.leadbank_gid]);
  //         window.location.reload()
  //         this.ToastrService.success(result.message)
  //       }
  //     });
  //   }
  // }


  //// Summary Grid//////
  Getcampaign() {
    this.NgxSpinnerService.show();
    var url = 'Whatsapp/Getcampaign'
    this.service.get(url).subscribe((result: any) => {
      $('#whatsappCampaign').DataTable().destroy();
      this.responsedata = result;
      this.whatsappCampaign = this.responsedata.whatsappCampaign;
      this.project_count = this.responsedata.whatsappCampaign[0].project_count
      this.delivered_messages = this.responsedata.whatsappCampaign[0].delivered_messages
      this.lsmtd = this.responsedata.whatsappCampaign[0].lsmtd
      this.lsytd = this.responsedata.whatsappCampaign[0].lsytd
      this.NgxSpinnerService.hide();
      //console.log(this.source_list)
      setTimeout(() => {
        $('#whatsappCampaign').DataTable();
      }, 1);
    });
  }
  onsend(params: any): void {
    const secretKey = 'storyboarderp';
    const project_id = AES.encrypt(params.project_id, secretKey).toString();
    const version_id = AES.encrypt(params.version_id, secretKey).toString();
    this.route.navigate(['/crm/CrmMstWabulkmessage', project_id, version_id])
  }

  GetTemplate() {
    this.NgxSpinnerService.show();
    var url = 'Whatsapp/GetTemplate'
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
      this.NgxSpinnerService.hide();

    });
  }
  public whatsapplog(params: any): void {
    const secretKey = 'storyboarderp';
    const project_id = AES.encrypt(params.project_id, secretKey).toString();
    this.route.navigate(['/crm/CrmSmmWhatsapplog', project_id])
  }
  GetContactCount() {
    var url = 'Whatsapp/GetContactCount'
    this.service.get(url).subscribe((result: any) => {
      $('#contactcount_list').DataTable().destroy();
      this.responsedata = result;
      this.contact_count1 = this.responsedata.contactcount_list;
      //console.log(this.source_list)

    });
  }
  GetWhatsappMessageCount() {
    var url = 'Whatsapp/GetWhatsappMessageCount'
    this.service.get(url).subscribe((result: any) => {
      $('#whatsappmessagescount').DataTable().destroy();
      this.responsedata = result;
      this.message_count = this.responsedata.whatsappmessagescount;
      //console.log(this.source_list)
      console.log("Received counts are" + this.message_count);


    });
  }


  openModaldelete(parameter: string) {

    this.parameterValue = parameter



  }

  ondelete() {

    console.log(this.parameterValue);

    var url = 'Whatsapp/DeleteCampaign'

    let param = {

      project_id: this.parameterValue

    }

    this.service.getparams(url, param).subscribe((result: any) => {

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

      }

      this.Getcampaign();

    });

  }

  onclose() {

    this.reactiveForm.reset();





  }
  openModalview(project_id: any) {
    let params = {
      project_id: project_id
    }
    var url = 'Whatsapp/GetTemplatepreview'
    this.service.getparams(url, params).subscribe((result: any) => {
      this.responsedata = result;
      this.templateview_list = this.responsedata.Gettemplateview_list;
      this.footers = this.responsedata.Gettemplateview_list[0].footer;

    });
  }

  // openModalview(id: string, p_name: string, p_type: string  ) {
  //   this.parameterValue1 = id,
  //     this.parameterValue3 = p_name,
  //     this.parameterValue4 = p_type

  //   this.reactiveForm.get("project_id")?.setValue(this.parameterValue1);
  //   this.reactiveForm.get("p_name")?.setValue(this.parameterValue3);
  //   this.reactiveForm.get("p_type")?.setValue(this.parameterValue4);
  // }
  GetWhatsappSummarycampaign() {
    var api = 'CampaignService/GetWhatsappSummary'
    this.service.get(api).subscribe((result: any) => {

      this.responsedata = result;
      this.campaignservice_list = this.responsedata.campaignservice_list;
      this.channel_name = this.responsedata.campaignservice_list[0].channel_name;
      this.mobile_number = this.responsedata.campaignservice_list[0].mobile_number;
    });
  }
  public toggleswitch(param: any): void {
    var api = 'Whatsapp/UpdateWhatsappCampaignStatus'
    this.service.post(api, param).subscribe((result: any) => {
      if (result.status == false) {
        window.scrollTo({
          top: 0, // Code is used for scroll top after event done
        });
        this.ToastrService.warning(result.message)
        this.Getcampaign();
      }
      else {
        window.scrollTo({

          top: 0, // Code is used for scroll top after event done

        });
        this.ToastrService.success(result.message)
        this.Getcampaign();
      }
    });
  }
}