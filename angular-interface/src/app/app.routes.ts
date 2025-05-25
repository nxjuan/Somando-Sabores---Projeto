import { EventosComponent } from './pages/eventos/eventos.component';
import { CardapioComponent } from './pages/cardapio/cardapio.component';
import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { PacotesComponent } from './pages/pacotes/pacotes.component';
import { ReservasComponent } from './pages/reservas/reservas.component';
import { ConfirmacaoComponent } from './pages/confirmacao/confirmacao.component';
import { ResumoPacoteComponent } from './pages/resumo-pacote/resumo-pacote.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'cardapio', component: CardapioComponent },
  { path: 'eventos', component: EventosComponent },
  { path: 'pacotes', component: PacotesComponent },
  { path: 'reservas', component: ReservasComponent },
  { path: 'confirmacao', component: ConfirmacaoComponent },
  { path: 'resumo-pacote', component: ResumoPacoteComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', component: HomeComponent }
];
