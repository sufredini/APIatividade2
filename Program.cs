using APIatividade2.Data;
using APIatividade2.Interfaces;
using APIatividade2.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models; // Adicione este using!

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext com a connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => // Adicionado 'c =>' para configurar
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIatividade2", Version = "v1" });

    // --- Início da Configuração JWT para o Swagger ---
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // Deve ser 'bearer' para JWT
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
    // --- Fim da Configuração JWT para o Swagger ---
});

// INÍCIO DA SEÇÃO ADICIONADA
// Configuração da Injeção de Dependência (Repositórios)
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
// FIM DA SEÇÃO ADICIONADA

// Adiciona e configura o serviço de autenticação JWT
// Assegura que o token será validado corretamente
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Valida o emissor do token (quem gerou)
            ValidateAudience = true, // Valida a audiência do token (para quem se destina)
            ValidateLifetime = true, // Valida o tempo de vida do token
            ValidateIssuerSigningKey = true, // Valida a assinatura do token
            ValidIssuer = "empresa", // Deve ser igual ao valor no método GerarToken()
            ValidAudience = "aplicacao", // Deve ser igual ao valor no método GerarToken()
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a4c9c2f6-3d8a-4b0c-9f8a-5a9e3d8f4c2f")) // Sua chave secreta
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adicione aqui antes do UseAuthorization()
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
