import { Component } from '@angular/core';
import {
  FormBuilder, FormControl, FormGroup, Validators, ValidationErrors,
  AbstractControl,
  ValidatorFn
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';

interface ILinkedin {
  body_content: string;


}
@Component({
  selector: 'app-crm-smm-linkedaccount',
  templateUrl: './crm-smm-linkedaccount.component.html',
  styleUrls: ['./crm-smm-linkedaccount.component.scss']
})
export class CrmSmmLinkedaccountComponent {
  invite_link: any;
  id: any;
  user_name: any;
  group: any;
  profile_photo: any;
  linkedin_list: any;
  LinkedinForm!: FormGroup;
  linkedin!: ILinkedin;
  responsedata: any;
  linkedin_summary: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService,private NgxSpinnerService: NgxSpinnerService) {
    this.linkedin = {} as ILinkedin;
  }
  ngOnInit(): void {
    this.Getlinkedinsummary();

    this.LinkedinForm = new FormGroup({

      body_content: new FormControl(this.linkedin.body_content, [
        Validators.required,
        this.noWhitespaceValidator(),

      ]),




    });

    var url = 'Linkedin/GetLinkedinProfile'
    this.service.get(url).subscribe((lsprofile_picture) => {
      this.profile_photo = lsprofile_picture

      //  this.profile_photo=result;
      //  console.log(this.profile_photo)


    });
    var url1 = 'Linkedin/GetLinkedinUser'
    this.service.get(url1).subscribe((lsuser_name) => {
      this.user_name = lsuser_name;
      // console.log(this.linkedin_list)
      // console.log(this.linkedin_list.localizedFirstName)
      // console.log(this.linkedin_list.localizedLastName)

    });
  }
  Getlinkedinsummary() {

    this.NgxSpinnerService.show();
    var url1 = 'Linkedin/Getlinkedin'
    this.service.get(url1).subscribe((result: any) => {
      $('#linkedin_summary').DataTable().destroy();
      this.responsedata = result;
   
      this.linkedin_summary = this.responsedata.linkedin_summary;
      this.NgxSpinnerService.hide();
      setTimeout(() => {
        $('#linkedin_summary').DataTable();
      }, 1);

    });
  }
  noWhitespaceValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const isWhitespace = (control.value || '').trim().length === 0;
      return isWhitespace ? { whitespace: true } : null;
    };
  }
  //////////// popup validtion////////
  get body_content() {
    return this.LinkedinForm.get('body_content')!;
  }

  public onsubmit(): void {
    // console.log(this.LinkedinForm.value)
    var url = 'Linkedin/Postlinkedin'
    this.service.post(url, this.LinkedinForm.value).subscribe((result: any) => {

      if (result.status == false) {
        this.LinkedinForm.reset();
        this.ToastrService.warning(result.message)
        this.Getlinkedinsummary();


      }
      else {
        this.LinkedinForm.reset();
        this.ToastrService.success(result.message)
        this.Getlinkedinsummary();

      }

    });
  }
  onclose() {
    this.LinkedinForm.reset();
  }
}
