import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-rbl-trn-proforma-invoice-add',
  templateUrl: './rbl-trn-proforma-invoice-add.component.html',
  styleUrls: ['./rbl-trn-proforma-invoice-add.component.scss']
})
export class RblTrnProformaInvoiceAddComponent {
  
  response_data: any;
  proformainvoiceaddsummary_list: any[] = [];  
  parameterValue1 : any;
  proformaaddproduct_list : any[] = [];
  directorder_gid : any;

  constructor(private router: Router, private route: ActivatedRoute, private service: SocketService) { }

  ngOnInit() {
    var api = 'ProformaInvoice/GetProformaInvoiceAddSummary';
    this.service.get(api).subscribe((result: any) => {
      this.response_data = result;
      this.proformainvoiceaddsummary_list = this.response_data.proformainvoiceaddsummary_list;
      setTimeout(() => {
        $('#addproformainvoice').DataTable();
      }, 1);
    });
  }

  Details(parameter: string,directorder_gid: string){
    this.parameterValue1 = parameter;
    this.directorder_gid = parameter;
  
    var url='ProformaInvoice/GetaddproformaProductdetails'
      let param = {
        directorder_gid : directorder_gid 
      }
      this.service.getparams(url,param).subscribe((result:any)=>{
      this.response_data=result;
      this.proformaaddproduct_list = result.proformaaddproduct_list;   
      });
    
  }

  GetaddproformaProductdetails() {
    
    var url = 'ProformaInvoice/GetaddproformaProductdetails'
    this.service.get(url).subscribe((result: any) => {
      $('#proformaaddproduct_list').DataTable().destroy();
      this.response_data = result;
      this.proformaaddproduct_list = this.response_data.proformaaddproduct_list;
      setTimeout(() => {
        $('#proformaaddproduct_list').DataTable();
      }, 1);
  
  
    })
  }
  

  back() {
    this.router.navigate(['/einvoice/ProformaInvoice']);
  }

  confirminvoice(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.router.navigate(['/einvoice/ProformaInvoiceConfirm', encryptedParam])
  }
}
