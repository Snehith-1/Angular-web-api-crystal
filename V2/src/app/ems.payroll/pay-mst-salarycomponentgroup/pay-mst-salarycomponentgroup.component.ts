import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
interface IComponenetGroup {
  componentgroup_gid: string;
  componentgroup_code: string;
  componentgroup_name: string;
  display_name: string;
  statutory: string;

}
@Component({
  selector: 'app-pay-mst-salarycomponentgroup',
  templateUrl: './pay-mst-salarycomponentgroup.component.html',
  styleUrls: ['./pay-mst-salarycomponentgroup.component.scss']
})
export class PayMstSalarycomponentgroupComponent {
  reactiveForm!: FormGroup;
  reactiveFormEdit!: FormGroup;
  reactiveFormadd!: FormGroup;
  responsedata: any;
  parameterValue: any;
  parameterValue1: any;
  Componentgroup_list: any[] = []
  componentgroupmaster!: IComponenetGroup;


  

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {
    this.componentgroupmaster = {} as IComponenetGroup;
  }

  ngOnInit(): void {
    this.ComponentgroupSummary();
    this.reactiveFormadd = new FormGroup({
      componentgroup_gid: new FormControl(''),
      componentgroup_code: new FormControl(this.componentgroupmaster.componentgroup_code, [Validators.required,]),
      componentgroup_name: new FormControl(this.componentgroupmaster.componentgroup_name, [Validators.required,]),
      display_name: new FormControl(this.componentgroupmaster.display_name, [Validators.required,]),
      statutory: new FormControl(this.componentgroupmaster.statutory, [Validators.required,])
    });
  }

  ComponentgroupSummary() {
    var url = 'PayMstComponentgroup/GetComponentgroupSummary'
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
      this.Componentgroup_list = this.responsedata.Componentgroup_list;

      setTimeout(() => {
        $('#Componentgroup_list').DataTable();
      }, 1);
    });  
  }

  get componentgroup_gid() {
    return this.reactiveFormadd.get('componentgroup_gid')!;
  }
  get componentgroup_code() {
    return this.reactiveFormadd.get('componentgroup_code')!;
  }
  get componentgroup_name() {
    return this.reactiveFormadd.get('componentgroup_name')!;
  }
  get display_name() {
    return this.reactiveFormadd.get('display_name')!;
  }
  get statutory() {
    return this.reactiveFormadd.get('statutory')!;
  }

  onRadioChange(value: string): void {
    debugger;
    this.reactiveFormadd.get('statutory')?.setValue(value);
  }

  openModaledit(parameter: string) {
    debugger;
    this.parameterValue1 = parameter
    this.reactiveFormadd.get("componentgroup_gid")?.setValue(this.parameterValue1.componentgroup_gid);
    this.reactiveFormadd.get("componentgroup_code")?.setValue(this.parameterValue1.componentgroup_code);
    this.reactiveFormadd.get("componentgroup_name")?.setValue(this.parameterValue1.componentgroup_name);
    this.reactiveFormadd.get("display_name")?.setValue(this.parameterValue1.display_name);
    this.reactiveFormadd.get("statutory")?.setValue(this.parameterValue1.statutory);

  }
  openModaldelete(parameter: string) {}

  public onsubmit(): void {
    if (this.reactiveFormadd.value.componentgroup_code != null && this.reactiveFormadd.value.componentgroup_code != '')
      if (this.reactiveFormadd.value.componentgroup_name != null && this.reactiveFormadd.value.componentgroup_name != '')
        if (this.reactiveFormadd.value.display_name != null && this.reactiveFormadd.value.display_name != '') {
          for (const control of Object.keys(this.reactiveFormadd.controls)) {
            this.reactiveFormadd.controls[control].markAsTouched();
          }
          debugger;
          this.reactiveFormadd.value;
          var url1 = 'PayMstComponentgroup/Postcomponentgroup'

          this.service.postparams(url1, this.reactiveFormadd.value).subscribe((result: any) => {
            debugger;
            if (result.status == false) {
              this.ToastrService.warning(result.message)
              this.ComponentgroupSummary();
            }
            else {
              this.reactiveFormadd.get("componentgroup_code")?.setValue(null);
              this.reactiveFormadd.get("componentgroup_name")?.setValue(null);
              this.reactiveFormadd.get("display_name")?.setValue(null);
              this.reactiveFormadd.get("statutory")?.setValue(null);
              this.ToastrService.success("Salary Component Group Added successfully")
              window.location.reload();
            }
          });
        }
        else {
          this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
        }
  }

  onclose(){
    window.location.reload();
  }
  public onupdate(): void {
    if (this.reactiveFormadd.value.componentgroup_code != null && this.reactiveFormadd.value.componentgroup_code != '') {
      for (const control of Object.keys(this.reactiveFormadd.controls)) {
        this.reactiveFormadd.controls[control].markAsTouched();
      }
      this.reactiveFormadd.value;
      var url = 'PayMstComponentgroup/Updatecomponentgroup'
      this.service.postparams(url, this.reactiveFormadd.value).pipe().subscribe(result => {
        this.responsedata = result;
        if (result.status == false) {
          this.ToastrService.warning(result.message)
          window.location.reload();
        }
        else {
          this.reactiveFormadd.get("componentgroup_code")?.setValue(null);
          this.reactiveFormadd.get("componentgroup_name")?.setValue(null);
          this.reactiveFormadd.get("display_name")?.setValue(null);
          this.reactiveFormadd.get("statutory")?.setValue(null);
          this.ToastrService.success("Update component group successfully")
          this.ComponentgroupSummary();
        }
      });
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }
}
