import { Component, OnInit, inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { VolService } from '../../services/vol.service';
import { AeroportService } from '../../services/aeroport.service';
import { AvionService } from '../../services/avion.service';
import { PersonnelService } from '../../services/personnel.service';

@Component({
  selector: 'app-vol-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './vol-form.component.html'
})
export class VolFormComponent implements OnInit {
  private volService = inject(VolService);
  aeroportService = inject(AeroportService);
  avionService = inject(AvionService);
  personnelService = inject(PersonnelService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);

  modeEdition = false;
  private idCourant = 0;

  form: FormGroup = this.fb.group({
    numeroVol: ['', Validators.required],
    dateDepart: ['', Validators.required],
    dateArrivee: ['', Validators.required],
    statut: ['Prevu', Validators.required],
    aeroportDepart: ['', Validators.required],
    aeroportArrivee: ['', Validators.required],
    idAvion: [null, Validators.required],
    idCommandant: [null, Validators.required]
  });

  ngOnInit(): void {
    this.aeroportService.loadAll();
    this.avionService.loadAll();
    this.personnelService.loadAll();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.modeEdition = true;
      this.idCourant = Number(id);
      this.volService.getById(this.idCourant).subscribe(v => {
        this.form.patchValue({
          numeroVol: v.numeroVol,
          dateDepart: v.dateDepart,
          dateArrivee: v.dateArrivee,
          statut: v.statut,
          aeroportDepart: v.aeroportDepartCode,
          aeroportArrivee: v.aeroportArriveeCode,
          idAvion: v.avionId,
          idCommandant: v.commandantId
        });
      });
    }
  }

  enregistrer(): void {
    if (this.form.invalid) return;
    const valeurs = this.form.getRawValue();
    valeurs.idAvion = Number(valeurs.idAvion);
    valeurs.idCommandant = Number(valeurs.idCommandant);
    if (this.modeEdition) {
      this.volService.update(this.idCourant, valeurs);
    } else {
      this.volService.create(valeurs);
    }
    this.router.navigate(['/vols']);
  }

  annuler(): void {
    this.router.navigate(['/vols']);
  }
}
