import { Genero } from './genero';
import { Artista } from './artista';
import { Media } from './media';

export interface Album {
  id: number;
  title: string;
  gender_id: number;
  artist_id: number,
  tipo_media_id: number,
  cover: string;
  price: number;
  gender: Genero;
  artist: Artista;
  tipo_media: Media;
}

export interface AlbumResponse {
  total: number;
  itens: Album[];
}
