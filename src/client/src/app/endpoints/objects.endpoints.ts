import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClient } from './apiclient';
import { ObjectVM } from './viewmodels';


@Injectable({
  providedIn: 'root'
})
export class ObjectsEndpoints {

  constructor(private client: ApiClient) { }

   get(): Observable<ObjectVM> {
    return this.client.get<ObjectVM>('objects/catalogue');
  }
}
