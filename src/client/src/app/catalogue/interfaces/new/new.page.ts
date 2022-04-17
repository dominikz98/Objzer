import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { InterfacesEndpoints } from 'src/app/endpoints/interfaces.endpoints';
import { AddPropertyVM, ListInterfaceVM } from 'src/app/models/viewmodels';
import { EditPropertyModalPage } from 'src/app/modals/edit-property-modal/edit-property-modal.page';
import { PropertyModel } from 'src/app/models/property.model';
import { InterfaceModel } from '../../../models/interface.model';
import { Router } from '@angular/router';
@Component({
  selector: 'app-new',
  templateUrl: './new.page.html',
  styleUrls: ['./new.page.scss'],
})
export class NewPage implements OnInit {

  public interfaces: ListInterfaceVM[];
  public model: InterfaceModel;

  constructor(private endpoints: InterfacesEndpoints,
    private modalCtrl: ModalController,
    private router: Router) {
    this.model = new InterfaceModel();
  }

  ngOnInit() {
    this.endpoints.get()
      // .pipe(
      //   filter(x => x.id != this.model.value.id)
      // )
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
        this.model.value.properties.push(property.value);
      }
    });

    return await modal.present();
  }

  removeProperty(property: AddPropertyVM) {
    const index = this.model.value.properties.indexOf(property, 0);
    if (index > -1) {
      this.model.value.properties.splice(index, 1);
    }
  }

  onSave() {
    if (this.model.form.invalid || !this.model.form.touched) {
      return;
    }

    this.model.fillUp();
    this.endpoints.create(this.model.value).subscribe();

    // Navigates to url entry to destroy page lifecycle
    this.router.navigate(['/']).then(() => {
      this.router.navigate(['/catalogue/interfaces']);
    });
  }

  onImplementationChange($event) {
    if ($event.target.value.id == null) {
      return;
    }

    if ($event.target.checked) {
      this.model.value.implementationIds.push($event.target.value.id);
    } else {
      const index = this.model.value.implementationIds.indexOf($event.target.value.id, 0);
      if (index > -1) {
        this.model.value.implementationIds.splice(index, 1);
      }
    }
  }
}
