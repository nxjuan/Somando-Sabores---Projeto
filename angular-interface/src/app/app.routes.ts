import { EventosComponent } from './pages/eventos/eventos.component';
import { CardapioComponent } from './pages/cardapio/cardapio.component';
import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { PacotesComponent } from './pages/pacotes/pacotes.component';
import { ReservasComponent } from './pages/reservas/reservas.component';


export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'cardapio', component: CardapioComponent },
  { path: 'eventos', component: EventosComponent },
  { path: 'pacotes', component: PacotesComponent },
  { path: 'reservas', component: ReservasComponent },

  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', component: HomeComponent }
];
