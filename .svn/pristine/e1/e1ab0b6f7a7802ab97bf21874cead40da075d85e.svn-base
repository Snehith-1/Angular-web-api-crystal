import { Component, ElementRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute } from '@angular/router';
import { AES, enc } from 'crypto-js';
import {SelectionModel} from '@angular/cdk/collections'; 
interface objInterface { task_gid: string; task_name: string; team_gid: string; team_name: string; 
  hrdocument_gid: string; hrdocument_name: string;  } 

@Component({
  selector: 'app-hrm-trn-employeeonboard-edit',
  templateUrl: './hrm-trn-employeeonboard-edit.component.html',
  styleUrls: ['./hrm-trn-employeeonboard-edit.component.scss']
})
export class HrmTrnEmployeeonboardEditComponent {
  selection = new SelectionModel<objInterface>(true, []);  
  AutoIDkey: any;
  formDataObject: FormData = new FormData();
  file_name : any;
  confirmPasswordTouched = false;
  updateresult: any;
  EmpEditForm : FormGroup | any;
  TaskInitiationForm : FormGroup | any;
  HRDocumentForm : FormGroup | any;
  cboeditentity! : {
    "entity_name": string,
    "entity_gid": string,
  };
  cboeditbranch : any;
  cboeditfunction: any;
  cboeditsubfunction: any;
  cboeditdesignation: any;
  cboeditrole: any;
  txteditemployee_user_code: any;
  txteditemployee_joining_date: any;
  cboeditreporting: any;
  cboeditbaselocation: any;
  txteditofficialemail_address: string = '';
  txteditofficialmobile_number: any;
  txteditfirst_name: any;
  txteditlast_name: string = '';
  rdbeditgender: string = 'Male';
  cboeditblood_group: any;
  cboeditmartial_status: any;
  txteditpersonal_email_address: string = '';
  txteditpersonal_phone_number: string = '';  
  txteditpermanent_address_line_one : string = '';
  txteditpermanent_address_line_two: string = '';
  cboeditpermanent_country: any;
  txteditpermanent_state: string = '';
  txteditpermanent_city: string = '';
  txteditpermanent_postal_code: string = '';
  txtedittemporary_address_line_one: string = '';
  txtedittemporary_address_line_two: string = '';
  cboedittemporary_country: any;
  txtedittemporary_state: string = '';
  txtedittemporary_city: string = '';
  txtedittemporary_postal_code: string = '';
  OnboardingTaskList: any[] = [];
  // txtTaskRemarks: any;
  // task_list: any;
  // hrdocument_list : any;
  // OnboardingTaskList: any[] = [];
  // hrdocumentData_list: any[] = [];
  // file_name : any;
  //  AutoIDkey: any;
  // TaskInitiationFormdata = {
  //   cboTask: null,
  //   cboteam: null,
  //   txtTaskRemarks: '',
  // };
  HRDocFormdata = {
    cboHRDocument: null 
  };
   team_list : any; 
  // formDataObject: FormData = new FormData();
  
    MartialStatus = [
      { MartialStatus: 'Married', MartialStatus_gid: 'Msts_001'},
      { MartialStatus: 'UnMarried',MartialStatus_gid: 'Msts_002' },
      { MartialStatus: 'Single',MartialStatus_gid: 'Msts_003'},
    ];
    entityList: any;
    branchList: any;
    departmentList: any;
    designationList: any;
    countryList: any;
    subfunction_list: any;
    location_list: any;
    roleList: any;
    reportingtoList: any;
    bloodgroup_list: any;
    maritalstatus_data: any;
    employee_gid: any;
    lstab: any;
    employee_details: any;
    txtemployeeacess: any;
    hrdocumentData_list: any[] = [];
    hrdocument_list: any;
    task_list: any;  
    param: any;
    values: any;  
    HRDocumentList: any;
  
  
    constructor(private route: ActivatedRoute,private NgxSpinnerService: NgxSpinnerService,private ToastrService: ToastrService,private SocketService: SocketService,public router:Router,private FormBuilder: FormBuilder) {
      this.Sample();     
    }
  
