import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CataloguePage } from './catalogue.page';

const routes: Routes = [
  {
    path: '',
    component: CataloguePage,
    children: [
      {
        path: '',
        redirectTo: '/catalogue/objects',
        pathMatch: 'full'
      },
      {
        path: 'objects',
        children: [
          {
            path: '',
            loadChildren: () => import('./objects/objects.module').then(m => m.ObjectsPageModule)
          }
        ]
      },
      {
        path: 'interfaces',
        children: [
          {
            path: '',
            loadChildren: () => import('./interfaces/interfaces.module').then(m => m.InterfacesPageModule)
          }
        ]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CataloguePageRoutingModule {}
