import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { ToastrService } from 'ngx-toastr';
interface deliveryorder{
  
  salesorder_gid: string;
 salesorder_date: string;
  branch_name: string;
  branch_gid: string;
  so_referencenumber: string;
  customer_gid: string;
  customer_name: string;
  customer_contact_gid: string;
  customercontact_names: string;
  customer_mobile: string;
  customer_email: string;
  salesperson_gid: string;
  user_name: string;
  customer_address: string;
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
  margin_percentage: string;
  margin_amount: string;
  product_price: string;
  tax1_gid: string;
  tax_name: string;
  tax_amount: string;
  tax2_gid: string;
  tax_name2: string;
  tax_amount2: string;
  tax3_gid: string;
  tax_name3: string;
  tax_amount3: string;
  price: string;
  product_requireddate: string;
  product_requireddateremarks: string;
  template_content: string;
}

@Component({
  selector: 'app-smr-trn-raisedeliveryorder',
  templateUrl: './smr-trn-raisedeliveryorder.component.html',
  styleUrls: ['./smr-trn-raisedeliveryorder.component.scss']
})
export class SmrTrnRaisedeliveryorderComponent {
  
  showInput: boolean = false;
  inputValue: string = ''
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '25rem',
    minHeight: '5rem',
    width: '975px',
    placeholder: 'Enter text here...',
    translate: 'no',

    defaultParagraphSeparator: 'p',

    defaultFontName: 'Arial',



  };


  deliveryform: FormGroup | any;
  template_content : FormGroup |any;
  productform: FormGroup | any;
  branch_list : any [] = [];
  customer_list: any [] = [];
  contact_list: any [] = [];
  sales_list: any [] = [];
  currency_list: any [] = [];
  product_list: any [] = [];
  productgroup_list: any [] = [];
  productname_list: any [] = [];
  tax_list: any [] = [];
  POproductlist: any [] = [];
  totalamount: any;
  taxamount3: any;
  mdlTaxName3: any;
  taxamount2: any;
  taxamount1: any;
  discountamount: any;
  discountpercentage: any;
  quantity: any;
  mdlProductName: any;
  mdlTaxName1: any;
  mdlTaxName2: any;
  unitprice: any;
  deliveryorder! : deliveryorder;

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private router: ActivatedRoute) {
    this.deliveryorder = {} as deliveryorder;
  }

  ngOnInIt(): void {

    this.deliveryform = new FormGroup ({
  salesorder_gid: new FormControl(''),
  salesorder_date: new FormControl(''),
  branch_name: new FormControl(''),
  branch_gid: new FormControl(''),
  so_referencenumber: new FormControl(''),
  customer_gid: new FormControl(''),
  customer_name: new FormControl(''),
  customer_contact_gid: new FormControl(''),
  customercontact_names: new FormControl(''),
  customer_mobile: new FormControl(''),
  customer_email: new FormControl(''),
  salesperson_gid: new FormControl(''),
  user_name: new FormControl(''),
  customer_address: new FormControl(''),
  shipping_to: new FormControl(''),
  so_remarks: new FormControl(''),
  start_date: new FormControl(''),
  end_date: new FormControl(''),
  freight_terms: new FormControl(''),
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
  display_field: new FormControl(''),
  qty_quoted: new FormControl(''),
  mrp_price: new FormControl(''),
  margin_percentage: new FormControl(''),
  margin_amount: new FormControl(''),
  product_price: new FormControl(''),
  tax_gid: new FormControl(''),
  tax_name: new FormControl(''),
  tax_amount: new FormControl(''),
  tax_name2: new FormControl(''),
  tax_amount2: new FormControl(''),
  tax_name3: new FormControl(''),
  tax_amount3: new FormControl(''),
  totalamount: new FormControl(''),
  product_requireddate: new FormControl(''),
  product_requireddateremarks: new FormControl(''),
    });

    this.productform = new FormGroup({
      productgroup_gid: new FormControl(''),
  productgroup_name: new FormControl(''),
  customerproduct_code: new FormControl(''),
  product_code: new FormControl(''),
  product_gid: new FormControl(''),
  product_name: new FormControl(''),
  display_field: new FormControl(''),
  qty_quoted: new FormControl(''),
  mrp_price: new FormControl(''),
  margin_percentage: new FormControl(''),
  margin_amount: new FormControl(''),
  product_price: new FormControl(''),
  tax_gid: new FormControl(''),
  tax_name: new FormControl(''),
  tax_amount: new FormControl(''),
  tax_name2: new FormControl(''),
  tax_amount2: new FormControl(''),
  tax_name3: new FormControl(''),
  tax_amount3: new FormControl(''),
  totalamount: new FormControl(''),
  product_uom: new FormControl(''),
  product_requireddate: new FormControl(''),
  product_requireddateremarks: new FormControl('')
    
  })
}
  

  get product_name() {
    return this.productform.get('product_name')!;
  }
  get product_code() {
    return this.productform.get('product_code')!;
  }
 
  productSubmit(){

  }

  GetOnChangeProductName(){

  }

  prodtotalcal(){

  }
  OnChangeTaxAmount1(){

  }

  OnChangeTaxAmount2(){

  }
  OnChangeTaxAmount3(){

  }

  summaryadd(){

  }
  openModaldelete(){

  }

  redirecttolist(){}
  onChange2(){}

}


