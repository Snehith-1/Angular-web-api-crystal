import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { CalendarOptions } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import { Ccalender } from 'src/app/layout/model/calendarmodel';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-hrm-member-officecalendar',
  templateUrl: './hrm-member-officecalendar.component.html',
  styleUrls: ['./hrm-member-officecalendar.component.scss']
})
export class HrmMemberOfficecalendarComponent {
  event: any;
  createevent: any[] = [];
  getData2: any;
  createeventform!: FormGroup;

  onDateClick(event: any) {
    console.log('Date clicked:', event);
  }

calendarOptions: CalendarOptions = {    initialView: 'dayGridMonth',    plugins: [dayGridPlugin] };

  responsedata: any;
  monthlyreport: any;
  holidaycalender_list : any;

  
  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService, private NgxSpinnerService: NgxSpinnerService) {
    this.createeventform = new FormGroup({
      event_date: new FormControl(''),
      event_time: new FormControl(''),
      event_title: new FormControl('')
    })
 
  }
  ngOnInit(): void {
    var api = 'hrmTrnDashboard/holidaycalender';
    this.service.get(api).subscribe((result:any) => {
      this.responsedata = result;
      this.holidaycalender_list = this.responsedata.holidaycalender_list;
      setTimeout(()=>{  
        $('#office-calendar').DataTable();
      }, 1);
    });


    setTimeout(() => {

      this.getData2.getEventcalendar().subscribe((result: any,)=>{
        this.responsedata=result;
        this.createevent = this.responsedata.createevent;
        this.createevent.forEach(obj =>{ 
          // console.log(obj.event_title);
          var objCcalender = new Ccalender
          objCcalender.title = obj.event_title;
          objCcalender.start = obj.event_time; 
          this.event.push(objCcalender);
        })
      
      });
    
    }, 2200);
   
  }
   
  createeventsubmit() {
    debugger;
    var url = 'hrmTrnDashboard/event';
    this.service.post(url, this.createeventform.value).subscribe((result: any) => {
      if (result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }

 
  back() {
    this.router.navigate(['/hrm/HrmMemberDashboard'])
  }
}
