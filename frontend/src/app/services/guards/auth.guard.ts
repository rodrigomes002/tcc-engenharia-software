import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable } from 'rxjs';
import { UsuarioService } from '../usuario.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private usuarioService: UsuarioService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    if (this.usuarioService.usuarioLogado()) {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
