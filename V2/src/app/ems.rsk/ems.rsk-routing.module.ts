import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';  
import { RskMstDashboardComponent } from './component/rsk-mst-dashboard/rsk-mst-dashboard.component';

const routes: Routes = [

  { path: 'RskMstDashboard', component: RskMstDashboardComponent}, 
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmsRskRoutingModule { }
