import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-hrm-trn-companypolicy',
  templateUrl: './hrm-trn-companypolicy.component.html',
  styleUrls: ['./hrm-trn-companypolicy.component.scss']
})

export class HrmTrnCompanypolicyComponent {

  policies_list: any[] = [];

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private router: Router) { }

  ngOnInit(): void {
    var url = 'hrmTrnDashboard/GetCompanyPolicies';
    this.service.get(url).subscribe((result: any) => {
      this.policies_list = result.CompanyPolicies;
    });
  }

  preprocessPolicyDesc(policyDesc: string): string {
    // Replace the tab character with a line break
    return policyDesc.replace(/\t/g, '<br>');
  }

  back() {
    this.router.navigate(['/hrm/HrmMemberDashboard'])
  }
}
