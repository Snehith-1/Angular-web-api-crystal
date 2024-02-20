import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { SalesTeamDualListComponent } from '../smr-mst-salesteam-summary/dual-list/dual-list.component'

interface Isalesteam {
  mail_id: string;
  team_name: any;  
  branch_gid: string;
  employee_name: string;
  employee_gid: string;
  description: string;
  branch_name: any;
  campaign_description : string;
  campaign_location : string;
  campaign_mailid : string;
  campaign_title:string;
  campaign_gid:string;

}

@Component({
  selector: 'app-smr-mst-salesteam-summary',
  templateUrl: './smr-mst-salesteam-summary.component.html',
  styleUrls: ['./smr-mst-salesteam-summary.component.scss']
})

export class SmrMstSalesteamSummaryComponent {
  reactiveForm!: FormGroup;
  reactiveFormEdit:FormGroup | any;
  responsedata: any;
  salesteam_list: any;
  salesteamgrid_list: any;
  getData: any;
  salesteam!: Isalesteam;
  campaign_gid: any;
  branch_list: any[] = [];
  Getemployee: any[] = [];
  data: any;
  parameterValue: any;
  editsalesteam_list: any;
  keepSorted = true;
  source: Array<any> = [];
  key!: string;
  key1!: string;
  display!: string;
  filter = false;
  confirmed: Array<any> = [];
  format: any = SalesTeamDualListComponent.DEFAULT_FORMAT;
  disabled = false;

  keepSorted1 = true;
  source1: Array<any> = [];
  key2!: string;
  key3!: string;
  display1!: string;
  filter1 = false;
  confirmed1: Array<any> = [];
  format1: any = SalesTeamDualListComponent.DEFAULT_FORMAT;
  disabled1 = false;

  parameterValue1: any;
  private confirmedStations: Array<any> = [];
  private sourceStations: Array<any> = [];
  sourceLeft = true;
  teamname: any;
  branchname: any;
  team: any;

  constructor(private formBuilder: FormBuilder, public route: ActivatedRoute, public service: SocketService, private router: Router, private ToastrService: ToastrService) {
    this.salesteam = {} as Isalesteam;

    this.reactiveForm = new FormGroup({
      branch_name: new FormControl(''),
      branch_gid: new FormControl(''),
      employee_name: new FormControl(''),
      employee_gid: new FormControl(''),
      description: new FormControl('')
    })
    this.reactiveFormEdit = new FormGroup ({
      campaign_title: new FormControl(''),
      campaign_description: new FormControl(''),
      campaign_location: new FormControl(''),
      campaign_mailid: new FormControl(''),
      campaign_gid: new FormControl(''),
    
    })
    
  }

