import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AES } from 'crypto-js';
import { data } from 'jquery';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-acc-mst-openingbalance',
  templateUrl: './acc-mst-openingbalance.component.html',
  styleUrls: ['./acc-mst-openingbalance.component.scss']
})
export class AccMstOpeningbalanceComponent {


  responsedata: any;
  Openingbalance_lists: any [] = [];
  Openingbalance_list: any [] = [];
  totaldebitAmount:any;


  constructor(public service :SocketService,private route:Router,private ToastrService: ToastrService) {
  }
  ngOnInit(): void {
    this.GetOpeningbalance();
    this.GetAccMstOpeningbalance();
  }
  GetOpeningbalance() {
    var url = 'AccMstOpeningbalance/GetOpeningbalance'

    this.service.get(url).subscribe((result: any) => {

      this.responsedata = result;
      this.Openingbalance_list = this.responsedata.Openingbalance_list;

    });
  }
  GetAccMstOpeningbalance() {
    var url = 'AccMstOpeningbalance/GetAccMstOpeningbalance'

    this.service.get(url).subscribe((result: any) => {

      this.responsedata = result;
      this.Openingbalance_lists = this.responsedata.Openingbalance_lists;

    });
  }
  calculateTotal(): number {

    let totalAmount = 0;

    for (const data of this.Openingbalance_list) {

      totalAmount += parseFloat(data.credit_amount);

    }

    return totalAmount;

  }
  calculateTotal1(): number {

    let totalAmount = 0;

    for (const data of this.Openingbalance_lists) {

      totalAmount += parseFloat(data.debit_amount);

    }

    return totalAmount;

  }

  difference(): number {
    
    let totalCreditAmount = this.calculateTotal();
    let totalDebitAmount = this.calculateTotal1();
   
    return Math.abs(totalDebitAmount - totalCreditAmount );

   
   }
   onedit(){}


}