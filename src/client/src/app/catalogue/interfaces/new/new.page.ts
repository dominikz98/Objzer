import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { InterfacesEndpoints } from 'src/app/endpoints/interfaces.endpoints';
import { ListInterfaceVM } from 'src/app/models/viewmodels';
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

  onSave() {
    if (this.model.form.invalid || !this.model.form.touched) {
      return;
    }

    console.log('Fillup');
    this.model.fillUp();
    console.log('Create');
    this.endpoints.create(this.model.value).subscribe();
    console.log('Finished');
  }

  // findInvalidControls() {
  //   const controls = this.model.form.controls;
  //   for (const name in controls) {
  //     if (controls[name].invalid) {
  //       console.log(`Errors: ${name}`)
  //     }
  //   }
  // }
}
