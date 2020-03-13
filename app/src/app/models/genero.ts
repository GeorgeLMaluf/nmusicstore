export interface Genero {
  id: number;
  description: string;
}

export interface GeneroResponse{
  total: number;
  itens: Genero[];
}
