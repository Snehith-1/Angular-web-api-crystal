import { Component } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-ims-trn-issuematerial-summary',
  templateUrl: './ims-trn-issuematerial-summary.component.html',
  styleUrls: ['./ims-trn-issuematerial-summary.component.scss']
})
export class ImsTrnIssuematerialSummaryComponent {

  issuematerial_list:any;
  responsedata: any;


  constructor(public service :SocketService,private route:Router,private ToastrService: ToastrService) {  
  }
  ngOnInit(): void {
    this.GetIssueMaterialSummary();
  }
  GetIssueMaterialSummary(){
    var url = 'ImsTrnIssueMaterial/GetIssueMaterialSummary'
    this.service.get(url).subscribe((result: any) => {
  
      this.responsedata = result;
      this.issuematerial_list = this.responsedata.Getissuematerial_list;
      setTimeout(() => {
        $('#issuematerial_list').DataTable();
      }, 1);
    });
  }

}
