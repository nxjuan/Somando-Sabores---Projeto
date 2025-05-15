import { EventosComponent } from './pages/eventos/eventos.component';
import { CardapioComponent } from './pages/cardapio/cardapio.component';
import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { PacotesComponent } from './pages/pacotes/pacotes.component';
import { ReservasComponent } from './pages/reservas/reservas.component';
import { AdminPanelComponent } from './admin/pages/admin-panel/admin-panel.component';
import { AdmRelatoriosComponent } from './admin/pages/adm-relatorios/adm-relatorios.component';
import { AdmReservasComponent } from './admin/pages/adm-reservas/adm-reservas.component';
import { AdmPacotesComponent } from './admin/pages/adm-pacotes/adm-pacotes.component';
import { AdmEventosComponent } from './admin/pages/adm-eventos/adm-eventos.component';
import { AdmPagamentosComponent } from './admin/pages/adm-pagamentos/adm-pagamentos.component';


export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'cardapio', component: CardapioComponent },
  { path: 'eventos', component: EventosComponent },
  { path: 'pacotes', component: PacotesComponent },
  { path: 'reservas', component: ReservasComponent },
  { path: 'admin-panel', component: AdminPanelComponent},
  { path: 'admin-panel/relatorios', component: AdmRelatoriosComponent},
  { path: 'admin-panel/reservas', component: AdmReservasComponent},
  { path: 'admin-panel/pacotes', component: AdmPacotesComponent},
  { path: 'admin-panel/eventos', component: AdmEventosComponent},
  { path: 'admin-panel/pagamentos', component: AdmPagamentosComponent},

  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', component: HomeComponent }
];
