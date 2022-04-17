import { Component, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { ModalController } from '@ionic/angular';
import { InterfacesEndpoints } from 'src/app/endpoints/interfaces.endpoints';
import { ListInterfaceVM } from 'src/app/endpoints/viewmodels';
import { EditPropertyModalPage } from 'src/app/modals/edit-property-modal/edit-property-modal.page';
import { PropertyModel } from 'src/app/models/property.model';
import { InterfaceModel } from '../../../models/interface.model';

@Component({
  selector: 'app-new',
  templateUrl: './new.page.html',
  styleUrls: ['./new.page.scss'],
})
export class NewPage implements OnInit {

  public interfaces: ListInterfaceVM[];
  public model: InterfaceModel;

  constructor(private endpoints: InterfacesEndpoints, private modalCtrl: ModalController) {
    this.model = new InterfaceModel();
  }

  ngOnInit() {
    this.endpoints.get()
      .subscribe((data: ListInterfaceVM[]) => {
        this.interfaces = data;
      })
  }

  async addProperty() {
    const modal = await this.modalCtrl.create({
      component: EditPropertyModalPage
    });

    modal.onDidDismiss().then((result) => {
      if (result.data != null) {
        const property = result.data as PropertyModel;
        this.model.attachProperty(property);
      }
    });

    return await modal.present();
  }
}