    Sample(){
      this.EmpEditForm = new FormGroup ({
        txteditemployee_user_code : new FormControl(null,
        [
          Validators.required,
          Validators.pattern(/^(?!\s*$).+/),
        ]),
        txtuser_password : new FormControl(null,
          [
            Validators.required,
            Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
          ]),
        txtconfrim_user_password: new FormControl(null,Validators.required),
        txteditofficialmobile_number: new FormControl(null,
          [
            Validators.required,
            Validators.pattern(/^[0-9]+$/),
            Validators.minLength(10),   
          ]),
        txteditpersonal_phone_number: new FormControl(null,
            [ 
              Validators.pattern(/^[0-9]+$/),
              Validators.minLength(10),
            ]),
        txteditofficialemail_address: new FormControl(null,[
          Validators.pattern('^([a-z0-9]+(?:[-.][a-z0-9]+)*)@([a-z0-9]+\\.[a-z]{2,20}(\\.[a-z]{2})?|\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\]|localhost)$')
          ]),
          txteditpersonal_email_address: new FormControl(null,[
            Validators.pattern('^([a-z0-9]+(?:[-.][a-z0-9]+)*)@([a-z0-9]+\\.[a-z]{2,20}(\\.[a-z]{2})?|\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\]|localhost)$')
          ]),
        txteditfirst_name: new FormControl(null,[
          Validators.required,
          Validators.pattern(/^(?!\s*$).+/),
        ]),
        txteditemployee_joining_date: new FormControl(null,Validators.required),
        cboeditbranch : new FormControl(null,Validators.required),
        cboeditfunction: new FormControl(null,Validators.required),
        cboeditsubfunction: new FormControl(null,Validators.required),
        cboeditdesignation: new FormControl(null,Validators.required),
        cboeditrole: new FormControl(null,Validators.required),
        cboeditreporting: new FormControl(null,Validators.required),
        cboeditbaselocation: new FormControl(null,Validators.required),
        txteditpermanent_postal_code: new FormControl(null,
          [ 
            Validators.pattern(/^[0-9]+$/),
            Validators.minLength(6), 
          ]),
          txtedittemporary_postal_code: new FormControl(null,
            [ 
              Validators.pattern(/^[0-9]+$/),
              Validators.minLength(6),
            ]),
            txtedittemporary_address_line_one : new FormControl(null),
            txtedittemporary_address_line_two: new FormControl(null),
            cboedittemporary_country: new FormControl(null),
            txtedittemporary_state: new FormControl(null),
            txtedittemporary_city: new FormControl(null),
            txteditpermanent_address_line_one: new FormControl(null),
            txteditpermanent_address_line_two: new FormControl(null),
            cboeditpermanent_country: new FormControl(null),
            txteditpermanent_state: new FormControl(null),
            txteditpermanent_city: new FormControl(null),
            txteditlast_name: new FormControl(null),
            cboeditentity: new FormControl(null),
            rdbemployee_access:new FormControl(null),
            rdbeditgender:new FormControl(null),
            cboeditmartial_status:new FormControl(null),
            cboeditblood_group:new FormControl(null),
            HRDocumentForm : this.FormBuilder.group({
              cboHRDocument: ['', Validators.required],
              fileInput: ['', Validators.required], 
            })
      })
    }
  
    
    ngOnInit(): void { 
  
      // this.route.queryParams.subscribe(params => {
      //   this.param= params['hash'];
      // })
      // console.log(this.param)
      // var replace = ' '
      // var str = this.param
      // var check = str.replace(new RegExp(replace, 'g'), "+")
      // const secretKey = 'storyboarderp';
      // const deencryptedParam = AES.decrypt(this.param, secretKey).toString(enc.Utf8);
      // console.log(deencryptedParam);
      // this.values = deencryptedParam.split('&')
      // console.log(this.values);
      // this.employee_gid = this.values[0]
      // this.lstab = this.values[1]
  
      const options: Options = {
        dateFormat: 'd-m-Y', 
      };
  
      flatpickr('.date-picker', options); 
  
      var url = 'EmployeeOnboard/PopEntity';
      this.SocketService.get(url).subscribe((result: any) => {
        this.entityList  = result.entity;
      });
  
      var url = 'EmployeeOnboard/PopBranch';
      this.SocketService.get(url).subscribe((result: any) => {
        this.branchList  = result.employee;
      });
  
      var url = 'EmployeeOnboard/PopDepartment';
      this.SocketService.get(url).subscribe((result: any) => {
        this.departmentList  = result.employee;
      });
  
      var url = 'EmployeeOnboard/PopDesignation';
      this.SocketService.get(url).subscribe((result: any) => {
        this.designationList  = result.employee;
      });
  
      var url = 'EmployeeOnboard/PopRole';
      this.SocketService.get(url).subscribe((result: any) => {
        this.roleList  = result.rolemaster;
      });
    
      var url = 'EmployeeOnboard/PopCountry';
      this.SocketService.get(url).subscribe((result: any) => {
        this.countryList  = result.country;
      });
  
      var url = 'EmployeeOnboard/PopReportingTo';
      this.SocketService.get(url).subscribe((result: any) => {
        this.reportingtoList  = result.reportingto;
      });
  
      var url = 'EmployeeOnboard/PopSubfunction';
      this.SocketService.get(url).subscribe((result: any) => {
        this.subfunction_list  = result.employee;
      });
  
      var url = 'HrmMaster/GetBaseLocationlist';
      this.SocketService.get(url).subscribe((result: any) => {
        this.location_list  = result.location_list;
      });
  
      var url = 'HrmMaster/GetBloodGroup';
      this.SocketService.get(url).subscribe((result: any) => {
        this.bloodgroup_list  = result.master_list;
      });
  
      var url = 'HRDocument/GetSysHRDocumentDropDown';
      this.SocketService.get(url).subscribe((result: any) => {
        this.hrdocument_list  = result.hrdocument_list;
      });
  
      var url = 'HrmMaster/GetTaskSummary';
      this.SocketService.get(url).subscribe((result: any) => {
        this.task_list  = result.master_list;  
      });
     // Get Task and Team Master Data
     
      var url = 'EmployeeOnboard/GetTeamList';
      this.SocketService.get(url).subscribe((result: any) => {
        this.team_list  = result.teamlist;
      });
  
      // // Get HR Document Master Data
      var url = 'HRDocument/GetSysHRDocumentDropDown';
      this.SocketService.get(url).subscribe((result: any) => {
        this.hrdocument_list  = result.hrdocument_list;
      });
  
      this.route.queryParams.subscribe(params => {
        this.employee_gid = params['employee_gid'];  
        this.lstab = params['lstab']; 
        });
      
        if ( this.lstab== 'pending') {
            var url = 'EmployeeOnboard/EmployeePendingEditView';
        } 
        else if( this.lstab== 'pending') { 
          var url = 'EmployeeOnboard/SysMstTeamMaster';
        } 
        else if( this.lstab== 'pending') {
          var url = 'EmployeeOnboard/EmployeePendingEditView';
        } 
        else {
          var url = 'EmployeeOnboard/EmployeeEditView';
        }
        var params = {
        employee_gid: this.employee_gid,
        }; 
        this.SocketService.getparams(url,params).subscribe((result: any) => { 
          this.employee_details  = result; 
          
          // this.cboeditentity  = (result.company_name == "") ? null : result.company_name
          this.cboeditentity  = { entity_name:result.company_name, entity_gid:result.entity_gid}
          this.cboeditbranch  = (result.branch_gid == "") ? null :result.branch_gid
          this.cboeditfunction = (result.department_gid == "") ? null :result.department_gid
          this.cboeditsubfunction = (result.subfunction_gid == "") ? null :result.subfunction_gid
          this.cboeditdesignation =  (result.designation_gid == "") ? null :result.designation_gid
          this.cboeditrole =  (result.role_gid == "") ? null :result.role_gid
          this.cboeditreporting =  (result.employee_reportingto == "") ? null :result.employee_reportingto
          this.cboeditbaselocation =  (result.baselocation_gid == "") ? null :result.baselocation_gid
          this.txteditemployee_user_code = result.user_code;
          this.txteditemployee_joining_date = result.joining_date
          this.txteditofficialemail_address = result.employee_emailid
          this.txteditofficialmobile_number = result.employee_mobileno
          this.txteditfirst_name = result.user_firstname
          this.txteditlast_name= result.user_lastname
          this.rdbeditgender= result.gender
          this.cboeditblood_group= (result.bloodgroup_name == "") ? null : result.bloodgroup_name
          this.cboeditmartial_status=(result.marital_status == "") ? null :  result.marital_status
          this.txteditpersonal_email_address= result.personal_emailid
          this.txteditpersonal_phone_number= result.personal_phone_no  
          this.txteditpermanent_address_line_one = result.per_address1
          this.txteditpermanent_address_line_two= result.per_address2
          this.cboeditpermanent_country= result.per_country_gid
          this.txteditpermanent_state= result.per_state
          this.txteditpermanent_city= result.per_city
          this.txteditpermanent_postal_code= result.per_postal_code
          this.txtedittemporary_address_line_one= result.temp_address1
          this.txtedittemporary_address_line_two= result.temp_address2
          this.cboedittemporary_country= result.temp_country_gid
          this.txtedittemporary_state= result.temp_state
          this.txtedittemporary_city= result.temp_city
          this.txtedittemporary_postal_code= result.temp_postal_code
          this.txtemployeeacess = result.user_status
        });
  
        var url = 'EmployeeOnboard/GetTaskOnboardView';
        this.SocketService.getparams(url,params).subscribe((result: any) => { 
            this.OnboardingTaskList  = result.MdlTaskViewInfo; 
          });
  
          var url = 'EmployeeOnboard/GetHRDoclist';
    this.SocketService.getparams(url,params).subscribe((result: any) => { 
        this.HRDocumentList  = result.hrdoc; 
     });
       
    }
  
    
    showPassword: boolean = false;
    showConfrimPassword: boolean = false;
  
  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }
  
  toggleConfrimPasswordVisibility(): void {
    this.showConfrimPassword = !this.showConfrimPassword;
  }
  
  onupdate(){ 
    try { 
      if (this.txteditemployee_joining_date.split("-")) 
      this.txteditemployee_joining_date = this.txteditemployee_joining_date.split("-").reverse().join("-"); 
    } 
    catch (e) {  
      this.txteditemployee_joining_date = this.txteditemployee_joining_date 
    }
    var params = {
      employee_gid : this.employee_gid,
      company_name :(this.cboeditentity == undefined) ? "" : this.cboeditentity.entity_name,
      branch_gid :(this.cboeditbranch == undefined) ? "" : this.cboeditbranch,
      department_gid:(this.cboeditfunction == undefined) ? "" : this.cboeditfunction,
      subfunction_gid:(this.cboeditsubfunction == undefined) ? "" :this.cboeditsubfunction,
      designation_gid:(this.cboeditdesignation == undefined) ? "" : this.cboeditdesignation,
      useraccess :(this.cboeditreporting == undefined) ? "" : this.txtemployeeacess,
      user_code :(this.txteditemployee_user_code == undefined) ? "" : this.txteditemployee_user_code,            
      role_gid:(this.cboeditrole == undefined) ? "" : this.cboeditrole,
      employee_reportingto  :(this.cboeditreporting == undefined) ? "" : this.cboeditreporting,
      // employee_photo : this.txtuploadphoto,
      user_firstname :(this.txteditfirst_name == undefined) ? "" : this.txteditfirst_name,
      user_lastname : this.txteditlast_name,
      gender :(this.rdbeditgender == undefined) ? "" : this.rdbeditgender,
      employee_emailid :(this.txteditpersonal_email_address == undefined) ? "" : this.txteditpersonal_email_address,
      employee_mobileno :(this.txteditpersonal_phone_number == undefined) ? "" : this.txteditpersonal_phone_number,
      per_address1:(this.txteditpermanent_address_line_one == undefined) ? "" : this.txteditpermanent_address_line_one,
      per_address2 :(this.txteditpermanent_address_line_two == undefined) ? "" : this.txteditpermanent_address_line_two,
      per_country_gid :(this.cboeditpermanent_country == undefined) ? "" : this.cboeditpermanent_country,
      per_state :(this.txteditpermanent_state == undefined) ? "" : this.txteditpermanent_state,
      per_city :(this.txteditpermanent_city == undefined) ? "" : this.txteditpermanent_city,
      per_postal_code :(this.txteditpermanent_postal_code == undefined) ? "" : this.txteditpermanent_postal_code,
      temp_address1 :(this.txtedittemporary_address_line_one == undefined) ? "" : this.txtedittemporary_address_line_one,
      temp_address2 :(this.txtedittemporary_address_line_two == undefined) ? "" : this.txtedittemporary_address_line_two,
      temp_country_gid :(this.cboedittemporary_country == undefined) ? "" : this.cboedittemporary_country,
      temp_state :(this.txtedittemporary_state == undefined) ? "" : this.txtedittemporary_state,
      temp_city :(this.txtedittemporary_city == undefined) ? "" : this.txtedittemporary_city,
      temp_postal_code:(this.txtedittemporary_postal_code == undefined) ? "" : this.txtedittemporary_postal_code,
      baselocation_gid:(this.cboeditbaselocation == undefined) ? "" : this.cboeditbaselocation,
      marital_status : (this.cboeditmartial_status == undefined) ? "" : this.cboeditmartial_status.MartialStatus,
      marital_status_gid : (this.cboeditmartial_status == undefined) ? "" : this.cboeditmartial_status.MartialStatus_gid,
      bloodgroup_name : (this.cboeditblood_group == undefined) ? "" : this.cboeditblood_group.bloodgroup_name,
      bloodgroup_gid : (this.cboeditblood_group == undefined) ? "" : this.cboeditblood_group.bloodgroup_gid,
      joining_date :(this.txteditemployee_joining_date == undefined) ? "" : this.txteditemployee_joining_date,
      personal_phone_no :(this.txteditpersonal_phone_number == undefined) ? "" : this.txteditpersonal_phone_number,
      personal_emailid :(this.txteditpersonal_email_address == undefined) ? "" : this.txteditpersonal_email_address,
  }
   
  if (this.lstab == 'pending') {
    var url = 'EmployeeOnboard/EmployeePendingUpdate';
  } 
  else {
    var url = 'EmployeeOnboard/EmployeeUpdate';
  }
  
  this.SocketService.post(url,params).subscribe((result: any) => { 
    if (result.status == true) {
      if(this.hrdocumentData_list != null){
        const jsonData = "" + JSON.stringify(this.hrdocumentData_list)+ "";  
        this.formDataObject.append('employee_gid', this.employee_gid);  
        this.formDataObject.append('project_flag', "Default");   
        this.formDataObject.append('hrdocumentList', jsonData);   
        this.updateresult = result.status;
        var api='EmployeeOnboard/HRDocumentUpload'
           
        this.SocketService.postfile(api,this.formDataObject).subscribe((result:any) => { 
                if(result.status==true && this.updateresult == true){
                  this.router.navigate(['/hrm/HrmtrnEmployeeonboard']); 
                  this.ToastrService.success("Employee details updated successfully"); 
                }
                else if(result.status==false && this.updateresult == true){
                  this.router.navigate(['/hrm/HrmtrnEmployeeonboard']); 
                  this.ToastrService.success("Employee details updated successfully " )  
                }
                else{
                  this.ToastrService.warning(result.message) 
                }
       });
      
      this.NgxSpinnerService.hide(); 
      }
     
    }
  
    else{
      this.ToastrService.warning(result.message)
     
    }
  
  
  });
  
  }
  
    backbutton(){
      this.router.navigate(['/hrm/HrmtrnEmployeeonboard']);
    }
   
  passwordsMatch(): boolean {
      const password = this.EmpEditForm.get('txtuser_password').value;
      const confirmPassword = this.EmpEditForm.get('txtconfrim_user_password').value;
      return password === confirmPassword;
    }
  
    DeleteDocumentClick(index : any){
      if (index >= 0 && index < this.hrdocumentData_list.length) {
        this.hrdocumentData_list.splice(index, 1);
      }  
    }
  
    DeleteDocumentEditClick(index : any){
      if (index >= 0 && index < this.HRDocumentList.length) {
        this.HRDocumentList.splice(index, 1);
      }  
    }
  
    HrDocumentClick(){
      this.AutoIDkey = this.generateKey(); 
      const fileInput: HTMLInputElement = document.getElementById('fileInput') as HTMLInputElement;
      if (fileInput) {
        const files: FileList | null = fileInput.files;
        if (files !=null && files.length !=0) { 
          for (let i = 0; i < files.length; i++) {  
            this.formDataObject.append(this.AutoIDkey, files[i]);  
            this.file_name = files[i].name;
          }
  
          const DocumentInfo = this.hrdocument_list.find((item: objInterface) => item.hrdocument_gid === this.HRDocFormdata.cboHRDocument);
          this.formDataObject.append('hrdocument_gid', this.HRDocFormdata.cboHRDocument==null ? "": this.HRDocFormdata.cboHRDocument); 
          this.formDataObject.append('hrdocument_name', DocumentInfo);  
          this.hrdocumentData_list.push({
            AutoID_Key: this.AutoIDkey,
            hrdocument_name: DocumentInfo.hrdocument_name,
            hrdocument_gid:this.HRDocFormdata.cboHRDocument, 
            file_name: this.file_name
           } 
          );  
          fileInput.value = '';
          this.HRDocumentForm.reset();
          // this.clearFileInput()
        } else {
          this.ToastrService.warning("Kindly Upload the Document") 
        } 
        console.log(this.hrdocumentData_list);
      } 
     
      
    }
  
    generateKey(): string { 
  
      return `AutoIDKey${new Date().getTime()}`;
    }
    
    masterToggle() { 
      
      this.isAllSelected() ? this.selection.clear() : 
        this.task_list.forEach((row: objInterface) => this.selection.select(row)); 
    }
  
    isAllSelected() { 
      const numSelected = this.selection.selected.length; 
      const numRows = this.task_list.length; 
      return numSelected === numRows; 
    }
  }
  
  


