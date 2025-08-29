using APIatividade2.Interfaces;
using APIatividade2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIatividade2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public PedidoController(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> BuscarTodosPedidos()
        {
            // No repositório, vamos querer carregar os dados do usuário junto.
            var pedidos = await _pedidoRepositorio.BuscarTodosPedidos();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> BuscarPorId(int id)
        {
            // No repositório, será importante carregar os produtos deste pedido também.
            var pedido = await _pedidoRepositorio.BuscarPorId(id);
            if (pedido == null)
            {
                return NotFound($"Pedido com ID {id} não encontrado.");
            }
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> Adicionar([FromBody] Pedido pedido)
        {
            // O objeto 'pedido' que chega aqui deve conter uma lista de produtos/itens.
            // A mágica de verdade acontecerá no repositório, que salvará o pedido
            // e também os itens na tabela PedidosProdutos.
            var novoPedido = await _pedidoRepositorio.Adicionar(pedido);
            return Ok(novoPedido);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pedido>> Atualizar(int id, [FromBody] Pedido pedido)
        {
            // Geralmente, o que se atualiza em um pedido é o Status ou o Endereço.
            pedido.Id = id;
            var pedidoAtualizado = await _pedidoRepositorio.Atualizar(pedido, id);
            return Ok(pedidoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _pedidoRepositorio.Apagar(id);
            if (!apagado)
            {
                return NotFound($"Pedido com ID {id} não encontrado para apagar.");
            }
            return Ok(apagado);
        }
    }
}