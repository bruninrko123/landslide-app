import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LandslideService {
  private apiUrl = 'http://localhost:5156/api/LandSlide/check';

  constructor(private http: HttpClient) {}

  getHotSpot(cityName: string): Observable<any> {
    return this.http.get(`${this.apiUrl}?cityName=${cityName}`);
  }   
}
