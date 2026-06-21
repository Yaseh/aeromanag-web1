import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { PassagerService } from '../../services/passager.service';

@Component({
  selector: 'app-passager-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './passager-list.component.html'
})
export class PassagerListComponent implements OnInit {
  service = inject(PassagerService);

  ngOnInit(): void {
    this.service.loadAll();
  }

  supprimer(id: number): void {
    if (confirm('Supprimer ce passager ?')) {
      this.service.delete(id);
    }
  }
}
