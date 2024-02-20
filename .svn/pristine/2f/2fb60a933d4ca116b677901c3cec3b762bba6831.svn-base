import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-crm-smm-facebookpostview',
  templateUrl: './crm-smm-facebookpostview.component.html',
  styleUrls: ['./crm-smm-facebookpostview.component.scss']

})
export class CrmSmmFacebookpostviewComponent {
  isCommentVisible = false;
  viewfacebook_list: any;
  responsedata: any;
  facebook: any;
  post_url: any;
  post_id: any;
  facebookmain_gid: any;
  caption: any;
  views_count: any;
  post_type: any;
  facebookuser_list: any;
  postcreated_time: any;
  comments_count: any;


  constructor(private route: Router, private router: ActivatedRoute, public service: SocketService,private NgxSpinnerService: NgxSpinnerService) { }


  ngOnInit(): void {
    this.GetPageuserdetails();

    const facebookmain_gid = this.router.snapshot.paramMap.get('facebookmain_gid');
    this.facebook = facebookmain_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.facebook, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.GetViewFaceBookSummary(deencryptedParam);
  }
  GetViewFaceBookSummary(facebookmain_gid: any) {
    var url = 'Facebook/GetViewFacebook'
    let param = {
      facebookmain_gid: facebookmain_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.viewfacebook_list = result.GetViewFaceBookSummary;
      this.post_url = result.post_url
      this.post_id = result.post_id
      this.facebookmain_gid = result.facebookmain_gid
      this.caption = result.caption
      this.views_count = result.views_count
      this.post_type = result.post_type
      this.postcreated_time = result.postcreated_time

      this.comments_count = result.comments_count
    });
  }

  GetPageuserdetails() {
    this.NgxSpinnerService.show();
    var url = 'Facebook/GetPageuserdetails'
    this.service.get(url).subscribe((result: any) => {
      // window.location.reload()
      this.responsedata = result;
      this.facebookuser_list = this.responsedata.facebookuser_list;
      this.NgxSpinnerService.hide();
      setTimeout(() => {
        $('#facebookuser_list').DataTable();
      }, 100);
    });


}
toggleCommentVisibility() {
  this.isCommentVisible = !this.isCommentVisible;
}
}
