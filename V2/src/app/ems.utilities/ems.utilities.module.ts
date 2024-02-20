import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmsUtilitiesRoutingModule } from './ems.utilities-routing.module';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './auth/login/login.component';
import { Error404Component } from './auth/Error/components/error404/error404.component';
import { Error401Component } from './auth/Error/components/error401/error401.component';
import { Error500Component } from './auth/Error/components/error500/error500.component';



@NgModule({
  declarations: [
    LoginComponent,
    Error404Component,
    Error401Component,
    Error500Component
  ],
  imports: [
    CommonModule,
    EmsUtilitiesRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    
  ]
})
export class EmsUtilitiesModule { }
