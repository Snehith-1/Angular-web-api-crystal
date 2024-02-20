import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-smr-trn-commission-payout',
  templateUrl: './smr-trn-commission-payout.component.html',
  styleUrls: ['./smr-trn-commission-payout.component.scss']
})
export class SmrTrnCommissionPayoutComponent {

  reactiveForm!: FormGroup;
  responsedata: any;
  commissionpayout_list: any;
  
  constructor(private formBuilder: FormBuilder,private route: Router, private ToastrService: ToastrService, public service: SocketService) {
    

  }
  ngOnInit(): void{
    this.CommissionPayoutSummary();

  
  }

  CommissionPayoutSummary(){
    var url = 'SmrCommissionManagement/GetCommissionPayoutSummary'
    debugger
    this.service.get(url).subscribe((result: any) => {
    $('#commissionpayout_list').DataTable().destroy();
     this.responsedata = result;
     this.commissionpayout_list = this.responsedata.GetCommissionPayout_List;
     
     setTimeout(() => {
       $('#commissionpayout_list').DataTable()
     }, 1);
    

    });

}
  CommissionAdd(){
    this.route.navigate(['/smr/SmrTrnCommissionPayoutAdd'])
  }

}
