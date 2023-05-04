import { MessageService } from 'primeng/api';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ToastModule } from 'primeng/toast';
import { LoginPage } from './pages/login/login.page';
import { CadastroPage } from './pages/cadastro/cadastro.page';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { VagaPage } from './pages/vaga/vaga.page';
import { AuthGuard } from './services/guards/auth.guard';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { MultiSelectModule } from 'primeng/multiselect';
import { PostarComponent } from './pages/vaga/postar/postar.component';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { PaginatorModule } from 'primeng/paginator';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [
    AppComponent,
    LoginPage,
    CadastroPage,
    VagaPage,
    PostarComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ButtonModule,
    InputTextModule,
    PasswordModule,
    RadioButtonModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    ToastModule,
    MultiSelectModule,
    InputTextareaModule,
    PaginatorModule,
    NgxSpinnerModule,
  ],
  providers: [
    MessageService,
    AuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
