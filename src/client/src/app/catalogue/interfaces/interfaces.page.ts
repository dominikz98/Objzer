import { Component, OnInit } from '@angular/core';
import { InterfacesEndpoints } from 'src/app/endpoints/interfaces.endpoints';
import { ListInterfaceVM } from 'src/app/endpoints/viewmodels';

@Component({
  selector: 'app-interfaces',
  templateUrl: './interfaces.page.html',
  styleUrls: ['./interfaces.page.scss'],
})
export class InterfacesPage implements OnInit {

  public objects: ListInterfaceVM[];

  constructor(private endpoints: InterfacesEndpoints) { }

  ngOnInit() {
    this.endpoints.get()
      .subscribe((data: ListInterfaceVM[]) => {
        this.objects = data;
      })
  }

}
