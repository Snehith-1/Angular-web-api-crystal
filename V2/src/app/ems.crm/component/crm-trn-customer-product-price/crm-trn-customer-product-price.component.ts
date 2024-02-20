import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service'; 
import { ToastrService } from 'ngx-toastr';
import { RouterTestingHarness } from '@angular/router/testing';
import { AES, enc } from 'crypto-js';
import { Subscription, map, share, timer } from 'rxjs';
import { DatePipe } from '@angular/common';
import { NgxSpinnerService } from 'ngx-spinner';
interface  ICustomer

{

}
@Component({
  selector: 'app-crm-trn-customer-product-price',
  templateUrl: './crm-trn-customer-product-price.component.html',
  styleUrls: ['./crm-trn-customer-product-price.component.scss']
})
export class CrmTrnCustomerProductPriceComponent {
  @ViewChild('Inbox') tableRef!: ElementRef;
  time = new Date();
  rxTime = new Date();
  intervalId: any;
  subscription!: Subscription;
  currentDayName: any;
  fromDate: any; toDate: any;
  salesproduct_list: any[] = [];
  saleproduct_list: any[] = [];
  cusprodForm!: FormGroup;
  customer!: ICustomer;
  productasgn: any;
  customer_name:any;
  customer_type:any;
  responsedata: any;
  customer_gid:any;
  data: any;
  isref:any;
  isdata:any;
  mrp_price: number = 0;
  cost_price: number = 0;
  selling_price : number = 0;
  discamount: number = 0;
amount: number = 0;
lead2campaign_gid: any;
leadbankcontact_gid: any;
lspage: any;
lspage1: any;
leadbank_gid:any

  constructor( private el: ElementRef, public service: SocketService, private ToastrService: ToastrService, private formBuilder: FormBuilder,public NgxSpinnerService:NgxSpinnerService,private datePipe: DatePipe,  private route: Router, private router: ActivatedRoute) {
    this.customer = {} as ICustomer;
    const today = new Date();
    this.currentDayName = today.toLocaleDateString('en-US', { weekday: 'long' });
  }
  ngOnInit(): void {

    this.cusprodForm = new FormGroup ({
      cost_price:new FormControl (''),
      selling_price:new FormControl (''),
      customer_name:new FormControl(''),
      customer_gid:new FormControl(''),
      amount : new FormControl (''),
      amount1 : new FormControl (''),

      discamount : new FormControl (''),
      saleproduct_list:  this.formBuilder.array([0]),
    });
debugger
    this.productasgn = this.router.snapshot.paramMap.get('customer_gid');
    const leadbank_gid = this.router.snapshot.paramMap.get('leadbank_gid');
    const leadbankcontact_gid = this.router.snapshot.paramMap.get('leadbankcontact_gid');
    const lead2campaign_gid = this.router.snapshot.paramMap.get('lead2campaign_gid');
    const lspage = this.router.snapshot.paramMap.get('lspage');
    this.leadbank_gid = leadbank_gid;
    this.leadbankcontact_gid = leadbankcontact_gid;
    this.lead2campaign_gid = lead2campaign_gid;
    this.lspage = lspage;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.productasgn, secretKey).toString(enc.Utf8);
    const deencryptedParam3 = AES.decrypt(this.lspage, secretKey).toString(enc.Utf8);
    this.lspage1 = deencryptedParam3;
    this.GetSmrTrnProductAssignSummary(deencryptedParam);
    this.customer_gid=deencryptedParam;
   console.log(deencryptedParam);

   let yesterday = new Date();
   yesterday.setDate(yesterday.getDate() - 1);
   this.fromDate = this.datePipe.transform(yesterday, 'dd-MM-yyyy');
   this.toDate = this.datePipe.transform(new Date(), 'dd-MM-yyyy');

