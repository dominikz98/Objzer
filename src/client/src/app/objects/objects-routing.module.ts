import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ObjectsPage } from './objects.page';

const routes: Routes = [
  {
    path: '',
    component: ObjectsPage,
    children: [
      {
        path: '',
        redirectTo: '/objects/catalogue',
        pathMatch: 'full'
      },
      {
        path: 'catalogue',
        children: [
          {
            path: '',
            loadChildren: () => import('./catalogue/catalogue.module').then(m => m.CataloguePageModule)
          }
        ]
      },
      {
        path: 'contracts',
        children: [
          {
            path: '',
            loadChildren: () => import('./contracts/contracts.module').then(m => m.ContractsPageModule)
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
export class ObjectsPageRoutingModule {}
