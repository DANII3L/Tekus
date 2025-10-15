import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseURL = 'http://localhost:5000/api/';
  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) {}

  private handleError(error: any): Observable<never> {
    console.error(`Error HTTP: ${error.status}`, error);
    return throwError(() => new Error(`Error HTTP: ${error.status}`));
  }

  get<T>(endpoint: string): Observable<any> {
    return this.http.get<any>(`${this.baseURL}${endpoint}`, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }

  post<T>(endpoint: string, data: any): Observable<any> {
    return this.http.post<any>(`${this.baseURL}${endpoint}`, data, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }

  put<T>(endpoint: string, data: any): Observable<any> {
    return this.http.put<any>(`${this.baseURL}${endpoint}`, data, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }

  delete<T>(endpoint: string): Observable<any> {
    return this.http.delete<any>(`${this.baseURL}${endpoint}`, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }
}
