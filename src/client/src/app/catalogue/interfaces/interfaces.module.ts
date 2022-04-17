import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { InterfacesPageRoutingModule } from './interfaces-routing.module';

import { InterfacesPage } from './interfaces.page';
import { PipesModule } from 'src/app/pipes/pipes.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    InterfacesPageRoutingModule,
    PipesModule
  ],
  declarations: [InterfacesPage]
})
export class InterfacesPageModule { }