  ngOnInit(): void {
    this.GetSmrMstSalesteamSummary();
    this.reactiveForm = new FormGroup({
      mail_id: new FormControl(this.salesteam.mail_id, [Validators.required,]),
      team_name: new FormControl(this.salesteam.team_name, [Validators.required,]),
      employee_name: new FormControl(this.salesteam.employee_name, [Validators.required,]),
      branch_name: new FormControl(this.salesteam.branch_name, [Validators.required,]),
      description: new FormControl(''),
    });
    this.reactiveFormEdit = new FormGroup({
      campaign_mailid: new FormControl(this.salesteam.campaign_mailid, [
        Validators.required,
      ]),
      campaign_title: new FormControl(this.salesteam.campaign_title, [
        Validators.required,
      ]),
      campaign_description: new FormControl(this.salesteam.campaign_description, [
        
      ]),
      campaign_location: new FormControl(this.salesteam.campaign_location, [
        Validators.required,
      ]),
      campaign_gid : new FormControl(''),

      
    });

    var url = 'SmrTrnSalesorder/GetBranchDtl'
    this.service.get(url).subscribe((result: any) => {
      this.branch_list = result.GetBranchDtl;
    });

    var url = 'SmrMstSalesteamSummary/Getemployee'
    this.service.get(url).subscribe((result: any) => {
      this.Getemployee = result.Getemployee;
    });
  }
  //// Summary //////
  GetSmrMstSalesteamSummary() {
    debugger
    var url = 'SmrMstSalesteamSummary/GetSmrMstSalesteamSummary'
    this.service.get(url).subscribe((result: any) => {
      $('#salesteam_list').DataTable().destroy();
      this.responsedata = result;
      this.salesteam_list = this.responsedata.salesteam_list;
      setTimeout(() => {
        $('#salesteam_list').DataTable();
      }, 1);
    })
  }
  /////////For Add PopUp/////////
  get mail_id() {
    return this.reactiveForm.get('mail_id')!;
  }
  get team_name() {
    return this.reactiveForm.get('team_name')!;
  }
  get branch_name() {
    return this.reactiveForm.get('branch_name')!;
  }
  get employee_name() {
    return this.reactiveForm.get('employee_name')!;
  }
  get campaign_mailid() {
    return this.reactiveFormEdit.get('campaign_mailid')!;
  }
  get campaign_title() {
    return this.reactiveFormEdit.get('campaign_title')!;
  }
  get campaign_location(){
    return this.reactiveFormEdit.get('campaign_location')!;
  }
  get description(){
    return this.reactiveFormEdit.get('campaign_description')!;
  }
  public onsubmit(): void {
    debugger
    if (this.reactiveForm.value.team_name != null && this.reactiveForm.value.mail_id != null) {
      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      this.reactiveForm.value;
      var url = 'SmrMstSalesteamSummary/PostSalesteam'
      this.service.post(url, this.reactiveForm.value).subscribe((result: any) => {

        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.GetSmrMstSalesteamSummary();
        }
        else {
          this.reactiveForm.get("team_name")?.setValue(null);
          this.reactiveForm.get("mail_id")?.setValue(null);
          this.reactiveForm.get("branch_name")?.setValue(null);
          this.reactiveForm.get("employee_name")?.setValue(null);
          this.reactiveForm.get("description")?.setValue(null);
          this.ToastrService.success(result.message)
          this.GetSmrMstSalesteamSummary();
          this.reactiveForm.reset();
        }
      });
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }

