import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-sys-mst-userprofile',
  templateUrl: './sys-mst-userprofile.component.html',
  styleUrls: ['./sys-mst-userprofile.component.scss']
})

export class SysMstUserprofileComponent {
  employee_details: any;
  document_list: any;
  constructor(public router:Router,public SocketService:SocketService,private NgxSpinnerService: NgxSpinnerService){

  }
  ngOnInit():void{
    this.NgxSpinnerService.show();
    var url='ManageEmployee/EmployeeProfileView';
    this.SocketService.get(url).subscribe((result:any)=>{
      this.employee_details  = result;
      this.NgxSpinnerService.hide();
    });
    this.NgxSpinnerService.show();
    // var url='ManageEmployee/GetHRDocProfilelist';
    // this.SocketService.get(url).subscribe((result:any)=>{
    //   this.document_list= result;
    //   this.NgxSpinnerService.hide();
    // });

  }

  backbutton(){
    this.router.navigate(['/system/SystemDashboard']);
  }

  };
