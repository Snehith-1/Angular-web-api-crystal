import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
interface ITask {
  customer_gid: string;
  customer_id: string;
  customer_name: string;
  contact_details: string;
  region_name: string;
  internal_notes: string;

}
@Component({
  selector: 'app-smr-trn-prospect',
  templateUrl: './smr-trn-prospect.component.html',
  styleUrls: ['./smr-trn-prospect.component.scss']
})
export class SmrTrnProspectComponent {
  new_list: any;
  MyTodayTaskCount_List :any;
  MyenquiryCount_list: any[] = [];
  task_list:any;
  prospect_list : any;
  Potential_list : any;
  All_list :any;
  Completed_list :any;
  Drop_list : any;
  task : ITask;
  responsedata : any;

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, private router: ActivatedRoute, private route: Router, public service: SocketService) {
    this.task = {} as ITask;
    
  }
  ngOnInit(): void {
    //this.GetSmrTrnMyEnquiry();
    //this.GetSmrTrnMyEnquirynew();
     this.GetSmrTrnMyEnquiryProspect();
    // this.GetSmrTrnMyEnquiryPotential();
    // this.GetSmrTrnMyEnquiryCompleted();
    // this.GetSmrTrnMyEnquiryDrop();
    // this.GetSmrTrnMyEnquiryAll();
    var api7 = 'SmrTrnMyEnquiry/GetMyenquiryCount';
    this.service.get(api7).subscribe((result:any) => {
    this.responsedata = result;
    this.MyenquiryCount_list = this.responsedata.MyenquiryCount_list; 
    console.log(this.MyenquiryCount_list,'testdata');
    }); 
}

  //// Summary Grid//////
  GetSmrTrnMyEnquiry() {
  var url = 'SmrTrnMyEnquiry/GetSmrTrnMyEnquiry'
  this.service.get(url).subscribe((result: any) => {
    $('#task_list').DataTable().destroy();
    this.responsedata = result;
    this.task_list = this.responsedata.task_list;
    setTimeout(() => {
      $('#task_list').DataTable();
    }, 1);


  })
  
  
}
GetSmrTrnMyEnquirynew() {
  var url = 'SmrTrnMyEnquiry/GetSmrTrnMyEnquirynew'
  this.service.get(url).subscribe((result: any) => {
    $('#new_list').DataTable().destroy();
    this.responsedata = result;
    this.new_list = this.responsedata.new_list;
    setTimeout(() => {
      $('#new_list').DataTable();
    }, 1);


  })
  
  
}
GetSmrTrnMyEnquiryProspect() {
  var url = 'SmrTrnMyEnquiry/GetSmrTrnMyEnquiryProspect'
  this.service.get(url).subscribe((result: any) => {
    $('#prospect_list').DataTable().destroy();
    this.responsedata = result;
    this.prospect_list = this.responsedata.prospect_list;
    setTimeout(() => {
      $('#prospect_list').DataTable();
    }, 1);


  })
  
  
}

GetSmrTrnMyEnquiryPotential() {
  var url = 'SmrTrnMyEnquiry/GetSmrTrnMyEnquiryPotential'
  this.service.get(url).subscribe((result: any) => {
    $('#Potential_list').DataTable().destroy();
    this.responsedata = result;
    this.Potential_list = this.responsedata.Potential_list;
    setTimeout(() => {
      $('#Potential_list').DataTable();
    }, 1);


  })
  
  
}

GetSmrTrnMyEnquiryCompleted() {
  var url = 'SmrTrnMyEnquiry/GetSmrTrnMyEnquiryCompleted'
  this.service.get(url).subscribe((result: any) => {
    $('#Completed_list').DataTable().destroy();
    this.responsedata = result;
    this.Completed_list = this.responsedata.Completed_list;
    setTimeout(() => {
      $('#Completed_list').DataTable();
    }, 1);


  })
  
  
}
GetSmrTrnMyEnquiryDrop() {
  var url = 'SmrTrnMyEnquiry/GetSmrTrnMyEnquiryDrop'
  this.service.get(url).subscribe((result: any) => {
    $('#Drop_list').DataTable().destroy();
    this.responsedata = result;
    this.Drop_list = this.responsedata.Drop_list;
    setTimeout(() => {
      $('#Drop_list').DataTable();
    }, 1);


  })
  
  
}
GetSmrTrnMyEnquiryAll() {
  var url = 'SmrTrnMyEnquiry/GetSmrTrnMyEnquiryAll'
  this.service.get(url).subscribe((result: any) => {
    $('#All_list').DataTable().destroy();
    this.responsedata = result;
    this.All_list = this.responsedata.All_list;
    setTimeout(() => {
      $('#All_list').DataTable();
    }, 1);


  })
  
  
}
openModal3(){}

}
