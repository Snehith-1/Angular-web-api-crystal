import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { AES } from 'crypto-js';


@Component({
  selector: 'app-hrm-member-approveleave',
  templateUrl: './hrm-member-approveleave.component.html',
  styleUrls: ['./hrm-member-approveleave.component.scss']
})
export class HrmMemberApproveleaveComponent {
  
  isApprovalPendingTableOpen: boolean = false;
  isApprovedTableOpen: boolean = false;
  isRejectedTableOpen: boolean = false;
  count_approvalpending: any;
  count_approval: any;
  count_rejected: any;
  count_history: any;
  leavesummary: any;
  permissionreject: any;
  compoffreject: any;
  ondutyreject: any;
  logoutreject: any;
  loginreject: any;
  leavereject: any;
  permissionsummary: any;
  compoffsummary: any;
  ondutysummary: any;
  logoutsummary: any;
  leave: any;
  permission: any;
  compoff: any;
  onduty: any;
  logout: any;
  login: any;
  response: any;
  loginpending_list: any;
  applyleave_list: any;
  login_list: any;
  logoutrejected_list: any;
  logout_list: any;
  ODpending_list: any;
  od_list: any;
  permissionpending_list: any;
  permission_list: any;
  compoffpending_list: any;
  compoffdtl_list: any;
  response_data: any;
  loginsummary: any;
  NgxSpinnerService: any;
  approveleaveclick: any;

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) {}

  ngOnInit(): void {

    var api = 'approveLeave/getapproval_count';
    this.service.get(api).subscribe((result: any) => {
      this.response = result;
      this.count_approvalpending = result.count_approvalpending;
      this.count_approval = result.count_approval;
      this.count_rejected = result.count_rejected;
      this.count_history = result.count_history;
    })

    var api1='approveLeave/getleaveapprovependingdetails'
    this.service.get(api1).subscribe((result:any)=> {
      this.response_data = result;
      this.leavesummary = this.response_data.applyleave_list;
      setTimeout(()=>{  
        $('#leavesummary').DataTable();
      }, 1);
    });

    var api2='approveLeave/getleaveapprovedetails'
    this.service.get(api2).subscribe((result:any)=> {
      this.response_data = result;
      this.leave = this.response_data.applyleave_list;
      setTimeout(()=>{  
        $('#leave').DataTable();
      }, 1);    
    });

    var api3='approveLeave/getleaverejectdetails'
    this.service.get(api3).subscribe((result:any)=> {
      this.response_data = result;
      this.leavereject = this.response_data.applyleave_list;
      setTimeout(()=>{  
        $('#leavereject').DataTable();
      }, 1);    
    });

    var api4='approveLeave/getloginsummarydetails'
    this.service.get(api4).subscribe((result:any)=> {
      this.response_data = result;
      this.login = this.response_data.loginpending_list;
      setTimeout(()=>{  
        $('#login').DataTable();
      }, 1);    
    });

    var api5='approveLeave/getloginleaveapprovedetails'
    this.service.get(api5).subscribe((result:any)=> {
      this.response_data = result;
      this.loginsummary = this.response_data.login_list;
      setTimeout(()=>{  
        $('#loginsummary').DataTable();
      }, 1);    
    });

    var api6='approveLeave/getloginleaverejectdetails'
    this.service.get(api6).subscribe((result:any)=> {
      this.response_data = result;
      this.loginreject = this.response_data.login_list;
      setTimeout(()=>{  
        $('#loginreject').DataTable();
      }, 1);    
    });
    
    var api7='approveLeave/getlogoutsummarydetails'
    this.service.get(api7).subscribe((result:any)=> {
      this.response_data = result;
      this.logout = this.response_data.logoutrejected_list;
      setTimeout(()=>{  
        $('#logout').DataTable();
      }, 1);    
    });

    var api8='approveLeave/getlogoutleaveapprovedetails'
    this.service.get(api8).subscribe((result:any)=> {
      this.response_data = result;
      this.logoutsummary = this.response_data.logout_list;
      setTimeout(()=>{  
        $('#logoutsummary').DataTable();
      }, 1);
    });

    var api9='approveLeave/getlogoutleaverejectdetails'
    this.service.get(api9).subscribe((result:any)=> {
      this.response_data = result;
      this.logoutreject = this.response_data.logout_list;
      setTimeout(()=>{  
        $('#logoutreject').DataTable();
      }, 1);
    });

    var api10='approveLeave/getODsummarydetails'
    this.service.get(api10).subscribe((result:any)=> {
      this.response_data = result;
      this.onduty = this.response_data.ODpending_list;
      setTimeout(()=>{  
        $('#onduty').DataTable();
      }, 1);    
    });

    var api11='approveLeave/getodleaveapprovedetails'
    this.service.get(api11).subscribe((result:any)=> {
      this.response_data = result;
      this.ondutysummary = this.response_data.od_list;
      setTimeout(()=>{  
        $('#ondutysummary').DataTable();
      }, 1);    
    });

    var api12='approveLeave/getodleaverejectdetails'
    this.service.get(api12).subscribe((result:any)=> {
      this.response_data = result;
      this.ondutyreject = this.response_data.od_list;
      setTimeout(()=>{  
        $('#ondutyreject').DataTable();
      }, 1);    
    });

    var api13='approveLeave/getPermissionsummarydetails'
    this.service.get(api13).subscribe((result:any)=> {
      this.response_data = result;
      this.permission = this.response_data.permissionpending_list;
      setTimeout(()=>{  
        $('#permission').DataTable();
      }, 1);    
    });

    var api14='approveLeave/getpermissionleaveapprovedetails'
    this.service.get(api14).subscribe((result:any)=> {
      this.response_data = result;
      this.permissionsummary = this.response_data.permission_list;
      setTimeout(()=>{  
        $('#permissionsummary').DataTable();
      }, 1);    
    });
    
    var api15='approveLeave/getpermissionleaverejectdetails'
    this.service.get(api15).subscribe((result:any)=> {
      this.response_data = result;
      this.permissionreject = this.response_data.permission_list;
      setTimeout(()=>{  
        $('#permissionreject').DataTable();
      }, 1);    
    });

    

    var api16='approveLeave/getCompoffsummarydetails'
    this.service.get(api16).subscribe((result:any)=> {
      this.response_data = result;
      this.compoff = this.response_data.compoffpending_list;
      setTimeout(()=>{  
        $('#compoff').DataTable();
      }, 1);    
    });

    var api17='approveLeave/getcompoffleaveapprovedetails'
    this.service.get(api17).subscribe((result:any)=> {
      this.response_data = result;
      this.compoffsummary = this.response_data.compoffdtl_list;
      setTimeout(()=>{  
        $('#compoffsummary').DataTable();
      }, 1);    
    });

    var api18='approveLeave/getcompoffleaverejectdetails'
    this.service.get(api18).subscribe((result:any)=> {
      this.response_data = result;
      this.compoffreject = this.response_data.compoffdtl_list;
      setTimeout(()=>{  
        $('#compoffreject').DataTable();
      }, 1);    
    });

   
    var api19='approveLeave/GetLeaveApproveDetails'
    this.service.get(api19).subscribe((result:any)=> {
      this.response_data = result;
      this.leavesummary = this.response_data.GetLeaveApproveDetails;
      setTimeout(()=>{  
        $('#leavesummary').DataTable();
      }, 1);
    });
  }

  approveleave(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
 
    var url = 'approveleave/approveleaveclick';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }

  rejectleave(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
    
    var url = 'approveleave/rejectleaveclick';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }

  approvelogin(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
    
    var url = 'approveleave/approvelogin';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }

  rejectlogin(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
    
    var url = 'approveleave/rejectlogin';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }

  approvelogout(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
    
    var url = 'approveleave/approvelogout';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }

  rejectlogout(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
    
    var url = 'approveleave/rejectlogout';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }

  approveod(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
    
    var url = 'approveleave/approveOD';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }

  rejectod(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
    
    var url = 'approveleave/rejectOD';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }
  
  approvecompoff(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
    
    var url = 'approveleave/approvecompoff';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }
  
  rejectcompoff(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
    
    var url = 'approveleave/rejectcompoff';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }

  approvepermission(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
    
    var url = 'approveleave/approvepermission';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }

  rejectpermission(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/hrm/HrmMemberDashboard',encryptedParam]) 
    
    var url = 'approveleave/rejectpermission';
    this.service.post(url,FormData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }
  
  back() {
    this.router.navigate(['/hrm/HrmMemberDashboard'])
  }

  toggleApprovalPendingTable() {
    this.isApprovalPendingTableOpen = !this.isApprovalPendingTableOpen;
  }

  toggleApprovedTable() {
    this.isApprovedTableOpen = !this.isApprovedTableOpen;
  }

  toggleRejectedTable() {
    this.isRejectedTableOpen = !this.isRejectedTableOpen;
  }
}
