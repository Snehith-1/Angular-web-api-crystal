import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
interface IEmployee {
  password: string;
  confirmpassword: string;
  showPassword: boolean;
  employee_gid:string;
  user_code:string;
  confirmusercode:string;
  asset_gid :string;
 
}
@Component({
  selector: 'app-hrm-trn-addassetcustodian',
  templateUrl: './hrm-trn-addassetcustodian.component.html',
  styleUrls: ['./hrm-trn-addassetcustodian.component.scss']
})
export class HrmTrnAddassetcustodianComponent  {
  employeegid: any;
  file!:File;
  parameterValue1: any;
  user_code:any;
  user_name:any;
  custodianadd_list: any[] = [];
  getuserdtl1: any[] = [];
  Document_list: any[] = [];
  employee!: IEmployee;
  reactiveForm!: FormGroup;
  reactiveuploadForm!: FormGroup;
  employeegid1:any;
  responsedata: any;
  selectedBranch: any = null;
  selectedDepartment: any = null;
  Assetgid: any
  Employeegid: any
  file_path:any;
  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService, private ToastrService: ToastrService, private route: Router, private router: ActivatedRoute,private formBuilder: FormBuilder) {
    this.employee = {} as IEmployee;
  }

  ngOnInit(): void {
    const employee_gid = this.router.snapshot.paramMap.get('employee_gid');
    this.employeegid = employee_gid
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.employeegid, secretKey).toString(enc.Utf8);
    this.employeegid1 =deencryptedParam;
    console.log(deencryptedParam)
    this.GetAddcustodianSummary(deencryptedParam)
    this.getuserdtl(deencryptedParam)
    this.reactiveForm = new FormGroup({
      user_name: new FormControl(''),
      user_code: new FormControl(''),
      asset_id: new FormControl(''),
      asset_gid: new FormControl(''),
      asset_name: new FormControl(''),
      assetref_no: new FormControl(''),
      custodian_date: new FormControl(''),
      custodian_enddate: new FormControl(''),
      remarks: new FormControl(''),
      employee_gid: new FormControl(''),
      asset_document: new FormControl(''),
      custodianadd_list: this.formBuilder.array([])

    }
    );
  }
  GetAddcustodianSummary(employee_gid: any) {
     var url = 'HrmTrnAssetcustodian/GetAddassetcustodian'
    let param = {
      employee_gid : employee_gid 
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.custodianadd_list = result.GetAddCustodian;
      debugger;
    for (let i = 0; i < this.custodianadd_list.length; i++) {
      this.reactiveForm.addControl(`asset_id_${i}`, new FormControl(this.custodianadd_list[i].asset_id));
      this.reactiveForm.addControl(`custodian_date_${i}`, new FormControl(this.custodianadd_list[i].custodian_date));
      this.reactiveForm.addControl(`custodian_enddate_${i}`, new FormControl(this.custodianadd_list[i].custodian_enddate));
      this.reactiveForm.addControl(`remarks_${i}`, new FormControl(this.custodianadd_list[i].remarks));
    }
    });
  }
  getuserdtl(employee_gid: any) {
    var url = 'HrmTrnAssetcustodian/Getusetdtl'
   let param = {
     employee_gid : employee_gid 
   }
   this.service.getparams(url, param).subscribe((result: any) => {
     this.getuserdtl1 = result.GetAddCustodian;
     debugger;
   });
 }
 submit() {
  for (const control of Object.keys(this.reactiveForm.controls)) {
    this.reactiveForm.controls[control].markAsTouched();
  }
  let flag = 0; 
  // Loop through each row in custodianadd_list
  for (let i = 0; i < this.custodianadd_list.length; i++) {
    const row = this.custodianadd_list.at(i);
     const employee_gid = this.employeegid1;
     const formData = {
      flag: i === 0 ? 1 : 0, 
      employee_gid: employee_gid,
      asset_gid: row.asset_gid,
      asset_name: row.asset_name,
      assetref_no: row.assetref_no,
      asset_id: row.asset_id,
      custodian_date: row.custodian_date,
      custodian_enddate:row.custodian_enddate,
      remarks: row.remarks,
    };

    var url1 = 'HrmTrnAssetcustodian/Postcusdotiandtl';
    this.service.post(url1,formData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning("Error While Adding Asset Custodian");
        this.route.navigate(['/hrm/HrmTrnAssetCustodian']);
      } else {

        this.ToastrService.success(result.message);
        this.route.navigate(['/hrm/HrmTrnAssetCustodian']);
      }
    });
  }
}
onChange2(event:any) {
  this.file =event.target.files[0];
  // var api='HrmTrnAdmincontrol/EmployeeProfileUpload'
  // //console.log(this.file)
  //   this.service.EmployeeProfileUpload(api,this.file).subscribe((result:any) => {
  //     this.responsedata=result;
  //   });
  }
  onclose() {
    this.reactiveuploadForm.reset();
  }
  onupload() {
    console.log(this.reactiveForm.value)
    this.employee = this.reactiveForm.value;  
    if (this.employee.asset_gid != null ) {
      let formData = new FormData();
      if (this.file != null && this.file != undefined) {
        formData.append("file", this.file, this.file.name);
        formData.append("asset_gid", this.employee.asset_gid);
        formData.append("employee_gid",this.employeegid1);
        var api = 'HrmTrnAssetcustodian/UpdateAssetdocument'
        this.service.post(api, formData).subscribe((result: any) => {
          this.responsedata = result;
          if (result.status == false) {
            this.ToastrService.warning(result.message)
          }
          else {
            this.ToastrService.success(result.message)
          }
        });
      }
      this.reactiveForm.reset();
      
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }  
    return;
  } 
  openModaldtl(parameter: string) {
    this.parameterValue1 = parameter
    this.reactiveForm.get("asset_gid")?.setValue(this.parameterValue1.asset_gid);
    this.reactiveForm.get("employee_gid")?.setValue(this.employeegid1);
    console.log(this.reactiveForm)
    const Assetgid = this.reactiveForm.value.asset_gid;
    const Employeegid = this.reactiveForm.value.employee_gid;
    if (this.reactiveForm.value.asset_gid != null && this.reactiveForm.value.employee_gid != '') {
      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      const params = {
       asset_gid: Assetgid,
       employee_gid: Employeegid
      };
    
      var url = 'HrmTrnAssetcustodian/AssetDocument'
      this.service.getparams(url, params).subscribe((result: any) => {
        this.Document_list = result.Assetcustodian;
     debugger;

      
      });
    }

  }
  get asset_gid() {
    return this.reactiveForm.get('asset_gid')!
  }
  get employee_gid() {
    return this.reactiveForm.get('employee_gid')!
  }


  downloadDocument(document_gid: any) {
    var api = 'HrmTrnAssetcustodian/downloadFile';
    let param = { document_gid: document_gid };
  
    this.service.download(api, param).subscribe((result: any) => {
      this.responsedata = result;
      this.file_path = this.responsedata.DownloadFile; // Access the first value in the DownloadFile array
  
      if (!this.file_path || this.file_path.length === 0) {
        window.scrollTo({
          top: 0,
        });
      } else {
        //  const documentPath = this.file_path[0].document_path;
        //  const documentName = documentPath.substring(documentPath.lastIndexOf('/') + 1);
        //  const link = document.createElement('a');
        //  link.href = this.file_path[0].document_path;
        //  link.download = documentName;
        //  link.click();



        const documentPath = this.file_path[0].document_path;
        const documentName = documentPath.substring(documentPath.lastIndexOf('/') + 1);
  
        // Construct a blob URL
        const blob = new Blob([this.file_path], { type: 'application/octet-stream' });
        const url = window.URL.createObjectURL(blob);
  
        // Create a temporary link element and trigger the download
        const link = document.createElement('a');
        link.href = url;
        link.download = documentName;
        link.click();
  
        // Clean up the object URL after the download is initiated
        window.URL.revokeObjectURL(url);

        this.GetAddcustodianSummary(this.employeegid1);
      }
  });
  }

  
}