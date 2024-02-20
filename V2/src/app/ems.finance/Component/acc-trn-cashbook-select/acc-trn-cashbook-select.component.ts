import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { enc } from 'crypto-js';
@Component({
  selector: 'app-acc-trn-cashbook-select',
  templateUrl: './acc-trn-cashbook-select.component.html',
  styleUrls: ['./acc-trn-cashbook-select.component.scss']
})
export class AccTrnCashbookSelectComponent {
  serviceorder:any;
  branch_gid:any;
  CashBookSelect_list:any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, private router:ActivatedRoute, private route:Router, public service: SocketService) {}
 
  ngOnInit(): void {
    debugger
    const branch_gid = this.router.snapshot.paramMap.get('branch_gid');
    // console.log(termsconditions_gid)
    this.branch_gid = branch_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.branch_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.AccTrnCashbooksummary(deencryptedParam)
   
    }
    AccTrnCashbooksummary(branch_gid: any) {
      
      var url1='AccTrnCashBookSummary/GetAccTrnCashbookSelect'
      let param = {
        branch_gid : branch_gid 
      }
      this.service.getparams(url1, param).subscribe((result: any) => {
        // this.responsedata=result;
        this.CashBookSelect_list = result.CashBookSelect_list; 
        setTimeout(()=>{  
          $('#CashBookSelect_list').DataTable();
        }, 1);  });
  }

}
