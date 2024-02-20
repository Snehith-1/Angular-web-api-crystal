import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators,} from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { AES } from 'crypto-js';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-smr-trn-quotation-summary',
  templateUrl: './smr-trn-quotation-summary.component.html',
  styleUrls: ['./smr-trn-quotation-summary.component.scss']
})
export class SmrTrnQuotationSummaryComponent {

  quoteform:FormControl | any;
  responsedata: any;
  quotation_list: any[] = [];
  product_list: any[] = [];
  quotrefnolist:any[] = [];
  getData: any;
  company_code: any;
  parameterValue1: any;
  parameterValue: any;
  remarks: any;
  quotation_gid: any;
  constructor(private formBuilder: FormBuilder,public NgxSpinnerService:NgxSpinnerService,public service :SocketService,private route:Router,private ToastrService: ToastrService) {

    this.quoteform = new FormGroup ({
      quotation_date: new FormControl(''),
      customer_name: new FormControl(''),
      quotation_referenceno1: new FormControl(''),
      quotation_gid: new FormControl(''),
      
      
      
      
    });
    

  }
  ngOnInit(): void {
    this.GetSmrTrnQuotation();
}

		  PrintPDF(quotation_gid: any) {
    // API endpoint URL
    const api = 'SmrTrnQuotation/GetQuotationRpt';
    this.NgxSpinnerService.show()
    let param = {
      quotation_gid:quotation_gid
    } 
    this.service.getparams(api,param).subscribe((result: any) => {
      if(result!=null){
        this.service.filedownload1(result);
      }
      this.NgxSpinnerService.hide()
    });
  }
 


//// Summary Grid//////
GetSmrTrnQuotation() {
  debugger
  var url = 'SmrTrnQuotation/GetSmrTrnQuotation'
  this.service.get(url).subscribe((result: any) => {
    $('#quotation_list').DataTable().destroy();
    this.responsedata = result;
    this.quotation_list = this.responsedata.quotation_list;
    setTimeout(() => {
      $('#quotation_list').DataTable();
    }, 1);


  })
  
  
}

GetProductdetails() {
  debugger
  var url = 'SmrTrnQuotation/GetProductdetails'
  this.service.get(url).subscribe((result: any) => {
    $('#product_list').DataTable().destroy();
    this.responsedata = result;
    this.product_list = this.responsedata.product_list;
    setTimeout(() => {
      $('#product_list').DataTable();
    }, 1);


  })
}


Mail(params : string)
  {
    debugger;
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/smr/SmrTrnQuotationmail',encryptedParam])
  }
  
  openModaledit(){}
  onview(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const lspage = 'SrmTrnNewquotationview'
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/smr/SrmTrnNewquotationview',encryptedParam, lspage]) 
  }
  
  openModalamend(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/smr/SmrTrnAmendQuotation',encryptedParam]) 
  }
onaddinfo(){}
onadd(params: any){
  const secretKey = 'storyboarderp';

  const param = (params);

  const encryptedParam = AES.encrypt(param,secretKey).toString() ;
  this.route.navigate(['/smr/SmrTrnQuoteToOrder',encryptedParam]);
}
Details(parameter: string,quotation_gid: string){
  this.parameterValue1 = parameter;
  this.quotation_gid = parameter;

  var url='SmrTrnQuotation/GetProductdetails'
    let param = {
      quotation_gid : quotation_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.responsedata=result;
    this.product_list = result.product_list;   
    });
  
}

get quotation_referenceno1() {
  return this.quoteform.get('quotation_referenceno1')!;
}
///Change Quotation Reference No///
GetQuoteRefNodetails(){
  debugger
  var url ='SmrTrnQuotation/GetQuotRefNodetails'
  this.service.get(url).subscribe((result: any) => {
    $('#quotrefnolist').DataTable().destroy();
    this.responsedata = result;
    this.quotrefnolist = this.responsedata.quotrefnolist;
    setTimeout(() => {
      $('#quotrefnolist').DataTable();
    }, 1);

});
}


QuotRefNopopup(){
  

  // var url='SmrTrnQuotation/GetQuotRefNodetails'
  // let param = {
  //   quotation_gid:quotation_gid
  // }
  // this.service.getparams(url,param).subscribe((result:any)=>{
  //   this.responsedata=result;
  //   this.quotrefnolist =result.quotrefnolist
  //   this.quoteform.get("quotation_referenceno1")?.setValue(this.quotrefnolist[0].quotation_referenceno1);
  //   // this.quoteform.get("quotation_gid")?.setValue(this.quotrefno_list[0].quotation_gid);  
  // });

}

onhistory(params:any){
  const secretKey = 'storyboarderp';
  const param = (params);
  const encryptedParam = AES.encrypt(param,secretKey).toString();
  this.route.navigate(['/smr/SmrTrnQuotationHistory',encryptedParam]) 
}


openModaldelete(param: string){
this.parameterValue= param
}

onupdate(){
// debugger
//   var params = {
//     quotation_referenceno1:this.quoteform.value.quotation_referenceno1,
//     quotation_gid:this.quoteform.value.quotation_gid,
//     customer_name:this.quoteform.value.customer_name,
//     quotation_date:this.quoteform.value.quotation_date

//   }
//       var url = 'SmrTrnQuotation/PostUpdatedQuotationRefno'

//       this.service.postparams(url,params).subscribe((result:any)=>{
//         this.responsedata=result;
//         this.quotrefnolist =result.quotrefnolist
//         this.quoteform.get("quotation_referenceno1")?.setValue(this.quotrefnolist[0].quotation_referenceno1);

//         if(result.status ==false){
//           this.ToastrService.warning(result.message)
          
//         }
//         else{
//           this.ToastrService.success(result.message)
         
//         }
//     }); 

  }
  

  onclose(){}
}









