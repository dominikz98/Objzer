import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EnumerationsPage } from './enumerations.page';

const routes: Routes = [
  {
    path: '',
    component: EnumerationsPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EnumerationsPageRoutingModule {}
