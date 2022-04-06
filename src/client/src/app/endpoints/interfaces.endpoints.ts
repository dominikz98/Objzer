import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClient } from './apiclient';
import { ListInterfaceVM } from './viewmodels';


@Injectable({
  providedIn: 'root'
})
export class InterfacesEndpoints {

  constructor(private client: ApiClient) { }

   get(): Observable<ListInterfaceVM[]> {
    return this.client.get<ListInterfaceVM[]>('interfaces');
  }
}
