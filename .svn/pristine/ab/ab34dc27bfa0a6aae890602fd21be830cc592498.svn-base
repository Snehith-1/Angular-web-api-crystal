import { NgModule,CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import{BrowserAnimationsModule} from '@angular/platform-browser/animations'
import { CKEditorModule } from 'ng2-ckeditor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmsUtilitiesModule } from './ems.utilities/ems.utilities.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthinterceptorInterceptor } from './ems.utilities/services/authinterceptor.interceptor';
import { ToastrModule } from 'ngx-toastr'; //toastr
import { NgxSpinnerModule } from 'ngx-spinner'; //spinner
import { LayoutModule } from './layout/layout.module'; //Layout
import { AngularEditorModule } from '@kolkov/angular-editor';
import { TabsModule} from 'ngx-bootstrap/tabs';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { DatePipe } from '@angular/common';
import { NgxIntlTelInputModule } from 'ngx-intl-tel-input';
import { FullCalendarModule } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    EmsUtilitiesModule,
    FullCalendarModule,
    FormsModule,AngularEditorModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    NgxSpinnerModule.forRoot({type:'ball-spin-clockwise'}),
    LayoutModule,
    CKEditorModule,
    TabsModule.forRoot(),TimepickerModule.forRoot(),NgxIntlTelInputModule
  ],
  //Spinner
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    {
      provide: HTTP_INTERCEPTORS, 
      useClass: AuthinterceptorInterceptor, 
      multi: true
    },DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { 
  calendarPlugins = [dayGridPlugin];
}
