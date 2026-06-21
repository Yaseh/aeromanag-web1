export interface Reservation {
  idReservation: number;
  numeroSiege: string;
  idVol: number;
  numeroVol: string;
  idPassager: number;
  passagerNom: string;
  passagerPrenom: string;
}

export interface CreateReservation {
  numeroSiege: string;
  idVol: number;
  idPassager: number;
}
