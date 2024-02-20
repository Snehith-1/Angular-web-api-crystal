import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-smr-trn-commission-setting',
  templateUrl: './smr-trn-commission-setting.component.html',
  styleUrls: ['./smr-trn-commission-setting.component.scss']
})
export class SmrTrnCommissionSettingComponent {


  reactiveForm: FormGroup | any;
  responsedata:any;
  commissionset_list:any[]=[];
  parameterValue1:any;
  
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService) {
    

  }
  ngOnInit(): void{
    this.CommissionSettingSummary();

    this.reactiveForm = new FormGroup ({
      neworder_percentage : new FormControl(''),
      repeatorder_percentage : new FormControl(''),
      sales_type:new FormControl('')
    })
  }


  CommissionSettingSummary(){
    var url = 'SmrCommissionManagement/GetCommissionSettingSummary'
    
    this.service.get(url).subscribe((result: any) => {
    $('#commissionset_list').DataTable().destroy();
     this.responsedata = result;
     this.commissionset_list = this.responsedata.GetCommissionManagement_List;
     //console.log(this.entity_list)
     setTimeout(() => {
       $('#commissionset_list').DataTable()
     }, 1);
     

    });

}
OnsetPercent(data:any){
  debugger
  
    this.reactiveForm.get('sales_type')?.setValue(data.sales_type);
    this.reactiveForm.get('neworder_percentage')?.setValue(data.neworder_percentage);
    this.reactiveForm.get('repeatorder_percentage')?.setValue(data.repeatorder_percentage);
    
}
onupdate(){
  debugger;
  var url = 'SmrCommissionManagement/PostSetPercentage';
  var params={
    sales_type:this.reactiveForm.value.sales_type,
    neworder_percentage: this.reactiveForm.value.neworder_percentage,
    repeatorder_percentage: this.reactiveForm.value.repeatorder_percentage,
  }

  this.service.postparams(url,params).pipe().subscribe((result: any) => {
          this.responsedata = result;
          if (result.status == false) {
            this.ToastrService.warning(result.message);
            this.CommissionSettingSummary();
          } else {
            this.ToastrService.success(result.message);
            this.CommissionSettingSummary();
          }
        });

}
}
