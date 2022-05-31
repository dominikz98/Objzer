import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ObjectsEndpoints } from 'src/app/endpoints/objects.endpoints';
import { Filter } from 'src/app/enums/Filter';
import { ListObjectVM } from 'src/app/models/viewmodels';

@Component({
  selector: 'app-objects',
  templateUrl: './objects.page.html',
  styleUrls: ['./objects.page.scss'],
})
export class ObjectsPage implements OnInit {

  public objects: ListObjectVM[];
  public searchField: FormControl;

  private filter: Filter = Filter.Active;
  private allObjects: ListObjectVM[];

  constructor(
    private endpoints: ObjectsEndpoints,
    private router: Router) {
    this.searchField = new FormControl('');
  }

  ngOnInit() {
    this.searchField.valueChanges.subscribe(() => {
      this.filterItems();
    })
    this.endpoints.getAll()
      .subscribe((data: ListObjectVM[]) => {
        this.allObjects = data;
        this.filterItems();
      });
  }


  onEdit(value: ListObjectVM) {
    this.router.navigate(['catalogue/objects/edit', value.id]);
  }

  checkIsSelected(filter: Filter): string {
    if (this.filter == filter) {
      return 'active-filter';
    }

    return '';
  }

  onFilterAll() {
    this.filter = Filter.All;
    this.filterItems();
  }

  onFilterActive() {
    this.filter = Filter.Active;
    this.filterItems();
  }

  onFilterLocked() {
    this.filter = Filter.Locked;
    this.filterItems();
  }

  onFilterArchived() {
    this.filter = Filter.Archived;
    this.filterItems();
  }

  filterItems() {
    // apply search
    let tmpObjects = this.allObjects.filter(x => x.name.toLowerCase().startsWith(this.searchField.value.toLowerCase()));

    // apply filter
    switch (this.filter) {
      case Filter.All: {
        this.objects = tmpObjects;
        break;
      }
      case Filter.Active: {
        this.objects = tmpObjects.filter(x => !x.archived && !x.locked);
        break;
      }
      case Filter.Locked: {
        this.objects = tmpObjects.filter(x => x.locked);
        break;
      }
      case Filter.Archived: {
        this.objects = tmpObjects.filter(x => x.archived);
        break;
      }
    }
  }
}
