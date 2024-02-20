import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-smr-trn-salesteampotentials',
  templateUrl: './smr-trn-salesteampotentials.component.html',
  styleUrls: ['./smr-trn-salesteampotentials.component.scss']
})
export class SmrTrnSalesteampotentialsComponent {

  countlist: any [] = [];
  potenialslist: any [] = [];
  responsedata: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService,
    public service: SocketService, private route: Router, private NgxSpinnerService: NgxSpinnerService) {

 }
 ngOnInit(): void {
  this.GetPotentialSummary();
  this.NgxSpinnerService.show();
  var url  = 'SmrTrnSalesManager/GetSmrTrnManagerCount';
    this.service.get(url).subscribe((result:any) => {
    this.responsedata = result;
    this.countlist = this.responsedata.teamcount_list; 
    //console.log(this.countlist, 'testdata');
    this.NgxSpinnerService.hide();
    });
  }
  GetPotentialSummary(){
    this.NgxSpinnerService.show();
   var url = 'SmrTrnSalesManager/GetSalesManagerPotential'
   this.service.get(url).subscribe((result: any) => {
    $('#potentialslist').DataTable().destroy();
     this.responsedata = result;
     this.potenialslist = this.responsedata.potentialslist;
     //console.log(this.entity_list)
     setTimeout(() => {
       $('#potentialslist').DataTable()
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
