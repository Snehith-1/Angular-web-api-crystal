import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AmsMstBlockComponent } from './component/ams-mst-block/ams-mst-block.component';
import { AmsMstAttributeComponent } from './component/ams-mst-attribute/ams-mst-attribute.component';
import { AmsMstProductsubgroupComponent } from './component/ams-mst-productsubgroup/ams-mst-productsubgroup.component';
import { AmsMstUnitSummaryComponent } from './component/ams-mst-unit-summary/ams-mst-unit-summary.component';
import { AmsMstProductgroupSummaryComponent } from './component/ams-mst-productgroup-summary/ams-mst-productgroup-summary.component';
const routes: Routes = [
  { path: 'AmsMstBlock', component: AmsMstBlockComponent},
  { path: 'AmsMstAttribute', component: AmsMstAttributeComponent},
  { path: 'AmsMstProductsubgroup', component: AmsMstProductsubgroupComponent},
  { path: 'AmsMstUnitSummary', component: AmsMstUnitSummaryComponent},
  { path: 'AmsMstProductGroupSummary', component: AmsMstProductgroupSummaryComponent},

  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class EmsAssetRoutingModule { }
  