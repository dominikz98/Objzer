import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClient } from './apiclient';
import { IdVM, ObjectVM, ListObjectVM } from '../models/viewmodels';


@Injectable({
  providedIn: 'root'
})
export class ObjectsEndpoints {

  private route: string = 'objects';

  constructor(private client: ApiClient) { }

  getById(id: string): Observable<ObjectVM> {
    return this.client.get<ObjectVM>(`${this.route}/${id}`);
  }

  getAll(): Observable<ListObjectVM[]> {
    return this.client.get<ListObjectVM[]>(this.route);
  }

  create(model: ObjectVM): Observable<ObjectVM> {
    return this.client.post<ObjectVM, ObjectVM>(this.route, model);
  }

  update(model: ObjectVM): Observable<ObjectVM> {
    return this.client.put<ObjectVM, ObjectVM>(this.route, model);
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
