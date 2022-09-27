import { JsonPipe } from '@angular/common';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, retry, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {

  baseUrl:string
  httpHeader = {
    headers:new HttpHeaders({
      'Content-Type':'application/json'
    })
  }

  constructor(private httpClient : HttpClient) { 
    this.baseUrl = 'https://localhost:7124/'
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

  connect()
  {
    return this.httpClient.post(this.baseUrl+'',JSON.stringify("connect"),this.httpHeader)
    .pipe(
      retry(1),
      catchError(this.httpError)
    )
  }

  disconnect()
  {
    return this.httpClient.post(this.baseUrl+'',JSON.stringify("disconnect"),this.httpHeader)
    .pipe(
      retry(1),
      catchError(this.httpError)
    )
  }
}
