import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup,Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-ims-trn-deliveryacknowledgement-update',
  templateUrl: './ims-trn-deliveryacknowledgement-update.component.html',
  styleUrls: ['./ims-trn-deliveryacknowledgement-update.component.scss']
})
export class ImsTrnDeliveryacknowledgementUpdateComponent {
  combinedFormData: FormGroup | any;
  deliverycus_list: any[] = [];
  deliveryorder: any;
  deliverycusprod_list: any[] = [];
  postdelivery_list:any[]=[];
  directorder_gid:any;

  constructor(private http: HttpClient, private fb: FormBuilder,public NgxSpinnerService:NgxSpinnerService, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) {
    this.combinedFormData = new FormGroup({
      remarks : new FormControl(''),
      delivery_by: new FormControl(''),
      delivery_to: new FormControl(''),
      // delivery_date: new FormControl(''),
      delivery_date: new FormControl(this.getCurrentDate(),Validators.required),
    })
  }
  getCurrentDate(): string {
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
    const yyyy = today.getFullYear();
   
    return dd + '-' + mm + '-' + yyyy;
  }
  ngOnInit(): void {
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    flatpickr('.date-picker', options);

    this.combinedFormData = new FormGroup({
      remarks : new FormControl(''),
      delivery_by: new FormControl(''),
      delivery_to: new FormControl(''),
      delivery_date: new FormControl(this.getCurrentDate(),Validators.required),
      directorder_gid:new FormControl('')
      
      
    })

    debugger
    this.deliveryorder = this.route.snapshot.paramMap.get('directorder_gid');
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.deliveryorder, secretKey).toString(enc.Utf8);
    this.GetDeliveryackSummary(deencryptedParam);
    this.directorder_gid =deencryptedParam;
  }
  GetDeliveryackSummary(directorder_gid: any) {
    debugger
    var api = 'ImsTrnDeliveryAcknowledgement/GetDeliveryAcknowledgeUpdate';
    this.NgxSpinnerService.show()
    let params = {

      directorder_gid: directorder_gid,

    }
    this.service.getparams(api, params).subscribe((result: any) => {
      this.deliverycus_list = result.deliverycus_list;
      setTimeout(() => {
        $('#deliverycus_list').DataTable();
      }, 1);
      this.NgxSpinnerService.hide()
    });

    var api = 'ImsTrnDeliveryAcknowledgement/GetDeliveryAcknowledgeUpdateProd';
    let param1 = {

      directorder_gid: directorder_gid,

    }
    this.service.getparams(api, param1).subscribe((result: any) => {
      this.deliverycusprod_list = result.deliverycusprod_list;
      setTimeout(() => {
        $('#deliverycusprod_list').DataTable();
      }, 1);
    });
  }

  OnDelSubmit() {

    debugger
    console.log(this.postdelivery_list)
    var params = {
      directorder_gid:this.directorder_gid,
      delivery_date:this.combinedFormData.value.delivery_date,
      delivery_to:this.combinedFormData.value.delivery_to,
      delivery_by:this.combinedFormData.value.delivery_by,
      remarks:this.combinedFormData.value.remarks,
    }


    var url = 'ImsTrnDeliveryAcknowledgement/PostDeliveryAckSubmit'
    this.NgxSpinnerService.show();
    this.service.post(url, params
      ).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
        this.router.navigate(['/ims/ImsTrnDeliveryOrderAcknowlegement']);

      }
      else {
        this.ToastrService.success(result.message)
        this.router.navigate(['/ims/ImsTrnDeliveryOrderAcknowlegement']);
        this.NgxSpinnerService.hide();

      }
      
    });
  }

}

