import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, retry, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ManualModeService {

  baseUrl:string
  httpHeader = {
    headers:new HttpHeaders({
      'Content-Type':'application/json'
    })
  }
  constructor(private httpClient : HttpClient) {
    this.baseUrl = 'https://localhost:7124/ManualMode'
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

  setRelayState(relayState:boolean)
  {
    return this.httpClient.post(this.baseUrl+'/SetRelayState', JSON.stringify(relayState), this.httpHeader)
    .pipe(
      retry(1),
      catchError(this.httpError)
    )
  }

  setCurrentValue(current:number)
  {
    return this.httpClient.post(this.baseUrl+'/SetCurrentValue', JSON.stringify(current), this.httpHeader)
    .pipe(
      retry(1),
      catchError(this.httpError)
    )
  }
}
