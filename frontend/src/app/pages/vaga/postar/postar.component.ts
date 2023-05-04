import { VagaService } from './../../../services/vaga.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CadastrarVagaDTO, Vaga } from 'src/app/models/vaga';
import { NotificacaoService } from 'src/app/services/notificacao.service';
import { UsuarioService } from 'src/app/services/usuario.service';
import { BasePage } from '../../base-page';

@Component({
  selector: 'app-postar',
  templateUrl: './postar.component.html',
  styleUrls: ['./postar.component.scss'],
})
export class PostarComponent extends BasePage implements OnInit {
  vagaForm: CadastrarVagaDTO = new CadastrarVagaDTO();

  constructor(
    usuarioService: UsuarioService,
    private service: VagaService,
    private router: Router,
    private notificacaoService: NotificacaoService
  ) {
    super(usuarioService);
  }

  ngOnInit(): void {}

  cadastrar() {
    if (
      this.vagaForm.empresa &&
      this.vagaForm.cargo &&
      this.vagaForm.salario &&
      this.vagaForm.descricao &&
      this.vagaForm.local
    ) {
      let vaga = new CadastrarVagaDTO();
      vaga.empresa = this.vagaForm.empresa;
      vaga.cargo = this.vagaForm.cargo;
      vaga.salario = this.vagaForm.salario;
      vaga.descricao = this.vagaForm.descricao;
      vaga.local = this.vagaForm.local;

      this.service.cadastrar(vaga).subscribe(
        (response) => {
          this.notificacaoService.success('Vaga cadastrada com sucesso');
          this.limparForm();
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

  limparForm() {
    this.vagaForm = new CadastrarVagaDTO();
  }
}
