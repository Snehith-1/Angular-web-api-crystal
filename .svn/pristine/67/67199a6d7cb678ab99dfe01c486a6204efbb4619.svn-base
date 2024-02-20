import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
interface IShiftType{

}
@Component({
  selector: 'app-hrm-mst-shifttype',
  templateUrl: './hrm-mst-shifttype.component.html',
  styleUrls: ['./hrm-mst-shifttype.component.scss']
})
export class HrmMstShifttypeComponent {
  ShiftTypeManagement!: IShiftType;
  responsedata: any;
  shift_list: any[] = [];
  Shifttime_list: any[] = [];
  parameterValue: any;


  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.ShiftTypeManagement = {} as IShiftType;
    }
   
    ngOnInit(): void {
    

       //// Summary Grid//////
   var url = 'ShiftType/GetShiftSummary'
      
   this.service.get(url).subscribe((result: any) => {
   this.responsedata = result;
   this.shift_list = this.responsedata.shift_list;
     setTimeout(() => {
       $('#shift_list').DataTable();
       }, );
 });
  }
  Shifttypeadd(){
    this.router.navigate(['/hrm/HrmMstAddShiftType'])
  }

  ShiftTimepopup(data: any): void {
    debugger;
    var api1 = 'ShiftType/GetshiftTimepopup';
 
    
    let params = {
      shifttype_gid: data.shifttype_gid,
    };
 
    this.service.getparams(api1, params).subscribe((result: any) => {
        this.responsedata = result;
        this.Shifttime_list = this.responsedata.Time_list;
    });
  }
  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  }

  ondelete() {
    console.log(this.parameterValue);
    var url3 = 'ShiftType/DeleteShift'
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
  openModalactive(parameter: string){
    this.parameterValue = parameter
  }
  onactive(){
    console.log(this.parameterValue);
      var url3 = 'ShiftType/GetshiftActive'
      this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
  
        if ( result.status == false) {
         this.ToastrService.warning('Error While Shift Type Activated')
        }
        else {
         this.ToastrService.success('Shift Type Activated Successfully')
          }
        window.location.reload();
      });
  }

  openModalinactive(parameter: string){
    this.parameterValue = parameter
  }
  oninactive(){
    console.log(this.parameterValue);
      var url3 = 'ShiftType/GetshiftInActive'
      this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
  
        if ( result.status == false) {
         this.ToastrService.warning('Error While Shift Type Inactivated')
        }
        else {
         this.ToastrService.success('Shift Type Inactivated Successfully')
          }
        window.location.reload();
      });
  }
}
