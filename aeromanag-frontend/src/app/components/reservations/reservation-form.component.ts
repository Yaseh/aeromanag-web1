import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { ReservationService } from '../../services/reservation.service';
import { VolService } from '../../services/vol.service';
import { PassagerService } from '../../services/passager.service';

@Component({
  selector: 'app-reservation-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './reservation-form.component.html'
})
export class ReservationFormComponent implements OnInit {
  private service = inject(ReservationService);
  volService = inject(VolService);
  passagerService = inject(PassagerService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  messageErreur = '';

  form: FormGroup = this.fb.group({
    numeroSiege: ['', Validators.required],
    idVol: [null, Validators.required],
    idPassager: [null, Validators.required]
  });

  ngOnInit(): void {
    this.volService.loadAll();
    this.passagerService.loadAll();
  }

  enregistrer(): void {
    if (this.form.invalid) return;
    this.messageErreur = '';
    const valeurs = this.form.getRawValue();
    valeurs.idVol = Number(valeurs.idVol);
    valeurs.idPassager = Number(valeurs.idPassager);
    this.service.create(valeurs).subscribe({
      next: () => {
        this.service.loadAll();
        this.router.navigate(['/reservations']);
      },
      error: (err: HttpErrorResponse) => {
        if (err.status === 409) {
          this.messageErreur = err.error.message;
        } else {
          this.messageErreur = 'Une erreur est survenue.';
        }
      }
    });
  }

  annuler(): void {
    this.router.navigate(['/reservations']);
  }
}
