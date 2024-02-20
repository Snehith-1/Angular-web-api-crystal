import { Component } from '@angular/core';
import { FormBuilder, FormGroup,} from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { AES } from 'crypto-js';

@Component({
  selector: 'app-smr-trn-adddeliveryorder',
  templateUrl: './smr-trn-adddeliveryorder.component.html',
  styleUrls: ['./smr-trn-adddeliveryorder.component.scss']
})
export class SmrTrnAdddeliveryorderComponent {
  responsedata: any;
  adddeliveryorder_list: any[] = [];
  getData: any;
  constructor(private formBuilder: FormBuilder,public service :SocketService,private router:Router,private ToastrService: ToastrService) {
  }
  ngOnInit(): void {
    this.GetSmrTrnAddDeliveryorderSummary();
}
//// Summary Grid//////
GetSmrTrnAddDeliveryorderSummary() {
  var url = 'SmrTrnDeliveryorderSummary/GetSmrTrnAddDeliveryorderSummary'
  this.service.get(url).subscribe((result: any) => {
    $('#adddeliveryorder_list').DataTable().destroy();
    this.responsedata = result;
    this.adddeliveryorder_list = this.responsedata.adddeliveryorder_list;
    setTimeout(() => {
      $('#adddeliveryorder_list').DataTable();
    }, 1);


  })
  
  
}


  openModalorder(){
    this.router.navigate(['/smr/SmrTrnRaisedeliveryorder'])
  }
  openModaldelete(){}
  openModalamend(){}
  onaddinfo(){}



}
