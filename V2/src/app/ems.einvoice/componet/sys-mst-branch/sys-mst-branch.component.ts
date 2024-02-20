import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
interface IBranch {
  GST_no: any;
  Email_address: any;
  Postal_code: any;
  Phone_no: any;
  branch_gid:string;
  branch_code: string;
  branch_name: string;
  branch_prefix: string;
  branchmanager_gid: string; 
  branch_code_edit: string; 
  branch_name_edit: string; 
  City:string;
  State:string;
  
  Branch_address:string;
  branch_prefix_edit: string; 

}
@Component({
  selector: 'app-sys-mst-branch',
  templateUrl: './sys-mst-branch.component.html',
  styleUrls: ['./sys-mst-branch.component.scss']
})
export class SysMstBranchComponent {
  reactiveForm!: FormGroup;
  reactiveFormEdit!: FormGroup;
  reactiveFormadd!: FormGroup;  
  responsedata: any;
  parameterValue: any; 
  parameterValue1: any;
  branch_list: any[] = []
  branch!: IBranch;
  route: any;
  ToastrService: any;
  file: any;
  branchgid1:any;
  parameterValue2: any;

  constructor(private formBuilder: FormBuilder,  public service: SocketService) {
    this.branch = {} as IBranch;

}
ngOnInit(): void {
    this.BranchSummary();
    // Form values for Add popup/////
    this.reactiveForm = new FormGroup({
      

 branch_code: new FormControl(this.branch.branch_code, [ Validators.required,]),
 branch_name: new FormControl(this.branch.branch_name, [ Validators.required,   ]),
 branch_prefix: new FormControl(this.branch.branch_prefix, [ Validators.required,   ]),
//  branchmanager_gid: new FormControl(this.branch.branchmanager_gid, [ Validators.required,   ]),
 });
 this.reactiveFormadd = new FormGroup({    

  branch_code: new FormControl(''),
  Branch_address: new FormControl(this.branch.Branch_address, [ Validators.required,]),
  City: new FormControl(''),
  State: new FormControl(''),
  Postal_code: new FormControl(''),
  Phone_no: new FormControl(this.branch.Phone_no, [ Validators.required,]),
  Email_address: new FormControl(this.branch.Email_address, [ Validators.pattern(/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/)]),
  GST_no: new FormControl(this.branch.GST_no, [ Validators.pattern(/[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[0-9]{1}[A-Z]{1}[0-9A-Z]{1}$/)]),

 
  });
//  / /Form values for Edit popup/////
    this.reactiveFormEdit = new FormGroup({

      branch_code_edit: new FormControl(this.branch.branch_code_edit, [Validators.required,]),
      branch_name_edit: new FormControl(this.branch.branch_name_edit, [Validators.required,]),
      branch_prefix_edit: new FormControl(this.branch.branch_prefix_edit, [Validators.required, ]),
      branch_gid: new FormControl(''),
  });

  }

  BranchSummary() {
    var url = 'Branch/BranchSummary'
this.service.get(url).subscribe((result: any) => {
 this.responsedata = result;
this.branch_list = this.responsedata.branch_list1;
//console.log(this.branch_list)
setTimeout(() => {
 $('#branch_list').DataTable();
}, 1);
 });
}

 onChange(event: any) {
  this.file = event.target.files[0];
 }
 //Add popup validtion//
 get branch_code() {
return this.reactiveForm.get('branch_code')!;
 }
get branch_name() {
return this.reactiveForm.get('branch_name')!;
 }
 get branch_prefix() {
  return this.reactiveForm.get('branch_prefix')!;
   }
   
  //  get branchmanager_gid() {
  //   return this.reactiveForm.get('branchmanager_gid')!;
  //    }
        
//Edit popup validtion///

get branch_code_edit() {return this.reactiveFormEdit.get('branch_code_edit')!; }       
 get branch_name_edit() {return this.reactiveFormEdit.get('branch_name_edit')!; }       
 get branch_prefix_edit() {return this.reactiveFormEdit.get('branch_prefix_edit')!; }       


////Add popup////

public onsubmit(): void {

  if (this.reactiveForm.value.branch_code != null && this.reactiveForm.value.branch_code != '')
  if (this.reactiveForm.value.branch_name != null && this.reactiveForm.value.branch_name != '')
  if (this.reactiveForm.value.branch_prefix != null && this.reactiveForm.value.branch_prefix != '')

{
  for (const control of Object.keys(this.reactiveForm.controls)) {
 this.reactiveForm.controls[control].markAsTouched();
  }
 this.reactiveForm.value;
 var url1 = 'Branch/PostBranch'

        this.service.postparams(url1, this.reactiveForm.value).subscribe((result: any) => {

            if (result.status == false) {

              // this.ToastrService.warning(result.message)
            this.BranchSummary();
            }

            else {

              this.reactiveForm.get("branch_code")?.setValue(null);
              this.reactiveForm.get("branch_name")?.setValue(null);
              this.reactiveForm.get("branch_prefix")?.setValue(null);
              //  this.ToastrService.success(result.message)

              this.BranchSummary();
            }
          });
        }
      else {
          // this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
        }
  }

//Edit popup////

openModaledit(parameter: string) {
   this.parameterValue1 = parameter
   this.reactiveFormEdit.get("branch_code_edit")?.setValue(this.parameterValue1.branch_code);
   this.reactiveFormEdit.get("branch_name_edit")?.setValue(this.parameterValue1.branch_name);
   this.reactiveFormEdit.get("branch_prefix_edit")?.setValue(this.parameterValue1.branch_prefix);
   this.reactiveFormEdit.get("branch_gid")?.setValue(this.parameterValue1.branch_gid);   
}
public onupdate(): void {
  if (this.reactiveFormEdit.value.branch_name_edit != null && this.reactiveFormEdit.value.branch_name_edit != '') {
    for (const control of Object.keys(this.reactiveFormEdit.controls)) {
      this.reactiveFormEdit.controls[control].markAsTouched();
    }
    this.reactiveFormEdit.value;

    //console.log(this.reactiveFormEdit.value)
    var url = 'Branch/getUpdatedBranch'

    this.service.postparams(url,this.reactiveFormEdit.value).pipe().subscribe(result=>{
      this.responsedata=result;
      if(result.status ==false){
        // this.ToastrService.warning(result.message)
        this.BranchSummary();
      }
      else{
        // this.ToastrService.success(result.message)
        this.BranchSummary();
      }     
  }); 

  }
  else {
    // this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
  }
}
myModaladddetails(parameter: string) {
  this.parameterValue2 = parameter
  this.reactiveFormadd.get("branch_code")?.setValue(this.parameterValue2.branch_code);

}
public validate(): void {
  console.log(this.reactiveFormadd.value)
  
     this.branch = this.reactiveFormadd.value;
    // this.service.Profileupload(this.reactiveForm.value).subscribe(result => {  
    //   this.responsedata=result;
    // });   
   
      let formData = new FormData();
      if (this.file != null && this.file != undefined) {      
      formData.append("file", this.file,this.file.name);
      formData.append("branchgid", this.branchgid1);
      formData.append("branch_code", this.branch.branch_code);
      formData.append("Branch_address", this.branch.Branch_address);
      formData.append("City", this.branch.City);
      formData.append("State", this.branch.State);
      formData.append("Postal_code", this.branch.Postal_code);
      formData.append("Email_address", this.branch.Email_address);
      formData.append("Phone_no", this.branch.Phone_no);
      formData.append("GST_no", this.branch.GST_no);     
     
      var api='Branch/Updatedbranchlogo'
      //console.log(this.file)
        this.service.postfile(api,formData,).subscribe((result:any) => {

          if(result.status ==true){
            this.ToastrService.warning(result.message)
          }
          else{
            this.route.navigate(['/einvoice/Dashboard']);
            this.ToastrService.success(result.message)
          }
          this.responsedata=result;        
        });
    
    }
    else{
      var api='Branch/BranchSummarydetail'
      //console.log(this.file)
        this.service.postparams(api,this.branch).subscribe((result:any) => {

          if(result.status ==true){
            // this.ToastrService.warning(result.message)
          }
          else{
            this.route.navigate(['/einvoice/Dashboard']);
            // this.ToastrService.success(result.message)
          }
          this.responsedata=result;
        });
    }
    
      // this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')

    // console.info('Name:', this.employee);
    return; 
}  
  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  }  

  ondelete() {
    console.log(this.parameterValue);
    var url3 = 'Branch/DeleteBranch'
    this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
      if (result.status == false) {
        // this.ToastrService.warning(result.message)
      }
      else {
        // this.ToastrService.success(result.message)
      }
      this.BranchSummary();

    });
  }
  onclose() {
   this.reactiveForm.reset();

  };
}
