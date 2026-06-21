import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { PersonnelService } from '../../services/personnel.service';

@Component({
  selector: 'app-personnel-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './personnel-list.component.html'
})
export class PersonnelListComponent implements OnInit {
  service = inject(PersonnelService);

  ngOnInit(): void {
    this.service.loadAll();
  }

  supprimer(id: number): void {
    if (confirm('Supprimer ce personnel ?')) {
      this.service.delete(id);
    }
  }
}
