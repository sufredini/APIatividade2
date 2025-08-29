using APIatividade2.Interfaces;
using APIatividade2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIatividade2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaController(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> BuscarTodasCategorias()
        {
            var categorias = await _categoriaRepositorio.BuscarTodasCategorias();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> BuscarPorId(int id)
        {
            var categoria = await _categoriaRepositorio.BuscarPorId(id);
            if (categoria == null)
            {
                return NotFound($"Categoria com ID {id} não encontrada.");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Adicionar([FromBody] Categoria categoria)
        {
            var novaCategoria = await _categoriaRepositorio.Adicionar(categoria);
            return Ok(novaCategoria);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Categoria>> Atualizar(int id, [FromBody] Categoria categoria)
        {
            categoria.Id = id;
            var categoriaAtualizada = await _categoriaRepositorio.Atualizar(categoria, id);
            return Ok(categoriaAtualizada);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _categoriaRepositorio.Apagar(id);
            if (!apagado)
            {
                return NotFound($"Categoria com ID {id} não encontrada para apagar.");
            }
            return Ok(apagado);
        }
    }
}