  redirecttolist() {
    this.router.navigate(['/smr/SmrMstSalesteamSummary']);
  }
  ////Expandable Grid////
  ondetail(campaign_gid: any) {
    var url = 'SmrMstSalesteamSummary/GetSmrMstSalesteamgrid'
    let param = {
      campaign_gid: campaign_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.salesteamgrid_list = result.salesteamgrid_list;
      console.log(this.salesteamgrid_list)
      setTimeout(() => {
        $('#salesteamgrid_list').DataTable();
      }, 1);
    });
  }

  onclose() {
    this.reactiveForm.reset();
  }

  filterBtn() {
    return (this.filter ? 'Hide Filter' : 'Show Filter');
  }

  doDisable() {
    this.disabled = !this.disabled;
  }

  disableBtn() {
    return (this.disabled ? 'Enable' : 'Disabled');
  }

  filterBtn1() {
    return (this.filter1 ? 'Hide Filter' : 'Show Filter');
  }

  doDisable1() {
    this.disabled = !this.disabled1;
  }

  disableBtn1() {
    return (this.disabled1 ? 'Enable' : 'Disabled');
  }

  swapDirection() {
    this.sourceLeft = !this.sourceLeft;
    this.format.direction = this.sourceLeft ? SalesTeamDualListComponent.LTR : SalesTeamDualListComponent.RTL;
  }

  private useStations() {
    this.key = 'employee_gid';
    this.key1 = 'campaign_gid';
    this.display = 'employee_name';
    this.keepSorted = true;

    if (this.confirmedStations === null) {
      this.source = this.sourceStations
      this.confirmed = this.confirmedStations;
    }
    else if (this.sourceStations === null) {
      this.confirmed = this.confirmedStations;
      this.source = this.sourceStations

    }
    else {
      this.source = [...this.sourceStations, ...this.confirmedStations];
      this.confirmed = this.confirmedStations;
      this.campaign_gid = this.parameterValue1.campaign_gid
    }
  }

  private usemanager() {
    this.key2 = 'employee_gid';
    this.key3 = 'campaign_gid';
    this.display1 = 'employee_name';
    this.keepSorted1 = true;
    
    if (this.confirmedStations === null) {
      this.source1 = this.sourceStations
      this.confirmed1 = this.confirmedStations;
    }
    else if (this.sourceStations === null) {
      this.confirmed1 = this.confirmedStations;
      this.source1 = this.sourceStations
    }
    else {
      this.source1 = [...this.sourceStations, ...this.confirmedStations];
      this.confirmed1 = this.confirmedStations;
    }
  }

  employeelist() {
    debugger
    console.log(this.parameterValue1)
    let param = {
      campaign_gid: this.parameterValue1.campaign_gid,
      campaign_location: this.parameterValue1.campaign_location,
    }

    var url = 'SmrMstSalesteamSummary/GetUnassignedEmplist'

    this.service.getparams(url, param).subscribe((result: any) => {
      this.sourceStations = result.GetUnassignedEmplist;

      var url1 = 'SmrMstSalesteamSummary/GetAssignedEmplist'
      this.service.getparams(url1, param).subscribe((result: any) => {
        this.confirmedStations = result.GetAssignedEmplist;
        this.useStations();
      });
    });
  }

  managerlist() {
    let param = {
      campaign_gid: this.parameterValue1.campaign_gid,
      campaign_location: this.parameterValue1.campaign_location,
    }
    var url = 'SmrMstSalesteamSummary/GetUnassignedManagerlist'

    this.service.getparams(url, param).subscribe((result: any) => {
      this.sourceStations = result.GetUnassignedManagerlist;
      
      var url1 = 'SmrMstSalesteamSummary/GetAssignedManagerlist'
      this.service.getparams(url1, param).subscribe((result: any) => {
        this.confirmedStations = result.GetAssignedManagerlist;
        this.usemanager();
      });
    });
  }

  openModalemployeelist(parameter: string) {
    this.parameterValue1 = parameter;
    console.log(this.parameterValue1)
    this.teamname = this.parameterValue1.campaign_title;
    this.branchname = this.parameterValue1.branch_name;
    this.employeelist();
  }

  openModalmanagerlist(parameter: string) {
    this.parameterValue1 = parameter
    this.teamname = this.parameterValue1.campaign_title;
    this.branchname = this.parameterValue1.branch_name;
    this.managerlist();
  }

  openModaledit(campaign_gid: any) {
    debugger
    var url = 'SmrMstSalesteamSummary/GetEditSalesTeamSummary'
      
          let param = {
      
            campaign_gid: campaign_gid
          }
          this.service.getparams(url, param).subscribe((result: any) => {
            this.editsalesteam_list = result.editsalesteam_list;
   
   this.reactiveFormEdit.get("campaign_mailid")?.setValue(this.editsalesteam_list[0].campaign_mailid);
   this.reactiveFormEdit.get("campaign_title")?.setValue(this.editsalesteam_list[0].campaign_title);
   this.reactiveFormEdit.get("campaign_location")?.setValue(this.editsalesteam_list[0].campaign_location);
   this.reactiveFormEdit.get("campaign_description")?.setValue(this.editsalesteam_list[0].campaign_description);
   this.reactiveFormEdit.get("campaign_gid")?.setValue(this.editsalesteam_list[0].campaign_gid);
  });
  }
  
  
  public onupdate(): void {
    if (this.reactiveFormEdit.value.campaign_mailid != null && this.reactiveFormEdit.value.campaign_title != null && this.reactiveFormEdit.value.campaign_gid != null && this.reactiveFormEdit.value.campaign_location != null && this.reactiveFormEdit.value.campaign_description != null) {
      for (const control of Object.keys(this.reactiveFormEdit.controls)) {
        this.reactiveFormEdit.controls[control].markAsTouched();
      }
      this.reactiveFormEdit.value;
  
      //console.log(this.reactiveFormEdit.value)
      var url = 'SmrMstSalesteamSummary/PostUpdateSalesTeam'
  
      this.service.post(url,this.reactiveFormEdit.value).pipe().subscribe((result:any) =>{
        this.responsedata=result;
        if(result.status ==false){
          this.ToastrService.warning(result.message)
          this.GetSmrMstSalesteamSummary();
        }
        else{
          this.ToastrService.success(result.message)
          this.GetSmrMstSalesteamSummary();
        }
       
    }); 
  
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }
  openModaldelete(parameter: string) {
    this.parameterValue = parameter
   }
   Details(parameter: string,campaign_gid: string){
    this.parameterValue1 = parameter;
    this.campaign_gid = parameter;
   
    var url = 'SmrMstSalesteamSummary/GetSmrMstSalesteamgrid'
    let param = {
      campaign_gid: campaign_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.salesteamgrid_list = result.salesteamgrid_list;
      this.team=this.salesteam_list[0].campaign_title;
      console.log(this.salesteamgrid_list)
      setTimeout(() => {
        $('#salesteamgrid_list').DataTable();
      }, 1);
 
    });
  }
}