   this.intervalId = setInterval(() => {
     this.time = new Date();
   }, 1000);
   // Using RxJS Timer
   this.subscription = timer(0, 1000)
     .pipe(
       map(() => new Date()),
       share()
     )
     .subscribe(time => {
       let hour = this.rxTime.getHours();
       let minuts = this.rxTime.getMinutes();
       let seconds = this.rxTime.getSeconds();
       //let a = time.toLocaleString('en-US', { hour: 'numeric', hour12: true });
       let NewTime = hour + ":" + minuts + ":" + seconds
       this.rxTime = time;
     });
  }

  GetSmrTrnProductAssignSummary(customer_gid: any) {       
  debugger
  this.isdata = true;
  this.isref = false;
    let param = {
      customer_gid: customer_gid,
    }
    var url = 'SmrTrnCustomerSummary/GetProductAssignSummary'
    this.service.getparams(url,param).subscribe((result: any) => {
      $('#salesproduct_list').DataTable().destroy();
     this.responsedata=result;
     this.salesproduct_list = result.product_list
     for(let i=0;i<this.salesproduct_list.length;i++){
      this.cusprodForm.addControl(`selling_price_${i}`, new FormControl(this.salesproduct_list[i].selling_price));
    }

     setTimeout(() => {
      $('#salesproduct_list').DataTable();
    }, 1);
    });
    var url = 'SmrTrnCustomerSummary/Getcustomername'
    this.service.getparams(url,param).subscribe((result: any) => {
      this.saleproduct_list = result.GetCustomerList
      this.customer_name=this.saleproduct_list[0].customer_name
      this.customer_type=this.saleproduct_list[0].customer_type
    });
  }
  // openModaledit(data: any){
  //   debugger
  //   this.salesproduct_list.forEach(element => {
  //   element.isEdit = false;
  //   data.originalProductPrice = data.cost_price;
  //   data.originalProductPrice = data.selling_price;
  //  });
  //  data.isEdit = true;    
  // }
  // onclose(data: any){  
  //   if (data.isEdit) {
  //     data.isEdit = false;
  //      this.GetSmrTrnProductAssignSummary(this.customer_gid)
  //   }
  // }
  onchangeamount()
  {
    debugger
if(this.cusprodForm.value.amount1=="Discount")
{
  for (let i=0;i<this.salesproduct_list.length;i++)
  {
    let price =this.salesproduct_list[i].product_price;
    let amount=this.cusprodForm.value.amount;
    let total=((Number(price))*Number(amount)/100)
    let onselling_price = Number(price) - Number(total);
    this.salesproduct_list[i].selling_price=onselling_price.toFixed(2);
    this.isdata = false;
    this.isref = true;

  }

}
else if (this.cusprodForm.value.discamount=="Margin")
{
  for (let i=0;i<this.salesproduct_list.length;i++)
    {
      let price1 =this.salesproduct_list[i].cost_price;
      let amount=this.cusprodForm.value.amount;
      let total1=((Number(price1))*Number(amount)/100)
      let oncost_price = Number(price1) + Number(total1);
      this.salesproduct_list[i].cost_price=oncost_price.toFixed(2);
      this.isdata = false;
      this.isref = true;

    }

}

    
//     for (let i=0;i<this.salesproduct_list.length;i++)
//     {
//       let price =this.salesproduct_list[i].product_price;
//       let amount=this.cusprodForm.value.amount;
//       let total=((Number(price))*Number(amount)/100)
//       let onselling_price = Number(price) - Number(total);
//       this.salesproduct_list[i].selling_price=onselling_price.toFixed(2);

//       let price1 =this.salesproduct_list[i].cost_price;
//       let discamount=this.cusprodForm.value.discamount;
//       let total1=((Number(price1))*Number(discamount)/100)
//       let oncost_price = Number(price1) + Number(total1);
//       this.salesproduct_list[i].cost_price=oncost_price.toFixed(2);
//       this.isdata = false;
//       this.isref = true;
// }
  }
  // edupdate(data:any,i:number)
  // {
  //   debugger
  // if(this.salesproduct_list[i].pricesegment2product_gid==null)
  //     {
  //      this.salesproduct_list[i].selling_price=this.cusprodForm.value.selling_price;
  //      this.salesproduct_list[i].cost_price=this.cusprodForm.value.cost_price;
  //      if (data.isEdit) {
  //        data.isEdit = false;}
  //      }
  // else{
  //     var url = 'SmrTrnCustomerSummary/Customerpriceupdate';
  //     const param = {        
  //       salesproduct_list:this.salesproduct_list,
  //        customer_gid:this.customer_gid
  //     };
  //     debugger;
  //     console.log(param)
  //     this.NgxSpinnerService.show();
  //     this.service.postparams(url, param).subscribe((result: any) => {   
  //       this.NgxSpinnerService.hide();
  //       if (result.status === false) {
  //         this.ToastrService.warning(result.message);
  //       } else {
  //         this.ToastrService.success(result.message);
  //         this.GetSmrTrnProductAssignSummary(this.customer_gid)
  //       }
  //     });
  //   }
  // }
  onSubmit()
  {
    var url = 'SmrTrnCustomerSummary/Customerprice';
      const param = {        
        salesproduct_list:this.salesproduct_list,
         customer_gid:this.customer_gid
      };
      debugger;
      console.log(param)
      this.NgxSpinnerService.show();
      this.service.postparams(url, param).subscribe((result: any) => {   
        this.NgxSpinnerService.hide();
        if (result.status === false) {
          this.ToastrService.warning(result.message);
        } else {
          this.ToastrService.success(result.message);
          if (this.lspage1 == 'MM-Total') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'MM-Upcoming') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'MM-Lapsed') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'MM-Longest') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'MM-New') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'MM-Prospect') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'MM-Potential') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'MM-mtd') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'MM-ytd') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'MM-Customer') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'MM-Drop') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'My-Today') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'My-New') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'My-Prospect') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'My-Potential') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'My-Customer') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'My-Drop') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'My-All') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else if (this.lspage1 == 'My-Upcoming') {
            this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
          }
          else {
            this.route.navigate(['/smr/SmrTrnCustomerSummary']);
          }
                }
      });
  }
  
  onclose()
{

  if (this.lspage1 == 'MM-Total') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'MM-Upcoming') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'MM-Lapsed') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'MM-Longest') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'MM-New') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'MM-Prospect') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'MM-Potential') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'MM-mtd') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'MM-ytd') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'MM-Customer') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'MM-Drop') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'My-Today') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'My-New') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'My-Prospect') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'My-Potential') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'My-Customer') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'My-Drop') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'My-All') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else if (this.lspage1 == 'My-Upcoming') {
    this.route.navigate(['/crm/CrmTrn360view', this.leadbank_gid, this.lead2campaign_gid, this.lspage]);
  }
  else {
    this.route.navigate(['/smr/SmrTrnCustomerSummary']);
  }
}

onchangerefresh()
{
  location.reload();
  this.isref = false;
  this.isdata = true;
 
}






  }