import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { VolService } from '../../services/vol.service';

@Component({
  selector: 'app-vol-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './vol-list.component.html'
})
export class VolListComponent implements OnInit {
  service = inject(VolService);

  ngOnInit(): void {
    this.service.loadAll();
  }

  supprimer(id: number): void {
    if (confirm('Supprimer ce vol ?')) {
      this.service.delete(id);
    }
  }
}
