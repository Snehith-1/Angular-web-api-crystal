import {  Component, OnInit } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-hrm-mst-department-summary',
  templateUrl: './hrm-mst-department-summary.component.html',
  styleUrls: ['./hrm-mst-department-summary.component.scss']
})
export class HrmMstDepartmentSummaryComponent {
  department_data:any = [];
 
  ngAfterViewInit(): void { 
  }
 
  constructor(private SocketService: SocketService,private NgxSpinnerService: NgxSpinnerService) {
  }
 
  ngOnInit(): void {
    this.NgxSpinnerService.show();
    var url= 'SystemMaster/GetDepartmentSummary';
   this.SocketService.get(url).subscribe((result:any)=>{
    if(result.master_list != null){
      $('#DepartmentSummary').DataTable().destroy();
      this.department_data = result.master_list;  
      this.NgxSpinnerService.hide();
      setTimeout(()=>{   
        $('#DepartmentSummary').DataTable();
      }, 1);
    }
    else{
      this.NgxSpinnerService.hide();
      $('#DepartmentSummary').DataTable();
    } 
  });  
  } 

}



