import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ExcelService } from 'src/app/Service/excel.service';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-crm-rpt-productconsumptionreport',
  templateUrl: './crm-rpt-productconsumptionreport.component.html',
  styleUrls: ['./crm-rpt-productconsumptionreport.component.scss']
})
export class CrmRptProductconsumptionreportComponent {
  ProductConsumptionReport_list: any[] = [];
  responsedata:any;
  ProductReportgrid_list: any[] = [];
  product_gid: any;
constructor(private excelService : ExcelService ,private route:Router,public service: SocketService,private http: HttpClient){

}
ngOnInit(): void {
  this.GetProductConsumptionReport();
}
//summary
GetProductConsumptionReport() {

  var url = 'ProductReport/GetProductConsumptionReport'
  this.service.get(url).subscribe((result: any) => {
    $('#ProductConsumptionReport_list').DataTable().destroy();
    this.responsedata = result;
    this.ProductConsumptionReport_list = this.responsedata.ProductConsumptionReport_list;
    setTimeout(() => {
      $('#ProductConsumptionReport_list').DataTable();
    }, 1);
  })
}
Details(product_gid: any){
  debugger
  
  var url = 'ProductReport/GetProductReportgrid'
  let param = {
    product_gid : product_gid
  }
  this.service.getparams(url, param).subscribe((result: any) => {
    this.responsedata = result;
    this.ProductReportgrid_list = result.ProductReportgrid_list;
    console.log(this.ProductReportgrid_list)
    setTimeout(() => {
      $('#ProductReportgrid_list').DataTable();
    }, 1);

  });
}

exportExcel1() :void {
  debugger
  const ProductReport = this.ProductConsumptionReport_list.map(item => ({
    Product_Group: item.productgroup_name || '', 
    Product_Code: item.product_code || '',
    Product: item.product_name || '',
    Units: item.productuom_name || '', 
   Quantity_Delivered: item.product_quantity || '',
   Quantity_Available: item.available_quantity || '',
 
   }));     
    // this.excelService.exportAsExcelFile(ProductReport, 'Product');
    // Create a new table element
  const table = document.createElement('table');

  // Add header row with background color
  const headerRow = table.insertRow();
  Object.keys(ProductReport[0]).forEach(header => {
    const cell = headerRow.insertCell();
    cell.textContent = header;
    cell.style.backgroundColor = '#00317a'; 
    cell.style.color = '#FFFFFF';
    cell.style.fontWeight = 'bold';
    cell.style.border = '1px solid #000000';
  });

  // Add data rows
  ProductReport.forEach(item => {
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
  link.download = 'Product.xls';
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
  }
}
