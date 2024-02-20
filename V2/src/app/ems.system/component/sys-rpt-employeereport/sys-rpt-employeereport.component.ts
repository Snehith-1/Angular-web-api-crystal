import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
interface IEmployeeReport {
  branch_name: string;
}
@Component({
  selector: 'app-sys-rpt-employeereport',
  templateUrl: './sys-rpt-employeereport.component.html',
  styleUrls: ['./sys-rpt-employeereport.component.scss']
})
export class SysRptEmployeereportComponent {
  reactiveForm!: FormGroup;
  responsedata: any;
  employeereportlist: any[] = [];
  branchlist: any[] = [];
  employeereport!: IEmployeeReport;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {
    this.employeereport = {} as IEmployeeReport;
  }
  ngOnInit(): void {
    this.reactiveForm = new FormGroup({

      branch_name : new FormControl(this.employeereport.branch_name, [
        Validators.required,
        Validators.minLength(1),
      ]),
    });
  

    var api='SystemRptEmployeeReport/GetBranchDtl'
    this.service.get(api).subscribe((result:any)=>{
    this.branchlist = result.GetBranchDtl;
    //console.log(this.branchlist)
   });
    // //// Summary Grid//////

    // var url = 'SystemRptEmployeeReport/GetEmployeeReportSummary'
    // this.service.get(url).subscribe((result: any) => {
   
    //   this.responsedata = result;
    //   this.employeereportlist = this.responsedata.employeereport_list;
    //   //console.log(this.employeereportlist)
    //   setTimeout(() => {
    //     $('#employeereportlist').DataTable();
    //   }, 1);
   
   
    // });
   
  }
 
  openexportExcel(){
    var api7 = 'SystemRptEmployeeReport/GetReportExportExcel'
    // //console.log(this.file)
    debugger
    this.service.generateexcel(api7).subscribe((result: any) => {
      this.responsedata = result;
      var phyPath = this.responsedata.getreport_exportexcel[0].lspath1;
      var relPath = phyPath.split("src");
      var hosts = window.location.host;
      var prefix = location.protocol + "//";
      var str = prefix.concat(hosts, relPath[1]);
      var link = document.createElement("a");
      var name = this.responsedata.getreport_exportexcel[0].lsname.split('.');
      link.download = name[0];
      link.href = str;
      link.click();
    });
  }
 
  GetPaymentSummary(){
    const selectedBranch = this.reactiveForm.value.branch_name || 'null';
    
    for (const control of Object.keys(this.reactiveForm.controls)) {
      this.reactiveForm.controls[control].markAsTouched();
    }
    const params = {
      branch_gid: selectedBranch,
     
    };
    const url = 'SystemRptEmployeeReport/GetEmployeeReportSummary';

    this.service.getparams(url, params).subscribe((result) => {
      this.responsedata = result;
      this.employeereportlist = this.responsedata.employeereport_list;
      setTimeout(() => {
        $('#employeereport_list').DataTable();
      },1);

      
    });

  }
 

}


