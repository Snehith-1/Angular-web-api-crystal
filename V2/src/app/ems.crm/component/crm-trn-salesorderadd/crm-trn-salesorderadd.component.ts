import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators  } from '@angular/forms';
import { Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-crm-trn-salesorderadd',
  templateUrl: './crm-trn-salesorderadd.component.html',
  styleUrls: ['./crm-trn-salesorderadd.component.scss']
})
export class CrmTrnSalesorderaddComponent {
  panelOpenState = false;
  
  date: Date = new Date();
  constructor(private builder: FormBuilder,private route:Router,private service : SocketService,private toastr: ToastrService) { }
  productform = this.builder.group({
    financial_year: this.builder.control('', Validators.required),
    accountref_no: this.builder.control('', Validators.required),
    poref_no: this.builder.control('', Validators.required),
    po_date: this.builder.control('', Validators.required),
    vendorref_no: this.builder.control('', Validators.required),
    invoice_date: this.builder.control('', Validators.required),
    entity_name: this.builder.control('', Validators.required),
    branch_name: this.builder.control('', Validators.required),
    department_name: this.builder.control('', Validators.required),
    vendor_companyname: this.builder.control('', Validators.required),
    remarks: this.builder.control(''),
    
    variants: this.builder.array([])
  });

department_list: any[] = [];
entity_list: any[] = [];
branch_list: any[] = [];
vendor_list: any[] = [];
product_list: any[] = [];
productsubgroup_list: any[] = [];
productgroup_list: any[] = [];
productgroup_name: any[] = [];
productsubgroup_name: any[] = [];
tax1_list: any[] = [];
tax2_list: any[] = [];
tax3_list: any[] = [];
tax_percentage1: any[] = [];
tax_percentage2: any[] = [];
tax_percentage3: any[] = [];
Gettax1bycodedropdown_list: any[] = [];
Gettax2bycodedropdown_list: any[] = [];
Gettax3bycodedropdown_list: any[] = [];

  responsedata: any;
  ngOnInit(): void {

   

    // this.service.Gettax1dropdown().subscribe((result:any)=>{
    //   this.responsedata=result;
    //   this.tax1_list = this.responsedata.Gettax1dropdown;
    
    // });
    // this.service.Gettax2dropdown().subscribe((result:any)=>{
    //   this.responsedata=result;
    //   this.tax2_list = this.responsedata.Gettax2dropdown;
    
    // });
    // this.service.Gettax3dropdown().subscribe((result:any)=>{
    //   this.responsedata=result;
    //   this.tax3_list = this.responsedata.Gettax3dropdown;
    
    // });
    // this.service.Getproductdropdown().subscribe((result:any)=>{
    //   this.responsedata=result;
    //   this.product_list = this.responsedata.Getproductdropdown;
    
    // });
  }
  
  formvariant !: FormArray;
  invoiceproduct !: FormGroup
  colordata: any;
  sizedata: any;
  categorydata: any
  saveresponse: any;
  editdata: any;
  editproductcode: any;
  AddVariants() {
    this.formvariant = this.productform.get("variants") as FormArray;
    this.formvariant.push(this.Generaterow());
  }

  Generaterow() {
    return this.builder.group({
      // id: this.builder.control({ value: 0, disabled: true }),
            // product_desc: this.builder.control(''),
      productgroup_name: this.builder.control(''),
      productsubgroup_name: this.builder.control(''),
      product_name: this.builder.control(''),
      quantity: this.builder.control(''),
      unit_price: this.builder.control(''),
      discount: this.builder.control(''),
      discount_amount: this.builder.control(''),
      tax_name: this.builder.control(''),
      tax_name1: this.builder.control(''),
      tax_name2: this.builder.control(''),
      tax_percentage1: this.builder.control(''),      
      tax_percentage2: this.builder.control(''),
      tax_percentage3: this.builder.control(''),
      total: this.builder.control(''),

    });
  }

