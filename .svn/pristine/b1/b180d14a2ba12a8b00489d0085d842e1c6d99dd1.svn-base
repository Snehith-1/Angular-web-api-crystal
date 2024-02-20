import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
@Component({
  selector: 'app-hrm-mst-holidaygrade-management',
  templateUrl: './hrm-mst-holidaygrade-management.component.html',
  styleUrls: ['./hrm-mst-holidaygrade-management.component.scss']
})
export class HrmMstHolidaygradeManagementComponent {
  responsedata:any;
  Holidaygrade_list:any[] = [];
  parameterValue: any;

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
  }
  ngOnInit(): void { 

    //// Summary Grid//////
var url = 'HolidayGradeManagement/HolidayGradeSummary'
   
this.service.get(url).subscribe((result: any) => {
this.responsedata = result;
this.Holidaygrade_list = this.responsedata.holidaygrade_list;
  setTimeout(() => {
    $('#Holidaygrade_list').DataTable();
    }, );
});
}

HolidayGrade(){
  this.router.navigate(['/hrm/HrmMstAddHolidaygrademanagement'])
}

openModaldelete(parameter: string) {
  this.parameterValue = parameter
}

ondelete() {
  console.log(this.parameterValue);
  var url3 = 'HolidayGradeManagement/Deleteholiday'
  this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
    if (result.status == false) {
      this.ToastrService.warning(result.message)

    }
    else {
      this.ToastrService.success(result.message)
    }
    window.location.reload();

  });
}
}
