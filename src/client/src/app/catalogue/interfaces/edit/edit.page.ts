import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalController, ToastController } from '@ionic/angular';
import { map } from 'rxjs/operators';
import { InterfacesEndpoints } from 'src/app/endpoints/interfaces.endpoints';
import { EditPropertyModalPage } from 'src/app/modals/edit-property-modal/edit-property-modal.page';
import { InterfaceModel } from 'src/app/models/interface.model';
import { PropertyModel } from 'src/app/models/property.model';
import { InterfaceVM, ListInterfaceVM, PropertyVM } from 'src/app/models/viewmodels';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.page.html',
  styleUrls: ['./edit.page.scss'],
})
export class EditPage implements OnInit {
  public interfaces: ListInterfaceVM[];
  public model: InterfaceModel;
  public modified: boolean;
  public historyCutOff: number = 6;

  private id: string;

  constructor(private endpoints: InterfacesEndpoints,
    private modalCtrl: ModalController,
    private router: Router,
    private route: ActivatedRoute) {
  }

  async ngOnInit() {
    this.route.params.subscribe(async params => {
      this.id = params['id'];

      this.loadInterface();
      this.loadIncludings();
    });
  }

  onDelete() {
    this.endpoints.remove(this.id).subscribe();
  }

  onLock() {
    this.endpoints.lock(this.id)
      .subscribe(() => {
        this.loadInterface();
        this.model.form.disable();
      });
  }

  onUnlock() {
    this.endpoints.unlock(this.id)
      .subscribe(() => {
        this.loadInterface();
        this.model.form.enable();
      });
  }

  onArchive() {
    this.endpoints.archive(this.id)
      .subscribe(() => {
        this.loadInterface();
        this.model.form.enable();
      });
  }

  onRestore() {
    this.endpoints.restore(this.id)
      .subscribe(() => {
        this.loadInterface();
        this.model.form.enable();
      });
  }

  loadInterface() {
    if (this.id == null) {
      this.model = new InterfaceModel(null);
      return;
    }

    this.endpoints.getById(this.id)
      .subscribe((response: any) => {
        var casted = response?.value as InterfaceVM;
        this.model = new InterfaceModel(casted);
        this.model.value.history = this.model.value.history.slice(0, this.historyCutOff);

        if (this.model.value.locked) {
          this.model.form.disable();
        }
      });
  }

  loadIncludings() {
    this.endpoints.getAll().pipe(
      map(data =>
        data.filter(entry => {
          return this.id != null && entry.id != this.id;
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
    this.router.navigate(['/catalogue/interfaces'])
      .then(() => {
        window.location.reload();
      });
  }

  onImplementationChange($event) {
    if ($event.target.value.id == null) {
      return;
    }

    this.modified = true;
    this.addOrRemoveImplementation($event.target.value.id, $event.target.checked);

    console.log(this.model.value.includingIds);
  }

  addOrRemoveImplementation(id: string, checked: boolean) {
    if (checked) {
      this.model.value.includingIds.push(id);
    } else {
      const index = this.model.value.includingIds.indexOf(id, 0);

      if (index > -1) {
        this.model.value.includingIds.splice(index, 1);
      }
    }
  }

  getHistoryColor(action: number): string {
    switch (action) {
      case 0:
        return 'item-history-add-color'
      case 1:
        return 'item-history-upd-color'
      case 2:
        return 'item-history-del-color'
      case 3:
      case 4:
        return 'item-history-lock-color'
      case 5:
      case 6:
        return 'item-history-archive-color'
      default:
        return;
    }
  }

  getHistoryName(action: number): string {
    switch (action) {
      case 0:
        return 'Add'
      case 1:
        return 'Update'
      case 2:
        return 'Delete'
      case 3:
        return 'Lock'
      case 4:
        return 'Unlock'
      case 5:
        return 'Archive'
      case 6:
        return 'Restore'
      default:
        return;
    }
  }
}
