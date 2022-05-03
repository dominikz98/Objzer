import { Injectable } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { ApiClient } from './apiclient';
import { EditInterfaceVM, IdVM, InterfaceVM, ListInterfaceVM } from '../models/viewmodels';


@Injectable({
  providedIn: 'root'
})
export class InterfacesEndpoints {

  private route: string = 'interfaces';

  constructor(private client: ApiClient) { }

  getById(id: string): Observable<InterfaceVM> {
    return this.client.get<InterfaceVM>(`${this.route}/${id}`);
  }

  getAll(): Observable<ListInterfaceVM[]> {
    return this.client.get<ListInterfaceVM[]>(this.route);
  }

  create(model: EditInterfaceVM): Observable<InterfaceVM> {
    return this.client.post<EditInterfaceVM, InterfaceVM>(this.route, model);
  }

  update(model: EditInterfaceVM): Observable<InterfaceVM> {
    return this.client.put<EditInterfaceVM, InterfaceVM>(this.route, model);
  }

  lock(id: string): Observable<any> {
    return this.client.putByUrl(`${this.route}/lock/${id}`);
  }

  unlock(id: string): Observable<any> {
    return this.client.putByUrl(`${this.route}/unlock/${id}`);
  }

  archive(id: string): Observable<any> {
    return this.client.putByUrl(`${this.route}/archive/${id}`);
  }

  restore(id: string): Observable<any> {
    return this.client.putByUrl(`${this.route}/restore/${id}`);
  }

  remove(id: string): Observable<IdVM> {
    return this.client.delete(this.route, { id: id });
  }
}
