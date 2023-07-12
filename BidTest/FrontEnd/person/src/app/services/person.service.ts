import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Person } from '../interfaces/person';
import { Environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private personGetAllPeopleUri = 'GetAllPeople';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(
    private http: HttpClient
  ) { }

  getPeople(): Observable<Person[]> {
    return this.http.get<Person[]>( Environment.appUrl + this.personGetAllPeopleUri )
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  savePerson(person: Person): Observable<any> {
    console.log("savePerson" + JSON.stringify(person));
    return this.http.post<any>(Environment.appUrl + "Person", JSON.stringify(person), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  errorHandler(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    }
    else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
