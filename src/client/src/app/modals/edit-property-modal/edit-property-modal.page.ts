import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { MasterDataEndpoints } from 'src/app/endpoints/masterdata.endpoints';
import { AddPropertyVM, EnumVM } from 'src/app/models/viewmodels';
import { PropertyModel } from 'src/app/models/property.model';

@Component({
  selector: 'app-edit-property-modal',
  templateUrl: './edit-property-modal.page.html',
  styleUrls: ['./edit-property-modal.page.scss'],
})
export class EditPropertyModalPage implements OnInit {

  @Input() property: AddPropertyVM;

  public types: EnumVM[];
  private model: PropertyModel;

  constructor(private masterdata: MasterDataEndpoints, private modalCtrl: ModalController) {
    this.model = new PropertyModel(this.property);
  }

  ngOnInit() {
    this.masterdata.getPropertyTypes()
      .subscribe((data: EnumVM[]) => {
        this.types = data;
      })
  }

  onSave() {
    if (this.model.form.invalid || !this.model.form.touched) {
      return;
    }

    this.model.fillUp();
    this.modalCtrl.dismiss(this.model);
  }

  onCancel() {
    this.modalCtrl.dismiss();
  }
}
