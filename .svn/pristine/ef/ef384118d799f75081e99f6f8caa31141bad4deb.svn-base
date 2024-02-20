import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators,} from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-smr-rpt-salesreport',
  templateUrl: './smr-rpt-salesreport.component.html',
  styleUrls: ['./smr-rpt-salesreport.component.scss']
})
export class SmrRptSalesreportComponent {
  GetMonthwiseOrderReport_List :any;
  GetOrderwiseOrderReport_List : any;
  month :any;
  data: any; 
  salesorder_gid :any; 
  parameterValue: any;
  reactiveForm: FormGroup | any;
  responsedata: any;
  constructor(private formBuilder: FormBuilder,public route:ActivatedRoute,public service :SocketService,private router:Router,private ToastrService: ToastrService) {
    
    
    

  }
  

  ngOnInit(): void {
    this.GetMonthwiseOrderReport();
    this.GetOrderwiseOrderReport();
  }
  GetMonthwiseOrderReport( )
 {
  debugger
  var url = 'SmrRptOrderReport/GetMonthwiseOrderReport'
  this.service.get(url).subscribe((result: any) => {
    $('#GetMonthwiseOrderReport_List').DataTable().destroy();
    this.responsedata = result;
    this.GetMonthwiseOrderReport_List = this.responsedata.GetMonthwiseOrderReport_List;
    setTimeout(() => {
      $('#GetMonthwiseOrderReport_List');
    }, 1);


  })

  
}

  GetOrderwiseOrderReport( )
  {
   debugger
   var url = 'SmrRptOrderReport/GetOrderwiseOrderReport'
   this.service.get(url).subscribe((result: any) => {
     $('#GetOrderwiseOrderReport_List').DataTable().destroy();
     this.responsedata = result;
     this.GetOrderwiseOrderReport_List = this.responsedata.GetOrderwiseOrderReport_List;
     setTimeout(() => {
       $('#GetOrderwiseOrderReport_List');
     }, 1);
 
 
   })
  
  
}

}

