export interface Avion {
  idAvion: number;
  modele: string;
  capacite: number;
  statut: string;
}

export interface CreateAvion {
  modele: string;
  capacite: number;
  statut: string;
}
