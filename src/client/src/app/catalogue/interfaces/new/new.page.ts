import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { InterfacesEndpoints } from 'src/app/endpoints/interfaces.endpoints';
import { ListInterfaceVM } from 'src/app/endpoints/viewmodels';
import { EditPropertyModalPage } from 'src/app/modals/edit-property-modal/edit-property-modal.page';
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
      component: EditPropertyModalPage,
      componentProps: {
        'name': 'The Winter Soldier'
      }
    });

    modal.onDidDismiss().then((modalDataResponse) => {
      if (modalDataResponse !== null) {
        console.log('Modal Sent Data : ' + modalDataResponse.data);
      }
    });

    return await modal.present();
  }
}
