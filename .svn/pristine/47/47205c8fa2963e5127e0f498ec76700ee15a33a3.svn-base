import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Error401Component } from './auth/Error/components/error401/error401.component';
import { Error404Component } from './auth/Error/components/error404/error404.component';
import { Error500Component } from './auth/Error/components/error500/error500.component';
import { LoginComponent } from './auth/login/login.component';

const routes: Routes = [
  {
    path:'login', component: LoginComponent
  },
  {
    path:'401', component: Error401Component
  },
  {
    path:'404', component: Error404Component
  },
  {
    path:'500', component: Error500Component
  },
  {
    path:'', redirectTo : '/auth/login', pathMatch:'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmsUtilitiesRoutingModule { }
