import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-hrm-member-officecalendar',
  templateUrl: './hrm-member-officecalendar.component.html',
  styleUrls: ['./hrm-member-officecalendar.component.scss']
})
export class HrmMemberOfficecalendarComponent {

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) {}
  
  back() {
    this.router.navigate(['/hrm/HrmMemberDashboard'])
  }
}
