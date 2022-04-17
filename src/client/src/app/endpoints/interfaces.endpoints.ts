import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClient } from './apiclient';
import { AddInterfaceVM, InterfaceVM, ListInterfaceVM } from '../models/viewmodels';


@Injectable({
  providedIn: 'root'
})
export class InterfacesEndpoints {

  constructor(private client: ApiClient) { }

  get(): Observable<ListInterfaceVM[]> {
    return this.client.get<ListInterfaceVM[]>('interfaces');
  }

  create(model: AddInterfaceVM): Observable<InterfaceVM> {
    return this.client.post<AddInterfaceVM, InterfaceVM>('interfaces', model);
  }
}
