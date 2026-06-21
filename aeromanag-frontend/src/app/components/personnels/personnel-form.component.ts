import { Component, OnInit, inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { PersonnelService } from '../../services/personnel.service';

@Component({
  selector: 'app-personnel-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './personnel-form.component.html'
})
export class PersonnelFormComponent implements OnInit {
  private service = inject(PersonnelService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);

  modeEdition = false;
  private idCourant = 0;

  form: FormGroup = this.fb.group({
    nom: ['', Validators.required],
    prenom: ['', Validators.required],
    role: ['', Validators.required]
  });

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.modeEdition = true;
      this.idCourant = Number(id);
      this.service.getById(this.idCourant).subscribe(p => this.form.patchValue(p));
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
    this.router.navigate(['/personnels']);
  }

  annuler(): void {
    this.router.navigate(['/personnels']);
  }
}
