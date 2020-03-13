export interface Artista {
  id: number;
  name: string;
}

export interface ArtistaResponse {
  total: number;
  itens: Artista[];
}
