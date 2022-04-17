import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditPropertyModalPage } from './edit-property-modal.page';

const routes: Routes = [
  {
    path: '',
    component: EditPropertyModalPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditPropertyModalPageRoutingModule {}
