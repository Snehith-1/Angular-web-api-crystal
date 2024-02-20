import { Component, ElementRef, Renderer2 } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { SafeResourceUrl } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';


interface Ivendorregister {
  vendorregister_gid: string;
  vendor_code: string;
  vendor_companyname: string;
  documenttype_type: string;
  documenttype_name: string;
  department_name:string;
  documenttype_gid:string;
}
@Component({
  selector: 'app-pmr-mst-vendorregisterdocument',
  templateUrl: './pmr-mst-vendorregisterdocument.component.html',
  styleUrls: ['./pmr-mst-vendorregisterdocument.component.scss']
})
export class PmrMstVendorregisterdocumentComponent {
  url: SafeResourceUrl | null = null;
  file!: File;
  vendor: any;
  reactiveForm!: FormGroup;
  vendorregister!: Ivendorregister;
  vendorregister_gid: any;
  DocumentVendorregister_list: any;
  Vendorregister_list: any;
  purchasetype_name: any;
  selectedDocumentType: any;
  responsedata: any;
  Document_list:any;


  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService, private ToastrService: ToastrService, private route: Router, private router: ActivatedRoute,private NgxSpinnerService:NgxSpinnerService) { this.vendorregister = {} as Ivendorregister }


  ngOnInit(): void {
    debugger
    const vendorregister_gid = this.router.snapshot.paramMap.get('vendorregister_gid');
    this.vendorregister_gid = vendorregister_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.vendorregister_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.GetVendorRegisterDetail(deencryptedParam)

    this.reactiveForm = new FormGroup({
      file: new FormControl(''),
      fileExtension: new FormControl(''),
      fileName: new FormControl(''),
      imagePath: new FormControl(''),
      vendorregister_gid: new FormControl(''),
      documenttype_name: new FormControl(''),
      department_name: new FormControl(''),
      documenttype_type: new FormControl('')

    });

  }
  GetVendorRegisterDetail(vendorregister_gid: any) {
    debugger
    this.NgxSpinnerService.show();
    var url1 = 'PmrMstVendorRegister/GetVendorRegisterDetail'
    let param = {
      vendorregister_gid: vendorregister_gid
    }
    this.service.getparams(url1, param).subscribe((result: any) => {
      this.Vendorregister_list = result.editvendorregistersummary_list;
      this.reactiveForm.get("vendorregister_gid")?.setValue(this.Vendorregister_list[0].vendorregister_gid);
      this.NgxSpinnerService.hide();
    });


    // Drop Down
    var api5 = 'PmrMstVendorRegister/GetDocumentType'
    this.service.get(api5).subscribe((result: any) => {
      this.DocumentVendorregister_list = result.GetDocumentType;
      this.reactiveForm.get("documenttype_gid")?.setValue(this.Vendorregister_list[0].documenttype_gid);
    });
    
    debugger
    var url12 = 'PmrMstVendorRegister/GetDocumentdtl'
    let params = {
      vendorregister_gid: vendorregister_gid
    }
    this.service.getparams(url12, params).subscribe((result: any) => {
      this.Document_list = result.GetDocument_list;
      
    });

    //Upload files
    this.reactiveForm = new FormGroup({

      documenttype_name: new FormControl(this.vendorregister.documenttype_name, [
        Validators.required,
      ]),
      file: new FormControl(''),
    })
  }
  get documenttype_name() {
    return this.reactiveForm.get('documenttype_name')!;
  }




  onChange2(event: any) {
    this.file = event.target.files[0];

  }
//   public validate(): void {
//     debugger
//     console.log(this.reactiveForm.value)
// this.vendorregister = this.reactiveForm.value;
//     this.vendorregister = this.reactiveForm.value;
//     if (this.vendorregister.documenttype_name != null && this.vendorregister.documenttype_type != null) {
//       let formData = new FormData();
//       if (this.file != null && this.file != undefined) {
//         formData.append("file", this.file, this.file.name);
//         formData.append("department_name", this.vendorregister.department_name);
//         formData.append("documenttype_name", this.vendorregister.documenttype_name);
//          formData.append("vendorregister_gid", this.vendorregister.vendorregister_gid);
//         // formData.append("designationname", this.vendorregister.designationname);
        

//         var api = 'PmrMstVendorRegister/UpdateVendorAttachUpload'
//         this.service.post(api, formData).subscribe((result: any) => {
//           this.responsedata = result;
//           if (result.status == false) {
//             this.ToastrService.warning(result.message)
//           }
//           else {
//             this.route.navigate(['/PmrMstVendorRegister/PmrMstVendorregister']);
//             this.ToastrService.success(result.message)
//           }
//         });

//       }
//       else {
//         var api7 = 'PmrMstVendorRegister/UpdateVendordetails'
//         this.service.post(api7, this.vendorregister).subscribe((result: any) => {

//           if (result.status == false) {
//             this.ToastrService.warning(result.message)
//           }
//           else {
//             this.route.navigate(['/PmrMstVendorRegister/PmrMstVendorregister']);
//             this.ToastrService.success(result.message)
//           }
//           this.responsedata = result;
//         });
//       }
//     }
//     else {
//       this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
//     }


//     return;


//   }
// // this.GetVendorRegisterDetail(deencryptedParam)
myModaladddetails(vendorregister_gid: string) {
  this.vendorregister_gid = vendorregister_gid
  this.reactiveForm.get("product_gid")?.setValue(this.vendorregister.vendorregister_gid);   

}

  public onsubmit(): void {
    debugger
    console.log(this.reactiveForm.value)
    this.vendorregister = this.reactiveForm.value;
        this.vendorregister = this.reactiveForm.value;
        if (this.vendorregister.documenttype_name != null && this.vendorregister.documenttype_type != null) {
          let formData = new FormData();
          if (this.file != null && this.file != undefined) {
            formData.append("file", this.file, this.file.name);
            // formData.append("department_name", this.vendorregister.department_name);
            formData.append("documenttype_name", this.vendorregister.documenttype_name);
             formData.append("vendorregister_gid", this.vendorregister.vendorregister_gid);
      // this.NgxSpinnerService.show();
      var api7 = 'PmrMstVendorRegister/PostdocumentImage'
      this.service.postfile(api7, formData).subscribe((result: any) => {
        if(result.status ==false){
          // this.NgxSpinnerService.hide();
          this.ToastrService.warning(result.message)
        }
        else{
      
          // this.NgxSpinnerService.hide();
          // this.GetVendorRegisterDetail();
          this.ToastrService.success(result.message)
           window.location.reload();

        }

        this.responsedata = result;
       

      });
      
    }
  }
  else {
          this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
        }
}

downloadFile(file_path: string, file_name: string): void {

  const image = file_path.split('.net/');
  const page = image[1];
  const url = page.split('?');
  const imageurl = url[0];
  const parts = imageurl.split('.');
  const extension = parts.pop();

  this.service.downloadfile(imageurl, file_name+'.'+ extension).subscribe(
    (data: any) => {
      if (data != null) {
        this.service.filedownload1(data);
      } else {
        this.ToastrService.warning('Error in file download');
      }
    },
  );
}
openDownloadLink(file_path: string): void {
  window.open(file_path, '_blank');
}
  
}

