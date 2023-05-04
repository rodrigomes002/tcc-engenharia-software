import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Login } from 'src/app/models/login';
import { UsuarioToken } from 'src/app/models/usuarioToken';
import { UsuarioService } from 'src/app/services/usuario.service';
import { NotificacaoService } from 'src/app/services/notificacao.service';
import { BasePage } from 'src/app/pages/base-page';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage extends BasePage implements OnInit {
  usuarioform: Login = {
    email: '',
    senha: '',
  };

  constructor(
    usuarioService: UsuarioService,
    private service: UsuarioService,
    private notificacaoService: NotificacaoService,
    private router: Router
  ) {
    super(usuarioService);
  }

  ngOnInit(): void {}
  login() {
    if (this.usuarioform.email && this.usuarioform.senha) {
      let usuario = new Login();
      usuario.email = this.usuarioform.email;
      usuario.senha = this.usuarioform.senha;

      this.notificacaoService.loading();
      this.service.login(usuario).subscribe(
        (response) => {
          let usuario = response as UsuarioToken;

          if (usuario.authenticated) {
            this.service.setToken(usuario.token);

            this.notificacaoService.loaded().subscribe(() => {
              this.router.navigate(['vagas']).then(() => {
                window.location.reload();
              });
            });
          }
        },
        (errors) => {
          this.notificacaoService.error('Usuário ou senha inválidos');
        }
      );
    }
  }
}
