import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AvionService } from '../../services/avion.service';

@Component({
  selector: 'app-avion-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './avion-list.component.html'
})
export class AvionListComponent implements OnInit {
  service = inject(AvionService);

  ngOnInit(): void {
    this.service.loadAll();
  }

  supprimer(id: number): void {
    if (confirm('Supprimer cet avion ?')) {
      this.service.delete(id);
    }
  }
}
