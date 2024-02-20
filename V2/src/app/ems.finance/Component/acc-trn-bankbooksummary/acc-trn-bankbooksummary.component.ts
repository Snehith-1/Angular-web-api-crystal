import { Component } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AES } from 'crypto-js';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-acc-trn-bankbooksummary',
  templateUrl: './acc-trn-bankbooksummary.component.html',
  styleUrls: ['./acc-trn-bankbooksummary.component.scss']
})
export class AccTrnBankbooksummaryComponent {

  bankbook_list: any[] = [];
  responsedata: any;


  constructor(public service :SocketService,private router: Router,private route: ActivatedRoute) {
    
    
  }

  ngOnInit(): void {
    this.AccTrnBankbooksummary();}

    AccTrnBankbooksummary(){
    var url='AccTrnBankbooksummary/GetBankBookSummary'
      
    this.service.get(url).subscribe((result:any)=>{
   
      this.responsedata=result;
      this.bankbook_list = this.responsedata.Getbankbook_list;  
      setTimeout(()=>{   
        $('#bankbook_list').DataTable();
      }, 1);
     console.log(this.bankbook_list)
      
    
   
  });
  }

  onclick(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/finance/AccTrnBankbook',encryptedParam]) 
  }
}
