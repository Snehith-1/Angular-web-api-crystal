import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';

@Component({
  selector: 'app-pmr-trn-purchase-requisition',
  templateUrl: './pmr-trn-purchase-requisition.component.html',
  styleUrls: ['./pmr-trn-purchase-requisition.component.scss']
})
export class PmrTrnPurchaseRequisitionComponent {

  purchaserequest_list: any [] = [];
  responsedata: any;
  quotation_gid: any;
  parameterValue1: any;
  purchaserequisition_gid:any;
  productlistdetailspr:any;

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private route: ActivatedRoute,private router: Router) {
  }


  ngOnInit(): void {
    this.GetPurchaseRequisitionSummary();
    


    
  }

  GetPurchaseRequisitionSummary(){
    var url = 'PmrTrnPurchaseRequisition/GetPmrTrnPurchaseRequisition'
    this.service.get(url).subscribe((result: any) => {
     $('#purchaserequest_list').DataTable().destroy();
      this.responsedata = result;
      this.purchaserequest_list = this.responsedata.purchaserequest_list;
      //console.log(this.entity_list)
      setTimeout(() => {
        $('#purchaserequest_list').DataTable()
      }, 1);
  
  
    });
  
 }
 onadd()
 {
  this.router.navigate(['/pmr/PmrTrnRaiseRequisition'])
 }


  openModaledit()
  {

  }

  openModaladd()
  {

  }

  onview(params:any)
  {
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/pmr/PmrTrnPurchaseRequisitionView',encryptedParam]) 
  }

  openModaldelete()
  {
    
  }
  Details(parameter: string,purchaserequisition_gid: string){
    this.parameterValue1 = parameter;
    this.purchaserequisition_gid = parameter;
  
    var url='PmrTrnPurchaseRequisition/GetProductdetails'
      let param = {
        purchaserequisition_gid : purchaserequisition_gid 
      }
      this.service.getparams(url,param).subscribe((result:any)=>{
      this.responsedata=result;
       this.productlistdetailspr = result.productlistdetailspr;   
      });
    
  }

}
