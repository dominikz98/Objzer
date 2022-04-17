import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { NewPageRoutingModule } from './new-routing.module';

import { NewPage } from './new.page';
import { EditPropertyModalPageModule } from 'src/app/modals/edit-property-modal/edit-property-modal.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    NewPageRoutingModule,
    ReactiveFormsModule,
    EditPropertyModalPageModule
  ],
  declarations: [NewPage]
})
export class NewPageModule { }
