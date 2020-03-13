import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Album } from 'src/app/models/album';
import { AlbunsService } from 'src/app/services/albuns.service';

@Component({
  selector: 'app-admin-albuns',
  templateUrl: './admin-albuns.component.html',
  styleUrls: ['./admin-albuns.component.scss']
})
export class AdminAlbunsComponent implements OnInit {
  formulario: FormGroup;
  albuns: Album[];
  total: number;


  constructor(
    private formBuilder: FormBuilder,
    private albumSrv: AlbunsService
  ) { }

  ngOnInit(): void {
    this.formulario = this.formBuilder.group({
      Buscar: ['']
    });

    this.loadAlbums();
  }

  private loadAlbums(
    Intervalo: string = '',
    Pagina: number = 1
  )
  {
    this.albumSrv.getAll(Intervalo, Pagina)
      .subscribe(response => {
        this.total = response.total;
        this.albuns = response.itens;
      });
  }

  filtrar(event: any = null) {
    if (this.formulario.invalid) {
      return;
    }
    let pagina = 1;
    if (event) {
      pagina = event.page;
    }
    this.loadAlbums(
      this.formulario.value.Buscar,
      pagina
    )
  }
}
