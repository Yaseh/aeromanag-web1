export interface Vol {
  idVol: number;
  numeroVol: string;
  dateDepart: string;
  dateArrivee: string;
  statut: string;
  aeroportDepartCode: string;
  aeroportDepartNom: string;
  aeroportArriveeCode: string;
  aeroportArriveeNom: string;
  avionId: number;
  avionModele: string;
  commandantId: number;
  commandantNom: string;
  commandantPrenom: string;
}

export interface CreateVol {
  numeroVol: string;
  dateDepart: string;
  dateArrivee: string;
  statut: string;
  aeroportDepart: string;
  aeroportArrivee: string;
  idAvion: number;
  idCommandant: number;
}
