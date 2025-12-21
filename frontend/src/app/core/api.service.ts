import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private apiUrl = 'https://localhost:5001/api/Home';  // Change if needed

  constructor(private http: HttpClient) {}

  getTest(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Test`);
  }
}
