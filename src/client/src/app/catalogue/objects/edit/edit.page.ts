import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalController } from '@ionic/angular';
import { ObjectsEndpoints } from 'src/app/endpoints/objects.endpoints';
import { ObjectModel } from 'src/app/models/object.model';
import { ListObjectVM, ObjectVM } from 'src/app/models/viewmodels';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.page.html',
  styleUrls: ['./edit.page.scss'],
})
export class EditPage implements OnInit {

  public interfaces: ListObjectVM[];
  public model: ObjectModel;
  public modified: boolean;
  public historyCutOff: number = 6;

  private id: string;

  constructor(private endpoints: ObjectsEndpoints,
    private modalCtrl: ModalController,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(async params => {
      this.id = params['id'];

      this.loadObject();
    });
  }


  onDelete() {
    this.endpoints.remove(this.id).subscribe();
  }

  onLock() {
    this.endpoints.lock(this.id)
      .subscribe(() => {
        this.loadObject();
        this.model.form.disable();
      });
  }

  onUnlock() {
    this.endpoints.unlock(this.id)
      .subscribe(() => {
        this.loadObject();
        this.model.form.enable();
      });
  }

  onArchive() {
    this.endpoints.archive(this.id)
      .subscribe(() => {
        this.loadObject();
        this.model.form.enable();
      });
  }

  onRestore() {
    this.endpoints.restore(this.id)
      .subscribe(() => {
        this.loadObject();
        this.model.form.enable();
      });
  }

  loadObject() {
    if (this.id == null) {
      this.model = new ObjectModel(null);
      return;
    }

    this.endpoints.getById(this.id)
      .subscribe((response: any) => {
        var casted = response?.value as ObjectVM;
        this.model = new ObjectModel(casted);
        this.model.value.history = this.model.value.history.slice(0, this.historyCutOff);

        if (this.model.value.locked) {
          this.model.form.disable();
        }
      });
  }
}
