import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { AES } from 'crypto-js';
import { NgxSpinnerService } from 'ngx-spinner';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-smr-trn-customer-retailer',
  templateUrl: './smr-trn-customer-retailer.component.html',
  styleUrls: ['./smr-trn-customer-retailer.component.scss']
})
export class SmrTrnCustomerRetailerComponent {
  file!: File;
  reactiveForm!: FormGroup;
  responsedata: any;
  customercount_list :any
  parameter: any;
  parameterValue: any;
  parameterValue1: any;
  smrcustomer_list: any[] = [];
  Documentdtl_list: any[] = [];
  Document_list: any[] = [];
  getData: any;
  constructor(private formBuilder: FormBuilder, public NgxSpinnerService:NgxSpinnerService,private ToastrService: ToastrService, private router: ActivatedRoute, private route: Router, public service: SocketService) {
  
  }
  ngOnInit(): void {
    // this.GetSmrTrnCustomerSummary();
    this.GetSmrTrnRetailerSummary();
}
//// Summary Grid//////
// GetSmrTrnCustomerSummary() {
//   var url = 'SmrTrnCustomerSummary/GetSmrTrnCustomerSummary'
//   this.service.get(url).subscribe((result: any) => {
//     $('#smrcustomer_list').DataTable().destroy();
//     this.responsedata = result;
//     this.smrcustomer_list = this.responsedata.smrcustomer_list;
//     setTimeout(() => {
//       $('#smrcustomer_list').DataTable();
//     }, 1);


//   })
  
  
// }
GetSmrTrnRetailerSummary() {
  var url = 'SmrTrnCustomerSummary/GetSmrTrnRetailerSummary'
  this.NgxSpinnerService.show();
  this.service.get(url).subscribe((result: any) => {
    $('#smrcustomer_list').DataTable().destroy();
    this.responsedata = result;
    this.smrcustomer_list = this.responsedata.smrcustomer_list;
    setTimeout(() => {
      $('#smrcustomer_list').DataTable();
    }, 1);
    this.NgxSpinnerService.hide();


  })
  
  
  var url  = 'SmrTrnCustomerSummary/GetSmrTrnCustomerCount';
  this.service.get(url).subscribe((result:any) => {
  this.responsedata = result;
  this.customercount_list = this.responsedata.customercount_list; 
  console.log(this.customercount_list,'testdata');
  });

}

onview(params:any){
  const secretKey = 'storyboarderp';
  const param = (params);
  const encryptedParam = AES.encrypt(param,secretKey).toString();
  this.route.navigate(['/smr/SrmTrnCustomerview',encryptedParam]) 
}
onedit(params: any){
  const secretKey = 'storyboarderp';
  const param = (params);
  const encryptedParam = AES.encrypt(param,secretKey).toString();
  this.route.navigate(['/smr/SmrMstCustomerEdit',encryptedParam]) 
}
onaddinfo(){}


openModalinactive(parameter: string){
  this.parameterValue = parameter
}


oninactive(){
  console.log(this.parameterValue);
    var url3 = 'SmrTrnCustomerSummary/GetcustomerInactive'
    this.service.getid(url3, this.parameterValue).subscribe((result: any) => {

      if ( result.status == false) {
       this.ToastrService.warning('Error While Customer Inactivated')
      }
      else {
       this.ToastrService.success('Customer Inactivated Successfully')
        }
      window.location.reload();
    });
}

openModalactive(parameter: string){
  this.parameterValue = parameter
}
onclose() {
  this.reactiveForm.reset();

}

onactive(){
  console.log(this.parameterValue);
    var url3 = 'SmrTrnCustomerSummary/GetcustomerActive'
    this.service.getid(url3, this.parameterValue).subscribe((result: any) => {

      if ( result.status == false) {
       this.ToastrService.warning('Error While Customer Activated')
      }
      else {
       this.ToastrService.success('Customer Activated Successfully')
        }
      window.location.reload();
    });
}


onprod(params: any)
{
  debugger

  const secretKey = 'storyboarderp';
  const param = (params);
  const encryptedParam = AES.encrypt(param,secretKey).toString();
  this.route.navigate(['/smr/SmrTrnCustomerPriceSegment',encryptedParam]) 
}

importexcel() {
  let formData = new FormData();
  if (this.file != null && this.file != undefined) {
    window.scrollTo({
      top: 0, // Code is used for scroll top after event done
    });
    formData.append("file", this.file, this.file.name);
    var api = 'SmrTrnCustomerSummary/CustomerImport'
    this.NgxSpinnerService.show();
    this.service.postfile(api, formData).subscribe((result: any) => {
      debugger;
      this.responsedata = result;       
       if(result.status ==false){
        this.ToastrService.warning(result.message)                    
      }
      else{
        this.ToastrService.success(result.message)
      }
      this.NgxSpinnerService.hide(); 
    });
  }
}
onChange1(event: any) {
  this.file = event.target.files[0];
}
downloadfileformat() {
  let link = document.createElement("a");
  link.download = "Customer Details";
  window.location.href = "https://"+ environment.host + "/Templates/Customer Details.xlsx";
  link.click();
}
onbranch(params: any)
{
  debugger

  const secretKey = 'storyboarderp';
  const param = (params);
  const encryptedParam = AES.encrypt(param,secretKey).toString();
  this.route.navigate(['/smr/SmrTrnCustomerbranch',encryptedParam]) 
}
oncontact(params: any)
{
  debugger

  const secretKey = 'storyboarderp';
  const param = (params);
  const encryptedParam = AES.encrypt(param,secretKey).toString();
  this.route.navigate(['/smr/SmrTrnCustomerCall',encryptedParam]) 
}
getdocumentlist(){

  var api1='SmrTrnCustomerSummary/GetDocumentlist'
     this.service.get(api1).subscribe((result:any)=>{
    this.responsedata=result;
    this.Document_list = this.responsedata.document_list1;
      });    
}

ondetail(document_name:any){
  debugger;
  var api1='SmrTrnCustomerSummary/GetDocumentDtllist'
  var param={
    document_gid:document_name,
  }
  this.service.getparams(api1,param).subscribe((result:any)=>{
 
    this.responsedata=result;
    this.Documentdtl_list = this.responsedata.documentdtl_list;  
  });
}


customerexportExcel() {
  debugger 
  var api7 = 'SmrTrnCustomerSummary/GetCustomerReportExport'
  this.service.generateexcel(api7).subscribe((result: any) => {
    this.responsedata = result;
    var phyPath = this.responsedata.customerexport_list[0].lspath1;
    var relPath = phyPath.split("src");
    var hosts = window.location.host;
    var prefix = location.protocol + "//";
    var str = prefix.concat(hosts, relPath[1]);
    var link = document.createElement("a");
    var name = this.responsedata.customerexport_list[0].lsname2.split('.');
    link.download = name[0];
    link.href = str;
    link.click();
    this.ToastrService.success("Customer Excel Exported Successfully")

  });
}


}
