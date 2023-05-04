﻿namespace HealthJobs.Application.Autenticacao.DTOs
{
    public class UsuarioToken
    {
        public bool Authenticated { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Message { get; set; }
    }
}