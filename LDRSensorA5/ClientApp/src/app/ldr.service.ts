import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { LDRData } from './models/LDRData';
import { LightThreshold } from './models/LightThreshold';

@Injectable({
  providedIn: 'root'
})
export class LDRService {
  baseUrl:string
  httpHeader = {
    headers:new HttpHeaders({
      'Content-Type':'application/json'
    })
  }
  constructor(private httpClient :HttpClient) { 
    this.baseUrl = 'https://localhost:7124/Ldr'
  }

  httpError(error:HttpErrorResponse){
    let msg='';
    if(error.error instanceof ErrorEvent){
      msg=error.error.message;
    }
    else{
      msg=`Error Code:${error.status}\nMessafe:${error.message}`;
    }
    console.log(msg);
    return throwError(msg);
  }

  setThresholdValues(threshold : LightThreshold)
  {
     return this.httpClient.post(this.baseUrl + '/SetThreshold',JSON.stringify(threshold), this.httpHeader)
     .pipe(
      retry(1),
      catchError(this.httpError)
     )
  }

 

  resetThresholdValues()
  {
    return this.httpClient.post(this.baseUrl+'/ResetThreshold',JSON.stringify("reset"), this.httpHeader)
    .pipe(
      retry(1),
      catchError(this.httpError)
    )
  }

  getLDRData() : Observable<LDRData>
  {
    return this.httpClient.get<LDRData>(this.baseUrl + '/GetLDRData', this.httpHeader)
    .pipe(
      retry(1),
      catchError(this.httpError)
    )
  }

}
