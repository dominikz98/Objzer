import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AbstractionsPage } from './abstractions.page';

const routes: Routes = [
  {
    path: '',
    component: AbstractionsPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AbstractionsPageRoutingModule {}
