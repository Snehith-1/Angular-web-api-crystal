import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

interface IEntity {
  entity_name: string;
  entity_description: string;
  entity_gid: string;
  entityedit_name: string;
}

@Component({
  selector: 'app-sys-mst-entity-summary',
  templateUrl: './sys-mst-entity-summary.component.html',
  styleUrls: ['./sys-mst-entity-summary.component.scss']
})

export class SysMstEntitySummaryComponent {
  reactiveForm!: FormGroup;
  reactiveFormEdit!: FormGroup;
  responsedata: any;
  parameterValue: any;

  parameterValue1: any;
  entity_list: any[] = [];
  entity!: IEntity;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {
    this.entity = {} as IEntity;
  }

  ngOnInit(): void {
    this.GetEntitySummary();
    // Form values for Add popup //
    this.reactiveForm = new FormGroup({
      entity_name: new FormControl(this.entity.entity_name, [Validators.required,]),
      entity_description: new FormControl(''),
    });

    // Form values for Edit popup //
    this.reactiveFormEdit = new FormGroup({
      entityedit_name: new FormControl(this.entity.entityedit_name, [Validators.required,]),
      entityedit_description: new FormControl(''),
      entity_gid: new FormControl(''),
    });
  }

  // Summary Grid //
  GetEntitySummary() {
    var url = 'Entitylist/GetEntitySummary'
    this.service.get(url).subscribe((result: any) => {

      this.responsedata = result;
      this.entity_list = this.responsedata.entity_lists;
      setTimeout(() => {
        $('#entity_list').DataTable();
      }, 1);
    });
  }

  // Add popup validtion //
  get entity_name() {
    return this.reactiveForm.get('entity_name')!;
  }

  // Edit popup validtion //
  get entityedit_name() {
    return this.reactiveFormEdit.get('entityedit_name')!;
  }

  // Add popup //
  public onsubmit(): void {
    if (this.reactiveForm.value.entity_name != null && this.reactiveForm.value.entity_name != '') {

      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      this.reactiveForm.value;
      var url = 'Entitylist/PostEntity'
      this.service.post(url, this.reactiveForm.value).subscribe((result: any) => {
        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.GetEntitySummary();
        }
        else {
          this.reactiveForm.get("entity_name")?.setValue(null);
          this.reactiveForm.get("entity_description")?.setValue(null);
          this.ToastrService.success(result.message)
          this.GetEntitySummary();
          window.location.reload();
        }
      });
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }
  
  // Edit popup //
  openModaledit(parameter: string) {
    this.parameterValue1 = parameter
    this.reactiveFormEdit.get("entityedit_name")?.setValue(this.parameterValue1.entity_name);
    this.reactiveFormEdit.get("entityedit_description")?.setValue(this.parameterValue1.entity_description);
    this.reactiveFormEdit.get("entity_gid")?.setValue(this.parameterValue1.entity_gid);
  }

  // Update popup //
  public onupdate(): void {
    if (this.reactiveFormEdit.value.entityedit_name != null && this.reactiveFormEdit.value.entityedit_name != '') {
      for (const control of Object.keys(this.reactiveFormEdit.controls)) {
        this.reactiveFormEdit.controls[control].markAsTouched();
      }
      this.reactiveFormEdit.value;

      var url = 'Entitylist/Getupdateentitydetails'
      this.service.post(url, this.reactiveFormEdit.value).pipe().subscribe((result: any) => {
        this.responsedata = result;
        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.GetEntitySummary();
        }
        else {
          this.ToastrService.success(result.message)
          this.GetEntitySummary();
        }
      });
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }

  // Delete popup //
  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  }

  ondelete() {
    console.log(this.parameterValue);
    var url = 'Entitylist/Getdeleteentitydetails'
    let param = {
      entity_gid: this.parameterValue
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.ToastrService.success(result.message)
      }
      this.GetEntitySummary();
    });
  }
  onclose() {
    window.location.reload();
  }
}
