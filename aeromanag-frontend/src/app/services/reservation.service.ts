import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '../core/api-config';
import { Reservation, CreateReservation } from '../models/reservation.model';

@Injectable({ providedIn: 'root' })
export class ReservationService {
  private http = inject(HttpClient);
  private apiUrl = `${API_BASE_URL}/reservations`;

  reservations = signal<Reservation[]>([]);

  loadAll(): void {
    this.http.get<Reservation[]>(this.apiUrl).subscribe({
      next: (data) => this.reservations.set(data),
      error: (err) => console.error('Erreur', err)
    });
  }

  getById(id: number): Observable<Reservation> {
    return this.http.get<Reservation>(`${this.apiUrl}/${id}`);
  }

  create(reservation: CreateReservation): Observable<Reservation> {
    return this.http.post<Reservation>(this.apiUrl, reservation);
  }

  delete(id: number): void {
    this.http.delete(`${this.apiUrl}/${id}`).subscribe({
      next: () => this.loadAll(),
      error: (err) => console.error('Erreur', err)
    });
  }
}
