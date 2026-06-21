import { Routes } from '@angular/router';
import { AeroportListComponent } from './components/aeroports/aeroport-list.component';
import { AeroportFormComponent } from './components/aeroports/aeroport-form.component';
import { AvionListComponent } from './components/avions/avion-list.component';
import { AvionFormComponent } from './components/avions/avion-form.component';
import { PersonnelListComponent } from './components/personnels/personnel-list.component';
import { PersonnelFormComponent } from './components/personnels/personnel-form.component';
import { PassagerListComponent } from './components/passagers/passager-list.component';
import { PassagerFormComponent } from './components/passagers/passager-form.component';
import { VolListComponent } from './components/vols/vol-list.component';
import { VolFormComponent } from './components/vols/vol-form.component';
import { ReservationListComponent } from './components/reservations/reservation-list.component';
import { ReservationFormComponent } from './components/reservations/reservation-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'aeroports', pathMatch: 'full' },
  { path: 'aeroports', component: AeroportListComponent },
  { path: 'aeroports/nouveau', component: AeroportFormComponent },
  { path: 'aeroports/:id/modifier', component: AeroportFormComponent },
  { path: 'avions', component: AvionListComponent },
  { path: 'avions/nouveau', component: AvionFormComponent },
  { path: 'avions/:id/modifier', component: AvionFormComponent },
  { path: 'personnels', component: PersonnelListComponent },
  { path: 'personnels/nouveau', component: PersonnelFormComponent },
  { path: 'personnels/:id/modifier', component: PersonnelFormComponent },
  { path: 'passagers', component: PassagerListComponent },
  { path: 'passagers/nouveau', component: PassagerFormComponent },
  { path: 'passagers/:id/modifier', component: PassagerFormComponent },
  { path: 'vols', component: VolListComponent },
  { path: 'vols/nouveau', component: VolFormComponent },
  { path: 'vols/:id/modifier', component: VolFormComponent },
  { path: 'reservations', component: ReservationListComponent },
  { path: 'reservations/nouveau', component: ReservationFormComponent }
];
