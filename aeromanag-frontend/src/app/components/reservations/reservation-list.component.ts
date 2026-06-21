import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ReservationService } from '../../services/reservation.service';

@Component({
  selector: 'app-reservation-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './reservation-list.component.html'
})
export class ReservationListComponent implements OnInit {
  service = inject(ReservationService);

  ngOnInit(): void {
    this.service.loadAll();
  }

  supprimer(id: number): void {
    if (confirm('Supprimer cette reservation ?')) {
      this.service.delete(id);
    }
  }
}
