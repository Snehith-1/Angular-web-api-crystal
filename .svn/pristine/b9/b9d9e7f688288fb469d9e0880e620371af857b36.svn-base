import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ImsTrnOpeningstockSummaryComponent } from './Component/ims-trn-openingstock-summary/ims-trn-openingstock-summary.component';
import { ImsTrnIssuematerialSummaryComponent } from './Component/ims-trn-issuematerial-summary/ims-trn-issuematerial-summary.component';
import { ImsTrnStockadjustmentSummaryComponent } from './Component/ims-trn-stockadjustment-summary/ims-trn-stockadjustment-summary.component';
import { ImsTrnDeliveryorderComponent } from './Component/ims-trn-deliveryorder/ims-trn-deliveryorder.component';
import { ImsTrnRaiseDeliveryorderComponent } from './Component/ims-trn-raise-deliveryorder/ims-trn-raise-deliveryorder.component';
import { ImsTrnAddDeliveryorderComponent } from './Component/ims-trn-add-deliveryorder/ims-trn-add-deliveryorder.component';
import { ImsTrnOpeningstockAddComponent } from './Component/ims-trn-openingstock-add/ims-trn-openingstock-add.component';
import { ImsTrnDeliveryAcknowledgementComponent } from './Component/ims-trn-delivery-acknowledgement/ims-trn-delivery-acknowledgement.component';
import { ImsTrnDeliveryacknowlegdementAddComponent } from './Component/ims-trn-deliveryacknowlegdement-add/ims-trn-deliveryacknowlegdement-add.component';
import { ImsTrnOpeningstockEditComponent } from './Component/ims-trn-openingstock-edit/ims-trn-openingstock-edit.component';
import { ImsTrnDeliveryacknowledgementUpdateComponent } from './Component/ims-trn-deliveryacknowledgement-update/ims-trn-deliveryacknowledgement-update.component';
import { ImsTrnOpendcsummaryComponent } from './Component/ims-trn-opendcsummary/ims-trn-opendcsummary.component';
import { ImsTrnOpendcAddselectComponent } from './Component/ims-trn-opendc-addselect/ims-trn-opendc-addselect.component';
import { ImsTrnOpendcaddselectUpdateComponent } from './Component/ims-trn-opendcaddselect-update/ims-trn-opendcaddselect-update.component';
import { ImsRptStockreportComponent } from './Component/ims-rpt-stockreport/ims-rpt-stockreport.component';
import { ImsTrnDespatchViewComponent } from './Component/ims-trn-despatch-view/ims-trn-despatch-view.component';
import { ImsTrnDeliveryorderViewComponent } from './Component/ims-trn-deliveryorder-view/ims-trn-deliveryorder-view.component';
const routes: Routes = [
  {path:'ImsTrnOpeningstockSummary', component: ImsTrnOpeningstockSummaryComponent},
  {path:'ImsTrnIssuematerialSummary', component: ImsTrnIssuematerialSummaryComponent},
  {path:'ImsTrnStockadjustmentSummary', component: ImsTrnStockadjustmentSummaryComponent},
  {path:'ImsTrnRaiseDeliveryorder/:salesorder_gid', component: ImsTrnRaiseDeliveryorderComponent},
  {path:'ImsTrnDeliveryorder', component: ImsTrnDeliveryorderComponent},
  {path:'ImsTrnAddDeliveryorder', component: ImsTrnAddDeliveryorderComponent},
  {path:'ImsTrnOpeningstockAdd', component: ImsTrnOpeningstockAddComponent},
  {path:'ImsTrnOpeningstockEdit/:stock_gid', component: ImsTrnOpeningstockEditComponent},
  {path:'ImsTrnDeliveryOrderAcknowlegement', component:ImsTrnDeliveryAcknowledgementComponent},
  {path:'ImsTrnDeliveryAcknowledgemantadd',component:ImsTrnDeliveryacknowlegdementAddComponent},
  {path:'ImsTrnDeliveryacknowledgementUpdate/:directorder_gid',component:ImsTrnDeliveryacknowledgementUpdateComponent},
  {path:'ImsTrnOpendcsummary',component:ImsTrnOpendcsummaryComponent},
  {path:'ImsTrnOpendcAddselect',component:ImsTrnOpendcAddselectComponent},
  {path:'ImsTrnOpendcaddselectUpdate/:salesorder_gid',component:ImsTrnOpendcaddselectUpdateComponent},
  {path:'ImsRptStockreport',component:ImsRptStockreportComponent},
  {path:'ImsTrnDespatchView/:salesorder_gid',component:ImsTrnDespatchViewComponent},
  {path:'ImsTrnDeliveryorderView/:directorder_gid',component:ImsTrnDeliveryorderViewComponent},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmsInventoryRoutingModule { }
