import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { AlbumResponse } from '../models/album';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AlbunsService {
  private headers = new HttpHeaders({
    'Content-type': 'application/json'
  });

  constructor(
    private http: HttpClient
  ) { }

  getAll(intervalo: string = '', pagina: number = 1) {
    return this.http.get<AlbumResponse>(`${environment.apiUrl}/albums?intervalo=${intervalo}&pg=${pagina}`,
      { headers: this.headers}
    );
  }
}
