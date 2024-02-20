import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-crm-smm-facebookaccount',
  templateUrl: './crm-smm-facebookaccount.component.html',
  styleUrls: ['./crm-smm-facebookaccount.component.scss']
})
export class CrmSmmFacebookaccountComponent {
  facebookuser_list: any;
  facebook_list: any;
  username: any;
  picture: any;
  friends: any;
  posts: any;
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
  responsedata: any;

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {

  }
  ngOnInit(): void {
    var url = 'Facebook/GetFacebook'
    this.service.get(url).subscribe((result: any) => {
      this.facebook_list = result;
      if (this.facebook_list.posts != null && this.facebook_list.posts != "") {
        this.posts = this.facebook_list.posts.data;
      }
      else {
        this.videos = null;
      }

      if (this.facebook_list.videos != null && this.facebook_list.videos != "") {
        this.videos = this.facebook_list.videos.data;
      }
      else {
        this.videos = null;
      }
      // this.videos = this.facebookuser_list.videos.data;
      console.log(this.posts)
      console.log(this.friends)
      console.log(this.facebook_list)

    });


    var url = 'Facebook/GetFacebookuserdetails'

    this.service.get(url).subscribe((result: any) => {
      $('#facebookuser_list').DataTable().destroy();
      // window.location.reload()
      this.responsedata = result;
      this.facebookuser_list = this.responsedata.facebookuser_list;
      //console.log(this.source_list)
    });

  }
}
