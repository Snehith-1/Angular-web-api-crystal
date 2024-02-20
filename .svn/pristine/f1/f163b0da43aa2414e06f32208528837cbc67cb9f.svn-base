import { Component, ElementRef, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router,ActivatedRoute } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';

interface Ileadbank{
  leadbank_gid:string;
  leadbankcontact_gid: string;
  area_code2:string;
  phone2: string;
  country_code1: string;
  area_code1:string;
  phone1: string;
  fax_area_code: string;
  fax_country_code: string;
  fax:string;
  designation: string;
  email:string;
  mobile: string;
  leadbankcontact_name: string;
  leadbankbranch_name: string;
  address1:string;
  address2:string;
  state:string;
  city:string;
  pincode:string;

}

@Component({
  selector: 'app-crm-trn-leadbankbranchedit',
  templateUrl: './crm-trn-leadbankbranchedit.component.html',
  styleUrls: ['./crm-trn-leadbankbranchedit.component.scss']
})
export class CrmTrnLeadbankbrancheditComponent {
   leadbank!: Ileadbank;
   leadbank_gid:any;
   leadbankcontact_gid:any
  brancheditform!: FormGroup | any;
  responsedata:any;
  editbranchSummary_list: any;
  selectedBranch: any;
  branch_list: any;
  contactlist: any[] = [];
  response_data: any;
  region_list: any[] = [];
  country_list: any[] = [];
 selectedCountry:any;
  selectedRegion:any;
  backButtonClicked: any;

  constructor(private renderer: Renderer2, private el: ElementRef,public service :SocketService,
    private ToastrService: ToastrService,private route:Router,private router: ActivatedRoute ) {
    this.leadbank = {} as Ileadbank;
  }
  ngOnInit(): void {

    // const leadbank_gid = this.router.snapshot.paramMap.get('leadbank_gid');
    // // console.log(termsconditions_gid)
    // this.leadbank_gid = leadbank_gid;

    // const secretKey = 'storyboarderp';

    // const deencryptedParam = AES.decrypt(this.leadbank_gid, secretKey).toString(enc.Utf8);
    // console.log(deencryptedParam)

    const leadbank_gid = this.router.snapshot.paramMap.get('leadbank_gid');
    const leadbankcontact_gid = this.router.snapshot.paramMap.get('leadbankcontact_gid');

    this.leadbank_gid = leadbank_gid;
    this.leadbankcontact_gid = leadbankcontact_gid;
    
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.leadbank_gid, secretKey).toString(enc.Utf8);
    const deencryptedParam1 = AES.decrypt(this.leadbankcontact_gid, secretKey).toString(enc.Utf8);

  
    console.log(" before decrypt: "+ this.leadbank_gid);
    
    console.log("leadbank_gid ="+ deencryptedParam);
    console.log("leadbankcontact_gid = "+deencryptedParam1);
    
    

    
    //this.GetleadbrancheditSummary(deencryptedParam)
   
       this.brancheditform = new FormGroup({
        leadbankbranch_name: new FormControl(this.leadbank.leadbankbranch_name, [
          Validators.required,
        ]),
        leadbankcontact_name: new FormControl(this.leadbank.leadbankcontact_name, [
          Validators.required,
        ]),
        mobile: new FormControl(this.leadbank.mobile, [
          Validators.required,
        ]),
        pincode: new FormControl(this.leadbank.pincode, [
          Validators.required,
        ]),
        
        state: new FormControl(this.leadbank.state, [
          Validators.required,
        ]),
        city: new FormControl(this.leadbank.city, [
          Validators.required,
        ]),
        address1: new FormControl(this.leadbank.address1, [
          Validators.required,
        ]),
        address2: new FormControl(this.leadbank.address2, [
          Validators.required,
        ]),
        leadbank_gid: new FormControl(''),
        leadbankcontact_gid: new FormControl(''),
        email: new FormControl(this.leadbank.email, [
          Validators.required,
          Validators.minLength(1),
          Validators.pattern('^([a-z0-9-]+|[a-z0-9-]+([.][a-z0-9-]+)*)@([a-z0-9-]+\.[a-z]{2,20}(\.[a-z]{2})?|\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\]|localhost)$')
        ]),
        designation: new FormControl(this.leadbank.designation, [
          Validators.required,
        ]),
      
       
       });
       var api2 = 'registerlead/Getregiondropdown1'
    this.service.get(api2).subscribe((result: any) => {
      $('#product').DataTable().destroy();
      this.response_data = result;
      this.region_list = this.response_data.Getregiondropdown1;
      setTimeout(() => {
        $('#product').DataTable();
      }, 1);
    });
    //this.Getregiondropdown();

