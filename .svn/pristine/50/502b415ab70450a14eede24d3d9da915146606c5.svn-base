import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-hrm-member-monthlyattendance',
  templateUrl: './hrm-member-monthlyattendance.component.html',
  styleUrls: ['./hrm-member-monthlyattendance.component.scss']
})
export class HrmMemberMonthlyattendanceComponent {

  monthlyreport : any;
  response_data : any;


  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) {}


  ngOnInit(): void {
    var api = 'hrmTrnDashboard/monthlyAttendenceReport';
    this.service.get(api).subscribe((result:any) => {
      this.response_data = result;
      this.monthlyreport = this.response_data.monthlyAttendenceReport_list;
      setTimeout(()=>{  
        $('#monthlyreport').DataTable();
      }, 1);
    });
  }

  back() {
    this.router.navigate(['/hrm/HrmMemberDashboard'])
  }
}
