import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { InterfacesPage } from './interfaces.page';

const routes: Routes = [
  {
    path: '',
    component: InterfacesPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InterfacesPageRoutingModule {}
