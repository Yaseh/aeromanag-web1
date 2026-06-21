import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '../core/api-config';
import { Vol, CreateVol } from '../models/vol.model';

@Injectable({ providedIn: 'root' })
export class VolService {
  private http = inject(HttpClient);
  private apiUrl = `${API_BASE_URL}/vols`;

  vols = signal<Vol[]>([]);

  loadAll(): void {
    this.http.get<Vol[]>(this.apiUrl).subscribe({
      next: (data) => this.vols.set(data),
      error: (err) => console.error('Erreur', err)
    });
  }

  getById(id: number): Observable<Vol> {
    return this.http.get<Vol>(`${this.apiUrl}/${id}`);
  }

  create(vol: CreateVol): void {
    this.http.post<Vol>(this.apiUrl, vol).subscribe({
      next: () => this.loadAll(),
      error: (err) => console.error('Erreur', err)
    });
  }

  update(id: number, vol: CreateVol): void {
    this.http.put(`${this.apiUrl}/${id}`, vol).subscribe({
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
