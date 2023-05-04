import { UsuarioService } from 'src/app/services/usuario.service';
import { Usuario } from '../models/usuario';
export class BasePage {
  usuario!: Usuario;
  constructor(protected usuarioService: UsuarioService) {
    this.usuarioService.currentUser.subscribe((usuario) => {
      this.usuario = usuario;
    });
  }

  isPerfil(perfil: string) {
    return this.usuario.tipo == perfil;
  }

  usuarioLogado() {
    return this.usuario.email.split('@')[0];
  }

  isLogado() {
    return this.usuarioService.usuarioLogado();
  }
}
