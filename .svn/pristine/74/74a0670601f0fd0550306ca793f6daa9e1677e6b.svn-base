import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
@Component({
  selector: 'app-hrm-mst-leavegrade',
  templateUrl: './hrm-mst-leavegrade.component.html',
  styleUrls: ['./hrm-mst-leavegrade.component.scss']
})
export class HrmMstLeavegradeComponent {
  responsedata:any;
  Leavegrade_list:any[] = [];
  leavegrade_gid: any;

constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
  }
  ngOnInit(): void { 

    //// Summary Grid//////
var url = 'LeaveGrade/LeaveGradeSummary'
   
this.service.get(url).subscribe((result: any) => {
this.responsedata = result;
this.Leavegrade_list = this.responsedata.leavegrade_list;
  setTimeout(() => {
    $('#Leavegrade_list').DataTable();
    }, );
});
}
Leavegradeadd(){
  this.router.navigate(['/hrm/HrmMstAddLeaveGrade'])
}

openModalAssignEmployee(params:any){
  debugger;
  const secretKey = 'storyboarderp';
  const param = (params);
  const encryptedParam = AES.encrypt(param,secretKey).toString();
  this.router.navigate(['/hrm/HrmTrnLeavegradeassign2employee',encryptedParam])
}
openModalUnssignEmployee(params:any){
  debugger;
  const secretKey = 'storyboarderp';
  const param = (params);
  const encryptedParam = AES.encrypt(param,secretKey).toString();
  this.router.navigate(['/hrm/HrmTrnLeavegradeunassign2employee',encryptedParam])
}


}
