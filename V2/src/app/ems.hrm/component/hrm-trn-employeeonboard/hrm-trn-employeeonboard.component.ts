import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { AES } from 'crypto-js';
@Component({
  selector: 'app-hrm-trn-employeeonboard',
  templateUrl: './hrm-trn-employeeonboard.component.html',
  styleUrls: ['./hrm-trn-employeeonboard.component.scss']
})
export class HrmTrnEmployeeonboardComponent {
  employeePendingList:any = [];
  lstab='summary';
  ngAfterViewInit(): void { 
  }
 
  constructor(private SocketService: SocketService,private NgxSpinnerService: NgxSpinnerService,public router:Router) {
  }
 
  ngOnInit(): void {
    this.NgxSpinnerService.show();
    var url= 'EmployeeOnboard/EmployeePendingSummary';
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
  
  // employee_view(params:any){
  //   const parameter1 = `${params}&Pending`;
  //   const secretKey = 'storyboarderp';
  //   const encryptedParam = AES.encrypt(parameter1,secretKey).toString();
  //   var url = '/system/SysMstEmployeeView?hash=' + encryptedParam;
  //   this.router.navigateByUrl(url)

  // }
  employee_view(employee_gid: any)  
		{
        const url = `/hrm/HrmtrnEmployeeonboardviewpending?employee_gid=${employee_gid}&lstab=${this.lstab}`;
			   this.router.navigateByUrl(url);
		}

    employee_edit(employee_gid: any){
      const url = `/hrm/HrmtrnEmployeeonboardedit?employee_gid=${employee_gid}&lstab=${this.lstab}`;
      this.router.navigateByUrl(url);
    }

    // employee_edit(params:any){
    //   const parameter1 = `${params}&Pending`;
    //   const secretKey = 'storyboarderp';
    //   const encryptedParam = AES.encrypt(parameter1,secretKey).toString();
    //   var url = '/system/SysMstEmployeeEdit?hash=' + encryptedParam;
    //   this.router.navigateByUrl(url)

    // }

}
