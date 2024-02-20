import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';

import { NgxSpinnerService } from 'ngx-spinner';

interface ITeam {
  branch: string;
  team: string;
  description: string;
  team_name: string;
  team_manager: string;
  mail_id: string;
  team_name_edit: string;
  branch_edit: string;
  description_edit: string;
  mail_id_edit: string;
  campaign_gid: string;
  assign_employee: string;
  assign_manager: string;
  no_of_leads_assigned: string;
  team_count:string;


}
@Component({
  selector: 'app-crm-mst-campaignexpand',
  templateUrl: './crm-mst-campaignexpand.component.html',
  styleUrls: ['./crm-mst-campaignexpand.component.scss']
})
export class CrmMstCampaignexpandComponent {
  form: FormGroup = new FormGroup({
    campagin_gid: new FormControl(null),


  });
  expand: any;
  responsedata: any;
  marketingteam_list1: any;
  campaign_name:any;




  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private route: Router, private router: ActivatedRoute, private NgxSpinnerService: NgxSpinnerService) {
  }




  ngOnInit(): void {
    const campaign_gid = this.router.snapshot.paramMap.get('campaign_gid');
    this.expand = campaign_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.expand, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.GetSummary(deencryptedParam);
  }


  GetSummary(campaign_gid: any) {
      var url = 'CampaignSummary/GetMarketingTeamDetailTable'
      let param = {
        campaign_gid: campaign_gid
      }
      this.service.getparams(url, param).subscribe((result: any) => {
        $('#marketingteam_list1').DataTable().destroy();
        this.responsedata = result;
        this.marketingteam_list1 = result.marketingteam_list1;
        this.campaign_name = result.campaign_name

        console.log(this.marketingteam_list1)
        setTimeout(() => {
          $('#marketingteam_list1').DataTable();
        }, 1);
  
      });

      
    }

    onassign(params: any) {
      const secretKey = 'storyboarderp';
      //console.log(params)
      const campaign_gid = AES.encrypt(params.campaign_gid, secretKey).toString();
      const employee_gid = AES.encrypt(params.employee_gid, secretKey).toString();
      this.route.navigate(['/crm/CrmTrnCampaignleadallocation', campaign_gid, employee_gid])
    }








}



