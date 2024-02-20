import { Component } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AES,enc } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { SelectionModel } from '@angular/cdk/collections';
import { ActivatedRoute} from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';


interface Igeneratedbonus {
  setAmount(bonus_amount: any): unknown;
  bonus_gid: any;
  employee_gid: any;
  bonus_amount: any;
  branch_gid: string;
  branch_gid1: string;
  branch_code: string;
  branch_name: string;
  branch_prefix: string;
  branchmanager_gid: string;
  emp_code: any;
  user_firstname: string;
  branch_code_add: string;
  Branch_address_add: string;
  City: string;
  State: string;
  Postal_code: any;
  Phone_no_add: any;
  Email_address_add: any;
  GST_no_add: any;
}


export class IEmployee {
  generatebonus_list: any[] = [];;
  employee_gid:any;
  bonusSummarylist: any[] = [];
  bonus_gid: any;
  generatedbonus!: Igeneratedbonus;

}

@Component({
  selector: 'app-pay-trn-generatedbonusview',
  templateUrl: './pay-trn-generatedbonusview.component.html',
  styleUrls: ['./pay-trn-generatedbonusview.component.scss']
})
export class PayTrnGeneratedbonusviewComponent {
  GenerateBonus : any[] = [];
  reactiveForm!: FormGroup;
  responsedata: any;
  parameterValue : any;
  GetBonus : any[] = [];
  generatebonus_list : any[] = [];
  parameterValue1: any;
  bonusgid: any;
  bonusgid1: any;
  reactiveFormEdit!: FormGroup;
  generatedbonus: Igeneratedbonus;
  emp_code: any;
  emp_name: any;
  bonus_name :any;
  bonus_from:any;
  bonus_to: any;
  bonus_percentage : any;
  bonus_amount :any;


constructor(public service :SocketService,private route:Router,private NgxSpinnerService: NgxSpinnerService, private router: ActivatedRoute,private ToastrService: ToastrService, private FormBuilder: FormBuilder) {
      this.generatedbonus = {} as Igeneratedbonus;
}

updateAmount() {
  this.generatedbonus.setAmount(this.bonus_amount);
}
  
  ngOnInit(): void {
    const bonus_gid = this.router.snapshot.paramMap.get('bonus_gid');
    this.bonusgid = bonus_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.bonusgid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam);
    this.bonusgid1 = deencryptedParam;
   this.getsummary();


   this.reactiveFormEdit = new FormGroup({
    bonus_amount: new FormControl(this.generatedbonus.bonus_amount, [Validators.required,]),
    employee_gid: new FormControl(''),
    bonus_gid: new FormControl(''),
  });
  }

getsummary(){
  
  const bonus_gid = this.router.snapshot.paramMap.get('bonus_gid');
  this.bonusgid = bonus_gid;
  const secretKey = 'storyboarderp';
  const deencryptedParam = AES.decrypt(this.bonusgid, secretKey).toString(enc.Utf8);
  console.log(deencryptedParam);
  this.bonusgid1 = deencryptedParam;

  var api = 'PayTrnBonus/GetgenerateBonus';
  let param = {
    bonus_gid: this.bonusgid1,
  };
 
    this.service.getparams(api,param).subscribe((result:any) => {
    $('#generatedbonus').DataTable().destroy();
      this.GenerateBonus =result.generatebonus_list;
    setTimeout(()=>{  
        $('#generatedbonus').DataTable();
      }, 1);
      this.emp_name = this.GenerateBonus[0].user_firstname;
      this.emp_code = this.GenerateBonus[0].user_code;
      this.bonus_name = this.GenerateBonus[0].bonus_name;
      this.bonus_from = this.GenerateBonus[0].bonus_from;
      this.bonus_to = this.GenerateBonus[0].bonus_to;
      this.bonus_percentage = this.GenerateBonus[0].bonus_percentage;
    });
  
}

openModaledit(parameter: string) {
  this.parameterValue1 = parameter
  this.reactiveFormEdit.get("bonus_amount")?.setValue(this.parameterValue1.bonus_amount);
  this.reactiveFormEdit.get("employee_gid")?.setValue(this.parameterValue1.employee_gid);
  this.reactiveFormEdit.get("bonus_gid")?.setValue(this.parameterValue1.bonus_gid);
}


public onupdate(): void {
  this.NgxSpinnerService.show();

  if (this.reactiveFormEdit.value.bonus_amount != null && this.reactiveFormEdit.value.bonus_amount != '') {
    for (const control of Object.keys(this.reactiveFormEdit.controls)) {
      this.reactiveFormEdit.controls[control].markAsTouched();
    }
    this.reactiveFormEdit.value;      
    var url = 'PayTrnBonus/Updatedbonus'

    this.service.postparams(url, this.reactiveFormEdit.value).pipe().subscribe((result: any) => {
    
      if (result.status == false) {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }
  else {

    this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
  }
}

}
