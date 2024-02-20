import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { AES, enc } from 'crypto-js';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';

 interface quotationadd{
  
  quotation_gid: string;
 salesorder_date: string;
  branch_name: string;
  branch_gid: string;
  so_referencenumber: string;
  vendor_gid: string;
  vendor_name: string;
  customer_contact_gid: string;
  contactperson_name: string;
  contact_telephonenumber: string;
  email_id: string;
  salesperson_gid: string;
  user_name: string;
  contact: string;
  shipping_to: string;
  so_remarks: string;
  start_date: string;
  end_date: string;
  freight_terms: string;
  payment_terms: string;
  currencyexchange_gid: string;
  currency_code: string;
  exchange_rate: string;
  productgroup_gid: string;
  productgroup_name: string;
  customerproduct_code: string;
  product_code: string;
  product_gid: string;
  product_name: string;
  display_field: string;
  qty_quoted: string;
  mrp_price: string;
  product_price: string;
  tax1_gid: string;
  tax_name: string;
  tax_amount: string;
  tax2_gid: string;
  tax_name2: string;
  tax_amount2: string;
  tax3_gid: string;
  tax_name4: string;
  tax_amount4: string;
  price: string;
  quotation_remarks :String;
  product_requireddate: string;
  product_requireddateremarks: string;
  template_content: string;

}
@Component({
  selector: 'app-pmr-trn-purchase-quotation',
  templateUrl: './pmr-trn-purchase-quotation.component.html',
  styleUrls: ['./pmr-trn-purchase-quotation.component.scss']
})
export class PmrTrnPurchaseQuotationComponent {


showInput: boolean = false;
  inputValue: string = ''
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '25rem',
    minHeight: '5rem',
    width: '1150px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };
  combinedFormData: FormGroup | any;
    template_content : FormGroup |any;
    productform: FormGroup | any;
    branch_list : any [] = [];
    vendor_list: any [] = [];
    contact_list: any [] = [];
    user_list : any[] =[];
    sales_list: any [] = [];
    currency_list: any [] = [];
    product_list: any [] = [];
    productgroup_list: any [] = [];
    productname_list: any [] = [];
    tax_list: any [] = [];
    tax2_list: any [] = [];
    tax3_list: any [] = [];
    tax4_list: any [] = [];
    QAproductlist: any [] = [];
    terms_list: any[] = [];
    currency_list1: any[] = [];
    mdlTerms: any;
    mdlBranchName:any;
    mdlbranchname :any;
    GetCustomerDet:any;
    mdlVendorName:any;
    mdlUserName:any;
    mdlCurrencyName :any;
    mdlProductName:any;
    mdlTaxName3:any;
    mdlTaxName2:any;
    mdlTaxName1:any;
    taxamount3: number = 0;
    taxamount2: number = 0;
    taxamount1: number = 0;
    grandtotal: number = 0;
    totalamount: number = 0;
    tax_amount: number = 0;
    tax_amount2: number = 0;
    tax_amount3: number = 0;
    taxpercentage: any;
    discount_amount: any;
    discount_percentage: number = 0;
    GetproductsCode:any;
    qty_quoted: number = 0;
    unitprice: number = 0;
    responsedata: any;
    quotationadd! : quotationadd;
    addon_charge: number = 0;
    freightcharges: number = 0;
    buybackcharges: number = 0;
    insurance_charges: number = 0;
    roundoff: number = 0;
    additional_discount: number = 0;
    producttotalamount: any;
    mdlTaxName4: any;
    tax_amount4: any;
  parameterValue: any; 
  cuscontact_gid:any;
  total_amount: number = 0;
  leadbank_gid: any;
  leadgig: any;
  quotation: any;
  GetVendord: any;
  productdetails_list: any;
  vendor_gid: any;
  packing_charges: any;
 
  
  
