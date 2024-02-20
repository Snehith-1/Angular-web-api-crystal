import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { environment } from 'src/environments/environment.development';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-hrm-trn-manualregulation',
  templateUrl: './hrm-trn-manualregulation.component.html',
  styleUrls: ['./hrm-trn-manualregulation.component.scss']
})
export class HrmTrnManualregulationComponent {
  offer_list: any[] = [];
  consider_list: any[] = [];
  branch_name:any;
  branch_list: any[] = [];
  dayslist: any[] = [];
  responsedata: any;
  reactiveForm!: FormGroup;
  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute,
     private router: Router, private ToastrService: ToastrService,
      public service: SocketService, public NgxSpinnerService:NgxSpinnerService,) {
  }
  ngOnInit(): void {

    this.reactiveForm = new FormGroup({

      branch_name : new FormControl(''),
      fromdate : new FormControl(''),     
      todate: new FormControl(''),
   });


}
search(){
debugger;
  let param={
    fromdate:this.reactiveForm.value.fromdate,
    todate :this.reactiveForm.value.todate
  }
  var url = 'ManualRegulation/GetManualRegulationsummary'
  this.NgxSpinnerService.show();  
this.service.getparams(url,param).subscribe((result: any) => {
  this.NgxSpinnerService.hide();
this.responsedata = result;
this.offer_list = this.responsedata.manuallist;
this.dayslist = this.responsedata.dayslist;
  setTimeout(() => {
    $('#offer_list').DataTable();
    }, );
});
}
}
