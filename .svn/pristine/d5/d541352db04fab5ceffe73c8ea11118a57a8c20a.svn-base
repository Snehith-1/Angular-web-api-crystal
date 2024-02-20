import { Component, Input } from '@angular/core';

@Component({
  selector: 'layout-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {

  @Input() collapsed = false;
  @Input() screenWidth = 0;
  
  getFooterClass(): string{
    let styleClass = '';
    if(this.collapsed && this.screenWidth > 768){
      styleClass = 'footer-trimmed';
    }else if(this.collapsed && this.screenWidth <= 768 && this.screenWidth > 0) {
      styleClass = 'footer-md-screen';
    }
    return styleClass;
  }
}