  get variants() {
    return this.productform.get("variants") as FormArray;
  }
  Removevariant(index: any) {
    if (confirm('do you want to remove this variant?')) {
      this.formvariant = this.productform.get("variants") as FormArray;
      this.formvariant.removeAt(index)
    }
  }
  onsubmit(){
    // console.log(this.productform.value);
    // this.CurObj.productgroup_name= this.productform.value.productgroup_name
    // this.CurObj.productsubgroup_name= this.productform.value.productsubgroup_name
    // this.CurObj.product_name= this.productform.value.product_name
    // this.CurObj.quantity= this.productform.value.quantity
    // this.CurObj.unit_price= this.productform.value.unit_price
    // this.CurObj.discount= this.productform.value.discount
    // this.CurObj.discount_amount= this.productform.value.discount_amount
    // this.CurObj.tax_name= this.productform.value.tax_name
    // this.CurObj.tax_name1= this.productform.value.tax_name1
    // this.CurObj.tax_name2= this.productform.value.tax_name2
    // this.CurObj.tax_percentage1= this.productform.value.tax_percentage1
    // this.CurObj.tax_percentage2= this.productform.value.tax_percentage2
    // this.CurObj.tax_percentage3= this.productform.value.tax_percentage3
    // this.CurObj.total= this.productform.value.total
    // this.CurObj.financial_year= this.productform.value.financial_year
    // this.CurObj.accountref_no= this.productform.value.accountref_no
    // this.CurObj.poref_no= this.productform.value.poref_no
    // this.CurObj.po_date= this.productform.value.po_date
    // this.CurObj.vendorref_no= this.productform.value.vendorref_no
    // this.CurObj.invoice_date= this.productform.value.invoice_date
    // this.CurObj.entity_name= this.productform.value.entity_name
    // this.CurObj.branch_name= this.productform.value.branch_name
    // this.CurObj.department_name= this.productform.value.department_name
    // this.CurObj.vendor_companyname= this.productform.value.vendor_companyname
    // this.CurObj.remarks= this.productform.value.remarks
    // this.CurObj.variants= this.productform.value.variants
    
    
  //     this.service.Postassetinvoice(this.CurObj).pipe().subscribe(res=>{
  // this.responsedata=res;
  // this.toastr.success('Asset Invoice Added Successfully!');
  // this.route.navigate(['asset/master/assetinvoice']);
  // //  window.location.reload();
  
  // });
  
  
    // this.toastr.warning('Please Enter Asset Invoice Details!');
  

  }
 
  tax1change(index: any){
    this.formvariant = this.productform.get("variants") as FormArray;
    this.invoiceproduct = this.formvariant.at(index) as FormGroup;
    let tax_name = this.invoiceproduct.get("tax_name")?.value;
    // this.service.Gettax1bycodedropdown(tax_name).subscribe((result:any)=>{
    //   this.responsedata=result;
    //   this.Gettax1bycodedropdown_list = this.responsedata.Gettax1bycodedropdown;
    //   //  console.log(this.Gettax1bycodedropdown_list)
    //   this.tax_percentage1 = this.Gettax1bycodedropdown_list[0].percentage;
    //   let quantity = this.invoiceproduct.get("quantity")?.value;
    //   let unit_price = this.invoiceproduct.get("unit_price")?.value;
    //   let total = (quantity*unit_price);
    //   let total1 = (total*this.Gettax1bycodedropdown_list[0].percentage)/100
    //   // console.log(total1)
    //     this.invoiceproduct.get("tax_percentage1")?.setValue(total1);

    //     let total2 =(total+total1);
    //     this.invoiceproduct.get("total")?.setValue(total2);
    
    // });
  }
  tax2change(index: any){
    this.formvariant = this.productform.get("variants") as FormArray;
    this.invoiceproduct = this.formvariant.at(index) as FormGroup;
    let tax_percentage1 = this.invoiceproduct.get("tax_percentage1")?.value;
    let tax_name1 = this.invoiceproduct.get("tax_name1")?.value;
    // this.service.Gettax2bycodedropdown(tax_name1).subscribe((result:any)=>{
    //   this.responsedata=result;
    //   this.Gettax2bycodedropdown_list = this.responsedata.Gettax2bycodedropdown;
    //   //  console.log(this.Gettax1bycodedropdown_list)
    //   this.tax_percentage2 = this.Gettax2bycodedropdown_list[0].percentage;
    //   let quantity = this.invoiceproduct.get("quantity")?.value;
    //   let unit_price = this.invoiceproduct.get("unit_price")?.value;
    //   let total = (quantity*unit_price);
    //   let total1 = (total*this.Gettax2bycodedropdown_list[0].percentage)/100
    //     this.invoiceproduct.get("tax_percentage2")?.setValue(total1);

    //     let total2 =(total+total1+tax_percentage1);
    //     this.invoiceproduct.get("total")?.setValue(total2);
      
    
    // });
  }
  tax3change(index: any){
    this.formvariant = this.productform.get("variants") as FormArray;
    this.invoiceproduct = this.formvariant.at(index) as FormGroup;
    let tax_percentage1 = this.invoiceproduct.get("tax_percentage1")?.value;
    let tax_percentage2 = this.invoiceproduct.get("tax_percentage2")?.value;
    let tax_name2 = this.invoiceproduct.get("tax_name2")?.value;
    // this.service.Gettax3bycodedropdown(tax_name2).subscribe((result:any)=>{
    //   this.responsedata=result;
    //   this.Gettax3bycodedropdown_list = this.responsedata.Gettax3bycodedropdown;
    //   //  console.log(this.Gettax1bycodedropdown_list)
    //   this.tax_percentage3 = this.Gettax3bycodedropdown_list[0].percentage;

    //   let quantity = this.invoiceproduct.get("quantity")?.value;
    //   let unit_price = this.invoiceproduct.get("unit_price")?.value;
    //   let total = (quantity*unit_price);
    //   let total1 = (total*this.Gettax3bycodedropdown_list[0].percentage)/100
    //     this.invoiceproduct.get("tax_percentage3")?.setValue(total1);

    //     let total2 =(total+total1+tax_percentage1+tax_percentage2);
    //     this.invoiceproduct.get("total")?.setValue(total2);
      
      
    
    // });
  }
  
  productsubgroupchange(index: any) {
    this.formvariant = this.productform.get("variants") as FormArray;
    this.invoiceproduct = this.formvariant.at(index) as FormGroup;
    let product_name = this.invoiceproduct.get("product_name")?.value;
    // this.service.Getproductsubgroupbycodedropdown(product_name).subscribe((result:any)=>{
    //   this.responsedata=result;
    //   this.productsubgroup_list = this.responsedata.Getproductsubgroupdropdown;
    //   // console.log(this.productsubgroup_list)
    //   this.productgroup_name = this.productsubgroup_list[0].productgroup_name;
    //   this.productsubgroup_name = this.productsubgroup_list[0].productsubgroup_name;
    //   // console.log(this.productgroup_name)
    //   // console.log(this.productsubgroup_name)

    //    this.invoiceproduct.get("productgroup_name")?.setValue(this.productgroup_name);
    //     this.invoiceproduct.get("productsubgroup_name")?.setValue(this.productsubgroup_name);      // console.log(this.productsubgroup_list);
      
    
    // });
 
  }
  vendorchange() {
    let vendorcompanyname = this.productform.get("vendor_companyname")?.value;
    // console.log(vendorcompanyname)
    // this.service.GetCustomerbycode(customercode).subscribe(res => {
    //   let custdata: any;
    //   custdata = res;
    //   if (custdata != null) {
    //     this.invoiceform.get("deliveryAddress")?.setValue(custdata.address + ',' + custdata.phoneno + ',' + custdata.email);
    //     this.invoiceform.get("customerName")?.setValue(custdata.name);
    //   }
    // });
  }
  Itemcalculationdiscount(index: any){
    this.formvariant = this.productform.get("variants") as FormArray;
    this.invoiceproduct = this.formvariant.at(index) as FormGroup;

    let total = this.invoiceproduct.get("total")?.value;
    let discount = this.invoiceproduct.get("discount")?.value;

     let total1 = ( total*discount/100);
         

    this.invoiceproduct.get("discount_amount")?.setValue(total1);
     let total12 = (total-total1)
    

    this.invoiceproduct.get("total")?.setValue(total12);
   
  }
  Itemcalculation(index: any) {
    this.formvariant = this.productform.get("variants") as FormArray;
    this.invoiceproduct = this.formvariant.at(index) as FormGroup;
    let quantity = this.invoiceproduct.get("quantity")?.value;
    let unit_price = this.invoiceproduct.get("unit_price")?.value;
    let total = quantity * unit_price;
    this.invoiceproduct.get("total")?.setValue(total);
  }
  
}
