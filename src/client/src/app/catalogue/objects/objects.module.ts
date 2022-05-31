import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ObjectsPageRoutingModule } from './objects-routing.module';

import { ObjectsPage } from './objects.page';
import { PipesModule } from 'src/app/pipes/pipes.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    ObjectsPageRoutingModule,
    PipesModule
  ],
  declarations: [ObjectsPage]
})
export class ObjectsPageModule {}
