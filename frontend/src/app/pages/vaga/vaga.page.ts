import { Candidatura } from './../../models/candidatura';
import { NotificacaoService } from 'src/app/services/notificacao.service';
import { VagaFilter } from './../../models/filter/vagaFilter';
import { VagaService } from './../../services/vaga.service';
import { BasePage } from '../base-page';
import { UsuarioService } from 'src/app/services/usuario.service';
import { Component, OnInit } from '@angular/core';
import { LocalFilter } from 'src/app/models/filter/localFilter';
import { EspecialidadeFilter } from 'src/app/models/filter/especialidadeFilter';
import { Vaga, VagaResult } from 'src/app/models/vaga';

@Component({
  selector: 'app-vaga',
  templateUrl: './vaga.page.html',
  styleUrls: ['./vaga.page.scss'],
})
export class VagaPage extends BasePage implements OnInit {
  cargos: EspecialidadeFilter[] = [];
  cargoSelecionada: EspecialidadeFilter[] = [];
  vagas: Vaga[] = [];
  locais: LocalFilter[] = [];
  localSelecionado: LocalFilter[] = [];
  vagaFilter: VagaFilter = new VagaFilter();
  count: number;
  constructor(
    usuarioService: UsuarioService,
    private service: VagaService,
    private notificacaoService: NotificacaoService
  ) {
    super(usuarioService);
  }

  ngOnInit(): void {
    this.listarFiltros();
    this.listar();
  }

  listar() {
    this.vagaFilter.locais = this.localSelecionado.map((l) => l.local);
    this.vagaFilter.especialidades = this.cargoSelecionada.map((e) => e.cargo);
    this.notificacaoService.loading();
    this.service.listarPorFiltro(this.vagaFilter).subscribe((response) => {
      let result = response as VagaResult;
      this.vagas = result.vagas;
      this.count = result.count;
      this.notificacaoService.loaded().subscribe(() => {});
    });
  }

  listarFiltros() {
    this.notificacaoService.loading();
    this.service.listarFiltros().subscribe((response) => {
      response.locais.forEach((local) => {
        this.locais.push(new LocalFilter(local));
      });

      response.cargos.forEach((cargo) => {
        this.cargos.push(new EspecialidadeFilter(cargo));
      });
      this.notificacaoService.loaded().subscribe(() => {});
    });
  }
  candidatarSe(vaga: any) {
    let candadatura = new Candidatura();
    candadatura.id = vaga.id;
    candadatura.candidato = this.usuario.email;

    this.notificacaoService.loading();
    this.service.candidatarSe(candadatura).subscribe(
      (response) => {
        this.notificacaoService.loaded().subscribe(() => {});
      },
      (error) => {
        this.notificacaoService.loaded().subscribe(() => {});
      }
    );
  }

  mudarPagina(event: any) {
    this.vagaFilter.page = event.page + 1;
    this.listar();
  }
}
