import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AES, format } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import {
  CountryISO,
  SearchCountryField,

} from "ngx-intl-tel-input";
import { saveAs } from 'file-saver';
import { InlinechatService } from 'src/app/ems.utilities/services/inlinechat.service';


@Component({
  selector: 'app-crm-smm-websitechats',
  templateUrl: './crm-smm-websitechats.component.html',
  styleUrls: ['./crm-smm-websitechats.component.scss']
})
export class CrmSmmWebsitechatsComponent {

  responsedata: any;
  parameterValue: any;
  whatsappmessage_list: any[] = [];
  file!: File;
  reactiveForm!: FormGroup;

  // chatused
  reactiveMessageForm!: FormGroup;
  searchText = '';
  listof_chat: any[] = [];
  viewchat_list: any[] = [];
  user_details: any[] = [];
  id: any;
  user_name: any;
  user_id: any;
  user_mail: any;
  created_date: any;
  location: any;
  user_agent: any;
  ip_address: any;
  first_letter: any;
  thread_id: any;
  chat_id: any;
  chatdetails: any[] = [];

  // chatused


  chatWindow: string = "Default";
  openDiv: boolean = false;
  filetype: string = "";
  OpenOption: boolean = false;
  parameterValue1: any;
  params: any;
  params1: any;

  image_url!: string;
  lastName: any;
  windowInterval: any;
  windowInterval1: any;
  contact_id: any;
  access_token !: string; // Use the ! operator
  static access_token: any;

  matchesSearch(item: any): boolean {
    const searchString = this.searchText.toLowerCase();
    return item.user_name.toLowerCase().includes(searchString) || item.user_mail.toLowerCase().includes(searchString);
  }
  constructor(private formBuilder: FormBuilder, private route: Router, private router: Router,
    private ToastrService: ToastrService, public service: SocketService,
    private NgxSpinnerService: NgxSpinnerService, private inlinechat: InlinechatService) {
  }

  ngOnInit(): void {
    this.GetchatSummarydetails();

    var url3 = 'website/Getlistofchat'
    this.service.get(url3).subscribe((result,) => {
    });
    var url6 = 'website/Getlistofthreads'
    this.service.get(url6).subscribe((result,) => {
    });

    this.reactiveMessageForm = new FormGroup({
      chat_id: new FormControl(''),
      sendtext: new FormControl(''),
    });
    this.reactiveForm = new FormGroup({
      chat_id: new FormControl(''),
    });

  }
  zoomed: boolean = false;

