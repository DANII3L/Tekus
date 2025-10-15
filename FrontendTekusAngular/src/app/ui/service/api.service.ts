import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseURL = 'http://localhost:5000/api/';
  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  // Toast configurado para no interrumpir
  private Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 4000,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.addEventListener('mouseenter', Swal.stopTimer);
      toast.addEventListener('mouseleave', Swal.resumeTimer);
    }
  });

  constructor(private http: HttpClient) {}

  private handleError = (error: any): Observable<never> => {
    const errorMessage = error.error?.message || error.message || 'An error occurred';
    
    this.Toast.fire({
      icon: 'error',
      title: 'Error',
      text: errorMessage
    });
    
    return throwError(() => new Error(`Error HTTP: ${error.status} ${errorMessage}`));
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
