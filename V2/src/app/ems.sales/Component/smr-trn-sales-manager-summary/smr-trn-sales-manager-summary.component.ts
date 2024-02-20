import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
@Component({
  selector: 'app-smr-trn-sales-manager-summary',
  templateUrl: './smr-trn-sales-manager-summary.component.html',
  styleUrls: ['./smr-trn-sales-manager-summary.component.scss']
})
export class SmrTrnSalesManagerSummaryComponent {
  responsedata:any;
  countlist: any [] = [];
  totallist: any [] = [];
  
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService,
    public service: SocketService, private route: Router, private NgxSpinnerService: NgxSpinnerService) {

 }
 ngOnInit(): void {
 this.GetTotalSummary();
 var url  = 'SmrTrnSalesManager/GetSmrTrnManagerCount';
    this.service.get(url).subscribe((result:any) => {
    this.responsedata = result;
    this.countlist = this.responsedata.teamcount_list; 
    console.log(this.countlist, 'testdata');
    });
 }
 GetTotalSummary(){
  var url = 'SmrTrnSalesManager/GetSalesManagerTotal'
  this.service.get(url).subscribe((result: any) => {
   $('#totalalllist').DataTable().destroy();
    this.responsedata = result;
    this.totallist = this.responsedata.totalalllist;
    //console.log(this.entity_list)
    setTimeout(() => {
      $('#totalalllist').DataTable()
    }, 1);


  });
}
 summary()
 {
    //this.NgxSpinnerService.show();
    this.route.navigate(['/smr/SmrTrnSalesManagerSummary'])
 }

  potentials()
  {
    //this.NgxSpinnerService.show();
    this.route.navigate(['/smr/SmrTrnSalesTeamPotentials'])
    
  }
  prospect()
  {
    //this.NgxSpinnerService.show();
    this.route.navigate(['/smr/SmrTrnSalesTeamProspects'])
  
  }

  completed()
  {
    //this.NgxSpinnerService.show();
    this.route.navigate(['/smr/SmrTrnSalesTeamProspects'])
  
  }

  drop()
  {
    //this.NgxSpinnerService.show();
    this.route.navigate(['/smr/SmrTrnSalesTeamDrop'])
  
  }
  Onopen(leadbank_gid:any,lead2campaign_gid:any)
  {
  debugger
  const secretKey = 'storyboarderp';
  const lspage1 = "My-Prospect";
  const lspage = AES.encrypt(lspage1, secretKey).toString();
  const param = (leadbank_gid);
  const param1 = (lead2campaign_gid);
  const encryptedParam = AES.encrypt(param,secretKey).toString();
  const encryptedParam1 = AES.encrypt(param1,secretKey).toString();
  this.route.navigate(['/smr/SmrTrnSales360',encryptedParam,encryptedParam1,lspage]) 
  }

  
}

