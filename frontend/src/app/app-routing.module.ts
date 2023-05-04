import { PostarComponent } from './pages/vaga/postar/postar.component';
import { AuthGuard } from './services/guards/auth.guard';
import { CadastroPage } from './pages/cadastro/cadastro.page';
import { LoginPage } from './pages/login/login.page';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VagaPage } from './pages/vaga/vaga.page';

const routes: Routes = [
  {
    path: '',
    component: LoginPage,
  },
  {
    path: 'login',
    component: LoginPage,
  },
  {
    path: 'cadastro',
    component: CadastroPage,
  },
  {
    path: 'vagas',
    component: VagaPage,
    canActivate: [AuthGuard],
  },
  {
    path: 'vagas/postar',
    component: PostarComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