  constructor(private formBuilder: FormBuilder,private route:Router, private ToastrService: ToastrService, public service: SocketService, private router: ActivatedRoute) {
  this.quotationadd = {} as quotationadd;
  this.combinedFormData = new FormGroup ({
        quotation_date: new FormControl(this.getCurrentDate()),
        branch_name: new FormControl(''),
        vendor_gid:new FormControl(''),
        quotation_referenceno1: new FormControl(''),
        branch_gid: new FormControl(''),
        customer_gid: new FormControl(''),
        quotation_gid: new FormControl(''),
        vendor_companyname: new FormControl(''),
        contactperson_name: new FormControl(''),
        contact_telephonenumber: new FormControl(''),
        email_id: new FormControl(''),
        user_gid: new FormControl(''),
        user_name: new FormControl(''),
        address1: new FormControl(''),
        quotation_remarks: new FormControl(''),
        payment_terms: new FormControl(''),
        currencyexchange_gid: new FormControl(''),
        currency_code: new FormControl(''),
        exchange_rate: new FormControl(''),
        productgroup_gid: new FormControl(''),
        productgroup_name: new FormControl(''),
        customerproduct_code: new FormControl(''),
        product_code: new FormControl(''),
        product_gid: new FormControl(''),
        product_name: new FormControl(''),
        product_requireddate: new FormControl(''),
        product_requireddateremarks: new FormControl(''),
        totalamount: new FormControl(''),
        grandtotal:new FormControl(''),
        termsandconditions: new FormControl(''),
        quotation_referencenumber: new FormControl(''),
        template_name: new FormControl(''),
        roundoff: new FormControl(''),
        insurance_charges: new FormControl(''),
        packing_charges: new FormControl(''),
        buybackcharges: new FormControl(''),
        freightcharges: new FormControl(''),
        additional_discount: new FormControl(''),
        addon_charge: new FormControl(''),
        total_amount: new FormControl(''),
        tax_amount4: new FormControl(''),
        tax_name4: new FormControl(''),
        producttotalamount: new FormControl(''),
        delivery_days: new FormControl(''),
        payment_days: new FormControl(''),
        address_gid: new FormControl(''),
        qty_quoted:new FormControl(''),
        shipping_to:new FormControl(''),        
        
      });
      
        this.productform = new FormGroup({
        productgroup_gid: new FormControl(''),
        quotation_gid :new FormControl(''),
        productgroup_name: new FormControl(''),
        customerproduct_code: new FormControl(''),
        product_code: new FormControl(''),
        product_gid: new FormControl(''),
        product_name: new FormControl(''),
        display_field: new FormControl(''),
        qty_quoted: new FormControl(''),
        selling_price: new FormControl(''),
        tax_gid: new FormControl(''),
        tax_name: new FormControl(''),
        tax_amount: new FormControl(''),
        tax_name2: new FormControl(''),
        tax_amount2: new FormControl(''),
        tax_name4: new FormControl(''),
        tax_amount4: new FormControl(''),
        totalamount: new FormControl(''),
        product_uom: new FormControl(''),
        productuom_name: new FormControl('', Validators.required),
        product_requireddate: new FormControl(''),
        product_requireddateremarks: new FormControl(''),
      unitprice: new FormControl('', Validators.required),
      discount_percentage: new FormControl('', Validators.required),
      discount_amount: new FormControl('', Validators.required),
      taxname: new FormControl('', Validators.required),
      taxname2: new FormControl('', Validators.required),
      taxname3: new FormControl('', Validators.required),     
    });
    }
  
