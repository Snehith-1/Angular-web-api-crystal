import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-pmr-mst-vendorregisterimportexcel',
  templateUrl: './pmr-mst-vendorregisterimportexcel.component.html',
  styleUrls: ['./pmr-mst-vendorregisterimportexcel.component.scss']
})
export class PmrMstVendorregisterimportexcelComponent {
  file!: File;
  reactiveForm!: FormGroup;
  responsedata: any;
  fileInputs: any;
  constructor(public service: SocketService, private ToastrService: ToastrService, private router: Router,public NgxSpinnerService:NgxSpinnerService ) {  }

  onback(){
    this.router.navigate(['/pmr/PmrMstVendorregister']);
  }

  downloadfileformat() {
    debugger;
    // let link = document.createElement("a");
    // link.download = "Purchase VendorExcel";
    //  window.location.href = "https://"+ environment.host + "/Templates/Purchase VendorExcel.xls";
    // link.click();
    let link = document.createElement("a"); 
    link.download = "Vendor Template";
    link.href = "assets/media/Excels/vendorregistertemplate/vendorregister.xlsx";
    link.click();

  }
  onChange1(event: any) {
    this.file = event.target.files[0];
  } 
//   importexcel()
//   {
//     debugger
//    let formData = new FormData();
//    if (this.file != null && this.file != undefined) {
//      window.scrollTo({
//        top: 0, // Code is used for scroll top after event done
//      });
//      formData.append("file", this.file, this.file.name);
//      var api = 'PmrMstVendorRegister/VendorImportExcel'
//      this.service.postfile(api, formData).subscribe((result: any) => {
//        this.responsedata = result;
//        if(result.status == false)
//        {       
//        this.ToastrService.warning()
//        //this.router.navigate(['/pmr/PmrMstVendorRegisterImportExcel'])
//        }
//        else
//        {       
//        this.ToastrService.success();
//        this.router.navigate(['/pmr/PmrMstVendorRegisterImportExcel'])
//        }
//      });
//    }
//  }
importexcel() {

  debugger
 
   let formData = new FormData();
   if (this.file != null && this.file != undefined) {
     window.scrollTo({
       top: 0, // Code is used for scroll top after event done
     });
     formData.append("file", this.file, this.file.name);
     var api = 'PmrMstVendorRegister/VendorImportExcel'
     this.NgxSpinnerService.show();

     this.service.postfile(api, formData).subscribe((result: any) => {
       this.responsedata = result;
       if (result.status == false)
        {

         this.NgxSpinnerService.hide();
         this.ToastrService.warning('Error While Occured Excel Upload')
       }
       else 
       {

         this.NgxSpinnerService.hide();
         // window.location.reload();
         this.fileInputs= null;
         this.ToastrService.success("Excel Uploaded Successfully")

       }
       

     });
   }
 }

}
