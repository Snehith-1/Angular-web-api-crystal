import {  Component, OnInit } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-hrm-mst-branch-summary',
  templateUrl: './hrm-mst-branch-summary.component.html',
  styleUrls: ['./hrm-mst-branch-summary.component.scss']
})
export class HrmMstBranchSummaryComponent {
  branch_data:any = [];
 
  ngAfterViewInit(): void { 
  }
 
  constructor(private SocketService: SocketService,private NgxSpinnerService: NgxSpinnerService) {
  }
 
  ngOnInit(): void {
    this.NgxSpinnerService.show();
    var url= 'SystemMaster/GetBranchSummary';
   this.SocketService.get(url).subscribe((result:any)=>{
    if(result.master_list != null){
      $('#BranchSummary').DataTable().destroy();
      this.branch_data = result.master_list;  
      this.NgxSpinnerService.hide();
      setTimeout(()=>{   
        $('#BranchSummary').DataTable();
      }, 1);
    }
    else{
      setTimeout(()=>{   
        $('#BranchSummary').DataTable();
      }, 1);
      this.NgxSpinnerService.hide();
      $('#BranchSummary').DataTable().destroy();
    } 
  });
  
  } 

}



