import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { InterfacesPageRoutingModule } from './interfaces-routing.module';

import { InterfacesPage } from './interfaces.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    InterfacesPageRoutingModule
  ],
  declarations: [InterfacesPage]
})
export class InterfacesPageModule {}
