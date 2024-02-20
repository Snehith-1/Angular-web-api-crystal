import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-hrm-trn-offerletter',
  templateUrl: './hrm-trn-offerletter.component.html',
  styleUrls: ['./hrm-trn-offerletter.component.scss']
})
export class HrmTrnOfferletterComponent {
  offer_list: any[] = [];
  consider_list: any[] = [];
  responsedata: any;
  reactiveFormadd!: FormGroup;
  company_code: any;




  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    }
   
    ngOnInit(): void {

      // this.reactiveFormadd = new FormGroup({
      //   leave_code: new FormControl(''),
      //   leave_name: new FormControl(''),
      //   Status_flag: new FormControl(''),
      //   weekoff_consider: new FormControl(''),
      //   holiday_consider: new FormControl(''),
      //   carry_forward: new FormControl(''),
      //   Accured_type: new FormControl(''),
      //   negative_leave: new FormControl(''),
      //   Consider_as: new FormControl(''),
      //   Leave_Days: new FormControl(''),
      
      // });
    

      //// Summary Grid//////
  var url = 'OfferLetter/OfferLetterSummary'
     
  this.service.get(url).subscribe((result: any) => {
  this.responsedata = result;
  this.offer_list = this.responsedata.Offersummary_list;
    setTimeout(() => {
      $('#offer_list').DataTable();
      }, );
});

 }
 PrintPDF(offer_gid: string) {
  this.company_code = localStorage.getItem('c_code')
  window.location.href = "http://" + environment.host + "/Print/EMS_print/Hrm_trn_Offerletter.aspx?offer_gid=" + offer_gid + "&companycode=" + this.company_code
}


}
