import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { InterfacesPage } from './interfaces.page';

const routes: Routes = [
  {
    path: '',
    component: InterfacesPage
  },  {
    path: 'new',
    loadChildren: () => import('./new/new.module').then( m => m.NewPageModule)
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InterfacesPageRoutingModule {}
