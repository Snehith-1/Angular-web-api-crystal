import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';

import { Subscription, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { HttpClient } from '@angular/common/http';
import { saveAs } from 'file-saver';
import { NgxSpinnerService } from 'ngx-spinner';
import { ExcelService } from 'src/app/Service/excel.service';
interface IProduct {
  product_gid: string;

}
@Component({
  selector: 'app-crm-mst-productsummary',
  templateUrl: './crm-mst-productsummary.component.html',
  styleUrls: ['./crm-mst-productsummary.component.scss']
})
export class CrmMstProductsummaryComponent {
  shopifyproduct_list:any;
  shopifyproduct:any;
  product!: IProduct;
  file!: File;
  image_path: any;
  imageData !: string;
  private unsubscribe: Subscription[] = [];
  reactiveForm!: FormGroup;
  responsedata: any;
  parameterValue: any;
  parameterValue1: any;
  product_gid1:any;
  product_gid: any;
  products: any[] = [];
  response_data: any;
  image:any;
  constructor(private excelService : ExcelService, private fb: FormBuilder,private NgxSpinnerService: NgxSpinnerService,private http: HttpClient, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService,) { }


  ngOnInit(): void {
    this.GetProductSummary();

    this.reactiveForm = new FormGroup({
      file: new FormControl(''),
      fileExtension: new FormControl(''),
      fileName: new FormControl(''),
      imagePath: new FormControl(''),
      product_gid: new FormControl('')

    });
  }
  GetProductSummary() {

    var api = 'Product/GetProductSummary';
    this.service.get(api).subscribe((result: any) => {
      $('#product').DataTable().destroy();
      this.response_data = result;
      this.products = this.response_data.product_list;
      setTimeout(() => {
        $('#product').DataTable();
      }, 1);
    });
  //   var url = 'Product/GetShopifyProduct'
  // this.service.get(url).subscribe((result: any) => {

  //   this.shopifyproduct= result;
  //   this.shopifyproduct_list = this.shopifyproduct.products;
   

  // });
  // var url2 = 'SmrTrnSalesorder/GetShopifyOrder'
  // this.service.get(url2).subscribe((result: any) => {

  //   this.shopifyproduct= result;
  //   // this.shopifyproduct_list = this.shopifyproduct.products;
   

  // });

  }
  onChange1(event: any) {
    this.file = event.target.files[0];
    }

  onChange2(event: any) {
    this.file = event.target.files[0];
   
  }
  downloadfileformat() {
    let link = document.createElement("a");
    link.download = "Product";
    link.href = "assets/media/Excels/UPLF23092048.xlsx";
    link.click();
  }
  downloadImage(data: any) {
  if(data.product_image !=null && data.product_image != "" ){
    saveAs(data.product_image, data.product_gid  + '.png');
  }
  else{
    window.scrollTo({
      top: 0, // Code is used for scroll top after event done
    });
    this.ToastrService.warning('No Image Found')

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

  onedit(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.router.navigate(['/crm/CrmMstProductEdit', encryptedParam])
  }
  onview(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.router.navigate(['/crm/CrmMstProductView', encryptedParam])
  }
  importexcel() {
    let formData = new FormData();
    if (this.file != null && this.file != undefined) {

      formData.append("file", this.file, this.file.name);

      var api = 'Product/ProductUploadExcels'

      this.service.postfile(api, formData).subscribe((result: any) => {
        this.responsedata = result;
       
          // this.router.navigate(['/crm/CrmMstProductsummary']);
          window.location.reload();
          this.ToastrService.success("Excel Uploaded Successfully")
        
      });

    }
  }
  onadd() {
    this.router.navigate(['/crm/CrmMstProductAdd'])

  }
  openModaldelete(parameter: string) {
    this.parameterValue = parameter


  }
 
  myModaladddetails(parameter: string) {
    this.parameterValue1 = parameter
    this.reactiveForm.get("product_gid")?.setValue(this.parameterValue1.product_gid);   

  }
  public onsubmit(): void {
    console.log(this.reactiveForm.value)
    this.product = this.reactiveForm.value;
    let formData = new FormData();
    if (this.file != null && this.file != undefined) {
      formData.append("file", this.file, this.file.name);
      formData.append("product_gid", this.product.product_gid);
      this.NgxSpinnerService.show();
      var api7 = 'Product/GetProductImage'
      this.service.postfile(api7, formData).subscribe((result: any) => {
        if(result.status ==false){
          this.NgxSpinnerService.hide();
          this.ToastrService.warning(result.message)
        }
        else{
          // this.router.navigate(['/crm/CrmMstProductsummary']);
          this.NgxSpinnerService.hide();
          this.GetProductSummary();
          this.ToastrService.success(result.message)
           window.location.reload();

        }

        this.responsedata = result;
       

      });
      
    }
  }

  exportExcel() :void {
    const ProductList = this.products.map(item => ({
      Product_Type: item.producttype_name || '',
      Product_Group: item.productgroup_name || '',
      Product_Code: item.product_code || '',
      Product: item.product_name || '',
      Unit: item.productuomclass_name || '',
     
     }));     
      // this.excelService.exportAsExcelFile(ProductList , 'Product');
       // Create a new table element
  const table = document.createElement('table');

  // Add header row with background color
  const headerRow = table.insertRow();
  Object.keys(ProductList[0]).forEach(header => {
    const cell = headerRow.insertCell();
    cell.textContent = header;
    cell.style.backgroundColor = '#00317a'; 
    cell.style.color = '#FFFFFF';
    cell.style.fontWeight = 'bold';
    cell.style.border = '1px solid #000000';
  });

  // Add data rows
  ProductList.forEach(item => {
    const dataRow = table.insertRow();
    Object.values(item).forEach(value => {
      const cell = dataRow.insertCell();
      cell.textContent = value;
      cell.style.border = '1px solid #000000';
    });
  });

  // Convert the table to a data URI
  const tableHtml = table.outerHTML;
  const dataUri = 'data:application/vnd.ms-excel;base64,' + btoa(unescape(encodeURIComponent(tableHtml)));

  // Trigger download
  const link = document.createElement('a');
  link.href = dataUri;
  link.download = 'Product.xls';
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
    }
  ondelete() {
    // console.log(this.parameterValue);
    this.NgxSpinnerService.show();
    var url = 'Product/Getdeleteproductdetails'
    let param = {
      product_gid: this.parameterValue
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      if (result.status == false) {
        this.NgxSpinnerService.hide();
        window.scrollTo({

 

          top: 0, // Code is used for scroll top after event done



        });
        this.ToastrService.warning(result.message)
      }
      else {
        this.NgxSpinnerService.hide();
        window.scrollTo({

 

          top: 0, // Code is used for scroll top after event done



        });
       


        this.ToastrService.success(result.message)

      }
      this.GetProductSummary();



    });
  }





}

