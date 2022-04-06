import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EnumerationsPageRoutingModule } from './enumerations-routing.module';

import { EnumerationsPage } from './enumerations.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EnumerationsPageRoutingModule
  ],
  declarations: [EnumerationsPage]
})
export class EnumerationsPageModule {}
