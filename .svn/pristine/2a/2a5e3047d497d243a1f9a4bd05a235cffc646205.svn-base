import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AES } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ExcelService } from 'src/app/Service/excel.service';


interface ILeadBank {
  //sourceedit_name: any;
  remarks: string;
  created_date: string;
  company_name: string;
  leadbank_name: string;
  leadbankcontact_name: string;
  contact_details: string;
  region_name: string;
  source_name: string;
  created_by: string;
  lead_status: string;
  assign_to: string;
  leadbank_gid: string;
  companyedit_name: string;
}

@Component({
  selector: 'app-crm-trn-retailerleadbank',
  templateUrl: './crm-trn-retailerleadbank.component.html',
  styleUrls: ['./crm-trn-retailerleadbank.component.scss']
})

export class CrmTrnRetailerleadbankComponent {
  file!: File;
  reactiveForm!: FormGroup;
  reactiveFormEdit!: FormGroup;
  responsedata: any;
  parameterValue: any;
  leadbank_name: any;
  parameterValue1: any;
  leadbank_list: any[] = [];
  leadbank_list1: any[] = [];
  leadbank_list2: any[] = [];
  firstCustomertype:any;
  firstCustomertype2:any; 
  firstCustomertype3:any;  
  LeadBankCountList: any[] = [];
  leadbank!: ILeadBank;
  customertype_list :any;
  remarks: any;
  leadbankcontact_name: any;
  fileInputs: any;
  constructor( private excelService : ExcelService ,private formBuilder: FormBuilder, private route: Router, private router: Router, private ToastrService: ToastrService, public service: SocketService, private NgxSpinnerService: NgxSpinnerService) {
    this.leadbank = {} as ILeadBank;
  }


