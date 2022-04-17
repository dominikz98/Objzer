import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { NewPageRoutingModule } from './new-routing.module';

import { NewPage } from './new.page';
import { PipesModule } from 'src/app/pipes/pipes.module';
import { ModalsModule } from 'src/app/modals/modals.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    NewPageRoutingModule,
    ReactiveFormsModule,
    ModalsModule,
    PipesModule
  ],
  declarations: [NewPage]
})
export class NewPageModule { }
