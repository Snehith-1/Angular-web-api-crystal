import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AngularEditorConfig } from '@kolkov/angular-editor';
@Component({
  selector: 'app-ims-trn-deliveryorder-view',
  templateUrl: './ims-trn-deliveryorder-view.component.html',
  styleUrls: ['./ims-trn-deliveryorder-view.component.scss']
})
export class ImsTrnDeliveryorderViewComponent {

  config: AngularEditorConfig = {
    editable: false,
    spellcheck: false,
    height: '25rem',
    minHeight: '5rem',
    width: '1000px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };
  deliveryorderview_list:any [] = [];
  deliveryorderview_list1:any [] = [];
  customer: any;
  responsedata: any;


  constructor(private formBuilder: FormBuilder,private route:Router,private router:ActivatedRoute,public service :SocketService) { }

  ngOnInit(): void {
    const directorder_gid =this.router.snapshot.paramMap.get('directorder_gid');    
     this.customer= directorder_gid; 
     const secretKey = 'storyboarderp';
     const deencryptedParam = AES.decrypt(this.customer,secretKey).toString(enc.Utf8);
     console.log(deencryptedParam)
     this.GetImsTrnDeliveryorderSummaryView(deencryptedParam);
     this.GetImsTrnDeliveryorderProductView(deencryptedParam);
  }

  GetImsTrnDeliveryorderSummaryView(directorder_gid: any) {
    var url='ImsTrnDeliveryorderSummary/GetImsTrnDeliveryorderSummaryView'
    let param = {
      directorder_gid : directorder_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.responsedata=result;
    this.deliveryorderview_list = result.deliveryorderview_list;   
    });
  }
  GetImsTrnDeliveryorderProductView(directorder_gid: any) {
    var url='ImsTrnDeliveryorderSummary/GetImsTrnDeliveryorderProductView'
    let param = {
      directorder_gid : directorder_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.responsedata=result;
    this.deliveryorderview_list1 = result.deliveryorderview_list1;   
    });
  }

back(){
  this.route.navigate(['/ims/ImsTrnDeliveryorder']);

}


}
