import { Component } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
@Component({
  selector: 'app-hrm-mst-role-summary',
  templateUrl: './hrm-mst-role-summary.component.html',
  styleUrls: ['./hrm-mst-role-summary.component.scss']
})
export class HrmMstRoleSummaryComponent {
  roleMasterData: any;
  parametervalue: any;
 
 lstab: any;
 
 constructor(private SocketService: SocketService,public router:Router,private NgxSpinnerService: NgxSpinnerService,private ToastrService: ToastrService){}

 ngOnInit() {
   this.NgxSpinnerService.show();
   var url= 'ManageRole/RoleSummary';
   this.SocketService.get(url).subscribe((result:any)=>{
     if(result.role != null){
       $('#roleManagement').DataTable().destroy();
       this.roleMasterData = result.role;  
       this.NgxSpinnerService.hide();
       setTimeout(()=>{   
         $('#roleManagement').DataTable();
       }, 1);
     }
     else{
       setTimeout(()=>{   
         $('#roleManagement').DataTable();
       }, 1);
       this.roleMasterData = result.role; 
       this.NgxSpinnerService.hide();
       $('#roleManagement').DataTable().destroy();
     } 
   });
 
 }

 role_edit(role_gid:any){
   //Edit the values in database 
   const url = `/hrm/HrmMstRoleEdit?role_gid=${role_gid}&lstab=${this.lstab}`;
   this.router.navigateByUrl(url);
 }

 assignuserrole(role_gid:any){
  //Edit the values in database 
  const url = `/hrm/HrmMstAssignuserrole?role_gid=${role_gid}`;
  this.router.navigateByUrl(url);
}

 delete(parameter:any){
this.parametervalue = parameter
 
 }
 ondelete(){
   this.NgxSpinnerService.show();
   var url = 'ManageRole/RoleDelete';
   let params = {
     role_gid : this.parametervalue
   }
   this.SocketService.getparams(url, params).subscribe((result:any) => {
     if(result.status == true){
       this.NgxSpinnerService.hide();
       this.ToastrService.success("Role Deleted Successfully");
       this.ngOnInit();
     }
     else {
       this.ToastrService.warning("Error Occurred While Deleting Base Location!");
       this.NgxSpinnerService.hide();
       this.ngOnInit();
     }   
   })
 }
}

