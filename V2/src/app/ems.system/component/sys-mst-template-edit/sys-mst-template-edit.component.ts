import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
  selector: 'app-sys-mst-template-edit',
  templateUrl: './sys-mst-template-edit.component.html',
  styleUrls: ['./sys-mst-template-edit.component.scss']
})

export class SysMstTemplateEditComponent {
  templateformedit: any;
  template_gid: any;
  templateedit_list: any;
  responsedata: any;  
  templatetype_list: any;

  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '12rem',
    minHeight: '0rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };

  ngOnInit() {
    const template_gid = this.route.snapshot.paramMap.get('template_gid');
    this.template_gid = template_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.template_gid, secretKey).toString(enc.Utf8);
    
    var api = 'SysMstTemplate/GetTemplateType';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.templatetype_list = this.responsedata.GetTemplateTypedropdown;
    });
    
    this.templateedit();
  }

  constructor(private router: Router, private route: ActivatedRoute, private fb: FormBuilder, private service: SocketService, private ToastrService: ToastrService) {

    this.templateformedit = new FormGroup ({
      template_gid_edit: new FormControl(''),
      template_name_edit: new FormControl('',[Validators.required]),
      templatetype_gid_edit: new FormControl('',[Validators.required]),
      template_content_edit: new FormControl('')
    })
  }

  get templatenameeditControl() {
    return this.templateformedit.get('template_name_edit');
  }

  get templatetypeeditControl() {
    return this.templateformedit.get('templatetype_gid_edit')
  }

  templateedit() {
    const template_gid = this.route.snapshot.paramMap.get('template_gid');
    this.template_gid = template_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.template_gid, secretKey).toString(enc.Utf8);
    
    let param = {
      template_gid: deencryptedParam
    }

    var api = 'SysMstTemplate/GetTemplateEditdata';

    console.log(param);
    this.service.getparams(api, param).subscribe((result: any) => {
      this.responsedata=result;
      this.templateedit_list = result.templateeditlist;

      this.templateformedit.get("template_gid_edit")?.setValue(this.templateedit_list[0].template_gid_edit);
      this.templateformedit.get("template_name_edit")?.setValue(this.templateedit_list[0].template_name_edit);
      this.templateformedit.get("templatetype_gid_edit")?.setValue(this.templateedit_list[0].templatetype_gid_edit);
      this.templateformedit.get("template_content_edit")?.setValue(this.templateedit_list[0].template_content_edit);
    });
  }

  templateupdate() {
    var api = 'SysMstTemplate/UpdatedTemplate';
    this.service.post(api, this.templateformedit.value).subscribe((result: any) => {
      this.responsedata=result;
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.router.navigate(['/system/SysMstTemplate']);
        this.ToastrService.success(result.message)
      }
    });
  }

  back() {
    this.router.navigate(['/system/SysMstTemplate']);
  }
}
