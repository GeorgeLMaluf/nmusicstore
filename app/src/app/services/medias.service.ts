import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { MediaResponse } from 'src/app/models/media';
@Injectable({
  providedIn: 'root'
})
export class MediasService {
  private headers = new HttpHeaders({
    'Content-type': 'application/json'
  });

  constructor(
    private http: HttpClient
  ) { }

  getAll(intervalo: string = '', pagina: number = 1) {
    return this.http.get<MediaResponse>(`${environment.apiUrl}/tipo_media?intervalo=${intervalo}&pg=${pagina}`,
      {headers: this.headers})
  }
}
