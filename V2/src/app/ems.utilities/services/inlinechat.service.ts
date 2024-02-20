import { Injectable } from '@angular/core';
import {HttpBackend, HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CrmSmmWebsitechatsComponent } from 'src/app/ems.crm/component/crm-smm-websitechats/crm-smm-websitechats.component';

@Injectable({
  providedIn: 'root'
})
export class InlinechatService {

  private httpClient: HttpClient;
  constructor(
    private handler: HttpBackend,
  ) {
    this.httpClient = new HttpClient(handler);
  }


  postfile(url:string,data:any,access_token:string) {
    const headers = { 'Access-Control-Allow-Origin': '*','Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT','Authorization': access_token}  
    return this.httpClient.post(url,data,{'headers':headers});
  }

}
