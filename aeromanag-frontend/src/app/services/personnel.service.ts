import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '../core/api-config';
import { Personnel, CreatePersonnel } from '../models/personnel.model';

@Injectable({ providedIn: 'root' })
export class PersonnelService {
  private http = inject(HttpClient);
  private apiUrl = `${API_BASE_URL}/personnels`;

  personnels = signal<Personnel[]>([]);

  loadAll(): void {
    this.http.get<Personnel[]>(this.apiUrl).subscribe({
      next: (data) => this.personnels.set(data),
      error: (err) => console.error('Erreur', err)
    });
  }

  getById(id: number): Observable<Personnel> {
    return this.http.get<Personnel>(`${this.apiUrl}/${id}`);
  }

  create(personnel: CreatePersonnel): void {
    this.http.post<Personnel>(this.apiUrl, personnel).subscribe({
      next: () => this.loadAll(),
      error: (err) => console.error('Erreur', err)
    });
  }

  update(id: number, personnel: CreatePersonnel): void {
    this.http.put(`${this.apiUrl}/${id}`, personnel).subscribe({
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
