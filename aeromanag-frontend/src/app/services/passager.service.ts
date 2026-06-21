import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '../core/api-config';
import { Passager, CreatePassager } from '../models/passager.model';

@Injectable({ providedIn: 'root' })
export class PassagerService {
  private http = inject(HttpClient);
  private apiUrl = `${API_BASE_URL}/passagers`;

  passagers = signal<Passager[]>([]);

  loadAll(): void {
    this.http.get<Passager[]>(this.apiUrl).subscribe({
      next: (data) => this.passagers.set(data),
      error: (err) => console.error('Erreur', err)
    });
  }

  getById(id: number): Observable<Passager> {
    return this.http.get<Passager>(`${this.apiUrl}/${id}`);
  }

  create(passager: CreatePassager): void {
    this.http.post<Passager>(this.apiUrl, passager).subscribe({
      next: () => this.loadAll(),
      error: (err) => console.error('Erreur', err)
    });
  }

  update(id: number, passager: CreatePassager): void {
    this.http.put(`${this.apiUrl}/${id}`, passager).subscribe({
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
