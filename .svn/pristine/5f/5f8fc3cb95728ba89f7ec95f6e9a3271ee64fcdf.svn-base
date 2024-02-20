import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-smr-trn-salesteamprospect',
  templateUrl: './smr-trn-salesteamprospect.component.html',
  styleUrls: ['./smr-trn-salesteamprospect.component.scss']
})
export class SmrTrnSalesteamprospectComponent {

  countlist: any [] = [];
  prospectslist: any [] = [];
  responsedata: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService,
    public service: SocketService, private route: Router, private NgxSpinnerService: NgxSpinnerService) {

 }
 ngOnInit(): void {
  this.GetProspectSummary();
  this.NgxSpinnerService.show();
  var url  = 'SmrTrnSalesManager/GetSmrTrnManagerCount';
    this.service.get(url).subscribe((result:any) => {
    this.responsedata = result;
    this.countlist = this.responsedata.teamcount_list; 
    //console.log(this.countlist, 'testdata');
    this.NgxSpinnerService.hide();
    });
  }
  GetProspectSummary(){
    this.NgxSpinnerService.show();
   var url = 'SmrTrnSalesManager/GetSalesManagerProspect'
   this.service.get(url).subscribe((result: any) => {
    $('#prospectslist').DataTable().destroy();
     this.responsedata = result;
     this.prospectslist = this.responsedata.prospectslist;
     //console.log(this.entity_list)
     setTimeout(() => {
       $('#prospectslist').DataTable()
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
