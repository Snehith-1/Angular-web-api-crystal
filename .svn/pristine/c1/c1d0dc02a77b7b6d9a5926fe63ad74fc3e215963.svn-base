import { Component } from '@angular/core';
import {
  FormBuilder, FormControl, FormGroup, Validators, ValidationErrors,
  AbstractControl,
  ValidatorFn
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { saveAs } from 'file-saver';
interface ITelegram {
  telegram_caption: string;
  file_path: string;
}
@Component({
  selector: 'app-crm-smm-telegramsummary',
  templateUrl: './crm-smm-telegramsummary.component.html',
  styleUrls: ['./crm-smm-telegramsummary.component.scss']
})
export class CrmSmmTelegramsummaryComponent {
  isReadOnly = true;
  responsedata: any;
  telegramsummary_list: any;
  video_list: any;
  media_url: any;
  profile_photo: any;
  parameterValue: any;
  parameterValue1: any;
  repost!: FormGroup;
  upload_path: string = '';
  present: any;
  currentPage: number = 1;
  pageSize: number = 5;
  invite_link: any;
  id: any;
  bot_name: any;
  title: any;
  username: any;
  image_id: any;

  image_count: any;
  video_count: any;
  text_count: any;
  total_count: any;
  absURL: any;
  telegram_list: any;
  file!: File;
  telegram!: ITelegram;
  TelegramForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private NgxSpinnerService: NgxSpinnerService, private route: Router) {
    this.telegram = {} as ITelegram;

  }
  ngOnInit(): void {
    this.absURL = window.location.origin

    this.GetTelegramdetailssummary();

    this.TelegramForm = new FormGroup({
      telegram_caption: new FormControl(this.telegram.telegram_caption, [
        Validators.required,
        this.noWhitespaceValidator(),

      ]),

      file_path: new FormControl(''),
    });
    this.repost = new FormGroup({
      id: new FormControl(''),
      file_path: new FormControl(''),
      upload_path: new FormControl(''),
      image_id: new FormControl(''),
      upload_type: new FormControl(''),
      telegram_caption: new FormControl(this.telegram.telegram_caption, [
        Validators.required,
        this.noWhitespaceValidator(),

      ]),



    });
    var url = 'Telegram/GetTelegram'
    this.service.get(url).subscribe((result,) => {

    });
    var url3 = 'Telegram/GetTelegramdetails'
    this.service.get(url3).subscribe((result: any) => {
      this.id = result.id;
      this.bot_name = result.bot_name;
      this.title = result.title;
      this.invite_link = result.invite_link;
    });
  }
  onChange2(event: any) {
    this.file = event.target.files[0];
  }
  GetTelegramdetailssummary() {

    this.NgxSpinnerService.show();
    var url1 = 'Telegram/GetTelegramImage'
    this.service.get(url1).subscribe((result: any) => {
      $('#telegramsummary_list').DataTable().destroy();
      this.responsedata = result;
      this.image_count = result.image_count;
      this.video_count = result.video_count;
      this.text_count = result.text_count;
      this.total_count = result.total_count;
      this.telegramsummary_list = this.responsedata.telegramsummary_list;
      this.NgxSpinnerService.hide();
      setTimeout(() => {
        $('#telegramsummary_list').DataTable();
      }, 1);

    });
  }



  get telegram_caption() {
    return this.TelegramForm.get('telegram_caption')!;
  }

  public onsubmit(): void {

    let formData = new FormData();
    if (this.file != null && this.file != undefined) {


      formData.append("file", this.file, this.file.name);

      formData.append("telegram_caption", this.TelegramForm.value.telegram_caption);
      var url = 'Telegram/TelegramUpload'
      this.service.postfile(url, formData).subscribe((result: any) => {

        if (result.status == false) {

          this.ToastrService.warning(result.message)
          this.GetTelegramdetailssummary();
        }
        else {

          this.TelegramForm.reset();
          this.ToastrService.success(result.message)
          this.route.navigate(['/crm/CrmSmmTelegramsummary']);
          this.GetTelegramdetailssummary();

        }
      });

    }
    else if (this.TelegramForm.value.telegram_caption != null && this.TelegramForm.value.telegram_caption != undefined) {
      var url = 'Telegram/Telegrammessage'

      this.service.post(url, this.TelegramForm.value).pipe().subscribe((result: any) => {

        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.GetTelegramdetailssummary();


        }
        else {
          this.TelegramForm.reset();
          this.ToastrService.success(result.message)
          this.route.navigate(['/crm/CrmSmmTelegramsummary']);
          this.GetTelegramdetailssummary();
        }
      });
    }
    else {
      this.ToastrService.warning("Kindly Upload Image/Video !!")
    }
  }

  public onupdate(image_id: any) {
    let param = {
      image_id: image_id,
      telegram_caption: this.repost.value.telegram_caption
    }
    this.repost.value.param = param;
    if (this.repost.value.telegram_caption != null) {

      for (const control of Object.keys(this.repost.controls)) {
        this.repost.controls[control].markAsTouched();
      }
      this.repost.value;
      var url4 = 'Telegram/Telegramrepostupload'
      this.service.post(url4, this.repost.value).subscribe((result: any) => {

        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.GetTelegramdetailssummary();


        }
        else {
          this.TelegramForm.reset();
          this.ToastrService.success(result.message)
          this.route.navigate(['/crm/CrmSmmTelegramsummary']);
          this.GetTelegramdetailssummary();
        }

      });
    }
  }
  noWhitespaceValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const isWhitespace = (control.value || '').trim().length === 0;
      return isWhitespace ? { whitespace: true } : null;
    };
  }
  modalimage(parameter: string) {
    this.parameterValue = parameter
    console.log(this.parameterValue)
  }

  onclose() {
    this.TelegramForm.reset();
  }
  openModalrepost(parameter: string) {
    this.parameterValue1 = parameter
    this.repost.get("image_id")?.setValue(this.parameterValue1.id);
    this.repost.get("telegram_caption")?.setValue(this.parameterValue1.telegram_caption);
    this.repost.get("upload_path")?.setValue(this.parameterValue1.upload_path);
    this.repost.get("upload_type")?.setValue(this.parameterValue1.upload_type);
  }
  // downloadImage(data: any) {
  //   if (data.upload_path != null && data.upload_path != "") {
  //     if (data.upload_type === 'Image') {
  //       saveAs(data.upload_path, data.id + '.png');
  //     }
  //     else if (data.upload_type === 'Video') {
  //       saveAs(data.upload_path, data.id + '.mp4');
  //     }
  //     else {
  //       saveAs(data.upload_path, data.id + '.png');
  //     }
  //   }
  //   else {
  //     window.scrollTo({
  //       top: 0, // Code is used for scroll top after event done
  //     });
  //     this.ToastrService.warning('No Image Found')

  //   }


  // }

  downloadImage(upload_path: string, upload_type: string): void {

    const image = upload_path.split('.net/');
    const page = image[1];
    const url = page.split('?');
    const imageurl = url[0];
    const parts = imageurl.split('.');
    const extension = parts.pop();

    this.service.downloadfile(imageurl, upload_type+'.'+ extension).subscribe(
      (data: any) => {
        if (data != null) {
          this.service.filedownload1(data);
        } else {
          this.ToastrService.warning('Error in file download');
        }
      },
    );
  }
}
