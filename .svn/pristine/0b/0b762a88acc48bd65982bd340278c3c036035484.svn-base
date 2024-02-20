import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import {
  FormBuilder, FormControl, FormGroup, Validators, ValidationErrors,
  AbstractControl,
  ValidatorFn
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { AES } from 'crypto-js';
import { saveAs } from 'file-saver';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
interface IFacebook {
  image_caption: string;
  file_path: string;
  video_caption: string;
  image_schedulercaption: string;
  schedule_at: string;

}
@Component({
  selector: 'app-crm-smm-facebookpage',
  templateUrl: './crm-smm-facebookpage.component.html',
  styleUrls: ['./crm-smm-facebookpage.component.scss']
})
export class CrmSmmFacebookpageComponent {
  @ViewChild('Inbox') tableRef!: ElementRef;

  searchTerm: string = '';
  searchResults: string[] = [];
  searchText: any;
  search: string = '';
  file!: File;
  facebook_list: any;
  username: any;
  picture: any;
  friends: any;
  posts: any;
  filteredData: any;
  videos: any;
  posts1: string[] = [];
  posts2: string[] = [];
  posts3: string[] = [];
  posts4: string[] = [];
  email: any;
  birthday: any;
  gender: any;
  age_range: any;
  hometown: any;
  present: any;
  currentPage: number = 1;
  pageSize: number = 5;
  responsedata: any;
  facebookuser_list: any;
  facebookpage_list: any;
  schedule_list: any;
  followers_count: any;
  link: any;
  id: any;
  cover: any;
  phone: any;
  category: any;
  facebook!: IFacebook;
  FacebookImageForm!: FormGroup;
  FacebookImagescheduleForm!: FormGroup;

  windowInterval: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private router: Router, private NgxSpinnerService: NgxSpinnerService) {
    this.facebook = {} as IFacebook;
  }
  ngOnInit(): void {
    var url = 'Facebook/Getschedulelogdetails'
    this.service.get(url).subscribe((result: any) => {
      // window.location.reload()
      this.responsedata = result;
      this.schedule_list = this.responsedata.schedulelist;

    });

    const today = new Date(); // Get current date and time
    const minDate = today; // Set minDate to today's date
    const maxDate = new Date(today.getTime() + 28 * 24 * 60 * 60 * 1000); // 30 days from now
    
    const options = {
      enableTime: true,
      dateFormat: 'Y-m-d H:i:S K',
      minDate: minDate,
      maxDate: maxDate,
      defaultDate: today, // Set defaultDate to today's date
      minuteIncrement: 1 // Set minute increment to 1 for 1-minute intervals
    };
    
    flatpickr('.date-picker', options); // Initialize Flatpickr with options
    
    


    this.GetPageuserdetails();
    this.GetPagedetails();
    this.FacebookImageForm = new FormGroup({
      image_caption: new FormControl(this.facebook.image_caption, [
        Validators.required,
        this.noWhitespaceValidator(),
      ]),
      file_path: new FormControl(this.facebook.file_path, [
        Validators.required,
        this.noWhitespaceValidator(),
      ]),     });
    this.FacebookImagescheduleForm = new FormGroup({
      image_schedulercaption: new FormControl(this.facebook.image_schedulercaption, [
        Validators.required,
        this.noWhitespaceValidator(),
      ]),
      schedule_at: new FormControl(this.facebook.schedule_at, [
        Validators.required,
        this.noWhitespaceValidator(),
      ]),
      file_path: new FormControl(this.facebook.file_path, [
        Validators.required,
        this.noWhitespaceValidator(),
      ]),    });
  }
  GetPagedetails() {
    var url1 = 'Facebook/GetPagedetails'
    this.service.get(url1).subscribe((result: any) => {
      $('#facebookpage_list').DataTable().destroy();
      this.facebookpage_list = result.facebookpage_list;
      setTimeout(() => {
        $('#facebookpage_list').DataTable();
      }, 100);
    });
  }
  GetPageuserdetails() {
    this.NgxSpinnerService.show();
    var url = 'Facebook/GetPageuserdetails'
    this.service.get(url).subscribe((result: any) => {
      // window.location.reload()
      this.responsedata = result;
      this.facebookuser_list = this.responsedata.facebookuser_list;
      setTimeout(() => {
        $('#facebookuser_list').DataTable();
      }, 100);
      this.NgxSpinnerService.hide();

    });
  }

  filter(searchTerm: any) {

  }

  filterData() {
    if (this.search) {
      this.filteredData = this.facebookpage_list.filter((item: { property1: string; property2: string; }) => {
        return item.property1.toLowerCase().includes(this.search.toLowerCase()) ||
          item.property2.toLowerCase().includes(this.search.toLowerCase());
        // Add more conditions for other properties if needed
      });
    } else {
      this.filteredData = this.facebookpage_list;
    }
  }

  matchesSearch(item: any): boolean {
    const searchString = this.searchText.toLowerCase();
    return item.sub.toLowerCase().includes(searchString) || item.value.toLowerCase().includes(searchString);
  }
  searchFunction() {
    const filterValue = this.searchText.toLowerCase();
    const rows = this.tableRef.nativeElement.getElementsByTagName('tr');

    for (let i = 0; i < rows.length; i++) {
      const cells = rows[i].getElementsByTagName('td');
      let foundMatch = false;

      for (let j = 0; j < cells.length; j++) {
        const cellText = cells[j].textContent || cells[j].innerText;

        if (cellText.toLowerCase().indexOf(filterValue) > -1) {
          foundMatch = true;
          break;
        }
      }

      if (foundMatch) {
        rows[i].style.display = '';
      } else {
        rows[i].style.display = 'none';
      }
    }
  }


  // this.windowInterval = window.setInterval(() => {
  //   var url = 'Facebook/GetPageuserdetails'
  //   this.socketservice.get(url).subscribe((result: any) => {
  //     this.facebookuser_list = result.facebookuser_list;

  //   });
  // }, 1000);
  noWhitespaceValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const isWhitespace = (control.value || '').trim().length === 0;
      return isWhitespace ? { whitespace: true } : null;
    };
  }

  onChange2(event: any) {
    this.file = event.target.files[0];
  }

  ////////////Add popup validtion////////
  get image_caption() {
    return this.FacebookImageForm.get('image_caption')!;
  }
  get image_schedulercaption() {
    return this.FacebookImagescheduleForm.get('image_schedulercaption')!;
  }
  get schedule_at() {
    return this.FacebookImagescheduleForm.get('schedule_at')!;
  }
  public scheduleonsubmit(): void {
    let formData = new FormData();
    if (this.file != null && this.file != undefined && this.schedule_at != null && this.schedule_at != undefined && this.image_schedulercaption != null && this.image_schedulercaption != undefined) {
      formData.append("file", this.file, this.file.name);
      formData.append("image_schedulercaption", this.FacebookImagescheduleForm.value.image_schedulercaption);
      formData.append("schedule_at", this.FacebookImagescheduleForm.value.schedule_at);
      this.NgxSpinnerService.show();

      var api = 'Facebook/ScheduleUploadImage'
      this.service.postfile(api, formData).subscribe((result: any) => {

        if (result.status == false) {
          this.NgxSpinnerService.hide();
          this.ToastrService.warning(result.message)
          this.FacebookImagescheduleForm.reset();
        }
        else {
          this.NgxSpinnerService.hide();
          this.ToastrService.success(result.message)
          this.FacebookImagescheduleForm.reset();
        }
      });

    } else {
      this.ToastrService.warning("Kindly Upload Image/Video !!")
    }
  }
  onscheduleclose() {

    this.FacebookImagescheduleForm.reset();

  }
  public onsubmit(): void {
    let formData = new FormData();
    if (this.file != null && this.file != undefined) {
      formData.append("file", this.file, this.file.name);

      formData.append("image_caption", this.FacebookImageForm.value.image_caption);
      this.NgxSpinnerService.show();
      var api = 'Facebook/UploadImage'
      this.service.postfile(api, formData).subscribe((result: any) => {

        if (result.status == false) {
          this.NgxSpinnerService.hide();
          this.ToastrService.warning(result.message)
          this.FacebookImageForm.reset();
          this.GetPagedetails();
        }
        else {
          this.NgxSpinnerService.hide();
          this.ToastrService.success(result.message)
          this.FacebookImageForm.reset();
          this.GetPagedetails();
        }
      });

    } else {
      this.ToastrService.warning("Kindly Upload Image/Video !!")
    }
  }
  onclose() {

    this.FacebookImageForm.reset();

  }
  onview(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.router.navigate(['/crm/CrmSmmFacebookpostview', encryptedParam])
  }
  // downloadImage(data: any) {
  //   if (data.post_url != null && data.post_url != "") {
  //     if (data.post_type === 'Picture') {
  //       saveAs(data.post_url, data.id + '.png');
  //     }
  //     else if (data.post_type === 'Videos') {
  //       saveAs(data.post_url, data.id + '.mp4');
  //     }
  //     else {
  //       saveAs(data.post_url, data.id + '.png');
  //     }
  //   }
  //   else {
  //     window.scrollTo({
  //       top: 0, // Code is used for scroll top after event done
  //     });
  //     this.ToastrService.warning('No Image Found')

  //   }


  // }
  downloadImage(post_url: string, post_type: string): void {

    const image = post_url.split('.net/');
    const page = image[1];
    const url = page.split('?');
    const imageurl = url[0];
    const parts = imageurl.split('.');
    const extension = parts.pop();

    this.service.downloadfile(imageurl, post_type + '.' + extension).subscribe(
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
