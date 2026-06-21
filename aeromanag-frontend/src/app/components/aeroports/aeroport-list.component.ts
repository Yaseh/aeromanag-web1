import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AeroportService } from '../../services/aeroport.service';

@Component({
  selector: 'app-aeroport-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './aeroport-list.component.html'
})
export class AeroportListComponent implements OnInit {
  service = inject(AeroportService);

  ngOnInit(): void {
    this.service.loadAll();
  }

  supprimer(id: string): void {
    if (confirm('Supprimer cet aeroport ?')) {
      this.service.delete(id);
    }
  }
}
