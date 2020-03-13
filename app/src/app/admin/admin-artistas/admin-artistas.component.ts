import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Artista } from 'src/app/models/artista';
import { ArtistasService } from 'src/app/services/artistas.service';

@Component({
  selector: 'app-admin-artistas',
  templateUrl: './admin-artistas.component.html',
  styleUrls: ['./admin-artistas.component.scss']
})
export class AdminArtistasComponent implements OnInit {
  formulario: FormGroup;
  artistas: Artista[];
  total: number;

  constructor(
    private formBuilder: FormBuilder,
    private artistSrv: ArtistasService
  ) { }

  ngOnInit(): void {
    this.formulario = this.formBuilder.group({
      Buscar: ['']
    });
    this.loadArtistas();
  }

  private loadArtistas(
    Intervalo: string = '',
    Pagina: number = 1
  )
  {
    this.artistSrv.getAll(Intervalo, Pagina)
      .subscribe(response => {
        this.total = response.total;
        this.artistas = response.itens;
      })
  }

  filtrar(event: any = null) {
    if (this.formulario.invalid) {
      return;
    }
    let pagina = 1;
    if (event) {
      pagina = event.page;
    }
    this.loadArtistas(
      this.formulario.value.Buscar,
      pagina
    )
  }
}
