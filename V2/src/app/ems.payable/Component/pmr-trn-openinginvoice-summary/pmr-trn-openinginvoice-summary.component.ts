import { Component } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder } from '@angular/forms';
@Component({
  selector: 'app-pmr-trn-openinginvoice-summary',
  templateUrl: './pmr-trn-openinginvoice-summary.component.html',
  styleUrls: ['./pmr-trn-openinginvoice-summary.component.scss']
})
export class PmrTrnOpeninginvoiceSummaryComponent {
  responsedata: any;
  PblTrnOpeninginvoice_lists: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {
    
  }
  ngOnInit(): void {
    this.GetOpeningInvoiveSummary();
  }
  GetOpeningInvoiveSummary(){
    debugger
    var url='PmrTrnOpeningInvoice/GetOpeningInvoiveSummary'
    
      
    this.service.get(url).subscribe((result:any)=>{
      $('#PblTrnOpeninginvoice_lists').DataTable().destroy();
      this.responsedata=result;
      this.PblTrnOpeninginvoice_lists = this.responsedata.openinginvoice_list;  
      setTimeout(()=>{   
        $('#PblTrnOpeninginvoice_lists').DataTable();
      }, 1);
    
     
  });
}
openModaldelete(){}
openModaledit(){}
}
