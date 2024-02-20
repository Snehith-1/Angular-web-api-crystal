import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ExcelService } from 'src/app/Service/excel.service';

@Component({
  selector: 'app-crm-rpt-activitylogreport',
  templateUrl: './crm-rpt-activitylogreport.component.html',
  styleUrls: ['./crm-rpt-activitylogreport.component.scss']
})
export class CrmRptActivitylogreportComponent {
  activitylog_list: any[] = [];
  responsedata: any;
  response_data :any;
  log_desc:  any;
  leadbank_name: any;
  parameterValue1: any;

constructor(private excelService : ExcelService ,private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService){}

ngOnInit(): void {
  this.GetReportLogSummary();


}
GetReportLogSummary() {
  var url = 'MarketingReport/GetReportLogSummary'
  this.service.get(url).subscribe((result: any) => {
    $('#productgroup_list').DataTable().destroy();
    this.responsedata = result;
    this.activitylog_list = this.responsedata.activitylog_list;
    //console.log(this.entity_list)
    setTimeout(() => {
      $('#activitylog_list').DataTable();
    }, 1);


  });


}
// exportExcel1(){
//   const logReport = this.activitylog_list.map(item => ({
//     Campaign_title: item.campaign_title || '', 
//     Leadbank_name: item.leadbank_name || '',
//     Log_type: item.log_type || '',
//     Log_date: item.log_date || '', 
//     Created_date: item.created_date || '',
//     Log_desc: item.log_desc || '',
//     Created_name: item.created_name || '',
   
//    })); 
       
//     this.excelService.exportAsExcelFile(logReport, 'activitylog');
// }

exportExcel1() {
  const logReport = this.activitylog_list.map(item => ({
    Campaign_Title: item.campaign_title || '',
    Leadbank_Name: item.leadbank_name || '',
    Log_Type: item.log_type || '',
    Log_Date: item.log_date || '',
    Created_Date: item.created_date || '',
    Log_Desc: item.log_desc || '',
    Created_Name: item.created_name || '',
  }));

  // Create a new table element
  const table = document.createElement('table');

  // Add header row with background color
  const headerRow = table.insertRow();
  Object.keys(logReport[0]).forEach(header => {
    const cell = headerRow.insertCell();
    cell.textContent = header;
    cell.style.backgroundColor = '#00317a'; 
    cell.style.color = '#FFFFFF';
    cell.style.fontWeight = 'bold';
    cell.style.border = '1px solid #000000';
  });

  // Add data rows
  logReport.forEach(item => {
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
  link.download = 'activitylog.xls';
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
}

popmodal(parameter: string, parameter1: string) {
  this.parameterValue1 = parameter;
  this.log_desc = parameter;
  this.leadbank_name = parameter1;
}
}