  toggleZoom() {
    this.zoomed = !this.zoomed;
  }
  //User summary start//
  GetchatSummarydetails() {
    this.NgxSpinnerService.show();
    var url = 'website/chatSummary'
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
      this.listof_chat = this.responsedata.listof_chat;
      this.chat_id = this.responsedata.listof_chat[0].chat_id,
        this.user_id = this.responsedata.listof_chat[0].user_id,


        this.NgxSpinnerService.hide();
      setTimeout(() => {
        $('#listof_chat').DataTable();
      }, 100);
    });

  }

  //Individual Chat Insert event //
  Getindividualchatsummary(chat_id: any) {
    this.NgxSpinnerService.show();
    var url5 = 'website/Getindividualchat'
    let param = {
      chat_id: chat_id

    }
    this.service.getparams(url5, param).subscribe((result: any) => {
      if (result != null) {
        this.viewchat_list = result.GetViewchatsummary;
        this.user_id = result.user_id
        this.user_name = result.user_name
        this.user_mail = result.user_mail
        this.first_letter = result.first_letter
        this.NgxSpinnerService.hide();
        setTimeout(() => {
          $('#viewchat_list').DataTable();
        }, 1000);
      }
      else {
        clearInterval(this.windowInterval1)
      }
    });
  }
  //Individual Chat Insert end //


  // contact to message//
  showResponsiveOutput( chat_id: string) {
    var url6 = 'website/Getlistofthreads'
    this.service.get(url6).subscribe((result,) => {
    });
    this.chat_id = chat_id;
    this.chatWindow = "Chat"
    this.Getindividualchatsummary(chat_id);


  }
  getfulluserdetails(user_id: string) {
    this.chatWindow = "About"
    var url = 'website/Getuserdeatils'
    var params = {
      user_id: user_id
    }
    this.service.getparams(url, params).subscribe((result: any) => {
      $('#user_details').DataTable().destroy();
      if (result != null) {
        this.responsedata = result;
        this.user_details = this.responsedata.user_details;
        this.user_id = this.user_details[0].user_id;
        this.first_letter = this.user_details[0].first_letter;
        this.user_name = this.user_details[0].user_name;
        this.user_mail = this.user_details[0].user_mail;
        this.created_date = this.user_details[0].created_date;
        this.location = this.user_details[0].location;
        this.ip_address = this.user_details[0].ip_address;
        this.user_agent = this.user_details[0].user_agent;

      }
      else {
        clearInterval(this.windowInterval1)
      }
    });
  }
  getchatreferesh( chat_id: string) {
    var url6 = 'website/Getlistofthreads'
    this.service.get(url6).subscribe((result,) => {
    });
    this.Getindividualchatsummary(chat_id);
  }
  backtochat() {
    this.chatWindow = "Chat"
  }
  ngOnDestroy(): void {
    clearInterval(this.windowInterval)
    clearInterval(this.windowInterval1)
  }

  i: number = 0;
  onSubmit() {
    if (this.i === 0) {
      this.onMessagesent(this.chat_id);
    }
  }
  // Message send //
  public onMessagesent(chat_id: string): void {
    var url6 = 'website/Getlistofthreads'
    this.service.get(url6).subscribe((result,) => {
    });
    this.reactiveMessageForm.value.chat_id = chat_id;

    if (this.reactiveMessageForm.value.sendtext != null) {

      var url = 'website/Messagesend'
      this.service.post(url, this.reactiveMessageForm.value).subscribe((result: any) => {
        if (result.status == false) {
          window.scrollTo({
            top: 0, // Code is used for scroll top after event done
          });
          this.ToastrService.warning(result.message)
          this.reactiveMessageForm.reset();
        }
        else {
          window.scrollTo({
            top: 0, // Code is used for scroll top after event done
          });
          this.ToastrService.success(result.message)
          this.reactiveMessageForm.reset();
        }
        this.Getindividualchatsummary(this.chat_id);
      });
    }

    else {
      window.scrollTo({
        top: 0, // Code is used for scroll top after event done
      });
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }

  onclose() {
    this.reactiveForm.reset();
  }
  public onupload(chat_id: string): void {
    var url6 = 'website/Getlistofthreads'
    this.service.get(url6).subscribe((result,) => {
    });
    var url9 = 'website/Getaccesstoken'
    this.service.get(url9).subscribe((result: any) => {
      this.id = result.id;
      this.access_token = result.access_token;  
      let formData = new FormData();
    if (this.file != null && this.file != undefined) {
      formData.append("file", this.file, this.file.name);
      this.NgxSpinnerService.show();
      var url = 'https://api.livechatinc.com/v3.4/agent/action/upload_file'
      this.inlinechat.postfile(url, formData,this.access_token).subscribe((result: any) => {
        if (result != null && result != undefined) {
          this.params = result;
          var url8 = 'website/uploadsend'
          var params1 = {
            image_url: this.params.url,
            chat_id: chat_id,
          }
          this.service.post(url8, params1).subscribe((result: any) => {

            if (result.status == false) {
              window.scrollTo({
                top: 0,
              });
              this.ToastrService.warning(result.message)
              this.reactiveMessageForm.reset();
            }
            else {
              window.scrollTo({
                top: 0,
              });
              this.ToastrService.success(result.message)
              this.reactiveMessageForm.reset();
            }
            this.Getindividualchatsummary(this.chat_id);
          });

        }
        else {
          this.ToastrService.success("Error Occured While Upload!!");
        }
      });

    }
    else {
      window.scrollTo({
        top: 0, // Code is used for scroll top after event done
      });
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
      });
  }
  onChange1(event: any) {
    this.file = event.target.files[0];
  }

}




