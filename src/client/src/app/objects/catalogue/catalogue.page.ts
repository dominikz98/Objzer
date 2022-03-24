import { Component, OnInit } from '@angular/core';
import { ObjectsEndpoints } from 'src/app/endpoints/objects.endpoints';

@Component({
  selector: 'app-catalogue',
  templateUrl: './catalogue.page.html',
  styleUrls: ['./catalogue.page.scss'],
})
export class CataloguePage implements OnInit {

  objects: any = {};

  constructor(private endpoints: ObjectsEndpoints) { }

  ngOnInit() {
    this.endpoints.get()
      .subscribe((data: {}) => {
        this.objects = data;
      })
  }

}
