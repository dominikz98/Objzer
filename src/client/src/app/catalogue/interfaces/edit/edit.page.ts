import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalController } from '@ionic/angular';
import { map } from 'rxjs/operators';
import { InterfacesEndpoints } from 'src/app/endpoints/interfaces.endpoints';
import { EditPropertyModalPage } from 'src/app/modals/edit-property-modal/edit-property-modal.page';
import { InterfaceModel } from 'src/app/models/interface.model';
import { PropertyModel } from 'src/app/models/property.model';
import { AddPropertyVM, EditInterfaceVM, InterfaceVM, ListInterfaceVM } from 'src/app/models/viewmodels';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.page.html',
  styleUrls: ['./edit.page.scss'],
})
export class EditPage implements OnInit {
  public interfaces: ListInterfaceVM[];
  public model: InterfaceModel;

  constructor(private endpoints: InterfacesEndpoints,
    private modalCtrl: ModalController,
    private router: Router,
    private route: ActivatedRoute) {
  }

  ngOnInit() {

    this.tryLoadInterface();
    this.loadInterfaces();
  }

  tryLoadInterface(): any {
    let id: string;
    this.route.params.subscribe(params => {
      id = params['id'];
    });

    if (id == null) {
      return null;
    }

    this.endpoints.getById(id)
      .subscribe((response: any) => {
        if (response == null) {
          this.model = new InterfaceModel(null);
        } else {
          var casted = response.value as EditInterfaceVM;
          this.model = new InterfaceModel(casted);
        }
      })
  }

  loadInterfaces() {
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

    if (this.model.value.id == null) {
      this.endpoints.create(this.model.value).subscribe();
    } else {
      this.endpoints.update(this.model.value).subscribe();
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
