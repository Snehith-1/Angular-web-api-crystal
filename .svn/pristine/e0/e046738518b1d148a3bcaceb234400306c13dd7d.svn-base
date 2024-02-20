import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AES } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
@Component({
  selector: 'app-pay-trn-bonussummary',
  templateUrl: './pay-trn-bonussummary.component.html',
  styleUrls: ['./pay-trn-bonussummary.component.scss']
})
export class PayTrnBonussummaryComponent {
  reactiveForm!: FormGroup;
  bonus_list: any[] = [];
  responsedata: any;


  constructor(public service :SocketService,private route:Router,private ToastrService: ToastrService) {}
  ngOnInit(): void {
    this.GetBonusSummary();
    this.reactiveForm = new FormGroup({
      attribute_name: new FormControl(''),
     
    });
     
   
    


  }
  GetBonusSummary(){
    var url = 'PayTrnBonus/GetBonusSummary'
    this.service.get(url).subscribe((result: any) => {
  
      this.responsedata = result;
      this.bonus_list = this.responsedata.GetBonus;
  
      setTimeout(() => {
        $('#bonus_list').DataTable();
      }, 1);
  
  
    });
  }
  selectEmployeeAction(params:any){
    debugger;
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/payroll/PayTrnEmployee2bonus',encryptedParam])

  }
  generateBonusAction(parameter: string){
    
  }
  deleteAction(parameter: string){
    
  }

}
