import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { HeaderComponent } from '../../../layout/components/header/header.component';
import { Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';

@Component({
  selector: 'app-hrm-trn-iattendance',
  templateUrl: './hrm-trn-iattendance.component.html',
  styleUrls: ['./hrm-trn-iattendance.component.scss']
})

export class HrmTrnIattendanceComponent {
  attendanceform: FormGroup | any;
  responsedata: any;
  Time: string;
  buttonClicked: boolean | undefined;
  button1Clicked: boolean | undefined;
  Date: any;

  ngOnInit() {
    setInterval(() => {
      this.Time = new Date().toString();
    }, 1000);
  }

  constructor(public router: Router, private service: SocketService) {
    this.Time = new Date().toString();
    HeaderComponent.constructor(); {
      setInterval(() => {
        this.Time = new Date().toString();
      }, 1000);
    }
    {
      this.attendanceform = new FormGroup({
        location: new FormControl(''),
      });
    }
  }

  signin() {
    const api = 'Iattendance/PostSignIn';
    this.service.post(api, this.attendanceform.value).subscribe((result: any) => {
      this.responsedata = result;
    }, (error: any) => {
      if (error.status === 401)
        this.router.navigate(['pages/401'])
      else if (error.status === 404)
        this.router.navigate(['pages/404'])
    });
  }

  punchin() {
    this.buttonClicked = !this.buttonClicked;
  }

  punchout() {
    this.button1Clicked = !this.button1Clicked;
   
  }

  signout() {
    const api = 'Iattendance/PostSignOut';
    this.service.post(api, this.attendanceform.value).subscribe((result: any) => {
      this.responsedata = result;
    }, (error: any) => {
      if (error.status === 401)
        this.router.navigate(['pages/401'])
      else if (error.status === 404)
        this.router.navigate(['pages/404'])
    });
  }

  // redirecttolist() {
  //   this.router.navigate(['/hrm/HrmMemberDashboard']);
  // }
}

function In() {
  throw new Error('Function not implemented.');
}