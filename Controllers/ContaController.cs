using APIatividade2.Data;
using APIatividade2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// O namespace correto para o seu projeto
namespace APIatividade2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        // Substitua 'SeuDbContext' pelo nome real do seu DbContext,
        // por exemplo, 'APIatividade2DbContext'
        private readonly AppDbContext _dbContext;

        public ContaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            // 1. Login Estático: verifica as credenciais fixas
            if (login.Nome == "teste" && login.Email == "admin@teste.com" && login.Senha == "1234")
            {
                var token = GerarToken(login);
                return Ok(new { token });
            }

            // 2. Login Dinâmico: verifica no banco de dados
            // Certifique-se de que a sua tabela de login no banco de dados se chame 'Logins'
            var loginExistente = await _dbContext.Logins
                                                 .FirstOrDefaultAsync(u => u.Email == login.Email && u.Senha == login.Senha);

            if (loginExistente != null)
            {
                var token = GerarToken(loginExistente);
                return Ok(new { token });
            }

            // Se nenhum dos logins for bem-sucedido
            return Unauthorized();
        }

        private string GerarToken(Login login)
        {
            string chaveSecreta = "a4c9c2f6-3d8a-4b0c-9f8a-5a9e3d8f4c2f";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, login.Id.ToString()),
                new Claim(ClaimTypes.Email, login.Email),
                new Claim(ClaimTypes.Name, login.Nome),
            };

            var token = new JwtSecurityToken(
                issuer: "empresa",
                audience: "aplicacao",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credencial
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
