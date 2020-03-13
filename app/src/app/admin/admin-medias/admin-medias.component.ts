import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Media } from 'src/app/models/media';
import { MediasService } from 'src/app/services/medias.service';


@Component({
  selector: 'app-admin-medias',
  templateUrl: './admin-medias.component.html',
  styleUrls: ['./admin-medias.component.scss']
})
export class AdminMediasComponent implements OnInit {
  formulario: FormGroup;
  medias: Media[];
  total: number;

  constructor(
    private formBuilder: FormBuilder,
    private mediaSrv: MediasService
  ) { }

  ngOnInit(): void {
    this.formulario = this.formBuilder.group({
      Buscar: ['']
    });
    this.loadMedias();
  }

  private loadMedias(
    Intervalo: string = '',
    Pagina: number = 1
  )
  {
    this.mediaSrv.getAll(Intervalo, Pagina)
      .subscribe(response => {
        this.total = response.total;
        this.medias = response.itens;
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
    this.loadMedias(
      this.formulario.value.Buscar,
      pagina
    )
  }
}
