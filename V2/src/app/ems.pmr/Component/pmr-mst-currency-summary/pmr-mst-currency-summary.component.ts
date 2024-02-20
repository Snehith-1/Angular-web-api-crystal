import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
interface ICurrency {
  country_name: string;
  currencyexchange_gid: string;
  exchange_rate:string;
  currency_code:string;
  currency_codeedit:string;
  exchange_rateedit:string;
  country_nameedit:string;
  selectedEntity:any;

}
@Component({
  selector: 'app-pmr-mst-currency-summary',
  templateUrl: './pmr-mst-currency-summary.component.html',
  styleUrls: ['./pmr-mst-currency-summary.component.scss']
})
export class PmrMstCurrencySummaryComponent {
  responsedata: any;
  reactiveForm!: FormGroup;
  reactiveFormEdit!: FormGroup;
  parameterValue1: any;
  parameterValue: any;
  currency_list: any[] = [];
  country_list: any[] = [];
  selectedCountry2: any;

  currency!: ICurrency;

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService,public NgxSpinnerService:NgxSpinnerService,) {
    this.currency = {} as ICurrency;

  }
  ngOnInit(): void {
   this.GetCurrencySummary();
   this.reactiveForm = new FormGroup({

     country_name: new FormControl(this.currency.country_name, [
       Validators.required,

     ]),
    currency_code: new FormControl(this.currency.currency_code, [
      Validators.required,

    ]),
    exchange_rate: new FormControl(this.currency.exchange_rate, [
      Validators.required,

    ]),
   
    
    currencyexchange_gid: new FormControl(''),
    //currency_code: new FormControl(''),



  });
  this.reactiveFormEdit = new FormGroup({

    currency_codeedit: new FormControl(this.currency.currency_codeedit, [
      Validators.required,
    ]),
    exchange_rateedit: new FormControl(this.currency.exchange_rateedit, [
      Validators.required,
    ]),
    country_nameedit: new FormControl(this.currency.country_nameedit, [
      Validators.required,
    ]),

    currencyexchange_gid : new FormControl(''),


  });
  
 
  var api1='PmrMstCurrency/GetPmrCountryDtl'
  this.service.get(api1).subscribe((result:any)=>{
    this.country_list = result.GetPmrCountryDtl;
  
  });
  }
  GetCurrencySummary(){
    var url = 'PmrMstCurrency/GetPmrCurrencySummary'
    this.NgxSpinnerService.show()
    this.service.get(url).subscribe((result: any) => {
      $('#currency_list').DataTable().destroy();
      this.responsedata = result;
      this.currency_list = this.responsedata.currency_list;
      setTimeout(() => {
        $('#currency_list').DataTable();
      }, 1);
      this.NgxSpinnerService.hide()
  
  
    });
  }
  get currency_code(){
    return this.reactiveForm.get('currency_code')!;
  }
  get exchange_rate() {
    return this.reactiveForm.get('exchange_rate')!;
  }
  get currency_codeedit() {
    return this.reactiveFormEdit.get('currency_codeedit')!;
  }
  get exchange_rateedit() {
    return this.reactiveFormEdit.get('exchange_rateedit')!;
  }

  public onsubmit(): void {
    debugger
    if (this.reactiveForm.value.country_name != null && this.reactiveForm.value.exchange_rate != '') {

      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      this.reactiveForm.value;
      var url='PmrMstCurrency/PostPmrCurrency'
      this.NgxSpinnerService.show()
            this.service.post(url,this.reactiveForm.value).subscribe((result:any) => {

              if(result.status ==false){
                this.ToastrService.warning(result.message)
                this.GetCurrencySummary();
              }
              else{
                this.reactiveForm.get("country_name")?.setValue(null);
                this.reactiveForm.get("currency_code")?.setValue(null);
                this.reactiveForm.get("exchange_rate")?.setValue(null);

                this.ToastrService.success(result.message)
                
                
                this.reactiveForm.reset();
                this.GetCurrencySummary();
                this.NgxSpinnerService.hide()
               
              }
              
            });
            
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
    
  }
 

  openModaledit(parameter: string) {
    this.parameterValue1 = parameter
    this.reactiveFormEdit.get("country_nameedit")?.setValue(this.parameterValue1.country_name);
    this.reactiveFormEdit.get("currencyexchange_gid")?.setValue(this.parameterValue1.currencyexchange_gid);
    this.reactiveFormEdit.get("currency_codeedit")?.setValue(this.parameterValue1.currency_code);
    this.reactiveFormEdit.get("exchange_rateedit")?.setValue(this.parameterValue1.exchange_rate);


   
  }
    public onupdate(): void {
    if (this.reactiveFormEdit.value.country_nameedit != null && this.reactiveFormEdit.value.currency_codeedit != null && this.reactiveFormEdit.value.exchange_rateedit != null) {
      for (const control of Object.keys(this.reactiveFormEdit.controls)) {
        this.reactiveFormEdit.controls[control].markAsTouched();
      }
      this.reactiveFormEdit.value;

      //console.log(this.reactiveFormEdit.value)
      var url1 = 'PmrMstCurrency/PmrCurrencyUpdate'
      this.NgxSpinnerService.show()

      this.service.post(url1,this.reactiveFormEdit.value).pipe().subscribe((result:any) =>{
        this.responsedata=result;
        if(result.status ==false){
          this.ToastrService.warning(result.message)
          this.GetCurrencySummary();
          this.NgxSpinnerService.hide()
        }
        else{
          this.ToastrService.success(result.message)
          this.GetCurrencySummary();
          this.NgxSpinnerService.hide()
        }
       
    }); 

    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }
  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  
  }
  ondelete() {
    console.log(this.parameterValue);
    var url = 'PmrMstCurrency/PmrCurrencySummaryDelete'
    this.NgxSpinnerService.show()
    let param = {
      currencyexchange_gid : this.parameterValue 
    }
    this.service.getparams(url,param).subscribe((result: any) => {
      if(result.status ==false){
        this.ToastrService.warning(result.message)
      }
      else{
        this.ToastrService.success(result.message)
      }
      this.GetCurrencySummary();
      this.NgxSpinnerService.hide()
    
  
  
    });
  }
  onclose(){
    this.reactiveForm.reset();
  }
  
 
}
