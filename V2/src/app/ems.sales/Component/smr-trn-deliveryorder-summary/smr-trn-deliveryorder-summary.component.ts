import { Component } from '@angular/core';
import { FormBuilder, FormGroup,} from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-smr-trn-deliveryorder-summary',
  templateUrl: './smr-trn-deliveryorder-summary.component.html',
  styleUrls: ['./smr-trn-deliveryorder-summary.component.scss']
})
export class SmrTrnDeliveryorderSummaryComponent {
  responsedata: any;
  deliveryorder_list: any[] = [];
  getData: any;
  constructor(private formBuilder: FormBuilder,public service :SocketService,private route:Router,private ToastrService: ToastrService) {
  }
  ngOnInit(): void {
     this.GetSmrTrnDeliveryorderSummary();
 }
// // //// Summary Grid//////
GetSmrTrnDeliveryorderSummary() {
   var url = 'SmrTrnDeliveryorderSummary/GetSmrTrnDeliveryorderSummary'
    this.service.get(url).subscribe((result: any) => {
      $('#deliveryorder_list').DataTable().destroy();
      this.responsedata = result;
      this.deliveryorder_list = this.responsedata.deliveryorder_list;
      setTimeout(() => {
        $('#deliveryorder_list').DataTable();
              }, 1);


   })
  
  
}

  
  openModaledit(){}
   openModaldelete(){}
   openModalamend(){}
   onaddinfo(){}


}