    var api2 = 'registerlead/Getcountrynamedropdown'
    this.service.get(api2).subscribe((result: any) => {
      $('#product').DataTable().destroy();
      this.response_data = result;
      this.country_list = this.response_data.Getcountrynamedropdown;
      setTimeout(() => {
        $('#product').DataTable();
      }, 1);
    });

this.GetleadbrancheditSummary(deencryptedParam1)

      }
    
      GetleadbrancheditSummary(deencryptedParam1: any) {
        var url = 'registerlead/GetleadbrancheditSummary'
         
        let param = {leadbankcontact_gid : deencryptedParam1}
       console.log("Leadbankcontact_gid:"+ param);
         
        this.service.getparams(url, param).subscribe((result: any) => {
          this.responsedata=result;
          this.editbranchSummary_list = result.leadaddbranch_list;
          console.log(this.leadbank)
          console.log(this.editbranchSummary_list)
          this.brancheditform.get("leadbank_gid")?.setValue(this.editbranchSummary_list[0].leadbank_gid);
          this.brancheditform.get("leadbankcontact_gid")?.setValue(this.editbranchSummary_list[0].leadbankcontact_gid);
          this.brancheditform.get("country_name")?.setValue(this.editbranchSummary_list[0].country);
          this.brancheditform.get("state")?.setValue(this.editbranchSummary_list[0].state);
          this.brancheditform.get("city")?.setValue(this.editbranchSummary_list[0].city);
          this.brancheditform.get("leadbankbranch_name")?.setValue(this.editbranchSummary_list[0].leadbankbranch_name);
          this.brancheditform.get("leadbankcontact_name")?.setValue(this.editbranchSummary_list[0].leadbankcontact_name);
          this.brancheditform.get("mobile")?.setValue(this.editbranchSummary_list[0].mobile);
          this.brancheditform.get("designation")?.setValue(this.editbranchSummary_list[0].designation)
          this.brancheditform.get("country_code1")?.setValue(this.editbranchSummary_list[0].country_code1);
          this.brancheditform.get("area_code1")?.setValue(this.editbranchSummary_list[0].area_code1);
          this.brancheditform.get("phone1")?.setValue(this.editbranchSummary_list[0].phone1);
          this.brancheditform.get("region_name")?.setValue(this.editbranchSummary_list[0].phone1);
          this.brancheditform.get("address1")?.setValue(this.editbranchSummary_list[0].address1);
          this.brancheditform.get("address2")?.setValue(this.editbranchSummary_list[0].address2);
          this.brancheditform.get("pincode")?.setValue(this.editbranchSummary_list[0].pincode);
          this.brancheditform.get("area_code2")?.setValue(this.editbranchSummary_list[0].area_code2);
          this.brancheditform.get("phone2")?.setValue(this.editbranchSummary_list[0].phone2);
      
          
        });

      }
      get leadbankbranch_name() {
        return this.brancheditform.get('leadbankbranch_name');
      }
      get pincode() {
        return this.brancheditform.get('pincode');
      }
      get leadbankcontact_name() {
        return this.brancheditform.get('leadbankcontact_name');
      }
      get mobile() {
        return this.brancheditform.get('mobile');
      }
      get address1() {
        return this.brancheditform.get('address1');
      }
      get address2() {
        return this.brancheditform.get('address2');
      }
      get state() {
        return this.brancheditform.get('state');
      }
      get city() {
        return this.brancheditform.get('city');
      }
      get email() {
        return this.brancheditform.get('email');
      }
      get designation() {
        return this.brancheditform.get('designation');
      }
    
     
      public validate(): void {
        this.leadbank = this.brancheditform.value;
       //console.log(this.brancheditform.value);
       
        if (this.leadbank.leadbankbranch_name != null && this.leadbank.leadbankcontact_name != null && this.leadbank.mobile != null && this.leadbank.email != null 
          && this.leadbank.designation != null &&  this.leadbank.address1 != null  && this.leadbank.address2 != null
           &&  this.leadbank.state != null && this.leadbank.city != null  && this.leadbank.pincode != null) 
         console.log(this.brancheditform.value)
        //  this.leadbank.region_name != null,this.leadbank.country_name != null  &&
         const api = 'registerlead/Updateleadbranchedit';
     
         this.service.post(api,this.brancheditform.value).subscribe((result: any) => {
          if (result.status == false) {
            this.ToastrService.warning(result.message)
          }
          else {
            //this.route.navigate(['/crm/CrmTrnLeadBankbranch',this.leadbank_gid]);
            this.route.navigate(['/crm/CrmTrnLeadbanksummary']);
            if (!this.backButtonClicked) {
              this.ToastrService.success(result.message);
            }
          }
          this.responsedata = result;
        });
      }


      onback(){
        this.backButtonClicked = true;
      //  this.route.navigate(['/crm/CrmTrnLeadbankbranchedit',this.leadbank_gid]);
        this.route.navigate(['/crm/CrmTrnLeadbanksummary']);
        //CrmTrnLeadbanksummary
      }
    }


