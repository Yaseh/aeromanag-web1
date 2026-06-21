import { Component, OnInit, inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AeroportService } from '../../services/aeroport.service';

@Component({
  selector: 'app-aeroport-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './aeroport-form.component.html'
})
export class AeroportFormComponent implements OnInit {
  private service = inject(AeroportService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);

  modeEdition = false;
  private idCourant = '';

  form: FormGroup = this.fb.group({
    idIata: ['', Validators.required],
    nom: ['', Validators.required],
    ville: ['', Validators.required],
    pays: ['', Validators.required]
  });

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.modeEdition = true;
      this.idCourant = id;
      this.form.get('idIata')?.disable();
      this.service.getById(id).subscribe(a => this.form.patchValue(a));
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
    this.router.navigate(['/aeroports']);
  }

  annuler(): void {
    this.router.navigate(['/aeroports']);
  }
}
