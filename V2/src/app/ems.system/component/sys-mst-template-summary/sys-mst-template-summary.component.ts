import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-sys-mst-template-summary',
  templateUrl: './sys-mst-template-summary.component.html',
  styleUrls: ['./sys-mst-template-summary.component.scss']
})

export class SysMstTemplateSummaryComponent {
  responsedata: any;
  template_list: any;
  parameterValue:any;

  constructor(private router: Router, private route: ActivatedRoute, private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) { }

  ngOnInit() : void {
    this.templatesummary();
  }

  templatesummary()
  {
    var url = 'SysMstTemplate/GetTemplateSummary'
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
      this.template_list = this.responsedata.templatesummarylist;
      setTimeout(() => {
        $('#template_list').DataTable();
      }, 1);
    });
  }

  edittemplate(params: string){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/system/SysMstTemplateEdit',encryptedParam])
  }

  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  }

  ondelete() {
    console.log(this.parameterValue);
    var url3 = 'SysMstTemplate/DeleteTemplate'
    this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
        this.templatesummary();
      }
      else {
        this.ToastrService.success(result.message)
        this.templatesummary();
      }
    });
  }
}
