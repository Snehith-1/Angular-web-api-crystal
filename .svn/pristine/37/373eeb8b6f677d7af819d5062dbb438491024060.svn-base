import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-hrm-member-myleave',
  templateUrl: './hrm-member-myleave.component.html',
  styleUrls: ['./hrm-member-myleave.component.scss']
})
export class HrmMemberMyleaveComponent {

  radioSelected: any;
  leaveperiod: any;  
  responsedata: any;
  leave_details: any;
  leavetypelist: any;
  leavetype_list:any[] = [];
  reactiveFormadd:FormGroup;
  count_Sickavailable: any;
  count_Sickleavetaken: any;
  count_Casualleavetaken:any;
  count_casualavailable:any;
  parameterValue:any;
  leavesummary:any;
  NgxSpinnerService: any;
  leave: any;
  file: any;
  data:any;
  SocketService: any;
  leave_datefrom:any;
  parameterValue2:any;

  constructor ( private fb: FormBuilder, private route: ActivatedRoute,private router: Router, private service: SocketService, private ToastrService: ToastrService ) {
    this.reactiveFormadd = new FormGroup ({
      leave_to : new FormControl(''),
      leave_from : new FormControl(''),
      leave_days : new FormControl(''),
      leavetype_name : new FormControl(''),
      leave_reason : new FormControl(''),
      leave_gid:new FormControl(''),
      leavetype_gid:new FormControl(''),

      
      
    })
  }
  
  ngOnInit(): void {
      var api = 'applyLeave/getleavetype_name';
      this.service.get(api).subscribe((result: any) => {
        this.responsedata = result;
        this.leavetypelist = this.responsedata.leave_type_list;
      });

    var api = 'applyLeave/leavesummary';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.leave_details = this.responsedata.leave_list;
      setTimeout(()=>{  
        $('#leave_details').DataTable();
      }, 1);
    });

    var api = 'applyLeave/leavetype';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.leavetype_list =  this.responsedata.leavetype_list;
      this.count_Sickleavetaken = this.leavetype_list[1].count_leavetaken;
      this.count_Sickavailable = this.leavetype_list[1].count_leaveavailable;
      this.count_casualavailable = this.leavetype_list[2].count_leaveavailable;
      this.count_Casualleavetaken = this.leavetype_list[2].count_leavetaken;

    })
         
  }
  myModaladd(parameter: string){
    this.parameterValue2 = parameter

    this.reactiveFormadd.get("leave_gid")?.setValue(this.parameterValue2.leave_gid);
  }

  mymodaladd(parameter: string){
    this.parameterValue2 = parameter

    this.reactiveFormadd.get("leave_gid")?.setValue(this.parameterValue2.leave_gid);
  }

  onapplyleave() {
    debugger;
    var url = 'applyLeave/applyleavesubmit';
    this.service.post(url, this.reactiveFormadd.value).subscribe((result: any) => {
      if (result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }


  // validate(): void {

  //   console.log(this.reactiveFormadd.value)    
  //      this.leave = this.reactiveFormadd.value;
  //      if(this.leave.leave_datefrom !=null && this.leave.leave_dateto !=null  && this.leave.leave_days !=null  && this.leave.leavetype_name !=null   && this.leave.leave_reason   ){

     
  //       let formData = new FormData();
  //       if (this.file != null && this.file != undefined) {      
  //       formData.append("file", this.file,this.file.name);
  //       formData.append("leave_gid", this.leave.leave_gid);
  //       formData.append("leave_datefrom", this.leave.leave_datefrom);
  //       formData.append("leave_dateto", this.leave.leave_dateto);
  //       formData.append("leave_days", this.leave.leave_days);
  //       formData.append("leavetype_name", this.leave.leavetype_name);
  //       formData.append("leave_reason", this.leave.leave_reason);   
          
       
  //       var api='applyLeave/applyleavesubmit'
  //         this.service.postfile(api,formData).subscribe((result:any) => {
  
  //           if(result.status ==true){
  //             this.ToastrService.warning(result.message)
  //           }
  //           else{
  //             this.ToastrService.success(result.message)
  //           }
  //           this.responsedata=result;        
  //         });
      
  //     }
     
  //     }
      
  //     return; 
  // } 

  delete(leave_gid: any) {
    this.parameterValue = leave_gid
  }


  ondelete() {
    this.NgxSpinnerService.show();
    var params = {
      leave_gid: this.parameterValue
    }
    console.log(this.parameterValue);
    var url3 = 'applyLeave/leavePendingDelete'
    this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
      if (result.status == false) {
         this.ToastrService.warning(result.message)
      }
      else {
         this.ToastrService.success("Delete successfully")
      }
      this.leavesummary();
      window.location.reload();


    });
  }

  leavePeriod() {
    this.leaveperiod = this.radioSelected;
  }

  back() {
    this.router.navigate(['/hrm/HrmMemberDashboard'])
  }

}
