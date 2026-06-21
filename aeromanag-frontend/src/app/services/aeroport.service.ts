import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '../core/api-config';
import { Aeroport, CreateAeroport } from '../models/aeroport.model';

@Injectable({ providedIn: 'root' })
export class AeroportService {
  private http = inject(HttpClient);
  private apiUrl = `${API_BASE_URL}/aeroports`;

  aeroports = signal<Aeroport[]>([]);

  loadAll(): void {
    this.http.get<Aeroport[]>(this.apiUrl).subscribe({
      next: (data) => this.aeroports.set(data),
      error: (err) => console.error('Erreur', err)
    });
  }

  getById(id: string): Observable<Aeroport> {
    return this.http.get<Aeroport>(`${this.apiUrl}/${id}`);
  }

  create(aeroport: CreateAeroport): void {
    this.http.post<Aeroport>(this.apiUrl, aeroport).subscribe({
      next: () => this.loadAll(),
      error: (err) => console.error('Erreur', err)
    });
  }

  update(id: string, aeroport: CreateAeroport): void {
    this.http.put(`${this.apiUrl}/${id}`, aeroport).subscribe({
      next: () => this.loadAll(),
      error: (err) => console.error('Erreur', err)
    });
  }

  delete(id: string): void {
    this.http.delete(`${this.apiUrl}/${id}`).subscribe({
      next: () => this.loadAll(),
      error: (err) => console.error('Erreur', err)
    });
  }
}
