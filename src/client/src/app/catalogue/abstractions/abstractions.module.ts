import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { AbstractionsPageRoutingModule } from './abstractions-routing.module';

import { AbstractionsPage } from './abstractions.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    AbstractionsPageRoutingModule
  ],
  declarations: [AbstractionsPage]
})
export class AbstractionsPageModule {}
