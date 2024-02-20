import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-sys-mst-employee-pending-summary',
  templateUrl: './sys-mst-employee-pending-summary.component.html',
  styleUrls: ['./sys-mst-employee-pending-summary.component.scss'],
  
})
export class SysMstEmployeePendingSummaryComponent {
  employeePendingList:any = [];
 
  ngAfterViewInit(): void { 
  }
 
  constructor(private SocketService: SocketService,private NgxSpinnerService: NgxSpinnerService) {
  }
 
  ngOnInit(): void {
    this.NgxSpinnerService.show();
    var url= 'ManageEmployee/EmployeePendingSummary';
   this.SocketService.get(url).subscribe((result:any)=>{
    if(result.employee != null){
      this.employeePendingList = result.employee;  
      this.NgxSpinnerService.hide();
      setTimeout(()=>{   
        $('#employeePendingtable').DataTable();
      }, 1);
    }
    else{
      this.NgxSpinnerService.hide();
      $('#employeePendingtable').DataTable();
    } 
  });



  
  } 
  

 

}
