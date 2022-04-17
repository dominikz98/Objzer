import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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

  constructor(private endpoints: InterfacesEndpoints,
    route: ActivatedRoute,
    platform: Platform) {
    this.isMobile = platform.is('ios') || platform.is('android');
    route.params.subscribe(val => {
      this.endpoints.get()
        .subscribe((data: ListInterfaceVM[]) => {
          this.interfaces = data;
        })
    });
  }

  ngOnInit() { }
}