    ngOnInit(): void {

      {
        const options: Options = {
          dateFormat: 'd-m-Y' ,    
        };
        flatpickr('.date-picker', options); }
      
    
      var api = 'PmrTrnPurchaseOrder/GetBranch';
      this.service.get(api).subscribe((result: any) => {
        this.responsedata = result;
        this.branch_list = this.responsedata.GetBranch;
  
      });
      var api = 'PmrTrnPurchaseOrder/GetProduct';
      this.service.get(api).subscribe((result: any) => {
        this.responsedata = result;
        this.productdetails_list = this.responsedata.GetProduct
      });
      var api = 'PmrTrnPurchaseQuotation/GetVendor';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.vendor_list = this.responsedata.Vendor_list;

    });
     var api = 'PmrTrnPurchaseQuotation/GetCurrencyDtl';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.currency_list = this.responsedata.GetCurrencyCodeDropdown;

    });
    var api = 'PmrTrnPurchaseOrder/GetTax';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.tax_list = this.responsedata.GetTax;

    });

    var api = 'PmrTrnPurchaseQuotation/GetTermsandConditions';
      this.service.get(api).subscribe((result: any) => {
        this.responsedata = result;
        this.terms_list = this.responsedata.GetTermsandConditions
      });
     var api = 'PmrTrnPurchaseOrder/GetProduct';
     this.service.get(api).subscribe((result: any) => {
       this.responsedata = result;
       this.productdetails_list = this.responsedata.GetProduct;
 
       setTimeout(() => {
 
         $('#product_list').DataTable();
 
       }, 0.1);
     });
     

}
getCurrentDate(): string {
  const today = new Date();
  const dd = String(today.getDate()).padStart(2, '0');
  const mm = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
  const yyyy = today.getFullYear();
 
  return dd + '-' + mm + '-' + yyyy;
}
  
   get branch_name() {
    return this.combinedFormData.get('branch_name')!;
    }
    get vendor_companyname() {
      return this.combinedFormData.get('vendor_companyname')!;
      }
      get quotation_referencenumber() {
        return this.combinedFormData.get('quotation_referencenumber')!;
        }
      get product_name() {
        return this.productform.get('product_name')!;
      }
      get product_code() {
        return this.productform.get('product_code')!;
      }
    get tax_name(){
      return this.productform.get('tax_name')!;
    }
    get tax_name4(){
      return this.productform.get('tax_name4')!;
    }
    get tax_name3(){
      return this.productform.get('tax_name3')!;
    }
    get currency_code(){
      return this.productform.get('currency_code')!;
    }
    get payment_days(){
      return this.combinedFormData.get('payment_days')!;
    }
    get delivery_days(){
      return this.combinedFormData.get('delivery_days')!;
    } 
    OnChangeVendor(){
debugger
      let vendor_gid = this.combinedFormData.value.vendor_companyname;
      let param ={
        vendor_gid :vendor_gid
      }
      var url = 'PmrTrnPurchaseQuotation/GetOnChangeVendor';
      
        this.service.getparams(url, param).subscribe((result: any) => {
        this.responsedata = result;
        //this.GetVendord = this.responsedata.GetVendordtl;
        this.combinedFormData.get("contact_telephonenumber")?.setValue(result.GetVendordtl[0].contact_telephonenumber);
        this.combinedFormData.get("contactperson_name")?.setValue(result.GetVendordtl[0].contactperson_name);
        this.combinedFormData.get("address1")?.setValue(result.GetVendordtl[0].address1);
        this.combinedFormData.get("email_id")?.setValue(result.GetVendordtl[0].email_id);
        this.combinedFormData.value.vendor_gid = result.GetVendordtl[0].vendor_gid;
        console.log(result.GetVendordtl[0])
      });
  
    }
    

      GetOnChangeProductName(){
        debugger;
        let product_gid = this.productform.value.product_name.product_gid;
        let param = {
          product_gid: product_gid
        }
        var url = 'PmrTrnPurchaseOrder/GetOnChangeProductName';
        this.service.getparams(url, param).subscribe((result: any) => {
          this.responsedata = result;
          this.GetproductsCode = this.responsedata.ProductsCode;
          this.productform.get("product_code")?.setValue(result.GetProductCode[0].product_code);
          this.productform.get("productuom_name")?.setValue(result.GetProductCode[0].productuom_name);
          this.productform.get("productgroup_name")?.setValue(result.GetProductCode[0].productgroup_name);
          this.productform.value.productgroup_gid = result.GetProductCode[0].productgroup_gid
           // this.productform.value.productuom_gid = result.GetProductsCode[0].productuom_gid
        });

      }

      getDimensionsByFilter(id: any) {
        return this.tax_list.filter((x: { tax_gid: any; }) => x.tax_gid === id);
      }
    
      prodtotalcal() {
        const subtotal = this.unitprice * this.qty_quoted;
        this.discount_amount = (subtotal * this.discount_percentage) / 100;
        this.totalamount = subtotal - this.discount_amount;
      }
     
      OnChangeTaxAmount1() {
        let tax_gid = this.productform.get('tax_name')?.value;
     
        this.taxpercentage = this.getDimensionsByFilter(tax_gid);
        console.log(this.taxpercentage);
        let tax_percentage = this.taxpercentage[0].percentage;
        console.group(tax_percentage)
     
        this.tax_amount = ((tax_percentage) * (this.totalamount) / 100);
     
        if (this.tax_amount == undefined) {
          const subtotal = this.unitprice * this.qty_quoted;
          this.discount_amount = (subtotal * this.discount_percentage) / 100;
          this.totalamount = subtotal - this.discount_amount;
        }
        else {
          this.totalamount = ((this.totalamount) + (+this.tax_amount));
        }
      }
    finaltotal(){
      this.grandtotal = (+this.producttotalamount) +(+this.addon_charge) + (+this.additional_discount);
    }
  

    GetOnChangeTerms() {
      debugger
  
      let termsconditions_gid = this.combinedFormData.value.template_name;
      let param = {
        termsconditions_gid: termsconditions_gid
      }
      var url = 'PmrTrnPurchaseQuotation/GetOnChangeTerms';
      this.service.getparams(url, param).subscribe((result: any) => {
        this.combinedFormData.get("termsandconditions")?.setValue(result.terms_list[0].termsandconditions);
        this.combinedFormData.value.termsconditions_gid = result.terms_list[0].termsconditions_gid
        //this.cusraiseform.value.productuom_gid = result.GetProductsName[0].productuom_gid
      });

      }


  
    openModaldelete(parameter: string){
  this.parameterValue = parameter
    }
    Quotationproductsummary() {
      var api = 'PmrTrnPurchaseQuotation/GetTempProductsSummary';
      this.service.get(api).subscribe((result: any) => {
        this.responsedata = result;
        this.QAproductlist = this.responsedata.prodsummary_list;
        
     this.combinedFormData.get("producttotalamount")?.setValue(this.responsedata.total_amount);
     this.combinedFormData.get("grandtotal")?.setValue(this.responsedata.ltotalamount);

        
      });
    }
    productSubmit(){
      debugger
      console.log(this.productform.value)
      var params = {
        quotation_gid: this.productform.value.quotation_gid,
        quotationdtl_gid: this.productform.value.quotationdtl_gid,
        product_name: this.productform.value.product_name.product_name,
        product_gid: this.productform.value.product_name.product_gid,
        selling_price: this.productform.value.selling_price,
        tax_name: this.productform.value.tax_name,
        tax_amount: this.productform.value.tax_amount,
        discount_amount: this.productform.value.discount_amount,
        discount_percentage: this.productform.value.discount_percentage,
        unitprice: this.productform.value.unitprice,
        productgroup_gid: this.productform.value.productgroup_gid,
        productgroup_name: this.productform.value.productgroup_name,
        product_code: this.productform.value.product_code,
        productuom_gid: this.productform.value.productuom_gid,
        productuom_name: this.productform.value.productuom_name,
        totalamount:this.productform.value.totalamount,
        producttotalamount:this.productform.value.producttotalamount,
        qty_quoted:this.productform.value.qty_quoted,
        //quotation_referencenumber:this.productform.value.quotation_referencenumber,
  
      }
      console.log(params)
      var api = 'PmrTrnPurchaseQuotation/PostAddProduct';
      this.service.post(api, params).subscribe((result: any) => {
        if(result.status ==false){
          this.ToastrService.warning(result.message)
        }
        else{
          this.ToastrService.success(result.message)
        }
      this.Quotationproductsummary();
      this.productform.reset();
      },
      );
    } 
  OnChangeCurrency(){
    debugger
    let currencyexchange_gid = this.combinedFormData.get("currency_code")?.value;
    console.log(currencyexchange_gid)
    let param = {
      currencyexchange_gid: currencyexchange_gid
    }
    var url = 'PmrTrnPurchaseQuotation/GetOnChangeCurrency';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.currency_list1 = this.responsedata.GetOnchangecurrency;
      this.combinedFormData.get("exchange_rate")?.setValue(this.currency_list1[0].exchange_rate);
      
    });
  }
    ondelete() {    
      var url = 'PmrTrnPurchaseQuotation/GetDeleteDirectPOProductSummary'
      let param = {
        quotationdtl_gid : this.parameterValue 
      }
      this.service.getparams(url,param).subscribe((result: any) => {
        if(result.status ==false){
          this.ToastrService.warning(result.message)
        }
        else{
          
          this.ToastrService.success(result.message)
          this.Quotationproductsummary();          
        }     
        });
    }
  
  OnSubmit()  
  {
    debugger
    console.log(this.combinedFormData.value)
   
      var params = {
        quotation_referenceno1: this.combinedFormData.value.quotation_referenceno1,
        branch_gid: this.combinedFormData.value.branch_name.branch_gid,
        branch_name:this.combinedFormData.value.branch_name,
        quotation_date: this.combinedFormData.value.quotation_date,
        quotation_gid: this.combinedFormData.value.quotation_gid,
        vendor_companyname: this.combinedFormData.value.vendor_companyname,
        vendor_gid: this.combinedFormData.vendor_gid,
        contactperson_name: this.combinedFormData.value.contactperson_name,
        contact_telephonenumber: this.combinedFormData.value.contact_telephonenumber,
        email_id: this.combinedFormData.value.email_id,
        quotation_remarks: this.combinedFormData.value.quotation_remarks,
        address1: this.combinedFormData.value.address1,
        exchange_rate: this.combinedFormData.value.exchange_rate,
        currency_code: this.combinedFormData.value.currency_code,
        freight_terms: this.combinedFormData.value.freight_terms,
        payment_terms: this.combinedFormData.value.payment_terms,
        user_name: this.combinedFormData.value.user_name, 
        payment_days: this.combinedFormData.value.payment_days,
        customer_gid: this.combinedFormData.value.customer_name,
        termsandconditions: this.combinedFormData.value.termsandconditions,
        template_name: this.combinedFormData.value.template_name,
        template_gid: this.combinedFormData.value.template_gid,
        grandtotal: this.combinedFormData.value.grandtotal,
        additional_discount: this.combinedFormData.value.additional_discount,
        addon_charge: this.combinedFormData.value.addon_charge,
        total_amount: this.combinedFormData.value.total_amount,
        tax_amount4: this.combinedFormData.value.tax_amount4,
        tax_name4: this.combinedFormData.value.tax_name4,
        producttotalamount: this.combinedFormData.value.producttotalamount,
        delivery_days: this.combinedFormData.value.delivery_days,
        quotation_referencenumber: this.combinedFormData.value.quotation_referencenumber,
        //discountamount: this.combinedFormData.value.discountamount,

      }
      console.log(this.vendor_gid)
      var url = 'PmrTrnPurchaseQuotation/PostDirectQuotation'

      this.service.postparams(url, params).subscribe((result: any) => {
        if (result.status == false) {
          this.ToastrService.warning(result.message)
        }
        else {
          this.ToastrService.success(result.message)
          this.route.navigate(['/pmr/PmrTrnPurchasequotaionSummary']);
        }
      });
   
  }


}