  ngOnInit(): void {
    this.GetLeadbankSummary();
    this.GetLeadbankSummary1(); // Call your GetLeadbankSummary1 function
    this.GetLeadbankSummary2(); // Call your GetLeadbankSummary2 function
    this.GetCustomerTypeSummary();
    // Form values for Add popup/////
    this.reactiveForm = new FormGroup({
      company_name: new FormControl(this.leadbank.company_name, [
        Validators.required,
      ]),
      leadbankcontact_name: new FormControl(this.leadbank.leadbankcontact_name, [
        Validators.required,
      ]),
      leadbank_name: new FormControl(this.leadbank.leadbank_name),
    });
    // Form values for Edit popup/////
    this.reactiveFormEdit = new FormGroup({
      companyedit_name: new FormControl(this.leadbank.companyedit_name, [
        Validators.required,
      ]),
      leadbank_gid: new FormControl(''),
    });

    var api = 'Leadbank/GetLeadBankCount'
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.LeadBankCountList = this.responsedata.LeadBankCount_List;
    });

  }
  //// Summary Grid//////
  GetLeadbankSummary() {
    this.NgxSpinnerService.show();
    var url = 'Leadbank/GetLeadbankSummary'
    this.service.get(url).subscribe((result: any) => {
      $('#leadbank_list').DataTable().destroy();
      this.responsedata = result;
      this.leadbank_list = this.responsedata.leadbank_list;
      this.NgxSpinnerService.hide();
      //console.log(this.source_list)
      setTimeout(() => {
        $('#leadbank_list').DataTable();
      }, 1);
    });
  }
  GetLeadbankSummary1() {
    this.NgxSpinnerService.show();
    var url = 'Leadbank/GetLeadbankSummary1'
    this.service.get(url).subscribe((result: any) => {
      $('#leadbank_list1').DataTable().destroy();
      this.responsedata = result;
      this.leadbank_list1 = this.responsedata.leadbank_list1;
      this.NgxSpinnerService.hide();
      //console.log(this.source_list)
      setTimeout(() => {
        $('#leadbank_list1').DataTable();
      }, 1);
    });
  }
  GetLeadbankSummary2() {
    this.NgxSpinnerService.show();
    var url = 'Leadbank/GetLeadbankSummary2'
    this.service.get(url).subscribe((result: any) => {
      $('#leadbank_list2').DataTable().destroy();
      this.responsedata = result;
      this.leadbank_list2 = this.responsedata.leadbank_list2;
      this.NgxSpinnerService.hide();
      //console.log(this.source_list)
      setTimeout(() => {
        $('#leadbank_list2').DataTable();
      }, 1);


    });
  }
  onview(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const lspage1 = "LeadBankretailer";
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    const lspage = AES.encrypt(lspage1, secretKey).toString();
    console.log(encryptedParam);
    this.route.navigate(['/crm/CrmTrnRetailerview', encryptedParam, lspage])
  }

  onbranch(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    //console.log(param);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    console.log(encryptedParam);
    this.route.navigate(['/crm/CrmTrnLeadBankbranch', encryptedParam])

  }

  onedit(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const lspage1 = "LeadBankretailer";
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    const lspage = AES.encrypt(lspage1, secretKey).toString();
    console.log(encryptedParam);
    this.route.navigate(['/crm/CrmTrnRetaileredit', encryptedParam, lspage])

  }
  ////////////Delete ////////

  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  }
  ondelete() {
    this.NgxSpinnerService.hide();
    console.log(this.parameterValue);
    var url = 'Leadbank/deleteLeadbankSummary'
    let param = {
      leadbank_gid: this.parameterValue
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      // if (result.Status == true) {
      //   window.scrollTo({
      //     top: 0, // Code is used for scroll top after event done
      //   });
      //   this.NgxSpinnerService.hide();
      //   this.ToastrService.success(result.message)
      // }
      // else {
      //   window.scrollTo({
      //     top: 0, // Code is used for scroll top after event done
      //   });
      //   this.NgxSpinnerService.hide();
      //   this.ToastrService.warning(result.message)
      // }
      if (result.Status == true) {
        window.scrollTo({
          top: 0, // Code is used for scroll top after event done
        });
        
         this.NgxSpinnerService.hide();
        this.ToastrService.success(result.message)
        setTimeout(() => {
          window.location.reload();
        }, 2000);
      }
      else {
        window.scrollTo({
          top: 0, // Code is used for scroll top after event done
        });
         //window.location.reload()
         this.NgxSpinnerService.hide();
        this.ToastrService.warning(result.message)
      }
      this.GetLeadbankSummary();
      setTimeout(() => {
        window.location.reload();
      }, 2000);
   
    });
  }
  GetCustomerTypeSummary() {
    var api = 'Leadbank/GetCustomerTypeSummary'
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.customertype_list = this.responsedata.customertype_list1;
      
      this.firstCustomertype = this.customertype_list[0].customer_type1;
      this.firstCustomertype2 = this.customertype_list[1].customer_type1;
      this.firstCustomertype3 = this.customertype_list[2].customer_type1;

      
  
      // this.reactiveFormCustomerType.get("customertype_gid1")?.setValue(firstCustomertype.customertype_gid1);
      // this.reactiveFormCustomerType.get("customer_type1")?.setValue(firstCustomertype.customer_type1);

    
    });
  }
  onChange1(event: any) {
    this.file = event.target.files[0];
    // var api='Employeelist/EmployeeProfileUpload'
    // //console.log(this.file)
    //   this.service.EmployeeProfileUpload(api,this.file).subscribe((result:any) => {
    //     this.responsedata=result;
    //   });
  }
  // importexcel() {
  //   this.NgxSpinnerService.show();
  //   let formData = new FormData();
  //   if (this.file != null && this.file != undefined) {
  //     window.scrollTo({
  //       top: 0, // Code is used for scroll top after event done
  //     });
  //     formData.append("file", this.file, this.file.name);
  //     var api = 'Leadbank/LeadReportImport'
  //     this.service.postfile(api, formData).subscribe((result: any) => {
  //       this.responsedata = result;
  //       this.NgxSpinnerService.hide();
  //       this.ToastrService.success("Excel Uploaded Successfully");
  //       window.location.reload();

  //     });

  //   }
  // }
  importexcel() {

    debugger
     this.NgxSpinnerService.show();
     let formData = new FormData();
     if (this.file != null && this.file != undefined) {
       window.scrollTo({
         top: 0, // Code is used for scroll top after event done
       });
       formData.append("file", this.file, this.file.name);
       var api = 'Leadbank/LeadReportImport'
       this.service.postfile(api, formData).subscribe((result: any) => {
         this.responsedata = result;
         if (result.status == false) {
 
           this.NgxSpinnerService.hide();
           this.ToastrService.warning('Error While Occured Excel Upload')
         }
         else {
 
           this.NgxSpinnerService.hide();
           // window.location.reload();
           this.fileInputs= null;
           this.ToastrService.success("Excel Uploaded Successfully")
 
         }
         // this.NgxSpinnerService.hide();
         // this.ToastrService.success("Excel Uploaded Successfully");
         // window.location.reload();
 
       });
     }
   }
  exportExcel2() :void {
    const LeadReport = this.leadbank_list2.map(item => ({
      Leadbank_Id: item.leadbank_id || '', 
      Leadbank_Name: item.leadbank_name || '',
      Remarks: item.remarks || '',
      Leadbankcontact_Name: item.leadbankcontact_name || '', 
      Mobile_Number: item.mobile || '',
      Email_Address: item.email || '',
      Region_Name: item.region_name || '',
      Leadbank_Address1: item.address1 || '', 
      Leadbank_Address2: item.address2 || '',
      Leadbank_City: item.leadbank_city || '',
      Leadbank_State: item.leadbank_state || '',
      Leadbank_Pin: item.leadbank_pin || '',
      Country_Name: item.country_name || '',
      Created_Date: item.created_date || '', 
      Source_Name: item.source_name || '',
      Customer_Type: item.customer_type || '',

     }));     
      // this.excelService.exportAsExcelFile(LeadReport, 'Lead');
        // Create a new table element
  const table = document.createElement('table');

  // Add header row with background color
  const headerRow = table.insertRow();
  Object.keys(LeadReport[0]).forEach(header => {
    const cell = headerRow.insertCell();
    cell.textContent = header;
    cell.style.backgroundColor = '#00317a'; 
    cell.style.color = '#FFFFFF';
    cell.style.fontWeight = 'bold';
    cell.style.border = '1px solid #000000';
  });

  // Add data rows
  LeadReport.forEach(item => {
    const dataRow = table.insertRow();
    Object.values(item).forEach(value => {
      const cell = dataRow.insertCell();
      cell.textContent = value;
      cell.style.border = '1px solid #000000';
    });
  });

  // Convert the table to a data URI
  const tableHtml = table.outerHTML;
  const dataUri = 'data:application/vnd.ms-excel;base64,' + btoa(unescape(encodeURIComponent(tableHtml)));

  // Trigger download
  const link = document.createElement('a');
  link.href = dataUri;
  link.download = 'Lead.xls';
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
    }
  
  oncontact(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    //console.log(param);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    console.log(encryptedParam);
    this.route.navigate(['/crm/CrmTrnLeadbankcontact', encryptedParam])
  }
  downloadfileformat() {
    let link = document.createElement("a");
    link.download = "Lead Template";
    link.href = "assets/media/Excels/leadbanktemplate/Lead_Template.xlsx";
    link.click();
  }

  onclose() {
    this.reactiveForm.reset();
  }

  onadd() {
    const secretKey = 'storyboarderp';
    const lspage1 = 'LeadBankretailer';
    const lspage = AES.encrypt(lspage1, secretKey).toString();
    this.route.navigate(['/crm/CrmTrnRetaileradd', lspage]);
  }

  popmodal(parameter: string, parameter1: string, parameter2: string) {
    this.parameterValue1 = parameter;
    this.remarks = parameter;
    this.leadbank_name = parameter1;
    this.leadbankcontact_name = parameter2;
  }

  canceluploadexcel(){
    this.fileInputs= null;
  }
}
