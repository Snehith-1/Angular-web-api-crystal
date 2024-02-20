import { style } from '@angular/animations';
import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SharedService } from '../../services/shared.service';
import { Observable, interval, Subject } from 'rxjs';
import { takeWhile, map, takeUntil, catchError } from 'rxjs/operators';
import { AES,enc } from 'crypto-js';
import { timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

interface menuList {
  sref: string;
  text: string;
}

@Component({
  selector: 'layout-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  private destroy$ = new Subject<void>();
  @Input() collapsed = false;
  @Input() screenWidth = 0;
  menu: any[] | undefined;
  sidemenu: any[] | string[] | undefined;
  menu_name: any;
  firstMenu: any;
  selectedIndex: number = 0;
  level_one_name: any;
  level_two_name: any;
  level_three_name: any;
  level_four_name: any;
  level_one_link: any;
  level_two_link: any;
  level_three_link: any;
  level_four_link: any;
  showBreadCurmList: boolean = false;
  notification_list: any[] | undefined;
  notification_count: number = 0;
  showBadge: boolean = false;
  showMessage: boolean = false;
  windowInterval: any;
  employee_details:any;
  responsedata: any;

  constructor(
    public socketservice: SocketService,
    public router: Router,
    public sharedservice: SharedService,
    private NgxSpinnerService: NgxSpinnerService,
    private route: Router
  ) {
    this.waitForToken().subscribe(() => {
      this.getmenu();
      this.getemployeename();
      this. fetchExchangeRateDaily();
    });
  }
  ngOnInit(): void {


    this.sharedservice.setMenuToCall(this.showBreadCurm.bind(this));
    this.showBreadCurm_local();
    var c_code = localStorage.getItem('c_code')
    if (c_code == "boba_tea") {
      this.windowInterval = window.setInterval(() => {
        var url = 'Whatsapp/waNotifications';
        this.socketservice.get(url).subscribe((result: any) => {
          this.notification_list = result.notification_Lists;
          this.notification_count = result.notification_count;
          if (this.notification_count > 0)
            this.showBadge = true;
        });
      }, 1000);
    }
  
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    var c_code = localStorage.getItem('c_code')
    clearInterval(this.windowInterval)
  }
  waitForToken(): Observable<boolean> {
    return interval(2000) // internal every 2 seconds  
      .pipe(
        takeUntil(this.destroy$), // Cleanup when the component is destroyed
        map(() => {
          const token = localStorage.getItem('token');
          return token !== null && token !== '';
        }),
        takeWhile((tokenAvailable) => !tokenAvailable, true),
        catchError((error) => {
          console.error('Error while polling for token:', error);
          return [];
        })
      );
  }
  getHeaderClass(): string {
    let styleClass = '';
    if (this.collapsed && this.screenWidth > 768) {
      styleClass = 'head-trimmed';
    } else if (this.collapsed && this.screenWidth <= 768 && this.screenWidth > 0) {
      styleClass = 'head-md-screen';
    }
    return styleClass;
  }

  onClickNotification() {
    this.showMessage = !this.showMessage;
  }

  getmenu() {
    this.NgxSpinnerService.show();
    let user_gid = localStorage.getItem('user_gid');
    let param = {
      user_gid: user_gid
    }
    var url = 'User/topmenu';
    this.socketservice.getparams(url, param).subscribe((result: any) => {
      this.menu = result.menu_list;
      this.firstMenu = result.menu_list[0];
      this.sharedservice.setData(this.firstMenu);
      this.destroy$;
    });
    this.NgxSpinnerService.hide();
  }
  logout() {
    localStorage.clear();
    this.router.navigate(['auth/login']);
  }
  social() {
    this.router.navigate(['crm/CrmSocailMediaDashboard']);
  }
  service() {
    this.router.navigate(['crm/CrmSmmCampaignsettings']);
  }
  getsidemenu(data: any) {
    // this.menu_name = data.text;
    this.sharedservice.setData(data);
    this.sharedservice.functionToCall();
    if (data.sref != null && data.sref != "") {
      this.router.navigate([data.sref]);
    }
  }



  selectHead(_index: number) {
    this.selectedIndex = _index;
  }

  showBreadCurm() {
    this.showBreadCurmList = true;
    this.sharedservice.getMenuOne().subscribe((data) => {
      this.level_one_name = data.text;
      this.level_one_link = data.sref;
    });
    this.sharedservice.getMenuTwo().subscribe((data) => {
      this.level_two_name = data.text;
      this.level_two_link = data.sref;
    });
    this.sharedservice.getMenuThree().subscribe((data) => {
      this.level_three_name = data.text;
      this.level_three_link = data.sref;
    });
    this.sharedservice.getMenuFour().subscribe((data) => {
      this.level_four_name = data.text;
      this.level_four_link = data.sref;
    });

    localStorage.removeItem("datas");
    let menuBreadCrum = [
      {
        "level_one_name": this.level_one_name,
        "level_one_link": this.level_one_link,
        "level_two_name": this.level_two_name,
        "level_two_link": this.level_two_link,
        "level_three_name": this.level_three_name,
        "level_three_link": this.level_three_link,
        "level_four_name": this.level_four_name,
        "level_four_link": this.level_four_link
      },
    ]
    localStorage.setItem("datas", JSON.stringify(menuBreadCrum));
  }

  redirect_menu(data: any) {
    if (data != null && data != "") {
      this.router.navigate([data])
    }
  }
  showBreadCurm_local() {
    this.showBreadCurmList = true;
    const menuLocalData = JSON.parse(localStorage.getItem("datas") || '{}');
    const localData = JSON.parse('{}');
    if (menuLocalData != localData) {
      this.level_one_name = menuLocalData[0].level_one_name
      this.level_one_link = menuLocalData[0].level_one_link
      this.level_two_name = menuLocalData[0].level_two_name
      this.level_two_link = menuLocalData[0].level_two_link
      this.level_three_name = menuLocalData[0].level_three_name
      this.level_three_link = menuLocalData[0].level_three_link
      this.level_four_name = menuLocalData[0].level_four_name
      this.level_four_link = menuLocalData[0].level_four_link
    }
  }

  showNotifications(event: Event) {

  }

  routepage() {
    this.router.navigate(['system/MstUserProfile']);
  }

  customer360redirect(param1: string, param2: string) {
    this.showMessage = !this.showMessage;
    const secretKey = 'storyboarderp';
    const lspage1 = "LeadBankdistributor";
    const lspage = AES.encrypt(lspage1, secretKey).toString();
    console.log(param1);
    console.log(param2);
    const leadbank_gid = AES.encrypt(param1, secretKey).toString();
    const lead2campaign_gid = AES.encrypt(param2, secretKey).toString();
    const deencryptedParam = AES.decrypt(leadbank_gid, secretKey).toString(enc.Utf8);
    if(deencryptedParam == "" || deencryptedParam == undefined || deencryptedParam == null){
      this.route.navigate(['/crm/CrmSmmWatsapp']);
    }
    else{
    this.route.navigate(['/crm/CrmTrn360view', leadbank_gid, lead2campaign_gid, lspage]);
    }
  }
  customer360redirect1(param1: string, param2: string) {
    this.showMessage = !this.showMessage;
    const secretKey = 'storyboarderp';
    const lspage1 = "LeadBankdistributor";
    const lspage = AES.encrypt(lspage1, secretKey).toString();
    console.log(param1);
    console.log(param2);
    const leadbank_gid = AES.encrypt(param1, secretKey).toString();
    const lead2campaign_gid = AES.encrypt(param2, secretKey).toString();
    const deencryptedParam = AES.decrypt(leadbank_gid, secretKey).toString(enc.Utf8);
    if(deencryptedParam == "" || deencryptedParam == undefined || deencryptedParam == null){
      this.route.navigate(['/crm/CrmSmmEmailmanagement']);
    }
    else{
    this.route.navigate(['/crm/CrmTrn360view', leadbank_gid, lead2campaign_gid, lspage]);
    }
  }
  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    // Check if the clicked element is not the button or notification area
    if (!event.target || !(event.target as HTMLElement).closest('#notification') && !(event.target as HTMLElement).closest('.sampel')) {
      // Toggle the notification off
      this.showMessage = false;
    }
  }
//  getemployeename(){
//     var url='ManageEmployee/EmployeeProfileView';
//     this.socketservice.get(url).subscribe((result:any)=>{
//       this.employee_details  = result;
//     });
//   }

  getemployeename(){
    let user_gid = localStorage.getItem('user_gid');
      let param = {
        user_gid: user_gid
      }
      var url='ManageEmployee/GetEmployeename';
      this.socketservice.getparams(url, param).subscribe((result:any)=>{
        this.employee_details  = result.employeename_list[0].Name;

      });
    }

    fetchExchangeRate() {
      const url = 'PmrDashboard/GetExchangeRateAsync';
      this.socketservice.get(url).subscribe((result: any) => {
        this.responsedata = result;
        console.log("ExchangeRate")
      });
    }
    
    // Define a function to fetch exchange rate data once a day
    fetchExchangeRateDaily() {
      // Set the interval to 24 hours (86400000 milliseconds)
      const interval = 86400000;
      const url = 'PmrDashboard/GetExchangeRateAsync';
      // Use timer to trigger the API call at specified intervals
      timer(0, interval).pipe(
        switchMap(() => this.socketservice.get(url))
      ).subscribe((result: any) => {
        this.responsedata = result;
      
      });
    }
    
}