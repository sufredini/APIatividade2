using APIatividade2.Interfaces;
using APIatividade2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIatividade2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // 1. Conexão com o Repositório (através da Interface)
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        // 2. Endpoint para Buscar Todos os Usuários (GET)
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> BuscarTodosUsuarios()
        {
            List<Usuario> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

        // 3. Endpoint para Buscar Usuário por ID (GET)
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> BuscarPorId(int id)
        {
            Usuario usuario = await _usuarioRepositorio.BuscarPorId(id);
            if (usuario == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }
            return Ok(usuario);
        }

        // 4. Endpoint para Adicionar um novo Usuário (POST)
        [HttpPost]
        public async Task<ActionResult<Usuario>> Adicionar([FromBody] Usuario usuario)
        {
            Usuario novoUsuario = await _usuarioRepositorio.Adicionar(usuario);
            return Ok(novoUsuario);
        }

        // 5. Endpoint para Atualizar um Usuário (PUT)
        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Atualizar(int id, [FromBody] Usuario usuario)
        {
            usuario.Id = id; // Garante que o ID do objeto é o mesmo da rota
            Usuario usuarioAtualizado = await _usuarioRepositorio.Atualizar(usuario, id);
            return Ok(usuarioAtualizado);
        }

        // 6. Endpoint para Apagar um Usuário (DELETE)
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _usuarioRepositorio.Apagar(id);
            if (!apagado)
            {
                return NotFound($"Usuário com ID {id} não encontrado para apagar.");
            }
            return Ok(apagado);
        }
    }
}