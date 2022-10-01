import { JsonPipe } from '@angular/common';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { ConnectionParameters } from './models/ConnectionParameters';

@Injectable({
  providedIn: 'root'
})
export class CommunicationService {

  baseUrl: string
  httpHeader = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private httpClient: HttpClient) {
    this.baseUrl = 'https://localhost:7124/Communication/'
  }

  httpError(error: HttpErrorResponse) {
    let msg = '';
    if (error.error instanceof ErrorEvent) {
      msg = error.error.message;
    }
    else {
      msg = `Error Code:${error.status}\nMessafe:${error.message}`;
    }
    console.log(msg);
    return throwError(msg);
  }

  connect(parameters: ConnectionParameters) {
    return this.httpClient.post(this.baseUrl + 'Connect', JSON.stringify(parameters), this.httpHeader)
      .pipe(
        retry(1),
        catchError(this.httpError)
      )
  }

  disconnect(parameters: ConnectionParameters) {
    return this.httpClient.post(this.baseUrl + 'Disconnect', JSON.stringify(parameters), this.httpHeader)
      .pipe(
        retry(1),
        catchError(this.httpError)
      )
  }

  isConnected(): Observable<boolean> {
    return this.httpClient.get<boolean>(this.baseUrl + '/IsConnected', this.httpHeader)
      .pipe(
        retry(1),
        catchError(this.httpError)
      )
  }
}
