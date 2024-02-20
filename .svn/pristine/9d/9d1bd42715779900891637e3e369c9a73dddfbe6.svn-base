import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-smr-trn-salesteam-drop',
  templateUrl: './smr-trn-salesteam-drop.component.html',
  styleUrls: ['./smr-trn-salesteam-drop.component.scss']
})
export class SmrTrnSalesteamDropComponent {

  countlist: any [] = [];
  dropstatuslist: any [] = [];
  responsedata: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService,
    public service: SocketService, private route: Router, private NgxSpinnerService: NgxSpinnerService) {

 }
 ngOnInit(): void {
  this.GetDropSummary();
  this.NgxSpinnerService.show();
  var url  = 'SmrTrnSalesManager/GetSmrTrnManagerCount';
    this.service.get(url).subscribe((result:any) => {
    this.responsedata = result;
    this.countlist = this.responsedata.teamcount_list; 
    //console.log(this.countlist, 'testdata');
    this.NgxSpinnerService.hide();
    });
  }
  GetDropSummary(){
    this.NgxSpinnerService.show();
   var url = 'SmrTrnSalesManager/GetSalesManagerdrop'
   this.service.get(url).subscribe((result: any) => {
    $('#dropstatuslist').DataTable().destroy();
     this.responsedata = result;
     this.dropstatuslist = this.responsedata.dropstatuslist;
     //console.log(this.entity_list)
     setTimeout(() => {
       $('#dropstatuslist').DataTable()
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
