import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './ems.utilities/auth/Guard/auth.guard';
const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('./ems.utilities/ems.utilities.module').then((m) => m.EmsUtilitiesModule),
  },
  {
    path: 'system',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.system/ems.system.module').then((m) => m.EmsSystemModule),
  },
  {
    path: 'hrm',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.hrm/ems.hrm.module').then((m) => m.EmsHrmModule),
  },
  {
    path: 'payroll',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.payroll/ems.payroll.module').then((m) => m.EmsPayrollModule),
  },
  {
    path: 'rsk',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.rsk/ems.rsk.module').then((m) => m.EmsRskModule),
  },
  {
    path: 'crm',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.crm/ems.crm.module').then((m) => m.EmsCrmModule),
  },
  {
    path: 'pmr',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.pmr/ems.pmr.module').then((m) => m.EmsPmrModule),
  },
  {
    path: 'ims',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.inventory/ems.inventory.module').then((m) => m.EmsInventoryModule),
  },
  {
    path: 'finance',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.finance/ems.finance.module').then((m) => m.EmsFinanceModule),
  },
  {
    path: 'einvoice',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.einvoice/ems.einvoice.module').then((m) => m.EmsEinvoiceModule),
  },
  {
    path: 'smr',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.sales/ems.sales.module').then((m) => m.EmsSalesModule),
  },
  {
    path: 'asset',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.asset/ems.asset.module').then((m) => m.EmsAssetModule),
  },
  {
    path: 'payable',canActivate: [AuthGuard], 
    loadChildren: () =>
      import('./ems.payable/ems.payable.module').then((m) => m.EmsPayableModule),
  },
  {
    path: 'payroll',canActivate: [AuthGuard],
    loadChildren: () =>
      import('./ems.payroll/ems.payroll.module').then((m) => m.EmsPayrollModule),
  },
  {
    path: '**',
    redirectTo: '/auth/login'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash:true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
