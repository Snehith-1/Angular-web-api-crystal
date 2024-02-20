import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { AES, enc } from 'crypto-js';

@Component({
  selector: 'app-hrm-trn-probationhistory',
  templateUrl: './hrm-trn-probationhistory.component.html',
  styleUrls: ['./hrm-trn-probationhistory.component.scss']
})
export class HrmTrnProbationhistoryComponent {
  probationhistory:any[] = [];
  employee_gid:any;

  employee_list: any[] = [];
  response_data :any;


  constructor(private fb: FormBuilder,private route: ActivatedRoute,private router: Router,private service: SocketService,) {} 

  ngOnInit(): void {
    const employee_gid = this.route.snapshot.paramMap.get('employee_gid');
    this.employee_gid = employee_gid;
  
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.employee_gid, secretKey).toString(enc.Utf8);
  
    let param = {
      employee_gid: deencryptedParam
    }

    var api = 'Probationperiod/GetProbationhistorySummary';
    this.service.getparams(api, param).subscribe((result:any) => {
      this.response_data = result;
      this.probationhistory = this.response_data.employee_list1;
      setTimeout(()=>{  
        $('#history').DataTable();
      }, 1);
    });
  }




}
