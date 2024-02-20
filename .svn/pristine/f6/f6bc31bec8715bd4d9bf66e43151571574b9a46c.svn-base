import { Component } from '@angular/core';
import { FormGroup,FormBuilder,FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
@Component({
  selector: 'app-acc-trn-cashbooksummary',
  templateUrl: './acc-trn-cashbooksummary.component.html',
  styleUrls: ['./acc-trn-cashbooksummary.component.scss']
})
export class AccTrnCashbooksummaryComponent {

  CashBook_list:any;
  response_data :any;
  reactiveForm!: FormGroup;
  responsedata: any;
  parameterValue: any;
  invoice:any;

  constructor(private fb: FormBuilder,private route: ActivatedRoute,private router: Router,private service: SocketService,private ToastrService: ToastrService,) {} 
  ngOnInit(): void {
    this.AccTrnCashbooksummary();

    this.reactiveForm = new FormGroup({
      file: new FormControl(''),

     
    });
  }
  AccTrnCashbooksummary(){

    debugger
    
    var api = 'AccTrnCashBookSummary/GetAccTrnCashbooksummary';
    this.service.get(api).subscribe((result:any) => {
      this.response_data = result;
      this.CashBook_list = this.response_data.CashBook_list;
      setTimeout(()=>{  
        $('#CashBook_list').DataTable();
      }, 1);
    });
  
  }
  onselect(params:any){
    debugger;
    const secretKey = 'storyboarderp';

    const param = (params);

    const encryptedParam = AES.encrypt(param,secretKey).toString();

    this.router.navigate(['/finance/AccTrnCashbookSelect',encryptedParam])

    // this.router.navigate(['/pmr/PmrTrnPurchaseOr


  
}
onclick(params:any){

  const secretKey = 'storyboarderp';

    const param = (params);

    const encryptedParam = AES.encrypt(param,secretKey).toString();

    this.router.navigate(['/finance/AccTrnCashbookedit',encryptedParam])
}
  
}
