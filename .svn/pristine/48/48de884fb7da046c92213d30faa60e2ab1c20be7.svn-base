import { Component } from '@angular/core';
import { FormGroup,FormBuilder,FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-ims-trn-openingstock-summary',
  templateUrl: './ims-trn-openingstock-summary.component.html',
  styleUrls: ['./ims-trn-openingstock-summary.component.scss']
})
export class ImsTrnOpeningstockSummaryComponent {
  stock_list:any;
  response_data :any;
  reactiveForm!: FormGroup;
  responsedata: any;
  parameterValue: any;
  invoice:any;
  

  constructor(private fb: FormBuilder,public NgxSpinnerService: NgxSpinnerService,private route: ActivatedRoute,private router: Router,private service: SocketService,private ToastrService: ToastrService,) {} 
  ngOnInit(): void {
    this.ImsTrnOpeningstockSummary();

    this.reactiveForm = new FormGroup({
      file: new FormControl(''),

     
    });
  }
  ImsTrnOpeningstockSummary(){

    debugger
    
    var api = 'ImsTrnOpeningStock/GetImsTrnOpeningstockSummary';
    this.NgxSpinnerService.show()
    this.service.get(api).subscribe((result:any) => {
      this.response_data = result;
      this.stock_list = this.response_data.stock_list;
      setTimeout(()=>{  
        $('#stock_list').DataTable();
      }, 1);
      this.NgxSpinnerService.hide()
    });
  
  }
  onadd()
  {
        this.router.navigate(['/ims/ImsTrnOpeningstockAdd'])

  }
  // onedit(params:any){
  //   const secretKey = 'storyboarderp';
  //   const param = (params);
  //   const encryptedParam = AES.encrypt(param,secretKey).toString();
  //   this.router.navigate(['/ims/ImsTrnOpeningstockEdit',encryptedParam]) 
  // }

  onedit(stockGid: any) {
    if (this.ShowEditButton(stockGid)) {
       
        const secretKey = 'storyboarderp';
        const param = stockGid;
        const encryptedParam = AES.encrypt(param, secretKey).toString();
        this.router.navigate(['/ims/ImsTrnOpeningstockEdit', encryptedParam]);
    } else {
       
        
        console.log('Issued_qty > 0. So, hiding the Edit button.');
    }
}

ShowEditButton(stockGid: any): boolean {
  const item = this.stock_list.find((item: { stock_gid: any; issued_qty: number; }) => item.stock_gid === stockGid);
  return item ? item.issued_qty == 0 : false;
}
}

