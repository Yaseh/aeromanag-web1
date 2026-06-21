import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '../core/api-config';
import { Avion, CreateAvion } from '../models/avion.model';

@Injectable({ providedIn: 'root' })
export class AvionService {
  private http = inject(HttpClient);
  private apiUrl = `${API_BASE_URL}/avions`;

  avions = signal<Avion[]>([]);

  loadAll(): void {
    this.http.get<Avion[]>(this.apiUrl).subscribe({
      next: (data) => this.avions.set(data),
      error: (err) => console.error('Erreur', err)
    });
  }

  getById(id: number): Observable<Avion> {
    return this.http.get<Avion>(`${this.apiUrl}/${id}`);
  }

  create(avion: CreateAvion): void {
    this.http.post<Avion>(this.apiUrl, avion).subscribe({
      next: () => this.loadAll(),
      error: (err) => console.error('Erreur', err)
    });
  }

  update(id: number, avion: CreateAvion): void {
    this.http.put(`${this.apiUrl}/${id}`, avion).subscribe({
      next: () => this.loadAll(),
      error: (err) => console.error('Erreur', err)
    });
  }

  delete(id: number): void {
    this.http.delete(`${this.apiUrl}/${id}`).subscribe({
      next: () => this.loadAll(),
      error: (err) => console.error('Erreur', err)
    });
  }
}
