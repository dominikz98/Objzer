import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditPropertyModalPageRoutingModule } from './edit-property-modal-routing.module';

import { EditPropertyModalPage } from './edit-property-modal.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ReactiveFormsModule, 
    EditPropertyModalPageRoutingModule
  ],
  declarations: [EditPropertyModalPage]
})
export class EditPropertyModalPageModule {}
