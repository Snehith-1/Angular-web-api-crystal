import { Component } from '@angular/core';
import { TableModule } from 'primeng/table';
import { Table } from 'primeng/table';
import { SelectionModel } from '@angular/cdk/collections';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ActivatedRoute } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';


export class IContactList {
  contacts_list: any[] = [];
  sendtext: string = "";
}
export interface Customer {

  leadbank_gid?: any;
  displayName?: any | null;
  value?: any | null;
  source_name?: any | null;
  customer_type?: any | null;
}
@Component({
  selector: 'app-crm-smm-wacustomizebulkmessage',
  templateUrl: './crm-smm-wacustomizebulkmessage.component.html',
  styleUrls: ['./crm-smm-wacustomizebulkmessage.component.scss']
})
export class CrmSmmWacustomizebulkmessageComponent {
  contacts_list: any[] = [];
  filteredData: IContactList[] = [];
  customer!: Customer;
  selectedCustomer: Customer[] = [];
  selection = new SelectionModel<IContactList>(true, []);
  CurObj: IContactList = new IContactList();
  reactiveMessageForm!: FormGroup;
  pick: Array<any> = [];

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private router: ActivatedRoute, private route: Router, private NgxSpinnerService: NgxSpinnerService) {

  }
  ngOnInit(): void {
    this.getContactLists();

    this.reactiveMessageForm = new FormGroup({
      identifierValue: new FormControl(''),
      type: new FormControl(''),
      sendtext: new FormControl(''),
      template_name: new FormControl(''),
      p_name: new FormControl(''),    
      message_id: new FormControl(''),
      contact_id: new FormControl(''),
    });
  }
  getContactLists() {
    var url = 'Whatsapp/GetContacts'
    this.service.get(url).subscribe((result: any) => {    
      this.contacts_list = result.whatscontact_list;
    });
  }
  onGlobalFilterChange(event: Event, dt: Table): void {
    const inputValue = (event.target as HTMLInputElement).value;
    dt.filterGlobal(inputValue, 'contains');
  }
  onsend(): void {
    this.CurObj.contacts_list = this.selectedCustomer
    this.CurObj.sendtext = this.reactiveMessageForm.value.sendtext
    if (this.CurObj.contacts_list.length == 0) {
      this.ToastrService.warning("Kindly select atleast 1 contact")
      return;
    }
    this.NgxSpinnerService.show();
    var url = 'Whatsapp/bulkcustomizeMessageSend'
    this.service.post(url, this.CurObj).subscribe((result: any) => {
      if (result.status == false) {
        window.scrollTo({
          top: 0, // Code is used for scroll top after event done
        });
        this.ToastrService.warning(result.message)
      }
      else {
        window.scrollTo({
          top: 0, // Code is used for scroll top after event done
        });
        this.ToastrService.success(result.message)
      }
      this.NgxSpinnerService.hide();
      this.selectedCustomer = [];
      this.reactiveMessageForm.reset();

    });
  }

}
