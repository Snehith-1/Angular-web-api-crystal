import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-smr-trn-salesteam-complete',
  templateUrl: './smr-trn-salesteam-complete.component.html',
  styleUrls: ['./smr-trn-salesteam-complete.component.scss']
})
export class SmrTrnSalesteamCompleteComponent {

  countlist: any [] = [];
  completelist: any [] = [];
  responsedata: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService,
    public service: SocketService, private route: Router, private NgxSpinnerService: NgxSpinnerService) {

 }
 ngOnInit(): void {
  this.GetCompleteSummary();
  this.NgxSpinnerService.show();
  var url  = 'SmrTrnSalesManager/GetSmrTrnManagerCount';
    this.service.get(url).subscribe((result:any) => {
    this.responsedata = result;
    this.countlist = this.responsedata.teamcount_list; 
    //console.log(this.countlist, 'testdata');
    this.NgxSpinnerService.hide();
    });
  }
  GetCompleteSummary(){
    this.NgxSpinnerService.show();
   var url = 'SmrTrnSalesManager/GetSalesManagerComplete'
   this.service.get(url).subscribe((result: any) => {
    $('#completelist').DataTable().destroy();
     this.responsedata = result;
     this.completelist = this.responsedata.completelist;
     //console.log(this.entity_list)
     setTimeout(() => {
       $('#completelist').DataTable()
       this.NgxSpinnerService.hide();
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
  
  drop()
  {
     //this.NgxSpinnerService.show();
    this.route.navigate(['/smr/SmrTrnSalesTeamDrop'])
  
  }

  completed()
  {
    //this.NgxSpinnerService.show();
    this.route.navigate(['/smr/SmrTrnSalesTeamComplete'])
  
  }
  Onopen()
  {

  }
}
