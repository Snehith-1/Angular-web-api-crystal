import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-smr-trn-commission-payout-add',
  templateUrl: './smr-trn-commission-payout-add.component.html',
  styleUrls: ['./smr-trn-commission-payout-add.component.scss']
})
export class SmrTrnCommissionPayoutAddComponent {

  prodForm!: FormGroup;
  commissionpayoutadd_list :any[]=[];
  responsedata: any;
  
  
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService,private route:Router) {
    

  }

  ngOnInit(): void{
    this.CommissionPayoutSummary();

    this.prodForm = new FormGroup({
      commission_amount : new FormControl('')
     
    })
  }

  CommissionPayoutSummary(){
    var url = 'SmrCommissionManagement/GetInvoiceSummary'
    
    this.service.get(url).subscribe((result: any) => {
    $('#commissionpayoutadd_list').DataTable().destroy();
     this.responsedata = result;
     this.commissionpayoutadd_list = this.responsedata.invoicesummary_list;
   
     setTimeout(() => {
       $('#commissionpayoutadd_list').DataTable()
     }, 1);
     for(let i=0;i<this.commissionpayoutadd_list.length;i++){
     // this.prodForm.addControl(`commission_amount_${i}`, new FormControl(this.commissionpayoutadd_list[i].payable_commission-this.commissionpayoutadd_list[i].commission_amount));
     const formattedValue = (this.commissionpayoutadd_list[i].payable_commission - this.commissionpayoutadd_list[i].commission_amount).toFixed(2);
     this.prodForm.addControl(`commission_amount_${i}`, new FormControl(formattedValue));
    }

    });

}


  onback(){
    this.route.navigate(['/smr/SmrTrnCommissionPayout'])
  }
  onupdate(i: number) {
    debugger;
    var url = 'SmrCommissionManagement/PostCommission';

    var params = {
        invoice_refno: this.commissionpayoutadd_list[i].invoice_refno,
        invoice_date: this.commissionpayoutadd_list[i].invoice_date,
        customer_type: this.commissionpayoutadd_list[i].customer_type,
        customer_name: this.commissionpayoutadd_list[i].customer_name,
        campaign_title: this.commissionpayoutadd_list[i].campaign_title,
        user_firstname: this.commissionpayoutadd_list[i].user_firstname,
        invoice_amount: this.commissionpayoutadd_list[i].invoice_amount,
        payable_commission: this.commissionpayoutadd_list[i].payable_commission,      
        balance_payable: this.commissionpayoutadd_list[i].balance_payable,
        commission_amount: this.prodForm.get(`commission_amount_${i}`)?.value ?? 0,
    };

   // console.log(params.balance_payable);
    
    this.service.postparams(url, params).pipe().subscribe((result: any) => {
        this.responsedata = result;
        if (result.status == false) {
            this.ToastrService.warning(result.message);
            this.CommissionPayoutSummary();
        } else {
            this.ToastrService.success(result.message);
            this.CommissionPayoutSummary();
        }
    });
}

  // onupdate(){
  //   debugger;
  //   let i=0;
  //   var url = 'SmrCommissionManagement/PostCommission';
   
  //   var params={
  //     invoice_refno:this.commissionpayoutadd_list[0].invoice_refno,
  //     invoice_date:this.commissionpayoutadd_list[0].invoice_date,
  //     customer_type:this.commissionpayoutadd_list[0].customer_type,
  //     customer_name:this.commissionpayoutadd_list[0].customer_name,
  //     campaign_title:this.commissionpayoutadd_list[0].campaign_title,
  //   user_firstname:this.commissionpayoutadd_list[0].user_firstname,
  //   invoice_amount:this.commissionpayoutadd_list[0].invoice_amount,
  //   payable_commission:this.commissionpayoutadd_list[0].payable_commission,
  //   //commission_amount:this.prodForm.value.commission_amount,
  //  // commission_amount: this.prodForm.get(`commission_amount_${i}`)?.value ?? 0,
  //  //commission_amount: this.prodForm.get(`commission_amount_${i}`)?.value ?? 0,
  //  //commission_amount: this.prodForm.get(`commission_amount_${this.commissionpayoutadd_list[i].invoice_gid}`)?.value ?? 0,
  //  //commission_amount: this.prodForm.get(`commission_amount_${this.commissionpayoutadd_list[i].invoice_gid}`)?.value ?? 0,
  //  commission_amount: this.prodForm.get(`commission_amount_${i}.invoice_refno`)?.value ?? 0,
  //   }
  //   console.log(params.commission_amount);
  //   this.service.postparams(url,params).pipe().subscribe((result: any) => {
  //           this.responsedata = result;
  //           if (result.status == false) {
  //             this.ToastrService.warning(result.message);
  //             this.CommissionPayoutSummary();
  //           } else {
  //             this.ToastrService.success(result.message);
  //             this.CommissionPayoutSummary();
  //           }
  //         });
  
  // }
}
