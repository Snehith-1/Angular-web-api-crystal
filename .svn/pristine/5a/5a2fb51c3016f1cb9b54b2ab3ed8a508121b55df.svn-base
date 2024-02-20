import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-acc-trn-bankbook',
  templateUrl: './acc-trn-bankbook.component.html',
  styleUrls: ['./acc-trn-bankbook.component.scss']
})
export class AccTrnBankbookComponent {
  subbankbook_list: any[] = [];
  bank_gid: any;
  responsedata: any;
  Getsubbankbook_list: any;


  constructor(public service: SocketService, private router: ActivatedRoute, private route: Router) {
  }



  ngOnInit(): void {
    const bank_gid = this.router.snapshot.paramMap.get('bank_gid');
    this.bank_gid = bank_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.bank_gid, secretKey).toString(enc.Utf8);
    this.bank_gid=deencryptedParam;
    console.log(deencryptedParam)
    this.GetAccTrnBankbooksummary(deencryptedParam);
  }

  GetAccTrnBankbooksummary(bank_gid: any) {
    var url = 'AccTrnBankbooksummary/GetSubBankBook'
    let param = {
      bank_gid: bank_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.Getsubbankbook_list = result.Getsubbankbook_list;
    });
  }
  onview(params:any){

    debugger;
  
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(params,secretKey).toString();
    this.route.navigate(['/finance/AccTrnBankBookEntry',encryptedParam])
  
   
  
    // this.router.navigate(['/pmr/PmrTrnPurchaseOr
  
   
  
   
  
   
  
  }

  calculateTotal(): number {
    let totalAmount = 0;
    for (const data of this.Getsubbankbook_list) {

      totalAmount += parseFloat(data.credit_amount.replace(",", ""));
    }
    return totalAmount;
  }

  calculateTotal1(): number {
    let totalAmount = 0;
    for (const data of this.Getsubbankbook_list) {
      totalAmount += parseFloat(data.debit_amount.replace(",", ""));

    }
    return totalAmount;
  }

  delete() {
  }



}
