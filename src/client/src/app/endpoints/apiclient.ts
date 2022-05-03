import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ToastController } from "@ionic/angular";
import { EMPTY, Observable, throwError } from 'rxjs';
import { retry, catchError, finalize } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiClient {

  constructor(
    private http: HttpClient,
    private toastCtrl: ToastController) { }

  // Http Options
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  delete<T>(url: string, model: T): Observable<T> {
    let failed = false
    return this.http
      .delete<T>(environment.apiUrl + '/' + url, model)
      .pipe(
        catchError((error) => {
          failed = true;
          this.displayError(error);
          return EMPTY;
        }),
        finalize(() => {
          if (!failed)
            this.displaySuccess('Item deleted!');
        })
      );
  }

  put<TRequest, TReponse>(url: string, model: TRequest): Observable<TReponse> {
    let failed = false
    return this.http
      .put<TReponse>(environment.apiUrl + '/' + url, model)
      .pipe(
        catchError((error) => {
          failed = true;
          this.displayError(error);
          return EMPTY;
        }),
        finalize(() => {
          if (!failed)
            this.displaySuccess('Changes saved!');
        })
      );
  }

  putByUrl(url: string): Observable<any> {
    let failed = false
    return this.http
      .put(environment.apiUrl + '/' + url, {})
      .pipe(
        catchError((error) => {
          failed = true;
          this.displayError(error);
          return EMPTY;
        }),
        finalize(() => {
          if (!failed)
            this.displaySuccess('Changes saved!');
        })
      );
  }

  post<TRequest, TReponse>(url: string, model: TRequest): Observable<TReponse> {
    let failed = false
    return this.http
      .post<TReponse>(environment.apiUrl + '/' + url, model)
      .pipe(
        catchError((error) => {
          failed = true;
          this.displayError(error);
          return EMPTY;
        }),
        finalize(() => {
          if (!failed)
            this.displaySuccess('Item created!');
        })
      );
  }

  get<T>(url: string): Observable<T> {
    return this.http
      .get<T>(environment.apiUrl + '/' + url)
      .pipe(
        retry(1),
        catchError((error) => {
          this.displayError(error);
          return EMPTY;
        })
      );
  }

  async displaySuccess(notification: string) {
    const toast = await this.toastCtrl.create({
      message: notification,
      icon: 'checkmark-done-outline',
      position: 'top',
      color: 'success',
      duration: 2000
    });
    toast.present();
  }

  async displayError(error: any) {
    const toast = await this.toastCtrl.create({
      message: 'Error occured!',
      icon: 'checkmark-done-outline',
      position: 'top',
      color: 'danger',
      duration: 2000
    });
    toast.present();
  }
}