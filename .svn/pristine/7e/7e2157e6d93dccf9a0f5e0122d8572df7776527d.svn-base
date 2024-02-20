import { Component } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AES } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from '../../../ems.utilities/services/socket.service';

@Component({
  selector: 'app-pay-trn-bonussummary',
  templateUrl: './pay-trn-bonussummary.component.html',
  styleUrls: ['./pay-trn-bonussummary.component.scss']
})
export class PayTrnBonussummaryComponent {
  reactiveForm!: FormGroup;
  responsedata: any;
  parameterValue : any;
  GetBonus : any[] = [];


  constructor(public service :SocketService,private route:Router,private ToastrService: ToastrService, private FormBuilder: FormBuilder) {

  }
  
  ngOnInit(): void {
   this.getsummary();
  }
getsummary(){
  var api = 'PayTrnBonus/GetBonusSummary';
    this.service.get(api).subscribe((result:any) => {
      $('#bonus').DataTable().destroy();
      this.GetBonus =result.GetBonus;
      setTimeout(()=>{  
        $('#bonus').DataTable();
      }, 1);
    });
}
  createbonus() {
    this.route.navigate(['/payroll/PayTrnBonuscreate'])
  }

  deleteAction(parameter: string) {
    debugger;
    this.parameterValue = parameter
  }

  ondelete() {
    debugger;
    console.log(this.parameterValue);
    var url3 = 'PayTrnBonus/deleteBonus'
    this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.ToastrService.success(result.message)
      }
    });
  }

  selectEmployeeAction(params:any){
    debugger;
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/payroll/PayTrnEmployee2bonus',encryptedParam])

  }
  GenerateBonus(params:any){
    debugger;
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/payroll/PayTrnGeneratedbonusview',encryptedParam])
  }
}
