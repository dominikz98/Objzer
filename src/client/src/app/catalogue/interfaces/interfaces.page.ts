import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { InterfacesEndpoints } from 'src/app/endpoints/interfaces.endpoints';
import { Filter } from 'src/app/enums/Filter';
import { ListInterfaceVM } from 'src/app/models/viewmodels';

@Component({
  selector: 'app-interfaces',
  templateUrl: './interfaces.page.html',
  styleUrls: ['./interfaces.page.scss'],
})
export class InterfacesPage implements OnInit {

  public interfaces: ListInterfaceVM[];
  public searchField: FormControl;
  private filter: Filter = Filter.Active;
  private allInterfaces: ListInterfaceVM[];

  constructor(
    private endpoints: InterfacesEndpoints,
    private router: Router) {
    this.searchField = new FormControl('');
  }

  ngOnInit() {
    this.searchField.valueChanges.subscribe(() => {
      this.filterItems();
    })
    this.endpoints.getAll()
      .subscribe((data: ListInterfaceVM[]) => {
        this.allInterfaces = data;
        this.filterItems();
      });
  }

  onEdit(value: ListInterfaceVM) {
    this.router.navigate(['catalogue/interfaces/edit', value.id]);
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
    let tmpInterfaces = this.allInterfaces.filter(x => x.name.toLowerCase().startsWith(this.searchField.value.toLowerCase()));

    // apply filter
    switch (this.filter) {
      case Filter.All: {
        this.interfaces = tmpInterfaces;
        break;
      }
      case Filter.Active: {
        this.interfaces = tmpInterfaces.filter(x => !x.archived && !x.locked);
        break;
      }
      case Filter.Locked: {
        this.interfaces = tmpInterfaces.filter(x => x.locked);
        break;
      }
      case Filter.Archived: {
        this.interfaces = tmpInterfaces.filter(x => x.archived);
        break;
      }
    }
  }
}