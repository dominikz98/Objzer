import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalController } from '@ionic/angular';
import { map } from 'rxjs/operators';
import { InterfacesEndpoints } from 'src/app/endpoints/interfaces.endpoints';
import { EditPropertyModalPage } from 'src/app/modals/edit-property-modal/edit-property-modal.page';
import { InterfaceModel } from 'src/app/models/interface.model';
import { PropertyModel } from 'src/app/models/property.model';
import { EditInterfaceVM, ListInterfaceVM, PropertyVM } from 'src/app/models/viewmodels';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.page.html',
  styleUrls: ['./edit.page.scss'],
})
export class EditPage implements OnInit {
  public interfaces: ListInterfaceVM[];
  public model: InterfaceModel;
  public modified: boolean;

  constructor(private endpoints: InterfacesEndpoints,
    private modalCtrl: ModalController,
    private router: Router,
    private route: ActivatedRoute) {
  }

  async ngOnInit() {
    await this.loadInterface();
    await this.loadIncludings();
  }

  async loadInterface(): Promise<void> {
    let id: string;
    this.route.params.subscribe(params => {
      id = params['id'];
    });

    if (id == null) {
      this.model = new InterfaceModel(null);
      return;
    }

    this.endpoints.getById(id)
      .subscribe((response: any) => {
        var casted = response?.value as EditInterfaceVM;
        this.model = new InterfaceModel(casted);
      });
  }

  async loadIncludings(): Promise<void> {
    this.endpoints.getAll()
      .pipe(
        map(data =>
          data.filter(entry => {
            return this.model.value.id != null && entry.id != this.model.value.id;
          })
        )
      )
      .subscribe((data: ListInterfaceVM[]) => {
        this.interfaces = data;
      });
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

  async editProperty(value: PropertyVM) {

    const modal = await this.modalCtrl.create({
      component: EditPropertyModalPage,
      componentProps: {
        property: value
      }
    });

    modal.onDidDismiss().then((result) => {
      if (result.data != null) {
        let property = result.data as PropertyModel;
        let index = this.model.value.properties.indexOf(property.value);

        if (index < 0) {
          return;
        }

        this.model.value.properties.splice(index, 1, property.value);
        this.modified = true;
      }
    });

    return await modal.present();
  }

  removeProperty(property: PropertyVM) {
    const index = this.model.value.properties.indexOf(property, 0);
    if (index > -1) {
      this.model.value.properties.splice(index, 1);
      this.modified = true;
    }
  }

  async onSave() {
    console.log(this.model.form.invalid);
    if (this.model.form.invalid) {
      return;
    }

    this.model.fillUp();

    if (this.model.value.id == null) {
      await this.endpoints.create(this.model.value).toPromise();
    } else {
      await this.endpoints.update(this.model.value).toPromise();
    }

    // Navigates to url entry to destroy page lifecycle
    this.router.navigate(['/']).then(() => {
      this.router.navigate(['/catalogue/interfaces']);
    });
  }

  onImplementationChange($event) {
    if ($event.target.value.id == null) {
      return;
    }

    this.modified = true;

    if ($event.target.checked) {
      this.model.value.includingIds.push($event.target.value.id);
    } else {
      const index = this.model.value.includingIds.indexOf($event.target.value.id, 0);
      if (index > -1) {
        this.model.value.includingIds.splice(index, 1);
      }
    }
  }
}
