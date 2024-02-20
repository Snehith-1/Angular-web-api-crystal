import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-pmr-mst-termsconditionssummary',
  templateUrl: './pmr-mst-termsconditionssummary.component.html',
  styleUrls: ['./pmr-mst-termsconditionssummary.component.scss']
})
export class PmrMstTermsconditionssummaryComponent {
  template_list: any[] = [];
  responsedata: any;
  parameter: any;
  parameterValue: any;
  parameterValue1: any;
  reactiveForm: any;

  constructor(public service: SocketService,public NgxSpinnerService:NgxSpinnerService, private ToastrService: ToastrService, private route: Router,) {



  }
  ngOnInit(): void {
    this.GetTermsConditionsSummary();
  }

  GetTermsConditionsSummary() {
    var url = 'PmrMstTermsConditions/GetTermsConditionsSummary'
    this.NgxSpinnerService.show()

    this.service.get(url).subscribe((result: any) => {
      $('#template_list').DataTable().destroy();
      this.responsedata = result;
      this.template_list = this.responsedata.Gettemplate_list;
      console.log(this.template_list)
      setTimeout(() => {
        $('#template_list').DataTable();
      }, 1);
      this.NgxSpinnerService.hide()


    });
  }

  onview(params: any) {

  }

  onedit(params: any) {
    debugger
    const secretKey = 'storyboarderp';
    const param = (params);
    console.log(param)
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.route.navigate(['/pmr/PmrMstTermsandconditionEdit', encryptedParam])
  }

  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  }

  
  ondelete() {
    debugger;
    console.log(this.parameterValue);
    var url = 'PmrMstTermsConditions/DeleteTemplate'
    let param = {
      termsconditions_gid : this.parameterValue 
    }
    this.service.getparams(url,param).subscribe((result: any) => {
      if(result.status ==false){
        this.ToastrService.warning(result.message)
      }
      else{
        this.ToastrService.success(result.message)
     window.location.reload();
      }
      
      this.GetTermsConditionsSummary();
      window.location.reload();
    
  
  
    });
  }
  onclose() {
    this.reactiveForm.reset();

  }
    }
    