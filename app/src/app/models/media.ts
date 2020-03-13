export interface Media {
  id: number;
  description: string;
}

export interface MediaResponse {
  total: number;
  itens: Media[];
}
