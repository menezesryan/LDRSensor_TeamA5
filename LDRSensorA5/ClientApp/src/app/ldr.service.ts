import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { LDRData } from './models/LDRData';
import { LightThreshold } from './models/LightThreshold';

@Injectable({
  providedIn: 'root'
})
export class LDRService {
  baseUrl: string
  httpHeader = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private httpClient: HttpClient) {
    this.baseUrl = 'https://localhost:7124/Ldr'
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

  setThresholdValues(threshold: LightThreshold) {
    return this.httpClient.post(this.baseUrl + '/SetThreshold', JSON.stringify(threshold), this.httpHeader)
      .pipe(
        retry(1),
        catchError(this.httpError)
      )
  }

  resetThresholdValues(threshold: LightThreshold) {
    return this.httpClient.post(this.baseUrl + '/ResetThreshold', JSON.stringify(threshold), this.httpHeader)
      .pipe(
        retry(1),
        catchError(this.httpError)
      )
  }

  getLDRData(): Observable<LDRData> {
    return this.httpClient.get<LDRData>(this.baseUrl + '/GetLDRData', this.httpHeader)
      .pipe(
        retry(1),
        catchError(this.httpError)
      )
  }

  getThresholdData(): Observable<LightThreshold> {
    return this.httpClient.get<LightThreshold>(this.baseUrl + '/GetThreshold', this.httpHeader)
      .pipe(
        retry(1),
        catchError(this.httpError)
      )
  }

  getDefaultThresholdData(): Observable<LightThreshold> {
    return this.httpClient.get<LightThreshold>(this.baseUrl + '/GetDefaultThreshold', this.httpHeader)
      .pipe(
        retry(1),
        catchError(this.httpError)
      )
  }

  saveThresholdData(threshold: LightThreshold) {
    return this.httpClient.post(this.baseUrl + '/SaveThreshold', JSON.stringify(threshold), this.httpHeader)
      .pipe(
        retry(1),
        catchError(this.httpError)
      )
  }

  getDatabaseData(): Observable<LDRData[]> {
    return this.httpClient.get<LDRData[]>(this.baseUrl + '/GetDatabaseData', this.httpHeader)
      .pipe(
        retry(1),
        catchError(this.httpError)
      )
  }

}
