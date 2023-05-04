import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario';
import { UsuarioService } from 'src/app/services/usuario.service';
import { MessageService } from 'primeng/api';
import { NotificacaoService } from 'src/app/services/notificacao.service';
import { UsuarioToken } from 'src/app/models/usuarioToken';
import { BasePage } from 'src/app/pages/base-page';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.page.html',
  styleUrls: ['./cadastro.page.scss'],
  providers: [MessageService],
})
export class CadastroPage extends BasePage implements OnInit {
  usuarioform: Usuario = {
    tipo: '',
    email: '',
    senha: '',
  };

  errorMessage: string;

  constructor(
    usuarioService: UsuarioService,
    private service: UsuarioService,
    private router: Router,
    private notificacaoService: NotificacaoService
  ) {
    super(usuarioService);
  }

  ngOnInit(): void {}
  cadastrar() {
    if (
      this.usuarioform.email &&
      this.usuarioform.senha &&
      this.usuarioform.tipo
    ) {
      let usuario = new Usuario();
      usuario.email = this.usuarioform.email;
      usuario.senha = this.usuarioform.senha;
      usuario.tipo = this.usuarioform.tipo;

      this.notificacaoService.loading();
      this.service.cadastrar(usuario).subscribe(
        (response) => {
          this.notificacaoService.success('Usuário cadastrado com sucesso');

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
          errors.forEach((error: any) => {
            let mensagem = JSON.stringify(error.description);
            this.notificacaoService.error(mensagem);
          });
        }
      );
    }
  }
}
