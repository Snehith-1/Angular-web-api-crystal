import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AES } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
interface IProductUnit {
  conversion_rate: any;
  sequence_level: any;
  productuomclass_name: string;
  productuomclass_code: any;
  productuomclassedit_name: string;
  productuomclassedit_code: any;
  productuomclassedit_name1: string;
  productuomclassedit_code1: string;
  batch_flag:string;
}


@Component({
  selector: 'app-pmr-mst-productunit',
  templateUrl: './pmr-mst-productunit.component.html',
  styleUrls: ['./pmr-mst-productunit.component.scss']
})
export class PmrMstProductunitComponent {
  reactiveFormReset!: FormGroup;
  reactiveForm!: FormGroup;
  reactiveFormadd : FormGroup | any;
  responsedata: any;
  productunit_list: any;
  productunit!: IProductUnit;
  reactiveFormEdit: FormGroup | any;
  parameterValue1: any;
  productunitgrid_list: any[] = [];

  constructor(private formBuilder: FormBuilder,public NgxSpinnerService:NgxSpinnerService, private route: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.productunit = {} as IProductUnit;
    
    this.reactiveFormadd = new FormGroup ({
      conversion_rate:new FormControl (''),
      sequence_level:new FormControl (''),
      productuomclassedit_name1:new FormControl (''),
      productuomclassedit_code1:new FormControl (''),
      batch_flag:new FormControl (''),
      productuomclass_gid:new FormControl(''),
      productuomclass_name:new FormControl(''),
    })

  }


  data: any;
  parameterValue: any;



  onedit() { }

  ngOnInit(): void {
    this.GetProductunitSummary();

    ///form values for add popup///
    this.reactiveForm = new FormGroup({
      productuomclass_name: new FormControl(this.productunit.productuomclass_name, [
        Validators.required,
      ]),
      productuomclass_code: new FormControl(this.productunit.productuomclass_code, [
        Validators.required,
      ]),
    });
    ///form values for edit///
    this.reactiveFormEdit = new FormGroup({
      productuomclassedit_name: new FormControl(this.productunit.productuomclassedit_name, [
        Validators.required,
      ]),
      productuomclassedit_code: new FormControl(this.productunit.productuomclassedit_code, [
        Validators.required,
      ]),
      productuomclass_gid: new FormControl(''),

    });

    this.reactiveFormadd = new FormGroup ({
      conversion_rate:new FormControl (''),
      sequence_level:new FormControl (''),
      productuomclassedit_name1:new FormControl (''),
      productuomclassedit_code1:new FormControl (''),
      batch_flag:new FormControl (''),
      productuomclass_gid:new FormControl(''),
      productuomclass_name:new FormControl(''),
    })
  }




  ////////////Get Summary for product unit//////////////////////
  GetProductunitSummary() {
    this.NgxSpinnerService.show()
    var url = 'PmrMstProductUnit/GetProductunitSummary'
    this.service.get(url).subscribe((result: any) => {
      $('#productunit_list').DataTable().destroy();
      this.responsedata = result;
      this.productunit_list = this.responsedata.productunit_list;
      setTimeout(() => {
        $('#productunit_list').DataTable();
      }, 1);

      this.NgxSpinnerService.hide()
    });
  }

  get productuomclassedit_code1() {
    return this.reactiveFormadd.get('productuomclassedit_code1')!;
  }
  get productuomclassedit_name1() {
    return this.reactiveFormadd.get('productuomclassedit_name1')!;
  }
  get sequence_level() {
    return this.reactiveFormadd.get('sequence_level')!;
  }
  get conversion_rate() {
    return this.reactiveFormadd.get('conversion_rate')!;
  }

  /////////For Add PopUp/////////
  get productuomclass_name() {
    return this.reactiveForm.get('productuomclass_name')!;
  }
  get productuomclass_code() {
    return this.reactiveForm.get('productuomclass_code')!;
  }


  onsubmit() {
    debugger
    if (this.reactiveForm.value.productuomclass_name != null && this.reactiveForm.value.productuomclass_code != null) {

      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      this.reactiveForm.value;
      var url = 'PmrMstProductUnit/PostProductUnit'
      this.NgxSpinnerService.show();
      this.service.post(url, this.reactiveForm.value).subscribe((result: any) => {

        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.GetProductunitSummary();
          this.NgxSpinnerService.hide();
        }
        else {
          this.reactiveForm.get("productuomclass_name")?.setValue(null);
          this.reactiveForm.get("productuomclass_code")?.setValue(null);
          this.ToastrService.success(result.message)
          this.reactiveForm.reset();

          this.GetProductunitSummary();
          this.NgxSpinnerService.hide();

        }

      });

    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
    
  }


