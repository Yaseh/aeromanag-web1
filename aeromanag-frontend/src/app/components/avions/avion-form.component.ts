import { Component, OnInit, inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AvionService } from '../../services/avion.service';

@Component({
  selector: 'app-avion-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './avion-form.component.html'
})
export class AvionFormComponent implements OnInit {
  private service = inject(AvionService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);

  modeEdition = false;
  private idCourant = 0;

  form: FormGroup = this.fb.group({
    modele: ['', Validators.required],
    capacite: [0, [Validators.required, Validators.min(1)]],
    statut: ['En service', Validators.required]
  });

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.modeEdition = true;
      this.idCourant = Number(id);
      this.service.getById(this.idCourant).subscribe(a => this.form.patchValue(a));
    }
  }

  enregistrer(): void {
    if (this.form.invalid) return;
    const valeurs = this.form.getRawValue();
    if (this.modeEdition) {
      this.service.update(this.idCourant, valeurs);
    } else {
      this.service.create(valeurs);
    }
    this.router.navigate(['/avions']);
  }

  annuler(): void {
    this.router.navigate(['/avions']);
  }
}
