import { Component } from '@angular/core';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-crm-rpt-productpreviouspricereport',
  templateUrl: './crm-rpt-productpreviouspricereport.component.html',
  styleUrls: ['./crm-rpt-productpreviouspricereport.component.scss']
})
export class CrmRptProductpreviouspricereportComponent {
  productdropdown_list: any[] = [];
  responsedata:any;
  productform:any;
  activitylog_list: any;
  productname: any;
  product_gid: any;
  emptyFlag: boolean=false;
  productpurchaseorder_list: any;
  productSaleseorder_list: any;
  show = true;
  Productname: any;
  constructor(private service: SocketService){
    this.productform = new FormGroup({
      product_name: new FormControl(null, Validators.required)
    });
  }
  ngOnInit(): void {
    debugger
    this.show = false;
//      var api = 'ProductReport/GetProductdropdown';
//     this.service.get(api).subscribe((result: any) => {
//       this.responsedata = result;
//       this.productdropdown_list = this.responsedata.productdropdown_list;
//     this.product_gid = Number(this.productdropdown_list[0].product_gid);
//     this.Productname = (this.productdropdown_list[0].product_name);
// //     if(this.product_gid != null && this.Productname != null) {
// //       debugger
// //       this.show = true;
// //     }
// //     else
// //     { 
// //       debugger
// // this.GetProductDetails(this.product_gid);
// //     }

//     });
this.GetProductdropdown();
  }
  GetProductdropdown(){
    var api = 'ProductReport/GetProductdropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productdropdown_list = this.responsedata.productdropdown_list;
    this.product_gid = Number(this.productdropdown_list[0].product_gid);
    this.Productname = (this.productdropdown_list[0].product_name);
    // this.show = false;
  });

  }
  GetReportProductSummary(product_gid: any) {
    debugger
    let product_name = this.productform.get("product_name")?.value;
    this.show = true;
    var url = 'ProductReport/GetReportProductSummary'
    let param = {product_gid : product_gid,
      product_name : product_name}
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata=result;
      this.productpurchaseorder_list = result.productpurchaseorder_list;

  });

}
GetReportSalesOrderSummary(product_gid: any){
  debugger
  let product_name = this.productform.get("product_name")?.value;
  this.show = true;
  var url = 'ProductReport/GetReportSalesOrderSummary'
  let param = {product_gid : product_gid,
    product_name : product_name}
  this.service.getparams(url, param).subscribe((result: any) => {
    this.responsedata=result;
    this.productSaleseorder_list = result.productSaleseorder_list;
  });

}
get product_name() {
  return this.productform.get('product_name')!;
};

productdropdown(){
  debugger
  this.GetReportProductSummary(this.product_gid);
  this.GetReportSalesOrderSummary(this.product_gid);
}
}