  /////EDIT POPUP/////
  get productuomclassedit_code() {
    return this.reactiveFormEdit.get('productuomclassedit_code')!;
  }
  get productuomclassedit_name() {
    return this.reactiveFormEdit.get('productuomclassedit_name')!;
  }

  openModaledit(parameter: string) {
    this.parameterValue1 = parameter
    this.reactiveFormEdit.get("productuomclassedit_code")?.setValue(this.parameterValue1.productuomclass_code);
    this.reactiveFormEdit.get("productuomclassedit_name")?.setValue(this.parameterValue1.productuomclass_name);
    this.reactiveFormEdit.get("productuomclass_gid")?.setValue(this.parameterValue1.productuomclass_gid);

  };

  ////////////Update popup////////
  public onupdate(): void {
    if (this.reactiveFormEdit.value.productuomclassedit_name != null && this.reactiveFormEdit.value.productuomclassedit_code != null) {
      for (const control of Object.keys(this.reactiveFormEdit.controls)) {
        this.reactiveFormEdit.controls[control].markAsTouched();
      }
      this.reactiveFormEdit.value;

      var url = 'PmrMstProductUnit/UpdatedProductunit'
      this.NgxSpinnerService.show();
      this.service.post(url, this.reactiveFormEdit.value).pipe().subscribe((result: any) => {
        this.responsedata = result;
        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.GetProductunitSummary();
          this.NgxSpinnerService.hide();
        }
        else {
          this.ToastrService.success(result.message)
          this.GetProductunitSummary();
          this.NgxSpinnerService.hide();
        }

      });

    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }
  ////////////Delete popup////////
  openModaldelete(parameter: string) {
    this.parameterValue = parameter

  }
  ondelete() {
    console.log(this.parameterValue);
    var url = 'PmrMstProductUnit/deleteProductunitSummary'
    this.NgxSpinnerService.show();
    let param = {
      productuomclass_gid: this.parameterValue
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.ToastrService.success(result.message)
      }
      this.GetProductunitSummary();
      this.NgxSpinnerService.hide();



    });
  }


  ////Expandable Grid////
  ondetail(productuomclass_gid: any) {
    var url = 'PmrMstProductUnit/GetProductUnitSummarygrid'
    let param = {
      productuomclass_gid: productuomclass_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.productunitgrid_list = result.productunitgrid_list;
      console.log(this.productunitgrid_list)
      setTimeout(() => {
        $('#productunitgrid_list').DataTable();
      }, 1);

    });
  }
  onclose(){
    this.reactiveForm.reset();
  }
  productunitclass(productuomclass_gid:any){
    
    var url = 'PmrMstProductUnit/GetProductunits'
    
    let param = {
      productuomclass_gid: productuomclass_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
    this.productunitgrid_list = result.productunitgrid_list;
    this.responsedata=result;
    console.log(this.productunitgrid_list)

  this.reactiveFormadd.get("productuomclass_name")?.setValue(this.productunitgrid_list[0].productuomclass_name);

});
  }

  onSubmitprod() {

    
   
    console.log(this.reactiveFormadd.value)
   var params={
     
      productuomclass_gid:this.reactiveFormadd.value.productuomclass_gid,
      conversion_rate:this.reactiveFormadd.value.conversion_rate,
      productuomclassedit_code1:this.reactiveFormadd.value.productuomclassedit_code1,
      productuomclassedit_name1:this.reactiveFormadd.value.productuomclassedit_name1,
      sequence_level:this.reactiveFormadd.value.sequence_level,
      productuomclass_name:this.reactiveFormadd.value.productuomclass_name,
      batch_flag:this.reactiveFormadd.value.batch_flag
      
  
      
    }
    var url = 'PmrMstProductUnit/PostProductunits'
    this.NgxSpinnerService.show();
      this.service.post(url,params).subscribe((result: any) => {
        if(result.status ==false){
          this.ToastrService.warning(result.message)
        }
        //this.reactiveFormadd.get("productuomclass_name")?.setValue(this.salesproductunitgrid_list[0].productuomclass_name);
        this.NgxSpinnerService.hide();
      });
  
    }
}