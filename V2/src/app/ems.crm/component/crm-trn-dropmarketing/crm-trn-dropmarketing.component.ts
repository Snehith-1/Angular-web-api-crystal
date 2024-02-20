import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { Router } from '@angular/router';
import { AES } from 'crypto-js';
import { NgxSpinnerService } from 'ngx-spinner';

interface IDrop
{
  remarks:any;
  leadbank_name:any;
}
@Component({
  selector: 'app-crm-trn-dropmarketing',
  templateUrl: './crm-trn-dropmarketing.component.html',
  styleUrls: ['./crm-trn-dropmarketing.component.scss']
})
export class CrmTrnDropmarketingComponent {
  teamdetails:any[] = [];
  responsedata: any;
  remarks: string | undefined;
  leadbank_name: string | undefined;
  Drop: any;
  countlist: any[] = [];
  totaltilecountdetails: any[] = [];
  totallapsedlongest: any[] = [];

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService,private route:Router,
    private NgxSpinnerService: NgxSpinnerService) {
    
  }
  ngOnInit(): void {
    this.GetDropManagerSummary();
    this.GetMarketingManagerSummary();
    this.Getteamcount();
    this.GetTotaltilecount();
    this.GetTotallapsedlongest();
}

//teamcount//
Getteamcount() {
  var url = 'MarketingManager/Getteamcount'
  this.service.get(url).subscribe((result: any) => {
    $('#teamdetails').DataTable().destroy();
    this.responsedata = result;
    this.teamdetails = this.responsedata.teamdetails;
    //console.log(this.entity_list)
    setTimeout(() => {
      $('#teamdetails').DataTable();
    }, 1);
  });
}

//Get Total tile count
GetTotaltilecount() {
  var url = 'MarketingManager/GetTotaltilecount'
  this.service.get(url).subscribe((result: any) => {
    $('#totaltilecountdetails').DataTable().destroy();
    this.responsedata = result;
    this.totaltilecountdetails = this.responsedata.totaltilecount_lists;
    //console.log(this.entity_list)
    setTimeout(() => {
      $('#totaltilecountdetails').DataTable();
    }, 1);
  });
}
//Get Total lapsed longest count
GetTotallapsedlongest() {
  var url = 'MarketingManager/Totallapsedlongest'
  this.service.get(url).subscribe((result: any) => {
    $('#totaltilecountdetails').DataTable().destroy();
    this.responsedata = result;
    this.totallapsedlongest = this.responsedata.totallapsedlongest;
    //console.log(this.entity_list)
    setTimeout(() => {
      $('#totaltilecountdetails').DataTable();
    }, 1);
  });
}

//// Summary Grid//////
GetDropManagerSummary() {
  this.NgxSpinnerService.show();
  var url = 'Marketingmanager/GetDropManagerSummary'
  this.service.get(url).subscribe((result: any) => {
    $('#Drop').DataTable().destroy();
    this.responsedata = result;
    this.Drop = this.responsedata.Drop;
    this.NgxSpinnerService.hide();
    //console.log(this.source_list)
    setTimeout(() => {
      $('#Drop').DataTable();
    }, 1);
  });
}
//360//
Onopen(param1: any, param2: any) {
  const secretKey = 'storyboarderp';
  const lspage1 = "MM-Drop";
  const lspage = AES.encrypt(lspage1, secretKey).toString();
  console.log(param1);
  console.log(param2);
  const leadbank_gid = AES.encrypt(param1, secretKey).toString();
  const lead2campaign_gid = AES.encrypt(param2, secretKey).toString();
  this.route.navigate(['/crm/CrmTrn360view', leadbank_gid, lead2campaign_gid, lspage]);
}
//Tiles count//
GetMarketingManagerSummary(){
  var url = 'MarketingManager/GetMarketingManagerSummary'
  this.service.get(url).subscribe((result: any) => {
    $('#countlist').DataTable().destroy();
    this.responsedata = result;
    this.countlist = this.responsedata.marketingmanager_lists;
    //console.log(this.entity_list)
    setTimeout(() => {
      $('#countlist').DataTable();
    }, 1);
  });
}

popmodal(parameter: string, parameter1: string) {

  this.remarks = parameter;
  this.leadbank_name = parameter1;
}




}
