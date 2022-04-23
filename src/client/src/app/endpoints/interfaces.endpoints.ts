import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClient } from './apiclient';
import { EditInterfaceVM, InterfaceVM, ListInterfaceVM } from '../models/viewmodels';


@Injectable({
  providedIn: 'root'
})
export class InterfacesEndpoints {

  constructor(private client: ApiClient) { }

  getById(id: string): Observable<InterfaceVM> {
    return this.client.get<InterfaceVM>(`interfaces/${id}`);
  }

  getAll(): Observable<ListInterfaceVM[]> {
    return this.client.get<ListInterfaceVM[]>('interfaces');
  }

  create(model: EditInterfaceVM): Observable<InterfaceVM> {
    return this.client.post<EditInterfaceVM, InterfaceVM>('interfaces', model);
  }

  update(model: EditInterfaceVM): Observable<InterfaceVM> {
    return this.client.put<EditInterfaceVM, InterfaceVM>('interfaces', model);
  }
}
