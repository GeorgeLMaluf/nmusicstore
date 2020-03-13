import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ArtistaResponse } from 'src/app/models/artista'
@Injectable({
  providedIn: 'root'
})
export class ArtistasService {
  private headers = new HttpHeaders({
    'Content-type': 'application/json'
  });

  constructor(
    private http: HttpClient
  ) { }

  getAll(intervalo: string = '', pagina: number = 1) {
    return this.http.get<ArtistaResponse>(`${environment.apiUrl}/artists?intervalo=${intervalo}&pg=${pagina}`,
      { headers: this.headers}
    );
  }
}
