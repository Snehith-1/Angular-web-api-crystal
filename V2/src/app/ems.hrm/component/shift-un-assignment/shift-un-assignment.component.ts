import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES,enc } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { SelectionModel } from '@angular/cdk/collections';
import { NgxSpinnerService } from 'ngx-spinner';

export class IShiftunassign {
  UnAssign_list: string[] = [];
  shifttype_gid: any;
  shift_type:any;
}

@Component({
  selector: 'app-shift-un-assignment',
  templateUrl: './shift-un-assignment.component.html',
  styleUrls: ['./shift-un-assignment.component.scss']
})
export class ShiftUnAssignmentComponent {
  ShiftTypeManagement!: IShiftunassign;
  responsedata: any;
  Shift_unassign: any[] = [];
  Shifttime_list: any[] = [];
  shifttype_gid:any;
  parameterValue: any;
  reactiveFormadd!: FormGroup;
  selection = new SelectionModel<IShiftunassign>(true, []);
  CurObj: IShiftunassign = new IShiftunassign();
  pick: Array<any> = [];
  shift_name:any;
  Assign_type: any;

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService,public NgxSpinnerService:NgxSpinnerService, public service: SocketService) {
    this.ShiftTypeManagement = {} as IShiftunassign;
    this.reactiveFormadd = new FormGroup({
      branch_name :new FormControl(''),
      shift_type :new FormControl(''),     
  });
    }
   
    ngOnInit(): void {
      const shifttype_gid = this.route.snapshot.paramMap.get('shifttype_gid');
  this.shifttype_gid = shifttype_gid;
  const secretKey = 'storyboarderp';
  const deencryptedParam = AES.decrypt(this.shifttype_gid, secretKey).toString(enc.Utf8);
  this.shifttype_gid = deencryptedParam;    

       //// Summary Grid//////
   var url = 'ShiftType/GetShiftUnAssignsumary'
      
   this.service.get(url).subscribe((result: any) => {
   this.responsedata = result;
   this.Shift_unassign = this.responsedata.UnAssign_list;
     setTimeout(() => {
       $('#Shift_unassign').DataTable();
       }, );
 });
 
 var url = 'ShiftType/Shifttypeassign'
 let param={
  shifttype_gid:this.shifttype_gid
 }
 this.service.getparams(url,param).subscribe((result: any) => {
   this.responsedata = result;   
   this.Assign_type = this.responsedata.Assign_type;
   this.shift_name = this.Assign_type[0].shifttype_name;
 });
  }
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.Shift_unassign.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.Shift_unassign.forEach((row: IShiftunassign) => this.selection.select(row));
  }

  onsubmit(){
    this.pick = this.selection.selected
    this.CurObj.UnAssign_list = this.pick
    this.CurObj.shifttype_gid=this.shifttype_gid
    this.CurObj.shift_type = this.reactiveFormadd.value.shift_type
    if ( this.CurObj.UnAssign_list.length === 0) {
      this.ToastrService.warning("Select Atleast one Shift Assign to Added");
      return;
    } 
  
    var url = 'ShiftType/ShiftUnAssignSubmit'
    this.NgxSpinnerService.show();
      this.service.post(url, this.CurObj).subscribe((result: any) => {
        this.NgxSpinnerService.hide();
        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.router.navigate(['/hrm/HrmMstShiftTypeSummary']);
           
       }
       else{
        this.ToastrService.success(result.message)
        this.router.navigate(['/hrm/HrmMstShiftTypeSummary']);        
       }  
       window.location.reload();

      });  
  }
 
}
