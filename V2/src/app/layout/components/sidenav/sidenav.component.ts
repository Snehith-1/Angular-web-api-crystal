import { animate, keyframes, style, transition, trigger } from '@angular/animations';
import { Component, Output, EventEmitter, OnInit, HostListener, Input } from '@angular/core';
import { Router } from '@angular/router';


import { NgxSpinnerService } from 'ngx-spinner';
import { SharedService } from '../../services/shared.service';
import { INavbarData, fadeInOut } from '../../model/layout_model';

interface SideNavToggle {
  screenWidth: number;
  collapsed: boolean;
}

@Component({
  selector: 'layout-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss'],
  animations: [
    fadeInOut,
    trigger('rotate', [
      transition(':enter', [
        animate('1000ms', 
          keyframes([
            style({transform: 'rotate(0deg)', offset: '0'}),
            style({transform: 'rotate(2turn)', offset: '1'})
          ])
        )
      ])
    ])
  ]
})
export class SidenavComponent implements OnInit {

  @Output() onToggleSideNav: EventEmitter<SideNavToggle> = new EventEmitter();
  collapsed = false;
  screenWidth = 0;
  // navData = navbarData;
  multiple: boolean = false;
  received: any;
  submenu: any;
  secondMenu !: INavbarData;

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.screenWidth = window.innerWidth;
    if(this.screenWidth <= 768 ) {
      this.collapsed = false;
      this.onToggleSideNav.emit({collapsed: this.collapsed, screenWidth: this.screenWidth});
    }
  }

  constructor(
    public router: Router,
    public sharedservice:SharedService,
    private NgxSpinnerService: NgxSpinnerService
  ) 
  {
    
  }

  ngOnInit(): void {
    this.NgxSpinnerService.show();
      this.screenWidth = window.innerWidth;
      this.sharedservice.getData().subscribe((data)=>{
        this.received = data;
        if(this.received != null){
          this.submenu = this.received.submenu;
        }
        else{
          this.submenu = [];
        }
        //this.submenu = this.received.submenu;
      });
      this.sharedservice.setFunctionToCall(this.submenucall.bind(this));
      this.sharedservice.setSideNavCloseCall(this.toggleCollapse.bind(this));
      this.sharedservice.selectMenuOneTwo(this.secondMenuIdentifier.bind(this));
      this.sharedservice.selectMenuOne(this.secondMenuIdentifier.bind(this));
      setTimeout(() => {this.NgxSpinnerService.hide()},3000);
    
  }

  toggleCollapse(): void {
    if(this.collapsed === false){
      this.collapsed = !this.collapsed;
      this.onToggleSideNav.emit({collapsed: this.collapsed, screenWidth: this.screenWidth});
    }
  }

  closeSidenav(): void {
    this.collapsed = false;
    this.onToggleSideNav.emit({collapsed: this.collapsed, screenWidth: this.screenWidth});
  }

  handleClick(item: INavbarData): void {
    this.shrinkItems(item);
    item.expanded = !item.expanded;
    this.secondMenu = item;
  }

  getActiveClass(data: INavbarData): string {
    return this.router.url.includes(data.sref) ? 'active' : '';
  }

  shrinkItems(item: INavbarData): void {
    if (!this.multiple) {
      for(let modelItem of this.submenu) {
        if (item !== modelItem && modelItem.expanded) {
          modelItem.expanded = false;
        }
      }
    }
  }

  submenucall(){
    this.toggleCollapse();
    this.shrinkItems(this.submenu);
  }

  redirectToPage(data:string){
    this.router.navigate([data]);
  }

  secondMenuIdentifier(){
    this.sharedservice.setMenuTwo(this.secondMenu);
    this.sharedservice.setMenuOne(this.received);
  }
 
  firstMenuIdentifier(){
    this.sharedservice.setMenuOne(this.received);
  }
}
