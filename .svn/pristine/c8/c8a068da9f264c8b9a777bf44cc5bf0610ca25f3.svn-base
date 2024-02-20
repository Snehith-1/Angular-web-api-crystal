import { Component, ElementRef, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router'; 
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AES , enc} from 'crypto-js';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-pmr-trn-vendorregister-view',
  templateUrl: './pmr-trn-vendorregister-view.component.html',
  styleUrls: ['./pmr-trn-vendorregister-view.component.scss']
})
export class PmrTrnVendorregisterViewComponent {
vendor:any;
vendorregister_gid:any;
ViewVendorregister_list:any;
  constructor(private renderer: Renderer2, private el: ElementRef,public service :SocketService,private ToastrService: ToastrService,private route:Router,private router: ActivatedRoute ) {}

  
  ngOnInit(): void {
    debugger
    const vendorregister_gid = this.router.snapshot.paramMap.get('vendorregister_gid');
    // console.log(termsconditions_gid)
    this.vendorregister_gid = vendorregister_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.vendorregister_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.GetVendorRegisterDetail(deencryptedParam)
   
    }
    GetVendorRegisterDetail(vendorregister_gid: any) {
      debugger
      var url1='PmrMstVendorRegister/GetVendorRegisterDetail'
      let param = {
        vendorregister_gid : vendorregister_gid 
      }
      this.service.getparams(url1, param).subscribe((result: any) => {
        // this.responsedata=result;
        this.ViewVendorregister_list = result.editvendorregistersummary_list;   });
  }}
