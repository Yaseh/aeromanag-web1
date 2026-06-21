export interface Personnel {
  idPersonnel: number;
  nom: string;
  prenom: string;
  role: string;
}

export interface CreatePersonnel {
  nom: string;
  prenom: string;
  role: string;
}
