using HealthJobs.API.Extensions;
using HealthJobs.Application.Autenticacao.Services;
using HealthJobs.Application.Usuarios.Services;
using HealthJobs.Application.Vagas.Handlers;
using HealthJobs.Domain.Vagas.Interface;
using HealthJobs.Infra.Context;
using HealthJobs.Infra.UoW;
using HealthJobs.Infra.Vagas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var jwtAudience = env == "Production" ? Environment.GetEnvironmentVariable("Audience") : builder.Configuration["TokenConfiguration:Audience"];
var jwtIssuer = env == "Production" ? Environment.GetEnvironmentVariable("Issuer") : builder.Configuration["TokenConfiguration:Issuer"];
var jwtKey = env == "Production" ? Environment.GetEnvironmentVariable("JwtKey") : builder.Configuration["Jwt:Key"];

if (env == "Production")
{
    var databaseURL = Environment.GetEnvironmentVariable("DATABASE_URL");
    var databaseURI = new Uri(databaseURL);
    var userInfo = databaseURI.UserInfo.Split(':');
    var pgBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = databaseURI.Host,
        Port = databaseURI.Port,
        Username = userInfo[0],
        Password = userInfo[1],
        Database = databaseURI.LocalPath.TrimStart('/'),
        SslMode = SslMode.Require,
        TrustServerCertificate = true,
    };

    builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(pgBuilder.ConnectionString));
}
else
{
    builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));   
}


builder.Services.AddScoped<VagaService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IVagaRepository, VagaRepository>();
builder.Services.AddScoped<ICandidaturaRepository, CandidaturaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddErrorDescriber<IdentityPortugueseMessages>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidAudience = jwtAudience,
    ValidIssuer = jwtIssuer,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
}
);

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer 12345abcdef\""
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
