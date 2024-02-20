import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators,  ValidationErrors,
  AbstractControl,
  ValidatorFn } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-crm-smm-instagram',
  templateUrl: './crm-smm-instagram.component.html',
  styleUrls: ['./crm-smm-instagram.component.scss']
})
export class CrmSmmInstagramComponent {

  responsedata: any;
  instagram_list: any;
  id: any;
  username: any;
  image_list: any;
  video_list: any;
  media_url: any;
  profile_photo: any;
  parameterValue: any;
  instagram_type: any;
  title: any;
  image_count: any;
  video_count: any;
  total_count: any;

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService,private NgxSpinnerService: NgxSpinnerService) {
   
  }
  ngOnInit(): void {
    this.GetInstagramsummary();

    var url = 'Instagram/GetInstagram'
      this.service.get(url).subscribe((result: any) => {

      this.id = result.id;
      this.instagram_type = result.instagram_type;
      this.username = result.username;



      console.log(this.responsedata)
    });

    var url1 = 'Instagram/GetInstagramProfile'
    this.service.get(url1).subscribe((result,) => {

    });

}
GetInstagramsummary() {

  this.NgxSpinnerService.show();
  var url1 = 'Instagram/GetInstagramProfile'
  this.service.get(url1).subscribe((result: any) => {
    $('#instagram_list').DataTable().destroy();
    this.responsedata = result;
    this.image_count = result.image_count;
    this.video_count = result.video_count;
    this.total_count = result.total_count;

    this.instagram_list = this.responsedata.instagram_list;
    this.NgxSpinnerService.hide();
    setTimeout(() => {
      $('#instagram_list').DataTable();
    }, 1);

  });
}
modalimage(parameter: string) {
  this.parameterValue = parameter
console.log(this.parameterValue)
}
downloadImage(data: any) {
  if (data.post_url != null && data.post_url != "") {
    if (data.post_type === 'IMAGE') {
      saveAs(data.post_url, data.instagram_gid + '.png');
    }
    else if (data.post_type === 'VIDEO') {
      saveAs(data.post_url, data.instagram_gid + '.mp4');
    }
    else {
      saveAs(data.post_url, data.instagram_gid + '.png');
    }
  }
  else {
    window.scrollTo({
      top: 0, // Code is used for scroll top after event done
    });
    this.ToastrService.warning('No Image Found')

  }


}


}
