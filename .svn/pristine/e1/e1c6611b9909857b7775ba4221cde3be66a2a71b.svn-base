import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
  selector: 'app-pmr-mst-termsandcondition-edit',
  templateUrl: './pmr-mst-termsandcondition-edit.component.html',
  styleUrls: ['./pmr-mst-termsandcondition-edit.component.scss']
})
export class PmrMstTermsandconditionEditComponent {
  templateformedit:FormGroup| any;
  
  templateedit_list: any;
  responsedata: any;  
  templatetype_list: any;
  termsconditions_gid:any;

  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '25rem',
    minHeight: '5rem',
    width: '1050px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };

  ngOnInit() {
    const termsconditions_gid = this.route.snapshot.paramMap.get('termsconditions_gid');
    this.termsconditions_gid = termsconditions_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.termsconditions_gid, secretKey).toString(enc.Utf8);
    
    // var api = 'SysMstTemplate/GetTemplateType';
    // this.service.get(api).subscribe((result: any) => {
    //   this.responsedata = result;
    //   this.templatetype_list = this.responsedata.GetTemplateTypedropdown;
    // });
    this.templateedit( deencryptedParam) 

   
  }

  constructor(private router: Router, private route: ActivatedRoute, private fb: FormBuilder, private service: SocketService, private ToastrService: ToastrService) {

    this.templateformedit = new FormGroup ({
      termsconditions_gid: new FormControl(''),
      payment_terms : new FormControl(''),
      template_name: new FormControl('',[Validators.required]),
      template_content: new FormControl('',[Validators.required]),
    })
  }

  get template_name() {
    return this.templateformedit.get('template_name');
  }

  get template_content() {
    return this.templateformedit.get('template_content')
  }

  get template_type() {
    return this.templateformedit.get('payment_terms')
  }

  templateedit(termsconditions_gid:any) {
    debugger
    
    let param = {
      termsconditions_gid: termsconditions_gid
    }

    var api = 'PmrMstTermsConditions/GetTemplateEditdata';

    console.log(param);
    this.service.getparams(api, param).subscribe((result: any) => {
      this.responsedata=result;
      this.templateedit_list = result.templateedit_list;

      this.templateformedit.get("termsconditions_gid")?.setValue(this.templateedit_list[0].termsconditions_gid);
      this.templateformedit.get("template_name")?.setValue(this.templateedit_list[0].template_name);
      this.templateformedit.get("template_content")?.setValue(this.templateedit_list[0].template_content);
      this.templateformedit.get("payment_terms")?.setValue(this.templateedit_list[0].payment_terms);
    });
  
  }

  templateupdate() {
    var api = 'PmrMstTermsConditions/UpdatedTemplate';
    this.service.post(api, this.templateformedit.value).subscribe((result: any) => {
      this.responsedata=result;
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.router.navigate(['/pmr/PmrMstTermsconditionssummary']);
        this.ToastrService.success(result.message)
      }
    });
  }

  
  
}


