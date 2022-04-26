import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Platform } from '@ionic/angular';
import { InterfacesEndpoints } from 'src/app/endpoints/interfaces.endpoints';
import { ListInterfaceVM } from 'src/app/models/viewmodels';

@Component({
  selector: 'app-interfaces',
  templateUrl: './interfaces.page.html',
  styleUrls: ['./interfaces.page.scss'],
})
export class InterfacesPage implements OnInit {

  public interfaces: ListInterfaceVM[];
  public isMobile: boolean;

  constructor(
    private endpoints: InterfacesEndpoints,
    private router: Router) {
  }

  ngOnInit() {
    this.endpoints.getAll()
      .subscribe((data: ListInterfaceVM[]) => {
        this.interfaces = data;
      });
  }

  onEdit(value: ListInterfaceVM) {
    this.router.navigate(['catalogue/interfaces/edit', value.id]);
  }
}
