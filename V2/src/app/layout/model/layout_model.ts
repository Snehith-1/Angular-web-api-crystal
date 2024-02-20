import { animate, style, transition, trigger } from "@angular/animations";

export interface sub1menu{
  expanded?: boolean;
  sref:string;
  text: string;
  sub2menu: sub2menu[];
}

export interface sub2menu{
  expanded?: boolean;
  sref:string;
  text: string;
  items: [];
}


export interface INavbarData {
    // routeLink: string;
    // icon?: string;
    // label: string;
    expanded?: boolean;
    // items?: INavbarData[];
    sref:string;
    text: string;
    sub1menu: sub1menu[];
}

export const fadeInOut = trigger('fadeInOut', [
    transition(':enter', [
      style({opacity: 0}),
      animate('350ms',
        style({opacity: 1})
      )
    ]),
    transition(':leave', [
      style({opacity: 1}),
      animate('350ms',
        style({opacity: 0})
      )
    ])
  ])