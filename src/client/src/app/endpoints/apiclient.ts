import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiClient {

  constructor(private http: HttpClient) { }

  // Http Options
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  put<TRequest, TReponse>(url: string, model: TRequest): Observable<TReponse> {
    return this.http
      .put<TReponse>(environment.apiUrl + '/' + url, model)
      .pipe(catchError(this.handleError))
  }

  post<TRequest, TReponse>(url: string, model: TRequest): Observable<TReponse> {
    return this.http
      .post<TReponse>(environment.apiUrl + '/' + url, model)
      .pipe(catchError(this.handleError))
  }

  get<TResult>(url: string) {
    return this.http
      .get<TResult>(environment.apiUrl + '/' + url)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  handleError(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }
}