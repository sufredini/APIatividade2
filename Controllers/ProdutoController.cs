using APIatividade2.Interfaces;
using APIatividade2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIatividade2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> BuscarTodosProdutos()
        {
            var produtos = await _produtoRepositorio.BuscarTodosProdutos();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> BuscarPorId(int id)
        {
            var produto = await _produtoRepositorio.BuscarPorId(id);
            if (produto == null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Adicionar([FromBody] Produto produto)
        {
            var novoProduto = await _produtoRepositorio.Adicionar(produto);
            return Ok(novoProduto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> Atualizar(int id, [FromBody] Produto produto)
        {
            produto.Id = id;
            var produtoAtualizado = await _produtoRepositorio.Atualizar(produto, id);
            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _produtoRepositorio.Apagar(id);
            if (!apagado)
            {
                return NotFound($"Produto com ID {id} não encontrado para apagar.");
            }
            return Ok(apagado);
        }
    }
}