import { Component, ElementRef, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router,ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { AES, enc } from 'crypto-js';
interface Ileadbank {

  leadbankcontact_gid:string;
  country_code2: string;
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
}
@Component({
  selector: 'app-crm-trn-leadbankcontact-edit',
  templateUrl: './crm-trn-leadbankcontact-edit.component.html',
  styleUrls: ['./crm-trn-leadbankcontact-edit.component.scss']
})
export class CrmTrnLeadbankcontactEditComponent {
  leadbank!: Ileadbank;
  leadbank_gid:any;
  contacteditform!: FormGroup | any;
  responsedata:any;
  editContactSummary_list: any;
  selectedBranch: any;
  branch_list: any;
  contactlist: any[] = [];
  leadbankcontact_gid: any;
  leadaddbranch_list: any[] = [];
  response_data: any;
  branch_list1: any[] = [];
  branchform: FormGroup<{}> | any;
  country_list: any[] = [];
  backButtonClicked: any;



  constructor(private renderer: Renderer2, private el: ElementRef,public service :SocketService,private route:Router, private ToastrService: ToastrService,private router: ActivatedRoute ) {
    this.leadbank = {} as Ileadbank;
  }
  ngOnInit(): void {

    // const leadbank_gid = this.router.snapshot.paramMap.get('leadbank_gid');
    //    // console.log(termsconditions_gid)
    //    this.leadbank_gid = leadbank_gid;
   
    //    const secretKey = 'storyboarderp';
     
    //    const deencryptedParam = AES.decrypt(this.leadbank_gid, secretKey).toString(enc.Utf8);
    //    console.log(deencryptedParam)
   

       const leadbank_gid = this.router.snapshot.paramMap.get('leadbank_gid');
       const leadbankcontact_gid = this.router.snapshot.paramMap.get('leadbankcontact_gid');
   
       this.leadbank_gid = leadbank_gid;
       this.leadbankcontact_gid = leadbankcontact_gid;
       
       const secretKey = 'storyboarderp';
       const deencryptedParam = AES.decrypt(this.leadbank_gid, secretKey).toString(enc.Utf8);
       const deencryptedParam1 = AES.decrypt(this.leadbankcontact_gid, secretKey).toString(enc.Utf8);

       // console.log(" before decrypt: "+ this.leadbank_gid);
       
       console.log("leadbank_gid ="+ deencryptedParam);
       console.log("leadbankcontact_gid = "+deencryptedParam1);

       if (deencryptedParam != null) {
        this.leadbank_gid=(deencryptedParam);
      }
      
      this.contacteditform = new FormGroup({
        leadbankcontact_name: new FormControl(this.leadbank.leadbankcontact_name, [
          Validators.required,
        ]),
  
        leadbankbranch_name: new FormControl('', 
        Validators.required
          ),
        address2: new FormControl(''),
        state: new FormControl(''),
        pincode: new FormControl(''),
       
  
        mobile: new FormControl(this.leadbank.mobile, [
          Validators.required,
          Validators.maxLength(10),
        ]),
        fax: new FormControl(this.leadbank.fax, [
          Validators.required,
          Validators.maxLength(10),
        ]),
        phone1: new FormControl(this.leadbank.phone1, [
          Validators.required,
          Validators.maxLength(10),
        ]),
        phone2: new FormControl(this.leadbank.phone2, [
          Validators.required,
          Validators.maxLength(10),
        ]),
        designation: new FormControl(this.leadbank.designation, [
          Validators.required,
          Validators.minLength(1),
        ]),
       
        email: new FormControl(this.leadbank.email, [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(250),
          Validators.pattern('^([a-z0-9-]+|[a-z0-9-]+([.][a-z0-9-]+)*)@([a-z0-9-]+\.[a-z]{2,20}(\.[a-z]{2})?|\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\]|localhost)$')
        ]),
        
        leadbank_gid: new FormControl(deencryptedParam),
        leadbankcontact_gid: new FormControl(deencryptedParam1),
      });
             
       this.GeteditContactSummary(deencryptedParam1)
       this.Getbranchdropdown(this.leadbank_gid);

      }
      Getbranchdropdown(leadbank_gid: any) {
        let param = {
          leadbank_gid: leadbank_gid
        }
        var api2 = 'registerlead/Getbranchdropdown'
        this.service.getparams(api2, param).subscribe((result: any) => {
          $('#product').DataTable().destroy();
          this.response_data = result;
          this.branch_list1 = this.response_data.branch_list;
          setTimeout(() => {
            $('#product').DataTable();
          }, 1);
        });
      }
      get leadbank_name() {
        return this.branchform.get('leadbank_name')!;
      }
      
      GeteditContactSummary(leadbankcontact_gid: any) {
        var url = 'Leadbank/GetleadbankcontacteditsSummary'
        
        let param = {leadbankcontact_gid : leadbankcontact_gid}
        console.log("Leadbankcontact_gid:"+ param);
          
        this.service.getparams(url, param).subscribe((result: any) => {
          this.responsedata=result;
          this.editContactSummary_list = result.leadbank_list;
    
          // this.product = result;
          console.log(this.leadbank)
          console.log(this.editContactSummary_list)
          this.contacteditform.get("leadbankcontact_gid")?.setValue(this.editContactSummary_list[0].leadbankcontact_gid);
          this.contacteditform.get("leadbankbranch_name")?.setValue(this.editContactSummary_list[0].leadbankbranch_name);
          this.contacteditform.get("leadbankcontact_name")?.setValue(this.editContactSummary_list[0].leadbankcontact_name);
          this.contacteditform.get("mobile")?.setValue(this.editContactSummary_list[0].mobile);
          this.contacteditform.get("email")?.setValue(this.editContactSummary_list[0].email);
          this.contacteditform.get("designation")?.setValue(this.editContactSummary_list[0].designation);
          this.selectedBranch = this.editContactSummary_list[0].leadbankbranch_name;
          this.contacteditform.get("country_code1")?.setValue(this.editContactSummary_list[0].country_code1);
          this.contacteditform.get("area_code1")?.setValue(this.editContactSummary_list[0].area_code1);
          this.contacteditform.get("phone1")?.setValue(this.editContactSummary_list[0].phone1);
      
          this.contacteditform.get("fax_area_code")?.setValue(this.editContactSummary_list[0].fax_area_code);
          this.contacteditform.get("fax_country_code")?.setValue(this.editContactSummary_list[0].fax_country_code);
          this.contacteditform.get("fax")?.setValue(this.editContactSummary_list[0].fax);
      
          this.contacteditform.get("country_code2")?.setValue(this.editContactSummary_list[0].country_code2);
          this.contacteditform.get("area_code2")?.setValue(this.editContactSummary_list[0].area_code2);
          this.contacteditform.get("phone2")?.setValue(this.editContactSummary_list[0].phone2);
      
          
        });

      }
      get leadbankbranch_name() {
        return this.contacteditform.get('leadbankbranch_name');
      }
      get leadbankcontact_name() {
        return this.contacteditform.get('leadbankcontact_name');
      }
      get mobile() {
        return this.contacteditform.get('mobile');
      }
     
      get email() {
        return this.contacteditform.get('email');
      }
      get designation() {
        return this.contacteditform.get('designation');
      }
      get country_code1() {
        return this.contacteditform.get('country_code1');
      }
      get fax_area_code() {
        return this.contacteditform.get('fax_area_code');
      }
      get country_code2() {
        return this.contacteditform.get('country_code2');
      }
      public validate(): void {
        this.leadbank = this.contacteditform.value;
       
        if (this.leadbank.leadbankbranch_name != null && this.leadbank.leadbankcontact_name != null
          && this.leadbank.mobile != null && this.leadbank.email != null)  
        {
          
         console.log(this.contacteditform.value)
     
         const api = 'Leadbank/UpdateleadbankContactedit';
     
         this.service.post(api,this.contacteditform.value).subscribe((result: any) => {
          if (result.status == false) {
            this.ToastrService.warning(result.message)
          }
          else {
            this.route.navigate(['/crm/CrmTrnLeadbanksummary']);
            if (!this.backButtonClicked) {
              this.ToastrService.success(result.message);
            }
          }
          this.responsedata = result;
        });
      }
    }
    onback(){
      this.backButtonClicked = true;
    //  this.route.navigate(['/crm/CrmTrnLeadbankcontact',this.leadbank_gid]);
      this.route.navigate(['/crm/CrmTrnLeadbanksummary']);
      //CrmTrnLeadbanksummary
    }
}
