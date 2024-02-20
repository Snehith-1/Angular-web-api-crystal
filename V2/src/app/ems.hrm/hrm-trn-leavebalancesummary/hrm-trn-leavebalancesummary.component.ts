import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../ems.utilities/services/socket.service';
import { AES } from 'crypto-js';


@Component({
  selector: 'app-hrm-trn-leavebalancesummary',
  templateUrl: './hrm-trn-leavebalancesummary.component.html',
  styleUrls: ['./hrm-trn-leavebalancesummary.component.scss']
})
export class HrmTrnLeavebalancesummaryComponent {

  leavebalance : any;
  response_data : any;



  constructor(private fb: FormBuilder,private route: ActivatedRoute,private router: Router,private service: SocketService,) {} 

  ngOnInit(): void {
    var api = 'Leaveopening/GetLeaveopening';
    this.service.get(api).subscribe((result:any) => {
      this.response_data = result;
      this.leavebalance = this.response_data.leaveopening_list;
      setTimeout(()=>{  
        $('#leavebalance').DataTable();
      }, 1);
    });
  }

  onedit(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmTrnLeavebalanceedit',encryptedParam]) 
  }

  onview(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmTrnLeavebalancesummary',encryptedParam]) 
  }


}


