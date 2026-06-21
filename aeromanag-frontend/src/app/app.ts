import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <nav class="navbar">
      <a routerLink="/aeroports" routerLinkActive="actif">Aeroports</a>
      <a routerLink="/avions" routerLinkActive="actif">Avions</a>
      <a routerLink="/personnels" routerLinkActive="actif">Personnels</a>
      <a routerLink="/passagers" routerLinkActive="actif">Passagers</a>
      <a routerLink="/vols" routerLinkActive="actif">Vols</a>
      <a routerLink="/reservations" routerLinkActive="actif">Reservations</a>
    </nav>
    <main class="contenu">
      <router-outlet></router-outlet>
    </main>
  `
})
export class App {}
